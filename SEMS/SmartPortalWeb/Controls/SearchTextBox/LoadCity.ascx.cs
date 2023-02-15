using System;
using System.Data;
using System.Linq;
public partial class Controls_SearchTextBox_City : Popup
{
    public bool SelectOnly = false;
    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();
    public string Text
    {
        get { return txtCity.Text; }
        set { txtCity.Text = value; }
    }

    public string getCityID()
    {
        return hdfCitySearchText.Value;
    }
    public void setCityID(string value)
    {
        hdfCitySearchText.Value = value;
    }
    public void SetError()
    {
        txtCity.BorderColor = System.Drawing.Color.Red;
        txtCity.Focus();
    }
    public override void SetDefault()
    {
        txtCity.BorderColor = System.Drawing.Color.Empty;
    }
    public string Value
    {
        get { return hdfCitySearchValue.Value; }
        set
        {
            if (!value.Equals("0"))
            {
                hdfCitySearchValue.Value = value;
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
                txtCity.Enabled = value;
                btnPopup.Disabled = false;
            }
            else
            {
                txtCity.Enabled = value;
                btnPopup.Disabled = true;
            }
        }
    }
    public override void OpenFocus()
    {
        ShowPopup(CityCode.ClientID);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
            btnPopup.Attributes["data-target"] = "#" + CityCode.ClientID;
        }
        gidview.pagingClickArgs += new EventHandler(btnSearch_Click);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ShowPopup(CityCode.ClientID);
        BindData();
    }
    private void BindData()
    {
        try
        {
            DataSet ds = new DataSet();
            object[] searchUser = new object[] { txtCityCode.Text, "", txtcityname.Text, "", "", "", gidview.pageIndex * gidview.pageSize, gidview.pageSize };
            ds = _service.common("SEMS_CITY_SEARCH_ADV", searchUser, ref IPCERRORCODE, ref IPCERRORDESC);
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
                    txtCity.Text = hdfCitySearchText.Value;
                    hdfCitySearchText.Value = value[0];
                }
            }
            else
            {
                string[] value = hdID.Value.Split('#').Where(x => !string.IsNullOrEmpty(x)).ToArray();
                if (value.Length > 0)
                {
                    txtCity.Text = hdfCitySearchText.Value + " - " + hdfCityNameSearchText.Value;
                    hdfCitySearchText.Value = value[0];
                }

            }
            //postbackArgs(sender, e);
            ClosePopup(CityCode.ClientID);
        }
        catch (Exception ex) { }
    }

}