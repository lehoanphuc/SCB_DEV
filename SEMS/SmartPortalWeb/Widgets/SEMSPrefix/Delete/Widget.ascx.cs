using SmartPortal.ExceptionCollection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Widgets_SEMSPrefix_Delete_Widget : WidgetBase
{

    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    string sPageList = "1111";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }

    private void BindData()
    {

    }

    protected void btsaveandcont_Click(object sender, EventArgs e)
    {
        DataSet Telcotable = new DataSet();
        string SSProductID = "";

        try
        {
            if (Session["_Prefix"] != null)
            {
                SSProductID = Session["_Prefix"].ToString();
                string[] pros = SSProductID.Split('#');

                foreach (string pro in pros)
                {
                    Telcotable = new SmartPortal.SEMS.User().DeletePrefix(pro, ref IPCERRORCODE, ref IPCERRORDESC);



                    if (IPCERRORCODE == "0")
                    {
                        lblError.Text = "Delete all your selected prefix !!";
                    }
                    else
                    {
                        if (IPCERRORDESC == "110211")
                        {
                            throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.USINGPRODUCT);
                        }
                        else
                        {
                            throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.IPC);
                        }
                    }
                }

                Session["_Prefix"] = null;
                btsaveandcont.Visible = false;
            }
            else
            {
                Telcotable = new SmartPortal.SEMS.User().DeletePrefix(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cid"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);



                if (IPCERRORCODE == "0")
                {
                    lblError.Text = "Delete all your selected prefix !!";
                }
                else
                {
                    if (IPCERRORDESC == "110211")
                    {
                        throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.USINGPRODUCT);
                    }
                    else
                    {
                        throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.IPC);
                    }
                }

                btsaveandcont.Visible = false;
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
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?po=4&p=" + sPageList));
    }
}