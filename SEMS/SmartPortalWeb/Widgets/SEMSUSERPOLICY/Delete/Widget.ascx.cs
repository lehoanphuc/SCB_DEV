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
using SmartPortal.ExceptionCollection;

public partial class Widgets_SEMSUSERPOLICYDelete_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    static string pageBackID = "1060";
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btsaveandcont_Click(object sender, EventArgs e)
    {
        DataSet dsPolicy = new DataSet();
        try
        {
            if (Session["_policy"] != null)
            {
            }
            else
            {
                int policyid = Convert.ToInt32(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["pid"].ToString());
                string serviceid = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["sid"].ToString();
                //check policy already asigned for users:
                DataTable dtpolicycheck = new SmartPortal.SEMS.USERPOLICY().Checkpolicyused(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["pid"].ToString(), serviceid);
                if (dtpolicycheck.Rows.Count != 0)
                {
                    lblError.Text = Resources.labels.Policydaduocganchouserhayxoausertrongpolicytruoc;
                    btsaveandcont.Visible = false;
                    goto EXIT;
                }
                dsPolicy = new SmartPortal.SEMS.USERPOLICY().PolicyDelete(policyid, serviceid, ref IPCERRORCODE, ref IPCERRORDESC);
                SmartPortal.Common.Log.WriteLogFile("DELETE POLICY ", "", "", "serviceid:" + serviceid + "-" + policyid.ToString() + "-numbers of user belong-" + dtpolicycheck.Rows.Count);
                if (IPCERRORCODE == "0")
                {
                    //Response.Redirect("~/Default.aspx?p=141");
                }
                else
                {
                    throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.IPC);
                }
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(IPCex.Message, Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }

        if (IPCERRORCODE == "0")
        {
            lblConfirm.Text = Resources.labels.ketquathuchien;
            lblError.Text = Resources.labels.Xoapolicythanhcong;
            btsaveandcont.Visible = false;
        }
    EXIT:
        {
        }
    }
    protected void btback_Click(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=" + pageBackID));
    }
}


