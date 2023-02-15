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

public partial class Widgets_SEMSReportManagement_NewControls_Widget : System.Web.UI.UserControl
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
                DataTable dttran = new DataTable();
                dttran = new SmartPortal.SEMS.Transactions().GetTranByParamAndService(Utility.KillSqlInjection(SmartPortal.Constant.IPC.ISREPORT), Utility.KillSqlInjection(SmartPortal.Constant.IPC.YES), Utility.KillSqlInjection(ddlSubSystem.SelectedValue.ToString()));
                if (dttran.Rows.Count < 0)
                {
                    throw new IPCException(IPCERRORDESC);
                }
                ddlParameterPage.DataSource = dttran;
                ddlParameterPage.DataTextField = "PAGENAME";
                ddlParameterPage.DataValueField = "PAGEID";
                ddlParameterPage.DataBind();
                //ddlParameterPage.Items.Insert(0, new ListItem(Resources.labels.khongtontai, ""));

                //load trang xem báo cáo
                DataTable dttranview = new DataTable();
                dttranview = new SmartPortal.SEMS.Transactions().GetTranByParamAndService(Utility.KillSqlInjection(SmartPortal.Constant.IPC.ISVIEWREPORT), Utility.KillSqlInjection(SmartPortal.Constant.IPC.YES), Utility.KillSqlInjection(ddlSubSystem.SelectedValue.ToString()));
                if (dttranview.Rows.Count < 0)
                {
                    throw new IPCException(IPCERRORDESC);
                }
                ddlViewReportPage.DataSource = dttranview;
                ddlViewReportPage.DataTextField = "PAGENAME";
                ddlViewReportPage.DataValueField = "PAGEID";
                ddlViewReportPage.DataBind();

                pnparamddl.Visible = false;
                hdgenID.Value = SmartPortal.Common.Utilities.Utility.GetID("RPT", "", "", 4);

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
                            // bind data to panel
                            ddlParameterPage.SelectedValue = ProDeTable.Rows[0]["PAGEPARAMID"].ToString();
                            ddlViewReportPage.SelectedValue = ProDeTable.Rows[0]["PAGEVIEWID"].ToString();
                            txtReportName.Text = ProDeTable.Rows[0]["RPTNAME"].ToString();
                            ddlSubSystem.SelectedValue = ProDeTable.Rows[0]["SERVICEID"].ToString();
                            lbtemp.Text = ProDeTable.Rows[0]["RPTFILE"].ToString();
                            if ((bool)(ProDeTable.Rows[0]["ISSHOW"]) == false)
                            {
                                cbIsDisPlay.Checked = false;

                            }
                            else
                            {
                                cbIsDisPlay.Checked = true;
                            }
                            //

                            DataTable tblTransDetailsEdit = new DataTable();
                            // tạo table tạm chứa TransDetails 
                            DataColumn RPTID = new DataColumn("RPTID");
                            DataColumn PARAMNAME = new DataColumn("PARAMNAME");
                            DataColumn CONTROLNAME = new DataColumn("CONTROLNAME");
                            DataColumn CONTROLTYPE = new DataColumn("CONTROLTYPE");
                            DataColumn WIDTH = new DataColumn("WIDTH");
                            DataColumn HEIGHT = new DataColumn("HEIGHT");
                            DataColumn DDLSTORE = new DataColumn("DDLSTORE");
                            DataColumn DDLTEXT = new DataColumn("DDLTEXT");
                            DataColumn DDLVALUE = new DataColumn("DDLVALUE");
                            DataColumn LANGID = new DataColumn("LANGID");
                            DataColumn ORDERBY = new DataColumn("ORDERBY");
                            DataColumn TAG = new DataColumn("TAG");
                            //----thêm cột id----
                            DataColumn FkID = new DataColumn("FkID");
                            //add col vào tblTransDetails
                            tblTransDetailsEdit.Columns.AddRange(new DataColumn[] { FkID, RPTID, PARAMNAME, CONTROLNAME, CONTROLTYPE, WIDTH, HEIGHT, DDLSTORE, DDLTEXT, DDLVALUE, LANGID, ORDERBY, TAG });

                            //Bind data vào bảng tạm TransDetails(EDIT)
                            foreach (DataRow ro in ProDeTable.Rows)
                            {
                                DataRow r = tblTransDetailsEdit.NewRow();
                                r["RPTID"] = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["rid"].ToString();
                                r["PARAMNAME"] = ro["PARAMNAME"].ToString();
                                r["CONTROLNAME"] = ro["CONTROLNAME"].ToString();
                                r["CONTROLTYPE"] = ro["CONTROLTYPE"].ToString();
                                r["WIDTH"] = ro["WIDTH"].ToString();
                                r["HEIGHT"] = ro["HEIGHT"].ToString();
                                r["DDLSTORE"] = ro["DDLSTORE"].ToString();
                                r["DDLTEXT"] = ro["DDLTEXT"].ToString();
                                r["DDLVALUE"] = ro["DDLVALUE"].ToString();
                                r["LANGID"] = ro["LANGID"].ToString();
                                r["ORDERBY"] = ro["ORDERBY"].ToString();
                                r["TAG"] = ro["TAG"].ToString();
                                r["FkID"] = SmartPortal.Common.Utilities.Utility.GetID("", "", "", 6);
                                tblTransDetailsEdit.Rows.Add(r);
                            }
                            tblTransDetailsEdit.DefaultView.Sort = "[" + tblTransDetailsEdit.Columns["ORDERBY"].ColumnName + "] ASC";
                            ViewState["FEEDETAILS"] = tblTransDetailsEdit;
                            gvAppTransDetailsList.DataSource = tblTransDetailsEdit;//ProDeTable;
                            gvAppTransDetailsList.DataBind();
                            //
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
                    pnparaminfo.Visible = false;
                    btback.Visible = true;
                    btsave.Visible = false;
                    gvAppTransDetailsList.Columns[13].Visible = false;
                    break;
                case "edit":
                    ddlParameterPage.Enabled = true;
                    ddlViewReportPage.Enabled = true;
                    txtReportName.Enabled = true;
                    ddlSubSystem.Enabled = true;
                    cbIsDisPlay.Enabled = true;
                    FileUploadRPT.Enabled = true;
                    pnparaminfo.Visible = true;
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
            string rtpID = hdgenID.Value.ToString();//SmartPortal.Common.Utilities.Utility.GetID("RPT", "", "", 4);
            if (FileUploadRPT.HasFile || lbtemp.Text.ToString() != "")
            {
                string randoom = SmartPortal.Common.Utilities.Utility.GetID("Report", "", "", 4);
                string fileName = randoom + FileUploadRPT.FileName;
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
                string rptname = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtReportName.Text.Trim());
                string serviceid = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlSubSystem.SelectedValue.Trim());
                string pageparamid = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlParameterPage.SelectedValue.Trim());
                string pageviewid = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlViewReportPage.SelectedValue.Trim());
                bool isshow = cbIsDisPlay.Checked;
                string usercreated = SmartPortal.Common.Utilities.Utility.KillSqlInjection(Session["userName"].ToString());
                string datecreated = SmartPortal.Common.Utilities.Utility.KillSqlInjection(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

                // tạo table  ReportDetails
                DataTable tblReportDetails = new DataTable();
                tblReportDetails = (DataTable)ViewState["FEEDETAILS"];
                //bỏ cột fkid trc khi insert
                tblReportDetails.Columns.Remove("FkID");


                switch (ACTION)
                {
                    case "add":

                        new SmartPortal.SEMS.Report().InsertReport(rtpID, rptname, serviceid, fileName, isshow, usercreated, datecreated, pageparamid, pageviewid, tblReportDetails, ref IPCERRORCODE, ref IPCERRORDESC);

                        if (IPCERRORCODE == "0")
                        {
                            lblError.Text = Resources.labels.thembaocaothanhcong;
                            pnparamddl.Visible = false;
                            pnparaminfo.Visible = false;
                            pnGV.Visible = false;
                            pnFee.Visible = false;
                            btsave.Visible = false;


                        }
                        else
                        {
                            throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.IPC);
                        }


                        break;
                    case "edit":
                        if (FileUploadRPT.HasFile)
                        {
                            new SmartPortal.SEMS.Report().UpdateReport(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["rid"].ToString(), rptname, serviceid, fileName, isshow, usercreated, datecreated, pageparamid, pageviewid, tblReportDetails, ref IPCERRORCODE, ref IPCERRORDESC);
                        }
                        else if (lbtemp.Text.ToString() != "")
                        {
                            new SmartPortal.SEMS.Report().UpdateReport(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["rid"].ToString(), rptname, serviceid, lbtemp.Text.ToString().Trim(), isshow, usercreated, datecreated, pageparamid, pageviewid, tblReportDetails, ref IPCERRORCODE, ref IPCERRORDESC);
                        }
                        if (IPCERRORCODE == "0")
                        {
                            lblError.Text = Resources.labels.suabaocaothanhcong;
                            pnparamddl.Visible = false;
                            pnparaminfo.Visible = false;
                            pnGV.Visible = false;
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
    protected void ddlobject_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlobject.SelectedValue.ToString() == SmartPortal.Constant.IPC.DROPDOWNLIST)
        {
            pnparamddl.Visible = true;
        }
        else
        {
            pnparamddl.Visible = false;
        }

    }

    //----------New-----------
    protected void gvAppTransDetailsList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            // CheckBox cbxSelect;
            Label lblreportdId, lblreportId, lblFkID;
            Label lblordno;
            Label lbldisplayname, lblparamname;
            Label lblobject;
            Label lblwidth, lblheight, lbllang, lbllangname, lbltag, lblddlstore, lblddlvalue, lblddltext;
            HyperLink hpDelete;

            DataRowView drv;

            //if (e.Row.RowType == DataControlRowType.Header)
            //{
            //    cbxSelect = new CheckBox();
            //    cbxSelect.ID = "cbxSelectAll";
            //    cbxSelect.Attributes.Add("onclick", "SelectCbx(this);");
            //    e.Row.Cells[0].Controls.Add(cbxSelect);
            //}

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView)e.Row.DataItem;


                // cbxSelect = (CheckBox)e.Row.FindControl("cbxSelect");
                e.Row.Attributes.Add("onmousemove", "this.className='hightlight';");
                e.Row.Attributes.Add("onmouseout", "this.className='nohightlight';");

                lblFkID = (Label)e.Row.FindControl("lblFkID");
                lblparamname = (Label)e.Row.FindControl("lblparamname");
                lblreportId = (Label)e.Row.FindControl("lblreportId");
                lblddltext = (Label)e.Row.FindControl("lblddltext");
                lblordno = (Label)e.Row.FindControl("lblordno");
                lbldisplayname = (Label)e.Row.FindControl("lbldisplayname");
                lblobject = (Label)e.Row.FindControl("lblobject");
                lblwidth = (Label)e.Row.FindControl("lblwidth");
                lblheight = (Label)e.Row.FindControl("lblheight");
                lbllang = (Label)e.Row.FindControl("lbllang");
                lbltag = (Label)e.Row.FindControl("lbltag");
                hpDelete = (HyperLink)e.Row.FindControl("hpDelete");
                lblddlstore = (Label)e.Row.FindControl("lblddlstore");
                lblddlvalue = (Label)e.Row.FindControl("lblddlvalue");
                lblddltext = (Label)e.Row.FindControl("lblddltext");

                lbllangname = (Label)e.Row.FindControl("lbllangname");

                //cbxSelect.Enabled = true;
                //cbxSelect.Attributes.Add("onclick", "HighLightCBX('" + e.Row.ClientID + "',this)");
                lblFkID.Text = drv["FkID"].ToString();
                lblreportId.Text = drv["rptid"].ToString();
                lblparamname.Text = drv["paramname"].ToString();
                lblordno.Text = drv["ORDERBY"].ToString();
                lbldisplayname.Text = drv["CONTROLNAME"].ToString();
                lblobject.Text = drv["CONTROLTYPE"].ToString();
                lblwidth.Text = drv["width"].ToString();
                lblheight.Text = drv["height"].ToString();
                lbllang.Text = drv["LANGID"].ToString();
                lbltag.Text = drv["TAG"].ToString();
                lblddlstore.Text = drv["ddlstore"].ToString();
                lblddlvalue.Text = drv["ddlvalue"].ToString();
                lblddltext.Text = drv["ddltext"].ToString();

                if (drv["LANGID"].ToString() == SmartPortal.Constant.IPC.VIVN)
                {
                    lbllangname.Text = Resources.labels.tiengviet;
                }

                if (drv["LANGID"].ToString() == SmartPortal.Constant.IPC.ENUS)
                {
                    lbllangname.Text = Resources.labels.english;
                }


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
    protected void gvAppTransDetailsList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvAppTransDetailsList.PageIndex = e.NewPageIndex;
            BindData();
        }
        catch
        {
        }
    }
    protected void gvAppTransDetailsList_Sorting(object sender, GridViewSortEventArgs e)
    {
        //isSort = true;

        string sortExpression = e.SortExpression;

        ViewState["SortExpression"] = sortExpression;
        //showImage = true;

        if (GridViewSortDirection == SortDirection.Ascending)
        {
            //isAscend = true;

            SortGridView(sortExpression, ASCENDING);
            GridViewSortDirection = SortDirection.Descending;

        }

        else
        {
            isAscend = false;
            SortGridView(sortExpression, DESCENDING);
            GridViewSortDirection = SortDirection.Ascending;

        }
    }

    private SortDirection GridViewSortDirection
    {
        get
        {

            if (ViewState["sortDirection"] == null)

                ViewState["sortDirection"] = SortDirection.Ascending;


            return (SortDirection)ViewState["sortDirection"];

        }

        set { ViewState["sortDirection"] = value; }

    }

    protected void SortGridView(string sortExpression, string direction)
    {
        DataTable dataTable;

        if (ViewState["s"] != null)
        {
            dataTable = null;
        }

        else
        {
            if (ViewState["search"] != null)
            {
                dataTable = (DataTable)ViewState["FEEDETAILS"];
            }
            else
            {
                dataTable = (DataTable)ViewState["FEEDETAILS"];

            }
        }


        if (dataTable != null)
        {
            DataView dataView = new DataView(dataTable);
            dataView.Sort = sortExpression + direction;

            gvAppTransDetailsList.DataSource = dataView;
            gvAppTransDetailsList.DataBind();

        }

    }

    protected void btnAdd_Details_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable tblTransDetails = new DataTable();
            DataTable tblTempt = new DataTable();

            if (ViewState["FEEDETAILS"] == null)
            {
                // tạo table tạm chứa TransDetails 
                DataColumn RPTID = new DataColumn("RPTID");
                DataColumn PARAMNAME = new DataColumn("PARAMNAME");
                DataColumn CONTROLNAME = new DataColumn("CONTROLNAME");
                DataColumn CONTROLTYPE = new DataColumn("CONTROLTYPE");
                DataColumn WIDTH = new DataColumn("WIDTH");
                DataColumn HEIGHT = new DataColumn("HEIGHT");
                DataColumn DDLSTORE = new DataColumn("DDLSTORE");
                DataColumn DDLTEXT = new DataColumn("DDLTEXT");
                DataColumn DDLVALUE = new DataColumn("DDLVALUE");
                DataColumn LANGID = new DataColumn("LANGID");
                DataColumn ORDERBY = new DataColumn("ORDERBY");
                DataColumn TAG = new DataColumn("TAG");
                //----thêm cột id----
                DataColumn FkID = new DataColumn("FkID");
                //add col vào tblTransDetails
                tblTransDetails.Columns.AddRange(new DataColumn[] { FkID, RPTID, PARAMNAME, CONTROLNAME, CONTROLTYPE, WIDTH, HEIGHT, DDLSTORE, DDLTEXT, DDLVALUE, LANGID, ORDERBY, TAG });

                //Bind data vào bảng tạm TransDetails
                DataRow r = tblTransDetails.NewRow();
                if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["a"].ToString() == "edit")
                {
                    r["RPTID"] = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["rid"].ToString();
                }
                else
                {
                    r["RPTID"] = hdgenID.Value.ToString();
                }
                r["FkID"] = SmartPortal.Common.Utilities.Utility.GetID("", "", "", 6);
                r["PARAMNAME"] = txtparamname.Text.Trim();
                r["CONTROLNAME"] = txtdisplayname.Text.Trim();
                r["CONTROLTYPE"] = ddlobject.SelectedValue.Trim();
                r["WIDTH"] = txtwidth.Text.Trim();
                r["HEIGHT"] = txtheight.Text.Trim();
                r["DDLSTORE"] = txtSPName.Text.Trim();
                r["DDLTEXT"] = txtddltext.Text.Trim();
                r["DDLVALUE"] = txtddlvalue.Text.Trim();
                r["LANGID"] = ddllang.SelectedValue.ToString();
                r["ORDERBY"] = ddlorder.SelectedValue.ToString();
                r["TAG"] = txttag.Text.Trim();


                tblTransDetails.Rows.Add(r);

                tblTransDetails.DefaultView.Sort = "[" + tblTransDetails.Columns["ORDERBY"].ColumnName + "] ASC";
                ViewState["FEEDETAILS"] = tblTransDetails;
            }
            else
            {
                // if ((long.Parse(tblTransDetails.Rows[tblTransDetails.Rows.Count - 1]["TOLIMIT"].ToString().Replace(",", "").Replace(".", "")) <= long.Parse(txtFrom.Text.Trim().Replace(",", "").Replace(".", ""))) || (long.Parse(tblTransDetails.Rows[tblTransDetails.Rows.Count - 1]["FROMLIMIT"].ToString().Replace(",", "").Replace(".", "")) >= long.Parse(txtTo.Text.Trim().Replace(",", "").Replace(".", ""))))
                // if (long.Parse(tblTransDetails.Rows[tblTransDetails.Rows.Count - 2]["TOLIMIT"].ToString().Replace(",", "").Replace(".", "")) >= long.Parse(txtFrom.Text.Trim().Replace(",", "").Replace(".", "")))
                tblTransDetails = (DataTable)ViewState["FEEDETAILS"];
                tblTempt = (DataTable)ViewState["FEEDETAILS"];
                DataRow r = tblTempt.NewRow();
                string a = "";
                foreach (DataRow row in tblTransDetails.Rows)
                {

                    // if ((SmartPortal.Common.Utilities.Utility.isDouble(row["TOLIMIT"].ToString(), true) <= SmartPortal.Common.Utilities.Utility.isDouble(txtFrom.Text.Trim(), true)) || (SmartPortal.Common.Utilities.Utility.isDouble(row["FROMLIMIT"].ToString(), true) >= SmartPortal.Common.Utilities.Utility.isDouble(txtTo.Text.Trim(), true)))
                    //{
                    if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["a"].ToString() == "edit")
                    {
                        r["RPTID"] = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["rid"].ToString();
                    }
                    else
                    {
                        r["RPTID"] = hdgenID.Value.ToString();
                    }
                    //Fuid += 1;
                    r["FkID"] = SmartPortal.Common.Utilities.Utility.GetID("", "", "", 6);
                    r["PARAMNAME"] = txtparamname.Text.Trim();
                    r["CONTROLNAME"] = txtdisplayname.Text.Trim();
                    r["CONTROLTYPE"] = ddlobject.SelectedValue.Trim();
                    r["WIDTH"] = txtwidth.Text.Trim();
                    r["HEIGHT"] = txtheight.Text.Trim();
                    r["DDLSTORE"] = txtSPName.Text.Trim();
                    r["DDLTEXT"] = txtddltext.Text.Trim();
                    r["DDLVALUE"] = txtddlvalue.Text.Trim();
                    r["LANGID"] = ddllang.SelectedValue.ToString();
                    r["ORDERBY"] = ddlorder.SelectedValue.ToString();
                    r["TAG"] = txttag.Text.Trim();


                    lblWarning.Text = "";

                    // }
                    // else
                    //{
                    //  a = "1";
                    // lblWarning.Text = Resources.labels.hanmuctinhphikhonghople;
                    // break;
                    //}
                }
                //if (a != "1")
                //{
                tblTempt.Rows.Add(r);
                // }
                tblTempt.DefaultView.Sort = "[" + tblTempt.Columns["ORDERBY"].ColumnName + "] ASC";
                ViewState["FEEDETAILS"] = tblTempt;
                tblTransDetails = (DataTable)ViewState["FEEDETAILS"];

            }

            gvAppTransDetailsList.DataSource = tblTransDetails;
            gvAppTransDetailsList.DataBind();




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
    protected void gvAppTransDetailsList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable tblTransDetailsDel = new DataTable();
        tblTransDetailsDel = (DataTable)ViewState["FEEDETAILS"];

        Label lblFkID = (Label)gvAppTransDetailsList.Rows[e.RowIndex].FindControl("lblFkID");
        DataRow[] dr = tblTransDetailsDel.Select("FkID='" + lblFkID.Text.Trim() + "'");

        tblTransDetailsDel.Rows.Remove(dr[0]);


        gvAppTransDetailsList.DataSource = tblTransDetailsDel;
        gvAppTransDetailsList.DataBind();

        if (tblTransDetailsDel.Rows.Count == 0)
        {
            ViewState["FEEDETAILS"] = null;
        }
        else
        {
            tblTransDetailsDel.DefaultView.Sort = "[" + tblTransDetailsDel.Columns["ORDERBY"].ColumnName + "] ASC";
            ViewState["FEEDETAILS"] = tblTransDetailsDel;
        }


    }

}
