using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using SmartPortal.BLL;
using SmartPortal.Common;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;


public partial class Widgets_SEMSHeader_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            changepass.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=342");
            if (Session["userName"] != null && Session["roleName"] != null && Session["lastLoginTime"] != null)
            {
                if (Session["userName"].ToString() == new PortalSettings().portalSetting.UserNameDefault || Session["userName"].ToString() == "guest")
                {
                    liProfile.Visible = false;
                }
                else
                {
                    liProfile.Visible = true;
                    lblUser.Text = Session["userName"].ToString();
                    try
                    {
                        lblRole.Text = Session["branchName"].ToString();
                    }
                    catch
                    {
                        lblRole.Text = Resources.labels.khongtontai;
                    }
                }
            }
            else
            {
                liProfile.Visible = false;
            }
            if (Utility.IsAdmin())
            {
                DataTable iRead;
                DataRow[] iRow;
                MenuBLL MB = new MenuBLL();
                iRead = MB.LoadMenu(System.Globalization.CultureInfo.CurrentCulture.ToString(), Session["userName"].ToString(), IPC.SEMS);
                StringBuilder sB = new StringBuilder();
                iRow = iRead.Select("MenuParent='0'");
                for (int i = 0; i < iRow.Length; i++)
                {
                    string link = iRow[i]["Link"].ToString().Equals("#") ? "#" : SmartPortal.Common.Encrypt.EncryptURL(iRow[i]["Link"].ToString());

                    sB.Append("<li><a href='" + link + "'><i class='" +
                              iRow[i]["MenuIcon"].ToString() + "'></i><span>" + iRow[i]["MenuTitle"].ToString() +
                              "</span>");

                    if (iRead.Select("MenuParent='" + iRow[i]["MenuID"].ToString() + "'").Length != 0)
                    {
                        sB.Append("<b class=\"collapse-sign\"><em class=\"fa fa-angle-down\" style=\"color: #039dfc\"></em></b></a>");
                        sB.Append("<ul>");
                        LoopMenu(iRead, iRow[i]["MenuID"].ToString(), sB);
                        sB.Append("</ul>");
                        sB.Append("</li>");
                    }
                    else
                    {
                        sB.Append("</a>");
                        sB.Append("</li>");
                    }
                }
                ltrContent.Text = sB.ToString();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void LoopMenu(DataTable iRead, string parentID, StringBuilder sB)
    {
        DataRow[] iRow;
        iRow = iRead.Select("MenuParent='" + parentID + "'");
        foreach (DataRow row in iRow)
        {
            string link = row["Link"].ToString().Equals("#") ? "#" : SmartPortal.Common.Encrypt.EncryptURL(row["Link"].ToString());
            sB.Append("<li><a href='" + link + "'><span>" + row["MenuTitle"].ToString() + "</span>");

            if (iRead.Select("MenuParent='" + row["MenuID"].ToString() + "'").Length != 0)
            {
                sB.Append("<b class=\"collapse-sign\"><em class=\"fa fa-angle-down\" style=\"color: #039dfc\"></em></b></a>");
                sB.Append("<ul>");
                LoopMenu(iRead, row["MenuID"].ToString(), sB);
                sB.Append("</ul>");
                sB.Append("</li>");
            }
            else
            {
                sB.Append("</a>");
                sB.Append("</li>");
            }
        }
    }
    protected void lbLangue1_Click(object sender, EventArgs e)
    {
        ReplaceLanguage("lo-LA");
    }
    protected void lbLangue2_Click(object sender, EventArgs e)
    {
        ReplaceLanguage("en-US");
    }
    protected void lbLangue3_Click(object sender, EventArgs e)
    {
        ReplaceLanguage("zh-CN");
    }
    public void ReplaceLanguage(string langid)
    {
        string trimUrl = System.Web.HttpContext.Current.Request.RawUrl.ToString().Replace(ConfigurationManager.AppSettings["routeurlslash"].ToString(), "");
        trimUrl = trimUrl.Trim().StartsWith("/") ? trimUrl.Trim().Substring(1) : trimUrl.Trim();
        string urlencry = SmartPortal.Common.Encrypt.DecryptURL(trimUrl);
        string url = SmartPortal.Common.Encrypt.EncryptURL(urlencry.Replace("&l=lo-LA", "").Replace("&l=zh-CN", "").Replace("&l=en-US", "") + "&l=" + langid + "");
        Response.Redirect(url);
    }
    protected void lbLogout_Click(object sender, EventArgs e)
    {
        try
        {
            if (!Convert.ToBoolean(ConfigurationManager.AppSettings["IBIsMultilogin"].ToString()))
            {
                UsersBLL UB = new UsersBLL();
                UB.UpdateIsLogin(Session["userName"].ToString(), DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), Session["UUID"].ToString());
                Session["userName"] = new PortalSettings().portalSetting.UserNameDefault;
                Session["serviceID"] = new PortalSettings().portalSetting.ServiceIDDefault;
                Session["type"] = null;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_LoginInfo_Widget", "lbLogout_Click", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
        //Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("?p=125"));
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("default.aspx?p=125"));
    }
}
