using System;
using System.Data;
using System.Linq;

public partial class Controls_SearchTextBox_User : Popup
{
    public bool SelectOnly = false;
    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();

    public string Text
    {
        get { return txtUserSearch.Text; }
        set { txtUserSearch.Text = value; }
    }
    public void SetError()
    {
        txtUserSearch.Attributes.Clear();
        txtUserSearch.Attributes.Add("style", "border: 1px solid Red; width: 90%; outline: none");
        txtUserSearch.Focus();
    }
    public override void SetDefault()
    {
        txtUserSearch.Attributes.Clear();
        txtUserSearch.Attributes.Add("style", "border: 1px solid #E5E5E5; width: 90%; outline: none");
    }
    
    public bool Enabled
    {
        set
        {
            if (value)
            {
                txtUserSearch.Enabled = value;
                btnPopup.Disabled = false;
            }
            else
            {
                txtUserSearch.Enabled = value;
                btnPopup.Disabled = true;
            }
        }
    }
    public string Value
    {
        get { return hdfUserSearchValue.Value; }
        set
        {
            if (!value.Equals("0"))
            {
                hdfUserSearchValue.Value = value;
                hdID.Value = "#" + value;
            }
        }
    }

    public string SetUserType
    {
        get { return hdfUserType.Value; }
        set { hdfUserType.Value = value; }
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
        //BindData();
    }

    public void refreshPopup() 
    {
        BindData();
    }
    private void BindData()
    {
        try
        {
            DataSet ds = new DataSet();
            object[] searchUser = new object[] { txtUserCode.Text, hdfUserType.Value, gidview.pageIndex * gidview.pageSize, gidview.pageSize };
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
                    //lblError.Text = Resources.labels.SelectOnlyOneValue;
                    return;
                }
                else
                {
                    lblError.Text = string.Empty;
                    txtUserSearch.Text = hdfUserSearchText.Value;
                    hdfUserSearchValue.Value = value[0];
                }
            }
            else
            {
                string[] value = hdID.Value.Split('#').Where(x => !string.IsNullOrEmpty(x)).ToArray();
                txtUserSearch.Text = hdfUserSearchText.Value;
                if (value.Length > 0)
                hdfUserSearchValue.Value = value[0];
            }
            //postbackArgs(sender, e);
            ClosePopup(UserCode.ClientID);
        }
        catch (Exception ex) { }
    }

}