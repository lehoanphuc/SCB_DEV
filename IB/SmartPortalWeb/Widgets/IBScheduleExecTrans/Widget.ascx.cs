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

using SmartPortal.Common;
using SmartPortal.Common.Utilities;
using SmartPortal.ExceptionCollection;

public partial class Widgets_IBScheduleExecTrans_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";    

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                pnDayPilot.Visible = false;
                DayPilotCalendar.Visible = false;
                //load trancode cho ddlTransferType
                ddlTransferType.DataSource = new SmartPortal.IB.Schedule().LoadTransferType(Utility.KillSqlInjection(SmartPortal.Constant.IPC.ISSCHEDULE), Utility.KillSqlInjection(SmartPortal.Constant.IPC.YES), ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE != "0")
                {
                    throw new IPCException(IPCERRORDESC);
                }
                ddlTransferType.DataTextField = "PAGENAME";
                ddlTransferType.DataValueField = "TRANCODE";
                ddlTransferType.DataBind();
                ddlTransferType.Items.Insert(0, new ListItem(Resources.labels.tatca, ""));
            }
            
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.Message, this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }

    protected void btn_Search_Click(object sender, EventArgs e)
    {
        pnDayPilot.Visible = true;
        DayPilotCalendar.Visible = true;
        DayPilotCalendar.StartDate = SmartPortal.Common.Utilities.Utility.IsDateTime1(txtFromW.Text.ToString().Trim());
        int days = ((SmartPortal.Common.Utilities.Utility.IsDateTime1(txtToW.Text.ToString().Trim())).Subtract(SmartPortal.Common.Utilities.Utility.IsDateTime1(txtFromW.Text.ToString().Trim()))).Days;
        DayPilotCalendar.Days = days;

        try
        {
            DataSet dsSchedule = new SmartPortal.IB.Schedule().GetSchedule("", SmartPortal.Common.Utilities.Utility.KillSqlInjection(Session["userID"].ToString()),"", "", txtFromW.Text.ToString().Trim(), txtToW.Text.ToString().Trim(), ddlTransferType.SelectedValue.ToString(), ref IPCERRORCODE, ref IPCERRORCODE);
            if (IPCERRORCODE == "0")
            {
                if (dsSchedule.Tables.Count > 0)
                {
                    DataTable dtSchedule = dsSchedule.Tables[0];
                    dtSchedule.Columns.Add("CONTENT", typeof(string));
                    dtSchedule.Columns.Add("START", typeof(DateTime));
                    dtSchedule.Columns.Add("END", typeof(DateTime));
                   // for (int r = 0; r <= dtSchedule.Rows.Count - 1; r++)
                   //{
                    foreach (DataRow dr in dtSchedule.Rows)
                    {
                        switch(dr["SCHEDULETYPE"].ToString())
                        {
                            case SmartPortal.Constant.IPC.DAILY:
                                dr["SCHEDULETYPE"] = Resources.labels.daily;
                                break;
                            case SmartPortal.Constant.IPC.WEEKLY:
                                dr["SCHEDULETYPE"] = Resources.labels.weekly;
                                break;
                            case SmartPortal.Constant.IPC.MONTHLY:
                                dr["SCHEDULETYPE"] = Resources.labels.monthly;
                                break;
                            case SmartPortal.Constant.IPC.ONETIME:
                                dr["SCHEDULETYPE"] = Resources.labels.onetime;
                                break;
                        }
                        dr["CONTENT"] = "<b>"+Resources.labels.kieulich+":</b><div style='color:BlueViolet;'>" + dr["SCHEDULETYPE"].ToString() + "</div><b>"+Resources.labels.tenlich+" :</b><a href='Default.aspx?po=3&p=265&sid=" + dr["SCHEDULEID"].ToString() + "&trcd=" + dr["IPCTRANCODE"].ToString() + "'><div style='color:green;'>" + dr["SCHEDULENAME"].ToString() + "</div></a><b>"+Resources.labels.loaichuyenkhoan+":</b><div style='color:blue;'>" + dr["IPCTRANCODEDESC"].ToString() + "</div><b>" + Resources.labels.taikhoanghino+": </b><div style='color:orange;'>" + dr["ACCTNO"] + "</div><b>"+Resources.labels.sotien+":</b> <div style='color:red;'>" + SmartPortal.Common.Utilities.Utility.FormatMoney(dr["AMOUNT"].ToString(), dr["CCYID"].ToString()) + "</div><b>"+Resources.labels.tiente+":</b><div style='color:Coral;'>" + dr["CCYID"] + "</div><br />";
                        dr["START"] = SmartPortal.Common.Utilities.Utility.IsDateTime1(dr["IPCTRANSDATE"].ToString()).ToString("MM/dd/yyyy") + " "+System.Configuration.ConfigurationManager.AppSettings["STARTDAYPILOT"];
                        dr["END"] = SmartPortal.Common.Utilities.Utility.IsDateTime1(dr["IPCTRANSDATE"].ToString()).ToString("MM/dd/yyyy") + " "+System.Configuration.ConfigurationManager.AppSettings["ENDDAYPILOT"];
                        dr["SCHEDULEID"] = dr["SCHEDULEID"].ToString() +"#"+ dr["IPCTRANCODE"].ToString();
                    }
                    DayPilotCalendar.DataStartField = "START";
                    DayPilotCalendar.DataEndField = "END";
                    DayPilotCalendar.DataTextField = "CONTENT";
                    DayPilotCalendar.DataValueField = "SCHEDULEID";
                    DayPilotCalendar.DataSource = dtSchedule;
                    DayPilotCalendar.DataBind();
                }
            }
            else
            {
                throw new IPCException(IPCERRORDESC);
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.Message, this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    protected void DayPilotCalendar_EventClick(object sender, DayPilot.Web.Ui.EventClickEventArgs e)
    {
        Response.Redirect("Default.aspx?po=3&p=265&sid=" + e.Value.Split('#')[0] + "&trcd=" + e.Value.Split('#')[1]);
    }
}
