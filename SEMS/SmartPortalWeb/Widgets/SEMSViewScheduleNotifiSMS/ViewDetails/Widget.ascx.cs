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
using SmartPortal.Common;
using SmartPortal.Common.Utilities;
using SmartPortal.ExceptionCollection;

public partial class Widgets_SEMSViewScheduleNotifiSMS_ViewDetails_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_IBSTV_ViewDetails_Widget", "Page_Load", IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_IBSTV_ViewDetails_Widget", "Page_Load", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        } 
    }
    void BindData()
    {

        #region Lấy thông tin lịch
        DataSet ScheduleDS = new DataSet();
        DataTable ScheduleTB = new DataTable();
        DataTable ScheduleDetailTB = new DataTable();
        DataTable ScheduleDayTB = new DataTable();
        string SID = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["sid"].ToString();
        SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();
        ScheduleDS = new SmartPortal.IB.Schedule().GetInfo_Schedule_ByID(SID, ref IPCERRORCODE, ref IPCERRORDESC);
        if (ScheduleDS.Tables[0].Rows.Count != 0)
        {
            ScheduleTB = ScheduleDS.Tables[0];
            lblScheduleType.Text = ScheduleTB.Rows[0]["PAGENAME"].ToString();
            lbschedulename.Text = ScheduleTB.Rows[0]["SCHEDULENAME"].ToString();
            lblTransferType.Text = ScheduleTB.Rows[0]["SCHEDULETYPE"].ToString();
            lbfromdate.Text = SmartPortal.Common.Utilities.Utility.FormatDatetime(ScheduleTB.Rows[0]["CREATEDATE"].ToString(), "dd/MM/yyyy");
            lbtodate.Text = SmartPortal.Common.Utilities.Utility.FormatDatetime(ScheduleTB.Rows[0]["ENDDATE"].ToString(), "dd/MM/yyyy");
            lbtime.Text = SmartPortal.Common.Utilities.Utility.FormatDatetime(ScheduleTB.Rows[0]["SCHEDULETIME"].ToString(), "dd/MM/yyyy HH:mm:ss");

            if (ScheduleDS.Tables[1].Rows.Count != 0)
            {
                ScheduleDetailTB = ScheduleDS.Tables[1];
                for (int i = 0; i < ScheduleDetailTB.Rows.Count; i++)
                {
                    string paravalue = ScheduleDetailTB.Rows[i]["PARAVALUE"].ToString();
                    switch (ScheduleDetailTB.Rows[i]["PARANAME"].ToString())
                    {
                        case SmartPortal.Constant.IPC.MSGCONTENT:
                            lblContent.Text = paravalue;
                            break;
                        case SmartPortal.Constant.IPC.PHONENOLIST:
                            lblListPhone.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(paravalue,"");
                            break;
                        case SmartPortal.Constant.IPC.SENDTYPE:
                            switch (paravalue)
                            {
                                case "1":
                                    lblOptionSend.Text = Resources.labels.guitrongdanhsachdangkysms;
                                    break;
                                case "2":
                                    lblOptionSend.Text = Resources.labels.guitrongdanhsachdienthoaibosung;
                                    break;
                                case "3":
                                    lblOptionSend.Text = Resources.labels.guitrongdanhsachdangkysmsvadanhsachdienthoaibosung;
                                    break;
                            }
                            break;
                    }
                }
            }
            if (ScheduleDS.Tables[2].Rows.Count != 0)
            {
                string StrDayno = "";
                ScheduleDayTB = ScheduleDS.Tables[2];
                for (int i = 0; i < ScheduleDayTB.Rows.Count; i++)
                {
                    if (ScheduleDayTB.Rows[i]["DAYNO"].ToString() == "1" && lblScheduleType.Text == SmartPortal.Constant.IPC.WEEKLY)
                    {
                        StrDayno += "Chủ nhật , ";
                        continue;
                    }
                    StrDayno += ScheduleDayTB.Rows[i]["DAYNO"].ToString() + " , ";
                }
                lbdatetransfer.Text = StrDayno.Substring(0, StrDayno.Length - 2);
            }
        }


        #endregion
    }
}
