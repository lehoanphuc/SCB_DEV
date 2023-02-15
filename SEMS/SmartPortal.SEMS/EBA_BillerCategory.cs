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
    public class EBA_BillerCategory
    {
        #region index, search
        public DataSet GetBillerCat(string CatID, string CatName, string CatShortName, string Status, int recPerPage, int recIndex, ref string errorCode, ref string errorDesc)
        {
            try
            {


                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                hasInput.Add(IPC.IPCTRANCODE, "EBA_BILLERCAT_SEARCH");
                hasInput.Add(IPC.SOURCEID, IPC.SOURCEIDVALUE);
                hasInput.Add(IPC.SOURCETRANREF, IPC.SOURCETRANREFVALUE);
                hasInput.Add(IPC.TRANDESC, "index and search billerCat");
                hasInput.Add(IPC.REVERSAL, "N");

                hasInput.Add("CATID", CatID);
                hasInput.Add("CATNAME", CatName);
                hasInput.Add("CATSHORTNAME", CatShortName);
                hasInput.Add("STATUS", Status);
                hasInput.Add("RECPERPAGE", recPerPage);
                hasInput.Add("RECINDEX", recIndex);

                hasOutput = AT.DBTRAN().ProcessTransHAS(hasInput);
                DataSet ds = new DataSet();

                if (hasOutput[IPC.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet)hasOutput[IPC.DATASET];
                    errorCode = "0";
                }
                else
                {
                    errorDesc = hasOutput[IPC.IPCERRORCODE].ToString();
                }
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region insert  
        public DataSet InsertBillerCat(string CatID, string CatName, string CatShortName, string Status, string CatLogoBin, string CatLogoType, string UserCreate, string CatNameLocal, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                hasInput.Add(IPC.IPCTRANCODE, "EBA_BILLERCAT_INSERT");
                hasInput.Add(IPC.SOURCEID, IPC.SOURCEIDVALUE);
                hasInput.Add(IPC.SOURCETRANREF, IPC.SOURCETRANREFVALUE);
                hasInput.Add(IPC.TRANDESC, "add new biller cat");
                hasInput.Add(IPC.REVERSAL, "N");
                hasInput.Add("CATID", CatID);
                hasInput.Add("CATNAME", CatName);
                hasInput.Add("CATSHORTNAME", CatShortName);
                hasInput.Add("STATUS", Status);
                hasInput.Add("CATLOGOBIN", CatLogoBin);
                hasInput.Add("CATLOGOTYPE", CatLogoType);
                hasInput.Add("USERCREATE", UserCreate);
                hasInput.Add("CATNAMELOCAL", CatNameLocal);


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

        #region delete    
        public DataSet DeleteBillerCat(string CatID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                hasInput.Add(IPC.IPCTRANCODE, "EBA_BILLERCAT_DELETE");
                hasInput.Add(IPC.SOURCEID, IPC.SOURCEIDVALUE);
                hasInput.Add(IPC.SOURCETRANREF, IPC.SOURCETRANREFVALUE);
                hasInput.Add(IPC.TRANDESC, "delete biller category");
                hasInput.Add(IPC.REVERSAL, "N");

                hasInput.Add("CATID", CatID);

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

        #region Get By CatID     
        public DataSet GetBillerCatByCatID(string CatID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                hasInput.Add(IPC.IPCTRANCODE, "EBA_BILLERCAT_GETID");
                hasInput.Add(IPC.SOURCEID, IPC.SOURCEIDVALUE);
                hasInput.Add(IPC.SOURCETRANREF, IPC.SOURCETRANREFVALUE);
                hasInput.Add(IPC.TRANDESC, "viewDetail biller category");
                hasInput.Add(IPC.REVERSAL, "N");

                hasInput.Add("CATID", CatID);

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

        #region edit  
        public DataSet EditBillerCat(string CatName, string CatShortName, string Status, string CatLogoBin
            , string CatLogoType, string UserModified, string CatID, string CatNameLocal, ref string errorCode, ref string errorDesc)
        {

            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                hasInput.Add(IPC.IPCTRANCODE, "EBA_BILLERCAT_EDIT");
                hasInput.Add(IPC.SOURCEID, IPC.SOURCEIDVALUE);
                hasInput.Add(IPC.SOURCETRANREF, IPC.SOURCETRANREFVALUE);
                hasInput.Add(IPC.TRANDESC, "edit biller category");
                hasInput.Add(IPC.REVERSAL, "N");

                hasInput.Add("CATNAME", CatName);
                hasInput.Add("CATSHORTNAME", CatShortName);
                hasInput.Add("STATUS", Status);
                hasInput.Add("CATLOGOBIN", CatLogoBin);
                hasInput.Add("CATLOGOTYPE", CatLogoType);
                hasInput.Add("USERMODIFIED", UserModified);
                hasInput.Add("CATID", CatID);
                hasInput.Add("CATNAMELOCAL", CatNameLocal);

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
