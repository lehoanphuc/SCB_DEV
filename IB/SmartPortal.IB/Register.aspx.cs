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

public partial class ibLogin : System.Web.UI.Page
{
    string IPCERRORCODE;
    string IPCERRORDESC;
    SmartPortal.IB.Common _service = new SmartPortal.IB.Common();


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
        if (!IsPostBack)
        {
            
            BindData();
        }
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ImportAcc(txtacc1, ddlCCYID1, cb01);


            listDocumentModel.Clear();
            Dictionary<object, object> _para = new Dictionary<object, object>();
            if (!validate())
            {
                return;
            }

            _para.Add("FULLNAME", SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtFullname.Text.Trim()));
            _para.Add("DOB", SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtDOB.Text.Trim()));
            _para.Add("GENDER", SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlGender.SelectedValue.Trim()));
            _para.Add("NATION", SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlNation.SelectedValue.Trim()));
            _para.Add("LICENSETYPE", SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlTypeID.SelectedValue.Trim()));
            _para.Add("LICENSEID", SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtID.Text.Trim()));
            _para.Add("ISSUEDATE", SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtIssueDate.Text.Trim()));
            _para.Add("EXPIRYDATE", SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtExpiryDate.Text.Trim()));
            _para.Add("HOUSENO", SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtHouseNo.Text.Trim()));
            _para.Add("UNIT", SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtUnit.Text.Trim()));
            _para.Add("VILLAGE", SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtVillage.Text.Trim()));
            _para.Add("DISTRICT", SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtHouseNo.Text.Trim()));
            _para.Add("PROVINCE", SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlprovince.SelectedValue.Trim()));
            _para.Add("PHONE", SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtphonenumber.Text.Trim()));
            _para.Add("EMAIL", SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtEmail.Text.Trim()));
            _para.Add("ACCNAME", SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtAccname.Text.Trim()));
            _para.Add("LOGINNAME", SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtloginname.Text.Trim()));
            _para.Add("OTPPHONE", SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtphoneOTP.Text.Trim()));

            //KCY
            #region insert KYC
            if(listDocumentModel.Count < 2)
            {
                ImportFile(FileFront);
                ImportFile(FileBack);
                ImportFile(FileSelfie);
            }
            if (listDocumentModel.Count == 0)
            {
                lblInfo.Text = Resources.labels.document + Resources.labels.IsNotNull;
                return;
            }
            
            DataTable tblDocument = new DataTable();
            tblDocument.Columns.AddRange(new DataColumn[] { new DataColumn("DOCUMENTTYPE"), new DataColumn("FILEBIN"), new DataColumn("STATUS") });
            foreach (var item in listDocumentModel)
            {
                DataRow r = tblDocument.NewRow();
                r["DOCUMENTTYPE"] = item.DocumentType;
                r["FILEBIN"] = item.File;
                r["STATUS"] = item.Status;
                tblDocument.Rows.Add(r);
            }
            _para.Add("DOCUMENT", JsonConvert.SerializeObject(tblDocument));
            _service.CallStore("REQREGISTER", _para, "Create request register IB", "N", ref IPCERRORCODE, ref IPCERRORDESC);
            #endregion

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
        city = objCity.GetCityByCondition("","",ref IPCERRORCODE,ref IPCERRORDESC);
        ddlprovince.DataSource = city.Tables[0];
        ddlprovince.DataTextField = "CITYNAME";
        ddlprovince.DataValueField = "CityCode";
        ddlprovince.DataBind();

        #region load nation
        DataSet dsNation = new DataSet();
        object[] _object = new object[] { string.Empty, string.Empty, null, null };
        dsNation = _service.common("WAL_BO_GET_NATION", _object, ref IPCERRORCODE, ref IPCERRORDESC);

        ddlNation.DataSource = dsNation;
        ddlNation.DataTextField = "NATIONNAME";
        ddlNation.DataValueField = "NATIONCODE";
        ddlNation.DataBind();
        #endregion
    }

    private bool validate()
    {
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
            return false;
        }
        if (String.IsNullOrEmpty(txtDOB.Text))
        {
            return false;
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
                    lblInfo.Text = "OTP" + Resources.labels.phonenumberwrong;
                    btnsubmit.Enabled = false;
                    return;
                }
            }
            else
            {
                lblInfo.Text = "OTP" + Resources.labels.phonenumberwrong;
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

    void ImportFile(FileUpload file)
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
        item.DocumentType = file.ID;

        item.File = base64String;
        listDocumentModel.Add(item);
    }

    void ImportAcc(TextBox txtacc, DropDownList ddlccyid, CheckBox cbfee)
    {
        try
        {
            DataTable tblDocument = new DataTable();
            tblDocument.Columns.AddRange(new DataColumn[] { new DataColumn("AccNo"), new DataColumn("CCYID"), new DataColumn("MONTHLYFEE") });
            DataRow r = tblDocument.NewRow();
            r["AccNo"] = txtacc.Text;
            r["CCYID"] = ddlccyid.SelectedValue;
            if (cbfee.Checked)
            {
                r["MONTHLYFEE"] = "Y";
            }
            else
            {
                r["MONTHLYFEE"] = "N";
            }
            listAcc.Rows.Add(r);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
}
