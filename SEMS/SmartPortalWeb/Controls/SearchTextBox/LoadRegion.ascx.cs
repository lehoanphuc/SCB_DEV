using System;
using System.Data;
using System.Linq;
public partial class Controls_SearchTextBox_Region : Popup
{
    public bool SelectOnly = false;
    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();
    public string Text
    {
        get { return txtRegion.Text; }
        set { txtRegion.Text = value; }
    }

    public string getRegionId()
    {
        return hdfRegionSearchText.Value;
    }
    public void setRegionID(string vau)
    {
        hdfRegionSearchText.Value = vau;

    }
    public void setRegionName(string value)
    {
        hdfRegionNameSearchText.Value = value;
    }

    public void SetError()
    {
        txtRegion.BorderColor = System.Drawing.Color.Red;
        txtRegion.Focus();
    }
    public override void SetDefault()
    {
        txtRegion.BorderColor = System.Drawing.Color.Empty;
    }
    public string Value
    {
        get { return hdfRegionSearchValue.Value; }
        set
        {
            if (!value.Equals("0"))
            {
                hdfRegionSearchValue.Value = value;
                hdID2.Value = "#" + value;
            }
        }
    }
    public bool Enabled
    {
        set
        {
            if (value)
            {
                txtRegion.Enabled = value;
                btnPopup.Disabled = false;
            }
            else
            {
                txtRegion.Enabled = value;
                btnPopup.Disabled = true;
            }
        }
    }

    public override void OpenFocus()
    {
        ShowPopup(RegionCode.ClientID);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
            btnPopup.Attributes["data-target"] = "#" + RegionCode.ClientID;
        }
        gidview.pagingClickArgs += new EventHandler(btnSearch_Click);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ShowPopup(RegionCode.ClientID);
        BindData();
    }
    private void BindData()
    {
        try
        {
            DataSet ds = new DataSet();
            object[] searchUser = new object[] { txtRegionCode.Text, "", txtRegionname.Text, "", "", "", gidview.pageIndex * gidview.pageSize, gidview.pageSize };
            ds = _service.common("SEMS_REGION_ADV", searchUser, ref IPCERRORCODE, ref IPCERRORDESC);
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
                string[] value = hdID2.Value.Split('#').Where(x => !string.IsNullOrEmpty(x)).ToArray();
                if (value.Count() > 1)
                {
                    //lblError.Text = Resources.labels.SelectOnlyOneValue;
                    return;
                }
                else
                {
                    lblError.Text = string.Empty;
                    txtRegion.Text = hdfRegionSearchText.Value;
                    hdfRegionSearchText.Value = value[0];
                }
            }
            else
            {
                string[] value = hdID2.Value.Split('#').Where(x => !string.IsNullOrEmpty(x)).ToArray();
                if (value.Length > 0)
                {
                    txtRegion.Text = hdfRegionSearchText.Value + " - " + hdfRegionNameSearchText.Value;
                    hdfRegionSearchText.Value = value[0];
                }

            }
            //postbackArgs(sender, e);
            ClosePopup(RegionCode.ClientID);
        }
        catch (Exception ex) { }
    }

}