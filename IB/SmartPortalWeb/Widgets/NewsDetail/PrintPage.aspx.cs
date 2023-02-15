using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.BLL;
using SmartPortal.Model;
using SmartPortal.Common.Utilities;

public partial class Widgets_NewsDetail_PrintPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["nid"] != null)
            {
                NewsBLL NB = new NewsBLL();
                NewsModel NM = new NewsModel();

                NM = NB.LoadNewsByID(Utility.IsInt(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["nid"]),1);

                //gán GUI
                lblTitle.Text = NM.Title;
                lblDateCreated.Text = NM.DateCreated;
                lblAuthor.Text = NM.Author;
                lblContent.Text = NM.Content;
            }
        }
    }
}
