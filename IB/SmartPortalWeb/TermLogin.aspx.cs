using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SmartPortal.Security;
using SmartPortal.Common.Utilities;
using System.IO;
using SmartPortal.Common;
using System.Configuration;
using SmartPortal.ExceptionCollection;
using System.Threading;

using SmartPortal.BLL;
using SmartPortal.Model;
using System.Text;
using System.Web.SessionState;
using System.Reflection;


public partial class ibLoginTerm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();
            if (Session["langID"] != null)
            {
                tmpl = SmartPortal.Common.ST.GetStringTemplate("TermLogin", "TermLogin" + Session["langID"].ToString());
            }
            else
            {
                tmpl = SmartPortal.Common.ST.GetStringTemplate("TermLogin", "TermLoginen-US");
            }
            ltrPrint.Text = tmpl.ToString();

        }

    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        Session["ShowTerm"] = "0";

        ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect","alert('You will be redirected to Change Password.'); window.location='" +
Request.ApplicationPath + SmartPortal.Common.Encrypt.EncryptURL("Default.aspx?p=206") + "';", true);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(ConfigurationManager.AppSettings["loginpage"].ToString());
    }
}