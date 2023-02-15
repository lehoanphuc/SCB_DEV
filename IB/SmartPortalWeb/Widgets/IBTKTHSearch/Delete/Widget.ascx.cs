using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.IB;
using System.Data;
using SmartPortal.ExceptionCollection;

public partial class Widgets_IBTKTHSearch_Delete_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        lblError.Text = "";
        pnresult.Visible = false;
    }
    protected void btsaveandcont_Click(object sender, EventArgs e)
    {
        try
        {   
            Account objAcct=new Account();
            if (Session["_AcceptList"] != null)
            {
                string SSProTranCCYID = Session["_AcceptList"].ToString();
                string[] Alls = SSProTranCCYID.Split('#');
                foreach (string All in Alls)
                {
                    string[] pros = All.Split('|');
                    objAcct.DelAcceptList(pros[0], pros[1], ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {

                        //Response.Redirect("~/Default.aspx?po=4&p=141");
                    }
                }
                Session["_AcceptList"] = null;

            }
            else
            {
                objAcct.DelAcceptList(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString(), SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["acctno"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
            }
                
            if (IPCERRORCODE == "0")
            {
                lblError.Text = "Xóa tài khoản được phép chuyển đến thành công";
                pnresult.Visible = true;
                btsaveandcont.Visible = false;
                lblConfirm.Visible = false;
                pnRole.Visible = false;
                //Response.Redirect("~/default.aspx?po=4&p=140");
                //Response.Redirect(SmartPortal.Common.Encrypt.DecryptData(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["returnUrl"].ToString()));
            }
                else
                {
                    throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.Quyen);
                }
            
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.Message, this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    
    protected void btback_Click(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?po=3&p=120"));
    }
}
