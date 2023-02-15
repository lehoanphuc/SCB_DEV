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

using System.Text;
using SmartPortal.Common.Utilities;
using SmartPortal.ExceptionCollection;

public partial class Widgets_IBFeedBack_Widget : WidgetBase
{
    string IPCERRORCODE;
    string IPCERRORDESC;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblAlert.Text = "";
            litPager.Text = "";

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
    public void BindData()
    {
        try
        {
        DataTable dtFeedback = new SmartPortal.IB.Transactions().GetFeedback("",txtsgd.Text, Session["userID"].ToString(), ddlstatus.SelectedValue.ToString(),"",txttieude.Text.Trim());
        if (dtFeedback.Rows.Count != 0)
        {
            gvSTV.DataSource = dtFeedback;
            gvSTV.DataBind();
            btnDelete.Visible = true;
            pndetails.Visible = true;
            litPager.Text = Resources.labels.danghienthi + " <b>" + (((gvSTV.PageIndex) * gvSTV.PageSize) + gvSTV.Rows.Count).ToString() + "</b> " + Resources.labels.cua + " <b>" + dtFeedback.Rows.Count.ToString() + "</b> " + Resources.labels.dong;
        }

        else
        {
            btnDelete.Visible = false;
            lblAlert.Text = "<div style='text-align:center; padding-top:10px;padding-bottom:10px;font-weight:bold;color:red'>" + Resources.labels.datanotfound + "</div>";
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
   


    protected void gvSTV_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            HyperLink hpTranCode, hpDelete;
            Label lbltitle, lblFeedid;
            Label lblcontent;
            Label lblstatus;
            Label lblcomment;



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
                e.Row.Attributes.Add("onmousemove", "this.className='hightlight';");
                e.Row.Attributes.Add("onmouseout", "this.className='nohightlight';");

                lblFeedid = (Label)e.Row.FindControl("lblFeedid");
                hpTranCode = (HyperLink)e.Row.FindControl("hpTranCode");
                lbltitle = (Label)e.Row.FindControl("lbltitle");
                lblcontent = (Label)e.Row.FindControl("lblcontent");
                lblstatus = (Label)e.Row.FindControl("lblstatus");
                lblcomment = (Label)e.Row.FindControl("lblcomment");
                hpDelete = (HyperLink)e.Row.FindControl("hpDelete");

                //cbxSelect.Enabled = true;
                //cbxSelect.Attributes.Add("onclick", "HighLightCBX('" + e.Row.ClientID + "',this)");

                hpTranCode.Text = drv["IPCTRANSID"].ToString();
                hpTranCode.NavigateUrl = "~/Default.aspx?po=3&p=395&tid=" + drv["FEEDID"].ToString() +"&a=";

                lblFeedid.Text = drv["FEEDID"].ToString();
                lbltitle.Text = drv["TITLE"].ToString();
                lblcontent.Text = drv["CONTENTFB"].ToString();
               
                lblcomment.Text = drv["COMMENT"].ToString();
                hpDelete.Text = Resources.labels.huy;
                hpDelete.NavigateUrl = "~/Default.aspx?po=3&p=396&tid=" + drv["FEEDID"].ToString();

                if(drv["STATUS"].ToString()==SmartPortal.Constant.IPC.YES)
                {
                    lblstatus.Text = Resources.labels.daxuly;
                }
                if(drv["STATUS"].ToString()==SmartPortal.Constant.IPC.NO)
                {
                    lblstatus.Text = Resources.labels.chuaxuly;
                }

            }
        }
        catch
        {
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        CheckBox cbxDelete;
        Label hpAppTranID;


        string Str_AppTranID = "";
        try
        {
            foreach (GridViewRow gvr in gvSTV.Rows)
            {
                cbxDelete = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                if (cbxDelete.Checked == true)
                {
                    hpAppTranID = (Label)gvr.Cells[1].FindControl("lblFeedid");
                    Str_AppTranID += hpAppTranID.Text.Trim() + "#";
                }
            }
            Session["_FEEDBACKTRANSACTION"] = Str_AppTranID.Substring(0, Str_AppTranID.Length - 1);
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
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?po=3&p=396"));
    }
    protected void btnnew_Click(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?po=3&p=395&tid=&a=add"));
    }
}
