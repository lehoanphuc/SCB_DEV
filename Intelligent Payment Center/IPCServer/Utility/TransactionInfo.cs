using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Collections;
using System.Data;
using DBConnection;

namespace Utility
{
    public class TransactionInfo
    {
        #region Public Variable
        public string InputData;
        public string OutputData="";
        public string MessageTypeSource;
        public string MessageTypeDest;
        public Hashtable Data = new Hashtable();

        public long IPCTransID = -1;
        public long IPCTransIDReversal;
        public string MessageInfo ="";
        public string ErrorCode = "0";
        public string ErrorDesc = "";
        public bool Online = true;
        public string Status = Common.TRANSTATUS.BEGIN;

        public object[] parm; // Su dung cho SmartBank
        #endregion

        #region Public Function
        public void NewIPCTransID()
        {
            IPCTransID = Common.GetIPCTransID();
            Data[Common.KEYNAME.IPCTRANSID] = IPCTransID;
        }

        public void SetErrorInfo(Exception exc)
        {
            try
            {
                this.Status = Common.TRANSTATUS.ERROR;
                if (exc.Message.IndexOf("IPCERROR") >= 0)
                {
                    string [] arrStrings = exc.Message.Split('#');
                    this.ErrorDesc = arrStrings[1];
                    this.ErrorCode = arrStrings[0].Split('=')[1];
                    if (arrStrings.Length > 2)
                    {
                        foreach (var item in arrStrings[2].Split('$'))
                        {
                            if (this.Data.ContainsKey(item.Split('=')[0]))
                            {
                                this.Data[item.Split('=')[0]] = item.Split('=')[1];
                            }
                            else
                            {
                                this.Data.Add(item.Split('=')[0], item.Split('=')[1]);
                            }
                        }
                    }
                    if (this.Data.ContainsKey(Common.KEYNAME.ERRORCODE))
                    {
                        this.Data[Common.KEYNAME.ERRORCODE] = this.ErrorCode;
                    }
                    else
                    {
                        this.Data.Add(Common.KEYNAME.ERRORCODE, this.ErrorCode);
                    }

                    if (this.Data.ContainsKey(Common.KEYNAME.ERRORDESC))
                    {
                        this.Data[Common.KEYNAME.ERRORDESC] = this.ErrorDesc;
                    }
                    else
                    {
                        this.Data.Add(Common.KEYNAME.ERRORDESC, this.ErrorDesc);
                    }
                }
                else
                {
                    try
                    {                        
                        double errcode = 0;
                        bool isnumber = double.TryParse(exc.Message, out errcode);
                        if (isnumber == true)
                        {
                            this.ErrorCode = (exc.Message.Equals("0")) ? Common.ERRORCODE.SYSTEM : exc.Message;
                            this.ErrorDesc = exc.Message;                            
                        }else{
                            this.ErrorCode = Common.ERRORCODE.SYSTEM;
                            this.ErrorDesc = exc.Message;
                        }
                    }
                    catch (Exception e)
                    {                        
                        this.ErrorCode = Common.ERRORCODE.SYSTEM;
                        this.ErrorDesc = exc.Message;
                    }
                    if (exc.InnerException != null)
                    {
                        this.ErrorDesc = this.ErrorDesc + " (InnerException: " + exc.InnerException.Message + ")";
                    }
                    //vutran 29062015 add errorcode to data
                    try
                    {
                        if(this.Data.ContainsKey(Common.KEYNAME.ERRORCODE))
                        {
                            this.Data[Common.KEYNAME.ERRORCODE] = this.ErrorCode;
                        }
                        else
                        {
                            this.Data.Add(Common.KEYNAME.ERRORCODE, this.ErrorCode);
                        }

                        if (this.Data.ContainsKey(Common.KEYNAME.ERRORDESC))
                        {
                            this.Data[Common.KEYNAME.ERRORDESC] = this.ErrorDesc;
                        }
                        else
                        {
                            this.Data.Add(Common.KEYNAME.ERRORDESC, this.ErrorDesc);
                        }
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        public void SetErrorInfo(string ErrorCode, string ErrorDesc)
        {
            this.Status = Common.TRANSTATUS.ERROR;
            this.ErrorCode = ErrorCode;
            this.ErrorDesc = ErrorDesc;

            //vutran 29062015 add errorcode to data
            try
            {
                if (this.Data.ContainsKey(Common.KEYNAME.ERRORCODE))
                {
                    this.Data[Common.KEYNAME.ERRORCODE] = this.ErrorCode;
                }
                else
                {
                    this.Data.Add(Common.KEYNAME.ERRORCODE, this.ErrorCode);
                }

                if (this.Data.ContainsKey(Common.KEYNAME.ERRORDESC))
                {
                    this.Data[Common.KEYNAME.ERRORDESC] = this.ErrorDesc;
                }
                else
                {
                    this.Data.Add(Common.KEYNAME.ERRORDESC, this.ErrorDesc);
                }
            }
            catch { }
        }

        public void MappingDestErrorCode()
        {
            try
            {
                DataRow[] errorMapping = Common.DBIERRORLISTDEST.Select("DESTERRORCODE = '" + this.ErrorCode + "'" +
                    "AND (IPCTRANCODE = '" + this.Data[Common.KEYNAME.IPCTRANCODEDEST].ToString() + "' OR IPCTRANCODE = '') " +
                    "AND (DESTID = '" + this.Data[Common.KEYNAME.DESTID].ToString() + "' OR DESTID = '')");
                this.Data[Common.KEYNAME.DESTERRORCODE] = this.ErrorCode;
                if (errorMapping.Length > 0)
                {
                    this.ErrorCode = errorMapping[0]["SYSERRORCODE"].ToString();
                    this.ErrorDesc = errorMapping[0]["DESTERRORDESC"].ToString();
                }
                else if (this.ErrorCode != Common.ERRORCODE.OK)
                {
                    this.ErrorCode = Common.ERRORCODE.DESTSYSTEM;
                    //vutt add error desc from dest
                    if (this.Data.ContainsKey(Common.KEYNAME.ERRORDESC))
                    {
                        this.ErrorDesc = this.Data[Common.KEYNAME.ERRORDESC].ToString();
                    }
                }
                this.Data[Common.KEYNAME.IPCERRORCODE] = this.ErrorCode;
                this.Data[Common.KEYNAME.IPCERRORDESC] = this.ErrorDesc;

                this.Data[Common.KEYNAME.ERRORCODE] = this.ErrorCode;
                this.Data[Common.KEYNAME.ERRORDESC] = this.ErrorDesc;
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        #endregion
    }
}