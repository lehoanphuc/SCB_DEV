using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;

public partial class Widgets_SEMSREGIONFEE_Controls_Widget : WidgetBase
{
    public static bool isAscend = false;
    string ACTION = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();
    public string _TITLE
    {
        get { return lblTitleBranch.Text; }
        set { lblTitleBranch.Text = value; }
    }
    public class ShareFeeDTL
    {
        public string FeeShareTypeID { get; set; }
        public decimal From { get; set; }
        public decimal To { get; set; }
        public string Benside { get; set; }
        public string Sharetype { get; set; }
        public decimal Percentage { get; set; }
        public decimal FlatAmount { get; set; }
        public string Priority { get; set; }

    }

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {

            chbIsLadder.Enabled = false;
            ddlTransactionType.Enabled = false;
            txtFlatAmount.Attributes.Add("onkeyup", "executeComma('" + txtFlatAmount.ClientID + "')");
            txtFrom.Attributes.Add("onkeyup", "executeComma('" + txtFrom.ClientID + "')");
            txtTo.Attributes.Add("onkeyup", "executeComma('" + txtTo.ClientID + "')");
            ACTION = GetActionPage();
            ViewState["ShareFeeTypeId"] = string.Empty; ;
            if (!IsPostBack)
            {
                loadCombobox();
                loadShareSide(sender, e);
                BindData();

            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    private void loadCombobox_Biller()
    {
        try
        {
            DataSet ds = new DataSet();
            object[] searchObject = new object[] { ddlFeeShareCode.SelectedValue };
            ds = _service.common("SEMSBLLERBYFEEID", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlBillerID.DataSource = ds;
                        ddlBillerID.DataValueField = "BILLERID";
                        ddlBillerID.DataTextField = "BILLERNAME";
                        ddlBillerID.DataBind();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void loadCombobox_FeeCode()
    {
        try
        {
            DataSet ds = new DataSet();
            object[] searchObject = new object[] { };
            ds = _service.common("SEMSGETSHAREFEELIST", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlFeeShareCode.DataSource = ds;
                        ddlFeeShareCode.DataValueField = "FeeShareID";
                        ddlFeeShareCode.DataTextField = "FeeShareCode";
                        ddlFeeShareCode.DataBind();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void loadCombobox_BeneficiarySideBiller()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = _service.GetValueList("BenSide", "FeeShareBiller", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlBeneficiarySide.DataSource = ds;
                    ddlBeneficiarySide.DataValueField = "VALUEID";
                    ddlBeneficiarySide.DataTextField = "CAPTION";
                    ddlBeneficiarySide.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void loadCombobox_BeneficiarySideNormal()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = _service.GetValueList("BEN_SIDE", "WAL_FEESHARE", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlBeneficiarySide.DataSource = ds;
                    ddlBeneficiarySide.DataValueField = "VALUEID";
                    ddlBeneficiarySide.DataTextField = "CAPTION";
                    ddlBeneficiarySide.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void loadCombobox_ShareType()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = _service.GetValueList("SHARE_TYPE", "WAL_FEESHARE", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlShareType.DataSource = ds;
                    ddlShareType.DataValueField = "VALUEID";
                    ddlShareType.DataTextField = "CAPTION";
                    ddlShareType.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void loadCombobox_TransactionType()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = _service.GetValueList("TransactionType", "WAL_SHAREFEE", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlTransactionType.DataSource = ds;
                    ddlTransactionType.DataValueField = "VALUEID";
                    ddlTransactionType.DataTextField = "CAPTION";
                    ddlTransactionType.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    private void loadCombobox()
    {
        loadCombobox_FeeCode();
        loadCombobox_Biller();
        loadCombobox_BeneficiarySideNormal();
        loadCombobox_ShareType();
        loadCombobox_TransactionType();


    }
    public void loadEditAndViewData(string sharefeeTypeID)
    {
        try
        {

            ddlBeneficiarySide.Enabled = false;
            ddlShareType.Enabled = false;
            string FeeshareTypeId = sharefeeTypeID.ToString().Split('+')[0].ToString();
            decimal from = 0;
            if (!sharefeeTypeID.ToString().Split('+')[1].ToString().Replace(",", "").Equals(""))
                from = decimal.Parse(sharefeeTypeID.ToString().Split('+')[1].ToString().Replace(",", ""));

            string to = 0.ToString();
            if (!sharefeeTypeID.ToString().Split('+')[2].ToString().Replace(",", "").Equals(""))
                to = sharefeeTypeID.ToString().Split('+')[2].ToString().Replace(",", "");
            if (to == "Unlimit")
            {
                to = (-1).ToString();
            }

            DataSet ds = new DataSet();
            object[] searchObject = new object[] { Utility.KillSqlInjection(FeeshareTypeId), from, decimal.Parse(to) };
            ds = _service.common("SEMSSHAREFEEDTLVIEW", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE.Equals("0"))
            {
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow row = ds.Tables[0].Rows[0];
                            lbFeeShareType.Text = row["FeeShareTypeID"].ToString();
                            ddlFeeShareCode.SelectedValue = row["FeeShareID"].ToString();
                            loadCombobox_Biller();
                            ddlBillerID.SelectedValue = row["BillerID"].ToString();
                            if (row["IsLadder"].ToString().Equals("1"))
                            {
                                chbIsLadder.Checked = true;
                                txtFrom.Enabled = true;
                                txtTo.Enabled = true;
                            }
                            else
                            {
                                chbIsLadder.Checked = false;
                                txtFrom.Enabled = false;
                                txtTo.Enabled = false;
                            }
                            ddlTransactionType.SelectedValue = row["TransactionType"].ToString();
                            if (ddlTransactionType.SelectedValue.ToString().Equals("B"))
                                loadCombobox_BeneficiarySideBiller();
                            else
                                loadCombobox_BeneficiarySideNormal();

                            if (!row["FeeShareTypeIDDTL"].ToString().Equals(""))
                            {
                                rptData.DataSource = ds;
                                rptData.DataBind();

                            }

                        }
                    }
                    else
                    {
                        lblError.Text = "Fee code " + ddlFeeShareCode.Text + " with this biller be not exists";
                        return;
                    }
                }
                else
                {
                    lblError.Text = "Fee code " + ddlFeeShareCode.Text + " with this biller be not exists";
                    return;
                }
            }
            else
            {
                lblError.Text = IPCERRORDESC;
                return;
            }

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    public void loadDataTableAdd()
    {
        try
        {
            DataSet ds = new DataSet();
            object[] searchObject = new object[] { Utility.KillSqlInjection(ddlFeeShareCode.SelectedValue), Utility.KillSqlInjection(ddlBillerID.SelectedValue) };
            ds = _service.common("SEMSINFOSHAREFEEDTL", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);

            if (IPCERRORCODE == "0" || IPCERRORCODE.Equals(""))
            {
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (!ds.Tables[0].Rows[0]["FeeShareTypeIDDTL"].ToString().Equals(""))
                            {
                                rptData.DataSource = ds;
                                rptData.DataBind();
                            }
                            else
                            {
                                rptData.DataSource = null;
                                rptData.DataBind();
                            }

                        }
                    }
                }

            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    public void loadDataTableEdit()
    {
        try
        {
            string FeeShareType = GetParamsPage(IPC.ID)[0].Trim();
            string FeeshareTypeId = FeeShareType.ToString().Split('+')[0].ToString();
            decimal from = 0;
            if (!FeeShareType.ToString().Split('+')[1].ToString().Replace(",", "").Equals(""))
                from = decimal.Parse(FeeShareType.ToString().Split('+')[1].ToString().Replace(",", ""));

            string to = 0.ToString();
            if (!FeeShareType.ToString().Split('+')[2].ToString().Replace(",", "").Equals(""))
                to = FeeShareType.ToString().Split('+')[2].ToString().Replace(",", "");
            if (to == "Unlimit")
            {
                to = (-1).ToString();
            }

            DataSet ds = new DataSet();
            object[] searchObject = new object[] { Utility.KillSqlInjection(FeeshareTypeId), from, decimal.Parse(to) };
            ds = _service.common("SEMSSHAREFEEDTLVIEW", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);

            if (IPCERRORCODE == "0" || IPCERRORCODE.Equals(""))
            {
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (!ds.Tables[0].Rows[0]["FeeShareTypeIDDTL"].ToString().Equals(""))
                            {
                                rptData.DataSource = ds;
                                rptData.DataBind();
                            }
                            else
                            {
                                rptData.DataSource = null;
                                rptData.DataBind();
                            }

                        }
                        else
                        {
                            rptData.DataSource = null;
                            rptData.DataBind();
                        }
                    }
                }

            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    void BindData()
    {
        try
        {
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    pnFee1.Enabled = true;
                    pnFeeShareDetail.Visible = false;
                    pnFeeShareDetail.Enabled = false;
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    string FeeShareType = GetParamsPage(IPC.ID)[0].Trim();
                    pnRegion.Visible = false;
                    //lblError.Text = "Choose Beneficiary Side to edit";
                    btsave.Enabled = false;
                    btClear.Enabled = false;
                    loadEditAndViewData(FeeShareType);
                    pnFee1.Enabled = false;
                    pnFeeShareDetail.Visible = true;
                    pnFeeShareDetail.Enabled = true;
                    break;
                case IPC.ACTIONPAGE.DETAILS:
                    string FeeShareTypeDetail = GetParamsPage(IPC.ID)[0].Trim();
                    btsave.Visible = false;
                    pnFee1.Enabled = false;
                    btClear.Visible = false;
                    pnRegion.Enabled = false;
                    pnFeeShareDetail.Visible = true;
                    loadEditAndViewData(FeeShareTypeDetail);
                    break;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    private bool checkvalidate()
    {
        setDefault();
        if (lbFeeShareType.Text.Equals(""))
        {
            lblError.Text = "Please Choose Fee share Information before perform action.";
            return false;
        }
        double from = 0;
        double to = 0;
        double percentage = 0;
        double fixAmt = 0;
        from = txtFrom.Text.ToString().Equals("") ? 0 : double.Parse(txtFrom.Text.ToString());
        if (txtTo.Text.ToString().Equals("Unlimit"))
        {
            to = -1;
        }
        else
        {
            to = txtTo.Text.ToString().Equals("") ? 0 : double.Parse(txtTo.Text.ToString());
        }
        percentage = txtPercentage.Text.ToString().Equals("") ? 0 : double.Parse(txtPercentage.Text.ToString());
        fixAmt = txtFlatAmount.Text.ToString().Equals("") ? 0 : double.Parse(txtFlatAmount.Text.ToString());

        if (chbIsLadder.Checked)
        {
            if (txtFrom.Text.Equals(""))
            {
                lblError.Text = Resources.labels.tu + " not null.";
                txtFrom.BorderColor = System.Drawing.Color.Red;
                txtFrom.Focus();
                return false;
            }
            if (txtTo.Text.Equals("") && !cbToLimit.Checked)
            {
                lblError.Text = Resources.labels.den + " not null.";
                txtTo.BorderColor = System.Drawing.Color.Red;
                txtTo.Focus();
                return false;
            }
            if (from >= to && !cbToLimit.Checked)
            {
                lblError.Text = Resources.labels.to + " amount must be greater than " + Resources.labels.from + " amount";
                txtTo.BorderColor = System.Drawing.Color.Red;
                txtTo.Focus();
                txtFrom.BorderColor = System.Drawing.Color.Red;
                txtFrom.Focus();
                return false;
            }
        }

        #region validate

        if (ddlShareType.SelectedValue.Equals("PER"))
        {
            if (percentage > 100)
            {
                lblError.Text = Resources.labels.Percentage + " do not allow greater than 100%";
                txtPercentage.BorderColor = System.Drawing.Color.Red;
                txtPercentage.Focus();
                return false;
            }
            if (percentage <= 0)
            {
                lblError.Text = Resources.labels.Percentage + " do not allow equal or below 0%";
                txtPercentage.BorderColor = System.Drawing.Color.Red;
                txtPercentage.Focus();
                return false;
            }
        }
        if (ddlShareType.SelectedValue.Equals("FIX") & fixAmt < 1)
        {
            lblError.Text = Resources.labels.FlatAmount + " not valid.";
            txtFlatAmount.BorderColor = System.Drawing.Color.Red;
            txtFlatAmount.Focus();
            return false;
        }
        if (txtPriority.Text.Trim().Equals(""))
        {
            lblError.Text = Resources.labels.Priority + " not null.";
            txtPriority.BorderColor = System.Drawing.Color.Red;
            txtPriority.Focus();
            return false;
        }
        return true;
        #endregion
    }

    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {
            if (!checkvalidate()) { return; }
            double percentage, flatAmount, from, to = 0;
            percentage = txtPercentage.Text.ToString().Equals("") ? 0 : double.Parse(txtPercentage.Text.ToString());
            flatAmount = txtFlatAmount.Text.ToString().Equals("") ? 0 : double.Parse(txtFlatAmount.Text.ToString());
            from = txtFrom.Text.ToString().Equals("") ? 0 : double.Parse(txtFrom.Text.ToString());
            if (cbToLimit.Checked)
            {
                to = -1;
            }
            else
            {
                to = txtTo.Text.ToString().Equals("") ? 0 : double.Parse(txtTo.Text.ToString());
            }
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    if (!CheckPermitPageAction(IPC.ACTIONPAGE.ADD))
                    {
                        lblError.Text = "User does not have permission.";
                        return;
                    }
                    try
                    {
                        DataSet ds = new DataSet();
                        object[] searchObject = new object[] {  Utility.KillSqlInjection(lbFeeShareType.Text), from, to, ddlShareType.SelectedValue, ddlBeneficiarySide.SelectedValue,
                            flatAmount, (percentage/100), Utility.KillSqlInjection (txtPriority.Text.Trim())};
                        ds = _service.common("SEMSSHAREFEEDTLADD", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE == "0")
                        {
                            loadDataTableAdd();

                            setDefault();
                            lblError.Text = Resources.labels.addsuccessfully;
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
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    if (!CheckPermitPageAction(IPC.ACTIONPAGE.EDIT))
                    {
                        lblError.Text = "User does not have permission.";
                        return;
                    }
                    try
                    {
                        DataSet ds = new DataSet();
                        object[] searchObject = new object[] {  Utility.KillSqlInjection(lbFeeShareType.Text), from, to, ddlShareType.SelectedValue, ddlBeneficiarySide.SelectedValue,
                            flatAmount, (percentage/100), Utility.KillSqlInjection (txtPriority.Text.Trim())};
                        ds = _service.common("SEMSSHAREFEEDTLEDIT", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE == "0")
                        {
                            loadDataTableEdit();

                            //setDefault();
                            lblError.Text = Resources.labels.updatesuccessfully;
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
                    break;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
        btsave.Enabled = true;
    }


    protected void btback_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }
    public void setDefault()
    {
        txtFrom.BorderColor = System.Drawing.Color.Empty;
        txtTo.BorderColor = System.Drawing.Color.Empty;
        txtFlatAmount.BorderColor = System.Drawing.Color.Empty;
        txtPercentage.BorderColor = System.Drawing.Color.Empty;

        txtPriority.BorderColor = System.Drawing.Color.Empty;
        lblError.Text = string.Empty;
    }

    protected void loadShareSide(object sender, EventArgs e)
    {
        txtFlatAmount.BorderColor = System.Drawing.Color.Empty;
        if (ddlShareType.SelectedValue == "PER")
        {
            txtPercentage.Enabled = true;
            txtFlatAmount.Enabled = false;
            txtFlatAmount.Text = string.Empty;
        }
        if (ddlShareType.SelectedValue == "FIX")
        {
            txtPercentage.Text = string.Empty;
            txtFlatAmount.Enabled = true;
            txtPercentage.Enabled = false;
        }
    }

    protected void btClear_Click(object sender, EventArgs e)
    {
        setDefault();
        ACTION = GetActionPage();
        if (ACTION == IPC.ACTIONPAGE.ADD)
        {
            pnFee1.Enabled = true;
            pnFeeShareDetail.Enabled = false;
            pnFeeShareDetail.Visible = false;
            ddlBeneficiarySide.Enabled = true;
            ddlShareType.Enabled = true;
            rptData.DataSource = null;
            rptData.DataBind();
        }
        if (ACTION == IPC.ACTIONPAGE.EDIT)
        {

            ddlBeneficiarySide.Enabled = false;
            ddlShareType.Enabled = false;
        }
        if (ACTION == IPC.ACTIONPAGE.DETAILS)
        {

            btback.Enabled = true;
        }

        txtPriority.Text = string.Empty;
        btsave.Enabled = true;

        if (ddlShareType.SelectedValue.Equals("PER"))
        {
            txtPercentage.Text = string.Empty;
        }
        if (ddlShareType.SelectedValue.Equals("FIX"))
        {
            txtFlatAmount.Text = string.Empty;
        }
    }

    protected void rptData_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string commandName = e.CommandName;
        string commandArg = e.CommandArgument.ToString();
        if (CheckPermitPageAction(commandName))
        {
            string FeeShareTypeID = commandArg.ToString().Split('+')[0].ToString();
            string FromLimit = commandArg.ToString().Split('+')[1].ToString();
            string ToLimit = commandArg.ToString().Split('+')[2].ToString();
            string ShareType = commandArg.ToString().Split('+')[3].ToString();
            string BenSide = commandArg.ToString().Split('+')[4].ToString();
            lblError.Text = string.Empty;
            switch (commandName)
            {
                case "edit":
                    pnRegion.Enabled = true;
                    lblError.Text = string.Empty;
                    //lblError.ForeColor = System.Drawing.Color.Red;
                    pnRegion.Visible = true;
                    btsave.Enabled = true;
                    btClear.Enabled = true;
                    loadEditFeeShareType(FeeShareTypeID, FromLimit, ToLimit, ShareType, BenSide);
                    break;
                case "delete":
                    deletefeeShare(FeeShareTypeID, FromLimit, ToLimit, ShareType, BenSide);
                    ACTION = GetActionPage();
                    if (ACTION == IPC.ACTIONPAGE.ADD)
                    { loadDataTableAdd(); }
                    else
                    {
                        loadDataTableEdit();
                        btsave.Enabled = false;
                        btClear.Enabled = false;
                        pnRegion.Visible = false;
                    }
                    break;
            }
        }
        else
        {
            lblError.Text = "User does not have permission.";
            return;
        }
    }

    public void loadEditFeeShareType(string FeeShareTypeID, string FromLimit, string ToLimit, string ShareType, string BenSide)
    {
        try
        {
            DataSet ds = new DataSet();
            if (ToLimit == "Unlimit")
            {
                ToLimit = (-1).ToString();
                cbToLimit.Checked = true;
            }
            object[] searchObject = new object[] { Utility.KillSqlInjection(FeeShareTypeID), Utility.KillSqlInjection(FromLimit.Replace(",","")),
                Utility.KillSqlInjection(ToLimit.Replace(",","")),  Utility.KillSqlInjection(ShareType), Utility.KillSqlInjection(BenSide)  };
            ds = _service.common("SEMSSHAREFEEDTLFIID", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE.Equals("0"))
            {
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = ds.Tables[0].Rows[0];
                        lbFeeShareType.Text = row["FeeShareTypeID"].ToString();
                        txtFrom.Text = row["FromLimit"].ToString();
                        txtTo.Text = row["ToLimit"].ToString();
                        ddlBeneficiarySide.SelectedValue = row["BenSide"].ToString();
                        ddlShareType.SelectedValue = row["ShareType"].ToString();
                        txtPercentage.Text = row["Percentage"].ToString();
                        //txtFlatAmount.Text = row["FlatAmount"].ToString();
                        txtFlatAmount.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(row["FlatAmount"].ToString(),"LAK");
                        txtPriority.Text = row["Priority"].ToString();

                        txtFrom.Enabled = false;
                        txtTo.Enabled = false;
                        cbToLimit.Enabled = false;
                        if (ddlShareType.SelectedValue == "FIX")
                        {
                            txtFlatAmount.Enabled = true;
                            txtPercentage.Enabled = false;
                        }
                        else
                        {
                            txtFlatAmount.Enabled = false;
                            txtPercentage.Enabled = true;
                        }
                    }
                }
            }
            else
            {
                lblError.Text = IPCERRORDESC;
                return;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    
    public void deletefeeShare(string FeeShareTypeID, string FromLimit, string ToLimit, string ShareType, string BenSide)
    {
        try
        {
            if (ToLimit == "Unlimit")
            {
                ToLimit = (-1).ToString();
            }
            DataSet ds = new DataSet();
            object[] searchObject = new object[] { Utility.KillSqlInjection(FeeShareTypeID), Utility.KillSqlInjection(FromLimit.Replace(",","")),
                Utility.KillSqlInjection(ToLimit.Replace(",","")),  Utility.KillSqlInjection(ShareType), Utility.KillSqlInjection(BenSide)  };
            ds = _service.common("SEMSSHAREFEEDTLDEL", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE.Equals("0"))
            {
                lblError.Text = Resources.labels.deletesuccessfully;
                ACTION = GetActionPage();
                if (ACTION == IPC.ACTIONPAGE.ADD)
                    loadDataTableAdd();
                else
                    loadDataTableEdit();
            }
            else
            {
                lblError.Text = IPCERRORDESC;
                return;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void rptData_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                LinkButton lbEdit, lbDelete;
                //Label thedit, thdelete, tdedit, tddelete, test;

                if (ACTION == IPC.ACTIONPAGE.DETAILS)
                {
                    lbEdit = (LinkButton)e.Item.FindControl("lbEdit");
                    lbEdit.Enabled = false;
                    lbEdit.CssClass = "btn btn-primary";
                    lbEdit.OnClientClick = null;

                    lbDelete = (LinkButton)e.Item.FindControl("lbDelete");
                    lbDelete.Enabled = false;
                    lbDelete.CssClass = "btn btn-secondary";
                    lbDelete.OnClientClick = null;

                    //var col = e.Item.FindControl("thedit");
                    //test.Visible = false;
                }
                if (ACTION == IPC.ACTIONPAGE.EDIT)
                {

                }
                if (ACTION == IPC.ACTIONPAGE.ADD)
                {
                    lbEdit = (LinkButton)e.Item.FindControl("lbEdit");
                    lbEdit.Enabled = false;
                    lbEdit.CssClass = "btn btn-primary";
                    lbEdit.OnClientClick = null;
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadCombobox_Biller();
    }

    protected void txtChoose_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            object[] searchObject = new object[] { Utility.KillSqlInjection(ddlFeeShareCode.SelectedValue), Utility.KillSqlInjection(ddlBillerID.SelectedValue) };
            ds = _service.common("SEMSINFOSHAREFEEDTL", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE.Equals("0"))
            {
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow row = ds.Tables[0].Rows[0];
                            lbFeeShareType.Text = row["FeeShareTypeID"].ToString();
                            if (cbToLimit.Checked)
                            {
                                cbToLimit.Checked = false;
                                txtTo.Text = 0.ToString();
                            }

                            if (row["IsLadder"].ToString().Equals("1"))
                            {
                                chbIsLadder.Checked = true;
                                txtFrom.Enabled = true;
                                txtTo.Enabled = true;
                                cbToLimit.Enabled = true;
                            }
                            else
                            {
                                chbIsLadder.Checked = false;
                                txtFrom.Enabled = false;
                                txtTo.Enabled = false;
                                cbToLimit.Enabled = false;
                            }
                            ddlTransactionType.SelectedValue = row["TransactionType"].ToString();
                            if (ddlTransactionType.SelectedValue.ToString().Equals("B"))
                                loadCombobox_BeneficiarySideBiller();
                            else
                                loadCombobox_BeneficiarySideNormal();

                            if (!row["FeeShareTypeIDDTL"].ToString().Equals(""))
                            {
                                rptData.DataSource = ds;
                                rptData.DataBind();

                            }

                            pnFee1.Enabled = false;
                            pnFeeShareDetail.Visible = true;
                            pnFeeShareDetail.Enabled = true;

                        }
                    }
                    else
                    {
                        lblError.Text = "Fee code " + ddlFeeShareCode.Text + " with this biller be not exists";
                        return;
                    }
                }
                else
                {
                    lblError.Text = "Fee code " + ddlFeeShareCode.Text + " with this biller be not exists";
                    return;
                }
            }
            else
            {
                lblError.Text = IPCERRORDESC;
                return;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }


    public List<ShareFeeDTL> getListShareFee()
    {
        List<ShareFeeDTL> dataList = new List<ShareFeeDTL>();
        foreach (RepeaterItem item in rptData.Items)
        {
            dataList.Add(
                            new ShareFeeDTL()
                            {
                                FeeShareTypeID = txtChoose.Text,
                                From = decimal.Parse((item.FindControl("txtFromLimittb") as Label).Text.ToString().Replace(",", "")),
                                To = decimal.Parse((item.FindControl("txtToLimittb") as Label).Text.ToString().Replace(",", "")),
                                Benside = (item.FindControl("txtBenSidetb") as Label).Text,
                                Sharetype = (item.FindControl("txtShareTypetb") as Label).Text,
                                Percentage = decimal.Parse((item.FindControl("txtPerrcentTb") as Label).Text.ToString().Replace(",", "")),
                                FlatAmount = decimal.Parse((item.FindControl("txtFlatAmounttb") as Label).Text.ToString().Replace(",", "")),
                                Priority = (item.FindControl("txtPriorityTb") as Label).Text
                            });

        }
        return dataList;
    }
    protected void cbToLimit_OnCheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (cbToLimit.Checked)
            {
                txtTo.Text = string.Empty;
                txtTo.Enabled = false;
                setDefault();
            }
            else
            {
                txtTo.Text = "0";
                txtTo.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
}
