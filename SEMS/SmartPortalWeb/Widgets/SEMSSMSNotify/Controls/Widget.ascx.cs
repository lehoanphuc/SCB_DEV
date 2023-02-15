using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using SmartPortal.Common.Utilities;


public partial class Widgets_SEMSSMSNotify_Controls_Widget : WidgetBase
{
    private const string pageHome = "1030";
    private const string thisPage = "1031";
    string ACTION = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
   
    //public string _IMAGE
    //{
    //    get { return imgLoGo.ImageUrl; }
    //    set { imgLoGo.ImageUrl = value; }
    //}

    //public string _TITLE
    //{
    //    get { return lblTitleProduct.Text; }
    //    set { lblTitleProduct.Text = value; }
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
           txtMinAmt.Attributes.Add("onkeyup", "executeComma('" + txtMinAmt.ClientID + "',event)");
           txtMaxAmt.Attributes.Add("onkeyup", "executeComma('" + txtMaxAmt.ClientID + "',event)");
           lblError.Text = "";
           ACTION = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["a"].ToString().Trim();
           if (!IsPostBack)
            {
                divRole.Visible = true;
                divConfig.Visible = false;
                divResult.Visible = false;

                divbtnAmt.Visible = false;
                divbtnCF.Visible = false;

                btnExit.Visible = false;

                BindData();
            }
        }
        catch (Exception ex)
        {
        }
     
    }
    void BindData()
    {
        LoadRole();
        LoadTranType();

        //load day
        for (int i = 1; i < 29; i++)
        {
            ListItem li = new ListItem(i.ToString(), i.ToString());
            cblThuM.Items.Add(li);
            //cblcfM.Items.Add(li);
        }
        cblThuM.Items.Add(new ListItem("EOM", "32"));

        ddlCurrency.DataSource = new SmartPortal.SEMS.Product().LoaddAllCCYID(ref IPCERRORCODE, ref IPCERRORDESC);
        ddlCurrency.DataTextField = "CCYID";
        ddlCurrency.DataValueField = "CCYID";
        ddlCurrency.DataBind();

        switch(ACTION)
        {
            case "viewdetail":
                divRole.Visible = false;
                divConfig.Visible = false;
                divResult.Visible = true;
                divbtnAmt.Visible = false;
                divbtnCF.Visible = true;
                btnSaveCF.Visible = false;
                btnBackCF.Visible = false;
                btnExit.Visible = true;
                LoadDetail();
                break;
            case "add":
                divRole.Visible = true;
                divConfig.Visible = false;
                divResult.Visible = false;
                divbtnAmt.Visible = false;
                divbtnCF.Visible = false;
                btnExit.Visible = false;
                break;
        }
    }
    void LoadDetail()
    {
        hdfID.Value = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cfid"].ToString().Trim();
        object[] para = new object[1] { hdfID.Value };

        DataSet ds = new SmartPortal.SEMS.Transactions().DoStored("SEMS_SMS_NOTIFY_CONFIG_GETBYID", para, ref IPCERRORCODE, ref IPCERRORDESC);

        if(IPCERRORCODE.Equals("0"))
        {
            DataTable dtcf = ds.Tables[0];
            DataTable dtdt = ds.Tables[1];

            lblRoleCF.Text = dtcf.Rows[0]["RoleName"].ToString();
            lblTranTypeCF.Text = dtcf.Rows[0]["PageName"].ToString();
            lblConfigNameCF.Text = dtcf.Rows[0]["Name"].ToString();
            lblDescCF.Text = dtcf.Rows[0]["Description"].ToString();

            divGrid.Visible = false;
            DataTable tblTransDetails = new DataTable();
            DataColumn FeeID = new DataColumn("CFID");
            DataColumn Min = new DataColumn("MINAMT");
            DataColumn Max = new DataColumn("MAXAMT");
            DataColumn Ccyid = new DataColumn("CCYID");
            DataColumn FkID = new DataColumn("FkID");

            tblTransDetails.Columns.AddRange(new DataColumn[] { FeeID, Min, Max, Ccyid, FkID });

            string trancode = dtcf.Rows[0]["TranCode"].ToString();
            switch(trancode)
            {
                case "SNO0001":
                case "SNO0002":
                case "SNO0003":
                case "SNO0004":
                case "SNO0005":
                case "SNO0006":
                    trTimeCF.Visible = false;
                    trScheduleTypeCF.Visible = false;
                    trScheduleDayCF.Visible = false;
                    trNumTranCF.Visible = false;
                    divGrid.Visible = true;
                    DataRow[] drccyid = dtdt.Select("PARANAME='MINAMT'");

                    foreach(DataRow dr in drccyid)
                    {
                        DataRow r = tblTransDetails.NewRow();
                        string ccyid = dr["PARAEXT"].ToString();

                        r["CFID"] = hdfID.Value;
                        r["MINAMT"] = SmartPortal.Common.Utilities.Utility.FormatMoneyInput(dr["PARAVALUE"].ToString(), ccyid);
                        //r["MAXAMT"] = SmartPortal.Common.Utilities.Utility.FormatMoneyInput(dtdt.Select("PARANAME='MAXAMT'")[0]["PARAVALUE"].ToString(), ccyid);
                        r["MAXAMT"] = SmartPortal.Common.Utilities.Utility.FormatMoneyInput("0", ccyid);
                        r["CCYID"] = ccyid;
                        r["FkID"] = SmartPortal.Common.Utilities.Utility.GetID("", "", "", 6);

                        tblTransDetails.Rows.Add(r);
                    }
                    tblTransDetails.DefaultView.Sort = "[" + tblTransDetails.Columns["CCYID"].ColumnName + "] ASC";
                    ViewState["CFDETAILS"] = tblTransDetails;
                    gvConfigDetails.DataSource = tblTransDetails;
                    gvConfigDetails.DataBind();
                    //gvConfigDetails.Columns[0].Visible = false;
                    gvConfigDetails.Columns[3].Visible = false;
                    gvConfigDetails.Columns[5].Visible = false;
                    break;
                case "SNO0007":
                    trTimeCF.Visible = true;
                    trScheduleTypeCF.Visible = false;
                    trScheduleDayCF.Visible = false;
                    trNumTranCF.Visible = false;

                    lblTimeCF.Text = dtdt.Select("PARANAME='TIME'")[0]["PARAVALUE"].ToString();
                    break;
                case "SNO0008":
                    trTimeCF.Visible = false;
                    trScheduleTypeCF.Visible = false;
                    trScheduleDayCF.Visible = false;
                    trNumTranCF.Visible = false;
                    divGrid.Visible = true;
                    DataRow[] drccyid8 = dtdt.Select("PARANAME='MINAMT'");

                    foreach(DataRow dr in drccyid8)
                    {
                        DataRow r = tblTransDetails.NewRow();
                        string ccyid = dr["PARAEXT"].ToString();

                        r["CFID"] = hdfID.Value;
                        r["MINAMT"] = SmartPortal.Common.Utilities.Utility.FormatMoneyInput(dtdt.Select("PARANAME='MINAMT' and PARAEXT = '" + ccyid + "'")[0]["PARAVALUE"].ToString(), ccyid);
                        r["MAXAMT"] = SmartPortal.Common.Utilities.Utility.FormatMoneyInput(dtdt.Select("PARANAME='MAXAMT' and PARAEXT = '" + ccyid + "'")[0]["PARAVALUE"].ToString(), ccyid);
                        r["CCYID"] = ccyid;
                        r["FkID"] = SmartPortal.Common.Utilities.Utility.GetID("", "", "", 6);

                        tblTransDetails.Rows.Add(r);
                    }
                    tblTransDetails.DefaultView.Sort = "[" + tblTransDetails.Columns["CCYID"].ColumnName + "] ASC";
                    ViewState["CFDETAILS"] = tblTransDetails;
                    gvConfigDetails.DataSource = tblTransDetails;
                    gvConfigDetails.DataBind();
                    //gvConfigDetails.Columns[0].Visible = false;
                    gvConfigDetails.Columns[3].Visible = true;
                    gvConfigDetails.Columns[5].Visible = false;
                    break;
                case "SNO0009":
                    trTimeCF.Visible = true;
                    trScheduleTypeCF.Visible = true;
                    trScheduleDayCF.Visible = !rdDaily.Checked;
                    trNumTranCF.Visible = true;

                    
                    lblNumTranCF.Text = dtdt.Select("PARANAME='NUMTRAN'")[0]["PARAVALUE"].ToString();
                    lblTimeCF.Text = dtdt.Select("PARANAME='TIME'")[0]["PARAVALUE"].ToString();

                    switch(dtdt.Select("PARANAME='TYPE'")[0]["PARAVALUE"].ToString())
                    {
                        case "D":
                            lblScheduleTypeCF.Text = Resources.labels.hangngay;
                            break;
                        case "W":
                            lblScheduleTypeCF.Text = Resources.labels.hangtuan;
                            lblScheduleDayCF.Text = dtdt.Select("PARANAME='DAY'")[0]["PARAVALUE"].ToString().Replace("32", "EOM");
                            break;
                        case "M":
                            lblScheduleTypeCF.Text = Resources.labels.hangthang;
                            lblScheduleDayCF.Text = dtdt.Select("PARANAME='DAY'")[0]["PARAVALUE"].ToString().Replace("32", "EOM");
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
        }


    }
    void LoadRole()
    {
        ddlRole.DataSource = new SmartPortal.SEMS.Role().GetRoleByType("SNO");
        ddlRole.DataTextField = "RoleName";
        ddlRole.DataValueField = "RoleID";
        ddlRole.DataBind();
    }
    void LoadTranType() 
    {
        ddlTransType.DataSource = new SmartPortal.SEMS.Role().GetTranOfRole(ddlRole.SelectedValue, ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE != "0")
        {
            lblError.Text = IPCERRORDESC;
        }
        ddlTransType.DataTextField = "PAGENAME";
        ddlTransType.DataValueField = "TRANCODE";
        ddlTransType.DataBind();
    }
    protected void btnNextRole_Click(object sender, EventArgs e)
    {
        DataTable dt = new SmartPortal.SEMS.Notification().SearchSMSConfig(ddlRole.SelectedValue.Trim(), ddlTransType.SelectedValue.Trim(), "");

        if(dt.Rows.Count>0)
        {
            lblError.Text = Resources.labels.configalreadyexist;
            return;
        }
        else if(string.IsNullOrEmpty(txtConfigName.Text.Trim()))
        {
            lblError.Text = Resources.labels.confignameinvalid;
        }
        else
        {
            trWeekly.Visible = false;
            trMonthly.Visible = false;
            trLastTransTime.Visible = false;
            divResult.Visible = false;

            divbtnAmt.Visible = true;
            divbtnCF.Visible = false;

            ViewState["CFDETAILS"] = null;

            gvConfigDetails.DataSource = null;
            gvConfigDetails.DataBind();
            divGrid.Visible = true;

            lblRoleConfig.Text = ddlRole.SelectedItem.Text;
            lblTranTypConfig.Text = ddlTransType.SelectedItem.Text;

            switch(ddlTransType.SelectedValue)
            {
                case "SNO0001":
                case "SNO0002":
                case "SNO0003":
                case "SNO0004":
                case "SNO0005":
                case "SNO0006":
                    divRole.Visible = false;
                    divConfig.Visible = true;
                    trMAmt.Visible = true;
                    trDueDate.Visible = false;
                    trLastTrans.Visible = false;
                    lblMinAmt.Visible = true;
                    txtMinAmt.Visible = true;
                    lblMaxAmt.Visible = false;
                    txtMaxAmt.Visible = false;
                    trCurrency.Visible = true;
                    gvConfigDetails.Columns[3].Visible = false;
                    break;
                case "SNO0007":
                    divRole.Visible = false;
                    divConfig.Visible = true;
                    trMAmt.Visible = false;
                    trDueDate.Visible = true;
                    trLastTrans.Visible = false;
                    lblMinAmt.Visible = true;
                    txtMinAmt.Visible = true;
                    lblMaxAmt.Visible = true;
                    txtMaxAmt.Visible = true;
                    trCurrency.Visible = false;
                    break;
                case "SNO0008":
                    divRole.Visible = false;
                    divConfig.Visible = true;
                    trMAmt.Visible = true;
                    trDueDate.Visible = false;
                    trLastTrans.Visible = false;
                    lblMinAmt.Visible = true;
                    txtMinAmt.Visible = true;
                    lblMaxAmt.Visible = true;
                    txtMaxAmt.Visible = true;
                    trCurrency.Visible = true;
                    gvConfigDetails.Columns[3].Visible = true;
                    break;
                case "SNO0009":
                    divRole.Visible = false;
                    divConfig.Visible = true;
                    trMAmt.Visible = false;
                    trDueDate.Visible = false;
                    trLastTrans.Visible = true;
                    lblMinAmt.Visible = true;
                    txtMinAmt.Visible = true;
                    lblMaxAmt.Visible = true;
                    txtMaxAmt.Visible = true;
                    trCurrency.Visible = false;
                    OnChooseScheduleType(sender, e);
                    break;
                default:
                    lblError.Text = "This role haven't config";
                    break;
            }
            hdfID.Value = GetID();
        }
    }
    protected void btnBackRole_Click(object sender, EventArgs e)
    {
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p="+pageHome));
    }
    protected void OnChooseRole(object sender, EventArgs e)
    {
        LoadTranType();
    }
    protected void btnBackMAmt_Click(object sender, EventArgs e)
    {
        divRole.Visible = true;
        divConfig.Visible = false;
        divbtnAmt.Visible = false;
        divbtnCF.Visible = false;

        divGrid.Visible = false;

        gvConfigDetails.DataSource = null;
        gvConfigDetails.DataBind();
        ViewState["CFDETAILS"] = null;


    }
    protected void btnNextMAmt_Click(object sender, EventArgs e)
    {
        #region validate
        switch (ddlTransType.SelectedValue)
        {
            case "SNO0001":
            case "SNO0002":
            case "SNO0003":
            case "SNO0004":
            case "SNO0005":
            case "SNO0006":
            case "SNO0008":
                if (ViewState["CFDETAILS"] == null)
                {
                    lblError.Text = Resources.labels.mustconfigallccyid;
                    return;
                }
                else if(((DataTable)ViewState["CFDETAILS"]).Rows.Count != ddlCurrency.Items.Count)
                {
                    lblError.Text = Resources.labels.mustconfigallccyid;
                    return;
                }
                break;
            case "SNO0007":
                if(string.IsNullOrEmpty(txtDueDateTime.Text))
                {
                    lblError.Text = Resources.labels.timeinvalid;
                    return;
                }
                break;
            case "SNO0009":
                if (string.IsNullOrEmpty(txtNumTrans.Text))
                {
                    lblError.Text = Resources.labels.numberoftraninvalid;
                    return;
                }
                else if (Double.Parse(txtNumTrans.Text)<=0)
                {
                    lblError.Text = Resources.labels.numberoftraninvalid;
                    return;
                }
                else if (!(rdDaily.Checked || rdWeekly.Checked || rdMonthly.Checked))
                {
                    lblError.Text = Resources.labels.pleasechoosescheduletype;
                    return;
                }
                else if (string.IsNullOrEmpty(txtLastTransTime.Text))
                {
                    lblError.Text = Resources.labels.timeinvalid;
                    return;
                }
                else if(rdWeekly.Checked)
                {
                    if( cblThu.SelectedIndex == -1)
                    {
                        lblError.Text = Resources.labels.pleasechooseday;
                        return;
                    }
                }
                else if(rdMonthly.Checked)
                {
                    if (cblThuM.SelectedIndex == -1)
                    {
                        lblError.Text = Resources.labels.pleasechooseday;
                        return;
                    }
                }
                break;
            default:
                break;
        }
        #endregion
        //clone data
        lblRoleCF.Text = lblRoleConfig.Text;
        lblTranTypeCF.Text = lblTranTypConfig.Text;

        lblConfigNameCF.Text = txtConfigName.Text;
        lblDescCF.Text = txtDesc.Text;
        
        switch(ddlTransType.SelectedValue)
        {
            case "SNO0001":
            case "SNO0002":
            case "SNO0003":
            case "SNO0004":
            case "SNO0005":
            case "SNO0006":
            case "SNO0008":
                gvConfigDetails.Visible = true;
                trTimeCF.Visible = false;
                trScheduleTypeCF.Visible = false;
                trScheduleDayCF.Visible = false;
                trNumTranCF.Visible = false;
                break;
            case "SNO0007":
                gvConfigDetails.Visible = false;
                trTimeCF.Visible = true;
                trScheduleTypeCF.Visible = false;
                trScheduleDayCF.Visible = false;
                trNumTranCF.Visible = false;

                lblTimeCF.Text = txtDueDateTime.Text;
                break;
            case "SNO0009":
                trTimeCF.Visible = true;
                trScheduleTypeCF.Visible = true;
                trScheduleDayCF.Visible = !rdDaily.Checked;
                trNumTranCF.Visible = true;
                if(rdMonthly.Checked)
                {
                    lblScheduleTypeCF.Text = rdMonthly.Text;
                    string sday = string.Empty;
                    foreach(ListItem li in cblThuM.Items)
                    {
                        if(li.Selected)
                        {
                            sday += li.Text + ", ";
                        }
                    }
                    lblScheduleDayCF.Text = sday.Substring(0, sday.Length - 2);
                }
                else if(rdWeekly.Checked)
                {
                    lblScheduleTypeCF.Text = rdWeekly.Text;
                    string sday = string.Empty;
                    foreach (ListItem li in cblThu.Items)
                    {
                        if (li.Selected)
                        {
                            sday += li.Text + ", ";
                        }
                    }
                    lblScheduleDayCF.Text = sday.Substring(0, sday.Length - 2);
                }
                else
                {
                    lblScheduleTypeCF.Text = rdDaily.Text;
                }
                lblNumTranCF.Text = txtNumTrans.Text;
                lblTimeCF.Text = txtLastTransTime.Text;
                break;
        }

        divRole.Visible = false;
        divConfig.Visible = false;
        divResult.Visible = true;
        divbtnAmt.Visible = false;
        divbtnCF.Visible = true;

        gvConfigDetails.Columns[5].Visible = false;
    }
    protected void btnBackCF_Click(object sender, EventArgs e)
    {
        divRole.Visible = false;
        divConfig.Visible = true;
        divResult.Visible = false;
        divbtnAmt.Visible = true;
        divbtnCF.Visible = false;

        gvConfigDetails.Columns[5].Visible = true;
    }
    protected void btnSaveCF_Click(object sender, EventArgs e)
    {
        if(string.IsNullOrEmpty(hdfID.Value))
        {
            hdfID.Value=GetID();
        }
        string cfid = hdfID.Value;
        string roleid = ddlRole.SelectedValue;
        string trancode = ddlTransType.SelectedValue;
        string name = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtConfigName.Text);
        string desc = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtDesc.Text);
        string status = "A";
        string useraction = SmartPortal.Common.Utilities.Utility.KillSqlInjection(Session["userName"].ToString());
        string dateaction = SmartPortal.Common.Utilities.Utility.KillSqlInjection(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

        DataTable ConfigDetails = new DataTable();
        DataTable givTable = new DataTable();
        if(ViewState["CFDETAILS"] != null)
        {
            givTable = (DataTable)ViewState["CFDETAILS"];
        }

        #region clone data
        DataColumn colID = new DataColumn("CFID");
        DataColumn colpname = new DataColumn("PARANAME");
        DataColumn colpext = new DataColumn("PARAEXT");
        DataColumn colpval = new DataColumn("PARAVALUE");
        ConfigDetails.Columns.AddRange(new DataColumn[] { colID, colpname, colpext, colpval });

        switch(ddlTransType.SelectedValue)
        {
            case "SNO0001":
            case "SNO0002":
            case "SNO0003":
            case "SNO0004":
            case "SNO0005":
            case "SNO0006":
                foreach (DataRow row in givTable.Rows)
                {
                    DataRow dr = ConfigDetails.NewRow();
                    dr["CFID"] = row["CFID"].ToString();
                    dr["PARANAME"] = "MINAMT";
                    dr["PARAEXT"] = row["CCYID"].ToString();
                    dr["PARAVALUE"] = row["MINAMT"].ToString();
                    ConfigDetails.Rows.Add(dr);
                }
                break;
            case "SNO0007":
                    DataRow dr7 = ConfigDetails.NewRow();
                    dr7["CFID"] = hdfID.Value;
                    dr7["PARANAME"] = "TIME";
                    dr7["PARAEXT"] = "";
                    dr7["PARAVALUE"] = txtDueDateTime.Text;
                    ConfigDetails.Rows.Add(dr7);
                break;
            case "SNO0008":
                foreach (DataRow row in givTable.Rows)
                {
                    DataRow dr = ConfigDetails.NewRow();
                    dr["CFID"] = row["CFID"].ToString();
                    dr["PARANAME"] = "MINAMT";
                    dr["PARAEXT"] = row["CCYID"].ToString();
                    dr["PARAVALUE"] = row["MINAMT"].ToString();
                    ConfigDetails.Rows.Add(dr);

                    DataRow dr1 = ConfigDetails.NewRow();
                    dr1["CFID"] = row["CFID"].ToString();
                    dr1["PARANAME"] = "MAXAMT";
                    dr1["PARAEXT"] = row["CCYID"].ToString();
                    dr1["PARAVALUE"] = row["MAXAMT"].ToString();
                    ConfigDetails.Rows.Add(dr1);
                }
                break;
            case "SNO0009":
                {
                    DataRow dr = ConfigDetails.NewRow();
                    dr["CFID"] = hdfID.Value;
                    dr["PARANAME"] = "TYPE";
                    dr["PARAVALUE"] = rdDaily.Checked ? "D" : rdWeekly.Checked ? "W" : "M";
                    ConfigDetails.Rows.Add(dr);

                    DataRow dr1 = ConfigDetails.NewRow();
                    dr1["CFID"] = hdfID.Value;
                    dr1["PARANAME"] = "NUMTRAN";
                    dr1["PARAVALUE"] = txtNumTrans.Text;
                    ConfigDetails.Rows.Add(dr1);

                    DataRow dr2 = ConfigDetails.NewRow();
                    dr2["CFID"] = hdfID.Value;
                    dr2["PARANAME"] = "TIME";
                    dr2["PARAVALUE"] = txtLastTransTime.Text;
                    ConfigDetails.Rows.Add(dr2);

                    string sday = string.Empty;
                    if (rdMonthly.Checked)
                    {
                        foreach (ListItem li in cblThuM.Items)
                        {
                            sday += li.Selected ? li.Value + "," : "";
                        }
                    }
                    else if (rdWeekly.Checked)
                    {
                        foreach (ListItem li in cblThu.Items)
                        {
                            sday += li.Selected ? li.Value + "," : "";
                        }
                    }
                    sday = string.IsNullOrEmpty(sday)?sday:sday.Substring(0, sday.Length - 1);
                    if (!string.IsNullOrEmpty(sday))
                    {
                        DataRow dr3 = ConfigDetails.NewRow();
                        dr3["CFID"] = hdfID.Value;
                        dr3["PARANAME"] = "DAY";
                        dr3["PARAVALUE"] = sday;
                        ConfigDetails.Rows.Add(dr3);
                    }
                    
                    break;
                }
            default:
                break;
        }
        
        
        #endregion

        switch (ACTION)
        {
            case "add":

                new SmartPortal.SEMS.Notification().InsertSMSNotifyConfig(cfid, roleid, trancode, name, desc, status, useraction, dateaction, "", "", ConfigDetails, ref IPCERRORCODE, ref IPCERRORDESC);

                if (IPCERRORCODE == "0")
                {
                    lblError.Text = Resources.labels.transactionsuccess;
                    divRole.Visible = false;
                    divConfig.Visible = false;
                    divResult.Visible = true;
                    btnSaveCF.Visible = false;
                    btnBackCF.Visible = false;
                    btnExit.Visible = true;
                    divbtnAmt.Visible = false;
                    divbtnCF.Visible = true;
                }
                else
                {
                    lblError.Text = Resources.labels.transactionerror;
                }


                break;
            case "edit":
                //new SmartPortal.SEMS.Fee().UpdateFee(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["fid"].ToString(), feename, feetype, fixamount, isladder, isregionfee, chargelater, ccyid, "", "", useraction, dateaction, Session["branch"].ToString(), ProcessApproveDetails, ref IPCERRORCODE, ref IPCERRORDESC);

                //if (IPCERRORCODE == "0")
                //{
                //    lblWarning.Text = "";
                //    lblError.Text = Resources.labels.transactionsuccess;
                //    pnResult.Visible = true;
                //    pnFee.Visible = false;
                //    tbLadderFee.Visible = false;
                //    btsave.Visible = false;
                //    pnGV.Visible = false;
                //    pnDetailsFee.Visible = false;
                //    pnWarning.Visible = false;
                //}
                //else
                //{
                //    throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.ERRORADDFEE);
                //}


                break;
        }

    }
    protected void btnExitCF_Click(object sender, EventArgs e)
    {
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=" + pageHome));
    }
    protected void OnChooseScheduleType(object sender, EventArgs e)
    {
        trWeekly.Visible = rdWeekly.Checked;
        trMonthly.Visible = rdMonthly.Checked;
        trLastTransTime.Visible = rdDaily.Checked || rdMonthly.Checked || rdWeekly.Checked;
    }
    protected void btnAddAmt_Click(object sender, EventArgs e)
    {
        DataTable tblTransDetails = new DataTable();
        DataTable tblTempt = new DataTable();
        bool isCorrect = true;
        lblError.Text = "";

        //validate
        if (ddlTransType.SelectedValue.Equals("SNO0008"))
        {
            if (double.Parse(txtMinAmt.Text) >= double.Parse(txtMaxAmt.Text))
            {
                lblError.Text = Resources.labels.maxamountmustbegreater;
                return;
            }
        }

        if (ViewState["CFDETAILS"] == null)
        {
            // tạo table tạm chứa TransDetails 
            DataColumn FeeID = new DataColumn("CFID");
            DataColumn Min = new DataColumn("MINAMT");
            DataColumn Max = new DataColumn("MAXAMT");
            DataColumn Ccyid = new DataColumn("CCYID");

            //----thêm cột id----
            DataColumn FkID = new DataColumn("FkID");
            //----thêm cột id----

            //add col vào tblTransDetails
            tblTransDetails.Columns.AddRange(new DataColumn[] { FeeID, Min, Max, Ccyid,FkID});

            //Bind data vào bảng tạm TransDetails
            DataRow r = tblTransDetails.NewRow();
            if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["a"].ToString() == "edit")
            {
                r["CFID"] = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cfid"].ToString();
            }
            else
            {
                r["CFID"] = hdfID.Value;
            }
            r["CFID"] = hdfID.Value;
            r["MINAMT"] = SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtMinAmt.Text.Trim(), ddlCurrency.SelectedValue);
            r["MAXAMT"] = SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtMaxAmt.Text, ddlCurrency.SelectedValue);
            r["CCYID"] = ddlCurrency.SelectedValue;
            r["FkID"] = SmartPortal.Common.Utilities.Utility.GetID("", "", "", 6);

            tblTransDetails.Rows.Add(r);

            tblTransDetails.DefaultView.Sort = "[" + tblTransDetails.Columns["CCYID"].ColumnName + "] ASC";
            ViewState["CFDETAILS"] = tblTransDetails;
        }
        else
        {
            try
            {
                tblTransDetails = (DataTable)ViewState["CFDETAILS"];
                tblTempt = ((DataTable)ViewState["CFDETAILS"]).Copy();
                DataRow r = tblTempt.NewRow();
                isCorrect = true;

                foreach (DataRow row in tblTransDetails.Rows)
                {

                    if (!row["CCYID"].ToString().Equals(ddlCurrency.SelectedValue))
                    {
                        if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["a"].ToString() == "edit")
                        {
                            r["CFID"] = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cfid"].ToString();
                        }
                        else
                        {
                            r["CFID"] = hdfID.Value;
                        }
                        r["CFID"] = hdfID.Value;
                        r["MINAMT"] = SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtMinAmt.Text.Trim(), ddlCurrency.SelectedValue);
                        r["MAXAMT"] = SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtMaxAmt.Text, ddlCurrency.SelectedValue);
                        r["CCYID"] = ddlCurrency.SelectedValue;
                        r["FkID"] = SmartPortal.Common.Utilities.Utility.GetID("", "", "", 6);
                    }
                    else
                    {
                        isCorrect = false;
                        lblError.Text = Resources.labels.invalidconfig;
                        break;
                    }
                }
                if(isCorrect)
                {
                    tblTempt.Rows.Add(r);
                    tblTempt.DefaultView.Sort = "[" + tblTempt.Columns["CCYID"].ColumnName + "] ASC";
                    ViewState["CFDETAILS"] = tblTempt;
                    tblTransDetails = (DataTable)ViewState["CFDETAILS"];
                }
            }
            catch (Exception ex)
            {

            }
        }

        gvConfigDetails.DataSource = tblTransDetails;
        gvConfigDetails.DataBind();
    }

    string GetID()
    {
        return "SNC" + ddlRole.SelectedValue + ddlTransType.SelectedValue.Substring(5, ddlTransType.SelectedValue.Length - 5) + new Random().Next(100000, 999999).ToString();
    }

    protected void gvConfigDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable tblTransDetailsDel = new DataTable();
        tblTransDetailsDel = (DataTable)ViewState["CFDETAILS"];

        Label lblFkID = (Label)gvConfigDetails.Rows[e.RowIndex].FindControl("lblFkID");
        DataRow[] dr = tblTransDetailsDel.Select("FkID='" + lblFkID.Text.Trim() + "'");

        tblTransDetailsDel.Rows.Remove(dr[0]);


        gvConfigDetails.DataSource = tblTransDetailsDel;
        gvConfigDetails.DataBind();

        if (tblTransDetailsDel.Rows.Count == 0)
        {
            ViewState["CFDETAILS"] = null;
        }
        else
        {
            tblTransDetailsDel.DefaultView.Sort = "[" + tblTransDetailsDel.Columns["CCYID"].ColumnName + "] ASC";
            ViewState["CFDETAILS"] = tblTransDetailsDel;
        }
    }

    protected void gvConfigDetails_Sorting(object sender, GridViewSortEventArgs e)
    {

        //string sortExpression = e.SortExpression;

        //ViewState["SortExpression"] = sortExpression;

        //if (GridViewSortDirection == SortDirection.Ascending)
        //{

        //    SortGridView(sortExpression, ASCENDING);
        //    GridViewSortDirection = SortDirection.Descending;

        //}

        //else
        //{
        //    isAscend = false;
        //    SortGridView(sortExpression, DESCENDING);
        //    GridViewSortDirection = SortDirection.Ascending;

        //}
    }

    protected void gvConfigDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //try
        //{
        //    gvAppTransDetailsList.PageIndex = e.NewPageIndex;
        //    BindData();
        //}
        //catch
        //{
        //}
    }

    protected void gvConfigDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            Label lblCFID;
            Label lblmin;
            Label lblmax;
            Label lblccyid;
            Label lblFkID;

            DataRowView drv;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView)e.Row.DataItem;

                e.Row.Attributes.Add("onmousemove", "this.className='hightlight';");
                e.Row.Attributes.Add("onmouseout", "this.className='nohightlight';");

                lblCFID = (Label)e.Row.FindControl("lblCFID");
                lblmin = (Label)e.Row.FindControl("lblmin");
                lblmax = (Label)e.Row.FindControl("lblmax");
                lblccyid = (Label)e.Row.FindControl("lblccyid");
                lblFkID = (Label)e.Row.FindControl("lblFkID");
                

                lblCFID.Text = drv["CFID"].ToString();
                lblmin.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(drv["MINAMT"].ToString(), ddlCurrency.SelectedValue);
                lblmax.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(drv["MAXAMT"].ToString(), ddlCurrency.SelectedValue);
                lblccyid.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(drv["CCYID"].ToString(), ddlCurrency.SelectedValue);
                lblFkID.Text = drv["FkID"].ToString();

            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
}

