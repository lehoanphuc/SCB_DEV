using System;
using System.Collections;
using System.Data;
using System.Security.Authentication;
using SmartPortal.Constant;
using SmartPortal.RemotingServices;

namespace SmartPortal.SEMS
{
    public class Currency
    {
        #region Get all currency

        public DataSet GetAll(ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();

                hasInput.Add(IPC.IPCTRANCODE, "SEMSCURRENCYGETALL");
                hasInput.Add(IPC.SOURCEID, IPC.SOURCEIDVALUE);
                hasInput.Add(IPC.SOURCETRANREF, IPC.SOURCETRANREFVALUE);
                hasInput.Add(IPC.TRANDESC, "lấy tất cả tiền tệ");

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

        #region SearchCurrencyByCondition

        public DataSet SearchCurrencyByCondition(string ccyid, string currencyNumber, string currencyName,
            string masterName, int recperpage, int recindex,
            ref string errorCode,
            ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable
                {
                    {IPC.IPCTRANCODE, "SEMSCURRENCYSEARCH"},
                    {IPC.SOURCEID, IPC.SOURCEIDVALUE},
                    {IPC.CCYID, ccyid},
                    {IPC.CURRENCYNUMBER, currencyNumber},
                    {IPC.CURRENCYNAME, currencyName},
                    {IPC.MASTERNAME, masterName},
                    {"RECPERPAGE",recperpage},
                    {"RECINDEX", recindex}
                };

                Hashtable hasOutput = new Hashtable();
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


        #region Get details Currency by CurrencyID

        public DataSet GetCurrencyDetailsById(string ccyid, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable
                {
                    {IPC.IPCTRANCODE, "SEMSCURRENCYDETAILS"},
                    {IPC.SOURCEID, IPC.SOURCEIDVALUE},
                    {IPC.SOURCETRANREF, IPC.SOURCETRANREFVALUE},
                    {IPC.TRANDESC, "get detail currency"},
                    {IPC.CCYID, ccyid}
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


        #region Insert Currency

        public void InsertCurrency(string ccyid, string sccyid, string currencyNumber, string currencyName,
            string masterName, string desc, string decimalDigits, string roundingDigit, string status, int order,
            string userCreated, ref string errorCode,
            ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                hasInput.Add(IPC.IPCTRANCODE, "SEMSCURRENCYINSERT");
                hasInput.Add(IPC.SOURCEID, IPC.SOURCEIDVALUE);
                hasInput.Add(IPC.CCYID, ccyid);
                hasInput.Add(IPC.SCURRENCYID, sccyid);
                hasInput.Add(IPC.CURRENCYNUMBER, currencyNumber);
                hasInput.Add(IPC.CURRENCYNAME, currencyName);
                hasInput.Add(IPC.MASTERNAME, masterName);
                hasInput.Add(IPC.DESC, desc);
                hasInput.Add(IPC.DECIMALDIGITS, decimalDigits);
                hasInput.Add(IPC.ROUNDINGDIGIT, roundingDigit);
                hasInput.Add(IPC.STATUS, status);
                hasInput.Add(IPC.ORDER, order);
                hasInput.Add(IPC.USERCREATED, userCreated);
                hasOutput = AT.DBTRAN().ProcessTransHAS(hasInput);
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

        #region Update Currency

        public DataSet UpdateCurrency(string ccyid, string sccyid, string currencyNumber, string currencyName,
            string masterName, string desc, string decimalDigits,  string roundingDigit, string status, int order, string userModified, ref string errorCode,
            ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable
                {
                    {IPC.IPCTRANCODE, "SEMSCURRENCYUPDATE"},
                    {IPC.SOURCEID, IPC.SOURCEIDVALUE},
                    {IPC.SOURCETRANREF, IPC.SOURCETRANREFVALUE},
                    {IPC.TRANDESC, "Update currency information"},

                    {IPC.CCYID, ccyid},
                    {IPC.SCURRENCYID, sccyid},
                    {IPC.CURRENCYNUMBER, currencyNumber},
                    {IPC.CURRENCYNAME, currencyName},
                    {IPC.MASTERNAME, masterName},
                    {IPC.DESC, desc},
                    {IPC.DECIMALDIGITS, decimalDigits},
                    {IPC.ROUNDINGDIGIT, roundingDigit},
                    {IPC.STATUS, status},
                    {IPC.ORDER, order},
                    {IPC.USERMODIFIED, userModified}

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

        #region Delete Currency

        public void DeleteCurrency(string ccyid, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable
                {
                    {IPC.IPCTRANCODE, "SEMSCURRENCYDELETE"},
                    {IPC.SOURCEID, IPC.SOURCEIDVALUE},
                    {IPC.SOURCETRANREF, IPC.SOURCETRANREFVALUE},
                    {IPC.TRANDESC, "Delete currency"},
                    {IPC.CCYID, ccyid}
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


        public DataSet SearchCurrencyFx(string fromccyid, string toccyid, int recperpage, int recindex, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable
                {
                    {IPC.IPCTRANCODE, "SEMSCURRENCYFXSEARCH"},
                    {IPC.SOURCEID, IPC.SOURCEIDVALUE},
                    {IPC.FROMCCYID, fromccyid},
                    {IPC.TOCCYID, toccyid},
                    {"RECPERPAGE",recperpage},
                    {"RECINDEX", recindex}
                };

                Hashtable hasOutput = new Hashtable();
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
        public void DeleteCurrencyFx(string fromccyid, string toccyid, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable
                {
                    {IPC.IPCTRANCODE, "SEMSCURRENCYFXDELETE"},
                    {IPC.SOURCEID, IPC.SOURCEIDVALUE},
                    {IPC.SOURCETRANREF, IPC.SOURCETRANREFVALUE},
                    {IPC.FROMCCYID, fromccyid},
                    {IPC.TOCCYID, toccyid},
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
        public DataSet InsertCurrencyFX(string fromccyid, string toccyid, string desc, string status, string userModified, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable
                {
                    {IPC.IPCTRANCODE, "SEMSCURRENCYFXINSERT"},
                    {IPC.SOURCEID, IPC.SOURCEIDVALUE},
                    {IPC.SOURCETRANREF, IPC.SOURCETRANREFVALUE},
                    {IPC.FROMCCYID, fromccyid},
                    {IPC.TOCCYID, toccyid},
                    {IPC.DESCRIPTION, desc},
                    {IPC.STATUS, status},
                    {IPC.USERCREATED, userModified}
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
        public DataSet UpdateCurrencyFX(string fromccyid, string toccyid, string desc, string status, string userModified, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable
                {
                    {IPC.IPCTRANCODE, "SEMSCURRENCYFXUPDATE"},
                    {IPC.SOURCEID, IPC.SOURCEIDVALUE},
                    {IPC.SOURCETRANREF, IPC.SOURCETRANREFVALUE},
                    {IPC.FROMCCYID, fromccyid},
                    {IPC.TOCCYID, toccyid},
                    {IPC.DESCRIPTION, desc},
                    {IPC.STATUS, status},
                    {IPC.USERMODIFIED, userModified}

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

        #region Config NPS Currency
        public DataSet SearchConfigNPSCurrency(string ccyid,int recperpage, int recindex, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable
                {
                    {IPC.IPCTRANCODE, "SEMSCONFIGNPSCURRENCY"},
                    {IPC.SOURCEID, IPC.SOURCEIDVALUE},
                    {IPC.CCYID, ccyid},
                    {"RECPERPAGE",recperpage},
                    {"RECINDEX", recindex}
                };

                Hashtable hasOutput = new Hashtable();
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

        public DataSet UpdateConfigNPSCurrency(string ccyid, string desc, string status, string userModified, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable
                {
                    {IPC.IPCTRANCODE, "SEMSCONFIGNPSCURRENCYUPDATE"},
                    {IPC.SOURCEID, IPC.SOURCEIDVALUE},
                    {IPC.SOURCETRANREF, IPC.SOURCETRANREFVALUE},
                     {IPC.CCYID, ccyid},
                    {IPC.DESCRIPTION, desc},
                    {IPC.STATUS, status},
                    {IPC.USERMODIFIED, userModified}

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
    }
}