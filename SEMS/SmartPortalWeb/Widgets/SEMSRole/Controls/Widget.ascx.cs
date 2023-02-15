using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.BLL;
using SmartPortal.Common.Utilities;
using SmartPortal.Model;
using SmartPortal.ExceptionCollection;
using System.Data;
using SmartPortal.Constant;
using SmartPortal.SEMS;

public partial class Widgets_Group_Controls_Widget : WidgetBase
{
    string IPCERRORCODE = string.Empty;
    string IPCERRORDESC = string.Empty;
    DataTable dtAction;
    private string ACTION = string.Empty;
    private string IdGroup = string.Empty;
    public string _TITLE
    {
        get { return lblTitle.Text; }
        set { lblTitle.Text = value; }
    }
    public string GROUPNAME
    {
        get { return ViewState["GROUPNAME"] != null ? ViewState["GROUPNAME"].ToString() : string.Empty; }
        set { ViewState["GROUPNAME"] = value; }
    }
    private DataTable TABLEACTION
    {
        get { return ViewState["TABLEACTION"] as DataTable; }
        set
        {
            ViewState["TABLEACTION"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            ACTION = GetActionPage();
            if (!IsPostBack)
            {
                LoadDll();
                checkAction();
                hdID.Value = IdGroup;
            }
            else
            {
                string value = !string.IsNullOrEmpty(hdID.Value) ? hdID.Value : "0";
                LoadSourcePermission(Utility.IsInt(value));
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    void LoadDll()
    {
        #region load service
        DataSet ds = new DataSet();
        //GMS : Group management service
        ds = new SmartPortal.SEMS.Services().GetAll("GMS", ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE == "0")
        {
            DataTable dtService = new DataTable();
            dtService = ds.Tables[0];

            ddlServiceID.DataSource = dtService;
            ddlServiceID.DataTextField = "SERVICENAME";
            ddlServiceID.DataValueField = "SERVICEID";
            ddlServiceID.DataBind();
        }
        else
        {
            throw new IPCException(IPCERRORDESC);
        }
        ddlServiceID.Items.Remove(ddlServiceID.Items.FindByValue(SmartPortal.Constant.IPC.SMS));

        #endregion

        ListItem l3 = new ListItem(Resources.labels.active, "1");
        ListItem l4 = new ListItem(Resources.labels.inactive, "0");
        ddlStatus.Items.Add(l3);
        ddlStatus.Items.Add(l4);
    }
    protected void OnChooseServices(object sender, EventArgs e)
    {
        LoadTypeByService();
        if (ddlServiceID.SelectedValue.Equals(IPC.SEMS))
        {
            DataTable dt = new DataTable();
            dt = new SmartPortal.BLL.PagesBLL().GetAction_Page(ddlServiceID.SelectedValue);
            TABLEACTION = dt;
            genarateColumn();
        }
        LoadControlByService();
    }

    protected void LoadTypeByService()
    {

        DataSet ds = new DataSet();
        DataSet ds1 = new DataSet();
        ds = new SmartPortal.SEMS.Role().GetTypeUserType(ddlServiceID.SelectedValue, ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE == "0")
        {
            ddlUserType.DataSource = ds;
            ddlUserType.DataTextField = ds.Tables[0].Columns["TypeName"].ColumnName.ToString();
            ddlUserType.DataValueField = ds.Tables[0].Columns["TypeID"].ColumnName.ToString();
            ddlUserType.DataBind();
        }
        ds1 = new SmartPortal.SEMS.Role().GetRoleTypeByService(ddlServiceID.SelectedValue, ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE == "0")
        {
            ddlRoleType.DataSource = ds1;
            ddlRoleType.DataTextField = ds1.Tables[0].Columns["RoleDesc"].ColumnName.ToString();
            ddlRoleType.DataValueField = ds1.Tables[0].Columns["RoleType"].ColumnName.ToString();
            ddlRoleType.DataBind();
        }
    }
    private void checkAction()
    {
        switch (ACTION)
        {
            case IPC.ACTIONPAGE.ADD:
                LoadTypeByService();
                OnChooseServices(null, EventArgs.Empty);
                break;
            case IPC.ACTIONPAGE.EDIT:
                ddlServiceID.Enabled = false;
                btnClear.Enabled = false;
                GetInfo();
                break;
            case IPC.ACTIONPAGE.DETAILS:
                GetInfo();
                pnGroupInfo.Enabled = false;
                pnPermission.Enabled = false;
                pnReport.Enabled = false;
                pnRole.Enabled = false;
                btnSave.Enabled = false;
                btnClear.Enabled = false;
                break;
        }
    }
    void GetInfo()
    {
        IdGroup = GetParamsPage(IPC.ID)[0].Trim();
        DataTable group = new MenuBLL().LoadGroupById(IdGroup.ToString());
        ddlServiceID.SelectedValue = group.Rows[0][IPC.SERVICEID].ToString();
        ddlUserType.SelectedValue = group.Rows[0][IPC.USERTYPE].ToString();
        ddlRoleType.SelectedValue = group.Rows[0]["RoleType"].ToString();
        ddlRoleType.Enabled = false;
        OnChooseServices(null, EventArgs.Empty);
        txtGroupName.Text = group.Rows[0][IPC.ROLENAME].ToString();
        txtDescription.Text = group.Rows[0][IPC.ROLEDESCRIPTION].ToString();
        ddlStatus.SelectedValue = group.Rows[0][IPC.STATUS].ToString();
    }
    void LoadControlByService()
    {
        if (ddlServiceID.SelectedValue.Equals(IPC.SEMS))
        {
            pnRole.Visible = false;
            pnPermission.Visible = true;
            pnReport.Visible = true;
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    LoadSourcePermission(0);
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    LoadSourcePermission(int.Parse(IdGroup));
                    btnClear.Enabled = false;
                    break;
                case IPC.ACTIONPAGE.DETAILS:
                    LoadSourcePermission(int.Parse(IdGroup));
                    btnSave.Enabled = false;
                    btnClear.Enabled = false;
                    pnGroupInfo.Enabled = false;
                    pnReport.Enabled = false;
                    break;
            }
        }
        //else if (ddlServiceID.SelectedValue.Equals(IPC.IB))
        //{
        //    pnRole.Visible = false;
        //    pnPermission.Visible = true;
        //    pnReport.Visible = false;
        //    switch (ACTION)
        //    {
        //        case IPC.ACTIONPAGE.ADD:
        //            LoadSourcePermission(0);
        //            break;
        //        case IPC.ACTIONPAGE.EDIT:
        //            LoadSourcePermission(int.Parse(IdGroup));
        //            btnClear.Enabled = false;
        //            break;
        //        case IPC.ACTIONPAGE.DETAILS:
        //            LoadSourcePermission(int.Parse(IdGroup));
        //            btnSave.Enabled = false;
        //            btnClear.Enabled = false;
        //            pnGroupInfo.Enabled = false;
        //            pnReport.Enabled = false;
        //            break;
        //    }
        //}
        else
        {
            pnRole.Visible = true;
            pnPermission.Visible = false;
            pnReport.Visible = false;
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    tvPage.Nodes.Clear();
                    LoadDataInTreeview();
                    RemoveCheck(tvPage.Nodes);
                    LoadRightForUser(0);
                    CheckParent();
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    tvPage.Nodes.Clear();
                    LoadDataInTreeview();
                    RemoveCheck(tvPage.Nodes);
                    LoadRightForUser(int.Parse(IdGroup));
                    CheckParent();
                    break;
                case IPC.ACTIONPAGE.DETAILS:
                    tvPage.Nodes.Clear();
                    LoadDataInTreeview();
                    RemoveCheck(tvPage.Nodes);
                    LoadRightForUser(int.Parse(IdGroup));
                    pnRole.Enabled = false;
                    CheckParent();
                    break;
            }
        }
    }
    private void genarateColumn()
    {
        gvSource.Columns.Clear();

        TemplateField col0 = new TemplateField();
        col0.Visible = false;

        gvSource.Columns.Add(col0);


        TemplateField boundCol1 = new TemplateField();
        boundCol1.HeaderText = "Menu";

        TemplateField boundCol2 = new TemplateField();
        boundCol2.HeaderText = "Sub-Menu";

        TemplateField boundCol3 = new TemplateField();
        boundCol3.HeaderText = "Sub-Menu";

        TemplateField boundCol4 = new TemplateField();
        boundCol4.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
        boundCol4.Visible = true;
        boundCol4.HeaderText = "All";

        gvSource.Columns.Add(boundCol1);
        gvSource.Columns.Add(boundCol2);
        gvSource.Columns.Add(boundCol3);
        gvSource.Columns.Add(boundCol4);
        dtAction = (DataTable)TABLEACTION;
        int countcol = dtAction.Rows.Count;
        if (countcol > 0)
        {
            foreach (DataRow item in dtAction.Rows)
            {
                TemplateField newColumnName = new TemplateField();
                newColumnName.HeaderText = item["Description"].ToString();
                newColumnName.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                newColumnName.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;

                gvSource.Columns.Add(newColumnName);
            }
        }
    }
    private void LoadSourcePermission(int groupid)
    {
        DataTable dt = new MenuBLL().LoadForPermission(groupid, System.Globalization.CultureInfo.CurrentCulture.ToString(), ddlServiceID.SelectedValue);
        if (ddlServiceID.SelectedValue.Equals(IPC.SEMS))
        {
            gvSource.DataSource = dt;
            gvSource.DataBind();
            DataSet dsRole = new Role().GetAllRoleReport(ddlServiceID.SelectedValue, groupid, ref IPCERRORCODE,
                ref IPCERRORDESC);
            if (IPCERRORCODE.Equals("0"))
            {
                DataTable dtRole = dsRole.Tables[0];
                rptRole.DataSource = dtRole;
                rptRole.DataBind();
            }
            else
            {
                ErrorCodeModel EM = new ErrorCodeModel();
                EM = new ErrorBLL().Load(Utility.IsInt(IPCERRORCODE),
                    System.Globalization.CultureInfo.CurrentCulture.ToString());
                lblError.Text = EM.ErrorDesc;
            }
        }
    }
    protected void gvSource_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSource.PageIndex = e.NewPageIndex;
        LoadSourcePermission(Utility.IsInt(hdID.Value));
    }
    protected void gvSource_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int countChecked = 0;
        int countEnabled = 0;
        int rowspan = 0;
        dtAction = (DataTable)TABLEACTION;
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[2].ColumnSpan = 2;
            e.Row.Cells.RemoveAt(3);
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView rowdata = (e.Row.DataItem as DataRowView);

            bool enable = true;
            HiddenField hdPageid = new HiddenField();
            hdPageid.Value = rowdata["PageID"].ToString();
            hdPageid.ID = "hdPageID";

            HiddenField hdMenuid = new HiddenField();
            hdMenuid.Value = rowdata["MenuID"].ToString();
            hdMenuid.ID = "hdMenuID";

            e.Row.Cells[0].Controls.Add(hdPageid);
            e.Row.Cells[0].Controls.Add(hdMenuid);
            if (rowdata["counter"].ToString().Equals("2") && rowdata["LEVEL0"].ToString().Equals("0") && rowdata["LEVEL1"].ToString().Equals("0"))
            {
                e.Row.Cells[1].Text = rowdata["Parent0"].ToString();
                e.Row.Cells[2].Text = rowdata["Parent1"].ToString();
                e.Row.Cells[3].Text = rowdata["MenuTitle"].ToString();
                e.Row.Cells[1].RowSpan = int.Parse(rowdata["COUNTR0"].ToString());
                e.Row.Cells[2].RowSpan = int.Parse(rowdata["COUNTR1"].ToString());
            }
            else if (rowdata["counter"].ToString().Equals("2") && rowdata["LEVEL0"].ToString().Equals("-1") && rowdata["LEVEL1"].ToString().Equals("0"))
            {
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].RowSpan = int.Parse(rowdata["COUNTR1"].ToString());
                e.Row.Cells[2].Text = rowdata["Parent1"].ToString();
                e.Row.Cells[3].Text = rowdata["MenuTitle"].ToString();
            }
            else if (rowdata["counter"].ToString().Equals("1") && rowdata["LEVEL0"].ToString().Equals("0") && rowdata["LEVEL1"].ToString().Equals("0"))
            {
                e.Row.Cells[1].Text = rowdata["Parent0"].ToString();
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Text = rowdata["MenuTitle"].ToString();
                e.Row.Cells[3].ColumnSpan = 2;
                rowspan = int.Parse(rowdata["COUNTR0"].ToString());
                if (rowspan != 0)
                {
                    e.Row.Cells[1].RowSpan = rowspan;
                }
                else
                {
                    e.Row.Cells[1].Visible = false;
                }
            }
            else if (rowdata["counter"].ToString().Equals("1") && rowdata["LEVEL0"].ToString().Equals("-1") && rowdata["LEVEL1"].ToString().Equals("0"))
            {
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Text = rowdata["MenuTitle"].ToString();
                e.Row.Cells[3].ColumnSpan = 2;
                rowspan = int.Parse(rowdata["COUNTR0"].ToString());
                if (rowspan != 0)
                {
                    e.Row.Cells[1].RowSpan = rowspan;
                }
                else
                {
                    e.Row.Cells[1].Visible = false;
                }
            }
            else if (rowdata["counter"].ToString().Equals("2") && rowdata["LEVEL0"].ToString().Equals("-1") && rowdata["LEVEL1"].ToString().Equals("-1"))
            {
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Text = rowdata["MenuTitle"].ToString();
            }
            else
            {
                e.Row.Cells[1].Text = string.Empty;
                e.Row.Cells[2].Text = string.Empty;
                e.Row.Cells[3].Text = string.Empty;
            }

