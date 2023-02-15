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
using SmartPortal.BLL;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;
using SmartPortal.Model;

public partial class Widgets_SEMSTellerApproveTrans_Controls_Widget : WidgetBase
{
    public static bool isAscend = false;
    private string ACTION = string.Empty;
    private string IPCERRORCODE = string.Empty;
    private string IPCERRORDESC = string.Empty;
    public static string APPTRANID;
    public static string IDp;
    DataTable ProDeTable = new DataTable();
    public string _TITLE
    {
        get { return lblTitleProduct.Text; }
        set { lblTitleProduct.Text = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            txtFrom.Attributes.Add("onkeypress", "executeComma('" + txtFrom.ClientID + "')");
            txtTo.Attributes.Add("onkeypress", "executeComma('" + txtTo.ClientID + "')");
            txtFrom.Attributes.Add("onkeyup", "executeComma('" + txtFrom.ClientID + "')");
            txtTo.Attributes.Add("onkeyup", "executeComma('" + txtTo.ClientID + "')");
            ACTION = GetActionPage();
            if (!IsPostBack)
            {
                ViewState["APPTRANS"] = null;
                APPTRANID = SmartPortal.Common.Utilities.Utility.GetID(SmartPortal.Constant.IPC.PROCESSAPPROVEPREFIX, "", "", 15);
                //load các quyền
                ddlRole.DataSource = new SmartPortal.SEMS.Product().GetProductByCondition("", "", "", "", ref IPCERRORCODE, ref IPCERRORDESC);
                ddlRole.DataTextField = "PRODUCTNAME";
                ddlRole.DataValueField = "PRODUCTID";
                ddlRole.DataBind();

                //load các giao dịch
                ddlTrans.DataSource = new SmartPortal.SEMS.Product().GetTranNameByTrancode(ref IPCERRORCODE, ref IPCERRORDESC);
                ddlTrans.DataTextField = "PAGENAME";
                ddlTrans.DataValueField = "TRANCODE";
                ddlTrans.DataBind();
                //add item cho ddlCấpDuyệtCuối và ddlLevel
                DataTable tblAllLevel = new SmartPortal.SEMS.BankUser().LoadAllLevel();
                ddlLastLvApprove.DataSource = tblAllLevel;
                ddlLastLvApprove.DataTextField = "DESCRIPTION";
                ddlLastLvApprove.DataValueField = "USERLEVEL";
                ddlLastLvApprove.DataBind();

                ddlLevel.DataSource = tblAllLevel;
                ddlLevel.DataTextField = "DESCRIPTION";
                ddlLevel.DataValueField = "USERLEVEL";
                ddlLevel.DataBind();

                //ddlOrderApp.DataSource = tblAllLevel;
                //ddlOrderApp.DataTextField = "USERLEVEL";
                //ddlOrderApp.DataValueField = "USERLEVEL";
                //ddlOrderApp.DataBind();
                for (int i = 1; i < 11; i++)
                {
                    ListItem li = new ListItem(i.ToString(), i.ToString());
                    ddlOrderApp.Items.Add(li);
                }

                ddlCCYID.DataSource = new SmartPortal.SEMS.Product().LoaddAllCCYID(ref IPCERRORCODE, ref IPCERRORDESC);
                ddlCCYID.DataTextField = "CCYID";
                ddlCCYID.DataValueField = "CCYID";
                ddlCCYID.DataBind();

                BindData();
            }
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
                case IPC.ACTIONPAGE.ADD:
                    break;
                default:
                    btnClear.Enabled = false;
                    #region Lấy thông tin san pham
                    IDp = GetParamsPage(IPC.ID)[0].Trim();
                    DataSet ds =
                        new SmartPortal.SEMS.Transactions().GetDetailsProcessApprove(IDp, ref IPCERRORCODE,
                            ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        ProDeTable = ds.Tables[0];
                        if (ProDeTable.Rows.Count != 0)
                        {
                            //bind data to panel
                            ddlTrans.SelectedValue = ProDeTable.Rows[0]["TRANCODE"].ToString();
                            ddlRole.SelectedValue = ProDeTable.Rows[0]["PRODUCTID"].ToString();
                            ddlCCYID.SelectedValue = ProDeTable.Rows[0]["CCYID"].ToString();
                            //txtlimit.Text = FormatNumber(productLMTable.Rows[0]["TRANLIMIT"].ToString());//string.Format("{0:#,###.##}",Double.Parse(productLMTable.Rows[0]["TRANLIMIT"].ToString()));
                            txtFrom.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(ProDeTable.Rows[0]["FROMLIMIT"].ToString(), ProDeTable.Rows[0]["CCYID"].ToString());
                            txtTo.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(ProDeTable.Rows[0]["TOLIMIT"].ToString(), ProDeTable.Rows[0]["CCYID"].ToString());
                            ddlLastLvApprove.SelectedValue = ProDeTable.Rows[0]["LASTLEVAPP"].ToString();
                            //bind data to gvAppTransDetailsList :D
                            if (ProDeTable.Rows[0]["DETAILID"].ToString() != "")
                            {
                                pnGV.Visible = true;
                                gvAppTransDetailsList.DataSource = ProDeTable;
                                gvAppTransDetailsList.DataBind();

                                //Bind data vào bảng tạm TransDetails
                                DataTable tblTransDetails = new DataTable();

                                if (ViewState["APPTRANS"] == null)
                                {

                                    // tạo table tạm chứa TransDetails 
                                    DataColumn AppTranIDCol = new DataColumn("DETAILID");
                                    DataColumn LevelCol = new DataColumn("USERLEVEL");
                                    DataColumn LevelNameCol = new DataColumn("LEVELNAME");
                                    DataColumn OrderAppCol = new DataColumn("ORDAPP");
                                    DataColumn DescCol = new DataColumn("DESCRIPTION");

                                    //add col vào tblTransDetails
                                    tblTransDetails.Columns.AddRange(new DataColumn[] { AppTranIDCol, LevelCol, LevelNameCol, OrderAppCol, DescCol });


                                    foreach (DataRow nr in ProDeTable.Rows)
                                    {
                                        if (nr["USERLEVEL"].ToString() != null)
                                        {
                                            DataRow r = tblTransDetails.NewRow();
                                            if (ACTION == IPC.ACTIONPAGE.EDIT)
                                            {
                                                r["DETAILID"] = IDp;
                                            }
                                            else
                                            {
                                                r["DETAILID"] = APPTRANID;
                                            }

                                            r["USERLEVEL"] = nr["USERLEVEL"].ToString();
                                            r["LEVELNAME"] = nr["LEVELNAME"].ToString();
                                            r["ORDAPP"] = nr["ORDAPP"].ToString();

                                            r["DESCRIPTION"] = nr["DESCRIPTION"].ToString();
                                            tblTransDetails.Rows.Add(r);
                                        }
                                    }

                                    ViewState["APPTRANS"] = tblTransDetails;
                                }
                            }
                            if (ProDeTable.Rows[0]["PASSLEVEL"].ToString().Trim() == SmartPortal.Constant.IPC.YES)
                            {
                                cbPassLevel.Checked = true;
                            }
                            else { cbPassLevel.Checked = false; }
                        }
                        else
                        {
                            pnGV.Visible = false;
                        }
                    }
                    else
                    {
                        lblError.Text = IPCERRORDESC;
                    }
                    #endregion
                    break;
            }
            #region Enable/Disable theo Action
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.DETAILS:
                    pnAdd.Enabled = false;
                    pnProcessApprove.Visible = false;
                    btsave.Enabled = false;
                    gvAppTransDetailsList.Columns[5].Visible = false;
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    ddlRole.Enabled = false;
                    ddlTrans.Enabled = false;
                    ddlCCYID.Enabled = false;
                    if (ProDeTable.Rows[0]["DETAILID"].ToString() != "")
                    {
                        DataTable tblTransDetailsEdit = new DataTable();

                        // tạo table tạm chứa TransDetails (EDIT)
                        DataColumn AppTranIDCole = new DataColumn("DETAILID");
                        DataColumn LevelCole = new DataColumn("USERLEVEL");
                        DataColumn LevelNameCole = new DataColumn("LEVELNAME");
                        DataColumn OrderAppCole = new DataColumn("ORDAPP");
                        DataColumn DescCole = new DataColumn("DESCRIPTION");

                        //add col vào tblTransDetails(EDIT)
                        tblTransDetailsEdit.Columns.AddRange(new DataColumn[] { AppTranIDCole, LevelCole, LevelNameCole, OrderAppCole, DescCole });

                        //Bind data vào bảng tạm TransDetails(EDIT)
                        foreach (DataRow ro in ProDeTable.Rows)
                        {
                            DataRow r = tblTransDetailsEdit.NewRow();
                            r["DETAILID"] = IDp;
                            r["USERLEVEL"] = (string)ro["USERLEVEL"].ToString();
                            r["LEVELNAME"] = (string)ro["LEVELNAME"].ToString();
                            r["ORDAPP"] = (string)ro["ORDAPP"].ToString();
                            r["DESCRIPTION"] = (string)ro["DESCRIPTION"].ToString();
                            tblTransDetailsEdit.Rows.Add(r);
                        }
                        Session["APPTRANS"] = tblTransDetailsEdit;
                    }

                    break;
            }
            #endregion
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void gvAppTransDetailsList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            Label lblOder, lblLevel, lblLevelName, lblProcessID, lblDescription;
            DataRowView drv;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView)e.Row.DataItem;
                lblProcessID = (Label)e.Row.FindControl("lbProcessID");
                lblOder = (Label)e.Row.FindControl("lblOder");
                lblLevel = (Label)e.Row.FindControl("lblLevel");
                lblLevelName = (Label)e.Row.FindControl("lblLevelName");
                lblDescription = (Label)e.Row.FindControl("lblDescription");
                lblProcessID.Text = drv["DETAILID"].ToString();
                lblOder.Text = drv["ORDAPP"].ToString();
                lblLevel.Text = drv["USERLEVEL"].ToString();
                lblLevelName.Text = drv["LEVELNAME"].ToString();
                lblDescription.Text = drv["DESCRIPTION"].ToString();
            }
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
            string trancode = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlTrans.SelectedValue.Trim());
            string roleid = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlRole.SelectedValue.Trim());
            string ccyid = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlCCYID.SelectedValue.Trim());
            string fromlm = SmartPortal.Common.Utilities.Utility.KillSqlInjection(SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtFrom.Text.Trim(), ddlCCYID.SelectedValue.Trim()));
            string tolm = SmartPortal.Common.Utilities.Utility.KillSqlInjection(SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtTo.Text.Trim(), ddlCCYID.SelectedValue.Trim()));
            string status = "";
            string contractno = "";
            string usercreate = Session["userName"].ToString();
            string userapprove = "";
            string usermodify = Session["userName"].ToString();
            string lastmodify = DateTime.Now.ToString("dd/MM/yyyy");
            string passlevel = "";
            if (cbPassLevel.Checked == true)
            {
                passlevel = SmartPortal.Constant.IPC.YES;
            }
            else
            {
                passlevel = SmartPortal.Constant.IPC.NO;
            }
            DataTable ProcessApproveDetails = new DataTable();
            ProcessApproveDetails = (DataTable)ViewState["APPTRANS"];
            //DataColumn AppTranIDCole = new DataColumn("DETAILID");
            //DataColumn LevelCole = new DataColumn("USERLEVEL");
            //DataColumn LevelNameCole = new DataColumn("LEVELNAME");
            //DataColumn OrderAppCole = new DataColumn("ORDAPP");
            //DataColumn DescCole = new DataColumn("DESCRIPTION");

            ////add col vào tblTransDetails(EDIT)
            //ProcessApproveDetails.Columns.AddRange(new DataColumn[] { AppTranIDCole, LevelCole, LevelNameCole, OrderAppCole, DescCole });
            if (ViewState["APPTRANS"] != null && ProcessApproveDetails.Rows.Count != 0)
            {
                string lastlevapp = ProcessApproveDetails.Rows[ProcessApproveDetails.Rows.Count - 1]["USERLEVEL"].ToString();
                ProcessApproveDetails.Columns.RemoveAt(2);
                ViewState["APPTRANS"] = ProcessApproveDetails;
                switch (ACTION)
                {
                    case IPC.ACTIONPAGE.ADD:
                        if (!CheckPermitPageAction(IPC.ACTIONPAGE.ADD)) return;

                        new SmartPortal.SEMS.Transactions().InsertProcessApprove(APPTRANID, trancode, roleid, contractno, ccyid, fromlm, tolm, lastlevapp, passlevel, status, usercreate, userapprove, "", "", (DataTable)ViewState["APPTRANS"], ref IPCERRORCODE, ref IPCERRORDESC);

                        if (IPCERRORCODE == "0")
                        {
                            lblError.Text = Resources.labels.addapprovalprocessthanhcong;
                            pnAdd.Enabled = false;
                            pnProcessApprove.Enabled = false;
                            btsave.Enabled = false;
                            gvAppTransDetailsList.Columns[5].Visible = false;
                        }
                        else
                        {
                            ErrorCodeModel EM = new ErrorCodeModel();
                            EM = new ErrorBLL().Load(Utility.IsInt(IPC.ERRORCODE.INVALIDLIMIT), System.Globalization.CultureInfo.CurrentCulture.ToString());
                            lblError.Text = EM.ErrorDesc;
                            return;
                        }
                        break;
                    case IPC.ACTIONPAGE.EDIT:

                        if (!CheckPermitPageAction(IPC.ACTIONPAGE.EDIT)) return;
                        new SmartPortal.SEMS.Transactions().UpdateProcessApprove(IDp, trancode, roleid, contractno, ccyid, fromlm, tolm, lastlevapp, passlevel, status, "", userapprove, usermodify, lastmodify, (DataTable)ViewState["APPTRANS"], ref IPCERRORCODE, ref IPCERRORDESC);

                        if (IPCERRORCODE == "0")
                        {
                            lblError.Text = Resources.labels.editapprovalprocessthanhcong;
                            pnAdd.Enabled = false;
                            pnProcessApprove.Enabled = false;
                            btsave.Enabled = false;
                            gvAppTransDetailsList.Columns[5].Visible = false;
                        }
                        else
                        {
                            ErrorCodeModel EM = new ErrorCodeModel();
                            EM = new ErrorBLL().Load(Utility.IsInt(IPC.ERRORCODE.INVALIDLIMIT), System.Globalization.CultureInfo.CurrentCulture.ToString());
                            lblError.Text = EM.ErrorDesc;
                            return;
                        }
                        break;
                }
            }
            else
            {
                ErrorCodeModel EM = new ErrorCodeModel();
                EM = new ErrorBLL().Load(Utility.IsInt(IPC.ERRORCODE.MUSTSETAPPROVEPROCESS), System.Globalization.CultureInfo.CurrentCulture.ToString());
                lblError.Text = EM.ErrorDesc;
                return;
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
    protected void btnSaveDetails_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable tblTransDetails = new DataTable();

            if (ViewState["APPTRANS"] == null)
            {
                // tạo table tạm chứa TransDetails 
                DataColumn AppTranIDCol = new DataColumn("DETAILID");
                DataColumn LevelCol = new DataColumn("USERLEVEL");
                DataColumn LevelNameCol = new DataColumn("LEVELNAME");
                DataColumn OrderAppCol = new DataColumn("ORDAPP");
                DataColumn DescCol = new DataColumn("DESCRIPTION");
                //add col vào tblTransDetails
                tblTransDetails.Columns.AddRange(new DataColumn[] { AppTranIDCol, LevelCol, LevelNameCol, OrderAppCol, DescCol });
                //Bind data vào bảng tạm TransDetails
                DataRow r = tblTransDetails.NewRow();
                if (ACTION == IPC.ACTIONPAGE.EDIT)
                {
                    r["DETAILID"] = IDp;
                }
                else
                {
                    r["DETAILID"] = APPTRANID;
                }
                r["USERLEVEL"] = ddlLevel.SelectedValue;
                r["LEVELNAME"] = ddlLevel.SelectedItem.Text;
                r["ORDAPP"] = ddlOrderApp.SelectedValue;
                r["DESCRIPTION"] = txtDesc.Text.Trim();
                tblTransDetails.Rows.Add(r);
                ViewState["APPTRANS"] = tblTransDetails;
            }
            else
            {
                tblTransDetails = (DataTable)ViewState["APPTRANS"];
                if (tblTransDetails.Select("ORDAPP='" + ddlOrderApp.SelectedValue + "'").Length == 0)
                {
                    DataRow r = tblTransDetails.NewRow();
                    if (ACTION == IPC.ACTIONPAGE.EDIT)
                    {
                        r["DETAILID"] = IDp;
                    }
                    else
                    {
                        r["DETAILID"] = APPTRANID;
                    }
                    r["USERLEVEL"] = ddlLevel.SelectedValue;
                    r["LEVELNAME"] = ddlLevel.SelectedItem;
                    r["ORDAPP"] = ddlOrderApp.SelectedValue;
                    r["DESCRIPTION"] = txtDesc.Text.Trim();
                    tblTransDetails.Rows.Add(r);
                }
                ViewState["APPTRANS"] = tblTransDetails;
            }
            tblTransDetails.DefaultView.Sort = "[" + tblTransDetails.Columns["ORDAPP"].ColumnName + "] asc";
            gvAppTransDetailsList.DataSource = tblTransDetails;
            gvAppTransDetailsList.DataBind();
            if (tblTransDetails.Rows.Count > 0 && tblTransDetails != null)
            {
                pnGV.Visible = true;
            }
            else
            {
                pnGV.Visible = false;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void gvAppTransDetailsList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            DataTable tblTransDetailsDel = new DataTable();
            tblTransDetailsDel = (DataTable)ViewState["APPTRANS"];
            tblTransDetailsDel.Rows.RemoveAt(e.RowIndex);
            gvAppTransDetailsList.DataSource = tblTransDetailsDel;
            gvAppTransDetailsList.DataBind();
            ViewState["APPTRANS"] = tblTransDetailsDel;
            if (tblTransDetailsDel.Rows.Count > 0 && tblTransDetailsDel != null)
            {
                pnGV.Visible = true;
            }
            else
            {
                pnGV.Visible = false;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblError.Text = string.Empty;
        pnAdd.Enabled = true;
        pnProcessApprove.Enabled = true;
        btsave.Enabled = true;
        ViewState["APPTRANS"] = null;
        ddlTrans.SelectedIndex = 0;
        ddlRole.SelectedIndex = 0;
        ddlCCYID.SelectedIndex = 0;
        txtFrom.Text = string.Empty;
        txtTo.Text = string.Empty;
        ddlOrderApp.SelectedIndex = 0;
        ddlLevel.SelectedIndex = 0;
        txtDesc.Text = string.Empty;
        pnGV.Visible = false;
    }
}
