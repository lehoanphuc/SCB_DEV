using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using SmartPortal.Common;
using SmartPortal.Common.Utilities;
using SmartPortal.ExceptionCollection;

public partial class Widgets_SEMSReportManagement_Controls_Widget : System.Web.UI.UserControl
{
    public static bool isAscend = false;
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";
    string ACTION = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    public static string APPTRANID;
    DataTable ProDeTable = new DataTable();

    public string _IMAGE
    {
        get { return imgLoGo.ImageUrl; }
        set { imgLoGo.ImageUrl = value; }
    }

    public string _TITLE
    {
        get { return lblTitleProduct.Text; }
        set { lblTitleProduct.Text = value; }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ACTION = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["a"].ToString().Trim();
            if (!IsPostBack)
            {
                lblWarning.Text = "";
                //load trang tham số 
                ddlParameterPage.DataSource = new SmartPortal.IB.Schedule().LoadTransferType(Utility.KillSqlInjection(SmartPortal.Constant.IPC.ISREPORT), Utility.KillSqlInjection(SmartPortal.Constant.IPC.YES), ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE != "0")
                {
                    throw new IPCException(IPCERRORDESC);
                }
                ddlParameterPage.DataTextField = "PAGENAME";
                ddlParameterPage.DataValueField = "PAGEID";
                ddlParameterPage.DataBind();
                ddlParameterPage.Items.Insert(0, new ListItem(Resources.labels.khongtontai, ""));

                //load trang xem báo cáo
                ddlViewReportPage.DataSource = new SmartPortal.IB.Schedule().LoadTransferType(Utility.KillSqlInjection(SmartPortal.Constant.IPC.ISVIEWREPORT), Utility.KillSqlInjection(SmartPortal.Constant.IPC.YES), ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE != "0")
                {
                    throw new IPCException(IPCERRORDESC);
                }
                ddlViewReportPage.DataTextField = "PAGENAME";
                ddlViewReportPage.DataValueField = "PAGEID";
                ddlViewReportPage.DataBind();

                pnResult.Visible = false;

                BindData();

            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }

    }
    void BindData()
    {
        DataTable tblRoleDefault = new DataTable();
        try
        {

            switch (ACTION)
            {
                case "add":
                    pnFee.Visible = true;
                    break;
                default:
                    #region Lấy thông tin san pham
                    DataSet dsfee = new DataSet();

                    dsfee = new SmartPortal.SEMS.Report().DetailsReport(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["rid"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                    if (dsfee.Tables.Count != 0)
                    {
                        ProDeTable = dsfee.Tables[0];
                        if (ProDeTable.Rows.Count != 0)
                        {
                            //bind data to panel
                            ddlParameterPage.SelectedValue = ProDeTable.Rows[0]["PAGEPARAMID"].ToString();
                            ddlViewReportPage.SelectedValue = ProDeTable.Rows[0]["PAGEVIEWID"].ToString();
                            txtReportName.Text = ProDeTable.Rows[0]["RPTNAME"].ToString();
                            ddlSubSystem.SelectedValue = ProDeTable.Rows[0]["SERVICEID"].ToString();
                            lbtemp.Text = ProDeTable.Rows[0]["RPTFILE"].ToString();
                            string ParamDetails = "";
                            foreach(DataRow ro in ProDeTable.Rows)
                            {
                                ParamDetails += ro["paramName"].ToString()+"#";
                            }
                            txtparamreport.Text = ParamDetails.Substring(0, ParamDetails.Length - 1);

                            if ((bool)(ProDeTable.Rows[0]["ISSHOW"]) == false)
                            {
                               cbIsDisPlay.Checked = false;

                            }
                            else
                            {
                                cbIsDisPlay.Checked = true;
                            }
                        }
                        else
                        {
                            throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.IPC);
                        }
                    }
                    else
                    {
                        throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.IPC);
                    }
                    #endregion
                    break;
            }
            #region Enable/Disable theo Action
            switch (ACTION)
            {
                case "viewdetail":
                    ddlParameterPage.Enabled = false;
                    ddlViewReportPage.Enabled = false;
                    txtReportName.Enabled = false;
                    ddlSubSystem.Enabled = false;
                    cbIsDisPlay.Enabled = false;
                    FileUploadRPT.Enabled = false;
                    txtparamreport.Enabled = false;
                    btback.Visible = true;
                    btsave.Visible = false;
                    break;
                case "edit":
                    ddlParameterPage.Enabled = true;
                    ddlViewReportPage.Enabled = true;
                    txtReportName.Enabled = true;
                    ddlSubSystem.Enabled = true;
                    cbIsDisPlay.Enabled = true;
                    FileUploadRPT.Enabled = true;
                    txtparamreport.Enabled = true;
                    btback.Visible = true;
                    btsave.Visible = true;

                    break;
            }
            #endregion

        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {
            
            //upload file to host
            string rtpID = SmartPortal.Common.Utilities.Utility.GetID("RPT", "", "", 4);
            if (FileUploadRPT.HasFile || lbtemp.Text.ToString()!="")
            {
                string randoom = SmartPortal.Common.Utilities.Utility.GetID("Report", "", "", 4);
                string fileName = randoom+FileUploadRPT.FileName;
                string extension = System.IO.Path.GetExtension(fileName).ToLower();
                string filePath = Server.MapPath("~/rptFile/") + fileName;
                if (FileUploadRPT.HasFile)
                {
                    switch (extension)
                    {
                        case ".rpt":
                            break;

                        default:
                            throw new BusinessExeption("1", new Exception("Only Upload .rpt"));
                            break;
                    }

                    FileUploadRPT.SaveAs(filePath);
                }
                  // tạo table  ReportDetails
                DataTable tblReportDetails = new DataTable();
                DataColumn rptIDCol = new DataColumn("rptID");
                DataColumn paramNameCol = new DataColumn("paramName");
                //add col vào ReportDetails
                tblReportDetails.Columns.AddRange(new DataColumn[] { rptIDCol, paramNameCol });
                //Split # lấy tham số
                string pr = txtparamreport.Text.Trim();
                string[] pprs = pr.Split('#');


                
                string rptname = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtReportName.Text.Trim());
                string serviceid = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlSubSystem.SelectedValue.Trim());
                string pageparamid = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlParameterPage.SelectedValue.Trim());
                string pageviewid = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlViewReportPage.SelectedValue.Trim());
                bool isshow = cbIsDisPlay.Checked;
                string usercreated = SmartPortal.Common.Utilities.Utility.KillSqlInjection(Session["userName"].ToString());
                string datecreated = SmartPortal.Common.Utilities.Utility.KillSqlInjection(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

                    switch (ACTION)
                    {
                        case "add":
                            foreach (string ppr in pprs)
                            {
                                //add row tblScheDetail
                                DataRow r = tblReportDetails.NewRow();
                                r["rptID"] = rtpID;
                                r["paramName"] = ppr;
                                tblReportDetails.Rows.Add(r);
                            }
                            if (txtparamreport.Text.Trim()=="")
                            {
                                tblReportDetails.Clear();
                            }
                            new SmartPortal.SEMS.Report().InsertReport(rtpID, rptname, serviceid,fileName,isshow,usercreated,datecreated, pageparamid, pageviewid, tblReportDetails, ref IPCERRORCODE, ref IPCERRORDESC);

                                if (IPCERRORCODE == "0")
                                {
                                    lblError.Text = Resources.labels.thembaocaothanhcong;
                                    pnResult.Visible = true;
                                    pnFee.Visible = false;
                                    btsave.Visible = false;
   

                                }
                                else
                                {
                                    throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.IPC);
                                }


                            break;
                        case "edit":
                            foreach (string ppr in pprs)
                            {
                                //add row tblScheDetail
                                DataRow r = tblReportDetails.NewRow();
                                r["rptID"] = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["rid"].ToString();
                                r["paramName"] = ppr;
                                tblReportDetails.Rows.Add(r);
                            }
                            if (txtparamreport.Text.Trim() == "")
                            {
                                tblReportDetails.Clear();
                            }
                            if (FileUploadRPT.HasFile)
                            {
                                new SmartPortal.SEMS.Report().UpdateReport(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["rid"].ToString(), rptname, serviceid, fileName, isshow, usercreated, datecreated, pageparamid, pageviewid, tblReportDetails, ref IPCERRORCODE, ref IPCERRORDESC);
                            }
                            else if (lbtemp.Text.ToString()!="")
                            {
                                new SmartPortal.SEMS.Report().UpdateReport(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["rid"].ToString(), rptname, serviceid, lbtemp.Text.ToString().Trim(), isshow, usercreated, datecreated, pageparamid, pageviewid, tblReportDetails, ref IPCERRORCODE, ref IPCERRORDESC);
                            }
                                if (IPCERRORCODE == "0")
                                {
                                    lblError.Text = Resources.labels.suabaocaothanhcong;
                                    pnResult.Visible = true;
                                    pnFee.Visible = false;
                                    btsave.Visible = false;

                                }
                                else
                                {
                                    throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.IPC);
                                }
                            


                            break;
                    }

                }
                else
                {
                    throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.SELECTFILEUPLOAD);
                  
                }
          
        }
        catch (BusinessExeption bex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["imageuploadec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, bex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["imageuploadec"], Request.Url.Query);
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
        //Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=322"));
    }

    protected void btback_Click(object sender, EventArgs e)
    {
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=370"));
    }
}
