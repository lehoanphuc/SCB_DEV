using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.ExceptionCollection;
using SmartPortal.SEMS;

public partial class Widgets_SEMSFDProduct_Delete_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        pnresult.Visible = false;

    }
    protected void btback_Click(object sender, EventArgs e)
    {
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=378"));
    }
    protected void btsaveandcont_Click(object sender, EventArgs e)
    {
        try
        {
            FDProductOnline objFDProduct = new FDProductOnline();
            if (Session["_FDPRODUCTID"] != null)
            {
                string SSProductID = Session["_FDPRODUCTID"].ToString();
                string[] pros = SSProductID.Split('#');
                foreach (string pro in pros)
                {
                    objFDProduct.DelFDProductOnline(pro, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {

                        //Response.Redirect("~/Default.aspx?p=141");
                    }
                    else
                    {
                        if (IPCERRORDESC == "110211")
                        {
                            throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.ACTIVEDISTRICT);
                        }
                        else
                        {
                            throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.IPC);
                        }
                    }
                }
                Session["_FDPRODUCTID"] = null;
            }
            else
            {
                objFDProduct.DelFDProductOnline(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["id"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {

                    //Response.Redirect("~/Default.aspx?p=141");
                }
                else
                {
                    if (IPCERRORDESC == "110211")
                    {
                        throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.ACTIVEDISTRICT);
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

        if (IPCERRORCODE == "0")
        {
            lblError.Text =Resources.labels.xoathongtinfdproductthanhcong;
            pnRole.Visible = false;
            pnresult.Visible = true;
            btsaveandcont.Visible = false;
        }
    }
}
