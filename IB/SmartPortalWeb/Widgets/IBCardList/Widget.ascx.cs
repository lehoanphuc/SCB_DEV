using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using SmartPortal.ExceptionCollection;
using SmartPortal.Common.Utilities;
using System.Threading;
using System.Collections.Generic;
using SmartPortal.IB;


public partial class Widgets_IBCardList_Widget : WidgetBase
{
    Thread th;
    private Object m_lock = new Object();
    private const string tranCode = "IB000621";
    private const string type = "All";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["userLevel"].ToString() == "3")
        {
            Response.Redirect("Default.aspx?po=3&p=336&f=huongdansudungqtht", false);
        }

        //LoadAccountList(sender,e);
    }
    protected void LoadData()
    {
        string errorCode = string.Empty;
        string errorDesc = string.Empty;
        DataSet ds = new DataSet();
        DataTable dtTran = new DataTable();
        SmartPortal.IB.CreditCard objCrc = new SmartPortal.IB.CreditCard();

        ds = objCrc.GetCardlistfromEbcore(Session["userID"].ToString(), type, tranCode, ref errorCode, ref errorDesc);
        dtTran = ds.Tables[0];
        if (dtTran.Rows.Count > 0)
        {
            LoadOwnerCard(dtTran);
            LoadOtherCard(dtTran);
        }
        else
        {
            Ownerdiv.Visible = false;
            Otherdiv.Visible = false;
            lblAlert.Text = "Card is not registered yet!";

        }
    }
    // edit by LanNH - 26/6/2018 - create datatable to bind source repeater
    private void LoadOtherCard(DataTable cardNo)
    {
        lock (m_lock)
        {
            Hashtable result = new Hashtable();
            try
            {
                string errorCode = string.Empty;
                string errorDesc = string.Empty;
                SmartPortal.IB.CreditCard objCrc = new SmartPortal.IB.CreditCard();

                DataTable dt = new DataTable();
                dt.Columns.Add("CardNo");
                dt.Columns.Add("CardLink");
                dt.Columns.Add("imgID");
                dt.Columns.Add("cardholderName");
                dt.Columns.Add("cardType");
                dt.Columns.Add("creditLimit");
                dt.Columns.Add("avaiLimit");
                dt.Columns.Add("outstandingAmt");

                foreach (DataRow dtRow in cardNo.Rows)
                {
                    if (dtRow["LinkType"].ToString() == "OTH" && dtRow["Status"].ToString() == "A")
                    {
                        result = objCrc.GetCardInfo("", "", dtRow["CardNo"].ToString(), ref errorCode, ref errorDesc);
                        if (result[SmartPortal.Constant.IPC.IPCERRORCODE].Equals("0"))
                        {
                            //xuat len man hinh 
                            {
                                string cardlinkdetail = SmartPortal.Common.Utilities.Utility.EncryptCard(result["cardNo"].ToString());
                                string newCardNo = dtRow["CardNo"].ToString();

                                DataRow row = dt.NewRow();
                                try
                                {
                                    string cardnumber = SmartPortal.Control.Security.DecryptCard(newCardNo.Trim());
                                    row["imgID"] = "/Images/card.png";//objCrc.GetCardImgID(cardnumber).Rows[0]["ImgID"];
                                }
                                catch { }

                                newCardNo = SmartPortal.Common.Utilities.Utility.MaskDigits(newCardNo);
                                row["CardNo"] = newCardNo;
                                row["CardLink"] = "?po=3&p=1057&cardno=" + cardlinkdetail;
                                row["cardholderName"] = result["cardholderName"].ToString();
                                row["cardType"] = Utility.mappingCardType(result["cardType"].ToString(), true);

                                row["creditLimit"] = Utility.FormatMoney(result["creditLimit"].ToString(), "");
                                row["avaiLimit"] = Utility.FormatMoney(result["avaiLimit"].ToString(), "");
                                row["outstandingAmt"] = Utility.FormatMoney(result["outstandingAmt"].ToString(), "");
                                dt.Rows.Add(row);
                            }
                        }
                        else
                        {
                            throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.IPC);
                        }
                    }

                }
                Otherdiv.Visible = dt.Rows.Count > 0;
                rptOtherCard.DataSource = dt;
                rptOtherCard.DataBind();

            }
            catch (IPCException IPCex)
            {
                SmartPortal.Common.Log.RaiseError(IPCex.Message, this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.Message, "Account list");
                //SmartPortal.Common.Log.GoToIBErrorPage(IPCex.Message, Request.Url.Query);

            }
            catch (Exception ex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "Account list");
                //SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

            }
        }
    }

    // LanNH - 25/6/2018
    protected void LoadOwnerCard(DataTable cardNo)
    {
        lock (m_lock)
        {
            Hashtable result = new Hashtable();
            try
            {
                string errorCode = string.Empty;
                string errorDesc = string.Empty;
                SmartPortal.IB.CreditCard objCrc = new SmartPortal.IB.CreditCard();

                DataTable dt = new DataTable();
                dt.Columns.Add("CardNo");
                dt.Columns.Add("CardLink");
                dt.Columns.Add("imgID");
                dt.Columns.Add("cardholderName");
                dt.Columns.Add("cardType");
                dt.Columns.Add("creditLimit");
                dt.Columns.Add("avaiLimit");
                dt.Columns.Add("outstandingAmt");


                foreach (DataRow dtRow in cardNo.Rows)
                {
                    if (dtRow["LinkType"].ToString() == "OWN" && dtRow["Status"].ToString() == "A")
                    {
                        result = objCrc.GetCardInfo("", "", dtRow["CardNo"].ToString(), ref errorCode, ref errorDesc);
                        if (result[SmartPortal.Constant.IPC.IPCERRORCODE].Equals("0"))
                        {
                            //xuat len man hinh 
                            {
                                string cardlinkdetail = SmartPortal.Common.Utilities.Utility.EncryptCard(result["cardNo"].ToString());
                                string newCardNo = dtRow["CardNo"].ToString();

                                DataRow row = dt.NewRow();
                                try
                                {
                                    string cardnumber = SmartPortal.Control.Security.DecryptCard(newCardNo.Trim());
                                    row["imgID"] = "/Images/card.png";// objCrc.GetCardImgID(cardnumber).Rows[0]["ImgID"];
                                }
                                catch { }

                                newCardNo = SmartPortal.Common.Utilities.Utility.MaskDigits(newCardNo);
                                row["CardNo"] = newCardNo;
                                row["CardLink"] = "?po=3&p=1057&cardno=" + cardlinkdetail;
                                row["cardholderName"] = result["cardholderName"].ToString();
                                row["cardType"] = Utility.mappingCardType(result["cardType"].ToString(), true);

                                row["creditLimit"] = Utility.FormatMoney(result["creditLimit"].ToString(), "");
                                row["avaiLimit"] = Utility.FormatMoney(result["avaiLimit"].ToString(), "");
                                row["outstandingAmt"] = Utility.FormatMoney(result["outstandingAmt"].ToString(), "");
                                dt.Rows.Add(row);

                            }
                        }
                        else
                        {
                            throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.IPC);
                        }
                    }

                }
                Ownerdiv.Visible = dt.Rows.Count > 0;
                rptOwncard.DataSource = dt;
                rptOwncard.DataBind();
            }
            catch (IPCException IPCex)
            {
                SmartPortal.Common.Log.RaiseError(IPCex.Message, this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.Message, "Account list");
                //SmartPortal.Common.Log.GoToIBErrorPage(IPCex.Message, Request.Url.Query);

            }
            catch (Exception ex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "Account list");
                //SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

            }
        }
    }

    protected void OnGetAccountListFinish(object sender, EventArgs e)
    {
        LoadData();
        Timer1.Enabled = false;
        //if (Session["strAccountList"] != null && Session["strAccountList"] != "#")
        //{
        //    ltrDD.Text = Session["strAccountList"].ToString();
        //    Timer1.Enabled = false;
        //    imgLoading.Visible = false;
        //    //th.Abort();
        //}
        //else if (Session["strAccountList"] == "#")
        //{
        //    Timer1.Enabled = false;
        //    imgLoading.Visible = false;
        //    //th.Abort();
        //    SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "?po=3&p=86");
        //}
    }


}
