using SmartPortal.BLL;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using SmartPortal.Model;
using SmartPortal.SEMS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Widgets_SEMSBillerCategory_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    private int size = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lbMessage.Text = string.Empty;
            if (!IsPostBack)
            {
                //btnDelete.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.DELETE);
                //btnAdd.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.ADD);
                GridViewPaging.Visible = false;
                divResult.Visible = false;
            }

            GridViewPaging.pagingClickArgs += new EventHandler(GridViewPaging_Click);
        }

        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    private void BindData()
    {
        try
        {
            divResult.Visible = true;
            litError.Text = string.Empty;
            if (Convert.ToInt32(((HiddenField)GridViewPaging.FindControl("TotalRows")).Value)
                < gvBiller.PageIndex * gvBiller.PageSize) return;

            DataSet dataSet = new DataSet();
            dataSet = new SmartPortal.SEMS.EBA_BillerCategory().GetBillerCat
                (Utility.KillSqlInjection(txtcatID.Text.Trim())
                , Utility.KillSqlInjection(txtCatName.Text.Trim())
                , Utility.KillSqlInjection(txtcatShortName.Text.Trim())
                , ddlStatus.SelectedValue.Trim(), gvBiller.PageSize, gvBiller.PageIndex * gvBiller.PageSize, ref IPCERRORCODE, ref IPCERRORDESC);

            if (IPCERRORCODE == "0")
            {
                gvBiller.DataSource = dataSet;
                gvBiller.DataBind();
            }
            else
            {
                lbMessage.Text = IPCERRORDESC;
            }
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                litError.Text = String.Empty;
                GridViewPaging.Visible = true;
                ((HiddenField)GridViewPaging.FindControl("TotalRows")).Value = dataSet.Tables[0].Rows[0]["TRECORDCOUNT"].ToString();
            }
            else
            {
                litError.Text = "<p class='divDataNotFound'>" + Resources.labels.datanotfound + "</p>";
                GridViewPaging.Visible = false;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    private void GridViewPaging_Click(object sender, EventArgs e)
    {
        try
        {
            gvBiller.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
            gvBiller.PageIndex = Convert.ToInt32(((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text) - 1;
            BindData();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData2();
    }
    private void BindData2()
    {
        try
        {
            ((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text = "1";
            ((HiddenField)GridViewPaging.FindControl("hdfCurrentPage")).Value = "1";
            gvBiller.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
            string SelectedPageNo = ((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text;
            gvBiller.PageIndex = !string.IsNullOrEmpty(SelectedPageNo) ? Convert.ToInt32(SelectedPageNo) - 1 : 0;
            BindData();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    } 
    protected void gvBiller_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            Label lbCatName, lbCatShortName, lbStatus, lbDateCreated;
            Image imgLogo;
            LinkButton btnDetail, lbEdit, lbDelete;
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
                cbxSelect = (CheckBox)e.Row.FindControl("cbxSelect");
                cbxSelect.Attributes.Add("onclick", "ChildClick(this);");

                btnDetail = (LinkButton)e.Row.FindControl("btnDetail");
                lbCatName = (Label)e.Row.FindControl("lbCatName");
                lbCatShortName = (Label)e.Row.FindControl("lbCatShortName");
                lbStatus = (Label)e.Row.FindControl("lbStatus");
                imgLogo = (Image)e.Row.FindControl("imgLogo");
                lbDateCreated = (Label)e.Row.FindControl("lbDateCreated");
                lbEdit = (LinkButton)e.Row.FindControl("lbEdit");
                lbDelete = (LinkButton)e.Row.FindControl("lbDelete");

                btnDetail.Text = drv["CatID"].ToString();
                lbCatName.Text = drv["CatName"].ToString();
                lbCatShortName.Text = drv["CatShortName"].ToString();
                switch (drv["Status"].ToString())
                {
                    case "A":
                        lbStatus.Text = Resources.labels.active;
                        lbStatus.Attributes.Add("class", "label-success");
                        break;
                    case "I":
                        lbStatus.Text = Resources.labels.inactive;
                        lbStatus.Attributes.Add("class", "label-warning");
                        break;

                }
                lbDateCreated.Text = Convert.ToDateTime(drv["DateCreate"].ToString()).ToString("dd/MM/yyyy HH:mm:ss");
                if (string.IsNullOrEmpty(drv["CatLogoBin"].ToString()))
                {
                    imgLogo.Visible = false;
                }
                else
                {
                    imgLogo.ImageUrl = drv["CatLogoBin"].ToString();
                }
                if (!CheckPermitPageAction(IPC.ACTIONPAGE.EDIT))
                {
                    lbEdit.Enabled = false;
                    lbEdit.OnClientClick = string.Empty;
                }
                if (!CheckPermitPageAction(IPC.ACTIONPAGE.DELETE))
                {
                    lbDelete.Enabled = false;
                    lbDelete.OnClientClick = string.Empty;
                }
                if (cbxSelect.Enabled)
                {
                    size++;
                }
                hdCounter.Value = "0";
                hdPageSize.Value = size.ToString();
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (CheckPermitPageAction(IPC.ACTIONPAGE.ADD))
        {
            RedirectToActionPage(IPC.ACTIONPAGE.ADD, string.Empty);
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            if (CheckPermitPageAction(IPC.ACTIONPAGE.DELETE))  
            {
                List<string> listCatID_ToDelete = new List<string>();
                foreach (GridViewRow gridViewRow in gvBiller.Rows)
                {
                    if (((CheckBox)gridViewRow.FindControl("cbxSelect")).Checked)
                    {
                        string catID = ((LinkButton)gridViewRow.FindControl("btnDetail")).CommandArgument;
                        listCatID_ToDelete.Add(catID);
                    }
                }

                if (listCatID_ToDelete.Count > 0)
                {
                    foreach (string _catID in listCatID_ToDelete)
                    {
                        new EBA_BillerCategory().DeleteBillerCat(_catID, ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE.Equals("0"))
                        {
                            BindData2();
                            lbMessage.Text = Resources.labels.xoathanhcong;
                        }
                        else
                        {
                            lbMessage.Text = IPCERRORDESC;
                            return;
                        }
                    }
                }
                else
                {
                    litError.Text = string.Empty;
                    lbMessage.Text = Resources.labels.pleaseselectbeforedeleting;
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    protected void gvBiller_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string commandName = e.CommandName;
        string commandArg = e.CommandArgument.ToString();

        if (CheckPermitPageAction(commandName))
        {
            switch (commandName)
            {
                case IPC.ACTIONPAGE.EDIT: 
                    RedirectToActionPage(IPC.ACTIONPAGE.EDIT, "&" + IPC.CATID + "=" + commandArg);
                    break;
                case IPC.ACTIONPAGE.DETAILS:
                    RedirectToActionPage(IPC.ACTIONPAGE.DETAILS, "&" + IPC.CATID + "=" + commandArg);
                    break;
            }
        }
    }
    protected void gvBiller_RowDeleting(object sender, GridViewDeleteEventArgs e) 
    {
        try
        {
            string commandArg = string.Empty;
            commandArg = ((LinkButton)gvBiller.Rows[e.RowIndex].Cells[1]
                .FindControl("btnDetail")).CommandArgument;

            new EBA_BillerCategory().DeleteBillerCat(commandArg, ref IPCERRORCODE, ref IPCERRORDESC);

            if (IPCERRORCODE.Equals("0"))
            {
                BindData2();
                lbMessage.Text = Resources.labels.xoathanhcong;
            }
            else
            {
                lbMessage.Text = IPCERRORDESC;
                return;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
}