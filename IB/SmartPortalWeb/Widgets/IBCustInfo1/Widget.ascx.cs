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

public partial class Widgets_IBCustInfo1_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadInfoCutomer();
        }
    }
    #region Load Info Customer
    private void LoadInfoCutomer()
    {
        try
        {
            Customer ct = new Customer();
            string ErrorCode = string.Empty;
            string ErrorDesc = string.Empty;
            DataSet ds = new DataSet();
            //thaity modify
            SmartPortal.Common.Log.WriteLogFile("IB_GETCUSTINFO", Session["userID"].ToString(), "", "");
            ds = ct.GetInfo("IB_GETCUSTINFO", new object[] { Session["userID"].ToString() }, ref ErrorCode, ref ErrorDesc);
            Show(ds);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void Show(DataSet ds)
    {
        try
        {
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lblAccountName.Text = ds.Tables[0].Rows[0]["FULLNAME"].ToString();
                lblAddress.Text = ds.Tables[0].Rows[0]["ADDRESS"].ToString();
                if (ds.Tables[0].Rows[0]["GENDER"].ToString().Trim() == "M")
                {
                    lblGender.Text = Resources.labels.nam;

                }
                else
                {
                    lblGender.Text = Resources.labels.nu;
                }

                lblPhone.Text = ds.Tables[0].Rows[0]["PHONE"].ToString();
                lblEmail.Text = ds.Tables[0].Rows[0]["EMAIL"].ToString();
                //HaiNT Edit birthDate (12/08/2013)
                string birthDate = SmartPortal.Common.Utilities.Utility.FormatDatetime(ds.Tables[0].Rows[0]["BIRTHDAY"].ToString(), "dd/MM/yyyy");
                lblBirthday.Text = birthDate.Equals("01/01/1900") ? "Unkown" : birthDate;
                lblUserType.Text = ds.Tables[0].Rows[0]["TYPENAME"].ToString();
                //lblCIF.Text = Session["userID"].ToString();
                string custtype = ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.CUSTTYPE].ToString();
                switch (custtype)
                {
                    case "O":
                        lblCreateDay.Text = Resources.labels.ngaythanhlap;
                        break;
                    case "P":
                        lblCreateDay.Text = Resources.labels.birthday;
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    #endregion
}
