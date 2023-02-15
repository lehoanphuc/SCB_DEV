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


public partial class Widgets_IBCKVTKTKKKH_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            pnConfirm.Visible = false;
            pnOTP.Visible = false;
            pnTransfer.Visible = true;
            pnResultTransaction.Visible = false;
        }
        catch
        {
        }
    }
    protected void BtConfirm_Click(object sender, EventArgs e)
    {
        try
        {
            pnConfirm.Visible = true;
            pnOTP.Visible = false;
            pnTransfer.Visible = false;
            pnResultTransaction.Visible = false;
        }
        catch
        {
        }

    }
    protected void BtTransfer_Click(object sender, EventArgs e)
    {
        try
        {
            pnConfirm.Visible = false;
            pnOTP.Visible = true;
            pnTransfer.Visible = false;
            pnResultTransaction.Visible = false;
            if (radOTP.Checked)
            {
                txtOTP.Enabled = true;
                txtOTPBSMS.Enabled = false;
            }
            if (radOTPBSMS.Checked)
            {
                txtOTP.Enabled = false;
                txtOTPBSMS.Enabled = true;
            }
        }
        catch
        {
        }

    }
    protected void BtCancel_Click(object sender, EventArgs e)
    {
        try
        {
            pnConfirm.Visible = false;
            pnOTP.Visible = false;
            pnTransfer.Visible = true;
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
            pnTransfer.Visible = false;
            pnResultTransaction.Visible = false;
        }
        catch
        {
        }
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        try
        {
            pnConfirm.Visible = false;
            pnOTP.Visible = false;
            pnTransfer.Visible = false;
            pnResultTransaction.Visible = true;
        }
        catch
        {
        }

    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        try
        {
            pnConfirm.Visible = false;
            pnOTP.Visible = false;
            pnTransfer.Visible = true;
            pnResultTransaction.Visible = false;
        }
        catch
        {
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

    }
}
