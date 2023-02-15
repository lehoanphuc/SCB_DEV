using System;
using System.Data;
using System.Linq;

public partial class Controls_SearchTextBox_Reason : Popup
{
    public bool SelectOnly = false;
    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();
    string ReasonAction = SmartPortal.Constant.IPC.REJECT;
    string ReasonType = SmartPortal.Constant.IPC.PARAMETER.MERCHANT_AGENT;
    string Event = "K";
    public string Text
    {
        get { return txtReason.Text; }
        set { txtReason.Text = value; }
    }

    public string getReasonID()
    {
        return hdfReasonSearchValue.Value;
    }
    public void SetError()
    {
        txtReason.Attributes.Clear();
        txtReason.Attributes.Add("style", "border: 1px solid Red; width: 90%; outline: none");
        txtReason.Focus();
    }
    public override void SetDefault()
    {
        txtReason.Attributes.Clear();
        txtReason.Attributes.Add("style", "border: 1px solid #E5E5E5; width: 90%; outline: none");
    }
    public string Value
    {
        get { return hdfReasonSearchValue.Value; }
        set
        {
            if (!value.Equals("0"))
            {
                hdfReasonSearchValue.Value = value;
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
                txtReason.Enabled = value;
                btnPopup.Disabled = false;
            }
            else
            {
                txtReason.Enabled = value;
                btnPopup.Disabled = true;
            }
        }
    }

    public override void OpenFocus()
    {
        ShowPopup(ReasonCode.ClientID);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
            btnPopup.Attributes["data-target"] = "#" + ReasonCode.ClientID;
            txtReason.Attributes.Add("placeholder", "Code - Name");
        }
        gidview.pagingClickArgs += new EventHandler(btnSearch_Click);
    }
    
    public void setEvent(string value)
    {
        Event = value;
    }
    public void setReasonAction(string value)
    {
        ReasonAction = value;
    }
    public void setReasonType(string value)
    {
        ReasonType = value;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ShowPopup(ReasonCode.ClientID);
        BindData();
    }
    private void BindData()
    {
        try
        {
            DataSet ds = new DataSet();
            object[] searchUser = new object[] { txtReasonCode.Text, txtDescription.Text, ReasonAction, ReasonType, Event, "A", gidview.pageIndex * gidview.pageSize, gidview.pageSize };
            ds = _service.common("SEMS_BO_GET_REASON", searchUser, ref IPCERRORCODE, ref IPCERRORDESC);
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
                    txtReason.Text = hdfReasonSearchText.Value;
                    hdfReasonSearchText.Value = value[0];
                }
            }
            else
            {
                string[] value = hdID.Value.Split('#').Where(x => !string.IsNullOrEmpty(x)).ToArray();
                txtReason.Text = hdfReasonSearchText.Value + " - " + hdfReasonNameSearchText.Value;
                if (value.Length > 0)
                hdfReasonSearchValue.Value = value[0];
            }
            //postbackArgs(sender, e);
            ClosePopup(ReasonCode.ClientID);
        }
        catch (Exception ex) { }
    }

}