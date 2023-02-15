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
using SmartPortal.Common.Utilities;
using SmartPortal.ExceptionCollection;



public partial class Widgets_SEMSBankList_Controls_Widget : System.Web.UI.UserControl
{
    string ACTION = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
   
    public string _IMAGE
    {
        get { return imgLoGo.ImageUrl; }
        set { imgLoGo.ImageUrl = value; }
    }

    public string _TITLE
    {
        get { return lblTitleProduct.Text; }
        set { lblTitleProduct.Text = value; }
    }
   

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ACTION = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["a"].ToString().Trim();

            //lblErro.Text = "";
            pnAdd.Visible = true;
            pnResult.Visible = false;
           if (!IsPostBack)
            {
                //load city
                SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();
                DataSet dsCity = new DataSet();
                dsCity = objAcct.GetCity();
                if (dsCity.Tables[0].Rows.Count == 0)
                {
                    throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.CITYNOTEXISTS);
                }

                ddlProvince.DataSource = dsCity;
                ddlProvince.DataTextField = dsCity.Tables[0].Columns[SmartPortal.Constant.IPC.CITYNAME].ColumnName.ToString();
                ddlProvince.DataValueField = dsCity.Tables[0].Columns[SmartPortal.Constant.IPC.CITYCODE].ColumnName.ToString();
                ddlProvince.DataBind();

                //load ngan hang
                DataTable dtBank = new DataTable();
                dtBank = new SmartPortal.IB.Bank().GetBank();
                if (dtBank.Rows.Count == 0)
                {
                    throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.BANKNOTEXISTS);
                }

                ddlbank.DataSource = dtBank;
                ddlbank.DataTextField = "BANKNAME";
                ddlbank.DataValueField = "BANKID";
                ddlbank.DataBind();


                DataTable dtRemit = new DataTable();
                dtRemit = new SmartPortal.SEMS.Bank().LoadRemittance();
                ddlremit.DataSource = dtRemit;
                ddlremit.DataTextField = "REMITTANCENAME";
                ddlremit.DataValueField = "REMITID";
                ddlremit.DataBind();

                BindData();

                      
            }
        }
        catch (Exception ex)
        {
        }
     
    }
    void BindData() 
    {

        try
        {



            switch (ACTION)
            {
                case "add":
                    break;
                default:
                    #region Lấy thông tin san pham

                    DataTable productTable = new DataTable();
                    productTable = new SmartPortal.SEMS.Bank().LoadChildBank(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["bid"].ToString().Trim(),"","","","");
                    if (productTable.Rows.Count != 0)
                    {
                        txtbankcode.Text = productTable.Rows[0]["BANKCODE"].ToString();
                        txtbankname.Text = productTable.Rows[0]["BANKNAME"].ToString();
                        txtdesc.Text = productTable.Rows[0]["DESCRIPTION"].ToString();
                        ddlremit.SelectedValue = productTable.Rows[0]["REMITTANCE"].ToString();
                        ddlProvince.SelectedValue = productTable.Rows[0]["CITYCODE"].ToString();
                        ddlbank.SelectedValue = productTable.Rows[0]["BANKID"].ToString();
                    }
                    #endregion


                    break;
            }
            #region Enable/Disable theo Action
            switch (ACTION)
            {
                case "viewdetail":
                    txtbankcode.Enabled = false;
                    txtbankname.Enabled = false;
                    ddlbank.Enabled = false;
                    ddlProvince.Enabled = false;
                    txtdesc.Enabled = false;
                    ddlremit.Enabled = false;
                    break;
                case "edit":
                    txtbankcode.Enabled = false;
                    txtbankname.Enabled =true;
                    ddlbank.Enabled = true;
                    ddlProvince.Enabled = true;
                    txtdesc.Enabled = true;
                    ddlremit.Enabled = true;

                    break;
            }
            #endregion 

        }
        catch
        {
        }
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        string bankname = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtbankname.Text.Trim());
        string bankcode = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtbankcode.Text.Trim());
        string desc = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtdesc.Text.Trim());
        string bankid = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlbank.SelectedValue);
        string citycode = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlProvince.SelectedValue);
        string remit = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlremit.SelectedValue);
      try
        {
        switch (ACTION)
        {
            case "add":
                    int insert = 1;

                    insert = new SmartPortal.SEMS.Bank().InsertChildBank(bankcode,citycode,bankname,desc,remit,bankid);

                    if (insert != 1)
                    {
                        throw new IPCException("");
                    }
                    else
                    {
                        lbResult.Text = Resources.labels.themnganhangthanhcong;
                        pnAdd.Visible = false;
                        pnResult.Visible = true;
                        btsave.Visible = false;

                    }

                    break;

                break;

            case "edit":
                int insertt = 1;

                insertt =    new SmartPortal.SEMS.Bank().EditChildBank(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["bid"].ToString().Trim(),citycode,bankname,desc,remit,bankid);

                    if (insertt != 1)
                    {
                        throw new IPCException("");
                    }
                    else
                    {
                        lbResult.Text = Resources.labels.themnganhangthanhcong;
                        pnAdd.Visible = false;
                        pnResult.Visible = true;
                        btsave.Visible = false;
                        
                    }
                       
                    break;
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
    protected void btback_Click(object sender, EventArgs e)
    {
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=408"));
    }
}
