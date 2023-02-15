using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Collections;
using SmartPortal.DAL;
using System.Data.SqlClient;

namespace SmartPortal.SEMS
{
    public class Fee
    {
        #region Load FeeType
        public DataSet LoadFeeType(ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMS00004");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "load fee type");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                //hasInput.Add(SmartPortal.Constant.IPC.USERLEVEL, userlevel);
                //hasInput.Add(SmartPortal.Constant.IPC.DESCRIPTION, desc);


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
        #region search Fee
        public DataSet SearchFee(string feename, string feetype, string fixamong, string ccyid, int recPerPage, int recIndex, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMS00005");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "search fee");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.FEENAME, feename);
                hasInput.Add(SmartPortal.Constant.IPC.FEETYPE, feetype);
                hasInput.Add(SmartPortal.Constant.IPC.FIXAMOUNT, fixamong);
                hasInput.Add(SmartPortal.Constant.IPC.CCYID, ccyid);
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
        public DataSet LoadFeeCbb(string feetype, string ccyid, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "LOADFEECBB");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "search fee");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.CCYID, ccyid);
                hasInput.Add(SmartPortal.Constant.IPC.FEETYPE, feetype);


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


        public DataSet GetTransFerFeeByContractNo(string contractNo, string ccyid, string trancode, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "GETFEETRANSFER");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "search fee");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");


                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTNO, contractNo);
                hasInput.Add(SmartPortal.Constant.IPC.CCYID, ccyid);
                hasInput.Add(SmartPortal.Constant.IPC.TRANCODE, trancode);

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
        #region Region fee
        public DataTable LoadRegionFee(string status)
        {
            DataTable iRead;

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@STATUS";
            p1.Value = status;
            p1.SqlDbType = SqlDbType.VarChar;


            iRead = DataAccess.GetFromDataTable("EBA_REGIONFEE_LOAD", p1);

            return iRead;
        }
        #endregion
        #region Insert Fee
        public DataSet InsertFee(string feeid, string feename, string feetype, string fixamount, bool isladder, bool isregionfee, string chargelater, string ccyid, string usercreated, string datecreated, string branchid, bool isbiller, DataTable tableFeeDetail, DataTable tablfeeTransfer, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMS00006");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);

                #region Insert bảng customer
                object[] insertFee = new object[2];
                insertFee[0] = "EBA_FEE_INSERT";
                //tao bang chua thong tin process
                DataTable tableFee = new DataTable();
                DataColumn colfeeid = new DataColumn("colfeeid");
                DataColumn colfeename = new DataColumn("colfeename");
                DataColumn colfeetype = new DataColumn("colfeetype");
                DataColumn colfixamount = new DataColumn("colfixamount");
                DataColumn colisladder = new DataColumn("colisladder");
                DataColumn colisregionfee = new DataColumn("colisregionfee");
                DataColumn colchargelater = new DataColumn("colchargelater");
                DataColumn colccyid = new DataColumn("colccyid");
                DataColumn colusercreated = new DataColumn("colusercreated");
                DataColumn coldatecreated = new DataColumn("coldatecreated");
                DataColumn colbranchid = new DataColumn("colbranchid");
                DataColumn colisbiller = new DataColumn("colisbiller");

                //add vào table product
                tableFee.Columns.Add(colfeeid);
                tableFee.Columns.Add(colfeename);
                tableFee.Columns.Add(colfeetype);
                tableFee.Columns.Add(colfixamount);
                tableFee.Columns.Add(colisladder);
                tableFee.Columns.Add(colisregionfee);
                tableFee.Columns.Add(colchargelater);
                tableFee.Columns.Add(colccyid);
                tableFee.Columns.Add(colusercreated);
                tableFee.Columns.Add(coldatecreated);
                tableFee.Columns.Add(colbranchid);
                tableFee.Columns.Add(colisbiller);

                //tao 1 dong du lieu
                DataRow row = tableFee.NewRow();
                row["colfeeid"] = feeid;
                row["colfeename"] = feename;
                row["colfeetype"] = feetype;
                row["colfixamount"] = fixamount;
                row["colisladder"] = isladder;
                row["colisregionfee"] = isregionfee;
                row["colchargelater"] = chargelater;
                row["colccyid"] = ccyid;
                row["colusercreated"] = usercreated;
                row["coldatecreated"] = datecreated;
                row["colbranchid"] = branchid;
                row["colisbiller"] = isbiller;

                tableFee.Rows.Add(row);

                //add vao phan tu thu 2 mang object
                insertFee[1] = tableFee;

                hasInput.Add(SmartPortal.Constant.IPC.INSERTFEE, insertFee);
                #endregion

                #region Insert FEEDETAILS
                object[] insertFeedetail = new object[2];
                insertFeedetail[0] = "EBA_FEEDDETAILS_INSERT";

                //add vao phan tu thu 2 mang object
                insertFeedetail[1] = tableFeeDetail;

                hasInput.Add(SmartPortal.Constant.IPC.INSERTFEEDETAILS, insertFeedetail);
                #endregion


                #region Insert FEEDETAILS
                object[] insertFeeTransfer = new object[2];
                insertFeeTransfer[0] = "EBA_FEE_TRANSFER_INSERT";

                //add vao phan tu thu 2 mang object
                insertFeeTransfer[1] = tablfeeTransfer;

                hasInput.Add("INSERTFEETRANSFER", insertFeeTransfer);
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

        #region Insert Fee FX
        public DataSet InsertFeeFX(string feeid, string feename, string feetype, string fixamount, bool isladder, bool isregionfee, string chargelater, string ccyid, string usercreated, string datecreated, string branchid, bool isbiller, DataTable tableFeeDetail, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMS00006");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);

                #region Insert bảng customer
                object[] insertFee = new object[2];
                insertFee[0] = "EBA_FEE_INSERT";
                //tao bang chua thong tin process
                DataTable tableFee = new DataTable();
                DataColumn colfeeid = new DataColumn("colfeeid");
                DataColumn colfeename = new DataColumn("colfeename");
                DataColumn colfeetype = new DataColumn("colfeetype");
                DataColumn colfixamount = new DataColumn("colfixamount");
                DataColumn colisladder = new DataColumn("colisladder");
                DataColumn colisregionfee = new DataColumn("colisregionfee");
                DataColumn colchargelater = new DataColumn("colchargelater");
                DataColumn colccyid = new DataColumn("colccyid");
                DataColumn colusercreated = new DataColumn("colusercreated");
                DataColumn coldatecreated = new DataColumn("coldatecreated");
                DataColumn colbranchid = new DataColumn("colbranchid");
                DataColumn colisbiller = new DataColumn("colisbiller");

                //add vào table product
                tableFee.Columns.Add(colfeeid);
                tableFee.Columns.Add(colfeename);
                tableFee.Columns.Add(colfeetype);
                tableFee.Columns.Add(colfixamount);
                tableFee.Columns.Add(colisladder);
                tableFee.Columns.Add(colisregionfee);
                tableFee.Columns.Add(colchargelater);
                tableFee.Columns.Add(colccyid);
                tableFee.Columns.Add(colusercreated);
                tableFee.Columns.Add(coldatecreated);
                tableFee.Columns.Add(colbranchid);
                tableFee.Columns.Add(colisbiller);

                //tao 1 dong du lieu
                DataRow row = tableFee.NewRow();
                row["colfeeid"] = feeid;
                row["colfeename"] = feename;
                row["colfeetype"] = feetype;
                row["colfixamount"] = fixamount;
                row["colisladder"] = isladder;
                row["colisregionfee"] = isregionfee;
                row["colchargelater"] = chargelater;
                row["colccyid"] = ccyid;
                row["colusercreated"] = usercreated;
                row["coldatecreated"] = datecreated;
                row["colbranchid"] = branchid;
                row["colisbiller"] = isbiller;

                tableFee.Rows.Add(row);

                //add vao phan tu thu 2 mang object
                insertFee[1] = tableFee;

                hasInput.Add(SmartPortal.Constant.IPC.INSERTFEE, insertFee);
                #endregion

                #region Insert FEEDETAILS
                object[] insertFeedetail = new object[2];
                insertFeedetail[0] = "EBA_FEEDDETAILSFX_INSERT";

                //add vao phan tu thu 2 mang object
                insertFeedetail[1] = tableFeeDetail;


                //QUANTXA UPDATE ADD 22/03/22
                //--------------------------
                #region Insert FEETRANSFER
                object[] insertFeeTransfer = new object[2];
                insertFeeTransfer[0] = "EBA_FEE_TRANSFER_INSERT";

                insertFeeTransfer[1] = new DataTable();

                hasInput.Add("INSERTFEETRANSFER", insertFeeTransfer);
                #endregion
                //--------------------


                hasInput.Add(SmartPortal.Constant.IPC.INSERTFEEDETAILS, insertFeedetail);
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
        #region details Fee
        public DataSet DetailsFee(string feeid, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMS00008");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "details fee");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.FEEID, feeid);


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
        #region Update Fee
        public DataSet UpdateFee(string feeid, string feename, string feetype, string fixamount, bool isladder, bool isregionfee, string chargelater, string ccyid, string usermodified, string datemodified, string branchid, bool isbiller, DataTable tableFeeDetail, DataTable tablfeeTransfer, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMS00007");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);

                #region Insert bảng customer
                object[] insertFee = new object[2];
                insertFee[0] = "EBA_FEE_UPDATE";
                //tao bang chua thong tin process
                DataTable tableFee = new DataTable();
                DataColumn colfeeid = new DataColumn("colfeeid");
                DataColumn colfeename = new DataColumn("colfeename");
                DataColumn colfeetype = new DataColumn("colfeetype");
                DataColumn colfixamount = new DataColumn("colfixamount");
                DataColumn colisladder = new DataColumn("colisladder");
                DataColumn colisregionfee = new DataColumn("colisregionfee");
                DataColumn colchargelater = new DataColumn("colchargelater");
                DataColumn colccyid = new DataColumn("colccyid");
                DataColumn colusermodified = new DataColumn("colusermodified");
                DataColumn coldatemodified = new DataColumn("coldatemodified");
                DataColumn colbranchid = new DataColumn("colbranchid");
                DataColumn colisbiller = new DataColumn("colisbiller");

                //add vào table product
                tableFee.Columns.Add(colfeeid);
                tableFee.Columns.Add(colfeename);
                tableFee.Columns.Add(colfeetype);
                tableFee.Columns.Add(colfixamount);
                tableFee.Columns.Add(colisladder);
                tableFee.Columns.Add(colisregionfee);
                tableFee.Columns.Add(colchargelater);
                tableFee.Columns.Add(colccyid);
                tableFee.Columns.Add(colusermodified);
                tableFee.Columns.Add(coldatemodified);
                tableFee.Columns.Add(colbranchid);
                tableFee.Columns.Add(colisbiller);

                //tao 1 dong du lieu
                DataRow row = tableFee.NewRow();
                row["colfeeid"] = feeid;
                row["colfeename"] = feename;
                row["colfeetype"] = feetype;
                row["colfixamount"] = fixamount;
                row["colisladder"] = isladder;
                row["colisregionfee"] = isregionfee;
                row["colchargelater"] = chargelater;
                row["colccyid"] = ccyid;
                row["colusermodified"] = usermodified;
                row["coldatemodified"] = datemodified;
                row["colbranchid"] = branchid;
                row["colisbiller"] = isbiller;

                tableFee.Rows.Add(row);

                //add vao phan tu thu 2 mang object
                insertFee[1] = tableFee;

                hasInput.Add(SmartPortal.Constant.IPC.UPDATEFEE, insertFee);
                #endregion

                #region Insert FEEDETAILS
                object[] insertFeedetail = new object[2];
                insertFeedetail[0] = "EBA_FEEDDETAILS_INSERT";

                //add vao phan tu thu 2 mang object
                insertFeedetail[1] = tableFeeDetail;

                hasInput.Add(SmartPortal.Constant.IPC.INSERTFEEDETAILS, insertFeedetail);
                #endregion


                #region Update FEETRANSFER
                object[] insertFeeTransfer = new object[2];
                insertFeeTransfer[0] = "EBA_FEE_TRANSFER_INSERT";

                //add vao phan tu thu 2 mang object
                insertFeeTransfer[1] = tablfeeTransfer;

                hasInput.Add("INSERTFEETRANSFER", insertFeeTransfer);
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
        #region delete Fee
        public DataSet DeleteFee(string feeid, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMS00009");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "delete fee");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.FEEID, feeid);



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

        #region Update Fee
        public DataSet UpdateFeeFX(string feeid, string feename, string feetype, string fixamount, bool isladder, bool isregionfee, string chargelater, string ccyid, string usermodified, string datemodified, string branchid, bool isbiller, DataTable tableFeeDetail, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMS00007");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);

                #region Insert bảng customer
                object[] insertFee = new object[2];
                insertFee[0] = "EBA_FEE_UPDATE";
                //tao bang chua thong tin process
                DataTable tableFee = new DataTable();
                DataColumn colfeeid = new DataColumn("colfeeid");
                DataColumn colfeename = new DataColumn("colfeename");
                DataColumn colfeetype = new DataColumn("colfeetype");
                DataColumn colfixamount = new DataColumn("colfixamount");
                DataColumn colisladder = new DataColumn("colisladder");
                DataColumn colisregionfee = new DataColumn("colisregionfee");
                DataColumn colchargelater = new DataColumn("colchargelater");
                DataColumn colccyid = new DataColumn("colccyid");
                DataColumn colusermodified = new DataColumn("colusermodified");
                DataColumn coldatemodified = new DataColumn("coldatemodified");
                DataColumn colbranchid = new DataColumn("colbranchid");
                DataColumn colisbiller = new DataColumn("colisbiller");

                //add vào table product
                tableFee.Columns.Add(colfeeid);
                tableFee.Columns.Add(colfeename);
                tableFee.Columns.Add(colfeetype);
                tableFee.Columns.Add(colfixamount);
                tableFee.Columns.Add(colisladder);
                tableFee.Columns.Add(colisregionfee);
                tableFee.Columns.Add(colchargelater);
                tableFee.Columns.Add(colccyid);
                tableFee.Columns.Add(colusermodified);
                tableFee.Columns.Add(coldatemodified);
                tableFee.Columns.Add(colbranchid);
                tableFee.Columns.Add(colisbiller);

                //tao 1 dong du lieu
                DataRow row = tableFee.NewRow();
                row["colfeeid"] = feeid;
                row["colfeename"] = feename;
                row["colfeetype"] = feetype;
                row["colfixamount"] = fixamount;
                row["colisladder"] = isladder;
                row["colisregionfee"] = isregionfee;
                row["colchargelater"] = chargelater;
                row["colccyid"] = ccyid;
                row["colusermodified"] = usermodified;
                row["coldatemodified"] = datemodified;
                row["colbranchid"] = branchid;
                row["colisbiller"] = isbiller;

                tableFee.Rows.Add(row);

                //add vao phan tu thu 2 mang object
                insertFee[1] = tableFee;

                hasInput.Add(SmartPortal.Constant.IPC.UPDATEFEE, insertFee);
                #endregion

                #region Insert FEEDETAILS
                object[] insertFeedetail = new object[2];
                insertFeedetail[0] = "EBA_FEEDDETAILSFX_INSERT";

                //add vao phan tu thu 2 mang object
                insertFeedetail[1] = tableFeeDetail;

                //QUANTXA UPDATE 22/03/22
                //--------------------------
                #region Update FEETRANSFER
                object[] insertFeeTransfer = new object[2];
                insertFeeTransfer[0] = "EBA_FEE_TRANSFER_INSERT";

                //add vao phan tu thu 2 mang object
                insertFeeTransfer[1] = new DataTable();

                hasInput.Add("INSERTFEETRANSFER", insertFeeTransfer);
                #endregion


                hasInput.Add(SmartPortal.Constant.IPC.INSERTFEEDETAILS, insertFeedetail);
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
        public DataSet SearchProductFee(string proid, string trancode, string feeid, string ccyid,
            string ContractLevelId, string islocal, ref string errorCode, ref string errorDesc)
        {
            return SearchProductFee(proid, trancode, feeid, ccyid, ContractLevelId,string.Empty, islocal, 0, 0, ref errorCode, ref errorDesc);
        }

        #region search product Fee
        public DataSet SearchProductFee(string proid, string trancode, string feeid, string ccyid, string ContractLevelId, string feetype, string islocal, int recPerPage, int recIndex, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMS00010");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.PRODUCTID, proid);
                hasInput.Add(SmartPortal.Constant.IPC.TRANCODE, trancode);
                hasInput.Add(SmartPortal.Constant.IPC.FEEID, feeid);
                hasInput.Add(SmartPortal.Constant.IPC.CCYID, ccyid);
                hasInput.Add("ContractLevelId", ContractLevelId);
                hasInput.Add(SmartPortal.Constant.IPC.FEETYPE, feetype);
                hasInput.Add(SmartPortal.Constant.IPC.ISLOCAL, islocal);
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
        #region Insert product Fee
        public DataSet InsertProductFee(string proid, string trancode, string feeid, string desc, string ccyid, string payer, string islocal, string ContractLevelId, string UserCreated, string SharefeeId, string transferFee, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMS00011");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);

                hasInput.Add(SmartPortal.Constant.IPC.PRODUCTID, proid);
                hasInput.Add(SmartPortal.Constant.IPC.TRANCODE, trancode);
                hasInput.Add(SmartPortal.Constant.IPC.FEEID, feeid);
                hasInput.Add(SmartPortal.Constant.IPC.DESCRIPTION, desc);
                hasInput.Add(SmartPortal.Constant.IPC.CCYID, ccyid);
                hasInput.Add(SmartPortal.Constant.IPC.PAYER, payer);
                hasInput.Add(SmartPortal.Constant.IPC.ISLOCAL, islocal);
                hasInput.Add("ContractLevelId", ContractLevelId);
                hasInput.Add("UserCreate", UserCreated);
                hasInput.Add("SharefeeId", SharefeeId);
                hasInput.Add("TransferFeeID", transferFee);


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
        #region update product Fee
        public DataSet UpdateProductFee(string proid, string trancode, string feeid, string desc, string ccyid, string payer, string islocal, string ContractLevelId, string UserModified, string SharefeeId, string transferfeeid, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMS00012");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);

                hasInput.Add(SmartPortal.Constant.IPC.PRODUCTID, proid);
                hasInput.Add(SmartPortal.Constant.IPC.TRANCODE, trancode);
                hasInput.Add(SmartPortal.Constant.IPC.FEEID, feeid);
                hasInput.Add(SmartPortal.Constant.IPC.DESCRIPTION, desc);
                hasInput.Add(SmartPortal.Constant.IPC.CCYID, ccyid);
                hasInput.Add(SmartPortal.Constant.IPC.PAYER, payer);
                hasInput.Add(SmartPortal.Constant.IPC.ISLOCAL, islocal);
                hasInput.Add("ContractLevelId", ContractLevelId);
                hasInput.Add("UserModified", UserModified);
                hasInput.Add("SharefeeId", SharefeeId);
                hasInput.Add("TRANSFERFEEID", transferfeeid);

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
        #region delete product Fee
        public DataSet DeleteProductFee(string proid, string trancode, string feeid, string ccyid, string ContractLevelId, string islocal, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMS00013");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);

                hasInput.Add(SmartPortal.Constant.IPC.PRODUCTID, proid);
                hasInput.Add(SmartPortal.Constant.IPC.TRANCODE, trancode);
                hasInput.Add(SmartPortal.Constant.IPC.FEEID, feeid);
                hasInput.Add(SmartPortal.Constant.IPC.CCYID, ccyid);
                hasInput.Add("ContractLevelId", ContractLevelId);
                hasInput.Add(SmartPortal.Constant.IPC.ISLOCAL, islocal);


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

        #region search contract Fee
        public DataSet SearchContractFee(string contractno, string feeid, string ccyid, string fullname, string status , string branchid, string feetype, int recPerPage, int recIndex, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMS00018");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                //hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "search fee");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");
                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTNO, contractno);
                //hasInput.Add(SmartPortal.Constant.IPC.TRANCODE, trancode);
                hasInput.Add(SmartPortal.Constant.IPC.FEEID, feeid);
                hasInput.Add(SmartPortal.Constant.IPC.CCYID, ccyid);
                hasInput.Add(SmartPortal.Constant.IPC.FULLNAME, fullname);
                hasInput.Add(SmartPortal.Constant.IPC.STATUS, status);
                hasInput.Add(SmartPortal.Constant.IPC.BRANCHID, branchid);
                hasInput.Add(SmartPortal.Constant.IPC.FEETYPE, feetype);
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
        #region Insert contract Fee
        public DataSet InsertContractFee(string contractno, string trancode, string feeid, string desc, string ccyid, string payer, string islocal, string usercreated, string datecreated, string usermodified, string datemodified, string status, string branchid, string transferfeeid, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMS00019");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "insert fee");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTNO, contractno);
                hasInput.Add(SmartPortal.Constant.IPC.TRANCODE, trancode);
                hasInput.Add(SmartPortal.Constant.IPC.FEEID, feeid);
                hasInput.Add(SmartPortal.Constant.IPC.DESCRIPTION, desc);
                hasInput.Add(SmartPortal.Constant.IPC.CCYID, ccyid);
                hasInput.Add(SmartPortal.Constant.IPC.PAYER, payer);
                hasInput.Add(SmartPortal.Constant.IPC.ISLOCAL, islocal);
                hasInput.Add(SmartPortal.Constant.IPC.USERCREATED, usercreated);
                hasInput.Add(SmartPortal.Constant.IPC.DATECREATED, datecreated);
                hasInput.Add(SmartPortal.Constant.IPC.USERMODIFIED, usermodified);
                hasInput.Add(SmartPortal.Constant.IPC.DATEMODIFIED, datemodified);
                hasInput.Add(SmartPortal.Constant.IPC.STATUS, status);
                hasInput.Add(SmartPortal.Constant.IPC.BRANCHID, branchid);
                hasInput.Add("TRANSFERFEEID", transferfeeid);

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
        #region update contract Fee
        public DataSet UpdateContractFee(string contractno, string trancode, string feeid, string desc, string ccyid, string payer, string islocal, string usercreated, string datecreated, string usermodified, string datemodified, string status, string statuscurrent, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMS00020");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "update fee");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTNO, contractno);
                hasInput.Add(SmartPortal.Constant.IPC.TRANCODE, trancode);
                hasInput.Add(SmartPortal.Constant.IPC.FEEID, feeid);
                hasInput.Add(SmartPortal.Constant.IPC.DESCRIPTION, desc);
                hasInput.Add(SmartPortal.Constant.IPC.CCYID, ccyid);
                hasInput.Add(SmartPortal.Constant.IPC.PAYER, payer);
                hasInput.Add(SmartPortal.Constant.IPC.ISLOCAL, islocal);
                hasInput.Add(SmartPortal.Constant.IPC.USERCREATED, usercreated);
                hasInput.Add(SmartPortal.Constant.IPC.DATECREATED, datecreated);
                hasInput.Add(SmartPortal.Constant.IPC.USERMODIFIED, usermodified);
                hasInput.Add(SmartPortal.Constant.IPC.DATEMODIFIED, datemodified);
                hasInput.Add(SmartPortal.Constant.IPC.STATUS, status);
                hasInput.Add(SmartPortal.Constant.IPC.STATUSCURRENT, statuscurrent);



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
        #region delete contract Fee
        public DataSet DeleteContractFee(string contractno, string trancode, string feeid, string ccyid, string status, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMS00021");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "delete fee");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTNO, contractno);
                hasInput.Add(SmartPortal.Constant.IPC.TRANCODE, trancode);
                hasInput.Add(SmartPortal.Constant.IPC.FEEID, feeid);
                hasInput.Add(SmartPortal.Constant.IPC.CCYID, ccyid);
                hasInput.Add(SmartPortal.Constant.IPC.STATUS, status);


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
        #region search sms fee
        public DataSet SearchFeeSMSNotify(string ROLEID, string FEEID, string ROLENAME, string FEENAME, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSSMSNOTIFYFEE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "search fee");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.ROLEID, ROLEID);
                hasInput.Add(SmartPortal.Constant.IPC.FEEID, FEEID);
                hasInput.Add(SmartPortal.Constant.IPC.ROLENAME, ROLENAME);
                hasInput.Add(SmartPortal.Constant.IPC.FEENAME, FEENAME);


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
        #region search sms fee
        public DataSet GetSMSNotifyFee(string expcd, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSLOADSMSNOTIFYFEE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "search fee");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add("expcd", expcd);



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
        #region Insert sms notify Fee
        public DataSet InsertFeeSMSNotify(string ROLEID, string FEEID, string FEENAME, double AMOUNT, string USERCREATE, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SMSNOTIFYINSERT");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "insert fee");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.ROLEID, ROLEID);
                hasInput.Add(SmartPortal.Constant.IPC.FEEID, FEEID);
                hasInput.Add(SmartPortal.Constant.IPC.FEENAME, FEENAME);
                hasInput.Add(SmartPortal.Constant.IPC.AMOUNT, AMOUNT);
                hasInput.Add(SmartPortal.Constant.IPC.USERCREATE, USERCREATE);


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
        #region Update sms notify Fee
        public DataSet UpdateFeeSMSNotify(string ROLEID, string FEEID, string FEENAME, double AMOUNT, string USERCREATE, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SMSNOTIFYFEEUPDATE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "update fee");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.ROLEID, ROLEID);
                hasInput.Add(SmartPortal.Constant.IPC.FEEID, FEEID);
                hasInput.Add(SmartPortal.Constant.IPC.FEENAME, FEENAME);
                hasInput.Add(SmartPortal.Constant.IPC.AMOUNT, AMOUNT);
                hasInput.Add(SmartPortal.Constant.IPC.USERCREATE, USERCREATE);


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
        #region delete sms notify Fee
        public DataSet DeleteFeeSMSNotify(string ROLEID, string FEEID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SMSNOTIFYFEEDELETE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "delete fee");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.ROLEID, ROLEID);
                hasInput.Add(SmartPortal.Constant.IPC.FEEID, FEEID);



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
        #region check owned fee register sms notification
        public Hashtable CheckSmsOwnedFee(string ACCTNO, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000131");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "check owned fee register sms notification");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add("ACCTNO", ACCTNO);



                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString().Equals("-20000"))
                {
                    errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                    errorDesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                    //ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATASET];
                }
                else
                {
                    errorCode = "0";
                }

                return hasOutput;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        public DataTable GetFeeDetailsTest(string feeid)
        {
            DataTable iRead;

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@FEEID";
            p1.Value = feeid;
            p1.SqlDbType = SqlDbType.VarChar;


            iRead = DataAccess.GetFromDataTable("EBA_FEE_GETALLBYFEEID", p1);

            return iRead;
        }
    }
    public class OtcFee
    {
        public DataSet SearchOtcFee(string trancode, string ccyid, string islocal, string contractlevelid, string feeid, int recPerPage, int recIndex, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "OTCFEESEARCH");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANCODE, trancode);
                hasInput.Add(SmartPortal.Constant.IPC.CCYID, ccyid);
                hasInput.Add(SmartPortal.Constant.IPC.ISLOCAL, islocal);
                hasInput.Add(SmartPortal.Constant.IPC.FEEID, feeid);
                hasInput.Add("ContractLevelId", contractlevelid);
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

        public DataSet InsertOtcFee(string trancode, string desc, string ccyid, string islocal, string UserCreated, string contractlevelid, string feeid, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "OTCFEEINSERT");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANCODE, trancode);
                hasInput.Add(SmartPortal.Constant.IPC.DESCRIPTION, desc);
                hasInput.Add(SmartPortal.Constant.IPC.CCYID, ccyid);
                hasInput.Add(SmartPortal.Constant.IPC.ISLOCAL, islocal);
                hasInput.Add("UserCreate", UserCreated);
                hasInput.Add(SmartPortal.Constant.IPC.FEEID, feeid);
                hasInput.Add("ContractLevelId", contractlevelid);
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

        public DataSet UpdateOtcFee(string trancode, string desc, string ccyid, string islocal, string UserCreated, string contractlevelid, string feeid, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "OTCFEEUPDATE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANCODE, trancode);
                hasInput.Add(SmartPortal.Constant.IPC.DESCRIPTION, desc);
                hasInput.Add(SmartPortal.Constant.IPC.CCYID, ccyid);
                hasInput.Add(SmartPortal.Constant.IPC.ISLOCAL, islocal);
                hasInput.Add("UserCreate", UserCreated);
                hasInput.Add(SmartPortal.Constant.IPC.FEEID, feeid);
                hasInput.Add("ContractLevelId", contractlevelid);
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

        #region delete otc Fee
        public DataSet DeleteOtcFee(string trancode, string feeid, string ccyid, string ContractLevelId, string islocal, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "OTCFEEDELETE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANCODE, trancode);
                hasInput.Add(SmartPortal.Constant.IPC.FEEID, feeid);
                hasInput.Add(SmartPortal.Constant.IPC.CCYID, ccyid);
                hasInput.Add("ContractLevelId", ContractLevelId);
                hasInput.Add(SmartPortal.Constant.IPC.ISLOCAL, islocal);


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
        public DataTable GetOTCFee(string ipctrancode, string amount, string CCYID)
        {
            DataTable iRead;

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@IPCTRANCODE";
            p1.Value = ipctrancode;
            p1.SqlDbType = SqlDbType.VarChar;

            SqlParameter p2 = new SqlParameter();
            p2.ParameterName = "@AMOUNT";
            p2.Value = amount;
            p2.SqlDbType = SqlDbType.VarChar;

            SqlParameter p3 = new SqlParameter();
            p3.ParameterName = "@CCYID";
            p3.Value = CCYID;
            p3.SqlDbType = SqlDbType.VarChar;

            iRead = DataAccess.GetFromDataTable("IPC_CALCULATOR_OTCFEE", p1, p2, p3);

            return iRead;
        }
        public DataSet GetTransProductFeeByIsShow(int isshow, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "TRANSPRODUCTFEE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.ISSHOW, isshow);
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
        public DataTable GetAccountByTransid(string ipctranid, string accounttype)
        {
            DataTable iRead;

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@IPCTRANSID";
            p1.Value = ipctranid;
            p1.SqlDbType = SqlDbType.VarChar;

            SqlParameter p2 = new SqlParameter();
            p2.ParameterName = "@ACCTTYPE";
            p2.Value = accounttype;
            p2.SqlDbType = SqlDbType.VarChar;

            iRead = DataAccess.GetFromDataTable("SEMS_GETACCOUNTNO_BYTRANID", p1, p2);

            return iRead;
        }
    }
}

