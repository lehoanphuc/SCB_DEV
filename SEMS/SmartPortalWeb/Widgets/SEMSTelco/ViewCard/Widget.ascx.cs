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

public partial class Widgets_SEMSTelco_ViewCard_Widget : WidgetBase
{

    string ACTION = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    string PageEdit = "1109";
    string PageBack = "1108";
    protected void Page_Load(object sender, EventArgs e)
    {
        ACTION = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["a"].ToString().Trim();
        if (!IsPostBack)
        {
            GridViewPaging1.Visible = false;
            BindData();
        }
        GridViewPaging1.pagingClickArgs += new EventHandler(GridViewPaging_Click);
    }
    private void GridViewPaging_Click(object sender, EventArgs e)
    {
        gvCardid.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging1.FindControl("PageRowSize")).SelectedValue);
        gvCardid.PageIndex = Convert.ToInt32(((TextBox)GridViewPaging1.FindControl("SelectedPageNo")).Text) - 1;
        BindData();
    }
    void BindData()
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable custTable = new DataTable();
            switch (ACTION)
            {
                case "viewdetail":
                case "DETAILS":
                    var cardid = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["ID"].ToString();
                    ds = new SmartPortal.SEMS.User().GetalltelcoDetail(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["ID"].ToString(), "ALL", "ALL", "ALL", gvCardid.PageSize, gvCardid.PageIndex * gvCardid.PageSize, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE != "0")
                    {
                        throw new IPCException(IPCERRORDESC);
                    }
                    else
                    {
                        custTable = ds.Tables[1];
                    }
                    gvCardid.DataSource = ds;
                    gvCardid.DataBind();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        GridViewPaging1.Visible = true;
                        ((HiddenField)GridViewPaging1.FindControl("TotalRows")).Value = ds.Tables[0].Rows[0]["TRECORDCOUNT"].ToString();
                    }
                    else
                    {
                        GridViewPaging1.Visible = false;
                    }
                    break;
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSTelco_Control_Widget", "BindData", IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSTelco_Control_Widget", "BindData", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    #region gvCardid
    protected void gvCardid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            Label lblcardid;
            Label lblshortname;
            Label lblcardamount;
            Label lblrealmoney;
            Label lblccyid;
            Label lblstatus;
            Label lbltype;
            HyperLink hpedit;
            LinkButton hpdelete;

            DataRowView drv;


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView)e.Row.DataItem;

                lblcardid = (Label)e.Row.FindControl("lblcardid");
                lblshortname = (Label)e.Row.FindControl("lblshortname");
                lblcardamount = (Label)e.Row.FindControl("lblcardamount");
                lblrealmoney = (Label)e.Row.FindControl("lblrealmoney");
                lblccyid = (Label)e.Row.FindControl("lblccyid");
                lblstatus = (Label)e.Row.FindControl("lblstatus");
                lbltype = (Label)e.Row.FindControl("lbltype");
                hpedit = (HyperLink)e.Row.FindControl("hpedit");
                hpdelete = (LinkButton)e.Row.FindControl("hpdelete");

                lblcardid.Text = drv["CardID"].ToString();
                lblshortname.Text = drv["ShortName"].ToString();
                lblcardamount.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(drv["CardAmount"].ToString(), drv["CCYID"].ToString().Trim());
                lblrealmoney.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(drv["RealAmount"].ToString(), drv["CCYID"].ToString().Trim());
                lblccyid.Text = drv["CCYID"].ToString();
                lblstatus.Text = drv["Status"].ToString();
                lbltype.Text = drv["Type"].ToString();


                switch (drv["Status"].ToString())
                {
                    case "A":
                        lblstatus.Text = Resources.labels.active;
                        break;
                    case "D":
                        lblstatus.Text = Resources.labels.condelete;
                        break;
                    case "B":
                        lblstatus.Text = Resources.labels.conblock;
                        break;
                }
            }

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSTelco_Control_Widget", "gvCardid_RowDataBound", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    #endregion
}