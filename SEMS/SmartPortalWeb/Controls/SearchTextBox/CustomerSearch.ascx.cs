using System;
using System.Data;
using System.Linq;
public partial class Controls_SearchTextBox_Customer : Popup
{
    public bool SelectOnly = false;
    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();
    public string Text
    {
        get { return txtCustomer.Text; }
        set { txtCustomer.Text = value; }
    }
    public void SetError()
    {
        txtCustomer.BorderColor = System.Drawing.Color.Red;
        txtCustomer.Focus();
    }
    public override void SetDefault()
    {
        txtCustomer.BorderColor = System.Drawing.Color.Empty;
    }
    public string Value
    {
        get { return hdfCustomerSearchValue.Value; }
        set
        {
            if (!value.Equals("0"))
            {
                hdfCustomerSearchValue.Value = value;
                hdID.Value = "#" + value;
            }
        }
    }
    public bool Enabled
    {
        set
        {
            if (value)
            {
                txtCustomer.Enabled = value;
                btnPopup.Disabled = false;
            }
            else
            {
                txtCustomer.Enabled = value;
                btnPopup.Disabled = true;
            }
        }
    }
    public override void OpenFocus()
    {
        ShowPopup(UserCode.ClientID);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
            btnPopup.Attributes["data-target"] = "#" + UserCode.ClientID;
        }
        gidview.pagingClickArgs += new EventHandler(btnSearch_Click);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ShowPopup(UserCode.ClientID);
        BindData();
    }

    private void BindData()
    {
        try
        {
            DataSet ds = new DataSet();
            object[] searchUser = new object[] { txtUserCode.Text, "", gidview.pageIndex * gidview.pageSize, gidview.pageSize };
            ds = _service.common("SEMS_BO_GETUSR_BYTYP", searchUser, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE.Equals("0"))
            {
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        rptData.DataSource = ds.Tables[0];
                        rptData.DataBind();
                    }
                    gidview.total = ds.Tables[0].Rows.Count == 0 ? "0" : ds.Tables[0].Rows[0]["TRECORDCOUNT"].ToString();
                }
            }
        }
        catch (Exception ex)
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
                    //lblError.Text = Resources.labels.SelectOnlyOneValue;
                    return;
                }
                else
                {
                    lblError.Text = string.Empty;
                    txtCustomer.Text = hdfCustomerSearchText.Value;
                    hdfCustomerSearchText.Value = value[0];
                }
            }
            else
            {
                string[] value = hdID.Value.Split('#').Where(x => !string.IsNullOrEmpty(x)).ToArray();
                txtCustomer.Text = hdfCustomerSearchText.Value;
                if (value.Length > 0)
                    hdfCustomerSearchValue.Value = value[0];
            }
            //postbackArgs(sender, e);
            ClosePopup(UserCode.ClientID);
        }
        catch (Exception ex) { }
    }

}