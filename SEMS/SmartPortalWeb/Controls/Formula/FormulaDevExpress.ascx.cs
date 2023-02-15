using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.Model;
using DevExpress.Web;
using SmartPortal.Common.Utilities;
using DevExpress.Data.Filtering.Helpers;

public partial class Controls_Formula_Formula : System.Web.UI.UserControl
{
    string errorCode = "";
    string errorDesc = "";
    public string Value
    {
        get { return filter.FilterExpression; }
        set
        {
            filter.FilterExpression = value;
        }
    }
    public string Name
    {
        get { return txtName.Text; }
        set
        {
            txtName.Text = value;
        }
    }
    string currentFieldName
    {
        get { return ViewState["FIELDNAME"] != null ? ViewState["FIELDNAME"].ToString() : ""; }
        set { ViewState["FIELDNAME"] = value; }
    }
    public DataTable dtFieldName
    {
        get { return ViewState["SUBCONDITION"] == null ? null : (DataTable)ViewState["SUBCONDITION"]; }
        set
        {
            ViewState["SUBCONDITION"] = value;
            gvFieldName.DataSource = value;
            gvFieldName.DataBind();
            foreach (DataRow row in ((DataTable)value).Rows)
            {
                addFieldName(filter, row["ID"].ToString(), row["ORIGINAL"].ToString(), row["DISPLAYNAME"].ToString());
            }
        }
    }
    public int ord
    {
        get { return ViewState["ORD"] != null ? (int)ViewState["ORD"] : 0; }
        set { ViewState["ORD"] = value; }
    }
    bool complexProperty(string fieldName)
    {
        return dtFileNameSource.Select(String.Format("FieldName = '{0}' AND Type <> 'NONE'", fieldName)).Count() > 0;
    }
    private DataTable dtFileNameSource
    {
        get
        {
            if (ViewState["FILENAMESOURCE"] == null)
            {
                SmartPortal.SEMS.Formula f = new SmartPortal.SEMS.Formula();
                DataSet dsSource = f.GetAll("1", ref errorCode, ref errorDesc);
                if (errorCode == "0")
                {
                    ViewState["FILENAMESOURCE"] = dsSource.Tables[0];
                }
            }
            return (DataTable)ViewState["FILENAMESOURCE"];
        }
        set
        {
            ViewState["FILENAMESOURCE"] = value;
        }
    }


