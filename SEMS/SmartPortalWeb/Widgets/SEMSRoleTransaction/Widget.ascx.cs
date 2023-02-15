using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.BLL;
using SmartPortal.Common.Utilities;
using System.Data;
using System.Data.SqlClient;
using SmartPortal.ExceptionCollection;

public partial class Widgets_SEMSRoleTransaction_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";

    void LoadRoleByService()
    {
        DataTable roleTable = new DataTable();
        //DataSet ds = new SmartPortal.SEMS.Role().GetRoleByServiceID(ddlService.SelectedValue.Trim(),"", ref IPCERRORCODE, ref IPCERRORDESC);
        //if (ds.Tables.Count != 0)
        //{
        //    roleTable = ds.Tables[0];
        //}
        string ServiceID = ddlService.SelectedValue.Trim();
        roleTable = new RoleBLL().LoadForView("", ServiceID, 1000, 1);

        ddlRole.DataSource = roleTable;
        ddlRole.DataTextField = "ROLENAME";
        ddlRole.DataValueField = "ROLEID";
        ddlRole.DataBind();
    }

    void LoadDataInTreeview()
    {
        DataTable tblSS = new DataTable();
        ////08.06.2016 minh tach sms notify vs sms transaction
        //if (ddlService.SelectedValue == SmartPortal.Constant.IPC.SMS)
        //{
        //    tblSS = new SubSystemBLL().LoadForServiceSMSbyRoleID(ddlRole.SelectedValue);
        //}
        //else
        //{

        //}
        tblSS = new SubSystemBLL().Load();

        foreach (DataRow row in tblSS.Rows)
        {
            TreeNode node = new TreeNode(row["SubSystemName"].ToString(), "ss-" + row["SubSystemID"].ToString());
            node.ShowCheckBox = true;

            DataTable tblPage = new PagesBLL().GetPageBySS(Utility.IsInt(row["SubSystemID"].ToString()), ddlService.SelectedValue == "EW" ? "MB" : ddlService.SelectedValue, ddlRole.SelectedValue);
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

    void LoadDataInTreeview1()
    {
        DataTable tblSS = new DataTable();
        ////08.06.2016 minh tach sms notify vs sms transaction
        //if (ddlService.SelectedValue == SmartPortal.Constant.IPC.SMS)
        //{
        //    tblSS = new SubSystemBLL().LoadForServiceSMSbyRoleID(ddlRole.SelectedValue);
        //}
        //else
        //{

        //}
        tblSS = new SubSystemBLL().Load();

        foreach (DataRow row in tblSS.Rows)
        {
            TreeNode node = new TreeNode(row["SubSystemName"].ToString(), "ss-" + row["SubSystemID"].ToString());
            node.ShowCheckBox = true;

            DataTable tblPage = new PagesBLL().GetPageBySS(Utility.IsInt(row["SubSystemID"].ToString()), ddlService.SelectedValue=="EW"?"MB": ddlService.SelectedValue, ddlRole.SelectedValue);
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
    void LoadRightForUser()
    {
        DataTable tblPR = new PageRightBLL().Load(Utility.IsInt(ddlRole.SelectedValue));
        foreach (DataRow rolerow in tblPR.Rows)
        {
            SetRole(tvPage.Nodes, rolerow["PageID"].ToString());
        }

        //CheckParent();
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            try
            {
                btnSave.Attributes.Add("onclick", "if(window.confirm('" + Resources.labels.areyousuresavethispermission + "')){return true} else {return false}");
                #region load service
                DataSet ds = new DataSet();
                ds = new SmartPortal.SEMS.Services().GetAll("", ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    DataTable dtService = new DataTable();
                    dtService = ds.Tables[0];

                    ddlService.DataSource = dtService;
                    ddlService.DataTextField = "SERVICENAME";
                    ddlService.DataValueField = "SERVICEID";
                    ddlService.DataBind();
                    if (ddlService.Items.FindByValue("SEMS") != null)
                        ddlService.SelectedValue = "SEMS";
                }
                else
                {
                    throw new IPCException(IPCERRORDESC);
                }
                #endregion

                #region Load group theo service
                LoadRoleByService();
                #endregion

                #region load group
                //ddlRole.DataSource = new GroupBLL().Load();
                //ddlRole.DataTextField = "RoleName";
                //ddlRole.DataValueField = "RoleID";
                //ddlRole.DataBind();
                #endregion

                #region load data in treeview
                LoadDataInTreeview();
                #endregion

                #region load quyen cho nguoi dung
                LoadRightForUser();

                #endregion


            }
            catch (IPCException IPCex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_PagePermission_Widget", "Page_Load", IPCex.ToString(), Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
            }
            catch (SQLException sex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_PagePermission_Widget", "Page_Load", sex.ToString(), Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
            }
            catch (Exception ex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_PagePermission_Widget", "Page_Load", ex.ToString(), Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
            }

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
    protected void tvPage_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
    {
        TreeNodeCollection tNC = new TreeNodeCollection();
        tNC = e.Node.ChildNodes;
        foreach (TreeNode tn in tNC)
        {
            tn.Checked = true;
        }
    }
    protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //08.06.2016 minh load treeview again
            #region Load data in treeview
            tvPage.Nodes.Clear();
            LoadDataInTreeview1();
            #endregion
            lblAlert.Text = "";
            RemoveCheck(tvPage.Nodes);
            //load quyen cho nguoi dung
            DataTable tblPR = new PageRightBLL().Load(Utility.IsInt(ddlRole.SelectedValue));
            foreach (DataRow rolerow in tblPR.Rows)
            {
                SetRole(tvPage.Nodes, rolerow["PageID"].ToString());
            }
            //check parent
            CheckParent();
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_PagePermission_Widget", "ddlRole_SelectedIndexChanged", sex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_PagePermission_Widget", "ddlRole_SelectedIndexChanged", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlRole.SelectedValue == "")
            {
                lblAlert.Text = Resources.labels.khongtontaiquyensudung;
                return;
            }

            #region luu thong tin quyen
            SaveRole(tvPage.Nodes, Utility.IsInt(ddlRole.SelectedValue));
            #endregion

            #region luu nhung page mac dinh
            PageRightBLL prBLL = new PageRightBLL();
            prBLL.InsertPageDefault(Utility.IsInt(ddlRole.SelectedValue));
            #endregion

            #region Load quyền theo role
            //LoadRoleByService();
            LoadRightForUser();
            #endregion


            lblAlert.Text = Resources.labels.insertsucessfull;
        }
        catch (BusinessExeption bex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["priec"], "Widgets_PagePermission_Widget", "btnSave_Click", bex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["priec"], Request.Url.Query);
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_PagePermission_Widget", "btnSave_Click", sex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_PagePermission_Widget", "btnSave_Click", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
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
                    int j = int.Parse(tn[i].Value);
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
                    int j = int.Parse(tn[i].Value);
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
    protected void btnExit_Click(object sender, EventArgs e)
    {
        //Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["homeadmin"]);
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(System.Configuration.ConfigurationManager.AppSettings["SEMSADMIN"]));
    }
    protected void ddlService_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            #region Load group theo service
            LoadRoleByService();
            #endregion

            #region Load data in treeview
            tvPage.Nodes.Clear();
            LoadDataInTreeview();
            #endregion

            #region Load quyền theo role
            lblAlert.Text = "";
            RemoveCheck(tvPage.Nodes);
            //load quyen cho nguoi dung
            DataTable tblPR = new PageRightBLL().Load(Utility.IsInt(ddlRole.SelectedValue));
            foreach (DataRow rolerow in tblPR.Rows)
            {
                SetRole(tvPage.Nodes, rolerow["PageID"].ToString());
            }
            #endregion
            //check parent
            CheckParent();

        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_PagePermission_Widget", "ddlRole_SelectedIndexChanged", sex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_PagePermission_Widget", "ddlRole_SelectedIndexChanged", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
}
