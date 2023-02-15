using SmartPortal.BLL;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;
using System;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Widgets_SEMSConsumerProfile_Controls_Activity : WidgetBase
{
    public static bool isAscend = false;
    string ACTION = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    String countRow = "0";
    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ACTION = GetActionPage();
            lblError.Text = string.Empty;
            if(!IsPostBack)
            {
                switch (ACTION)
                {
                    case IPC.ACTIONPAGE.ADD:
                        break;
                    default:
                        BindDataActivity();
                        break;
                }
            }
            
            GridViewPaging.pagingClickArgs += new EventHandler(btnSearch_Click1);
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
    }

    void BindDataActivity()
    {
        try
        {
            DataSet ds = new DataSet();
            string ID = GetParamsPage(IPC.ID)[0].Trim();
            object[] searchObject = new object[] { Utility.KillSqlInjection(ID), GridViewPaging.pageIndex * GridViewPaging.pageSize, GridViewPaging.pageSize };
            ds = _service.common("SEMS_WAL_KYCACTIVITY", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt.Clear();
                    dt.Columns.Add("UserID");
                    dt.Columns.Add("Daydata");
                    dt.Columns.Add("Action");
                    dt.Columns.Add("username");
                    foreach ( DataRow row in ds.Tables[0].Rows )
                    {
                        if (!row["UserCreated"].ToString().Equals(""))
                        {
                            object[] o = { row["UserIDCreate"].ToString(), row["datecrea"].ToString(), "Created", row["UserCreated"].ToString() };
                            dt.Rows.Add(o);
                        }
                        if (!row["UserModified"].ToString().Equals(""))
                        {
                            object[] o = { row["UserIDModify"].ToString(), row["lastmo"].ToString(), "Update Information", row["UserModified"].ToString() };
                            dt.Rows.Add(o);
                        }
                        if (!row["UserApproved"].ToString().Equals(""))
                        {
                            object[] o = { row["UserIDApprove"].ToString(), row["dateapp"].ToString(), "Approved Information" , row["UserApproved"].ToString() };
                            dt.Rows.Add(o);
                        }
                    }
                    countRow = dt.Rows.Count == 0 ? "0" : dt.Rows.Count.ToString();
                    rptData.DataSource = dt;
                    rptData.DataBind();
                }
            }
            GridViewPaging.total = countRow;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
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
    protected void rptData_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

        string commandArg = e.CommandArgument.ToString();
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=176&a=DETAILS&ID=" + commandArg + "&returnUrl=" + SmartPortal.Common.Encrypt.EncryptData(HttpContext.Current.Request.Url.PathAndQuery)));
       
       
    }
    protected void btnSearch_Click1(object sender, EventArgs e)
    {
        BindDataActivity();
    }
}