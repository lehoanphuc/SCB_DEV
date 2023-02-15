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

public partial class Widgets_SEMSContractListCorp_Widget : WidgetBase
{
    public static bool isAscend = false;
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";

    protected void Page_Load(object sender, EventArgs e)
    {
       
            if (!IsPostBack)
            {
                try
                {
                    DropDownList2.Items.Add(new ListItem(Resources.labels.tatca, "ALL"));
                    DropDownList2.Items.Add(new ListItem(Resources.labels.moi, "N"));
                    DropDownList2.Items.Add(new ListItem(Resources.labels.hoatdong, "A"));
                    DropDownList2.Items.Add(new ListItem(Resources.labels.xoa, "D"));

                    #region load loai hop dong
                    DataSet dsUserType = new DataSet();
                    dsUserType = new SmartPortal.SEMS.Services().GetAllUserType("","Y", ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        DataTable dtUserType = new DataTable();
                        dtUserType = dsUserType.Tables[0];

                        ddlContractType.DataSource = dtUserType;
                        ddlContractType.DataTextField = "TYPENAME";
                        ddlContractType.DataValueField = "USERTYPE";
                        ddlContractType.DataBind();

                        ddlContractType.Items.Insert(0, new ListItem(Resources.labels.tatca, "ALL"));
                    }
                    else
                    {
                        throw new IPCException(IPCERRORDESC);
                    }
                    #endregion

                    BindData();

                }
                catch (IPCException IPCex)
                {
                    SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSContractList_Widget", "Page_Load", IPCex.ToString(), Request.Url.Query);
                    SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
                }
                catch (Exception ex)
                {
                    SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSContractList_Widget", "Page_Load", ex.ToString(), Request.Url.Query);
                    SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

                } 
            }
       
    }

    void BindData()
    {
        try
        {
            DataSet dtContr = new DataSet();
            dtContr = new SmartPortal.SEMS.Contract().GetContractByCondition(Utility.KillSqlInjection(txtContractCode.Text.Trim()), Utility.KillSqlInjection(txtCustName.Text.Trim()), Utility.KillSqlInjection(txtopenPer.Text.Trim()), Utility.KillSqlInjection(txtOpenDate.Text.Trim()), Utility.KillSqlInjection(txtEndDate.Text.Trim()), ddlContractType.SelectedValue, DropDownList2.SelectedValue,"","", string.Empty,"", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                gvcontractList.DataSource = dtContr;
                gvcontractList.DataBind();
            }
            else
            {

                throw new IPCException(IPCERRORCODE); 
            }

            litPager.Text = Resources.labels.danghienthi + " <b>" + (((gvcontractList.PageIndex) * gvcontractList.PageSize) + gvcontractList.Rows.Count).ToString() + "</b> " + Resources.labels.cua + " <b>" + dtContr.Tables[0].Rows.Count.ToString() + "</b> " + Resources.labels.dong;

            goto EXIT;
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
    EXIT:

        ;
    }

    protected void gvcontractList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            HyperLink hpcontractCode;
            Label lblcustName;
            Label lblOpen;
            Label lblOpendate;
            Label lblClosedate;
            Label lblcontractType;
            Label lblStatus;
            HyperLink hpEdit;
            HyperLink hpDelete;

            DataRowView drv;

            if (e.Row.RowType == DataControlRowType.Header)
            {
                cbxSelect = new CheckBox();
                cbxSelect.ID = "cbxSelectAll";
                cbxSelect.Attributes.Add("onclick", "SelectCbx(this);");
                e.Row.Cells[0].Controls.Add(cbxSelect);
            }
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                TableRow tbRow = (TableRow)(e.Row.Cells[0].Controls[0].Controls[0]);
                tbRow.Cells.AddAt(0, new TableCell());
                tbRow.Cells[0].Text = "<strong>" + Resources.labels.page + " :</strong>";
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView)e.Row.DataItem;


                cbxSelect = (CheckBox)e.Row.FindControl("cbxSelect");

                if (drv["Status"].ToString().Trim() == SmartPortal.Constant.IPC.DELETE)
                {
                    cbxSelect.Enabled = false;
                }

                e.Row.Attributes.Add("onmousemove", "this.className='hightlight';");
                e.Row.Attributes.Add("onmouseout", "this.className='nohightlight';");


                hpcontractCode = (HyperLink)e.Row.FindControl("hpcontractCode");
                lblcustName = (Label)e.Row.FindControl("lblcustName");
                lblOpen = (Label)e.Row.FindControl("lblOpen");
                lblOpendate = (Label)e.Row.FindControl("lblOpendate");
                lblClosedate = (Label)e.Row.FindControl("lblClosedate");
                lblcontractType = (Label)e.Row.FindControl("lblcontractType");
                lblStatus = (Label)e.Row.FindControl("lblStatus");

                hpEdit = (HyperLink)e.Row.FindControl("hpEdit");
                hpDelete = (HyperLink)e.Row.FindControl("hpDelete");
                //cbxSelect.Enabled = true;
                //cbxSelect.Attributes.Add("onclick", "HighLightCBX('" + e.Row.ClientID + "',this)");


