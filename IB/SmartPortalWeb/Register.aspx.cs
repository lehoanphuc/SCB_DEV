using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

using SmartPortal.BLL;
using SmartPortal.Model;
using SmartPortal.SEMS;
using System.Drawing;
using Newtonsoft.Json;
using SmartPortal.Security;
using SmartPortal.Common;
using Image = System.Web.UI.WebControls.Image;
using System.Configuration;
using System.Runtime.Serialization;
using System.Text;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Collections;
using System.ComponentModel.DataAnnotations;

public class RecaptchaApiResponse
{
    [DataMember(Name = "success")]
    public bool Success;

    [DataMember(Name = "error-codes")]
    public List<string> ErrorCodes;
} 

public partial class ibRegister : System.Web.UI.Page
{
    string IPCERRORCODE;
    string IPCERRORDESC;

    public List<DocumentModel> listDocumentModel
    {
        get
        {
            // check if not exist to make new (normally before the post back)
            // and at the same time check that you did not use the same viewstate for other object
            if (!(ViewState["listDoc"] is List<DocumentModel>))
            {
                // need to fix the memory and added to viewstate
                ViewState["listDoc"] = new List<DocumentModel>();
            }

            return (List<DocumentModel>)ViewState["listDoc"];
        }
    }

    public DataTable listAcc
    {
        get
        {
            listAcc.Columns.AddRange(new DataColumn[] { new DataColumn("AccNo"), new DataColumn("CCYID"), new DataColumn("MONTHLYFEE") });
            return listAcc;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        UpLoadFile();
        pn1.Visible = true;
        if (!IsPostBack)
        {
            ViewState["ACCDETAILS"] = null;
            BindData();
        }
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            //check capchar
            if (txtValidateCode.Text.Trim() != Session["randomIBLogin"].ToString().Trim())
            {
                lblInfo.Text = Resources.labels.maxacnhankhongdung;
                return;
            }
            if (!validate())
            {
                return;
            }

            if (ViewState["isSendFirst"] == null || (int)ViewState["isSendFirst"] == 0)
            {
                LoadLoaiXacThuc();
            }
            else
            {
                if (DateTime.Compare(DateTime.Now, Convert.ToDateTime(ViewState["timeReSendOTP"].ToString())) > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "key", "resetOTP();", true);
                }
                LoadLoaiXacThuc();
            }
            pn1.Visible = false;
            pnOTP.Visible = true;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    void BindData()
    {
        

        City objCity = new City();
        DataSet city = new DataSet();
        city = objCity.GetCityByCondition("", "", ref IPCERRORCODE, ref IPCERRORDESC);
        ddlprovince.DataSource = city.Tables[0];
        ddlprovince.DataTextField = "CITYNAME";
        ddlprovince.DataValueField = "CityCode";
        ddlprovince.DataBind();

        #region load nation
        object[] _object = new object[] { string.Empty, string.Empty, null, null };
        Hashtable hasInputNation = new Hashtable();
        Hashtable hasOutputNation = new Hashtable();

        hasInputNation.Add("IPCTRANCODE", "WAL_BO_GET_NATION");
        hasInputNation.Add("SOURCEID", "SEMS");
        hasInputNation.Add("SOURCETRANREF", "010");
        hasInputNation.Add("TRANDESC", "Get kyc infor");
        hasInputNation.Add("PARA", _object);

        hasOutputNation = new SmartPortal.IB.Transactions().CommonProcessTrans(hasInputNation, ref IPCERRORCODE, ref IPCERRORDESC);

        DataSet dsNation = new DataSet();
        if (hasOutputNation[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
        {
            dsNation = (DataSet)hasOutputNation[SmartPortal.Constant.IPC.DATASET];
            ddlNation.DataSource = dsNation;
            ddlNation.DataTextField = "NATIONNAME";
            ddlNation.DataValueField = "NATIONCODE";
            ddlNation.DataBind();
        }
        #endregion


        Hashtable hasInputIDtype = new Hashtable();
        Hashtable hasOutputIDtype = new Hashtable();
        hasInputIDtype.Add("IPCTRANCODE", "SEMS_BO_GET_INFO_KYC");
        hasInputIDtype.Add("SOURCEID", "SEMS");
        hasInputIDtype.Add("SOURCETRANREF", "010");
        hasInputIDtype.Add("TRANDESC", "Get kyc infor");
        hasInputIDtype.Add("PARA", "");
        hasOutputIDtype = new SmartPortal.IB.Transactions().CommonProcessTrans(hasInputIDtype, ref IPCERRORCODE, ref IPCERRORDESC);

        DataSet dskyc = new DataSet();
        if (hasOutputIDtype[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
        {
            dskyc = (DataSet)hasOutputIDtype[SmartPortal.Constant.IPC.DATASET];
            ddlTypeID.DataSource = dskyc;
            ddlTypeID.DataTextField = "KYCNAME";
            ddlTypeID.DataValueField = "KYCCODE";
            ddlTypeID.DataBind();
        }

        Hashtable hasInput = new Hashtable();
        Hashtable hasOutput = new Hashtable();
        hasInput.Add("IPCTRANCODE", "WLM_GETTRANLIMIT");
        hasInput.Add("SOURCEID", "IB");
        hasInput.Add("SOURCETRANREF", "010");
        hasInput.Add("TRANDESC", "IB GET LIMIT TRAN");

        hasOutput = new SmartPortal.IB.Transactions().CommonProcessTrans(hasInput, ref IPCERRORCODE, ref IPCERRORDESC);
        DataSet ds = new DataSet();
        if (IPCERRORCODE == "0")
        {
            string infor = "";
            if (SmartPortal.Common.Utilities.Utility.isDouble(hasOutput["LIMITPERDAY"].ToString(), true) < 0)
            {
                infor = "Unlimit";
            }
            else
            {
                infor = SmartPortal.Common.Utilities.Utility.FormatMoney(hasOutput["LIMITPERDAY"].ToString(), "");
            }
            lblpush.Text = "Enjoy using Hi App! You will have ( " + infor + " LAK ) per day transfer limit.";
        } 
    }

    private bool validate()
    {
        lblInfo.Text = String.Empty;
        if (String.IsNullOrEmpty(txtFullname.Text))
        {
            lblInfo.Text = "User name is not empty!";
            return false;
        }

        if (String.IsNullOrEmpty(txtID.Text) || txtID.Text.Length < 9)
        {
            lblInfo.Text = "Invalid ID No.";
            return false;
        }

        if (String.IsNullOrEmpty(txtEmail.Text))
        {
            lblInfo.Text = "Invalid Email.";
            return false;
        }
        else if (new EmailAddressAttribute().IsValid(txtEmail.Text))
        {

        }
        else
        {
            lblInfo.Text = "Please spell Email address correctly";
            return false;
        }

        if (String.IsNullOrEmpty(txtDOB.Text))
        {
            lblInfo.Text = "Please input Date of birth.";
            return false;
        }
        else
        {
            DateTime dob1 = Convert.ToDateTime(txtDOB.Text);
            int now = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
            int dob = int.Parse(dob1.ToString("yyyyMMdd"));
            int age = (now - dob) / 10000;
            if (age < 15)
            {
                lblInfo.Text = "Customers must be over 15 years old.";
                return false;
            }
        }

        if (String.IsNullOrEmpty(txtIssueDate.Text))
        {
            lblInfo.Text = "Please input Issue Date.";
            return false;
        }

        if (String.IsNullOrEmpty(txtExpiryDate.Text))
        {
            lblInfo.Text = "Please input Expiry Date.";
            return false;
        }

        if (String.IsNullOrEmpty(txtDistrict.Text))
        {
            lblInfo.Text = "Please input District.";
            return false;
        }

        if (String.IsNullOrEmpty(txtphoneOTP.Text))
        {
            lblInfo.Text = "Please input OTP Phone.";
            return false;
        }
        //check kyc
        if (listDocumentModel.Count < 3)
        {
            lblInfo.Text = "Paper photos must be uploaded 3 photos";
            return false;
        }
        //check account
        if (ViewState["ACCDETAILS"] != null)
        {
            if (((DataTable)ViewState["ACCDETAILS"]).Rows.Count > 0)
            {
                if (String.IsNullOrEmpty(txtAccname.Text))
                {
                    lblInfo.Text = "Please input Account name.";
                    return false;
                }
            }
        }

        
        return true;
    }

    protected void txtphonenumber_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string errorcode = "";
            string errordesc = "";
            lblInfo.Text = "";
            btnsubmit.Enabled = true;
            DataSet ds = new SmartPortal.IB.Transactions().ETopup_GetTecoByPhoneNumber(txtphonenumber.Text.Trim(), ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE.Equals("0"))
            {
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && !ds.Tables[0].Rows[0]["TelcoID"].ToString().Equals("0"))
                {
                    new SmartPortal.IB.User().CheckPhoneNumberNonWallet(txtphonenumber.Text, ref errorcode, ref errordesc);
                    if (errorcode != "0")
                    {
                        lblInfo.Text = errordesc;
                        btnsubmit.Enabled = false;
                        return;
                    }
                }
                else
                {
                    lblInfo.Text = Resources.labels.phonenumberwrong;
                    btnsubmit.Enabled = false;
                    return;
                }
            }
            else
            {
                lblInfo.Text = Resources.labels.phonenumberwrong;
                btnsubmit.Enabled = false;
                return;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void txtphoneOTP_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string errorcode = "";
            string errordesc = "";
            lblInfo.Text = "";
            btnsubmit.Enabled = true;
            string phone = "020" + txtphoneOTP.Text.Trim();
            DataSet ds = new SmartPortal.IB.Transactions().ETopup_GetTecoByPhoneNumber(phone, ref errorcode, ref errordesc);
            if (errorcode.Equals("0"))
            {
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && !ds.Tables[0].Rows[0]["TelcoID"].ToString().Equals("0"))
                {
                    return;
                }
                else
                {
                    lblInfo.Text = "Please provide correct Phone number to receiver OTP";
                    btnsubmit.Enabled = false;
                    return;
                }
            }
            else
            {
                lblInfo.Text = "Please provide correct Phone number to receiver OTP";
                btnsubmit.Enabled = false;
                return;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void ddlprovince_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    void ImportFile(FileUpload file, string licienname,string licientype)
    {
        System.Drawing.Image imageToBeResized = System.Drawing.Image.FromStream(file.PostedFile.InputStream);
        string base64String;
        int imageHeight = imageToBeResized.Height;
        int imageWidth = imageToBeResized.Width;
        int maxHeight = 400;
        int maxWidth = 600;
        imageHeight = (imageHeight * maxWidth) / imageWidth;
        imageWidth = maxWidth;
        if (imageHeight > maxHeight)
        {
            imageWidth = (imageWidth * maxHeight) / imageHeight;
            imageHeight = maxHeight;
        }
        Bitmap bitmap = new Bitmap(imageToBeResized, imageWidth, imageHeight);
        System.IO.MemoryStream stream = new MemoryStream();
        bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
        stream.Position = 0;
        byte[] byteImage = stream.ToArray();
        base64String = Convert.ToBase64String(byteImage);

        DocumentModel item = new DocumentModel();
        DataTable tb = (DataTable)ViewState["listDocumentName"];
        item.IsNew = true;
        item.DocumentCode = string.Empty;
        item.DateCreated = DateTime.Now.ToString("dd/MM/yyyy");
        item.Status = "P";
        int no = listDocumentModel.Count;
        item.No = no + 1;
        item.DocumentName = licienname;
        item.DocumentType = licientype;

        item.File = base64String;
        listDocumentModel.Add(item);
    }

    protected void UpLoadFile()
    {
        if (IsPostBack && FUNRICBackNew.PostedFile != null)
        {
            if (FUNRICBackNew.PostedFile.FileName.Length > 0)
            {
                foreach (var item in listDocumentModel)
                {
                    if(item.DocumentName == "NB")
                    {
                        listDocumentModel.Remove(item);
                        break;
                    }
                }
                lblNRICBackNew.Text = Base64ImageAndShow(FUNRICBackNew, ImgNRICBackNew);
                PopupImgNRICBackNew.ImageUrl = "data:image/jpg;base64," + lblNRICBackNew.Text;
                ImportFile(FUNRICBackNew, "PP BACK", "NB");
            }
        }
        if (IsPostBack && FUNRICFontNew.PostedFile != null)
        {
            if (FUNRICFontNew.PostedFile.FileName.Length > 0)
            {
                foreach (var item in listDocumentModel)

                {
                    if (item.DocumentName == "NF")
                    {
                        listDocumentModel.Remove(item);
                        break;
                    }
                }
                lblNRICFontNew.Text = Base64ImageAndShow(FUNRICFontNew, ImgNRICFontNew);
                PopupImgNRICFontNew.ImageUrl = "data:image/jpg;base64," + lblNRICFontNew.Text;
                ImportFile(FUNRICFontNew, "PP FRONT", "NF");
            }
        }
        if (IsPostBack && FUSelfieNRIC.PostedFile != null)
        {
            if (FUSelfieNRIC.PostedFile.FileName.Length > 0)
            {
                foreach (var item in listDocumentModel)
                {
                    if (item.DocumentName == "SN")
                    {
                        listDocumentModel.Remove(item);
                        break;
                    }
                }
                lblSelfieNRIC.Text = Base64ImageAndShow(FUSelfieNRIC, ImgSelfieNRIC);
                PopupImgSelfieNRIC.ImageUrl = "data:image/jpg;base64," + lblSelfieNRIC.Text;
                ImportFile(FUSelfieNRIC, "SELFIE WITH PP", "SN");
            }
        }
    }

    private string Base64ImageAndShow(FileUpload fileUpload, Image Img)
    {
        try
        {
            string base64String;
            System.Drawing.Image imageToBeResized = System.Drawing.Image.FromStream(fileUpload.PostedFile.InputStream);
            int imageHeight = imageToBeResized.Height;
            int imageWidth = imageToBeResized.Width;
            int maxHeight = 400;
            int maxWidth = 600;
            imageHeight = (imageHeight * maxWidth) / imageWidth;
            imageWidth = maxWidth;
            if (imageHeight > maxHeight)
            {
                imageWidth = (imageWidth * maxHeight) / imageHeight;
                imageHeight = maxHeight;
            }
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(imageToBeResized, imageWidth, imageHeight);
            System.IO.MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            stream.Position = 0;
            byte[] byteImage = stream.ToArray();
            base64String = Convert.ToBase64String(byteImage);
            Img.ImageUrl = "data:image/png;base64," + base64String;
            Img.Visible = true;
            return base64String;
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void ddlLoaiXacThuc_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnSendOTP.Visible = ddlLoaiXacThuc.SelectedValue.ToString() == "ESMSOTP";
    }

    protected void btnSendOTP_Click(object sender, EventArgs e)
    {
        try
        {
            btnAction.Enabled = true;
            btnSendOTP.Visible = false;
            pnSendOTP.Visible = true;
            SmartPortal.SEMS.OTP.SendSMSOTPBYPHONE(SmartPortal.Common.Utilities.Utility.KillSqlInjection("020"+txtphoneOTP.Text.Trim()),ddlLoaiXacThuc.SelectedValue, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE != "0")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "key", "resetOTP();", true);
            }
            switch (IPCERRORCODE)
            {
                case "0":
                    int time = !String.IsNullOrEmpty(ConfigurationManager.AppSettings["OTPtimeexpires"]) ? int.Parse(ConfigurationManager.AppSettings["OTPtimeexpires"].ToString()) : 20;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "key", "countdownOTP(" + time + ");", true);
                    ViewState["timeReSendOTP"] = DateTime.Now.AddSeconds(time);
                    break;
                case "7003":
                    lblInfo.Text = Resources.labels.notregotp;
                    btnAction.Enabled = false;
                    break;
                default:
                    lblInfo.Text = IPCERRORDESC;
                    btnAction.Enabled = false;
                    break;
            }
            pn1.Visible = false;
            pnOTP.Visible = true;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    private void LoadLoaiXacThuc()
    {
        try
        {
            ViewState["isSendFirst"] = 1;

            if (ddlLoaiXacThuc.SelectedValue.ToString() == "ESMSOTP")
            {
                btnSendOTP_Click(null, EventArgs.Empty);
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void btnAction_Click(object sender, EventArgs e)
    {
        try
        {
            lblInfo.Text = string.Empty;
            Hashtable hasInput = new Hashtable();
            Hashtable hasOutput = new Hashtable();

            DateTime dt = DateTime.Parse(txtDOB.Text);
            dt.ToString("dd/MM/yyyy");
            Random rd = new Random();
            string pin = rd.Next(100000, 999999).ToString();

            #region prepare register

            hasInput.Add("IPCTRANCODE", "IB_REQREGISTER");
            hasInput.Add("SOURCEID", "IB");
            hasInput.Add("SOURCETRANREF", "010");
            hasInput.Add("TRANDESC", "IB Register");

            hasInput.Add("FULLNAME", SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtFullname.Text.Trim()));
            hasInput.Add("DOB", SmartPortal.Common.Utilities.Utility.KillSqlInjection(dt.ToString("dd/MM/yyyy")));
            hasInput.Add("GENDER", SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlGender.SelectedValue.Trim()));
            hasInput.Add("NATION", SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlNation.SelectedValue.Trim()));
            hasInput.Add("LICENSETYPE", SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlTypeID.SelectedValue.Trim()));
            hasInput.Add("LICENSEID", SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtID.Text.Trim()));
            hasInput.Add("ISSUEDATE", SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtIssueDate.Text.Trim()));
            hasInput.Add("EXPIRYDATE", SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtExpiryDate.Text.Trim()));
            hasInput.Add("HOUSENO", SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtHouseNo.Text.Trim()));
            hasInput.Add("UNIT", SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtUnit.Text.Trim()));
            hasInput.Add("VILLAGE", SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtVillage.Text.Trim()));
            hasInput.Add("DISTRICT", SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtHouseNo.Text.Trim()));
            hasInput.Add("PROVINCE", SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlprovince.SelectedValue.Trim()));
            hasInput.Add("PHONENO", SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtphonenumber.Text.Trim()));
            hasInput.Add("EMAIL", SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtEmail.Text.Trim()));
            hasInput.Add("ACCNAME", SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtAccname.Text.Trim()));
            hasInput.Add("LOGINNAME", SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtloginname.Text.Trim()));
            hasInput.Add("OTPPHONE", SmartPortal.Common.Utilities.Utility.KillSqlInjection("020"+txtphoneOTP.Text.Trim()));
            hasInput.Add("USERID", SmartPortal.Common.Utilities.Utility.KillSqlInjection("020"+txtphoneOTP.Text.Trim()));
            hasInput.Add("PINCODE", SmartPortal.Common.Utilities.Utility.KillSqlInjection(Encryption.Encrypt(pin)));
            hasInput.Add("PIN", SmartPortal.Common.Utilities.Utility.KillSqlInjection(pin));
            hasInput.Add(SmartPortal.Constant.IPC.AUTHENTYPE, ddlLoaiXacThuc.SelectedValue);
            hasInput.Add(SmartPortal.Constant.IPC.AUTHENCODE, txtOTP.Text.Trim());
            hasInput.Add("PASSWORD", "");
            hasInput.Add("POLICY", "1");
            hasInput.Add("CCYID", "LAK");
            hasInput.Add("PHONECOUNTRYCODE", "+95");
            hasInput.Add("PUSHID", "");
            hasInput.Add("DEVICEID", "");
            hasInput.Add("DEVICETYPE", "");

            //KCY
            #region insert KYC
            
            DataTable tblDocument = new DataTable();
            tblDocument.Columns.AddRange(new DataColumn[] { new DataColumn("DOCUMENTTYPE"), new DataColumn("DOCUMENTNAME"), new DataColumn("FILEBIN"), new DataColumn("STATUS") });
            foreach (var item in listDocumentModel)
            {
                DataRow r = tblDocument.NewRow();
                r["DOCUMENTTYPE"] = item.DocumentType;
                r["DOCUMENTNAME"] = item.DocumentName;
                r["FILEBIN"] = item.File;
                r["STATUS"] = item.Status;
                tblDocument.Rows.Add(r);
            }
            hasInput.Add("DOCUMENT", JsonConvert.SerializeObject(tblDocument));
            #endregion

            if (ViewState["ACCDETAILS"] != null)
            {
                if(((DataTable)ViewState["ACCDETAILS"]).Rows.Count > 0)
                {
                    hasInput.Add("DOCACC", JsonConvert.SerializeObject((DataTable)ViewState["ACCDETAILS"]));
                }
                else
                {
                    hasInput.Add("DOCACC", "");
                }
            }
            else
            {
                hasInput.Add("DOCACC", "");
            }
            #endregion

            hasOutput = new SmartPortal.IB.Transactions().CommonProcessTrans(hasInput, ref IPCERRORCODE, ref IPCERRORDESC);
            if (!IPCERRORCODE.Equals("0"))
            {
                lblInfo.Text = IPCERRORDESC;
                pn1.Visible = false;
                pnOTP.Visible = true;
                btnSendOTP.Visible = true;
                pnSendOTP.Visible = false;
            }
            else
            {
                pninfor.Visible = true;
                txtContract.Text = hasOutput["CONTRACTNO"].ToString();
                txtPhoneno.Text = hasOutput["PHONENO"].ToString();
                pn1.Visible = false;
                pnOTP.Visible = false;
                pnresult.Visible = true;
                pninfor.Visible = true;
            }
            
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void btnBackConfirm_Click(object sender, EventArgs e)
    {
        try
        {
            pn1.Visible = true;
            pnOTP.Visible = false;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void gvACC_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            Label lblaccno, lblccyid;
            LinkButton lbDelete;


            DataRowView drv;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView)e.Row.DataItem;

                lblaccno = (Label)e.Row.FindControl("lblaccno");
                lblccyid = (Label)e.Row.FindControl("lblccyid");
                lbDelete = (LinkButton)e.Row.FindControl("lbDelete");

                lblaccno.Text = drv["ACCNUMBER"].ToString();
                lblccyid.Text = drv["CURRENCY"].ToString();
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void gvACC_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvACC.PageIndex = e.NewPageIndex;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void gvACC_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable tblTransDetailsDel = (DataTable)ViewState["ACCDETAILS"];
        Label lblaccno = (Label)gvACC.Rows[e.RowIndex].FindControl("lblaccno");
        DataRow[] dr = tblTransDetailsDel.Select("ACCNUMBER='" + lblaccno.Text.Trim() + "'");
        tblTransDetailsDel.Rows.Remove(dr[0]);
        gvACC.DataSource = tblTransDetailsDel;
        gvACC.DataBind();
        if (tblTransDetailsDel.Rows.Count == 0)
        {
            ViewState["ACCDETAILS"] = null;
        }
        else
        {
            ViewState["ACCDETAILS"] = tblTransDetailsDel;
        }
        if (tblTransDetailsDel != null && tblTransDetailsDel.Rows.Count > 0)
        {
            pnGV.Visible = true;
        }
        else
        {
            pnGV.Visible = false;
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        DataTable tblTransDetails = new DataTable();
        DataTable tblTempt = new DataTable();
        string check = "";
        if (!ValidateAcc())
        {
            return;
        }
        if (ViewState["ACCDETAILS"] == null)
        {
            // tạo table tạm chứa TransDetails 
            DataColumn ACCNUMBER = new DataColumn("ACCNUMBER");
            DataColumn CURRENCY = new DataColumn("CURRENCY");
            DataColumn MONTHLYFEE = new DataColumn("MONTHLYFEE");
            //----thêm cột id----
            tblTransDetails.Columns.AddRange(new DataColumn[] { ACCNUMBER, CURRENCY, MONTHLYFEE});
            DataRow r = tblTransDetails.NewRow();
            r["ACCNUMBER"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtaccnumber.Text.Trim());
            r["CURRENCY"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlcciyd.SelectedValue.Trim());
            
            tblTransDetails.Rows.Add(r);
            ViewState["ACCDETAILS"] = tblTransDetails;
        }
        else
        {
            tblTransDetails = (DataTable)ViewState["ACCDETAILS"];
            tblTempt = (DataTable)ViewState["ACCDETAILS"];
            
            foreach (DataRow row in tblTransDetails.Rows)
            {
                if (row["ACCNUMBER"].ToString().Equals(txtaccnumber.Text.Trim()))
                {
                    lblInfo.Text = "Account number invalid !";
                    check = "1";
                    break;
                }
            }
            if(check == "1")
            {
                return;
            }

            DataRow r = tblTempt.NewRow();
            r["ACCNUMBER"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtaccnumber.Text.Trim());
            r["CURRENCY"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlcciyd.SelectedValue.Trim());

            tblTransDetails.Rows.Add(r);
            ViewState["ACCDETAILS"] = tblTransDetails;
            ViewState["ACCDETAILS"] = tblTempt;
            tblTransDetails = (DataTable)ViewState["ACCDETAILS"];
        }

        gvACC.DataSource = tblTransDetails;
        gvACC.DataBind();
        if (tblTransDetails != null && tblTransDetails.Rows.Count > 0)
        {
            pnGV.Visible = true;
        }
        else
        {
            pnGV.Visible = false;
        }
    }

    bool ValidateAcc()
    {
        lblInfo.Text = "";
        DataTable checklistacc = new DataTable();
        checklistacc = (DataTable)ViewState["ACCDETAILS"];
        if(checklistacc != null)
        {
            if (checklistacc.Rows.Count > 4)
            {
                lblInfo.Text = "Only allow up to 5 accounts.";
                return false;
            }
        }
        if (String.IsNullOrEmpty(txtaccnumber.Text))
        {
            lblInfo.Text = "Account number invalid !";
            return false;
        }
        else if(txtaccnumber.Text.Trim().Length < 8)
        {
            lblInfo.Text = "Account number must be 8-16 number !";
            return false;
        }
        return true;
    }

    protected void gvtran_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            Label lbtrantype, lblimitday, lblimittran;


            DataRowView drv;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView)e.Row.DataItem;

                lbtrantype = (Label)e.Row.FindControl("lbtrantype");
                lblimitday = (Label)e.Row.FindControl("lblimitday");
                lblimittran = (Label)e.Row.FindControl("lblimittran");

                lbtrantype.Text = drv["PageName"].ToString();
                if(SmartPortal.Common.Utilities.Utility.isDouble(drv["LIMITPERDAY"].ToString(), true) < 0)
                {
                    lblimitday.Text = "Unlimit";
                }
                else
                {
                    lblimitday.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(drv["LIMITPERDAY"].ToString(), "");
                }

                if (SmartPortal.Common.Utilities.Utility.isDouble(drv["LIMITPERTRAN"].ToString(), true) < 0)
                {
                    lblimittran.Text = "Unlimit";
                }
                else
                {
                    lblimittran.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(drv["LIMITPERTRAN"].ToString(), "");
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
}
