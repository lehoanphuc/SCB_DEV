using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using System.Text;
using SmartPortal.BLL;
using SmartPortal.Common.Utilities;

public partial class Widgets_IBVerticalMenu_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if(Session["accType"].ToString() != "IND")
            {
                StringBuilder sB1 = new StringBuilder();
                sB1.Append("<link href='widgets/IBVerticalMenu/CSS/cssCo.css' rel='stylesheet' type='text/css' />");
                Literal1.Text = sB1.ToString();
            }
            else
            {
                StringBuilder sB1 = new StringBuilder();
                sB1.Append("<link href='widgets/IBVerticalMenu/CSS/css.css' rel='stylesheet' type='text/css' />");
                Literal1.Text = sB1.ToString();
            }
            if (Utility.IsAdmin())
            {
                

                DataTable iRead;
                DataRow[] iRow;
                MenuBLL MB = new MenuBLL();
                iRead = MB.LoadMenu(System.Globalization.CultureInfo.CurrentCulture.ToString(), Session["UserID"].ToString(), SmartPortal.Constant.IPC.IB);

                StringBuilder sB = new StringBuilder();

                sB.Append("<div class='left_content'>");

                iRow = iRead.Select("MenuParent=0");
                for (int i = 0; i < iRow.Length; i++)
                {
                    sB.Append("<div class='left_content_menu'>");
                    sB.Append("<div class='middle_left_content_menu_header'>");
                    sB.Append("<div class='left_menu_icon' style='background-image:url(\"../Images/MenuIcon/" + iRow[i]["MenuIcon"].ToString() + "\")'></div>");
                    sB.Append(iRow[i]["MenuTitle"].ToString());
                    sB.Append("</div>");
                    DataRow[] row = iRead.Select("MenuParent='" + iRow[i]["MenuID"].ToString() + "'");
                    for (int j = 0; j < row.Length; j++)
                    {
                        sB.Append("<div class='arrowlistmenu'>");
                        if (iRead.Select("MenuParent='" + row[j]["MenuID"].ToString() + "'").Length == 0)
                        {
                            string link = row[j]["Link"].ToString().Equals("#") ? "#" : SmartPortal.Common.Encrypt.EncryptURL(row[j]["Link"].ToString());
                            sB.Append("<div class='menuheader'><a href='" + link + "'>" + row[j]["MenuTitle"].ToString() + "</a></div>");
                        }
                        else
                        {
                            sB.Append("<div class='menuheader expandable'>" + row[j]["MenuTitle"].ToString() + "</div>");
                            sB.Append("<ul class='categoryitems'>");
                            LoopMenu(iRead, row[j]["MenuID"].ToString(), sB);
                            sB.Append("</ul>");
                        }
                        sB.Append("</div>");
                    }

                    sB.Append("</div>");
                }
                sB.Append("</div>");

                #region mobile menu
                sB.Append("<nav id = 'mobile_menu'>");
                mobileMenu(iRead, sB, "0");

                sB.Append("</nav>");
                #endregion

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
            sB.Append("<li><a href='" + SmartPortal.Common.Encrypt.EncryptURL(row["Link"].ToString()) + "'>" + row["MenuTitle"].ToString() + "</a>");
            sB.Append("</li>");
        }
    }
    private void mobileMenu(DataTable iRead, StringBuilder sB, string count)
    {
        DataRow[] row = iRead.Select("MenuParent='" + count + "'");
        if (row.Length > 0)
        {
            sB.Append("<ul>");
            for (int j = 0; j < row.Length; j++)
            {
                sB.Append("<li>");
                if (row[j]["Link"].Equals("#") && row[j]["MenuParent"].ToString().Trim().Equals("0"))
                {
                    if (Session["accType"].ToString().Equals("IND"))
                    {
                        sB.Append(" <span style='background-color:#007F3E;font-weight:bold;color:#FFF;'>" + row[j]["MenuTitle"].ToString() + "</span>");
                    }
                    else
                    {
                        sB.Append(" <span style='background-color:#7A58BF;font-weight:bold;color:#FFF;'>" + row[j]["MenuTitle"].ToString() + "</span>");
                    }
                }
                else if (row[j]["Link"].Equals("#"))
                    sB.Append(" <span>" + row[j]["MenuTitle"].ToString() + "</span>");
                else
                {
                    sB.Append(" <a href='" + SmartPortal.Common.Encrypt.EncryptURL(row[j]["Link"].ToString()) + "' >" + row[j]["MenuTitle"].ToString() + "</a>");
                }
                mobileMenu(iRead, sB, row[j]["MenuID"].ToString());
                sB.Append("</li>");
            }
            sB.Append("</ul>");
        }
    }
}
