using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.Common;
using SmartPortal.Common.Utilities;

public partial class Widgets_SEMSPoster_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }

    void BindData()
    {
        DataSet dtCL = new DataSet();
        dtCL = new SmartPortal.SEMS.Poster().GetAllPoster(ref IPCERRORCODE, ref IPCERRORDESC);
     
        if (IPCERRORCODE == "0")
        {
            gvPosterList.DataSource = dtCL;
            gvPosterList.DataBind();
        }
        else
        {
            lblError.Text = IPCERRORDESC;
        }

        litPager.Text = Resources.labels.danghienthi + " <b>" + (((gvPosterList.PageIndex) * gvPosterList.PageSize) + gvPosterList.Rows.Count).ToString() + "</b> " + Resources.labels.cua + " <b>" + dtCL.Tables[0].Rows.Count.ToString() + "</b> " + Resources.labels.dong;
    }


    protected void btnAddNew_Click(object sender, EventArgs e)
    {
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=417&a=add"));
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        CheckBox cbxDelete;
        HiddenField hpPosterID;

        string Str_PosterID = "";
        try
        {
            foreach (GridViewRow gvr in gvPosterList.Rows)
            {
                cbxDelete = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                if (cbxDelete.Checked == true)
                {
                    hpPosterID = (HiddenField)gvr.Cells[0].FindControl("hdfPosterId");
                    Str_PosterID += hpPosterID.Value.Trim() + "#";
                }
            }
            Session["_PosterID"] = Str_PosterID.Substring(0, Str_PosterID.Length - 1);
        }
        catch (Exception ex)
        {
        }
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=419"));
    }

    protected void gvPosterList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            Label lblFileName;
            Label lblPath;
            Label lblWidth;
            Label lblHeight;
            Label lblType;
            Label lblPosition;
            Label lblIndex;
            Label lblPublish;
            HiddenField hdfPosterId;
            HyperLink hpEdit;
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

                hdfPosterId = (HiddenField)e.Row.FindControl("hdfPosterId");
                lblFileName = (Label)e.Row.FindControl("lblFileName");
                lblPath = (Label)e.Row.FindControl("lblPath");
                lblWidth = (Label)e.Row.FindControl("lblWidth");
                lblHeight = (Label)e.Row.FindControl("lblHeight");
                lblType = (Label)e.Row.FindControl("lblType");
                lblPosition = (Label)e.Row.FindControl("lblPosition");
                lblIndex = (Label)e.Row.FindControl("lblIndex");
                lblPublish = (Label)e.Row.FindControl("lblPublish");

                hpEdit = (HyperLink)e.Row.FindControl("hpEdit");
                hpDelete = (HyperLink)e.Row.FindControl("hpDelete");

                hdfPosterId.Value = drv["PosterID"].ToString();
                lblFileName.Text = drv["FileName"].ToString();
                lblPath.Text = drv["Path"].ToString();
                lblWidth.Text = drv["Width"].ToString() + " px";
                lblHeight.Text = drv["Height"].ToString() + " px";
                lblType.Text = drv["Type"].ToString();
                lblPosition.Text = drv["Position"].ToString();
                lblIndex.Text = drv["Idx"].ToString();
                lblPublish.Text = drv["Publish"].ToString();

                hpEdit.Text = Resources.labels.edit;
                hpEdit.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=418&a=edit&pid=" + drv["PosterID"].ToString());
                hpDelete.Text = Resources.labels.delete;
                hpDelete.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=419&pid=" + drv["PosterID"].ToString());
            }
        }
        catch
        {
        }
    }
}