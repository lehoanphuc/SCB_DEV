using System;
using System.Data;
using System.Linq;

public partial class Controls_SearchTextBox_Branch : Popup
{
    public bool SelectOnly = false;
    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();
    public string Text
    {
        get { return txtBranch.Text; }
        set { txtBranch.Text = value; }
    }
    public bool Enabled
    {
        set
        {
            if (value)
            {
                txtBranch.Enabled = value;
                btnPopup.Disabled = false;

            }
            else
            {
                txtBranch.Enabled = value;
                btnPopup.Disabled = true;
            }
        }
    }


    public string getId()
    {
        return hdfSearchText.Value;
    }
    public void setBranchID(string value)
    {
        hdfSearchText.Value = value;
    }

    public void SetErrorBranch()
    {
        txtBranch.BorderColor = System.Drawing.Color.Red;
        txtBranch.Focus();
    }
    public void SetDefaultBranch()
    {
        txtBranch.BorderColor = System.Drawing.Color.Empty;
        //txtBranch.Attributes.Clear();
        //txtBranch.Attributes.Add("style", "border: 1px solid #E5E5E5; width: 90%; outline: none");
    }
    public string Value
    {
        get { return hdfSearchText.Value; }
        set
        {
            if (!value.Equals("0"))
            {
                hdfSearchText.Value = value;
                hdID.Value = "#" + value;
            }
        }
    }
    public string ValueName
    {
        get { return hdfNameSearchText.Value; }
        set
        {
            if (!value.Equals(""))
            {
                hdfNameSearchText.Value = value;
                
            }
        }
    }
    public override void OpenFocus()
    {
        ShowPopup(Branch.ClientID);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
            btnPopup.Attributes["data-target"] = "#" + Branch.ClientID;
        }
        gidview.pagingClickArgs += new EventHandler(btnSearch_Click);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            ShowPopup(Branch.ClientID);
            BindData();
        }
        catch (Exception exx)
        {

        }
    }
    private void BindData()
    {
        try
        {
            DataSet ds = new DataSet();
            object[] searchUser = new object[] { txtbranchId.Text, txtBranchName.Text, gidview.pageIndex * gidview.pageSize, gidview.pageSize };
            ds = _service.common("SEMS_BRANCH_ID_NAME", searchUser, ref IPCERRORCODE, ref IPCERRORDESC);
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
                    txtBranch.Text = hdfSearchText.Value;
                    hdfSearchText.Value = value[0];
                }
            }
            else
            {
                string[] value = hdID.Value.Split('#').Where(x => !string.IsNullOrEmpty(x)).ToArray();
                if (value.Length > 0 )
                {
                    txtBranch.Text = hdfSearchText.Value + " - " + hdfNameSearchText.Value;
                    hdfSearchText.Value = value[0];
                }
            }
            //postbackArgs(sender, e);
            ClosePopup(Branch.ClientID);
        }
        catch (Exception ex) { }
    }

}