using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SmartPortal.ExceptionCollection;
using SmartPortal.SEMS;

public partial class Widgets_SEMSFDProduct_Controls_Widget : System.Web.UI.UserControl
{
    string ACTION = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    public string FDProductHeader
    {
        get
        {
            return lblFDProductHeader.Text;
        }

        set
        {
            lblFDProductHeader.Text = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {      
        lblError.Text = "";      
    }
    protected void btback_Click(object sender, EventArgs e)
    {
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=378"));
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {
            string FDProductID = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtFDProductID.Text.Trim());
            string FDProductName = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtFDProductName.Text.Trim());
            string Term = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtTerm.Text.Trim());
            string InterestRate = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtInterestRate.Text.Trim());
            string IsClose ;
            string Note = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtNote.Text.Trim());
            string Desc = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtDesc.Text.Trim());
            if (cbIsClose.Checked)
                IsClose = "Y";
            else
                IsClose = "N";

            SmartPortal.SEMS.FDProductOnline objFDProduct = new FDProductOnline();
            objFDProduct.InsertFDProductOnline(FDProductID, FDProductName, Term, InterestRate, IsClose, Note, Desc, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE != "0")
            {
                throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
            }
            else
            {
                lblError.Text = Resources.labels.themmoifdproductthanhcong;
                btsave.Enabled = false;
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }

   
     
}
