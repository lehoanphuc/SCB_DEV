using SmartPortal.DAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using SmartPortal.Constant;
using SmartPortal.RemotingServices;

namespace SmartPortal.SEMS
{
    public class Region
    {
        #region Insert Region fee

        public Region()
        {
        }

        public DataSet InsertRegion(string regionName, string regionSpecial, string description,
            string usercreate, string status,
            ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable
                {
                    {IPC.IPCTRANCODE, "SEMSREGIONINSERT"},
                    {IPC.SOURCEID, IPC.SOURCEIDVALUE},
                    {IPC.SOURCETRANREF, IPC.SOURCETRANREFVALUE},
                    {IPC.TRANDESC, "ADD NEW REGION"},
                    {IPC.REGIONNAME, regionName},
                    {IPC.REGIONSPECIAL, regionSpecial},
                    {IPC.DESCRIPTION, description},
                    {IPC.USERCREATE, usercreate},
                    {IPC.STATUS, status},
                 
                };
                //add key into input
                Hashtable hasOutput = AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

                if (hasOutput[IPC.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet) hasOutput[IPC.DATASET];
                    errorCode = "0";
                }
                else
                {
                    errorCode = hasOutput[IPC.IPCERRORCODE].ToString();
                    errorDesc = hasOutput[IPC.IPCERRORDESC].ToString();
                }

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        public DataTable CheckRegionname(string regionid, string regionname)
        {
            try
            {
                DataTable RegionExist = new DataTable();

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@regionid";
                p1.Value = regionid;
                p1.SqlDbType = SqlDbType.Text;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@regionname";
                p2.Value = regionname;
                p2.SqlDbType = SqlDbType.Text;


                RegionExist = DataAccess.GetFromDataTable("EBA_Region_check_exist", p1, p2);


                return RegionExist;
            }
            catch
            {
                return null;
            }
        }

        public DataTable CheckRegionID(string regionID)
        {
            try
            {
                DataTable RegionExist = new DataTable();

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@regionID";
                p1.Value = regionID;
                p1.SqlDbType = SqlDbType.Text;

                RegionExist = DataAccess.GetFromDataTable("EBA_Region_GetById", p1);


                return RegionExist;
            }
            catch
            {
                return null;
            }
        }

        #region Search region fee by condition

        public DataSet SearchRegionByCondition( string regionname, string regionSpecial,
            string description,
            int recperpage, int recindex,
            ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable
                {
                    {IPC.IPCTRANCODE, "SEMSREGIONSEARCH"},
                    {IPC.SOURCEID, IPC.SOURCEIDVALUE},
                    {IPC.SOURCETRANREF, IPC.SOURCETRANREFVALUE},
                    {IPC.TRANDESC, "lấy thông tin chi tiết branch by condition"},
                    {IPC.REGIONNAME, regionname},
                    {IPC.REGIONSPECIAL,regionSpecial},
                    {IPC.DESCRIPTION, description},
                    {"RECPERPAGE", recperpage},
                    {"RECINDEX", recindex}
                };
                Hashtable hasOutput = AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

                if (hasOutput[IPC.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet) hasOutput[IPC.DATASET];
                    errorCode = "0";
                }
                else
                {
                    errorCode = hasOutput[IPC.IPCERRORCODE].ToString();
                    errorDesc = hasOutput[IPC.IPCERRORDESC].ToString();
                }

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        public DataSet showbranchbyregionid(string regionid, int recperpage, int recindex,
            ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable
                {
                    {IPC.IPCTRANCODE, "SEMSGETBRANCHREGION"},
                    {IPC.SOURCEID, IPC.SOURCEIDVALUE},
                    {IPC.REGIONID, regionid},
                    {"RECPERPAGE", recperpage},
                    {"RECINDEX", recindex}
                };
                Hashtable hasOutput = AT.DBTRAN().ProcessTransHAS(hasInput);
                DataSet ds = new DataSet();
                if (hasOutput[IPC.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet) hasOutput[IPC.DATASET];
                    errorCode = "0";
                }
                else
                {
                    errorCode = hasOutput[IPC.IPCERRORCODE].ToString();
                    errorDesc = hasOutput[IPC.IPCERRORDESC].ToString();
                }

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region DELETE region fee

        public DataSet DeleteRegion(string regionid, string userid, string status, ref string errorCode,
            ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(IPC.IPCTRANCODE, "SEMSREGIONDELETE");
                hasInput.Add(IPC.SOURCEID, IPC.SOURCEIDVALUE);
                hasInput.Add(IPC.SOURCETRANREF, IPC.SOURCETRANREFVALUE);
                hasInput.Add(IPC.TRANDESC, "Xóa region");
                hasInput.Add(IPC.REVERSAL, "N");
                hasInput.Add(IPC.REGIONID, regionid);
                hasInput.Add(IPC.USERID, userid);
                hasInput.Add(IPC.STATUS, status);


                hasOutput = AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

                if (hasOutput[IPC.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet) hasOutput[IPC.DATASET];
                    errorCode = "0";
                }
                else
                {
                    errorCode = hasOutput[IPC.IPCERRORCODE].ToString();
                    errorDesc = hasOutput[IPC.IPCERRORDESC].ToString();
                }

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Update region

        public DataSet Updateregion(string regionid, string regionname, string regionSpecial,
            string description, string userModify,
            string status,  ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(IPC.IPCTRANCODE, "SEMSREGIONUPDATE");
                hasInput.Add(IPC.SOURCEID, IPC.SOURCEIDVALUE);
                hasInput.Add(IPC.SOURCETRANREF, IPC.SOURCETRANREFVALUE);
                hasInput.Add(IPC.TRANDESC, "lấy thông tin chi tiết region by condition");

                hasInput.Add(IPC.REGIONID, regionid);
                hasInput.Add(IPC.REGIONNAME, regionname);
                hasInput.Add(IPC.REGIONSPECIAL, regionSpecial);
                hasInput.Add(IPC.DESCRIPTION, description);
                hasInput.Add(IPC.USERMODIFY, userModify);
                hasInput.Add(IPC.STATUS, status);
                hasOutput = AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

                if (hasOutput[IPC.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet) hasOutput[IPC.DATASET];
                    errorCode = "0";
                }
                else
                {
                    errorCode = hasOutput[IPC.IPCERRORCODE].ToString();
                    errorDesc = hasOutput[IPC.IPCERRORDESC].ToString();
                }

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region check for delete regionfee

        public DataTable checkfordeleteRegion(string regionid)
        {
            try
            {
                DataTable RegionExist = new DataTable();

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@regionid";
                p1.Value = regionid;
                p1.SqlDbType = SqlDbType.Text;


                RegionExist = DataAccess.GetFromDataTable("EBA_checkfordeleteregionfee", p1);


                return RegionExist;
            }
            catch
            {
                return null;
            }
        }

        #endregion
    }
}