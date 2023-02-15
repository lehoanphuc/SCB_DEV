using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.ExceptionCollection;
using SmartPortal.SEMS;
using System.Data;
using SmartPortal.BLL;
using SmartPortal.Constant;
using SmartPortal.Common.Utilities;
using SmartPortal.Model;

public partial class Widgets_SEMSDistrict_Widget : WidgetBase
{
    public static bool isAscend = false;
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";
    string Pagemain = "208";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    private int size = 0;
    string cid = "0";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            litError.Text = "";
            cid = GetParamsPage("cid")[0].Trim();
            if (!IsPostBack)
            {
                GridViewPaging.Visible = false;
                LoadDist();
                //divResult.Visible = false;
                BindData();
            }
            GridViewPaging.pagingClickArgs += new EventHandler(GridViewPaging_Click);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    protected void gvTownshipList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            LinkButton lbltownshipcode;
            Label lbltownshipname;
            Label lbltownshipnameMM;
            Label lbldistrictname;
            LinkButton lbEdit;
            LinkButton lbDelete;
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
                cbxSelect.Attributes.Add("onclick", "ChildClick(this);");

                lbltownshipcode = (LinkButton)e.Row.FindControl("lbltownshipcode");
                lbltownshipname = (Label)e.Row.FindControl("lbltownshipname");
                lbltownshipnameMM = (Label)e.Row.FindControl("lbltownshipnameMM");
                lbldistrictname = (Label)e.Row.FindControl("lbldistrictname");
                lbEdit = (LinkButton)e.Row.FindControl("lbEdit");
                lbDelete = (LinkButton)e.Row.FindControl("lbDelete");

                lbltownshipcode.Text = drv["TownshipCode"].ToString();
                lbltownshipname.Text = drv["TownshipName"].ToString();
                lbltownshipnameMM.Text = drv["TownshipNameMM"].ToString();
                lbldistrictname.Text = drv["DistName"].ToString();
         
                if (cbxSelect.Enabled)
                {
                    size++;
                }

