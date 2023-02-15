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
using System.Collections.Generic;
using System.IO;

public partial class Widgets_SEMSScheduleLog_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        litError.Text = "";
        if (!IsPostBack)
        {
            txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            BindData();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            BindData();
        }
        catch
        {
        }
    }
    protected void gvSchedulelog_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            Label lblScheduleID;
            Label lblScheduleType;
            Label lblScheduleName;
            Label lblDescription;
            Label lblUserCreate;
            Label lblCreateDate;
            Label lblEndDate;
            Label lblIPCTransdate;
            Label lblIPCTransWorkDate;
            HyperLink lblIPCTransID;
            Label lblAcctNo;
            Label lblSenderName;
            Label lblReceverAccount;
            Label lblReceiverName;
            Label lblAmount;
            Label lblCKType;
            DataRowView drv;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView)e.Row.DataItem;
                e.Row.Attributes.Add("onmousemove", "this.className='hightlight';");
                e.Row.Attributes.Add("onmouseout", "this.className='nohightlight';");


                lblScheduleID = (Label)e.Row.FindControl("lblScheduleID");
                lblScheduleType = (Label)e.Row.FindControl("lblScheduleType");
                lblScheduleName = (Label)e.Row.FindControl("lblScheduleName");
                lblDescription = (Label)e.Row.FindControl("lblDescription");
                lblUserCreate = (Label)e.Row.FindControl("lblUserCreate");
                lblCreateDate = (Label)e.Row.FindControl("lblCreateDate");
                lblEndDate = (Label)e.Row.FindControl("lblEndDate");
                lblIPCTransdate = (Label)e.Row.FindControl("lblIPCTransdate");
                lblIPCTransWorkDate = (Label)e.Row.FindControl("lblIPCTransWorkDate");
                lblIPCTransID = (HyperLink)e.Row.FindControl("lblIPCTransID");
                lblAcctNo = (Label)e.Row.FindControl("lblAcctNo");
                lblSenderName = (Label)e.Row.FindControl("lblSenderName");
                lblReceverAccount = (Label)e.Row.FindControl("lblReceverAccount");
                lblReceiverName = (Label)e.Row.FindControl("lblReceiverName");
                lblAmount = (Label)e.Row.FindControl("lblAmount");
                lblCKType = (Label)e.Row.FindControl("lblCKType");
                //cbxSelect.Enabled = true;
                //cbxSelect.Attributes.Add("onclick", "HighLightCBX('" + e.Row.ClientID + "',this)");

                lblScheduleID.Text = drv["SCHEDULEID"].ToString();
                lblScheduleType.Text = drv["SCHEDULETYPE"].ToString();
                lblScheduleName.Text = drv["SCHEDULENAME"].ToString();
                lblDescription.Text = drv["DESCRIPTION"].ToString();
                lblUserCreate.Text = drv["USERCREATE"].ToString();
                lblCreateDate.Text = SmartPortal.Common.Utilities.Utility.IsDateTime2(drv["CREATEDATE"].ToString()).ToString("dd/MM/yyyy HH:mm");
                lblEndDate.Text = SmartPortal.Common.Utilities.Utility.IsDateTime2(drv["ENDDATE"].ToString()).ToString("dd/MM/yyyy HH:mm");
                lblIPCTransdate.Text = drv["IPCTRANSDATE"].ToString();
                lblIPCTransWorkDate.Text = drv["IPCTRANSWORKDATE"].ToString();

                lblIPCTransID.Text = drv["IPCTRANSID"].ToString();
                lblIPCTransID.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=272&tranid=" + drv["IPCTRANSID"].ToString());
                lblAcctNo.Text = drv["ACCTNO"].ToString();
                lblSenderName.Text = drv["SENDERNAME"].ToString();
                lblReceverAccount.Text = drv["RECEIVERACCOUNT"].ToString();
                lblReceiverName.Text = drv["RECEIVERNAME"].ToString();
                lblAmount.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(drv["AMOUNT"].ToString(), drv["CCYID"].ToString());
                lblCKType.Text = drv["IPCTRANCODEDESC"].ToString();

            }
        }
        catch
        {
        }
    }

    #region Bind Data
    void BindData()
    {
        try
        {
            DataSet ds = new DataSet();
            SmartPortal.IB.Schedule objSchedule = new SmartPortal.IB.Schedule();
            string errorCode = string.Empty;
            string errorDesc = string.Empty;
            ds = objSchedule.GetSchedule(txScheduleID.Text, txUserCreate.Text, txtUserApproved.Text, ddlScheduleType.SelectedValue, txtFromDate.Text, txtToDate.Text, ddlCKType.SelectedValue, ref errorCode, ref errorDesc);
            gvSchedulelog.DataSource = ds;
            gvSchedulelog.DataBind();
            if (ds.Tables[0].Rows.Count == 0)
            {
                litError.Text = "<p class='divDataNotFound'>" + Resources.labels.datanotfound + "</p>";
            }
        }
        catch
        {
        }
    }
    #endregion
}
