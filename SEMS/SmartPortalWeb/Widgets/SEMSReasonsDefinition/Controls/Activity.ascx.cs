using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Widgets_SEMSConsumerProfile_Controls_Activity : WidgetBase
{
    public static bool isAscend = false;
    string ACTION = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();
    protected void Page_Load(object sender, EventArgs e)
    {
        ACTION = GetActionPage();
        if (ACTION == IPC.ACTIONPAGE.ADD)
        {
            BigPnAcitivity.Disabled = true;
        }
        else
        {
            BindData();
            GridViewPaging.pagingClickArgs += new EventHandler(btnSearch_Click);
        }

    }
    void BindData()
    {
        try
        {
            rptData.DataSource = null;
            DataSet ds = new DataSet();
            string ID = GetParamsPage(IPC.ID)[0].Trim();
            object[] searchObject = new object[] { Utility.KillSqlInjection(ID), GridViewPaging.pageIndex * GridViewPaging.pageSize, GridViewPaging.pageSize };
            ds = _service.common("SEMS_REASON_ACTIVITY", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    GridViewPaging.total = ds.Tables[0].Rows.Count == 0 ? "0" : ds.Tables[0].Rows[0][IPC.TRECORDCOUNT].ToString();
                    rptData.DataSource = ds;
                    rptData.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {

    }
    protected void rptData_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string commandName = e.CommandName;
        string commandArg = e.CommandArgument.ToString();
        switch (commandName)
        {
            case IPC.ACTIONPAGE.DETAILS:
                Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=176&a=DETAILS&ID=" + commandArg+ "&returnUrl=" + SmartPortal.Common.Encrypt.EncryptData(System.Web.HttpContext.Current.Request.Url.PathAndQuery)));
                break;
        }
    }

    protected override void Render(HtmlTextWriter writer)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<div class=\"content \" >");
        sb.Append("<div>");
        writer.Write(sb.ToString());
        base.Render(writer);
        writer.Write("</div>");
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }
}