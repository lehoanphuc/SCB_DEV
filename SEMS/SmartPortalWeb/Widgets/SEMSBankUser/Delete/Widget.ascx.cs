using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.Common.Utilities;
using SmartPortal.BLL;
using SmartPortal.ExceptionCollection;
using SmartPortal.Common.Utilities;
using SmartPortal.ExceptionCollection;

public partial class Widgets_SEMSBankUser_Delete_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string IPCERRORCODE = "";
        string IPCERRORDESC = "";
        try
        {
            UsersBLL UB = new UsersBLL();

            if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"] != null)
            {
                //UB.Delete(Utility.KillSqlInjection(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].Trim()));
                new SmartPortal.SEMS.User().DeleteUserTeller(Utility.KillSqlInjection(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].Trim()), ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    lblError.Text = Resources.labels.deleteusersuccessfully;
                    //Response.Redirect("~/Default.aspx?p=141");
                }
                else
                {
                    if (IPCERRORDESC == "110211")
                    {
                        throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.ACTIVEUSER);
                    }
                    else
                    {
                        throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.IPC);
                    }
                }
                btnDelete.Visible = false;
                pnRole.Visible = false;
                pnResult.Visible = true;
                //Write Log
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["USER"], System.Configuration.ConfigurationManager.AppSettings["DELETE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLUSER"], System.Configuration.ConfigurationManager.AppSettings["USERNAME"], SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].Trim(), "");

            }
            else
            {
                if (Session["userNameDelete"] != null)
                {
                    string catID = Session["userNameDelete"].ToString();
                    string[] catIDArray = catID.Split('-');

                    foreach (string p in catIDArray)
                    {
                        if (p != "")
                        {
                            //UB.Delete(p);
                            new SmartPortal.SEMS.User().DeleteUserTeller(p, ref IPCERRORCODE, ref IPCERRORDESC);
                            if (IPCERRORCODE == "0")
                            {
                                lblError.Text = Resources.labels.deleteusersuccessfully;
                                //Response.Redirect("~/Default.aspx?p=141");
                            }
                            else
                            {
                                if (IPCERRORDESC == "110211")
                                {
                                    throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.ACTIVEUSER);
                                }
                                else
                                {
                                    throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.IPC);
                                }
                            }
                            btnDelete.Visible = false;
                            pnRole.Visible = false;
                            pnResult.Visible = true;
                            //Write Log
                            SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["USER"], System.Configuration.ConfigurationManager.AppSettings["DELETE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLUSER"], System.Configuration.ConfigurationManager.AppSettings["USERNAME"], p, "");

                        }
                    }
                }
            }

        }
        catch (BusinessExeption bex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["userdec"], "Widgets_User_Delete_Widget", "btnDelete_Click", bex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["userdec"], Request.Url.Query);
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_User_Delete_Widget", "btnDelete_Click", sex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
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
        //redirect to page view
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=175"));
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        //redirect to page view
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=175"));
    }
}
