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
using System.Collections.Generic;

public partial class Widgets_SEMSinternational_Control_Widget : WidgetBase
{
    private string ACTION = string.Empty;
    string IPCERRORCODE = string.Empty;
    string IPCERRORDESC = string.Empty;
    string ID = "";
    string sendtype = string.Empty;
    string userid = string.Empty;
    string reference = string.Empty;
    string Trandate = string.Empty;
    string Linktracking = "https://accessportal.jpmorgan.com/PaymentTracker";
    public DataTable TBLDOCUMENT
    {
        get { return ViewState["TBLDOCUMENT"] != null ? (DataTable)ViewState["TBLDOCUMENT"] : new DataTable(); }
        set { ViewState["TBLDOCUMENT"] = TBLDOCUMENT; }
    }

    public DataTable TRANSINFO
    {
        get { return ViewState["TRANSINFO"] != null ? (DataTable)ViewState["TRANSINFO"] : new DataTable(); }
        set { ViewState["TRANSINFO"] = TRANSINFO; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
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
            //string linkparam = new SmartPortal.SEMS.EBASYSVAR().ViewDetail("LINKTRACKINGINTERNATIONALTRANSFER", ref IPCERRORCODE, ref IPCERRORDESC).Tables[0].Rows[0]["VARVALUE"].ToString();
            //Linktracking =  !String.IsNullOrEmpty(linkparam) ? linkparam : Linktracking;
			DataSet linkparam = new SmartPortal.SEMS.EBASYSVAR().ViewDetail("LINKTRACKINGINTERNATIONALTRANSFER", ref IPCERRORCODE, ref IPCERRORDESC);
            if (linkparam.Tables.Count != 0)
            {
                if (linkparam.Tables[0].Rows.Count != 0)
                {
                    Linktracking = !String.IsNullOrEmpty(linkparam.Tables[0].Rows[0]["VARVALUE"].ToString()) ? linkparam.Tables[0].Rows[0]["VARVALUE"].ToString() : Linktracking;
                }
            }
            txtEnterTrandate.Attributes["max"] = DateTime.Now.ToString("yyyy-MM-dd");
            txtTrandate.Attributes["max"] = DateTime.Now.ToString("yyyy-MM-dd");

            DataSet ds = new DataSet();
            ds = new SmartPortal.SEMS.Transactions().GetDetailCBTransfer(ID, ref IPCERRORCODE, ref IPCERRORDESC);
			if (ds.Tables[0].Rows.Count == 0)
            {
                IPCERRORDESC = "Current data does not exist or has been deleted";
                IPCERRORCODE = "9999";
            }
            if (IPCERRORCODE == "0")
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];

                txtEnterReference.Text = txtReference.Text = dt.Rows[0]["MTCNCODE"].ToString();
                lblSenderAmount.Text = Utility.FormatMoney(dt.Rows[0]["NUM01"].ToString(), dt.Rows[0]["CCYID"].ToString());
                lblSenderCCYIDAmount.Text = dt.Rows[0]["CCYID"].ToString();

                lblEnterSenderAmount.Text = Utility.FormatMoney(dt.Rows[0]["NUM01"].ToString(), dt.Rows[0]["CCYID"].ToString());
                lblEnterSenderCCYIDAmount.Text = dt.Rows[0]["CCYID"].ToString();
                if (txtReference.Text != "" || dt.Rows[0]["STATUS"].ToString().Equals("D"))
                {
                    txtReference.Enabled = false;
                    txtEnterReference.Enabled = false;
                    //btresend.Visible = false;
                }
                if (dt.Rows[0]["CHAR26"].ToString().Equals("IND") || dt.Rows[0]["CHAR26"].ToString().Equals("CTK") || dt.Rows[0]["CHAR26"].ToString().Equals("DCKT"))
                {
                    divConsumber.Visible = true;
                    divEnterprise.Visible = false;
                }
                else
                {
                    divConsumber.Visible = false;
                    divEnterprise.Visible = true;
                }
                if (dt.Rows[0]["IPCTRANCODE"].ToString().Equals("IBCBTRANSFER"))
                {
                    lblEntername.Text = dt.Rows[0]["SNAME"].ToString();
                }
                else
                {
                    switch (dt.Rows[0]["CHAR26"].ToString())
                    {
                        case "CTK":
                        case "DCKT":
                            lblEntername.Text = dt.Rows[0]["SNAME"].ToString();
                            break;
                        default:
                            lblEntername.Text = dt.Rows[0]["CHAR28"].ToString();
                            break;
                    }
                }
                lblSenderName.Text = dt.Rows[0]["SNAME"].ToString();
                lblSenderIDType.Text = dt.Rows[0]["IDType"].ToString();
                lblSenderIDNumber.Text = dt.Rows[0]["SIDNUMBER"].ToString();
                lblExpDate.Text = dt.Rows[0]["SEXPDATE"].ToString();
                lblIssDate.Text = dt.Rows[0]["SISSUEDATE"].ToString();
                lblSenderPhone.Text = dt.Rows[0]["SPHONE"].ToString();
                lblSenderAddress.Text = dt.Rows[0]["SADDRESS"].ToString();
                lblEnterPhone.Text = dt.Rows[0]["SPHONE"].ToString();
                lblEnterAddress.Text = dt.Rows[0]["SADDRESS"].ToString();
                lblSenderCountry.Text = dt.Rows[0]["SCOUNTRY"].ToString();

                lblEnterLicense.Text = dt.Rows[0]["CHAR01"].ToString();
                lblEnterTaxcode.Text = dt.Rows[0]["CHAR02"].ToString();

                lblBankName.Text = dt.Rows[0]["RBANKNAME"].ToString();
                lblSwiftCode.Text = dt.Rows[0]["RSWIFTCODE"].ToString();
                lblBenName.Text = dt.Rows[0]["RNAME"].ToString();
                lblBenaccount.Text = dt.Rows[0]["RACCOUNT"].ToString();
                lblBenAddress.Text = dt.Rows[0]["RADDRESS"].ToString();
                lblBenPhone.Text = dt.Rows[0]["RPHONE"].ToString();
                lblBenEmail.Text = dt.Rows[0]["REMAIL"].ToString();

                lblDebitAccount.Text = dt.Rows[0]["SACCOUNT"].ToString();
                lblAmount.Text = Utility.FormatMoney(dt.Rows[0]["NUM01"].ToString(), dt.Rows[0]["CCYID"].ToString());
                lblFeeType.Text = dt.Rows[0]["CHAR21"].ToString();
                lblFee.Text = Utility.FormatMoney(dt.Rows[0]["NUM02"].ToString(), dt.Rows[0]["CCYID"].ToString());
                lblTotalAmount.Text = Utility.FormatMoney(dt.Rows[0]["NUM03"].ToString(), dt.Rows[0]["CCYID"].ToString());
                lblPurPose.Text = dt.Rows[0]["PURPOSE"].ToString();
                lblDetailPurPose.Text = dt.Rows[0]["TRANDESC"].ToString();
                lblCCYIDFEE.Text = lblCCYIDTotalAmount.Text = lblCCYIDAmount.Text = dt.Rows[0]["CCYID"].ToString();

                switch (dt.Rows[0]["STATUS"].ToString().Trim())
                {
                    case "A":
                        hdfstatus.Value = Resources.labels.send;
                        break;                 
                    case "P":
                        hdfstatus.Value = Resources.labels.conpending;
                        break;
                    case "F":
                        hdfstatus.Value = Resources.labels.loi;
                        break;                 
                }
                //Document
                DataTable dtDocumentLink = new DataTable();
                DataColumn documentnamelink = new DataColumn("DOCUMENTNAME");
                DataColumn documenttypelink = new DataColumn("DOCUMENTTYPE");
                DataColumn filelink = new DataColumn("FILE");

                dtDocumentLink.Columns.Add(documentnamelink);
                dtDocumentLink.Columns.Add(documenttypelink);
                dtDocumentLink.Columns.Add(filelink);
                if (ds.Tables[1] != null)
                {
                    if (ds.Tables[1].Rows.Count != 0)
                    {
                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                            string dctype = ds.Tables[1].Rows[i]["DocumentType"].ToString();
                            string dcname = "Document " + ds.Tables[1].Rows[i]["ID"].ToString();
                            string base64 = ds.Tables[1].Rows[i]["LINK"].ToString();
                            dtDocumentLink.Rows.Add(dcname, dctype,base64);
                        }

                        pnlDocument.Visible = true;
                        rptDocument.DataSource = dtDocumentLink;
                        rptDocument.DataBind();
                        ViewState["TBLDOCUMENT"] = dtDocumentLink;
                    }
                }

