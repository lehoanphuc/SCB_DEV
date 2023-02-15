using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using SmartPortal.Constant;
using SmartPortal.RemotingServices;

namespace SmartPortal.SEMS
{
    public class ATM
    {
        #region Search product by condition

        public DataSet GetATMPlaceByCondition(string atmId, string address, string country,
            string cityCode, string distCode, string branchId, int recperpage, int recindex,
            ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable
                {
                    {IPC.IPCTRANCODE, "IB000110"},
                    {IPC.SOURCEID, IPC.SOURCEIDVALUE},
                    {IPC.SOURCETRANREF, IPC.SOURCETRANREFVALUE},
                    {IPC.TRANDESC, "Tìm kiếm địa điểm đặt máy ATM"},
                    {IPC.ATMID, atmId},
                    {IPC.ADDRESS, address},
                    {IPC.COUNTRYID, country},
                    {IPC.CITYCODE, cityCode},
                    {IPC.DISTCODE, distCode},
                    {IPC.BRANCHID, branchId},
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

        #region Get All Country

        public DataSet GetAllCountry(ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable
                {
                    {IPC.SOURCEID, IPC.SOURCEIDVALUE},
                    {IPC.SOURCETRANREF, IPC.SOURCETRANREFVALUE},
                    {IPC.IPCTRANCODE, "SEMSATMALLCOUNTRY"},
                };
                DataSet ds = new DataSet();
                Hashtable hasOutput = AT.DBTRAN().ProcessTransHAS(hasInput);
                if (hasOutput["IPCERRORCODE"].ToString() == "0")
                {
                    ds = (DataSet) hasOutput["DATASET"];
                    errorCode = "0";
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

        #region Get All City

        public DataSet GetAllCity(ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable
                {
                    {IPC.SOURCEID, IPC.SOURCEIDVALUE},
                    {IPC.SOURCETRANREF, IPC.SOURCETRANREFVALUE},
                    {IPC.IPCTRANCODE, "SEMSCITYGETALL"},
                };
                DataSet ds = new DataSet();
                Hashtable hasOutput = AT.DBTRAN().ProcessTransHAS(hasInput);
                if (hasOutput["IPCERRORCODE"].ToString() == "0")
                {
                    ds = (DataSet) hasOutput["DATASET"];
                    errorCode = "0";
                    errorDesc = "";
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

        #region Get All District

        public DataSet GetAllDistrict(ref string errorCode, ref string errorDesc)
        {
            try
            {
                DataSet ds = new DataSet();
                Hashtable hasInput = new Hashtable
                {
                    {IPC.SOURCEID, IPC.SOURCEIDVALUE},
                    {IPC.SOURCETRANREF, IPC.SOURCETRANREFVALUE},
                    {IPC.IPCTRANCODE, "SEMSDISTRICTGETALL"},
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


        #region Get ATM by ATMID

        public DataSet GetAtmById(string atmId, ref string errorCode, ref string errorDesc)
        {
            try
            {
                DataSet ds = new DataSet();
                Hashtable hasInput = new Hashtable
                {
                    {IPC.IPCTRANCODE, "SEMSATMGETBYID"},
                    {IPC.SOURCEID, IPC.SOURCEIDVALUE},
                    {"ATMID", atmId}
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

        #region Delete ATM

        public void DeleteATM(string ATMCode, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(IPC.IPCTRANCODE, "IB000111");
                hasInput.Add(IPC.SOURCEID, IPC.SOURCEIDVALUE);
                hasInput.Add(IPC.SOURCETRANREF, IPC.SOURCETRANREFVALUE);
                hasInput.Add(IPC.TRANDESC, "Xóa địa điểm đặt máy ATM");
                hasInput.Add(IPC.REVERSAL, "N");

                hasInput.Add(IPC.ATMID, ATMCode);

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);
                errorCode = hasOutput[IPC.IPCERRORCODE].ToString();
                errorDesc = hasOutput[IPC.IPCERRORDESC].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region ADD ATM

        public void AddATM(string atmId, string atmCode, string address, string branchId, string distCode,
            string cityCode, string regionId, string countryId, string PosX, string PosY, string desc, string timeOpen,
            string timeClose, string userCreate, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                //add key into input
                hasInput.Add(IPC.IPCTRANCODE, "IB000112");
                hasInput.Add(IPC.SOURCEID, IPC.SOURCEIDVALUE);
                hasInput.Add(IPC.SOURCETRANREF, IPC.SOURCETRANREFVALUE);
                hasInput.Add(IPC.TRANDESC, "Thêm địa điểm đặt máy ATM");
                hasInput.Add(IPC.ATMID, atmId);
                hasInput.Add(IPC.ATMCODE, atmCode);
                hasInput.Add(IPC.ADDRESS, address);
                hasInput.Add(IPC.BRANCHID, branchId);
                hasInput.Add(IPC.DISTCODE, distCode);
                hasInput.Add(IPC.CITYCODE, cityCode);
                hasInput.Add(IPC.REGIONID, regionId);
                hasInput.Add(IPC.COUNTRYID, countryId);
                hasInput.Add(IPC.POSITIONX, PosX);
                hasInput.Add(IPC.POSITIONY, PosY);
                hasInput.Add(IPC.DESCRIPTION, desc);
                hasInput.Add(IPC.TIMEOPEN, timeOpen);
                hasInput.Add(IPC.TIMECLOSE, timeClose);
                hasInput.Add(IPC.USERCREATED, userCreate);
                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);
                errorCode = hasOutput[IPC.IPCERRORCODE].ToString();
                errorDesc = hasOutput[IPC.IPCERRORDESC].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region EDIT ATM

        public void EditATM(string atmId, string atmCode, string address, string branchId, string distCode,
            string cityCode, string regionId, string countryId, string PosX, string PosY, string desc, string timeOpen,
            string timeClose, string userCreate, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();

                //add key into input
                hasInput.Add(IPC.IPCTRANCODE, "IB000114");
                hasInput.Add(IPC.SOURCEID, IPC.SOURCEIDVALUE);
                hasInput.Add(IPC.SOURCETRANREF, IPC.SOURCETRANREFVALUE);

                hasInput.Add(IPC.ATMID, atmId);
                hasInput.Add(IPC.ATMCODE, atmCode);
                hasInput.Add(IPC.ADDRESS, address);
                hasInput.Add(IPC.BRANCHID, branchId);
                hasInput.Add(IPC.DISTCODE, distCode);
                hasInput.Add(IPC.CITYCODE, cityCode);
                hasInput.Add(IPC.REGIONID, regionId);
                hasInput.Add(IPC.COUNTRYID, countryId);
                hasInput.Add(IPC.POSITIONX, PosX);
                hasInput.Add(IPC.POSITIONY, PosY);
                hasInput.Add(IPC.DESCRIPTION, desc);
                hasInput.Add(IPC.TIMEOPEN, timeOpen);
                hasInput.Add(IPC.TIMECLOSE, timeClose);
                hasInput.Add(IPC.USERMODIFIED, userCreate);

                Hashtable hasOutput = AT.DBTRAN().ProcessTransHAS(hasInput);
                if (hasOutput["IPCERRORCODE"].ToString() != "0")
                {
                    errorCode = hasOutput["IPCERRORCODE"].ToString();
                    errorDesc = hasOutput["IPCERRORDESC"].ToString();
                }
                else
                {
                    errorCode = "0";
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