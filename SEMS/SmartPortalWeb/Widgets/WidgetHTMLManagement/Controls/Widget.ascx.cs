using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.Common.Utilities;
using SmartPortal.BLL;
using SmartPortal.Model;
using SmartPortal.ExceptionCollection;

public partial class Widgets_WidgetHTMLManagement_Controls_Widget : System.Web.UI.UserControl
{
    static string widgetid;
    static string widgetcontent;
    static string usermodi;
    static string datemodi;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //load widget
                ddlWidget.DataSource = new WidgetsBLL().LoadReader(System.Globalization.CultureInfo.CurrentCulture.ToString());
                ddlWidget.DataTextField = "WidgetTitle";
                ddlWidget.DataValueField = "WidgetID";
                ddlWidget.DataBind();

                //truong hop edit load thong tin
                if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["wid"] != null)
                {
                    HTMLWidgetBLL WB = new HTMLWidgetBLL();
                    HTMLWidgetModel WM = new HTMLWidgetModel();

                    WM = WB.GetByID(Utility.IsInt(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["wid"].ToString()),System.Globalization.CultureInfo.CurrentCulture.ToString());

                    ddlWidget.SelectedValue = WM.WidgetID.ToString();
                    widgetid = WM.WidgetID.ToString();

                    fckContent.Value = WM.WidgetContent;
                    widgetcontent = WM.WidgetContent;

                    usermodi = WM.UserModified;
                    datemodi = WM.DateModified;
                  
                    //kiem tra neu view diable control
                    if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["type"] != null)
                    {
                        ddlWidget.Enabled = false;                        
                        btnSave.Visible = false;
                        btnSave1.Visible = false;
                        imgSave.Visible = false;
                        imgSave1.Visible = false;
                        lblContent.Text = WM.WidgetContent;
                        fckContent.Visible = false;
                    }

                }
            }

        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_WidgetHTMLManagement_Controls_Widget", "Page_Load", sex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_WidgetHTMLManagement_Controls_Widget", "Page_Load", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["wpid"] == null)
        {
            //alert
           Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(System.Configuration.ConfigurationManager.AppSettings["viewhtmlwidget"]));
        }
        else
        {
            //redirect trang truoc
            WidgetPageBLL wpB = new WidgetPageBLL();
            WidgetPageModel wpM = new WidgetPageModel();
            wpM = wpB.GetByID(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["wpid"].ToString());

           Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("Default.aspx?p=" + wpM.PageID));
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["wid"] == null)
        {
            try
            {

                //insert
                HTMLWidgetBLL RB = new HTMLWidgetBLL();
                RB.Insert(Utility.IsInt(ddlWidget.SelectedValue), Utility.KillSqlInjection(fckContent.Value), HttpContext.Current.Session["userName"].ToString());

                //Write Log
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["HTMLWIDGET"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress,System.Configuration.ConfigurationManager.AppSettings["TBLHTMLWIDGET"],System.Configuration.ConfigurationManager.AppSettings["WIDGETID"],"",ddlWidget.SelectedValue);
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["HTMLWIDGET"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLHTMLWIDGET"], System.Configuration.ConfigurationManager.AppSettings["WIDGETCONTENT"], "", Utility.KillSqlInjection(fckContent.Value));
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["HTMLWIDGET"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLHTMLWIDGET"], System.Configuration.ConfigurationManager.AppSettings["USERCREATED"], "", HttpContext.Current.Session["userName"].ToString());
            }
            catch (BusinessExeption bex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["htmlwidgetiec"], "Widgets_WidgetHTMLManagement_Controls_Widget", "btnSave_Click", bex.ToString(), Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["htmlwidgetiec"], Request.Url.Query);
            }
            catch (SQLException sex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_WidgetHTMLManagement_Controls_Widget", "btnSave_Click", sex.ToString(), Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
            }
            catch (Exception ex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_WidgetHTMLManagement_Controls_Widget", "btnSave_Click", ex.ToString(), Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
            }

            //alert
            lblAlert.Text = Resources.labels.insertsucessfull;

            fckContent.Value = "";
        }
        else
        {
            try
            {

                //insert
                HTMLWidgetBLL RB = new HTMLWidgetBLL();
                string dM = DateTime.Now.ToString("dd/MM/yyyy");
                RB.Update(Utility.IsInt(ddlWidget.SelectedValue), Utility.KillSqlInjection(fckContent.Value), HttpContext.Current.Session["userName"].ToString(),dM);

                //Write Log
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["HTMLWIDGET"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLHTMLWIDGET"], System.Configuration.ConfigurationManager.AppSettings["WIDGETID"], widgetid, ddlWidget.SelectedValue);
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["HTMLWIDGET"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLHTMLWIDGET"], System.Configuration.ConfigurationManager.AppSettings["WIDGETCONTENT"], widgetcontent, Utility.KillSqlInjection(fckContent.Value));
                
            }
            catch (BusinessExeption bex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["htmlwidgetuec"], "Widgets_WidgetHTMLManagement_Controls_Widget", "btnSave_Click", bex.ToString(), Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["htmlwidgetuec"], Request.Url.Query);
            }
            catch (SQLException sex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_WidgetHTMLManagement_Controls_Widget", "btnSave_Click", sex.ToString(), Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
            }
            catch (Exception ex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_WidgetHTMLManagement_Controls_Widget", "btnSave_Click", ex.ToString(), Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
            }

            if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["wpid"] == null)
            {
                //alert
               Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(System.Configuration.ConfigurationManager.AppSettings["viewhtmlwidget"]));
            }
            else
            {
                //redirect trang truoc
                WidgetPageBLL wpB = new WidgetPageBLL();
                WidgetPageModel wpM = new WidgetPageModel();
                wpM = wpB.GetByID((SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["wpid"].ToString()));

               Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("Default.aspx?p=" + wpM.PageID));
            }
        }
    }
}
