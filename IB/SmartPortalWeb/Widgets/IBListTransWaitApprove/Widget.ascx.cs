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
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;
using SmartPortal.Common.Utilities;

public partial class Widgets_IBListTransWaitApprove_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            txtFromDate.Text = DateTime.Now.AddDays(-20).ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            ltrError.Text = "";
            lblError.Text = "";

            if (!IsPostBack)
            {
                Session["tranID"] = null;

                //load tran app
                string contractNo = string.Empty;
                DataSet dsContract = new SmartPortal.IB.Transactions().GetUserOfContractNoByUID(Utility.KillSqlInjection(Session["userID"].ToString()), "", ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE != "0")
                {
                    throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORCODE);
                }
                DataTable dtContract = new DataTable();
                dtContract = dsContract.Tables[0];
                if (dtContract.Rows.Count != 0)
                {
                    contractNo = dtContract.Rows[0]["CONTRACTNO"].ToString();
                }

                DataSet dsResult = new SmartPortal.SEMS.Product().GetTranNameByContractNo(Utility.KillSqlInjection(contractNo), ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE != "0")
                {
                    throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORCODE);
                }
                ddlTransactionType.DataSource = dsResult;
                ddlTransactionType.DataTextField = "PAGENAME";
                ddlTransactionType.DataValueField = "TRANCODE";
                ddlTransactionType.DataBind();
                ddlTransactionType.Items.Insert(0, new ListItem(Resources.labels.tatca, IPC.ALL));

                SmartPortal.IB.Account accountList = new SmartPortal.IB.Account();

                DataSet ds = accountList.GetInfoAcct(Session["userID"].ToString(), "IB0002021", "", ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE != "0")
                {
                    throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
                }
                ddlDebitAcct.DataSource = ds;
                ddlDebitAcct.DataTextField = "ACCTNO";
                ddlDebitAcct.DataValueField = "ACCTNO";
                ddlDebitAcct.DataBind();
                ddlDebitAcct.Items.Insert(0, new ListItem(Resources.labels.all, string.Empty));


                BindData();
            }
        }
        catch
        {
        }
    }
    void BindData()
    {
        #region Load các giao dịch chờ Approve
        DataTable dtTran = new DataTable();
        DataSet dsTran = new SmartPortal.IB.Transactions().LoadTranForApprove(Session["userID"].ToString(), SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtTranID.Text.Trim()), ddlTransactionType.SelectedValue, SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtFromDate.Text.Trim()), SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtToDate.Text.Trim()), "ALL", SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlDebitAcct.SelectedValue), "ALL", ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE == "0")
        {
            dtTran = dsTran.Tables[0];

            if (dtTran.Rows.Count == 0)
            {
                ltrError.Text = "<center><div style='text-align:center; padding-top:10px;padding-bottom:10px;font-weight:bold;color:red'>" + Resources.labels.gridNodata + "</div></center>";
            }
        }
        #endregion
        if (dtTran.Rows.Count > 0)
        {
            gvLTWA.DataSource = dtTran;
            gvLTWA.DataBind();
            gvLTWA.Visible = true;
        }
        else
            gvLTWA.Visible = false;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (txtFromDate.Text.Length > 0 && txtToDate.Text.Length > 0)
        {
            DateTime myDate = DateTime.ParseExact(txtFromDate.Text.ToString(), "dd/MM/yyyy", null);
            DateTime targetDate = DateTime.ParseExact(txtToDate.Text.ToString(), "dd/MM/yyyy", null);
            if (myDate > targetDate)
            {
                lblError.Text = Resources.labels.ngayketthucphailonhonngaybatdau;
                return;
            }
        }
        BindData();
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        //Duyệt giao dịch TUANTA
        CheckBox cbxSelect;
        LinkButton hpTranID;
        Label lblTranCode;
        List<string> lstTran = new List<string>();
        List<string> lstTranBatch = new List<string>();
        try
        {
            foreach (RepeaterItem item in gvLTWA.Items)
            {
                cbxSelect = (CheckBox)item.FindControl("cbxSelect");
                if (cbxSelect.Checked == true)
                {
                    hpTranID = (LinkButton)item.FindControl("hpTranID");
                    lblTranCode = (Label)item.FindControl("lblTranCode");
                    if (lblTranCode != null && lblTranCode.Text == "IB000499")
                        lstTranBatch.Add(hpTranID.Text);
                    else
                        lstTran.Add(hpTranID.Text.Trim());
                }
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }

        if (lstTran.Count != 0 || lstTranBatch.Count != 0)
        {
            if (lstTran.Count != 0)
            {
                Session["tranID"] = lstTran;
                if (lstTranBatch.Count != 0)
                    Session["tranIDBatch"] = lstTranBatch;
                Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?po=3&p=217&type=a"));
            }
            else
            {
                if (lstTranBatch.Count != 0)
                {
                    Session["tranIDBatch"] = lstTranBatch;
                    Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?po=3&p=278&type=a"));
                }
                else
                {
                    lblError.Text = Resources.labels.banvuilongchongiaodichcanduyet;
                    BindData();
                }
            }
        }
        else
        {
            lblError.Text = Resources.labels.banvuilongchongiaodichcanduyet;
            BindData();
        }
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        //Duyệt giao dịch TUANTA

        CheckBox cbxSelect;
        LinkButton hpTranID;
        Label lblTranCode;
        List<string> lstTran = new List<string>();
        List<string> lstTranBatch = new List<string>();
        try
        {
            foreach (RepeaterItem item in gvLTWA.Items)
            {
                cbxSelect = (CheckBox)item.FindControl("cbxSelect");
                if (cbxSelect.Checked == true)
                {
                    hpTranID = (LinkButton)item.FindControl("hpTranID");

                    //approve
                    //new SmartPortal.SEMS.Transactions().TellerApp(hpTranID.Text.Trim(), ref IPCERRORCODE, ref IPCERRORDESC);
                    lblTranCode = (Label)item.FindControl("lblTranCode");
                    //approve
                    //new SmartPortal.SEMS.Transactions().TellerApp(hpTranID.Text.Trim(), ref IPCERRORCODE, ref IPCERRORDESC);
                    if (lblTranCode != null && lblTranCode.Text == "IB000499")
                    {
                        lstTranBatch.Add(hpTranID.Text);
                    }
                    else
                    {
                        lstTran.Add(hpTranID.Text.Trim());
                    }
                    //lstTran.Add(hpTranID.Text.Trim());
                }
            }


        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
        if (lstTran.Count != 0 || lstTranBatch.Count != 0)
        {
            if (lstTran.Count != 0)
            {
                Session["tranID"] = lstTran;
                if (lstTranBatch.Count != 0)
                {
                    Session["tranIDBatch"] = lstTranBatch;
                    //Response.Redirect("~/Default.aspx?po=3&p=278&type=a");
                }
                Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?po=3&p=217&type=r"));
            }
            else
            {
                if (lstTranBatch.Count != 0)
                {
                    Session["tranIDBatch"] = lstTranBatch;
                    Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?po=3&p=278&type=r"));
                }
                else
                {
                    lblError.Text = Resources.labels.banvuilongchongiaodichcanhuy;
                    BindData();
                }
            }
            //if (ddlTransactionType.SelectedValue.Trim() == "IB000499")
            //{
            //    Response.Redirect("~/Default.aspx?po=3&p=278&type=a");
            //}
            //else
            //{
            //    Response.Redirect("~/Default.aspx?po=3&p=217&type=a");
            //}
        }
        else
        {
            lblError.Text = Resources.labels.banvuilongchongiaodichcanhuy;
            BindData();
        }

        //if (lstTran.Count != 0)
        //{
        //    Session["tranID"] = lstTran;
        //    if (ddlTransactionType.SelectedValue.Trim() == "IB000499")
        //    {
        //        Response.Redirect("~/Default.aspx?po=3&p=278&type=r");
        //    }
        //    else
        //    {
        //        Response.Redirect("~/Default.aspx?po=3&p=217&type=r");
        //    }
        //}
        //else
        //{
        //    lblError.Text = "Bạn vui lòng chọn giao dịch cần hủy";
        //    BindData();
        //}
    }
    protected static string GetLink(object dataItem)
    {
        string result = string.Empty;
        string code = Convert.ToString(DataBinder.Eval(dataItem, "IPCTRANCODE"));
        string id = Convert.ToString(DataBinder.Eval(dataItem, "IPCTRANSID"));
        if (code.Trim() == "IB000499")
        {
            result = "~/Default.aspx?po=3&p=278&tranid=" + id + "&br=" + id;
        }
        else
        {
            result = "~/Default.aspx?po=3&p=217&tranid=" + id;
        }
        return result;


    }
    protected static string GetStatus(object dataItem)
    {
        string result = string.Empty;
        string status = Convert.ToString(DataBinder.Eval(dataItem, "STATUS"));
        switch (status)
        {
            case SmartPortal.Constant.IPC.TRANSTATUS.BEGIN:
                result = Resources.labels.stsApprProcessing;// "Đang xử lý";
                break;
            case SmartPortal.Constant.IPC.TRANSTATUS.FINISH:
                result = Resources.labels.stsApprFinished;// "Hoàn thành";
                break;
            case SmartPortal.Constant.IPC.TRANSTATUS.WAITING_APPROVE:
                result = Resources.labels.stsApprWaitCust; //"Chờ duyệt";
                break;
            case SmartPortal.Constant.IPC.TRANSTATUS.ERROR:
                result = Resources.labels.stsApprError;// "Lỗi";
                break;

        }
        return result;
    }

    protected void gvLTWA_OnItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        string commandName = e.CommandName.ToString();
        string commandArg = e.CommandArgument.ToString();
        string[] lst = commandArg.Split('|');
        string ipctranid = lst[0].ToString();
        string ipctrancode = lst[1].ToString();
        if (ipctrancode.Trim() == "IB000499")
        {
            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?po=3&p=278&tranid=" + ipctranid + "&br=" + ipctranid));
        }
        else
        {
            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?po=3&p=217&tranid=" + ipctranid));
        }
    }
    protected void gvLTWA_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                DataRowView drv = e.Item.DataItem as DataRowView;
                RepeaterItem item = e.Item;
                CheckBox cbxSelect = (CheckBox)item.FindControl("cbxSelect");
                cbxSelect.Attributes.Add("onclick", "checkitem();");
                LinkButton hpTranID = (LinkButton)item.FindControl("hpTranID");
                Label lblDate = (Label)item.FindControl("lblDate");
                Label tranname = (Label)item.FindControl("tranname");
                Label lblAmount = (Label)item.FindControl("lblAmount");
                Label lblFeeAmount = (Label)item.FindControl("lblFeeAmount");
                Label lblCCYID = (Label)item.FindControl("lblCCYID");
                Label lblAccount = (Label)item.FindControl("lblAccount");
                Label lblDesc = (Label)item.FindControl("lblDesc");
                Label lblStatus = (Label)item.FindControl("lblStatus");
                Label lblTranCode = (Label)item.FindControl("lblTranCode");

                //if (drv["IPCTRANCODE"].ToString().Trim() == "IB000499")
                //{
                //    hpTranID.NavigateUrl = "~/Default.aspx?po=3&p=278&tranid=" + drv["IPCTRANSID"].ToString() + "&br=" + drv["IPCTRANSID"].ToString();
                //}
                //else
                //{
                //    hpTranID.NavigateUrl = "~/Default.aspx?po=3&p=217&tranid=" + drv["IPCTRANSID"].ToString();
                //}
                hpTranID.Text = drv["IPCTRANSID"].ToString();
                lblTranCode.Text = drv["IPCTRANCODE"].ToString();
                lblDesc.Text = drv["TRANDESC"].ToString();
                lblAccount.Text = drv["CHAR01"].ToString();
                lblAmount.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(drv["NUM01"].ToString(), drv["CCYID"].ToString().Trim());
                lblFeeAmount.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(drv["NUM02"].ToString(), drv["CCYID"].ToString().Trim());
                lblCCYID.Text = drv["CCYID"].ToString();
                tranname.Text = drv["tranname"].ToString();
                lblDate.Text = SmartPortal.Common.Utilities.Utility.IsDateTime2(drv["IPCTRANSDATE"].ToString()).ToString("dd/MM/yyyy HH:mm");

                switch (drv["STATUS"].ToString().Trim())
                {
                    case SmartPortal.Constant.IPC.TRANSTATUS.BEGIN:
                        lblStatus.Text = Resources.labels.stsApprProcessing; // "Đang xử lý";
                        break;
                    case SmartPortal.Constant.IPC.TRANSTATUS.FINISH:
                        lblStatus.Text = Resources.labels.stsApprFinished; // "Hoàn thành";
                        break;
                    case SmartPortal.Constant.IPC.TRANSTATUS.WAITING_APPROVE:
                        lblStatus.Text = Resources.labels.stsApprWaitCust; //"Chờ duyệt";
                        break;
                    case SmartPortal.Constant.IPC.TRANSTATUS.ERROR:
                        lblStatus.Text = Resources.labels.stsApprError; // "Lỗi";
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    protected void gvLTWA_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Header)
        {
            RepeaterItem item = e.Item;
            CheckBox cbxall = (CheckBox)item.FindControl("ckhAll");
            cbxall.Attributes.Add("onclick", "checkall(this);");
        }
    }
}
