using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using SmartPortal.DAL;

namespace SmartPortal.SEMS
{
    public class Product
    {
        public DataSet GetProductByCondition(string proID, string proname, string CustType, string Desc, ref string errorCode, ref string errorDesc)
        {
            return GetProductByCondition(proID,  proname, CustType, "",  Desc, 0, 0, ref errorCode, ref errorDesc);
        }
        public DataSet GetProductCorpByCondition(string proID, string proname, string CustType, string ProductType, string Desc, ref string errorCode, ref string errorDesc)
        {
            return GetProductByCondition(proID, proname, CustType, ProductType, Desc, 0, 0, ref errorCode, ref errorDesc);
        }
        #region Search product by condition
        public DataSet GetProductByCondition(string proID, string proname, string custType, string productType, string Desc, int recPerPage, int recIndex, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSPRODUCTSEARCH");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Tìm kiếm sản phẩm Ebanking");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.PRODUCTID, proID);
                hasInput.Add(SmartPortal.Constant.IPC.PRODUCTNAME, proname);
                hasInput.Add(SmartPortal.Constant.IPC.CUSTTYPE, custType);
                hasInput.Add("PRODUCTTYPE", productType);
                hasInput.Add(SmartPortal.Constant.IPC.DESC, Desc);
                hasInput.Add("RECPERPAGE", recPerPage);
                hasInput.Add("RECINDEX", recIndex);


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

