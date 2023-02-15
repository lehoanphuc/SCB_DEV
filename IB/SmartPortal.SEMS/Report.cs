using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Data.SqlClient;
using SmartPortal.DAL;
using System.Collections;

namespace SmartPortal.SEMS
{
   public class Report
    {
       #region lấy param từ database
       public DataTable LoadParam(string reportID,string langID)
       {
           DataTable iRead;

           SqlParameter p1 = new SqlParameter();
           p1.ParameterName = "@reportID";
           p1.Value = reportID;
           p1.SqlDbType = SqlDbType.Text;

           SqlParameter p2 = new SqlParameter();
           p2.ParameterName = "@langID";
           p2.Value = langID;
           p2.SqlDbType = SqlDbType.Text;


           iRead = DataAccess.GetFromDataTable("rpt_LoadParam", p1,p2);
           
          return iRead;
       }
       #endregion

       #region lấy thong tin report database
       public DataTable LoadReport(string reportID)
       {
           DataTable iRead;

           SqlParameter p1 = new SqlParameter();
           p1.ParameterName = "@reportID";
           p1.Value = reportID;
           p1.SqlDbType = SqlDbType.Text;

           iRead = DataAccess.GetFromDataTable("rpt_LoadReport", p1);

           return iRead;
       }
       #endregion

       #region lấy tat ca thong tin report
       public DataTable LoadAllReport(string userName)
       {
           DataTable iRead;

           SqlParameter p1 = new SqlParameter();
           p1.ParameterName = "@userName";
           p1.Value = userName;
           p1.SqlDbType = SqlDbType.Text;

           iRead = DataAccess.GetFromDataTable("rpt_LoadAllReport", p1);

           return iRead;
       }
       #endregion