                hpcontractCode.Text = drv["ContractNo"].ToString();
                hpcontractCode.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=146&a=viewdetail&cn=" + drv["ContractNo"].ToString());
                lblcustName.Text = drv["FULLNAME"].ToString();
                lblOpen.Text = drv["UserCreate"].ToString();
                lblOpendate.Text = SmartPortal.Common.Utilities.Utility.IsDateTime2(drv["CreateDate"].ToString()).ToString("dd/MM/yyyy");
                lblClosedate.Text = SmartPortal.Common.Utilities.Utility.IsDateTime2(drv["EndDate"].ToString()).ToString("dd/MM/yyyy");
                //lblStatus.Text = "<img src='Widgets/SEMSContractList/Images/activecheck_icon.gif'/>";
                
               DataSet dsUserType = new DataSet();
               dsUserType=new SmartPortal.SEMS.Services().GetAllUserType(drv["USERTYPE"].ToString().Trim(),"",ref IPCERRORCODE,ref IPCERRORDESC);
               if (IPCERRORCODE == "0")
               {
                   DataTable dtUserType = new DataTable();
                   dtUserType = dsUserType.Tables[0];

                   if (dtUserType.Rows.Count != 0)
                   {
                       lblcontractType.Text = dtUserType.Rows[0]["TYPENAME"].ToString();
                   }
               }
               else
               {
                   throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
               }
                
                switch (drv["Status"].ToString().Trim())
                {
                    case SmartPortal.Constant.IPC.NEW:
                        lblStatus.Text = Resources.labels.connew;
                        break;
                    case SmartPortal.Constant.IPC.DELETE:
                        lblStatus.Text = Resources.labels.daxoa;
                        break;
                    case SmartPortal.Constant.IPC.ACTIVE:
                        lblStatus.Text = Resources.labels.conactive;
                        break;
                    case SmartPortal.Constant.IPC.BLOCK:
                        lblStatus.Text = Resources.labels.tamdung;
                        break;
                }

                if (drv["Status"].ToString().Trim() == SmartPortal.Constant.IPC.DELETE)
                {
                    hpEdit.Text="<img src='widgets/semscontractlist/images/stop.gif'/>";                    

                    hpDelete.Text = "<img src='widgets/semscontractlist/images/stop.gif'/>";                    
                }
                else
                {
                    hpEdit.Text = "<img src='Widgets/Pages/view/images/icon_edit.gif'/>";
                    hpEdit.ToolTip = Resources.labels.edit;
                    hpEdit.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=146&a=edit&cn=" + drv["ContractNo"].ToString());

                    hpDelete.Text = "<img src='Widgets/Pages/view/images/icon_delete.gif'/>";
                    hpDelete.ToolTip = Resources.labels.delete;
                    hpDelete.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=166&cn=" + drv["ContractNo"].ToString() + "&returnUrl=" + SmartPortal.Common.Encrypt.EncryptData(HttpContext.Current.Request.Url.PathAndQuery));

                }

             }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSContractList_Widget", "gvcontractList_RowDataBound", IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
        catch(Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSContractList_Widget", "gvcontractList_RowDataBound", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
     
    }

    protected void gvcontractList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
       
            gvcontractList.PageIndex = e.NewPageIndex;
            BindData();
       
    }
    protected void gvcontractList_Sorting(object sender, GridViewSortEventArgs e)
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
        try
        {
            DataSet ds = new DataSet();
            DataTable dataTable;

            ds = new SmartPortal.SEMS.Contract().GetContractByCondition(Utility.KillSqlInjection(txtContractCode.Text.Trim()), Utility.KillSqlInjection(txtCustName.Text.Trim()), Utility.KillSqlInjection(txtopenPer.Text.Trim()), Utility.KillSqlInjection(txtOpenDate.Text.Trim()), Utility.KillSqlInjection(txtEndDate.Text.Trim()), ddlContractType.SelectedValue, DropDownList2.SelectedValue,"","", string.Empty,"", ref IPCERRORCODE, ref IPCERRORDESC); ;

            if (IPCERRORCODE == "0")
            {
                dataTable = ds.Tables[0];
            }
            else
            {
                dataTable = null;
            }

            if (dataTable != null)
            {
                DataView dataView = new DataView(dataTable);
                dataView.Sort = sortExpression + direction;

                gvcontractList.DataSource = dataView;
                gvcontractList.DataBind();
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=165"));
    }
   
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        CheckBox cbxDelete;
        HyperLink hpContractNo;

        string Str_ContractNo= "";
        try
        {
            foreach (GridViewRow gvr in gvcontractList.Rows)
            {
                cbxDelete = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                if (cbxDelete.Checked == true)
                {
                    hpContractNo = (HyperLink)gvr.Cells[1].FindControl("hpcontractCode");
                    Str_ContractNo += hpContractNo.Text.Trim() + "#";
                }
            }
            Session["_ContractNo"] = Str_ContractNo.Substring(0, Str_ContractNo.Length - 1);

            goto EXIT;
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
    EXIT:
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=166" + "&returnUrl=" + SmartPortal.Common.Encrypt.EncryptData(HttpContext.Current.Request.Url.PathAndQuery))); 
    }
}
