using System;

public partial class Widgets_SEMSContractLevel_Controls_Widget : WidgetBase
{
    string ACTION = "";
    private SmartPortal.SEMS.ContractLevel _service;
    private SmartPortal.SEMS.Common _common;
    public Widgets_SEMSContractLevel_Controls_Widget()
    {
        _service = new SmartPortal.SEMS.ContractLevel();
        _common = new SmartPortal.SEMS.Common();
    }


    public string _TITLE
    {
        get { return lblTitleMerchantProfile.Text; }
        set { lblTitleMerchantProfile.Text = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = "";

            ACTION = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["a"].ToString().Trim();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }

    }
}
