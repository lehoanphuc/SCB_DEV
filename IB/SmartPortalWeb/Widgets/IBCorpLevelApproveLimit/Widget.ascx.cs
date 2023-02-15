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

using SmartPortal.Common.Utilities;
using SmartPortal.ExceptionCollection;

public partial class Widgets_IBCorpUserApproveLimit_Widget : WidgetBase
{
    public static bool isAscend = false;
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    string contractno = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            litPager.Text = "";
            ltrError.Text = "";
            lblError.Text = "";

            //get Contractno
            DataSet ContrNoDS = new SmartPortal.IB.Transactions().GetUserOfContractNoByUID(Utility.KillSqlInjection(Session["userID"].ToString()), "", ref IPCERRORCODE, ref IPCERRORDESC);

            if (IPCERRORCODE != "0")
            {
                throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
            }
            DataTable ContrNoDT = new DataTable();
            ContrNoDT = ContrNoDS.Tables[0];
            if (ContrNoDT.Rows.Count != 0)
            {
                contractno = ContrNoDT.Rows[0]["CONTRACTNO"].ToString();
            }

            if (!IsPostBack)
            {
                //load user
                //Session["userID"] = "U44864000";
                //DataSet ds = new SmartPortal.IB.Transactions().GetUserOfContractNoByUID(Utility.KillSqlInjection(Session["userID"].ToString()),"Y", ref IPCERRORCODE, ref IPCERRORDESC);

                
                //add item cho ddlCấpDuyệtCuối và ddlLevel
                DataSet tblAllLevel = new SmartPortal.IB.CorpUser().LoadCorpUserlevelByContractNo(contractno, ref IPCERRORCODE, ref IPCERRORDESC);
               
                if (IPCERRORCODE != "0")
                {
                    goto ERROR;
                }

                //DataTable deptTable = new DataTable();
                //deptTable = ds.Tables[0];

                lstDept.DataSource = tblAllLevel;
                lstDept.DataTextField = "DESCRIPTION";
                lstDept.DataValueField = "USERLEVEL";
                lstDept.DataBind();

                if (lstDept.Items.Count != 0)
                {
                    lstDept.Items[0].Selected = true;
                }
            }
            BindData();
            

            goto EXIT;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_IBCorpUserApproveLimit_Widget", "Page_Load", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }

