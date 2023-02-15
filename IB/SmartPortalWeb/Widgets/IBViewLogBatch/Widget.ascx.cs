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

public partial class Widgets_IBListTransWaitApprove_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //ltrError.Text = "";
            lblError.Text = "";

            if (!IsPostBack)
            {

                //ddlTransactionType.Items.Insert(0, new ListItem(Resources.labels.all,SmartPortal.Constant.IPC.ALL));
                BindData();
            }
        }
        catch
        {
        }
    }
    void BindData()
    {
        #region Xem giao dịch
        DataTable dtTran = new DataTable();
        DataSet dsTran = new SmartPortal.IB.Transactions().ViewLogBatch(Session["userID"].ToString(), SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtFromDate.Text.Trim()), SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtToDate.Text.Trim()), SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtAccno.Text.Trim()), SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtTranID.Text.Trim()), ddlStatus.SelectedValue, DDLAppSta.SelectedValue, ref IPCERRORCODE, ref IPCERRORDESC);
        //if (IPCERRORCODE == "0")
        //{
        //    dtTran = dsTran.Tables[0];

        //    if (dtTran.Rows.Count == 0)
        //    {
        //        ltrError.Text = "<p style='color:red;margin-left:10px; margin-top:20px;'>" + Resources.labels.khongtimthaydulieu + "</p>";
        //    }
        //}
        //else
        //{
        //}
        #endregion
        //hien len luoi
        rptLTWA.DataSource = dtTran;
        rptLTWA.DataBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        //Duyệt giao dịch LanNh
        string[] lst = hdId.Value.Split('#');
        List<string> lstTran = new List<string>();
        foreach (var item in lst)
        {
            if (item.Length != 0)
                lstTran.Add(item);
        }
        if (lstTran.Count > 0)
        {
            Session["tranID"] = lstTran;
            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?po=3&p=217"));
        }
        else
        {
            lblError.Text = Resources.labels.banvuilongchongiaodichcanduyet;
            lblError.Visible = true;
        }
    }

    //LanNh - suport bind source repeater
    protected static string GetStatus(object dataItem)
    {
        string result = string.Empty;
        string status = Convert.ToString(DataBinder.Eval(dataItem, "STATUS"));
        switch (status)
        {
            case SmartPortal.Constant.IPC.TRANSTATUS.BEGIN:
                result = Resources.labels.dangxuly;
                break;
            case SmartPortal.Constant.IPC.TRANSTATUS.FINISH:
                result = Resources.labels.hoanthanh;
                break;
            case SmartPortal.Constant.IPC.TRANSTATUS.WAITING_APPROVE:
                result = Resources.labels.choduyet;
                break;
            case SmartPortal.Constant.IPC.TRANSTATUS.ERROR:
                result = Resources.labels.loi;
                break;
        }
        return result;
    }
    protected static string GetResult(object dataItem)
    {
        string result = string.Empty;
        string status = Convert.ToString(DataBinder.Eval(dataItem, "STATUS"));
        string kq = Convert.ToString(DataBinder.Eval(dataItem, "APPRSTS"));
        switch (status)
        {
            case SmartPortal.Constant.IPC.TRANSTATUS.FINISH:
                switch (kq)
                {
                    case SmartPortal.Constant.IPC.APPROVESTATUS.APPROVED:
                        result = Resources.labels.duyet;
                        break;
                    case SmartPortal.Constant.IPC.APPROVESTATUS.REJECTED:
                        result = Resources.labels.huy;
                        break;
                    case SmartPortal.Constant.IPC.APPROVESTATUS.WAITTINGCUST:
                        result = Resources.labels.choduyet;
                        break;
                    case SmartPortal.Constant.IPC.APPROVESTATUS.BEGIN:
                        result = Resources.labels.dangxuly;
                        break;


                }
                break;
            case SmartPortal.Constant.IPC.TRANSTATUS.WAITING_APPROVE:
                switch (kq)
                {
                    case SmartPortal.Constant.IPC.APPROVESTATUS.APPROVED:
                        result = Resources.labels.duyet;
                        break;
                    case SmartPortal.Constant.IPC.APPROVESTATUS.REJECTED:
                        result = Resources.labels.huy;
                        break;
                    case SmartPortal.Constant.IPC.APPROVESTATUS.WAITTINGCUST:
                        result = Resources.labels.choduyet;
                        break;
                    case SmartPortal.Constant.IPC.APPROVESTATUS.BEGIN:
                        result = Resources.labels.dangxuly;
                        break;


                }
                break;
            case SmartPortal.Constant.IPC.TRANSTATUS.ERROR:
                switch (kq)
                {
                    case SmartPortal.Constant.IPC.APPROVESTATUS.APPROVED:
                        result = Resources.labels.duyet;
                        break;
                    case SmartPortal.Constant.IPC.APPROVESTATUS.REJECTED:
                        result = Resources.labels.huy;
                        break;
                    case SmartPortal.Constant.IPC.APPROVESTATUS.WAITTINGCUST:
                        result = Resources.labels.choduyet;
                        break;
                    case SmartPortal.Constant.IPC.APPROVESTATUS.BEGIN:
                        result = Resources.labels.dangxuly;
                        break;
                }
                break;

        }
        return result;
    }
}
