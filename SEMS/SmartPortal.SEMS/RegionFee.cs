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
    public class RegionFee
    {
        #region Insert Region fee

        public RegionFee()
        {
        }

        public DataSet InsertRegionFee(string regionCode, string regionName, string regionType, string description,
            string usercreate, string status, int order, string countryId,
            ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable
                {
                    {IPC.IPCTRANCODE, "SEMSREGIONFEEINSERT"},
                    {IPC.SOURCEID, IPC.SOURCEIDVALUE},
                    {IPC.SOURCETRANREF, IPC.SOURCETRANREFVALUE},
                    {IPC.TRANDESC, "ADD NEW REGION"},
                    {IPC.REGIONCODE, regionCode},
                    {IPC.REGIONNAME, regionName},
                    {IPC.REGIONTYPE, regionType},
                    {IPC.DESCRIPTION, description},
                    {IPC.USERCREATE, usercreate},
                    {IPC.STATUS, status},
                    {IPC.ORDER, order},
                    {IPC.COUNTRYID, countryId}
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


                RegionExist = DataAccess.GetFromDataTable("EBA_Region_check_id_exist", p1);


                return RegionExist;
            }
            catch
            {
                return null;
            }
        }

        #region Search Country By Id

        public DataTable SearchCountryById(int id)
        {
            SqlParameter p1 = new SqlParameter("@COUNTRYID", id);
            p1.SqlDbType = SqlDbType.Int;
            return DataAccess.GetFromDataTable("SEMS_EBA_COUNTRY_GETBYID", p1);
        }

        #endregion

        #region Get All Country

        public DataSet GetAllCountry(ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable
                {
                    {IPC.IPCTRANCODE, "SEMSBRANCHGETCOUNTRY"},
                    {IPC.SOURCEID, IPC.SOURCEIDVALUE},
                };
                DataSet ds = new DataSet();
                Hashtable hasOutput = AT.DBTRAN().ProcessTransHAS(hasInput);
                if (hasOutput["IPCERRORCODE"].ToString() == "0")
                {
                    ds = (DataSet) hasOutput["DATASET"];
                    errorCode = "0";
                    errorDesc = string.Empty;
                }
                else
                {
                    errorCode = hasOutput["IPCERRORCODE"].ToString();
                    errorDesc = hasOutput["IPCERRORDESC"].ToString();
                }

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Search region fee by condition

        public DataSet SearchRegionFeeByCondition(string regionid, string regionFeeCode, string regionname, string regionType,
            string countryId,
            int recperpage, int recindex,
            ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable
                {
                    {IPC.IPCTRANCODE, "SEMSREGIONFEESEARCH"},
                    {IPC.SOURCEID, IPC.SOURCEIDVALUE},
                    {IPC.SOURCETRANREF, IPC.SOURCETRANREFVALUE},
                    {IPC.TRANDESC, "lấy thông tin chi tiết branch by condition"},
                    {IPC.REGIONID, regionid},
                    {IPC.REGIONCODE, regionFeeCode},
                    {IPC.REGIONNAME, regionname},
                    {IPC.REGIONTYPE, regionType},
                    {IPC.COUNTRYID, countryId},
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

        public DataSet showbranchbyregionfeeid(string regionid, int recperpage, int recindex,
            ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable
                {
                    {IPC.IPCTRANCODE, "SEMSREGIONGETBRANCH"},
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

        public DataSet DeleteRegionFee(string regionid, string userid, string status, ref string errorCode,
            ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(IPC.IPCTRANCODE, "SEMSREGIONFEEDELETE");
                hasInput.Add(IPC.SOURCEID, IPC.SOURCEIDVALUE);
                hasInput.Add(IPC.SOURCETRANREF, IPC.SOURCETRANREFVALUE);
                hasInput.Add(IPC.TRANDESC, "Xóa sản phẩm");
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

        public DataSet Updateregionfee(string regionid, string regionCode, string regionname, string regionType,
            string description, string userModify,
            string status, int order, string countryId, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(IPC.IPCTRANCODE, "SEMSREGIONFEEUPDATE");
                hasInput.Add(IPC.SOURCEID, IPC.SOURCEIDVALUE);
                hasInput.Add(IPC.SOURCETRANREF, IPC.SOURCETRANREFVALUE);
                hasInput.Add(IPC.TRANDESC, "lấy thông tin chi tiết region by condition");

                hasInput.Add(IPC.REGIONID, regionid);
                hasInput.Add(IPC.REGIONCODE, regionCode);
                hasInput.Add(IPC.REGIONNAME, regionname);
                hasInput.Add(IPC.REGIONTYPE, regionType);
                hasInput.Add(IPC.DESCRIPTION, description);
                hasInput.Add(IPC.USERMODIFY, userModify);
                hasInput.Add(IPC.STATUS, status);
                hasInput.Add(IPC.ORDER, order);
                hasInput.Add(IPC.COUNTRYID, countryId);

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

        public DataTable checkfordeleteRegionFee(string regionid)
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