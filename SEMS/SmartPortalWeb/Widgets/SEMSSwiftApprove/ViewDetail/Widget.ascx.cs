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

using System.Text;

public partial class Widgets_SEMSSwiftApprove_ViewDetail_Widget : WidgetBase
{
    string ACTION = "";
    string cn = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    DataSet ApproveContractTable = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                #region Load Authen Type
                DataTable tblAuthen = new SmartPortal.IB.Transactions().LoadAuthenType(Session["userID"].ToString());
                ddlAuthenType.DataSource = tblAuthen;
                ddlAuthenType.DataTextField = "TYPENAME";
                ddlAuthenType.DataValueField = "AUTHENTYPE";
                ddlAuthenType.DataBind();
                #endregion

                //BindData();
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSContractList_ViewDetail_Widget", "Page_Load", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }

    void BindData()
    {
    //    #region Lấy thông tin giao dịch
    //    try
    //    {
    //        DataSet ds = new DataSet();
    //        ds = new SmartPortal.SEMS.Transactions().GetTranByTranID(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["tranid"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);

    //        if (IPCERRORCODE != "0")
    //        {
    //            goto ERROR;
    //        }

    //        DataTable dt = new DataTable();
    //        dt = ds.Tables[0];
    //        if (dt.Rows.Count != 0)
    //        {

    //            lblTransID.Text = dt.Rows[0]["IPCTRANSID"].ToString();
    //            lblAmount.Text = dt.Rows[0]["NUM01"].ToString();
    //            //lbcontracttype.Text = contractTable.Rows[0]["CONTRACTTYPE"].ToString();
    //            lblDesc.Text = dt.Rows[0]["TRANDESC"].ToString();
        //            lblDate.Text = SmartPortal.Common.Utilities.Utility.IsDateTime2(dt.Rows[0]["IPCTRANSDATE"].ToString()).ToString("dd/MM/yyyy HH:mm");

    //            switch (dt.Rows[0]["STATUS"].ToString().Trim())
    //            {
    //                case SmartPortal.Constant.IPC.TRANSTATUS.BEGIN:
    //                    lblStatus.Text = "Chưa xử lý";
    //                    break;
    //                case SmartPortal.Constant.IPC.TRANSTATUS.FINISH:
    //                    lblStatus.Text = "Xử lý";
    //                    switch (dt.Rows[0]["APPRSTS"].ToString().Trim())
    //                    {
    //                        case SmartPortal.Constant.IPC.APPROVESTATUS.APPROVED:
    //                            lblResult.Text = "Duyệt";
    //                            break;
    //                        case SmartPortal.Constant.IPC.APPROVESTATUS.REJECTED:
    //                            lblResult.Text = "Chưa duyệt";
    //                            break;
    //                        case SmartPortal.Constant.IPC.APPROVESTATUS.WAITTING:
    //                            lblResult.Text = "Chờ xử lý";
    //                            break;
    //                        case SmartPortal.Constant.IPC.APPROVESTATUS.BEGIN:
    //                            lblResult.Text = "Chưa xử lý";
    //                            break;

    //                    }
    //                    break;
    //                case SmartPortal.Constant.IPC.TRANSTATUS.WAITING_APPROVE:
    //                    lblStatus.Text = "Chờ duyệt";
    //                    switch (dt.Rows[0]["APPRSTS"].ToString().Trim())
    //                    {
    //                        case SmartPortal.Constant.IPC.APPROVESTATUS.APPROVED:
    //                            lblResult.Text = "Duyệt";
    //                            break;
    //                        case SmartPortal.Constant.IPC.APPROVESTATUS.REJECTED:
    //                            lblResult.Text = "Không duyệt";
    //                            break;
    //                        case SmartPortal.Constant.IPC.APPROVESTATUS.WAITTING:
    //                            lblResult.Text = "Chờ xử lý";
    //                            break;
    //                        case SmartPortal.Constant.IPC.APPROVESTATUS.BEGIN:
    //                            lblResult.Text = "Chưa xử lý";
    //                            break;

    //                    }
    //                    break;
    //                case SmartPortal.Constant.IPC.TRANSTATUS.ERROR:
    //                    lblStatus.Text = "Lỗi";
    //                    break;

    //            }

    //        }
    //        goto EXIT;
    //    }
    //    catch (Exception ex)
    //    {
    //        SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSContractList_ViewDetail_Widget", "BindData", ex.ToString(), Request.Url.Query);
    //        SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

    //    }
    //ERROR:
    //    SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSContractList_ViewDetail_Widget", "BindData", IPCERRORDESC, Request.Url.Query);
    //    SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
    //EXIT: ;
       
    //    #endregion

       
    }    

    protected void btApprove_Click(object sender, EventArgs e)
    {
        try 
        {
            //ApproveContractTable = new SmartPortal.SEMS.Contract().ApproveContract(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString(), SmartPortal.Constant.IPC.ACTIVE, ref IPCERRORCODE, ref IPCERRORDESC);
            //SendInfoLogin();
            //Response.Redirect("~/Default.aspx?p=148");
         }
        
        catch(Exception ex)
        {}
    }
    protected void btReject_Click1(object sender, EventArgs e)
    {
        try
        {
            //ApproveContractTable = new SmartPortal.SEMS.Contract().ApproveContract(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString(), SmartPortal.Constant.IPC.BLOCK, ref IPCERRORCODE, ref IPCERRORDESC);
            //Response.Redirect("~/Default.aspx?p=148");
        }

        catch (Exception ex)
        { }
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=212"));
    }
}
