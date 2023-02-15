using System;
using System.Collections;
using System.Data;
using SmartPortal.ExceptionCollection;
using System.Web;
using SmartPortal.Constant;
using System.Text;
using SmartPortal.Common;
using SmartPortal.Common.Utilities;

public partial class Widgets_IBTransferBAC1_print : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo(new PortalSettings().portalSetting.DefaultLang);
        System.Threading.Thread.CurrentThread.CurrentCulture = ci;
        System.Threading.Thread.CurrentThread.CurrentUICulture = ci;

        if (Session["userName"] == null || Session["userName"].ToString() == "guest")
            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx"));
        try
        {
            //ghi vo session dung in
            DataTable hasPrint = new DataTable();
            hasPrint = (DataTable)Session["printUL1"];

            string str = "<table>";

            foreach(DataRow dr in hasPrint.Rows)
            {
                str += "<tr>";
                str += "<td>" + Resources.labels.userid;
                str += "</td>";
                str += "<td>";
                str += dr[IPC.USERID].ToString();
                str += "</td>";
                str += "</tr>";
                str += "<tr>";

                str += "<tr>";
                str += "<td>" + Resources.labels.fullname;
                str += "</td>";
                str += "<td>";
                str += dr[IPC.USERNAME].ToString();
                str += "</td>";
                str += "</tr>";
                str += "<tr>";

                str += "<tr>";
                str += "<td>" + Resources.labels.transaction;
                str += "</td>";
                str += "<td>";
                str += dr[IPC.TRANSACTION].ToString();
                str += "</td>";
                str += "</tr>";

                str += "<tr>";
                str += "<td>" + Resources.labels.approvallimit;
                str += "</td>";
                str += "<td>";
                str += Utility.FormatMoneyInputToView(dr[IPC.APPROVALLIMIT].ToString(), dr[IPC.CCYID].ToString());
                str += "</td>";
                str += "</tr>";

                str += "<tr>";
                str += "<td>" + Resources.labels.tonghanmucngay;
                str += "</td>";
                str += "<td>";
                str += Utility.FormatMoneyInputToView(dr[IPC.TOTALLIMITDAY].ToString(), dr[IPC.CCYID].ToString());
                str += "</td>";
                str += "</tr>";

                str += "<tr>";
                str += "<td>" + Resources.labels.sogiaodichtrenngay;
                str += "</td>";
                str += "<td>";
                str += dr[IPC.COUNTLIMIT].ToString();
                str += "</td>";
                str += "</tr>";

                str += "<tr>";
                str += "<td>" + Resources.labels.tiente;
                str += "</td>";
                str += "<td>";
                str += dr[IPC.CCYID].ToString();
                str += "</td>";
                str += "</tr>";

                str += "<tr><td colspan='2'><hr/></td></tr>";
            }
            
            str += "</table>";


            ltrStatement.Text = str;
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.Message, this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
}
