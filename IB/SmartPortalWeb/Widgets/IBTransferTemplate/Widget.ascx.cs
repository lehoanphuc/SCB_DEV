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
using SmartPortal.BLL;
using SmartPortal.Common.Utilities;
using SmartPortal.ExceptionCollection;
using SmartPortal.Model;


public partial class Widgets_IBTransferTemplate_Widget : WidgetBase
{
    string IPCERRORCODE;
    string IPCERRORDESC;
    DataSet dsReceiverListIn = new DataSet();
    DataSet dsIn = new DataSet();
    DataSet dsBAC = new DataSet();
    public static string Fee = "";
    public static string ExecNow = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            txtAmountTIB.Attributes.Add("onkeyup", "ntt('" + txtAmountTIB.ClientID + "','" + lblTextTIB.ClientID + "',event)");
            txtAmountBAC.Attributes.Add("onkeyup", "ntt('" + txtAmountBAC.ClientID + "','" + lblTextBAC.ClientID + "',event)");
            if (!IsPostBack)
            {
                pnConfirm.Visible = false;
                pnTIB.Visible = false;
                pnTBAC.Visible = false;
                pnResultTransaction.Visible = false;
                txtRecieverAccountTIB.Enabled = true;
                SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();

                DataSet dsTranApp = new SmartPortal.IB.Schedule().LoadTransferType(Utility.KillSqlInjection(SmartPortal.Constant.IPC.ISTEMPLATE), Utility.KillSqlInjection(SmartPortal.Constant.IPC.YES), ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE != "0")
                {
                    throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
                }
                DataTable dtTranApp = new DataTable();
                dtTranApp = dsTranApp.Tables[0];
                if (dtTranApp.Rows.Count != 0)
                {
                    ddlTransferType.DataSource = dtTranApp;
                    ddlTransferType.DataTextField = "PAGETITLE";
                    ddlTransferType.DataValueField = "TRANCODE";
                    ddlTransferType.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btnTIBNext_Click(object sender, EventArgs e)
    {
        try
        {
            SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();
            bool sameAcct = false;
            switch (ddlTransferType.SelectedValue)
            {
                case SmartPortal.Constant.IPC.TIB:
                    PanelCFBAC.Visible = false;
                    PanelCFTIB.Visible = true;
                    lbnoidung.Text = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtDescTIB.Text);
                    lbtenmau.Text = txtTemplateName.Text;
                    lbhinhthuc.Text = ddlTransferType.SelectedItem.Text;
                    lbsotien.Text = SmartPortal.Common.Utilities.Utility.FormatMoneyInputToView(txtAmountTIB.Text, lbCCYID.Text.Trim());

                    sameAcct = objAcct.CheckSameAccount(ddlSenderAccountTIB.Text.ToString(), txtRecieverAccountTIB.Text.Trim().ToString());
                    if (!sameAcct)
                    {
                        lblError.Text = Resources.labels.duplicateaccount;
                        return;
                    }

                    Hashtable hasReceiverTIB = objAcct.GetInfoAccountCredit(txtRecieverAccountTIB.Text.Trim(), ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE.Equals("0"))
                    {
                        try
                        {
                            lbTKD_TIB.Text = txtRecieverAccountTIB.Text.Trim();
                            lbNN_TIB.Text = hasReceiverTIB[SmartPortal.Constant.IPC.FULLNAME].ToString();
                        }
                        catch
                        {
                            lblError.Text = Resources.labels.creditacccountinvalid;
                            return;
                        }
                    }
                    else
                    {
                        lblError.Text = Resources.labels.creditacccountinvalid;
                        return;
                    }

                    Hashtable hasSender = objAcct.GetInfoAccount(Session["userID"].ToString(), ddlSenderAccountTIB.Text.Trim(), ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE.Equals("0"))
                    {
                        try
                        {
                            lbTKCD.Text = ddlSenderAccountTIB.Text.Trim();
                            lbNCD.Text = hasSender[SmartPortal.Constant.IPC.FULLNAME].ToString();
                            lbCCYID.Text = hasSender[SmartPortal.Constant.IPC.CCYID].ToString();
                        }
                        catch
                        {
                            lblError.Text = Resources.labels.debitacccountinvalid;
                            return;
                        }
                    }
                    else
                    {
                        lblError.Text = Resources.labels.debitacccountinvalid;
                        return;
                    }
                    break;
                case SmartPortal.Constant.IPC.BAC:
                    PanelCFBAC.Visible = true;
                    PanelCFTIB.Visible = false;
                    lbnoidung.Text = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtDescBAC.Text);
                    lbtenmau.Text = txtTemplateName.Text;
                    lbhinhthuc.Text = ddlTransferType.SelectedItem.Text;
                    lbsotien.Text = SmartPortal.Common.Utilities.Utility.FormatMoneyInputToView(txtAmountBAC.Text, lbCCYID.Text.Trim());

                    sameAcct = objAcct.CheckSameAccount(ddlSenderAccountBAC.Text.ToString(), ddlReceiverAccountBAC.Text.Trim().ToString());
                    if (!sameAcct)
                    {
                        lblError.Text = Resources.labels.duplicateaccount;
                        return;
                    }

                    Hashtable hasSenderBAC = objAcct.GetInfoAccount(Session["userID"].ToString(), ddlSenderAccountBAC.Text.Trim(),ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE.Equals("0"))
                    {
                        try
                        {
                            lbTKCD.Text = ddlSenderAccountBAC.SelectedItem.ToString().Trim();
                            lbNCD.Text = hasSenderBAC[SmartPortal.Constant.IPC.FULLNAME].ToString();
                            lbCCYID.Text = hasSenderBAC[SmartPortal.Constant.IPC.CCYID].ToString();
                        }
                        catch
                        {
                            lblError.Text = Resources.labels.debitacccountinvalid;
                            return;
                        }
                    }
                    else
                    {
                        lblError.Text = Resources.labels.debitacccountinvalid;
                        return;
                    }

                    Hashtable hasReceiverBAC = objAcct.GetInfoAccountCredit(ddlReceiverAccountBAC.Text.Trim(), ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE.Equals("0"))
                    {
                        try
                        {
                            lbTKD_BAC.Text = ddlReceiverAccountBAC.Text.Trim();
                            lbNN_BAC.Text = hasReceiverBAC[SmartPortal.Constant.IPC.FULLNAME].ToString();
                        }
                        catch
                        {
                            lblError.Text = Resources.labels.creditacccountinvalid;
                            return;
                        }
                    }
                    else
                    {
                        lblError.Text = Resources.labels.creditacccountinvalid;
                        return;
                    }
                    break;
            }
            pnTIB.Visible = false;
            pnTBAC.Visible = false;
            pnResultTransaction.Visible = false;
            pnInfomation.Visible = false;
            pnConfirm.Visible = true;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            string TEMPLATENAME = SmartPortal.Common.Utilities.Utility.KillSqlInjection(lbtenmau.Text.Trim());
            string DESCRIPTION = SmartPortal.Common.Utilities.Utility.KillSqlInjection(lbnoidung.Text.Trim());
            string SENDERNAME = SmartPortal.Common.Utilities.Utility.KillSqlInjection(lbNCD.Text.Trim());
            string SENDERACCOUNT = SmartPortal.Common.Utilities.Utility.KillSqlInjection(lbTKCD.Text.Trim());
            string AMOUNT = SmartPortal.Common.Utilities.Utility.FormatMoneyInput(lbsotien.Text.Trim(), lbCCYID.Text.Trim());
            string CCYID = SmartPortal.Common.Utilities.Utility.KillSqlInjection(lbCCYID.Text.Trim());
            string IPCTRANCODE = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlTransferType.SelectedValue.ToString());
            string USERID = SmartPortal.Common.Utilities.Utility.KillSqlInjection(Session["userID"].ToString());
            string EXECNOW = string.Empty;
            string EXECDATE = string.Empty;
            string CHARGEFEE = Fee;
            string CITYCODE = string.Empty;
            string COUNTRYCODE = string.Empty;
            string IDENTIFYNO = string.Empty;
            string ISSUEDATE = string.Empty;
            string ISSUEPLACE = string.Empty;
            string BANKCODE = string.Empty;
            string BRANCHID = string.Empty;
            string RECEIVERACCOUNT = string.Empty;
            string RECEIVERNAME = string.Empty;
            string BRANCHDESC = string.Empty;
            switch (ddlTransferType.SelectedValue)
            {
                case SmartPortal.Constant.IPC.TIB:
                    RECEIVERACCOUNT = SmartPortal.Common.Utilities.Utility.KillSqlInjection(lbTKD_TIB.Text.Trim());
                    RECEIVERNAME = SmartPortal.Common.Utilities.Utility.KillSqlInjection(lbNN_TIB.Text.Trim());
                    EXECNOW = ExecNow;
                    new SmartPortal.IB.Transfer().InsertTransferTemplate(TEMPLATENAME, DESCRIPTION, SENDERACCOUNT, RECEIVERACCOUNT, AMOUNT, CCYID, IPCTRANCODE, USERID, EXECNOW, EXECDATE, CHARGEFEE, CITYCODE, COUNTRYCODE, IDENTIFYNO, ISSUEDATE, ISSUEPLACE, RECEIVERNAME, SENDERNAME, BANKCODE, string.Empty, BRANCHID, BRANCHDESC, ddlReceiverNameTIB.SelectedValue, ref IPCERRORCODE, ref IPCERRORDESC);
                    break;
                case SmartPortal.Constant.IPC.BAC:
                    RECEIVERACCOUNT = SmartPortal.Common.Utilities.Utility.KillSqlInjection(lbTKD_BAC.Text.Trim());
                    RECEIVERNAME = SmartPortal.Common.Utilities.Utility.KillSqlInjection(lbNN_BAC.Text.Trim());
                    EXECNOW = ExecNow;
                    new SmartPortal.IB.Transfer().InsertTransferTemplate(TEMPLATENAME, DESCRIPTION, SENDERACCOUNT, RECEIVERACCOUNT, AMOUNT, CCYID, IPCTRANCODE, USERID, EXECNOW, EXECDATE, CHARGEFEE, CITYCODE, COUNTRYCODE, IDENTIFYNO, ISSUEDATE, ISSUEPLACE, RECEIVERNAME, SENDERNAME, BANKCODE, string.Empty, BRANCHID, BRANCHDESC, "", ref IPCERRORCODE, ref IPCERRORDESC);
                    break;
            }
            if (IPCERRORCODE == "0")
            {
                pnConfirm.Visible = false;
                pnTIB.Visible = false;
                pnTBAC.Visible = false;
                pnResultTransaction.Visible = true;
                pnInfomation.Visible = false;
            }
            else
            {
                if (IPCERRORDESC == "110211")
                {
                    throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.SAMENAMETRANSFERTEMPLATE);

                }
                else
                {
                    throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_IBTransferTemplate_Widget", "Button2_Click", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlTransferType.SelectedValue == SmartPortal.Constant.IPC.TIB)
            {
                pnConfirm.Visible = false;
                pnTIB.Visible = true;
                pnTBAC.Visible = false;
                pnResultTransaction.Visible = false;
                pnInfomation.Visible = false;
            }
            if (ddlTransferType.SelectedValue == SmartPortal.Constant.IPC.BAC)
            {
                pnConfirm.Visible = false;
                pnTIB.Visible = false;
                pnTBAC.Visible = true;
                pnResultTransaction.Visible = false;
                pnInfomation.Visible = false;
            }
        }
        catch
        {
        }
    }
    protected void Button9_Click(object sender, EventArgs e)
    {
        try
        {
            pnConfirm.Visible = false;
            pnTIB.Visible = false;
            pnTBAC.Visible = false;
            pnResultTransaction.Visible = false;
            pnInfomation.Visible = true;
        }
        catch
        {
        }
    }
    protected void Button7_Click(object sender, EventArgs e)
    {
        try
        {
            #region check same name template transfer
            new SmartPortal.IB.Transfer().CheckNameTransferTemplate(Utility.KillSqlInjection(txtTemplateName.Text.Trim()), Session["userID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();
                if (ddlTransferType.SelectedValue == SmartPortal.Constant.IPC.TIB)
                {
                    dsReceiverListIn = objAcct.GetReceiverList(Session["userID"].ToString(), SmartPortal.Constant.IPC.TIB);

                    string isSendReceiver = dsReceiverListIn.Tables[1].Rows[0]["ISRECEIVERLIST"].ToString();
                    if (isSendReceiver.Equals("N"))
                    {
                        DataRow row = dsReceiverListIn.Tables[0].NewRow();
                        row["ID"] = "";
                        row["RECEIVERNAME"] = Resources.labels.khac;
                        dsReceiverListIn.Tables[0].Rows.InsertAt(row, 0);
                    }
                    else
                    {
                        if (dsReceiverListIn == null || dsReceiverListIn.Tables.Count == 0 || dsReceiverListIn.Tables[0].Rows.Count == 0)
                        {
                            throw new IPCException("4012");

                        }
                    }
                    ddlReceiverNameTIB.DataSource = dsReceiverListIn;
                    ddlReceiverNameTIB.DataTextField = SmartPortal.Constant.IPC.RECEIVERNAME;
                    ddlReceiverNameTIB.DataValueField = SmartPortal.Constant.IPC.ID;
                    ddlReceiverNameTIB.DataBind();
                    ddlReceiverNameTIB_SelectedIndexChanged(sender, e);

                    dsIn = objAcct.getAccount(Session["userID"].ToString(), SmartPortal.Constant.IPC.TIB, "", ref IPCERRORCODE, ref IPCERRORDESC);
                    dsIn.Tables[0].DefaultView.RowFilter = "accttype in ('DD', 'CD') and status not in ('CLS','S','M','V')";

                    if (dsIn == null || dsIn.Tables.Count == 0 || dsIn.Tables[0].Rows.Count == 0)
                    {
                        throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.AccountNotRegisted);
                    }
                    else
                    {
                        dsIn.Tables[0].DefaultView.RowFilter = "ACCTTYPE <> 'WL'";
                        ddlSenderAccountTIB.DataSource = dsIn.Tables[0].DefaultView;
                        ddlSenderAccountTIB.DataTextField = SmartPortal.Constant.IPC.ACCTNO;
                        ddlSenderAccountTIB.DataValueField = SmartPortal.Constant.IPC.ACCTNO;
                        ddlSenderAccountTIB.DataBind();

                        LayCCYID(ddlSenderAccountTIB.SelectedValue, lblCurrencyTIB);

                        pnConfirm.Visible = false;
                        pnTIB.Visible = true;
                        pnTBAC.Visible = false;
                        pnResultTransaction.Visible = false;
                        pnInfomation.Visible = false;
                    }
                }

                if (ddlTransferType.SelectedValue == SmartPortal.Constant.IPC.BAC)
                {
                    dsBAC = objAcct.getAccount(Session["userID"].ToString(), SmartPortal.Constant.IPC.BAC, "", ref IPCERRORCODE, ref IPCERRORDESC);
                    if (dsBAC == null || dsBAC.Tables.Count == 0 || dsBAC.Tables[0].Rows.Count == 0)
                    {
                        throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.AccountNotRegisted);
                    }
                    else
                    {
                        dsBAC.Tables[0].DefaultView.RowFilter = "ACCTTYPE <> 'WL'";
                        ddlSenderAccountBAC.DataSource = dsBAC.Tables[0].DefaultView;
                        ddlSenderAccountBAC.DataTextField = SmartPortal.Constant.IPC.ACCTNO;
                        ddlSenderAccountBAC.DataValueField = SmartPortal.Constant.IPC.ACCTNO;
                        ddlSenderAccountBAC.DataBind();

                        ddlReceiverAccountBAC.DataSource = dsBAC.Tables[0].DefaultView;
                        ddlReceiverAccountBAC.DataTextField = SmartPortal.Constant.IPC.ACCTNO;
                        ddlReceiverAccountBAC.DataValueField = SmartPortal.Constant.IPC.ACCTNO;
                        ddlReceiverAccountBAC.DataBind();

                        LayCCYID(ddlSenderAccountBAC.SelectedValue, lbCCYIDBAC);

                        pnConfirm.Visible = false;
                        pnTIB.Visible = false;
                        pnTBAC.Visible = true;
                        pnResultTransaction.Visible = false;
                        pnInfomation.Visible = false;
                    }
                }
            }
            else
            {
                if (IPCERRORDESC == "110211")
                {
                    ErrorCodeModel EM = new ErrorCodeModel();
                    EM = new ErrorBLL().Load(Utility.IsInt(SmartPortal.Constant.IPC.ERRORCODE.SAMENAMETRANSFERTEMPLATE), System.Globalization.CultureInfo.CurrentCulture.ToString());
                    lblError.Text = EM.ErrorDesc;
                }
                else
                {
                    lblError.Text = IPCERRORDESC;
                }
            }
            #endregion
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btnTIBNext0_Click(object sender, EventArgs e)
    {
        try
        {
            pnConfirm.Visible = false;
            pnTIB.Visible = false;
            pnTBAC.Visible = false;
            pnResultTransaction.Visible = false;
            pnInfomation.Visible = true;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void Button8_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }
    protected void ddlReceiverNameTIB_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlReceiverNameTIB.SelectedValue.ToString() != "")
            {
                SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();
                DataSet dsRCTIB = new DataSet();
                dsRCTIB = objAcct.GetReceiverList(Session["userID"].ToString(), SmartPortal.Constant.IPC.TIB);
                DataRow[] dr = dsRCTIB.Tables[0].Select("ID = '" + ddlReceiverNameTIB.SelectedValue.ToString() + "'");
                if (dr.Length > 0 && !dr[0]["ID"].Equals(""))
                {
                    txtRecieverAccountTIB.Text = dr[0][SmartPortal.Constant.IPC.ACCTNO].ToString();
                    txtRecieverAccountTIB.Enabled = false;
                }
                else
                {
                    throw new Exception();
                }
            }
            else
            {
                txtRecieverAccountTIB.Text = "";
                txtRecieverAccountTIB.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }
    protected void ddlSenderAccountTIB_SelectedIndexChanged(object sender, EventArgs e)
    {
        LayCCYID(ddlSenderAccountTIB.SelectedValue, lblCurrencyTIB);
    }
    public void LayCCYID(string acctno, Label result)
    {
        try
        {
            DataTable tblAcctnoInfo = new SmartPortal.IB.Account().GetAcctnoInfo(acctno, Session["Userid"].ToString());
            if (tblAcctnoInfo.Rows.Count != 0)
            {
                result.Text = tblAcctnoInfo.Rows[0]["CCYID"].ToString();
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void ddlSenderAccountBAC_SelectedIndexChanged(object sender, EventArgs e)
    {
        LayCCYID(ddlSenderAccountBAC.SelectedValue, lbCCYIDBAC);
    }
}
