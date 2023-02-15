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
    public class Partner
    {
        #region Search partner bank by condition
        public DataSet SearchBankByCondition(string bankcode, string bankname,
            int recperpage, int recindex,
            ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable
                {
                    {IPC.IPCTRANCODE, "PARTNERBANKSEARCH"},
                    {IPC.SOURCEID, IPC.SOURCEIDVALUE},
                    {IPC.SOURCETRANREF, IPC.SOURCETRANREFVALUE},
                    {IPC.TRANDESC, "lấy thông tin chi tiết partner bank by condition"},
                    {IPC.BANKCODE, bankcode},
                    {IPC.BANKNAME, bankname},
                    {"RECPERPAGE", recperpage},
                    {"RECINDEX", recindex}
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
        #region Get details partner bank  by BranchID

        public DataSet GetBankDetailsByID(string bankid, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(IPC.IPCTRANCODE, "PARTNERBANKDETAILS");
                hasInput.Add(IPC.SOURCEID, IPC.SOURCEIDVALUE);
                hasInput.Add(IPC.SOURCETRANREF, IPC.SOURCETRANREFVALUE);
                hasInput.Add(IPC.TRANDESC, "lấy thông tin chi tiết partner bank by bankid");

                hasInput.Add(IPC.BANKID, bankid);

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
        #region Insert partner bank
        public DataSet InsertPartnerBank(string bankcode, string bankname, string determination, string status,
            string ismanual, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(IPC.IPCTRANCODE, "PARTNERBANKINSERT");
                hasInput.Add(IPC.SOURCEID, IPC.SOURCEIDVALUE);
                hasInput.Add(IPC.SOURCETRANREF, IPC.SOURCETRANREFVALUE);
                hasInput.Add(IPC.TRANDESC, "thêm mới chi nhánh");

                hasInput.Add(IPC.BANKCODE, bankcode);
                hasInput.Add(IPC.BANKNAME, bankname);
                hasInput.Add(IPC.DETERMINATION, determination);
                hasInput.Add(IPC.STATUS, status);
                hasInput.Add(IPC.ISMANUAL, ismanual);

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
        #region Update partner bank
        public DataSet UpdatePartnerBank(string bankid, string bankcode, string bankname, string determination, string status,
            string ismanual, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(IPC.IPCTRANCODE, "PARTNERBANKUPDATE");
                hasInput.Add(IPC.SOURCEID, IPC.SOURCEIDVALUE);
                hasInput.Add(IPC.SOURCETRANREF, IPC.SOURCETRANREFVALUE);
                hasInput.Add(IPC.TRANDESC, "thêm mới chi nhánh");

                hasInput.Add(IPC.BANKID, bankid);
                hasInput.Add(IPC.BANKCODE, bankcode);
                hasInput.Add(IPC.BANKNAME, bankname);
                hasInput.Add(IPC.DETERMINATION, determination);
                hasInput.Add(IPC.STATUS, status);
                hasInput.Add(IPC.ISMANUAL, ismanual);

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
        #region DELETE partner bank

        public DataSet DeletePartnerBank(string bankID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(IPC.IPCTRANCODE, "PARTNERBANKDELETE");
                hasInput.Add(IPC.SOURCEID, IPC.SOURCEIDVALUE);
                hasInput.Add(IPC.SOURCETRANREF, IPC.SOURCETRANREFVALUE);
                hasInput.Add(IPC.TRANDESC, "Xóa partner bank");
                hasInput.Add(IPC.REVERSAL, "N");

                hasInput.Add(IPC.BANKID, bankID);

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

        #region check exists  partner bank branch

        public DataSet CheckExistsPartnerBankBranch(string bankID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(IPC.IPCTRANCODE, "CHECKBANKBRANCH");
                hasInput.Add(IPC.SOURCEID, IPC.SOURCEIDVALUE);
                hasInput.Add(IPC.SOURCETRANREF, IPC.SOURCETRANREFVALUE);
                hasInput.Add(IPC.TRANDESC, "kiem tra ton tai partner bank branch");
                hasInput.Add(IPC.REVERSAL, "N");

                hasInput.Add(IPC.BANKID, bankID);

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


        #region Search partner bank branch by condition
        public DataSet SearchBankBranchByCondition(string partnerbankid, string branchname,
            int recperpage, int recindex,
            ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable
                {
                    {IPC.IPCTRANCODE, "BANKBRANCHSEARCH"},
                    {IPC.SOURCEID, IPC.SOURCEIDVALUE},
                    {IPC.SOURCETRANREF, IPC.SOURCETRANREFVALUE},
                    {IPC.TRANDESC, "lấy thông tin chi tiết partner bank by condition"},
                    {IPC.PARTNERBANKID, partnerbankid},
                    {IPC.BRANCHNAME, branchname},
                    {"RECPERPAGE", recperpage},
                    {"RECINDEX", recindex}
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
        #region Get details partner bank branch by BranchID
        public DataSet GetBankBranchDetailsByID(string branchID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(IPC.IPCTRANCODE, "BANKBRANCHDETAILS");
                hasInput.Add(IPC.SOURCEID, IPC.SOURCEIDVALUE);
                hasInput.Add(IPC.SOURCETRANREF, IPC.SOURCETRANREFVALUE);
                hasInput.Add(IPC.TRANDESC, "lấy thông tin chi tiết partner bank branch by bankid");

                hasInput.Add(IPC.BRANCHID, branchID);

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
        #region Insert partner bank branch
        public DataSet InsertPartnerBankBranch(string branchcode, string branchname, string partnerbankid, string originalbranchcode, string regionid,string status,
             ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(IPC.IPCTRANCODE, "BANKBRANCHINSERT");
                hasInput.Add(IPC.SOURCEID, IPC.SOURCEIDVALUE);
                hasInput.Add(IPC.SOURCETRANREF, IPC.SOURCETRANREFVALUE);
                hasInput.Add(IPC.TRANDESC, "them moi chi nhanh doi tac");

                hasInput.Add(IPC.BRANCHCODE, branchcode);
                hasInput.Add(IPC.BRANCHNAME, branchname);
                hasInput.Add(IPC.PARTNERBANKID, partnerbankid);
                hasInput.Add(IPC.REGIONID, regionid);
                hasInput.Add(IPC.ORIGINALBRANCHCODE, originalbranchcode);
                hasInput.Add(IPC.STATUS, status);

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
        #region Update partner bank branch
        public DataSet UpdatePartnerBankBranch(string branchId,string branchcode, string branchname, string partnerbankid, string originalbranchcode,string regionid, string status,
            ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(IPC.IPCTRANCODE, "BANKBRANCHUPDATE");
                hasInput.Add(IPC.SOURCEID, IPC.SOURCEIDVALUE);
                hasInput.Add(IPC.SOURCETRANREF, IPC.SOURCETRANREFVALUE);
                hasInput.Add(IPC.TRANDESC, "cap nhat chi nhanh doi tac");
                hasInput.Add(IPC.BRANCHID, branchId);
                hasInput.Add(IPC.BRANCHCODE, branchcode);
                hasInput.Add(IPC.BRANCHNAME, branchname);
                hasInput.Add(IPC.REGIONID, regionid);
                hasInput.Add(IPC.PARTNERBANKID, partnerbankid);
                hasInput.Add(IPC.ORIGINALBRANCHCODE, originalbranchcode);
                hasInput.Add(IPC.STATUS, status);
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
        #region DELETE partner bank branch
        public DataSet DeletePartnerBankBranch(string branchID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(IPC.IPCTRANCODE, "BANKBRANCHDELETE");
                hasInput.Add(IPC.SOURCEID, IPC.SOURCEIDVALUE);
                hasInput.Add(IPC.SOURCETRANREF, IPC.SOURCETRANREFVALUE);
                hasInput.Add(IPC.TRANDESC, "Xóa partner bank branch");
                hasInput.Add(IPC.REVERSAL, "N");

                hasInput.Add(IPC.BRANCHID, branchID);

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


        #region Load all Bank

        public DataSet GetBankALL(ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(IPC.IPCTRANCODE, "GETBANKLIST");
                hasInput.Add(IPC.SOURCEID, IPC.SOURCEIDVALUE);
                hasInput.Add(IPC.SOURCETRANREF, IPC.SOURCETRANREFVALUE);
                hasInput.Add(IPC.TRANDESC, "lấy tat ca bank");
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

        #region Load all Bank

        public DataSet GetRegionALL(ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(IPC.IPCTRANCODE, "GETREGIONLIST");
                hasInput.Add(IPC.SOURCEID, IPC.SOURCEIDVALUE);
                hasInput.Add(IPC.SOURCETRANREF, IPC.SOURCETRANREFVALUE);
                hasInput.Add(IPC.TRANDESC, "lấy tat ca region bank");
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

        #region Load all other Bank branch

        public DataSet GetOtherBranchALL(ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(IPC.IPCTRANCODE, "GETOTHERBRANCH");
                hasInput.Add(IPC.SOURCEID, IPC.SOURCEIDVALUE);
                hasInput.Add(IPC.SOURCETRANREF, IPC.SOURCETRANREFVALUE);
                hasInput.Add(IPC.TRANDESC, "lấy tat ca other bank branch");
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

        #region import excel 
        public DataSet ImportOtherBranch(DataTable Otherbranch, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IMPORTOTHERBRANCH");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "IMPORTOTHERBRANCH ");
                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                #region import
                object[] other = new object[2];
                other[0] = "INSERTOTHERBRANCHMUTI";

                //add vao phan tu thu 2 mang object
                other[1] = Otherbranch;

                hasInput.Add("OTHERBRANCHLIST", other);
                #endregion
                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);


                DataSet ds = new DataSet();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATARESULT];
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

        #region UPDATE other branch muti 
        public DataSet UPDATEOtherBranchMuti(DataTable Otherbranch, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "OTBRANCHUPDATEMUTI");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "UPDATE OTHER BRANCH MUTI");
                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                #region CashBack
                object[] other = new object[2];
                other[0] = "UPDATEOTHERBRANCHMUTI";
                //add vao phan tu thu 2 mang object
                other[1] = Otherbranch;

                hasInput.Add("OTHERBRANCHLIST", other);
                #endregion
                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);


                DataSet ds = new DataSet();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATARESULT];
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


        #region update branch muti 
        public DataSet UPDATEBranchMuti(DataTable Otherbranch, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "BRANCHUPDATEMUTI");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "UPDATE OTHER BRANCH MUTI");
                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                #region CashBack
                object[] other = new object[2];
                other[0] = "UPDATEBRANCHMUTI";
                //add vao phan tu thu 2 mang object
                other[1] = Otherbranch;

                hasInput.Add("BRANCHLIST", other);
                #endregion
                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);


                DataSet ds = new DataSet();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATARESULT];
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

        #region Load all Bank Cross Border
        public DataSet GetCBBankALL(string CCYID,ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(IPC.IPCTRANCODE, "CBGETALLBANK");
                hasInput.Add(IPC.SOURCEID, IPC.SOURCEIDVALUE);
                hasInput.Add(IPC.CCYID, CCYID);
                hasInput.Add(IPC.SOURCETRANREF, IPC.SOURCETRANREFVALUE);
                hasInput.Add(IPC.TRANDESC, "lấy tat ca bank");
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