using System;
using System.Data;
using System.Configuration;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


/// <summary>
/// Summary description for ReportBase
/// </summary>
public partial class ReportBase : WidgetBase
{

    public virtual void ClearSession()
    {
        DataTable tblParam = new DataTable();
        tblParam = new SmartPortal.SEMS.Report().LoadParam(GetParamsPage("rpt")[0], System.Globalization.CultureInfo.CurrentCulture.ToString());

        if (tblParam.Rows.Count != 0)
        {
            foreach (DataRow row in tblParam.Rows)
            {

                HttpContext.Current.Session[row["paramName"].ToString().Trim()] = null;

            }

        }

    }

    public virtual void SetParam()
    {

        DataTable tblParam = new DataTable();
        tblParam = new SmartPortal.SEMS.Report().LoadParam(GetParamsPage("rpt")[0], System.Globalization.CultureInfo.CurrentCulture.ToString());
        Type t;

        if (tblParam.Rows.Count != 0)
        {
            foreach (DataRow row in tblParam.Rows)
            {
                t = FindControl("pnControl").FindControl(row["paramName"].ToString().Trim()).GetType();
                Control co = FindControl("pnControl").FindControl(row["paramName"].ToString().Trim());
                switch (t.Name)
                {
                    case "Label":
                        Label lblText = (Label)co;
                        HttpContext.Current.Session.Add(row["paramName"].ToString().Trim(), lblText.Text);
                        break;
                    case "TextBox":
                        TextBox txtText = (TextBox)co;
                        HttpContext.Current.Session.Add(row["paramName"].ToString().Trim(), txtText.Text);
                        break;
                    case "DropDownList":
                        DropDownList ddlText = (DropDownList)co;
                        HttpContext.Current.Session.Add(row["paramName"].ToString().Trim(), ddlText.SelectedValue.Trim());
                        break;
                    case "HiddenField":
                        //HtmlInputHidden hdText = (HtmlInputHidden)co;

                        HiddenField hdText = (HiddenField)co;

                        Control co1 = FindControl("pnControl").FindControl(row["ddlValue"].ToString().Trim());

                        if (row["tag"].ToString().Trim().ToUpper() == "DDL")
                        {
                            DropDownList ddlHid = (DropDownList)co1;
                            hdText.Value = ddlHid.SelectedItem.Text;
                        }


                        HttpContext.Current.Session.Add(row["paramName"].ToString().Trim(), hdText.Value.Trim());
                        break;
                    case "CheckBox":

                        CheckBox dhcbx = (CheckBox)co;
                        if (dhcbx.CssClass != "nothing")
                        {
                            HttpContext.Current.Session.Add(row["paramName"].ToString().Trim(), dhcbx.Checked);
                        }
                        break;

                }

            }

            //Button btnViewReport = (Button)FindControl("btnViewReport");
            //btnViewReport.Click += new EventHandler(ViewReport);
            ViewReport(null, null);
        }
        else
        {
            DataTable tblReport = new SmartPortal.SEMS.Report().LoadReport(GetParamsPage("rpt")[0]);
            if (tblReport.Rows.Count != 0)
            {
                SmartPortal.Model.PagesModel tblPage = new SmartPortal.BLL.PagesBLL().GetPageInfo(tblReport.Rows[0]["PageViewID"].ToString().Trim());

                HttpContext.Current.Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=" + tblPage.PageID.ToString() + "&rpt=" + Request.QueryString["rpt"] + "&param=" + SmartPortal.Common.Encrypt.EncryptData(Request.Url.PathAndQuery)));

            }
        }

    }

    public void ViewReport(object sender, System.EventArgs e)
    {

        DataTable tblReport = new SmartPortal.SEMS.Report().LoadReport(GetParamsPage("rpt")[0]);
        if (tblReport.Rows.Count != 0)
        {
            SmartPortal.Model.PagesModel tblPage = new SmartPortal.BLL.PagesBLL().GetPageInfo(tblReport.Rows[0]["PageViewID"].ToString().Trim());

            HttpContext.Current.Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=" + tblPage.PageID.ToString() + "&rpt=" + GetParamsPage("rpt")[0] + "&param=" + SmartPortal.Common.Encrypt.EncryptData(Request.Url.PathAndQuery)));

        }

    }
}
