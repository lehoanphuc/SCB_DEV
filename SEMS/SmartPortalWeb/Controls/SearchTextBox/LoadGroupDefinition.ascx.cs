using System;
using System.Data;
using System.Linq;

public partial class Controls_SearchTextBox_LoadGroupDefinition : Popup
{
    public bool SelectOnly = false;
    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();
    public string Text
    {
        get { return txtgrpdef.Text; }
        set { txtgrpdef.Text = value; }
    }
    public bool Enabled
    {
        set
        {
            if (value)
            {
                txtgrpdef.Enabled = value;
                btnPopup.Disabled = false;

            }
            else
            {
                txtgrpdef.Enabled = value;
                btnPopup.Disabled = true;
            }
        }
    }

    public string getModule()
    {
        return hdfNameSearchText.Value.ToString();
    }
    public string getgroupid()
    {
        return hdfSearchText.Value;
    }

    public void SetErrorAccGrp()
    {
        txtgrpdef.BorderColor = System.Drawing.Color.Red;
        txtgrpdef.Focus();
    }
    public void SetDefaultAccGrp()
    {
        txtgrpdef.BorderColor = System.Drawing.Color.Empty;
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

    public override void OpenFocus()
    {
        ShowPopup(GroupDef.ClientID);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
            btnPopup.Attributes["data-target"] = "#" + GroupDef.ClientID;
        }
        gidview.pagingClickArgs += new EventHandler(btnSearch_Click);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ShowPopup(GroupDef.ClientID);
        BindData();
    }
    private void BindData()
    {
        try
        {
            DataSet ds = new DataSet();
            object[] searchUser = new object[] { txtGroupID.Text, txtModuleName.Text, "", gidview.pageIndex * gidview.pageSize, gidview.pageSize };
            ds = _service.common("SEMS_ACC_GRPDEF_ADV", searchUser, ref IPCERRORCODE, ref IPCERRORDESC);
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
                    txtgrpdef.Text = hdfSearchText.Value;
                    hdfSearchText.Value = value[0];
                }
            }
            else
            {
                string[] value = hdID.Value.Split('#').Where(x => !string.IsNullOrEmpty(x)).ToArray();
                if (value.Length > 0)
                {
                    txtgrpdef.Text = hdfSearchText.Value + " - " + hdfNameSearchText.Value;
                    hdfSearchText.Value = value[0];
                }
            }
            //postbackArgs(sender, e);
            ClosePopup(GroupDef.ClientID);
        }
        catch (Exception ex) { }
    }

}