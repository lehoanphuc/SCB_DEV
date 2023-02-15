using System;
using System.Collections;
using System.Data;
using SmartPortal.ExceptionCollection;

public partial class Widgets_IBRequestExportStatement_viewprint : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();
            if (Session["langID"] != null)
            {
                tmpl = SmartPortal.Common.ST.GetStringTemplate("IBRequestExportStatement", "Term" + Session["langID"].ToString());
            }
            else
            {
                tmpl = SmartPortal.Common.ST.GetStringTemplate("IBRequestExportStatement", "Termen-US");
            }
            Hashtable hasPrint = new Hashtable();
            hasPrint = (Hashtable)Session["printSTM"];
            tmpl.SetAttribute("fee", hasPrint["fee"].ToString());
            ltrPrint.Text = tmpl.ToString();
        }
        catch
        {

        }
    }
}
