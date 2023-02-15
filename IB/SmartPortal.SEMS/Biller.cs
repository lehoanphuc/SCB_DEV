using SmartPortal.Constant;
using SmartPortal.RemotingServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SmartPortal.SEMS
{
    public class Biller
    {
        #region SearchBillByCondition
        public DataSet SearchBillByCondition(string billerID, string billerCode, string billerName, string shortName, string mastername, int recIndex, int recPerPage, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();


                //add key into input
                hasInput.Add(IPC.IPCTRANCODE, "SEMSBILLERSEARCH");
                hasInput.Add(IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);

                hasInput.Add(IPC.BILLERID, billerID);
                hasInput.Add(IPC.BILLERCODE, billerCode);
                hasInput.Add(IPC.BILLERNAME, billerName);
                hasInput.Add(IPC.SHORTNAME, shortName);
                hasInput.Add(IPC.MASTERNAME, mastername);
                hasInput.Add(IPC.RECINDEX, recIndex);
                hasInput.Add(IPC.RECPERPAGE, recPerPage);


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

        #endregion

        #region Get details Bill by billerID

        public DataSet GetBillDetailsById(string billerID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable
                {
                    {IPC.IPCTRANCODE, "SEMSBILLERDETAILS"},
                    {IPC.SOURCEID, IPC.SOURCEIDVALUE},
                    {IPC.SOURCETRANREF, IPC.SOURCETRANREFVALUE},
                    {IPC.TRANDESC, "get detail biller"},
                    {IPC.BILLERID, billerID },
                };

                Hashtable hasOutput = AT.DBTRAN().ProcessTransHAS(hasInput);
                DataSet ds = new DataSet();
                if (hasOutput[IPC.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet)hasOutput[IPC.DATASET];
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

        #region Insert biller
        public DataSet Insert(string billerID, string billerCode, string billerName, string shortName,
                              string description, string phone, string website, string logoBin, string logoType,
                              string countryID, int cityID, string catID, string showAsBill,
                              string mastername, string status, string timeOpen, string timeClose, string SUNDRYACCTNOBANK, string INCOMEACCTNOBANK, string SUNDRYACCTNOWALLET, string INCOMEACCTNOWALLET, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable
                {
                    {IPC.IPCTRANCODE, "SEMSBILLERINSERT" },
                    {IPC.SOURCEID, IPC.SOURCEIDVALUE },
                    {IPC.BILLERID, billerID },
                    {IPC.BILLERCODE, billerCode },
                    {IPC.BILLERNAME, billerName },
                    {IPC.SHORTNAME, shortName },
                    {IPC.DESCRIPTION,description},
                    {IPC.PHONE, phone },
                    {IPC.WEBSITE, website },
                    {IPC.LOGOBIN, logoBin },
                    {IPC.LOGOTYPE, logoType },
                    {IPC.COUNTRYID, countryID },
                    //{IPC.REGIONID, regionID },
                    {IPC.CITYID, cityID },
                    {IPC.CATID, catID },
                    {IPC.SHOWASBILL, showAsBill },
                    {IPC.MASTERNAME, mastername },
                    {IPC.STATUS, status },
                    {IPC.TIMEOPEN, timeOpen},
                    {IPC.TIMECLOSE, timeClose},
                    {IPC.SUNDRYACCTNOBANK, SUNDRYACCTNOBANK},
                    {IPC.INCOMEACCTNOBANK, INCOMEACCTNOBANK},
                    {IPC.SUNDRYACCTNOWALLET, SUNDRYACCTNOWALLET},
                    {IPC.INCOMEACCTNOWALLET,INCOMEACCTNOWALLET}
                };
                Hashtable hasOutput = AT.DBTRAN().ProcessTransHAS(hasInput);
                DataSet ds = new DataSet();
                if (hasOutput[IPC.IPCERRORCODE].ToString() == "0")
                {
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

        #region Update biller
        public DataSet Update(string billerID, string billerCode, string billerName, string shortName,
                              string description, string phone, string website, string logoBin, string logoType,
                              string countryID, int cityID, string catID, string showAsBill,
                              string mastername, string status, string timeOpen, string timeClose, string SUNDRYACCTNOBANK, string INCOMEACCTNOBANK, string SUNDRYACCTNOWALLET, string INCOMEACCTNOWALLET,
                              ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable
                {
                    {IPC.IPCTRANCODE, "SEMSBILLERUPDATE" },
                    {IPC.SOURCEID, IPC.SOURCEIDVALUE },
                    {IPC.BILLERID, billerID },
                    {IPC.BILLERCODE, billerCode },
                    {IPC.BILLERNAME, billerName },
                    {IPC.SHORTNAME, shortName },
                    {IPC.DESCRIPTION,description},
                    {IPC.PHONE, phone },
                    {IPC.WEBSITE, website },
                    {IPC.LOGOBIN, logoBin },
                    {IPC.LOGOTYPE, logoType },
                    {IPC.COUNTRYID, countryID },
                    //{IPC.REGIONID, regionID },
                    {IPC.CITYID, cityID },
                    {IPC.CATID, catID },
                    {IPC.SHOWASBILL, showAsBill },
                    {IPC.MASTERNAME, mastername },
                    {IPC.STATUS, status },
                    {IPC.TIMEOPEN, timeOpen},
                    {IPC.TIMECLOSE, timeClose},
                    {IPC.SUNDRYACCTNOBANK, SUNDRYACCTNOBANK},
                    {IPC.INCOMEACCTNOBANK, INCOMEACCTNOBANK},
                    {IPC.SUNDRYACCTNOWALLET, SUNDRYACCTNOWALLET},
                    {IPC.INCOMEACCTNOWALLET,INCOMEACCTNOWALLET}


                };
                Hashtable hasOutput = AT.DBTRAN().ProcessTransHAS(hasInput);
                DataSet ds = new DataSet();
                if (hasOutput[IPC.IPCERRORCODE].ToString() == "0")
                {
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

        #region Delete News
        public void Delete(string billerID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable
                {
                    {IPC.IPCTRANCODE, "SEMSBILLERDELETE"},
                    {IPC.SOURCEID, IPC.SOURCEIDVALUE},
                    {IPC.SOURCETRANREF, IPC.SOURCETRANREFVALUE},
                    {IPC.TRANDESC, "Delete biller"},
                    {IPC.BILLERID, billerID },
                };

                Hashtable hasOutput = AT.DBTRAN().ProcessTransHAS(hasInput);
                if (hasOutput[IPC.IPCERRORCODE].ToString() == "0")
                {
                    errorCode = "0";
                }
                else
                {
                    errorCode = hasOutput[IPC.IPCERRORCODE].ToString();
                    errorDesc = hasOutput[IPC.IPCERRORDESC].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Get catID DROPDOWNLIST
        public DataSet GetCatIDGroup(ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                hasInput.Add(IPC.IPCTRANCODE, "SEMSCATIDSEARCH");
                hasInput.Add(IPC.SOURCEID, IPC.SOURCEIDVALUE);
                hasInput.Add(IPC.SOURCETRANREF, IPC.SOURCETRANREFVALUE);
                hasOutput = AT.DBTRAN().ProcessTransHAS(hasInput);
                DataSet ds = new DataSet();
                if (hasOutput[IPC.IPCERRORCODE].ToString() == "0")
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


        #region Get All Country

        public DataSet GetAllCountry(ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                hasInput.Add(IPC.IPCTRANCODE, "SEMSBILLERGETCOUNTRY");
                hasInput.Add(IPC.SOURCEID, IPC.SOURCEIDVALUE);
                hasInput.Add(IPC.SOURCETRANREF, IPC.SOURCETRANREFVALUE);
                hasOutput = AT.DBTRAN().ProcessTransHAS(hasInput);
                DataSet ds = new DataSet();
                if (hasOutput[IPC.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet)hasOutput[IPC.DATASET];
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

        //public DataSet GetAllRegionByCountryId(string countryId, ref string errorCode, ref string errorDesc)
        //{
        //    try
        //    {
        //        Hashtable hasInput = new Hashtable();
        //        Hashtable hasOutput = new Hashtable();

        //        hasInput.Add(IPC.IPCTRANCODE, "SEMSREGIONBYCOUNTRY");
        //        hasInput.Add(IPC.SOURCEID, IPC.SOURCEIDVALUE);

        //        hasInput.Add(IPC.COUNTRYID, countryId);

        //        hasOutput = AT.DBTRAN().ProcessTransHAS(hasInput);

        //        DataSet ds = new DataSet();

        //        if (hasOutput[IPC.IPCERRORCODE].ToString() == "0")
        //        {
        //            ds = (DataSet)hasOutput[IPC.DATASET];
        //            errorCode = "0";
        //        }
        //        else
        //        {
        //            errorCode = hasOutput[IPC.IPCERRORCODE].ToString();
        //            errorDesc = hasOutput[IPC.IPCERRORDESC].ToString();
        //        }

        //        return ds;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        #endregion

        #region Get All city by region ID

        //public DataSet GetAllCityByRegionId(int regionId, ref string errorCode, ref string errorDesc)
        //{
        //    try
        //    {
        //        Hashtable hasInput = new Hashtable();
        //        Hashtable hasOutput = new Hashtable();

        //        //add key into input
        //        hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSCITYGETBYREGION");
        //        hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
        //        hasInput.Add(IPC.REGIONID, regionId);

        //        hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

        //        DataSet ds = new DataSet();

        //        if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
        //        {
        //            ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATASET];
        //            errorCode = "0";
        //        }
        //        else
        //        {
        //            errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
        //            errorDesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
        //        }

        //        return ds;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        #endregion


        #region Get City by CountryID

        public DataSet GetAllCityByCountryId(string countryId, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                hasInput.Add(IPC.IPCTRANCODE, "SEMSCITYGETBYCOUNTRY");
                hasInput.Add(IPC.SOURCEID, IPC.SOURCEIDVALUE);

                hasInput.Add(IPC.COUNTRYID, countryId);

                hasOutput = AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

                if (hasOutput[IPC.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet)hasOutput[IPC.DATASET];
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
    }
}
