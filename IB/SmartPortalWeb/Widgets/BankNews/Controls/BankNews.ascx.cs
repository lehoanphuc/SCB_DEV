using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Widgets_BankNews_Controls_BankNews : System.Web.UI.UserControl
{
    private HyperLink _title;

    public Label Title
    {
        get { return hpTitle; }
        set { hpTitle = value; }
    }
    private DataList _dlICN;

    public DataList DlICN
    {
        get { return dlBankNews; }
        set { dlBankNews = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
