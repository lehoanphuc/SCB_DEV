using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.BLL;
using SmartPortal.Common.Utilities;
using SmartPortal.Model;
using SmartPortal.ExceptionCollection;
using System.Data;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;

public partial class Widgets_Pages_Controls_Widget : System.Web.UI.UserControl
{
    static string pageID;
    static string pageName;
    static string pageDesc;
    static string portalID;
    static string pageTitle;
    static string theme;
    static string isShow;
    static string IsDefault;
    static string IsApprove;
    static string IsNotification;
    static string IsSchedule;
    static string IsTemplate;
    static string IsReceive;
    static string IsProductFee;
    static string IsReport;
    static string IsViewReport;
    static string IsBeneficiary;
    static string IsReversal;
    static string TranCode;
    static string LinkApprove;
    static string masterpageID;
    static string subSystem;
    static string userModified;
    static string dateModified;
    static string action;
    static string pageReference;
    static string serviceID;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //load portal info
                PagesBLL PBLL = new PagesBLL();
                ddlPortalID.DataSource = PBLL.LoadService();
                ddlPortalID.DataTextField = "ServiceID";
                ddlPortalID.DataValueField = "ServiceID";
                ddlPortalID.DataBind();

                //load masterpage info 
                MasterPageBLL MPB = new MasterPageBLL();
                ddlMasterPage.DataSource = MPB.Load();
                ddlMasterPage.DataTextField = "MasterPageName";
                ddlMasterPage.DataValueField = "MasterPageID";
                ddlMasterPage.DataBind();

                //load theme
                ThemeBLL TB = new ThemeBLL();
                ddlTheme.DataSource = TB.Load();
                ddlTheme.DataTextField = "ThemeName";
                ddlTheme.DataValueField = "ThemeID";
                ddlTheme.DataBind();

                //load subsystem
                SubSystemBLL SS = new SubSystemBLL();
                ddlSubSystem.DataSource = SS.Load();
                ddlSubSystem.DataTextField = "SubSystemName";
                ddlSubSystem.DataValueField = "SubSystemID";
                ddlSubSystem.DataBind();

                //LanNTH - 13082018
                //Load Reference Page

                DataTable dt = PBLL.GetPageReference();
                ddlReference.DataSource = dt;
                ddlReference.DataTextField = "PageName";
                ddlReference.DataValueField = "PageID";
                ddlReference.DataBind();
                ddlReference.Items.Insert(0, new ListItem("No Reference", "0"));


                //Load action Page
                ddlAction.DataSource = PBLL.GetAction_Page(ddlPortalID.SelectedValue);
                ddlAction.DataTextField = "Description";
                ddlAction.DataValueField = "ActionKey";
                ddlAction.DataBind();

