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
using SmartPortal.ExceptionCollection;
using System.Collections.Generic;

public partial class Widgets_SEMSReceiverApprove_ViewDetail_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = "";

            if (!IsPostBack)
            {
                switch (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["stt"].ToString())
                {
                    case SmartPortal.Constant.IPC.ACTIVE:
                        Button1.Visible = false;
                        Button2.Visible = true;
                        break;
                    case SmartPortal.Constant.IPC.DELETE:
                        Button1.Visible = true;
                        Button2.Visible = false;
                        break;
                }
                BindData();
            }
        }
        catch (Exception ex)
        {
        }
    }

    void BindData()
    {
        try
        {

            #region Lấy thông tin hợp đồng
            DataTable contractTable = new DataTable();
            contractTable = (new SmartPortal.SEMS.Services().SelectByIdReveiver(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["id"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
            if (contractTable.Rows.Count != 0)
            {
                //SmartPortal.Common.Utilities.Utility.IsDateTime2().ToString("dd/MM/yy")
                lblAcctNo.Text = contractTable.Rows[0]["ACCTNO"].ToString();
                lblDesc.Text = contractTable.Rows[0]["DESCRIPTION"].ToString();
                lblLicense.Text = contractTable.Rows[0]["LICENSE"].ToString();
                lblIssuePlace.Text = contractTable.Rows[0]["ISSUEPLACE"].ToString();
                lblReceiverName.Text = contractTable.Rows[0]["RECEIVERNAME"].ToString();
                lblTranferType.Text = contractTable.Rows[0]["PAGENAME"].ToString();
                lblfullname.Text = contractTable.Rows[0]["FULLNAME"].ToString();
                if (contractTable.Rows[0]["ISSUEDATE"].ToString() != "")
                {
                    lblIssueDate.Text = SmartPortal.Common.Utilities.Utility.FormatDatetime(contractTable.Rows[0]["ISSUEDATE"].ToString(), "dd/MM/yyyy");
                }
                else
                {
                    lblIssueDate.Text = "";
                }
            }


            #endregion

            //set to session to export
            Session["DataExport"] = contractTable;
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
    protected void btApprove_Click(object sender, EventArgs e)
    {
        try
        {
           new SmartPortal.SEMS.Services().ApproveReveiver(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["id"].ToString(),SmartPortal.Constant.IPC.ACTIVE, ref IPCERRORCODE, ref IPCERRORDESC);

           if (IPCERRORCODE == "0")
           {
               lbresult.Text = Resources.labels.duyetnguoithuhuongthanhcong;
               pnConfirm.Visible = false;
               pnResult.Visible = true;
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
    protected void btReject_Click1(object sender, EventArgs e)
    {
        try
        {
            new SmartPortal.SEMS.Services().ApproveReveiver(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["id"].ToString(), SmartPortal.Constant.IPC.DELETE, ref IPCERRORCODE, ref IPCERRORDESC);

            if (IPCERRORCODE == "0")
            {
                lbresult.Text = Resources.labels.huynguoithuhuongthanhcong;
                pnConfirm.Visible = false;
                pnResult.Visible = true;
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
    protected void btnExit_Click(object sender, EventArgs e)
    {
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("Default.aspx?p=317"));
        //Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(SmartPortal.Common.Encrypt.DecryptData(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["returnURL"].ToString())));
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("Default.aspx?p=317"));
    }
}
