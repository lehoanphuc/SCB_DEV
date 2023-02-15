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
using SmartPortal.Common;

public partial class Widgets_IBHorizontalMenu_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["accType"].ToString() != "IND")
            {
                StringBuilder sB1 = new StringBuilder();
                sB1.Append("<link rel='stylesheet' type='text/css' href='widgets/IBHorizontalMenu/cssCo.css' />");
                Literalcss.Text = sB1.ToString();
                //imglogo.ImageUrl = "Images/img_corporate.png";
            }
            else
            {
                StringBuilder sB1 = new StringBuilder();
                sB1.Append("<link rel='stylesheet' type='text/css' href='widgets/IBHorizontalMenu/css.css' />");
                Literalcss.Text = sB1.ToString();
               // imglogo.ImageUrl = "Images/img_individual.png";
            }
            if (SmartPortal.Common.Utilities.Utility.IsGuest())
            {
                //litProfile.Text = "<li><a href='" + SmartPortal.Common.Encrypt.EncryptURL("Default.aspx?p=88") + "'>" + "Profile" + "</a></li>";
                litLogout.Text = "";
                litChangePass.Text = "";
                ltrHome.Text = "<li><a href='" + SmartPortal.Common.Encrypt.EncryptURL("Default.aspx") + "'>" + Resources.labels.trangchu + "</a></li>";
                ltrDieuKhoan.Text = "";
                ltrHuongDanSuDung.Text = "";
                ltrLienHe.Text = "<li><a href='" + SmartPortal.Common.Encrypt.EncryptURL(ConfigurationManager.AppSettings["lienhe"]) + "'>" + Resources.labels.lienhe + "</a></li>";
                ltrGioiThieu.Text = "<li><a href='" + SmartPortal.Common.Encrypt.EncryptURL(ConfigurationManager.AppSettings["gioithieu"]) + "'>" + Resources.labels.gioithieu + "</a></li>";
            }
            else
            {
                //litProfile.Text = "<li><a href='" + SmartPortal.Common.Encrypt.EncryptURL("Default.aspx?p=88") + "'>" + "Profile" + "</a></li>";
                litLogout.Text = "<li><a id='logout'  onclick='return confirm('Are you sure?')' href='" + SmartPortal.Common.Encrypt.EncryptURL("Default.aspx?p=220") + "'>" + Resources.labels.thoatchuongtrinh+"</a></li>";
                litChangePass.Text = "<li id='changepass'><a  href='" + SmartPortal.Common.Encrypt.EncryptURL("Default.aspx?p=292") + "'>"+Resources.labels.doimatkhau+"</a></li>";
                ltrHome.Text = "<li><a href='" + SmartPortal.Common.Encrypt.EncryptURL("Default.aspx?p=86") + "'>" + Resources.labels.trangchu + "</a></li>";
                ltrDieuKhoan.Text = "<li><a href='" + SmartPortal.Common.Encrypt.EncryptURL(ConfigurationManager.AppSettings["dieukhoansudung1"]) + "'>"+Resources.labels.dieukhoansudung+"</a></li>";
                ltrHuongDanSuDung.Text = "<li><a href='" + SmartPortal.Common.Encrypt.EncryptURL(ConfigurationManager.AppSettings["huongdansudung1"]) + "'>"+Resources.labels.huongdansudung+"</a></li>";
                ltrLienHe.Text = "<li><a href='" + SmartPortal.Common.Encrypt.EncryptURL(ConfigurationManager.AppSettings["lienhe1"]) + "'>"+Resources.labels.lienhe+"</a></li>";
            }
        }
        catch (Exception ex)
        {

        }
    }
    
}
