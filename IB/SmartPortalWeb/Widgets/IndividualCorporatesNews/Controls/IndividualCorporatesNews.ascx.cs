using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Widgets_IndividualCorporatesNews_Controls_IndividualNews : System.Web.UI.UserControl
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
        get { return dlInvidualNews; }
        set { dlInvidualNews = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
