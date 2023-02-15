using System;
using System.Data;
using System.Linq;

public partial class Controls_SearchTextBox_Bank : Popup
{
    public bool SelectOnly = false;
    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();
    public string Text
    {
        get { return txtBank.Text; }
        set { txtBank.Text = value; }
    }

    public bool Enabled
    {
        set
        {
            if (value)
            {
                txtBank.Enabled = value;
                btnPopup.Disabled = false;
            }
            else
            {
                txtBank.Enabled = value;
                btnPopup.Disabled = true;
            }
        }
    }

    public string getBankID()
    {
        return hdfBankSearchValue.Value;
    }
    public void SetError()
    {
        txtBank.Attributes.Clear();
        txtBank.Attributes.Add("style", "border: 1px solid Red; width: 90%; outline: none");
        txtBank.Focus();
    }
    public override void SetDefault()
    {
        txtBank.Attributes.Clear();
        txtBank.Attributes.Add("style", "border: 1px solid #E5E5E5; width: 90%; outline: none");
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
        ShowPopup(BankCode.ClientID);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
            btnPopup.Attributes["data-target"] = "#" + BankCode.ClientID;
            txtBank.Attributes.Add("placeholder", "Code - Name");
        }
        gidview.pagingClickArgs += new EventHandler(btnSearch_Click);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ShowPopup(BankCode.ClientID);
        BindData();
    }
    private void BindData()
    {
        try
        {
            DataSet ds = new DataSet();
            object[] searchUser = new object[] { txtBankCode.Text, txtBankName.Text, gidview.pageIndex * gidview.pageSize, gidview.pageSize };
            ds = _service.common("SEMS_BO_BANK_SEARCH", searchUser, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE.Equals("0"))
            {
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        rptData.DataSource = ds.Tables[0];
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
                    txtBank.Text = hdfBankSearchText.Value;
                    hdfBankSearchText.Value = value[0];
                }
            }
            else
            {
                string[] value = hdID.Value.Split('#').Where(x => !string.IsNullOrEmpty(x)).ToArray();
                txtBank.Text = hdfBankSearchText.Value + " - " + hdfBankNameSearchText.Value;
                if (value.Length > 0)
                hdfBankSearchValue.Value = value[0];
            }
            ClosePopup(BankCode.ClientID);
        }
        catch (Exception ex) { }
    }

}