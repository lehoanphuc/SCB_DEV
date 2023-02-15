using System;
using System.Data;
using System.Linq;
public partial class Controls_SearchTextBox_LoadCountry : Popup
{
    public bool SelectOnly = false;
    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();

    public string Text
    {
        get { return txtSearch.Text; }
        set { txtSearch.Text = value; }
    }
    public void SetError()
    {
        txtSearch.BorderColor = System.Drawing.Color.Red;
        txtSearch.Focus();
    }
    public override void SetDefault()
    {
        txtSearch.BorderColor = System.Drawing.Color.Empty;
    }
    public void setCountryID(string value)
    {
        hdfCountrySearchText.Value = value;
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
        get { return hdfCountrySearchText.Value; }
        set
        {
            if (!value.Equals("0"))
            {
                hdfCountrySearchText.Value = value;
                hdID.Value = "#" + value;
            }
        }
    }


    public string getCountryID()
    {
        return hdfCountrySearchText.Value;
    }

    public override void OpenFocus()
    {
        ShowPopup(countryID.ClientID);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BindData();
                btnPopup.Attributes["data-target"] = "#" + countryID.ClientID;

            }
            //refreshPopup();
            gidview.pagingClickArgs += new EventHandler(btnSearch_Click);
        }
        catch (Exception)
        {
            ClosePopup(countryID.ClientID);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ShowPopup(countryID.ClientID);
        BindData();
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
            object[] searchUser = new object[] { txtcountryID.Text, txtCountryName.Text, gidview.pageIndex * gidview.pageSize, gidview.pageSize };
            ds = _service.common("SEMS_NATION_ADV", searchUser, ref IPCERRORCODE, ref IPCERRORDESC);
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
                    txtSearch.Text = hdfCountrySearchText.Value;
                    hdfCountrySearchText.Value = value[0];
                }
            }
            else
            {
                string[] value = hdID.Value.Split('#').Where(x => !string.IsNullOrEmpty(x)).ToArray();
                if (value.Length > 0)
                {
                    txtSearch.Text = hdfCountrySearchText.Value + " - " + hdfCountryNameSearchText.Value; ;
                    hdfCountrySearchText.Value = value[0];
                }
            }
            //postbackArgs(sender, e);
            ClosePopup(countryID.ClientID);
        }
        catch (Exception ex) { }
    }

}