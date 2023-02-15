using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.Common.Utilities;
using SmartPortal.BLL;
using SmartPortal.ExceptionCollection;

public partial class Widgets_Group_Delete_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            string errorCode;
            RoleBLL MB = new RoleBLL();
            if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["gid"] != null)
            {
                errorCode= MB.Delete(Utility.IsInt(Utility.KillSqlInjection(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["gid"].Trim())));
                //Write Log
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["GROUP"], System.Configuration.ConfigurationManager.AppSettings["DELETE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLGROUP"], System.Configuration.ConfigurationManager.AppSettings["ROLEID"], Utility.KillSqlInjection(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["gid"].Trim()),"");
                if (errorCode == "0")
                {
                    lblError.Text = Resources.labels.deleterolesuccessfully;
                    lblConfirm.Text = Resources.labels.ketquathuchien;
                    btnDelete.Visible = false;
                }
                else
                {
                    throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.GROUPEXISTS);
                }
            }
            else
            {
                if (Session["roleIDDelete"] != null)
                {
                    string roleID = Session["roleIDDelete"].ToString();
                    string[] roleIDArray = roleID.Split('-');

                    foreach (string p in roleIDArray)
                    {
                        if (p != "")
                        {
                            errorCode= MB.Delete(Utility.IsInt(p));
                            //Write Log
                            SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["GROUP"], System.Configuration.ConfigurationManager.AppSettings["DELETE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLGROUP"], System.Configuration.ConfigurationManager.AppSettings["ROLEID"], p,"");
                            if (errorCode == "-1")
                            {
                                lblError.Text = Resources.labels.deleterolesuccessfully;
                                lblConfirm.Text = Resources.labels.ketquathuchien;
                                btnDelete.Visible = false;
                            }
                            else
                            {
                                throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.GROUPEXISTS);
                            }
                        }
                    }
                }
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (BusinessExeption bex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["gdec"], "Widgets_Group_Delete_Widget", "btnDelete_Click", bex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["gdec"], Request.Url.Query);
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_Group_Delete_Widget", "btnDelete_Click", sex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Group_Delete_Widget", "btnDelete_Click", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=159"));
    }
}