       #region search report
       public DataSet SearchReport(string prtid, string rptname, string serviceid,ref string errorCode, ref string errorDesc)
       {
           try
           {
               //Show hastable result from IPC
               Hashtable hasInput = new Hashtable();
               Hashtable hasOutput = new Hashtable();

               //add key into input
               hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMS00026");
               hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
               hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
               hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "search report");
               hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

               hasInput.Add(SmartPortal.Constant.IPC.RPTID, prtid);
               hasInput.Add(SmartPortal.Constant.IPC.RPTNAME, rptname);
               hasInput.Add(SmartPortal.Constant.IPC.SERVICEID, serviceid);



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

       #region Insert Report
       public DataSet InsertReport(string rptid, string rptname, string serviceid, string rptfile, bool isshow, string usercreated, string datecreated, string pageparamid, string pageviewid, DataTable tblReportDetail, ref string errorCode, ref string errorDesc)
       {
           try
           {
               //Show hastable result from IPC
               Hashtable hasInput = new Hashtable();
               Hashtable hasOutput = new Hashtable();

               //add key into input
               hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMS00027");
               hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
               hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
               hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "thêm báo cáo");

               #region Insert bảng report
               object[] insertreport = new object[2];
               insertreport[0] = "SEMS_RTPREPORT_INSERT";
               //tao bang chua thong tin process
               DataTable tblinsertreport = new DataTable();
               DataColumn colrptid = new DataColumn("colrptid");
               DataColumn colrptname = new DataColumn("colrptname");
               DataColumn colserviceid = new DataColumn("colserviceid");
               DataColumn colrptfile = new DataColumn("colrptfile");
               DataColumn colisshow = new DataColumn("colisshow");
               DataColumn colusercreated = new DataColumn("colusercreated");
               DataColumn coldatecreated = new DataColumn("coldatecreated");
               DataColumn colpageparamid = new DataColumn("colpageparamid");
               DataColumn colpageviewid = new DataColumn("colpageviewid");


               //add vào table product
               tblinsertreport.Columns.Add(colrptid);
               tblinsertreport.Columns.Add(colrptname);
               tblinsertreport.Columns.Add(colserviceid);
               tblinsertreport.Columns.Add(colrptfile);
               tblinsertreport.Columns.Add(colisshow);
               tblinsertreport.Columns.Add(colusercreated);
               tblinsertreport.Columns.Add(coldatecreated);
               tblinsertreport.Columns.Add(colpageparamid);
               tblinsertreport.Columns.Add(colpageviewid);


               //tao 1 dong du lieu
               DataRow row = tblinsertreport.NewRow();
               row["colrptid"] = rptid;
               row["colrptname"] = rptname;
               row["colserviceid"] = serviceid;
               row["colrptfile"] = rptfile;
               row["colisshow"] = isshow;
               row["colusercreated"] = usercreated;
               row["coldatecreated"] = datecreated;
               row["colpageparamid"] = pageparamid;
               row["colpageviewid"] = pageviewid;


               tblinsertreport.Rows.Add(row);

               //add vao phan tu thu 2 mang object
               insertreport[1] = tblinsertreport;

               hasInput.Add(SmartPortal.Constant.IPC.INSERTREPORT, insertreport);
               #endregion


               #region Insert REPORT DETAILS
               object[] insertreportdetail = new object[2];
               insertreportdetail[0] = "SEMS_RPT_REPORTDETAIL_INSERT";

               //add vao phan tu thu 2 mang object
               insertreportdetail[1] = tblReportDetail;

               hasInput.Add(SmartPortal.Constant.IPC.INSERTREPORTDETAILS, insertreportdetail);
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

       #region details report
       public DataSet DetailsReport(string rptid, ref string errorCode, ref string errorDesc)
       {
           try
           {
               //Show hastable result from IPC
               Hashtable hasInput = new Hashtable();
               Hashtable hasOutput = new Hashtable();

               //add key into input
               hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMS00028");
               hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
               hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
               hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "details report");
               hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

               hasInput.Add(SmartPortal.Constant.IPC.RPTID, rptid);


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

       #region Update Report
       public DataSet UpdateReport(string rptid, string rptname, string serviceid, string rptfile, bool isshow, string usercreated, string datecreated, string pageparamid, string pageviewid, DataTable tblReportDetail, ref string errorCode, ref string errorDesc)
       {
           try
           {
               //Show hastable result from IPC
               Hashtable hasInput = new Hashtable();
               Hashtable hasOutput = new Hashtable();

               //add key into input
               hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMS00029");
               hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
               hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
               hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "sửa báo cáo");

               #region Insert bảng report
               object[] insertreport = new object[2];
               insertreport[0] = "SEMS_RTPREPORT_UPDATE";
               //tao bang chua thong tin process
               DataTable tblinsertreport = new DataTable();
               DataColumn colrptid = new DataColumn("colrptid");
               DataColumn colrptname = new DataColumn("colrptname");
               DataColumn colserviceid = new DataColumn("colserviceid");
               DataColumn colrptfile = new DataColumn("colrptfile");
               DataColumn colisshow = new DataColumn("colisshow");
               DataColumn colusercreated = new DataColumn("colusercreated");
               DataColumn coldatecreated = new DataColumn("coldatecreated");
               DataColumn colpageparamid = new DataColumn("colpageparamid");
               DataColumn colpageviewid = new DataColumn("colpageviewid");


               //add vào table product
               tblinsertreport.Columns.Add(colrptid);
               tblinsertreport.Columns.Add(colrptname);
               tblinsertreport.Columns.Add(colserviceid);
               tblinsertreport.Columns.Add(colrptfile);
               tblinsertreport.Columns.Add(colisshow);
               tblinsertreport.Columns.Add(colusercreated);
               tblinsertreport.Columns.Add(coldatecreated);
               tblinsertreport.Columns.Add(colpageparamid);
               tblinsertreport.Columns.Add(colpageviewid);


               //tao 1 dong du lieu
               DataRow row = tblinsertreport.NewRow();
               row["colrptid"] = rptid;
               row["colrptname"] = rptname;
               row["colserviceid"] = serviceid;
               row["colrptfile"] = rptfile;
               row["colisshow"] = isshow;
               row["colusercreated"] = usercreated;
               row["coldatecreated"] = datecreated;
               row["colpageparamid"] = pageparamid;
               row["colpageviewid"] = pageviewid;


               tblinsertreport.Rows.Add(row);

               //add vao phan tu thu 2 mang object
               insertreport[1] = tblinsertreport;

               hasInput.Add(SmartPortal.Constant.IPC.UPDATEREPORT, insertreport);
               #endregion


               #region Insert REPORT DETAILS
               object[] insertreportdetail = new object[2];
               insertreportdetail[0] = "SEMS_RPT_REPORTDETAIL_INSERT";

               //add vao phan tu thu 2 mang object
               insertreportdetail[1] = tblReportDetail;

               hasInput.Add(SmartPortal.Constant.IPC.INSERTREPORTDETAILS, insertreportdetail);
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
       public DataSet DeleteReport(string rptid, ref string errorCode, ref string errorDesc)
       {
           try
           {
               //Show hastable result from IPC
               Hashtable hasInput = new Hashtable();
               Hashtable hasOutput = new Hashtable();

               //add key into input
               hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMS00030");
               hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
               hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
               hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "delete report");
               hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

               hasInput.Add(SmartPortal.Constant.IPC.RPTID, rptid);



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

       #region load data with store name
       public DataTable LoadDynamic(string storeName)
       {
           DataTable iRead;

           iRead = DataAccess.GetFromDataTable(storeName, null);

           return iRead;
       }
       #endregion

    }
}
