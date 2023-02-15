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

public partial class Widgets_PagePermission_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                btnSave.Attributes.Add("onclick", "if(window.confirm('"+Resources.labels.areyousuresavethispermission+"')){return true} else {return false}");

                btnSave1.Attributes.Add("onclick", "if(window.confirm('" + Resources.labels.areyousuresavethispermission + "')){return true} else {return false}");
                //load group
                ddlRole.DataSource = new GroupBLL().Load();
                ddlRole.DataTextField = "RoleName";
                ddlRole.DataValueField = "RoleID";
                ddlRole.DataBind();

                //load data in treeview
                DataTable tblSS = new SubSystemBLL().Load();
                foreach (DataRow row in tblSS.Rows)
                {
                    TreeNode node = new TreeNode(row["SubSystemName"].ToString(),"ss-"+row["SubSystemID"].ToString());
                    node.ShowCheckBox = true;                    

                    DataTable tblPage = new PagesBLL().GetPageBySS(Utility.IsInt(row["SubSystemID"].ToString()),"","");
                    foreach (DataRow row1 in tblPage.Rows)
                    {
                        TreeNode node1 = new TreeNode(row1["PageName"].ToString(), row1["PageID"].ToString());                        
                        node1.ShowCheckBox = true;
                        node1.ToolTip = row1["PageDescription"].ToString();

                        node.ChildNodes.Add(node1);
                    }

                    tvPage.Nodes.Add(node);
                }
               tvPage.Attributes.Add("onclick","OnTreeClick(event)");

                //load quyen cho nguoi dung
               DataTable tblPR = new PageRightBLL().Load(Utility.IsInt(ddlRole.SelectedValue));
               foreach (DataRow rolerow in tblPR.Rows)
               {
                   SetRole(tvPage.Nodes, rolerow["PageID"].ToString());
               }
            }
            catch (SQLException sex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_PagePermission_Widget", "Page_Load", sex.Message, Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
            }
            catch (Exception ex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_PagePermission_Widget", "Page_Load", ex.Message, Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
            }

        }
    }
    public void SetRole(TreeNodeCollection tn,string str)
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
            lblAlert.Text = "";
            RemoveCheck(tvPage.Nodes);
            //load quyen cho nguoi dung
            DataTable tblPR = new PageRightBLL().Load(Utility.IsInt(ddlRole.SelectedValue));
            foreach (DataRow rolerow in tblPR.Rows)
            {
                SetRole(tvPage.Nodes, rolerow["PageID"].ToString());
            }
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_PagePermission_Widget", "ddlRole_SelectedIndexChanged", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_PagePermission_Widget", "ddlRole_SelectedIndexChanged", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            SaveRole(tvPage.Nodes, Utility.IsInt(ddlRole.SelectedValue));
            lblAlert.Text = Resources.labels.insertsucessfull;

           
        }
        catch (BusinessExeption bex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["priec"], "Widgets_PagePermission_Widget", "btnSave_Click", bex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["priec"], Request.Url.Query);
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_PagePermission_Widget", "btnSave_Click", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_PagePermission_Widget", "btnSave_Click", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    public void SaveRole(TreeNodeCollection tn,int roleID)
    {
        PageRightBLL prBLL = new PageRightBLL();
        for (int i = 0; i < tn.Count; i++)
        {
            if (tn[i].Checked == true)
            {
                try
                {
                    //int j = int.Parse(tn[i].Value);
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
                    //int j = int.Parse(tn[i].Value);
                    prBLL.Insert(roleID,tn[i].Value, false);
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
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(System.Configuration.ConfigurationManager.AppSettings["homeadmin"]));
    }
}
