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
using SmartPortal.Security;
using SmartPortal.ExceptionCollection;

public partial class Widgets_IBDept_Controls_Widget : System.Web.UI.UserControl
{
    //public string _TITLE
    //{

    //    get { return lblTitleUser.Text; }
    //    set { lblTitleUser.Text = value; }
    //}
    string ACTION = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    string contractNo = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ACTION = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["a"].ToString().Trim();

            if (!IsPostBack)
            {
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
            if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["did"] != null)
            {
                #region lấy thông tin dept
                DataTable deptTable = new DataTable();
                deptTable = (new SmartPortal.IB.Dept().ViewDeptByDeptID(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["did"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
                if (deptTable.Rows.Count != 0)
                {

                    txtroomname.Text = deptTable.Rows[0]["DEPTNAME"].ToString();
                    txtdesc.Text = deptTable.Rows[0]["DESCRIPTION"].ToString();
                }
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
         //set to session to export
        //Session["DataExport"] = userTable;
    }
    protected void btsaveandcont_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable userTable = new DataTable();
            userTable = (new SmartPortal.SEMS.User().GetFullUserByUID(Session["userID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
            if (userTable.Rows.Count != 0)
            {

                contractNo = userTable.Rows[0]["CONTRACTNO"].ToString();
            }
            string Deptid = SmartPortal.Constant.IPC.DEPTIDPREFIX + DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 8);
            string Deptname = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtroomname.Text.Trim()); 
            string Desc = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtdesc.Text.Trim());

            switch (ACTION)
                    {
                        case SmartPortal.Constant.IPC.ADD:
                    #region insert user

                            new SmartPortal.IB.Dept().InsertDept(Deptid,Deptname,Desc,contractNo, ref IPCERRORCODE, ref IPCERRORDESC);

                    if (IPCERRORCODE == "0")
                    {
                        goto EXIT;
                    }
                    else
                    {
                        throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORCODE);
                    }

                    #endregion
                    break;
                case SmartPortal.Constant.IPC.EDIT:
                #region edit dept

                    new SmartPortal.IB.Dept().UpdateDept(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["did"].ToString(), Deptname, Desc, ref IPCERRORCODE, ref IPCERRORDESC);

                    if (IPCERRORCODE == "0")
                    {
                        goto EXIT;
                    }
                    else
                    {
                        throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORCODE);
                    }
                #endregion
                    break;
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.Message, this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSUser_Widget", "btsaveandcont_Click", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
 
        }
    EXIT:
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?po=3&p=190")); 
        //Response.Redirect(SmartPortal.Common.Encrypt.DecryptData(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["returnURL"].ToString())); 
    }
}


