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


public partial class Widgets_IBTKTHSearch_Widget : WidgetBase
{
    public static bool isAscend = false;
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    public static string ContractNo = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblAlert.Text = "";
            if (!IsPostBack)
            {
                BindData();
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.Message, this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }

    void BindData()
    {
       try
        {
            SmartPortal.SEMS.User objUser = new SmartPortal.SEMS.User();
            SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();
            DataSet dsUser = new DataSet();
            dsUser = objUser.GetFullUserByUID(Session["userID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
            DataSet dtProcess = new DataSet();
            if (IPCERRORCODE == "0" && dsUser.Tables.Count == 1)
            {
                if (dsUser.Tables[0].Rows.Count == 1)
                {
                    ContractNo = dsUser.Tables[0].Rows[0]["CONTRACTNO"].ToString();
                    dtProcess = objAcct.GetAcceptList(ContractNo, Utility.KillSqlInjection(txtSender.Text), Utility.KillSqlInjection(txtAccount.Text),"", ref IPCERRORCODE, ref IPCERRORCODE);
                    if (IPCERRORCODE == "0" && dtProcess.Tables.Count == 1)
                    {
                        gvProcessList.DataSource = dtProcess;
                        gvProcessList.DataBind();
                        if (dtProcess.Tables[0].Rows.Count == 0)
                        {
                            //btnDelete.Visible = false;
                            lblAlert.Text = "<div style='width:100%; padding-top:10px; padding-bottom:10px; text-align:center;'>" + Resources.labels.datanotfound + "</div>";

                        }
                        else
                        {
                            //btnDelete.Visible = true;
                            litPager.Text = "Đang hiển thị <b>" + (((gvProcessList.PageIndex) * gvProcessList.PageSize) + gvProcessList.Rows.Count).ToString() + "</b> của <b>" + gvProcessList.Rows.Count.ToString() + "</b> dòng";

                        }
                    }
                    if (IPCERRORCODE != "0")
                        throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.Quyen);
                }
            }
            else
            { 
                throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.Quyen); 
            }
            
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.Message, this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    protected void gvProcessList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
           CheckBox cbxSelect;
            HyperLink hpsenderid;
            HyperLink lblsender;
            Label lblaccount;
            Label lblDesc;
            HyperLink hpDelete;

            DataRowView drv;

            if (e.Row.RowType == DataControlRowType.Header)
            {
                cbxSelect = new CheckBox();
                cbxSelect.ID = "cbxSelectAll";
                cbxSelect.Attributes.Add("onclick", "SelectCbx(this);");
                e.Row.Cells[0].Controls.Add(cbxSelect);
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView)e.Row.DataItem;


               // cbxSelect = (CheckBox)e.Row.FindControl("cbxSelect");
                e.Row.Attributes.Add("onmousemove", "this.className='hightlight';");
                e.Row.Attributes.Add("onmouseout", "this.className='nohightlight';");


                hpsenderid = (HyperLink)e.Row.FindControl("hpsenderid");
                lblsender = (HyperLink)e.Row.FindControl("lblsender");
                lblaccount = (Label)e.Row.FindControl("lblaccount");
                lblDesc = (Label)e.Row.FindControl("lblDesc");
                hpDelete = (HyperLink)e.Row.FindControl("hpDelete");

                //cbxSelect.Enabled = true;
                //cbxSelect.Attributes.Add("onclick", "HighLightCBX('" + e.Row.ClientID + "',this)");


                hpsenderid.Text = drv["CONTRACTNO"].ToString() + "|"+drv["ACCTNO"].ToString();
                lblsender.Text = drv["SENDERNAME"].ToString();
                lblsender.NavigateUrl = "~/default.aspx?po=3&p=293&cno=" + drv["ACCTNO"].ToString();
                lblaccount.Text = drv["ACCTNO"].ToString();
                lblDesc.Text = drv["DESCRIPTION"].ToString();
                hpDelete.Text = "Xóa";
                hpDelete.NavigateUrl = "~/default.aspx?po=3&p=266&cn=" + drv["CONTRACTNO"].ToString() + "&acctno=" + drv["ACCTNO"].ToString();

            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.Message, this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    protected void gvProcessList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvProcessList.PageIndex = e.NewPageIndex;
            BindData();
        }
        catch
        {
        }
    }
    protected void gvProcessList_Sorting(object sender, GridViewSortEventArgs e)
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
            if (Session["search"] != null)
            {
                dataTable = (new SmartPortal.IB.Account().GetAcceptList(ContractNo, Utility.KillSqlInjection(txtSender.Text), Utility.KillSqlInjection(txtAccount.Text), "", ref IPCERRORCODE, ref IPCERRORCODE)).Tables[0];
            }
            else
            {
                dataTable = (new SmartPortal.IB.Account().GetAcceptList(ContractNo, Utility.KillSqlInjection(txtSender.Text), Utility.KillSqlInjection(txtAccount.Text),"", ref IPCERRORCODE, ref IPCERRORCODE)).Tables[0];
            }
        }


        if (dataTable != null)
        {
            DataView dataView = new DataView(dataTable);
            dataView.Sort = sortExpression + direction;

            gvProcessList.DataSource = dataView;
            gvProcessList.DataBind();
        }

    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/?po=3&p=119");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {

        try
        {
            Session["search"] = "true";
            BindData();
        }
        catch (Exception ex)
        {
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        CheckBox cbxDelete;
        HyperLink hpsenderid;


        string Str_ProTranCCYID = "";
        try
        {
            foreach (GridViewRow gvr in gvProcessList.Rows)
            {
                cbxDelete = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                if (cbxDelete.Checked == true)
                {
                    hpsenderid = (HyperLink)gvr.Cells[1].FindControl("hpsenderid");
                    Str_ProTranCCYID += hpsenderid.Text.Trim() + "#";
                }
            }
            Session["_AcceptList"] = Str_ProTranCCYID.Substring(0, Str_ProTranCCYID.Length - 1);
            // + "&returnUrl=" + SmartPortal.Common.Encrypt.EncryptData(SmartPortal.Common.Encrypt.DecryptURL(System.Web.HttpContext.Current.Request.Url.ToString().Substring(System.Web.HttpContext.Current.Request.Url.ToString().IndexOf(ConfigurationManager.AppSettings["routeurlslash"].ToString()) + ConfigurationManager.AppSettings["routeurlslash"].ToString().Length, System.Web.HttpContext.Current.Request.Url.ToString().Length - (System.Web.HttpContext.Current.Request.Url.ToString().IndexOf(ConfigurationManager.AppSettings["routeurlslash"].ToString()) + ConfigurationManager.AppSettings["routeurlslash"].ToString().Length)))));
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.Message, this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?po=3&p=266"));
    }
}
