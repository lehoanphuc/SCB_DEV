using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.Common.Utilities;
using SmartPortal.BLL;
using SmartPortal.ExceptionCollection;

public partial class Widgets_IBCorpRole_Delete_Widget : WidgetBase
{
    string IPCERRORCODE;
    string IPCERRORDESC;

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            RoleBLL MB = new RoleBLL();

            if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["gid"] != null)
            {
                new SmartPortal.IB.Role().Delete(Utility.IsInt(Utility.KillSqlInjection(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["gid"].Trim())),ref IPCERRORCODE,ref IPCERRORDESC);
                //MB.Delete(Utility.IsInt(Utility.KillSqlInjection(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["gid"].Trim())));
                if (IPCERRORCODE != "0")
                {
                    if (IPCERRORDESC == SmartPortal.Constant.IPC.ERRORCODE.GROUPEXISTS)
                    {
                        throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.GROUPEXISTS);
                    }
                    else
                    {
                        throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.IPC);
                    }
                }
                else
                {
                    lblDelete.Text = "<span style='color:red;'>"+Resources.labels.xoanhomthanhcong+"</span>";
                    lbDelete.Visible = false;
                    imgDelete.Visible = false;
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
                            new SmartPortal.IB.Role().Delete(Utility.IsInt(p), ref IPCERRORCODE, ref IPCERRORDESC);
                            //MB.Delete(Utility.IsInt(Utility.KillSqlInjection(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["gid"].Trim())));
                            if (IPCERRORCODE != "0")
                            {
                                if (IPCERRORDESC == SmartPortal.Constant.IPC.ERRORCODE.GROUPEXISTS)
                                {
                                    throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.GROUPEXISTS);
                                }
                                else
                                {
                                    throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.IPC);
                                }
                            }
                            else
                            {
                                lblDelete.Text = "<span style='color:red;'>"+Resources.labels.xoanhomthanhcong+"</span>";
                                lbDelete.Visible = false;
                                imgDelete.Visible = false;
                            }
                            //MB.Delete(Utility.IsInt(p));
                            ////Write Log
                            //SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["GROUP"], System.Configuration.ConfigurationManager.AppSettings["DELETE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLGROUP"], System.Configuration.ConfigurationManager.AppSettings["ROLEID"], p,"");

                        }
                    }
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
      
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?po=3&p=193"));
    }
}
