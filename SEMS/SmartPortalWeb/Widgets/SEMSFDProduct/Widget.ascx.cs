using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.ExceptionCollection;
using SmartPortal.SEMS;
using System.Data;

public partial class Widgets_SEMSFDProduct_Widget : WidgetBase
{
    public static bool isAscend = false;
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {           
            BindData();
        }
    }
    protected void gvFDProductList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            Label lblFDProductID;
            Label lblFDProductName;
            Label lblTerm;
            Label lblInterestrate;
            Label lblNote;
            Label lblDesc;

            
            HyperLink hpDelete;
            DataRowView drv;

            if (e.Row.RowType == DataControlRowType.Header)
            {
                cbxSelect = new CheckBox();
                cbxSelect.ID = "cbxSelectAll";
                cbxSelect.Attributes.Add("onclick", "SelectCbx(this);");
                e.Row.Cells[0].Controls.Add(cbxSelect);
            }
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                TableRow tbRow = (TableRow)(e.Row.Cells[0].Controls[0].Controls[0]);
                tbRow.Cells.AddAt(0, new TableCell());
                tbRow.Cells[0].Text = "<strong>" + Resources.labels.page + " :</strong>";
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView)e.Row.DataItem;


                cbxSelect = (CheckBox)e.Row.FindControl("cbxSelect");
                e.Row.Attributes.Add("onmousemove", "this.className='hightlight';");
                e.Row.Attributes.Add("onmouseout", "this.className='nohightlight';");


                lblFDProductID = (Label)e.Row.FindControl("lblFDProductID");
                lblFDProductName = (Label)e.Row.FindControl("lblFDProductName");
                lblTerm = (Label)e.Row.FindControl("lblTerm");
                lblInterestrate = (Label)e.Row.FindControl("lblInterestRate");
                lblNote = (Label)e.Row.FindControl("lblNote");
                lblDesc = (Label)e.Row.FindControl("lblDesc");
                hpDelete = (HyperLink)e.Row.FindControl("hpDelete");

                //cbxSelect.Enabled = true;
                //cbxSelect.Attributes.Add("onclick", "HighLightCBX('" + e.Row.ClientID + "',this)");


                lblFDProductID.Text = drv["ID"].ToString();
                lblFDProductName.Text = drv["FDPRODUCTNAME"].ToString();
                lblTerm.Text = drv["TERM"].ToString();
                lblInterestrate.Text = drv["INTERESTRATE"].ToString();
                lblNote.Text = drv["NOTE"].ToString();
                lblDesc.Text = drv["DESCRIPTION"].ToString();
               

                hpDelete.Text = Resources.labels.delete;
                hpDelete.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=380&a=delete&id=" + drv["ID"].ToString());
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }
    protected void btn_Add_Click(object sender, EventArgs e)
    {
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=379&a=add"));
    }
    protected void btn_Del_Click(object sender, EventArgs e)
    {
        CheckBox cbxDelete;
        Label lblDistCode;

        string Str_ProID = "";
        try
        {
            foreach (GridViewRow gvr in gvFDProductList.Rows)
            {
                cbxDelete = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                if (cbxDelete.Checked == true)
                {
                    lblDistCode = (Label)gvr.Cells[1].FindControl("lblFDProductID");
                    Str_ProID += lblDistCode.Text.Trim() + "#";
                }
            }
            Session["_FDPRODUCTID"] = Str_ProID.Substring(0, Str_ProID.Length - 1);
        }
        catch (Exception ex)
        {

        }
        if (Str_ProID == "")
        {
            //lblError.Text = "Bạn vui lòng chọn máy ATM để xóa";
            BindData();
        }
        else
        {
           Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=380"));
        }
    }
    protected void gvFDProductList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvFDProductList.PageIndex = e.NewPageIndex;
            BindData();
        }
        catch
        {
        }
    }
    #region load data
    private void BindData()
    {
        try
        {
            SmartPortal.SEMS.FDProductOnline objFDProduct = new FDProductOnline();
            DataSet ds = new DataSet();
            //ds = objDis.GetDistrictByCondition("", txtDistName.Text, ddlCity.SelectedValue, ref IPCERRORCODE, ref IPCERRORDESC);
            //ds=objFDProduct.GetFDProductOnlineByCondition(txtFDProductID.Text,txtFDProductName.Text,txtTerm.Text,
            if(cbIsClose.Checked)
                ds=objFDProduct.GetFDProductOnlineByCondition(txtFDProductID.Text,txtFDProductName.Text,txtTerm.Text,txtInterestRate.Text,
                                                                "Y", txtNote.Text, txtDesc.Text, ref IPCERRORCODE, ref IPCERRORDESC);
            else
                ds = objFDProduct.GetFDProductOnlineByCondition(txtFDProductID.Text, txtFDProductName.Text, txtTerm.Text, txtInterestRate.Text,
                                                                "N", txtNote.Text, txtDesc.Text, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0" && ds.Tables.Count > 0)
            {
                gvFDProductList.DataSource = ds;
                gvFDProductList.DataBind();
            }
            else
            {
                throw new IPCException(IPCERRORDESC);
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    
    #endregion




    
}
