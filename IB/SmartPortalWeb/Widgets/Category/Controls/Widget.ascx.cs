using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.Common.Utilities;
using SmartPortal.BLL;
using System.Data;
using SmartPortal.Model;
using SmartPortal.ExceptionCollection;

public partial class Widgets_Category_Controls_Widget : System.Web.UI.UserControl
{
    private static string catName;
    private static string catDescription;
    private static string link;
    private static string tag;
    private static string order;
    private static string parent;
    private static string isshow;
    private static string lang;
    private static string userModified;
    private static string dateModified;
    private static string catID;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //load category
                CategoryBLL CB = new CategoryBLL();

                //load data in treeview
                DataTable tblMBLL = CB.LoadCategory(System.Globalization.CultureInfo.CurrentCulture.ToString());
                DataRow[] arrRow = tblMBLL.Select("ParentID='0'");
                foreach (DataRow row in arrRow)
                {
                    ListItem node = new ListItem(row["CatName"].ToString(), row["CatID"].ToString());
                    ddlParent.Items.Add(node);

                    AddNodeChild(tblMBLL, row["CatID"].ToString().Trim(), 1);
                }
                ddlParent.Items.Insert(0, new ListItem("Root","0"));

                //load order
                for (int i = 0; i < Utility.IsInt(System.Configuration.ConfigurationManager.AppSettings["ordernumber"]); i++)
                {
                    ddlOrder.Items.Add(new ListItem(i.ToString(),i.ToString()));
                }

