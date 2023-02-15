using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using SmartPortal.DAL;

namespace SmartPortal.SEMS
{
    public class Branch
    {
        #region Select all branch
        public DataSet GetAll(ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSBRANCHGETALL");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "lấy tất cả chi nhánh");
                               

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

        #region Search Branch by condition
        public DataSet SearchBranchByCondition(string branchid,string branchname,string address,string phoneno,ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSBRANCHSEARCH");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "lấy thông tin chi tiết branch by condition");

                hasInput.Add(SmartPortal.Constant.IPC.BRANCHID, branchid);
                hasInput.Add(SmartPortal.Constant.IPC.BRANCHNAME, branchname);
                hasInput.Add(SmartPortal.Constant.IPC.ADDRESS, address);
                hasInput.Add(SmartPortal.Constant.IPC.PHONE, phoneno);

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

        #region Get details Branch by BranchID
        public DataSet GetBranchDetailsByID(string branchid, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSBRANCHDETAILS");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "lấy thông tin chi tiết branch by branchid");

                hasInput.Add(SmartPortal.Constant.IPC.BRANCHID, branchid);

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

        #region Update Branch
        public DataSet UpdateBranch(string branchid, string branchname, string address, string citycode, string distcode, string phoneno, string isopenfd, string posx, string posy, string regionid, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSBRANCHUPDATE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "lấy thông tin chi tiết branch by condition");

                hasInput.Add(SmartPortal.Constant.IPC.BRANCHID, branchid);
                hasInput.Add(SmartPortal.Constant.IPC.BRANCHNAME, branchname);
                hasInput.Add(SmartPortal.Constant.IPC.ADDRESS, address);
                hasInput.Add(SmartPortal.Constant.IPC.CITYCODE, citycode);
                hasInput.Add(SmartPortal.Constant.IPC.DISTCODE, distcode);
                hasInput.Add(SmartPortal.Constant.IPC.POSITIONX, posx);
                hasInput.Add(SmartPortal.Constant.IPC.POSITIONY, posy);
                hasInput.Add(SmartPortal.Constant.IPC.PHONE, phoneno);
                hasInput.Add(SmartPortal.Constant.IPC.ISOPENFD, isopenfd);
                hasInput.Add(SmartPortal.Constant.IPC.REGIONID, regionid);
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

        public DataTable GetBranchForSavingOnline()
        {
            try
            {
                DataTable iRead = new DataTable();
                iRead = DataAccess.GetFromDataTable("IB_GetBranchSavingOnline", null);

                return iRead;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Insert Branch
        public DataSet InsertBranch(string branchid, string branchname, string address, string citycode, string distcode, string phoneno, string isopenfd, string posx, string posy, string regionid, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSBRANCHINSERT");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "thêm mới chi nhánh");

                hasInput.Add(SmartPortal.Constant.IPC.BRANCHID, branchid);
                hasInput.Add(SmartPortal.Constant.IPC.BRANCHNAME, branchname);
                hasInput.Add(SmartPortal.Constant.IPC.ADDRESS, address);
                hasInput.Add(SmartPortal.Constant.IPC.CITYCODE, citycode);
                hasInput.Add(SmartPortal.Constant.IPC.DISTCODE, distcode);
                hasInput.Add(SmartPortal.Constant.IPC.POSITIONX, posx);
                hasInput.Add(SmartPortal.Constant.IPC.POSITIONY, posy);
                hasInput.Add(SmartPortal.Constant.IPC.PHONE, phoneno);
                hasInput.Add(SmartPortal.Constant.IPC.ISOPENFD, isopenfd);
                hasInput.Add(SmartPortal.Constant.IPC.REGIONID, regionid);
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

        #region DELETE branch
        public DataSet DeleteBranch(string branchID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSBRANCHDELETE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Xóa sản phẩm");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.BRANCHID, branchID);

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

        #region Select all CITY OF BRANCH
        public DataSet GetAllCity(ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSBRANCHGETCITY");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "lấy tất cả thành phố");


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

        #region Select all district OF BRANCH by city
        public DataSet GetAllDistOfCity(string citycode,ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSBRANCHGETDIST");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "lấy tất cả các quận của thành phố");
                hasInput.Add(SmartPortal.Constant.IPC.CITYCODE, citycode);

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
        public DataSet getallregionfee(string branchid, ref string errorCode, ref string errorDesc)
        {
            DataSet result;
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                hasInput.Add("IPCTRANCODE", "SEMSBRANCHGETREGION");
                hasInput.Add("SOURCEID", "SEMS");
                hasInput.Add("SOURCETRANREF", "010");
                hasInput.Add("TRANDESC", "lấy tất cả các region");
                hasInput.Add("BRANCHID", branchid);
                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();
                if (hasOutput["IPCERRORCODE"].ToString() == "0")
                {
                    ds = (DataSet)hasOutput["DATASET"];
                    errorCode = "0";
                }
                else
                {
                    errorCode = hasOutput["IPCERRORCODE"].ToString();
                    errorDesc = hasOutput["IPCERRORDESC"].ToString();
                }
                result = ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}
