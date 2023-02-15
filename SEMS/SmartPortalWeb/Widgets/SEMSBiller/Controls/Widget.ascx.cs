using SmartPortal.Common;
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.Common.Utilities;
using System.IO;
using Org.BouncyCastle.Utilities.Encoders;
using System.Text;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using SmartPortal.SEMS;

public partial class Widgets_SEMSBiller_Controls_Widget : WidgetBase
{
    private string ACTION = string.Empty;
    private string IPCERRORCODE = string.Empty;
    private string IPCERRORDESC = string.Empty;
    private string pid = string.Empty;
    private static string logoType = string.Empty;
    public string _TITLE
    {
        set { lblTitle.Text = value; }
        get { return lblTitle.Text; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, typeof(Page), "display cplx", " hidepwdcpdetail();", true);
        ScriptManager.RegisterStartupScript(this, typeof(Page), "display timelogin", " hidetimelogindetail();", true);
        try
        {
            ACTION = GetActionPage();
            if (!IsPostBack)
            {
                LoadAllDropDownList();
                BindData();
            }
        }
        catch (Exception ex)
        {
            Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                this.GetType().BaseType.Name, MethodBase.GetCurrentMethod().Name, ex.ToString(),
                Request.Url.Query);
            Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                Request.Url.Query);
        }
    }
    private void BindData()
    {
        try
        {
            txtDesc.Attributes.Add("maxlength", txtDesc.MaxLength.ToString());
            txtTimeOpen.Text = "08:00:00";
            txtTimeClose.Text = "16:00:00";
            ddlBill.Items.Add(new ListItem(Resources.labels.yes, IPC.YES));
            ddlBill.Items.Add(new ListItem(Resources.labels.no, IPC.NO));
            ddlStatus.Items.Add(new ListItem(Resources.labels.active, IPC.ACTIVE));
            ddlStatus.Items.Add(new ListItem(Resources.labels.inactive, IPC.INACTIVE));
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    break;

                default:
                    string ID = GetParamsPage(IPC.ID)[0].Trim();
                    DataTable bt = new Biller().GetBillDetailsById(ID, ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
                    if (IPCERRORCODE.Equals("0"))
                    {
                        if (bt.Rows.Count != 0)
                        {
                            txtBillerID.Text = bt.Rows[0]["BillerID"].ToString();
                            txtBillerCode.Text = bt.Rows[0]["BillerCode"].ToString();
                            txtBillerName.Text = bt.Rows[0]["BillerName"].ToString();
                            txtShortName.Text = bt.Rows[0]["ShortName"].ToString();
                            txtDesc.Text = bt.Rows[0]["Description"].ToString();
                            txtWebsite.Text = bt.Rows[0]["Website"].ToString();
                            txtPhone.Text = bt.Rows[0]["Phone"].ToString();
                            ddlCountryID.SelectedValue = bt.Rows[0]["CountryId"].ToString();
                            //LoadRegionDdl(ddlCountryID.SelectedValue);
                            //ddlRegion.SelectedValue = bt.Rows[0]["RegionID"].ToString();
                            txtBankSundry.Text = bt.Rows[0]["SUNDRYACCTNOBANK"].ToString();
                            txtBankIncome.Text = bt.Rows[0]["INCOMEACCTNOBANK"].ToString();
                            txtWalletSundry.Text = bt.Rows[0]["SUNDRYACCTNOWALLET"].ToString();
                            txtWalletIncome.Text = bt.Rows[0]["INCOMEACCTNOWALLET"].ToString();

                            LoadCityDdl(ddlCountryID.SelectedValue);
                            ddlCity.SelectedValue = bt.Rows[0]["CityID"].ToString();
                            ddlCatID.SelectedValue = bt.Rows[0]["CatID"].ToString();
                            ddlBill.SelectedValue = bt.Rows[0]["ShowAsBill"].ToString();
                            ddlStatus.SelectedValue = bt.Rows[0]["Status"].ToString();
                            txtTimeOpen.Text = bt.Rows[0]["TimeOpen"].ToString();
                            txtTimeClose.Text = bt.Rows[0]["TimeClose"].ToString();
                            imgLogo.ImageUrl = bt.Rows[0]["LogoBin"].ToString();
                            logoType = bt.Rows[0]["LogoType"].ToString();
                            txtLogoBin.Text = imgLogo.ImageUrl;
                        }
                    }
                    else
                    {
                        lblError.Text = IPCERRORDESC;
                    }
                    break;
            }
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    break;
                case IPC.ACTIONPAGE.DETAILS:
                    pnAdd.Enabled = false;
                    lbUpload.Visible = false;
                    //fuLogoBin.Visible = false;
                    txtLogoBin.Visible = false;
                    //cbxType.Visible = false;
                    btnSave.Enabled = false;
                    btnClear.Enabled = false;


                    if (!(string.IsNullOrEmpty(imgLogo.ImageUrl)))
                    {
                        lbImg.Visible = true;
                        imgLogo.Visible = true;
                    }
                    break;

                case IPC.ACTIONPAGE.EDIT:
                    txtBillerID.Enabled = false;
                    btnClear.Enabled = false;
                    txtBankSundry.Enabled = false;
                    txtBankIncome.Enabled = false;
                    txtWalletSundry.Enabled = false;
                    txtWalletIncome.Enabled = false;


                    if (!(string.IsNullOrEmpty(imgLogo.ImageUrl)))
                    {
                        lbImg.Visible = true;
                        imgLogo.Visible = true;
                    }
                    if (logoType.Equals(IPC.URI))
                    {
                        //fuLogoBin.Enabled = false;
                        //cbxType.Checked = true;
                        //lbPath.Visible = true;
                        txtLogoBin.Visible = true;
                    }
                    else if (logoType.Equals(IPC.BASE64))
                    {
                        txtLogoBin.Text = string.Empty;
                    }
                    break;
            }

        }
        catch (Exception ex)
        {
            Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                this.GetType().BaseType.Name, MethodBase.GetCurrentMethod().Name, ex.ToString(),
                Request.Url.Query);
            Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                Request.Url.Query);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string logoBin = txtLogoBin.Text.Trim();
            //if (cbxType.Checked == false && fuLogoBin.HasFile)
            //{
            //    System.IO.Stream fs = fuLogoBin.PostedFile.InputStream;
            //    System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
            //    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
            //    string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
            //    imgLogo.ImageUrl = "data:image/png;base64," + base64String;
            //    logoBin = imgLogo.ImageUrl;
            //    imgLogo.Visible = true;
            //    logoType = IPC.BASE64;
            //}
            //else if (cbxType.Checked == true)
            // {
            if (!logoBin.Equals(""))
            {
                logoType = IPC.URI;
                imgLogo.ImageUrl = logoBin;
                imgLogo.Visible = true;
                lbImg.Visible = true;
            }
            else
            {
                imgLogo.ImageUrl = logoBin;
                imgLogo.Visible = false;
                lbImg.Visible = false;
            }
            //}
            //else
            //{
            //    imgLogo.ImageUrl = string.Empty;
            //    logoType = IPC.BASE64;
            //}
            if (Convert.ToDateTime(txtTimeOpen.Text) >= Convert.ToDateTime(txtTimeClose.Text))
            {
                lblError.Text = Resources.labels.timeopenmustsmallerthantimeclose;
                return;
            }

            string bID = Utility.KillSqlInjection(txtBillerID.Text.Trim());
            string bCode = Utility.KillSqlInjection(txtBillerCode.Text.Trim());
            string bName = Utility.KillSqlInjection(txtBillerName.Text.Trim());
            string sName = Utility.KillSqlInjection(txtShortName.Text.Trim());
            string desc = Utility.KillSqlInjection(txtDesc.Text.Trim());
            string phone = Utility.KillSqlInjection(txtPhone.Text.Trim());
            string website = Utility.KillSqlInjection(txtWebsite.Text.Trim());
            string countryID = Utility.KillSqlInjection(ddlCountryID.SelectedValue.Trim());
            //int regionID = int.Parse(Utility.KillSqlInjection(ddlRegion.SelectedValue.Trim()));
            int cityID = int.Parse(Utility.KillSqlInjection(ddlCity.SelectedValue.Trim()));
            string catID = Utility.KillSqlInjection(ddlCatID.SelectedValue.Trim());
            string sAB = Utility.KillSqlInjection(ddlBill.SelectedValue.Trim());
            string status = Utility.KillSqlInjection(ddlStatus.SelectedValue.Trim());
            string timeOpen = Utility.KillSqlInjection(txtTimeOpen.Text.Trim());
            string timeClose = Utility.KillSqlInjection(txtTimeClose.Text.Trim());
            string user = Session["userName"].ToString();
            string SUNDRYACCTNOBANK = Utility.KillSqlInjection(txtBankSundry.Text.Trim());
            string INCOMEACCTNOBANK = Utility.KillSqlInjection(txtBankIncome.Text.Trim());
            string SUNDRYACCTNOWALLET = Utility.KillSqlInjection(txtWalletSundry.Text.Trim());
            string INCOMEACCTNOWALLET = Utility.KillSqlInjection(txtWalletIncome.Text.Trim());

            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    if (!CheckPermitPageAction(IPC.ACTIONPAGE.ADD))
                    {
                        return;
                    }
                    try
                    {
                        new Biller().Insert(bID, bCode, bName, sName, desc, phone, website, logoBin, logoType, countryID, cityID, catID, sAB, user, status, timeOpen, timeClose, SUNDRYACCTNOBANK, INCOMEACCTNOBANK, SUNDRYACCTNOWALLET, INCOMEACCTNOWALLET, ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE == "0")
                        {
                            lblError.Text = Resources.labels.addbillersuccessfully;
                            pnAdd.Enabled = false;
                            //fuLogoBin.Enabled = false;
                            btnSave.Enabled = false;
                        }
                        else
                        {
                            lblError.Text = IPCERRORDESC;
                            return;
                        }
                    }
                    catch (IPCException IPCex)
                    {
                        Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, MethodBase.GetCurrentMethod().Name,
                            IPCex.ToString(), Request.Url.Query);
                        Log.GoToErrorPage(IPCex.ToString(), Request.Url.Query);
                    }
                    catch (Exception ex)
                    {
                        Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                            this.GetType().BaseType.Name, MethodBase.GetCurrentMethod().Name,
                            ex.ToString(), Request.Url.Query);
                        Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                            Request.Url.Query);
                    }

                    break;
                case IPC.ACTIONPAGE.EDIT:
                    if (!CheckPermitPageAction(IPC.ACTIONPAGE.EDIT))
                    {
                        return;
                    }
                    try
                    {
                        new Biller().Update(bID, bCode, bName, sName, desc, phone, website, logoBin, logoType, countryID, cityID, catID, sAB, user, status, timeOpen, timeClose, SUNDRYACCTNOBANK, INCOMEACCTNOBANK, SUNDRYACCTNOWALLET, INCOMEACCTNOWALLET, ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE == "0")
                        {
                            lblError.Text = Resources.labels.updatebillersuccessfully;
                            pnAdd.Enabled = false;
                            //fuLogoBin.Enabled = false;
                            btnSave.Enabled = false;
                        }
                        else
                        {
                            lblError.Text = IPCERRORDESC;
                            return;
                            throw new IPCException(IPCERRORCODE);
                        }
                    }
                    catch (IPCException IPCex)
                    {
                        Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name,
                            MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
                        Log.GoToErrorPage(IPCex.Message, Request.Url.Query);
                    }
                    catch (Exception ex)
                    {
                        Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                            this.GetType().BaseType.Name, MethodBase.GetCurrentMethod().Name,
                            ex.ToString(), Request.Url.Query);
                        Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                            Request.Url.Query);
                    }

                    break;
            }
        }
        catch (BusinessExeption bex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["imageuploadec"], "Widgets_Widget_Controls_Widget", "btnSave_Click", bex.InnerException.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["imageuploadec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                this.GetType().BaseType.Name, MethodBase.GetCurrentMethod().Name, ex.ToString(),
                Request.Url.Query);
            Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                Request.Url.Query);
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }


    //protected void cbxType_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (cbxType.Checked == true)
    //    {
    //        //fuLogoBin.Enabled = false;
    //        txtLogoBin.Visible = true;
    //        lbPath.Visible = true;
    //        txtLogoBin.Text = string.Empty;
    //    }
    //    else
    //    {
    //        lbPath.Visible = false;
    //        //fuLogoBin.Enabled = true;
    //        txtLogoBin.Visible = false;
    //        txtLogoBin.Text = string.Empty;
    //        imgLogo.ImageUrl = string.Empty;
    //        imgLogo.Visible = false;
    //    }
    //}

    //protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //DataTable cityTable = new Biller().GetAllCityByRegionId(int.Parse(ddlRegion.SelectedValue), ref IPCERRORCODE,
    //    ref IPCERRORDESC).Tables[0];
    //if (cityTable.Rows.Count != 0)
    //{
    //    ddlCity.DataSource = cityTable;
    //    ddlCity.DataTextField = "CityName";
    //    ddlCity.DataValueField = "CityId";
    //    ddlCity.DataBind();
    //    ddlCity.Items.Insert(0, new ListItem("Please Choose City!", "0"));
    //    ddlCity.SelectedIndex = 0;
    //}
    //else
    //{
    //    ddlCity.Items.Clear();
    //    ddlCity.Items.Add(new ListItem("", "0"));
    //}
    //}

    protected void ddlCountryID_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable cityTable = new Biller()
            .GetAllCityByCountryId(ddlCountryID.SelectedValue, ref IPCERRORCODE, ref IPCERRORDESC)
            .Tables[0];
        if (cityTable.Rows.Count != 0)
        {
            ddlCity.DataSource = cityTable;
            ddlCity.DataTextField = "CityName";
            ddlCity.DataValueField = "CityId";
            ddlCity.DataBind();
            ddlCity.Items.Insert(0, new ListItem("Please Choose City!", "0"));
            ddlCity.SelectedIndex = 0;
        }
        else
        {
            //ddlRegion.Items.Clear();
            //ddlRegion.Items.Add(new ListItem("", "0"));
            //ddlRegion.SelectedIndex = 0;
            ddlCity.Items.Clear();
            ddlCity.Items.Add(new ListItem("", "0"));
            ddlCity.SelectedIndex = 0;
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        pnAdd.Enabled = true;
        btnSave.Enabled = true;
        btnSave.Visible = true;
        txtBillerID.Text = string.Empty;
        txtBillerCode.Text = string.Empty;
        txtBillerName.Text = string.Empty;
        txtShortName.Text = string.Empty;
        txtDesc.Text = string.Empty;
        txtPhone.Text = string.Empty;
        txtWebsite.Text = string.Empty;
        txtLogoBin.Text = string.Empty;
        txtBankSundry.Text = string.Empty;
        txtBankIncome.Text = string.Empty;
        txtWalletSundry.Text = string.Empty;
        txtWalletIncome.Text = string.Empty;

        ddlBill.ClearSelection();
        ddlCatID.ClearSelection();
        ddlCountryID.ClearSelection();
        ddlStatus.ClearSelection();
        //ddlRegion.ClearSelection();
        ddlCity.ClearSelection();

        txtTimeOpen.Text = "08:00:00";
        txtTimeClose.Text = "16:00:00";

        lblError.Text = string.Empty;
        imgLogo.ImageUrl = string.Empty;
        imgLogo.Visible = false;
        //fuLogoBin.Enabled = true;

    }

    #region Load To Drop Down List

    public void LoadAllDropDownList()
    {
        try
        {
            LoadCountryDdl();
            LoadCatIdDdl();
            //LoadRegionDdl(ddlCountryID.SelectedValue);
            LoadCityDdl(ddlCountryID.SelectedValue);
        }
        catch (Exception ex)
        {
            Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                this.GetType().BaseType.Name, MethodBase.GetCurrentMethod().Name, ex.ToString(),
                Request.Url.Query);
            Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                Request.Url.Query);
        }
    }

    private void LoadCatIdDdl()
    {
        DataSet catID = new Biller().GetCatIDGroup(ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE == "0")
        {
            ddlCatID.DataSource = catID;
            ddlCatID.DataTextField = "CatName";
            ddlCatID.DataValueField = "CatID";
            ddlCatID.DataBind();
        }
        else
        {
            lblError.Text = IPCERRORDESC;
        }
    }

    private void LoadCountryDdl()
    {
        DataTable countryTable = new Biller().GetAllCountry(ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
        if (countryTable.Rows.Count != 0)
        {
            ddlCountryID.DataSource = countryTable;
            ddlCountryID.DataTextField = "CountryName";
            ddlCountryID.DataValueField = "CountryId";
            ddlCountryID.DataBind();
            ddlCountryID.Items.Insert(0, new ListItem("Please Choose Country!", "0"));
            ddlCountryID.SelectedIndex = 0;
        }
        else
        {
            ddlCountryID.Items.Clear();
            ddlCountryID.Items.Add(new ListItem("", "0"));
        }
    }

    //private void LoadRegionDdl(string countryId)
    //{
    //    DataTable regionTable = new Biller()
    //        .GetAllRegionByCountryId(countryId, ref IPCERRORCODE, ref IPCERRORDESC)
    //        .Tables[0];
    //    if (regionTable.Rows.Count != 0)
    //    {
    //        ddlRegion.DataSource = regionTable;
    //        ddlRegion.DataTextField = "RegionName";
    //        ddlRegion.DataValueField = "RegionID";
    //        ddlRegion.DataBind();
    //        ddlRegion.Items.Insert(0, new ListItem("Please Choose Region!", "0"));
    //        ddlRegion.SelectedIndex = 0;
    //    }
    //    else
    //    {
    //        ddlRegion.Items.Clear();
    //        ddlRegion.Items.Add(new ListItem("", "0"));
    //        ddlRegion.SelectedIndex = 0;
    //        ddlCity.Items.Clear();
    //        ddlCity.Items.Add(new ListItem("", "0"));
    //    }
    //}

    private void LoadCityDdl(string countryID)
    {
        DataTable cityTable = new Biller().GetAllCityByCountryId(countryID, ref IPCERRORCODE,
            ref IPCERRORDESC).Tables[0];
        if (cityTable.Rows.Count != 0)
        {
            ddlCity.DataSource = cityTable;
            ddlCity.DataTextField = "CityName";
            ddlCity.DataValueField = "CityId";
            ddlCity.DataBind();
            ddlCity.Items.Insert(0, new ListItem("Please Choose City!", "0"));
            ddlCity.SelectedIndex = 0;
        }
        else
        {
            ddlCity.Items.Clear();
            ddlCity.Items.Add(new ListItem("", "0"));
        }
    }

    #endregion
}
