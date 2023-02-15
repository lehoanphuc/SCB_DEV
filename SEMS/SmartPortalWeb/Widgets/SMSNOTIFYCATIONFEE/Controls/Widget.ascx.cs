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

using SmartPortal.ExceptionCollection;
using SmartPortal.BLL;

public partial class Widgets_SMSNOTIFYCATIONFEE_Controls_Widget :System.Web.UI.UserControl
{
    string ACTION = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    string pid = "";
    private string PageHomeID="1033";
   
    public string _IMAGE
    {
        get { return imgLoGo.ImageUrl; }
        set { imgLoGo.ImageUrl = value; }
    }

    public string _TITLE
    {
        get { return lblTitleBranch.Text; }
        set { lblTitleBranch.Text = value; }
    }
   

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ACTION = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["a"].ToString().Trim();
           if (!IsPostBack)
            {
                //load group
                string filter = "serviceid='"+ConfigurationManager.AppSettings["ServiceidSMS"].ToString()+
                    "' and roletype='" + ConfigurationManager.AppSettings["smsnotifycode"].ToString()+"'";
                string  test= ConfigurationManager.AppSettings["ServiceidSMS"].ToString();

               string stSort = "serviceid asc";
                DataTable dtrole = new GroupBLL().Load().Select(filter, stSort).CopyToDataTable();
               
                ddlRole.DataSource = dtrole;
                ddlRole.DataTextField = "RoleName";
                ddlRole.DataValueField = "RoleID";
                ddlRole.DataBind();  
               //load sms notify fee
               try
               { 
               DataSet ds = new DataSet();
                ds = new SmartPortal.SEMS.Fee().GetSMSNotifyFee("SMS",ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
               {

                   ddlFeetype.DataSource = ds.Tables[0];
                   ddlFeetype.DataTextField = "IFCNAME";
                   ddlFeetype.DataValueField = "IFCCD";
                   ddlFeetype.DataBind();
                    //gan vao viewstate de dung sau:
                   ViewState["smsnotifyfee"] = ds.Tables[0];
               }
               else
               {
                   SmartPortal.Common.Log.WriteLogFile("", "", "", "LOAD SMS NOTIFY FEE FROM CORE ERROR WITH ERRORCODE---" + IPCERRORCODE);
               }
               }
               catch (Exception ex)
               {
                   SmartPortal.Common.Log.WriteLogFile("", "", "", "LOAD SMS NOTIFY FEE FROM CORE ERROR---" + ex.ToString());
               }
                BindData();                      
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.WriteLogFile("", "", "", "ROLE CAN NOT LOAD ---" + ex.ToString() );
        }
     
    }
    void BindData() 
    {
        try
        {
            string rid = string.Empty;
            string fid = string.Empty;
            lblError.Text = string.Empty;
            // LOAD ROLEID,FEEID FROM EBCORE
            switch (ACTION)
            {
                case "add":
                    break;
                default:
                      rid=SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["rid"].ToString();
                    fid=SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["fid"].ToString();
                    DataSet ds = new DataSet();
                    ds = new SmartPortal.SEMS.Fee().SearchFeeSMSNotify(rid, fid, string.Empty, string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);
                    ddlRole.SelectedValue = rid;
                    ddlFeetype.SelectedValue = fid;
                    break;
            }


            //ds=new SmartPortal.SEMS.Fee().SearchFeeSMSNotify()

            // load for right list
            DataTable dtright = new DataTable();
            int roleid = int.Parse(ddlRole.SelectedValue.ToString());
            dtright = new SmartPortal.SEMS.Role().GetRightforSMSnotify(roleid, ConfigurationManager.AppSettings["smsnotifytrancode"].ToString());
            if (dtright.Rows.Count > 0)
            {
                string rightlist = string.Empty;
                foreach (DataRow row in dtright.Rows)
                {
                    rightlist = rightlist + row["PageName"].ToString() + "<br />";


                }
                lbrightlist.Text = rightlist;
            }
            else
            {

                SmartPortal.Common.Log.WriteLogFile("", "", "", "SMS NOTIFICATION GET RIGHT LIST ERROR  WITH NO ROWS");
            }
            //load sms notify fee
            DataTable tblfee = (DataTable)ViewState["smsnotifyfee"];
            DataRow[] ro = tblfee.Select("IFCCD='" + ddlFeetype.SelectedValue.ToString().Trim() + "'");
            if (ro.Length != 0)
            {
                lbmaphi.Text = ro[0]["IFCCD"].ToString();
                lbtenphi.Text = ro[0]["IFCNAME"].ToString();
                lbamount.Text = ro[0]["IFCVAL"].ToString();
                lblccyid.Text = ro[0]["CCRCD"].ToString();
                
            }

         
            #region Enable/Disable theo Action
            switch (ACTION)
            {
                case "viewdetail":
                    ddlRole.Enabled = false;
                    ddlFeetype.Enabled = false;
                    btback.Visible = true;
                    btsave.Visible = false;
                    break;
                case "edit":
                    ddlRole.Enabled = false;
                    btback.Visible = true;
                    btsave.Visible = true;
                    break;
            }
            #endregion 

        }
        catch(Exception ex)
        {
        }
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        //string Branchid = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtmacn.Text.Trim());
        //string Branchname = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txttencn.Text.Trim());
        //string address = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtaddress.Text.Trim());
        //string phone = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtphone.Text.Trim());
        //string PosX = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtPosX.Text.Trim());
        //string PosY = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtPosY.Text.Trim());
        //string city = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlcity.SelectedValue.Trim());
        //string dist = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlDist.SelectedValue.Trim());
        lblError.Text = string.Empty;
        string rid = ddlRole.SelectedValue.ToString();
        string fid = ddlFeetype.SelectedValue.ToString();
        string fname = lbtenphi.Text;
        double amount = Convert.ToDouble(lbamount.Text.Trim());
        //check for duplicate insert
        DataSet ds = new DataSet();
        ds = new SmartPortal.SEMS.Fee().SearchFeeSMSNotify(rid, string.Empty, string.Empty, string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE=="0")
        {
            if (ds.Tables[0].Rows.Count>0)
            {
                lblError.Text = Resources.labels.datontaismsnotifyfee;
                return;
            }
        }




        switch (ACTION)
        {
           case "add":
                try
                {
                  
                    
                   
                    new SmartPortal.SEMS.Fee().InsertFeeSMSNotify(rid, fid, fname, amount, Session["userName"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        lblError.Text = Resources.labels.thanhcong;
                        btsave.Visible = false;
                    }
					else
					{
						lblError.Text = IPCERRORDESC;
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

                break;
            case "edit":
              
                 try
                {
                   
                   
                    
                    new SmartPortal.SEMS.Fee().UpdateFeeSMSNotify(rid, fid, fname, amount, Session["userName"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        lblError.Text = Resources.labels.thanhcong;
                        btsave.Visible = false;
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
                break;
        }

    }

    protected void btback_Click(object sender, EventArgs e)
    {
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p="+ PageHomeID));
    }
    protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void ddlFeetype_SelectedIndexChanged(object sender, EventArgs e)
    {
        //load sms notify fee
        DataTable tblfee = (DataTable)ViewState["smsnotifyfee"];
        DataRow[] ro = tblfee.Select("IFCCD='" + ddlFeetype.SelectedValue.ToString().Trim() + "'");
        if (ro.Length != 0)
        {
            lbmaphi.Text = ro[0]["IFCCD"].ToString();
            lbtenphi.Text = ro[0]["IFCNAME"].ToString();
            lbamount.Text = ro[0]["IFCVAL"].ToString();

        }
    }
}
