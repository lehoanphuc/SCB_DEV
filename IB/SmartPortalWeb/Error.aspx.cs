using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Error : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        //    if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["e"] != null)
        //    {
                
        //            switch(SmartPortal.Common.Utilities.Utility.KillSqlInjection(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["e"].ToString()))
        //            {
        //                case "qs":
        //                    lblError.Text="Query String Not Invalid";
        //                    break;
        //                case "ii":
        //                    lblError.Text = "Error Convert To Int Type";
        //                    break;
        //                case "ce":
        //                    lblError.Text = "Connection Error";
        //                    break;
        //                case "ae":
        //                    lblError.Text = "System Error";
        //                    break;
        //            }
               
        //    }
        //}
    }
}
