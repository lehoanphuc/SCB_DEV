using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_ViewMode_Widget : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
            if (Session["type"] != null)
            {
                if (Session["type"].ToString() == "view")
                {
                    radView.Checked = true;
                    radEdit.Checked = false;
                }
                else
                {
                    radEdit.Checked = true;
                    radView.Checked = false;
                }
            }
            else
            {
                radView.Checked = true;
            }
        }
    }
    protected void radView_CheckedChanged(object sender, EventArgs e)
    {
        Session["type"] = "view";
        Response.Redirect(Request.Url.ToString());
    }
    protected void radEdit_CheckedChanged(object sender, EventArgs e)
    {
        Session["type"] = "edit";
        Response.Redirect(Request.Url.ToString());
    }
}
