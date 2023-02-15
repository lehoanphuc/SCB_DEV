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


public partial class Widgets_IBAccountList_Widget : WidgetBase
{
    Thread th;
    private Object m_lock = new Object();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userLevel"].ToString() == "3")
        {
            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("Default.aspx?p=336&f=huongdansudungqtht"), false);
        }
        if (!IsPostBack)
        {
            th = new Thread(() =>
            {
                LoadAccountList();
            });
            th.Start();
        }
        //LoadAccountList();
    }
    protected void LoadAccountList()
    {
        //vutt 07092015 speedup account list page
        lock (m_lock)
        {
            try
            {
                string strAccountList = string.Empty;
                Session["strAccountList"] = null;

                string errorCode = string.Empty;
                string errorDesc = string.Empty;
                string AccountList = string.Empty;
                string CfCode = string.Empty;
                string CFType = string.Empty;
                DataSet dsAcctEB = new DataSet();
                DataSet dsAcctCore = new DataSet();
                DataSet dsCustInfo = new DataSet();
                //Bang chua thong tin tai khoan
                DataTable accountTable = new DataTable();
                accountTable.Columns.AddRange(new DataColumn[] { new DataColumn("Account"), new DataColumn("AccountName"), new DataColumn("Desc"), new DataColumn("AvailableBalance") , new DataColumn("Balance") , new DataColumn("Type") , new DataColumn("PocketAmt") });
                string Status = string.Empty;
                string TypeID = string.Empty;

                SmartPortal.IB.Account acct = new SmartPortal.IB.Account();
                dsAcctEB = acct.getAccount(Session["userID"].ToString(), "IB0002021", "", ref errorCode, ref errorDesc);
                DataRow[] dr;
                if (errorCode == "0")
                {
                    dsCustInfo = acct.GetCustIDCustType(Session["userID"].ToString(), ref errorCode, ref errorDesc);
                    if (errorCode == "0" && dsCustInfo.Tables[0].Rows.Count == 1)
                    {
                        CfCode = dsCustInfo.Tables[0].Rows[0]["CFCODE"].ToString().Replace(" ", "");
                        CFType = dsCustInfo.Tables[0].Rows[0]["CFTYPE"].ToString().Replace(" ", "");
                        dsAcctCore = acct.GetTKKH(CfCode.Replace(" ", ""), CFType.Replace(" ", ""), Session["userID"].ToString(), "IB0002021", "IBWL_INQUIRY", ref errorCode, ref errorDesc);

                        if (dsAcctCore != null && dsAcctCore.Tables[0].Rows.Count > 0)
                        {
                            dr = dsAcctCore.Tables[0].Select("statuscd in" + "('CLS')");
                            for (int k = 0; k < dr.Length; k++)
                            {
                                string clsAcct = dr[k]["accountno"].ToString();
                                if (dsAcctEB.Tables[0].Select("ACCTNO = '" + clsAcct + "' AND Status = 'CLS'").Length > 0)
                                {
                                    acct.UpdateCloseAcct(dr[k]["accountno"].ToString(), dr[k]["statuscd"].ToString(), ref errorCode, ref errorDesc);
                                }
                            }

                            dr = dsAcctCore.Tables[0].Select("");
                            for (int k = 0; k < dr.Length; k++)
                            {
                                string strQuerry = "ACCTNO = '" + dr[k]["accountno"].ToString().Trim()
                                    + "' AND Status = '" + dr[k]["statuscd"].ToString() + "'";
                                if (dsAcctEB.Tables[0].Select(strQuerry).Length == 0)
                                {
                                    acct.UpdateAcct(dr[k]["accountno"].ToString(), dr[k]["typeid"].ToString(), dr[k]["statuscd"].ToString(), dr[k]["ccyid"].ToString()
                                    , SmartPortal.Common.Utilities.Utility.FormatStringCore(dr[k]["BRID"].ToString()), CfCode
                                    , CFType, ref errorCode, ref errorDesc);
                                }
                            }
                        }
                    }
                }
                //vutran 07092015 thay doi cach lay danh sach tai khoan de tang toc do
                dsAcctEB = acct.getAccount(Session["userID"].ToString(), "IB0002021", "", ref errorCode, ref errorDesc);
                List<string> lsright = new List<string>();
                lsright = dsAcctEB.Tables[0].Rows.OfType<DataRow>().Select(drright => drright.Field<string>("ACCTNO")).ToList();

                if (errorCode == "0" && dsAcctCore != null && dsAcctCore.Tables.Count > 0)
                {
                    foreach (DataRow drAcct in dsAcctCore.Tables[0].Rows)
                    {
                        DataRow r = accountTable.NewRow();
                        Status = drAcct["Statuscd"].ToString();
                        TypeID = drAcct["typeid"].ToString();
                        if (String.IsNullOrEmpty(Status) == false && Status.ToUpper() != "CLS" && Status.ToUpper() != "S")
                        {
                            if (lsright.Contains(drAcct["accountno"].ToString()))
                            {
                                r["Account"] = drAcct["accountno"].ToString();
                                switch (TypeID)
                                {
                                    case "CD":
                                        r["Desc"] = Resources.labels.tktt;
                                        break;
                                    case "DD":
                                        r["Desc"] = Resources.labels.taikhoantietkiemonline;
                                        break;
                                    case "FD":
                                        r["Desc"] = Resources.labels.taikhoantietkiemcokyhan;
                                        break;
                                    case "LN":
                                        r["Desc"] = Resources.labels.taikhoanloan;
                                        break;
                                    case "WLM":
                                        r["Desc"] = Resources.labels.ewallet;
                                        break;
                                    default:
                                        r["Desc"] = "";
                                        break;
                                }
								//if (drAcct["ccyid"].Equals("LAK")) drAcct["ccyid"] = "VND";
                                r["AvailableBalance"] = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(drAcct["availablebal"].ToString().Trim()), drAcct["ccyid"].ToString().Trim()) + " " + drAcct["ccyid"].ToString().Trim();
                                r["Balance"] = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(drAcct["balance"].ToString().Trim()), drAcct["ccyid"].ToString().Trim()) + " " + drAcct["ccyid"].ToString().Trim();
                                r["Type"] = TypeID;
                                r["PocketAmt"] = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(drAcct["POCKETAMT"].ToString().Trim()), drAcct["ccyid"].ToString().Trim()) + " " + drAcct["ccyid"].ToString().Trim();
                                switch (TypeID)
                                {
                                    case "WLM":
                                        Hashtable hashtable = acct.GetInfoAccount(Session["userID"].ToString(), drAcct["accountno"].ToString(), ref errorCode, ref errorDesc);
                                        if (errorCode.Equals("0"))
                                        {
                                            r["accountName"] = hashtable[SmartPortal.Constant.IPC.FULLNAME].ToString();
                                        }
                                        else
                                        {
                                            throw new SmartPortal.ExceptionCollection.IPCException(errorCode);
                                        }
                                        break;
                                    default:
                                        r["accountName"] = drAcct["accountname"].ToString();
                                        break;
                                }
                                accountTable.Rows.Add(r);
                            }
                        }
                    }
                    if (accountTable.Rows.Count > 0)
                    {
                        //xuat len man hinh
                        StringBuilder sT = new StringBuilder();

                        foreach (DataRow row in accountTable.Rows)
                        {
                            switch (row["Type"].ToString())
                            {
                                case "WLM":
                                    sT.Append(@"<div class=""col-xs-12 col-sm-6 col-md-4"" style=""padding: 10px;""> 
		                                <div class=""a1"">
                                            <a href=""" + SmartPortal.Common.Encrypt.EncryptURL("?p=87&ACCTNO=" + row["Account"].ToString()) + @""" class=""s1"">
			                                    <img class=""img-responsive"" src='Images/card-scb.png' alt=""card"" style=""border-radius: 13px; width: 100%"">
			                                    <div class=""a2"">
                                                    <div class=""b0"">
                                                        <div>
						                                    <b class=""acctype"" style='float: left; padding-left: 10px;'>" + row["Desc"].ToString() + @" Account</b>
					                                    </div>
                                                    </div>
				                                    <div class=""row b1"" style= 'margin-top: 5px;'> 
					                                    
                                                    </div>
													<div class=""b2"" style= 'margin-top: 35px;' >
														<div>
						                                    <span >Account Number</span>
					                                    </div>
													</div>
													<div class=""b3"" style= 'margin-top: 50px;'>
														<div>
						                                    <strong >" + row["Account"].ToString() + @"</strong>
					                                    </div>
													</div>
													<div class=""b4"" style= 'margin-top: 70px;' >
														<div>
						                                    <span >Name</span>
					                                    </div>
													</div>
													<div class=""b5"" style= 'margin-top: 85px;'>
														<div>
						                                    <strong >" + row["accountName"].ToString() + @"</strong>
					                                    </div>
													</div>
													<div class=""b6"">
														<div>
						                                    <span >Balance</span>
					                                    </div>
													</div>
			                                    </div>
	                                        </a>
		                                </div>
										<div class=""b7"" style=""float: left; font-style: normal;font-weight: 700;font-size: 13px;line-height: 15px;"">
                                            <div>
                                                <i id=""btn" + row["Account"].ToString() + @""" onclick=""abc(this.id)"" style=""background-color: transparent; background-repeat: no-repeat; border: none; cursor: pointer; overflow: hidden; outline: none;"">
                                                    <span id=""spn" + row["Account"].ToString() + @""" class=""fa fa-fw fa-eye-slash""></span> 
                                                </i>
                                                <strong style=""display: none; color: #FAB114"" id=""blc" + row["Account"].ToString() + @"""> " + row["Balance"].ToString() + @"</strong>
                                                <strong style=""display: contents"" id=""blch" + row["Account"].ToString() + @""">********* </strong>
                                            </div>
                                        </div>
                                </div>");
                                    break;
                                default:
                                    sT.Append(@"<div class=""col-xs-12 col-sm-6 col-md-4"" style=""padding: 10px;""> 
		                                <div class=""a1"">
                                            <a href=""" + SmartPortal.Common.Encrypt.EncryptURL("?p=87&ACCTNO=" + row["Account"].ToString()) + @""" class=""s1"">
                                                <img class=""img-responsive"" src=""Images/" + (row["Balance"].ToString().Contains("USD") ? "card-scb.png" : row["Balance"].ToString().Contains("THB") ? "card-scb.png" : "card-scb.png" ) + @""" alt=""card"" style=""border-radius: 13px;  width: 100%"">		
                                                <div class=""a2"">
                                                    <div class=""b0"">
                                                        <div>
						                                    <b class=""acctype"" style='float: left; margin-left: 10px; position: initial'>" + row["Desc"].ToString() + @" Account</b>
					                                    </div>
                                                    </div>
				                                    <div class=""row b1"" style= 'margin-top: 5px;'> 
                                                    </div>
													<div class=""b2"" style= 'margin-top: 35px;' >
														<div>
						                                    <span>Account Number</span>
					                                    </div>
													</div>
													<div class=""b3"" style= 'margin-top: 50px;'>
														<div>
						                                    <strong>" + row["Account"].ToString() + @"</strong>
					                                    </div>
													</div>
													<div class=""b4"" style= 'margin-top: 70px;' >
														<div>
						                                    <span>Name</span>
					                                    </div>
													</div>
													<div class=""b5"" style= 'margin-top: 85px;'>
														<div>
						                                    <strong>" + row["accountName"].ToString() + @"</strong>
					                                    </div>
													</div>
													<div class=""b6"">
														<div>
						                                    <span>Balance</span>
					                                    </div>
													</div>
			                                    </div>
	                                        </a>
		                                </div>
										<div class=""b7"" style=""float: left; font-style: normal;font-weight: 700;font-size: 13px;line-height: 15px;"">
                                            <div>
                                                <i id=""btn" + row["Account"].ToString() + @""" onclick=""abc(this.id)"" style=""background-color: transparent; background-repeat: no-repeat; border: none; cursor: pointer; overflow: hidden; outline: none;"">
                                                    <span id=""spn" + row["Account"].ToString() + @""" class=""fa fa-fw fa-eye-slash""></span> 
                                                </i>
                                                <strong style=""display: none; color: #FAB114"" id=""blc" + row["Account"].ToString() + @"""> " + row["Balance"].ToString() + @"</strong>
                                                <strong style=""display: contents"" id=""blch" + row["Account"].ToString() + @""">********* </strong>
                                            </div>
                                        </div>
                                </div>");
                                    break;
                            }
                        }

                        ltrDD.Text = sT.ToString();
                        strAccountList = sT.ToString();
                    }
                    else
                    {
                        if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["p"].Trim() == "86")
                        {
                            this.Visible = false;
                        }
                        else
                        {
                            StringBuilder sT = new StringBuilder();
                            sT.Append(Resources.labels.banchuadangkytaikhoan);

                            ltrDD.Text = sT.ToString();
                            strAccountList = sT.ToString();
                        }
                    }
                }
                else
                {
                    throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.IPC);
                }
                //Session["strAccountList"] = strAccountList;
                Session["strAccountList"] = (string.IsNullOrEmpty(strAccountList) ? "#" : strAccountList);
            }
            catch (IPCException IPCex)
            {
                Session["strAccountList"] = "#";
                SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), "Account list");
                //SmartPortal.Common.Log.GoToIBErrorPage(IPCex.ToString(), Request.Url.Query);

            }
            catch (Exception ex)
            {
                Session["strAccountList"] = "#";
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), "Account list");
                //SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

            }
        }
    }

    // private string GetCurrencyImage(string balance)
    // {
    //     if(!string.IsNullOrEmpty(Balance))
    //     {
    //         switch(balance.Substring(input.Length - 3))
    //         {
    //             case "USD":
    //                 return "acc2.png";
    //             case "TBH":
    //                 return "acc3.png";
    //             case "LAK":
    //                 return "acc4.png";
    //             default:
    //                 return "acc1.png";

    //         }
    //     }
    //     return "acc1.png";
    // }

    protected void OnGetAccountListFinish(object sender, EventArgs e)
    {
        if (Session["strAccountList"] != null && Session["strAccountList"] != "#")
        {
            ltrDD.Text = Session["strAccountList"].ToString();
            Timer1.Enabled = false;
            imgLoading.Visible = false;
            //th.Abort();
        }
        else if (Session["strAccountList"] == "#")
        {
            Timer1.Enabled = false;
            imgLoading.Visible = false;
            //th.Abort();
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "?po=3&p=86");
        }
    }
}
