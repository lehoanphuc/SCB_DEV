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

public partial class Widgets_HorizontalMenu_Widget :WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            DataTable iRead;
            DataRow []iRow;
            CategoryBLL CB = new CategoryBLL();
            iRead = CB.LoadCategory(System.Globalization.CultureInfo.CurrentCulture.ToString());

            StringBuilder sB = new StringBuilder();
                        
            sB.Append("<ul id='nav'>");
            iRow = iRead.Select("parentID=0");
            for(int i=0;i<iRow.Length;i++)
            {
                if (iRow[i]["Link"].ToString() == "")
                {
                    sB.Append("<li class='liparent'><a href='" + System.Configuration.ConfigurationManager.AppSettings["newsofcategory"] + "&catid=" + iRow[i]["CatID"].ToString() + "'>" + iRow[i]["CatName"].ToString() + "</a>");
                }
                else
                {
                    sB.Append("<li class='liparent'><a href='" + iRow[i]["Link"].ToString() + "&catid=" + iRow[i]["CatID"].ToString() + "'>" + iRow[i]["CatName"].ToString() + "</a>");
                }
                sB.Append("<ul>");
                LoopMenu(iRead, int.Parse(iRow[i]["CatID"].ToString()), sB);
                sB.Append("</ul>");

                sB.Append("</li>");
                if (i != iRow.Length - 1)
                {
                    sB.Append("<li class='liimage'><img src='App_Themes/Bank1/images/menu_sepa.gif'/></li>");
                }
            }
            sB.Append("</ul>");

            ltrContent.Text = sB.ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void LoopMenu(DataTable iRead, int parentID, StringBuilder sB)
    {
        DataRow[] iRow;
        iRow = iRead.Select("parentID='"+parentID+"'");
        foreach (DataRow row in iRow)
        {
            if (row["Link"].ToString() == "")
            {
                sB.Append("<li class='linest' style='background-color:#64B5F6'><a href='" + System.Configuration.ConfigurationManager.AppSettings["newsofcategory"] + "&catid=" + row["CatID"].ToString() + "'>" + row["CatName"].ToString() + "</a>");
            }
            else
            {
                sB.Append("<li class='linest'><a href='" + row["Link"].ToString() + "'>" + row["CatName"].ToString() + "</a>");
            }
            sB.Append("<ul >");
            LoopMenu(iRead, int.Parse(row["CatID"].ToString()), sB);
            sB.Append("</ul>");

            sB.Append("</li>");
        }
    }
}
