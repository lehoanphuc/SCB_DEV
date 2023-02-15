using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using SmartPortal.BLL;
using SmartPortal.Common.Utilities;
using SmartPortal.Model;
using System.Globalization;
using SmartPortal.ExceptionCollection;

public partial class Widgets_Language_Controls_Widget : WidgetBase
{
    static string langID;
    static string langName;
    static string userModified;
    static string datemodified;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //load culture
                CultureInfo[] cultures = System.Globalization.CultureInfo.GetCultures(CultureTypes.SpecificCultures);
                
                foreach (CultureInfo c in cultures)
                {
                    ddlCulture.Items.Add(new ListItem(c.EnglishName ,c.Name));                   
                } 

                //truong hop edit load thong tin
                if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["lid"] != null)
                {
                    LanguageBLL LB = new LanguageBLL();
                    LanguageModel LM = new LanguageModel();

                    LM = LB.LoadByID(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["lid"].ToString());

                    ddlCulture.SelectedValue = LM.LangID;
                    langID = LM.LangID;

                    langName = LM.LangName;
                    userModified = LM.UserModified;
                    datemodified = LM.DateModified;

                    ddlCulture.Enabled = false;

                    //kiem tra neu view diable control
                    if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["type"] != null)
                    {                       
                        fuLanguageFile.Enabled = false;
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
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_Theme_Controls_Widget", "Page_Load", sex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Theme_Controls_Widget", "Page_Load", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string userName = Session["userName"].ToString();

        if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["lid"] == null)
        {
            try
            {
                //upload icon
                string image = "";
                if (fuLanguageFile.HasFile)
                {
                    string fileName = fuLanguageFile.FileName;
                    string extension = System.IO.Path.GetExtension(fileName).ToLower();
                    string filePath = Server.MapPath(ConfigurationManager.AppSettings["langupload"]);

                    switch (extension)
                    {
                        case ".resx":
                            image = fileName;
                            break;
                        default:
                            throw new BusinessExeption("Only upload file ZIP");
                            break;
                    }

                    //upload
                    fuLanguageFile.SaveAs(filePath + "/label." + ddlCulture.SelectedValue+".resx");                   
                    
                }
                //insert
                LanguageBLL LB = new LanguageBLL();
                LB.Insert(ddlCulture.SelectedValue, ddlCulture.SelectedItem.Text, HttpContext.Current.Session["userName"].ToString());

                //Write Log
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["LANGUAGE"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress,System.Configuration.ConfigurationManager.AppSettings["TBLLANGUAGE"],System.Configuration.ConfigurationManager.AppSettings["LANGID"],"",ddlCulture.SelectedValue);
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["LANGUAGE"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLLANGUAGE"], System.Configuration.ConfigurationManager.AppSettings["LANGNAME"], "", ddlCulture.SelectedItem.Text);
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["LANGUAGE"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLLANGUAGE"], System.Configuration.ConfigurationManager.AppSettings["USERCREATED"], "", HttpContext.Current.Session["userName"].ToString());

                //alert
                lblAlert.Text = Resources.labels.insertsucessfull;
            }
            catch (BusinessExeption bex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["tiec"], "Widgets_Theme_Controls_Widget", "btnSave_Click", bex.ToString(), Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["tiec"], Request.Url.Query);
            }
            catch (SQLException sex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_Theme_Controls_Widget", "btnSave_Click", sex.ToString(), Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
            }
            catch (Exception ex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Theme_Controls_Widget", "btnSave_Click", ex.ToString(), Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
            }

        }
        else
        {
            try
            {
                //upload icon                
                string image = "";
                if (fuLanguageFile.HasFile)
                {
                    string fileName = fuLanguageFile.FileName;
                    string extension = System.IO.Path.GetExtension(fileName).ToLower();
                    string filePath = Server.MapPath(ConfigurationManager.AppSettings["langupload"]);

                    switch (extension)
                    {
                        case ".resx":
                            image = fileName;
                            break;
                        default:
                            throw new BusinessExeption("Only upload file ZIP");
                            break;
                    }

                    //upload
                    fuLanguageFile.SaveAs(filePath + "/label." + ddlCulture.SelectedValue+".resx");                  
                }
                //insert
                LanguageBLL LB = new LanguageBLL();
                string dM=DateTime.Now.ToString("dd/MM/yyyy");
                LB.Update(ddlCulture.SelectedValue, dM, HttpContext.Current.Session["userName"].ToString());

                //Write Log
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["LANGUAGE"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLLANGUAGE"], System.Configuration.ConfigurationManager.AppSettings["LANGID"], langID, ddlCulture.SelectedValue);
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["LANGUAGE"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLLANGUAGE"], System.Configuration.ConfigurationManager.AppSettings["LANGNAME"], langName, ddlCulture.SelectedItem.Text);
                
            }
            catch (BusinessExeption bex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["tuec"], "Widgets_Theme_Controls_Widget", "btnSave_Click", bex.ToString(), Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["tuec"], Request.Url.Query);
            }
            catch (SQLException sex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_Theme_Controls_Widget", "btnSave_Click", sex.ToString(), Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
            }
            catch (Exception ex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Theme_Controls_Widget", "btnSave_Click", ex.ToString(), Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
            }
            
            //respone
           Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(System.Configuration.ConfigurationManager.AppSettings["viewlanguage"]));
        }
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(System.Configuration.ConfigurationManager.AppSettings["viewlanguage"]));
    }
}