        #region DELETE PRODUCT   
        public DataSet DeleteProduct(string proID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSPRODUCTDELETE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Xóa sản phẩm");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.PRODUCTID, proID);

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

        #region view details product by productid
        public DataSet GetProductByID(string proID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSPRODUCTDETAILS");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "chi tiết sản phẩm Ebanking");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.PRODUCTID, proID);



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

        #region Insert product
        public DataSet Insert(string ProductID, string ProductName, string CustType, string Description, string ProductType, string GRP_ID, string Status, string UserCreated, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSPRODUCTINSERT");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add("ProductID", ProductID);
                hasInput.Add("ProductName", ProductName);
                hasInput.Add("CustType", CustType);
                hasInput.Add("Description", Description);
                hasInput.Add("ProductType", ProductType);
                hasInput.Add("GRP_ID", GRP_ID);
                hasInput.Add("Status", Status);
                hasInput.Add("UserCreated", UserCreated);

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

        #region Update product
        public DataSet Update(string ProductID, string ProductName, string CustType, string Description, string ProductType, string GRP_ID, string Status, string UserModified, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSPRODUCTUPDATE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add("ProductID", ProductID);
                hasInput.Add("ProductName", ProductName);
                hasInput.Add("CustType", CustType);
                hasInput.Add("Description", Description);
                hasInput.Add("ProductType", ProductType);
                hasInput.Add("GRP_ID", GRP_ID);
                hasInput.Add("Status", Status);
                hasInput.Add("UserModified", UserModified);

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

        #region get TranName by Trancode in SMP_Pages [PRODUCTLIMIT]
        public DataSet GetTranNameByTrancode(ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSPRODUCTLMGTRNAME");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Lấy TranName by trancode");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");


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

        #region Search PRODUCTLIMIT by condition
        public DataSet GetProductLimitByCondition(string proID, string trancode, string ccyid, string tranlimit, string branchid, string limittype, string ContractLevelId, int recPerPage, int recIndex, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSPRODUCTLMSEARCH");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);

                hasInput.Add(SmartPortal.Constant.IPC.PRODUCTID, proID);
                hasInput.Add(SmartPortal.Constant.IPC.TRANCODE, trancode);
                hasInput.Add(SmartPortal.Constant.IPC.CCYID, ccyid);
                hasInput.Add(SmartPortal.Constant.IPC.TRANLIMIT, tranlimit);
                hasInput.Add(SmartPortal.Constant.IPC.BRANCHID, branchid);
                hasInput.Add(SmartPortal.Constant.IPC.LIMITTYPE, limittype);
                hasInput.Add("ContractLevelId", ContractLevelId);
                hasInput.Add("RECPERPAGE", recPerPage);
                hasInput.Add("RECINDEX", recIndex);

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

        #region view details ProductLimit by ProductID
        public DataSet GetAllProLimitByProID(string proID, string trancode, string ccyid, string branchid, string limittype, decimal ContractLevelId, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSPRODUCTLMGETALL");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Xem Chi tiết hạn mức của sản phẩm");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.PRODUCTID, proID);
                hasInput.Add(SmartPortal.Constant.IPC.TRANCODE, trancode);
                hasInput.Add(SmartPortal.Constant.IPC.CCYID, ccyid);
                hasInput.Add(SmartPortal.Constant.IPC.BRANCHID, branchid);
                hasInput.Add(SmartPortal.Constant.IPC.LIMITTYPE, limittype);
                hasInput.Add("ContractLevelId", ContractLevelId);


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

        #region Insert ProductLimit
        public DataSet InsertProLimit(string proid, string trancode, string ccyid, string branchid,string biolimit, string limittype, decimal contractlevel, string tranlm, string totalLimit, string countlm, string unittype, string status, string desc, string usercreated, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSPRODUCTLMINSERT");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);

                hasInput.Add(SmartPortal.Constant.IPC.PRODUCTID, proid);
                hasInput.Add(SmartPortal.Constant.IPC.TRANCODE, trancode);
                hasInput.Add(SmartPortal.Constant.IPC.CCYID, ccyid);
                hasInput.Add(SmartPortal.Constant.IPC.TRANLIMIT, tranlm);
                hasInput.Add(SmartPortal.Constant.IPC.COUNTLIMIT, countlm);
                hasInput.Add(SmartPortal.Constant.IPC.BRANCHID, branchid);
                hasInput.Add(SmartPortal.Constant.IPC.LIMITTYPE, limittype);
                hasInput.Add("ContractLevelId", contractlevel);
                hasInput.Add("UnitType", unittype);
                hasInput.Add("Status", status);
                hasInput.Add("Description", desc);
                hasInput.Add("UserCreated", usercreated);
                hasInput.Add("TotalLimit", totalLimit);
                hasInput.Add("BIOLIMIT", biolimit);

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

        #region Update ProductLimit
        public DataSet UpdateProLimit(string proid, string trancode, string ccyid, string branchid, string biolimit, string limittype, decimal contractlevel, string tranlm, string totalLimit, string countlm, string unittype, string status, string desc, string usermodified, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSPRODUCTLMUPDATE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);

                hasInput.Add(SmartPortal.Constant.IPC.PRODUCTID, proid);
                hasInput.Add(SmartPortal.Constant.IPC.TRANCODE, trancode);
                hasInput.Add(SmartPortal.Constant.IPC.CCYID, ccyid);
                hasInput.Add(SmartPortal.Constant.IPC.TRANLIMIT, tranlm);
                hasInput.Add(SmartPortal.Constant.IPC.COUNTLIMIT, countlm);
                hasInput.Add(SmartPortal.Constant.IPC.BRANCHID, branchid);
                hasInput.Add(SmartPortal.Constant.IPC.LIMITTYPE, limittype);
                hasInput.Add("ContractLevelId", contractlevel);
                hasInput.Add("UnitType", unittype);
                hasInput.Add("Status", status);
                hasInput.Add("Description", desc);
                hasInput.Add("UserModified", usermodified);
                hasInput.Add("TotalLimit", totalLimit);
                hasInput.Add("BIOLIMIT", biolimit);

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

        #region Delete ProductLimit
        public DataSet DeleteProLimit(string proid, string trancode, string ccyid, string branchid, string limittype, decimal contractlevel, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSPRODUCTLMDELETE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.PRODUCTID, proid);
                hasInput.Add(SmartPortal.Constant.IPC.TRANCODE, trancode);
                hasInput.Add(SmartPortal.Constant.IPC.CCYID, ccyid);
                hasInput.Add(SmartPortal.Constant.IPC.BRANCHID, branchid);
                hasInput.Add(SmartPortal.Constant.IPC.LIMITTYPE, limittype);
                hasInput.Add("ContractLevelId", contractlevel);


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

        #region Lay tat ca cac quyen san pham
        public DataTable GetRoleOfProduct(string productID)
        {
            try
            {
                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@productID";
                p1.Value = productID;
                p1.SqlDbType = SqlDbType.VarChar;

                return DataAccess.GetFromDataTable("SEMS_EBA_PRODUCT_GETALLROLE", p1);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Lay loai tien te vutran 15122014
        public DataTable LoaddAllCCYID(ref string errorcode, ref string errordesc)
        {
            try
            {
                return DataAccess.GetFromDataTable("SEMS_EBA_PRODUCT_GETALLCCYID");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Load accounting group
        public DataSet GetAccountingGroup(string productType, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSACGRPSEARCH");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add("PRODUCTTYPE", productType);
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

        #region Search product role
        public DataSet GetProductRole(string proID, string proname, string CONTRACT_LEVEL_ID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "PRODUCTROLESEARCH");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.PRODUCTID, proID);
                hasInput.Add(SmartPortal.Constant.IPC.PRODUCTNAME, proname);
                hasInput.Add("CONTRACT_LEVEL_ID", CONTRACT_LEVEL_ID);
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

        #region Insert product role
        public DataSet InsertRole(string ProductID, decimal CONTRACT_LEVEL_ID, DataTable ProductUserRight, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "PRODUCTROLEINSERT");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Tạo sản phẩm mới");

                #region Check exists
                object[] ProductRight = new object[2];
                ProductRight[0] = "SEMS_EBA_PRODUCTROLEDEFAULT_CHECKEXISTS";
                //tao bang chua thong tin product
                DataTable tblDelProduct = new DataTable();
                DataColumn colDelProID = new DataColumn("colProductID");
                DataColumn colDelProName = new DataColumn("colCONTRACT_LEVEL_ID");
                //add vào table product
                tblDelProduct.Columns.Add(colDelProID);
                tblDelProduct.Columns.Add(colDelProName);
                //tao 1 dong du lieu
                DataRow rowDCheck = tblDelProduct.NewRow();
                rowDCheck["colProductID"] = ProductID;
                rowDCheck["colCONTRACT_LEVEL_ID"] = CONTRACT_LEVEL_ID;

                tblDelProduct.Rows.Add(rowDCheck);
                //add vao phan tu thu 2 mang object
                ProductRight[1] = tblDelProduct;

                hasInput.Add("PRODUCTROLE_CHECKEXISTS", ProductRight);
                #endregion

                object[] insertProductUserRight = new object[2];
                insertProductUserRight[0] = "SEMS_EBA_PRODUCTROLEDEFAULT_INSERT";

                insertProductUserRight[1] = ProductUserRight;
                hasInput.Add(SmartPortal.Constant.IPC.INSERTPRODUCTROLE, insertProductUserRight);

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

        #region Update product role
        public DataSet UpdateRole(string ProductID, decimal CONTRACT_LEVEL_ID, DataTable ProductUserRight, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "UPDATEPRODUCTROLE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);

                #region Delete ProductRole to Insert(Update) new productrole
                object[] DeleteProductUserRight = new object[2];
                DeleteProductUserRight[0] = "SEMS_EBA_PRODUCTROLEDEFAULT_DELETEBYPROID";
                //tao bang chua thong tin product
                DataTable tblDelProduct = new DataTable();
                DataColumn colDelProID = new DataColumn("colDelProductID");
                DataColumn colDelProName = new DataColumn("colDelCONTRACT_LEVEL_ID");
                //add vào table product
                tblDelProduct.Columns.Add(colDelProID);
                tblDelProduct.Columns.Add(colDelProName);
                //tao 1 dong du lieu
                DataRow rowDel = tblDelProduct.NewRow();
                rowDel["colDelProductID"] = ProductID;
                rowDel["colDelCONTRACT_LEVEL_ID"] = CONTRACT_LEVEL_ID;

                tblDelProduct.Rows.Add(rowDel);
                //add vao phan tu thu 2 mang object
                DeleteProductUserRight[1] = tblDelProduct;

                hasInput.Add(SmartPortal.Constant.IPC.DELETEPRODUCTROLE, DeleteProductUserRight);
                #endregion

                #region Insert quyền PRODUCT
                object[] insertProductUserRight = new object[2];
                insertProductUserRight[0] = "SEMS_EBA_PRODUCTROLEDEFAULT_INSERT";

                //add vao phan tu thu 2 mang object
                insertProductUserRight[1] = ProductUserRight;
                hasInput.Add(SmartPortal.Constant.IPC.INSERTPRODUCTROLE, insertProductUserRight);
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
        #endregion
        #region Delete role
        public DataSet DeleteRole(string ProductID, decimal CONTRACT_LEVEL_ID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSPROROLEDELETE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.PRODUCTID, ProductID);
                hasInput.Add("CONTRACT_LEVEL_ID", CONTRACT_LEVEL_ID);
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

        #region Search product by condition
        public DataSet LoadProductCbb(string custType, string productType, string contracLevel, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSLOADPROCDUCTCBB");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Load product by product type");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.CUSTTYPE, custType);
                hasInput.Add("PRODUCTTYPE", productType);
                hasInput.Add("CONTRACTLEVEL", contracLevel);


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

        #region Load Contract Level Active
        public DataSet LoadContractLevelActive(ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "CONTRACTLEVELACTIVE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
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
        #region get TranName by Trancode in SMP_Pages [PRODUCTLIMIT]
        public DataSet GetTranNameByProduct(string ProductID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSTRANBYPRODUCT");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.PRODUCTID, ProductID);
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

        public DataSet GetTranNameByProduct2(string ProductID, string LimitType, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSTRANBYPRODUCT2");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.PRODUCTID, ProductID);
                hasInput.Add(SmartPortal.Constant.IPC.LIMITTYPE, LimitType);
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
        #region Load Biller
        public DataTable LoaddBiller(ref string errorcode, ref string errordesc)
        {
            try
            {
                return DataAccess.GetFromDataTable("MB_GETBILLERLIST");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Search product by userType
        public DataSet LoadProductByUserType(string subUserType, string productType, string IsUsers, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSLOADPRODUCTCBB");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Load product by product type");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add("SUBUSERTYPE", subUserType);
                hasInput.Add("PRODUCTTYPE", productType);
                hasInput.Add("ISUSERS", IsUsers);
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

        #region LoadUserType

        public DataSet LoadSubUserByType(string type, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "GETSUBUSTYPE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Load subuser type");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");
                hasInput.Add(SmartPortal.Constant.IPC.TYPE, type);
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

        public DataSet InsertProductSubUserType(string productId, string productType, string subUserCode, string isUse,
            ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SUBUSERTYPEUPDATE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Load subuser type");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");
                hasInput.Add(SmartPortal.Constant.IPC.PRODUCTID, productId);
                hasInput.Add(SmartPortal.Constant.IPC.TYPE, productType);
                hasInput.Add(SmartPortal.Constant.IPC.SUBUSERCODE, subUserCode);
                hasInput.Add(SmartPortal.Constant.IPC.IsUse, isUse);
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


        public DataSet LoadProductSubUserType(string productId, string productType, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "GETSUBUSTYPELOAD");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Load subuser type");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.PRODUCTID, productId);
                hasInput.Add(SmartPortal.Constant.IPC.TYPE, productType);
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


        public DataSet GetTranNameByContractNo(string contractNo, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSTRANBYCONTRACTNO");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTNO, contractNo);
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
