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

public partial class Widgets_SEMSContractListCorp_Delete_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
        }

        catch (Exception ex)
        {
        }
    }

    protected void btsaveandcont_Click(object sender, EventArgs e)
    {
        DataSet contractTable = new DataSet();
        string SSContractNo = "";
        try
        {
            if (Session["_ContractNo"] != null)
            {
                SSContractNo = Session["_ContractNo"].ToString();
                string[] ctrs = SSContractNo.Split('#');
                foreach (string ctr in ctrs)
                {
                    #region Ghi log
                    try
                    {
                        SmartPortal.Common.Log.WriteLog("SEMSCONTRACTDELETE", DateTime.Now.ToString(), Session["userName"].ToString(), "EBA_CONTRACT", "CONTRACTNO='" + ctr + "'");
                    }
                    catch
                    {
                    }
                    #endregion

                    contractTable = new SmartPortal.SEMS.Contract().DeleteContract(ctr, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE != "0")
                    {
                        goto ERROR;
                    }
                }
                Session["_ContractNo"] = null;                
            }
            else
            {
                #region Ghi log
                try
                {
                    SmartPortal.Common.Log.WriteLog("SEMSCONTRACTDELETE", DateTime.Now.ToString(), Session["userName"].ToString(), "EBA_CONTRACT", "CONTRACTNO='" + SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString() + "'");
                }
                catch
                {
                }
                #endregion

                contractTable = new SmartPortal.SEMS.Contract().DeleteContract(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE != "0")
                {
                    goto ERROR;
                }
                
            }
            goto EXIT;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSContractDelete_Widget", "btsaveandcont_Click", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    ERROR:
        SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSContractDelete_Widget", "btsaveandcont_Click", IPCERRORDESC, Request.Url.Query);
        SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
    EXIT:
        if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["returnUrl"] != null)
        {
           Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(SmartPortal.Common.Encrypt.DecryptData(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["returnUrl"].ToString())));
        }
        else
        {
           Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("/?p=141"));
        }
    }
}


