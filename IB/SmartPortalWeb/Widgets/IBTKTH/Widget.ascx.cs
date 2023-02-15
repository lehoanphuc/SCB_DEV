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

using SmartPortal.IB;
using SmartPortal.ExceptionCollection;

public partial class Widgets_IBTKTH_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            pnReTranstion.Visible = false;
            //pnTIB.Visible = true;
            pnConfirm.Visible = false;
            pnNTHCNH.Visible = true;
            
           
        }
        catch
        {
        }
    }


    protected void btnTIBNext_Click(object sender, EventArgs e)
    {
        try
        {            
            pnNTHCNH.Visible = true;
            pnConfirm.Visible = false;
            pnReTranstion.Visible = false;   
        }
        catch
        {
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            //
            txtDesc.Text = string.Empty;
            txtNTHAccount.Text = string.Empty;
            txtNTHName.Text = string.Empty;
            //
            string errorCode = string.Empty;
            string errorDesc = string.Empty;
            SmartPortal.SEMS.User objUser = new SmartPortal.SEMS.User();
            Account objAcct = new Account();
            DataSet dsUser = new DataSet();
            dsUser = objUser.GetFullUserByUID(Session["userID"].ToString(), ref errorCode, ref errorDesc);            
            string ContractNo = string.Empty;
            if (errorCode == "0" && dsUser.Tables.Count == 1)
            {
                if (dsUser.Tables[0].Rows.Count == 1)
                {
                    ContractNo = dsUser.Tables[0].Rows[0]["CONTRACTNO"].ToString();
                    objAcct.InsertAcceptList(ContractNo, lblSenderName.Text, lblAcctNo.Text, lblDesc.Text, ref errorCode, ref errorDesc);
                    if (errorCode == "0")
                    {
                        pnConfirm.Visible = false;
                        pnNTHCNH.Visible = false;
                        pnReTranstion.Visible = true;
                    }
                    else
                    {
                        throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.CREATEACCOUNTFAILURE);
                    }
                }
            }
            else
            {
                throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.IPC);
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
    protected void Button3_Click(object sender, EventArgs e)
    {
        try
        {
            //pnTIB.Visible = false;
            pnNTHCNH.Visible = true;
            pnConfirm.Visible = false;
            pnReTranstion.Visible = false;
        }
        catch
        {
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //pnTIB.Visible = false;
        pnNTHCNH.Visible = false;
        pnConfirm.Visible = true;
        lblDesc.Text = txtDesc.Text;
        lblAcctNo.Text = txtNTHAccount.Text;
        lblSenderName.Text = txtNTHName.Text;
       
    }
    protected void Button4_Click1(object sender, EventArgs e)
    {
        //pnTIB.Visible = false;
        pnNTHCNH.Visible = false;
        pnConfirm.Visible = true;
        
    }
    protected void Button4_Click2(object sender, EventArgs e)
    {
        Response.Redirect("~/?po=3&p=120");
    }
    protected void Button5_Click1(object sender, EventArgs e)
    {
        Response.Redirect("~/?po=3&p=120");
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        //tao moi
        pnNTHCNH.Visible = true;
        pnConfirm.Visible = false;
        pnReTranstion.Visible = false;
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/?po=3&p=120");
    }
}
