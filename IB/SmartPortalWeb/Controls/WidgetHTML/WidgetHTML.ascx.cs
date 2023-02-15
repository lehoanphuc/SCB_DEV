using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SmartPortal.DAL;
using System.Text;
using System.Data.SqlClient;

public partial class Controls_WidgetHTML_WidgetHTML : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            StringBuilder sb = GetContentHTML(WidgetID);
            ltr.Text = sb.ToString();
        }
    }
    private int _widgetID;

    public int WidgetID
    {
        get { return _widgetID; }
        set { _widgetID = value; }
    }
    private StringBuilder GetContentHTML(int widgetID)
    {
        IDataReader iRead;

        SqlParameter p1 = new SqlParameter();
        p1.ParameterName = "@widgetid";
        p1.Value = widgetID;
        p1.SqlDbType = SqlDbType.Int;

        iRead = DataAccess.GetFromReader("WidgetHTML_GetContent", p1);
        StringBuilder sb = new StringBuilder();        
        while (iRead.Read())
        {
            sb.Append(iRead["WidgetContent"].ToString());
        }
        iRead.Close();
        return sb;
    }
    
}
