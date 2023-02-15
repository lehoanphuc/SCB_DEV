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

public partial class Widgets_SEMSCard_Detele_Widget : WidgetBase
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
        DataSet delTable = new DataSet();
        string SSUID="";
        try
        {
            if (Session["_dCardID"] != null)
            {
                SSUID = Session["_dCardID"].ToString();
                string[] uids = SSUID.Split('#');
                foreach (string uid in uids)
                {
                    string[] arruid = uid.Split('|');
                    delTable = new SmartPortal.SEMS.Card().DeleteCardByContractNoAndHoldlerCIF(arruid[0],arruid[1], SmartPortal.Constant.IPC.DELETE, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        try
                        {
                                SmartPortal.Common.Log.WriteLogDatabase(arruid[0], "", Request.Url.ToString(), Session["userName"].ToString(),
                                Request.UserHostAddress, "EBA_UserCard", "Delete Credit User", "", arruid[1],SmartPortal.Constant.IPC.PENDING);
                        }
                        catch { }
                    }
                }

                pnRole.Visible = false;
                Session["_dCardID"] = "";
                lblError.Text = "<div class='block1'><div class='handle'>"+Resources.labels.thongtinchitiet+"</div><div class='content' style='text-align:center; font-weight:bold;'>" + Resources.labels.deletecardsuccesful + "<br/><br/></div></div>";
            }
            else
            {
                //DataSet delTable = new DataSet();
                delTable = new SmartPortal.SEMS.Card().DeleteCardByContractNoAndHoldlerCIF(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString().Trim(), SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["hcif"].ToString().Trim(), SmartPortal.Constant.IPC.DELETE, ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    btsaveandcont.Visible = false;
                    pnRole.Visible = false;

                    lblError.Text = "<div class='block1'><div class='handle'>"+Resources.labels.thongtinchitiet+"</div><div class='content' style='text-align:center; font-weight:bold;'>" + Resources.labels.deletecardsuccesful + "<br/><br/></div></div>";
                    try
                    {
                        SmartPortal.Common.Log.WriteLogDatabase(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString().Trim(), "", Request.Url.ToString(), Session["userName"].ToString(),
                        Request.UserHostAddress, "EBA_UserCard", "Delete Credit User", "", SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["hcif"].ToString().Trim(), SmartPortal.Constant.IPC.PENDING);
                    }
                    catch { }
                }
            }
            btsaveandcont.Visible = false;
        }
        catch (Exception ex)
        { }
    }
    protected void btback_Click(object sender, EventArgs e)
    {
        try
        {
            Session["_dCardID"] = null;
           Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx" + SmartPortal.Common.Encrypt.DecryptData(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["returnUrl"].ToString())), true);
        }
        catch
        {

        }
        
    }
}


