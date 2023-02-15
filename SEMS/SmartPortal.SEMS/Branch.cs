using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using SmartPortal.Constant;
using SmartPortal.DAL;
using SmartPortal.RemotingServices;

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
                hasInput.Add(IPC.IPCTRANCODE, "SEMSBRANCHGETALL");
                hasInput.Add(IPC.SOURCEID, IPC.SOURCEIDVALUE);
                hasInput.Add(IPC.SOURCETRANREF, IPC.SOURCETRANREFVALUE);
                hasInput.Add(IPC.TRANDESC, "lấy tất cả chi nhánh");


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

        #region Search Branch by condition

        public DataSet SearchBranchByCondition(string branchid, string branchname, string address, string phoneno,
            int recperpage, int recindex,
            ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable
                {
                    {IPC.IPCTRANCODE, "SEMSBRANCHSEARCH"},
                    {IPC.SOURCEID, IPC.SOURCEIDVALUE},
                    {IPC.SOURCETRANREF, IPC.SOURCETRANREFVALUE},
                    {IPC.TRANDESC, "lấy thông tin chi tiết branch by condition"},
                    {IPC.BRANCHID, branchid},
                    {IPC.BRANCHNAME, branchname},
                    {IPC.ADDRESS, address},
                    {IPC.PHONE, phoneno},
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



        #region Load Branch by condition

        public DataSet LoadBranchByCBB(string branchid, string branchname, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSBRANCHLOADCBB");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Load branch by combobox");

                hasInput.Add(SmartPortal.Constant.IPC.BRANCHID, branchid);
                hasInput.Add(SmartPortal.Constant.IPC.BRANCHNAME, branchname);

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


        #region Get All Branch By

        public DataSet GetBranchByDistrictId(string distId, ref string errorCode, ref string errorDesc)
        {
            try
            {
                DataSet ds = null;
                Hashtable hasInput = new Hashtable
                {
                    {IPC.IPCTRANCODE, "SEMSBRANCHGETBYDIST"},
                    {IPC.SOURCEID, IPC.SOURCEIDVALUE},
                    {IPC.DISTCODE, distId}
                };
                Hashtable hasOutput = AT.DBTRAN().ProcessTransHAS(hasInput);
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

        #region Get details Branch by BranchID

        public DataSet GetBranchDetailsByID(string branchid, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(IPC.IPCTRANCODE, "SEMSBRANCHDETAILS");
                hasInput.Add(IPC.SOURCEID, IPC.SOURCEIDVALUE);
                hasInput.Add(IPC.SOURCETRANREF, IPC.SOURCETRANREFVALUE);
                hasInput.Add(IPC.TRANDESC, "lấy thông tin chi tiết branch by branchid");

                hasInput.Add(IPC.BRANCHID, branchid);

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

        #region Update Branch

        public DataSet UpdateBranch(string branchid, string branchname, string address, string citycode,
            string distcode, string phoneno, string isopenfd, string posx, string posy, string regionid, string taxCode,
            string bicCode, string swiftCode, string countryId, string timeOpen, string timeClose, string email,
            string userModified, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(IPC.IPCTRANCODE, "SEMSBRANCHUPDATE");
                hasInput.Add(IPC.SOURCEID, IPC.SOURCEIDVALUE);
                hasInput.Add(IPC.SOURCETRANREF, IPC.SOURCETRANREFVALUE);
                hasInput.Add(IPC.TRANDESC, "lấy thông tin chi tiết branch by condition");

                hasInput.Add(IPC.BRANCHID, branchid);
                hasInput.Add(IPC.BRANCHNAME, branchname);
                hasInput.Add(IPC.ADDRESS, address);
                hasInput.Add(IPC.CITYCODE, citycode);
                hasInput.Add(IPC.DISTCODE, distcode);
                hasInput.Add(IPC.POSITIONX, posx);
                hasInput.Add(IPC.POSITIONY, posy);
                hasInput.Add(IPC.PHONE, phoneno);
                hasInput.Add(IPC.ISOPENFD, isopenfd);
                hasInput.Add("REGIONID", regionid);
                hasInput.Add(IPC.TAXCODE, taxCode);
                hasInput.Add(IPC.BICCODE, bicCode);
                hasInput.Add(IPC.SWIFTCODE, swiftCode);
                hasInput.Add("COUNTRYID", countryId);
                hasInput.Add(IPC.TIMEOPEN, timeOpen);
                hasInput.Add(IPC.TIMECLOSE, timeClose);
                hasInput.Add(IPC.EMAIL, email);
                hasInput.Add(IPC.USERMODIFIED, userModified);
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

        public DataSet InsertBranch(string branchid, string branchname, string address, string citycode,
            string distcode, string regionid, string phoneno, string isopenfd, string posx, string posy, string taxCode,
            string bicCode, string swiftCode, string countryId, string timeOpen, string timeClose, string email,
            string userCreate, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(IPC.IPCTRANCODE, "SEMSBRANCHINSERT");
                hasInput.Add(IPC.SOURCEID, IPC.SOURCEIDVALUE);
                hasInput.Add(IPC.SOURCETRANREF, IPC.SOURCETRANREFVALUE);
                hasInput.Add(IPC.TRANDESC, "thêm mới chi nhánh");

                hasInput.Add(IPC.BRANCHID, branchid);
                hasInput.Add(IPC.BRANCHNAME, branchname);
                hasInput.Add(IPC.ADDRESS, address);
                hasInput.Add(IPC.CITYCODE, citycode);
                hasInput.Add(IPC.DISTCODE, distcode);
                hasInput.Add(IPC.POSITIONX, posx);
                hasInput.Add(IPC.POSITIONY, posy);
                hasInput.Add(IPC.PHONE, phoneno);
                hasInput.Add(IPC.ISOPENFD, isopenfd);
                hasInput.Add(IPC.REGIONID, regionid);
                hasInput.Add(IPC.TAXCODE, taxCode);
                hasInput.Add(IPC.BICCODE, bicCode);
                hasInput.Add(IPC.SWIFTCODE, swiftCode);
                hasInput.Add(IPC.COUNTRYID, countryId);
                hasInput.Add(IPC.TIMEOPEN, timeOpen);
                hasInput.Add(IPC.TIMECLOSE, timeClose);
                hasInput.Add(IPC.EMAIL, email);
                hasInput.Add(IPC.USERCREATE, userCreate);
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

        #region DELETE branch

        public DataSet DeleteBranch(string branchID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(IPC.IPCTRANCODE, "SEMSBRANCHDELETE");
                hasInput.Add(IPC.SOURCEID, IPC.SOURCEIDVALUE);
                hasInput.Add(IPC.SOURCETRANREF, IPC.SOURCETRANREFVALUE);
                hasInput.Add(IPC.TRANDESC, "Xóa sản phẩm");
                hasInput.Add(IPC.REVERSAL, "N");

                hasInput.Add(IPC.BRANCHID, branchID);

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

        #region Get All Country

        public DataSet GetAllCountry(ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                hasInput.Add(IPC.IPCTRANCODE, "SEMSBRANCHGETCOUNTRY");
                hasInput.Add(IPC.SOURCEID, IPC.SOURCEIDVALUE);
                hasInput.Add(IPC.SOURCETRANREF, IPC.SOURCETRANREFVALUE);
                hasInput.Add(IPC.TRANDESC, "lấy tất cả quốc gia");

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

        #region Get Region by CountryID

        public DataSet GetAllRegionByCountryId(string countryId, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                hasInput.Add(IPC.IPCTRANCODE, "SEMSREGIONBYCOUNTRY");
                hasInput.Add(IPC.SOURCEID, IPC.SOURCEIDVALUE);

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

        #region Select all CITY OF BRANCH

        public DataSet GetAllCity(ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(IPC.IPCTRANCODE, "SEMSBRANCHGETCITY");
                hasInput.Add(IPC.SOURCEID, IPC.SOURCEIDVALUE);
                hasInput.Add(IPC.SOURCETRANREF, IPC.SOURCETRANREFVALUE);
                hasInput.Add(IPC.TRANDESC, "lấy tất cả thành phố");

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

        #region Get All city by region ID

        public DataSet GetAllCityByRegionId(int regionId, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(IPC.IPCTRANCODE, "SEMSCITYGETBYREGION");
                hasInput.Add(IPC.SOURCEID, IPC.SOURCEIDVALUE);
                hasInput.Add(IPC.REGIONID, regionId);

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

        #region Get All city by country

        public DataSet GetAllCityOfCountry(string countryId, ref string errorCode, ref string errorDesc)
        {
            try
            {
                DataSet ds = new DataSet();
                Hashtable hasInput = new Hashtable
                {
                    {IPC.SOURCEID, IPC.SOURCEIDVALUE},
                    {IPC.SOURCETRANREF, IPC.SOURCETRANREFVALUE},
                    {IPC.IPCTRANCODE, "SEMSCITYBYCOUNTRYID"},
                    {IPC.COUNTRYID, countryId},
                };

                Hashtable hasOutput = AT.DBTRAN().ProcessTransHAS(hasInput);
                if (hasOutput["IPCERRORCODE"].ToString() == "0")
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

        #region Select all district OF BRANCH by city

        public DataSet GetAllDistAndBranchOfCity(string citycode, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(IPC.IPCTRANCODE, "SEMSBRANCHGETDIST");
                hasInput.Add(IPC.SOURCEID, IPC.SOURCEIDVALUE);
                hasInput.Add(IPC.SOURCETRANREF, IPC.SOURCETRANREFVALUE);
                hasInput.Add(IPC.TRANDESC, "lấy tất cả các quận của thành phố");
                hasInput.Add(IPC.CITYCODE, citycode);

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

        public DataSet GetBranchByDistAndCityCode(string cityCode, string distCode, ref string errorCode,
            ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable
                {
                    {IPC.IPCTRANCODE, "SEMSBRANCHBYCITY"},
                    {IPC.SOURCEID, IPC.SOURCEIDVALUE},
                    {IPC.CITYCODE, cityCode},
                    {IPC.DISTCODE, distCode}
                };
                DataSet ds = new DataSet();
                Hashtable hasOutput = AT.DBTRAN().ProcessTransHAS(hasInput);
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
    }
}