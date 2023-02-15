using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using SmartPortal.DAL;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using Microsoft.VisualBasic.FileIO;
using System.Security.Cryptography;

namespace SmartPortal.SEMS
{
    public class Topup
    {
        public DataSet GetTopUp(string telco,string amount,string serialno,string softpin,ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                object[] para = new object[] { telco, amount, serialno, softpin };
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000113");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add("STORENAME", "TU_TOPUP_SELECT");
                hasInput.Add("PARA", para);

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATASET];
                    errorCode = "0";
                }
                else
                {
                    errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                    errorDesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                }
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }  
        }
        public DataSet GetTecoList(ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                object[] para = new object[0];
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000113");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add("STORENAME", "TU_TELCO_SELECT");
                hasInput.Add("PARA", para);    

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATASET];
                    errorCode = "0";
                }
                else
                {
                    errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                    errorDesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                }
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteTopupCardbySerial(string serial, string usermodify, string datemodify, ref string errorCode,
            ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                object[] para = new object[]{serial,usermodify,datemodify};
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000113");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add("STORENAME", "TU_DELETECARD_BYSERIAL");
                hasInput.Add("PARA", para);

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    errorCode = "0";
                }
                else
                {
                    errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                    errorDesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetAmountbyTelco(string TelcoID, string Type, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                object[] para = new object[] { TelcoID, Type };
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000113");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add("STORENAME", "TU_CARDAMOUNT_SELECTBYTELCOID");
                hasInput.Add("PARA", para);

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATASET];
                    errorCode = "0";
                }
                else
                {
                    errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                    errorDesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                }
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ImportTopup(string userid,string filepath,string telco,string amount,string buyrate,string sellrate, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                DataTable dt = new DataTable("TOP_Topup");
                string rptpath="";

                dt = TopupGetData(userid,filepath,telco,amount,buyrate,sellrate,ref rptpath);

                if (dt.Rows.Count > 0)
                {
                    hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSIMPORTTOPUP");
                    hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                    hasInput.Add(SmartPortal.Constant.IPC.DATATABLE, dt);
                    hasInput.Add(SmartPortal.Constant.IPC.TELCO, telco);
                    hasInput.Add(SmartPortal.Constant.IPC.AMOUNT, amount);

                    hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);
                }
                else
                {
                    errorCode = "4997";
                }

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    errorCode = "0";
                }
                else
                {
                    errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                    errorDesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                }

                return rptpath;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public DataTable TopupGetData(string userid,string path,string telco,string amount,string buyrate,string sellrate, ref string rptpath)
        {
            DataTable dt = new DataTable("TOP_Topup");
            DataTable dtrpt = new DataTable("Topup_Report");

            try
            {
                DataTable tmp = GetDataTabletFromCSVFile(path);

                dt.Columns.Add("TopupID", typeof(string));
                dt.Columns.Add("TelcoID", typeof(Int32));
                dt.Columns.Add("CardType", typeof(string));
                dt.Columns.Add("SoftPin", typeof(String));
                dt.Columns.Add("SerialNo", typeof(String));
                dt.Columns.Add("BuyRate", typeof(String));
                dt.Columns.Add("SellRate", typeof(String));
                dt.Columns.Add("DateCreated", typeof(String));
                dt.Columns.Add("UserCreated", typeof(String));
                dt.Columns.Add("ExpireTime", typeof(String));
                dt.Columns.Add("DateModified", typeof(String));
                dt.Columns.Add("UserModified", typeof(String));
                dt.Columns.Add("Status", typeof(String));
                dt.Columns.Add("IsUsed", typeof(bool));

                dtrpt.Columns.Add("SoftPin", typeof(String));
                dtrpt.Columns.Add("SerialNo", typeof(String));
                dtrpt.Columns.Add("DateCreated", typeof(String));
                dtrpt.Columns.Add("UserCreated", typeof(String));
                dtrpt.Columns.Add("ExpireTime", typeof(String));
                dtrpt.Columns.Add("Status", typeof(String));

                foreach (DataRow dr in tmp.Rows)
                {
                    //dt import vao data base
                    DataRow workRow = dt.NewRow();
                    workRow["TopupID"] = dr["SerialNo"];
                    workRow["TelcoID"] = telco;
                    workRow["CardType"] = amount;
                    workRow["SoftPin"] = EncryptsoftPin(dr["SoftPin"].ToString());
                    workRow["SerialNo"] = dr["SerialNo"];
                    workRow["BuyRate"] = buyrate;
                    workRow["SellRate"] = sellrate;
                    workRow["DateCreated"] = DateTime.Now.ToShortDateString();
                    workRow["UserCreated"] = userid;
                    workRow["ExpireTime"] = dr["ExpireTime"];
                    workRow["DateModified"] = "";
                    workRow["UserModified"] = "";
                    workRow["Status"] = "A";
                    workRow["IsUsed"] = false;
                    dt.Rows.Add(workRow);

                    //dt report
                    string sp = dr["SoftPin"].ToString();
                    sp = "********" + sp.Substring(sp.Length - 8, 8);

                    DataRow workRowrpt = dtrpt.NewRow();
                    workRowrpt["SoftPin"] = sp;
                    workRowrpt["SerialNo"] = dr["SerialNo"].ToString();
                    workRowrpt["DateCreated"] = DateTime.Now.ToShortDateString();
                    workRowrpt["UserCreated"] = userid;
                    workRowrpt["ExpireTime"] = dr["ExpireTime"].ToString();
                    workRowrpt["Status"] = "Success";
                    dtrpt.Rows.Add(workRowrpt);

                }
                rptpath = CreateReportFile(dtrpt, path);
                return dt;
            }
            catch (Exception ex)
            {
                return new DataTable();
            }
        }

        public string CreateReportFile(DataTable dtrpt, string uri)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                uri = uri.Substring(0, uri.LastIndexOf(".")) + "_rpt_" + DateTime.Now.ToString("dd-MM-yyyy H-mm-ss") + ".csv";

                string[] columnNames = dtrpt.Columns.Cast<DataColumn>().
                                                  Select(column => column.ColumnName).
                                                  ToArray();
                sb.AppendLine(string.Join(",", columnNames));

                foreach (DataRow row in dtrpt.Rows)
                {
                    string[] fields = row.ItemArray.Select(field => field.ToString()).
                                                    ToArray();
                    sb.AppendLine(string.Join(",", fields));
                }
                File.WriteAllText(uri, sb.ToString());

                return uri;
            }
            catch (Exception ex)
            {
                return ex.ToString() + uri;
            }
        }

        private static DataTable GetDataTabletFromCSVFile(string csv_file_path)
        {
            DataTable csvData = new DataTable();
            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(csv_file_path))
                {
                    csvReader.SetDelimiters(new string[] { "," });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    string[] colFields = csvReader.ReadFields();
                    foreach (string column in colFields)
                    {
                        DataColumn datecolumn = new DataColumn(column);
                        datecolumn.AllowDBNull = true;
                        csvData.Columns.Add(datecolumn);
                    }
                    while (!csvReader.EndOfData)
                    {
                        string[] fieldData = csvReader.ReadFields();
                        //Making empty value as null
                        for (int i = 0; i < fieldData.Length; i++)
                        {
                            if (fieldData[i] == "")
                            {
                                fieldData[i] = null;
                            }
                        }
                        csvData.Rows.Add(fieldData);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return csvData;
        }

        public static string EncryptsoftPin(string Data)
        {
            try
            {
                byte[] PP = Encoding.Unicode.GetBytes("IPC");
                byte[] DataByte = Encoding.Unicode.GetBytes(Data);
                HashAlgorithm HashPassword = HashAlgorithm.Create("MD5");
                byte[] V = { 0, 9, 0, 4, 4, 9, 5, 9, 4, 2, 2, 2, 0, 6, 8, 2 };
                RijndaelManaged EncryptData = new RijndaelManaged();
                EncryptData.Key = HashPassword.ComputeHash(PP);
                ICryptoTransform encryptor = EncryptData.CreateEncryptor(EncryptData.Key, V);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
                cs.Write(DataByte, 0, DataByte.Length);
                cs.FlushFinalBlock();
                byte[] Result = ms.ToArray();
                ms.Close();
                cs.Close();
                EncryptData.Clear();
                return Convert.ToBase64String(Result);
            }
            catch
            {
                return "";
            }
        }
        public Hashtable BuyTopupOnline(string UserID, string SenderAccount, string ReceiverAccount, string CCYID, string senderName, string receiverName, string debitBrachID, string creditBrachID, string Desc, string authentype, string authencode, string telco, string amount, string PhoneNo)
        {
            Hashtable result = new Hashtable();
            try
            {
                Hashtable input = new Hashtable();

                //SerialNo for Ooredoo with format: Sign + yyyyMMddhhmmss + 6 digits serial number For example: T20120809101010002313
                Random generator = new Random();
                String r = generator.Next(0, 1000000).ToString("D6");
                string SeriaNo = "T" + DateTime.Now.ToString("yyyyMMddhhmmss") + r;

                input.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000035");
                input.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                input.Add(SmartPortal.Constant.IPC.USERID, UserID);
                input.Add(SmartPortal.Constant.IPC.ACCTNO, SenderAccount);
                input.Add(SmartPortal.Constant.IPC.CCYID, CCYID);
                input.Add(SmartPortal.Constant.IPC.SENDERNAME, senderName);
                input.Add(SmartPortal.Constant.IPC.RECEIVERNAME, receiverName);
                input.Add(SmartPortal.Constant.IPC.CREDITBRACHID, creditBrachID);
                input.Add(SmartPortal.Constant.IPC.DEBITBRACHID, debitBrachID);
                input.Add(SmartPortal.Constant.IPC.TRANDESC, Desc);
                input.Add(SmartPortal.Constant.IPC.AUTHENTYPE, authentype);
                input.Add(SmartPortal.Constant.IPC.AUTHENCODE, authencode);
                input.Add(SmartPortal.Constant.IPC.TELCO, telco);
                input.Add(SmartPortal.Constant.IPC.AMOUNT, amount);
                input.Add(SmartPortal.Constant.IPC.BUYRATE, amount);
                input.Add(SmartPortal.Constant.IPC.PHONENO, PhoneNo);
                input.Add(SmartPortal.Constant.IPC.SERIALNO, SeriaNo);

                result = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(input);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public DataSet GetSupplierList(ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                object[] para = new object[0];
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000113");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add("STORENAME", "GetSupplierID");
                hasInput.Add("PARA", para);

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATASET];
                    errorCode = "0";
                }
                else
                {
                    errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                    errorDesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                }
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetAllGroupId(ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                object[] para = new object[0];
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "Get_GroupIdPrefix");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);
                DataSet ds = new DataSet();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATASET];
                    errorCode = "0";
                }
                else
                {
                    errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                    errorDesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                }
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
