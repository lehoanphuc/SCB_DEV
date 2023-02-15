using SmartPortal.SEMS;
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


public partial class rptTest_Widget : ReportBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.ClearSession();
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            string rpt = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["rpt"];
            string rptName = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["rptname"];
            //if (!ReportRightBLL.CheckPermissionReport(Session["userName"].ToString(), rpt))
            //{
            //    Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["accessdenied"]);
            //}

            //load control dong
            DataTable tblParam = new DataTable();
            tblParam = new SmartPortal.SEMS.Report().LoadParam(rpt, System.Globalization.CultureInfo.CurrentCulture.ToString());

            lbltitle.Text = tblParam.Rows[0]["rptName"].ToString();

            if (tblParam.Rows.Count != 0)
            {
                HtmlGenericControl main = new HtmlGenericControl();

                HtmlGenericControl divrow = new HtmlGenericControl("div");
                divrow.Attributes.Add("class", "col-sm-12");

            
                foreach (DataRow row in tblParam.Rows)
                {
                    HtmlGenericControl div = new HtmlGenericControl("div");
                    div.Attributes.Add("class", "row");

                    HtmlGenericControl formgroup = new HtmlGenericControl("div");
                    formgroup.Attributes.Add("class", "form-group");

                    HtmlGenericControl div2 = new HtmlGenericControl("div");
                    div2.Attributes.Add("class", "col-sm-8");

                    Label lblText = new Label();
                    lblText.Attributes.Add("class", "col-sm-4 control-label");
                    lblText.Text = row["controlName"].ToString().Trim();

                    switch (row["controlType"].ToString().Trim().ToUpper())
                    {
                        case "TEXTBOX":
                            TextBox txtText = new TextBox();
                            txtText.ID = row["paramName"].ToString().Trim();
                            txtText.CssClass = "form-control";
                            if (row["tag"].ToString().Trim().ToUpper() == "SESSION")
                            {
                                txtText.Text = Session["userID"].ToString();
                                txtText.Enabled = false;
                            }
                            if (row["tag"].ToString().Trim().ToUpper() == "DATE")
                            {
                                txtText.Text = DateTime.Now.ToString("dd/MM/yyyy");
                                if (row["TimeRpt"].ToString().Trim() == "From")
                                {
                                    txtText.CssClass = "form-control datetimepicker fromDate";
                                }
                                else if (row["TimeRpt"].ToString().Trim() == "To")
                                {
                                    txtText.CssClass = "form-control datetimepicker toDate";
                                }
                                else
                                {
                                    txtText.CssClass = "form-control datetimepicker ";
                                }
                            }
                            if (row["tag"].ToString().Trim().ToUpper() == "DATETIME")
                            {
                                txtText.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                                if (row["TimeRpt"].ToString().Trim() == "From")
                                {
                                    txtText.CssClass = "form-control datepicker fromDate";
                                }
                                else if (row["TimeRpt"].ToString().Trim() == "To")
                                {
                                    txtText.CssClass = "form-control datepicker toDate";
                                }
                                else
                                {
                                    txtText.CssClass = "form-control datepicker ";
                                }
                            }
                            if (row["tag"].ToString().Trim().ToUpper() == "TEXT")
                            {
                                txtText.Text = row["ddlValue"].ToString().Trim();
                            }
                            div2.Controls.Add(txtText);
                            formgroup.Controls.Add(lblText);
                            formgroup.Controls.Add(div2);

                            div.Controls.Add(formgroup);
                            divrow.Controls.Add(div);
                            break;
                        case "HIDDENFIELD":
                            HiddenField HiddenField = new HiddenField();
                            HiddenField.ID = row["paramName"].ToString().Trim();
                            if (row["tag"].ToString().Trim().ToUpper() == "LANGUAGE")
                            {
                                HiddenField.Value = System.Globalization.CultureInfo.CurrentCulture.ToString();
                            }
                            if (row["tag"].ToString().Trim().ToUpper() == "SESSION")
                            {
                                HiddenField.Value = Session["userID"].ToString();
                            }
                            if (row["tag"].ToString().Trim().ToUpper() == "DATETIME")
                            {
                                HiddenField.Value = DateTime.Now.ToString("dd/MM/yyyy");
                            }
                            if (row["tag"].ToString().Trim().ToUpper() == "TEXT")
                            {
                                HiddenField.Value = row["ddlValue"].ToString().Trim();
                            }
                            pnControl.Controls.Add(HiddenField);
                            break;
                        case "DROPDOWNLIST":
                            DropDownList ddlText = new DropDownList();
                            ddlText.ID = row["paramName"].ToString().Trim();
                            ddlText.CssClass = "form-control select2";
                            if (row["ddlStore"].ToString().Trim().Contains("|"))
                            {
                                ddlText.DataSource = StoredWithParams(row["ddlStore"].ToString().Trim());
                            }
                            else
                                ddlText.DataSource = new SmartPortal.SEMS.Report().LoadDynamic(row["ddlStore"].ToString().Trim());
                            ddlText.DataTextField = row["ddlText"].ToString().Trim();
                            ddlText.DataValueField = row["ddlValue"].ToString().Trim();
                            ddlText.DataBind();
                            if (row["tag"].ToString().Trim().ToUpper() == "ALL")
                            {
                                ddlText.Items.Insert(0, new ListItem(Resources.labels.all, SmartPortal.Constant.IPC.ALL));
                            }
                            div2.Controls.Add(ddlText);
                            formgroup.Controls.Add(lblText);
                            formgroup.Controls.Add(div2);

                            div.Controls.Add(formgroup);
                            divrow.Controls.Add(div);
                            break;
                        case "CHECKBOX":
                            CheckBox CheckBox = new CheckBox();
                            CheckBox.ID = row["paramName"].ToString().Trim();
                            div2.Controls.Add(CheckBox);
                            formgroup.Controls.Add(lblText);
                            formgroup.Controls.Add(div2);

                            div.Controls.Add(formgroup);
                            divrow.Controls.Add(div);
                            break;
                    }
                }

                main.Controls.Add(divrow);

                pnControl.Controls.Add(main);
            }
            else
            {
                this.SetParam();
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

    public override void ClearSession()
    {
        base.ClearSession();
    }
    public override void SetParam()
    {
        base.SetParam();
    }
    protected void btnViewReport_Click(object sender, EventArgs e)
    {
        this.SetParam();
    }
    protected void btback_Click(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=358"));
    }

    protected DataTable StoredWithParams(string stored)
    {
        DataTable dt = new DataTable();
        string[] parmlist = stored.Split('|');
        object[] parms = new object[parmlist.Length - 1];
        string storedName = parmlist[0].ToString();

        if (parms.Length == 1)
        {
            if (parmlist[1].ToString().Equals(SmartPortal.Constant.IPC.USERID))
                parms[0] = Session["userID"].ToString();

            try
            {
                dt = SmartPortal.DAL.DataAccess.FillDataTable(storedName, parms);
            }
            catch
            {
                dt = null;
            }
        }

        return dt;
    }
}
