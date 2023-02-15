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

public partial class Widgets_SEMSCustFXLimit_Control_Widget : WidgetBase
{
    string ACTION = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    string LISTFXPAGEID = "1098";
    public string _IMAGE
    {
        get { return imgLoGo.ImageUrl; }
        set { imgLoGo.ImageUrl = value; }
    }

    public string _TITLE
    {
        get { return lblTitleProduct.Text; }
        set { lblTitleProduct.Text = value; }
    }
    private string IDXRADIOSELECTED
    {
        get { return ViewState["IDXRADIOSELECTED"] == null ? string.Empty : ViewState["IDXRADIOSELECTED"].ToString(); }
        set { ViewState["IDXRADIOSELECTED"] = value; }
    }
    private DataTable TABLEADDFX
    {
        get { return ViewState["TABLEADDFX"] == null ? null : (DataTable)ViewState["TABLEADDFX"]; }
        set { ViewState["TABLEADDFX"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        lblError.Text = string.Empty;
        txtCountLimit.Attributes.Add("onkeyup", "executeComma2_checkdecimal('" + txtCountLimit.ClientID + "',event)");
        txtTotalLimitDay.Attributes.Add("onkeyup", "executeComma2('" + txtTotalLimitDay.ClientID + "',event)");
        txtlimit.Attributes.Add("onkeyup", "executeComma2('" + txtlimit.ClientID + "',event)");
        ACTION = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["a"].ToString().Trim();
        if (!IsPostBack)
        {
            //#region Load Transaction
            //DataSet dsTran = new SmartPortal.SEMS.Product().GetTranNameByTrancode(ref IPCERRORCODE, ref IPCERRORDESC);
            //dsTran.Tables[0].DefaultView.RowFilter = ("TranCode in ('IB000405','MB000103')");
            //ddlTrans.DataSource = dsTran.Tables[0];
            //if (IPCERRORCODE != "0")
            //{
            //    throw new IPCException(IPCERRORDESC);
            //}

            //ddlTrans.DataTextField = "PAGENAME";
            //ddlTrans.DataValueField = "TRANCODE";
            //ddlTrans.DataBind();
            ////ddlTrans.Items.Insert(0, new ListItem(SmartPortal.Constant.IPC.ALL, "ALL"));
            //#endregion
            DataTable dt = new SmartPortal.SEMS.Product().LoaddAllCCYID(ref IPCERRORCODE, ref IPCERRORDESC);
            #region Load From CCY
            string[] lstFrCCY = System.Configuration.ConfigurationManager.AppSettings["fxtranfer"].ToString().Split('|');
            string sFrCCy = "";
            foreach (string s in lstFrCCY)
            {
                sFrCCy += "'" + s.Split('#')[0].ToString() + "',";
            }
            if (sFrCCy.Length > 0) sFrCCy = sFrCCy.Substring(0, sFrCCy.Length - 1);
            dt.DefaultView.RowFilter= "CCYID in (" + sFrCCy + ")";
            ddlFromCCYID.DataSource = dt;
            ddlFromCCYID.DataTextField = "CCYID";
            ddlFromCCYID.DataValueField = "CCYID";
            ddlFromCCYID.DataBind();
            #endregion
            #region Load To CCY
            string[] lstToCCY = System.Configuration.ConfigurationManager.AppSettings["fxtranfer"].ToString().Split('|');
            string sToCCY = "";
            foreach (string s in lstToCCY)
            {
                string[] lsts = s.Split('#');
                foreach (string s1 in lsts[1].Split(','))
                {
                    sToCCY += "'" + s1.ToString() + "',";
                }
            }
            if (sToCCY.Length > 0) sToCCY = sToCCY.Substring(0, sToCCY.Length - 1);

            dt.DefaultView.RowFilter= "CCYID in (" + sToCCY + ")";
            ddlToCCYID.DataSource = dt.DefaultView;
            ddlToCCYID.DataTextField = "CCYID";
            ddlToCCYID.DataValueField = "CCYID";
            ddlToCCYID.DataBind();
            #endregion
            BindData();
        }
    }
    void BindData()
    {
        try
        {
            #region GetDetail FX TransactionLimit
            if (ACTION != "add")
            {
                string sContractNo = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["contract"].ToString().Trim();
                string sTranCode = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["trand"].ToString().Trim();
                string sFromCCY = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["fromccy"].ToString().Trim();
                string sToCCY = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["toccy"].ToString().Trim();
                DataSet Ds = new SmartPortal.SEMS.FxTransactionLimit().GetListFXTransactionLimitByCondition(sContractNo, "", sTranCode, sFromCCY, sToCCY, SmartPortal.Constant.IPC.ALL, ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE != "0")
                {
                    throw new IPCException(IPCERRORDESC);
                }
                if (Ds == null)
                {
                    lblError.Text = Resources.labels.datanotfound;
                    return;
                }
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    txtContractNo.Text = Ds.Tables[0].Rows[0]["CONTRACTNO"].ToString();
                    //ddlTrans.SelectedValue = Ds.Tables[0].Rows[0]["TRANDCODE"].ToString();
                    txtlimit.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(Ds.Tables[0].Rows[0]["TRANLIMIT"].ToString(), Ds.Tables[0].Rows[0]["FROMCCYID"].ToString());
                    txtCountLimit.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(Ds.Tables[0].Rows[0]["COUNTLIMIT"].ToString(),"").Replace(".00","");
                    txtTotalLimitDay.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(Ds.Tables[0].Rows[0]["TOTALLIMITDAY"].ToString(), Ds.Tables[0].Rows[0]["FROMCCYID"].ToString());
                    ddlFromCCYID.SelectedValue = Ds.Tables[0].Rows[0]["FROMCCYID"].ToString();
                    ddlToCCYID.SelectedValue = Ds.Tables[0].Rows[0]["TOCCYID"].ToString();
                    cbFX.Checked = Ds.Tables[0].Rows[0]["ALLOWFOREIGN"].ToString().Trim() == "Y" ? true : false;
                }
            }
            else
            {
                GetAllContractByCondition();
            }
            #endregion
            #region Enable/Disable theo Action
            switch (ACTION)
            {
                case "viewdetail":
                    //ddlTrans.Enabled = false;
                    txtlimit.Enabled = false;
                    txtTotalLimitDay.Enabled = false;
                    txtCountLimit.Enabled = false;
                    ddlFromCCYID.Enabled = false;
                    ddlToCCYID.Enabled = false;
                    btsave.Visible = false;
                    btnNext.Visible = false;
                    pnSearch.Visible = false;
                    btnAdd.Visible = false;
                    cbFX.Enabled = false;
                    break;
                case "edit":
                    //ddlTrans.Enabled = false;
                    txtlimit.Enabled = true;
                    txtTotalLimitDay.Enabled = true;
                    txtCountLimit.Enabled = true;
                    ddlFromCCYID.Enabled = false;
                    ddlToCCYID.Enabled = false;
                    btsave.Visible = true;
                    btnNext.Visible = false;
                    pnSearch.Visible = false;
                    btnAdd.Visible = false;
                    cbFX.Enabled = true;
                    break;
                case "add":
                    txtContractNo.Text = Session["contractNo"] != null ? Session["contractNo"].ToString() : "";
                    //ddlTrans.Enabled = true;
                    txtlimit.Enabled = true;
                    txtTotalLimitDay.Enabled = true;
                    txtCountLimit.Enabled = true;
                    ddlFromCCYID.Enabled = true;
                    ddlToCCYID.Enabled = true;
                    btsave.Visible = false;
                    btnNext.Visible = true;
                    pnAdd.Visible = false;
                    pnSearch.Visible = true;
                    btnAdd.Visible = true;
                    btback.Visible = false;
                    btbackHome.Visible = true;
                    cbFX.Enabled = true;
                    break;
            }
            #endregion
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
    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtFX = new DataTable();
            dtFX.Columns.Add("CONTRACTNO");
            dtFX.Columns.Add("TRANDCODE");
            dtFX.Columns.Add("FROMCCYID");
            dtFX.Columns.Add("TOCCYID");
            dtFX.Columns.Add("TRANLIMIT");
            dtFX.Columns.Add("COUNTLIMIT");
            dtFX.Columns.Add("TOTALLIMITDAY");
            dtFX.Columns.Add("USER");
            dtFX.Columns.Add("DATE");
            dtFX.Columns.Add("ALLOWFOREIGN");
            dtFX.PrimaryKey = new DataColumn[] { dtFX.Columns["CONTRACTNO"], dtFX.Columns["FROMCCYID"], dtFX.Columns["TOCCYID"] };
            DataRow row = dtFX.NewRow();
            switch (ACTION)
            {
                case "add":
                    #region Add
                    DataSet dsFXLm = new SmartPortal.SEMS.FxTransactionLimit().GetListFXTransactionLimitByCondition(SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtContractNo.Text.Trim()), "", "ALL", "ALL", "ALL", SmartPortal.Constant.IPC.ALL, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (TABLEADDFX != null)
                    {
                        foreach (DataRow dr in TABLEADDFX.Rows)
                        {
                            #region Check eixts FX limit account
                            if (dsFXLm != null)
                            {
                                if (dsFXLm.Tables.Count > 0)
                                {
                                    //DataRow[] drf = dsFXLm.Tables[0].Select("TRANDCODE = '" + dr["TRANDCODE"].ToString() + "' AND FROMCCYID = '" + dr["FROMCCYID"].ToString() + "' AND TOCCYID = '" + dr["TOCCYID"].ToString() + "'");
                                    DataRow[] drf = dsFXLm.Tables[0].Select("FROMCCYID = '" + dr["FROMCCYID"].ToString() + "' AND TOCCYID = '" + dr["TOCCYID"].ToString() + "'");
                                    if (drf.Length > 0)
                                    {
                                        lblError.Text = string.Format(Resources.labels.FxTransactionLimitexists, SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtContractNo.Text.Trim()), dr["TRANDCODENAME"].ToString(), dr["FROMCCYID"].ToString(), dr["TOCCYID"].ToString());
                                        return;
                                    }
                                }
                            }
                            #endregion
                            row = dtFX.NewRow();
                            row["CONTRACTNO"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtContractNo.Text.Trim());
                            row["TRANDCODE"] = dr["TRANDCODE"];
                            row["FROMCCYID"] = dr["FROMCCYID"];
                            row["TOCCYID"] = dr["TOCCYID"];
                            row["TRANLIMIT"] = dr["LIMITTRAN"];
                            row["COUNTLIMIT"] = dr["COUNTTRAN"];
                            row["TOTALLIMITDAY"] = dr["TOTALLIMIT"];
                            row["USER"] = Session["userName"].ToString();
                            row["DATE"] = DateTime.Now;
                            row["ALLOWFOREIGN"] = dr["ALLOWFOREIGN"];
                            dtFX.Rows.Add(row);
                        }
                    }
                    if (dtFX.Rows.Count == 0)
                    {
                        lblError.Text = "You must add rows detail.";
                        return;
                    }
                    new SmartPortal.SEMS.FxTransactionLimit().FXLimitInsert(dtFX, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        lblError.Text = Resources.labels.FxTransactionLimitAddsuccessful;
                        //ddlTrans.Enabled = false;
                        txtlimit.Enabled = false;
                        txtTotalLimitDay.Enabled = false;
                        txtCountLimit.Enabled = false;
                        ddlFromCCYID.Enabled = false;
                        ddlToCCYID.Enabled = false;
                        btsave.Visible = false;
                        btnAdd.Enabled = false;
                        btback.Visible = false;
                        btbackHome.Visible = true;
                    }
                    else
                    {
                        lblError.Text = Resources.labels.FxTransactionLimitAddfail;
                    }
                    #endregion
                    break;
                case "edit":
                    #region Check Limit Tran and Total Limit day
                    if (SmartPortal.Common.Utilities.Utility.isDouble(txtlimit.Text.Trim(), true) > SmartPortal.Common.Utilities.Utility.isDouble(txtTotalLimitDay.Text.Trim(), true))
                    {
                        lblError.Text = "Total Limit must greater than Limit.";
                        return;
                    }
                    #endregion
                    #region Edit
                    row = dtFX.NewRow();
                    row["CONTRACTNO"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtContractNo.Text.Trim());
                    //row["TRANDCODE"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlTrans.SelectedValue.ToString());
                    row["TRANDCODE"] = string.Empty;
                    row["FROMCCYID"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlFromCCYID.SelectedValue.ToString());
                    row["TOCCYID"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlToCCYID.SelectedValue.ToString());
                    row["TRANLIMIT"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtlimit.Text.Trim(), ddlFromCCYID.SelectedValue.Trim()));
                    row["COUNTLIMIT"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(SmartPortal.Common.Utilities.Utility.IsInt(txtCountLimit.Text.Trim()).ToString());
                    row["TOTALLIMITDAY"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtTotalLimitDay.Text.Trim(), ddlFromCCYID.SelectedValue.Trim()));
                    row["USER"] = Session["userName"].ToString();
                    row["DATE"] = DateTime.Now;
                    row["ALLOWFOREIGN"] = cbFX.Checked == true ? "Y" : "N";
                    dtFX.Rows.Add(row);
                    new SmartPortal.SEMS.FxTransactionLimit().FXLimitEdit(dtFX, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        lblError.Text = Resources.labels.FxTransactionLimitEditsuccessful;
                        //ddlTrans.Enabled = false;
                        txtlimit.Enabled = false;
                        txtTotalLimitDay.Enabled = false;
                        txtCountLimit.Enabled = false;
                        ddlFromCCYID.Enabled = false;
                        ddlToCCYID.Enabled = false;
                        btsave.Visible = false;
                        btback.Visible = false;
                        btbackHome.Visible = true;
                        btnAdd.Enabled = false;
                        cbFX.Enabled = false;
                    }
                    else
                    {
                        lblError.Text = Resources.labels.FxTransactionLimitEditfail;
                    }
                    #endregion
                    break;
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(IPCex.ToString(), Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
        
    }
    protected void btback_Click(object sender, EventArgs e)
    {
        TABLEADDFX = null;
        if (string.IsNullOrEmpty(IDXRADIOSELECTED))
        {
            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=" + LISTFXPAGEID),false);
        }
        else
        {
            pnAdd.Visible = false;
            pnAddDetail.Visible = false;
            pnSearch.Visible = true;
            btnNext.Visible = true;
            btback.Visible = false;
            btbackHome.Visible = true;
            btsave.Visible = false;
        }
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(IDXRADIOSELECTED))
        {
            lblError.Text = Resources.labels.bancanchonhopdong;
            return;
        }
        else
        {
            txtContractNo.Text = IDXRADIOSELECTED.Split('|')[1];
            //ddlTrans.Enabled = true;
            txtlimit.Enabled = true;
            txtTotalLimitDay.Enabled = true;
            txtCountLimit.Enabled = true;
            ddlFromCCYID.Enabled = true;
            ddlToCCYID.Enabled = true;
            btsave.Visible = true;
            btnNext.Visible = false;
            pnSearch.Visible = false;
            pnAdd.Visible = true;
            btback.Visible = true;
            btbackHome.Visible = false;
            pnAddDetail.Visible = true;
            BindAddDetail();
        }
    }
    protected void gvContractList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = (DataRowView)e.Row.DataItem;
                e.Row.Attributes.Add("onmousemove", "this.className='hightlight';");
                e.Row.Attributes.Add("onmouseout", "this.className='nohightlight';");
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void gvContractList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvContractList.PageIndex = e.NewPageIndex;
        GetAllContractByCondition();
        IDXRADIOSELECTED = string.Empty;
    }
    protected void colRbContractNo_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            RadioButton item = (RadioButton)sender;
            GridViewRow row = item.Parent.Parent as GridViewRow;
            Label colContractNo = (Label)row.Cells[1].Controls[1];
            if (!string.IsNullOrEmpty(IDXRADIOSELECTED))
            {
                int idx = int.Parse(IDXRADIOSELECTED.Split('|')[0]);
                if (idx <= gvContractList.Rows.Count - 1)
                {
                    GridViewRow rowOld = gvContractList.Rows[idx];
                    RadioButton colRbContractNo = (RadioButton)rowOld.Cells[0].Controls[1];
                    colRbContractNo.Checked = false;
                }
            }
            IDXRADIOSELECTED = row.RowIndex.ToString() + "|" + colContractNo.Text;
            item.Checked = true;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void GetAllContractByCondition()
    {
        try
        {
            string sContractNo = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtContractSearch.Text.Trim());
            string sFullName = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtFullNameSearch.Text.Trim());
            DataSet dsResult = new SmartPortal.SEMS.FxTransactionLimit().GetListContract(sContractNo, sFullName, ref IPCERRORCODE, ref IPCERRORDESC);
            if (dsResult == null)
            {
                lblError.Text = Resources.labels.datanotfound;
                return;
            }
            else
            {
                gvContractList.DataSource = dsResult;
                gvContractList.DataBind();
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        lblError.Text = string.Empty;
        IDXRADIOSELECTED = null;
        GetAllContractByCondition();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            CreateFXTable();
            lblError.Text = string.Empty;
            #region Check exits limit
            if (TABLEADDFX.Rows.Count > 0)
            {
                //DataRow[] drf = TABLEADDFX.Select("TRANDCODE = '" + ddlTrans.SelectedValue.ToString().Trim() + "' AND FROMCCYID = '" + ddlFromCCYID.SelectedValue.ToString().Trim() + "' AND TOCCYID = '" + ddlToCCYID.SelectedValue.ToString().Trim() + "' AND ALLOWFOREIGN = '" + "Y" + "'");
                DataRow[] drf = TABLEADDFX.Select("FROMCCYID = '" + ddlFromCCYID.SelectedValue.ToString().Trim() + "' AND TOCCYID = '" + ddlToCCYID.SelectedValue.ToString().Trim() + "' AND ALLOWFOREIGN = '" + "Y" + "'");
                if (drf.Length > 0)
                {
                    int index = TABLEADDFX.Rows.IndexOf(drf[0]) / gvAddDetail.PageSize;
                    lblError.Text = Resources.labels.FxTransactionLimitExists1;
                    Highlight();
                    return;
                }
            }
            #endregion
            #region Check Limit Tran and Total Limit day
            if (SmartPortal.Common.Utilities.Utility.isDouble(txtlimit.Text.Trim(),true) > SmartPortal.Common.Utilities.Utility.isDouble(txtTotalLimitDay.Text.Trim(), true))
            {
                lblError.Text = "Total Limit must greater than Limit.";
                return;
            }
            #endregion
            #region Add new limit to table
            DataRow dr = TABLEADDFX.NewRow();
            //dr["TRANDCODE"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlTrans.SelectedValue.ToString());
            dr["TRANDCODE"] = string.Empty;
            //dr["TRANDCODENAME"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlTrans.SelectedItem.ToString());
            dr["TRANDCODENAME"] = string.Empty;
            dr["FROMCCYID"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlFromCCYID.SelectedValue.ToString());
            dr["TOCCYID"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlToCCYID.SelectedValue.ToString());
            dr["LIMITTRAN"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtlimit.Text.Trim(), ddlFromCCYID.SelectedValue.Trim()));
            dr["COUNTTRAN"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(SmartPortal.Common.Utilities.Utility.IsInt(txtCountLimit.Text.Trim()).ToString());
            dr["TOTALLIMIT"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtTotalLimitDay.Text.Trim(), ddlFromCCYID.SelectedValue.Trim()));
            dr["ALLOWFOREIGN"] = cbFX.Checked == true ? "Y" : "N";
            TABLEADDFX.Rows.Add(dr);
            int pageNextIndex = TABLEADDFX.Rows.Count + 1 / gvAddDetail.PageSize;
            gvAddDetail.PageIndex = pageNextIndex;
            BindAddDetail();
            #endregion
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void CreateFXTable()
    {
        try
        {
            if (TABLEADDFX == null)
            {
                TABLEADDFX = new DataTable();
                TABLEADDFX.Columns.Add("TRANDCODE");
                TABLEADDFX.Columns.Add("TRANDCODENAME");
                TABLEADDFX.Columns.Add("FROMCCYID");
                TABLEADDFX.Columns.Add("TOCCYID");
                TABLEADDFX.Columns.Add("LIMITTRAN");
                TABLEADDFX.Columns.Add("COUNTTRAN");
                TABLEADDFX.Columns.Add("TOTALLIMIT");
                TABLEADDFX.Columns.Add("ALLOWFOREIGN");
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void Highlight()
    {
        try
        {
            Label lblTranCode, lblTranCodeName, lblFromCCY, lblToCCY;

            foreach (GridViewRow gvr in gvAddDetail.Rows)
            {
                lblTranCode = (Label)gvr.Cells[0].FindControl("lblTranCode");
                lblTranCodeName = (Label)gvr.Cells[0].FindControl("lblTranCodeName");
                lblFromCCY = (Label)gvr.Cells[1].FindControl("lblFromCCY");
                lblToCCY = (Label)gvr.Cells[2].FindControl("lblToCCY");

                //if (ddlTrans.SelectedValue.Equals(lblTranCode.Text) && ddlFromCCYID.SelectedValue.Equals(lblFromCCY.Text) && ddlToCCYID.SelectedValue.Equals(lblToCCY.Text))
                if (ddlFromCCYID.SelectedValue.Equals(lblFromCCY.Text) && ddlToCCYID.SelectedValue.Equals(lblToCCY.Text))
                {
                    lblTranCodeName.ForeColor = System.Drawing.Color.Red;
                    lblFromCCY.ForeColor = System.Drawing.Color.Red;
                    lblToCCY.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void gvAddDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAddDetail.PageIndex = e.NewPageIndex;
        BindAddDetail();
    }
    protected void BindAddDetail()
    {
        gvAddDetail.DataSource = TABLEADDFX;
        gvAddDetail.DataBind();
    }
    protected void colDelete_Click(object sender, EventArgs e)
    {
        string argrument = ((sender as LinkButton).CommandArgument).ToString();
        if (!string.IsNullOrWhiteSpace(argrument))
        {
            string[] lstargrument = argrument.Split('#');
            if (TABLEADDFX.Rows.Count > 0)
            {
               // DataRow[] drf = TABLEADDFX.Select("TRANDCODE = '" + lstargrument[0] + "' AND FROMCCYID = '" + lstargrument[1] + "' AND TOCCYID = '" + lstargrument[2] + "'");
                DataRow[] drf = TABLEADDFX.Select("FROMCCYID = '" + lstargrument[1] + "' AND TOCCYID = '" + lstargrument[2] + "'");
                if (drf.Length > 0)
                {
                    TABLEADDFX.Rows.Remove(drf[0]);
                    int pageNextIndex = TABLEADDFX.Rows.Count + 1 / gvAddDetail.PageSize;
                    gvAddDetail.PageIndex = pageNextIndex;
                    BindAddDetail();
                }
            }
        }
    }

    protected void btbackHome_Click(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=" + LISTFXPAGEID),false);
    }
}