                //truong hop edit load thong tin
                if (Request["pid"] != null)
                {
                    PagesModel PM = new PagesModel();

                    PM = PBLL.GetPageInfo(Request["pid"].ToString().Trim());

                    ddlPortalID.SelectedValue = PM.ServiceID.ToString();
                    serviceID = PM.ServiceID.ToString();

                    txtPageName.Text = PM.PageName;
                    pageName = PM.PageName;

                    txtPageDesc.Text = PM.Description;
                    pageDesc = PM.Description;

                    txtPageTitle.Text = PM.Title;
                    pageTitle = PM.Title;

                    ddlMasterPage.SelectedValue = PM.MasterPageID.ToString();
                    masterpageID = PM.MasterPageID.ToString();

                    ddlTheme.SelectedValue = PM.ThemeID.ToString();
                    theme = PM.ThemeID.ToString();

                    ddlSubSystem.SelectedValue = PM.SubSystem.ToString();
                    subSystem = PM.SubSystem.ToString();

                    cbIsShow.Checked = PM.IsShow;
                    isShow = PM.IsShow.ToString();
                    ////? "True" : "False"
                    //Boolean doesIsDefault = Convert.ToBoolean(cbIsDefault.Checked) ;
                    //doesIsDefault = Convert.ToBoolean(PM.IsDefault);  
                    //IsDefault = PM.IsDefault.ToString(); 

                    string doesIsDefault = cbIsDefault.Checked ? "Y" : "N";
                    doesIsDefault = (PM.IsDefault);
                    IsDefault = PM.IsDefault.ToString();


                    if (doesIsDefault == "Y")
                    {
                        cbIsDefault.Checked = true;
                    }
                    else
                    {
                        cbIsDefault.Checked = false;
                    }

                    string doesIsApprove = cbIsApprove.Checked ? "Y" : "N";
                    doesIsApprove = (PM.IsApprove);
                    IsApprove = PM.IsApprove.ToString();
                    if (doesIsApprove == "Y")
                    {
                        cbIsApprove.Checked = true;
                    }
                    else
                    {
                        cbIsApprove.Checked = false;
                    }

                    string doesIsNotification = cbIsNotification.Checked ? "Y" : "N";
                    doesIsNotification = (PM.IsNotification);
                    IsNotification = PM.IsNotification.ToString();
                    if (doesIsNotification == "Y")
                    {
                        cbIsNotification.Checked = true;
                    }
                    else
                    {
                        cbIsNotification.Checked = false;
                    }
                    string doesIsSchedule = cbIsSchedule.Checked ? "Y" : "N";
                    doesIsSchedule = (PM.IsSchedule);
                    IsSchedule = PM.IsSchedule.ToString();
                    if (doesIsSchedule == "Y")
                    {
                        cbIsSchedule.Checked = true;
                    }
                    else
                    {
                        cbIsSchedule.Checked = false;
                    }

                    string doesIsTemplate = cbIsTemplate.Checked ? "Y" : "N";
                    doesIsTemplate = (PM.IsTemplate);
                    IsTemplate = PM.IsTemplate.ToString();
                    if (doesIsTemplate == "Y")
                    {
                        cbIsTemplate.Checked = true;
                    }
                    else
                    {
                        cbIsTemplate.Checked = false;
                    }

                    string doesIsReceive = cbIsReceive.Checked ? "Y" : "N";
                    doesIsReceive = (PM.IsReceive);
                    IsReceive = PM.IsReceive.ToString();
                    if (doesIsReceive == "Y")
                    {
                        cbIsReceive.Checked = true;
                    }
                    else
                    {
                        cbIsReceive.Checked = false;
                    }
                    string doesIsProductFee = cbIsProductFee.Checked ? "Y" : "N";
                    doesIsProductFee = (PM.IsProductFee);
                    IsProductFee = PM.IsProductFee.ToString();
                    if (doesIsProductFee == "Y")
                    {
                        cbIsProductFee.Checked = true;
                    }
                    else
                    {
                        cbIsProductFee.Checked = false;
                    }

                    string doesIsReport = cbIsReport.Checked ? "Y" : "N";
                    doesIsReport = (PM.IsReport);
                    IsReport = PM.IsReport.ToString();
                    if (doesIsReport == "Y")
                    {
                        cbIsReport.Checked = true;
                    }
                    else
                    {
                        cbIsReport.Checked = false;
                    }

                    string doesIsViewReport = cbIsViewReport.Checked ? "Y" : "N";
                    doesIsViewReport = (PM.IsViewReport);
                    IsViewReport = PM.IsViewReport.ToString();
                    if (doesIsViewReport == "Y")
                    {
                        cbIsViewReport.Checked = true;
                    }
                    else
                    {
                        cbIsViewReport.Checked = false;
                    }

                    string doesIsbeneficiary = cbIsbeneficiary.Checked ? "Y" : "N";
                    doesIsbeneficiary = (PM.Isbeneficiary);
                    IsBeneficiary = PM.Isbeneficiary.ToString();
                    if (doesIsbeneficiary == "Y")
                    {
                        cbIsbeneficiary.Checked = true;
                    }
                    else
                    {
                        cbIsbeneficiary.Checked = false;
                    }

                    string doesIsReversal = cbIsReversal.Checked ? "Y" : "N";
                    doesIsReversal = (PM.IsReversal);
                    IsReversal = PM.IsReversal.ToString();
                    if (doesIsReversal == "Y")
                    {
                        cbIsReversal.Checked = true;
                    }
                    else
                    {
                        cbIsReversal.Checked = false;
                    }

                    txtTrancode.Text = PM.Trancode;
                    TranCode = PM.Trancode;

                    txtLinkaApprove.Text = PM.LinkApprove;
                    LinkApprove = PM.LinkApprove;



                    txtPageId.Text = PM.PageID;
                    pageID = PM.PageID;
                    txtPageId.Enabled = false;
                    userModified = PM.UserModified;
                    dateModified = PM.DateModified;

                    ddlReference.SelectedValue = PM.PageReference;
                    pageReference = PM.PageReference;


                    ddlAction.SelectedValue = !string.IsNullOrEmpty(PM.Action) ? PM.Action : SmartPortal.Constant.IPC.ACTIONPAGE.NOACTION;
                    pageReference = PM.Action;

                    //kiem tra neu view diable control
                    if (Request["type"] != null)
                    {
                        ddlPortalID.Enabled = false;
                        txtPageId.Enabled = false;
                        txtPageName.Enabled = false;
                        txtPageDesc.Enabled = false;
                        txtPageTitle.Enabled = false;
                        ddlMasterPage.Enabled = false;
                        ddlTheme.Enabled = false;
                        ddlSubSystem.Enabled = false;
                        cbIsShow.Enabled = false;
                        cbIsViewReport.Enabled = false;
                        cbIsTemplate.Enabled = false;
                        cbIsSchedule.Enabled = false;
                        cbIsReversal.Enabled = false;
                        cbIsReport.Enabled = false;
                        cbIsReceive.Enabled = false;
                        cbIsProductFee.Enabled = false;
                        cbIsNotification.Enabled = false;
                        cbIsDefault.Enabled = false;
                        cbIsbeneficiary.Enabled = false;
                        cbIsApprove.Enabled = false;
                        ddlReference.Enabled = false;
                        ddlAction.Enabled = false;
                        btnSave.Visible = false;
                        txtTrancode.Enabled = false;
                        txtLinkaApprove.Enabled = false;
                        txtSearch.Enabled = false;
                        btnSave1.Visible = false;
                        imgSave.Visible = false;
                        imgSave1.Visible = false;
                    }
                }
            }

        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_Pages_Controls_Widget", "Page_Load", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Pages_Controls_Widget", "Page_Load", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void ddlPortalID_OnTextChanged(object sender, EventArgs e)
    {
        PagesBLL PBLL = new PagesBLL();
        ddlReference.Items.Insert(0, new ListItem("No Reference", "0"));
        //Load action Page
        ddlAction.DataSource = PBLL.GetAction_Page(ddlPortalID.SelectedValue);
        ddlAction.DataTextField = "Description";
        ddlAction.DataValueField = "ActionKey";
        ddlAction.DataBind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (Request["pid"] == null)
        {
            try
            {
                //insert
                PagesBLL PB = new PagesBLL();
                PagesModel model = PB.GetPageInfo(Utility.KillSqlInjection(txtPageId.Text.Trim()));
                if (model != null)
                {
                    try
                    {
                        PB.Insert(Utility.KillSqlInjection(txtPageId.Text.Trim()),
                              Utility.KillSqlInjection(txtPageName.Text.Trim()),
                              Utility.KillSqlInjection(txtPageDesc.Text.Trim()),
                              Utility.KillSqlInjection(ddlPortalID.SelectedValue),
                              Utility.IsInt(ddlTheme.SelectedValue),
                              Utility.KillSqlInjection(txtPageTitle.Text.Trim()),

                              cbIsShow.Checked, Convert.ToString(cbIsDefault.Checked ? "Y" : "N"),
                              Convert.ToString(cbIsApprove.Checked ? "Y" : "N"), Convert.ToString(cbIsNotification.Checked ? "Y" : "N"),
                              Convert.ToString(cbIsSchedule.Checked ? "Y" : "N"), Convert.ToString(cbIsTemplate.Checked ? "Y" : "N"),
                              Convert.ToString(cbIsReceive.Checked ? "Y" : "N"), Convert.ToString(cbIsProductFee.Checked ? "Y" : "N"),
                              Convert.ToString(cbIsReport.Checked ? "Y" : "N"), Convert.ToString(cbIsViewReport.Checked ? "Y" : "N"),
                              Convert.ToString(cbIsbeneficiary.Checked ? "Y" : "N"), Convert.ToString(cbIsReversal.Checked ? "Y" : "N"),
                              Utility.KillSqlInjection(txtTrancode.Text.Trim()),
                              Utility.KillSqlInjection(txtLinkaApprove.Text.Trim()),


                              Session["userName"].ToString(),
                              Utility.IsInt(ddlMasterPage.SelectedValue),
                              Utility.IsInt(ddlSubSystem.SelectedValue),
                              Utility.KillSqlInjection(ddlReference.SelectedValue),
                              ddlAction.SelectedValue.Trim()
                              );
                    }

                    catch
                    {
                        lblAlert.Text = Resources.labels.pageidAlreadyExist;
                        return;
                    }
                    //Write Log
                    SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["PAGE"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLPAGE"], System.Configuration.ConfigurationManager.AppSettings["PAGEID"], "", Utility.KillSqlInjection(txtPageId.Text.Trim()));
                    SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["PAGE"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLPAGE"], System.Configuration.ConfigurationManager.AppSettings["PAGENAME"], "", Utility.KillSqlInjection(txtPageName.Text.Trim()));
                    SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["PAGE"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLPAGE"], System.Configuration.ConfigurationManager.AppSettings["PAGEDESCRIPTION"], "", Utility.KillSqlInjection(txtPageDesc.Text.Trim()));
                    SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["PAGE"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLPAGE"], System.Configuration.ConfigurationManager.AppSettings["THEME"], "", ddlTheme.SelectedValue);
                    SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["PAGE"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLPAGE"], System.Configuration.ConfigurationManager.AppSettings["PAGETITLE"], "", Utility.KillSqlInjection(txtPageTitle.Text.Trim()));
                    SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["PAGE"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLPAGE"], System.Configuration.ConfigurationManager.AppSettings["AUTHOR"], "", Session["userName"].ToString());
                    SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["PAGE"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLPAGE"], System.Configuration.ConfigurationManager.AppSettings["ISSHOW"], "", cbIsShow.Checked.ToString());
                    SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["PAGE"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLPAGE"], System.Configuration.ConfigurationManager.AppSettings["MASTERPAGEID"], "", ddlMasterPage.SelectedValue);
                    SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["PAGE"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLPAGE"], System.Configuration.ConfigurationManager.AppSettings["SUBSYSTEMID"], "", ddlSubSystem.SelectedValue);
                    SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["PAGE"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLPAGE"], System.Configuration.ConfigurationManager.AppSettings["REFERENCE"], "", ddlReference.SelectedValue);
                    SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["PAGE"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLPAGE"], System.Configuration.ConfigurationManager.AppSettings["ACTION"], "", ddlAction.SelectedValue);

                }
            }
            catch (BusinessExeption bex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["piec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, bex.Message, Request.Url.Query);

                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["piec"], Request.Url.Query);
            }
            catch (SQLException sex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, sex.Message, Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
            }
            catch (Exception ex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
            }

            //alert
            lblAlert.Text = Resources.labels.insertsucessfull;

            txtPageName.Text = "";
            txtPageDesc.Text = "";
            txtPageTitle.Text = "";
        }
        else
        {
            try
            {
                //update     
                PagesBLL PB = new PagesBLL();
                PB.Update(DateTime.Now, Utility.KillSqlInjection(txtPageName.Text.Trim()), Utility.KillSqlInjection(txtPageDesc.Text.Trim()), Utility.KillSqlInjection(ddlPortalID.SelectedValue)
                    , Utility.IsInt(ddlTheme.SelectedValue), Utility.KillSqlInjection(txtPageTitle.Text.Trim()),
                             cbIsShow.Checked, Convert.ToString(cbIsDefault.Checked ? "Y" : "N"),
                              Convert.ToString(cbIsApprove.Checked ? "Y" : "N"), Convert.ToString(cbIsNotification.Checked ? "Y" : "N"),
                              Convert.ToString(cbIsSchedule.Checked ? "Y" : "N"), Convert.ToString(cbIsTemplate.Checked ? "Y" : "N"),
                              Convert.ToString(cbIsReceive.Checked ? "Y" : "N"), Convert.ToString(cbIsProductFee.Checked ? "Y" : "N"),
                              Convert.ToString(cbIsReport.Checked ? "Y" : "N"), Convert.ToString(cbIsViewReport.Checked ? "Y" : "N"),
                              Convert.ToString(cbIsbeneficiary.Checked ? "Y" : "N"), Convert.ToString(cbIsReversal.Checked ? "Y" : "N"),
                              Utility.KillSqlInjection(txtTrancode.Text.Trim()),
                              Utility.KillSqlInjection(txtLinkaApprove.Text.Trim()),
                     Session["userName"].ToString(), Utility.IsInt(ddlMasterPage.SelectedValue), Utility.IsInt(ddlSubSystem.SelectedValue), ddlReference.SelectedValue.Trim(), ddlAction.SelectedValue.Trim(),
                    (Request["pid"].ToString().Trim()));

                //Write Log
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["PAGE"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLPAGE"], System.Configuration.ConfigurationManager.AppSettings["PAGENAME"], pageName, Utility.KillSqlInjection(txtPageId.Text.Trim()));

                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["PAGE"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLPAGE"], System.Configuration.ConfigurationManager.AppSettings["PAGENAME"], pageName, Utility.KillSqlInjection(txtPageName.Text.Trim()));
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["PAGE"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLPAGE"], System.Configuration.ConfigurationManager.AppSettings["PAGEDESCRIPTION"], pageDesc, Utility.KillSqlInjection(txtPageDesc.Text.Trim()));
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["PAGE"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLPAGE"], System.Configuration.ConfigurationManager.AppSettings["PORTALID"], portalID, ddlPortalID.SelectedValue);
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["PAGE"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLPAGE"], System.Configuration.ConfigurationManager.AppSettings["THEME"], theme, ddlTheme.SelectedValue);
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["PAGE"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLPAGE"], System.Configuration.ConfigurationManager.AppSettings["PAGETITLE"], pageTitle, Utility.KillSqlInjection(txtPageTitle.Text.Trim()));

                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["PAGE"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLPAGE"], System.Configuration.ConfigurationManager.AppSettings["ISSHOW"], isShow, cbIsShow.Checked.ToString());
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["PAGE"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLPAGE"], System.Configuration.ConfigurationManager.AppSettings["MASTERPAGEID"], masterpageID, ddlMasterPage.SelectedValue);
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["PAGE"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLPAGE"], System.Configuration.ConfigurationManager.AppSettings["SUBSYSTEMID"], subSystem, ddlSubSystem.SelectedValue);
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["PAGE"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLPAGE"], System.Configuration.ConfigurationManager.AppSettings["REFERENCE"], "", ddlReference.SelectedValue);
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["PAGE"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLPAGE"], System.Configuration.ConfigurationManager.AppSettings["ACTION"], "", ddlAction.SelectedValue);

                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["PAGE"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLPAGE"], System.Configuration.ConfigurationManager.AppSettings["PAGEID"], pageID, Request["pid"].ToString());
            }
            catch (BusinessExeption bex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["puec"], "Widgets_Pages_Controls_Widget", "btnSave_Click", bex.Message, Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["puec"], Request.Url.Query);
            }
            catch (SQLException sex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_Pages_Controls_Widget", "btnSave_Click", sex.Message, Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
            }
            catch (Exception ex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Pages_Controls_Widget", "btnSave_Click", ex.Message, Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
            }
            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(System.Configuration.ConfigurationManager.AppSettings["viewpage"]));
        }


    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(System.Configuration.ConfigurationManager.AppSettings["viewpage"]));
    }

}
