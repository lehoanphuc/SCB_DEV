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

using System.Text;
using System.Collections.Generic;

public partial class Widgets_SEMSSwiftCreate_ViewDetail_Widget : WidgetBase
{
    string ACTION = "";
    string cn = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    DataSet ApproveContractTable = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = "";
            if (!IsPostBack)
            {
                #region Load Authen Type
                DataTable tblAuthen = new SmartPortal.IB.Transactions().LoadAuthenType(Session["userID"].ToString());
                ddlAuthenType.DataSource = tblAuthen;
                ddlAuthenType.DataTextField = "TYPENAME";
                ddlAuthenType.DataValueField = "AUTHENTYPE";
                ddlAuthenType.DataBind();
                #endregion

                BindData();
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSContractList_ViewDetail_Widget", "Page_Load", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }

    void BindData()
    {
        #region Lấy thông tin giao dịch
        try
        {
            string tranID = "";

            if (Session["tranID"] == null)
            {
                tranID = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["tranid"].ToString();
            }
            else
            {
                List<string> lstTran = new List<string>();
                lstTran = (List<string>)Session["tranID"];

                tranID = lstTran[0];
            }

            #region Trường hợp session null
            DataSet ds = new DataSet();
            ds = new SmartPortal.SEMS.Transactions().GetSwiftDetail(tranID, ref IPCERRORCODE, ref IPCERRORDESC);

            if (IPCERRORCODE != "0")
            {
                goto ERROR;
            }

            DataTable dtLogTran = new DataTable();
            DataTable dtLogTranDetail = new DataTable();

            dtLogTran = ds.Tables[0];
            dtLogTranDetail = ds.Tables[1];

            if (dtLogTran.Rows.Count != 0)
            {
                lblTransID.Text = dtLogTran.Rows[0]["IPCTRANSID"].ToString();
                lblDate.Text = SmartPortal.Common.Utilities.Utility.IsDateTime2(dtLogTran.Rows[0]["IPCTRANSDATE"].ToString()).ToString("dd/MM/yyyy HH:mm");
                lblSenderAccount.Text = dtLogTran.Rows[0]["CHAR01"].ToString();
                lblRecieverAccount.Text = dtLogTran.Rows[0]["CHAR02"].ToString();
                lblBank.Text = dtLogTran.Rows[0]["BANKNAME"].ToString();
                lblCity.Text = dtLogTran.Rows[0]["CITYNAME"].ToString();
                lblCCYID.Text = dtLogTran.Rows[0]["CCYID"].ToString();
                lblAmount.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(dtLogTran.Rows[0]["NUM01"].ToString(), dtLogTran.Rows[0]["CCYID"].ToString().Trim());
                lblFee.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(dtLogTran.Rows[0]["NUM02"].ToString(), dtLogTran.Rows[0]["CCYID"].ToString().Trim());
                lblSenderName.Text = dtLogTran.Rows[0]["FULLNAME"].ToString();              
              
            }
            foreach (DataRow r in dtLogTranDetail.Rows)
            {
                switch (r["FIELDNAME"].ToString().Trim())
                {
                    case SmartPortal.Constant.IPC.RECEIVERNAME:
                        lblRecieverName.Text = r["FIELDVALUE"].ToString().Trim();
                        break;
                    case SmartPortal.Constant.IPC.LICENSE:
                        lblCMND.Text = r["FIELDVALUE"].ToString().Trim();
                        break;
                    case SmartPortal.Constant.IPC.CREATEDATE:
                        lblCMNDDate.Text = r["FIELDVALUE"].ToString().Trim();
                        break;
                    case SmartPortal.Constant.IPC.PLACE:
                        lblCMNDPlace.Text = r["FIELDVALUE"].ToString().Trim();
                        break;
                    case SmartPortal.Constant.IPC.DESC:
                        lblDesc.Text = r["FIELDVALUE"].ToString().Trim();
                        break;
                }
            }
            #endregion

            goto EXIT;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSContractList_ViewDetail_Widget", "BindData", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    ERROR:
        SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSContractList_ViewDetail_Widget", "BindData", IPCERRORDESC, Request.Url.Query);
        SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
    EXIT: ;

        #endregion

       
    }    

    protected void btApprove_Click(object sender, EventArgs e)
    {
        try
        {
            //approve
            string tranID = "";

            if (Session["tranID"] != null)
            {
                List<string> lstTran = new List<string>();
                lstTran = (List<string>)Session["tranID"];

                tranID = lstTran[0];

                //xử lý                


                bool dsApprove = new SmartPortal.SEMS.Transactions().CreateSwift(lblTransID.Text.Trim(),ddlSwift.SelectedValue, ref IPCERRORCODE, ref IPCERRORDESC);

                if (IPCERRORCODE == "0")
                {
                    lblError.Text = "Giao dịch " + lblTransID.Text + " tạo điện thành công";

                    //cập nhật lại List
                    lstTran.RemoveAt(0);
                }
                else
                {
                    if (IPCERRORCODE == ConfigurationManager.AppSettings["waittingapproveec"].ToString())
                    {
                        lblError.Text = SmartPortal.Common.Utilities.Utility.GetError(int.Parse(ConfigurationManager.AppSettings["waittingapproveec"].ToString())) + " - " + lblTransID.Text;

                        //cập nhật lại List
                        lstTran.RemoveAt(0);
                    }
                    else
                    {
                        lblError.Text = IPCERRORDESC;
                    }
                }

                if (lstTran.Count != 0)
                {
                    Session["tranID"] = lstTran;
                    //chuyển qua giao dịch kế
                    BindData();
                }
                else
                {
                    Session["tranID"] = null;
                    goto REDI;
                }

            }
            else
            {
                //approve khi duyệt 1 record
                bool dsApprove = new SmartPortal.SEMS.Transactions().CreateSwift(lblTransID.Text.Trim(), ddlSwift.SelectedValue, ref IPCERRORCODE, ref IPCERRORDESC);


                if (IPCERRORCODE == "0")
                {
                    goto REDI;
                }
                else
                {
                    if (IPCERRORCODE == ConfigurationManager.AppSettings["waittingapproveec"].ToString())
                    {
                        lblError.Text = SmartPortal.Common.Utilities.Utility.GetError(int.Parse(ConfigurationManager.AppSettings["waittingapproveec"].ToString())) + " - " + lblTransID.Text;

                    }
                    else
                    {
                        lblError.Text = IPCERRORDESC;
                    }
                }

                goto REDI;
            }
            goto EXIT;
        }
        catch
        {
        }
    REDI:
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(SmartPortal.Common.Encrypt.DecryptData(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["returnURL"].ToString())));
    EXIT:
        ;

    }
    protected void btReject_Click1(object sender, EventArgs e)
    {
        try
        {
            //ApproveContractTable = new SmartPortal.SEMS.Contract().ApproveContract(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString(), SmartPortal.Constant.IPC.BLOCK, ref IPCERRORCODE, ref IPCERRORDESC);
            //Response.Redirect("~/Default.aspx?p=148");
        }

        catch (Exception ex)
        { }
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=212"));
    }
}
