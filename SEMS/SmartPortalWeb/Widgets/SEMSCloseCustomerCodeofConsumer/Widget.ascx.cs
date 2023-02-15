using System;
using System.Data;
using System.Collections.Generic;
using System.Web;

public partial class Widgets_SEMSCloseCustomerCodeofConsumer_Widget : WidgetBase
{
    public static bool isAscend = false;
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            enableControl();

            if (!IsPostBack)
            {
                BindData();
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
   protected void BindData()
    {
        try
        {
            defaultColor();
            //txtTransactionDate.Text= DateTime.Now.ToString("dd/MM/yyyy");
            loadDropDownList();
            DataSet ds = new DataSet();
            object[] inforConsumer = new object[] { txtPhoneNumber.Text };
            ds = _service.common("SEMS_BO_GETINFO_WAL", inforConsumer, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtFullName.Text = ds.Tables[0].Rows[0]["FULLNAME"].ToString();
                    ddlStatus.SelectedValue = ds.Tables[0].Rows[0]["STATUS_CUSINFO"].ToString();
                    txtConsumerCode.Text = ds.Tables[0].Rows[0]["CUSTID"].ToString();
                }
            }
            else
            {
                lblError.Text = IPCERRORDESC;
            }
        }
        catch(Exception e)
        {
            lblError.Text = e.Message;
        }
        
    }
    void loadDropDownList()
    {
        DataSet ds = new DataSet();
        ds = _service.GetValueList("STATUS", "EBA_Contract", ref IPCERRORCODE, ref IPCERRORDESC);
        if(IPCERRORCODE=="0")
        {
            if(ds.Tables[0].Rows.Count>0 )
            {
                ddlStatus.DataSource = ds;
                ddlStatus.DataValueField = "ValueID";
                ddlStatus.DataTextField = "Caption";
                ddlStatus.DataBind();
            }
        }
    }

    void enableControl()
    {
        txtTransactionNumber.Enabled = false;
        txtTransactionDate.Enabled = false;
        txtFullName.Enabled = false;
        ddlStatus.Enabled = false;
        txtConsumerCode.Enabled = false;
    }

    protected void load_info(object sender, EventArgs e)
    {
        BindData();
    }
    void setPara(Dictionary<object, object> info)
    {

        info.Add("PHONE", txtPhoneNumber.Text);
        info.Add("CURRENTSTATUS", ddlStatus.SelectedValue);
        info.Add("USERMODIFY", HttpContext.Current.Session["userID"].ToString());
    }
    void defaultColor()
    {
        txtPhoneNumber.BorderColor = System.Drawing.Color.Empty;
        txtFullName.BorderColor = System.Drawing.Color.Empty;
    }
    protected void btnAccept_Click(object sender, EventArgs e)
    {
        try
        {
            #region validation
            defaultColor();
            if (txtPhoneNumber.Text.Equals(string.Empty))
            {
                lblError.Text = Resources.labels.PhoneNumber + Resources.labels.IsNotNull;
                txtPhoneNumber.BorderColor = System.Drawing.Color.Red;
                txtPhoneNumber.Focus();
                return;
            }
            if (txtFullName.Text.Equals(string.Empty))
            {
                lblError.Text = Resources.labels.fullname + Resources.labels.IsNotNull;
                txtFullName.BorderColor = System.Drawing.Color.Red;
                txtFullName.Focus();
                return;
            }
            #endregion
            DataSet ds = new DataSet();
            Dictionary<object, object> info = new Dictionary<object, object>();
            setPara(info);
            ds = _service.CallStore("SEMS_BO_CUSCHANGESTA", info, "Change Consumer Status of Consumer", "N", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtTransactionNumber.Text = ds.Tables[0].Rows[0]["TXREFID"].ToString();
                    txtTransactionDate.Text = ds.Tables[0].Rows[0]["TXDT"].ToString();
                    lblError.Text = Resources.labels.success;
                }
            }
            else
            {
                lblError.Text = IPCERRORDESC;
            }


        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtTransactionNumber.Text = string.Empty;
        txtTransactionDate.Text = string.Empty;
        txtPhoneNumber.Text = string.Empty;
        txtFullName.Text = string.Empty;
        txtConsumerCode.Text = string.Empty;
        loadDropDownList();
    }
}