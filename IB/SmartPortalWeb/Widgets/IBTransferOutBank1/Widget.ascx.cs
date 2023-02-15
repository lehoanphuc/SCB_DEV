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

using SmartPortal.ExceptionCollection;
using SmartPortal.Common.Utilities;

public partial class Widgets_IBTransferOutBank1_Widget : WidgetBase
{
    //string senderName = "";
    //string receiverName = "";
    //string balanceSender = "";
    //string acctCCYID = "";    


    DataSet ds = new DataSet();
    string IPCERRORCODE;
    string IPCERRORDESC;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //if (radAcctNo.Checked)
            //{
            //    txtReceiverAccount.Enabled = true;
            //    txtCMND.Enabled = false;
            //    txtissuedate.Enabled = false;
            //    txtissueplace.Enabled = false;
            //}
            //else
            //{
            //    txtReceiverAccount.Enabled = false;
            //    txtCMND.Enabled = true;
            //    txtissuedate.Enabled = true;
            //    txtissueplace.Enabled = true;
            //}

            if (!IsPostBack)
            {
                //txtCMND.Enabled = false;
                //txtissuedate.Enabled = false;
                //txtissueplace.Enabled = false;
                //radAcctNo.Checked = true;
                txtCMND.Text = "";
                txtReceiverAccount.Text = "";
                txtissuedate.Text = "";
                txtissueplace.Text = "";

                txtAmount.Attributes.Add("onkeyup", "ntt('" + txtAmount.ClientID + "','" + lblText.ClientID + "',event)");

                load_unit(sender, e);
                //hide panel
                pnConfirm.Visible = false;
                pnOTP.Visible = false;
                pnResultTransaction.Visible = false;

                LayThongTinNguoiGui();

                //disable textbox nguoi thu huong
                if (ddlNguoiThuHuong.SelectedValue == "")
                {
                    txtReceiverAccount.Enabled = true;
                    txtReceiverAccount.Enabled = true;
                    txtCMND.Enabled = true;
                    txtissuedate.Enabled = true;
                    txtissueplace.Enabled = true;
                    txtReceiverName.Enabled = true;
                    txtReceiverAdd.Enabled = true;
                }
                else
                {
                    txtReceiverAccount.Enabled = false;
                    txtReceiverAccount.Enabled = false;
                    txtCMND.Enabled = false;
                    txtissuedate.Enabled = false;
                    txtissueplace.Enabled = false;
                    txtReceiverName.Enabled = true;
                    txtReceiverAdd.Enabled = false;
                }

            }
        }
        catch (Exception ex)
        {
        }
    }

    private void load_unit(object sender, EventArgs e)
    {
        string errorcode = "";
        string errorDesc = "";
        try
        {
            SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();
            DataSet dsReceiverList = new DataSet();
            dsReceiverList = objAcct.GetReceiverList(Session["userID"].ToString(), SmartPortal.Constant.IPC.TOB);

            //load city
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

            ddlBankRecieve.DataSource = dtBank;
            ddlBankRecieve.DataTextField = "BANKNAME";
            ddlBankRecieve.DataValueField = "BANKID";
            ddlBankRecieve.DataBind();

            string isSendReceiver = dsReceiverList.Tables[1].Rows[0]["ISRECEIVERLIST"].ToString();
            if (isSendReceiver.Equals("N"))
            {
                DataRow row = dsReceiverList.Tables[0].NewRow();
                row["ID"] = "";
                row["RECEIVERNAME"] = " other ";
                dsReceiverList.Tables[0].Rows.InsertAt(row, 0);
            }
            else
            {
                if (dsReceiverList == null || dsReceiverList.Tables.Count == 0 || dsReceiverList.Tables[0].Rows.Count == 0)
                {
                    throw new IPCException("4012");

                }
            }


            ds = objAcct.getAccount(Session["userID"].ToString(), "IB000206", "DD", ref errorcode, ref errorDesc);

            if (ds.Tables.Count == 0)
            {
                throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.AccountNotRegisted);
            }

            ddlSenderAccount.DataSource = ds;
            ddlSenderAccount.DataValueField = ds.Tables[0].Columns["ACCTNO"].ColumnName.ToString();
            ddlSenderAccount.DataBind();

            ddlNguoiThuHuong.DataSource = dsReceiverList.Tables[0];
            //add to common later
            ddlNguoiThuHuong.DataTextField = dsReceiverList.Tables[0].Columns["RECEIVERNAME"].ColumnName.ToString();
            ddlNguoiThuHuong.DataValueField = dsReceiverList.Tables[0].Columns["ID"].ColumnName.ToString();
            ddlNguoiThuHuong.DataBind();


            //ddlNguoiThuHuong_SelectedIndexChanged(sender, e);
            //ddlProvince_SelectedIndexChanged(sender, e);

            //lay ccyid
            LayCCYID();
            //Load Template của QuyenNPV

            if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["tid"] != null)
            {
                string TID = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["tid"].ToString();
                DataSet TemplateDS = new DataSet();
                TemplateDS = new SmartPortal.IB.Transfer().LoadtemplateByID(TID, ref IPCERRORCODE, ref IPCERRORDESC);
                if (TemplateDS.Tables[0].Rows.Count != 0)
                {
                    DataTable TemplateTB = TemplateDS.Tables[0];
                    ddlSenderAccount.SelectedValue = TemplateTB.Rows[0]["SENDERACCOUNT"].ToString();
                    txtSenderName.Text = TemplateTB.Rows[0]["SENDERNAME"].ToString();
                    ddlNguoiThuHuong.SelectedValue = TemplateTB.Rows[0]["RECEIVERID"].ToString(); 
                    if (TemplateTB.Rows[0]["RECEIVERACCOUNT"].ToString() == "Qua số CMND")
                    {
                        txtReceiverAccount.Text = "";
                    }
                    else
                    {
                        txtReceiverAccount.Text = TemplateTB.Rows[0]["RECEIVERACCOUNT"].ToString();
                    }
                    txtReceiverName.Text = TemplateTB.Rows[0]["RECEIVERNAME"].ToString();
                    txtReceiverAdd.Text = TemplateTB.Rows[0]["RECEIVERADD"].ToString();
                    txtCMND.Text = TemplateTB.Rows[0]["IDENTIFYNO"].ToString();
                    txtBranchDesc.Text = TemplateTB.Rows[0]["BRANCHDESC"].ToString();
                    if (txtCMND.Text.Trim() != "")
                    {
                        radAcctNo.Checked = false;
                        radCMND.Checked = true;
                        txtCMND.Enabled = true;
                        txtissuedate.Enabled = true;
                        txtissueplace.Enabled = true;
                    }
                    txtissuedate.Text = SmartPortal.Common.Utilities.Utility.FormatDatetime(TemplateTB.Rows[0]["ISSUEDATE"].ToString(), "dd/MM/yyyy");
                    txtissueplace.Text = TemplateTB.Rows[0]["ISSUEPLACE"].ToString();
                    txtAmount.Text =SmartPortal.Common.Utilities.Utility.FormatMoney(TemplateTB.Rows[0]["AMOUNT"].ToString(),TemplateTB.Rows[0]["CCYID"].ToString());
                    ddlProvince.SelectedValue = TemplateTB.Rows[0]["CITYCODE"].ToString();
                    ddlProvince_SelectedIndexChanged(sender, e);
                    ddlBankRecieve.SelectedValue = TemplateTB.Rows[0]["BANKCODE"].ToString();
                    //lay danh sach chi nhanh
                    ddlBankRecieve_SelectedIndexChanged(sender, e);
                    ddlChildBank.SelectedValue = TemplateTB.Rows[0]["BRANCH"].ToString();
                    lblCurrency.Text = TemplateTB.Rows[0]["CCYID"].ToString();
                    txtDesc.Text = TemplateTB.Rows[0]["DESCRIPTION"].ToString();
                }
            }
            ddlNguoiThuHuong_SelectedIndexChanged(sender, e);
            ddlProvince_SelectedIndexChanged(sender, e);

            //Load Template của QuyenNPV            
        }
        catch (BusinessExeption bex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["notregtransfer"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, bex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["notregtransfer"], Request.Url.Query);
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

    protected void btnTIBNext_Click(object sender, EventArgs e)
    {
        string senderName = "";
        string balanceSender = "";
        string acctCCYID = "";
        string phi = "0";

        try
        {
            if (radAcctNo.Checked)
            {
                pnConfirmAccount.Visible = true;
                pnConfirmCMND.Visible = false;
            }
            else
            {
                pnConfirmAccount.Visible = false;
                pnConfirmCMND.Visible = true;
            }

            //ktra ngân hàng
            if (ddlBankRecieve.SelectedValue == "")
            {
                throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.BANKERROR);
            }

            if ((cbmau.Checked == true && txttenmau.Text.ToString().Trim() != "") || (cbmau.Checked == false))
            {
                SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();
                Hashtable hasSender = objAcct.loadInfobyAcct(ddlSenderAccount.Text.Trim());
                if (hasSender[SmartPortal.Constant.IPC.IPCERRORCODE].Equals("0"))
                {
                    senderName = hasSender[SmartPortal.Constant.IPC.CUSTOMERNAME].ToString();
                    balanceSender = SmartPortal.Common.Utilities.Utility.FormatStringCore(hasSender[SmartPortal.Constant.IPC.AVAILABLEBALANCE].ToString());
                    acctCCYID = hasSender[SmartPortal.Constant.IPC.CCYID].ToString();
                    lblSenderBranch.Text = hasSender[SmartPortal.Constant.IPC.BRANCHID].ToString();
                }
                else
                {
                    lblTextError.Text = "Error : " + hasSender[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                    return;
                }

                //tinh phi
                DataTable dtBranchID = new SmartPortal.IB.Bank().GetBranchID(ddlChildBank.SelectedValue);
                if (dtBranchID.Rows.Count != 0)
                {
                    DataTable dtFee = new SmartPortal.IB.Bank().GetFee(Session["userID"].ToString(), "IB000206", SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtAmount.Text.Trim(), lblCurrency.Text), ddlSenderAccount.SelectedValue, dtBranchID.Rows[0]["CREDITBRACHID"].ToString(), lblCurrency.Text, ddlProvince.SelectedValue);

                    if (dtFee.Rows.Count != 0)
                    {
                        phi = SmartPortal.Common.Utilities.Utility.FormatMoney(dtFee.Rows[0]["FEEAMOUNT"].ToString(),acctCCYID);
                    }
                }

                // CHECK AMOUNT
                if (SmartPortal.Common.Utilities.Utility.isDouble(txtAmount.Text.Trim(),true) + SmartPortal.Common.Utilities.Utility.isDouble(phi,true) > SmartPortal.Common.Utilities.Utility.isDouble(balanceSender,true))
                {
                    lblTextError.Text = Resources.labels.amountinvalid;
                    return;
                }
                //// CHECK SAME ACCOUNT
                //if (!txtReceiverAccount.Text.Equals(""))
                //{
                //    SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();
                //    bool sameAcct = objAcct.CheckSameAccount(ddlSenderAccount.Text.ToString(), txtReceiverAccount.Text.Trim().ToString());
                //    if (!sameAcct)
                //    {
                //        lblTextError.Text = Resources.labels.Accountnotsame;
                //        return;
                //    }
                //}
                #region check same name template transfer
                if (cbmau.Checked == true && txttenmau.Text.ToString().Trim() != "")
                {
                    new SmartPortal.IB.Transfer().CheckNameTransferTemplate(Utility.KillSqlInjection(txttenmau.Text.Trim()), Session["userID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {

                    }
                    else
                    {
                        if (IPCERRORDESC == "110211")
                        {
                            throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.SAMENAMETRANSFERTEMPLATE);

                        }
                        else
                        {
                            throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
                        }
                    }
                }
                #endregion


                #region LOAD INFO


                lblLicense.Text = txtCMND.Text;
                lblIssuePlace.Text = txtissueplace.Text;
                lblIssueDate.Text = txtissuedate.Text;
                lblReceiverAccount.Text = txtReceiverAccount.Text;

                pnTIB.Visible = true;

                lblSenderAccount.Text = ddlSenderAccount.Text;
                lblSenderName.Text = txtSenderName.Text;
                lblBalanceSender.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(balanceSender,acctCCYID);
                lblSenderCCYID.Text = acctCCYID;

                lblReceiverName.Text = txtReceiverName.Text;
                lblConfirmReceiverAdd.Text = txtReceiverAdd.Text;

                //lblBank.Text = ddlBankRecieve.SelectedItem.Text;
                //lblProvince.Text = ddlProvince.SelectedItem.Text;
                lblConfirmChildBank.Text = ddlChildBank.SelectedItem.Text;

                //lblNation.Text = ddlNation.Text;
                lblAmountCCYID.Text = acctCCYID;
                lblAmount.Text = SmartPortal.Common.Utilities.Utility.FormatMoneyInputToView(txtAmount.Text,acctCCYID);
                lblDesc.Text =SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtDesc.Text);
                lblPhi.Text = rdPhi.SelectedValue.ToString();
                //tinh phi                
                lblPhiAmount.Text = phi;


                lblFeeCCYID.Text = acctCCYID;



                #endregion

                lblTextError.Text = "";
                pnConfirm.Visible = true;
                pnOTP.Visible = false;
                pnTIB.Visible = false;
                pnResultTransaction.Visible = false;

            }
            else
            {
                throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.UNNAMEDTRANSFERTEMPLATE);
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
    protected void ddlNguoiThuHuong_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtissuedate.Attributes.Add("onclick", " var cal = Calendar.setup({onSelect: function(cal) { cal.hide() } }); cal.manageFields(" + txtissuedate.ClientID + ", " + txtissuedate.ClientID + ", '%d/%m/%Y'); ");
        try
        {
            DataSet dsReceiverList = new DataSet();
            dsReceiverList = new SmartPortal.IB.Account().GetReceiverList(Session["userID"].ToString(), SmartPortal.Constant.IPC.TOB);

            DataRow[] dr = dsReceiverList.Tables[0].Select("ID = '" + ddlNguoiThuHuong.SelectedValue.ToString() + "'");
            if (dr.Length > 0 && !dr[0]["ID"].Equals(""))
            {
                SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();
                txtReceiverAccount.Text = dr[0][SmartPortal.Constant.IPC.ACCTNO].ToString();
                //radAcctNo.Checked = true;
                //txtReceiverName.Text = dr[0][SmartPortal.Constant.IPC.RECEIVERNAME].ToString();
                txtCMND.Text = dr[0][SmartPortal.Constant.IPC.LICENSE].ToString();
                txtissueplace.Text = dr[0][SmartPortal.Constant.IPC.ISSUEPLACE].ToString();
                if (dr[0][SmartPortal.Constant.IPC.ISSUEDATE].ToString().Trim().Equals("") || dr[0][SmartPortal.Constant.IPC.ISSUEDATE] == null) { txtissuedate.Text = ""; }
                else
                {
                    txtissuedate.Text = SmartPortal.Common.Utilities.Utility.FormatDatetime(dr[0][SmartPortal.Constant.IPC.ISSUEDATE].ToString(), "dd/MM/yyyy");
                }
                txtReceiverName.Text = dr[0][SmartPortal.Constant.IPC.RECEIVERNAME].ToString();
                txtReceiverAdd.Text = dr[0][SmartPortal.Constant.IPC.ADDRESS].ToString();

                if (dr[0][SmartPortal.Constant.IPC.ACCTNO].ToString().Trim() != "")
                {
                    ddlProvince.SelectedValue = dr[0][SmartPortal.Constant.IPC.CITYCODE].ToString();
                    ddlBankRecieve.SelectedValue = dr[0][SmartPortal.Constant.IPC.BANKID].ToString();
                    ddlBankRecieve_SelectedIndexChanged(sender, e);
                    ddlChildBank.SelectedValue = dr[0]["BRANCH"].ToString();
                    txtBranchDesc.Text = dr[0][SmartPortal.Constant.IPC.BRANCHDESC].ToString();

                    txtBranchDesc.Enabled = false;
                    ddlProvince.Enabled = false;
                    ddlBankRecieve.Enabled = false;
                    ddlChildBank.Enabled = false;

                    radAcctNo.Checked = true;
                    radCMND.Checked = false;

                    radAcctNo.Enabled = false;
                    radCMND.Enabled = false;
                }
                else
                {
                    txtBranchDesc.Enabled = true;
                    ddlProvince.Enabled = true;
                    ddlBankRecieve.Enabled = true;
                    ddlChildBank.Enabled = true;

                    radCMND.Checked = true;
                    radAcctNo.Checked = false;

                    radAcctNo.Enabled = false;
                    radCMND.Enabled = false;
                }

                //lblLicense.Text = txtCMND.Text;
                //lblIssuePlace.Text = txtissueplace.Text;
                //lblIssueDate.Text = txtissuedate.Text;
                //lblReceiverAccount.Text = txtReceiverAccount.Text;

                txtReceiverAccount.Enabled = false;
                txtReceiverAccount.Enabled = false;
                txtCMND.Enabled = false;
                txtissuedate.Enabled = false;
                txtissueplace.Enabled = false;
                txtReceiverName.Enabled = true;
                txtReceiverAdd.Enabled = false;


            }
            else
            {
                txtReceiverAccount.Text = "";
                txtCMND.Text = "";
                txtissuedate.Text = "";
                txtissueplace.Text = "";
                txtReceiverName.Text = "";
                txtReceiverAdd.Text = "";
                txtBranchDesc.Text = "";

                txtReceiverAccount.Enabled = true;
                txtReceiverAccount.Enabled = true;
                txtCMND.Enabled = true;
                txtissuedate.Enabled = true;
                txtissueplace.Enabled = true;
                txtReceiverName.Enabled = true;
                txtReceiverAdd.Enabled = true;
                radAcctNo.Enabled = true;
                radCMND.Enabled = true;
                ddlProvince.Enabled = true;
                ddlBankRecieve.Enabled = true;
                ddlChildBank.Enabled = true;
                txtBranchDesc.Enabled = true;
            }

            //txtReceiverAccount.Text = string.Empty;
            //txtCMND.Text = "";
            //txtissuedate.Text = "";
            //txtissueplace.Text = "";
            //txtReceiverAdd.Text = "";
            //txtReceiverName.Text = string.Empty;
            //txtCMND.Enabled = true;
            //txtissuedate.Enabled = true;
            //txtissueplace.Enabled = true;
            //txtReceiverName.Enabled = true;
            //txtReceiverAccount.Enabled = true;


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
        // return;
    }
    protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet dsBankList = new DataSet();
        dsBankList = new SmartPortal.IB.Account().GetBankList(ddlProvince.SelectedValue, ddlBankRecieve.SelectedValue);


        try
        {
            //DataRow[] dr = dsCity.Tables[0].Select(SmartPortal.Constant.IPC.CITYCODE + " = '" + ddlProvince.Text + "'");
            //if (dr.Length > 0)
            //{
            //DataRow[] drBank = dsBankList.Tables[0].Select(SmartPortal.Constant.IPC.CITYCODE + " = '" + dr[0][SmartPortal.Constant.IPC.CITYCODE] + "'");
            //foreach (DataRow row in drBank)
            //{
            //    temp.ImportRow(row);
            //}
            if (dsBankList.Tables[0].Rows.Count == 0)//|| temp.Columns[SmartPortal.Constant.IPC.BANKCODE].ColumnName.ToString() = null)
            {
                ddlChildBank.Items.Clear();
                ddlChildBank.Items.Insert(0, new ListItem(Resources.labels.chuacodanhsachnganhang, ""));
            }
            else
            {
                ddlChildBank.DataSource = dsBankList.Tables[0];
                ddlChildBank.DataTextField = dsBankList.Tables[0].Columns[SmartPortal.Constant.IPC.BANKNAME].ColumnName.ToString();
                ddlChildBank.DataValueField = dsBankList.Tables[0].Columns[SmartPortal.Constant.IPC.BANKCODE].ColumnName.ToString();
                ddlChildBank.DataBind();
            }

            //}
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
    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            pnConfirm.Visible = false;
            pnOTP.Visible = false;
            pnTIB.Visible = true;
            pnResultTransaction.Visible = false;
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
    protected void btnApply_Click(object sender, EventArgs e)
    {
        try
        {
            if (int.Parse(Session["userLevel"].ToString().Trim()) > 5)
            {
                btnApply.Text = Resources.labels.transfer;
                //chuyen khoan luon doi voi user doanh nghiep level3 tro len
                btnAction_Click(sender, e);
            }
            else
            {
                btnApply.Text = Resources.labels.confirm;
                pnConfirm.Visible = false;
                pnOTP.Visible = true;
                pnTIB.Visible = false;

                SmartPortal.IB.Transactions objTran = new SmartPortal.IB.Transactions();
                DataTable dt = new DataTable();
                dt = objTran.LoadAuthenType(Session["userID"].ToString());
                ddlLoaiXacThuc.DataSource = dt;
                ddlLoaiXacThuc.DataTextField = "TYPENAME";
                ddlLoaiXacThuc.DataValueField = "AUTHENTYPE";
                ddlLoaiXacThuc.DataBind();
            }

        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_IBTransferOutBank1_Widget", "btnApply_Click", IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_IBTransferOutBank1_Widget", "btnApply_Click", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btnBackTransfer_Click(object sender, EventArgs e)
    {
        try
        {
            pnConfirm.Visible = true;
            pnOTP.Visible = false;
            pnTIB.Visible = false;
            pnResultTransaction.Visible = false;
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
    protected void btnAction_Click(object sender, EventArgs e)
    {
        try
        {
            string receiverAcctno = "";
            if (radCMND.Checked)
            {
                receiverAcctno = "";
            }
            else
            {
                receiverAcctno = lblReceiverAccount.Text;
            }

            string desc = "";
            if (radCMND.Checked)
            {
                desc =SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtDesc.Text) + " - "+Resources.labels.chuyenquasochungminh + txtCMND.Text + " , "+Resources.labels.ngaycap + txtissuedate.Text + " , "+Resources.labels.noicap + txtissueplace.Text+"#####"+SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtBranchDesc.Text);
            }
            else
            {
                desc =SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtDesc.Text) + "#####" +SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtBranchDesc.Text);
            }

            SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();
            Hashtable result = objAcct.TransferOutBank(Session["userID"].ToString(), lblSenderAccount.Text, lblSenderName.Text, receiverAcctno
                                                    , lblLicense.Text, lblIssuePlace.Text, lblIssueDate.Text, lblReceiverName.Text, lblConfirmReceiverAdd.Text
                                                    , lblSenderBranch.Text, ddlBankRecieve.SelectedValue, ddlChildBank.SelectedValue, ddlProvince.SelectedValue, SmartPortal.Common.Utilities.Utility.FormatMoneyInput(lblPhiAmount.Text, lblSenderCCYID.Text.ToString()), SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtAmount.Text, lblSenderCCYID.Text.ToString()), lblSenderCCYID.Text.ToString(), desc,(cbQuaNgay.Checked==true)?"Y":"N", ddlLoaiXacThuc.SelectedValue.ToString(), txtOTP.Text.Trim());



            if (result[SmartPortal.Constant.IPC.IPCERRORCODE].Equals("0"))
            {
                lblTextError.Text = Resources.labels.transactionsuccessful;
                if (cbmau.Checked == true)
                {

                    string fee = rdPhi.SelectedIndex.ToString();
                    string Recei_Acct_CMND = "";
                    if (radCMND.Checked == true)
                    {
                        Recei_Acct_CMND = Resources.labels.quasocmnd;
                    }
                    else { Recei_Acct_CMND = lblReceiverAccount.Text; }
                    new SmartPortal.IB.Transfer().InsertTransferTemplate(txttenmau.Text, txtDesc.Text, ddlSenderAccount.Text, Recei_Acct_CMND, SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtAmount.Text, lblSenderCCYID.Text), lblSenderCCYID.Text, SmartPortal.Constant.IPC.TOB, Session["userID"].ToString(), "0", "", fee, ddlProvince.SelectedValue, ddlNation.SelectedValue, lblLicense.Text, lblIssueDate.Text, lblIssuePlace.Text, lblReceiverName.Text, lblSenderName.Text, ddlBankRecieve.SelectedValue, lblConfirmReceiverAdd.Text, ddlChildBank.SelectedValue, txtBranchDesc.Text, ddlNguoiThuHuong.SelectedValue, ref IPCERRORCODE, ref IPCERRORDESC);

                    if (IPCERRORCODE == "0")
                    {
                        lblTextError.Text += " & "+Resources.labels.luumauchuyenkhoanthanhcong;
                    }
                    else
                    {
                        throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
                    }
                }

                //ghi vo session dung in
                Hashtable hasPrint = new Hashtable();
                if (radAcctNo.Checked)
                {
                    hasPrint.Add("isAccount", "Y");
                }
                else
                {
                    hasPrint.Add("isAccount", "N");
                }
                hasPrint.Add("senderAccount", lblSenderAccount.Text);
                hasPrint.Add("senderBalance", lblBalanceSender.Text);
                hasPrint.Add("ccyid", lblSenderCCYID.Text);
                hasPrint.Add("senderName", lblSenderName.Text);
                hasPrint.Add("recieverAccount", lblReceiverAccount.Text);
                hasPrint.Add("recieverName", lblReceiverName.Text);
                hasPrint.Add("recieverAdd", txtReceiverAdd.Text);
                hasPrint.Add("recieverBank", lblConfirmChildBank.Text);
                //hasPrint.Add("recieverCity", lblProvince.Text);
                //hasPrint.Add("recieverNation", lblNation.Text);
                hasPrint.Add("CMND", txtCMND.Text);
                hasPrint.Add("issuePlace", lblIssuePlace.Text);
                hasPrint.Add("issuseDate", lblIssueDate.Text);
                hasPrint.Add("amount", lblAmount.Text);
                hasPrint.Add("amountchu", txtChu.Value.ToString());
                hasPrint.Add("feeType", lblPhi.Text);
                hasPrint.Add("feeAmount", lblPhiAmount.Text);
                hasPrint.Add("desc", lblDesc.Text);
                hasPrint.Add("tranID", result["IPCTRANSID"] != null ? result["IPCTRANSID"].ToString() : "");
                hasPrint.Add("tranDate", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                hasPrint.Add("senderBranch", SmartPortal.Common.Utilities.Utility.FormatStringCore(lblSenderBranch.Text));
                Session["print"] = hasPrint;

                btnPrint.Visible = true;
                btnView.Visible = true;
            }
            else
            {
                txtOTP.Text = "";
                btnPrint.Visible = false;
                btnView.Visible = false;
                switch (result[SmartPortal.Constant.IPC.IPCERRORCODE].ToString().Trim())
                {
                    case SmartPortal.Constant.IPC.ERRORCODE.OTPINVALID:
                        lblTextError.Text = result[SmartPortal.Constant.IPC.IPCERRORDESC].ToString().Trim();
                        return;
                    case SmartPortal.Constant.IPC.ERRORCODE.WATTINGBANKAPPROVE:
                        lblTextError.Text = Resources.labels.wattingbankapprove;
                        if (cbmau.Checked == true)
                        {

                            string fee = rdPhi.SelectedIndex.ToString();
                            string Recei_Acct_CMND = "";
                            if (radCMND.Checked == true)
                            {
                                Recei_Acct_CMND = Resources.labels.quasocmnd;
                            }
                            else { Recei_Acct_CMND = lblReceiverAccount.Text; }
                            new SmartPortal.IB.Transfer().InsertTransferTemplate(txttenmau.Text, txtDesc.Text, ddlSenderAccount.Text, Recei_Acct_CMND, SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtAmount.Text, lblSenderCCYID.Text), lblSenderCCYID.Text, SmartPortal.Constant.IPC.TOB, Session["userID"].ToString(), "0", "", fee, ddlProvince.SelectedValue, ddlNation.SelectedValue, lblLicense.Text, lblIssueDate.Text, lblIssuePlace.Text, lblReceiverName.Text, lblSenderName.Text, ddlBankRecieve.SelectedValue, lblConfirmReceiverAdd.Text, ddlChildBank.SelectedValue, txtBranchDesc.Text, ddlNguoiThuHuong.SelectedValue, ref IPCERRORCODE, ref IPCERRORDESC);

                            if (IPCERRORCODE == "0")
                            {
                                lblTextError.Text += " & "+Resources.labels.luumauchuyenkhoanthanhcong;
                            }
                            else
                            {
                                throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
                            }
                        }
                        break;
                    case SmartPortal.Constant.IPC.ERRORCODE.WATTINGUSERAPPROVE:
                        lblTextError.Text = Resources.labels.wattinguserapprove;
                        if (cbmau.Checked == true)
                        {

                            string fee = rdPhi.SelectedIndex.ToString();
                            string Recei_Acct_CMND = "";
                            if (radCMND.Checked == true)
                            {
                                Recei_Acct_CMND = Resources.labels.quasocmnd;
                            }
                            else { Recei_Acct_CMND = lblReceiverAccount.Text; }
                            new SmartPortal.IB.Transfer().InsertTransferTemplate(txttenmau.Text, txtDesc.Text, ddlSenderAccount.Text, Recei_Acct_CMND, SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtAmount.Text, lblSenderCCYID.Text), lblSenderCCYID.Text, SmartPortal.Constant.IPC.TOB, Session["userID"].ToString(), "0", "", fee, ddlProvince.SelectedValue, ddlNation.SelectedValue, lblLicense.Text, lblIssueDate.Text, lblIssuePlace.Text, lblReceiverName.Text, lblSenderName.Text, ddlBankRecieve.SelectedValue, lblConfirmReceiverAdd.Text, ddlChildBank.SelectedValue, txtBranchDesc.Text, ddlNguoiThuHuong.SelectedValue, ref IPCERRORCODE, ref IPCERRORDESC);

                            if (IPCERRORCODE == "0")
                            {
                                lblTextError.Text += " & " + Resources.labels.luumauchuyenkhoanthanhcong;
                            }
                            else
                            {
                                throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
                            }
                        }
                        break;
                    case SmartPortal.Constant.IPC.ERRORCODE.NOTREGOTP:
                        lblTextError.Text = Resources.labels.notregotp;
                        return;
                    case SmartPortal.Constant.IPC.ERRORCODE.AUTHENTYPEINVALID:
                        lblTextError.Text = Resources.labels.authentypeinvalid;
                        return;
                    case "9908":
                        lblTextError.Text = Resources.labels.sotienvuothanmucgiaodich;
                        return;
                    case "9909":
                        lblTextError.Text = Resources.labels.sotienvuotquatonghanmuctrongngay;
                        return;
                    case "9907":
                        lblTextError.Text = Resources.labels.solangiaodichvuotquasolangiaodichtrongngay;
                        return;
                    default:
                        throw new Exception();
                }

                pnConfirm.Visible = true;
                btnApply.Enabled = false;
                btnBack.Enabled = false;

            }

            pnConfirm.Visible = false;
            pnOTP.Visible = false;
            pnTIB.Visible = false;
            pnResultTransaction.Visible = true;



            #region LOAD RESULT TRANSFER
            if (radAcctNo.Checked)
            {
                pnEndAccount.Visible = true;
                pnEndCMND.Visible = false;
            }
            else
            {
                pnEndAccount.Visible = false;
                pnEndCMND.Visible = true;
            }

            lblEndSenderAccount.Text = lblSenderAccount.Text;

            string errorCode = "0";
            string errorDesc = string.Empty;
            SmartPortal.IB.Account acct = new SmartPortal.IB.Account();
            DataSet dsDetailAcc = new DataSet();
            dsDetailAcc = acct.GetInfoDD(Session["userID"].ToString(), lblSenderAccount.Text.Trim(), ref errorCode, ref errorDesc);

            if (errorCode == "0")
            {
                lblEndBalanceSender.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(dsDetailAcc.Tables[0].Rows[0][SmartPortal.Constant.IPC.AVAILABLEBALANCE].ToString()), lblSenderCCYID.Text.Trim());
            }
            else
            {
                lblEndBalanceSender.Text = SmartPortal.Common.Utilities.Utility.FormatMoney((SmartPortal.Common.Utilities.Utility.isDouble(lblBalanceSender.Text.ToString(),true) - SmartPortal.Common.Utilities.Utility.isDouble(lblAmount.Text,true)).ToString(), lblSenderCCYID.Text.Trim());
            }

            lblEndSenderCCYID.Text = lblSenderCCYID.Text;
            lbEndCCYID.Text = lblSenderCCYID.Text;
            lblEndFeeCCYID.Text = lblSenderCCYID.Text;
            lblEndSenderName.Text = txtSenderName.Text;
            lblEndLicense.Text = lblLicense.Text;
            lblEndIssuePlace.Text = lblIssuePlace.Text;
            lblEndIssueDate.Text = lblIssueDate.Text;
            lblEndReceiverAccount.Text = lblReceiverAccount.Text;
            lblEndReceiverName.Text = lblReceiverName.Text;
            lblEndReceiverAdd.Text = lblConfirmReceiverAdd.Text;
            //lblEndBank.Text = lblBank.Text;
            //lblEndProvince.Text = lblProvince.Text;
            lblEndChildBank.Text = ddlChildBank.SelectedItem.Text;
            //lblEndNation.Text = lblNation.Text;
            lblEndAmount.Text = lblAmount.Text;
            lblEndPhi.Text = lblPhi.Text;
            lblEndPhiAmount.Text = lblPhiAmount.Text;
            lblEndDesc.Text = lblDesc.Text;
            #endregion
            txtOTP.Text = "";
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
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?po=3&p=90"));
    }
    public void LayThongTinNguoiGui()
    {
        try
        {
            SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();
            Hashtable hasSender = objAcct.loadInfobyAcct(ddlSenderAccount.Text.Trim());
            if (hasSender[SmartPortal.Constant.IPC.IPCERRORCODE].Equals("0"))
            {
                txtSenderName.Text = hasSender[SmartPortal.Constant.IPC.CUSTOMERNAME].ToString();
            }
            else
            {
                throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.IPC);
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

    protected void ddlSenderAccount_SelectedIndexChanged(object sender, EventArgs e)
    {
        //LayThongTinNguoiGui();
        LayCCYID();
    }

    public void LayCCYID()
    {
        DataTable tblAcctnoInfo = new SmartPortal.IB.Account().GetAcctnoInfo(ddlSenderAccount.SelectedValue, Session["Userid"].ToString());
        if (tblAcctnoInfo.Rows.Count != 0)
        {
            lblCurrency.Text = tblAcctnoInfo.Rows[0]["CCYID"].ToString();
        }
    }
    protected void ddlBankRecieve_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet dsBankList = new DataSet();
        dsBankList = new SmartPortal.IB.Account().GetBankList(ddlProvince.SelectedValue, ddlBankRecieve.SelectedValue);

        try
        {
            //DataRow[] dr = dsCity.Tables[0].Select(SmartPortal.Constant.IPC.CITYCODE + " = '" + ddlProvince.Text + "'");
            //if (dr.Length > 0)
            //{
            //DataRow[] drBank = dsBankList.Tables[0].Select(SmartPortal.Constant.IPC.CITYCODE + " = '" + dr[0][SmartPortal.Constant.IPC.CITYCODE] + "'");
            //foreach (DataRow row in drBank)
            //{
            //    temp.ImportRow(row);
            //}
            if (dsBankList.Tables[0].Rows.Count == 0)//|| temp.Columns[SmartPortal.Constant.IPC.BANKCODE].ColumnName.ToString() = null)
            {
                ddlChildBank.Items.Clear();
                ddlChildBank.Items.Insert(0, new ListItem(Resources.labels.chuacodanhsachnganhang, ""));
            }
            else
            {
                ddlChildBank.DataSource = dsBankList.Tables[0];
                ddlChildBank.DataTextField = dsBankList.Tables[0].Columns[SmartPortal.Constant.IPC.BANKNAME].ColumnName.ToString();
                ddlChildBank.DataValueField = dsBankList.Tables[0].Columns[SmartPortal.Constant.IPC.BANKCODE].ColumnName.ToString();
                ddlChildBank.DataBind();
            }

            //}
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
    protected void btnView_Click(object sender, EventArgs e)
    {
        //15.12.2015 minh add to fix error 9999 
        if (Session["print"] == null)
        {
            Response.Redirect("~/" + System.Configuration.ConfigurationManager.AppSettings["loginpage"]);
        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        //15.12.2015 minh add to fix error 9999 
        if (Session["print"] == null)
        {
            Response.Redirect("~/" + System.Configuration.ConfigurationManager.AppSettings["loginpage"]);
        }
    }
}
