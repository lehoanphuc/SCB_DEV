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

public partial class Widgets_SEMSREGIONFEEDelete_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    private string pageIDhome = "1020";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            pnResult.Visible = false;
        }

        catch (Exception ex)
        {
        }
    }

    protected void btsaveandcont_Click(object sender, EventArgs e)
    {
        



        DataSet BranchTable = new DataSet();
        string SSBranchID = "";
        try
        {
            if (Session["_regionid"] != null)
            {
                SSBranchID = Session["_regionid"].ToString();
                string[] brchs = SSBranchID.Split('#');
                //check cho phep xoa
                foreach (string brch in brchs)
                {
                    if (!check_regionfee_todelete(brch))
                    {
                        pnRole.Visible = false;
                        pnResult.Visible = true;
                        btsaveandcont.Visible = false;
                        return;
                    }
                }
                foreach (string brch in brchs)
                {
                  
                    BranchTable = new SmartPortal.SEMS.RegionFee().DeleteRegionFee(brch,Session["userName"].ToString(),SmartPortal.Constant.IPC.DELETE, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {

                        //Response.Redirect("~/Default.aspx?p=141");
                    }
                    else
                    {
                        if (IPCERRORDESC == "110211")
                        {
                            throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.ACTIVEBRANCH);
                        }
                        else
                        {
                            throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.IPC);
                        }
                    }
                }
                Session["_regionid"] = null;
                //Response.Redirect(SmartPortal.Common.Encrypt.DecryptData(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["returnUrl"].ToString()));

            }
            else
            {
                //check cho phep xoa hay khong
                if (!check_regionfee_todelete(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["bid"].ToString().Trim()))
                {
                    pnRole.Visible = false;
                    pnResult.Visible = true;
                    btsaveandcont.Visible = false;
                    return;
                }
                //string regionid = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["bid"].ToString().Trim();
                BranchTable = new SmartPortal.SEMS.RegionFee().DeleteRegionFee(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["bid"].ToString(), Session["userName"].ToString(), SmartPortal.Constant.IPC.DELETE, ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {

                    //Response.Redirect("~/Default.aspx?p=141");
                }
                else
                {
                    if (IPCERRORDESC == "110211")
                    {
                        throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.ACTIVEBRANCH);
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
            pnRole.Visible = false;
            pnResult.Visible = true;
            btsaveandcont.Visible = false;
            //
            //Response.Redirect(SmartPortal.Common.Encrypt.DecryptData(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["returnUrl"].ToString()));
        }

    }
    protected void btback_Click(object sender, EventArgs e)
    {
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=" + pageIDhome));
    }
    private bool check_regionfee_todelete(string regionid)
    {

        DataTable dtregion = new SmartPortal.SEMS.RegionFee().checkfordeleteRegionFee(regionid);
        if (dtregion.Rows.Count != 0)
        {
            lblError.Text = string.Format(Resources.labels.regionfeedaduockhaibaobranch, regionid);
            return false;
        }
        else
        {
            return true;
        }


    }
}