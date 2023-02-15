using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.ExceptionCollection;
using SmartPortal.SEMS;
using System.Data;
using System.Collections;

public partial class Widgets_SEMSFeedBack_Controls_Widget : System.Web.UI.UserControl
{
    string ACTION = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    public string ProvinceHeader
    {
        get
        {
            return lblProvinceHeader.Text;
        }

        set
        {
            lblProvinceHeader.Text = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        ACTION = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["a"].ToString().Trim();

        lblError.Text = "";
        pnAdd.Visible = true;
        pnResult.Visible = false;

        if (!IsPostBack)
        {            
            BindData();
        }
    }
    void BindData()
    {
        try
        {
            switch (ACTION)
            {
                case "add":
                   
                    break;
                default:
                    #region Lấy thông tin feedback
                    DataTable dtfeedback = new SmartPortal.IB.Transactions().GetFeedback(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["fid"].ToString(), "", "", "","","");
                    if (dtfeedback.Rows.Count != 0)
                    {
                        txttitle.Text = dtfeedback.Rows[0]["TITLE"].ToString();
                        txttitle.Enabled = false;
                        areacontent.Value = dtfeedback.Rows[0]["CONTENTFB"].ToString();
                        areacontent.Attributes["readonly"] = "readonly";
                        areacomment.Value = dtfeedback.Rows[0]["COMMENT"].ToString();

                        //ghi vo session dung in
                        Hashtable hasPrint = new Hashtable();
                        hasPrint.Add("transid", dtfeedback.Rows[0]["IPCTransID"].ToString());
                        hasPrint.Add("title", txttitle.Text);
                        hasPrint.Add("content", areacontent.Value);
                        hasPrint.Add("comment", areacomment.Value);
                        Session["printFeedBack"] = hasPrint;
                    }
           
                    break;
                    #endregion
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {
            switch (ACTION)
            {
                //case "add":
                //    objCity.InsertCity(CityCode, CityName, SearchCode, ref IPCERRORCODE, ref IPCERRORDESC);
                //    if (IPCERRORCODE != "0")
                //        throw new IPCException(IPCERRORDESC);
                //    else
                //        lblError.Text = Resources.labels.themmoitinhthanhthanhcong;
                //    break;
                case "edit":
                    int insert = -1;
                    insert = new SmartPortal.IB.Transactions().CommentFeedBack(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["fid"].ToString(),areacomment.Value.ToString(), SmartPortal.Constant.IPC.YES);
                    if (insert != -1)
                    {
                        throw new IPCException(IPCERRORDESC);
                    }
                    else
                    {
                        lbResult.Text = Resources.labels.hoidaptrasoatgiaodichthanhcong;
                        pnAdd.Visible = false;
                        pnResult.Visible = true;
                        btsave.Visible = false;
                        btnPrint.Visible = false;
                        
                    }
                       
                    break;
            }            
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    protected void btback_Click(object sender, EventArgs e)
    {
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=399"));
    }
}
