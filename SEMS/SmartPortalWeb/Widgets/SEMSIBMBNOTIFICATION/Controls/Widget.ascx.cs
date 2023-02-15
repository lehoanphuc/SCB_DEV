using SmartPortal.SEMS;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


public partial class Widgets_SEMSProduct_Controls_Widget :System.Web.UI.UserControl
{
    string ACTION = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    static string usercreate = string.Empty;
    static string id = string.Empty;
    static string pageBackID = "1015";
    public string _IMAGE
    {
        get { return imgLoGo.ImageUrl; }
        set { imgLoGo.ImageUrl = value; }
    }

    public string _TITLE
    {
        get { return lblTitleProduct.Text; }
        set { lblTitleProduct.Text = value; }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        //TabCustomerInfoHelper.LoadConfig();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ACTION = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["a"].ToString().Trim();
           if (!IsPostBack)
            {
                string errorCode = string.Empty;
                string errorDesc = string.Empty;
                DataSet dsservice = new Services().GetAll(SmartPortal.Constant.IPC.ACTIVE, ref  errorCode, ref  errorDesc);

                if (errorCode == "0")
                {

                    DataTable dtservice = dsservice.Tables[0];
                    ddlService.DataSource = dtservice;
                    ddlService.DataTextField = "ServiceName";
                    ddlService.DataValueField = "ServiceID";
                    ddlService.DataBind();
                    ddlService.Items.Remove(ddlService.Items.FindByValue("PHO"));
                    ddlService.Items.Remove(ddlService.Items.FindByValue("SEMS"));
                    ddlService.Items.Remove(ddlService.Items.FindByValue("SMS"));
                }
                DataTable dtvar = new Notification().GetVarnotify();
                ddlVarname.DataSource = dtvar;
                ddlVarname.DataTextField = "varname";
                ddlVarname.DataValueField = "varid";
                ddlVarname.DataBind();
                DataTable dtmbver = new Notification().GetMBversion();
               
                ddlMBversion.DataSource = dtmbver;
                ddlMBversion.DataTextField = "VARVALUE1";
                ddlMBversion.DataValueField = "VARVALUE1";
                ddlMBversion.DataBind();
                showhideversionMB();
               //show hide item
                trval5.Visible = false;
                trval6.Visible = false;
                trval7.Visible = false;
                trval8.Visible = false;
                trval9.Visible = false;
                trval10.Visible = false;
                //load loop default
                ddlLoop.Text = ConfigurationManager.AppSettings["ibmbnotificationLoopdefault"].ToString();
               //load datetime
                txtdatestart.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtdateend.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txttimestart.Text = "00:00:00";
                txttimeend.Text = "23:59:00";
                DateTime dt1 = DateTime.ParseExact(txtdatestart.Text + ' ' + txttimestart.Text, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                DateTime dt2 = DateTime.ParseExact(txtdateend.Text + ' ' + txttimeend.Text, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                ddlVarname.Enabled = false;
               BindData();
                      
            }
        }
        catch (Exception ex)
        {
        }
     
    }
    void BindData() 
    {
        
      
        DataTable tblRoleDefault = new DataTable();
        try
        {
            //DropDownList.Items.Add(new ListItem(Resources.labels.canhan, "P"));
            //DropDownList.Items.Add(new ListItem(Resources.labels.doanhnghiep, "O"));

  

            switch (ACTION)
            {
                case "add":
                   
                    break;
                default:
                    #region Lấy thông tin input

                    DataTable productTable = new DataTable();
                    productTable = (new Notification().GetNotifyByCondition(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["pid"].ToString(),string.Empty,string.Empty,string.Empty,string.Empty, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
                    if (productTable.Rows.Count != 0)
                    {
                        id = productTable.Rows[0]["ID"].ToString();
                        ddlService.SelectedValue = productTable.Rows[0]["serviceID"].ToString();
                        ddlVarname.SelectedValue = productTable.Rows[0]["varname"].ToString();
                        txtContent.Text = productTable.Rows[0]["varvalue1"].ToString();
                        txtLink.Text = productTable.Rows[0]["varvalue2"].ToString();
                        ddlLoop.Text = productTable.Rows[0]["varvalue3"].ToString();
                        
                        ddlDevicetype.SelectedValue = productTable.Rows[0]["devicetype"].ToString();
                        if(ddlService.SelectedValue.Equals(SmartPortal.Constant.IPC.MB))
                        {
                            string[] mb = productTable.Rows[0]["varvalue4"].ToString().Split('-');
                            ddlMBversion.SelectedValue = mb[0].ToString();
                            ddlVersionupdown.SelectedValue = mb[1].ToString();
                        }
                        txtdatestart.Text = Convert.ToDateTime(productTable.Rows[0]["startdate"]).ToString("dd/MM/yyyy");
                        txtdateend.Text = Convert.ToDateTime(productTable.Rows[0]["enddate"]).ToString("dd/MM/yyyy");
                        txttimestart.Text = Convert.ToDateTime(productTable.Rows[0]["startdate"]).ToString("HH:mm:00");
                        txttimeend.Text = Convert.ToDateTime(productTable.Rows[0]["enddate"]).ToString("HH:mm:00");
                        showhideversionMB();
                    }
                    #endregion


                    break;
            }




            #region Enable/Disable theo Action
            switch (ACTION)
            {
                case "viewdetail":
                    pnAdd.Enabled = false;
                    btback.Visible = true;
                    btsave.Visible = false;
                    
                    break;
                case "edit":
                    ddlService.Enabled=false;
                    break;
            }
            #endregion 

        }
        catch
        {
        }
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
       
        if(!validateinput())
        {
            return;
        }
        if (!checkvalidatedatetime(DateTime.ParseExact(txtdatestart.Text + ' ' + txttimestart.Text, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture),DateTime.ParseExact(txtdateend.Text + ' ' + txttimeend.Text, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture)))
        {
            return;
        }
      //get data
        IPCERRORCODE = string.Empty;
        IPCERRORDESC = string.Empty;
        string serviceid = ddlService.SelectedValue.ToString();
        string varname = ddlVarname.SelectedValue.ToString();
        string content = txtContent.Text.Trim();
        
        while (content.Contains("  "))
        {
            content=content.Replace("  ", " ");
        }
        
        string link = txtLink.Text.Trim();
        string loop = ddlLoop.Text.ToString();
        string devicetype = ddlDevicetype.SelectedValue.ToString();
        string mbversion = ddlMBversion.SelectedValue.ToString() + "-" + ddlVersionupdown.SelectedValue.ToString();
        string value5 = txtValue5.Text.Trim();
        string value6 = txtValue6.Text.Trim();
        string value7 = txtValue7.Text.Trim();
        string value8 = txtValue8.Text.Trim();
        string value9 = txtValue9.Text.Trim();
        string value10 = txtValue10.Text.Trim();
        usercreate = Session["Username"].ToString();
        string status = SmartPortal.Constant.IPC.ACTIVE;
        DateTime starttime = DateTime.ParseExact(txtdatestart.Text + ' ' + txttimestart.Text, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
        DateTime endtime = DateTime.ParseExact(txtdateend.Text + ' ' + txttimeend.Text, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                    
        switch (ACTION)
        {
            case "add":
                
                try
                {
                    new SmartPortal.SEMS.Notification().InsertNotify(serviceid, varname, content, link, loop, devicetype, mbversion, starttime, endtime, value5, value6, value7, value8, value9, value10, usercreate, status, ref IPCERRORCODE, ref IPCERRORDESC);

               
                    if (IPCERRORCODE.Equals("0"))
                    {
                        lbError.Text = Resources.labels.themmoithanhcong;
						btsave.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    lbError.Text = ex.ToString();
                }

                break;
            case "edit":
                  try
                {
                    new SmartPortal.SEMS.Notification().UpdateNotify(id,serviceid, varname, content, link, loop, devicetype, mbversion, starttime, endtime, value5, value6, value7, value8, value9, value10, usercreate, status, ref IPCERRORCODE, ref IPCERRORDESC);

               
                    if (IPCERRORCODE.Equals("0"))
                    {
                        btsave.Visible = false;
                        lbError.Text = Resources.labels.suathanhcong;
                    }
                }
                catch (Exception ex)
                {
                    lbError.Text = ex.ToString();
                }

                break;
        }
      
    }
    protected void ddlService_SelectedIndexChanged(object sender, EventArgs e)
    {
        showhideversionMB();
    }
    private void showhideversionMB()
    {
        if (ddlService.SelectedValue.ToString()==SmartPortal.Constant.IPC.MB)
        {
            trmbver.Visible = true;
            trdevicetype.Visible = true;
        }
        else
        {
            trmbver.Visible = false;
            trdevicetype.Visible =false;
        }
    }
    private void ShowPopUpMsg(string msg)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("alert('");
        sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"));
        sb.Append("');");
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showalert", sb.ToString(), true);
    }
    private bool validateinput()
    {
        lbError.Text = string.Empty;
        
        if (string.IsNullOrEmpty(txtContent.Text.Trim()))
        {
            lbError.Text = "Content can not be null or empty";
            txtContent.Focus();
            //ShowPopUpMsg("Input link invalid");
            return false;
        }
        if (!string.IsNullOrEmpty(txtLink.Text.Trim()))
        {
            if (!System.Uri.IsWellFormedUriString(txtLink.Text.Trim(), System.UriKind.Absolute))
            {
                lbError.Text = "input link invalid";
                //ShowPopUpMsg("Input link invalid");
                return false;
            }
            else
            {
                //return true;
            }
        }
        //check looptime
        if (string.IsNullOrEmpty(ddlLoop.Text.Trim()))
        {
            lbError.Text = "Loop can not be Empty";
            return false;
        }
        if (int.Parse(ddlLoop.Text.Trim()) > int.Parse(ConfigurationManager.AppSettings["ibmbnotificationLoop"]))
        {
            lbError.Text = "Loop must be equal or smaller than 100";
            return false;
        }
        if (!checkdate(txtdatestart) || !checkdate(txtdateend) || !checktime(txttimestart) || !checktime(txttimeend))
      {
          return false;
      }
        return true;
    }
    private bool checkdate(TextBox text)
    {
        try
        {
            lbError.Text = string.Empty;
            DateTime dt = DateTime.ParseExact(text.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
        }
        catch
        {
            lbError.Text = Resources.labels.datetimeinvalid;
            text.Focus();
            return false;
           
        }
        return true;

    }
    private bool checktime(TextBox text)
    {
        try
        {
            lbError.Text = string.Empty;
            DateTime dt = DateTime.ParseExact(text.Text.Trim(), "HH:mm:ss", CultureInfo.InvariantCulture);
        }
        catch
        {
            lbError.Text = Resources.labels.timeinvalid;
            text.Focus();
            return false;

        }
        return true;

    }
    private bool checkvalidatedatetime(DateTime dt1,DateTime dt2)
    {
        lbError.Text = string.Empty;
        if(DateTime.Compare(dt1,dt2)<=0)
        {
            return true;
        }
        else
        {
            lbError.Text = Resources.labels.timestartmustsmallerthantimeend;
            return false;
        }
    }
    protected void btback_Click(object sender, EventArgs e)
    {
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=" + pageBackID));
    }
}
