using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using SmartPortal.Constant;
using SmartPortal.RemotingServices;

namespace SmartPortal.SEMS
{
    public class PROMOTION
    {
        public DataSet PROMOTION_SEARCH(string PROMOTIONCODE, string STATUS, string EXPIREDDATE, int RECINDEX, int RECPERPAGE, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                hasInput.Add(IPC.IPCTRANCODE, "PROMOTIONCODE");
                hasInput.Add(IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add("PROMOTIONCODE", PROMOTIONCODE);
                hasInput.Add("STATUS", STATUS);
                hasInput.Add("EXPIREDDATE", EXPIREDDATE);
                hasInput.Add(IPC.RECINDEX, RECINDEX);
                hasInput.Add(IPC.RECPERPAGE, RECPERPAGE);
                hasOutput = AT.DBTRAN().ProcessTransHAS(hasInput);
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
        public DataSet PROMOTIONCODE_GETID(string ID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                hasInput.Add(IPC.IPCTRANCODE, "PROMOTIONCODEGETID");
                hasInput.Add(IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add("ID", ID);
                hasOutput = AT.DBTRAN().ProcessTransHAS(hasInput);
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
        public DataSet PROMOTIONCODE_DELETE(string ID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                hasInput.Add(IPC.IPCTRANCODE, "PROMOTIONCODEDELETE");
                hasInput.Add(IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add("ID", ID);
                hasOutput = AT.DBTRAN().ProcessTransHAS(hasInput);
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
        public DataSet PROMOTIONCODE_INSERT(string PROMOTIONCODE, string STATUS, string USERCREATE, string EXPIREDDATE, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                hasInput.Add(IPC.IPCTRANCODE, "PROMOTIONCODEINSERT");
                hasInput.Add(IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add("PROMOTIONCODE", PROMOTIONCODE);
                hasInput.Add("STATUS", STATUS);
                hasInput.Add("USERCREATE", USERCREATE);
                hasInput.Add("EXPIREDDATE", EXPIREDDATE);
                hasOutput = AT.DBTRAN().ProcessTransHAS(hasInput);
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
        public DataSet PROMOTIONCODE_UPDATE(string ID, string PROMOTIONCODE, string STATUS, string EXPIREDDATE, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                hasInput.Add(IPC.IPCTRANCODE, "PROMOTIONCODEUPDATE");
                hasInput.Add(IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add("ID", ID);
                hasInput.Add("PROMOTIONCODE", PROMOTIONCODE);
                hasInput.Add("STATUS", STATUS);
                hasInput.Add("EXPIREDDATE", EXPIREDDATE);
                hasOutput = AT.DBTRAN().ProcessTransHAS(hasInput);
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


        public DataSet InsertPromotion(Hashtable hasInput, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasOutput = new Hashtable();
                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "PROMOTIONINSERT");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);

                #region Insert bảng Promotion

                #endregion

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
        public DataSet DetailsPromotion(string promotionId, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSDETAILSPROMOTION");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "detail promotion");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add("PROMOTIONID", promotionId);


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
        public DataSet UpdatePromotion(Hashtable hasInput, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasOutput = new Hashtable();
                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "PROMOTIONUPDATE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);

                #region Insert bảng Discount

                #endregion

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
        public DataSet DeletePromotion(string promotionId, string action, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "PROMOTIONDELETE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "delete promotion ");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add("PROMOTIONID", promotionId);
                hasInput.Add("ACTION", action);



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
        public DataSet RejectOrApprovePromotion(string discountId, string action, string desc, string userid, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "PROMOTIONACTION");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "approve or reject discount ");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add("PROMOTIONID", discountId);
                hasInput.Add("ACTION", action);
                hasInput.Add(SmartPortal.Constant.IPC.DESC, desc);
                hasInput.Add(SmartPortal.Constant.IPC.USERID, userid);



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
        public DataSet GetPromotionList(string objectID, string contractlevel, string promotionside, string transname, string schedualtype, string status, string promotiontype, string objecttype, string promotionname, int pagesize, int pageindex, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMS_GETLISTPROMOTION");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add("OBJECTID", objectID);
                hasInput.Add("CONTRACTLEVEL", contractlevel);
                hasInput.Add("PROMOTIONSIDE", promotionside);
                hasInput.Add("TRANNAME", transname);
                hasInput.Add("SCHEDUALTYPE", schedualtype);
                hasInput.Add(SmartPortal.Constant.IPC.STATUS, status);
                hasInput.Add("PROMOTIONTYPE", promotiontype);
                hasInput.Add("OBJECTTYPE", objecttype);
                hasInput.Add("PROMOTIONNAME", promotionname);
                hasInput.Add("RECPERPAGE", pagesize);
                hasInput.Add("RECINDEX", pageindex);

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
