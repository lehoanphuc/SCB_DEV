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


public partial class Widgets_IBTKKKHNewAccount_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            pnConfirm.Visible = false;
            pnOTP.Visible = false;
            pnResultTransaction.Visible = false;
            pnTIB.Visible = true;
        }
        catch
        {
        }
    }


    protected void btnTIBNext_Click(object sender, EventArgs e)
    {
        try
        {
            pnConfirm.Visible = true;
            pnOTP.Visible = false;
            pnResultTransaction.Visible = false;
            pnTIB.Visible = false;
        }
        catch
        {
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            pnOTP.Visible = true;
            pnConfirm.Visible = false;            
            pnResultTransaction.Visible = false;
            pnTIB.Visible = false;
        }
        catch
        {
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        try
        {
            pnTIB.Visible = true;
            pnConfirm.Visible = false;
            pnOTP.Visible = false;
            pnResultTransaction.Visible = false;           
        }
        catch
        {
        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        try
        {
            pnConfirm.Visible = true;          
            pnOTP.Visible = false;
            pnResultTransaction.Visible = false;
            pnTIB.Visible = false;
        }
        catch
        {
        }
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        try
        {
            pnResultTransaction.Visible = true;
            pnConfirm.Visible = false;
            pnOTP.Visible = false;          
            pnTIB.Visible = false;
        }
        catch
        {
        }
    }
}
