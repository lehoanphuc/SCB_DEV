using System;
using System.Web;
using System.Web.UI;
/// <summary>
/// Summary description for Popup
/// </summary>
public class Popup : System.Web.UI.UserControl
{
    public Popup()
    {
        postbackArgs += new EventHandler(Postback);
    }
    public virtual string GetValue()
    {
        return string.Empty;
    }
    public virtual void SetFocus()
    {
    }
    public virtual void SetDefault()
    {
    }
    public virtual void OpenFocus()
    {
    }
    #region Properties
    public EventHandler postbackArgs;
    public string IPCERRORCODE = "";
    public string IPCERRORDESC = "";

    //lg-900, md-700,sm-600,xs-500
    public string Size = "md";

    #endregion

    public void ShowPopup(string popup)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Pop", "closeModal('" + popup + "');openModal('" + popup + "');", true);
    }
    public void ClosePopup(string popup)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Pop", "closeModal('" + popup + "');", true);
    }


    public void Postback(object sender, EventArgs e)
    {


    }
}