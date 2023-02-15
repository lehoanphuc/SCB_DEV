using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Web.Services3;
using System.Xml;
using Utility;
using System.Configuration;
using System.Data;
using Microsoft.Web.Services3.Addressing;
using System.IO;
using Formatters;
using DBConnection;
using System.Collections;
using System.ServiceModel.Description;
using System.Collections.ObjectModel;
using System.CodeDom.Compiler;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Reflection;
using System.Globalization;

namespace Interfaces
{
    public class WCFClient
    {
        //vutt wcf client 05102015
        private static Hashtable Instances = new Hashtable();
        private string _INSTANCENAME = string.Empty;
        private string _METHODNAME = string.Empty;
        private string _CONTRACTNAME = string.Empty;
        private string _URLWEBSERVICE = string.Empty;
        object _lockObject = new Object();
        public void CreateInstance()
        {
            lock (_lockObject) 
            { 
                try
                {
                    System.Net.ServicePointManager.DefaultConnectionLimit = 200;

                    Uri mexAddress = new Uri(_URLWEBSERVICE);
                    MetadataExchangeClientMode mexMode = MetadataExchangeClientMode.HttpGet;
                    string contractName = _CONTRACTNAME;

                    MetadataExchangeClient mexClient = new MetadataExchangeClient(mexAddress, mexMode);
                    mexClient.ResolveMetadataReferences = true;
                    MetadataSet metaSet = mexClient.GetMetadata();

                    WsdlImporter importer = new WsdlImporter(metaSet);
                    Collection<ContractDescription> contracts = importer.ImportAllContracts();
                    ServiceEndpointCollection allEndpoints = importer.ImportAllEndpoints();

                    ServiceContractGenerator generator = new ServiceContractGenerator();
                    var endpointsForContracts = new Dictionary<string, IEnumerable<ServiceEndpoint>>();

                    foreach (ContractDescription contract in contracts)
                    {
                        generator.GenerateServiceContractType(contract);
                        endpointsForContracts[contract.Name] = allEndpoints.Where(
                            se => se.Contract.Name == contract.Name).ToList();
                    }

                    if (generator.Errors.Count != 0)
                        throw new Exception("There were errors during code compilation.");

                    CodeGeneratorOptions options = new CodeGeneratorOptions();
                    options.BracingStyle = "C";
                    CodeDomProvider codeDomProvider = CodeDomProvider.CreateProvider("C#");

                    CompilerParameters compilerParameters = new CompilerParameters(
                    new string[] { 
                          "System.dll", "System.ServiceModel.dll", 
                         "System.Runtime.Serialization.dll" });
                    compilerParameters.GenerateInMemory = true;
                    compilerParameters.OutputAssembly = AppDomain.CurrentDomain.BaseDirectory + @"Temp\" + DateTime.Now.Ticks + ".dll";

                    CompilerResults results = codeDomProvider.CompileAssemblyFromDom(
                                    compilerParameters, generator.TargetCompileUnit);


                    if (results.Errors.Count > 0)
                    {
                        throw new Exception("There were errors during generated code compilation");
                    }
                    else
                    {
                        Type clientProxyType = results.CompiledAssembly.GetTypes().First(
                            t => t.IsClass &&
                               t.GetInterface(contractName) != null &&
                               t.GetInterface(typeof(ICommunicationObject).Name) != null);

                        ServiceEndpoint se = endpointsForContracts[contractName].First();

                        object instance = new object();
                        instance = results.CompiledAssembly.CreateInstance(
                            clientProxyType.Name,
                              false,
                              System.Reflection.BindingFlags.CreateInstance,
                             null,
                              new object[] { se.Binding, se.Address },
                              CultureInfo.CurrentCulture, null);
                        
                        if(Instances.ContainsKey(_INSTANCENAME))
                        {
                            Instances[_INSTANCENAME] = instance;
                        }
                        else
                        {
                            Instances.Add(_INSTANCENAME, instance);
                        }
                    
                    }
                }
                catch(Exception ex)
                {
                    Instances[_INSTANCENAME] = null;
                    Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                }
            }
        }
        public bool InvokeWCF(TransactionInfo tran)
        {
            try
            {
                //lay thong tin WS
                string condition = " DESTID = '" + tran.Data[Common.KEYNAME.DESTID].ToString() + "'";
                condition += " AND IPCTRANCODE = '" + tran.Data[Common.KEYNAME.IPCTRANCODEDEST].ToString() + "'";
                DataRow[] row = Common.DBICONNECTIONWCF.Select(condition);
                if (row.Length != 1)
                {
                    tran.ErrorCode = Common.ERRORCODE.INVALID_WCFMAST;
                    return false;
                }
                else
                {
                    _INSTANCENAME = row[0]["INSTANCENAME"].ToString();
                    _METHODNAME = row[0]["METHODNAME"].ToString();
                    _URLWEBSERVICE = row[0]["URLWEBSERVICE"].ToString();
                    _CONTRACTNAME = row[0]["CONTRACTNAME"].ToString();
                }

                //kiem tra instance khoi tao chua
                if(Instances.ContainsKey(_INSTANCENAME))
                {
                    if(Instances[_INSTANCENAME] == null)
                    {
                        CreateInstance();
                    }
                }
                else
                {
                    CreateInstance();
                }

                //truong hop khong khoi tao duoc instance tra ra giao dich loi
                if (Instances[_INSTANCENAME] == null)
                {
                    tran.ErrorCode = Common.ERRORCODE.CANNOTCREATEINSTANCE;
                    return false;
                }

                //ban msg qua gw
                if(tran.Data.ContainsKey(Common.KEYNAME.WCFOUTPUTDATA))
                {
                    //neu hoi sang wcf loi retry khoi tao lai 1 lan
                    int flag_running_fail = 0;
                    while (flag_running_fail <= 1)
                    {
                        try 
                        { 
                            TransLib.LogOutput(tran);

                            object[] output = (object[])tran.Data[Common.KEYNAME.WCFOUTPUTDATA];
                            object input = Instances[_INSTANCENAME].GetType().GetMethod(_METHODNAME).Invoke(Instances[_INSTANCENAME], output);
                            tran.InputData = FormatInputXML(input.ToString());

                            TransLib.LogInput(tran);
                            break;
                        }
                        catch (Exception e)
                        {
                            CreateInstance();
                            flag_running_fail++;
                            Utility.ProcessLog.LogError(e, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                        }
                    }
                    
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                if (ex.GetType().FullName.Equals("System.TimeoutException"))
                {
                    Connection con = new Connection();
                    con.ExecuteNonquery(Common.ConStr, "IPC_CHECKREVERSAL", tran.IPCTransID, tran.Data[Common.KEYNAME.IPCTRANCODE].ToString(), Common.ERRORCODE.SYSTEM);
                }
                return false;
            }
            return true;
        }

        private string FormatInputXML(string input)
        {
            input = input.Replace("\r\n", "");
            input = input.Replace("<", "&lt;");
            input = input.Replace(">", "&gt;");
            return @"<string xmlns=""http://tempuri.org/"">" + input + "</string>";
        }
    }
}
