using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SmartPortal.ExceptionCollection;

public partial class Widgets_SEMSBillPayment_Widget : WidgetBase
{
    double sum = 0;
    string errorcode = "";
    string errorDesc = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        lblTextError.Text = "";
        try
        {
            if (!IsPostBack)
            {
                pnResult.Visible = false;
                //load service
                DataTable dtSV = new DataTable();
                dtSV = new SmartPortal.IB.Payment().GetService();
                if (dtSV.Rows.Count == 0)
                {
                    throw new Exception();
                }
                ddlservice.DataSource = dtSV;
                ddlservice.DataTextField = "Paymenttypename";
                ddlservice.DataValueField = "Paymenttypeid";
                ddlservice.DataBind();

                //load provider...
                ddlservice_SelectedIndexChanged(sender, e);
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(IPCex.ToString(), Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    protected void ddlservice_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtProvider = new DataTable();
        dtProvider = new SmartPortal.IB.Payment().GetPaymentProvider(ddlservice.SelectedValue);

        //load provider...
        //DataTable dtPR = new DataTable();
        //dtPR = new SmartPortal.IB.Payment().GetProvider();
        if (dtProvider.Rows.Count == 0)
        {
            ddlprovider.Items.Clear();
            ddlprovider.Items.Insert(0, new ListItem(Resources.labels.khongconhacungcapnao, ""));
        }
        else
        {
            ddlprovider.DataSource = dtProvider;
            ddlprovider.DataTextField = "Paymentprovidername";
            ddlprovider.DataValueField = "Paymentprovideraccount";
            ddlprovider.DataBind();
        }
    }
    protected void btnXemHoaDon_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlprovider.SelectedValue != "")
            {
                BindData();

            }
            else
            {
                lblTextError.Text = Resources.labels.khongconhacungcapnao;
            }

        }
        catch
        {
        }
    }

    void BindData()
    {
        try
        {
            lblTextError.Text = "";
            lblService.Text = ddlservice.SelectedItem.ToString();
            lblprovider.Text = ddlprovider.SelectedItem.ToString();
            DataSet dtWater = new DataSet();
            dtWater = new SmartPortal.IB.Payment().GetBillPayment(txtCustCode.Text.Trim(), ref errorcode, ref errorDesc);

            if (dtWater == null || errorcode != "0")
            {
                lblTextError.Text = Resources.labels.makhachhangkhonghople;
                return;
            }

            if (dtWater.Tables[0].Rows.Count == 0)
            {
                lblTextError.Text = Resources.labels.khongcohoadonnaodethanhtoan + " / " + Resources.labels.makhachhangkhonghople;
                return;
                //litPager.Text = Resources.labels.danghienthi + " <b>" + (((gvProductList.PageIndex) * gvProductList.PageSize) + gvProductList.Rows.Count).ToString() + "</b> " + Resources.labels.cua + " <b>" + dtProLim.Tables[0].Rows.Count.ToString() + "</b> " + Resources.labels.dong;
            }
           /* NuocChoLon.Service WS = new NuocChoLon.Service();

            dtWater = WS.getW_Bill(txtCustCode.Text.Trim(), SmartPortal.Constant.IPC.PHUONGNAM, DateTime.Now.Day.ToString());
            
            if (dtWater == null)
            {
                lblTextError.Text = Resources.labels.makhachhangkhonghople;
            }

            if (dtWater.Tables[0].Rows.Count == 0)
            {
                lblTextError.Text = Resources.labels.khongcohoadonnaodethanhtoan + " / " + Resources.labels.makhachhangkhonghople;
                return;
                //litPager.Text = Resources.labels.danghienthi + " <b>" + (((gvProductList.PageIndex) * gvProductList.PageSize) + gvProductList.Rows.Count).ToString() + "</b> " + Resources.labels.cua + " <b>" + dtProLim.Tables[0].Rows.Count.ToString() + "</b> " + Resources.labels.dong;
            }*/
            if (dtWater.Tables[0].Rows.Count != 0)
            {
                gvProductList.DataSource = dtWater;
                gvProductList.DataBind();

                foreach (DataRow r in dtWater.Tables[0].Rows)
                {
                    sum += SmartPortal.Common.Utilities.Utility.isDouble(r["TONGCONG"].ToString().Trim(),false);
                }
                lblSum.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(sum.ToString(), Resources.labels.lak);

                DataTable dt = new DataTable();
                dt = dtWater.Tables[0];
                lblcustname.Text = dt.Rows[0]["KHang"].ToString();
                lblCustCode.Text = dt.Rows[0]["DBo"].ToString();
                lbladdress.Text = dt.Rows[0]["Dchi1"].ToString();
                lblstreet.Text = dt.Rows[0]["Dchi2"].ToString();
                //hien thi  menu
                pnResult.Visible = true;

            }
            else
            {
                lblTextError.Text = Resources.labels.makhachhangkhonghople;
                return;
                //throw new IPCException(IPCERRORDESC);
                //throw new IPCException("");
            }
            //else
            //{
            //    ltrError.Text = "<p style='color:red;margin-left:10px; margin-top:20px;'>" + Resources.labels.datanotfound + "</p>";
            //}
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
    protected void gvProductList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            Label lbsohoadon;
            Label lblnamhoadon, lbkyhoadon, lbltienphi;
            Label lbtienthue, lbbieuphi, lbdinhmuc, lbgiaban, lbtongcong, lbID;


            DataRowView drv;

            if (e.Row.RowType == DataControlRowType.Header)
            {
                cbxSelect = new CheckBox();
                cbxSelect.ID = "cbxSelectAll";
                cbxSelect.Attributes.Add("onclick", "SelectCbx(this);");
                e.Row.Cells[0].Controls.Add(cbxSelect);
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView)e.Row.DataItem;


                cbxSelect = (CheckBox)e.Row.FindControl("cbxSelect");
                e.Row.Attributes.Add("onmousemove", "this.className='hightlight';");
                e.Row.Attributes.Add("onmouseout", "this.className='nohightlight';");


                lbsohoadon = (Label)e.Row.FindControl("lbsohoadon");
                lblnamhoadon = (Label)e.Row.FindControl("lblnamhoadon");
                lbkyhoadon = (Label)e.Row.FindControl("lbkyhoadon");
                lbltienphi = (Label)e.Row.FindControl("lbltienphi");
                lbtienthue = (Label)e.Row.FindControl("lbtienthue");
                lbbieuphi = (Label)e.Row.FindControl("lbbieuphi");
                lbgiaban = (Label)e.Row.FindControl("lbgiaban");
                lbtongcong = (Label)e.Row.FindControl("lbtongcong");
                lbID = (Label)e.Row.FindControl("lbID");

                //hpEdit = (HyperLink)e.Row.FindControl("hpEdit");
                //hpDelete = (HyperLink)e.Row.FindControl("hpDelete");
                //lblstatus = (Label)e.Row.FindControl("lblstatus");
                //cbxSelect.Enabled = true;
                //cbxSelect.Attributes.Add("onclick", "HighLightCBX('" + e.Row.ClientID + "',this)");


                lbsohoadon.Text = drv["SHDON"].ToString();
                lblnamhoadon.Text = drv["NamHD"].ToString();
                lbkyhoadon.Text = drv["KyHD"].ToString();
                lbltienphi.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(drv["PBVMT"].ToString(), Resources.labels.lak);
                lbtienthue.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(drv["TGTGT"].ToString(), Resources.labels.lak);
                //lblproductid.Text = drv["PRODUCTID"].ToString();
                lbbieuphi.Text = drv["GBieu"].ToString();
                lbgiaban.Text = drv["TThu"].ToString();
                lbID.Text = drv["IDKey"].ToString();
                lbtongcong.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(drv["TONGCONG"].ToString(), Resources.labels.lak);
                
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void gvProductList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvProductList.PageIndex = e.NewPageIndex;
            BindData();
        }
        catch
        {
        }
    }
}

