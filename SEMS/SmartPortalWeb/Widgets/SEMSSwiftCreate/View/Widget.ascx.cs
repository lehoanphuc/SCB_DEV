using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using SmartPortal.Common;
using SmartPortal.Common.Utilities;
using System.Text;
using SmartPortal.ExceptionCollection;
using System.Collections.Generic;

public partial class Widgets_SEMSSwiftCreate_Widget : WidgetBase
{
    public static bool isAscend = false;
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    string IPCTRANCODE = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        lblError.Text = "";

            if (!IsPostBack)
            {
                #region Load thông tin ngân hàng
                ddlBank.DataSource = new SmartPortal.SEMS.Bank().Load();
                ddlBank.DataTextField = "BANKNAME";
                ddlBank.DataValueField = "BANKCODE";
                ddlBank.DataBind();
                #endregion
                BindData();
            }
        
    }

    void BindData()
    {
        try
        {

            if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["tc"] == null)
            {
                IPCTRANCODE = SmartPortal.Constant.IPC.IBTRANSFEROUTBANK;
            }
            else
            {
                IPCTRANCODE = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["tc"].ToString();                
            }

            DataSet dtContr = new DataSet();
            dtContr = new SmartPortal.SEMS.Transactions().LoadAllTranForSwiftCreate(txtTranID.Text.Trim(), txtFrom.Text.Trim(), txtTo.Text.Trim(), "", "ALL", IPCTRANCODE, "ALL",Session["branch"].ToString(),ddlBank.SelectedValue, ref IPCERRORCODE, ref IPCERRORDESC);


            if (IPCERRORCODE == "0")
            {
                if (dtContr.Tables[0].Rows.Count == 0)
                {
                    pnbutton.Visible = false;

                }
                else { pnbutton.Visible = true; }
                gvcontractList.DataSource = dtContr;
                gvcontractList.DataBind();

            }
            else
            {
                goto ERROR;
            }

            litPager.Text = Resources.labels.danghienthi + " <b>" + (((gvcontractList.PageIndex) * gvcontractList.PageSize) + gvcontractList.Rows.Count).ToString() + "</b> " + Resources.labels.cua + " <b>" + dtContr.Tables[0].Rows.Count.ToString() + "</b> " + Resources.labels.dong;

            goto EXIT;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSContractApprove_Widget", "BindData", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    ERROR:
        SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSContractApprove_Widget", "BindData", IPCERRORDESC, Request.Url.Query);
        SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
    EXIT:

        ;

    }
    protected void gvcontractList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            HyperLink hpTranID;
            Label lblDate;
            Label lblAmount;
            Label lblDesc;
            Label lblBank;
            Label lblCity;
            

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


                hpTranID = (HyperLink)e.Row.FindControl("hpTranID");
                lblDate = (Label)e.Row.FindControl("lblDate");
                lblAmount = (Label)e.Row.FindControl("lblAmount");
                lblDesc = (Label)e.Row.FindControl("lblDesc");
                lblBank = (Label)e.Row.FindControl("lblBank");
                lblCity = (Label)e.Row.FindControl("lblCity");


                hpTranID.Text = drv["IPCTRANSID"].ToString();
                hpTranID.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=213&tranid=" + drv["IPCTRANSID"].ToString() + "&returnUrl=" + SmartPortal.Common.Encrypt.EncryptData(HttpContext.Current.Request.Url.PathAndQuery));
                
                lblAmount.Text =SmartPortal.Common.Utilities.Utility.FormatMoney(drv["NUM01"].ToString(),"")+ Resources.labels.lak;
                lblDate.Text = SmartPortal.Common.Utilities.Utility.IsDateTime2(drv["IPCTRANSDATE"].ToString()).ToString("dd/MM/yyyy HH:mm");
                lblBank.Text = drv["BANKNAME"].ToString();
                lblCity.Text = drv["CITYNAME"].ToString();                
            }
        }
        catch(Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSContractApprove_Widget", "gvcontractList_RowDataBound", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    protected void gvcontractList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvcontractList.PageIndex = e.NewPageIndex;
            BindData();
        }
        catch
        {
        }
    }
    protected void gvcontractList_Sorting(object sender, GridViewSortEventArgs e)
    {
        //isSort = true;

        string sortExpression = e.SortExpression;

        ViewState["SortExpression"] = sortExpression;
        //showImage = true;

        if (GridViewSortDirection == SortDirection.Ascending)
        {
            //isAscend = true;

            SortGridView(sortExpression, ASCENDING);
            GridViewSortDirection = SortDirection.Descending;

        }

        else
        {
            isAscend = false;
            SortGridView(sortExpression, DESCENDING);
            GridViewSortDirection = SortDirection.Ascending;

        }
    }

    private SortDirection GridViewSortDirection
    {
        get
        {

            if (ViewState["sortDirection"] == null)

                ViewState["sortDirection"] = SortDirection.Ascending;


            return (SortDirection)ViewState["sortDirection"];

        }

        set { ViewState["sortDirection"] = value; }

    }

    protected void SortGridView(string sortExpression, string direction)
    {
        DataTable dataTable;

        if (ViewState["s"] != null)
        {
            dataTable = null;
        }

        else
        {
            if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["letter"] != null)
            {
                dataTable = null;

            }
            else
            {
                dataTable = null;

            }
        }


        if (dataTable != null)
        {
            DataView dataView = new DataView(dataTable);
            dataView.Sort = sortExpression + direction;

            gvcontractList.DataSource = dataView;
            gvcontractList.DataBind();
        }

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }
   

    void SendInfoLogin()
    {
        Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();
        tmpl=SmartPortal.Common.ST.GetStringTemplate("SEMSContractApprove", "ContractApprove" + System.Globalization.CultureInfo.CurrentCulture.ToString());
        
        //lay thong tin hop dong de gui mail
        CheckBox cbxApprove;
        HyperLink hpcontractNo;
        DataSet ApproveContractTable = new DataSet();
        try
        {
            foreach (GridViewRow gvr in gvcontractList.Rows)
            {
                cbxApprove = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                if (cbxApprove.Checked == true)
                {
                    hpcontractNo = (HyperLink)gvr.Cells[1].FindControl("hpcontractCode");
                    string custID = "";
                    tmpl.Reset();

                    //lay thong tin hop dong boi contractno
                    DataTable contractTable = (new SmartPortal.SEMS.Contract().GetContractByContractNo(hpcontractNo.Text.Trim(), ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
                    if (IPCERRORCODE != "0")
                    {
                        goto ERROR;
                    }
                    //gan thong tin hop dong vao stringtemplate
                    if(contractTable.Rows.Count!=0)
                    {
                        tmpl.SetAttribute("CONTRACTNO",contractTable.Rows[0]["CONTRACTNO"].ToString());
                        DataSet dsUserType = new DataSet();
                        dsUserType = new SmartPortal.SEMS.Services().GetAllUserType(contractTable.Rows[0]["USERTYPE"].ToString().Trim(),"", ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE == "0")
                        {
                            DataTable dtUserType = new DataTable();
                            dtUserType = dsUserType.Tables[0];

                            if (dtUserType.Rows.Count != 0)
                            {
                                tmpl.SetAttribute("CONTRACTTYPE",dtUserType.Rows[0]["TYPENAME"].ToString());
                            }
                        }
                        else
                        {
                            throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
                        }


                        tmpl.SetAttribute("OPENDATE", SmartPortal.Common.Utilities.Utility.IsDateTime2(contractTable.Rows[0]["CREATEDATE"].ToString()).ToString("dd/MM/yyyy"));
                        tmpl.SetAttribute("ENDDATE", SmartPortal.Common.Utilities.Utility.IsDateTime2(contractTable.Rows[0]["ENDDATE"].ToString()).ToString("dd/MM/yyyy"));


                        tmpl.SetAttribute("PRODUCT", contractTable.Rows[0]["PRODUCTNAME"].ToString());
                        
                        
                        custID = contractTable.Rows[0]["CUSTID"].ToString();
                    }

                    //lay thong tin khach hang
                    DataTable custTable = (new SmartPortal.SEMS.Customer().GetCustomerByCustcode(custID, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
                    if (IPCERRORCODE != "0")
                    {
                        goto ERROR;
                    }
                    //gan thong tin hop dong vao stringtemplate
                    if (custTable.Rows.Count != 0)
                    {
                        tmpl.SetAttribute("CUSTID", custTable.Rows[0]["CUSTID"].ToString());
                        tmpl.SetAttribute("FULLNAME", custTable.Rows[0]["FULLNAME"].ToString());
                        switch (custTable.Rows[0]["CFTYPE"].ToString().Trim())
                        {
                            case SmartPortal.Constant.IPC.PERSONAL:
                                tmpl.SetAttribute("CUSTTYPE", Resources.labels.canhan);
                                break;
                            case SmartPortal.Constant.IPC.CORPORATE:
                                tmpl.SetAttribute("CUSTTYPE", Resources.labels.doanhnghiep);
                                break;
                        }
                        tmpl.SetAttribute("PHONE", custTable.Rows[0]["TEL"].ToString());
                        tmpl.SetAttribute("EMAIL", custTable.Rows[0]["EMAIL"].ToString());
                    }

                    //lay thong tin tai khoan
                    DataTable userTable = (new SmartPortal.SEMS.Contract().GetUserByContractNo(hpcontractNo.Text.Trim(),"","", ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
                    if (IPCERRORCODE != "0")
                    {
                        goto ERROR;
                    }

                                        
                    //gan thong tin user vao stringtemplate
                    foreach (DataRow row in userTable.Rows)
                    {
                        
                        StringBuilder st = new StringBuilder();
                        
                        st.Append("<table style='width:100%;'>");

                        st.Append("<tr>");
                        st.Append("<td>");
                        st.Append("Tên đầy đủ :");
                        st.Append("</td>");
                        st.Append("<td>");
                        st.Append(row["FULLNAME"].ToString());
                        st.Append("</td>");
                        st.Append("<td>");
                        st.Append("Email :");
                        st.Append("</td>");
                        st.Append("<td>");
                        st.Append(row["EMAIL"].ToString());
                        st.Append("</td>");
                        st.Append("</tr>");

                        st.Append("<tr>");
                        st.Append("<td>");
                        st.Append("Điện thoại :");
                        st.Append("</td>");
                        st.Append("<td>");
                        st.Append(row["PHONE"].ToString());
                        st.Append("</td>");
                        st.Append("<td>");
                        st.Append("");
                        st.Append("</td>");
                        st.Append("<td>");
                        st.Append("");
                        st.Append("</td>");
                        st.Append("</tr>");

                        //lay het các tai khoan Ibank cua user theo userID
                        DataSet accountIBDataset= new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.IB,row["USERID"].ToString().Trim(), string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);

                        if (IPCERRORCODE != "0")
                        {
                            goto ERROR;
                        }

                        DataTable accountIBTable = accountIBDataset.Tables[0];
                        if (accountIBTable.Rows.Count != 0)
                        {
                            
                            st.Append("<tr>");
                            st.Append("<td colspan='4'>");
                            st.Append("<B>INTERNET BANKING</B><hr/>");
                            st.Append("</td>");
                            st.Append("</tr>");

                            st.Append("<tr>");
                            st.Append("<td>");
                            st.Append(Resources.labels.username);
                            st.Append("</td>");
                            st.Append("<td>");
                            st.Append("<b>"+accountIBTable.Rows[0]["USERNAME"].ToString()+"</b>");
                            st.Append("</td>");
                            st.Append("<td>");
                            st.Append(Resources.labels.matkhau);
                            st.Append("</td>");
                            st.Append("<td>");
                            st.Append("<b>"+SmartPortal.Security.Encryption.Decrypt(accountIBTable.Rows[0]["PASSWORD"].ToString())+"</b>");
                            st.Append("</td>");
                            st.Append("</tr>");
                        }

                        //lay het các tai khoan SMS cua user theo userID
                        DataSet accountSMSDataset = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.SMS, row["USERID"].ToString().Trim(),string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);

                        if (IPCERRORCODE != "0")
                        {
                            goto ERROR;
                        }

                        DataTable accountSMSTable = accountSMSDataset.Tables[0];
                        if (accountSMSTable.Rows.Count != 0)
                        {
                            st.Append("<tr>");
                            st.Append("<td colspan='4'>");
                            st.Append("<B>SMS</B><hr/>");
                            st.Append("</td>");
                            st.Append("</tr>");

                            st.Append("<tr>");
                            st.Append("<td>");
                            st.Append(Resources.labels.phone );
                            st.Append("</td>");
                            st.Append("<td>");
                            st.Append("<b>"+accountSMSTable.Rows[0]["UN"].ToString()+"</b>");
                            st.Append("</td>");
                            st.Append("<td>");
                            st.Append(Resources.labels.taikhoanmacdinh );
                            st.Append("</td>");
                            st.Append("<td>");
                            st.Append("<b>"+accountSMSTable.Rows[0]["DEFAULTACCTNO"].ToString()+"</b>");
                            st.Append("</td>");
                            st.Append("</tr>");
                        }

                        //lay het các tai khoan MB cua user theo userID
                        DataSet accountMBDataset = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.MB, row["USERID"].ToString().Trim(), string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);

                        if (IPCERRORCODE != "0")
                        {
                            goto ERROR;
                        }

                        DataTable accountMBTable = accountMBDataset.Tables[0];
                        if (accountMBTable.Rows.Count != 0)
                        {
                            st.Append("<tr>");
                            st.Append("<td colspan='4'>");
                            st.Append("<B>MOBILE</B><hr/>");
                            st.Append("</td>");
                            st.Append("</tr>");

                            st.Append("<tr>");
                            st.Append("<td>");
                            st.Append(Resources.labels.phone );
                            st.Append("</td>");
                            st.Append("<td>");
                            st.Append("<b>"+accountMBTable.Rows[0]["UN"].ToString()+"</b>");
                            st.Append("</td>");
                            st.Append("<td>");
                            st.Append(Resources.labels.matkhau);
                            st.Append("</td>");
                            st.Append("<td>");
                            st.Append("<b>"+SmartPortal.Security.Encryption.Decrypt(accountMBTable.Rows[0]["PASS"].ToString())+"</b>");
                            st.Append("</td>");
                            st.Append("</tr>");
                        }

                        //lay het các tai khoan PHO cua user theo userID
                        DataSet accountPHODataset = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.PHO, row["USERID"].ToString().Trim(), string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);

                        if (IPCERRORCODE != "0")
                        {
                            goto ERROR;
                        }

                        DataTable accountPHOTable = accountPHODataset.Tables[0];
                        if (accountPHOTable.Rows.Count != 0)
                        {
                            st.Append("<tr>");
                            st.Append("<td colspan='4'>");
                            st.Append("<B>PHONE</B><hr/>");
                            st.Append("</td>");
                            st.Append("</tr>");

                            st.Append("<tr>");
                            st.Append("<td>");
                            st.Append(Resources.labels.phone );
                            st.Append("</td>");
                            st.Append("<td>");
                            st.Append("<b>"+accountPHOTable.Rows[0]["UN"].ToString()+"</b>");
                            st.Append("</td>");
                            st.Append("<td>");
                            st.Append(Resources.labels.matkhau);
                            st.Append("</td>");
                            st.Append("<td>");
                            st.Append("<b>"+SmartPortal.Security.Encryption.Decrypt(accountPHOTable.Rows[0]["PASS"].ToString())+"</b>");
                            st.Append("</td>");
                            st.Append("</tr>");
                        }

                        st.Append("</table>");

                        tmpl.SetAttribute("USERINFO", st.ToString());

                        //send mail
                        SmartPortal.Common.EmailHelper.SendMailMessageAsync(System.Configuration.ConfigurationManager.AppSettings["contractapprovemailfrom"], row["EMAIL"].ToString(), System.Configuration.ConfigurationManager.AppSettings["contractapprovemailtitle"], tmpl.ToString());

                        tmpl.RemoveAttribute("USERINFO");
                    }
                }                
              
            }
            goto EXIT;            
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSContractApprove_Widget", "SendInfoLogin", IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSContractApprove_Widget", "SendInfoLogin", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    ERROR:
        SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSContractApprove_Widget", "SendInfoLogin", IPCERRORDESC, Request.Url.Query);
        SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
    EXIT:
        
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=148"));
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        //Duyệt giao dịch TUANTA

        CheckBox cbxSelect;
        HyperLink hpTranID;
        List<string> lstTran = new List<string>();
        try
        {
            foreach (GridViewRow gvr in gvcontractList.Rows)
            {
                cbxSelect = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                if (cbxSelect.Checked == true)
                {
                    hpTranID = (HyperLink)gvr.Cells[1].FindControl("hpTranID");

                    //approve
                    //new SmartPortal.SEMS.Transactions().TellerApp(hpTranID.Text.Trim(), ref IPCERRORCODE, ref IPCERRORDESC);

                    lstTran.Add(hpTranID.Text.Trim());
                }
            }

        }
        catch (Exception ex)
        {

        }
        if (lstTran.Count != 0)
        {
            Session["tranID"] = lstTran;
           Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=213"+"&returnURL="+SmartPortal.Common.Encrypt.EncryptData(Request.Url.Query)));
        }
        else
        {
            lblError.Text = "Bạn vui lòng chọn giao dịch cần tạo điện";
            BindData();
        }
        
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        ////từ chối hợp đồng -Quyềnnnpv
        //Approvereject(SmartPortal.Constant.IPC.BLOCK);

    }
    
}
