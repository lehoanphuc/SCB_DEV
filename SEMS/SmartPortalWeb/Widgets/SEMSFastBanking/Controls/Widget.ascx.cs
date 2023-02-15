using System;
using System.Collections;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SmartPortal.SEMS;
using SmartPortal.ExceptionCollection;

public partial class Widgets_SEMSFastBanking_Controls_Widget : System.Web.UI.UserControl
{
    string ACTION = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";

    public string ATMHeader
    {
        get
        {
            return lblATMHeader.Text;
        }

        set
        {
            lblATMHeader.Text = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        ACTION = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["a"].ToString().Trim();

        lblError.Text = "";

        if (!IsPostBack)
        {
            BindData();
        }
    }
    void BindData()
    {        
        try
        {

            switch (ACTION)
            {
                case "add":
                    break;
                case "edit":
                    DataSet ds = new SmartPortal.SEMS.FastBank().SelectShop("", SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["pid"].ToString(),
                        "", "","", ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE.Equals("0") && ds.Tables[0].Rows.Count != 0)
                    {
                        txtContractNo.Text = ds.Tables[0].Rows[0]["ContractNo"].ToString();
                        txtShopName.Text = ds.Tables[0].Rows[0]["ShopName"].ToString();
                        txtEmail.Text = ds.Tables[0].Rows[0]["Email"].ToString();
                        txtPhoneNo.Text = ds.Tables[0].Rows[0]["PhoneNo"].ToString();
                        txtShopCode.Text = ds.Tables[0].Rows[0]["ShopCode"].ToString();
                        txtShopAcctNo.Text = ds.Tables[0].Rows[0]["ShopAcctNo"].ToString();
                        txtSuspendAcctNo.Text = ds.Tables[0].Rows[0]["SuspendAcctNo"].ToString();
                        txtIncomAcctNo.Text = ds.Tables[0].Rows[0]["IncomAcctNo"].ToString();
                    }
                    break;
                case "delete":

                    break;
                default:
                    break;
            }
            

            #region Enable/Disable theo Action
            switch (ACTION)
            {
                case "add":
                    txtContractNo.Enabled = true;
                    txtShopName.Enabled = true;
                    txtEmail.Enabled = true;
                    txtPhoneNo.Enabled = true;
                    txtShopCode.Enabled = true;
                    txtShopAcctNo.Enabled = true;
                    txtSuspendAcctNo.Enabled = true;
                    txtIncomAcctNo.Enabled = true;
                    break;
                case "edit":
                    txtContractNo.Enabled = false;
                    txtShopName.Enabled = false;
                    txtEmail.Enabled = true;
                    txtPhoneNo.Enabled = true;
                    txtShopCode.Enabled = false;
                    txtShopAcctNo.Enabled = true;
                    txtSuspendAcctNo.Enabled = true;
                    txtIncomAcctNo.Enabled = true;                  
                    break;
            }
            #endregion

        }
        catch
        {
        }
    }
    protected void OnEnterContractNo(object sender, EventArgs e)
    {
        DataSet dt = new SmartPortal.SEMS.FastBank().GetContractInfo(SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtContractNo.Text.Trim()),ref IPCERRORCODE,ref IPCERRORDESC);

        if (IPCERRORCODE.Equals("0") && dt.Tables[0].Rows.Count > 0)
        {
            txtShopName.Text = dt.Tables[0].Rows[0]["FULLNAME"].ToString();
            txtEmail.Text = dt.Tables[0].Rows[0]["Email"].ToString();
            txtPhoneNo.Text = dt.Tables[0].Rows[0]["TEL"].ToString();
            txtShopCode.Text = SmartPortal.Common.Utilities.Utility.GetID("FB", txtContractNo.Text.Trim().Substring(2, 10), dt.Tables[0].Rows[0]["CFTYPE"].ToString().Trim(), 15);
            txtContractNo.Enabled = false;
        }
        else
        {
            lblError.Text = Resources.labels.saicontractno;
            txtContractNo.Text = "";
        }
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {
            string ContractNo = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtContractNo.Text.Trim());
            string ShopName = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtShopName.Text.Trim());
            string Email = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtEmail.Text.Trim());
            string PhoneNo = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtPhoneNo.Text.Trim());
            string ShopCode = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtShopCode.Text.Trim());
            string Password = SmartPortal.Common.Utilities.Utility.GenPassword(16);
            string AcctNo = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtShopAcctNo.Text.Trim());
            string SuspendAcctNo = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtSuspendAcctNo.Text.Trim());
            string IncomAcctNo = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtIncomAcctNo.Text.Trim());
            string SecretCode = SmartPortal.Common.Utilities.Utility.GenPassword(16);

            string HoldTime = "1";
            string HoldAmount = "1";
            string UserCreate = Session["username"].ToString();

            switch (ACTION)
            {
                case "add":

                    //check GL account - chua code

                    //check contract
                    DataSet dt = new SmartPortal.SEMS.FastBank().GetContractInfo(SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtContractNo.Text.Trim()),ref IPCERRORCODE,ref IPCERRORDESC);

                    if (IPCERRORCODE.Equals("0") && dt.Tables[0].Rows.Count == 0)
                    {
                        lblError.Text = Resources.labels.saicontractno;
                        txtContractNo.Enabled = true;
                    }

                    //insert
                    DataSet ds = new SmartPortal.SEMS.FastBank().InsertNewShop(ContractNo, ShopName, Email,PhoneNo, ShopCode, SmartPortal.Security.Encryption.Encrypt(Password), AcctNo,
                        SuspendAcctNo, IncomAcctNo, SmartPortal.Common.Encrypt.SHA256_Encrypt(SecretCode), HoldTime, HoldAmount, UserCreate,ref IPCERRORCODE,ref IPCERRORDESC);
                    if (IPCERRORCODE != "0")
                    {
                        switch (IPCERRORCODE)
                        {
                            case "4993":
                                lblError.Text = Resources.labels.contractdatontai;
                                break;
                            case "4992":
                                lblError.Text = Resources.labels.shopnamedatontai;
                                break;
                            case "4991":
                                lblError.Text = Resources.labels.shopcodedatontai;
                                break;
                            case "9999":
                                lblError.Text = "System error";
                                break;
                            default:
                                lblError.Text = Resources.labels.loi;
                                break;

                        }
                        
                        //throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.EXISTATMID);
                    }
                    else
                    {
                        lblError.Text = Resources.labels.themshopthanhcong;

                        btsave.Visible = false;

                        //ghi vo session dung in

                        Hashtable hasPrint = new Hashtable();
                        hasPrint.Add("ContractNo", ContractNo);
                        hasPrint.Add("ShopName", ShopName);
                        hasPrint.Add("Email", Email);
                        hasPrint.Add("PhoneNo", PhoneNo);
                        hasPrint.Add("ShopCode", ShopCode);
                        hasPrint.Add("Password", Password);
                        hasPrint.Add("AcctNo", AcctNo);
                        hasPrint.Add("SuspendAcctNo", SuspendAcctNo);
                        hasPrint.Add("IncomAcctNo", IncomAcctNo);
                        hasPrint.Add("SecretCode", SecretCode);
                        hasPrint.Add("ShopID", ds.Tables[0].Rows[0][0].ToString());
                        Session["print"] = hasPrint;

                        //send mail edit by VuTran
                        try
                        {
                            SmartPortal.Common.EmailHelper.AddShopSuccess_SendMail(hasPrint);
                        }
                        catch (Exception ex)
                        {
                            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString() + "Error when send email from TranserInBAC1", Request.Url.Query);
                            lblError.Text = "Add shop success but can't send mail!";
                        }
                    } 
                    break;
                case "edit":
                    DataSet dsedit = new SmartPortal.SEMS.FastBank().UpdateShop(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["pid"].ToString(), ShopName, Email, PhoneNo, ShopCode, AcctNo,
                        SuspendAcctNo, IncomAcctNo, HoldTime, HoldAmount, UserCreate,ref IPCERRORCODE,ref IPCERRORDESC);
                    if (IPCERRORCODE != "0")
                    {
                        switch (IPCERRORCODE)
                        {
                            case "4990":
                                lblError.Text = Resources.labels.shopkhongtontai;
                                break;
                            case "9999":
                                lblError.Text = "System error";
                                break;
                            default:
                                lblError.Text = Resources.labels.loi;
                                break;
                        }
                    }
                    else
                    {
                        lblError.Text = Resources.labels.suashopthanhcong;
                        btsave.Visible = false;
                    } 
                    break;        
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //LoadDistrict(ddlCity.SelectedItem.Value, "");
        }
        catch
        {
        }
    }
    protected void btback_Click(object sender, EventArgs e)
    {
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=1001"));
    }
}
