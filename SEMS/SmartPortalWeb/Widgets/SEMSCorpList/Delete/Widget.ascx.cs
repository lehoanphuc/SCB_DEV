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

public partial class Widgets_SEMSCustomerListCorp_Delete_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
        }

        catch (Exception ex)
        {
        }
    }

    protected void btsaveandcont_Click(object sender, EventArgs e)
    {
        DataSet CustomerTable = new DataSet();
        string SSCustomer = "";
        try
        {
            if (Session["_CustomerID"] != null)
            {
                SSCustomer = Session["_CustomerID"].ToString();
                string[] custs = SSCustomer.Split('#');
                foreach (string cust in custs)
                {
                    CustomerTable = new SmartPortal.SEMS.Customer().DeleteUserByUID(cust,SmartPortal.Constant.IPC.DELETE, ref IPCERRORCODE, ref IPCERRORDESC);
                    
                }
                //Response.Redirect(SmartPortal.Common.Encrypt.DecryptData(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["returnUrl"].ToString()));

            }
            else
            {
                CustomerTable = new SmartPortal.SEMS.Customer().DeleteUserByUID(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cid"].ToString(), SmartPortal.Constant.IPC.DELETE, ref IPCERRORCODE, ref IPCERRORDESC);

            }
        }
        catch (Exception ex)
        { }
        if (IPCERRORCODE == "0")
        {

           Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=138"));
        }
    }
}


