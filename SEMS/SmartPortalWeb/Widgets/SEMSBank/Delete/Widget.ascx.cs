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

public partial class Widgets_SEMSProductDelete_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
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
        DataTable BankTable = new DataTable();
        string SSProductID = "";
        int insertt = -1;
        try
        {
            if (Session["_OtherBank"] != null)
            {
                SSProductID = Session["_OtherBank"].ToString();
                string[] pros = SSProductID.Split('#');
                foreach (string pro in pros)
                {

                    //insertt = new SmartPortal.SEMS.Bank().DeleteRootBank(pro);
                    BankTable = new SmartPortal.SEMS.Bank().DeleteRootBank(pro);
                    if (BankTable.Rows.Count>0)
                    {
                        lblError.Text = Resources.labels.nganhangcontontaichinhanh;
                        pnResult.Visible = true;
                        pnRole.Visible = false;
                        btsaveandcont.Visible = false;
                        
                    }
                    else
                    {
                     
                        pnResult.Visible = true;
                        pnRole.Visible = false;
                        btsaveandcont.Visible = false;
                        //throw new IPCException("");
                    }

                    //}
                    //if (insertt != -1)
                    //{
                    //    throw new IPCException("");
                    //}
                    //else
                    //{
                    //    pnResult.Visible = true;
                    //    pnRole.Visible = false;
                    //    btsaveandcont.Visible = false;

                    //}

                }
                Session["_OtherBank"] = null;
                //Response.Redirect(SmartPortal.Common.Encrypt.DecryptData(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["returnUrl"].ToString()));

            }
            else
            {
                //insertt = new SmartPortal.SEMS.Bank().DeleteBank(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["bid"].ToString().Trim());
                BankTable = new SmartPortal.SEMS.Bank().DeleteRootBank(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["bid"].ToString().Trim());
                if (BankTable.Rows.Count > 0)
                {
                    lblError.Text = Resources.labels.nganhangcontontaichinhanh;
                    pnResult.Visible = true;
                    pnRole.Visible = false;
                    btsaveandcont.Visible = false;

                }
                else
                {

                    pnResult.Visible = true;
                    pnRole.Visible = false;
                    btsaveandcont.Visible = false;
                    //throw new IPCException("");
                }
                //if (insertt != -1)
                //{
                //    throw new IPCException("");
                //}
                //else
                //{
                //    pnResult.Visible = true;
                //    pnRole.Visible = false;
                //    btsaveandcont.Visible = false;

                //}
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
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=403"));
    }
}


