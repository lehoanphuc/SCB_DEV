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
using SmartPortal.SEMS;

public partial class Widgets_Admintopup_Widget : WidgetBase
{
    public static bool isAscend = false;
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";
    private string IPCERRORCODE = "";
    private string IPCERRORDESC = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = "";
            litError.Text = "";
            litPager.Text = "";
            if (!IsPostBack)
            {

                //lay danh sach telco
                DataSet ds = new SmartPortal.SEMS.Topup().GetTecoList(ref IPCERRORCODE, ref IPCERRORDESC);
                ddlTelco.DataTextField = "TelcoName";
                ddlTelco.DataValueField = "TelcoID";
                ddlTelco.DataSource = ds;
                ddlTelco.DataBind();

                //lay danh sach menh gia the theo telco
                LoadCardAmount(ddlTelco.SelectedValue.ToString());

                btnSearch_Click(sender, e);
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name,
                System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(),
                Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                Request.Url.Query);

        }
    }

    protected void LoadCardAmount(string selected)
    {
        DataSet dsamount = new SmartPortal.SEMS.Topup().GetAmountbyTelco(selected, "1", ref IPCERRORCODE, ref IPCERRORDESC);
        dsamount.Tables[0].Columns.Add("CardandCCYID", typeof(string), "CardAmount +' '+ CCYID");
        ddlAmount.DataTextField = "CardandCCYID";
        ddlAmount.DataValueField = "CardID";
        ddlAmount.DataSource = dsamount;
        ddlAmount.DataBind();
    }
    public void ddlTelco_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCardAmount(ddlTelco.SelectedValue.ToString());
        BindData();
    }
    public void btnImport_Click(object sender, EventArgs eventArgs)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=421"));
    }

    public void btnDelete_Click(object sender, EventArgs eventArgs)
    {
        CheckBox cbxDelete;
        Label lblserial;

        string Str_Serials = "";
        try
        {
            foreach (GridViewRow gvr in gvTopUp.Rows)
            {
                cbxDelete = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                if (cbxDelete.Checked == true)
                {
                    lblserial = (Label)gvr.Cells[1].FindControl("lblserial");
                    Str_Serials += lblserial.Text.Trim().Replace(" ", "") + "#";
                }
            }
            Session["_TOPUPSERIAL"] = Str_Serials.Substring(0, Str_Serials.Length - 1);
        }
        catch (Exception ex)
        {

        }
        if (Str_Serials == "")

        {
            lblError.Text = Resources.labels.banvuilongchonmayatmdexoa;
            BindData();
        }
        else
        {
            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=432"));
        }
    }

    public void btnSearch_Click(object sender, EventArgs eventArgs)
    {
        Session["search"] = "true";
        BindData();
    }

    public void BindData()
    {
        try
        {
            DataSet dtTU = new DataSet();
            if (Session["search"] != null)
            {
                string ensoftpin = (txtSoftPin.Text == "") ? "" :
                SmartPortal.Common.Encrypt.EncryptData1(Utility.KillSqlInjection(txtSoftPin.Text.Trim()));
                dtTU = new SmartPortal.SEMS.Topup().GetTopUp(ddlTelco.SelectedValue.Trim(),
                    ddlAmount.SelectedValue.Trim(), Utility.KillSqlInjection(txtSerialNo.Text.Trim()),
                    ensoftpin, ref IPCERRORCODE, ref IPCERRORDESC);
            }

            if (IPCERRORCODE != "0")
            {
                throw new IPCException(IPCERRORDESC);
            }

            gvTopUp.DataSource = dtTU;
            gvTopUp.DataBind();

            //set to session to export
            Session["DataExport"] = gvTopUp;
            Session["TableExport"] = dtTU;
            if (dtTU.Tables[0].Rows.Count > 0)
            {
                litPager.Text = Resources.labels.danghienthi + " <b>" +
                                (((gvTopUp.PageIndex) * gvTopUp.PageSize) + gvTopUp.Rows.Count).ToString() + "</b> " +
                                Resources.labels.cua + " <b>" + dtTU.Tables[0].Rows.Count.ToString() + "</b> " +
                                Resources.labels.dong;
            }
            else
            {
                litError.Text = "<p class='divDataNotFound'>" + Resources.labels.datanotfound + "</p>";
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"],
                "Widgets_SEMSCustomerList_Widget", "BindData", IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"],
                Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                "Widgets_SEMSCustomerList_Widget", "BindData", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                Request.Url.Query);

        }

    }

    public void gvTopUp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            Label lbltelco;
            Label lblserial;
            Label lblsoftpin;
            Label lbldatecreate;
            Label lblavailable;
            Label lblexpriedate;
            Label lblbuyrate;
            Label lblsellrate;
            Label lblcardprice;
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
                e.Row.Attributes.Add("onmousemove", "this.className='hightlight';");
                e.Row.Attributes.Add("onmouseout", "this.className='nohightlight';");

                lbltelco = (Label)e.Row.FindControl("lbltelco");
                lblserial = (Label)e.Row.FindControl("lblserial");
                lblsoftpin = (Label)e.Row.FindControl("lblsoftpin");
                lbldatecreate = (Label)e.Row.FindControl("lbldatecreate");
                lblavailable = (Label)e.Row.FindControl("lblavailable");
                lblexpriedate = (Label)e.Row.FindControl("lblexpriedate");
                lblbuyrate = (Label)e.Row.FindControl("lblbuyrate");
                lblsellrate = (Label)e.Row.FindControl("lblsellrate");
                lblcardprice = (Label)e.Row.FindControl("lblcardprice");

                hpDelete = (HyperLink)e.Row.FindControl("hpDelete");

                string sp = drv["SoftPin"].ToString().Replace(" ", "");
                sp = SmartPortal.Common.Encrypt.DecryptSoftpin(sp);
                sp = sp.Substring(sp.Length - 8, 8);

                lbltelco.Text = ddlTelco.SelectedItem.ToString();
                lblserial.Text = drv["SerialNo"].ToString();
                lblsoftpin.Text = "********" + sp;
                lbldatecreate.Text = SmartPortal.Common.Utilities.Utility.IsDateTime2(drv["DateCreated"].ToString()).ToString("dd/MM/yyyy");
                lblavailable.Text = (drv["IsUsed"].ToString() == "True") ? "False" : "True";
                lblexpriedate.Text = drv["ExpireTime"].ToString();
                lblbuyrate.Text = drv["BuyRate"].ToString();
                lblsellrate.Text = drv["SellRate"].ToString();
                lblcardprice.Text = drv["CardAmount"].ToString();

                hpDelete.Text = Resources.labels.huy;
                hpDelete.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=432&a=delete&pid=" + drv["SerialNo"].ToString());
            }
        }
        catch (Exception ex)
        {
        }
    }

    public void gvTopUp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvTopUp.PageIndex = e.NewPageIndex;
            BindData();
        }
        catch
        {
        }
    }
}
