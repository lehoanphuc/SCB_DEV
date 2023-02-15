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

using System.Text;
using SmartPortal.Common;
using SmartPortal.Common.Utilities;
using SmartPortal.ExceptionCollection;

public partial class Widgets_IBFeedBack_Details_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    public static string pn = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["a"].ToString().Trim() == "add")
                {
                    btnPrint.Visible = false;
                }

                BindData();
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.Message, this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    void BindData()
    {
        try
        {
            #region Load comment
            DataTable dtComment = new DataTable();
            if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["tid"].ToString() != "")
            {
                btnsend.Visible = false;
                string TrID = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["tid"].ToString();
                pnipctransid.Visible = false;
                pntitle.Visible = true;
                pnfeedback.Visible = false;
                pncomment.Visible = true;
                dtComment = new SmartPortal.IB.Transactions().GetFeedback(TrID, "", Session["userID"].ToString(), "","","");
                if (dtComment.Rows.Count != 0)
                {
                    lbltieude.Text = dtComment.Rows[0]["title"].ToString();
                    areaContent.Value = dtComment.Rows[0]["contentfb"].ToString();

                    areacomment.Value = dtComment.Rows[0]["comment"].ToString();
                    //ghi vo session dung in
                    Hashtable hasPrint = new Hashtable();
                    hasPrint.Add("transid", dtComment.Rows[0]["IPCTransID"].ToString());
                    hasPrint.Add("title", lbltieude.Text);
                    hasPrint.Add("content", areaContent.Value);
                    hasPrint.Add("comment", areacomment.Value);
                    Session["printFeedBack"] = hasPrint;

                }
            }
            else
            {
                pncomment.Visible = false;
                pnfeedback.Visible = false;
                pnipctransid.Visible = true;
                pntitle.Visible = false;
            }
            if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["a"].ToString() == "")
            {
                areaContent.Attributes["readonly"] = "readonly";

            }

        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.Message, this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }

        #endregion
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx?po=3&p=394");
    }
    protected void btnsend_Click(object sender, EventArgs e)
    {
        try
        {
            int insert = -1;
            if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["tid"].ToString() != "")
            {
                insert = new SmartPortal.IB.Transactions().InsertFeedBack(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["tid"].ToString(), Session["userID"].ToString(), txttitle.Text.ToString(), areaContent.Value.ToString(), "", SmartPortal.Constant.IPC.NO);

            }
            else
            {
                insert = new SmartPortal.IB.Transactions().InsertFeedBack(txtsgd.Text.Trim(), Session["userID"].ToString(),txttitle.Text.Trim(), areaContent.Value.ToString(), "", SmartPortal.Constant.IPC.NO);
            }
            if (insert != -1)
            {
                throw new IPCException(IPCERRORDESC);
            }
            else
            {
                pnConfirm.Visible = false;
                pncomment.Visible = false;
                pnipctransid.Visible = false;
                pntitle.Visible = false;
                pnfeedback.Visible = true;
                btnsend.Visible = false;
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.Message, this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
}
