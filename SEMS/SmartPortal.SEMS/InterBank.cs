using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using SmartPortal.DAL;

namespace SmartPortal.SEMS
{
    public class InterBank
    {
        public DataSet DETAIL(string varname, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSIPCSYSVARDETAIL");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add("VARNAME", varname);

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
        public DataSet UPDATE(string varname, string varvalue, string userid, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSIPCSYSVARUPDATE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Update config");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");
                hasInput.Add("VARNAME", varname);
                hasInput.Add("VARVALUE", varvalue);
                hasInput.Add("USERID", userid);

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
        public DataSet HISTORY(int recPerPage, int recIndex, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "CBMNETHISTORY");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
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
        public DataSet DetailsFee(string feeid, string bankid, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSFEEINTERDETAIL");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "details fee");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.FEEID, feeid);
                hasInput.Add(SmartPortal.Constant.IPC.BANKID, bankid);


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
        public DataSet InsertFee(string feeid, string feename, string feetype, string fixamount, bool isladder, bool isregionfee, string chargelater, string ccyid, string usercreated, string datecreated, string branchid, bool isbiller, DataTable tableFeeDetail, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMS00006");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                object[] insertFee = new object[2];
                insertFee[0] = "EBA_FEE_INSERT";

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

                insertFee[1] = tableFee;
                hasInput.Add(SmartPortal.Constant.IPC.INSERTFEE, insertFee);


                object[] insertFeedetail = new object[2];
                insertFeedetail[0] = "EBA_FEEINTERDETAIL_INSERT";
                insertFeedetail[1] = tableFeeDetail;
                hasInput.Add(SmartPortal.Constant.IPC.INSERTFEEDETAILS, insertFeedetail);
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


        public DataSet LoadSwiftFee(ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSFEESWIFTDETAIL");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "details fee");
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

        public DataSet InsertSwiftFee(DataTable tableFeeDetail, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSSWIFTFEEINSERT");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                object[] insertFeedetail = new object[2];
                insertFeedetail[0] = "EBA_SWIFTFEEDETAIL_INSERT";
                insertFeedetail[1] = tableFeeDetail;
                hasInput.Add("INSERTSWIFTFEE", insertFeedetail);

                //object[] insertFeeTransfer = new object[2];
                //insertFeeTransfer[0] = null;
                //insertFeeTransfer[1] = null;
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

        public DataSet GetSwiftfeebycondition( string bankid , string feetype , string feepayer, string currency, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "GETSWIFTFEEBYCONDI");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);                
                hasInput.Add(SmartPortal.Constant.IPC.BANKID, bankid);                
                hasInput.Add(SmartPortal.Constant.IPC.FEETYPE, feetype);                
                hasInput.Add("FEEPAYER", feepayer);                
                hasInput.Add(SmartPortal.Constant.IPC.CCYID, currency);                
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

        public DataSet UpdateFee(string feeid, string feename, string feetype, string fixamount, bool isladder, bool isregionfee, string chargelater, string ccyid, string usermodified, string datemodified, string branchid, bool isbiller, string bankid, DataTable tableFeeDetail, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSUPDATEFEEINTER");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                object[] insertFee = new object[2];
                insertFee[0] = "EBA_FEE_UPDATE";
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
                insertFee[1] = tableFee;

                hasInput.Add(SmartPortal.Constant.IPC.UPDATEFEE, insertFee);

                DataTable tableDelete = new DataTable();
                DataColumn dcFeeID = new DataColumn("dcFeeID");
                DataColumn dcBankID = new DataColumn("dcBankID");
                tableDelete.Columns.Add(dcFeeID);
                tableDelete.Columns.Add(dcBankID);
                DataRow drRow = tableDelete.NewRow();
                drRow["dcFeeID"] = feeid;
                drRow["dcBankID"] = bankid;
                tableDelete.Rows.Add(drRow);

                object[] deleteFeedetail = new object[2];
                deleteFeedetail[0] = "EBA_FEEINTERDETAIL_DELETE";
                deleteFeedetail[1] = tableDelete;

                hasInput.Add("DELETEFEEINTERDETAILS", deleteFeedetail);

                object[] insertFeedetail = new object[2];
                insertFeedetail[0] = "EBA_FEEINTERDETAIL_INSERT";
                insertFeedetail[1] = tableFeeDetail;

                hasInput.Add(SmartPortal.Constant.IPC.INSERTFEEDETAILS, insertFeedetail);
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



        public DataSet DeleteSwiftFee(string feetype, string bankid, string feepayer, string ccyid,string from , string to , ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSDELETESWIFTFEE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add("FEETYPE", feetype);
                hasInput.Add("BANKID",bankid);
                hasInput.Add("FEEPAYER", feepayer);
                hasInput.Add("CCYID",ccyid);      
                hasInput.Add("FROMLIMIT",from);
                hasInput.Add("TOLIMIT",to);                                        
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
        public DataSet DeleteFee(string feeid, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

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
        public DataTable GetFilterRegion()
        {
            DataTable iRead;
            iRead = DataAccess.GetFromDataTable("SEMS_EBA_FILTER_REGION_GET", null);
            return iRead;
        }

        public DataTable Getfeeidbyfeename(string feename)
        {
            DataTable iRead;
            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@FEENAME";
            p1.Value = feename;
            p1.SqlDbType = SqlDbType.VarChar;

            iRead = DataAccess.GetFromDataTable("GETFEEIDBYFEENAME", p1);

            return iRead;
        }


    }
}
