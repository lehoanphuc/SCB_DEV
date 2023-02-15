using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Configuration;
using SmartPortal.Common.Utilities;
using System.IO;

public partial class Widgets_SEMSPoster_Controls_Widget : System.Web.UI.UserControl
{
    string ACTION = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";

    public string _IMAGE
    {
        get { return imgLoGo.ImageUrl; }
        set { imgLoGo.ImageUrl = value; }
    }

    public string _TITLE
    {
        get { return lblTitleProduct.Text; }
        set { lblTitleProduct.Text = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            txtWidth.Attributes.Add("onkeyup", "ntt('" + txtWidth.ClientID + "',event)");
            txtHeight.Attributes.Add("onkeyup", "ntt('" + txtHeight.ClientID + "',event)");
            txtIndex.Attributes.Add("onkeyup", "ntt('" + txtIndex.ClientID + "',event)");

            ACTION = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["a"].ToString().Trim();
            if (!IsPostBack)
            {
                BindData();
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void btsave_Click(object sender, EventArgs e)
    {
        string physicalPathSEMS = ConfigurationManager.AppSettings["posteruploadpathsems"];
        string physicalPathIB = ConfigurationManager.AppSettings["posteruploadpathib"];
        string filename = null;
        string virtualPath = null;
        try
        {
            if (FileUpload.HasFile)
            {
                filename = FileUpload.FileName;
                FileUpload.SaveAs(physicalPathSEMS + filename);
                virtualPath = "~/" + physicalPathSEMS.Substring(HttpContext.Current.Request.PhysicalApplicationPath.Length).Replace("\\", "/");
                if (!physicalPathSEMS.Equals(physicalPathIB))
                {
                    File.Copy(physicalPathSEMS + filename, physicalPathIB + filename);
                }
            }
            else if (ACTION.Equals("edit"))
            {
                filename = hdfFileName.Value;
                virtualPath = hdfPath.Value;
            }
        }
        catch (Exception)
        {
        }

        int width = int.Parse(Utility.KillSqlInjection(txtWidth.Text));
        int height = int.Parse(Utility.KillSqlInjection(txtHeight.Text));
        string type = Utility.KillSqlInjection(ddlFileType.SelectedValue);
        string position = Utility.KillSqlInjection(ddlPosition.SelectedValue);
        int index = int.Parse(Utility.KillSqlInjection(txtIndex.Text));
        bool publish = cbxPublish.Checked;

        switch (ACTION)
        {
            case "add":
                try
                {
                    new SmartPortal.SEMS.Poster().Insert(filename, virtualPath, width, height, position, index, publish, type, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE != "13")
                    {
                       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=416"));
                    }
                }
                catch (Exception)
                {
                }
                break;
            case "edit":
                try
                {
                    string posterId = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["pid"].ToString();
                    new SmartPortal.SEMS.Poster().Update(posterId, filename, virtualPath, width, height, position, index, publish, type, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE != "13")
                    {
                       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=416"));
                    }
                }
                catch (Exception)
                {
                }
                break;
        }
    }

    void BindData()
    {
        switch (ACTION)
        {
            case "add":
                break;
            default:
                DataTable posterTable = new DataTable();
                posterTable = (new SmartPortal.SEMS.Poster().GetPosterByID(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["pid"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
                if (posterTable.Rows.Count != 0)
                {
                    string posterUrl = posterTable.Rows[0].Field<string>("Path") + posterTable.Rows[0].Field<string>("FileName");
                    string type = posterTable.Rows[0].Field<string>("Type");
                    int width = posterTable.Rows[0].Field<int>("Width");
                    int height = posterTable.Rows[0].Field<int>("Height");
                    ShowPoster(type, posterUrl, width, height);

                    hdfFileName.Value = posterTable.Rows[0].Field<string>("FileName");
                    hdfPath.Value = posterTable.Rows[0].Field<string>("Path");
                    ddlFileType.SelectedValue = type;
                    ddlPosition.SelectedValue = posterTable.Rows[0].Field<string>("Position");
                    txtWidth.Text = width.ToString();
                    txtHeight.Text = height.ToString();
                    txtIndex.Text = posterTable.Rows[0].Field<int>("Idx").ToString();
                    cbxPublish.Checked = posterTable.Rows[0].Field<bool>("Publish");
                }

                break;
        }
    }

    void ShowPoster(string type, string posterUrl, int width, int height)
    {
        if (type == "image")
        {
            Image poster = new Image();
            poster.ImageUrl = posterUrl;
            poster.Width = width;
            poster.Height = height;

            PosterPanel.Controls.Add(poster);
        }
        else if (type == "flash")
        {
            HtmlGenericControl poster = new HtmlGenericControl("object");
            HtmlGenericControl embedTag = new HtmlGenericControl("embed");
            embedTag.Attributes["type"] = "application/x-shockwave-flash";
            embedTag.Attributes.Add("src", VirtualPathUtility.ToAbsolute(posterUrl));
            embedTag.Style.Add("Width", width + "px");
            embedTag.Style.Add("Height", height + "px");

            poster.Controls.Add(embedTag);

            PosterPanel.Controls.Add(poster);
        }
    }
}