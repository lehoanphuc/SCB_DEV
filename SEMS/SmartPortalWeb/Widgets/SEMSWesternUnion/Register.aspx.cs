using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SmartPortal.ExceptionCollection;
using System.Text;
using System.Configuration;
using SmartPortal.Common;
using SmartPortal.Common.Utilities;

public partial class Widgets_SEMSWesternUnion_Register : System.Web.UI.Page
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["userName"] == null || Session["userName"].ToString() == "guest")
                Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx"));
            if (!IsPostBack)
            {
                if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["id"] != null)
                {
                    BindData();
                }
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
            string TranID = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["id"].ToString().Trim();
            LoadTranfers(TranID);
            BindData1();
        }

        catch
        { }
    }

    void BindData1()
    {
        string str = "<div><img src = '../../../Images/logo.png' style='height:70px;font-size: 14px;' /><br/><br/></div>";
        str += Session["print"].ToString();
        str += "<div style='font-size: 14px;'><br/><p><a href='https://www.scb.co.th/en/personal-banking.html' target='blank'></a></p><span style='font-weight:bold;'>" + Resources.labels.camonquykhachdasudungdichvucuaABank + "!	</span></div>";

        lblView.Text = str;

    }

    void LoadTranfers(string cn)
    {
        Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();
        tmpl = SmartPortal.Common.ST.GetStringTemplate("SEMSWesternUnion", "ContractAttachment" + "en-US");

        //lay thong tin hop dong de gui mail
        try
        {
            goto EXIT;
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSContractApprove_Widget", "SendInfoLogin", IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSContractApprove_Widget", "SendInfoLogin", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    ERROR:
        SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSContractApprove_Widget", "SendInfoLogin", IPCERRORDESC, Request.Url.Query);
        SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
    EXIT:

        ;
    }

}
