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

public partial class Widgets_SEMSRoleAPI_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";

    void LoadRoleByService()
    {
        DataTable roleTable = new DataTable();
        roleTable = new RoleBLL().LoadForView("", ddlService.SelectedValue.Trim(), 1000, 1);

        ddlRole.DataSource = roleTable;
        ddlRole.DataTextField = "ROLENAME";
        ddlRole.DataValueField = "ROLEID";
        ddlRole.DataBind();
    }

    void LoadDataInTreeview()
    {
        DataTable tblSS = new DataTable();
        tblSS = new SmartPortal.SEMS.CardLessWithdrawal().GetListAPIByGroup(ddlRole.SelectedValue.Trim(), ref IPCERRORCODE, ref IPCERRORDESC);
        tvPage.Nodes.Clear();

        foreach (DataRow row in tblSS.Rows)
        {
            TreeNode node = new TreeNode(row["APIName"].ToString(), row["APIID"].ToString());
            node.ShowCheckBox = true;
            node.Checked = row["HASROLE"].ToString().Equals("Y");
            tvPage.Nodes.Add(node);
        }
        tvPage.Attributes.Add("onclick", "OnTreeClick(event)");
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

                #region load data in treeview
                LoadDataInTreeview();
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
            #region Load data in treeview
            tvPage.Nodes.Clear();
            LoadDataInTreeview();
            #endregion
            lblAlert.Text = "";
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
            DataTable dtEditAPIRight = new DataTable();
            dtEditAPIRight.Columns.Add(new DataColumn("APIID"));
            dtEditAPIRight.Columns.Add(new DataColumn("RoleID"));
            dtEditAPIRight.Columns.Add(new DataColumn("isDel"));

            SaveRole(ref dtEditAPIRight, tvPage.Nodes, Utility.IsInt(ddlRole.SelectedValue));

            new SmartPortal.SEMS.CardLessWithdrawal().EditAPIRight(dtEditAPIRight, ref IPCERRORCODE, ref IPCERRORDESC);

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
    public void SaveRole(ref DataTable dtEditAPIRight, TreeNodeCollection tn, int roleID)
    {
        for (int i = 0; i < tn.Count; i++)
        {
            if (tn[i].Checked == true)
            {
                try
                {
                    int j = 0;
                    DataRow row = dtEditAPIRight.NewRow();
                    row[j++] = tn[i].Value;
                    row[j++] = roleID;
                    row[j++] = "N";
                    dtEditAPIRight.Rows.Add(row);
                }
                catch
                {
                }
            }
            else
            {
                try
                {
                    int j = 0;
                    DataRow row = dtEditAPIRight.NewRow();
                    row[j++] = tn[i].Value;
                    row[j++] = roleID;
                    row[j++] = "Y";
                    dtEditAPIRight.Rows.Add(row);
                }
                catch
                {
                }
            }
            SaveRole(ref dtEditAPIRight, tn[i].ChildNodes, roleID);
        }

    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
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
