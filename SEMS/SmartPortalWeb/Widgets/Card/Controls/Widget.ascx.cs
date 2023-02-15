using SmartPortal.BLL;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Widgets_Card_Controls_Widget : WidgetBase
{

    string ACTION = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    string Pagemain = "1111";

    protected void Page_Load(object sender, EventArgs e)
    {
        ACTION = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["a"].ToString().Trim();
        lblcardamountpn.Attributes.Add("onkeypress", "executeComma('" + lblcardamountpn.ClientID + "',event)");
        lblrealmoneypn.Attributes.Add("onkeypress", "executeComma('" + lblrealmoneypn.ClientID + "',event)");
        lblcardamountpn.Attributes.Add("onkeyup", "executeComma('" + lblcardamountpn.ClientID + "',event)");
        lblrealmoneypn.Attributes.Add("onkeyup", "executeComma('" + lblrealmoneypn.ClientID + "',event)");
        if (!IsPostBack)
        {
            BindData();
        }
    }

    private void BindData()
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable custTable = new DataTable();
            if (!PagesBLL.ChekcPermitPageAction("1108", Session["userName"].ToString(), ACTION))
            {
                btsave.Enabled = false;
            }
            switch (ACTION)
            {
                case "ADD":
                    //04102019
                    lbltitle.Text = "Add New Card";
                    lblstatuspn.Enabled = false;
                    //Label11.Visible = false;
                    lblcardidpn.Visible = false;
                    break;
                case "EDIT":

                    lblcardidpn.Enabled = false;
                    btnClear.Enabled = false;
                    lbltitle.Text = "Edit Card";
                    pnCard.Visible = true;
                    DataSet dsaa = new DataSet();
                    DataTable custTableA = new DataTable();
                    dsaa = new SmartPortal.SEMS.User().GetalltelcoDetail(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["b"].ToString(), "ALL", "ALL", "ALL",0,0, ref IPCERRORCODE, ref IPCERRORDESC);

                    ds = new SmartPortal.SEMS.User().Getallcardtop(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["ID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                    ViewState["ULCard"] = ds.Tables[0];
                    if (IPCERRORCODE != "0")
                    {
                        throw new IPCException(IPCERRORDESC);
                    }
                    else
                    {
                        custTable = ds.Tables[0];
                        custTableA = dsaa.Tables[1];
                    }
                    if (custTable.Rows.Count != 0)
                    {
                        lblcardidpn.Text = custTable.Rows[0]["CardID"].ToString();
                        lblshortnamepn.Text = custTable.Rows[0]["ShortName"].ToString();
                
                        System.Globalization.CultureInfo dk = new System.Globalization.CultureInfo("en-US");
                        string Amountcard = double.Parse(custTable.Rows[0]["CardAmount"].ToString()).ToString("#,#", dk);
                        string Realmoney = double.Parse(custTable.Rows[0]["RealAmount"].ToString()).ToString("#,#", dk);
                        lblcardamountpn.Text = Amountcard;
                        lblrealmoneypn.Text = Realmoney;
                        lblccyidpn.Text = custTable.Rows[0]["CCYID"].ToString();
                        lbltypepn.Text = custTable.Rows[0]["Type"].ToString();
                        lblstatuspn.SelectedValue = custTable.Rows[0]["Status"].ToString();
                    }
                    break;
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();

            switch (ACTION)
            {
                case "EDIT":

                    if (lblshortnamepn.Text == "")
                    {
                        lblError.Text = "Short Name can not be blank";
                        lblshortnamepn.Focus();
                        break;
                    }
                    else if (lblcardamountpn.Text == "")
                    {
                        lblError.Text = "Card Amount can not be blank";
                        lblcardamountpn.Focus();
                        break;
                    }
                    else if (lblrealmoneypn.Text == "")
                    {
                        lblError.Text = "Real Money can not be blank";
                        lblrealmoneypn.Focus();
                        break;
                    }
                    else
                    {
                        ds = new SmartPortal.SEMS.User().Updatecardtop(Utility.KillSqlInjection(lblcardidpn.Text.Trim()), Utility.KillSqlInjection(lblshortnamepn.Text.Trim()),
                        Utility.KillSqlInjection(lblcardamountpn.Text.Trim()), Utility.KillSqlInjection(lblrealmoneypn.Text.Trim()),
                        lblccyidpn.SelectedValue, lblstatuspn.SelectedValue, lbltypepn.SelectedValue,
                        ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE == "0")
                        {
                            lblError.Text = "Update successful";
                            btsave.Enabled = false;
                            lblcardidpn.Enabled = false;
                            lblshortnamepn.Enabled = false;
                            lblcardamountpn.Enabled = false;
                            lblrealmoneypn.Enabled = false;
                            lblccyidpn.Enabled = false;
                            lbltypepn.Enabled = false;
                            btnClear.Enabled = false;
                            lblstatuspn.Enabled = false;
                            break;
                        }
                        else
                        {
                            lblError.Text = "Card has been existed. Please input another card";
                            break;

                        }
                    }
                case "ADD":
                    if (lblshortnamepn.Text == "")
                    {
                        lblError.Text = "Short Name can not be blank";
                        lblshortnamepn.Focus();
                        break;
                    }
                    else if (lblcardamountpn.Text == "")
                    {
                        lblError.Text = "Card Amount can not be blank";
                        lblcardamountpn.Focus();
                        break;
                    }
                    else if (lblrealmoneypn.Text == "")
                    {
                        lblError.Text = "Real Money can not be blank";
                        lblrealmoneypn.Focus();
                        break;
                    }
                    else
                    {
                        ds = new SmartPortal.SEMS.User().Addcardtop(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["b"].ToString(),
                        Utility.KillSqlInjection(lblshortnamepn.Text.Trim()), Utility.isDecimal(lblcardamountpn.Text.Trim(), true),
                        Utility.isDecimal(lblrealmoneypn.Text.Trim(), true), lblccyidpn.SelectedValue,
                        "A", lbltypepn.SelectedValue, ref IPCERRORCODE, ref IPCERRORDESC);

                        if (IPCERRORDESC == "A")
                        {
                            lblError.Text = "Add card successful";
                            lblshortnamepn.Enabled = false;
                            lblcardamountpn.Enabled = false;
                            lblrealmoneypn.Enabled = false;
                            lblccyidpn.Enabled = false;
                            lbltypepn.Enabled = false;
                            btsave.Enabled = false;
                            lblstatuspn.Enabled = false;
                            break;
                        }
                        else
                        {
                            lblError.Text = "Card has been existed . Please input another card";
                            lblshortnamepn.Enabled = true;
                            lblcardamountpn.Enabled = true;
                            lblrealmoneypn.Enabled = true;
                            lblccyidpn.Enabled = true;
                            lbltypepn.Enabled = true;
                            btsave.Enabled = true;
                            break;
                        }

                    }
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void btback_OnClick(object sender, EventArgs e)
    {
        try
        {
            var urlb = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["b"] != null ? SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["b"].ToString() : "";
            if (urlb != "" )
            {
                string param = "&a=LIST&cid="+  SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["b"].ToString();
                RedirectBackToMainPage(param);
            }
            else
            {
                string link = PagesBLL.GetLinkMaster_Page("1108");
                Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(link));
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblshortnamepn.Enabled = true;
        lblshortnamepn.Text = string.Empty;
        lblccyidpn.Enabled = true;
        lblcardamountpn.Text = string.Empty;
        lblcardamountpn.Enabled = true;
        lbltypepn.Enabled = true;
        btsave.Enabled = true;
        lblrealmoneypn.Enabled = true;
        lblrealmoneypn.Text = string.Empty;
        lblstatuspn.Enabled = true;
        lblError.Text = string.Empty;
    }

}