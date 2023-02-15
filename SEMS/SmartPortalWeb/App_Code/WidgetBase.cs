using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Text;
using SmartPortal.Common.Utilities;
using System.Reflection;
using SmartPortal.BLL;
using SmartPortal.Constant;
using SmartPortal.Common;
using System.Web.UI.WebControls;
using System.Configuration;

/// <summary>
/// Summary description for WidgetBase
/// </summary>
public abstract class WidgetBase : UserControl
{
    public WidgetBase()
    {
    }

    #region Properties
    private string _widgetID;

    public string WidgetID
    {
        get { return _widgetID; }
        set { _widgetID = value; }
    }

    private string _title;

    public string Title
    {
        get { return _title; }
        set { _title = value; }
    }
    private string _iconPath;

    public string IconPath
    {
        get { return _iconPath; }
        set { _iconPath = value; }
    }
    private Boolean _enableTheme;

    public Boolean EnableTheme
    {
        get { return _enableTheme; }
        set { _enableTheme = value; }
    }
    private Boolean _showTitle;

    public Boolean ShowTitle
    {
        get { return _showTitle; }
        set { _showTitle = value; }
    }
    #endregion

    #region GetWidgetInPageForRole
    public virtual void SetID(string id)
    {
        _widgetID = id;
    }
    public virtual void SetTitle(string title)
    {
        _title = title;
    }
    public virtual void SetIconpath(string iconPath)
    {
        _iconPath = iconPath;
    }
    public virtual void SetEnableTheme(Boolean enableTheme)
    {
        _enableTheme = enableTheme;
    }
    public virtual void SetShowTitle(Boolean showTitle)
    {
        _showTitle = showTitle;
    }
    private void ViewMode(StringBuilder sb, bool icon)
    {
        if (icon == false)
        {
            if (_enableTheme)
            {
                if (_showTitle)
                {
                    sb.Append("<div class=\"block1 \" id=\" " + _widgetID + "\">");
                    sb.Append("<div class=\"handle \">" + _title + "</div>");
                }
                else
                {
                    sb.Append("<div class=\"block1 \" id=\" " + _widgetID + "\">");
                    sb.Append("<div class=\"handle \"></div>");
                }
            }
            else
            {
                if (_showTitle)
                {
                    sb.Append("<div class=\"block2 \" id=\" " + _widgetID + "\">");
                    sb.Append("<div class=\"handle \">" + _title + "</div>");
                }
                else
                {
                    sb.Append("<div class=\"block2 \" id=\" " + _widgetID + "\">");
                    sb.Append("<div class=\"handle \"></div>");
                }
            }
        }
        else
        {
            //if (_enableTheme)
            //{
            //    if (_showTitle)
            //    {
            //        sb.Append("<div class=\"block1 \" id=\" " + _widgetID + "\">");
            //        sb.Append("<div class=\"handle \"><img align='left' class='imgWidget' src='" + System.Configuration.ConfigurationManager.AppSettings["widgeticonpathupload"].Replace("~/", "") + _iconPath + "'/>" + _title + "</div>");
            //    }
            //    else
            //    {
            //        sb.Append("<div class=\"block1 \" id=\" " + _widgetID + "\">");
            //        sb.Append("<div class=\"handle \"<img align='left' class='imgWidget' src='" + System.Configuration.ConfigurationManager.AppSettings["widgeticonpathupload"].Replace("~/", "") + _iconPath + "'/>></div>");
            //    }
            //}
            //else
            //{
            //    if (_showTitle)
            //    {
            //        sb.Append("<div class=\"block2 \" id=\" " + _widgetID + "\">");
            //        sb.Append("<div class=\"handle \"<img align='left' class='imgWidget' src='" + System.Configuration.ConfigurationManager.AppSettings["widgeticonpathupload"].Replace("~/", "") + _iconPath + "'/>>" + _title + "</div>");
            //    }
            //    else
            //    {
            //        if (_widgetID != null)
            //        {
            //            //sb.Append("<div class=\"block2 \" id=\" " + _widgetID + "\">");
            //            //sb.Append("<div class=\"handle \"><img align='left' class='imgWidget' src='" + System.Configuration.ConfigurationManager.AppSettings["widgeticonpathupload"].Replace("~/", "") + _iconPath + "'/></div>");
            //        }
            //    }
            //}
        }
    }

