using System;
using System.Data;
using System.Collections.Generic;
using System.Web;

public partial class Widgets_SEMSChangeContractStatusofAgent_Widget : WidgetBase
{
    public static bool isAscend = false;
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();
    protected void Page_Load(object sender, EventArgs e)
    {
        lblError.BorderColor = System.Drawing.Color.Red;
        try
        {
            lblError.Text = string.Empty;
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
            enableControl();
            loadDropDownList();
            DataSet ds = new DataSet();
            object[] inforConsumer = new object[] { txtPhoneNumber.Text, "AGT" };
            ds = _service.common("SEMS_BO_GETINFO_WAL", inforConsumer, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtFullName.Text = ds.Tables[0].Rows[0]["FULLNAME"].ToString();
                }
                else
                {
                    setControlDefault();
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
    void loadDropDownList()
    {
        DataSet ds = new DataSet();
        ds = _service.GetValueList("EBA_Contract", "STT", ref IPCERRORCODE, ref IPCERRORDESC);
        if(IPCERRORCODE=="0")
        {
            if(ds.Tables[0].Rows.Count>0 )
            {
                ddlSourceStatus.DataSource = ds;
                ddlSourceStatus.DataValueField = "ValueID";
                ddlSourceStatus.DataTextField = "Caption";
                ddlSourceStatus.DataBind();

                ddlDestinationStatus.DataSource = ds;
                ddlDestinationStatus.DataValueField = "ValueID";
                ddlDestinationStatus.DataTextField = "Caption";
                ddlDestinationStatus.DataBind();
            }
        }
    }

    void enableControl()
    {
        txtTransactionNumber.Enabled = false;
        txtTransactionDate.Enabled = false;
        txtFullName.Enabled = false;
        ddlSourceStatus.Enabled = false;
    }

    protected void load_info(object sender, EventArgs e)
    {
        txtPhoneNumber.BorderColor = System.Drawing.Color.Empty;
        BindData();
        if (txtFullName.Text == string.Empty)
        {
            lblError.Text = Resources.labels.PhoneNumber + " incorrect";
            txtPhoneNumber.BorderColor = System.Drawing.Color.Red;
            txtPhoneNumber.Focus();
        }
    }
    void setPara(Dictionary<object, object> info)
    {

        info.Add("PHONE", txtPhoneNumber.Text);
        info.Add("CURRENTSTATUS", ddlSourceStatus.SelectedValue);
        info.Add("RETURNSTATUS", ddlDestinationStatus.SelectedValue);
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
                lblError.Text = Resources.labels.PhoneNumber + " is not null";
                txtPhoneNumber.BorderColor = System.Drawing.Color.Red;
                txtPhoneNumber.Focus();
                return;
            }
            if (txtFullName.Text.Equals(string.Empty))
            {
                lblError.Text = Resources.labels.fullname + " is not null";
                txtFullName.BorderColor = System.Drawing.Color.Red;
                txtFullName.Focus();
                return;
            }
            #endregion
            DataSet ds = new DataSet();
            Dictionary<object, object> info = new Dictionary<object, object>();
            setPara(info);
            ds = _service.CallStore("SEMS_BO_CHANGESTTMER", info, "Change Status Contract of Agent", "N", ref IPCERRORCODE, ref IPCERRORDESC);
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
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=129"));
    }
    void setControlDefault()
    {
        txtTransactionNumber.Text = "";
        txtTransactionDate.Text = "";
        txtFullName.Text = "";
        lbProductCode.Text = string.Empty;
        lbProductName.Text = string.Empty;
        lbContractCode.Text = string.Empty;
        lbCurrency.Text = string.Empty;
        loadDropDownList();

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtPhoneNumber.Text = "";
        setControlDefault();
    }

  
   
}