            CheckBox cbAll = new CheckBox();
            cbAll.Enabled = enable;
            cbAll.Attributes.Add("onclick", "AllCell(this, " + e.Row.RowIndex + ");");
            e.Row.Cells[4].Controls.Add(cbAll);

            bool checkall = true;

            for (int i = 5; i < gvSource.Columns.Count; i++)
            {
                CheckBox cb = new CheckBox();
                cb.Enabled = enable;

                //tao menu de tim kiem theo action
                cb.ID = "cb" + dtAction.Rows[i - 5]["ActionKey"].ToString();

                if (string.IsNullOrEmpty(rowdata["PageID"].ToString()))
                {
                    cb.Enabled = false;
                }
                else
                {
                    cb.Checked = rowdata[dtAction.Rows[i - 5]["ActionKey"].ToString()].ToString().Equals("1");
                    if (cb.Checked)
                    {
                        countChecked++;
                    }

                    cb.Enabled = !string.IsNullOrEmpty(rowdata[dtAction.Rows[i - 5]["ActionKey"].ToString()].ToString()) && !hdID.Value.Equals("0");
                    if (!cb.Enabled)
                    {
                        countEnabled++;
                    }
                }
                cb.Attributes.Add("onclick", "ChildCell(this, " + e.Row.RowIndex + ");");
                e.Row.Cells[i].Controls.Add(cb);

                if (checkall) checkall = cb.Checked || (!cb.Enabled && !hdID.Value.Equals("0"));
            }