                //ghi vo session dung in
                Hashtable hasPrint = new Hashtable();
                ViewState["TRANSINFO"] = ds.Tables[0];
                hasPrint = AddtoHashtable(hasPrint, ds.Tables[0]);
                Session["print"] = hasPrint;


                #region Enable/Disable theo Action
                switch (ACTION)
                {
                    case IPC.ACTIONPAGE.DETAILS:
                        btsend.Visible = false;
                        btresend.Visible = false;
                        txtReference.Enabled = false;
                        txtEnterReference.Enabled = false;
                        txtEnterTrandate.Enabled = false;
                        txtTrandate.Enabled = false;
                        ddlsendtype.Enabled = false;
                        ddlEntersendtype.Enabled = false;
                        if (dt.Rows[0]["STATUS"].ToString().Equals("P") || dt.Rows[0]["STATUS"].ToString().Equals("A"))
                        {
                            btnPrint.Visible = true;
                        }
                        
                        break;
                    case IPC.ACTIONPAGE.EDIT:                 
                        if (dt.Rows[0]["STATUS"].ToString().Equals("A"))
                        {
                            btresend.Visible = true;
                            txtReference.Enabled = true;
                            txtEnterReference.Enabled = true;
                        }
                        if (dt.Rows[0]["STATUS"].ToString().Equals("P"))
                        {
                            btsend.Visible = true;
                            btresend.Visible = false;
                        }
                        if (dt.Rows[0]["STATUS"].ToString().Equals("P") || dt.Rows[0]["STATUS"].ToString().Equals("A"))
                        {
                            btnPrint.Visible = true;
                        }
                        break;                   
                }
                #endregion
            
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

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        //Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?po=4&p=183&returnURL=" + SmartPortal.Common.Encrypt.EncryptData(Request.Url.Query)));
    }

    protected void rptDocument_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        HyperLink HyperLinkDocument;
        HyperLinkDocument = (HyperLink)rptDocument.FindControl("HyperLinkDocument");
        DataTable dt = new DataTable();
        dt = (DataTable)ViewState["TBLDOCUMENT"];
        string base64 = dt.Rows[e.Item.ItemIndex]["FILE"].ToString();
        string filename = dt.Rows[e.Item.ItemIndex]["DOCUMENTNAME"].ToString() + dt.Rows[e.Item.ItemIndex]["DOCUMENTTYPE"].ToString().ToLower();
        if (!base64.StartsWith("http"))
        {
            byte[] bytes = System.Convert.FromBase64String(base64);
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + filename + "\"");
            HttpContext.Current.Response.AddHeader("Content-Length", Convert.ToString(bytes.Length));
            HttpContext.Current.Response.BinaryWrite(bytes);
        }
        else
        {
            HttpContext.Current.Response.Redirect(base64);
        }
    }
    public IEnumerable<Control> FindControlRecursive(Control Root, string Id)
    {
        try
        {
            var results = new List<Control>();
            if (Root.ID == Id)
                results.Add(Root);
            foreach (Control Ctl in Root.Controls)
            {
                results.AddRange(FindControlRecursive(Ctl, Id));
            }
            return results;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
        return null;
    }


    protected void rptDocument_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            int a = e.Item.ItemIndex;
            if (!(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem))
            {
                return;
            }
            //LinkButton linkButton = FindControlRecursive(rptDocument, "lbDownload") as LinkButton;
            IEnumerable<Control> listlink = FindControlRecursive(rptDocument, "lblDownload");
            foreach (var Linkbutton in listlink)
            {
                var scriptManager = ScriptManager.GetCurrent(this.Page);
                if (scriptManager != null)
                {
                    scriptManager.RegisterPostBackControl(Linkbutton);
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void btsave_Click(object sender, EventArgs e)
    {

        reference = !String.IsNullOrEmpty(txtReference.Text.Trim()) ? txtReference.Text.Trim() : txtEnterReference.Text.Trim();
        Trandate = !String.IsNullOrEmpty(txtEnterTrandate.Text.Trim()) ? txtEnterTrandate.Text.Trim() : txtTrandate.Text.Trim();
        try
        {           
            lblError.Text = "";
            if(string.IsNullOrEmpty(reference))
            {
                lblError.Text = "Please input Payment Reference .";
                return;
            }
            if (reference.Length != 15)
            {
                lblError.Text = "MTCN must be 15 characters.";
                return;
            }
            if (string.IsNullOrEmpty(Trandate))
            {
                lblError.Text = "Please input Transaction Date .";
                return;
            }        
            new SmartPortal.SEMS.Transactions().UpdateCBTransfer(ID,reference, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {


                DataSet sb = new SmartPortal.SEMS.Transactions().GetDetailCBTransfer(ID, ref IPCERRORCODE, ref IPCERRORDESC);
                ViewState["TRANSINFO"] = sb.Tables[0];
                Hashtable hasPrint = new Hashtable();
                hasPrint = AddtoHashtable(hasPrint, sb.Tables[0]);
                Session["print"] = hasPrint;
                string email = String.Empty;
                string phone = String.Empty;
                string amount = String.Empty;
                string ccyid = String.Empty;
                Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();               
                if (IPCERRORCODE == "0")
                {
                    if (sb.Tables[0].Rows.Count > 0)
                    {
                        SmartPortal.SEMS.User UserInstance = new SmartPortal.SEMS.User();
                        DataRow dr = sb.Tables[0].Rows[0];
                        userid = dr["USERID"].ToString();
                        DataRow userRow = UserInstance.GetUBID(userid).Rows[0];
                        email = userRow["EMAIL"].ToString().Trim();
                        phone = userRow["PHONE"].ToString().Trim();
                        amount = Utility.FormatMoney(dr["NUM01"].ToString(), dr["CCYID"].ToString()); 
                        ccyid  = dr["CCYID"].ToString();
                        switch (dr["STATUS"].ToString().Trim())
                        {
                            case "A":
                                hdfstatus.Value = Resources.labels.send;
                                break;
                            case "P":
                                hdfstatus.Value = Resources.labels.conpending;
                                break;
                            case "F":
                                hdfstatus.Value = Resources.labels.loi;
                                break;
                        }
                        if (divConsumber.Visible)
                        {
                            tmpl = SmartPortal.Common.ST.GetStringTemplate("SEMSinternational", "INDForm");
                            tmpl.Reset();
                            tmpl.SetAttribute("tranID", ID);
                            tmpl.SetAttribute("tranDate", dr["TRANDATE"].ToString());
                            tmpl.SetAttribute("status", hdfstatus.Value.ToString().Trim());

                            tmpl.SetAttribute("senderName", dr["SNAME"].ToString());
                            tmpl.SetAttribute("idType", dr["IDType"].ToString());
                            tmpl.SetAttribute("idNumber", dr["SIDNUMBER"].ToString());
                            tmpl.SetAttribute("senderCountry", dr["SCOUNTRY"].ToString());
                            tmpl.SetAttribute("expiredDate", dr["SEXPDATE"].ToString());
                            tmpl.SetAttribute("issueDate", dr["SISSUEDATE"].ToString());
                            tmpl.SetAttribute("senderAddress", dr["SADDRESS"].ToString());
                            tmpl.SetAttribute("senderPhone", dr["SPHONE"].ToString());

                            tmpl.SetAttribute("bankName", dr["RBANKNAME"].ToString());
                            tmpl.SetAttribute("swiftCode", dr["RSWIFTCODE"].ToString());
                            tmpl.SetAttribute("benName", dr["RNAME"].ToString());
                            tmpl.SetAttribute("benAccount", dr["RACCOUNT"].ToString());
                            tmpl.SetAttribute("benAddress", dr["RADDRESS"].ToString());
                            tmpl.SetAttribute("benPhone", dr["RPHONE"].ToString());
                            tmpl.SetAttribute("benEmail", dr["REMAIL"].ToString());

                            tmpl.SetAttribute("senderAccount", dr["SACCOUNT"].ToString());
                            tmpl.SetAttribute("amount", Utility.FormatMoney(dr["NUM01"].ToString(), dr["CCYID"].ToString()));
                            tmpl.SetAttribute("feeType", dr["CHAR21"].ToString());
                            tmpl.SetAttribute("fee", Utility.FormatMoney(dr["NUM02"].ToString(), dr["CCYID"].ToString()));
                            tmpl.SetAttribute("totalAmount", Utility.FormatMoney(dr["NUM03"].ToString(), dr["CCYID"].ToString()));
                            tmpl.SetAttribute("purpose", dr["PURPOSE"].ToString());
                            tmpl.SetAttribute("purposeDetail", dr["TRANDESC"].ToString());
                            tmpl.SetAttribute("linktracking", Linktracking);
                        }
                        else
                        {
                            tmpl = SmartPortal.Common.ST.GetStringTemplate("SEMSinternational", "MTK");
                            tmpl.Reset();
                            tmpl.SetAttribute("MTCNCODE", dr["MTCNCODE"].ToString());
                            tmpl.SetAttribute("tranID", ID);
                            tmpl.SetAttribute("tranDate", dr["TRANDATE"].ToString());
                            tmpl.SetAttribute("status", hdfstatus.Value.ToString().Trim());

                            tmpl.SetAttribute("enterName", dr["SNAME"].ToString());
                            tmpl.SetAttribute("enterAddress", dr["SADDRESS"].ToString());
                            tmpl.SetAttribute("enterPhone", dr["SPHONE"].ToString());
                            tmpl.SetAttribute("enterLicense", dr["CHAR01"].ToString());
                            tmpl.SetAttribute("enterTaxCode", dr["CHAR02"].ToString());

                            tmpl.SetAttribute("bankName", dr["RBANKNAME"].ToString());
                            tmpl.SetAttribute("swiftCode", dr["RSWIFTCODE"].ToString());
                            tmpl.SetAttribute("benName", dr["RNAME"].ToString());
                            tmpl.SetAttribute("benAccount", dr["RACCOUNT"].ToString());
                            tmpl.SetAttribute("benAddress", dr["RADDRESS"].ToString());
                            tmpl.SetAttribute("benPhone", dr["RPHONE"].ToString());
                            tmpl.SetAttribute("benEmail", dr["REMAIL"].ToString());

                            tmpl.SetAttribute("senderAccount", dr["SACCOUNT"].ToString());
                            tmpl.SetAttribute("amount", Utility.FormatMoney(dr["NUM01"].ToString(), dr["CCYID"].ToString()));
                            tmpl.SetAttribute("feeType", dr["CHAR21"].ToString());
                            tmpl.SetAttribute("fee", Utility.FormatMoney(dr["NUM02"].ToString(), dr["CCYID"].ToString()));
                            tmpl.SetAttribute("totalAmount", Utility.FormatMoney(dr["NUM03"].ToString(), dr["CCYID"].ToString()));
                            tmpl.SetAttribute("purpose", dr["PURPOSE"].ToString());
                            tmpl.SetAttribute("purposeDetail", dr["TRANDESC"].ToString());
                            tmpl.SetAttribute("linktracking", Linktracking);
                        }
                    }
                }

                string templateSMS = "Your SWIFT Payment Reference No " + reference + " of amount " + amount + ccyid + " and Transaction Date: " + Convert.ToDateTime(Trandate).ToString("dd/MM/yyyy") + ".Details are sent to your registered email ID.";
                if (sendtype.Equals("EMAIL"))
                {
                    try
                    {

                        EmailHelper.SendMailMessageAsync(ConfigurationManager.AppSettings["contractapprovemailfrom"], email, ConfigurationManager.AppSettings["contractapprovemailtitle"], tmpl.ToString());
                        SmartPortal.Common.Log.WriteLogFile("EMAIL NOTIFY", "", "", "  Send transfer email success:  " + email + " TransID: " + ID + " UserID: " + userid);
                    }
                    catch
                    {

                    }
                }
                else if(sendtype.Equals("PHONE"))
                {

                    
                    SmartPortal.SEMS.Contract.sendSMS_Contract(phone, templateSMS, ref IPCERRORCODE, ref IPCERRORDESC);
                    SmartPortal.Common.Log.WriteLogFile("SMS LOG", "", "", "sent to " + phone);
                }
                else
                {
                    EmailHelper.SendMailMessageAsync(ConfigurationManager.AppSettings["contractapprovemailfrom"], email, ConfigurationManager.AppSettings["contractapprovemailtitle"], tmpl.ToString());
                    SmartPortal.Common.Log.WriteLogFile("EMAIL NOTIFY", "", "", "  Send transfer email success:  " + email + " TransID: " + ID + " UserID: " + userid);
                    SmartPortal.SEMS.Contract.sendSMS_Contract(phone, templateSMS, ref IPCERRORCODE, ref IPCERRORDESC);
                    SmartPortal.Common.Log.WriteLogFile("SMS LOG", "", "", "sent to " + phone);
                }
            }
            else
            {
                lblError.Text = IPCERRORDESC;
            }
            
            lblError.Text = Resources.labels.sendsuccessful;
            btsend.Visible = false;
            btresend.Visible = false;
            txtReference.Enabled = false;
            txtEnterReference.Enabled = false;
            txtEnterTrandate.Enabled = false;
            txtTrandate.Enabled = false;
            ddlsendtype.Enabled = false;
            ddlEntersendtype.Enabled = false;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
             SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
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
            else if(ddlsendtype.SelectedValue == "PHONE")
            {
                sendtype = "PHONE";
            }
            else
            {
                sendtype = "BOTH";
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }

    }

    protected void ddlEntersendtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlEntersendtype.SelectedValue == "EMAIL")
            {
                sendtype = "EMAIL";
            }
            else if (ddlEntersendtype.SelectedValue == "PHONE")
            {
                sendtype = "PHONE";
            }
            else
            {
                sendtype = "BOTH";
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }

    }

    protected Hashtable AddtoHashtable(Hashtable hasPrint, DataTable dt)
    {
        DataRow dr = dt.Rows[0];

        switch (dr["STATUS"].ToString().Trim())
        {
            case "A":
                hdfstatus.Value = Resources.labels.send;
                break;
            case "P":
                hdfstatus.Value = Resources.labels.conpending;
                break;
            case "F":
                hdfstatus.Value = Resources.labels.loi;
                break;
        }
        hasPrint.Add("status", hdfstatus.Value);
        hasPrint.Add("accType", dr["CHAR26"].ToString());
        hasPrint.Add("tranID", ID);
        hasPrint.Add("MTCNCODE", dr["MTCNCODE"].ToString());
        hasPrint.Add("tranDate", Trandate);

        hasPrint.Add("senderName", dr["SNAME"].ToString());
        hasPrint.Add("idType", dr["IDType"].ToString());
        hasPrint.Add("idNumber", dr["SIDNUMBER"].ToString());
        hasPrint.Add("senderCountry", dr["SCOUNTRY"].ToString());
        hasPrint.Add("expiredDate", dr["SEXPDATE"].ToString());
        hasPrint.Add("issueDate", dr["SISSUEDATE"].ToString());
        hasPrint.Add("senderAddress", dr["SADDRESS"].ToString());
        hasPrint.Add("senderPhone", dr["SPHONE"].ToString());

        hasPrint.Add("enterName", dr["SNAME"].ToString());
        hasPrint.Add("enterAddress", dr["SADDRESS"].ToString());
        hasPrint.Add("enterPhone", dr["SPHONE"].ToString());
        hasPrint.Add("enterLicense", dr["CHAR01"].ToString());
        hasPrint.Add("enterTaxCode", dr["CHAR02"].ToString());

        hasPrint.Add("bankName", dr["RBANKNAME"].ToString());
        hasPrint.Add("swiftCode", dr["RSWIFTCODE"].ToString());
        hasPrint.Add("benName", dr["RNAME"].ToString());
        hasPrint.Add("benAccount", dr["RACCOUNT"].ToString());
        hasPrint.Add("benAddress", dr["RADDRESS"].ToString());
        hasPrint.Add("benPhone", dr["RPHONE"].ToString());
        hasPrint.Add("benEmail", dr["REMAIL"].ToString());

        hasPrint.Add("senderAccount", dr["SACCOUNT"].ToString());
        hasPrint.Add("amount", Utility.FormatMoney(dr["NUM01"].ToString(), dr["CCYID"].ToString()));
        hasPrint.Add("ccyid", dr["CCYID"].ToString());
        hasPrint.Add("feeType", dr["CHAR21"].ToString());
        hasPrint.Add("fee", Utility.FormatMoney(dr["NUM02"].ToString(), dr["CCYID"].ToString()));
        hasPrint.Add("totalAmount", Utility.FormatMoney(dr["NUM03"].ToString(), dr["CCYID"].ToString()));
        hasPrint.Add("purpose", dr["PURPOSE"].ToString());
        hasPrint.Add("purposeDetail", dr["TRANDESC"].ToString());
        hasPrint.Add("linktracking", Linktracking);
        return hasPrint;
    }
    protected void txtTrandate_TextChanged(object sender, EventArgs e)
    {
        Trandate = !String.IsNullOrEmpty(txtEnterTrandate.Text.Trim()) ? txtEnterTrandate.Text.Trim() : txtTrandate.Text.Trim();
        Hashtable hasPrint = new Hashtable();
        hasPrint = AddtoHashtable(hasPrint,(DataTable)ViewState["TRANSINFO"]);
        Session["print"] = hasPrint;

    }
}
