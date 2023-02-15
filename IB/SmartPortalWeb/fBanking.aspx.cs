using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SmartPortal.Security;
using SmartPortal.Common.Utilities;
using System.IO;
using SmartPortal.Common;
using System.Configuration;
using SmartPortal.ExceptionCollection;
using System.Threading;


public partial class fBanking : System.Web.UI.Page
{
    string IPCERRORCODE;
    string IPCERRORDESC;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["fdata"] != null)
        {
            string fid = InsertTransaction(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["fdata"].ToString(), SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["fid"].ToString(), ref IPCERRORCODE);

            if (IPCERRORCODE == "0" && fid != "")
            {
                if (Session["userName"] == null)
                {
                    Response.Redirect("~/iblogin.aspx?fid=" + fid);
                }
                else if (Session["userName"].Equals("guest"))
                {
                    Response.Redirect("~/iblogin.aspx?fid=" + fid);
                }
                else
                {
                    Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?po=3&p=1000&fid=" + fid));
                }
            }
            else
            {
                switch(IPCERRORCODE)
                {
                    case "4997":
                        LbResult.Text = "Shop does not exist, please contact administrator for support";
                        break;
                    case "4996":
                        LbResult.Text = "Transaction was completed, please make new transaction.";
                        break;
                    case "4994":
                        LbResult.Text = "Transaction was expried, please make new transaction";
                        break;
                    default:
                        LbResult.Text="System error.";
                        break;
                }
                    
            }
        }
    }

    private string InsertTransaction(string fdata,string fid,ref string  errorcode)
    {
        string order_code = "";
        string shopcode = "";
        string amount = "";
        string ccyid = "";
        string trandesc = "";
        string returnurl = "";
        string sessionid = "";
        string secretcode = "";

        string errordesc = "";
        string password = SmartPortal.IB.FastBank.GetShopPasswod(fid);
        password = SmartPortal.Security.Encryption.Decrypt(password);
        fdata = SmartPortal.Common.Encrypt.DecryptDataFB(fdata, password);
        string[] ldata = fdata.Split('&');
        foreach (string ditem in ldata)
        {
            string[] ldsub = ditem.Split('=');
            switch (ldsub[0])
            {
                case "order_code":
                    order_code = ldsub[1]; break;
                case "shopcode"  :
                    shopcode   = ldsub[1]; break;
                case "amount"    :
                    amount     = ldsub[1]; break;
                case "ccyid"     :
                    ccyid      = ldsub[1]; break;
                case "trandesc"  :
                    trandesc   = ldsub[1]; break;
                case "returnurl" :
                    returnurl  = ldsub[1]; break;
                case "sessionid" :
                    sessionid  = ldsub[1]; break;
                case  "secretcode":
                    secretcode = ldsub[1]; break;
                default:
                    break;
            }
        }
        if (order_code != "" && shopcode != "" && amount != "" && trandesc != "" && returnurl != "" && sessionid != "" && secretcode != "")
        {
            SmartPortal.IB.FastBank.InsertTransaction(order_code, shopcode, amount, ccyid, trandesc, returnurl, sessionid, secretcode, ref errorcode, ref errordesc);

            if (errorcode =="0")
            {
                return order_code;
            }
            else if (errorcode == "4995")
            {
                IPCERRORCODE = "0";
                return order_code;
            }
            else
            {
                return "";
            }
        }
        else
            return "";

              
    }
        
}