            if (countChecked == dtAction.Rows.Count - countEnabled)
            {
                cbAll.Checked = true;
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            PagesBLL pBLL = new PagesBLL();
            PageRightBLL prBLL = new PageRightBLL();
            MenuRightBLL mrBLL = new MenuRightBLL();
            ReportRightBLL rBll = new ReportRightBLL();
            int roleid = 0;
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    if (!CheckPermitPageAction(IPC.ACTIONPAGE.ADD)) return;
                    try
                    {
                        roleid = new RoleBLL().Insert(Utility.KillSqlInjection(txtGroupName.Text.Trim()),
                            Utility.KillSqlInjection(txtDescription.Text), Session["userName"].ToString(),
                            Utility.KillSqlInjection(ddlServiceID.SelectedValue.ToString()),
                            Utility.KillSqlInjection(ddlUserType.SelectedValue.ToString()),
                            Utility.KillSqlInjection(ddlStatus.SelectedValue.Trim()),
                            Utility.KillSqlInjection(ddlRoleType.SelectedValue.ToString()));
                        lblError.Text = string.Format(Resources.labels.insertgroupsucessfull, txtGroupName.Text.Trim());
                    }
                    catch
                    {
                        lblError.Text = Resources.labels.groupnamealeadtexist;
                        return;
                    }
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    if (!CheckPermitPageAction(IPC.ACTIONPAGE.EDIT)) return;
                    try
                    {
                        roleid = Utility.IsInt(hdID.Value);
                        RoleBLL r = new RoleBLL();
                        r.Update(roleid, Utility.KillSqlInjection(txtGroupName.Text), Utility.KillSqlInjection(txtDescription.Text), Session["userName"].ToString(), Utility.KillSqlInjection(ddlServiceID.SelectedValue.ToString()), Utility.KillSqlInjection(ddlUserType.SelectedValue.ToString()), Utility.KillSqlInjection(ddlStatus.SelectedValue.Trim()), Utility.KillSqlInjection(ddlRoleType.SelectedValue.ToString()));
                        lblError.Text = Resources.labels.editgroupsuccessfully;
                    }
                    catch
                    {
                        lblError.Text = Resources.labels.groupupdatefailed;
                        return;
                    }
                    break;
            }
            //Set role for page default

