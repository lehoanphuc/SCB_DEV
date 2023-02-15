using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_SearchTextBox_Reason : Popup
{
    public bool SelectOnly = false;
    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();
    public EventHandler popupClickArgs;
    public string Text
    {
        get { return txtImage.Text; }
        set { txtImage.Text = value; }
    }

    public override void OpenFocus()
    {
        ShowPopup(Image.ClientID);
    }

    public void ViewImage(String stringBase64)//theo luong xu x
    {
        btnPopup.Attributes["data-target"] = "#" + Image.ClientID;
        btnPopup.Attributes["data-toggle"] = "modal";
        ImageView.Attributes.Add("src", stringBase64);

        ShowPopup(Image.ClientID);
        //string script = "document.getElementById(btnPopup).click()";
        //ShowPopup(Image.ClientID);

        //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "showModal()", true);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnPopup.Attributes["data-target"] = "#" + Image.ClientID;
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ShowPopup(Image.ClientID);
        BindData();
    }
    private void BindData()
    {
        try
        {
            
        }
        catch(Exception ex)
        {

        }
    }

}