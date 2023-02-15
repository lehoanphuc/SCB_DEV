using System;
using System.Collections;
using System.Data;
using SmartPortal.Common;
using SmartPortal.ExceptionCollection;

public partial class Widgets_SEMSTransactionsApprove_print : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo(new PortalSettings().portalSetting.DefaultLang);
        System.Threading.Thread.CurrentThread.CurrentCulture = ci;
        System.Threading.Thread.CurrentThread.CurrentUICulture = ci;

        if (Session["userName"] == null || Session["userName"].ToString() == "guest")
            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx"));
        string errorCode = "";
        string erroDesc = "";

        try
        {
            Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();
            tmpl = SmartPortal.Common.ST.GetStringTemplate("SEMSTransactionsApprove", "DetailCloseFD" + SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cul"]);

            //ghi vo session dung in
            Hashtable hasPrint = new Hashtable();
            hasPrint = (Hashtable)Session["printDetailCloseFD"];
            tmpl.SetAttribute("sogiaodich", hasPrint["sogiaodich"].ToString());
            tmpl.SetAttribute("ngaygiogiaodich", hasPrint["ngaygiogiaodich"].ToString());
            tmpl.SetAttribute("taikhoantietkiemcokyhan", hasPrint["taikhoantietkiemcokyhan"].ToString());
            tmpl.SetAttribute("soduhientai", hasPrint["soduhientai"].ToString());
            tmpl.SetAttribute("ngaymotaikhoan", hasPrint["ngaymotaikhoan"].ToString());
            tmpl.SetAttribute("laisuat", hasPrint["laisuat"].ToString());
            tmpl.SetAttribute("ngaydenhan", hasPrint["ngaydenhan"].ToString());
            tmpl.SetAttribute("tentaikhoan", hasPrint["tentaikhoan"].ToString());
            tmpl.SetAttribute("taikhoanthanhtoan", hasPrint["taikhoanthanhtoan"].ToString());
            tmpl.SetAttribute("chinhanh", hasPrint["chinhanh"].ToString());
            tmpl.SetAttribute("chinhanhfd", hasPrint["chinhanhfd"].ToString());
            tmpl.SetAttribute("sotien", hasPrint["sotien"].ToString());
            tmpl.SetAttribute("mota", hasPrint["mota"].ToString());
            tmpl.SetAttribute("tenkhachhang", hasPrint["tenkhachhang"].ToString());
            tmpl.SetAttribute("nguoiduyet", hasPrint["nguoiduyet"].ToString());
            tmpl.SetAttribute("trangthai", hasPrint["trangthai"].ToString());
            tmpl.SetAttribute("ketqua", hasPrint["ketqua"].ToString());
            tmpl.SetAttribute("laiduochuongtamtinh", hasPrint["laiduochuongtamtinh"].ToString());
            tmpl.SetAttribute("laicongdon", hasPrint["laicongdon"].ToString());
            tmpl.SetAttribute("diengiai", hasPrint["diengiai"].ToString());
            tmpl.SetAttribute("thongtinchitietduyet", hasPrint["thongtinchitietduyet"].ToString());

            ltrPrint.Text = tmpl.ToString();
            ClientScript.RegisterStartupScript(this.GetType(), "Print", "<script language='javascript'> window.print(); window.close(); </script>");
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
}
