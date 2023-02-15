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
using SmartPortal.IB;

public partial class Widgets_IBCKVTKTKCKH_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblTextError.Text = "";
            string ccyid = "LAK";

            if (!IsPostBack)
            {
                txtAmount.Attributes.Add("onkeyup", "ntt('" + txtAmount.ClientID + "','" + lblText.ClientID + "',event)");
                //hide panel
                pnConfirm.Visible = false;
                pnOTP.Visible = false;
                pnResultTransaction.Visible = false;

                #region load thong tin chi nhanh
                ddlBranch.DataSource = new SmartPortal.SEMS.Branch().GetBranchForSavingOnline();
                ddlBranch.DataTextField = "BranchName";
                ddlBranch.DataValueField = "BranchID";
                ddlBranch.DataBind();
                ddlBranch.Items.Insert(0, new ListItem(Resources.labels.chonchinhanhpgd, ""));
                #endregion

                #region load thong tin san pham tiet kiem
                DataTable tblFD = new DataTable();
                tblFD = new SmartPortal.IB.FD().LoadFDProduct("");

                ddlFDProduct.DataSource = tblFD;
                ddlFDProduct.DataTextField = "FDPRODUCTNAME";
                ddlFDProduct.DataValueField = "ID";
                ddlFDProduct.DataBind();

                DataTable tbldetail = new SmartPortal.IB.FD().LoadFDProduct(ddlFDProduct.SelectedValue);
                if (tbldetail.Rows.Count > 0)
                {
                    //lblLaiSuat.Text=tbldetail.Rows[0]["INTERESTRATE"].ToString();
                    lblKyHan.Text = tbldetail.Rows[0]["TERM"].ToString();
                    ccyid = tbldetail.Rows[0]["CCYID"].ToString();
                }
                #endregion

                #region load tai khoan thanh toan
                string errorcode = "";
                string errorDesc = "";
                DataSet ds = new DataSet();
                SmartPortal.IB.Account accountList = new SmartPortal.IB.Account();
                ds = accountList.getAccountOnline(Session["userID"].ToString(), "IB000201", "DD", ccyid, ref errorcode, ref errorDesc);
                if (ds.Tables[0].Rows.Count > 0)
                {

                    ddlSenderAccount.DataSource = ds;
                    ddlSenderAccount.DataValueField = ds.Tables[0].Columns["ACCTNO"].ColumnName.ToString();
                    ddlSenderAccount.DataBind();

                    //ddlAccount1.DataSource = ds;
                    //ddlAccount1.DataValueField = ds.Tables[0].Columns["ACCTNO"].ColumnName.ToString();
                    //ddlAccount1.DataBind();

                    //ddlAccount2.DataSource = ds;
                    //ddlAccount2.DataValueField = ds.Tables[0].Columns["ACCTNO"].ColumnName.ToString();
                    //ddlAccount2.DataBind();
                }
                else
                {
                    throw new IPCException("3995");
                }
                #endregion
                #region load so du
                SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();
                Hashtable hasSender = objAcct.loadInfobyAcct(ddlSenderAccount.SelectedValue.Trim());
                if (hasSender[SmartPortal.Constant.IPC.IPCERRORCODE].Equals("0"))
                {

                    lblBalance.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(hasSender[SmartPortal.Constant.IPC.AVAILABLEBALANCE].ToString()), hasSender[SmartPortal.Constant.IPC.CCYID].ToString());
                    lblCCYID1.Text = hasSender[SmartPortal.Constant.IPC.CCYID].ToString();

                }
                else
                {
                    throw new IPCException("4024");
                }
                #endregion
                rad2.Text += " " + ddlSenderAccount.SelectedValue;
                //lblACCTNOTT.Text = ddlSenderAccount.SelectedValue;
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

            // check selct chi nhanh / pgd
            if (ddlBranch.SelectedIndex == 0)
            {
                lblTextError.Text = Resources.labels.vuilongchonchinhanhpgd;
                return;
            }
            // check MINAMOUNT
            DataTable dtMin = new DataTable();
            dtMin = new SmartPortal.IB.Transactions().CheckMinAmount(ddlFDProduct.SelectedValue.ToString().Trim(), SmartPortal.Common.Utilities.Utility.isDouble(txtAmount.Text.Trim(), true).ToString());

            if (dtMin.Rows[0]["ERRORCODE"].ToString() == "1")
            {
                lblTextError.Text = Resources.labels.sotienguitietkiemtoithieu + " " + SmartPortal.Common.Utilities.Utility.FormatMoney(dtMin.Rows[0]["MIN"].ToString(), lblCCYID1.Text.Trim()) + " " + lblCCYID1.Text.Trim();
                return;
            }
            // CHECK AMOUNT
            if (SmartPortal.Common.Utilities.Utility.isDouble(txtAmount.Text.Trim(), true) > SmartPortal.Common.Utilities.Utility.isDouble(lblBalance.Text.Trim(), true))
            {
                lblTextError.Text = Resources.labels.amountinvalid;
                return;
            }
            if (SmartPortal.Common.Utilities.Utility.isDouble(txtAmount.Text.Trim(), true) > SmartPortal.Common.Utilities.Utility.isDouble(System.Configuration.ConfigurationManager.AppSettings["limitopensave"].ToString().Trim(), true))
            {
                lblTextError.Text = Resources.labels.sotienguitietkiemkhongqua2ty;
                return;
            }

            //an panel
            pnConfirm.Visible = true;
            pnOTP.Visible = false;
            pnTIB.Visible = false;
            pnResultTransaction.Visible = false;



            //hien thi thong tin confirm
            lblConfirmFDProduct.Text = ddlFDProduct.SelectedItem.Text;
            lblCFKyHan.Text = lblKyHan.Text;
            lblCFTermDes.Text = lblTermDes.Text;
            //lblCFLaiSuat.Text = lblLaiSuat.Text;
            lblCFAccount.Text = ddlSenderAccount.SelectedValue;
            lblCFBalance.Text = lblBalance.Text;
            lblCFCCYID.Text = lblCCYID1.Text;
            lblAmount.Text = txtAmount.Text;
            lblCFCCYID1.Text = lblCCYID1.Text;
            lblCFChiNhanh.Text = ddlBranch.SelectedItem.Text;
            //if (rad1.Checked)
            //{
            //    lblDesc.Text = rad1.Text;
            //}
            //if (rad2.Checked)
            //{
            //    lblDesc.Text = rad2.Text + " " + ddlAccount1.SelectedValue;
            //}
            //if (rad3.Checked)
            //{
            //    lblDesc.Text = rad3.Text + " " + ddlAccount2.SelectedValue;
            //}
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
            //an panel
            pnConfirm.Visible = false;
            pnOTP.Visible = true;
            pnTIB.Visible = false;
            pnResultTransaction.Visible = false;

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
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_IBTransferInBAC1_Widget", "btnApply_Click", IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_IBTransferInBAC1_Widget", "btnApply_Click", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        try
        {
            pnConfirm.Visible = false;
            pnOTP.Visible = false;
            pnTIB.Visible = true;
            pnResultTransaction.Visible = false;
        }
        catch
        {
        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        try
        {
            pnConfirm.Visible = true;
            pnOTP.Visible = false;
            pnTIB.Visible = false;
            pnResultTransaction.Visible = false;
        }
        catch
        {
        }
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        try
        {
            string IPCERRORCODE = "";
            string IPCERRORDESC = "";


            string branch = "";

            //lay chi nhanh 
            SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();
            Hashtable hasSender = objAcct.loadInfobyAcct(lblCFAccount.Text.Trim());

            //    if (hasSender[SmartPortal.Constant.IPC.IPCERRORCODE].Equals("0"))
            //    {

            //        branch = hasSender[SmartPortal.Constant.IPC.BRANCHID].ToString();
            //        branch = SmartPortal.Common.Utilities.Utility.FormatStringCore(branch);
            //    }
            //    else
            //    {
            //        lblTextError.Text = Resources.labels.loi + ": " +hasSender[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
            //        return;
            //    }

            branch = ddlBranch.SelectedValue;
            //goi giao dich mo tai khoan tiet kiem
            Hashtable hasTK = new SmartPortal.IB.FD().OpenSavingOnline(Session["userID"].ToString(), lblCFAccount.Text, lblCFKyHan.Text, SmartPortal.Common.Utilities.Utility.FormatMoneyInput(lblAmount.Text, lblCFCCYID.Text), branch, Session["fullName"].ToString(), ddlFDProduct.SelectedValue, ddlLoaiXacThuc.SelectedValue, txtOTP.Text, ref IPCERRORCODE, ref IPCERRORDESC);

            switch (IPCERRORCODE)
            {
                case SmartPortal.Constant.IPC.ERRORCODE.OTPINVALID:
                    lblTextError.Text = IPCERRORDESC;
                    txtOTP.Text = "";
                    return;
                case SmartPortal.Constant.IPC.ERRORCODE.WATTINGBANKAPPROVE:
                    lblTextError.Text = Resources.labels.wattingbankapprove;
                    txtOTP.Text = "";
                    break;
                case SmartPortal.Constant.IPC.ERRORCODE.WATTINGUSERAPPROVE:
                    lblTextError.Text = Resources.labels.wattinguserapprove;
                    txtOTP.Text = "";
                    break;
                case SmartPortal.Constant.IPC.ERRORCODE.NOTREGOTP:
                    lblTextError.Text = Resources.labels.notregotp;
                    txtOTP.Text = "";
                    return;
                case SmartPortal.Constant.IPC.ERRORCODE.AUTHENTYPEINVALID:
                    lblTextError.Text = Resources.labels.authentypeinvalid;
                    txtOTP.Text = "";
                    return;

            }

            if (IPCERRORCODE == "0")
            {
                //luu tai khoan online
                if (rad1.Checked)
                {
                    new SmartPortal.IB.FD().SaveSAO(Session["userID"].ToString(), SmartPortal.Common.Utilities.Utility.FormatStringCore(hasTK["FDACCTNO"].ToString()), "", "");
                }
                else
                {
                    new SmartPortal.IB.FD().SaveSAO(Session["userID"].ToString(), SmartPortal.Common.Utilities.Utility.FormatStringCore(hasTK["FDACCTNO"].ToString()), lblCFAccount.Text, hasTK["EXPIREDATE"].ToString());
                }
                lblRLaiSuat.Text = (SmartPortal.Common.Utilities.Utility.isDouble(hasTK["INTERESTRATE"].ToString(), true)).ToString();
                lblOpenDate.Text = SmartPortal.Common.Utilities.Utility.IsDateTime2(hasTK["OPENDATE"].ToString()).ToString("dd/MM/yyyy");
                lblExpireDate.Text = SmartPortal.Common.Utilities.Utility.IsDateTime2(hasTK["EXPIREDATE"].ToString()).ToString("dd/MM/yyyy");
                lblAccountTK.Text = SmartPortal.Common.Utilities.Utility.FormatStringCore(hasTK["FDACCTNO"].ToString());
                lblFDBalance.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(hasTK["AVAIABLEBALANCE"].ToString()), lblCFCCYID.Text);

                //cap nhat thong so vao log cho giao dich mo TK
                new SmartPortal.IB.FD().UpdateLogOTK(hasTK["IPCTRANSID"].ToString(), lblOpenDate.Text, lblExpireDate.Text, lblRLaiSuat.Text);

            }
            else
            {
                txtOTP.Text = "";
                btnPrint.Visible = false;
                lblTextError.Text = Resources.labels.loimotaikhoantietkiemonline;
                //lblTextError.Text=Resources.labels.loi+": "+ IPCERRORDESC;
                return;
            }
            //hien thi ket qua
            lblRSanPham.Text = lblConfirmFDProduct.Text;
            lblRKyHan.Text = lblCFKyHan.Text;
            lblRTD.Text = lblCFTermDes.Text;

            lblRAccount.Text = lblCFAccount.Text;
            #region load so du
            SmartPortal.IB.Account objAcct2 = new SmartPortal.IB.Account();
            Hashtable hasSender1 = objAcct2.loadInfobyAcct(lblCFAccount.Text);
            if (hasSender1[SmartPortal.Constant.IPC.IPCERRORCODE].Equals("0"))
            {

                lblRBalance.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(hasSender1[SmartPortal.Constant.IPC.AVAILABLEBALANCE].ToString()), hasSender1[SmartPortal.Constant.IPC.CCYID].ToString());


            }
            else
            {
                throw new IPCException("4024");
            }
            #endregion

            lblRCCYID.Text = lblCFCCYID.Text;
            lblRCCYID1.Text = lblCFCCYID1.Text;
            lblRCCYID2.Text = lblCFCCYID1.Text;
            lblRAmount.Text = lblAmount.Text;
            lblRSChiNhanh.Text = lblCFChiNhanh.Text;
            //lblRDesc.Text = lblDesc.Text;

            //ghi vo session dung in

            Hashtable hasPrint = new Hashtable();
            hasPrint.Add("status", Resources.labels.thanhcong);
            hasPrint.Add("senderAccount", lblAccountTK.Text);
            hasPrint.Add("product", lblRSanPham.Text);
            hasPrint.Add("term", lblRKyHan.Text);
            hasPrint.Add("rate", lblRLaiSuat.Text);
            hasPrint.Add("openDate", lblOpenDate.Text);
            hasPrint.Add("expireDate", lblExpireDate.Text);
            hasPrint.Add("FDBalance", lblFDBalance.Text);

            hasPrint.Add("receiverAccount", lblRAccount.Text);
            hasPrint.Add("amount", lblRAmount.Text);
            hasPrint.Add("ccyid", lblRCCYID.Text);
            hasPrint.Add("amountchu", hidChu.Value.Replace("(", "").Replace(")", ""));

            hasPrint.Add("senderName", hasSender[SmartPortal.Constant.IPC.CUSTOMERNAME].ToString());

            hasPrint.Add("tranID", hasTK["IPCTRANSID"].ToString());
            hasPrint.Add("tranDate", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            hasPrint.Add("senderBranch", lblRSChiNhanh.Text);
            hasPrint.Add("receiverBranch", SmartPortal.Common.Utilities.Utility.FormatStringCore(hasSender[SmartPortal.Constant.IPC.BRANCHID].ToString()));
            Session["print"] = hasPrint;

            btnPrint.Visible = true;

            //a panel
            pnConfirm.Visible = false;
            pnOTP.Visible = false;
            pnTIB.Visible = false;
            pnResultTransaction.Visible = true;

            lblTextError.Text = Resources.labels.giaodichthanhcong;
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
    protected void ddlFDProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable tbldetail = new SmartPortal.IB.FD().LoadFDProduct(ddlFDProduct.SelectedValue);
            if (tbldetail.Rows.Count > 0)
            {
                //lblLaiSuat.Text = tbldetail.Rows[0]["INTERESTRATE"].ToString();
                lblKyHan.Text = tbldetail.Rows[0]["TERM"].ToString();
                lblTermDes.Text = tbldetail.Rows[0]["TERMDES"].ToString();
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
    protected void ddlSenderAccount_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            #region load so du
            SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();
            Hashtable hasSender = objAcct.loadInfobyAcct(ddlSenderAccount.SelectedValue.Trim());
            if (hasSender[SmartPortal.Constant.IPC.IPCERRORCODE].Equals("0"))
            {

                lblBalance.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(hasSender[SmartPortal.Constant.IPC.AVAILABLEBALANCE].ToString()), hasSender[SmartPortal.Constant.IPC.CCYID].ToString());
                lblCCYID1.Text = hasSender[SmartPortal.Constant.IPC.CCYID].ToString();

            }
            else
            {
                throw new IPCException("4024");
            }
            #endregion

            lblACCTNOTT.Text = ddlSenderAccount.SelectedValue;
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
    protected void Button6_Click(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?po=3&p=122"));
    }
    protected void btnPreview_Click(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?po=3&p=86"));
    }
}
