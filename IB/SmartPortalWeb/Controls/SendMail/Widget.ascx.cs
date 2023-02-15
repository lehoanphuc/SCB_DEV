using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.Common.Utilities;

public partial class Controls_SendMail_Widget : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtCaptcha.Text.Trim() == Session["randomstruserreg"].ToString())
            {
                bool flag;
                string content="";

                if(Session["url"]!=null)
                {
                    content=Utility.KillSqlInjection(txtContent.Text)+"<br/>"+Session["url"].ToString();
                }
                else
                {
                    content=Utility.KillSqlInjection(txtContent.Text);
                }
                SmartPortal.Common.EmailHelper.SendMailMessageAsync(Utility.KillSqlInjection(txtFrom.Text), Utility.KillSqlInjection(txtTo.Text), Utility.KillSqlInjection(txtTitle.Text),content );

                //reset textbox
                txtFrom.Text = "";
                txtTo.Text = "";
                txtTitle.Text = "";
                txtContent.Text = "";
                txtCaptcha.Text = "";
                lblAlert.Text = Resources.labels.sendsuccessful;
            }
            else
            {
                lblAlert.Text = Resources.labels.sercuritycodeinvalid;
            }

        }
        catch (Exception ex)
        {
            lblAlert.Text = Resources.labels.sendfail;

            //Write Log
            SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["MAIL"], System.Configuration.ConfigurationManager.AppSettings["SEND"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, "", "", "", "");

        }
    }
}