    ERROR:
        SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_IBCorpUserApproveLimit_Widget", "Page_Load", IPCERRORDESC, Request.Url.Query);
        SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
    EXIT: ;

    }
    void BindData()
    {
        try
        {
            
            DataSet dtLimitTeller = new DataSet();
            DataTable dtLevel = new DataTable();
            dtLevel = new SmartPortal.SEMS.Transactions().GetAllLimitLevelByContract(contractno, Utility.KillSqlInjection(lstDept.SelectedValue), "", "");


            if (IPCERRORCODE == "0")
            {
                gvUser.DataSource = dtLevel;
                gvUser.DataBind();
            }
            else
            {
                throw new IPCException(IPCERRORDESC);
            }

            if (dtLevel.Rows.Count != 0)
            {
                ltrError.Text = "";
                litPager.Text = Resources.labels.danghienthi+" <b>" + (((gvUser.PageIndex) * gvUser.PageSize) + gvUser.Rows.Count).ToString() + "</b> "+Resources.labels.cua+" <b>" + dtLevel.Rows.Count.ToString() + "</b> "+Resources.labels.dong;
            }
            else
            {
                ltrError.Text = "<table rules='all' cellspacing='0' cellpadding='3' border='1' style='background-color: White; border:1px solid rgb(204, 204, 204); width: 100%; border-collapse: collapse;' id='ctl00_ctl11_gvUser'><tbody><tr style='color: White; background-color: rgb(52, 154, 192); font-weight: bold;'><th scope='col' class='gvHeader'><input type='checkbox' onclick='SelectCbx(this);' name='ctl00$ctl11$gvUser$ctl01$cbxSelectAll' id='ctl00_ctl11_gvUser_ctl01_cbxSelectAll'></th><th scope='col' class='gvHeader'>"+Resources.labels.giaodich+"</th><th scope='col' class='gvHeader'>"+Resources.labels.hanmuc+"</th><th scope='col' class='gvHeader'>"+Resources.labels.tiente+"</th><th scope='col' class='gvHeader'>"+Resources.labels.sua+"</th><th scope='col' class='gvHeader'>"+Resources.labels.xoa+"</th></tr></tbody><tr><th colspan='8'><center><span style='font-weight:bold;color:red;'>"+Resources.labels.khongtimthaydulieu+"</span></center></th></tr></table>";
                litPager.Text = "";
            }


            if (lstDept.Items.Count == 0)
            {
                lbUserInsert.Visible = false;
                lbDeleteLimit.Visible = false;
            }
            else
            {
                lbUserInsert.Visible = true;

                if (gvUser.Rows.Count == 0)
                {
                    lbDeleteLimit.Visible = false;
                }
                else
                {
                    lbDeleteLimit.Visible = true;
                }
            }

        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_IBCorpUserApproveLimit_Widget", "BindData", IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_IBCorpUserApproveLimit_Widget", "BindData", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void gvUser_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            Label lblUID;
            Label lblTRANCODE;
            Label lblTrans;
            Label lblLimit;
            Label lblCCYID;
            HyperLink hpEdit;
            HyperLink hpDelete;

            DataRowView drv;

            if (e.Row.RowType == DataControlRowType.Header)
            {
                cbxSelect = new CheckBox();
                cbxSelect.ID = "cbxSelectAll";
                cbxSelect.Attributes.Add("onclick", "SelectCbx(this);");
                e.Row.Cells[0].Controls.Add(cbxSelect);
                e.Row.TableSection = TableRowSection.TableHeader;
                e.Row.Cells[4].Attributes.Add("data-breakpoints", "xs");
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView)e.Row.DataItem;
                cbxSelect = (CheckBox)e.Row.FindControl("cbxSelect");
                e.Row.Attributes.Add("onmousemove", "this.className='hightlight';");
                e.Row.Attributes.Add("onmouseout", "this.className='nohightlight';");

                lblUID = (Label)e.Row.FindControl("lblUID");
                lblTRANCODE = (Label)e.Row.FindControl("lblTRANCODE");
                lblTrans = (Label)e.Row.FindControl("lblTrans");
                lblLimit = (Label)e.Row.FindControl("lblLimit");
                lblCCYID = (Label)e.Row.FindControl("lblCCYID");

                hpEdit = (HyperLink)e.Row.FindControl("hpEdit");
                hpDelete = (HyperLink)e.Row.FindControl("hpDelete");

                //cbxSelect.Enabled = true;
                //cbxSelect.Attributes.Add("onclick", "HighLightCBX('" + e.Row.ClientID + "',this)");

                lblUID.Text = drv["ID"].ToString();
                lblTRANCODE.Text = drv["IPCTRANCODE"].ToString();
                lblTrans.Text = drv["PAGENAME"].ToString();
                lblLimit.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(drv["LIMITAPPROVE"].ToString(), drv["CCYID"].ToString().Trim());
                lblCCYID.Text = drv["CCYID"].ToString();


                hpEdit.Text = Resources.labels.sua;
                hpEdit.NavigateUrl = "~/default.aspx?po=3&p=353&a=edit&id=" + drv["ID"].ToString()+"&cn="+contractno;
                hpDelete.Text = Resources.labels.huy;
                hpDelete.NavigateUrl = "~/default.aspx?po=3&p=354&id=" + drv["ID"].ToString();

            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSSetLimitTeller_Widget", "gvUser_RowDataBound", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    protected void gvUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvUser.PageIndex = e.NewPageIndex;
            BindData();
        }
        catch
        {
        }
    }
    protected void gvUser_Sorting(object sender, GridViewSortEventArgs e)
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
                dataTable = (new SmartPortal.SEMS.Transactions().GetAllLimitTellerByUID(Utility.KillSqlInjection(lstDept.SelectedValue), "", "", ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
            }
            else
            {
                dataTable = (new SmartPortal.SEMS.Transactions().GetAllLimitTellerByUID(Utility.KillSqlInjection(lstDept.SelectedValue), "", "", ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
            }
        }


        if (dataTable != null)
        {
            DataView dataView = new DataView(dataTable);
            dataView.Sort = sortExpression + direction;

            gvUser.DataSource = dataView;
            gvUser.DataBind();
        }

    }

    protected void lstDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            //litPager.Text = "";
            ////lay thong tin user theo phong ban
            //DataSet dsUser = new SmartPortal.SEMS.Transactions().GetAllLimitTellerByUID(Utility.KillSqlInjection(lstDept.SelectedValue), "", "", ref IPCERRORCODE, ref IPCERRORDESC);
            //if (IPCERRORCODE != "0")
            //{
            //    goto ERROR;
            //}

            //DataTable userTable = new DataTable();
            //userTable = dsUser.Tables[0];

            //gvUser.DataSource = userTable;
            //gvUser.DataBind();
            //if (userTable.Rows.Count != 0)
            //{
            //    litPager.Text = "Đang hiển thị <b>" + (((gvUser.PageIndex) * gvUser.PageSize) + gvUser.Rows.Count).ToString() + "</b> của <b>" + dsUser.Tables[0].Rows.Count.ToString() + "</b> dòng";
            //}
            //goto EXIT;
            BindData();

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSSetLimitTeller_Widget", "lstDept_SelectedIndexChanged", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }

  
    }
    protected void lbUserInsert_Click(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?po=3&p=351&a=add&lv=" + lstDept.SelectedValue+"&cn="+contractno));
    }
    protected void lbUserDelete_Click(object sender, EventArgs e)
    {
        CheckBox cbxDelete;
        Label hpProID;
        Label lbTrancode;
        Label lbCCYID;


        string Str_ProTranCCYID = "";
        try
        {
            foreach (GridViewRow gvr in gvUser.Rows)
            {
                cbxDelete = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                if (cbxDelete.Checked == true)
                {
                    hpProID = (Label)gvr.Cells[1].FindControl("lblUID");
                    Str_ProTranCCYID += hpProID.Text.Trim() + "|";
                    lbTrancode = (Label)gvr.Cells[1].FindControl("lblTRANCODE");
                    Str_ProTranCCYID += lbTrancode.Text.Trim() + "|";
                    lbCCYID = (Label)gvr.Cells[1].FindControl("lblCCYID");
                    Str_ProTranCCYID += lbCCYID.Text.Trim() + "#";
                }
            }
            Session["_3thamso"] = Str_ProTranCCYID.Substring(0, Str_ProTranCCYID.Length - 1);
        }
        catch (Exception ex)
        {

        }
        if (Str_ProTranCCYID == "")
        {
            lblError.Text = Resources.labels.pleaseselecttransactiontype;
        }

        else
        {
            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?po=3&p=354"));
        }
    }
    protected void lbDeptDelete_Click(object sender, EventArgs e)
    {
        // Response.Redirect("~/Default.aspx?po=3&p=203&did="+lstDept.SelectedValue);
    }
    protected void lbDeptEdit_Click(object sender, EventArgs e)
    {
        // Response.Redirect("~/Default.aspx?po=3&p=201&a=edit&did="+lstDept.SelectedValue);
    }
    protected void lbDeptInsert_Click(object sender, EventArgs e)
    {
        // Response.Redirect("~/Default.aspx?po=3&p=200&a=add");
    }
}
