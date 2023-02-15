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
using SmartPortal.Common.Utilities;
using SmartPortal.ExceptionCollection;

public partial class Widgets_IBCardTenTransactions_Widget : WidgetBase
{
    string errorCode = string.Empty;
    string errorDesc = string.Empty;
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    string tranisBatch = string.Empty;

    private const string tranCode = "IB000625";
    private const string type = "All";

    static DataTable target;
    static DataTable dtCardList;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = "";

            if (!IsPostBack)
            {

                DataSet dsTranApp = new SmartPortal.SEMS.Transactions().LoadTranApp(ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE != "0")
                {
                    throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
                }


                DataSet ds = new DataSet();
                SmartPortal.IB.CreditCard objCrc = new SmartPortal.IB.CreditCard();

                ds = objCrc.GetCardlistfromEbcore(Session["userID"].ToString(), type, tranCode, ref errorCode, ref errorDesc);
                if (ds == null)
                {
                    //ShowPopUpMsg(Resources.labels.cardholdercustcodenotexist);
                    ltrError.Text = Resources.labels.datanotfound;
                    //                    return;
                }
                else
                {
                    dtCardList = ds.Tables[0];
                }
                if (ds != null && dtCardList.Rows.Count > 0)
                {
                    for (int i = 0; i <= dtCardList.Rows.Count - 1; i++)
                    {
                        string newCardNo = dtCardList.Rows[i][2].ToString();
                        //newCardNo = newCardNo.Substring(0, newCardNo.Length - 8) + "****" + newCardNo.Substring(newCardNo.Length - 4, 4);
                        //vutt 13062017
                        newCardNo = SmartPortal.Common.Utilities.Utility.MaskDigits(newCardNo);
                        dtCardList.Rows[i][3] = newCardNo;
                    }
                    target = new DataTable();
                    BindData(dtCardList);

                }
                else
                    lblError.Text = "Card is not registered yet!";

                //}
            }
        }
        catch (Exception ex)
        {

        }
    }
    void BindData(DataTable cardList)
    {
        bool hasOwn = false;
        foreach (DataRow r in cardList.Rows)
        {
            if (r[4].ToString().Equals("OWN"))
            {
                loadCardByType("OWN");
                hasOwn = true;
                break;
            }
        }

        if (hasOwn)
        {
            ddlCardType.Items.FindByValue("OWN").Selected = true;
            loadCardByType("OWN");
        }
        else
        {
            ddlCardType.Items.FindByValue("OTH").Selected = true;
            loadCardByType("OTH");
        }
        //hien len luoi
        loadTenTransaction();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (ddlAccount.SelectedIndex > -1)
            loadTenTransaction();
        else
            lblError.Text = "Card is not registered yet!";
    }

    void loadTenTransaction()
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dtTran = new DataTable();
            SmartPortal.IB.CreditCard objCrc = new SmartPortal.IB.CreditCard();
            ds = objCrc.GetTenTransactions("", "", ddlAccount.SelectedValue.ToString(), ref errorCode, ref errorDesc);
            if (errorCode != "0")
            {
                //ShowPopUpMsg("Connection to Card web service have problem");
                //ltrError.Text = Resources.labels.cardholdercustcodenotexist;
                return;
                //throw new IPCException("Transaction error");
            }
            else
            {
                if (ds == null || ds.Tables.Count <= 0)
                {
                    //ShowPopUpMsg(Resources.labels.cardholdercustcodenotexist);
                    //ltrError.Text = Resources.labels.cardholdercustcodenotexist;
                    lblError.Text = Resources.labels.datanotfound;
                    return;
                }
                else
                {
                    dtTran = ds.Tables[0];
                    for (int i = 0; i <= dtTran.Rows.Count - 1; i++)
                    {
                        string newCardNo = ddlAccount.SelectedValue.ToString();
                        //newCardNo = newCardNo.Substring(0, newCardNo.Length - 8) + "****" + newCardNo.Substring(newCardNo.Length - 4, 4);
                        //vutt 13062017
                        newCardNo = SmartPortal.Common.Utilities.Utility.MaskDigits(newCardNo);
                        dtTran.Rows[i][0] = newCardNo;
                        dtTran.Rows[i][2] = Utility.MappingCurrency(dtTran.Rows[i][2].ToString());
                        dtTran.Rows[i][3] = Utility.FormatMoney(dtTran.Rows[i][3].ToString(), "");
                        dtTran.Rows[i][4] = Utility.FormatDatetime((dtTran.Rows[i][4].ToString() + dtTran.Rows[i][5].ToString()), "yyyyMMddHHmmss", "yyyy-MM-dd HH:mm:ss");

                    }
                    rptProcessList.DataSource = dtTran;
                    rptProcessList.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    void loadCardByType(string type)
    {


        if (dtCardList.Rows.Count > 0)
        {
            ddlAccount.DataSource = dtCardList;
            ddlAccount.DataTextField = "CardType";
            ddlAccount.DataValueField = "CardNo";
            ddlAccount.DataBind();
        }
        else
            lblError.Text = "Card is not registered yet!";
    }
    protected void ddlCardType_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadCardByType(ddlCardType.SelectedValue.ToString());

    }
    protected void ddlAccount_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
