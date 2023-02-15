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

using System.Text;
using SmartPortal.Common;
using SmartPortal.Common.Utilities;
using SmartPortal.ExceptionCollection;

public partial class Widgets_IBNTHSearch_Details_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    public static string pn = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_IBNTH_Details_Widget", "Page_Load", IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_IBNTH_Details_Widget", "Page_Load", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        } 
    }
    void BindData()
    {
        try
        {
            string NID = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["nid"].ToString();
            DataSet dsTranApp = new SmartPortal.IB.Account().GetReceiverList(NID, Session["userID"].ToString(), "", "", "", "", "", "", "", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE != "0")
            {
                throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
            }
            DataTable dtTranApp = new DataTable();
            dtTranApp = dsTranApp.Tables[0];
            if (dtTranApp.Rows.Count != 0)
            {
               //switch (dtTranApp.Rows[0]["TRANSFERTYPE"].ToString())
               //{
               //    case SmartPortal.Constant.IPC.TIB:
               //        lbgiaodich.Text = "Chuyển khoản cùng ngân hàng";
               //        break;
               //    case SmartPortal.Constant.IPC.TOB:
               //        lbgiaodich.Text = "Chuyển khoản khác ngân hàng";
               //        break;
               //    case SmartPortal.Constant.IPC.BAC:
               //        lbgiaodich.Text = "Chuyển khoản giữa hai tài khoản cùng khách hàng";
               //        break;
               //}
                lbgiaodich.Text = dtTranApp.Rows[0]["PAGENAME"].ToString();
               lbnguoithuhuong.Text = dtTranApp.Rows[0]["RECEIVERNAME"].ToString();
               lbcmnd.Text = dtTranApp.Rows[0]["LICENSE"].ToString();
               lbacctno.Text = dtTranApp.Rows[0]["ACCTNO"].ToString();
               lbnoicap.Text = dtTranApp.Rows[0]["ISSUEPLACE"].ToString();
               lbdesc.Text = dtTranApp.Rows[0]["DESCRIPTION"].ToString();
               lblBank.Text = dtTranApp.Rows[0]["BANKNAME"].ToString();
               lblConfirmAddress.Text = dtTranApp.Rows[0]["ADDRESS"].ToString();
               if (dtTranApp.Rows[0]["ISSUEDATE"].ToString()!="")
                {
                    if (SmartPortal.Common.Utilities.Utility.IsDateTime2(dtTranApp.Rows[0]["ISSUEDATE"].ToString()) > SmartPortal.Common.Utilities.Utility.IsDateTime2("01/01/1900"))
                   {
                       lbngaycap.Text = dtTranApp.Rows[0]["ISSUEDATE"].ToString();
                   }
                   else
                   {
                       lbngaycap.Text = "";
                   }
                }

            }


           
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_IBNTH_Details_Widget", "BindData", IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_IBNTH_Details_Widget", "BindData", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        } 
    }
    protected void btnBack_OnClick(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }
}
