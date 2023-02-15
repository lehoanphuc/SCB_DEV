using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_SearchLetter_Widget : System.Web.UI.UserControl
{
    private string _url;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            for (byte i = 65; i <= 90; i++)
            {
                ltrSearchLetter.Text += "<a href='" + _url + "&letter=" + Convert.ToChar(i) + "'>" + Convert.ToChar(i) + "</a> &nbsp;&nbsp;&nbsp;&nbsp;";
            }
            ltrSearchLetter.Text += "<a href='" + _url + "'>All</a>"; ;
        }
        
    }

    public string url
    {
        set
        {
            _url = value;
            //default.aspx?view=comCustomer&action=view
        }
    }
}
