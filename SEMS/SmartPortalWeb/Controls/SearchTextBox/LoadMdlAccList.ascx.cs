using System;
using System.Data;
using System.Linq;
public partial class Controls_SearchTextBox_LoadMdlAccList : Popup
{
    public bool SelectOnly = false;
    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();

    public void SetModule(string value)
    {
        hdModule.Value = value;
    }
    public string getgroupname()
    {
        return hdfUserSearchText.Value;
    }

    public string Text
    {
        get { return txtSearch.Text; }
        set { txtSearch.Text = value; }
    }
    public void SetErrorAccList()
    {
        txtSearch.BorderColor = System.Drawing.Color.Red;
        txtSearch.Focus();

    }
    public void SetDefaultAccList()
    {
        txtSearch.BorderColor = System.Drawing.Color.Empty;
    }

    public bool Enabled
    {
        set
        {
            if (value)
            {
                txtSearch.Enabled = value;
                btnPopup.Disabled = false;

            }
            else
            {
                txtSearch.Enabled = value;
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
        ShowPopup(SysAccName.ClientID);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
            btnPopup.Attributes["data-target"] = "#" + SysAccName.ClientID;
            txtSysAccName.Attributes.Add("placeholder", "System account name");
            txtSearch.Attributes.Add("Data_Filter", "");
        }
        gidview.pagingClickArgs += new EventHandler(btnSearch_Click);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ShowPopup(SysAccName.ClientID);
        BindData();
    }

    public void refreshPopup()
    {
        BindData();
    }

    public void reload_Bindata()
    {
        BindData();
    }
    private void BindData()
    {
        try
        {
            //string moudule = txtSearch.Attributes["Data_Filter"].ToString();
            DataSet ds = new DataSet();
            object[] searchUser = new object[] { hdModule.Value.Trim().ToString(), txtSysAccName.Text, "", gidview.pageIndex * gidview.pageSize, gidview.pageSize };
            ds = _service.common("SEMS_ACGRPDEFDTL_SAV", searchUser, ref IPCERRORCODE, ref IPCERRORDESC);
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
                    txtSearch.Text = hdfUserSearchText.Value;
                    hdfUserSearchValue.Value = value[0];
                }
            }
            else
            {
                string[] value = hdID.Value.Split('#').Where(x => !string.IsNullOrEmpty(x)).ToArray();
                if (value.Length > 0)
                {
                    txtSearch.Text = hdfUserSearchText.Value;
                    hdfUserSearchValue.Value = value[0];
                }

            }
            //postbackArgs(sender, e);
            ClosePopup(SysAccName.ClientID);
        }
        catch (Exception ex) { }
    }

}