                hdCounter.Value = "0";
                hdPageSize.Value = size.ToString();
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData2();
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?po=4&p=TOWNSHIPADD" + "&a=ADD" + "&b=" + SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cid"].ToString()));
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
    
            CheckBox cbxDelete;
            LinkButton lbltownshipcode;
            string strtownsipcode = "";
            try
            {
                foreach (GridViewRow gvr in gvTownshipList.Rows)
                {
                    cbxDelete = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                    if (cbxDelete.Checked == true)
                    {
                        lbltownshipcode = (LinkButton)gvr.Cells[1].FindControl("lbltownshipcode");
                        strtownsipcode += lbltownshipcode.CommandArgument.Trim() + "#";
                    }
                }
                if (string.IsNullOrEmpty(strtownsipcode))
                {
                    lblError.Text = Resources.labels.pleaseselectbeforedeleting;
                    return;
                }
                else
                {
                    string[] township = strtownsipcode.Split('#');
                    for (int i = 0; i < township.Length - 1; i++)
                    {
                        new Township().DelTownship(township[i], ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE == "0")
                        {
                            BindData2();
                            lblError.Text = Resources.labels.deletetownshipsuccess;
                        }
                        else
                        {
                            if (IPCERRORDESC == "110211")
                            {
                                lblError.Text = Resources.labels.unremovetownshipbydistrict;
                            }
                            else
                            {
                                lblError.Text = Resources.labels.unremovetownship;
                            }
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
            }
    }
    protected void gvTownshipList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string commandName = e.CommandName;
        string commandArg = e.CommandArgument.ToString();
      
            switch (commandName)
            {
                case IPC.ACTIONPAGE.DETAILS:
                    RedirectToActionPage(IPC.ACTIONPAGE.DETAILS, "&" + SmartPortal.Constant.IPC.ID + "=" + commandArg + "&b=" + SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cid"].ToString());
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    RedirectToActionPage(IPC.ACTIONPAGE.EDIT, "&" + SmartPortal.Constant.IPC.ID + "=" + commandArg + "&b=" + SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cid"].ToString());
                    break;
            }
    }
    protected void gvTownshipList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string commandArg = string.Empty;
            commandArg = ((LinkButton)gvTownshipList.Rows[e.RowIndex].Cells[1].FindControl("lbltownshipcode")).CommandArgument;
            new Township().DelTownship(commandArg, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                BindData2();
                lblError.Text = Resources.labels.deletetownshipsuccess;
            }
            else
            {
                if (IPCERRORDESC == "110211")
                {
                    lblError.Text = Resources.labels.unremovetownshipbydistrict;
                }
                else
                {
                    lblError.Text = Resources.labels.unremovetownship;
                }
                return;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    #region load data
    private void GridViewPaging_Click(object sender, EventArgs e)
    {
        gvTownshipList.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
        gvTownshipList.PageIndex = Convert.ToInt32(((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text) - 1;
        BindData();
    }
    void BindData()
    {
        try
        {
            divResult.Visible = true; 
            litError.Text = string.Empty;
            if (Convert.ToInt32(((HiddenField)GridViewPaging.FindControl("TotalRows")).Value) < gvTownshipList.PageIndex * gvTownshipList.PageSize) return;
            Township objTown = new Township();
            DataSet ds = new DataSet();
            ds = objTown.GetTownShipByCondition(" ", txtTownShipname.Text, cid, txtTownShipnameMM.Text, gvTownshipList.PageSize,
                gvTownshipList.PageIndex * gvTownshipList.PageSize, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0"&& ds != null  && ds.Tables.Count > 0)
            {
                gvTownshipList.DataSource = ds;
                gvTownshipList.DataBind();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    litError.Text = String.Empty;
                    GridViewPaging.Visible = true;
                    ((HiddenField)GridViewPaging.FindControl("TotalRows")).Value = ds.Tables[0].Rows[0]["TRECORDCOUNT"].ToString();
                }
                else
                {
                    GridViewPaging.Visible = false;
                    litError.Text = "<p class='divDataNotFound'>" + Resources.labels.datanotfound + "</p>";
                }
            }
            else
            {
                throw new IPCException(IPCERRORDESC);
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void LoadDist()
    {
        try
        {
            DataSet dts = new DataSet();
            dts = new SmartPortal.SEMS.Township().GetDistList(ref IPCERRORCODE, ref IPCERRORDESC);
            string ErrorCode = string.Empty;
            string ErrorDesc = string.Empty;

            if (IPCERRORCODE == "0")
            {
                //ddlDistCode.DataSource = dts;
                //ddlDistCode.DataTextField = "DistName";
                //ddlDistCode.DataValueField = "DistCode";
                //ddlDistCode.DataBind();
                //ddlDistCode.Items.Insert(0, new ListItem(Resources.labels.tatca, ""));
            }
            else
            {
                lblError.Text = IPCERRORDESC;
            }


        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    #endregion
    void BindData2()
    {
        try
        {
            ((TextBox) GridViewPaging.FindControl("SelectedPageNo")).Text = "1";
            ((HiddenField) GridViewPaging.FindControl("hdfCurrentPage")).Value = "1";
            gvTownshipList.PageSize = Convert.ToInt32(((DropDownList) GridViewPaging.FindControl("PageRowSize")).SelectedValue);
            string SelectedPageNo = ((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text;
            gvTownshipList.PageIndex = !string.IsNullOrEmpty(SelectedPageNo) ? Convert.ToInt32(SelectedPageNo) - 1 : 0;
            BindData();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings ["sysec"], "");
        }
    }

    protected void Button8_OnClickck1(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=280"));
    }

    protected bool CheckActionPage(string action)
    {
        return PagesBLL.ChekcPermitPageAction(Pagemain, Session["userName"].ToString(), action);
    }
}
