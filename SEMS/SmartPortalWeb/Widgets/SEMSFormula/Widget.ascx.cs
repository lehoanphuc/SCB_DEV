using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Widgets_SEMSFormula_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack) loadData();
    }
    void loadData()
    {
        string expression = "[BIRTHDAY] = True And [BIRTHMONTH] = True And [FIRSTTRAN1] = True And [FIRSTRCV2] = True And [TOTALTRAN3] = 1111";
        DataTable dtFieldName = new DataTable();
        dtFieldName.Columns.Add("ID");
        dtFieldName.Columns.Add("ORIGINAL");
        dtFieldName.Columns.Add("DISPLAYNAME");
        dtFieldName.Columns.Add("DESC");
        dtFieldName.Columns.Add("EXPRESSION");

        DataRow r1= dtFieldName.NewRow();
        r1["ID"] = "BIRTHDAY";
        r1["ORIGINAL"] = "BIRTHDAY";
        r1["DISPLAYNAME"] = "Birthday";
        r1["EXPRESSION"] = "";
        dtFieldName.Rows.Add(r1);

        DataRow r2 = dtFieldName.NewRow();
        r2["ID"] = "BIRTHMONTH";
        r2["ORIGINAL"] = "BIRTHMONTH";
        r2["DISPLAYNAME"] = "Birthmonth";
        r2["EXPRESSION"] = "";
        dtFieldName.Rows.Add(r2);

        DataRow r3 = dtFieldName.NewRow();
        r3["ID"] = "FIRSTTRAN1";
        r3["ORIGINAL"] = "FIRSTTRAN";
        r3["DISPLAYNAME"] = "First Transaction 1";
        r3["EXPRESSION"] = "StartsWith([TRANCODE], '123')";
        dtFieldName.Rows.Add(r3);

        DataRow r4 = dtFieldName.NewRow();
        r4["ID"] = "FIRSTRCV2";
        r4["ORIGINAL"] = "FIRSTRCV";
        r4["DISPLAYNAME"] = "First Receiver";
        r4["EXPRESSION"] = "StartsWith([TRANCODE], '1444')";
        dtFieldName.Rows.Add(r4);

        DataRow r5 = dtFieldName.NewRow();
        r5["ID"] = "TOTALTRAN3";
        r5["ORIGINAL"] = "TOTALTRAN";
        r5["DISPLAYNAME"] = "Total Transaction";
        r5["EXPRESSION"] = "StartsWith([TRANCODE], '56565')";
        dtFieldName.Rows.Add(r5);

        FormulaDevExpress.dtFieldName = dtFieldName;
        FormulaDevExpress.Value = expression;

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        lblTest.Text = FormulaDevExpress.Value;
    }
}