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

using SmartPortal.IB;
using SmartPortal.ExceptionCollection;

public partial class Widgets_IBTKTH_Details_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BindData();
            }
           
        }
        catch
        {
        }
    }

  void BindData()
    {
        try
        {
            string errorCode = string.Empty;
            string errorDesc = string.Empty;
            SmartPortal.SEMS.User objUser = new SmartPortal.SEMS.User();
            Account objAcct = new Account();
            DataSet dsUser = new DataSet();
            DataSet dsAcceptlist = new DataSet();
            dsUser = objUser.GetFullUserByUID(Session["userID"].ToString(), ref errorCode, ref errorDesc);
            string ContractNo = string.Empty;
            if (errorCode == "0" && dsUser.Tables.Count == 1)
            {
                if (dsUser.Tables[0].Rows.Count == 1)
                {
                    string CNO = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cno"].ToString();
                    ContractNo = dsUser.Tables[0].Rows[0]["CONTRACTNO"].ToString();
                    dsAcceptlist = objAcct.DetailsAcceptList(ContractNo, CNO, ref errorCode, ref errorDesc);
                    if (errorCode == "0" && dsAcceptlist.Tables.Count != 0)
                    {
                        txtNTHName.Text = dsAcceptlist.Tables[0].Rows[0]["Sendername"].ToString();
                        txtNTHAccount.Text = dsAcceptlist.Tables[0].Rows[0]["Acctno"].ToString();
                        txtDesc.Text = dsAcceptlist.Tables[0].Rows[0]["Description"].ToString();
                        txtNTHName.Enabled = false;
                        txtNTHAccount.Enabled = false;
                        txtDesc.Enabled = false;
                    }
                    else
                    {
                        throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.InvalidDestAccount);
                    }
                }
            }
            else
            {
                throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.IPC);
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

    protected void Button4_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/?po=3&p=120");
    }
}
