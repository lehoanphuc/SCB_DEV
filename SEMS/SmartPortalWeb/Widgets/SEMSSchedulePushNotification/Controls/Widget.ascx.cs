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
using System.Globalization;
using SmartPortal.BLL;
using SmartPortal.Common;
using SmartPortal.Common.Utilities;
using SmartPortal.ExceptionCollection;
using SmartPortal.Constant;
using SmartPortal.Model;
using SmartPortal.SEMS;
using System.IO;
using OfficeOpenXml;
using System.Linq;

public partial class Widgets_SEMSSchedulePushNotification_Widget : WidgetBase
{
    private string IPCERRORCODE = string.Empty;
    string IPCERRORDESC = string.Empty;
    public static string fromdate = string.Empty;
    public static string todate = string.Empty;
    public static string ccyid = string.Empty;
    public static string description = string.Empty;
    public static string type = string.Empty;
    private string ACTION = string.Empty;
    private static string SchID = string.Empty;
    string temp;
    public string _TITLE
    {
        get { return lblHeader.Text; }
        set { lblHeader.Text = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            lblError.Text = "";
            ACTION = GetActionPage();
            if (!IsPostBack)
            {
                LoadDll();
                switch (ACTION)
                {
                    case IPC.ACTIONPAGE.DETAILS:
                        pnCFInfoLich.Visible = true;
                        pnConfirm.Visible = true;
                        pnConfirmWeekly.Visible = false;
                        pnConfirmOnetime.Visible = false;
                        pnConfirmMonthly.Visible = false;
                        pnConfirmDaily.Visible = false;
                        BindData();
                        pnCFketqua.Visible = false;
                        pnSchedule.Visible = false;
                        pnTIB.Visible = false;
                        pnOnetime.Visible = false;
                        pnMonthly.Visible = false;
                        pnDaily.Visible = false;
                        pnWeekly.Visible = false;
                        btnSave.Visible = false;
                        cblcfW.Enabled = false;
                        cblcfM.Enabled = false;
                        break;
                    case IPC.ACTIONPAGE.EDIT:
                        pnCFInfoLich.Visible = false;
                        radSchedule.Enabled = false;
                        pnConfirm.Visible = false;
                        pnCFketqua.Visible = false;
                        pnSchedule.Visible = false;
                        pnOnetime.Visible = false;
                        pnMonthly.Visible = false;
                        pnDaily.Visible = false;
                        pnConfirmWeekly.Visible = false;
                        pnConfirmOnetime.Visible = false;
                        pnConfirmMonthly.Visible = false;
                        pnConfirmDaily.Visible = false;
                        pnWeekly.Visible = false;
                        btnTIBNext.Text = Resources.labels.save;
                        pnTIB.Visible = true;
                        trPNID.Visible = false;
                        BindData();
                        break;
                    default:
                        break;
                }
            }
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
    public void LoadDll()
    {
        ddlSendType.Items.Add(new ListItem(Resources.labels.all, IPC.ALL));
        ddlSendType.Items.Add(new ListItem(Resources.labels.Consumer, IPC.CONSUMER));
        ddlSendType.Items.Add(new ListItem(Resources.labels.agentmerchant, IPC.AGENTMERCHANT));
        ddlSendType.Items.Add(new ListItem(Resources.labels.user, IPC.USER));


        radSchedule.Items.Add(new ListItem(Resources.labels.daily, IPC.DAILY));
        radSchedule.Items.Add(new ListItem(Resources.labels.weekly, IPC.WEEKLY));
        radSchedule.Items.Add(new ListItem(Resources.labels.monthly, IPC.MONTHLY));
        radSchedule.Items.Add(new ListItem(Resources.labels.onetime, IPC.ONETIME));
        radSchedule.SelectedIndex = 0;

        DataSet dsProIndi = new Product().GetProductByCondition("", "", "P", "", ref IPCERRORCODE, ref IPCERRORDESC);
        DataSet dsProCorp = new Product().GetProductByCondition("", "", IPC.ONETIME, "", ref IPCERRORCODE, ref IPCERRORDESC);

        DataTable dtProduct = dsProIndi.Tables[0];
        DataTable dtProCorp = dsProCorp.Tables[0];

        dtProduct.Merge(dtProCorp);

        //hide panel
        pnSchedule.Visible = true;
        pnDaily.Visible = false;
        pnMonthly.Visible = false;
        pnWeekly.Visible = false;
        pnOnetime.Visible = false;
        pnTIB.Visible = false;
        pnConfirm.Visible = false;
        pnCFInfoLich.Visible = false;
        pnCFketqua.Visible = false;

        //load day
        for (int i = 1; i < 32; i++)
        {
            ListItem li = new ListItem(i.ToString(), i.ToString());
            cblThuM.Items.Add(li);
            cblcfM.Items.Add(li);
        }

        //set hour
        for (int j = 0; j < 24; j++)
        {
            ListItem li = new ListItem(j.ToString(), j.ToString());
            ddlHour.Items.Add(li);
            ddlhourD.Items.Add(li);
            ddlhourW.Items.Add(li);
            ddlhourM.Items.Add(li);

        }
        //set minute
        for (int k = 0; k < 56; k += 5)
        {
            ListItem li = new ListItem(k.ToString(), k.ToString());
            ddlMinute.Items.Add(li);
            ddlminuteD.Items.Add(li);
            ddlminuteW.Items.Add(li);
            ddlminuteM.Items.Add(li);
        }
    }
    public void BindData()
    {
        try
        {
            string ID = GetParamsPage(IPC.ID)[0].Trim();
            DataSet ds = new DataSet();
            ds = new Notification().GetPushNotificationByID(ID, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE.Equals("0"))
            {
                DataTable dt = ds.Tables[0];
                SchID = dt.Rows[0][IPC.SCHEDULEID].ToString();

                lblCFTenLich.Text = dt.Rows[0][IPC.NAME].ToString();
                lblCFLoaiThongBao.Text = dt.Rows[0][IPC.PAGENAME].ToString();
                lblTitle.Text = dt.Rows[0][IPC.TITLE].ToString();
                lblBodyContent.Text = dt.Rows[0][IPC.BODY].ToString();
                lblCFContent.Text = dt.Rows[0][IPC.DETAILS].ToString();
                switch (dt.Rows[0][IPC.SENDTYPE].ToString())
                {
                    case IPC.ALL:
                        lblCFSendType.Text = Resources.labels.all;
                        break;
                    case IPC.CONSUMER:
                        lblCFSendType.Text = Resources.labels.Consumer;
                        break;
                    case IPC.AGENTMERCHANT:
                        lblCFSendType.Text = Resources.labels.agentmerchant;
                        break;
                    case IPC.USER:
                        lblCFSendType.Text = Resources.labels.user;
                        break;
                }
                txtTitle.Text = dt.Rows[0][IPC.TITLE].ToString();
                txtBody.Text = dt.Rows[0][IPC.BODY].ToString();
                txtContent.Text = dt.Rows[0][IPC.DETAILS].ToString();
                ddlSendType.SelectedValue = dt.Rows[0][IPC.SENDTYPE].ToString();
                txtPNID.Text = ID;
                txtPNID.Enabled = false;
                lblSendOption.Text = dt.Rows[0][IPC.VALUE].ToString();
                pnSchedule.Visible = true;
                Button7.Visible = false;
                btnBack.Visible = false;
                txtScheduleName.Text = dt.Rows[0][IPC.NAME].ToString();
                ddlTransferType.SelectedValue = dt.Rows[0][IPC.TYPENOTIFY].ToString();
                radSchedule.SelectedValue = dt.Rows[0][IPC.TYPE].ToString();
                hdScheduleType.Value = dt.Rows[0][IPC.TYPE].ToString();
                switch (dt.Rows[0][IPC.TYPE].ToString())
                {
                    case IPC.DAILY:
                        pnDaily.Enabled = false;
                        pnDaily.Visible = true;
                        Button8.Visible = Button12.Visible = false;
                        pnConfirmDaily.Visible = true;
                        lbScheduleName.Text = Resources.labels.daily;
                        lbfromcfD.Text = Utility.FormatDatetime(dt.Rows[0][IPC.FROMDATE].ToString(), "dd/MM/yyyy");
                        txtFromD.Text = Utility.FormatDatetime(dt.Rows[0][IPC.FROMDATE].ToString(), "dd/MM/yyyy");
                        lbtocfD.Text = Utility.FormatDatetime(dt.Rows[0][IPC.TODATE].ToString(), "dd/MM/yyyy");
                        txtToD.Text = Utility.FormatDatetime(dt.Rows[0][IPC.TODATE].ToString(), "dd/MM/yyyy");
                        lbcftimeD.Text = Utility.FormatDatetime(dt.Rows[0][IPC.SCHEDULETIME].ToString(), "HH:mm:ss");
                        ddlhourD.SelectedValue = int.Parse(Utility.FormatDatetime(dt.Rows[0][IPC.SCHEDULETIME].ToString(), "HH:mm:ss").Split(':')[0]).ToString();
                        ddlminuteD.SelectedValue = int.Parse(Utility.FormatDatetime(dt.Rows[0][IPC.SCHEDULETIME].ToString(), "HH:mm:ss").Split(':')[1]).ToString();

                        break;
                    case IPC.WEEKLY:
                        pnWeekly.Enabled = false;
                        pnWeekly.Visible = true;
                        pnConfirmWeekly.Visible = true;
                        Button9.Visible = Button13.Visible = false;
                        lbScheduleName.Text = Resources.labels.weekly;
                        lbfromcfW.Text = Utility.FormatDatetime(dt.Rows[0][IPC.FROMDATE].ToString(), "dd/MM/yyyy");
                        lbtocfW.Text = Utility.FormatDatetime(dt.Rows[0][IPC.TODATE].ToString(), "dd/MM/yyyy");
                        lbfctimeW.Text = Utility.FormatDatetime(dt.Rows[0][IPC.SCHEDULETIME].ToString(), "HH:mm:ss");
                        txtFromW.Text = Utility.FormatDatetime(dt.Rows[0][IPC.FROMDATE].ToString(), "dd/MM/yyyy");
                        txtToW.Text = Utility.FormatDatetime(dt.Rows[0][IPC.TODATE].ToString(), "dd/MM/yyyy");
                        ddlhourW.SelectedValue = int.Parse(Utility.FormatDatetime(dt.Rows[0][IPC.SCHEDULETIME].ToString(), "HH:mm:ss").Split(':')[0]).ToString();
                        ddlminuteW.SelectedValue = int.Parse(Utility.FormatDatetime(dt.Rows[0][IPC.SCHEDULETIME].ToString(), "HH:mm:ss").Split(':')[1]).ToString();

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            int index = int.Parse(dt.Rows[i][IPC.DAYNO].ToString()) - 1;
                            cblcfW.Items[index].Selected = true;
                            cblThu.Items[index].Selected = true;
                        }
                        break;
                    case IPC.MONTHLY:
                        pnMonthly.Enabled = false;
                        pnMonthly.Visible = true;
                        pnConfirmMonthly.Visible = true;
                        Button10.Visible = Button14.Visible = false;
                        lbScheduleName.Text = Resources.labels.monthly;
                        lbfromcfM.Text = Utility.FormatDatetime(dt.Rows[0][IPC.FROMDATE].ToString(), "dd/MM/yyyy");
                        lbtocfM.Text = Utility.FormatDatetime(dt.Rows[0][IPC.TODATE].ToString(), "dd/MM/yyyy");
                        lbcftimeM.Text = Utility.FormatDatetime(dt.Rows[0][IPC.SCHEDULETIME].ToString(), "HH:mm:ss");
                        txtFromM.Text = Utility.FormatDatetime(dt.Rows[0][IPC.FROMDATE].ToString(), "dd/MM/yyyy");
                        txtToM.Text = Utility.FormatDatetime(dt.Rows[0][IPC.TODATE].ToString(), "dd/MM/yyyy");
                        ddlhourM.SelectedValue = int.Parse(Utility.FormatDatetime(dt.Rows[0][IPC.SCHEDULETIME].ToString(), "HH:mm:ss").Split(':')[0]).ToString();
                        ddlminuteM.SelectedValue = int.Parse(Utility.FormatDatetime(dt.Rows[0][IPC.SCHEDULETIME].ToString(), "HH:mm:ss").Split(':')[1]).ToString();

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            int index = int.Parse(dt.Rows[i][IPC.DAYNO].ToString()) - 1;
                            cblcfM.Items[index].Selected = true;
                            cblThuM.Items[index].Selected = true;
                        }
                        break;
                    case IPC.ONETIME:
                        pnOnetime.Enabled = false;
                        pnConfirmOnetime.Visible = true;
                        pnOnetime.Visible = true;
                        Button11.Visible = Button15.Visible = false;
                        lbScheduleName.Text = Resources.labels.onetime;
                        lbDateO.Text = Utility.FormatDatetime(dt.Rows[0][IPC.FROMDATE].ToString(), "dd/MM/yyyy HH:mm:ss");
                        txtDateO.Text = Utility.FormatDatetime(dt.Rows[0][IPC.FROMDATE].ToString(), "dd/MM/yyyy");
                        ddlHour.SelectedValue = int.Parse(Utility.FormatDatetime(dt.Rows[0][IPC.FROMDATE].ToString(), "HH:mm:ss").Split(':')[0]).ToString();
                        ddlMinute.SelectedValue = int.Parse(Utility.FormatDatetime(dt.Rows[0][IPC.FROMDATE].ToString(), "HH:mm:ss").Split(':')[1]).ToString();

                        break;
                }

                switch (dt.Rows[0][IPC.SENDTYPE].ToString())
                {
                    case IPC.USER:
                        trSendOptionCF.Visible = true;
                        txtUser.Text = dt.Rows[0][IPC.VALUE].ToString();
                        trSendOption.Visible = false;
                        trSend.Visible = txtUser.Visible = Label11.Visible = true;
                        divUserNoti.Visible = true;
                        Label10.Text = Resources.labels.userid;
                        break;
                    case IPC.ALL:
                        trSendOption.Visible = false;
                        trSendOptionCF.Visible = false;
                        break;
                }
            }
            else
            {

            }
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
    protected void btnTIBNext_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(txtScheduleName.Text.Trim()))
            {
                lblError.Text = Resources.labels.bannhaptencholich;
                return;
            }
            if (ddlSendType.SelectedValue.Equals(IPC.USER))
            {
                
                if (txtUser.Text.ToString().EndsWith(",")) {
                    txtUser.Text = txtUser.Text.ToString().Remove(txtUser.Text.ToString().Length-1);
                };
                string listnotexist = String.Empty;
                string userlist = txtUser.Text;
                string[] key = userlist.Split(',');
                foreach (string value in key)
                {
                    DataSet dt = new SmartPortal.SEMS.User().GETMBUSERBYPHONENO(value.Trim(), "MB", ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        if (dt.Tables[0].Rows.Count == 0 || dt == null)
                        {
                            listnotexist += value.Trim() + " ,";
                        }
                    }
                    else
                    {
                        lblError.Text = IPCERRORCODE;
                        return;
                    }
                }

                if (!string.IsNullOrEmpty(listnotexist))
                {
                    listnotexist = listnotexist.Substring(0, listnotexist.Length - 1);
                    lblError.Text = Resources.labels.phonenumberdoesnotexist + ": " + listnotexist;
                    return;
                }
            }

            if (ACTION.Equals(IPC.ACTIONPAGE.EDIT))
            {
                if (!CheckPermitPageAction(IPC.ACTIONPAGE.EDIT)) return;

                string name = Utility.KillSqlInjection(txtScheduleName.Text.Trim());
                string trancode = Utility.KillSqlInjection(ddlTransferType.SelectedValue.ToString());
                string time = string.Empty;
                string value = string.Empty;

                if (ddlSendType.SelectedValue.Equals(IPC.ALL))
                {
                    value = string.Empty;
                }
                else
                {
                    value = txtUser.Text;
                }

                //Tạo table ScheduleDay
                DataTable dtScheduleDay = new DataTable();
                DataColumn ScheduleDayIDCol = new DataColumn(IPC.SCHEDULEID);
                DataColumn DayNoCol = new DataColumn(IPC.DAYNO);
                //add col vào ScheduleDay
                dtScheduleDay.Columns.AddRange(new DataColumn[] { ScheduleDayIDCol, DayNoCol });
                switch (radSchedule.SelectedValue)
                {
                    case IPC.DAILY:
                        fromdate = txtFromD.Text;
                        todate = txtToD.Text;
                        time = fromdate + " " + (int.Parse(ddlhourD.SelectedValue.ToString()) < 10 ? "0" + ddlhourD.SelectedValue.ToString() : ddlhourD.SelectedValue.ToString()) + ":" + (int.Parse(ddlminuteD.SelectedValue.ToString()) < 10 ? "0" + ddlminuteD.SelectedValue.ToString() : ddlminuteD.SelectedValue.ToString()) + ":00";
                        break;
                    case IPC.WEEKLY:
                        for (int i = 0; i < cblThu.Items.Count; i++)
                        {
                            if (cblThu.Items[i].Selected)
                            {
                                DataRow row = dtScheduleDay.NewRow();
                                row[IPC.SCHEDULEID] = SchID;
                                row[IPC.DAYNO] = cblThu.Items[i].Value.ToString();
                                dtScheduleDay.Rows.Add(row);
                            }
                        }
                        fromdate = txtFromW.Text;
                        todate = txtToW.Text;
                        time = fromdate + " " + (int.Parse(ddlhourW.SelectedValue.ToString()) < 10 ? "0" + ddlhourW.SelectedValue.ToString() : ddlhourW.SelectedValue.ToString()) + ":" + (int.Parse(ddlminuteW.SelectedValue.ToString()) < 10 ? "0" + ddlminuteW.SelectedValue.ToString() : ddlminuteW.SelectedValue.ToString()) + ":00";
                        break;
                    case IPC.MONTHLY:
                        for (int j = 0; j < cblThuM.Items.Count; j++)
                        {
                            if (cblThuM.Items[j].Selected)
                            {
                                DataRow row1 = dtScheduleDay.NewRow();
                                row1[IPC.SCHEDULEID] = SchID;
                                row1[IPC.DAYNO] = cblThuM.Items[j].Value.ToString();
                                dtScheduleDay.Rows.Add(row1);
                            }
                        }
                        fromdate = txtFromM.Text;
                        todate = txtToM.Text;
                        time = fromdate + " " + (int.Parse(ddlhourM.SelectedValue.ToString()) < 10 ? "0" + ddlhourM.SelectedValue.ToString() : ddlhourM.SelectedValue.ToString()) + ":" + (int.Parse(ddlminuteM.SelectedValue.ToString()) < 10 ? "0" + ddlminuteM.SelectedValue.ToString() : ddlminuteM.SelectedValue.ToString()) + ":00";
                        break;
                    case IPC.ONETIME:
                        fromdate = txtDateO.Text + " " + (int.Parse(ddlHour.SelectedValue.ToString()) < 10 ? "0" + ddlHour.SelectedValue.ToString() : ddlHour.SelectedValue.ToString()) + ":" + (int.Parse(ddlMinute.SelectedValue.ToString()) < 10 ? "0" + ddlMinute.SelectedValue.ToString() : ddlMinute.SelectedValue.ToString()) + ":00";
                        todate = txtDateO.Text + " " + (int.Parse(ddlHour.SelectedValue.ToString()) < 10 ? "0" + ddlHour.SelectedValue.ToString() : ddlHour.SelectedValue.ToString()) + ":" + (int.Parse(ddlMinute.SelectedValue.ToString()) < 10 ? "0" + ddlMinute.SelectedValue.ToString() : ddlMinute.SelectedValue.ToString()) + ":00";
                        time = txtDateO.Text + " " + (int.Parse(ddlHour.SelectedValue.ToString()) < 10 ? "0" + ddlHour.SelectedValue.ToString() : ddlHour.SelectedValue.ToString()) + ":" + (int.Parse(ddlMinute.SelectedValue.ToString()) < 10 ? "0" + ddlMinute.SelectedValue.ToString() : ddlMinute.SelectedValue.ToString()) + ":00";
                        break;
                }
                type = radSchedule.SelectedValue;

                DataTable dtPN = new DataTable();
                dtPN.Columns.Add(IPC.ID, typeof(string));
                dtPN.Columns.Add(IPC.NAME, typeof(string));
                dtPN.Columns.Add(IPC.TYPE, typeof(string));
                dtPN.Columns.Add(IPC.SENDTYPE, typeof(string));
                dtPN.Columns.Add(IPC.TITLE, typeof(string));
                dtPN.Columns.Add(IPC.BODY, typeof(string));
                dtPN.Columns.Add(IPC.DETAILS, typeof(string));
                dtPN.Columns.Add(IPC.TYPENOTIFY, typeof(string));
                dtPN.Columns.Add(IPC.USERMODIFIED, typeof(string));
                dtPN.Columns.Add(IPC.DATEMODIFIED, typeof(string));
                dtPN.Columns.Add(IPC.FROMDATE, typeof(string));
                dtPN.Columns.Add(IPC.TODATE, typeof(string));
                dtPN.Columns.Add(IPC.VALUE, typeof(string));

                DataRow drPN = dtPN.NewRow();
                drPN[IPC.ID] = txtPNID.Text.ToString();
                drPN[IPC.NAME] = name;
                drPN[IPC.TYPE] = type;
                drPN[IPC.SENDTYPE] = ddlSendType.SelectedValue;
                drPN[IPC.TITLE] = Utility.KillSqlInjection(txtTitle.Text);
                drPN[IPC.BODY] = Utility.KillSqlInjection(txtBody.Text);
                drPN[IPC.DETAILS] = Utility.KillSqlInjection(txtContent.Text);
                drPN[IPC.TYPENOTIFY] = ddlTransferType.SelectedValue;
                drPN[IPC.USERMODIFIED] = Session["userName"].ToString();
                drPN[IPC.DATEMODIFIED] = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                drPN[IPC.FROMDATE] = fromdate;
                drPN[IPC.TODATE] = todate;
                drPN[IPC.VALUE] = value;

                dtPN.Rows.Add(drPN);

                DataTable dtDel = new DataTable();
                dtDel.Columns.Add(IPC.SCHEDULEID, typeof(string));
                DataRow drDel = dtDel.NewRow();
                drDel[IPC.SCHEDULEID] = SchID;
                dtDel.Rows.Add(drDel);

                DataTable dtSchedule = new DataTable();
                dtSchedule.Columns.Add(IPC.SCHEDULEID, typeof(string));
                dtSchedule.Columns.Add(IPC.SCHEDULETYPE, typeof(string));
                dtSchedule.Columns.Add(IPC.SCHEDULETIME, typeof(string));
                dtSchedule.Columns.Add(IPC.SCHEDULENAME, typeof(string));
                dtSchedule.Columns.Add(IPC.USERCREATE, typeof(string));
                dtSchedule.Columns.Add(IPC.TRANCODE, typeof(string));
                dtSchedule.Columns.Add(IPC.CREATEDATE, typeof(string));
                dtSchedule.Columns.Add(IPC.ENDDATE, typeof(string));

                DataRow drSchedule = dtSchedule.NewRow();
                drSchedule[IPC.SCHEDULEID] = SchID;
                drSchedule[IPC.SCHEDULETYPE] = type;
                drSchedule[IPC.SCHEDULETIME] = time;
                drSchedule[IPC.SCHEDULENAME] = name;
                drSchedule[IPC.USERCREATE] = Session["userName"].ToString();
                drSchedule[IPC.TRANCODE] = "SEMSPUSH";
                drSchedule[IPC.CREATEDATE] = fromdate;
                drSchedule[IPC.ENDDATE] = todate;

                dtSchedule.Rows.Add(drSchedule);

                new Notification().EditPushNotification(txtPNID.Text.ToString(),
                    dtPN, dtSchedule, dtDel, dtScheduleDay,
                    ref IPCERRORCODE, ref IPCERRORDESC
                    );

                if (IPCERRORCODE.Equals("0"))
                {
                    pnTIB.Visible = false;
                    pnCFketqua.Visible = true;
                    pnCFInfoLich.Visible = false;
                    pnDaily.Visible = false;
                    pnMonthly.Visible = false;
                    pnOnetime.Visible = false;
                    pnSchedule.Visible = false;
                    pnWeekly.Visible = false;

                    lblketquadatlich.Text = Resources.labels.suathanhcong;
                    return;
                }
                else
                {
                    lblError.Text = Resources.labels.suathatbai;
                    return;
                }
            }

            #region Vi-Invi Panel
            pnConfirm.Visible = true;
            pnTIB.Visible = false;

            lblTitle.Text = txtTitle.Text;
            lblBodyContent.Text = txtBody.Text;
            lblCFContent.Text = txtContent.Text;
            lblCFSendType.Text = ddlSendType.SelectedItem.Text;
            lblUserCF.Text = txtUser.Text;
            if (ddlSendType.SelectedValue.Equals(IPC.USER))
            {
                trSendOption.Visible = true;
            }
            else
            {
                trSendOption.Visible = false;
            }

            switch (radSchedule.SelectedValue)
            {
                case IPC.DAILY:
                    lbScheduleName.Text = Resources.labels.daily;
                    pnConfirmDaily.Visible = true;
                    pnConfirmMonthly.Visible = false;
                    pnConfirmWeekly.Visible = false;
                    pnConfirmOnetime.Visible = false;
                    break;
                case IPC.WEEKLY:
                    lbScheduleName.Text = Resources.labels.weekly;
                    pnConfirmDaily.Visible = false;
                    pnConfirmMonthly.Visible = false;
                    pnConfirmWeekly.Visible = true;
                    pnConfirmOnetime.Visible = false;
                    break;
                case IPC.MONTHLY:
                    lbScheduleName.Text = Resources.labels.monthly;
                    pnConfirmDaily.Visible = false;
                    pnConfirmMonthly.Visible = true;
                    pnConfirmWeekly.Visible = false;
                    pnConfirmOnetime.Visible = false;
                    break;
                case IPC.ONETIME:
                    lbScheduleName.Text = Resources.labels.onetime;
                    pnConfirmDaily.Visible = false;
                    pnConfirmMonthly.Visible = false;
                    pnConfirmWeekly.Visible = false;
                    pnConfirmOnetime.Visible = true;
                    break;
            }
            #endregion

        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(IPCex.ToString(), Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        if (ACTION.Equals(IPC.ACTIONPAGE.DETAILS))
        {
            RedirectBackToMainPage();
        }
        else
        {
            try
            {
                pnConfirm.Visible = false;
                pnTIB.Visible = true;
            }
            catch (Exception ex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
            }
        }
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        try
        {
            if (!CheckPermitPageAction(IPC.ACTIONPAGE.ADD)) return;
            string ScheName = Utility.KillSqlInjection(txtScheduleName.Text.Trim());
            string group = Utility.KillSqlInjection(ddlTransferType.SelectedValue.ToString());
            string details = Utility.KillSqlInjection(txtContent.Text);
            string body = Utility.KillSqlInjection(txtBody.Text);
            string title = Utility.KillSqlInjection(txtTitle.Text);
            string userCreate = Session["userName"].ToString();
            string ScheID = SmartPortal.Constant.IPC.SCHEDULEPREFIX + DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 16, 16);
            string time = "";
            DataTable dtPN = new DataTable();
            dtPN.Columns.Add("SCHEDULEID", typeof(string));
            dtPN.Columns.Add("NAME", typeof(string));
            dtPN.Columns.Add("TYPE", typeof(string));
            dtPN.Columns.Add("SENDTYPE", typeof(string));
            dtPN.Columns.Add("TITLE", typeof(string));
            dtPN.Columns.Add("BODY", typeof(string));
            dtPN.Columns.Add("DETAILS", typeof(string));
            dtPN.Columns.Add("STATUS", typeof(string));
            dtPN.Columns.Add("TYPENOTIFY", typeof(string));
            dtPN.Columns.Add("USERCREATED", typeof(string));
            dtPN.Columns.Add("USERMODIFIED", typeof(string));
            dtPN.Columns.Add("DATECREATED", typeof(string));
            dtPN.Columns.Add("DATEMODIFIED", typeof(string));
            dtPN.Columns.Add("FROMDATE", typeof(string));
            dtPN.Columns.Add("TODATE", typeof(string));
            dtPN.Columns.Add("APPROVER", typeof(string));
            dtPN.Columns.Add("VALUE", typeof(string));

            DataRow drPN = dtPN.NewRow();
            drPN["SCHEDULEID"] = ScheID;
            drPN["NAME"] = ScheName;
            drPN["SENDTYPE"] = ddlSendType.SelectedValue;
            drPN["TITLE"] = title;
            drPN["BODY"] = body;
            drPN["DETAILS"] = details;
            drPN["STATUS"] = SmartPortal.Constant.IPC.NEW;
            drPN["TYPENOTIFY"] = group;
            drPN["USERCREATED"] = userCreate;
            drPN["APPROVER"] = string.Empty;

            if (ddlSendType.SelectedValue.Equals(IPC.USER))
            {
                drPN["VALUE"] = txtUser.Text;
            }
            else
            {
                drPN["VALUE"] = string.Empty;
            }

            dtPN.Rows.Add(drPN);

            //Tạo table ScheduleDay
            DataTable tblScheduleDay = new DataTable();
            DataColumn ScheduleDayIDCol = new DataColumn("ScheduleDayID");
            DataColumn DayNoCol = new DataColumn("DayNo");
            //add col vào ScheduleDay
            tblScheduleDay.Columns.AddRange(new DataColumn[] { ScheduleDayIDCol, DayNoCol });

            //Get dtPN & tblScheduleDay
            switch (radSchedule.SelectedValue)
            {
                case IPC.DAILY:
                    type = SmartPortal.Constant.IPC.DAILY;
                    fromdate = lbfromcfD.Text;
                    todate = lbtocfD.Text;
                    time = fromdate + " " + lbcftimeD.Text;
                    break;
                case IPC.WEEKLY:
                    type = SmartPortal.Constant.IPC.WEEKLY;
                    for (int i = 0; i < cblcfW.Items.Count; i++)
                    {
                        if (cblcfW.Items[i].Selected)
                        {
                            DataRow row = tblScheduleDay.NewRow();
                            row["ScheduleDayID"] = ScheID;
                            row["DayNo"] = cblcfW.Items[i].Value.ToString();
                            tblScheduleDay.Rows.Add(row);
                        }
                    }
                    fromdate = lbfromcfW.Text;
                    todate = lbtocfW.Text;
                    time = fromdate + " " + lbfctimeW.Text;
                    break;
                case IPC.MONTHLY:
                    type = SmartPortal.Constant.IPC.MONTHLY;
                    for (int j = 0; j < cblcfM.Items.Count; j++)
                    {
                        if (cblcfM.Items[j].Selected)
                        {
                            DataRow row1 = tblScheduleDay.NewRow();
                            row1["ScheduleDayID"] = ScheID;
                            row1["DayNo"] = cblcfM.Items[j].Value.ToString();
                            tblScheduleDay.Rows.Add(row1);
                        }
                    }
                    fromdate = lbfromcfM.Text;
                    todate = lbtocfM.Text;
                    time = fromdate + " " + lbcftimeM.Text;
                    break;
                case IPC.ONETIME:
                    type = SmartPortal.Constant.IPC.ONETIME;
                    fromdate = lbDateO.Text;
                    todate = lbDateO.Text;
                    time = lbDateO.Text;
                    break;
            }
            drPN["TYPE"] = radSchedule.SelectedValue;
            drPN["DATECREATED"] = time;
            drPN["FROMDATE"] = fromdate;
            drPN["TODATE"] = todate;

            // tạo table  tblScheDetail
            DataTable tblScheDetail = new DataTable();
            DataColumn ScheIDCol = new DataColumn("ScheduleID");
            DataColumn ParaNameCol = new DataColumn("ParaName");
            DataColumn ParaValueCol = new DataColumn("ParaValue");

            //add col vào tblScheDetail
            tblScheDetail.Columns.AddRange(new DataColumn[] { ScheIDCol, ParaNameCol, ParaValueCol });

            //Get tblScheDetail

            DataRow r1 = tblScheDetail.NewRow();
            r1["ScheduleID"] = ScheID;
            r1["ParaName"] = SmartPortal.Constant.IPC.SENDTYPE;
            r1["ParaValue"] = ddlSendType.SelectedValue;
            tblScheDetail.Rows.Add(r1);

            DataRow r2 = tblScheDetail.NewRow();
            r2["ScheduleID"] = ScheID;
            r2["ParaName"] = SmartPortal.Constant.IPC.SOURCEID;
            r2["ParaValue"] = SmartPortal.Constant.IPC.MB;
            tblScheDetail.Rows.Add(r2);
            new SmartPortal.SEMS.Schedule().InsertScheduleV3(
                Session["userID"].ToString(),
                ScheID, type, time, ScheName, string.Empty,
                SmartPortal.Constant.IPC.NEW, userCreate,
                userCreate, SmartPortal.Constant.IPC.YES,
                SmartPortal.Constant.IPC.SEMS, string.Empty, group,
                DateTime.Now.ToString("dd/MM/yyyy"), fromdate,
                todate, tblScheduleDay, tblScheDetail, dtPN,
                ref IPCERRORCODE, ref IPCERRORDESC);

            if (IPCERRORCODE == "0")
            {
                pnConfirm.Visible = false;
                pnTIB.Visible = false;
                pnSchedule.Visible = false;
                pnCFInfoLich.Visible = false;
                pnCFketqua.Visible = true;
            }
            else
            {
                switch (IPCERRORCODE)
                {
                    case SmartPortal.Constant.IPC.ERRORCODE.OTPINVALID:
                        lblError.Text = IPCERRORDESC;
                        return;
                    case SmartPortal.Constant.IPC.ERRORCODE.WATTINGBANKAPPROVE:
                        lblError.Text = Resources.labels.wattingbankapprove;
                        break;
                    case SmartPortal.Constant.IPC.ERRORCODE.WATTINGUSERAPPROVE:
                        lblError.Text = Resources.labels.wattinguserapprove;
                        break;
                    case SmartPortal.Constant.IPC.ERRORCODE.NOTREGOTP:
                        lblError.Text = Resources.labels.notregotp;
                        return;
                    default:
                        throw new Exception();
                }
            }
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
    protected void Button7_Click(object sender, EventArgs e)
    {
        try
        {
            lblCFTenLich.Text = txtScheduleName.Text;
            lblCFLoaiThongBao.Text = ddlTransferType.SelectedItem.Text;
            pnCFInfoLich.Visible = true;
            switch (radSchedule.SelectedValue)
            {
                case IPC.DAILY:
                    pnSchedule.Visible = false;
                    pnDaily.Visible = true;
                    pnMonthly.Visible = false;
                    pnWeekly.Visible = false;
                    pnOnetime.Visible = false;
                    pnTIB.Visible = false;
                    pnConfirm.Visible = false;
                    type = SmartPortal.Constant.IPC.DAILY;

                    break;
                case IPC.WEEKLY:
                    pnSchedule.Visible = false;
                    pnDaily.Visible = false;
                    pnMonthly.Visible = false;
                    pnWeekly.Visible = true;
                    pnOnetime.Visible = false;
                    pnTIB.Visible = false;
                    pnConfirm.Visible = false;
                    type = SmartPortal.Constant.IPC.WEEKLY;
                    break;
                case IPC.MONTHLY:
                    pnSchedule.Visible = false;
                    pnDaily.Visible = false;
                    pnMonthly.Visible = true;
                    pnWeekly.Visible = false;
                    pnOnetime.Visible = false;
                    pnTIB.Visible = false;
                    pnConfirm.Visible = false;
                    type = SmartPortal.Constant.IPC.MONTHLY;
                    break;
                case IPC.ONETIME:
                    pnSchedule.Visible = false;
                    pnDaily.Visible = false;
                    pnMonthly.Visible = false;
                    pnWeekly.Visible = false;
                    pnOnetime.Visible = true;
                    pnTIB.Visible = false;
                    pnConfirm.Visible = false;
                    type = SmartPortal.Constant.IPC.ONETIME;
                    break;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void Button12_Click(object sender, EventArgs e)
    {
        pnSchedule.Visible = true;
        pnDaily.Visible = false;
        pnMonthly.Visible = false;
        pnWeekly.Visible = false;
        pnOnetime.Visible = false;
        pnTIB.Visible = false;
        pnConfirm.Visible = false;
        pnCFInfoLich.Visible = false;
    }
    protected void Button13_Click(object sender, EventArgs e)
    {
        pnSchedule.Visible = true;
        pnDaily.Visible = false;
        pnMonthly.Visible = false;
        pnWeekly.Visible = false;
        pnOnetime.Visible = false;
        pnTIB.Visible = false;
        pnConfirm.Visible = false;
        pnCFInfoLich.Visible = false;
    }
    protected void Button14_Click(object sender, EventArgs e)
    {
        pnSchedule.Visible = true;
        pnDaily.Visible = false;
        pnMonthly.Visible = false;
        pnWeekly.Visible = false;
        pnOnetime.Visible = false;
        pnTIB.Visible = false;
        pnConfirm.Visible = false;
        pnCFInfoLich.Visible = false;
    }
    protected void Button15_Click(object sender, EventArgs e)
    {
        pnSchedule.Visible = true;
        pnDaily.Visible = false;
        pnMonthly.Visible = false;
        pnWeekly.Visible = false;
        pnOnetime.Visible = false;
        pnTIB.Visible = false;
        pnConfirm.Visible = false;
        pnCFInfoLich.Visible = false;
    }
    protected void Button8_Click(object sender, EventArgs e)
    {
        try
        {
            pnSchedule.Visible = false;
            pnMonthly.Visible = false;
            pnWeekly.Visible = false;
            pnOnetime.Visible = false;
            pnConfirm.Visible = false;
            fromdate = txtFromD.Text;
            todate = txtToD.Text;
            lbfromcfD.Text = fromdate;
            lbtocfD.Text = todate;
            lbcftimeD.Text = (int.Parse(ddlhourD.SelectedValue.ToString()) < 10 ? "0" + ddlhourD.SelectedValue.ToString() : ddlhourD.SelectedValue.ToString()) + ":" + (int.Parse(ddlminuteD.SelectedValue.ToString()) < 10 ? "0" + ddlminuteD.SelectedValue.ToString() : ddlminuteD.SelectedValue.ToString()) + ":00";
            cblcfW.Enabled = false;
            cblcfM.Enabled = false;
            DateTime dt = DateTime.ParseExact(fromdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            fromdate = dt.ToString("MM/dd/yyyy");
            pnTIB.Visible = checkvalidTime(fromdate + " " + lbcftimeD.Text);
            pnDaily.Visible = !pnTIB.Visible;

        }
        catch(Exception ex)
        {

        }

    }
    protected void Button9_Click(object sender, EventArgs e)
    {
        pnSchedule.Visible = false;
        pnDaily.Visible = false;
        pnMonthly.Visible = false;
        pnOnetime.Visible = false;
        pnConfirm.Visible = false;
        fromdate = txtFromW.Text;
        todate = txtToW.Text;
        lbfromcfW.Text = fromdate;
        lbtocfW.Text = todate;
        cblcfW.Enabled = false;
        cblcfM.Enabled = false;
        lbfctimeW.Text = (int.Parse(ddlhourW.SelectedValue.ToString()) < 10 ? "0" + ddlhourW.SelectedValue.ToString() : ddlhourW.SelectedValue.ToString()) + ":" + (int.Parse(ddlminuteW.SelectedValue.ToString()) < 10 ? "0" + ddlminuteW.SelectedValue.ToString() : ddlminuteW.SelectedValue.ToString()) + ":00";

        DateTime dt = DateTime.ParseExact(fromdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        fromdate = dt.ToString("MM/dd/yyyy");

        pnTIB.Visible = checkvalidTime(fromdate + " " + lbfctimeW.Text);
        pnWeekly.Visible = !pnTIB.Visible;
        for (int i = 0; i < cblThu.Items.Count; i++)
        {
            if (cblThu.Items[i].Selected)
            {
                cblcfW.Items[i].Selected = true;
            }
        }
    }
    protected void Button10_Click(object sender, EventArgs e)
    {
        pnSchedule.Visible = false;
        pnDaily.Visible = false;
        pnWeekly.Visible = false;
        pnOnetime.Visible = false;
        pnConfirm.Visible = false;
        fromdate = txtFromM.Text;
        todate = txtToM.Text;
        lbfromcfM.Text = fromdate;
        lbtocfM.Text = todate;
        cblcfW.Enabled = false;
        cblcfM.Enabled = false;
        lbcftimeM.Text = (int.Parse(ddlhourM.SelectedValue.ToString()) < 10 ? "0" + ddlhourM.SelectedValue.ToString() : ddlhourM.SelectedValue.ToString()) + ":" + (int.Parse(ddlhourM.SelectedValue.ToString()) < 10 ? "0" + ddlminuteM.SelectedValue.ToString() : ddlminuteM.SelectedValue.ToString()) + ":00";

        DateTime dt = DateTime.ParseExact(fromdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        fromdate = dt.ToString("MM/dd/yyyy");

        pnTIB.Visible = checkvalidTime(fromdate + " " + lbcftimeM.Text);
        pnMonthly.Visible = !pnTIB.Visible;

        for (int i = 0; i < cblThuM.Items.Count; i++)
        {
            if (cblThuM.Items[i].Selected)
            {
                cblcfM.Items[i].Selected = true;
            }
            else
            {
                cblcfM.Items[i].Selected = false;
            }
        }
    }
    protected void Button11_Click(object sender, EventArgs e)
    {
        lblTemplate.Visible = true;
        pnSchedule.Visible = false;
        pnDaily.Visible = false;
        pnMonthly.Visible = false;
        pnWeekly.Visible = false;
        pnConfirm.Visible = false;
        string fromdate = txtDateO.Text;
        DateTime dt = DateTime.ParseExact(fromdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        fromdate = dt.ToString("MM/dd/yyyy");
        string hour = (int.Parse(ddlHour.SelectedValue.ToString()) < 10 ? "0" + ddlHour.SelectedValue.ToString() : ddlHour.SelectedValue.ToString()) + ":" + (int.Parse(ddlMinute.SelectedValue.ToString()) < 10 ? "0" + ddlMinute.SelectedValue.ToString() : ddlMinute.SelectedValue.ToString()) + ":00";
        pnTIB.Visible = checkvalidTime(fromdate + " " + hour);
        pnOnetime.Visible = !pnTIB.Visible;
        fromdate = txtDateO.Text + " " + hour;
        lbDateO.Text = fromdate;
    }
    protected void Button17_Click(object sender, EventArgs e)
    {
        if (ACTION.Equals(IPC.ACTIONPAGE.EDIT))
        {
            RedirectBackToMainPage();
        }
        else
        {
            try
            {
                switch (radSchedule.SelectedValue)
                {
                    case IPC.DAILY:
                        pnSchedule.Visible = false;
                        pnDaily.Visible = true;
                        pnMonthly.Visible = false;
                        pnWeekly.Visible = false;
                        pnOnetime.Visible = false;
                        pnTIB.Visible = false;
                        pnConfirm.Visible = false;
                        break;
                    case IPC.WEEKLY:
                        pnSchedule.Visible = false;
                        pnDaily.Visible = false;
                        pnMonthly.Visible = false;
                        pnWeekly.Visible = true;
                        pnOnetime.Visible = false;
                        pnTIB.Visible = false;
                        pnConfirm.Visible = false;
                        break;
                    case IPC.MONTHLY:
                        pnSchedule.Visible = false;
                        pnDaily.Visible = false;
                        pnMonthly.Visible = true;
                        pnWeekly.Visible = false;
                        pnOnetime.Visible = false;
                        pnTIB.Visible = false;
                        pnConfirm.Visible = false;
                        break;
                    case IPC.ONETIME:
                        pnSchedule.Visible = false;
                        pnDaily.Visible = false;
                        pnMonthly.Visible = false;
                        pnWeekly.Visible = false;
                        pnOnetime.Visible = true;
                        pnTIB.Visible = false;
                        pnConfirm.Visible = false;
                        break;
                }
            }
            catch (Exception ex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                    this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(),
                    Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                    Request.Url.Query);
            }
        }
    }
    protected bool checkvalidTime(string time)
    {
        try
        {
            time= DateTime.ParseExact(time, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture).ToString();
            if (SmartPortal.Common.Utilities.Utility.IsDateTime2(time) > DateTime.Now)
            {
                return true;
            }
            else
            {
                ErrorCodeModel EM = new ErrorCodeModel();
                EM = new ErrorBLL().Load(Utility.IsInt(IPC.ERRORCODE.ErrorTime), System.Globalization.CultureInfo.CurrentCulture.ToString());
                lblError.Text = EM.ErrorDesc;
                return false;
            }

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
        return true;
    }
    protected void Button18_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }
    protected void ddlSendType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSendType.SelectedValue.Equals(IPC.USER))
        {
            trSendExecl.Visible = true;
            txtUser.Visible = true;
            lblUserCF.Visible = true;
            Label11.Visible = true;
            trSend.Visible = true;
            divUserNoti.Visible = true;
            Label12.Text = Resources.labels.userid;
            lblUpload.Visible = true;
        }
        else
        {
            trSend.Visible = false;
        }
    }
    protected void radSchedule_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ACTION.Equals(IPC.EDIT))
        {

            
            Button8.Visible = Button12.Visible = false;
            Button9.Visible = Button13.Visible = false;
            Button10.Visible = Button14.Visible = false;
            Button11.Visible = Button15.Visible = false;
            switch (radSchedule.SelectedValue)
            {
                case IPC.DAILY:
                    pnDaily.Visible = true;
                    pnWeekly.Visible = false;
                    pnMonthly.Visible = false;
                    pnOnetime.Visible = false;
                    break;
                case IPC.WEEKLY:
                    pnDaily.Visible = false;
                    pnWeekly.Visible = true;
                    pnMonthly.Visible = false;
                    pnOnetime.Visible = false;
                    break;
                case IPC.MONTHLY:
                    pnDaily.Visible = false;
                    pnWeekly.Visible = false;
                    pnMonthly.Visible = true;
                    pnOnetime.Visible = false;
                    break;
                case IPC.ONETIME:
                    pnDaily.Visible = false;
                    pnWeekly.Visible = false;
                    pnMonthly.Visible = false;
                    pnOnetime.Visible = true;
                    break;
                default:
                    break;
            }
        }
    }
    protected void btnUploadFile_Click(object sender, EventArgs e)
    {
        try
          {
            if (FUUserID.HasFiles && (Path.GetExtension(FUUserID.FileName) == ".xlsx" || Path.GetExtension(FUUserID.FileName) == ".xls"))
            {
                using (var excel = new ExcelPackage(FUUserID.PostedFile.InputStream))
                {
                    var tbl = new DataTable();

                    var ws = excel.Workbook.Worksheets.First();
                    var hasHeader = true;  // adjust accordingly
                                           // add DataColumns to DataTable
                    foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                        tbl.Columns.Add(hasHeader ? firstRowCell.Text
                            : String.Format("Column {0}", firstRowCell.Start.Column));

                    // add DataRows to DataTable
                    int startRow = hasHeader ? 2 : 1;
                    for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                    {
                        var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                        DataRow row = tbl.NewRow();
                        foreach (var cell in wsRow)
                            row[cell.Start.Column - 1] = cell.Text;
                        tbl.Rows.Add(row);
                        txtUser.Text += row["PHONENUMBER"].ToString() + ',';
                    }

                }
            }
            }catch
            {
            lblError.Text = "You did not specify a file to upload.";
            }    
        
    }
   
}
