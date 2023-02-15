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
using SmartPortal.SEMS;

public partial class Widgets_SEMSProvince_Delete_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btsaveandcont_Click(object sender, EventArgs e)
    {
        try
        {
            City objCity = new City();
            DataSet ds = new DataSet();
            if (Session["_CITYCODE"] != null)
            {
                string SSProductID = Session["_CITYCODE"].ToString();
                string[] pros = SSProductID.Split('#');
                foreach (string pro in pros)
                {
                    ds = objCity.DelCity(pro, ref IPCERRORCODE, ref IPCERRORDESC);

                    if (IPCERRORCODE == "0")
                    {
                        lblConfirm.Text = Resources.labels.ketquathuchien;
                        lblError.Text = Resources.labels.xoathongtintinhthanhthanhcong;
                        btsaveandcont.Visible = false;
                    }
                    else
                    {
                        if (IPCERRORDESC == "110211")
                        {
                            throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.EXISTDISTRICT);
                        }
                        else
                        {
                            throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.IPC);
                        }
                    }
                }
                Session["_CITYCODE"] = null;
            }
            else
            {
                ds = objCity.DelCity(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["id"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);

                if (IPCERRORCODE == "0")
                {
                    lblConfirm.Text = Resources.labels.ketquathuchien;
                    lblError.Text = Resources.labels.xoathongtintinhthanhthanhcong;
                    btsaveandcont.Visible = false;
                }
                else
                {
                    if (IPCERRORDESC == "110211")
                    {
                        throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.EXISTDISTRICT);
                    }
                    else
                    {
                        throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.IPC);
                    }
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

    }

    protected void btback_Click(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=144"));
    }
}