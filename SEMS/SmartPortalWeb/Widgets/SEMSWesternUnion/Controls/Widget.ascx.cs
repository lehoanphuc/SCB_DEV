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
using System.Globalization;
using SmartPortal.Constant;
using System.IO;

public partial class Widgets_SEMSWesternUnion_Control_Widget : WidgetBase
{
    private string ACTION = string.Empty;
    string IPCERRORCODE = string.Empty;
    string IPCERRORDESC = string.Empty;
    private string sendtype = ""; 
    string ID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //txtExchange.Attributes.Add("onkeyup", "ntt('" + txtExchange.ClientID + "','',event)");
        sendtype = ddlsendtype.SelectedValue;
        ID = GetParamsPage(IPC.ID)[0].Trim();
        ACTION = GetActionPage();

        if (!IsPostBack)
        {
            try
            {
                BindData(sender,e);
            }
            catch (Exception ex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
            }

        }
    }

    void BindData(object sender, EventArgs e)
    {
        try
        {
            DataSet sb = new SmartPortal.SEMS.Transactions().GetDetailWestern(ID,ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                DataTable dt = new DataTable();
                dt = sb.Tables[0];
                txtCashcode.Text = dt.Rows[0]["CASHCODE"].ToString();
                
                lblExchangerate.Text = dt.Rows[0]["RCCYID"].ToString();
                lblex.Text = dt.Rows[0]["RCCYID"].ToString() + "/1 USD";

                lblSenderFirstName.Text = dt.Rows[0]["SENDERFIRSTNAME"].ToString();
                lblSenderLastName.Text = dt.Rows[0]["SENDERLASTNAME"].ToString();
                lbldebit.Text = dt.Rows[0]["SENDERACC"].ToString();
                lblSenderCCYID.Text = dt.Rows[0]["SCCYID"].ToString();
                lblPayOut.Text = dt.Rows[0]["PAYCOUNTRY"].ToString();
                lblAmountSend.Text = txttranfersamount.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(dt.Rows[0]["SENDERAMOUNT"].ToString(),"USD");
                lblFee.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(dt.Rows[0]["FEE"].ToString(),"USD");
                lblTotalAmount.Text = SmartPortal.Common.Utilities.Utility.FormatMoney((SmartPortal.Common.Utilities.Utility.isDouble(lblAmountSend.Text, true) + SmartPortal.Common.Utilities.Utility.isDouble(lblFee.Text, true)).ToString(), "USD");

                #region Sender detail
                lblTemUnit.Text = dt.Rows[0]["SENDERUNIT"].ToString();
                lblTemDistrict.Text = dt.Rows[0]["SENDERDISTRICT"].ToString();
                lblTemCity.Text = dt.Rows[0]["SENDERCITY"].ToString();
                lblTemProvince.Text = dt.Rows[0]["SENDERPROVINCE"].ToString();
                lblPostcode.Text = dt.Rows[0]["POSTCODE"].ToString();
                lblSenderCountry.Text = dt.Rows[0]["SENDERCOUNTRY"].ToString();
                lblCountryCode.Text = dt.Rows[0]["COUNTRYCODE"].ToString();
                lbltelephone.Text = dt.Rows[0]["TELEPHONE"].ToString();
                lblmobilecode.Text = dt.Rows[0]["MOBILEPHONECODE"].ToString();
                lblMobilephone.Text = dt.Rows[0]["PHONENO"].ToString();
                txtEmail.Text = dt.Rows[0]["EMAIL"].ToString();
                #endregion

                lbIDType.Text = dt.Rows[0]["ICTYPE"].ToString();
                lbIDNumber.Text = dt.Rows[0]["ICNUMBER"].ToString();
                lbCouuntryOfIssue.Text = dt.Rows[0]["ISSUECOUNTRY"].ToString();
                lbExpiry.Text = dt.Rows[0]["EXDATE"].ToString();
                lbIssue.Text = dt.Rows[0]["ISSUEDATE"].ToString();
                lbDOB.Text = dt.Rows[0]["DATEOFBIRTH"].ToString();
                lblOffcicePhone.Text = dt.Rows[0]["OFFICEPHONE"].ToString();
                lbOccupation.Text = dt.Rows[0]["OCCUPATION"].ToString();
                lbPurpose.Text = dt.Rows[0]["PURPOSE"].ToString();
                lbCountryOfBirth.Text = dt.Rows[0]["BIRTHCOUTRY"].ToString();

                lbSOF.Text = dt.Rows[0]["SOURCEOFUND"].ToString();
                lbUnit.Text = dt.Rows[0]["ICUNIT"].ToString();
                lbDistrict.Text = dt.Rows[0]["ICDISTRICT"].ToString();
                lbProvince.Text = dt.Rows[0]["ICPROVINCE"].ToString();
                lbCountry.Text = dt.Rows[0]["ICCOUNTRY"].ToString();
                lblNation.Text = dt.Rows[0]["NATIONLITY"].ToString();

                fileDoc.NavigateUrl = dt.Rows[0]["DOC"].ToString();

                if (SmartPortal.Common.Utilities.Utility.isDouble(dt.Rows[0]["FEEOFWU"].ToString(), true) == 0)
                {
                    txtranfersfee.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(dt.Rows[0]["FEE"].ToString(), "USD");
                    txtTotalwu.Text = SmartPortal.Common.Utilities.Utility.FormatMoney((SmartPortal.Common.Utilities.Utility.isDouble(lblAmountSend.Text, true) + SmartPortal.Common.Utilities.Utility.isDouble(lblFee.Text, true)).ToString(), "USD");
                }
                else
                {
                    txtranfersfee.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(dt.Rows[0]["FEEOFWU"].ToString(), "USD");
                    txtTotalwu.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(dt.Rows[0]["TOTALOFWU"].ToString(), "USD");
                }

                if (dt.Rows[0]["GENDER"].ToString() == "M")
                {
                    lbGender.Text = "Male";
                }
                else
                {
                    lbGender.Text = "FeMale";
                }
                

                lblComment.Text = dt.Rows[0]["COMMENT"].ToString();

                lblrefirstname.Text = dt.Rows[0]["REFIRSTNAME"].ToString();
                lbllastname.Text = dt.Rows[0]["RELASTNAME"].ToString();

                if (txtCashcode.Text != "" || sb.Tables[0].Rows[0]["STATUS"].ToString().Equals("D"))
                {
                    txtCashcode.Enabled = false;
                    btresend.Visible = false;
                }

                string email = new SmartPortal.SEMS.User().GetUBID(sb.Tables[0].Rows[0]["USERCREATE"].ToString()).Rows[0]["EMAIL"].ToString();
                if (email == "")
                {
                    ddlsendtype.SelectedValue = "PHONE";
                    sendtype = "PHONE";
                    ddlsendtype.Enabled = false;
                }

                Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();
                tmpl = SmartPortal.Common.ST.GetStringTemplate("SEMSWesternUnion", "ContractAttachment" + "en-US");
                if (sb.Tables[0].Rows[0]["STATUS"].ToString().Equals("A") && ACTION != "DELETE")
                {
                    btnPrint.Visible = true;

                    DataRow dr = sb.Tables[0].Rows[0];
                    tmpl.Reset();
                    tmpl.SetAttribute("CASHCODE", dr["CASHCODE"].ToString());
                    tmpl.SetAttribute("TOTALOFWU", SmartPortal.Common.Utilities.Utility.FormatMoney(dr["TOTALOFWU"].ToString(), ""));
                    tmpl.SetAttribute("FEEOFWU", SmartPortal.Common.Utilities.Utility.FormatMoney(dr["FEEOFWU"].ToString(), ""));
                    tmpl.SetAttribute("RCCYID", dr["RCCYID"].ToString());
                    tmpl.SetAttribute("SENDERFIRSTNAME", dr["SENDERFIRSTNAME"].ToString());
                    tmpl.SetAttribute("SENDERLASTNAME", dr["SENDERLASTNAME"].ToString());
                    tmpl.SetAttribute("SCCYID", dr["SCCYID"].ToString());
                    tmpl.SetAttribute("PAYCOUNTRY", dr["PAYCOUNTRY"].ToString());
                    tmpl.SetAttribute("SENDERAMOUNT", SmartPortal.Common.Utilities.Utility.FormatMoney(dr["SENDERAMOUNT"].ToString(), ""));
                    tmpl.SetAttribute("FEE", SmartPortal.Common.Utilities.Utility.FormatMoney(dr["FEE"].ToString(), ""));
                    tmpl.SetAttribute("SENDERUNIT", dr["SENDERUNIT"].ToString());
                    tmpl.SetAttribute("SENDERDISTRICT", dr["SENDERDISTRICT"].ToString());
                    tmpl.SetAttribute("SENDERCITY", dr["SENDERCITY"].ToString());
                    tmpl.SetAttribute("SENDERPROVINCE", dr["SENDERPROVINCE"].ToString());
                    tmpl.SetAttribute("SENDERCOUNTRY", dr["SENDERCOUNTRY"].ToString());
                    tmpl.SetAttribute("COUNTRYCODE", dr["COUNTRYCODE"].ToString());
                    tmpl.SetAttribute("TELEPHONE", dr["TELEPHONE"].ToString());
                    tmpl.SetAttribute("MOBILEPHONECODE", dr["MOBILEPHONECODE"].ToString());
                    tmpl.SetAttribute("PHONENO", dr["PHONENO"].ToString());
                    tmpl.SetAttribute("EMAIL", dr["EMAIL"].ToString());
                    tmpl.SetAttribute("ICTYPE", dr["ICTYPE"].ToString());
                    tmpl.SetAttribute("ICNUMBER", dr["ICNUMBER"].ToString());
                    tmpl.SetAttribute("ISSUECOUNTRY", dr["ISSUECOUNTRY"].ToString());
                    tmpl.SetAttribute("EXDATE", dr["EXDATE"].ToString());
                    tmpl.SetAttribute("ISSUEDATE", dr["ISSUEDATE"].ToString());
                    tmpl.SetAttribute("DATEOFBIRTH", dr["DATEOFBIRTH"].ToString());
                    tmpl.SetAttribute("OFFICEPHONE", dr["OFFICEPHONE"].ToString());
                    tmpl.SetAttribute("OCCUPATION", dr["OCCUPATION"].ToString());
                    tmpl.SetAttribute("PURPOSE", dr["PURPOSE"].ToString());
                    tmpl.SetAttribute("BIRTHCOUTRY", dr["BIRTHCOUTRY"].ToString());
                    tmpl.SetAttribute("SOURCEOFUND", dr["SOURCEOFUND"].ToString());
                    tmpl.SetAttribute("ICUNIT", dr["ICUNIT"].ToString());
                    tmpl.SetAttribute("ICDISTRICT", dr["ICDISTRICT"].ToString());
                    tmpl.SetAttribute("ICPROVINCE", dr["ICPROVINCE"].ToString());
                    tmpl.SetAttribute("ICCOUNTRY", dr["ICCOUNTRY"].ToString());
                    tmpl.SetAttribute("NATIONLITY", dr["NATIONLITY"].ToString());
                    tmpl.SetAttribute("GENDER", dr["GENDER"].ToString());
                    tmpl.SetAttribute("COMMENT", dr["COMMENT"].ToString());
                    tmpl.SetAttribute("REFIRSTNAME", dr["REFIRSTNAME"].ToString());
                    tmpl.SetAttribute("RELASTNAME", dr["RELASTNAME"].ToString());
                    tmpl.SetAttribute("EXCHANGERATE", dr["EXCHANGERATE"].ToString());
                    tmpl.SetAttribute("POSTCODE", dr["POSTCODE"].ToString());

                    tmpl.SetAttribute("REAMOUNT", txtReamount.Text);
                    tmpl.SetAttribute("TOTAL", txtTotalwu.Text);

                    ViewState["tmpl"] = tmpl.ToString();
                    Session["print"] = null;
                    Session["print"] = ViewState["tmpl"];
                }
            }
            else
            {
                lblError.Text = IPCERRORDESC;
            }

            txtranfersfee_TextChanged(sender, e);

            #region Enable/Disable theo Action
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.DETAILS:
                    txtCashcode.Enabled = false;
                    btdel.Visible = false;
                    pndel.Visible = false;
					btresend.Visible = false;
                    btsave.Visible = false;
                    txtExchange.Enabled = false;
                    txtExchange.Text = SmartPortal.Common.Utilities.Utility.FormatMoneyInputToView(sb.Tables[0].Rows[0]["EXCHANGERATE"].ToString(), "USD");
                    txtranfersfee.Enabled = false;
                    ddlsendtype.Enabled = false;
                    if (sb.Tables[0].Rows[0]["STATUS"].ToString().Equals("P") || sb.Tables[0].Rows[0]["STATUS"].ToString().Equals("A"))
                    {
                        btnPrint.Visible = true;
                    }
                    //txtExchange_TextChanged(sender, e);
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    btdel.Visible = false;
                    pndel.Visible = false;
                    txtranfersfee.Enabled = false;          
                    if (sb.Tables[0].Rows[0]["STATUS"].ToString().Equals("A"))
					{
						btresend.Visible = true;
                        txtExchange.Enabled = true;
                        txtExchange.Text = SmartPortal.Common.Utilities.Utility.FormatMoneyInputToView(sb.Tables[0].Rows[0]["EXCHANGERATE"].ToString(), "USD");
                        txtExchange_TextChanged(sender, e);
                        txtCashcode.Enabled = true;
                    }
                    if(sb.Tables[0].Rows[0]["STATUS"].ToString().Equals("P"))
					{
                        btsave.Visible = true;
                        btresend.Visible = false;
					}
                    if (sb.Tables[0].Rows[0]["STATUS"].ToString().Equals("P") || sb.Tables[0].Rows[0]["STATUS"].ToString().Equals("A"))
                    {
						btsave.Visible = false;
                        btnPrint.Visible = true;
                    }
                    break;
                case IPC.ACTIONPAGE.DELETE:
                    txtExchange.Enabled = false;
                    txtCashcode.Enabled = false;
                    txtranfersfee.Enabled = false;
                    btresend.Visible = false;
                    ddlsendtype.Enabled = false;
                    break;
            }
            #endregion
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = "";
            string cashcode = txtCashcode.Text.Trim();
            
            if (string.IsNullOrEmpty(cashcode))
            {
                lblError.Text = "Please input MTCN.";
                return;
            }
            if(cashcode.Length != 10)
            {
                lblError.Text = "MTCN must be 10 characters.";
                return;
            }
            if (string.IsNullOrEmpty(txtExchange.Text))
            {
                lblError.Text = "Please input Exchange rate amount.";
                return;
            }
            
            double exchangerate = SmartPortal.Common.Utilities.Utility.isDouble(txtExchange.Text.Trim(), true);
            double reamount = SmartPortal.Common.Utilities.Utility.isDouble(txtReamount.Text.Trim(), true);
            double fee = SmartPortal.Common.Utilities.Utility.isDouble(txtranfersfee.Text.Trim(), true);
            double total = SmartPortal.Common.Utilities.Utility.isDouble(txtTotalwu.Text.Trim(), true);

            new SmartPortal.SEMS.Transactions().UpdateWestern(ID, "UPDATE", cashcode, sendtype, "", exchangerate, reamount, fee, total, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                lblError.Text = Resources.labels.sendsuccessful;
                btsave.Visible = false;
                txtCashcode.Enabled = false;
                txtExchange.Enabled = false;
                txtranfersfee.Enabled = false;
                ddlsendtype.Enabled = false;

                Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();
                tmpl = SmartPortal.Common.ST.GetStringTemplate("SEMSWesternUnion", "ContractAttachment" + "en-US");
                DataSet sb = new SmartPortal.SEMS.Transactions().GetDetailWestern(ID, ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    btnPrint.Visible = true;
                    btresend.Visible = false;

                    DataRow dr = sb.Tables[0].Rows[0];
                    tmpl.Reset();
                    tmpl.SetAttribute("CASHCODE", dr["CASHCODE"].ToString());
                    tmpl.SetAttribute("TOTALOFWU", SmartPortal.Common.Utilities.Utility.FormatMoney(dr["TOTALOFWU"].ToString(), ""));
                    tmpl.SetAttribute("FEEOFWU", SmartPortal.Common.Utilities.Utility.FormatMoney(dr["FEEOFWU"].ToString(), ""));
                    tmpl.SetAttribute("RCCYID", dr["RCCYID"].ToString());
                    tmpl.SetAttribute("SENDERFIRSTNAME", dr["SENDERFIRSTNAME"].ToString());
                    tmpl.SetAttribute("SENDERLASTNAME", dr["SENDERLASTNAME"].ToString());
                    tmpl.SetAttribute("SCCYID", dr["SCCYID"].ToString());
                    tmpl.SetAttribute("PAYCOUNTRY", dr["PAYCOUNTRY"].ToString());
                    tmpl.SetAttribute("SENDERAMOUNT", SmartPortal.Common.Utilities.Utility.FormatMoney(dr["SENDERAMOUNT"].ToString(), ""));
                    tmpl.SetAttribute("FEE", SmartPortal.Common.Utilities.Utility.FormatMoney(dr["FEE"].ToString(), ""));
                    tmpl.SetAttribute("SENDERUNIT", dr["SENDERUNIT"].ToString());
                    tmpl.SetAttribute("SENDERDISTRICT", dr["SENDERDISTRICT"].ToString());
                    tmpl.SetAttribute("SENDERCITY", dr["SENDERCITY"].ToString());
                    tmpl.SetAttribute("SENDERPROVINCE", dr["SENDERPROVINCE"].ToString());
                    tmpl.SetAttribute("SENDERCOUNTRY", dr["SENDERCOUNTRY"].ToString());
                    tmpl.SetAttribute("COUNTRYCODE", dr["COUNTRYCODE"].ToString());
                    tmpl.SetAttribute("TELEPHONE", dr["TELEPHONE"].ToString());
                    tmpl.SetAttribute("MOBILEPHONECODE", dr["MOBILEPHONECODE"].ToString());
                    tmpl.SetAttribute("PHONENO", dr["PHONENO"].ToString());
                    tmpl.SetAttribute("EMAIL", dr["EMAIL"].ToString());
                    tmpl.SetAttribute("ICTYPE", dr["ICTYPE"].ToString());
                    tmpl.SetAttribute("ICNUMBER", dr["ICNUMBER"].ToString());
                    tmpl.SetAttribute("ISSUECOUNTRY", dr["ISSUECOUNTRY"].ToString());
                    tmpl.SetAttribute("EXDATE", dr["EXDATE"].ToString());
                    tmpl.SetAttribute("ISSUEDATE", dr["ISSUEDATE"].ToString());
                    tmpl.SetAttribute("DATEOFBIRTH", dr["DATEOFBIRTH"].ToString());
                    tmpl.SetAttribute("OFFICEPHONE", dr["OFFICEPHONE"].ToString());
                    tmpl.SetAttribute("OCCUPATION", dr["OCCUPATION"].ToString());
                    tmpl.SetAttribute("PURPOSE", dr["PURPOSE"].ToString());
                    tmpl.SetAttribute("BIRTHCOUTRY", dr["BIRTHCOUTRY"].ToString());
                    tmpl.SetAttribute("SOURCEOFUND", dr["SOURCEOFUND"].ToString());
                    tmpl.SetAttribute("ICUNIT", dr["ICUNIT"].ToString());
                    tmpl.SetAttribute("ICDISTRICT", dr["ICDISTRICT"].ToString());
                    tmpl.SetAttribute("ICPROVINCE", dr["ICPROVINCE"].ToString());
                    tmpl.SetAttribute("ICCOUNTRY", dr["ICCOUNTRY"].ToString());
                    tmpl.SetAttribute("NATIONLITY", dr["NATIONLITY"].ToString());
                    tmpl.SetAttribute("GENDER", dr["GENDER"].ToString());
                    tmpl.SetAttribute("COMMENT", dr["COMMENT"].ToString());
                    tmpl.SetAttribute("REFIRSTNAME", dr["REFIRSTNAME"].ToString());
                    tmpl.SetAttribute("RELASTNAME", dr["RELASTNAME"].ToString());
                    tmpl.SetAttribute("EXCHANGERATE", SmartPortal.Common.Utilities.Utility.FormatMoneyInputToView(dr["EXCHANGERATE"].ToString(),""));
                    tmpl.SetAttribute("POSTCODE", dr["POSTCODE"].ToString());
                    tmpl.SetAttribute("FILE", dr["DOC"].ToString());

                    tmpl.SetAttribute("REAMOUNT", txtReamount.Text);
                    tmpl.SetAttribute("TOTAL", txtTotalwu.Text);

                }
                //luu thong tin vao session de hien thi cho nguoi dung
                ViewState["tmpl"] = tmpl.ToString();
                Session["print"] = null;
                Session["print"] = ViewState["tmpl"];
            }
            else
            {
                lblError.Text = IPCERRORDESC;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void btback_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }

    protected void ddlsendtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = "";
            if (ddlsendtype.SelectedValue == "EMAIL")
            {
                sendtype = "EMAIL";
            }
            else
            {
                sendtype = "PHONE";
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }

    }

    protected void ddlreason_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            switch (ddlreason.SelectedValue)
            {
                case "0":
                    lbreason.Visible = true;
                    txtreason.Visible = true;
                    break;
                case "1":
                    lbreason.Visible = false;
                    txtreason.Visible = false;
                    break;
                case "2":
                    lbreason.Visible = false;
                    txtreason.Visible = false;
                    break;
                case "3":
                    lbreason.Visible = false;
                    txtreason.Visible = false;
                    break;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void btdel_Click(object sender, EventArgs e)
    {
        try
        {
            btdel.Enabled = false;
            string reason = "";
            switch (ddlreason.SelectedValue)
            {
                case "0":
                    reason = txtreason.Text;
                    break;
                case "1":
                    reason = ddlreason.SelectedItem.Text;
                    break;
                case "2":
                    reason = ddlreason.SelectedItem.Text; 
                    break;
                case "3":
                    reason = ddlreason.SelectedItem.Text; 
                    break;
            }

            new SmartPortal.SEMS.Transactions().CancelWestern(ID, reason, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                lblError.Text = "Cancel successfully.";
            }
            else
            {
                lblError.Text = IPCERRORDESC;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void btresend_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet dt = new SmartPortal.SEMS.Transactions().SendWestern(ID, sendtype, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();
                tmpl = SmartPortal.Common.ST.GetStringTemplate("SEMSWesternUnion", "ContractAttachment" + "en-US");
                DataSet sb = new SmartPortal.SEMS.Transactions().GetDetailWestern(ID, ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    btnPrint.Visible = true;
                    DataRow dr = sb.Tables[0].Rows[0];
                    tmpl.Reset();
                    tmpl.SetAttribute("CASHCODE", dr["CASHCODE"].ToString());
                    tmpl.SetAttribute("TOTALOFWU", SmartPortal.Common.Utilities.Utility.FormatMoney(dr["TOTALOFWU"].ToString(), ""));
                    tmpl.SetAttribute("FEEOFWU", SmartPortal.Common.Utilities.Utility.FormatMoney(dr["FEEOFWU"].ToString(), ""));
                    tmpl.SetAttribute("RCCYID", dr["RCCYID"].ToString());
                    tmpl.SetAttribute("SENDERFIRSTNAME", dr["SENDERFIRSTNAME"].ToString());
                    tmpl.SetAttribute("SENDERLASTNAME", dr["SENDERLASTNAME"].ToString());
                    tmpl.SetAttribute("SCCYID", dr["SCCYID"].ToString());
                    tmpl.SetAttribute("PAYCOUNTRY", dr["PAYCOUNTRY"].ToString());
                    tmpl.SetAttribute("SENDERAMOUNT", SmartPortal.Common.Utilities.Utility.FormatMoney(dr["SENDERAMOUNT"].ToString(),""));
                    tmpl.SetAttribute("FEE", SmartPortal.Common.Utilities.Utility.FormatMoney(dr["FEE"].ToString(),""));
                    tmpl.SetAttribute("SENDERUNIT", dr["SENDERUNIT"].ToString());
                    tmpl.SetAttribute("SENDERDISTRICT", dr["SENDERDISTRICT"].ToString());
                    tmpl.SetAttribute("SENDERCITY", dr["SENDERCITY"].ToString());
                    tmpl.SetAttribute("SENDERPROVINCE", dr["SENDERPROVINCE"].ToString());
                    tmpl.SetAttribute("SENDERCOUNTRY", dr["SENDERCOUNTRY"].ToString());
                    tmpl.SetAttribute("COUNTRYCODE", dr["COUNTRYCODE"].ToString());
                    tmpl.SetAttribute("TELEPHONE", dr["TELEPHONE"].ToString());
                    tmpl.SetAttribute("MOBILEPHONECODE", dr["MOBILEPHONECODE"].ToString());
                    tmpl.SetAttribute("PHONENO", dr["PHONENO"].ToString());
                    tmpl.SetAttribute("EMAIL", dr["EMAIL"].ToString());
                    tmpl.SetAttribute("ICTYPE", dr["ICTYPE"].ToString());
                    tmpl.SetAttribute("ICNUMBER", dr["ICNUMBER"].ToString());
                    tmpl.SetAttribute("ISSUECOUNTRY", dr["ISSUECOUNTRY"].ToString());
                    tmpl.SetAttribute("EXDATE", dr["EXDATE"].ToString());
                    tmpl.SetAttribute("ISSUEDATE", dr["ISSUEDATE"].ToString());
                    tmpl.SetAttribute("DATEOFBIRTH", dr["DATEOFBIRTH"].ToString());
                    tmpl.SetAttribute("OFFICEPHONE", dr["OFFICEPHONE"].ToString());
                    tmpl.SetAttribute("OCCUPATION", dr["OCCUPATION"].ToString());
                    tmpl.SetAttribute("PURPOSE", dr["PURPOSE"].ToString());
                    tmpl.SetAttribute("BIRTHCOUTRY", dr["BIRTHCOUTRY"].ToString());
                    tmpl.SetAttribute("SOURCEOFUND", dr["SOURCEOFUND"].ToString());
                    tmpl.SetAttribute("ICUNIT", dr["ICUNIT"].ToString());
                    tmpl.SetAttribute("ICDISTRICT", dr["ICDISTRICT"].ToString());
                    tmpl.SetAttribute("ICPROVINCE", dr["ICPROVINCE"].ToString());
                    tmpl.SetAttribute("ICCOUNTRY", dr["ICCOUNTRY"].ToString());
                    tmpl.SetAttribute("NATIONLITY", dr["NATIONLITY"].ToString());
                    tmpl.SetAttribute("GENDER", dr["GENDER"].ToString());
                    tmpl.SetAttribute("COMMENT", dr["COMMENT"].ToString());
                    tmpl.SetAttribute("REFIRSTNAME", dr["REFIRSTNAME"].ToString());
                    tmpl.SetAttribute("RELASTNAME", dr["RELASTNAME"].ToString());
                    tmpl.SetAttribute("EXCHANGERATE", dr["EXCHANGERATE"].ToString());
                    tmpl.SetAttribute("POSTCODE", dr["POSTCODE"].ToString());
                    tmpl.SetAttribute("FILE", dr["DOC"].ToString());

                    tmpl.SetAttribute("REAMOUNT", txtReamount.Text);
                    tmpl.SetAttribute("TOTAL", txtTotalwu.Text);

                }
                //luu thong tin vao session de hien thi cho nguoi dung
                ViewState["tmpl"] = tmpl.ToString();
                Session["print"] = null;
                Session["print"] = ViewState["tmpl"];

                lblError.Text = Resources.labels.sendsuccessful;
				btresend.Visible = false;
            }
            else
            {
                lblError.Text = "Error";
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    protected void txtExchange_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            if (String.IsNullOrEmpty(txtExchange.Text))
            {
                lblError.Text = "Exchangrate is wrong!";
                btsave.Visible = false;
                btresend.Visible = false;
                txtReamount.Text = "";
            }
            else
            {

               
                btsave.Visible = true;
                txtReamount.Text = SmartPortal.Common.Utilities.Utility.FormatMoney((SmartPortal.Common.Utilities.Utility.isDouble(lblAmountSend.Text, true) * SmartPortal.Common.Utilities.Utility.isDouble(txtExchange.Text.Trim(), true)).ToString(),"");
                txtExchange.Text = SmartPortal.Common.Utilities.Utility.FormatMoneyInputToView(txtExchange.Text, "");
            }
        }
        catch (Exception ex)
        {
            btresend.Visible = false;
            btsave.Visible = false;
            lblError.Text = ex.Message.ToString();
            return;
            //SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            //SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    protected void txtranfersfee_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (String.IsNullOrEmpty(txtranfersfee.Text))
            {
                lblError.Text = "Invalid Tranfer fee!";
            }
            else
            {
                txtranfersfee.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(txtranfersfee.Text, "");
                txtTotalwu.Text = SmartPortal.Common.Utilities.Utility.FormatMoney((SmartPortal.Common.Utilities.Utility.isDouble(lblAmountSend.Text, true) + SmartPortal.Common.Utilities.Utility.isDouble(txtranfersfee.Text.Trim(), true)).ToString(), "");
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        //Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?po=4&p=183&returnURL=" + SmartPortal.Common.Encrypt.EncryptData(Request.Url.Query)));
    }
}