                //truong hop edit load thong tin
                if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["catid"] != null)
                {
                    CategoryBLL CBB = new CategoryBLL();
                    CategoryModel CM = new CategoryModel();

                    CM = CBB.LoadByID(Utility.IsInt(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["catid"]));

                    txtTitle.Text = CM.CatName;
                    catName = CM.CatName; 

                    txtLink.Text = CM.Link;
                    link = CM.Link;

                    txtDescription.Text = CM.CatDescription;
                    catDescription = CM.CatDescription;

                    ddlParent.SelectedValue = CM.ParentID.ToString();
                    parent = CM.ParentID.ToString();

                    txtTag.Text = CM.Tag;
                    tag = CM.Tag;

                    ddlOrder.SelectedValue = CM.Order.ToString();
                    order = CM.Order.ToString();

                    //cbIsShow.Checked = CM.IsPublished;
                    isshow = CM.IsPublished.ToString();

                    lang = CM.LangID;
                    catID = CM.CatID.ToString();
                    
                    //kiem tra neu view diable control
                    if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["type"] != null)
                    {
                        txtTitle.Enabled = false;
                        txtLink.Enabled = false;
                        txtTag.Enabled = false;
                        ddlParent.Enabled = false;
                        ddlOrder.Enabled = false;
                        cbIsShow.Enabled = false;
                        txtDescription.Enabled = false;
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
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_Category_Controls_Widget", "Page_Load", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Category_Controls_Widget", "Page_Load", ex.Message, Request.Url.Query);
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

        DataRow[] arrRow = tblMenu.Select("ParentID='" + menuID + "'");
        foreach (DataRow row in arrRow)
        {
            ListItem node = new ListItem(chuoi + row["CatName"].ToString(), row["CatID"].ToString());
            ddlParent.Items.Add(node);

            AddNodeChild(tblMenu, row["CatID"].ToString().Trim(), so + 1);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["catid"] == null)
        {
            int smt = 0;
            try
            {
                //insert
                CategoryBLL CB = new CategoryBLL();
                smt=CB.Insert(Utility.KillSqlInjection(txtTitle.Text), Utility.KillSqlInjection(txtDescription.Text), Utility.IsInt(ddlParent.SelectedValue), System.Globalization.CultureInfo.CurrentCulture.ToString(), 0, Utility.IsInt(ddlOrder.SelectedValue), Utility.KillSqlInjection(txtLink.Text), Utility.KillSqlInjection(txtTag.Text), Session["userName"].ToString());

                //Write Log
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["CATEGORY"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLCATEGORY"], System.Configuration.ConfigurationManager.AppSettings["CATNAME"], "", Utility.KillSqlInjection(txtTitle.Text));
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["CATEGORY"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLCATEGORY"], System.Configuration.ConfigurationManager.AppSettings["CATDESCRIPTION"], "", Utility.KillSqlInjection(txtDescription.Text));
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["CATEGORY"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLCATEGORY"], System.Configuration.ConfigurationManager.AppSettings["PARENTID"], "", ddlParent.SelectedValue);
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["CATEGORY"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLCATEGORY"], System.Configuration.ConfigurationManager.AppSettings["LANGID"], "", System.Globalization.CultureInfo.CurrentCulture.ToString());
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["CATEGORY"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLCATEGORY"], System.Configuration.ConfigurationManager.AppSettings["ISPUBLISHED"], "", cbIsShow.Checked.ToString());
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["CATEGORY"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLCATEGORY"], System.Configuration.ConfigurationManager.AppSettings["ORDER"], "", ddlOrder.SelectedValue);
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["CATEGORY"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLCATEGORY"], System.Configuration.ConfigurationManager.AppSettings["LINK"], "", Utility.KillSqlInjection(txtLink.Text));
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["CATEGORY"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLCATEGORY"], System.Configuration.ConfigurationManager.AppSettings["TAG"], "", Utility.KillSqlInjection(txtTag.Text));
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["CATEGORY"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLCATEGORY"], System.Configuration.ConfigurationManager.AppSettings["USERCREATED"], "", Session["userName"].ToString());
            }
            catch (BusinessExeption bex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["catiec"], "Widgets_Category_Controls_Widget", "btnSave_Click", bex.Message, Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["catiec"], Request.Url.Query);
            }
            catch (SQLException sex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_Category_Controls_Widget", "btnSave_Click", sex.Message, Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
            }
            catch (Exception ex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Category_Controls_Widget", "btnSave_Click", ex.Message, Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
            }
            //alert
            lblAlert.Text = Resources.labels.insertsucessfull+"["+smt.ToString()+"]";
            txtTitle.Text = "";
            txtTag.Text = "";
            txtLink.Text = "";
            txtDescription.Text = "";
        }
        else
        {
            try
            {

                //update
                CategoryBLL CB = new CategoryBLL();
                string dM=DateTime.Now.ToString("dd/MM/yyyy");
                CB.Update(Utility.KillSqlInjection(txtTitle.Text), Utility.KillSqlInjection(txtDescription.Text), Utility.IsInt(ddlParent.SelectedValue), System.Globalization.CultureInfo.CurrentCulture.ToString(), 0, Utility.IsInt(ddlOrder.SelectedValue), Utility.KillSqlInjection(txtLink.Text), Utility.KillSqlInjection(txtTag.Text), Session["userName"].ToString(),dM,Utility.IsInt(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["catid"]));

                //Write Log
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["CATEGORY"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLCATEGORY"], System.Configuration.ConfigurationManager.AppSettings["CATID"], catID, catID);
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["CATEGORY"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLCATEGORY"], System.Configuration.ConfigurationManager.AppSettings["CATNAME"], catName, Utility.KillSqlInjection(txtTitle.Text));
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["CATEGORY"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLCATEGORY"], System.Configuration.ConfigurationManager.AppSettings["CATDESCRIPTION"], catDescription, Utility.KillSqlInjection(txtDescription.Text));
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["CATEGORY"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLCATEGORY"], System.Configuration.ConfigurationManager.AppSettings["PARENTID"], parent, ddlParent.SelectedValue);
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["CATEGORY"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLCATEGORY"], System.Configuration.ConfigurationManager.AppSettings["LANGID"],lang , System.Globalization.CultureInfo.CurrentCulture.ToString());
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["CATEGORY"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLCATEGORY"], System.Configuration.ConfigurationManager.AppSettings["ISPUBLISHED"], isshow, cbIsShow.Checked.ToString());
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["CATEGORY"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLCATEGORY"], System.Configuration.ConfigurationManager.AppSettings["ORDER"], order, ddlOrder.SelectedValue);
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["CATEGORY"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLCATEGORY"], System.Configuration.ConfigurationManager.AppSettings["LINK"], link, Utility.KillSqlInjection(txtLink.Text));
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["CATEGORY"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLCATEGORY"], System.Configuration.ConfigurationManager.AppSettings["TAG"], tag, Utility.KillSqlInjection(txtTag.Text));
         
            }
            catch (BusinessExeption bex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["catuec"], "Widgets_Category_Controls_Widget", "btnSave_Click", bex.Message, Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["catuec"], Request.Url.Query);
            }
            catch (SQLException sex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_Category_Controls_Widget", "btnSave_Click", sex.Message, Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
            }
            catch (Exception ex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Category_Controls_Widget", "btnSave_Click", ex.Message, Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
            }

                        
            //alert
            Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["viewcategory"]);
        }
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["viewcategory"]);
    }
}
