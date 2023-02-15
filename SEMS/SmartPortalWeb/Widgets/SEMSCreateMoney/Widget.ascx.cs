using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using SmartPortal.Common.Utilities;
using System.Web.UI.WebControls;
using SmartPortal.Constant;
using System.Collections;
using System.IO;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Linq;

public partial class Widgets_SEMSCloseWalletAccount_Widget : WidgetBase
{
    public static bool isAscend = false;
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";

    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();
    protected void Page_Load(object sender, EventArgs e)
    {
        lblError.ForeColor = System.Drawing.Color.Red;
        try
        {
            lblError.Text = string.Empty;
            txtDebitAmt.Attributes.Add("onkeyup", "executeComma('" + txtDebitAmt.ClientID + "',event)");
            txtCreditAmt.Attributes.Add("onkeyup", "executeComma('" + txtCreditAmt.ClientID + "',event)");
            if (!IsPostBack)
            {
                loadCombobox_Type();
                BindData(String.Empty);
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    void setControlDefault()
    {
        lblError.Text = string.Empty;
        txtTransactionNumber.Text = string.Empty;
        txtTransactionDate.Text = string.Empty;
        txtCreditAccNo.Text = string.Empty;
        txtCreAccName.Text = string.Empty;
        txtCreditAmt.Text = string.Empty;
        txtCreditCurrency.Text = string.Empty;
        txtDebitAccNo.Text = string.Empty;
        txtDebitAccName.Text = string.Empty;
        txtDebitAmt.Text = string.Empty;
        txtDebitCurrency.Text = string.Empty;

    }

    private void loadCombobox_Type()
    {
        DataSet ds = new DataSet();
        ds = _service.GetValueList("TYPE", "ACT", ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE == "0")
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlType.DataSource = ds;
                ddlType.DataValueField = "VALUEID";
                ddlType.DataTextField = "CAPTION";
                ddlType.DataBind();
            }
        }
    }
    void setPara(Dictionary<object, object> infor, string DebitAccountNo, string CreditAccountNo, string DebitAmount, string CreditAmount)
    {
        infor.Add("USERID", Session["userName"].ToString());
        infor.Add("DebitAccountNo", Utility.KillSqlInjection(DebitAccountNo));
        infor.Add("CreditAccountNo", Utility.KillSqlInjection(CreditAccountNo));
        infor.Add("AmtDebit", Utility.KillSqlInjection(DebitAmount));
        infor.Add("AmtCredit", Utility.KillSqlInjection(CreditAmount));
    }

    void defaultColor()
    {
        txtDebitAccName.BorderColor = System.Drawing.Color.Empty;
        txtDebitAmt.BorderColor = System.Drawing.Color.Empty;
        txtDebitAccNo.BorderColor = System.Drawing.Color.Empty;
        txtCreditAccNo.BorderColor = System.Drawing.Color.Empty;
        txtCreAccName.BorderColor = System.Drawing.Color.Empty;
        txtCreditAmt.BorderColor = System.Drawing.Color.Empty;
    }

    private void PrintVoucher()
    {
        Label lblType, lblAccno, lblAmount, lblAcname = new Label();
        try
        {
            string str = "<table  style='width: 100 %; '>";
            str += "<tr>";
            str += "<td colspan='2' style='font-weight: bold;'>";
            str += "<img src = '../../../Images/logo.png' style = 'height:70px;' />";
            str += "<br/>";
            str += "<br/>";
            str += "</td>";
            str += "</tr>";

            str += "<tr>";
            str += "<td align='center' colspan='2'style = 'font-weight: bold; font-size:14pt;' >CREATE E-MONEY";
            str += "</td>";
            str += "</tr>";

            str += "<tr>";
            str += "<td>" + "Transaction Number";
            str += "</td>";
            str += "<td>";
            str += txtTransactionNumber.Text;
            str += "</td>";
            str += "</tr>";
            str += "<tr>";

            str += "<tr>";
            str += "<td>" + "Transaction Date";
            str += "</td>";
            str += "<td>";
            str += txtTransactionDate.Text;
            str += "</td>";
            str += "</tr>";
            str += "<tr>";

            //-----Line
            str += "<tr>";
            str += "<td style='height: 5px; '>";
            str += "</td>";
            str += "</tr>";
            //-----------

            str += "<tr>";
            str += "<td colspan='2' style = 'font-weight: bold; font-size:10pt;' > " + "TRANSACTION INFORMATION <hr />";
            str += "</td>";
            str += "</tr>";

            //-----Line
            str += "<tr>";
            str += "<td style='height: 5px; '>";
            str += "</td>";
            str += "</tr>";
            //-----------
            //-----Line
            str += "<tr>";
            str += "<td style='height: 5px; '>";
            str += "</td>";
            str += "</tr>";
            //-----------
            //Table Debit side
            str += "<tr>";
            str += "<td style = 'font-weight: bold;'>";
            str += "Debit Side";
            str += "</td>";
            str += "</tr>";
            str += "<tr>";
            str += "<th>";
            str += "Base Account Number";
            str += "</th>";
            str += "<th>";
            str += "Account Name";
            str += "</th>";
            str += "<th>";
            str += "Amount";
            str += "</th>";
            str += "</tr>";
            foreach (RepeaterItem item in rptData.Items)
            {
                if (((Label)item.FindControl("txtSide")).Text == "Debit")
                {
                    lblAccno = (Label)item.FindControl("txtACCNO");
                    lblAmount = (Label)item.FindControl("txtAmount");
                    lblAcname = (Label)item.FindControl("txtACNAME");

                    str += "<tr>";

                    str += "<td>";
                    str += lblAccno.Text;
                    str += "</td>";

                    str += "<td>";
                    str += lblAcname.Text;
                    str += "</td>";

                    str += "<td>";
                    str += lblAmount.Text;
                    str += "</td>";

                    str += "</tr>";
                }
            }
            //End Debit Side
            //-----Line
            str += "<tr>";
            str += "<td style='height: 5px; '>";
            str += "</td>";
            str += "</tr>";
            //-----Line
            str += "<tr>";
            str += "<td style='height: 5px; '>";
            str += "</td>";
            str += "</tr>";

            //Table Credit side
            str += "<tr>";
            str += "<td style = 'font-weight: bold;'>";
            str += "Credit Side";
            str += "</td>";
            str += "</tr>";
            str += "<tr>";
            str += "<th>";
            str += "Base Account Number";
            str += "</th>";
            str += "<th>";
            str += "Account Name";
            str += "</th>";
            str += "<th>";
            str += "Amount";
            str += "</th>";
            str += "</tr>";
            foreach (RepeaterItem item in rptData.Items)
            {
                if (((Label)item.FindControl("txtSide")).Text == "Credit")
                {
                    lblAccno = (Label)item.FindControl("txtACCNO");
                    lblAmount = (Label)item.FindControl("txtAmount");
                    lblAcname = (Label)item.FindControl("txtACNAME");

                    str += "<tr>";

                    str += "<td>";
                    str += lblAccno.Text;
                    str += "</td>";

                    str += "<td>";
                    str += lblAcname.Text;
                    str += "</td>";

                    str += "<td>";
                    str += lblAmount.Text;
                    str += "</td>";

                    str += "</tr>";
                }
            }
            //-------End Credit infomation-----------

            // Print Info
            str += "<tr>";
            str += "<td style='height: 5px; '>";
            str += "</td>";
            str += "</tr>";

            str += "<tr>";
            str += "<td colspan='2' style = 'font-weight: bold; font-size:10pt;' > MODIFICATION INFORMATION<hr />";
            str += "</td>";
            str += "</tr>";
            str += "<tr>";
            str += "<td>" + "Date Created";
            str += "</td>";
            str += "<td>";
            str += DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            str += "</td>";
            str += "</tr>";
            str += "<tr>";

            str += "<tr>";
            str += "<td>" + "User Created";
            str += "</td>";
            str += "<td>";
            str += HttpContext.Current.Session["userID"].ToString();
            str += "</td>";
            str += "</tr>";
            str += "<tr>";

            str += "<tr>";
            str += "<td colspan='2'>";
            str += "<br/>";
            str += "<hr/>";
            str += "<p><a href='https://www.scb.co.th/en/personal-banking.html' target='blank'></a></p>";
            str += "<span style='font-weight:bold;'>";
            str += "Thank you for using services of Siam Commercial Bank!";
            str += "</span>";
            str += "</td>";
            str += "</tr>";
            str += "</table>";
            string key = "MONEY_" + HttpContext.Current.Session["userID"].ToString() + txtTransactionNumber.Text;
            Cache.Insert(key, str);

        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message + " - " + Resources.labels.PrintFailedToCreate;
        }
    }

    protected void btnAccept_click(object sender, EventArgs e)
    {
        try
        {
            defaultColor();
            #region MyRegion
            if (rptData.Items.Count < 1)
            {
                lblError.Text = "No credit, debit side yet";
                return;
            }
            #endregion
            DataSet ds = new DataSet();
            Dictionary<object, object> inforCreateMoney = new Dictionary<object, object>();
            string debitAcnoList = string.Empty;
            string creditAcnoList = string.Empty;
            string debitAmountList = string.Empty;
            string creditAmountList = string.Empty;
            double totalDebit = 0;
            double totalCredit = 0;
            foreach (RepeaterItem item in rptData.Items)
            {
                if ((item.FindControl("txtSide") as Label).Text.Trim() == "Debit")
                {
                    totalDebit = totalDebit + Double.Parse((item.FindControl("txtAmount") as Label).Text.Replace(",", "").Trim());
                    debitAcnoList = debitAcnoList + (item.FindControl("txtACCNO") as Label).Text.Trim() + "-";
                    debitAmountList = debitAmountList + (item.FindControl("txtAmount") as Label).Text.Replace(",", "").Trim() + "-";
                }
                else
                {
                    totalCredit = totalCredit + Double.Parse((item.FindControl("txtAmount") as Label).Text.Replace(",", "").Trim());
                    creditAcnoList = creditAcnoList + (item.FindControl("txtACCNO") as Label).Text.Trim() + "-";
                    creditAmountList = creditAmountList + (item.FindControl("txtAmount") as Label).Text.Replace(",", "").Trim() + "-";
                }
            }
            debitAcnoList = debitAcnoList.Substring(0, debitAcnoList.Length - 1);
            debitAmountList = debitAmountList.Substring(0, debitAmountList.Length - 1);
            creditAcnoList = creditAcnoList.Substring(0, creditAcnoList.Length - 1);
            creditAmountList = creditAmountList.Substring(0, creditAmountList.Length - 1);
            if (totalCredit != totalDebit)
            {
                lblError.Text = "Total Credit amount is not equal to total Debit amount";
                txtDebitAmt.BorderColor = System.Drawing.Color.Red;
                txtCreditAmt.BorderColor = System.Drawing.Color.Red;
                return;
            }
            setPara(inforCreateMoney, debitAcnoList, creditAcnoList, debitAmountList, creditAmountList);
            ds = _service.CallStore("SEMS_CREATE_EMONEY", inforCreateMoney, "Create EMoney", "N", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtTransactionNumber.Text = ds.Tables[0].Rows[0]["TransactionID"].ToString();
                    txtTransactionDate.Text = ds.Tables[0].Rows[0]["TransactonDate"].ToString();
                    lblError.Text = Resources.labels.CreateEMoneySuccessfully;
                    pnAdd.Enabled = false;
                    btsave.Enabled = false;
                    btnPrint.Enabled = true;
                    PrintVoucher();
                }
            }
            else
            {
                lblError.Text = IPCERRORDESC;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    protected void btnClear_click(object sender, EventArgs e)
    {
        setControlDefault();
        defaultColor();
        pnAdd.Enabled = true;
        btsave.Enabled = true;
        btnPrint.Enabled = false;
        btnAddCredit.Enabled = true;
        ddlType.Enabled = false;
        txtCreAccName.Enabled = false;
        txtDebitAccName.Enabled = false;
        //List<ItemDebitCredit> dataList = new List<ItemDebitCredit>();
        rptData.DataSource = null;
        rptData.DataBind();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }

    protected void rptData_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
    {
        string commandName = e.CommandName;
        string commandArg = e.CommandArgument.ToString();
        switch (commandName)
        {
            case IPC.ACTIONPAGE.DELETE:
                BindData(commandArg);
                break;
        }
    }

    void BindData(string id)
    {
        try
        {
            int i = 1;
            List<ItemDebitCredit> dataList = new List<ItemDebitCredit>();
            //-- add all existing values to a list
            foreach (RepeaterItem item in rptData.Items)
            {
                if ((item.FindControl("linkID") as LinkButton).CommandArgument.Equals(id)) continue;
                dataList.Add(
                                new ItemDebitCredit()
                                {
                                    Id = i,
                                    Side = (item.FindControl("txtSide") as Label).Text,
                                    AccNo = (item.FindControl("txtACCNO") as Label).Text,
                                    Amount = (item.FindControl("txtAmount") as Label).Text,
                                    Aacname = (item.FindControl("txtACNAME") as Label).Text
                                }); ;
                i++;
            }

            rptData.DataSource = dataList;
            rptData.DataBind();


        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    private bool checkvalidateCreditside()
    {
        defaultColor();
        #region MyRegion
        if (txtCreditAccNo.Text.Equals(""))
        {
            lblError.Text = Resources.labels.CreditSide + " " + Resources.labels.accountnumber + " can not be null";
            txtCreditAccNo.BorderColor = System.Drawing.Color.Red;
            txtCreditAccNo.Focus();
            return false;
        }
        if (txtCreAccName.Text.Equals(""))
        {
            lblError.Text = Resources.labels.CreditSide + " " + Resources.labels.accountname + " can not be null";
            txtCreAccName.BorderColor = System.Drawing.Color.Red;
            txtCreAccName.Focus();
            return false;
        }
        if (txtCreditAmt.Text.Equals(""))
        {
            lblError.Text = Resources.labels.CreditSide + " " + Resources.labels.amount + " can not be null";
            txtCreditAmt.BorderColor = System.Drawing.Color.Red;
            txtCreditAmt.Focus();
            return false;
        }
        if (double.Parse(txtCreditAmt.Text) <= 0)
        {
            lblError.Text = Resources.labels.CreditSide + " " + Resources.labels.amount + Resources.labels.mustbegreaterthan0;
            txtCreditAmt.BorderColor = System.Drawing.Color.Red;
            txtCreditAmt.Focus();
            return false;
        }


        return true;
        #endregion
    }
    private bool checkvalidateDebitside()
    {
        defaultColor();
        #region MyRegion
        if (txtDebitAccNo.Text.Equals(""))
        {
            lblError.Text = Resources.labels.DebitSide + " " + Resources.labels.accountnumber + " can not be null";
            txtDebitAccNo.BorderColor = System.Drawing.Color.Red;
            txtDebitAccNo.Focus();
            return false;
        }
        if (txtDebitAccName.Text.Equals(""))
        {
            lblError.Text = Resources.labels.DebitSide + " " + Resources.labels.accountname + " can not be null";
            txtDebitAccName.BorderColor = System.Drawing.Color.Red;
            txtDebitAccName.Focus();
            return false;
        }
        if (txtDebitAmt.Text.Equals(""))
        {
            lblError.Text = Resources.labels.DebitSide + " " + Resources.labels.amount + " can not be null";
            txtDebitAmt.BorderColor = System.Drawing.Color.Red;
            txtDebitAmt.Focus();
            return false;
        }
        if (double.Parse(txtDebitAmt.Text) <= 0)
        {
            lblError.Text = Resources.labels.DebitSide + " " + Resources.labels.amount + Resources.labels.mustbegreaterthan0;
            txtDebitAmt.BorderColor = System.Drawing.Color.Red;
            txtDebitAmt.Focus();
            return false;
        }

        return true;
        #endregion
    }

    protected void btnAddCredit_Click(object sender, EventArgs e)
    {
        try
        {
            if (!checkvalidateCreditside()) { return; }

            int i = 1;
            List<ItemDebitCredit> dataList = new List<ItemDebitCredit>();
            //-- add all existing values to a list
            foreach (RepeaterItem item in rptData.Items)
            {
                dataList.Add(
                                new ItemDebitCredit()
                                {
                                    Id = i,
                                    Side = (item.FindControl("txtSide") as Label).Text,
                                    AccNo = (item.FindControl("txtACCNO") as Label).Text,
                                    Amount = (item.FindControl("txtAmount") as Label).Text,
                                    Aacname = (item.FindControl("txtACNAME") as Label).Text
                                }); ;
                i++;
            }
            if (dataList.Where(s => s != null && s.Side.Equals("Debit")).Count() > 1 && dataList.Where(s => s != null && s.Side.Equals("Credit")).Count() == 1 )
            {
                lblError.Text = "Can not add more " + Resources.labels.CreditSide;
                return;
            }
            //-- add a blank row to list to show a new row added
            dataList.Add(new ItemDebitCredit()
            {
                Id = i,
                Side = "Credit",
                AccNo = txtCreditAccNo.Text,
                Amount = txtCreditAmt.Text,
                Aacname = txtCreAccName.Text
            });

            rptData.DataSource = dataList;
            rptData.DataBind();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    protected void btnAddDebit_Click(object sender, EventArgs e)
    {
        try
        {
            if (!checkvalidateDebitside()) { return; }
            int i = 1;
            List<ItemDebitCredit> dataList = new List<ItemDebitCredit>();
            //-- add all existing values to a list
            foreach (RepeaterItem item in rptData.Items)
            {
                dataList.Add(
                                new ItemDebitCredit()
                                {
                                    Id = i,
                                    Side = (item.FindControl("txtSide") as Label).Text,
                                    AccNo = (item.FindControl("txtACCNO") as Label).Text,
                                    Amount = (item.FindControl("txtAmount") as Label).Text,
                                    Aacname = (item.FindControl("txtACNAME") as Label).Text
                                }); ;
                i++;
            }
            if (dataList.Where(s => s != null && s.Side.Equals("Debit")).Count() == 1 && dataList.Where(s => s != null && s.Side.Equals("Credit")).Count() > 1)
            {
                lblError.Text = "Can not add more " + Resources.labels.DebitSide;
                return;
            }
            //-- add a blank row to list to show a new row added
            dataList.Add(new ItemDebitCredit()
            {
                Id = i,
                Side = "Debit",
                AccNo = txtDebitAccNo.Text,
                Amount = txtDebitAmt.Text,
                Aacname = txtDebitAccName.Text
            });

            rptData.DataSource = dataList;
            rptData.DataBind();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    protected void txtDebitAccNo_TextChanged(object sender, EventArgs e)
    {
        defaultColor();
        try
        {
            DataSet ds = new DataSet();
            object[] inforConsumer = new object[] { Utility.KillSqlInjection(txtDebitAccNo.Text) };
            ds = _service.common("SEMS_ACC_GET_ACCHART", inforConsumer, ref IPCERRORCODE, ref IPCERRORDESC);

            if (IPCERRORCODE == "0")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtDebitAccName.Text = ds.Tables[0].Rows[0]["ACName"].ToString();
                    txtDebitCurrency.Text = ds.Tables[0].Rows[0]["CurrencyID"].ToString();
                }
                else
                {
                    txtDebitAccName.Text = string.Empty;
                    txtDebitCurrency.Text = string.Empty;
                }
            }
            else
            {
                lblError.Text = IPCERRORDESC;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
        if (txtDebitAccName.Text == string.Empty)
        {
            lblError.Text = Resources.labels.accountnumber + " dose not exist or is not level 3 GL";
            txtDebitAccNo.BorderColor = System.Drawing.Color.Red;
            txtDebitAccNo.Focus();
        }
    }

    protected void txtCreditAccNo_TextChanged(object sender, EventArgs e)
    {
        defaultColor();
        try
        {
            DataSet ds = new DataSet();
            object[] inforConsumer = new object[] { Utility.KillSqlInjection(txtCreditAccNo.Text) };
            ds = _service.common("SEMS_ACC_GET_ACCHART", inforConsumer, ref IPCERRORCODE, ref IPCERRORDESC);

            if (IPCERRORCODE == "0")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtCreAccName.Text = ds.Tables[0].Rows[0]["ACName"].ToString();
                    txtCreditCurrency.Text = ds.Tables[0].Rows[0]["CurrencyID"].ToString();
                }
                else
                {
                    txtCreAccName.Text = string.Empty;
                    txtCreditCurrency.Text = string.Empty;
                }
            }
            else
            {
                lblError.Text = IPCERRORDESC;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
        if (txtCreAccName.Text == string.Empty)
        {
            lblError.Text = Resources.labels.accountnumber + " does not exist or is not level 3 GL ";
            txtCreditAccNo.BorderColor = System.Drawing.Color.Red;
            txtCreditAccNo.Focus();
        }
    }
    public class ItemDebitCredit
    {
        public int Id { get; set; }
        public string Side { get; set; }
        public string AccNo { get; set; }
        public string Amount { get; set; }
        public string Aacname { get; set; }
    }
}