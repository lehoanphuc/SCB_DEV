using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using QRCoder;
using SmartPortal.Common;
using SmartPortal.Common.Utilities;
using SmartPortal.ExceptionCollection;

public partial class Widgets_SEMSContract_print : System.Web.UI.Page
{
    string walletid;
    public string ACCTNO
    {
        get { return ViewState["ACCTNO"] != null ? (string)ViewState["ACCTNO"] : string.Empty; }
        set { ViewState["ACCTNO"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null || Session["userName"].ToString() == "guest")
            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx"));
        try
        {
            string userid = "";
            SmartPortal.Model.DictionaryWithDefault<string, string> urlparams = Encrypt.GetURLParam(HttpContext.Current.Request.RawUrl);
            if (urlparams.ContainsKey("ID"))
                userid = Utility.KillSqlInjection(urlparams["ID"]);
            if (urlparams.ContainsKey("Acctno"))
                ACCTNO = Utility.KillSqlInjection(urlparams["Acctno"]);
            string qrStr = GenerateQRByUserAcct(userid,ACCTNO);
            ltrPrint.Text = qrStr;
            if (!string.IsNullOrEmpty(qrStr))
            {
                ltrPrint.Text = qrStr;
                ClientScript.RegisterStartupScript(this.GetType(), "Print", "<script language='javascript'> window.print();</script>");

            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(IPCex.ToString(), Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }


    protected string GenerateQRByUserAcct(string userid,string account)
    {
        try
        {
            string IPCERRORCODE = string.Empty;
            string IPCERRORDESC = string.Empty;

            QRCodeGenerator qr = new QRCodeGenerator();
            DataTable QRCodeDt = new DataTable();
            string qrCode = string.Empty;

            DataSet dsQR = new SmartPortal.SEMS.User().GetUserQR(userid,account, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0" && dsQR.Tables.Count > 0 && dsQR.Tables[0].Rows.Count > 0)
            {
                DataRow drQR = dsQR.Tables[0].Rows[0];
                Dictionary<string, object> dicQrcode = new Dictionary<string, object>();
                Dictionary<string, object> dicQrcode51 = new Dictionary<string, object>();
                Dictionary<string, object> dicQrcode64 = new Dictionary<string, object>();
                DataSet dsconfigqr = new DataSet();
                dsconfigqr = new SmartPortal.SEMS.Contract().GetConfigQr(ref IPCERRORCODE, ref IPCERRORDESC);

                Hashtable Data = new Hashtable();              
                Data.Add("CITYNAME", drQR["CITYNAME"].ToString());
                Data.Add("BRANCHNAME", drQR["BRANCHNAME"].ToString());
                Data.Add("ACCTNO", ACCTNO.ToString());
                Data.Add("USERID", Utility.EncodeUserid(userid));
                Data.Add("PAYMENTTYPEID", drQR["PAYMENTTYPEID"].ToString());
                Data.Add("CCYID", drQR["CCYID"].ToString());
                Data.Add("QRTYPE", drQR["QRTYPE"].ToString());
                walletid = drQR["PHONE"].ToString();
                if (IPCERRORCODE == "0" && dsconfigqr != null && dsconfigqr.Tables[0].Rows.Count > 0)
                {
                    new GenarateQRnew().GenConfigQrCode(ref dicQrcode, dsconfigqr, Data, true, "");
                }
                else
                {

                }
                qrCode += "000201";
                new SmartPortal.Common.GenarateQRnew().GenQRCode(dicQrcode, ref qrCode);
                string refqr = "";
                DataSet ds = new DataSet();
                ds = new SmartPortal.SEMS.Contract().GenQRCode(qrCode, userid, ref refqr, ref IPCERRORCODE, ref IPCERRORDESC);
                QRCodeData data = qr.CreateQrCode(refqr, QRCodeGenerator.ECCLevel.Q);
                QRCode code = new QRCode(data);
                string strCode = "data:image/png;base64," + Convert.ToBase64String(BitmapToBytesCode(code.GetGraphic(20)));
                string merchantname = drQR["LOCALFULLNAME"].ToString();
                if (Session["langID"] != null && Session["langID"].ToString().Equals("lo-LA") && !string.IsNullOrEmpty(drQR["LOCALFULLNAME"].ToString()))
                {
                    merchantname = drQR["LOCALFULLNAME"].ToString();
                }
                if (string.IsNullOrEmpty(merchantname))
                {
                    merchantname = drQR["CUSTNAME"].ToString();
                }

                return PrintQRByUserAcct(userid, strCode, merchantname);
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
        return string.Empty;
    }
    private static Byte[] BitmapToBytesCode(Bitmap image)
    {
        using (MemoryStream stream = new MemoryStream())
        {
            image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            return stream.ToArray();
        }
    }
    private string PrintQRByUserAcct(string userid, string base64Image, string merchantname)
    {
        try
        {
            var srchead = "data:image/png;base64," + "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";
            string str = "<div style='display: flex;align-items: center;flex-direction:row;justify-content: space-around;'>";    
            str += "<div style='margin: 20px;width: 265px;height: 310px; border-style:groove;display: flex;align-items: center;justify-content: center;align-content: center;flex-direction: column;'>";
            str += "<div style = ' display: inline-block;position: relative;z-index:10; max-height:100%; top: 2px' >";
            str += "<img style= 'width: 265px;max-height: 265px;' src ='" + base64Image;
            str += "'/>";            
            str += "<div style='display: block;position: absolute;z-index: 999;top: 115px;left: 110px;bottom: 0;right: 0;margin: auto;'>";            
            str += "<img  style= 'height:50px; width:50px;' src ='" + srchead;
            str += "'/>";
            str += "</div>";
            str += "</div>";
            str += "<div style='width:265px;word-wrap: break-word;height: 50px;font-size: 16px;' ;>";
            str += "<p  style= 'font-weight: bold;color: #000000;margin: 0px;text-align:center;' >" + merchantname;
            str += "</p>";       
            str += "<p  style= 'font-weight: bold;color: #000000;margin:auto;margin-bottom: 5px;text-align:center;' >" + "(" + walletid + ")";
            str += "</p>";
            str += "</div>";
            str += "</div>";
            str += "<div style='margin: 20px;width: 265px;height: 310px; border-style:groove;display: flex;align-items: center;justify-content: center;align-content: center;flex-direction: column;'>";
            str += "<div style = ' display: inline-block;position: relative;z-index:10; max-height:100%; top: 2px' >";
            str += "<img style= 'width: 265px;max-height: 265px;' src ='" + base64Image;
            str += "'/>";
            str += "<div style='display: block;position: absolute;z-index: 999;top: 115px;left: 110px;bottom: 0;right: 0;margin: auto;'>";
            str += "<img  style= 'height:50px; width:50px;' src ='" + srchead;
            str += "'/>";
            str += "</div>";
            str += "</div>";
            str += "<div style='width:265px;word-wrap: break-word;height: 50px;font-size: 16px;' ;>";
            str += "<p  style= 'font-weight: bold;color: #000000;margin: 0px;text-align:center;' >" + merchantname;
            str += "</p>";
            str += "<p  style= 'font-weight: bold;color: #000000;margin:auto;margin-bottom: 5px;text-align:center;' >" + "(" + walletid + ")";
            str += "</p>";
            str += "</div>";
            str += "</div>";
            str += "</div>";

            str += "<div style='display: flex;align-items: center;flex-direction:row;justify-content: space-around;'>";
            str += "<div style='margin: 50px;width: 160px;height: 215px;border-style:groove;display: flex;align-items: center;justify-content: center;align-content: center;flex-direction: column;'>";
            str += "<div style = 'display: inline-block;position: relative;z-index:10;max-height:100%;' >";
            str += "<img style= 'width:100%;max-height:100%;' src ='" + base64Image;
            str += "'/>";
            str += "<div style='display: block;position: absolute;z-index: 999;top: 70px;left: 65px;bottom: 0;right: 0;margin: auto;'>";
            str += "<img  style= 'height:25px; width:25px;' src ='" + srchead;
            str += "'/>";
            str += "</div>";
            str += "</div>";
            str += "<div style='width:160px;word-wrap: break-word;height: 40px;font-size: 13px;'>";
            str += "<p  style= 'font-weight: bold;color: #000000; margin:0;text-align:center;' >" + merchantname;
            str += "</p>";
            str += "<p  style= 'font-weight: bold;color: #000000;text-align:center;margin: 0;' >" + "(" + walletid + ")";
            str += "</p> </br>";
            str += "</div>"; 
            str += "</div>";
            str += "<div style='margin: 50px;width: 160px;height: 215px;border-style:groove;display: flex;align-items: center;justify-content: center;align-content: center;flex-direction: column;'>";
            str += "<div style = 'display: inline-block;position: relative;z-index:10;max-height:100%;' >";
            str += "<img style= 'width:100%;max-height:100%;' src ='" + base64Image;
            str += "'/>";
            str += "<div style='display: block;position: absolute;z-index: 999;top: 70px;left: 65px;bottom: 0;right: 0;margin: auto;'>";
            str += "<img  style= 'height:25px; width:25px;' src ='" + srchead;
            str += "'/>";
            str += "</div>";
            str += "</div>";
            str += "<div style='width:160px;word-wrap: break-word;height: 40px;font-size: 13px;'>";
            str += "<p  style= 'font-weight: bold;color: #000000; margin:0;text-align:center;' >" + merchantname;
            str += "</p>";
            str += "<p  style= 'font-weight: bold;color: #000000;text-align:center;margin: 0;' >" + "(" + walletid + ")";
            str += "</p> </br>";
            str += "</div>";
            str += "</div>";
            str += "<div style='margin: 50px;width: 160px;height: 215px;border-style:groove;display: flex;align-items: center;justify-content: center;align-content: center;flex-direction: column;'>";
            str += "<div style = 'display: inline-block;position: relative;z-index:10;max-height:100%;' >";
            str += "<img style= 'width:100%;max-height:100%;' src ='" + base64Image;
            str += "'/>";
            str += "<div style='display: block;position: absolute;z-index: 999;top: 70px;left: 65px;bottom: 0;right: 0;margin: auto;'>";
            str += "<img  style= 'height:25px; width:25px;' src ='" + srchead;
            str += "'/>";
            str += "</div>";
            str += "</div>";
            str += "<div style='width:160px;word-wrap: break-word;height: 40px;font-size: 13px;'>";
            str += "<p  style= 'font-weight: bold;color: #000000; margin:0;text-align:center;' >" + merchantname;
            str += "</p>";
            str += "<p style= 'font-weight: bold;color: #000000;text-align:center;margin: 0;' >" + "(" + walletid + ")";
            str += "</p> </br>";
            str += "</div>";
            str += "</div>";




            return str;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
            //lblError.Text = ex.Message + " - " + Resources.labels.PrintFailedToCreate;
        }
        return "";
    }
}
