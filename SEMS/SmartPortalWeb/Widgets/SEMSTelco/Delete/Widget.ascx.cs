using SmartPortal.ExceptionCollection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Widgets_SEMSTelco_Delete_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    string PageBack = "1108";
    string PageDetail = "1109";
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void btsaveandcont_Click(object sender, EventArgs e)
    {
        DataSet Telcotable = new DataSet();
        DataSet Cardtable = new DataSet();
        string SSProductID = "";
        try
        {
            if (Session["_TelcoID"] != null)
            {
                SSProductID = Session["_TelcoID"].ToString();
                string[] pros = SSProductID.Split('#');
                foreach (string pro in pros)
                {
                    Telcotable = new SmartPortal.SEMS.User().Deletetelco(pro, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {

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
                Session["_TelcoID"] = "Telco";

            }

            if (Session["_CardID"] != null)
            {
                
                SSProductID = Session["_CardID"].ToString();
                string[] pros = SSProductID.Split('#');
                foreach (string pro in pros)
                {
                    Cardtable = new SmartPortal.SEMS.User().Deletecardtop(pro, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {

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
                Session["_CardID"] = "Card";

            }
            if (IPCERRORCODE == "0")
            {
                pnRole.Visible = true;
                btsaveandcont.Visible = false;
                btback.Visible = true;
                lblError.Text = "Deleted record(s)";
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

    protected void Button8_Click(object sender, EventArgs e)
    {
        if (Session["_TelcoID"] != null)
        {
            Session["_TelcoID"] = null;
            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?po=4&p=" + PageBack));
        }
        if (Session["_CardID"] != null)
        {
            Session["_CardID"] = null;
            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?po=4&p=" + PageDetail + "&a=edit&cid=" + SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cid"].ToString()));
        }
    }
}