using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SmartPortal.BLL;
using SmartPortal.Common.Utilities;
using SmartPortal.ExceptionCollection;

public partial class Widgets_MenuPermission_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                btnSave.Attributes.Add("onclick", "if(window.confirm('" + Resources.labels.areyousuresavethispermission + "')){return true} else {return false}");

                btnSave1.Attributes.Add("onclick", "if(window.confirm('" + Resources.labels.areyousuresavethispermission + "')){return true} else {return false}");
                //load group
                ddlRole.DataSource = new GroupBLL().Load();
                ddlRole.DataTextField = "RoleName";
                ddlRole.DataValueField = "RoleID";
                ddlRole.DataBind();

                //load data in treeview
                DataTable tblMBLL = new MenuBLL().LoadForPermission(System.Globalization.CultureInfo.CurrentCulture.ToString());
                DataRow []arrRow= tblMBLL.Select("MenuParent='0'");
                foreach (DataRow row in arrRow)
                {
                    TreeNode node = new TreeNode(row["MenuTitle"].ToString(),row["MenuID"].ToString());
                    node.ShowCheckBox = true;

                    tvMenu.Nodes.Add(node);

                    AddNodeChild(tblMBLL, row["MenuID"].ToString().Trim(), node);
                }
                tvMenu.Attributes.Add("onclick", "OnTreeClick(event)");

                //load quyen cho nguoi dung
                DataTable tblPR = new MenuRightBLL().Load(Utility.IsInt(ddlRole.SelectedValue));
                foreach (DataRow rolerow in tblPR.Rows)
                {
                    SetRole(tvMenu.Nodes, rolerow["MenuID"].ToString());
                }
            }
            catch (SQLException sex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_MenuPermission_Widget", "Page_Load", sex.Message, Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
            }
            catch (Exception ex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_MenuPermission_Widget", "Page_Load", ex.Message, Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
            }
        }
    }

    public void AddNodeChild(DataTable tblMenu,string menuID,TreeNode tn)
    {
        DataRow[] arrRow = tblMenu.Select("MenuParent='" + menuID + "'");
        foreach (DataRow row in arrRow)
        {
            TreeNode node = new TreeNode(row["MenuTitle"].ToString(), row["MenuID"].ToString());
            node.ShowCheckBox = true;
            tn.ChildNodes.Add(node);

            AddNodeChild(tblMenu, row["MenuID"].ToString().Trim(), node);
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

    public void RemoveCheck(TreeNodeCollection tn)
    {
        for (int i = 0; i < tn.Count; i++)
        {
            tn[i].Checked = false;
            RemoveCheck(tn[i].ChildNodes);

        }
    }
   
    protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblAlert.Text = "";
            RemoveCheck(tvMenu.Nodes);
            //load quyen cho nguoi dung
            DataTable tblPR = new MenuRightBLL().Load(Utility.IsInt(ddlRole.SelectedValue));
            foreach (DataRow rolerow in tblPR.Rows)
            {
                SetRole(tvMenu.Nodes, rolerow["MenuID"].ToString());
            }
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_MenuPermission_Widget", "ddlRole_SelectedIndexChanged", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_MenuPermission_Widget", "ddlRole_SelectedIndexChanged", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            SaveRole(tvMenu.Nodes, Utility.IsInt(ddlRole.SelectedValue));
            lblAlert.Text = Resources.labels.insertsucessfull;
        }
        catch (BusinessExeption bex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["mriec"], "Widgets_MenuPermission_Widget", "btnSave_Click", bex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["mriec"], Request.Url.Query);
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_MenuPermission_Widget", "btnSave_Click", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_MenuPermission_Widget", "btnSave_Click", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    public void SaveRole(TreeNodeCollection tn, int roleID)
    {
        MenuRightBLL prBLL = new MenuRightBLL();
        for (int i = 0; i < tn.Count; i++)
        {
            if (tn[i].Checked == true)
            {
                try
                {
                    //int j = int.Parse(tn[i].Value);
                    prBLL.Insert(roleID, Utility.KillSqlInjection(tn[i].Value), true);
                    //Write Log
                    SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["MENU"], System.Configuration.ConfigurationManager.AppSettings["PERMISSION"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLMENURIGHT"], System.Configuration.ConfigurationManager.AppSettings["MENUID"], "", tn[i].Value);
                    SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["MENU"], System.Configuration.ConfigurationManager.AppSettings["PERMISSION"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLMENURIGHT"], System.Configuration.ConfigurationManager.AppSettings["ROLEID"], "", roleID.ToString());
                }
                catch
                {
                }
            }
            else
            {
                try
                {
                    //int j = int.Parse(tn[i].Value);
                    prBLL.Insert(roleID, Utility.KillSqlInjection(tn[i].Value), false);
                    //Write Log
                    SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["MENU"], System.Configuration.ConfigurationManager.AppSettings["PERMISSION"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLMENURIGHT"], System.Configuration.ConfigurationManager.AppSettings["MENUID"], "", tn[i].Value);
                    SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["MENU"], System.Configuration.ConfigurationManager.AppSettings["PERMISSION"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLMENURIGHT"], System.Configuration.ConfigurationManager.AppSettings["ROLEID"], "", roleID.ToString());

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
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(System.Configuration.ConfigurationManager.AppSettings["homeadmin"]));
    }
}
