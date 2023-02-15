using System;
using System.Linq;
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
using SmartPortal.IB;
using SmartPortal.ExceptionCollection;
using System.Globalization;
using SmartPortal.Common.Utilities;

public partial class Widgets_IBCardDetail_Widget : WidgetBase
{
    string ErrorCode = string.Empty;
    string ErrorDesc = string.Empty;
    private const string tranCode = "IB000621";
    private const string type = "ALL";
    Hashtable result = new Hashtable();
    SmartPortal.IB.CreditCard objCrc = new SmartPortal.IB.CreditCard();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //ltrTH.Text = ""; 

            if (!IsPostBack)
            {
                if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cardno"] != null && SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cardno"].ToString() != string.Empty)
                {
                    hdfCardNo.Value = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cardno"].ToString();

                    DataSet ds = new DataSet();
                    DataTable dtTran = new DataTable();
                    SmartPortal.IB.CreditCard objCrc = new SmartPortal.IB.CreditCard();

                    ds = objCrc.CheckCardlistfromEbcore(Session["userID"].ToString(), type, tranCode, hdfCardNo.Value.Substring(0, 6), ref ErrorCode, ref ErrorDesc);
                    if (ErrorCode != "0")
                    {
                        return;
                    }
                    else
                    {
                        dtTran = ds.Tables[0];
                        if (dtTran.Rows.Count > 0)
                        {
                            hdfCardNo.Value = dtTran.Rows[0][2].ToString();
                            LoadCardDetail();
                        }
                        else
                        {
                            //Ownerdiv.Visible = false;
                            //Otherdiv.Visible = false;
                            ltrError.Text = "Card is not registered yet!";
                        }
                    }

                }
                //ShowPanel();
            }
            back.Click += (sa, ea) =>
            {
                Response.Redirect("Default.aspx?po=3&p=1045");
            };
        }
        catch
        {
        }
    }



    #region Load Info
    private void LoadCardDetail()
    {
        try
        {

            result = objCrc.GetCardInfo("", "", hdfCardNo.Value, ref ErrorCode, ref ErrorDesc);
            if (result[SmartPortal.Constant.IPC.IPCERRORCODE].Equals("0"))
            {
                ShowLN(result);
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.Message, this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }

    #endregion
    #region Show Panel
    private void ShowLN(Hashtable result)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dtTran = new DataTable();
            ds = objCrc.GetTenTransactions("", "", hdfCardNo.Value, ref ErrorCode, ref ErrorDesc);
            if (ErrorCode != "0")
            {
                //ShowPopUpMsg("Connection to Card web service have problem");
                ltrError.Text = ErrorDesc;
                //                return;
                //throw new IPCException("Transaction error");
            }
            else
            {
                if (ds == null || ds.Tables.Count <= 0)
                {
                    //ShowPopUpMsg(Resources.labels.cardholdercustcodenotexist);
                    ltrError.Text = Resources.labels.datanotfound;
                    //                    return;
                }
                else
                {
                    dtTran = ds.Tables[0];
                }
            }
            // card detail
            string cardType = Utility.mappingCardType(result["cardType"].ToString(), true);
            string card_no = (result["cardNo"]).ToString().Substring(0, (result["cardNo"]).ToString().Length - 6) + "****" + (result["cardNo"]).ToString().Substring((result["cardNo"]).ToString().Length - 4, 4);
            lblcardNo.Text = card_no;
            lblcardholderName.Text = result["cardholderName"].ToString();
            lblcardType.Text = Utility.mappingCardType(result["cardType"].ToString(), true);
            lblcreditLimit.Text = Utility.FormatMoney(result["creditLimit"].ToString(), "");
            lblavaiLimit.Text = Utility.FormatMoney(result["avaiLimit"].ToString(), "");
            lblcardStatus.Text = Utility.mappingCardStatus(result["cardStatus"].ToString(), true);
            lbloutstandingAmt.Text = Utility.FormatMoney(result["outstandingAmt"].ToString(), "");

            // last ten transaction
            if (ds != null)
            {
                for (int i = 0; i <= dtTran.Rows.Count - 1; i++)
                {
                    string newCardNo = dtTran.Rows[i][0].ToString();
                    newCardNo = newCardNo.Substring(0, newCardNo.Length - 8) + "****" + newCardNo.Substring(newCardNo.Length - 4, 4);
                    dtTran.Rows[i][0] = newCardNo;
                    dtTran.Rows[i][2] = Utility.MappingCurrency(dtTran.Rows[i][2].ToString());
                    dtTran.Rows[i][3] = Utility.FormatMoney(dtTran.Rows[i][3].ToString(), "");
                    dtTran.Rows[i][4] = Utility.FormatDatetime((dtTran.Rows[i][4].ToString() + dtTran.Rows[i][5].ToString()), "yyyyMMddHHmmss", "yyyy-MM-dd HH:mm:ss");

                }
                dtTran.Columns.Remove("trxnTime");
                dtTran.AcceptChanges();

                //hien len luoi
                rptData.DataSource = dtTran;
                rptData.DataBind();
            }
        }
        catch
        {
        }
    }
    #endregion


}
