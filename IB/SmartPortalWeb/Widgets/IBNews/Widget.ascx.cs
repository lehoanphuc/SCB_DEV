using System;
using System.IO;

public partial class Widgets_IBNews_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if(Session["accType"].ToString() != "IND")
            {
                if(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["f"]!=null)
                {
                    switch (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["f"].ToString().Trim())
                    {
                        case "gioithieu":
                            lblTitle.Text = Resources.labels.gioithieu;
                            break;
                        case "huongdansudung":
                            lblTitle.Text = Resources.labels.huongdansudung;
                            divhuongdansudung.Visible = true;
                            break;
                        case "dieukhoansudung":
                            lblTitle.Text = Resources.labels.dieukhoansudung;
                            divdieukhoansudung.Visible = true;
                            break;
                        case "lienhe":
                            lblTitle.Text = Resources.labels.lienhe;
                            divlienhe.Visible = true;
                            break;
                        case "huongdansudungqtht":
                            lblTitle.Text = Resources.labels.huongdansudungqtht;
                            break;
                    }
                }
            }
            else
            {
                divhionline.Visible = true;
                if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["f"] != null)
                {
                    switch (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["f"].ToString().Trim())
                    {
                        case "gioithieu":
                            lblTitle.Text = Resources.labels.gioithieu;
                            break;
                        case "huongdansudung":
                            lblTitle.Text = Resources.labels.huongdansudung;
                            break;
                        case "dieukhoansudung":
                            lblTitle.Text = Resources.labels.dieukhoansudung;
                            break;
                        case "lienhe":
                            lblTitle.Text = Resources.labels.lienhe;
                            break;
                        case "huongdansudungqtht":
                            lblTitle.Text = Resources.labels.huongdansudungqtht;
                            break;
                    }
                }
            }
        }
        catch
        {
        }
    }
}
