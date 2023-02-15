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
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;

public partial class Widgets_IBCorpUserApproveLimit_Widget : WidgetBase
{
    public static bool isAscend = false;
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //litPager.Text = "";
            ltrError.Text = "";
            lblError.Text = "";

            if (!IsPostBack)
            {
                //load user
                //Session["userID"] = "U44864000";
                DataSet ds = new SmartPortal.IB.Transactions().GetUserOfContractNoByUID(Utility.KillSqlInjection(Session["userID"].ToString()),"IB", ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE != "0")
                {
                    throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORCODE);
                }

                DataTable deptTable = new DataTable();
                deptTable = ds.Tables[0];

                lstDept.DataSource = deptTable;
                lstDept.DataTextField = "FULLNAME";
                lstDept.DataValueField = "USERID";
                lstDept.DataBind();

                if (lstDept.Items.Count != 0)
                {
                    lstDept.Items[0].Selected = true;
                }
            }
            BindData();
            

            goto EXIT;
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.Message, this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_IBCorpUserApproveLimit_Widget", "Page_Load", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }


    EXIT: ;

    }
    void BindData()
    {
        try
        {
            
            DataSet dtLimitTeller = new DataSet();
            dtLimitTeller = new SmartPortal.SEMS.Transactions().GetAllLimitTellerByUID(Utility.KillSqlInjection(lstDept.SelectedValue), "", "", ref IPCERRORCODE, ref IPCERRORDESC);


            if (IPCERRORCODE == "0")
            {
                gvUser.DataSource = dtLimitTeller;
                gvUser.DataBind();
            }
            else
            {
                throw new IPCException(IPCERRORDESC);
            }


            if (lstDept.Items.Count == 0)
            {
                lbUserInsert.Visible = false;
                lbDeleteLimit.Visible = false;
            }
            else
            {
                lbUserInsert.Visible = true;

                if (gvUser.Items.Count == 0)
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
    //protected void gvUser_RowDataBound(object sender, RepeaterItemEventArgs e)
    //{
    //    try
    //    {
    //        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
    //        {
    //            DataRowView drv = e.Item.DataItem as DataRowView;
    //            RepeaterItem item = e.Item;
    //            Label lblUID = (Label)item.FindControl("lblUID");
    //            Label lblTrans = (Label)item.FindControl("lblTrans");
    //            Label lblLimit = (Label)item.FindControl("lblLimit");
    //            Label lblCCYID = (Label)item.FindControl("lblCCYID");
    //            Label lblTRANCODE = (Label)item.FindControl("lblTRANCODE");

                
    //            lblUID.Text = drv["USERID"].ToString();
    //            lblTrans.Text = drv["PAGENAME"].ToString();
    //            lblTRANCODE.Text = drv["IPCTRANCODE"].ToString();
    //            lblLimit.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(drv["LIMITAPPROVE"].ToString(), drv["CCYID"].ToString().Trim());
    //            lblCCYID.Text = drv["CCYID"].ToString();

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
    //        SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
    //    }
    //}
  

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
            lblError.Text = "";
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
        RedirectToActionPage(IPC.ACTIONPAGE.ADD, "&" + "uid=" + lstDept.SelectedValue, false);
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

            if (!IBCheckPermitPageAction(IPC.ACTIONPAGE.DELETE)) return;

            string lst = hdId.Value;
            if (lst.Length > 0)
            {
                string[] arrSplit = lst.Split('#');
                foreach (string item in arrSplit)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        string[] pros = item.Split('|');
                         new SmartPortal.SEMS.Transactions().DeleteUserApproveLimit(pros[0], pros[1], pros[2], ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE == "0")
                        {
                            BindData();
                            lblError.Text = Resources.labels.xoahanmucgiaodichthanhcong;
                        }
                    }
                }
            }
            else
            {
                lblError.Text = Resources.labels.chooserecordtodelete;
                lblError.Visible = true;
            }

            hdId.Value = string.Empty;
       
        }
        catch (Exception ex)
        {

        }
    
    }
    protected void lbDeptDelete_Click(object sender, EventArgs e)
    {
    }
    protected void lbDeptEdit_Click(object sender, EventArgs e)
    {
    }
    protected void lbDeptInsert_Click(object sender, EventArgs e)
    {
    }

    protected void gvUser_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                DataRowView drv = e.Item.DataItem as DataRowView;
                RepeaterItem item = e.Item;
                Label lblStatus = (Label)e.Item.FindControl("lblStatus");
                LinkButton lbEdit = (LinkButton)e.Item.FindControl("lbEdit");
                LinkButton lbDelete = (LinkButton)e.Item.FindControl("lbDelete");
                Label lblUID = (Label)item.FindControl("lblUID");
                Label lblTrans = (Label)item.FindControl("lblTrans");
                Label lblLimit = (Label)item.FindControl("lblLimit");
                Label lblLimitday = (Label)item.FindControl("lblLimitday");
                Label lblCCYID = (Label)item.FindControl("lblCCYID");
                Label lblTRANCODE = (Label)item.FindControl("lblTRANCODE");

                lblUID.Text = drv["USERID"].ToString();
                lblTrans.Text = drv["PAGENAME"].ToString();
                lblTRANCODE.Text = drv["TRANCODE"].ToString();
                lblLimit.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(drv["ApprovalLimit"].ToString(), drv["CCYID"].ToString().Trim());
                lblLimitday.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(drv["TotalLimitDay"].ToString(), drv["CCYID"].ToString().Trim());
                lblCCYID.Text = drv["CCYID"].ToString();
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    protected void gvUser_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            string commandName = e.CommandName;
            string commandArg = e.CommandArgument.ToString();
            string[] pros = commandArg.Split('|');
            switch (commandName.ToUpper())
            {
                case IPC.DETAILS:
                    if (!IBCheckPermitPageAction("DETAILS")) return;
                    RedirectToActionPage(IPC.ACTIONPAGE.DETAILS, "&" + "uid=" + commandArg, false);
                    break;
                case IPC.EDIT:
                case "EDIT":
                    if (!IBCheckPermitPageAction("EDIT")) return;
                    RedirectToActionPage(IPC.ACTIONPAGE.EDIT, "&"  + "uid=" + pros[0] + "&trcod=" + pros[1] + "&cyid=" + pros[2], false);

                    break;
                case IPC.DELETE:
                case "DELETE":
                    //Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=198&uid=" + commandArg), false);
                    if (!IBCheckPermitPageAction("DELETE")) return;

                    new SmartPortal.SEMS.Transactions().DeleteUserApproveLimit(pros[0], pros[1], pros[2], ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        BindData();
                        lblError.Text = Resources.labels.xoahanmucgiaodichthanhcong;
                    }
                    break;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
}
