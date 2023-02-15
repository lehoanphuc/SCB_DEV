using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.Common.Utilities;
using SmartPortal.BLL;
using System.Configuration;
using SmartPortal.Model;
using SmartPortal.ExceptionCollection;

public partial class Widgets_Widget_Controls_Widget : System.Web.UI.UserControl
{
    static string widgettitle;
    static string langID;
    static string link;
    static string iconpath;
    static string enabletheme;
    static string showtitle;
    static string ispublish;
    static string usermodi;
    static string datemodi;
    static string widgetid;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {


                //truong hop edit load thong tin
                if (Request["wid"] != null)
                {
                    WidgetsBLL WBLL = new WidgetsBLL();
                    WidgetsModel WM = new WidgetsModel();
                    WidgetsModel WM1 = new WidgetsModel();

                    WM = WBLL.GetWidgetByID(Request["wid"].ToString().Trim());

                    txtWidgetid.Text = WM.WidgetID;
                    txtWidgetid.Enabled = false;
                    widgetid = WM.WidgetID;

                    txtPath.Text = WM.WidgetLink;
                    link = WM.WidgetLink;

                    cbEnableTheme.Checked = WM.EnableTheme;
                    enabletheme = WM.EnableTheme.ToString();

                    cbShowTitle.Checked = WM.ShowTitle;
                    showtitle = WM.ShowTitle.ToString();

                    cbIsShow.Checked = WM.IsPublish;
                    ispublish = WM.IsPublish.ToString();

                    if (WM.IconPath != "")
                    {
                        //lblPicture.Text = "Picture";
                        fuIcon.Attributes.Add("onmousemove", "Tip('" + "<img src=\\'" + ConfigurationManager.AppSettings["widgeticonpathupload"].Replace("~/", "") + WM.IconPath + "\\'/>" + "')");
                        fuIcon.Attributes.Add("onmouseout", "UnTip()");
                    }
                    lblPicture.Text = WM.IconPath;
                    iconpath = WM.IconPath;

                    WM1 = WBLL.GetWidgetTitleByID(Request["wid"].ToString().Trim(), System.Globalization.CultureInfo.CurrentCulture.ToString());
                    txtTitle.Text = WM1.WidgetTitle;
                    widgettitle = WM1.WidgetTitle;
                    langID = WM1.LangID;
                    widgetid = WM1.WidgetID.ToString();

                    if (txtTitle.Text == "")
                    {
                        txtPath.Enabled = false;
                        fuIcon.Enabled = false;
                        cbEnableTheme.Enabled = false;
                        cbIsShow.Enabled = false;
                        cbShowTitle.Enabled = false;
                    }
                    else
                    {
                        txtPath.Enabled = true;
                        fuIcon.Enabled = true;
                        cbEnableTheme.Enabled = true;
                        cbIsShow.Enabled = true;
                        cbShowTitle.Enabled = true;
                    }
                    //kiem tra neu view diable control
                    if (Request["type"] != null)
                    {
                        txtTitle.Enabled = false;
                        txtPath.Enabled = false;
                        fuIcon.Enabled = false;
                        cbEnableTheme.Enabled = false;
                        cbShowTitle.Enabled = false;
                        cbIsShow.Enabled = false;
                        btnSave.Visible = false;
                        LinkButton1.Visible = false;
                        imgSave1.Visible = false;
                        imgSave.Visible = false;
                    }
                }
            }

        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_Widget_Controls_Widget", "Page_Load", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Widget_Controls_Widget", "Page_Load", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Request["wid"] == null)
        {
            try
            {
                //upload icon
                string image = "";
                if (fuIcon.HasFile)
                {
                    string fileName = System.IO.Path.GetRandomFileName().Replace(".", "") + "_" + fuIcon.FileName;
                    string extension = System.IO.Path.GetExtension(fileName).ToLower();
                    string filePath = Server.MapPath(ConfigurationManager.AppSettings["widgeticonpathupload"]) + fileName;

                    switch (extension)
                    {
                        case ".jpg":
                            image = fileName;
                            break;
                        case ".gif":
                            image = fileName;
                            break;
                        case ".png":
                            image = fileName;
                            break;
                        case ".ico":
                            image = fileName;
                            break;
                        default:
                            throw new BusinessExeption("1", new Exception("Only Upload .jpg .gif .png .ico"));
                            break;
                    }   
                    fuIcon.SaveAs(filePath);
                }
                //insert
                try
                {
                    WidgetsBLL WB = new WidgetsBLL();
                    WB.Insert(Utility.KillSqlInjection(txtWidgetid.Text.Trim()),
                        System.Globalization.CultureInfo.CurrentCulture.ToString(),
                        Utility.KillSqlInjection(txtTitle.Text), Utility.KillSqlInjection(txtPath.Text),
                        Session["userName"].ToString(), image, cbEnableTheme.Checked, cbShowTitle.Checked,
                        cbIsShow.Checked);
                }
                catch
                {
                    lblAlert.Text = Resources.labels.widgetidAlreadyExist;
                    return;

                }

                //Write Log 
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["WIDGET"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLWIDGET"], System.Configuration.ConfigurationManager.AppSettings["WIDGETID"], "", Utility.KillSqlInjection(txtWidgetid.Text.Trim()));
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["WIDGET"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLWIDGET"], System.Configuration.ConfigurationManager.AppSettings["LINK"], "", Utility.KillSqlInjection(txtPath.Text));
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["WIDGET"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLWIDGET"], System.Configuration.ConfigurationManager.AppSettings["USERCREATED"], "", Session["userName"].ToString());
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["WIDGET"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLWIDGET"], System.Configuration.ConfigurationManager.AppSettings["ICONPATH"], "", image);
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["WIDGET"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLWIDGET"], System.Configuration.ConfigurationManager.AppSettings["ENABLETHEME"], "", cbEnableTheme.Checked.ToString());
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["WIDGET"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLWIDGET"], System.Configuration.ConfigurationManager.AppSettings["SHOWTITLE"], "", cbShowTitle.Checked.ToString());
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["WIDGET"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLWIDGET"], System.Configuration.ConfigurationManager.AppSettings["ISPUBLISH"], "", cbIsShow.Checked.ToString());
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["WIDGET"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLWIDGETLANG"], System.Configuration.ConfigurationManager.AppSettings["LANGID"], "", System.Globalization.CultureInfo.CurrentCulture.ToString());
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["WIDGET"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLWIDGETLANG"], System.Configuration.ConfigurationManager.AppSettings["WIDGETTITLE"], "", Utility.KillSqlInjection(txtTitle.Text));
            }
            catch (BusinessExeption bex)
            {
                if (bex.Message == "1")
                {
                    SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["imageuploadec"], "Widgets_Widget_Controls_Widget", "btnSave_Click", bex.InnerException.Message, Request.Url.Query);
                    SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["imageuploadec"], Request.Url.Query);
                }
                else
                {
                    SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["widgetiec"], "Widgets_Widget_Controls_Widget", "btnSave_Click", bex.Message, Request.Url.Query);
                    SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["widgetiec"], Request.Url.Query);
                }
            }
            catch (SQLException sex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_Widget_Controls_Widget", "btnSave_Click", sex.Message, Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
            }
            catch (Exception ex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Widget_Controls_Widget", "btnSave_Click", ex.Message, Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
            }

            //alert
            lblAlert.Text = Resources.labels.insertsucessfull;

            txtTitle.Text = "";
            txtPath.Text = "";
        }
        else
        {
            try
            {
                //upload icon
                string image = lblPicture.Text;
                if (fuIcon.HasFile)
                {
                    string fileName = System.IO.Path.GetRandomFileName().Replace(".", "") + "_" + fuIcon.FileName;
                    string extension = System.IO.Path.GetExtension(fileName).ToLower();
                    string filePath = Server.MapPath(ConfigurationManager.AppSettings["widgeticonpathupload"]) + fileName;

                    switch (extension)
                    {
                        case ".jpg":
                            image = fileName;
                            break;
                        case ".gif":
                            image = fileName;
                            break;
                        case ".png":
                            image = fileName;
                            break;
                        case ".ico":
                            image = fileName;
                            break;
                        default:
                            throw new BusinessExeption("1", new Exception("Only Upload .jpg .gif .png .ico"));
                            break;
                    }
                    fuIcon.SaveAs(filePath);
                }
                //update
                WidgetsBLL WB = new WidgetsBLL();
                WB.Update(Utility.KillSqlInjection(Request["wid"].ToString().Trim()), System.Globalization.CultureInfo.CurrentCulture.ToString(), Utility.KillSqlInjection(txtTitle.Text), Utility.KillSqlInjection(txtPath.Text), Session["userName"].ToString(), image, cbEnableTheme.Checked, cbShowTitle.Checked, cbIsShow.Checked, DateTime.Now);

                //Write Log
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["WIDGET"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLWIDGET"], System.Configuration.ConfigurationManager.AppSettings["WIDGETID"], link, Utility.KillSqlInjection(txtWidgetid.Text.Trim()));
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["WIDGET"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLWIDGET"], System.Configuration.ConfigurationManager.AppSettings["LINK"], link, Utility.KillSqlInjection(txtPath.Text));

                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["WIDGET"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLWIDGET"], System.Configuration.ConfigurationManager.AppSettings["ICONPATH"], iconpath, image);
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["WIDGET"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLWIDGET"], System.Configuration.ConfigurationManager.AppSettings["ENABLETHEME"], enabletheme, cbEnableTheme.Checked.ToString());
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["WIDGET"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLWIDGET"], System.Configuration.ConfigurationManager.AppSettings["SHOWTITLE"], showtitle, cbShowTitle.Checked.ToString());
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["WIDGET"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLWIDGET"], System.Configuration.ConfigurationManager.AppSettings["ISPUBLISH"], ispublish, cbIsShow.Checked.ToString());
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["WIDGET"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLWIDGETLANG"], System.Configuration.ConfigurationManager.AppSettings["LANGID"], langID, System.Globalization.CultureInfo.CurrentCulture.ToString());
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["WIDGET"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLWIDGETLANG"], System.Configuration.ConfigurationManager.AppSettings["WIDGETTITLE"], widgettitle, Utility.KillSqlInjection(txtTitle.Text));

                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["WIDGET"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLWIDGETLANG"], System.Configuration.ConfigurationManager.AppSettings["WIDGETID"], widgetid, Request["wid"].ToString());
            }
            catch (BusinessExeption bex)
            {
                if (bex.Message == "1")
                {
                    SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["imageuploadec"], "Widgets_Widget_Controls_Widget", "btnSave_Click", bex.InnerException.Message, Request.Url.Query);
                    SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["imageuploadec"], Request.Url.Query);
                }
                else
                {
                    SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["widgetuec"], "Widgets_Widget_Controls_Widget", "btnSave_Click", bex.Message, Request.Url.Query);
                    SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["widgetuec"], Request.Url.Query);
                }
            }
            catch (SQLException sex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_Widget_Controls_Widget", "btnSave_Click", sex.Message, Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
            }
            catch (Exception ex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Widget_Controls_Widget", "btnSave_Click", ex.Message, Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
            }
            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(System.Configuration.ConfigurationManager.AppSettings["viewwidget"]));
        }
    }

    protected void btnExit_Click1(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(System.Configuration.ConfigurationManager.AppSettings["viewwidget"]));
    }

}
