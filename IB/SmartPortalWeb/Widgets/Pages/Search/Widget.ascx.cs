using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.Common.Utilities;
using SmartPortal.BLL;
using System.Data;
using System.Data.SqlClient;

public partial class Widgets_Pages_Search_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        PagesBLL PB = new PagesBLL();
        DataTable tblPage = new DataTable();

        tblPage = PB.Search(Utility.KillSqlInjection(txtSearch.Text.Trim()));

        Session["searchPage"] = tblPage;

        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(System.Configuration.ConfigurationManager.AppSettings["viewpage"]));
    }
}
