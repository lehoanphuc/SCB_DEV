using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.BLL;
using SmartPortal.Model;
using SmartPortal.Common.Utilities;
using System.Configuration;
using System.Data;
using SmartPortal.ExceptionCollection;

public partial class Widgets_Menu_Controls_Widget : System.Web.UI.UserControl
{
    static string menuTitle;
    static string link;
    static string portalID;
    static string menuOrder;
    static string menuParent;
    static string isPublished;
    static string langID;
    static string userModified;
    static string dateModified;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //load portal info
                PortalBLL PB = new PortalBLL();
                ddlPortal.DataSource = PB.Load();
                ddlPortal.DataTextField = "PortalName";
                ddlPortal.DataValueField = "PortalID";
                ddlPortal.DataBind();

                //load page info
                PagesBLL PBL = new PagesBLL();
                ddlPage.DataSource = PBL.Load();
                ddlPage.DataTextField = "PageName";
                ddlPage.DataValueField = "PageID";
                ddlPage.DataBind();

                //get serviceid by pageid
                string serviceid = "";
                serviceid = PagesBLL.GetServiceidByPageId(ddlPage.DataValueField);

                //load data in treeview
                DataTable tblMBLL = new MenuBLL().LoadForPermission(System.Globalization.CultureInfo.CurrentCulture.ToString(), serviceid);
                DataRow[] arrRow = tblMBLL.Select("MenuParent='0'");
                foreach (DataRow row in arrRow)
                {
                    ListItem node = new ListItem(row["MenuTitle"].ToString(), row["MenuID"].ToString());
                    ddlParent.Items.Add(node);

                    AddNodeChild(tblMBLL, row["MenuID"].ToString().Trim(), 1);
                }
                ddlParent.Items.Insert(0, new ListItem("Root", "0"));

                ddlOrder.Items.Clear();
                //load order
                for (int i = 0; i < Utility.IsInt(ConfigurationManager.AppSettings["ordernumber"]); i++)
                {
                    ddlOrder.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }

