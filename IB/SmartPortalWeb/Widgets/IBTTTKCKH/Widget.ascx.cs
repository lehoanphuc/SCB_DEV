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

using SmartPortal.ExceptionCollection;
using System.Globalization;


public partial class Widgets_IBTTTKCKH_Widget : WidgetBase
{
    string IPCERRORCODE;
    string IPCERRORDESC;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string ccyid = "LAK";
            lblTextError.Text = "";

            if (!IsPostBack)
            {
                pnConfirm.Visible = false;
                pnOTP.Visible = false;
                pnResultTransaction.Visible = false;
                pnTIB.Visible = true;

                #region load tai khoan tiet kiem
                DataTable tblFDOnline = new SmartPortal.IB.FD().LoadFDAccount(Session["userID"].ToString());
                if (tblFDOnline.Rows.Count > 0)
                {
                    ddlFDAccount.DataSource = tblFDOnline;
                    ddlFDAccount.DataTextField = "ACCOUNT";
                    ddlFDAccount.DataValueField = "ACCOUNT";
                    ddlFDAccount.DataBind();
                }
                else
                {
                    throw new IPCException("3996");
                }
                #endregion

                #region lay thong tin so du fd
                string acct = ddlFDAccount.SelectedItem.Value;
                string ErrorCode = string.Empty;
                string ErrorDesc = string.Empty;
                SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();
                DataSet dstk = new DataSet();
                dstk = objAcct.GetFDAcctInfo(Session["userID"].ToString(), acct, ref ErrorCode, ref ErrorDesc);
                if (dstk.Tables[0].Rows.Count > 0)
                {
                    lblCCYIDTK.Text = dstk.Tables[0].Rows[0][SmartPortal.Constant.IPC.CCYID].ToString().Trim();
                    lblBalanceTK.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(dstk.Tables[0].Rows[0][SmartPortal.Constant.IPC.AVAILABLEBALANCE].ToString()), dstk.Tables[0].Rows[0][SmartPortal.Constant.IPC.CCYID].ToString().Trim());
                }
                #endregion

                #region load tai khoan thanh toan
                string errorcode = "";
                string errorDesc = "";
                DataSet ds = new DataSet();
                SmartPortal.IB.Account accountList = new SmartPortal.IB.Account();
                ds = accountList.getAccount(Session["userID"].ToString(), "IB000201", "DD", ref errorcode, ref errorDesc);
                if (ds.Tables[0].Rows.Count > 0)
                {

                    ddlDDAccount.DataSource = ds;
                    ddlDDAccount.DataValueField = ds.Tables[0].Columns["ACCTNO"].ColumnName.ToString();
                    ddlDDAccount.DataBind();

                   
                }
                else
                {
                    throw new IPCException("3995");
                }
                #endregion

