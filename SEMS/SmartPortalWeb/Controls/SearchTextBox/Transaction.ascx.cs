using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

public partial class Controls_SearchTextBox_Bank : Popup
{
    public bool SelectOnly = false;
    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();
    public string Text
    {
        get { return txtTransaction.Text; }
        set { txtTransaction.Text = value; }
    }

    public bool Enabled
    {
        set
        {
            if (value)
            {
                txtTransaction.Enabled = value;
                btnPopup.Disabled = false;
            }
            else
            {
                txtTransaction.Enabled = value;
                btnPopup.Disabled = true;
            }
        }
    }

    public string getTransactionCode()
    {
        return hdfBankSearchValue.Value;
    }
    public void SetError()
    {
        txtTransaction.Attributes.Clear();
        txtTransaction.Attributes.Add("style", "border: 1px solid Red; width: 90%; outline: none; height: 28px");
        txtTransaction.Focus();
    }
    public override void SetDefault()
    {
        txtTransaction.Attributes.Clear();
        txtTransaction.Attributes.Add("style", "border: 1px solid #E5E5E5; width: 90%; outline: none; height: 28px");
    }
    public string Value
    {
        get { return hdfBankSearchValue.Value; }
        set
        {
            if (!value.Equals("0"))
            {
                hdfBankSearchValue.Value = value;
                hdID.Value = "#" + value;
            }
        }
    }

    public override void OpenFocus()
    {
        ShowPopup(TransactionCode.ClientID);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
            loadCombobox();
            btnPopup.Attributes["data-target"] = "#" + TransactionCode.ClientID;
            //txtTransaction.Attributes.Add("placeholder", "Code - Name");
        }
        gidview.pagingClickArgs += new EventHandler(btnSearch_Click);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ShowPopup(TransactionCode.ClientID);
        BindData();
    }

    private void loadCombobox()
    {
        DataSet ds = new DataSet();
        object[] _object = new object[] { string.Empty };
        ds = _service.common("SEMS_SERVICE_IPCTRAN", _object, ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE == "0")
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddServiceID.DataSource = ds;
                ddServiceID.DataValueField = "serviceid";
                ddServiceID.DataTextField = "serviceid";
                ddServiceID.DataBind();
            }
            ddServiceID.Items.Insert(0, new ListItem("ALL", string.Empty));
        }
    }
    private void BindData()
    {
        try
        {
            DataSet ds = new DataSet();
            object[] searchUser = new object[] { txtTransactionCode.Text, txtTransactionName.Text, ddServiceID.SelectedValue, gidview.pageIndex * gidview.pageSize, gidview.pageSize };
            ds = _service.common("SEMS_GET_TRANS_LIST", searchUser, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE.Equals("0"))
            {
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        rptData.DataSource = ds;
                        rptData.DataBind();
                    }
                    gidview.total = ds.Tables[0].Rows.Count == 0 ? "0" : ds.Tables[0].Rows[0]["TRECORDCOUNT"].ToString();
                }
            }
        }
        catch(Exception ex)
        {

        }
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            if (SelectOnly)
            {
                string[] value = hdID.Value.Split('#').Where(x => !string.IsNullOrEmpty(x)).ToArray();
                if (value.Count() > 1)
                {
                    return;
                }
                else
                {
                    lblError.Text = string.Empty;
                    txtTransaction.Text = hdfBankSearchText.Value;
                    hdfBankSearchText.Value = value[0];
                }
            }
            else
            {
                string[] value = hdID.Value.Split('#').Where(x => !string.IsNullOrEmpty(x)).ToArray();
                txtTransaction.Text = hdfBankNameSearchText.Value;//hdfBankSearchText.Value + " - " + hdfBankNameSearchText.Value;
                if (value.Length > 0)
                hdfBankSearchValue.Value = value[0];
            }
            ClosePopup(TransactionCode.ClientID);
        }
        catch (Exception ex) { }
    }

}