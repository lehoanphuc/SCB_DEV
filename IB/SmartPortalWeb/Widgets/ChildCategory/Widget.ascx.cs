using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using SmartPortal.BLL;
using SmartPortal.Common.Utilities;

public partial class Widgets_ChildCategory_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["catid"] != null)
            {
                DataTable tblChildCat = new DataTable();
                tblChildCat = new CategoryBLL().LoadChildCategory(Utility.IsInt(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["catid"].ToString()),System.Globalization.CultureInfo.CurrentCulture.ToString());
                StringBuilder sb = new StringBuilder();
                sb.Append("<div id='imenu'>");
                foreach (DataRow row in tblChildCat.Rows)
                {
                    DataTable tblChildCat1 = new DataTable();
                    tblChildCat1 = new CategoryBLL().LoadChildCategory1(Utility.IsInt(row["CatID"].ToString()));

                    if (tblChildCat1.Rows.Count != 0)
                    {
                        sb.Append("<h3><img src='widgets/childcategory/images/availuname.gif'/> <a onmouseover=\"javascript: jQuery14('#mylist" + row["CatID"].ToString() + "').slideToggle('slow');\" href='" + System.Configuration.ConfigurationManager.AppSettings["newsofcategory"] + "&catid=" + row["CatID"].ToString() + "'>" + row["CatName"].ToString() + "</a></h3> ");
                        sb.Append("<ul id='mylist" + row["CatID"].ToString() + "'>");
                        //load child cap 2

                        foreach (DataRow row1 in tblChildCat1.Rows)
                        {
                            sb.Append("<li><a href='" + System.Configuration.ConfigurationManager.AppSettings["newsofcategory"]+"&catid="+row1["CatID"].ToString() + "'>" + row1["CatName"].ToString() + "</a></li>");
                        }
                        sb.Append("</ul>");
                        
                    }
                    else
                    {
                        sb.Append("<h3><img src='widgets/childcategory/images/availuname.gif'/>"+" " +"<a href='" + System.Configuration.ConfigurationManager.AppSettings["newsofcategory"] + "&catid=" + row["CatID"].ToString() + "'>"+ row["CatName"].ToString() + "</a></h3>");
                        
                    }
                }
                sb.Append("</div>");
                ltrChildCategory.Text = sb.ToString();
            }
        }
    }
}
