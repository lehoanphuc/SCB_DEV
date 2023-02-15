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

public partial class Widgets_IBCorpUser_Detele_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = "";
        }

        catch (Exception ex)
        {
        }
    }

    protected void btsaveandcont_Click(object sender, EventArgs e)
    {
        DataSet delTable = new DataSet();
        string SSUID = "";
        try
        {
            if (Session["_Userid"] != null)
            {
                SSUID = Session["_Userid"].ToString();
                string[] uids = SSUID.Split('#');
                foreach (string uid in uids)
                {
                    DataTable dtuser = (new SmartPortal.SEMS.User().GetFullUserByUID(uid, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
                    if (dtuser.Rows[0]["TypeID"].ToString().Trim().Equals(SmartPortal.Constant.IPC.CHUTAIKHOAN))
                    {
                        lblError.Text = "Can not delete Account holder";
                        btsaveandcont.Visible = false;
                        Label2.Visible = false;
                        return;
                    }
                }
                
                foreach (string uid in uids)
                {
                    delTable = new SmartPortal.SEMS.User().DeleteUserByID(uid, SmartPortal.Constant.IPC.DELETE, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        try
                        {
                            string sContract = "";
                            DataSet ds = new SmartPortal.SEMS.User().GetFullUserByUID(uid, ref IPCERRORCODE, ref IPCERRORDESC);
                            if (ds.Tables[0].Rows.Count > 0) sContract = ds.Tables[0].Rows[0]["ContractNo"].ToString();
                            SmartPortal.Common.Log.WriteLogDatabase(sContract, uid, Request.Url.ToString(), Session["userName"].ToString(),
                                Request.UserHostAddress, "EBA_Users", "Delete User", "", "Deleted", SmartPortal.Constant.IPC.DELETE);
                        }
                        catch { }
                    }
                }
                lblError.Text = Resources.labels.deleteusersuccessfull;
                lbxacnhan.Visible = false;
                btsaveandcont.Visible = false;
                Label2.Visible = false;
            }
            else
            {
                //DataSet delTable = new DataSet();
                delTable = new SmartPortal.SEMS.User().DeleteUserByID(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].ToString(), SmartPortal.Constant.IPC.DELETE, ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    try
                    {
                        string sContract = "";
                        DataSet ds = new SmartPortal.SEMS.User().GetFullUserByUID(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                        if (ds.Tables[0].Rows.Count > 0) sContract = ds.Tables[0].Rows[0]["ContractNo"].ToString();
                        SmartPortal.Common.Log.WriteLogDatabase(sContract, SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].ToString(), Request.Url.ToString(), Session["userName"].ToString(),
                            Request.UserHostAddress, "EBA_Users", "Delete User", "", "Deleted", SmartPortal.Constant.IPC.DELETE);
                    }
                    catch { }
                    //Response.Redirect(SmartPortal.Common.Encrypt.DecryptData(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["returnUrl"].ToString()));
                    btsaveandcont.Visible = false;

                    lblError.Text =Resources.labels.deleteusersuccessfull ;
                    lbxacnhan.Visible = false;
                    Label2.Visible = false;
                }
                else
                {
                    lblError.Text = IPCERRORDESC;
                    btsaveandcont.Enabled = false;
                }
            }
        }
        catch (Exception ex)
        { }
    }
    protected void btback_Click(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.DecryptData(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["returnUrl"].ToString()));
    }
}