    private void ViewMode1(StringBuilder sb)
    {
        //if (_enableTheme)
        //{
        //    if (_showTitle)
        //    {
        //        sb.Append("<div class=\"block1 \" id=\" " + _widgetID + "\">");
        //        sb.Append("<div class=\"handle \"><img align='left' class='imgWidget' src='" + System.Configuration.ConfigurationManager.AppSettings["widgeticonpathupload"].Replace("~/", "") + _iconPath + "'/>" + _title + "</div>");
        //    }
        //    else
        //    {
        //        sb.Append("<div class=\"block1 \" id=\" " + _widgetID + "\">");
        //        sb.Append("<div class=\"handle \"><img align='left' class='imgWidget' src='" + System.Configuration.ConfigurationManager.AppSettings["widgeticonpathupload"].Replace("~/", "") + _iconPath + "'/></div>");

        //    }
        //}
        //else
        //{
        //    if (_showTitle)
        //    {
        //        sb.Append("<div class=\"block2 \" id=\" " + _widgetID + "\">");
        //        sb.Append("<div class=\"handle \"><img align='left' class='imgWidget' src='" + System.Configuration.ConfigurationManager.AppSettings["widgeticonpathupload"].Replace("~/", "") + _iconPath + "'/>" + _title + "</div>");
        //    }
        //    else
        //    {
        //        sb.Append("<div class=\"block2 \" id=\" " + _widgetID + "\">");
        //        sb.Append("<div class=\"handle \"><img align='left' class='imgWidget' src='" + System.Configuration.ConfigurationManager.AppSettings["widgeticonpathupload"].Replace("~/", "") + _iconPath + "'/></div>");

        //    }
        //}
    }
    public void RedirectToActionPage(string action, string paraUrl)
    {
        string pageid = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["p"].ToString();
        string link = PagesBLL.GetLinkAction_Page(pageid, action) + paraUrl;
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(link));

    }
    public void RedirectBackToMainPage()
    {
        string pageid = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["p"].ToString();
        string link = PagesBLL.GetLinkMaster_Page(pageid);
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(link));

    }
    public bool CheckPermitPageAction(string action)
    {
        string pageid = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["p"].ToString();
        return PagesBLL.ChekcPermitPageAction(pageid, Session["userName"].ToString(), action);
    }
    protected void OpenPopup(string popup)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Pop", "openModal('" + popup + "');", true);
    }

    public void ShowPopUpMsg(Page page, Type type, string msg)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("alert('");
        sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"));
        sb.Append("');");
        ScriptManager.RegisterStartupScript(page, type, "showalert", sb.ToString(), true);
    }

    public string GetActionPage()
    {
        string pageid = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["p"].ToString();
        return PagesBLL.GetActionPage(pageid);
    }
    public string[] GetParamsPage(params string[] paramnames)
    {
        if (paramnames.Length == 0) return null;
        string[] results = new string[paramnames.Length];
        SmartPortal.Model.DictionaryWithDefault<string, string> urlparams = Encrypt.GetURLParam(HttpContext.Current.Request.RawUrl);
        for (int i = 0; i < paramnames.Length; i++)
        {
            string param = paramnames[i].Trim();
            if (urlparams.ContainsKey(param)) results[i] = Utility.KillSqlInjection(urlparams[param]);
        }
        return results;
    }

    protected override void Render(HtmlTextWriter writer)
    {
        StringBuilder sb = new StringBuilder();

        if (_iconPath == "")
        {
            if (Utility.WidgetBaseAdmin(Session["userName"].ToString()))
            {
                if (Session["type"] != null)
                {
                    if (Session["type"].ToString() == "view")
                    {
                        ViewMode(sb, false);
                    }
                    else
                    {
                        if (Utility.CheckHTMLWidget(Utility.KillSqlInjection(_widgetID.Split('-')[0])))
                        {
                            sb.Append("<div class=\"block \" id=\" " + _widgetID + "\">");
                            sb.Append("<div class=\"handle\">" + _title + "<div style=\"width:30px;float:right\"><ul id=\"sddm\"><li><a onmouseover=\"mopen('m1" + _widgetID + "')\" onmouseout=\"mclosetime()\">" + Resources.labels.edit + "</a><div style='z-index:100;' id='m1" + _widgetID + "' onmouseover=\"mcancelclosetime()\" onmouseout=\"mclosetime()\"><a href=\"" + System.Configuration.ConfigurationManager.AppSettings["edithtmlwidget"].Replace("~/", "") + "&wid=" + _widgetID.Split('-')[0] + "&wpid=" + _widgetID.Split('-')[1] + "\">" + Resources.labels.editcontent + "</a><a href=\"" + System.Configuration.ConfigurationManager.AppSettings["setroleforwidget"] + "&wpid=" + _widgetID.Split('-')[1] + "\">" + Resources.labels.setrole + "</a><a href=\"#\">Help</a><a onclick=\"RemoveWidget('" + _widgetID.Split('-')[1] + "','" + Resources.labels.areyousuredeletethiswidget + "')\">" + Resources.labels.delete + "</a></div></li></ul></div></div>");
                        }
                        else
                        {
                            sb.Append("<div class=\"block \" id=\" " + _widgetID + "\">");
                            sb.Append("<div class=\"handle\">" + _title + "<div style=\"width:30px;float:right\"><ul id=\"sddm\"><li><a onmouseover=\"mopen('m1" + _widgetID + "')\" onmouseout=\"mclosetime()\">" + Resources.labels.edit + "</a><div style='z-index:100;' id='m1" + _widgetID + "' onmouseover=\"mcancelclosetime()\" onmouseout=\"mclosetime()\"><a href=\"" + System.Configuration.ConfigurationManager.AppSettings["setroleforwidget"] + "&wpid=" + _widgetID.Split('-')[1] + "\">" + Resources.labels.setrole + "</a><a href=\"#\">Help</a><a onclick=\"RemoveWidget('" + _widgetID.Split('-')[1] + "','" + Resources.labels.areyousuredeletethiswidget + "')\">" + Resources.labels.delete + "</a></div></li></ul></div></div>");

                        }
                    }
                }
                else
                {
                    ViewMode(sb, false);
                }

            }
            else
            {
                ViewMode(sb, false);
            }
        }
        else
        {
            if (Utility.WidgetBaseAdmin(Session["userName"].ToString()))
            {
                if (Session["type"] != null)
                {
                    if (Session["type"].ToString() == "view")
                    {
                        ViewMode(sb, true);
                    }
                    else
                    {
                        if (Utility.CheckHTMLWidget(Utility.KillSqlInjection(_widgetID.Split('-')[0])))
                        {
                            sb.Append("<div class=\"block \" id=\" " + _widgetID + "\">");
                            sb.Append("<div class=\"handle\"><img align='center' style='margin-left:5px;margin-right:5px' width='15px' height='15px' src='" + System.Configuration.ConfigurationManager.AppSettings["widgeticonpathupload"].Replace("~/", "") + _iconPath + "'/>" + _title + "<div style=\"width:30px;float:right\"><ul id=\"sddm\"><li><a onmouseover=\"mopen('m1" + _widgetID + "')\" onmouseout=\"mclosetime()\">" + Resources.labels.edit + "</a><div style='z-index:100;' id='m1" + _widgetID + "' onmouseover=\"mcancelclosetime()\" onmouseout=\"mclosetime()\"><a href=\"" + System.Configuration.ConfigurationManager.AppSettings["edithtmlwidget"].Replace("~/", "") + "&wid=" + _widgetID.Split('-')[0] + "&wpid=" + _widgetID.Split('-')[1] + "\">" + Resources.labels.editcontent + "</a><a href=\"" + System.Configuration.ConfigurationManager.AppSettings["setroleforwidget"] + "&wpid=" + _widgetID.Split('-')[1] + "\">" + Resources.labels.setrole + "</a><a href=\"#\">Help</a><a onclick=\"RemoveWidget('" + _widgetID.Split('-')[1] + "','" + Resources.labels.areyousuredeletethiswidget + "')\">" + Resources.labels.delete + "</a></div></li></ul></div></div>");
                        }
                        else
                        {
                            sb.Append("<div class=\"block \" id=\" " + _widgetID + "\">");
                            sb.Append("<div class=\"handle\"><img align='center' style='margin-left:5px;margin-right:5px' width='15px' height='15px' src='" + System.Configuration.ConfigurationManager.AppSettings["widgeticonpathupload"].Replace("~/", "") + _iconPath + "'/>" + _title + "<div style=\"width:30px;float:right\"><ul id=\"sddm\"><li><a onmouseover=\"mopen('m1" + _widgetID + "')\" onmouseout=\"mclosetime()\">" + Resources.labels.edit + "</a><div style='z-index:100;' id='m1" + _widgetID + "' onmouseover=\"mcancelclosetime()\" onmouseout=\"mclosetime()\"><a href=\"" + System.Configuration.ConfigurationManager.AppSettings["setroleforwidget"] + "&wpid=" + _widgetID.Split('-')[1] + "\">" + Resources.labels.setrole + "</a><a href=\"#\">Help</a><a onclick=\"RemoveWidget('" + _widgetID.Split('-')[1] + "','" + Resources.labels.areyousuredeletethiswidget + "')\">" + Resources.labels.delete + "</a></div></li></ul></div></div>");

                        }
                    }
                }
                else
                {
                    ViewMode(sb, true);
                }

            }
            else
            {
                ViewMode1(sb);
            }
        }
        sb.Append("<div class=\"content \" >");
        sb.Append("<div>");
        writer.Write(sb.ToString());

        base.Render(writer);

        writer.Write("</div>");
        writer.Write("</div>");
        writer.Write("</div>");


        //if (Thread.CurrentPrincipal.IsInRole(BlogSettings.Instance.AdministratorRole))
        //{
        //    sb.Append("<a class=\"delete\" href=\"javascript:void(0)\" onclick=\"removeWidget('" + WidgetID + "');return false\" title=\"" + Resources.labels.delete + " widget\">X</a>");
        //    //if (IsEditable)
        //    sb.Append("<a class=\"edit\" href=\"javascript:void(0)\" onclick=\"editWidget('" + Name + "', '" + WidgetID + "');return false\" title=\"" + Resources.labels.edit + " widget\">" + Resources.labels.edit + "</a>");
        //}        


    }

    #endregion
    public void RedirectBackToMainPage(string param)
    {
        string pageid = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["p"].ToString();
        string link = PagesBLL.GetLinkMaster_Page(pageid) + param;
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(link));
    }
    public void RaiseErrorTransactionResponse(ref Label lblError, string message, string methodname, string msgOnScreen = "")
    {
        lblError.Text = string.IsNullOrEmpty(msgOnScreen) ? message : msgOnScreen;
        SmartPortal.Common.Log.RaiseError(ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, methodname, message, Request.Url.Query);
    }

    public void RaiseErrorException(ref Label lblError, string message, string methodname)
    {
        lblError.Text = Resources.labels.exception;
        SmartPortal.Common.Log.RaiseError(ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, methodname, message, Request.Url.Query);
    }
}
