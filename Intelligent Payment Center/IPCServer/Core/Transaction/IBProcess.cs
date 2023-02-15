using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;
using System.Data;
using DBConnection;
using Formatters;
using System.Collections;
using Interfaces;
using Schedules;
using System.Configuration;
using System.Reflection;

namespace Transaction
{
    public class IBProcess
    {
        Assembly assembly = null;
        Type type = null;
        string method = string.Empty;
        object instance = null;
        object result = null;
        int count = 0;

        AutoTrans Autotrans = new AutoTrans();
        public bool ExcessSweeping(TransactionInfo esTran)
        {
            string errorcode = "0";
            string errordesc = "";
            Double subAmount = 0;
            Double cenAmount = 0;
            Double Amount = 0;
            Double surLvl = 0;
            Double shrLvl = 0;
            count = 0;
            int nRetry = (ConfigurationManager.AppSettings.Get("NumberRetry") == "") ? 3 : int.Parse(ConfigurationManager.AppSettings.Get("NumberRetry"));

            try
            {
                do
                {
                    count++;
                    errorcode = "0";
                    subAmount = 0;
                    cenAmount = 0;
                    errordesc = "";
                    Amount = 0;

                    surLvl = Double.Parse(esTran.Data[Common.KEYNAME.SURLEVEL].ToString());
                    shrLvl = Double.Parse(esTran.Data[Common.KEYNAME.SHRLEVEL].ToString());

                    //check tien tai khoan phu
                    Hashtable subOutputData = CheckAccountInfor(esTran.Data[Common.KEYNAME.SUBACCTNO].ToString());
                    if (subOutputData[Common.KEYNAME.IPCERRORCODE].Equals("0") && ((DataSet)subOutputData[Common.KEYNAME.DATARESULT]).Tables.Count > 0)
                    {
                        DataSet dsSub = (DataSet)subOutputData[Common.KEYNAME.DATARESULT];
                        if (dsSub.Tables[0].Rows.Count > 0)
                        {
                            subAmount = Double.Parse(dsSub.Tables[0].Rows[0][Common.KEYNAME.AVAILABLEBAL].ToString());
                        }
                        else
                        {
                            Utility.ProcessLog.LogInformation("Get sub account sucess but no data " + subOutputData[Common.KEYNAME.IPCERRORDESC].ToString() + esTran.Data[Common.KEYNAME.SUBACCTNO].ToString());
                            errorcode = Common.ERRORCODE.SYSTEM;
                            continue;
                        }
                    }
                    else
                    {
                        Utility.ProcessLog.LogInformation("Get sub account error " + subOutputData[Common.KEYNAME.IPCERRORDESC].ToString() + esTran.Data[Common.KEYNAME.SUBACCTNO].ToString());
                        errorcode = Common.ERRORCODE.SYSTEM;
                        errordesc = subOutputData[Common.KEYNAME.IPCERRORDESC].ToString();
                        continue;
                    }

                    //check tien tai khoan chinh
                    Hashtable cenOutputData = CheckAccountInfor(esTran.Data[Common.KEYNAME.CENACCTNO].ToString());
                    if (cenOutputData[Common.KEYNAME.IPCERRORCODE].Equals("0") && ((DataSet)cenOutputData[Common.KEYNAME.DATARESULT]).Tables.Count > 0)
                    {
                        DataSet dsCen = (DataSet)cenOutputData[Common.KEYNAME.DATARESULT];
                        if (dsCen.Tables[0].Rows.Count > 0)
                        {
                            cenAmount = Double.Parse(dsCen.Tables[0].Rows[0][Common.KEYNAME.AVAILABLEBAL].ToString());
                        }
                        else
                        {
                            Utility.ProcessLog.LogInformation("Get central account success but no data " + cenOutputData[Common.KEYNAME.IPCERRORDESC].ToString() + esTran.Data[Common.KEYNAME.CENACCTNO].ToString());
                            errorcode = Common.ERRORCODE.SYSTEM;
                            continue;
                        }
                    }
                    else
                    {
                        Utility.ProcessLog.LogInformation("Get central account error " + cenOutputData[Common.KEYNAME.IPCERRORDESC].ToString() + esTran.Data[Common.KEYNAME.CENACCTNO].ToString());
                        errorcode = Common.ERRORCODE.SYSTEM;
                        errordesc = cenOutputData[Common.KEYNAME.IPCERRORDESC].ToString();
                        continue;
                    }

                    Hashtable hasOutput = new Hashtable();

                    //check tai khoan phu thieu tien, chuyen tien tu tai khoan chinh sang
                    if (subAmount < shrLvl)
                    {
                        Amount = (cenAmount < shrLvl - subAmount) ? cenAmount : shrLvl - subAmount;
                        hasOutput = ESTransfer(esTran.Data[Common.KEYNAME.CENACCTNO].ToString(), esTran.Data[Common.KEYNAME.SUBACCTNO].ToString(), Amount, esTran);
                    }
                    //check tai khoan phu thua tien chuyen sang tai khoan chinh
                    else if (subAmount > surLvl)
                    {
                        Amount = subAmount - surLvl;
                        hasOutput = ESTransfer(esTran.Data[Common.KEYNAME.SUBACCTNO].ToString(), esTran.Data[Common.KEYNAME.CENACCTNO].ToString(), Amount, esTran);
                    }
                    else
                    {
                        return true;
                    }

                    //kiem tra ket qua chuyen tien
                    errorcode = hasOutput[Common.KEYNAME.IPCERRORCODE].ToString();
                    errordesc = hasOutput[Common.KEYNAME.IPCERRORDESC].ToString();
                }
                while (count < nRetry && errorcode != "0");

                //tra ve ket qua
                if (errorcode.Equals("0"))
                {
                    return true;
                }
                else
                {
                    esTran.ErrorCode = errorcode;
                    esTran.ErrorDesc = errordesc;
                    Utility.ProcessLog.LogInformation("Transfer error " + esTran.Data[Common.KEYNAME.SUBACCTNO].ToString() + " - " + esTran.Data[Common.KEYNAME.CENACCTNO].ToString() + " : " + Amount.ToString() + " EC: " + errorcode + " ED: " + errordesc);
                    //throw new Exception("[ES] Error : " + esTran.Data[Common.KEYNAME.SUBACCTNO].ToString() + " - " + esTran.Data[Common.KEYNAME.CENACCTNO].ToString() + " : " + Amount.ToString());
                    return false;
                }
            }
            catch (Exception ex)
            {
                esTran.SetErrorInfo(ex);
                Utility.ProcessLog.LogInformation(ex.ToString());
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
        }
        public Hashtable CheckAccountInfor(string acctno)
        {
            Hashtable hasInput = new Hashtable();
            Hashtable hasOutput = new Hashtable();

            hasInput.Add(Common.KEYNAME.IPCTRANCODE, "IB000100");
            hasInput.Add(Common.KEYNAME.SOURCEID, Common.KEYNAME.SOURCEIBVALUE);
            hasInput.Add(Common.KEYNAME.ACCTNO, acctno);

            hasOutput = Autotrans.ProcessTransHAS(hasInput);

            return hasOutput;
        }

        public Hashtable ESTransfer(string Sender, string Receiver, Double Amount, TransactionInfo esTran)
        {
            Hashtable hasInput = new Hashtable();
            Hashtable hasOutput = new Hashtable();

            hasInput.Add(Common.KEYNAME.IPCTRANCODE, "IB000216");
            hasInput.Add(Common.KEYNAME.SOURCEID, Common.KEYNAME.SOURCEIBVALUE);
            hasInput.Add(Common.KEYNAME.USERID, esTran.Data[Common.KEYNAME.USERID].ToString());
            hasInput.Add(Common.KEYNAME.ACCTNO, Sender);
            hasInput.Add(Common.KEYNAME.RECEIVERACCOUNT, Receiver);
            hasInput.Add(Common.KEYNAME.AMOUNT, Amount.ToString());
            hasInput.Add(Common.KEYNAME.CCYID, esTran.Data[Common.KEYNAME.CCYID].ToString());
            hasInput.Add(Common.KEYNAME.SEDFEE, esTran.Data[Common.KEYNAME.SEDFEE].ToString());
            hasInput.Add(Common.KEYNAME.REVFEE, esTran.Data[Common.KEYNAME.REVFEE].ToString());
            hasInput.Add(Common.KEYNAME.SENDERNAME, esTran.Data[Common.KEYNAME.SENDERNAME].ToString());
            hasInput.Add(Common.KEYNAME.RECEIVERNAME, esTran.Data[Common.KEYNAME.RECEIVERNAME].ToString());
            hasInput.Add(Common.KEYNAME.CREDITBRACHID, esTran.Data[Common.KEYNAME.CREDITBRACHID].ToString());
            hasInput.Add(Common.KEYNAME.DEBITBRACHID, esTran.Data[Common.KEYNAME.DEBITBRACHID].ToString());
            hasInput.Add(Common.KEYNAME.TRANDESC, esTran.Data[Common.KEYNAME.TRANDESC].ToString());
            hasInput.Add(Common.KEYNAME.ISSCHEDULE, "Y");
            hasInput.Add(Common.KEYNAME.SCHEDULEID, esTran.Data[Common.KEYNAME.SCHEDULEID].ToString());

            hasOutput = Autotrans.ProcessTransHAS(hasInput);

            return hasOutput;
        }
        public static bool SendMailSweeping(TransactionInfo tran)
        {
            try
            {
                //minh add 26.11.2015 if config=false-> don't send mail:
                if (!bool.Parse(ConfigurationManager.AppSettings["SendEmailafterSweeping"]))
                {
                    Utility.ProcessLog.LogInformation("======>  Don't send mail by config");
                    return true;
                    
                }
                //if (tran.Status == Common.TRANSTATUS.FINISH)
                //{
                Connection con = new Connection();
                DataTable sweepinnginfor = con.FillDataTable(Common.ConStr, "eba_get_sweeping_infor_to_sendmail", DateTime.Now.ToString("dd/MM/yyyy"),
                    tran.Data[Common.KEYNAME.SCHEDULEID].ToString(), tran.Data[Common.KEYNAME.CENACCTNO].ToString(), tran.Data["CONTRACTNO"].ToString(), tran.Data["IPCTRANSID"].ToString());
                Utility.ProcessLog.LogInformation("send input:" + DateTime.Now.ToString("dd/MM/yyyy") + "-" + tran.Data[Common.KEYNAME.SCHEDULEID] + "-" + tran.Data[Common.KEYNAME.CENACCTNO] + "-" + tran.Data["IPCTRANSID"].ToString());
                string email = sweepinnginfor.Rows[0]["EMAIL"].ToString();
                //string email = "minhnt@jits.vn";
                if (string.IsNullOrEmpty(email))
                {
                    Utility.ProcessLog.LogInformation("======>  There is no email for contract " + tran.Data["CONTRACTNO"].ToString());
                    return false;
                }


                Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();
                if (!sweepinnginfor.Rows[0]["ERRORCODE"].ToString().Equals("0"))
                {
                    tmpl = Common.GetEmailTemplate("transactionSweepingerror");

                    tmpl.SetAttribute("errordesc", sweepinnginfor.Rows[0]["ERRORDESC"].ToString());




                }
                else
                {
                    tmpl = Common.GetEmailTemplate("transactionSweeping");
                    
                    tmpl.SetAttribute("sweeptype", sweepinnginfor.Rows[0]["sweeptype"].ToString());
                    tmpl.SetAttribute("ccyid", tran.Data.ContainsKey("CCYID") ? tran.Data["CCYID"].ToString() : "");
                    tmpl.SetAttribute("amount", sweepinnginfor.Rows[0]["NUM01INT"].ToString());
                    //tmpl.SetAttribute("amountchu", Utility.Common.IntegerToWords(long.Parse(sweepinnginfor.Rows[0]["NUM01INT"].ToString())));
                    tmpl.SetAttribute("feeType", sweepinnginfor.Rows[0]["feetype"].ToString());
                    tmpl.SetAttribute("feeAmount", sweepinnginfor.Rows[0]["feeamount"].ToString());
                    tmpl.SetAttribute("desc", tran.Data.ContainsKey("TRANDESC") ? tran.Data["TRANDESC"].ToString() : "");
                }

                tmpl.SetAttribute("senderName", tran.Data.ContainsKey("SENDERNAME") ? tran.Data["SENDERNAME"].ToString() : "");
                tmpl.SetAttribute("contractno", tran.Data.ContainsKey("CONTRACTNO") ? tran.Data["CONTRACTNO"].ToString() : "");
                tmpl.SetAttribute("senderBranch", tran.Data.ContainsKey("DEBITBRACHID") ? tran.Data["DEBITBRACHID"].ToString() : "");
                tmpl.SetAttribute("CENACCTNO", tran.Data.ContainsKey("CENACCTNO") ? tran.Data["CENACCTNO"].ToString() : "");
                tmpl.SetAttribute("SUBACCTNO", tran.Data.ContainsKey("SUBACCTNO") ? tran.Data["SUBACCTNO"].ToString() : "");
                tmpl.SetAttribute("SURLEVEL", tran.Data.ContainsKey("SURLEVEL") ? tran.Data["SURLEVEL"].ToString() : "");
                tmpl.SetAttribute("SHRLEVEL", tran.Data.ContainsKey("SHRLEVEL") ? tran.Data["SHRLEVEL"].ToString() : "");
                tmpl.SetAttribute("tranDate", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")); //doan nay sua sau
                tmpl.SetAttribute("tranID", sweepinnginfor.Rows[0]["IPCTRANSID"].ToString());
                tmpl.SetAttribute("SCHEDULEID", tran.Data.ContainsKey("SCHEDULEID") ? tran.Data["SCHEDULEID"].ToString() : "");

                tmpl.SetAttribute("StatusE", sweepinnginfor.Rows[0]["StatusE"].ToString());






                //tmpl.SetAttribute("senderAccount",
                //    tran.Data.ContainsKey(Common.KEYNAME.ACCTNO) ? tran.Data[Common.KEYNAME.ACCTNO].ToString() : "");

                //tmpl.SetAttribute("senderBalance",
                //    tran.Data.ContainsKey("REFGLDEBIT") ? tran.Data["REFGLDEBIT"].ToString() : "");

                //tmpl.SetAttribute("ccyid",
                //    tran.Data.ContainsKey("currencyid") ? tran.Data["currencyid"].ToString() : "");

                //tmpl.SetAttribute("senderName", userInfo.Rows[0]["FullName"].ToString());

                //tmpl.SetAttribute("recieverAccount",
                //    tran.Data.ContainsKey("RECEIVERACCOUNT") ? tran.Data["RECEIVERACCOUNT"].ToString() : "");

                //tmpl.SetAttribute("amount", tran.Data.ContainsKey("AMOUNT") ? tran.Data["AMOUNT"].ToString() : "");

                //tmpl.SetAttribute("amountchu", Utility.Common.IntegerToWords(long.Parse(tran.Data["AMOUNT"].ToString())));

                //tmpl.SetAttribute("feeAmount",
                //    tran.Data.ContainsKey("feeSenderAmt") ? tran.Data["feeSenderAmt"].ToString() : "");

                //tmpl.SetAttribute("feeType", tran.Data["feeReceiverAmt"] == "0" ? "Receiver" : "Sender");


                //tmpl.SetAttribute("desc", tran.Data.ContainsKey("TRANDESC") ? tran.Data["TRANDESC"].ToString() : "");

                //tmpl.SetAttribute("tranID",
                //    tran.Data.ContainsKey("IPCTRANSID") ? tran.Data["IPCTRANSID"].ToString() : "");

                //tmpl.SetAttribute("tranDate", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

                ////lay branch nguoi gui
                //DataTable dtSenderBranch = con.FillDataTable(Common.ConStr, "SEMS_EBA_BRANCH_DETAILS",
                //    tran.Data["chinhanh"].ToString());
                //tmpl.SetAttribute("senderBranch", dtSenderBranch.Rows[0]["BRANCHNAME"].ToString());

                //try
                //{
                //    string creditBranch;
                //    //lay ten nguoi nhan
                //    DataTable revinfo = con.FillDataTable(Common.ConStr, "MB_GETINFO_BYACCNO", tran.Data["RECEIVERACCOUNT"].ToString());
                //    if (revinfo.Columns.Contains("FullName") && revinfo.Rows.Count > 0)
                //    {
                //        tmpl.SetAttribute("recieverName", revinfo.Rows[0]["FullName"].ToString());
                //        creditBranch = revinfo.Rows[0]["BranchID"].ToString();
                //    }
                //    else
                //    {
                //        tmpl.SetAttribute("recieverName", tran.Data.ContainsKey("RECEIVERNAME") ? tran.Data["RECEIVERNAME"].ToString() : "");
                //        creditBranch = tran.Data.ContainsKey("CREDITBRACHID") ? tran.Data["CREDITBRACHID"].ToString() : "";
                //    }

                //lay branch nguoi nhan
                //DataTable dtReceiverBranch = con.FillDataTable(Common.ConStr, "SEMS_EBA_BRANCH_DETAILS", creditBranch);
                //if (dtReceiverBranch.Columns.Contains("BranchName") && dtReceiverBranch.Rows.Count > 0)
                //    tmpl.SetAttribute("receiverBranch", dtReceiverBranch.Rows[0]["BranchName"].ToString());
                //else
                //    tmpl.SetAttribute("receiverBranch", "");
                //}
                //catch { }


                Common.SendMailMessageAsync(ConfigurationManager.AppSettings["contractapprovemailfrom"], email,
                    ConfigurationManager.AppSettings["contractapprovemailtitle"], tmpl.ToString());

                Utility.ProcessLog.LogInformation("======>  Send mail sweeping transfer success " + email);
                return true;
                //}
                //else
                //{
                //    return false;
                //}
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(),
                    System.Reflection.MethodBase.GetCurrentMethod().Name);
                Utility.ProcessLog.LogInformation("======>  Send mail sweeping transfer fail");
                return false;
            }
        }
    }

}