                #region load so du
                SmartPortal.IB.Account objAcct1 = new SmartPortal.IB.Account();
                Hashtable hasSender = objAcct1.loadInfobyAcct(ddlDDAccount.SelectedValue.Trim());
                if (hasSender[SmartPortal.Constant.IPC.IPCERRORCODE].Equals("0"))
                {

                    lblBalanceDD.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(hasSender[SmartPortal.Constant.IPC.AVAILABLEBALANCE].ToString()), hasSender[SmartPortal.Constant.IPC.CCYID].ToString());
                    lblCCYIDDD.Text = hasSender[SmartPortal.Constant.IPC.CCYID].ToString();

                }
                else
                {
                    throw new IPCException("4024");
                }
                #endregion
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


    protected void btnTIBNext_Click(object sender, EventArgs e)
    {
        try
        {
            pnConfirm.Visible = true;
            pnOTP.Visible = false;
            pnResultTransaction.Visible = false;
            pnTIB.Visible = false;

            //hien thi thong tin
            lblCFFDAccount.Text = ddlFDAccount.SelectedValue;

            string acct = ddlFDAccount.SelectedItem.Value;
            string ErrorCode = string.Empty;
            string ErrorDesc = string.Empty;
            SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();
            DataSet dstk = new DataSet();
            dstk = objAcct.GetFDAcctInfo(Session["userID"].ToString(), acct, ref ErrorCode, ref ErrorDesc);
           
            if (dstk.Tables[0].Rows.Count > 0)
            
            {

                lblCFBalanceFD.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(dstk.Tables[0].Rows[0][SmartPortal.Constant.IPC.AVAILABLEBALANCE].ToString()), dstk.Tables[0].Rows[0][SmartPortal.Constant.IPC.CCYID].ToString()) + " " + dstk.Tables[0].Rows[0][SmartPortal.Constant.IPC.CCYID].ToString();
                lblCFOpenDate.Text = SmartPortal.Common.Utilities.Utility.FormatDatetime(dstk.Tables[0].Rows[0][SmartPortal.Constant.IPC.OPENDATE].ToString(), "dd/MM/yyyy"); ;
                lblCFMD.Text = SmartPortal.Common.Utilities.Utility.FormatDatetime(dstk.Tables[0].Rows[0][SmartPortal.Constant.IPC.EXPIREDATE].ToString(), "dd/MM/yyyy"); ;

                lblCFCCYID.Text = dstk.Tables[0].Rows[0][SmartPortal.Constant.IPC.CCYID].ToString();
                lblCFACI.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(dstk.Tables[0].Rows[0][SmartPortal.Constant.IPC.ACCRUEDCRINTEREST].ToString()), dstk.Tables[0].Rows[0][SmartPortal.Constant.IPC.CCYID].ToString().Trim()) + " " + dstk.Tables[0].Rows[0][SmartPortal.Constant.IPC.CCYID].ToString();
                lblCFAccountName.Text = dstk.Tables[0].Rows[0]["ACCTNAME"].ToString();

                lblBranchID.Text = SmartPortal.Common.Utilities.Utility.FormatStringCore(dstk.Tables[0].Rows[0]["branch"].ToString());
                DataSet dsBranch = new SmartPortal.SEMS.Branch().GetBranchDetailsByID(SmartPortal.Common.Utilities.Utility.FormatStringCore(dstk.Tables[0].Rows[0]["branch"].ToString()), ref IPCERRORCODE, ref IPCERRORDESC);
                if (dsBranch.Tables.Count != 0)
                {
                    if (dsBranch.Tables[0].Rows.Count != 0)
                    {
                        lblCFBranch.Text = dsBranch.Tables[0].Rows[0]["BRANCHNAME"].ToString();
                    }
                }

                NumberStyles style;
                CultureInfo culture;
                double laisuat;

                style = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands;
                culture = CultureInfo.CreateSpecificCulture("en-US");

                double.TryParse(dstk.Tables[0].Rows[0][SmartPortal.Constant.IPC.INTERESTRATE].ToString(), style, culture, out laisuat);
                lblCFInterestRate.Text = (laisuat ).ToString(culture.NumberFormat);

                lblCFDDAcount.Text = ddlDDAccount.SelectedValue;
                lblCFBalanceDD.Text = lblBalanceDD.Text + " " + lblCCYIDDD.Text;
                lblCFDesc.Text =SmartPortal.Common.Utilities.Utility.KillSqlInjection( txtDesc.Text);

            }
            else
            {
                throw new IPCException("4024");
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
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            pnOTP.Visible = true;
            pnConfirm.Visible = false;            
            pnResultTransaction.Visible = false;
            pnTIB.Visible = false;

            SmartPortal.IB.Transactions objTran = new SmartPortal.IB.Transactions();
            DataTable dt = new DataTable();
            dt = objTran.LoadAuthenType(Session["userID"].ToString());
            ddlLoaiXacThuc.DataSource = dt;
            ddlLoaiXacThuc.DataTextField = "TYPENAME";
            ddlLoaiXacThuc.DataValueField = "AUTHENTYPE";
            ddlLoaiXacThuc.DataBind();
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
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?po=3&p=102"));
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        try
        {
            pnConfirm.Visible = true;          
            pnOTP.Visible = false;
            pnResultTransaction.Visible = false;
            pnTIB.Visible = false;
        }
        catch
        {
        }
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        try
        {

            UpdateAllAccount();

            //goi ham tat toan tai khoan
            Hashtable hasTK = new SmartPortal.IB.FD().CloseSavingOnline(Session["userID"].ToString(),lblCFFDAccount.Text, lblCFDDAcount.Text,Session["fullName"].ToString(),SmartPortal.Common.Utilities.Utility.FormatMoneyInput(lblBalanceTK.Text.Trim(),lblCCYIDTK.Text),lblCFDesc.Text,lblCFOpenDate.Text,lblCFMD.Text,lblCFInterestRate.Text,lblCFAccountName.Text,lblBranchID.Text, ddlLoaiXacThuc.SelectedValue, txtOTP.Text, ref IPCERRORCODE, ref IPCERRORDESC);

            if (IPCERRORCODE != "0")
            {
                txtOTP.Text="";
                switch (IPCERRORCODE)
                {
                    case SmartPortal.Constant.IPC.ERRORCODE.OTPINVALID:
                        lblTextError.Text = IPCERRORDESC ;
                        return;
                    case SmartPortal.Constant.IPC.ERRORCODE.WATTINGBANKAPPROVE:
                        lblTextError.Text = Resources.labels.wattingbankapprove;
                        //cap nhat trang thai tai khoan tiet kiem
                        new SmartPortal.IB.FD().UpdateIsClose(lblCFFDAccount.Text.Trim(), "Y");

                        break;
                    case SmartPortal.Constant.IPC.ERRORCODE.WATTINGUSERAPPROVE:
                        lblTextError.Text = Resources.labels.wattinguserapprove;

                        //cap nhat trang thai tai khoan tiet kiem
                        new SmartPortal.IB.FD().UpdateIsClose(lblCFFDAccount.Text.Trim(), "Y");

                        break;
                    case SmartPortal.Constant.IPC.ERRORCODE.NOTREGOTP:
                        lblTextError.Text = Resources.labels.notregotp;
                        return;
                    case SmartPortal.Constant.IPC.ERRORCODE.AUTHENTYPEINVALID:
                        lblTextError.Text = Resources.labels.authentypeinvalid;
                        return;
                    default:

                        lblTextError.Text = Resources.labels.loi + ": " + IPCERRORDESC;
                        return;
                }

            }

            //hien thi ket qua
            lblRFDAccount.Text = lblCFFDAccount.Text;
            lblRBalanceFD.Text = lblCFBalanceFD.Text;
            lblROpenDate.Text = lblCFOpenDate.Text;
            lblRMD.Text = lblCFMD.Text;
            lblRCCYID.Text = lblCFCCYID.Text;
            lblRInterestRate.Text = lblCFInterestRate.Text;
            lblRACI.Text = lblCFACI.Text;
            lblRDDAcount.Text = lblCFDDAcount.Text;
            lblRBalanceDD.Text = lblCFBalanceDD.Text;
            lblRD.Text = lblCFDesc.Text;
            lblRSAccountName.Text = lblCFAccountName.Text;
            lblRBranch.Text = lblCFBranch.Text;

            pnResultTransaction.Visible = true;
            pnConfirm.Visible = false;
            pnOTP.Visible = false;
            pnTIB.Visible = false;
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
    protected void ddlDDAccount_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
           

            #region load so du
            SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();
            Hashtable hasSender = objAcct.loadInfobyAcct(ddlDDAccount.SelectedValue.Trim());
            if (hasSender[SmartPortal.Constant.IPC.IPCERRORCODE].Equals("0"))
            {

                lblBalanceDD.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(hasSender[SmartPortal.Constant.IPC.AVAILABLEBALANCE].ToString()), hasSender[SmartPortal.Constant.IPC.CCYID].ToString());
                lblCCYIDDD.Text = hasSender[SmartPortal.Constant.IPC.CCYID].ToString();

            }
            else
            {
                throw new IPCException("4024");
            }
            #endregion
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
    protected void ddlFDAccount_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            #region lay thong tin so du fd
            string acct = ddlFDAccount.SelectedItem.Value;
            string ErrorCode = string.Empty;
            string ErrorDesc = string.Empty;
            SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();
            DataSet dstk = new DataSet();
            dstk = objAcct.GetFDAcctInfo(Session["userID"].ToString(), acct, ref ErrorCode, ref ErrorDesc);
            if (dstk.Tables[0].Rows.Count > 0)
            {
                lblCCYIDTK.Text = dstk.Tables[0].Rows[0][SmartPortal.Constant.IPC.CCYID].ToString().Trim();
                lblBalanceTK.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(dstk.Tables[0].Rows[0][SmartPortal.Constant.IPC.AVAILABLEBALANCE].ToString()), dstk.Tables[0].Rows[0][SmartPortal.Constant.IPC.CCYID].ToString().Trim());
            }
            #endregion

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
    private void UpdateAllAccount()
    {
        //Get TK tu DB
        string errorCode = string.Empty;
        string errorDesc = string.Empty;
        string AccountList = string.Empty;
        string CustCode = string.Empty;
        string CFType = string.Empty;
        DataSet dsAcctEB = new DataSet();
        DataSet dsAcctCore = new DataSet();
        DataSet dsCustInfo = new DataSet();
        SmartPortal.IB.Account acct = new SmartPortal.IB.Account();
        dsAcctEB = acct.getAccount(Session["userID"].ToString(), "IB000200", "", ref errorCode, ref errorDesc);
        DataRow[] dr;
        if (errorCode == "0")
        {
            dsCustInfo = acct.GetCustIDCustType(Session["userID"].ToString(), ref errorCode, ref errorDesc);
            if (errorCode == "0" && dsCustInfo.Tables[0].Rows.Count == 1)
            {
                CustCode = dsCustInfo.Tables[0].Rows[0]["CUSTCODE"].ToString().Replace(" ", "");
                CFType = dsCustInfo.Tables[0].Rows[0]["CFTYPE"].ToString().Replace(" ", "");
                for (int i = 0; i < dsAcctEB.Tables[0].Rows.Count; i++)
                {
                    if (i == dsAcctEB.Tables[0].Rows.Count - 1)
                        AccountList = AccountList + "'" + dsAcctEB.Tables[0].Rows[i]["ACCTNO"].ToString().Trim() + "'";
                    else
                        AccountList = AccountList + "'" + dsAcctEB.Tables[0].Rows[i]["ACCTNO"].ToString().Trim() + "',";
                }
                //dsAcctCore = acct.GetTKKH(CustCode.Replace(" ", ""), CFType.Replace(" ", ""), ref errorCode, ref errorDesc);
                //
                if (dsAcctCore != null && dsAcctCore.Tables.Count > 0)
                {
                    dr = dsAcctCore.Tables[0].Select("accountno not in (" + AccountList + ") and statuscd not in" + "('CLS')");
                    for (int j = 0; j < dr.Length; j++)
                    {
                        acct.InsertNewAcct(dr[j]["accountno"].ToString(), dr[j]["typeid"].ToString(), dr[j]["ccyid"].ToString()
                            , SmartPortal.Common.Utilities.Utility.FormatStringCore(dr[j]["branch"].ToString()), dr[j]["statuscd"].ToString(), CustCode
                            , CFType, ref errorCode, ref errorDesc);
                    }
                    dr = dsAcctCore.Tables[0].Select("statuscd in" + "('CLS')");
                    for (int k = 0; k < dr.Length; k++)
                    {
                        acct.UpdateCloseAcct(dr[k]["accountno"].ToString(), dr[k]["statuscd"].ToString(), ref errorCode, ref errorDesc);
                    }
                }
            }
        }
        //Get TK tu Core

        //So sanh
        //+ Them moi
        //+close
    }
}
