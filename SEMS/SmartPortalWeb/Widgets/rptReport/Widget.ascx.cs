using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SmartPortal.DAL;
using SmartPortal.SEMS;

public partial class Widgets_rptReport_Widget : WidgetBase
{
    private ReportDocument cryRpt = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // DataBind();
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        //string rpt = GetParamsPage("rpt")[0];
        //if (!ReportRightBLL.CheckPermissionReport(Session["userName"].ToString(), rpt))
        //    Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["accessdenied"]);
        DataBind();
    }

    public void DataBind()
    {
        try
        {
            //lay thong tin report
            string rpt = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["rpt"];
            string param = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["param"];
            DataTable tblReport = new SmartPortal.SEMS.Report().LoadReport(rpt);

            if (tblReport.Rows.Count != 0)
            {
                lblReportName.Text = tblReport.Rows[0]["rptName"].ToString().ToUpper();

                if (param != null)
                {
                    hpParam.Text = Resources.labels.thamsobaocao;
                    hpParam.NavigateUrl = SmartPortal.Common.Encrypt.DecryptData(SmartPortal.Common.Encrypt.GetURLParam(HttpContext.Current.Request.RawUrl)["param"]);
                }

                cryRpt = new ReportDocument();
                TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                ConnectionInfo crConnectionInfo = new ConnectionInfo();
                Tables CrTables;

                crSEMS.AllowedExportFormats = (int)(ViewerExportFormats.WordFormat | ViewerExportFormats.PdfFormat | ViewerExportFormats.ExcelFormat | ViewerExportFormats.ExcelRecordFormat);

                cryRpt.Load(Server.MapPath("~/rptFile/" + tblReport.Rows[0]["rptFile"].ToString().Trim()));


                //VuTT 20181129, if datasource is dataset (multi table), set this flag to true to use Oledb
                bool useOledb = false;
                if (useOledb)
                {
                    crConnectionInfo.ServerName = ConfigurationManager.AppSettings["rptServer"];
                    crConnectionInfo.DatabaseName = ConfigurationManager.AppSettings["rptDatabase"];
                    crConnectionInfo.UserID = DataAccess.DecryptData(ConfigurationManager.AppSettings["rptUser"].ToString());
                    crConnectionInfo.Password = DataAccess.DecryptData(ConfigurationManager.AppSettings["rptPass"].ToString());

                    CrTables = cryRpt.Database.Tables;
                    foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                    {
                        crtableLogoninfo = CrTable.LogOnInfo;
                        crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                        CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    }
                    //gan tham so cho report
                    //ReportParameter();
                    DataTable tblParam = new SmartPortal.SEMS.Report().LoadParam(rpt, System.Globalization.CultureInfo.CurrentCulture.ToString());

                    foreach (DataRow row in tblParam.Rows)
                    {
                        SetParameter(row["paramName"].ToString().Trim(), Session[row["paramName"].ToString().Trim()].ToString(), cryRpt);
                    }
                    //cryRpt.SetDataSource
                    //crSEMS.DisplayGroupTree = false;
                    //crSEMS.ToolPanelView = CrystalDecisions.Web.ToolPanelViewType.None;
                    crSEMS.ReportSource = cryRpt;
                    //crSEMS.RefreshReport();
                }
                else
                {
                    //VuTT 20181129 try to remove oledb
                    string spname = cryRpt.Database.Tables[0].Location.Split(';')[0];
                    List<object> list = GetParamList(cryRpt);
                    DataTable dt = new DataTable();
                    if (ConfigurationManager.AppSettings["ReportConnection"] != null)
                    {
                        dt = DataAccess.FillDataTable(DataAccess.DecryptData(ConfigurationManager.AppSettings["ReportConnection"]), spname, list.ToArray());
                    }
                    else
                    {
                        dt = DataAccess.FillDataTable(spname, list.ToArray());
                    }
                    DataTable tblParam = new SmartPortal.SEMS.Report().LoadParam(rpt, System.Globalization.CultureInfo.CurrentCulture.ToString());
                    try
                    {
                        if (rpt == "RPT000111" && dt.Rows.Count > 0)
                        {
                            string appdomain = AppDomain.CurrentDomain.BaseDirectory.ToString();
                            foreach (DataRow row in dt.Rows)
                            {
                                if (row["FIELDNAME"].Equals("PAYSLIP") || row["FIELDNAME"].Equals("SALARY") || row["FIELDNAME"].Equals("HOUSEHOLDREGISTRATION")
                                    || row["FIELDNAME"].Equals("WARDRECOMMENDATION") || row["FIELDNAME"].Equals("LIVESELFIE"))
                                {
                                    row["FIELDVALUE"] = HttpContext.Current.Request.Url.Scheme  + "://" + HttpContext.Current.Request.Url.Authority + "/" + SmartPortal.Common.Encrypt.EncryptURL("/Default.aspx?p=REQUESTFORLOANVIEW" + "&rqid=" + row["IPCTRANSID"].ToString() + "&type=" + row["FIELDNAME"].ToString());
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
                    }
                    foreach (DataRow row in tblParam.Rows)
                    {
                        SetParameter(row["paramName"].ToString().Trim(), Session[row["paramName"].ToString().Trim()].ToString(), cryRpt);
                    }
                    cryRpt.SetDataSource(dt);

                    if (cryRpt.Subreports.Count > 0)
                    {
                        foreach (ReportDocument subRpt in cryRpt.Subreports)
                        {
                            spname = string.Empty;
                            spname = subRpt.Database.Tables[0].Location.Split(';')[0];
                            list = new List<object>();
                            list = GetParamList(subRpt);
                            dt = DataAccess.FillDataTable(spname, list.ToArray());
                            subRpt.SetDataSource(dt);
                        }
                    }
                    crSEMS.ReportSource = cryRpt;
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.WriteLogFile("", "", "", ex.InnerException.ToString());
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }

    public List<object> GetParamList(ReportDocument cryRpt)
    {
        List<object> list = new List<object>();

        object[] arrParam = new object[cryRpt.DataDefinition.ParameterFields.Count];
        string rptName = cryRpt.DataDefinition.ParameterFields[0].ReportName;
        for (int i = 0; i < cryRpt.DataDefinition.ParameterFields.Count; i++)
        {
            string name = cryRpt.DataDefinition.ParameterFields[i].Name.Replace("@", "").Trim();
            if (rptName.Equals(cryRpt.DataDefinition.ParameterFields[i].ReportName))
            {
                arrParam[i] = Session[name].ToString();
                list.Add(arrParam[i]);
            }
        }

        return list;
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        if (cryRpt != null)
        {
            cryRpt.Close();
            cryRpt.Dispose();
        }
    }

    public void SetParameter(string name, object value, ReportDocument rpt)
    {
        //Access the specified parameter from the parameters collection
        CrystalDecisions.CrystalReports.Engine.ParameterFieldDefinition parameter = rpt.DataDefinition.ParameterFields["@" + name];

        // Now get the current value for the parameter and clear them
        CrystalDecisions.Shared.ParameterValues currentValues = parameter.CurrentValues;
        currentValues.Clear();

        // Create a value object for Crystal reports and assign the specified value.
        CrystalDecisions.Shared.ParameterDiscreteValue newValue = new CrystalDecisions.Shared.ParameterDiscreteValue();
        newValue.Value = value;

        // Now add the new value to the values collection and apply the 
        // collection to the report.
        currentValues.Add(newValue);
        parameter.ApplyCurrentValues(currentValues);
    }
}
