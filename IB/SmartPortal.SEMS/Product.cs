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
       #region Search product by condition
       public DataSet GetProductByCondition(string proID, string proname, string custType, string Desc, ref string errorCode, ref string errorDesc)
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
               hasInput.Add(SmartPortal.Constant.IPC.DESC, Desc);


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
       public DataSet Insert(string proid, string proname, string custtype, string desc, DataTable ProductUserRight, ref string errorCode, ref string errorDesc)
       {
           try
           {
               //Show hastable result from IPC
               Hashtable hasInput = new Hashtable();
               Hashtable hasOutput = new Hashtable();

               //add key into input
               hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSPRODUCTINSERT");
               hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
               hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
               hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Tạo sản phẩm mới");

               #region Insert bảng customer
               object[] insertproduct = new object[2];
               insertproduct[0] = "SEMS_EBA_PRODUCT_INSERT";
               //tao bang chua thong tin product
               DataTable tblProduct = new DataTable();
               DataColumn colProID = new DataColumn("colProID");
               DataColumn colProName = new DataColumn("colProName");
               DataColumn colCusttype = new DataColumn("colCusttype");
               DataColumn colDesc = new DataColumn("colDesc");

               //add vào table product
               tblProduct.Columns.Add(colProID);
               tblProduct.Columns.Add(colProName);
               tblProduct.Columns.Add(colCusttype);
               tblProduct.Columns.Add(colDesc);
               

               //tao 1 dong du lieu
               DataRow row = tblProduct.NewRow();
               row["colProID"] = proid;
               row["colProName"] = proname;
               row["colCusttype"] = custtype;
               row["colDesc"] = desc;

               tblProduct.Rows.Add(row);

               //add vao phan tu thu 2 mang object
               insertproduct[1] = tblProduct;

               hasInput.Add(SmartPortal.Constant.IPC.INSERTPRODUCT, insertproduct);
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

       #region Update product
       public DataSet Update(string proid, string proname, string custtype, string desc, DataTable ProductUserRight, ref string errorCode, ref string errorDesc)
       {
           try
           {
               //Show hastable result from IPC
               Hashtable hasInput = new Hashtable();
               Hashtable hasOutput = new Hashtable();

               //add key into input
               hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSPRODUCTUPDATE");
               hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
               hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
               hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Update sản phẩm");

               #region Update bảng customer
               object[] updateproduct = new object[2];
               updateproduct[0] = "SEMS_EBA_PRODUCT_UPDATE";
               //tao bang chua thong tin product
               DataTable tblProduct = new DataTable();
               DataColumn colProID = new DataColumn("colProID");
               DataColumn colProName = new DataColumn("colProName");
               DataColumn colCusttype = new DataColumn("colCusttype");
               DataColumn colDesc = new DataColumn("colDesc");

               //add vào table product
               tblProduct.Columns.Add(colProID);
               tblProduct.Columns.Add(colProName);
               tblProduct.Columns.Add(colCusttype);
               tblProduct.Columns.Add(colDesc);


               //tao 1 dong du lieu
               DataRow row = tblProduct.NewRow();
               row["colProID"] = proid;
               row["colProName"] = proname;
               row["colCusttype"] = custtype;
               row["colDesc"] = desc;

               tblProduct.Rows.Add(row);

               //add vao phan tu thu 2 mang object
               updateproduct[1] = tblProduct;

               hasInput.Add(SmartPortal.Constant.IPC.UPDATEPRODUCT, updateproduct);
               #endregion

               #region Delete ProductRole to Insert(Update) new productrole
               object[] DeleteProductUserRight = new object[2];
               DeleteProductUserRight[0] = "SEMS_EBA_PRODUCTROLEDEFAULT_DELETEBYPROID";
               //tao bang chua thong tin product
               DataTable tblDelProduct = new DataTable();
               DataColumn colDelProID = new DataColumn("colDelProID");
               DataColumn colDelProName = new DataColumn("colDelProName");
               //add vào table product
               tblDelProduct.Columns.Add(colDelProID);
               tblDelProduct.Columns.Add(colDelProName);
               //tao 1 dong du lieu
               DataRow rowDel = tblDelProduct.NewRow();
               rowDel["colDelProID"] = proid;
               rowDel["colDelProName"] = proname;

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
       public DataSet GetProductLimitByCondition(string proID, string trancode, string ccyid, string tranlimit,string branchid, string limittype, ref string errorCode, ref string errorDesc)
       {
           try
           {
               //Show hastable result from IPC
               Hashtable hasInput = new Hashtable();
               Hashtable hasOutput = new Hashtable();

               //add key into input
               hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSPRODUCTLMSEARCH");
               hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
               hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
               hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Tìm kiếm Chi tiết hạn mức của sản phẩm");
               hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

               hasInput.Add(SmartPortal.Constant.IPC.PRODUCTID, proID);
               hasInput.Add(SmartPortal.Constant.IPC.TRANCODE, trancode);
               hasInput.Add(SmartPortal.Constant.IPC.CCYID, ccyid);
               hasInput.Add(SmartPortal.Constant.IPC.TRANLIMIT, tranlimit);
               hasInput.Add(SmartPortal.Constant.IPC.BRANCHID, branchid);
               hasInput.Add(SmartPortal.Constant.IPC.LIMITTYPE, limittype);

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
       public DataSet GetAllProLimitByProID(string proID,string trancode,string ccyid,string branchid, string limittype, ref string errorCode, ref string errorDesc)
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
       public DataSet InsertProLimit(string proid, string trancode, string ccyid, string tranlm,string countlm ,string totallmday ,string branchid, string limittype, ref string errorCode, ref string errorDesc)
       {
           try
           {
               //Show hastable result from IPC
               Hashtable hasInput = new Hashtable();
               Hashtable hasOutput = new Hashtable();

               //add key into input
               hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSPRODUCTLMINSERT");
               hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
               hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
               hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "tạo hạn mức của sản phẩm");
               hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

               hasInput.Add(SmartPortal.Constant.IPC.PRODUCTID, proid);
               hasInput.Add(SmartPortal.Constant.IPC.TRANCODE, trancode);
               hasInput.Add(SmartPortal.Constant.IPC.CCYID, ccyid);
               hasInput.Add(SmartPortal.Constant.IPC.TRANLIMIT, tranlm);
               hasInput.Add(SmartPortal.Constant.IPC.COUNTLIMIT, countlm);
               hasInput.Add(SmartPortal.Constant.IPC.TOTALLIMITDAY, totallmday);
               hasInput.Add(SmartPortal.Constant.IPC.BRANCHID, branchid);
               hasInput.Add(SmartPortal.Constant.IPC.LIMITTYPE, limittype);


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
       public DataSet UpdateProLimit(string proid,string trancode, string ccyid, string tranlm, string countlm, string totallmday,string branchid, string limittype, ref string errorCode, ref string errorDesc)
       {
           try
           {
               //Show hastable result from IPC
               Hashtable hasInput = new Hashtable();
               Hashtable hasOutput = new Hashtable();

               //add key into input
               hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSPRODUCTLMUPDATE");
               hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
               hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
               hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "sửa hạn mức của sản phẩm");
               hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

               hasInput.Add(SmartPortal.Constant.IPC.PRODUCTID, proid);
               hasInput.Add(SmartPortal.Constant.IPC.TRANCODE, trancode);
               hasInput.Add(SmartPortal.Constant.IPC.CCYID, ccyid);
               hasInput.Add(SmartPortal.Constant.IPC.TRANLIMIT, tranlm);
               hasInput.Add(SmartPortal.Constant.IPC.COUNTLIMIT, countlm);
               hasInput.Add(SmartPortal.Constant.IPC.TOTALLIMITDAY, totallmday);
               hasInput.Add(SmartPortal.Constant.IPC.BRANCHID, branchid);
               hasInput.Add(SmartPortal.Constant.IPC.LIMITTYPE, limittype);


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
       public DataSet DeleteProLimit(string proid, string trancode, string ccyid,string branchid, string limittype, ref string errorCode, ref string errorDesc)
       {
           try
           {
               //Show hastable result from IPC
               Hashtable hasInput = new Hashtable();
               Hashtable hasOutput = new Hashtable();

               //add key into input
               hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSPRODUCTLMDELETE");
               hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
               hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
               hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Xóa hạn mức của sản phẩm");
               hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

               hasInput.Add(SmartPortal.Constant.IPC.PRODUCTID, proid);
               hasInput.Add(SmartPortal.Constant.IPC.TRANCODE, trancode);
               hasInput.Add(SmartPortal.Constant.IPC.CCYID, ccyid);
               hasInput.Add(SmartPortal.Constant.IPC.BRANCHID, branchid);
               hasInput.Add(SmartPortal.Constant.IPC.LIMITTYPE, limittype);


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
