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

using SmartPortal.IB;
using SmartPortal.ExceptionCollection;
using SmartPortal.Common;
using SmartPortal.Common.Utilities;
using System.Collections.Generic;


public partial class Widgets_IBNTH_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                pnTIB.Visible = true;
                pnConfirm.Visible = false;
                pnNTHCNH.Visible = false;
                pnNTHNNH.Visible = false;
                pnResult.Visible = false;
                LoadTranferType();
                ddlTransferType_Changed(null, null);
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

    protected void ddlTransferType_Changed(object sender, EventArgs e)
    {
        try
        {
            if (ddlTransferType.SelectedValue.Trim().Equals("IB000206"))
            {
                pnNTHCNH.Visible = false;
                pnConfirm.Visible = false;
                pnNTHNNH.Visible = true;
                lbtemp.Text = SmartPortal.Constant.IPC.TOB;

                SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();
                //load city
                DataSet dsCity = new DataSet();
                dsCity = objAcct.GetCity();
                if (dsCity.Tables[0].Rows.Count == 0)
                {
                    throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.CITYNOTEXISTS);
                }

                ddlProvince.DataSource = dsCity;
                ddlProvince.DataTextField = dsCity.Tables[0].Columns[SmartPortal.Constant.IPC.CITYNAME].ColumnName.ToString();
                ddlProvince.DataValueField = dsCity.Tables[0].Columns[SmartPortal.Constant.IPC.CITYCODE].ColumnName.ToString();
                ddlProvince.DataBind();

                //load ngan hang
                DataTable dtBank = new DataTable();
                dtBank = new SmartPortal.IB.Bank().GetBank();
                if (dtBank.Rows.Count == 0)
                {
                    throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.BANKNOTEXISTS);
                }

                ddlBankRecieve.DataSource = dtBank;
                ddlBankRecieve.DataTextField = "BANKNAME";
                ddlBankRecieve.DataValueField = "BANKID";
                ddlBankRecieve.DataBind();

                ddlBankRecieve_SelectedIndexChanged(sender, e);
            }
            else
            {
                pnNTHCNH.Visible = true;
                pnConfirm.Visible = false;
                pnNTHNNH.Visible = false;
                lbtemp.Text = SmartPortal.Constant.IPC.TIB;
            }

            txtNTHAccount.Text = string.Empty;
            txtNTHAccount1.Text = string.Empty;
            //txtNTHCode.Text = string.Empty;
            //txtNTHCode1.Text = string.Empty;
            txtNTHDesc.Text = string.Empty;
            txtNTHDesc1.Text = string.Empty;
            txtNTHissuedate.Text = string.Empty;
            txtNTHIssuePlace.Text = string.Empty;
            txtNTHLicense.Text = string.Empty;
            txtNTHName.Text = string.Empty;
            txtNTHName1.Text = string.Empty;
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
            //Luu thong tin nguoi thu huong
            string errorCode = string.Empty;
            string errorDesc = string.Empty;
            Account objAcct = new Account();
            if (ddlTransferType.SelectedValue == "IB000206")
            {
                objAcct.InsertReceiverList(Session["userID"].ToString(), lblReceiverName.Text.Trim()
                    , lblAcctNo.Text.Trim(), ddlTransferType.SelectedValue.Trim(), "", "", "", lblDesc.Text.Trim(), SmartPortal.Constant.IPC.NEW, "", ddlBankRecieve.SelectedValue, ddlProvince.SelectedValue, ddlChildBank.SelectedValue, txtBranchDesc.Text, ref errorCode, ref errorDesc);
            }
            else
            {
                objAcct.InsertReceiverList(Session["userID"].ToString(), lblReceiverName.Text.Trim()
                   , lblAcctNo.Text.Trim(), ddlTransferType.SelectedValue.Trim(), "", "", "", lblDesc.Text.Trim(), SmartPortal.Constant.IPC.NEW, "", "", "", "", "", ref errorCode, ref errorDesc);

            }
            if (errorCode != "0")
            {
                lblError.Text = "Register infomation exists";
            }
            else
            {
                pnResult.Visible = true;
                pnConfirm.Visible = false;
                pnNTHCNH.Visible = false;
                pnNTHNNH.Visible = false;
                pnTIB.Visible = false;
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
    protected void Button3_Click(object sender, EventArgs e)
    {

        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?po=3&p=114"));
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        try
        {

        }
        catch
        {
        }
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        try
        {

        }
        catch
        {
        }
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtNTHName.Text != "")
            {
                divNext.Visible = false;
                pnTIB.Visible = false;
                pnNTHCNH.Visible = false;
                pnConfirm.Visible = true;
                pnNTHNNH.Visible = false;
                //Tk cung ngan hang
                lblTranferType.Text = ddlTransferType.SelectedItem.Text;
                //lblReceiverID.Text = txtNTHCode.Text;
                lblReceiverName.Text = txtNTHName.Text;
                lblAcctNo.Text = txtNTHAccount.Text;
                lblDesc.Text = txtNTHDesc.Text;
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
    protected void Button4_Click1(object sender, EventArgs e)
    {
        try
        {
            if (ddlTransferType.SelectedValue.Equals("IB000206"))
            {

            }
            else
            {
                if (string.IsNullOrEmpty(txtNTHAccount.Text.Trim()))
                {
                    lblError.Text = Resources.labels.taikhoandenkhongrong;
                    return;
                }
            }
            pnTIB.Visible = false;
            pnNTHCNH.Visible = false;
            pnConfirm.Visible = true;
            pnNTHNNH.Visible = false;
            // TK ngoai ngan hang
            lblTranferType.Text = ddlTransferType.SelectedItem.Text;
            //lblReceiverID.Text = txtNTHCode1.Text;

            lblReceiverName.Text = txtNTHName1.Text;
            lblDesc.Text = txtNTHDesc1.Text;
            //lblConfirmAddress.Text = txtAddress.Text;

            if (radAccount.Checked)
            {
                lblAcctNo.Text = txtNTHAccount1.Text;
                //lblBank.Text = ddlChildBank.SelectedItem.Text;

                //lblLicense.Text = "";
                //lblIssuePlace.Text = "";
                //lblIssueDate.Text = "";
            }
            else
            {
                lblAcctNo.Text = "";
                //lblBank.Text = "";

                //lblLicense.Text = txtNTHLicense.Text;
                //lblIssuePlace.Text = txtNTHIssuePlace.Text;
                //lblIssueDate.Text = txtNTHissuedate.Text;
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

    #region Load data
    private void LoadTranferType()
    {
        try
        {

            DataSet ds = new DataSet();
            string ErrorCode = string.Empty;
            string ErrorDesc = string.Empty;
            ds = new SmartPortal.IB.Schedule().LoadTransferType(Utility.KillSqlInjection(SmartPortal.Constant.IPC.ISTEMPLATE), Utility.KillSqlInjection(SmartPortal.Constant.IPC.YES), ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0" && ds.Tables.Count > 0)
            {
                ddlTransferType.DataSource = ds.Tables[0].DefaultView.ToTable();
                ddlTransferType.DataTextField = "PAGETITLE";
                ddlTransferType.DataValueField = "TRANCODE";
                ddlTransferType.DataBind();
            }
            else
                throw new IPCException(IPCERRORDESC);
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
    }
    #endregion
    protected void btbank2_Click(object sender, EventArgs e)
    {
        //co cmnd
        pnResult.Visible = false;
        pnConfirm.Visible = false;
        pnNTHCNH.Visible = false;
        pnNTHNNH.Visible = false;
        pnTIB.Visible = true;
    }
    protected void btback3_Click(object sender, EventArgs e)
    {
        //confirm

        switch (lbtemp.Text)
        {
            case SmartPortal.Constant.IPC.TIB:
                pnNTHCNH.Visible = true;
                pnNTHNNH.Visible = false;
                lblError.Text = string.Empty;
                break;
            case SmartPortal.Constant.IPC.TOB:
                pnNTHCNH.Visible = false;
                pnNTHNNH.Visible = true;
                break;
        }
        pnResult.Visible = false;
        pnConfirm.Visible = false;

        pnTIB.Visible = true;
        divNext.Visible = true;
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?po=3&p=113"));
    }
    protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtNTHissuedate.Attributes.Add("onclick", " var cal = Calendar.setup({onSelect: function(cal) { cal.hide() } }); cal.manageFields(" + txtNTHissuedate.ClientID + ", " + txtNTHissuedate.ClientID + ", '%d/%m/%Y'); ");

        DataSet dsBankList = new DataSet();
        dsBankList = new SmartPortal.IB.Account().GetBankList(ddlProvince.SelectedValue, ddlBankRecieve.SelectedValue);


        try
        {
            //DataRow[] dr = dsCity.Tables[0].Select(SmartPortal.Constant.IPC.CITYCODE + " = '" + ddlProvince.Text + "'");
            //if (dr.Length > 0)
            //{
            //DataRow[] drBank = dsBankList.Tables[0].Select(SmartPortal.Constant.IPC.CITYCODE + " = '" + dr[0][SmartPortal.Constant.IPC.CITYCODE] + "'");
            //foreach (DataRow row in drBank)
            //{
            //    temp.ImportRow(row);
            //}
            if (dsBankList.Tables[0].Rows.Count == 0)//|| temp.Columns[SmartPortal.Constant.IPC.BANKCODE].ColumnName.ToString() = null)
            {
                ddlChildBank.Items.Clear();
                ddlChildBank.Items.Insert(0, new ListItem(Resources.labels.chuacodanhsachnganhang, ""));
            }
            else
            {
                ddlChildBank.DataSource = dsBankList.Tables[0];
                ddlChildBank.DataTextField = dsBankList.Tables[0].Columns[SmartPortal.Constant.IPC.BANKNAME].ColumnName.ToString();
                ddlChildBank.DataValueField = dsBankList.Tables[0].Columns[SmartPortal.Constant.IPC.BANKCODE].ColumnName.ToString();
                ddlChildBank.DataBind();
            }

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
    protected void ddlBankRecieve_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtNTHissuedate.Attributes.Add("onclick", " var cal = Calendar.setup({onSelect: function(cal) { cal.hide() } }); cal.manageFields(" + txtNTHissuedate.ClientID + ", " + txtNTHissuedate.ClientID + ", '%d/%m/%Y'); ");

        DataSet dsBankList = new DataSet();
        dsBankList = new SmartPortal.IB.Account().GetBankList(ddlProvince.SelectedValue, ddlBankRecieve.SelectedValue);


        try
        {
            //DataRow[] dr = dsCity.Tables[0].Select(SmartPortal.Constant.IPC.CITYCODE + " = '" + ddlProvince.Text + "'");
            //if (dr.Length > 0)
            //{
            //DataRow[] drBank = dsBankList.Tables[0].Select(SmartPortal.Constant.IPC.CITYCODE + " = '" + dr[0][SmartPortal.Constant.IPC.CITYCODE] + "'");
            //foreach (DataRow row in drBank)
            //{
            //    temp.ImportRow(row);
            //}
            if (dsBankList.Tables[0].Rows.Count == 0)//|| temp.Columns[SmartPortal.Constant.IPC.BANKCODE].ColumnName.ToString() = null)
            {
                ddlChildBank.Items.Clear();
                ddlChildBank.Items.Insert(0, new ListItem(Resources.labels.chuacodanhsachnganhang, ""));
            }
            else
            {
                ddlChildBank.DataSource = dsBankList.Tables[0];
                ddlChildBank.DataTextField = dsBankList.Tables[0].Columns[SmartPortal.Constant.IPC.BANKNAME].ColumnName.ToString();
                ddlChildBank.DataValueField = dsBankList.Tables[0].Columns[SmartPortal.Constant.IPC.BANKCODE].ColumnName.ToString();
                ddlChildBank.DataBind();
            }

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
    protected void txtNTHAccount_TextChanged(object sender, EventArgs e)
    {
        try
        {
            SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();
            Hashtable htact = objAcct.GetInfoAccountCredit(txtNTHAccount.Text.Trim().ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
            if(IPCERRORCODE.Equals("0") && htact.Count > 0)
            {
                if (htact[SmartPortal.Constant.IPC.FULLNAME] != null)
                {
                    txtNTHName.Text = htact[SmartPortal.Constant.IPC.FULLNAME].ToString();
                    lberror2.Text = string.Empty;
                }
                else
                {
                    lberror2.Text = Resources.labels.destacccountinvalid;
                    txtNTHName.Text = string.Empty;
                    return;
                }
            }
            else
            {
                lberror2.Text = Resources.labels.destacccountinvalid;
                txtNTHName.Text = string.Empty;
                return;
            }
        }
        catch
        {
            lberror2.Text = Resources.labels.destacccountinvalid;
            txtNTHName.Text = string.Empty;
            return;
        }
    }
}