                //truong hop edit load thong tin
                if (Request["mid"] != null)
                {
                    MenuBLL MBLL = new MenuBLL();
                    MenuModel MM = new MenuModel();

                    MM = MBLL.GetInfoByID(Utility.KillSqlInjection(Request["mid"].ToString()));



                    txtMenuid.Text = MM.MenuID;
                    txtMenuid.Enabled = false;

                    LoadMenuByPortal();

                    ddlOrder.SelectedValue = MM.MenuOrder.ToString();
                    menuOrder = MM.MenuOrder.ToString();

                    ddlParent.SelectedValue = MM.MenuParent.ToString();
                    menuParent = MM.MenuParent.ToString();

                    cbIsShow.Checked = MM.IsPublished;
                    isPublished = MM.IsPublished.ToString();

                    link = MM.MenuLink;
                    userModified = MM.UserModified;
                    dateModified = MM.DateModified;

                    if (MM.MenuLink.Trim() == "#")
                    {
                        cbLink.Checked = false;
                        ddlPage.Enabled = false;
                    }
                    else
                    {
                        cbLink.Checked = true;
                        ddlPage.SelectedValue = MM.MenuLink.Split('&')[1].Split('=')[1].Trim();
                    }

                    //get title theo lang
                    MM = MBLL.GetByID(Utility.KillSqlInjection(Request["mid"].ToString()), System.Globalization.CultureInfo.CurrentCulture.ToString());
                    txtTitle.Text = MM.MenuTitle;
                    menuTitle = MM.MenuTitle;
                    langID = MM.MenuLang;

                    if (txtTitle.Text == "")
                    {
                        ddlParent.Enabled = false;
                        ddlPortal.Enabled = false;
                        ddlPage.Enabled = false;
                        ddlOrder.Enabled = false;
                        cbIsShow.Enabled = false;
                        cbLink.Enabled = false;
                    }
                    else
                    {
                        ddlParent.Enabled = true;
                        ddlPortal.Enabled = true;
                        ddlPage.Enabled = true;
                        ddlOrder.Enabled = true;
                        cbIsShow.Enabled = true;
                        cbLink.Enabled = true;
                    }
                    //kiem tra neu view diable control
                    if (Request["type"] != null)
                    {
                        ddlParent.Enabled = false;
                        ddlPortal.Enabled = false;
                        ddlPage.Enabled = false;
                        ddlOrder.Enabled = false;
                        cbIsShow.Enabled = false;
                        cbLink.Enabled = false;
                        txtTitle.Enabled = false;
                        btnSave.Visible = false;
                        btnSave1.Visible = false;
                        imgSave.Visible = false;
                        imgSave1.Visible = false;
                    }
                }
            }

        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_Menu_Controls_Widget", "Page_Load", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Menu_Controls_Widget", "Page_Load", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    public void AddNodeChild(DataTable tblMenu, string menuID, int so)
    {
        string chuoi = "";
        for (int i = 0; i < so; i++)
        {
            chuoi += "- ";
        }

        DataRow[] arrRow = tblMenu.Select("MenuParent='" + menuID + "'");
        foreach (DataRow row in arrRow)
        {
            ListItem node = new ListItem(chuoi + row["MenuTitle"].ToString(), row["MenuID"].ToString());
            ddlParent.Items.Add(node);

            AddNodeChild(tblMenu, row["MenuID"].ToString().Trim(), so + 1);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (Request["mid"] == null)
        {
            try
            {
                //insert
                MenuBLL MB = new MenuBLL();
                string link = "";
                string serviceid = "";
                if (cbLink.Checked)
                {
                    link = "?p=" + ddlPage.SelectedValue.Trim();
                    serviceid = PagesBLL.GetServiceidByPageId(ddlPage.SelectedValue.Trim());

                }
                else
                {
                    link = "#";
                }

                MB.Insert(Utility.KillSqlInjection(txtMenuid.Text), Utility.KillSqlInjection(txtTitle.Text), link, serviceid, Utility.IsInt(ddlOrder.SelectedValue), Utility.KillSqlInjection(ddlParent.SelectedValue), cbIsShow.Checked, Session["userName"].ToString(), System.Globalization.CultureInfo.CurrentCulture.ToString(), ddlPage.SelectedValue.Trim());

                //load menu
                LoadMenuByPortal();
            }
            catch (BusinessExeption bex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["miec"], "Widgets_Menu_Controls_Widget", "btnSave_Click", bex.Message, Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["miec"], Request.Url.Query);
            }
            catch (SQLException sex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_Menu_Controls_Widget", "btnSave_Click", sex.Message, Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
            }
            catch (Exception ex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Menu_Controls_Widget", "btnSave_Click", ex.Message, Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
            }

            //alert
            lblAlert.Text = Resources.labels.insertsucessfull;

            txtTitle.Text = "";
            txtMenuid.Text = "";
        }
        else
        {
            try
            {
                //update
                MenuBLL MB = new MenuBLL();
                string link = "";
                string serviceid = "";
                if (cbLink.Checked)
                {
                    link = "?p=" + ddlPage.SelectedValue.Trim();
                    serviceid = PagesBLL.GetServiceidByPageId(ddlPage.SelectedValue.Trim());

                }
                else
                {
                    link = "#";
                }

                string dM = DateTime.Now.ToString("dd/MM/yyyy");
                MB.Update(Utility.KillSqlInjection(Request["mid"].ToString()), Utility.KillSqlInjection(txtTitle.Text), link, serviceid, Utility.IsInt(ddlOrder.SelectedValue), Utility.KillSqlInjection(ddlParent.SelectedValue), cbIsShow.Checked, Session["userName"].ToString(), System.Globalization.CultureInfo.CurrentCulture.ToString(), dM, ddlPage.SelectedValue.Trim());


                //Write Log
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["MENU"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLMENU"], System.Configuration.ConfigurationManager.AppSettings["MENUID"], Request["mid"].ToString(), Request["mid"].ToString());
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["MENU"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLMENULANG"], System.Configuration.ConfigurationManager.AppSettings["MENUTITLE"], menuTitle, Utility.KillSqlInjection(txtTitle.Text));
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["MENU"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLMENULANG"], System.Configuration.ConfigurationManager.AppSettings["LANGID"], langID, System.Globalization.CultureInfo.CurrentCulture.ToString());
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["MENU"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLMENU"], System.Configuration.ConfigurationManager.AppSettings["LINK"], link, link);
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["MENU"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLMENU"], System.Configuration.ConfigurationManager.AppSettings["PORTALID"], portalID, ddlPortal.SelectedValue);
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["MENU"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLMENU"], System.Configuration.ConfigurationManager.AppSettings["MENUORDER"], menuOrder, ddlOrder.SelectedValue);
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["MENU"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLMENU"], System.Configuration.ConfigurationManager.AppSettings["MENUPARENT"], menuParent, ddlParent.SelectedValue);
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["MENU"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLMENU"], System.Configuration.ConfigurationManager.AppSettings["ISPUBLISHED"], isPublished, cbIsShow.Checked.ToString());

            }
            catch (BusinessExeption bex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["muec"], "Widgets_Menu_Controls_Widget", "btnSave_Click", bex.Message, Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["muec"], Request.Url.Query);
            }
            catch (SQLException sex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_Menu_Controls_Widget", "btnSave_Click", sex.Message, Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
            }
            catch (Exception ex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Menu_Controls_Widget", "btnSave_Click", ex.Message, Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
            }
            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=168"));
        }

    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["viewmenu"]);
    }

    protected void cbLink_CheckedChanged(object sender, EventArgs e)
    {
        if (cbLink.Checked)
        {
            ddlPage.Enabled = true;
        }
        else
        {
            ddlPage.Enabled = false;
        }
    }

    protected void btnExit_Click1(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=168"));
    }
    protected void ddlPortal_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            LoadMenuByPortal();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Menu_Controls_Widget", "ddlPortal_SelectedIndexChanged", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    void LoadMenuByPortal()
    {
        ddlParent.Items.Clear();
        string serviceid = "";
        serviceid = PagesBLL.GetServiceidByPageId(ddlPage.SelectedValue.Trim());

        //load data in treeview
        DataTable tblMBLL = new MenuBLL().LoadForPermission(System.Globalization.CultureInfo.CurrentCulture.ToString(), serviceid);
        DataRow[] arrRow = tblMBLL.Select("MenuParent='0'");
        foreach (DataRow row in arrRow)
        {
            ListItem node = new ListItem(row["MenuTitle"].ToString(), row["MenuID"].ToString());
            ddlParent.Items.Add(node);

            AddNodeChild(tblMBLL, row["MenuID"].ToString().Trim(), 1);
        }
        ddlParent.Items.Insert(0, new ListItem("Root", "0"));
    }
}