    void CreateFilterColumnsSub()
    {
        var colTranCode = (FilterControlComboBoxColumn)condition.Columns["IPCTRANCODE"];
        var comboBoxProperties = colTranCode.PropertiesEdit as ComboBoxProperties;
        DataSet dsSource = new SmartPortal.SEMS.Transactions().LoadTranApp(ref errorCode, ref errorDesc);
        comboBoxProperties.DataSource = dsSource.Tables[0];

        comboBoxProperties.ValueField = "TRANCODE";
        comboBoxProperties.TextField = "PAGENAME";
        comboBoxProperties.ValueType = typeof(string);

    }
    static List<ClauseType> GlobalAllowedOperations
    {
        get
        {
            return new List<ClauseType>{
                    ClauseType.Equals,
                    ClauseType.DoesNotEqual,
                    ClauseType.Greater,
                    ClauseType.GreaterOrEqual,
                    ClauseType.Less,
                    ClauseType.LessOrEqual,
                    ClauseType.Between,
                    ClauseType.NotBetween,
                    ClauseType.Contains,
                    ClauseType.DoesNotContain,
                    ClauseType.BeginsWith,
                    ClauseType.EndsWith,
                    //ClauseType.Like,
                    //ClauseType.NotLike,
                    //ClauseType.IsNull,
                    //ClauseType.IsNotNull,
                    //ClauseType.AnyOf,
                    //ClauseType.NoneOf
                };
        }
    }
    protected void filter_FilterOperationVisibility(object sender, FilterControlOperationVisibilityEventArgs e)
    {
        var propertyName = e.PropertyInfo.PropertyName;
        var dr = dtFileNameSource.Select(string.Format("FieldName = '{0}'", propertyName));
        if (dr != null && dr.Length > 0)
        {
            List<string> operators = dr[0]["Operator"].ToString().Split(',').Select(p => p.Trim()).ToList();
            e.Visible = operators.Contains(e.Operation.ToString());
        }
        else
        {
            e.Visible = GlobalAllowedOperations.Contains(e.Operation);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        //    if (dtFileNameSource == null)
        //        setFieldNameSource();
        //}
    }
    protected void Page_Init(object sender, EventArgs e)
    {

        setFieldNameSource();

        DataTable dtTran = dtFileNameSource.Select("Level=2").CopyToDataTable();
        if (dtTran.Rows.Count > 0)
        {
            foreach (DataRow row in dtTran.Rows)
            {
                addFieldName(condition, row["FieldName"].ToString(), row["FieldName"].ToString(), row["DisplayName"].ToString(), false);
            }
        }

        //FilterControlComboBoxColumn colTranCode = new FilterControlComboBoxColumn();
        //colTranCode.PropertiesComboBox.DataSource = new SmartPortal.SEMS.Transactions().LoadTranApp(ref errorCode, ref errorDesc).Tables[0];
        //colTranCode.PropertiesComboBox.TextField = "PAGENAME";
        //colTranCode.PropertiesComboBox.ValueField = "TRANCODE";

        //colTranCode.PropertyName = "IPCTRANCODE";
        //colTranCode.DisplayName = "Transaction Type";
        //colTranCode.ColumnType = FilterControlColumnType.String;

        //FilterControlColumn colPeriodTime = new FilterControlColumn();
        //colPeriodTime.PropertyName = "PERIODTIME";
        //colPeriodTime.DisplayName = "Period Time";
        //colPeriodTime.ColumnType = FilterControlColumnType.String;

        //FilterControlComboBoxColumn colMonthTran = new FilterControlComboBoxColumn();
        //colMonthTran.PropertyName = "TRANMONTH";
        //colMonthTran.DisplayName = "Transaction Month";
        //colMonthTran.ColumnType = FilterControlColumnType.Integer;

        //FilterControlComboBoxColumn colBiller = new FilterControlComboBoxColumn();
        //try
        //{
        //    colBiller.PropertiesComboBox.DataSource = new SmartPortal.SEMS.Biller().SearchBillByCondition("", "", "", "", "", 0, 0, ref errorCode, ref errorDesc).Tables[0];
        //    colBiller.PropertiesComboBox.TextField = "BillerName";
        //    colBiller.PropertiesComboBox.ValueField = "BillerId";
        //}
        //catch { }

        //colBiller.PropertyName = "BILLERID";
        //colBiller.DisplayName = "Biller";
        //colBiller.ColumnType = FilterControlColumnType.String;

        //condition.Columns.Add(colTranCode);
        //condition.Columns.Add(colPeriodTime); ;
        //condition.Columns.Add(colBiller);

    }
    private void setFieldNameSource()
    {
        DataTable dtTran = dtFileNameSource.Select("Type <> 'NONE' and Level=1").CopyToDataTable();
        ddlFieldName.DataSource = dtTran;
        ddlFieldName.DataTextField = "DisplayName";
        ddlFieldName.DataValueField = "FieldName";
        ddlFieldName.DataBind();

        DataTable dtFormulaSource = dtFileNameSource.Select("Type = 'NONE'").CopyToDataTable();
        if (dtFormulaSource.Rows.Count > 0)
        {
            foreach (DataRow row in dtFormulaSource.Rows)
            {
                addFieldName(filter, row["FieldName"].ToString(), row["FieldName"].ToString(), row["DisplayName"].ToString(), false);
            }
        }
    }
    protected void btnApplyFieldName_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtSubName.Text))
        {
            lblPopupError.Text = "Display Name can not be empty.";
            return;
        }
        lblPopupError.Text = "";
        String exp = condition.FilterExpression;

        DataRow row;
        //edit sub expression
        if (dtFieldName.Select(String.Format("ID = '{0}'", currentFieldName)).Length > 0)
        {
            row = dtFieldName.Select(String.Format("ID = '{0}'", currentFieldName))[0];
            row["EXPRESSION"] = exp;
            row["DISPLAYNAME"] = txtSubName.Text;
            row["DESC"] = !string.IsNullOrEmpty(txtDesc.Text) ? txtDesc.Text : txtSubName.Text;
            gvFieldName.DataSource = dtFieldName;
            gvFieldName.DataBind();
        }
        else
        {
            string newFieldName = currentFieldName + ord;
            row = dtFieldName.NewRow();
            row["ORIGINAL"] = currentFieldName;
            row["ID"] = newFieldName;
            row["DISPLAYNAME"] = txtSubName.Text;
            row["DESC"] = !string.IsNullOrEmpty(txtDesc.Text) ? txtDesc.Text : txtSubName.Text;
            row["EXPRESSION"] = exp;
            dtFieldName.Rows.Add(row);

            addFieldName(filter, newFieldName, currentFieldName, txtSubName.Text);
        }
        lblFieldName.Value = "";
        hdfCheck.Value = "Notshowing";
        condition.ClearFilter();
        txtSubName.Text = "";
        txtDesc.Text = "";

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        createDataTableFieldName();
        string fieldName = ddlFieldName.SelectedValue;
        if (complexProperty(fieldName))
        {
            lblFieldName.Value = fieldName;
            hdfCheck.Value = "Show";
            currentFieldName = fieldName;
            txtSubName.Text = ddlFieldName.SelectedItem.Text;
            ord += 1;
        }
        else if (dtFieldName.Select(string.Format("ID='{0}'", fieldName)).Count() == 0)
        {
            DataRow row = dtFieldName.NewRow();
            row["ID"] = fieldName;
            row["ORIGINAL"] = fieldName;
            row["DESC"] = ddlFieldName.Text;
            row["DISPLAYNAME"] = ddlFieldName.SelectedItem.Text;
            row["EXPRESSION"] = "";
            dtFieldName.Rows.Add(row);

            addFieldName(filter, fieldName, fieldName, ddlFieldName.SelectedItem.Text);

        }
    }
    private void createDataTableFieldName()
    {
        if (dtFieldName == null)
        {
            dtFieldName = new DataTable();
            dtFieldName.Columns.Add("ID");
            dtFieldName.Columns.Add("ORIGINAL");
            dtFieldName.Columns.Add("DISPLAYNAME");
            dtFieldName.Columns.Add("DESC");
            dtFieldName.Columns.Add("EXPRESSION");
        }
    }
    void addFieldName(ASPxFilterControl filter, string fieldName, string original, string displayName, bool refreshGrid = true)
    {
        if (refreshGrid)
        {
            gvFieldName.DataSource = dtFieldName;
            gvFieldName.DataBind();
        }
        DataRow editorFieldName;

        if (dtFileNameSource == null)
            setFieldNameSource();
        if (dtFileNameSource.Select(String.Format("FieldName='{0}'", fieldName)).Count() > 0)
            editorFieldName = dtFileNameSource.Select(String.Format("FieldName='{0}'", fieldName))[0];
        else
            editorFieldName = dtFileNameSource.Select(String.Format("FieldName='{0}'", original))[0];
        string dataType = editorFieldName["DataType"].ToString();
        string controlType = editorFieldName["ControlType"].ToString();
        string sqlSource = editorFieldName["SQLSource"].ToString();
        if (controlType == "CBB" && !string.IsNullOrEmpty(sqlSource))
        {
            FilterControlComboBoxColumn col = new FilterControlComboBoxColumn();
            DataSet source = SmartPortal.DAL.DataAccess.FillDataSet(sqlSource);
            col.PropertiesComboBox.DataSource = source;
            col.PropertiesComboBox.TextField = source.Tables[0].Columns[1].ToString();
            col.PropertiesComboBox.ValueField = source.Tables[0].Columns[0].ToString();

            col.PropertyName = editorFieldName["FieldName"].ToString();
            col.DisplayName = editorFieldName["DisplayName"].ToString();
            col.ColumnType = FilterControlColumnType.String;
            filter.Columns.Add(col);
        }
        else
        {
            var col = new FilterControlColumn();
            col.PropertyName = fieldName;
            col.DisplayName = displayName;
            switch (dataType)
            {
                case "Boolean":
                    col.ColumnType = FilterControlColumnType.Boolean; break;
                case "Int":
                    col.ColumnType = FilterControlColumnType.Integer; break;
                case "Decimal":
                    col.ColumnType = FilterControlColumnType.Decimal; break;
                case "DateTime":
                    col.ColumnType = FilterControlColumnType.DateTime; break;
            }
            filter.Columns.Add(col);
        }


    }

    protected void btnEditFieldName_Click(object sender, EventArgs e)
    {
        hdfCheck.Value = "Show";
        string fieldName = lblFieldName.Value;
        currentFieldName = fieldName;
        DataRow row = dtFieldName.Select(string.Format("ID='{0}'", fieldName))[0];
        txtSubName.Text = row["DISPLAYNAME"].ToString();
        txtDesc.Text = row["DESC"].ToString();
        string exp = row["EXPRESSION"].ToString();
        condition.FilterExpression = exp;
    }

    protected void btnApply_Click(object sender, EventArgs e)
    {
        lblFilter.Text = filter.FilterExpression;
    }

}
