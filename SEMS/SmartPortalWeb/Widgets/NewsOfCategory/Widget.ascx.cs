using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.BLL;
using SmartPortal.Common.Utilities;
using System.Data;
using SmartPortal.ExceptionCollection;

public partial class Widgets_NewsOfCategory_Widget :WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["catid"] != null)
                {
                    NewsBLL NB = new NewsBLL();
                    DataTable tblNewsOfCategory = NB.LoadNewsOfCategory(Utility.IsInt(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["catid"].ToString()));

                    //truong hop co 1 mau tin
                    if (tblNewsOfCategory.Rows.Count == 1)
                    {
                        //gán GUI
                        lblNewsID.Text = tblNewsOfCategory.Rows[0]["NewsID"].ToString();
                        lblTitle.Text = tblNewsOfCategory.Rows[0]["Title"].ToString();                        
                        lblContent.Text = tblNewsOfCategory.Rows[0]["NewsContent"].ToString();

                        //set session for send mail
                        Session["url"] = Request.Url.ToString();
                    }
                    else
                    {
                        pnNewsDetail.Visible = false;
                        //truong hop co nhieu mau tin
                        gvNOC.DataSource = tblNewsOfCategory;
                        gvNOC.DataBind();
                    }
                }
            }
            catch (SQLException sex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_NewsOfCategory_Widget", "Page_Load", sex.ToString(), Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
            }
            catch (Exception ex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_NewsOfCategory_Widget", "Page_Load", ex.ToString(), Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
            }

        }
    }
    protected void gvNOC_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {            
            HyperLink hpTitle;           
            Label lblDateCreated;         
            
            Label lblSummary;

            DataRowView drv;
           
            if (e.Row.RowType == DataControlRowType.Pager)
            {

                TableRow tbRow = (TableRow)(e.Row.Cells[0].Controls[0].Controls[0]);
                tbRow.Cells.AddAt(0, new TableCell());
                tbRow.Cells[0].Text = "<strong>" + Resources.labels.page + " :</strong>";

            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView)e.Row.DataItem;
               
                lblDateCreated = (Label)e.Row.FindControl("lblDateCreated");
                hpTitle = (HyperLink)e.Row.FindControl("hpTitle");
              
                lblSummary = (Label)e.Row.FindControl("lblSummary");              
               
               
                hpTitle.Text = drv["Title"].ToString();
                hpTitle.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL(System.Configuration.ConfigurationManager.AppSettings["newsdetail"] + "&nid=" + drv["NewsID"].ToString());

                lblDateCreated.Text = "(" + Utility.FormatDatetime(SmartPortal.Common.Utilities.Utility.IsDateTime2(drv["DateCreated"].ToString()).ToString("dd/MM/yyyy HH:mm:ss"), "dd/MM/yyyy HH:mm:ss", DateTimeStyle.DateMMM) + ")";
                            
                lblSummary.Text = drv["Summary"].ToString();
               
            }
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_NewsOfCategory_Widget", "gvNOC_RowDataBound", sex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_NewsOfCategory_Widget", "gvNOC_RowDataBound", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void gvNOC_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvNOC.PageIndex = e.NewPageIndex;
            NewsBLL NB = new NewsBLL();
            gvNOC.DataSource = NB.LoadNewsOfCategory(Utility.IsInt(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["catid"].ToString()));
            gvNOC.DataBind();
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_NewsOfCategory_Widget", "gvNOC_PageIndexChanging", sex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_NewsOfCategory_Widget", "gvNOC_PageIndexChanging", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
}
