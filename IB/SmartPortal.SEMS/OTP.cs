using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Data.SqlClient;
using SmartPortal.DAL;
using System.Collections;

namespace SmartPortal.SEMS
{
    public class OTP
    {
        #region search
        public DataTable Search(string userFullname,string status,string authenType,string authenCode,string userID, string style)
        {
            try
            {
                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@FULLNAME";
                p1.Value = userFullname;
                p1.SqlDbType = SqlDbType.NVarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@STATUS";
                p2.Value = status;
                p2.SqlDbType = SqlDbType.VarChar;

                SqlParameter p3 = new SqlParameter();
                p3.ParameterName = "@AUTHENTYPE";
                p3.Value = authenType;
                p3.SqlDbType = SqlDbType.VarChar;

                SqlParameter p4 = new SqlParameter();
                p4.ParameterName = "@AUTHENCODE";
                p4.Value = authenCode;
                p4.SqlDbType = SqlDbType.VarChar;

                SqlParameter p5 = new SqlParameter();
                p5.ParameterName = "@USERID";
                p5.Value = userID;
                p5.SqlDbType = SqlDbType.VarChar;

                SqlParameter p6 = new SqlParameter();
                p6.ParameterName = "@STYLE";
                p6.Value = style;
                p6.SqlDbType = SqlDbType.VarChar;

                return DataAccess.GetFromDataTable("SEMS_EBA_USERAUTHEN_SEARCH", p1,p2,p3,p4,p5,p6);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region MAP OTP FOR USER
        public DataSet ViewOTP(string userID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSVIEWOTP");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Thêm OTP cho User");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");


                hasInput.Add(SmartPortal.Constant.IPC.USERID, userID);


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
        #endregion

        #region search user for otp
        public DataTable SearchUser(string userFullname, string usertype, string email, string phone,string userlevel,string status,string contractno,string userid, string brid)
        {
            try
            {
                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@FULLNAME";
                p1.Value = userFullname;
                p1.SqlDbType = SqlDbType.NVarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@USERTYPE";
                p2.Value = usertype;
                p2.SqlDbType = SqlDbType.VarChar;

                SqlParameter p3 = new SqlParameter();
                p3.ParameterName = "@EMAIL";
                p3.Value = email;
                p3.SqlDbType = SqlDbType.VarChar;

                SqlParameter p4 = new SqlParameter();
                p4.ParameterName = "@PHONE";
                p4.Value = phone;
                p4.SqlDbType = SqlDbType.VarChar;

                SqlParameter p5 = new SqlParameter();
                p5.ParameterName = "@USERLEVEL";
                p5.Value = userlevel;
                p5.SqlDbType = SqlDbType.VarChar;

                SqlParameter p6 = new SqlParameter();
                p6.ParameterName = "@STATUS";
                p6.Value = status;
                p6.SqlDbType = SqlDbType.VarChar;

                SqlParameter p7 = new SqlParameter();
                p7.ParameterName = "@CONTRACTNO";
                p7.Value = contractno;
                p7.SqlDbType = SqlDbType.VarChar;

                SqlParameter p8 = new SqlParameter();
                p8.ParameterName = "@USERID";
                p8.Value = userid;
                p8.SqlDbType = SqlDbType.VarChar;

                SqlParameter p9 = new SqlParameter();
                p9.ParameterName = "@BRID";
                p9.Value = brid;
                p9.SqlDbType = SqlDbType.VarChar;

                return DataAccess.GetFromDataTable("SEMS_EBA_USER_SEARCHFOROTP", p1, p2, p3, p4, p5, p6, p7,p8,p9);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region update OTP FOR USER
        public void Update(DataTable MapOTP, DataTable deleteOTP, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMS0409");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "update OTP cho User");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                #region xoa bang danh sach OTP
                object[] tbldeleteOTP = new object[2];
                tbldeleteOTP[0] = "SEMS_EBA_USERAUTHEN_DELETEOTP";

                //add vao phan tu thu 2 mang object
                tbldeleteOTP[1] = deleteOTP;

                hasInput.Add(SmartPortal.Constant.IPC.DELETEOTP, tbldeleteOTP);
                #endregion

                #region them bang danh sach OTP
                object[] updateOTP = new object[2];
                updateOTP[0] = "SEMS_EBA_USERAUTHEN_MAPOTP";

                //add vao phan tu thu 2 mang object
                updateOTP[1] = MapOTP;

                hasInput.Add(SmartPortal.Constant.IPC.MAPOTP, updateOTP);
                #endregion

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
        #endregion

        #region MAP OTP FOR USER
        public void MapOTP(DataTable MapOTP, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSMAPOTP");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Thêm OTP cho User");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                #region them bang danh sach OTP
                object[] insertOTP = new object[2];
                insertOTP[0] = "SEMS_EBA_USERAUTHEN_MAPOTP";

                //add vao phan tu thu 2 mang object
                insertOTP[1] = MapOTP;

                hasInput.Add(SmartPortal.Constant.IPC.MAPOTP, insertOTP);
                #endregion

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
        #endregion

        #region delete OTP
        public void Delete(string id,string status,string datedeleted,string userdeleted, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMS0410");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "delete OTP cho User");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.ID, id);
                hasInput.Add(SmartPortal.Constant.IPC.STATUS, status);
                hasInput.Add(SmartPortal.Constant.IPC.DATEDELETED, datedeleted);
                hasInput.Add(SmartPortal.Constant.IPC.USERDELETED, userdeleted);

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
        #endregion

        #region Approve user OTP
        public void ApproveUserOTP(string ID,string status,string date,string user,string sttcurr, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMS00017");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "approve user otp");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.ID, ID);
                hasInput.Add(SmartPortal.Constant.IPC.STATUS, status);
                hasInput.Add(SmartPortal.Constant.IPC.DATE, date);
                hasInput.Add(SmartPortal.Constant.IPC.USER, user);
                hasInput.Add(SmartPortal.Constant.IPC.STATUSCURRENT, sttcurr);

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
        #endregion

        #region Gui SMSOTP
        public static void SendSMSOTP(string userID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SMS00020");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDSMS);
                hasInput.Add("SERVICETRAN", SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.USERID, userID);

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
        #endregion

        #region Gui SMSOTP corp
        public static void SendSMSOTPCORP(string userID,string Authentype,  ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "OTP00020");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDSMS);
                hasInput.Add("SERVICETRAN", SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.USERID, userID);
                hasInput.Add("AUTHENTYPE", Authentype);



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
        #endregion

        #region Send mail normal login
        public static void SendMailNormalLogin(string userID, string Authentype, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "OTP00021");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDSMS);
                hasInput.Add("SERVICETRAN", SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.USERID, userID);
                hasInput.Add("AUTHENTYPE", Authentype);



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
        #endregion


        #region Send mail first login
        public static void SendMailFirstLogin(string userID, string Authentype, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "OTP00022");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDSMS);
                hasInput.Add("SERVICETRAN", SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.USERID, userID);
                hasInput.Add("AUTHENTYPE", Authentype);



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
        #endregion

        #region Send mail change password successfully
        public static void SendMailChangePassword(string userID, string Authentype, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "OTP00023");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDSMS);
                hasInput.Add("SERVICETRAN", SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.USERID, userID);
                hasInput.Add("AUTHENTYPE", Authentype);



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
        #endregion
        #region Gui SMS OTP BY PHONE
        public static void SendSMSOTPBYPHONE(string phoneno, string Authentype, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "OTPREGISTER");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDSMS);
                hasInput.Add("SERVICETRAN", SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.PHONENO, phoneno);
                hasInput.Add("DEVICEID", phoneno);
                hasInput.Add("APPHASHKEY", "");
                hasInput.Add("AUTHENTYPE", Authentype);


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
        #endregion
    }
}