            prBLL.InsertPageDefault(roleid);
            mrBLL.InsertMenuDefault(roleid);
            if (ddlServiceID.SelectedValue.Equals(IPC.SEMS))
            {
                foreach (GridViewRow row in gvSource.Rows)
                {
                    var hdPage = row.Cells[0].FindControl("hdPageID") as HiddenField;
                    var hdMenu = row.Cells[0].FindControl("hdMenuID") as HiddenField;
                    string pageid = hdPage.Value;
                    string menuid = hdMenu.Value;
                    if (!string.IsNullOrEmpty(pageid))
                    {
                        DataTable dtPageRef = pBLL.GetAllPageReference(pageid);
                        foreach (DataRow page in dtPageRef.Rows)
                        {
                            var check = row.FindControl("cb" + page["Action"].ToString()) as CheckBox;
                            if (check != null)
                            {
                                prBLL.Insert(roleid, page["PageID"].ToString(), check.Checked);
                                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["PAGE"], System.Configuration.ConfigurationManager.AppSettings["PERMISSION"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLPAGERIGHT"], System.Configuration.ConfigurationManager.AppSettings["PAGEID"], "", page["PageID"].ToString());
                                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["PAGE"], System.Configuration.ConfigurationManager.AppSettings["PERMISSION"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLPAGERIGHT"], System.Configuration.ConfigurationManager.AppSettings["ROLEID"], "", roleid.ToString());
                            }
                        }
                    }
                    var checkMenu = row.FindControl("cb" + IPC.ACTIONPAGE.LIST) as CheckBox;
                    if (checkMenu != null)
                    {
                        mrBLL.Insert(roleid, menuid, checkMenu.Checked);
                        SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["MENU"], System.Configuration.ConfigurationManager.AppSettings["PERMISSION"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLMENURIGHT"], System.Configuration.ConfigurationManager.AppSettings["MENUID"], "", menuid);
                        SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["MENU"], System.Configuration.ConfigurationManager.AppSettings["PERMISSION"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLMENURIGHT"], System.Configuration.ConfigurationManager.AppSettings["ROLEID"], "", roleid.ToString());
                    }
                }
                foreach (RepeaterItem item in rptRole.Items)
                {
                    CheckBox cb = item.FindControl("cbRole") as CheckBox;
                    HiddenField hd = item.FindControl("hdRole") as HiddenField;
                    rBll.Insert(roleid, hd.Value, cb.Checked);
                }
                btnSave.Enabled = false;
                pnGroupInfo.Enabled = false;
                pnReport.Enabled = false;
                pnPermission.Enabled = false;
            }
            else
            {
                #region luu thong tin quyen
                SaveRole(tvPage.Nodes, roleid);
                #endregion
                #region Load quyền theo role
                LoadRightForUser(roleid);
                #endregion
                btnSave.Enabled = false;
                pnGroupInfo.Enabled = false;
                pnRole.Enabled = false;
            }

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }
    void LoadDataInTreeview()
    {
        DataTable tblSS = new DataTable();
        tblSS = new SubSystemBLL().Load();

        foreach (DataRow row in tblSS.Rows)
        {
            TreeNode node = new TreeNode(row["SubSystemName"].ToString(), "ss-" + row["SubSystemID"].ToString());
            node.ShowCheckBox = true;

            DataTable tblPage = new PagesBLL().GetPageBySS(Utility.IsInt(row["SubSystemID"].ToString()), ddlServiceID.SelectedValue.ToString(), ddlRoleType.SelectedValue.ToString());
            foreach (DataRow row1 in tblPage.Rows)
            {
                TreeNode node1 = new TreeNode(row1["PAGENAME"].ToString(), row1["PageID"].ToString());
                node1.ShowCheckBox = true;
                node1.ToolTip = row1["PageDescription"].ToString();
                node.ChildNodes.Add(node1);
            }
            if (tblPage.Rows.Count != 0)
            {
                tvPage.Nodes.Add(node);
            }
        }
        tvPage.Attributes.Add("onclick", "OnTreeClick(event)");
    }
    void LoadRightForUser(int roleid)
    {
        try
        {
            DataTable tblPR = new PageRightBLL().Load(roleid);
            foreach (DataRow rolerow in tblPR.Rows)
            {
                SetRole(tvPage.Nodes, rolerow["PageID"].ToString());
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    public void SetRole(TreeNodeCollection tn, string str)
    {
        for (int i = 0; i < tn.Count; i++)
        {
            if (tn[i].Value == str)
            {
                tn[i].Checked = true;
                break;
            }
            else
            {
                SetRole(tn[i].ChildNodes, str);
            }
        }

    }
    public void CheckParent()
    {
        foreach (TreeNode n in tvPage.Nodes)
        {
            bool flag = false;
            for (int k = 0; k < n.ChildNodes.Count; k++)
            {
                if (n.ChildNodes[k].Checked)
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                    goto NEXT;
                }
            }
        NEXT:
            if (flag)
            {
                n.Checked = true;
            }
            else
            {
                n.Checked = false;
            }
        }
    }
    public void RemoveCheck(TreeNodeCollection tn)
    {
        for (int i = 0; i < tn.Count; i++)
        {
            tn[i].Checked = false;
            RemoveCheck(tn[i].ChildNodes);
        }
    }
    public void SaveRole(TreeNodeCollection tn, int roleID)
    {
        PageRightBLL prBLL = new PageRightBLL();
        for (int i = 0; i < tn.Count; i++)
        {
            if (tn[i].Checked == true)
            {
                try
                {
                    prBLL.Insert(roleID, tn[i].Value, true);
                    //Write Log
                    SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["PAGE"], System.Configuration.ConfigurationManager.AppSettings["PERMISSION"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLPAGERIGHT"], System.Configuration.ConfigurationManager.AppSettings["PAGEID"], "", tn[i].Value);
                    SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["PAGE"], System.Configuration.ConfigurationManager.AppSettings["PERMISSION"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLPAGERIGHT"], System.Configuration.ConfigurationManager.AppSettings["ROLEID"], "", roleID.ToString());

                }
                catch
                {
                }
            }
            else
            {
                try
                {
                    prBLL.Insert(roleID, tn[i].Value, false);
                    //Write Log
                    SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["PAGE"], System.Configuration.ConfigurationManager.AppSettings["PERMISSION"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLPAGERIGHT"], System.Configuration.ConfigurationManager.AppSettings["PAGEID"], "", tn[i].Value);
                    SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["PAGE"], System.Configuration.ConfigurationManager.AppSettings["PERMISSION"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLPAGERIGHT"], System.Configuration.ConfigurationManager.AppSettings["ROLEID"], "", roleID.ToString());

                }
                catch
                {
                }
            }
            SaveRole(tn[i].ChildNodes, roleID);
        }
    }
    protected void ddlRoleType_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        LoadControlByService();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblError.Text = string.Empty;
        pnGroupInfo.Enabled = true;
        pnReport.Enabled = true;
        pnPermission.Enabled = true;
        ddlServiceID.SelectedIndex = 0;
        ddlUserType.SelectedIndex = 0;
        ddlRoleType.SelectedIndex = 0;
        txtGroupName.Text = string.Empty;
        txtDescription.Text = string.Empty;
        ddlStatus.SelectedIndex = 0;
        OnChooseServices(null, EventArgs.Empty);
        btnSave.Enabled = true;
        pnRole.Enabled = true;
    }
}
