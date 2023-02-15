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

public partial class Widgets_IBCorpRoleTransaction_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    string contractNo = "";

    void LoadRoleByService()
    {
       
            DataTable roleTable = new DataTable();
            DataSet ds = new SmartPortal.SEMS.Role().GetRoleByServiceID(ddlService.SelectedValue.Trim(), contractNo, ref IPCERRORCODE, ref IPCERRORDESC);
            if (ds.Tables.Count != 0)
            {
                roleTable = ds.Tables[0];
            }
            else
            {
                ddlRole.Items.Insert(0, new ListItem("-----"+Resources.labels.khongtontai+"-----",""));
            }
            ddlRole.DataSource = roleTable;
            ddlRole.DataTextField = "ROLENAME";
            ddlRole.DataValueField = "ROLEID";
            ddlRole.DataBind();
           
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            #region Lấy thông tin Contract
            DataSet dsUser = new SmartPortal.IB.User().GetFullUserByUID(Session["userID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE != "0")
            {
                throw new IPCException(IPCERRORDESC);
            }
            DataTable dtUser = dsUser.Tables[0];
            if (dtUser.Rows.Count != 0)
            {
                contractNo = dtUser.Rows[0]["CONTRACTNO"].ToString();
            }
            #endregion

            if (!IsPostBack)
            {
               
                    btnSave.Attributes.Add("onclick", "if(window.confirm('" + Resources.labels.areyousuresavethispermission + "')){return true} else {return false}");

                    btnSave1.Attributes.Add("onclick", "if(window.confirm('" + Resources.labels.areyousuresavethispermission + "')){return true} else {return false}");

                    #region load service
                    DataSet ds = new DataSet();
                    ds = new SmartPortal.SEMS.Services().GetAll(SmartPortal.Constant.IPC.ACTIVE, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        DataTable dtService = new DataTable();
                        dtService = ds.Tables[0];

                        ddlService.DataSource = dtService;
                        ddlService.DataTextField = "SERVICENAME";
                        ddlService.DataValueField = "SERVICEID";
                        ddlService.DataBind();

                        //ddlServiceID.Items.Insert(0, new ListItem("Tất cả", "ALL"));
                    }
                    else
                    {
                        throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.IPC);
                    }
                    #endregion

                    #region Load group theo service
                    LoadRoleByService();
                    #endregion

                    #region load data in treeview
                    tvPage.Nodes.Clear();

                    DataTable tblSS = new SubSystemBLL().Load();
                    foreach (DataRow row in tblSS.Rows)
                    {
                        TreeNode node = new TreeNode(row["SubSystemName"].ToString(), "ss-" + row["SubSystemID"].ToString());
                        node.ShowCheckBox = true;

                        DataTable tblPage = new PagesBLL().GetPageByContract(Utility.IsInt(row["SubSystemID"].ToString()), contractNo, ddlService.SelectedValue);
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
                    #endregion

                    #region load quyen cho nguoi dung
                    try
                    {
                        DataTable tblPR = new PageRightBLL().Load(Utility.IsInt(ddlRole.SelectedValue));
                        foreach (DataRow rolerow in tblPR.Rows)
                        {
                            SetRole(tvPage.Nodes, rolerow["PageID"].ToString());
                        }
                    }
                    catch
                    {
                        throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.MUSTADDGROUP);
                    }
                    #endregion
               

            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.Message, this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

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

        //check node cha nếu node con check
        CheckParent();
    }

    public void CheckParent()
    {
        foreach (TreeNode n in tvPage.Nodes)
        {
            for (int i = 0; i < n.ChildNodes.Count; i++)
            {
                if (n.ChildNodes[i].Checked)
                {
                    n.Checked = true;
                    break;
                }
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
        //TreeNodeCollection tNC = new TreeNodeCollection();
        //tNC = e.Node.ChildNodes;
        //foreach (TreeNode tn in tNC)
        //{
        //    tn.Checked = true;
        //}
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
            if (ddlRole.SelectedValue == "")
            {
                lblAlert.Text = "Không tồn tại quyền sử dụng";
                return;
            }
           
            //huy tat cac cac menu cua role
            new SmartPortal.IB.Role().DeleteMenuRight(ddlRole.SelectedValue);

            //LƯU QUYỀN
            SaveRole(tvPage.Nodes, Utility.IsInt(ddlRole.SelectedValue));

            //luu nhung page mac dinh
            PageRightBLL prBLL = new PageRightBLL();
            prBLL.InsertPageDefault(Utility.IsInt(ddlRole.SelectedValue));

            //thiết lập menu mặc định
            //List<string> lstRoleID = new List<string>();
            //lstRoleID = (List<string>)Session["roleID"];

            //for (int i = 0; i < lstRoleID.Count; i++)
            //{
            //    new SmartPortal.IB.Role().SetMenuForUser(lstRoleID[i], ddlRole.SelectedValue.Trim());
            //}
            
            #region Load quyền theo role
            LoadRoleByService();
            #endregion

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
                    int j = int.Parse(tn[i].Value);
                    //prBLL.Insert(roleID, Utility.IsInt(tn[i].Value), true);
                    //Write Log
                    SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["PAGE"], System.Configuration.ConfigurationManager.AppSettings["PERMISSION"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLPAGERIGHT"], System.Configuration.ConfigurationManager.AppSettings["PAGEID"], "", tn[i].Value);
                    SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["PAGE"], System.Configuration.ConfigurationManager.AppSettings["PERMISSION"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLPAGERIGHT"], System.Configuration.ConfigurationManager.AppSettings["ROLEID"], "", roleID.ToString());

                    //tem quyen menu
                    new SmartPortal.IB.Role().SetMenuForUser(roleID, Utility.IsInt(tn[i].Value), true);
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
                    //prBLL.Insert(roleID, Utility.IsInt(tn[i].Value), false);
                    //Write Log
                    SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["PAGE"], System.Configuration.ConfigurationManager.AppSettings["PERMISSION"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLPAGERIGHT"], System.Configuration.ConfigurationManager.AppSettings["PAGEID"], "", tn[i].Value);
                    SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["PAGE"], System.Configuration.ConfigurationManager.AppSettings["PERMISSION"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLPAGERIGHT"], System.Configuration.ConfigurationManager.AppSettings["ROLEID"], "", roleID.ToString());


                    //tem quyen menu
                    //new SmartPortal.IB.Role().SetMenuForUser(roleID, Utility.IsInt(tn[i].Value), true);
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
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?po=3&p=190"));
    }
    protected void ddlService_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            #region Load group theo service
            LoadRoleByService();
            #endregion

            #region load data in treeview
            tvPage.Nodes.Clear();

            DataTable tblSS = new SubSystemBLL().Load();
            foreach (DataRow row in tblSS.Rows)
            {
                TreeNode node = new TreeNode(row["SubSystemName"].ToString(), "ss-" + row["SubSystemID"].ToString());
                node.ShowCheckBox = true;

                DataTable tblPage = new PagesBLL().GetPageByContract(Utility.IsInt(row["SubSystemID"].ToString()), contractNo, ddlService.SelectedValue);
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
            #endregion
            
            #region Load quyền theo role
            lblAlert.Text = "";
            RemoveCheck(tvPage.Nodes);
            //load quyen cho nguoi dung
            if (ddlRole.SelectedValue != "")
            {
                DataTable tblPR = new PageRightBLL().Load(Utility.IsInt(ddlRole.SelectedValue));
                foreach (DataRow rolerow in tblPR.Rows)
                {
                    SetRole(tvPage.Nodes, rolerow["PageID"].ToString());
                }
            }
            else
            {
                ddlRole.Items.Add(new ListItem("-----Không có-----", ""));
            }
            #endregion

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
}
