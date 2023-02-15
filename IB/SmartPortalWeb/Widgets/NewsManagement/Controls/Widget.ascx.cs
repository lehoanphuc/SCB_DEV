using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SmartPortal.Common.Utilities;
using SmartPortal.BLL;
using SmartPortal.Model;
using SmartPortal.ExceptionCollection;

public partial class Widgets_NewsManagement_Controls_Widget : System.Web.UI.UserControl
{
    static string title;
    static string summary;
    static string content;
    static string catID;
    static string userModified;
    static string dateModified;

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
                    //if (row["Link"].ToString() != "")
                    //{
                    //    node.Enabled = false;                        
                    //}
                    ddlCategory.Items.Add(node);

                    AddNodeChild(tblMBLL, row["CatID"].ToString().Trim(), 1);
                }

                //truong hop edit load thong tin
                if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["nid"] != null)
                {
                    NewsBLL NB = new NewsBLL();
                    NewsModel NM = new NewsModel();

                    NM = NB.LoadNewsByID(Utility.IsInt(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["nid"]),0);

                    ddlCategory.SelectedValue = NM.CatID.ToString();
                    catID = NM.CatID.ToString();

                    txtTitle.Text = NM.Title;
                    title = NM.Title;

                    fckSummary.Value = NM.Summary;
                    summary = NM.Summary;

                    fckContent.Value = NM.Content;
                    content = NM.Content;

                    userModified = NM.UserModified;
                    dateModified = NM.DateModified;

                    //kiem tra neu view diable control
                    if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["type"] != null)
                    {
                        txtTitle.Enabled = false;
                        ddlCategory.Enabled = false;                        
                        btnSave.Visible = false;
                        btnSave1.Visible = false;
                        imgSave.Visible = false;
                        imgSave1.Visible = false;

                        lblSummary.Text = NM.Summary;
                        fckSummary.Visible = false;
                        lblContent.Text = NM.Content;
                        fckContent.Visible = false;
                    }
                    
                }
            }

        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_NewsManagement_Controls_Widget", "Page_Load", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_NewsManagement_Controls_Widget", "Page_Load", ex.Message, Request.Url.Query);
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
            ddlCategory.Items.Add(node);

            //if (row["Link"].ToString() != "")
            //{
            //    node.Enabled = false;
            //}

            AddNodeChild(tblMenu, row["CatID"].ToString().Trim(), so + 1);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["nid"] == null)
        {
            try
            {    
                
                //insert
                NewsBLL NB = new NewsBLL();
                NB.Insert(Utility.KillSqlInjection(txtTitle.Text), Utility.KillSqlInjection(fckSummary.Value),Utility.KillSqlInjection(fckContent.Value),Session["userName"].ToString(),Utility.IsInt(ddlCategory.SelectedValue));

                //Write Log
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["NEWS"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLNEWS"], System.Configuration.ConfigurationManager.AppSettings["TITLE"], "", Utility.KillSqlInjection(txtTitle.Text));
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["NEWS"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLNEWS"], System.Configuration.ConfigurationManager.AppSettings["SUMMARY"], "", Utility.KillSqlInjection(fckSummary.Value));
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["NEWS"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLNEWS"], System.Configuration.ConfigurationManager.AppSettings["CONTENT"], "", Utility.KillSqlInjection(fckContent.Value));
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["NEWS"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLNEWS"], System.Configuration.ConfigurationManager.AppSettings["NEWSAUTHOR"], "", Session["userName"].ToString());
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["NEWS"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLNEWS"], System.Configuration.ConfigurationManager.AppSettings["CATID"], "", ddlCategory.SelectedValue);
            }
            catch (BusinessExeption bex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["niec"], "Widgets_NewsManagement_Controls_Widget", "btnSave_Click", bex.Message, Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["niec"], Request.Url.Query);
            }
            catch (SQLException sex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_NewsManagement_Controls_Widget", "btnSave_Click", sex.Message, Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
            }
            catch (Exception ex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_NewsManagement_Controls_Widget", "btnSave_Click", ex.Message, Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
            }

            //alert
            lblAlert.Text = Resources.labels.insertsucessfull;

            txtTitle.Text = "";
            fckSummary.Value = "";
            fckContent.Value = "";
        }
        else
        {
            try
            {

                //update
                NewsBLL NB = new NewsBLL();
                string dM=System.DateTime.Now.ToString("dd/MM/yyyy");
                NB.Update(Utility.KillSqlInjection(txtTitle.Text), Utility.KillSqlInjection(fckSummary.Value), Utility.KillSqlInjection(fckContent.Value), Session["userName"].ToString(), Utility.IsInt(ddlCategory.SelectedValue),dM,Utility.IsInt(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["nid"]));

                //Write Log
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["NEWS"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLNEWS"], System.Configuration.ConfigurationManager.AppSettings["NEWSID"], SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["nid"].ToString(), SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["nid"].ToString());
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["NEWS"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLNEWS"], System.Configuration.ConfigurationManager.AppSettings["TITLE"], title, Utility.KillSqlInjection(txtTitle.Text));
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["NEWS"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLNEWS"], System.Configuration.ConfigurationManager.AppSettings["SUMMARY"], summary, Utility.KillSqlInjection(fckSummary.Value));
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["NEWS"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLNEWS"], System.Configuration.ConfigurationManager.AppSettings["CONTENT"], content, Utility.KillSqlInjection(fckContent.Value));
                
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["NEWS"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLNEWS"], System.Configuration.ConfigurationManager.AppSettings["CATID"], catID, ddlCategory.SelectedValue);
                

               
            }
            catch (BusinessExeption bex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["nuec"], "Widgets_NewsManagement_Controls_Widget", "btnSave_Click", bex.Message, Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["nuec"], Request.Url.Query);
            }
            catch (SQLException sex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_NewsManagement_Controls_Widget", "btnSave_Click", sex.Message, Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
            }
            catch (Exception ex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_NewsManagement_Controls_Widget", "btnSave_Click", ex.Message, Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
            }

            //alert
            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(System.Configuration.ConfigurationManager.AppSettings["viewnews"]));
        }
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(System.Configuration.ConfigurationManager.AppSettings["viewnews"]));
    }
}
