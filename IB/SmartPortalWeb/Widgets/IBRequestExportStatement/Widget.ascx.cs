using System;
using System.Data;
using SmartPortal.IB;
using SmartPortal.ExceptionCollection;
using System.Globalization;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Collections;

public partial class Widgets_IBRequestExportStatement_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblError.Text = "";
        if (!IsPostBack)
        {
            txtFromDate.Text = DateTime.Now.AddMonths(-3).ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            Customer ct = new Customer();
            string ErrorCode = string.Empty;
            string ErrorDesc = string.Empty;
            DataSet ds = new DataSet();
            try
            {
                SmartPortal.Common.Log.WriteLogFile("MB_GETINFOSTATEMENT", Session["userID"].ToString(), "", "");
                ds = ct.GetInfo("IB_GETCUSTINFO", new object[] { Session["userID"].ToString() }, ref ErrorCode, ref ErrorDesc);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lblfullname.Text = ds.Tables[0].Rows[0]["FULLNAME"].ToString();
                        hdUsername.Value = Session["USERNAME"].ToString();
                        txtTel.Text = ds.Tables[0].Rows[0]["PHONE"].ToString();
                        lblEmail.Text = ds.Tables[0].Rows[0]["EMAIL"].ToString();
                    }
                }

                SmartPortal.IB.Account accountList = new SmartPortal.IB.Account();
                DataSet ds1 = accountList.GetSerialNo(ref ErrorCode, ref ErrorDesc);
                if (ds1 != null)
                {
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        lblSerialNo.Text = ds1.Tables[0].Rows[0]["SERIALNO"].ToString();
                    }
                }
                //public DataSet SysVarGetByParGrp(string pargrp, ref string errorCode, ref string errorDesc)
                DataSet ds2 = new SmartPortal.IB.Customer().SysVarGetByParGrp("FEESTATEMENT", ref ErrorCode, ref ErrorDesc);
                if (ds2 != null)
                {
                    if (ds2.Tables[0].Rows.Count > 0)
                    {
                        HdFee.Value = ds2.Tables[0].Rows[0]["VARVALUE"].ToString();
                        Hashtable hasPrint = new Hashtable();
                        hasPrint.Add("fee", SmartPortal.Common.Utilities.Utility.FormatMoney(HdFee.Value, "LAK"));
                        Session["printSTM"] = hasPrint;
                    }
                }

                LoadAccount();
            }
            catch (IPCException IPCex)
            {
                SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), "Account list");
            }
            catch (Exception IPCex)
            {
                SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), "Account list");
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (!cbPolicy.Checked)
        {
            lblError.Text = Resources.labels.pleaseagreewithtermsandvonditionsofbank;
            return;
        }
        if (content.Visible && string.IsNullOrEmpty(txtDesc.Text.Trim()))
        {
            lblError.Text = Resources.labels.youneedtofillinalltheinformation;
            return;
        }
        if (string.IsNullOrEmpty(txtFromDate.Text.Trim()) || string.IsNullOrEmpty(txtToDate.Text.Trim()))
        {
            lblError.Text = Resources.labels.ngayketthucphailonhonngaybatdau;
            return;
        }
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
        string date = string.Empty; //SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlchoosemounth.SelectedItem.Value.Trim());
        string fromdate = txtFromDate.Text;
        string todate = txtToDate.Text;

        //string fromdate = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtFromDate.Text.Trim());
        //string todate = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtToDate.Text.Trim());
        string email = SmartPortal.Common.Utilities.Utility.KillSqlInjection(lblEmail.Text.Trim());
        string phone = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtTel.Text.Trim());
        string accno = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlAccount.SelectedItem.Text.Split('-')[0]);
        string custname = SmartPortal.Common.Utilities.Utility.KillSqlInjection(lblfullname.Text.Trim());
        string serialno = SmartPortal.Common.Utilities.Utility.KillSqlInjection(lblSerialNo.Text.Trim());
        string version = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlVersion.SelectedItem.Text.Trim());
        string detail = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtDesc.Text.Trim());
        string purpose = string.Empty;
        if (rdPurpose1.Checked)
        {
            purpose = rdPurpose1.Text;
        }
        else if (rdPurpose2.Checked)
        {
            purpose = rdPurpose2.Text;
        }
        else
        {
            purpose = rdPurpose3.Text;
        }
        if (string.IsNullOrEmpty(email))
        {
            lblError.Text = Resources.labels.vuilongnhapemail;
            return;
        }
        else
        {
            if (!Regex.IsMatch(email, @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"))
            {
                lblError.Text = Resources.labels.emailvalidate;
                return;
            }
        }
        try
        {
            string userName = hdUsername.Value;
            DateTime myDate = DateTime.ParseExact(fromdate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime targetDate = DateTime.ParseExact(todate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            string ErrorCode = "";
            string ErrorDesc = "";
            DataSet ds = new DataSet();
            ds = new SmartPortal.SEMS.CBMNet().DETAIL("LIMITEXPORTSTATEMENT", ref ErrorCode, ref ErrorDesc);
            if (ErrorCode == "0" && ds.Tables[0].Rows.Count > 0)
            {
                int limitmouth = int.Parse(ds.Tables[0].Rows[0]["VARVALUE"].ToString());
                DateTime thisDate = targetDate.AddMonths(limitmouth);
                if (myDate > targetDate)
                    lblError.Text = Resources.labels.ngayketthucphailonhonngaybatdau;
                else
                {

                    RequestExportStatement_SendMail(email, phone, fromdate, todate, accno, custname, userName, serialno, version, purpose, detail);
                }
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(IPCex.ToString(), Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    #region Load Info
    private void LoadAccount()
    {
        try
        {
            string ErrorCode = string.Empty;
            string ErrorDesc = string.Empty;
            DataSet ds = new DataSet();

            SmartPortal.IB.Account accountList = new SmartPortal.IB.Account();
            ds = accountList.GetListOfAccounts(Session["userID"].ToString(), "IB_RESTATEMENT", "IB_RESTATEMENT", "DD,CD", "", ref ErrorCode, ref ErrorDesc);
            if (ds.Tables[0].DefaultView.Count > 0)
            {
                // CHECK PERMISSION SENDER
                ds.Tables[0].DefaultView.RowFilter = "TYPEID IN ('DD', 'CD') AND STATUSCD in ('A')";
                ddlAccount.DataSource = ds.Tables[0].DefaultView;
                ddlAccount.DataValueField = ds.Tables[0].Columns["UNIQUEID"].ColumnName.ToString();
                ddlAccount.DataTextField = ds.Tables[0].Columns["ACCOUNTNO"].ColumnName.ToString();
                ddlAccount.DataBind();
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(IPCex.ToString(), Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    #endregion
    protected void RequestExportStatement_SendMail(string email, string phone, string fromdate, string todate, string accno, string custname, string userid, string serialno, string version, string purpose, string detail)
    {
        try
        {
            string ErrorCode = "";
            string ErrorDesc = "";
            DataSet ds = new DataSet();
            //ds = new SmartPortal.SEMS.CBMNet().DETAIL("LISTMAILECHANNEL", ref ErrorCode, ref ErrorDesc);
            ds = new SmartPortal.IB.Account().RequestStatement(custname, phone, version, serialno, fromdate, todate, accno, userid, detail, email, purpose, ref ErrorCode, ref ErrorDesc);
            if (ErrorCode.Equals("0"))
            {
                lblError.Text = lblError.Text = Resources.labels.sendrequestsuccess;
            }
            else
            {
                lblError.Text = "Can't send mail, please try again later!";
            }
            //Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();
            //tmpl = SmartPortal.Common.ST.GetStringTemplate("IBRequestExportStatement", "RequestExportStatement");
            //tmpl.SetAttribute("tranDate", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            //tmpl.SetAttribute("senderName", custname);
            //tmpl.SetAttribute("senderAccount", accno);
            //tmpl.SetAttribute("userid", userid);
            //tmpl.SetAttribute("serialno", serialno);
            //tmpl.SetAttribute("version", version);
            //tmpl.SetAttribute("purpose", purpose);
            //tmpl.SetAttribute("phone", phone);
            //tmpl.SetAttribute("detail", detail);
            //tmpl.SetAttribute("email", email);
            //tmpl.SetAttribute("fromdate", fromdate);
            //tmpl.SetAttribute("todate", todate);
            //if (ds != null && ds.Tables[0].Rows.Count > 0)
            //{
            //    SmartPortal.Common.EmailHelper.SendMailMessageAsync(ConfigurationManager.AppSettings["contractapprovemailfrom"], ds.Tables[0].Rows[0]["VARVALUE"].ToString(), ConfigurationManager.AppSettings["contractapprovemailtitle"], tmpl.ToString());

            //}
            //else

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            lblError.Text = "Can't send mail, please try again later!";
        }
    }
    protected void Purpose_OnCheckedChanged(object sender, EventArgs e)
    {

        if (rdPurpose3.Checked)
        {
            content.Visible = true;
        }
        else
        {
            content.Visible = false;
        }
    }
}
