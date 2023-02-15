using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.IB;
using System.Data;
using System.Web.UI.HtmlControls;

public partial class Controls_Poster_Poster : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        return;
        string errorCode = "0";
        string errorDesc = string.Empty;
        Poster posterDAO = new Poster();
        DataSet ds = posterDAO.GetAllPoster("SMP_Poster_SelectAll", new object[] { }, ref errorCode, ref errorDesc);

        DataTable dt = ds.Tables[0];
        if (dt != null)
        {
            var posters = from poster in dt.AsEnumerable()
                          where poster.Field<string>("Position").Trim() == Position
                          select poster;

            if (posters.Count() > 0)
            {
                posters = posters.OrderBy(x => x.Field<int>("Idx"));
                foreach (var item in posters)
                {
                    Panel posterPanel = new Panel();
                    if (item.Field<string>("Type") == "image")
                    {
                        Image poster = new Image();
                        poster.ImageUrl = item.Field<string>("Path") + item.Field<string>("FileName");
                        poster.Width = item.Field<int>("Width");
                        poster.Height = item.Field<int>("Height");

                        posterPanel.Controls.Add(poster);
                    }
                    else if (item.Field<string>("Type") == "flash")
                    {
                        HtmlGenericControl poster = new HtmlGenericControl("object");
                        HtmlGenericControl embedTag = new HtmlGenericControl("embed");
                        embedTag.Attributes["type"] = "application/x-shockwave-flash";
                        embedTag.Attributes.Add("src", VirtualPathUtility.ToAbsolute(item.Field<string>("Path") + item.Field<string>("FileName")));
                        embedTag.Style.Add("Width", item.Field<int>("Width") + "px");
                        embedTag.Style.Add("Height", item.Field<int>("Height") + "px");

                        poster.Controls.Add(embedTag);

                        posterPanel.Controls.Add(poster);
                    }
                    postersPanel.Controls.Add(posterPanel);
                }
            }
        }
        postersPanel.ID = postersPanel.ID + "-" + Position;
    }

    public string Position { get; set; }
}