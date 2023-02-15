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

using SmartPortal.Common.Utilities;

public partial class Widgets_SEMSDeposit_Delete_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = "";
        }

        catch (Exception ex)
        {
        }
    }

    protected void btsaveandcont_Click(object sender, EventArgs e)
    {
        DataSet CustomerTable = new DataSet();
        string SSCustomer = "";
        Hashtable tblResult;
        try
        {
            if (Session["checkno"] != null)
            {
                SSCustomer = Session["checkno"].ToString();
                string[] custs = SSCustomer.Split('#');
                foreach (string cust in custs)
                {
                    tblResult = new SmartPortal.SEMS.Transactions().Deposit(cust.Trim(), SmartPortal.Constant.IPC.APPROVESTATUS.DEPOSIT,Session["userID"].ToString(),ref IPCERRORCODE,ref IPCERRORDESC);                    
                }
                //Response.Redirect(SmartPortal.Common.Encrypt.DecryptData(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["returnUrl"].ToString()));

            }
            else
            {
                tblResult = new SmartPortal.SEMS.Transactions().Deposit(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString().Trim(), SmartPortal.Constant.IPC.APPROVESTATUS.DEPOSIT,Session["userID"].ToString(),ref IPCERRORCODE,ref IPCERRORDESC);

            }
       
        if (IPCERRORCODE== "0")
        {
            lblError.Text = Resources.labels.hoantratienthanhcong;
            btsaveandcont.Visible = false;
            pnRole.Visible = false;
        }
        else
        {
            lblError.Text = Resources.labels.hoantratienkhongthanhcong;
            btsaveandcont.Visible = false;
            pnRole.Visible = false;
        }
        }
        catch (Exception ex)
        { }
    }
    protected void btback_Click(object sender, EventArgs e)
    {
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=333"));
    }
}


