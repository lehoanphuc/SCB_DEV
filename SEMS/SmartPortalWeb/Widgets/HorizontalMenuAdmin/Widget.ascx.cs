using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.Common.Utilities;
using System.Text;
using SmartPortal.BLL;
using System.Data;
using System.Data.SqlClient;

public partial class Widgets_HorizontalMenuAdmin_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {           
            if (Utility.IsAdmin())
            {
                DataTable iRead;
                DataRow[] iRow;
                MenuBLL MB = new MenuBLL();
                iRead = MB.LoadMenu(System.Globalization.CultureInfo.CurrentCulture.ToString(), Session["userName"].ToString(),"AD");

                StringBuilder sB = new StringBuilder();

                sB.Append("<ul id='nav'>");
                iRow = iRead.Select("MenuParent=0");
                for (int i=0;i<iRow.Length;i++)
                {
                    string link = "Default.aspx" + iRow[i]["Link"].ToString();
                    link = iRow[i]["Link"].ToString().Equals("#") ? "#" : SmartPortal.Common.Encrypt.EncryptURL(link);
                        
                    sB.Append("<li class='liparent'><a href='" + link + "'>" + iRow[i]["MenuTitle"].ToString() + "</a>");
                    sB.Append("<ul>");
                    LoopMenu(iRead, iRow[i]["MenuID"].ToString(), sB);
                    sB.Append("</ul>");

                    sB.Append("</li>");
                    if (i != iRow.Length - 1)
                    {
                        sB.Append("<li class='liimage'><img src='App_Themes/Bank2/images/menu_sepa.gif'/></li>");
                    }
                }
                sB.Append("</ul>");

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
            string link = "Default.aspx" + row["Link"].ToString();
            link = row["Link"].ToString().Equals("#") ? "#" : SmartPortal.Common.Encrypt.EncryptURL(link);

            sB.Append("<li class='linest'><a href='" + link + "'>" + row["MenuTitle"].ToString() + "</a>");
            sB.Append("<ul >");
            LoopMenu(iRead, row["MenuID"].ToString(), sB);
            sB.Append("</ul>");

            sB.Append("</li>");
        }
    }
}
