using System;
using System.Data;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;

public partial class Widgets_SEMSKYCDefinition_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;

            btView.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.DETAILS);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    public bool CheckNumeric(string input)
    {
        int outPut;
        if (int.TryParse(input, out outPut))
            return true;

        else
            return false;
    }
    protected void btView_click(object sender, EventArgs e)
    {
        if (!CheckPermitPageAction(IPC.ACTIONPAGE.DETAILS)) return;

        if (txtSearch.Text.Equals(string.Empty))
        {
            lblError.Text = Resources.labels.Transid + " cannot be null";
            txtSearch.BorderColor = System.Drawing.Color.Red;
            txtSearch.Focus();
            return;
        }
        if (!Utility.CheckSpecialCharacters(txtSearch.Text))
        {
            lblError.Text = Resources.labels.Transid + Resources.labels.ErrorSpeacialCharacters;
            txtSearch.BorderColor = System.Drawing.Color.Red;
            txtSearch.Focus();
            return;
        }


        if (!CheckNumeric(txtSearch.Text.Trim()))
        {
            lblError.Text = "Transaction ID must be number";
            txtSearch.BorderColor = System.Drawing.Color.Red;
            txtSearch.Focus();
            return;
        }
        txtSearch.Text = txtSearch.Text.Trim();
        DataTable dtTran = new DataTable();
        DataSet dsTran = new SmartPortal.SEMS.Transactions().GetTranByTranID(SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtSearch.Text.Trim()), ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE == "0")
        {
            if (dsTran.Tables[0].Rows.Count > 0)
            {
                string commandName = IPC.ACTIONPAGE.DETAILS;
                string commandArg = Utility.KillSqlInjection(txtSearch.Text.Trim());
                if (CheckPermitPageAction(commandName))
                {
                    switch (commandName)
                    {
                        case IPC.ACTIONPAGE.DETAILS:
                            RedirectToActionPage(IPC.ACTIONPAGE.DETAILS, "&" + SmartPortal.Constant.IPC.ID + "=" + commandArg);
                            break;
                    }
                }
            }
            else
            {
                lblError.Text = "<p class='divDataNotFound'>" + "Cannot find transaction" + "</p>";
            }
        }
        else
        {
            lblError.Text = IPCERRORDESC;
        }

    }

}
