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


public partial class Controls_LetterSearch_LetterSearch : System.Web.UI.UserControl
{
    private string _url;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            litLetter.Text += "<a href='" + _url + "'>"+Resources.labels.tatca+"</a>&nbsp;&nbsp;&nbsp;&nbsp;"; 

            for (byte i = 65; i <= 90; i++)
            {
                litLetter.Text += "<a href='" + _url + "&letter=" + Convert.ToChar(i) + "'>" + Convert.ToChar(i) + "</a> &nbsp;&nbsp;&nbsp;&nbsp;";
            }
           
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
