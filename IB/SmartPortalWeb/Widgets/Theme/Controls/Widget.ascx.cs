using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using SmartPortal.BLL;
using SmartPortal.Common.Utilities;
using SmartPortal.Model;
using SmartPortal.ExceptionCollection;
using System.IO;

public partial class Widgets_Theme_Controls_Widget : WidgetBase
{
    static string themeName;
    static string themeDesc;
    static string themeID;
    static string userModi;
    static string dateModi;
    static string file;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                //truong hop edit load thong tin
                if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["tid"] != null)
                {
                    ThemeBLL TB = new ThemeBLL();
                    ThemeModel TM = new ThemeModel();

                    TM = TB.LoadByID(Utility.IsInt(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["tid"].ToString()));

                    txtThemeName.Text = TM.ThemeName;
                    themeName = TM.ThemeName;

                    txtThemeDescription.Text = TM.ThemeDescription;
                    themeDesc = TM.ThemeDescription;

                    userModi = TM.UserModified;
                    dateModi = TM.DateModified;
                    themeID = TM.ThemeID.ToString();

                    
                    file = TM.FileName;
                    //fuThemeFile.Enabled = false;

                    //kiem tra neu view diable control
                    if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["type"] != null)
                    {
                        txtThemeName.Enabled = false;
                        txtThemeDescription.Enabled = false;
                        btnSave.Visible = false;
                        fuThemeFile.Enabled = false;
                        btnSave1.Visible = false;
                        imgSave.Visible = false;
                        imgSave1.Visible = false;
                    }

                }
            }

        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_Theme_Controls_Widget", "Page_Load", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Theme_Controls_Widget", "Page_Load", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["tid"] == null)
        {
            try
            {
                //upload icon
                string image = "";
                if (fuThemeFile.HasFile)
                {
                    string fileName =fuThemeFile.FileName;
                    string extension = System.IO.Path.GetExtension(fileName).ToLower();
                    string filePath = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["themeupload"]);

                    switch (extension)
                    {
                        case ".zip":
                            image = fileName;
                            break;
                        default:
                            throw new BusinessExeption("1", new Exception("Only upload ZIP file"));
                            break;
                    }
                    //if (fileName.Replace(extension, "").Trim() == txtThemeName.Text.Trim())
                    {
                       // try
                        {
                            fuThemeFile.SaveAs(filePath + fileName);
                            //exact file zip
                            SmartPortal.Common.ZipHelp.UnZip(filePath+fileName, filePath, 4096, Utility.IsInt(System.Configuration.ConfigurationManager.AppSettings["filenumber"]));
                        }
                        //catch
                        //{
                            
                        //    throw new BusinessExeption("3", new Exception("File Upload exist on server"));
                        //}

                        //insert
                        ThemeBLL TB = new ThemeBLL();
                        TB.Insert(Utility.KillSqlInjection(txtThemeName.Text.Trim()), Utility.KillSqlInjection(txtThemeDescription.Text.Trim()), HttpContext.Current.Session["userName"].ToString(), fileName.Replace(extension, "").Trim());

                        //Write Log
                        SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["THEME"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLTHEME"], System.Configuration.ConfigurationManager.AppSettings["THEMENAME"], "", Utility.KillSqlInjection(txtThemeName.Text.Trim()));
                        SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["THEME"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLTHEME"], System.Configuration.ConfigurationManager.AppSettings["THEMEDESCRIPTION"], "", Utility.KillSqlInjection(txtThemeDescription.Text.Trim()));
                        SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["THEME"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLTHEME"], System.Configuration.ConfigurationManager.AppSettings["USERCREATED"], "", HttpContext.Current.Session["userName"].ToString());
                        //alert
                        lblAlert.Text = Resources.labels.insertsucessfull;

                        txtThemeName.Text = "";
                        txtThemeDescription.Text = "";
                    }
                    //else
                    //{
                    //    throw new BusinessExeption("2",new Exception("Theme name and file name is same."));
                    //}
                }
                else
                {
                    //alert
                    lblAlert.Text = Resources.labels.pleasechoosefileupload;
                }
            }
            catch (BusinessExeption bex)
            {
                if(bex.Message=="1")
                {
                    SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["zipec"], "Widgets_Theme_Controls_Widget", "btnSave_Click", bex.InnerException.Message, Request.Url.Query);
                    SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["zipec"], Request.Url.Query);
                }
                else
                {
                    if(bex.Message=="2")
                    {
                        SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["filesameec"], "Widgets_Theme_Controls_Widget", "btnSave_Click", bex.InnerException.Message, Request.Url.Query);
                        SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["filesameec"], Request.Url.Query);
                    }
                    else
                    {
                        if (bex.Message == "3")
                        {
                            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["uploadthemeec"], "Widgets_Theme_Controls_Widget", "btnSave_Click", bex.InnerException.Message, Request.Url.Query);
                            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["uploadthemeec"], Request.Url.Query);
                        }
                        else
                        {
                            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["themeiec"], "Widgets_Theme_Controls_Widget", "btnSave_Click", bex.Message, Request.Url.Query);
                            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["themeiec"], Request.Url.Query);
                        }
                    }
                }
            }
            catch (SQLException sex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_Theme_Controls_Widget", "btnSave_Click", sex.Message, Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
            }
            catch (Exception ex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Theme_Controls_Widget", "btnSave_Click", ex.Message, Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
            }
           
        }
        else
        {
            try
            {                
                //update
                ThemeBLL TB = new ThemeBLL();

                //upload icon
                string image = "";

                string fileName = file;

                if (fuThemeFile.HasFile)
                {
                    fileName = fuThemeFile.FileName;
                    string extension = System.IO.Path.GetExtension(fileName).ToLower();
                    string filePath = Server.MapPath(ConfigurationManager.AppSettings["themeupload"]);

                    //if (fileName.Replace(extension, "").Trim() == txtThemeName.Text.Trim())
                    //{                    

                        switch (extension)
                        {
                            case ".zip":
                                image = fileName;
                                break;
                            default:
                                throw new BusinessExeption("1", new Exception("Only upload ZIP file"));
                                break;
                        }

                        fuThemeFile.SaveAs(filePath + fileName);
                        //exact file zip
                        SmartPortal.Common.ZipHelp.UnZip(filePath + fileName, filePath, 4096, Utility.IsInt(System.Configuration.ConfigurationManager.AppSettings["filenumber"]));
                    //}
                    //else
                    //{
                    //    throw new BusinessExeption("2", new Exception("Theme name and file name is same."));
                    //}

                        fileName = fileName.Replace(extension, "");
                }
                        string dM = DateTime.Now.ToString("dd/MM/yyyy");
                        TB.Update(Utility.IsInt(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["tid"]), Utility.KillSqlInjection(txtThemeName.Text.Trim()), Utility.KillSqlInjection(txtThemeDescription.Text.Trim()), HttpContext.Current.Session["userName"].ToString(), dM, fileName.Trim());

                        //Write Log
                        SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["THEME"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLTHEME"], System.Configuration.ConfigurationManager.AppSettings["THEMENAME"], themeName, Utility.KillSqlInjection(txtThemeName.Text.Trim()));
                        SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["THEME"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLTHEME"], System.Configuration.ConfigurationManager.AppSettings["THEMEDESCRIPTION"], themeDesc, Utility.KillSqlInjection(txtThemeDescription.Text.Trim()));
                        SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["THEME"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLTHEME"], System.Configuration.ConfigurationManager.AppSettings["THEMEID"], themeID, SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["tid"]);
                        SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["THEME"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLTHEME"], System.Configuration.ConfigurationManager.AppSettings["FILENAME"], file, fileName);
                    
               
            }
            catch (BusinessExeption bex)
            {
                if (bex.Message == "1")
                {
                    SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["zipec"], "Widgets_Theme_Controls_Widget", "btnSave_Click", bex.InnerException.Message, Request.Url.Query);
                    SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["zipec"], Request.Url.Query);
                }
                else
                {
                    if (bex.Message == "2")
                    {
                        SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["filesameec"], "Widgets_Theme_Controls_Widget", "btnSave_Click", bex.InnerException.Message, Request.Url.Query);
                        SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["filesameec"], Request.Url.Query);
                    }
                    else
                    {
                        SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["themeuec"], "Widgets_Theme_Controls_Widget", "btnSave_Click", bex.Message, Request.Url.Query);
                        SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["themeuec"], Request.Url.Query);
                    }
                }
            }
            catch (SQLException sex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_Theme_Controls_Widget", "btnSave_Click", sex.Message, Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
            }
            catch (Exception ex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Theme_Controls_Widget", "btnSave_Click", ex.Message, Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
            }

            //respone
            Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["viewtheme"]);
        }
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["viewtheme"]);
    }
}
