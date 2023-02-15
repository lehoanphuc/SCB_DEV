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

using SmartPortal.Common.Utilities;
using SmartPortal.ExceptionCollection;

public partial class Widgets_SEMSCustomerListCorp_Controls_Widget : System.Web.UI.UserControl
{
    string ACTION = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";

    protected void Page_Load(object sender, EventArgs e)
    {
       
            ACTION = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["a"].ToString().Trim();
            lblError.Text = "";

            if (!IsPostBack)
            {
                BindData();
            }
        
    }

    void BindData()
    {
        try
        {
            //enable(disable) theo action
            switch (ACTION)
            {
                case "viewdetail":
                    txtCustID.Enabled = false;
                    txtFullName.Enabled = false;
                    txtShortName.Enabled = false;
                    ddlCustType.Enabled = false;
                    txtMobi.Enabled = false;              
                    txtEmail.Enabled = false;
                    txtResidentAddress.Enabled = false;                   
                                  
                    txtIF.Enabled = false;
                   
                    txtFAX.Enabled = false;
                    txtBirth.Enabled = false;

                    txtDesc.Enabled = false;
                    btnSave.Visible = false;
                    pnToolbarContract.Visible = false;
                    btnDongBoKH.Visible = false;
                    break;
                case "edit":
                    txtCustID.Enabled = false;
                    txtFullName.Enabled = false;
                    txtShortName.Enabled = true;
                    ddlCustType.Enabled = false;
                    txtMobi.Enabled = false;                   
                   
                    txtEmail.Enabled = true;
                    txtResidentAddress.Enabled = false;
                   
                    txtBirth.Enabled = false;
                    txtIF.Enabled = false;                   
                    txtFAX.Enabled = true;
                   
                   
                    txtDesc.Enabled = true;
                    pnToolbarContract.Visible = false;
                    break;
            }

            #region Lấy thông tin khách hàng
            DataSet ds = new DataSet();
            DataTable custTable = new DataTable();
            ds = (new SmartPortal.SEMS.Customer().GetCustomerByCustcode(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cid"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC));

            if (IPCERRORCODE != "0")
            {
                throw new IPCException(IPCERRORDESC);
            }
            else
            {
                custTable = ds.Tables[0];
            }

            if (custTable.Rows.Count != 0)
            {
                txtCustID.Text = custTable.Rows[0]["CUSTID"].ToString();
                txtFullName.Text = custTable.Rows[0]["FULLNAME"].ToString();
                txtShortName.Text = custTable.Rows[0]["SHORTNAME"].ToString();
                ddlCustType.SelectedValue = custTable.Rows[0]["CFTYPE"].ToString();
                hfCustType.Value = custTable.Rows[0]["CFTYPE"].ToString().Replace(" ","");
                txtBirth.Text = ((DateTime)(custTable.Rows[0]["DOB"])).ToString("dd/MM/yyyy");
                
                txtMobi.Text = custTable.Rows[0]["TEL"].ToString();
                txtEmail.Text = custTable.Rows[0]["EMAIL"].ToString();
                txtResidentAddress.Text = custTable.Rows[0]["ADDR_RESIDENT"].ToString();
               
                txtIF.Text = custTable.Rows[0]["LICENSEID"].ToString();
                txtBirth.Text = SmartPortal.Common.Utilities.Utility.IsDateTime2(custTable.Rows[0]["DOB"].ToString()).ToString("dd/MM/yyyy");
               
               
                txtFAX.Text = custTable.Rows[0]["FAX"].ToString();
               
               
                txtDesc.Text = custTable.Rows[0]["DESCRIPTION"].ToString();
                lblCustCode.Text = custTable.Rows[0]["CFCODE"].ToString();
                
            }


            #endregion

            #region Lấy thông tin hợp đồng
            DataSet contractDS = new DataSet();
            DataTable contractTable = new DataTable();
            contractDS = (new SmartPortal.SEMS.Contract().GetContractByCustcode(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cid"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC));

            if (IPCERRORCODE != "0")
            {
                throw new IPCException(IPCERRORDESC);
            }
            else
            {
                contractTable = contractDS.Tables[0];
            }

            gvcontractList.DataSource = contractTable;
            gvcontractList.DataBind();
            #endregion

            #region Lấy thông tin user
            DataSet userDS = new DataSet();
            DataTable userTable = new DataTable();
            userDS = (new SmartPortal.SEMS.User().GetUserByCustcode(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cid"].ToString(),"", ref IPCERRORCODE, ref IPCERRORDESC));

            if (IPCERRORCODE != "0")
            {
                throw new IPCException(IPCERRORDESC);
            }
            else
            {
                userTable = userDS.Tables[0];
            }

            gvCustomerList.DataSource = userTable;
            gvCustomerList.DataBind();
            #endregion

            //set to session to export
            Session["DataExport"] = userTable;
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSCustomerList_Controls_Widget", "BindData", IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSCustomerList_Controls_Widget", "BindData", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }     
    }

    protected void gvCustomerList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            HyperLink hpFullName;           
            Label lblPhone;
            Label lblEmail;
            Label lblUserType;
            Label lblStatus;
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

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView)e.Row.DataItem;


                cbxSelect = (CheckBox)e.Row.FindControl("cbxSelect");
                e.Row.Attributes.Add("onmousemove", "this.className='hightlight';");
                e.Row.Attributes.Add("onmouseout", "this.className='nohightlight';");


                hpFullName = (HyperLink)e.Row.FindControl("hpUserFullName");                
                lblPhone = (Label)e.Row.FindControl("lblPhone");
                lblEmail = (Label)e.Row.FindControl("lblEmail");
                lblUserType = (Label)e.Row.FindControl("lblUserType");
                lblStatus = (Label)e.Row.FindControl("lblStatus");

                hpEdit = (HyperLink)e.Row.FindControl("hpEdit");
                hpDelete = (HyperLink)e.Row.FindControl("hpDelete");

                //cbxSelect.Enabled = true;
                //cbxSelect.Attributes.Add("onclick", "HighLightCBX('" + e.Row.ClientID + "',this)");


                hpFullName.Text = drv["FULLNAME"].ToString();
                //hpFullName.NavigateUrl = "~/Default.aspx?p=157&a=viewdetail&uid=" + drv["USERID"].ToString();
                
                lblPhone.Text = drv["PHONE"].ToString();
                lblEmail.Text = drv["EMAIL"].ToString();
                lblUserType.Text = drv["USERTYPE"].ToString();

                switch (drv["STATUS"].ToString().Trim())
                {
                    case SmartPortal.Constant.IPC.NEW:
                        lblStatus.Text = "New";
                        break;
                    case SmartPortal.Constant.IPC.DELETE:
                        lblStatus.Text = "Deleted";
                        break;
                    case SmartPortal.Constant.IPC.ACTIVE:
                        lblStatus.Text = "Active";
                        break;
                }
                
                        hpEdit.Text = "Resources.labels. edit ";
                        hpEdit.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=157&a=edit&uid=" + drv["USERID"].ToString());
                        hpDelete.Text = "Resources.labels. delete ";
                        hpDelete.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=167&uid=" + drv["USERID"].ToString() + "&returnUrl=" + SmartPortal.Common.Encrypt.EncryptData(HttpContext.Current.Request.Url.PathAndQuery));
                    
                                          
            }
        }
        catch(Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSCustomerList_Controls_Widget", "gvCustomerList_RowDataBound", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=224"));
    }

    protected void gvcontractList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            HyperLink hpcontractCode;
            Label lblcustName;
            Label lblOpen;
            Label lblOpendate;
            Label lblClosedate;
            Label lblcontractType;
            Label lblStatus;
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

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView)e.Row.DataItem;


                cbxSelect = (CheckBox)e.Row.FindControl("cbxSelect");

                if (drv["STATUS"].ToString().Trim() == SmartPortal.Constant.IPC.DELETE)
                {
                    cbxSelect.Enabled = false;
                }

                e.Row.Attributes.Add("onmousemove", "this.className='hightlight';");
                e.Row.Attributes.Add("onmouseout", "this.className='nohightlight';");


                hpcontractCode = (HyperLink)e.Row.FindControl("hpcontractCode");
                lblcustName = (Label)e.Row.FindControl("lblcustName");
                lblOpen = (Label)e.Row.FindControl("lblOpen");
                lblOpendate = (Label)e.Row.FindControl("lblOpendate");
                lblClosedate = (Label)e.Row.FindControl("lblClosedate");
                lblcontractType = (Label)e.Row.FindControl("lblcontractType");
                lblStatus = (Label)e.Row.FindControl("lblStatus");

                hpEdit = (HyperLink)e.Row.FindControl("hpEdit");
                hpDelete = (HyperLink)e.Row.FindControl("hpDelete");
                //cbxSelect.Enabled = true;
                //cbxSelect.Attributes.Add("onclick", "HighLightCBX('" + e.Row.ClientID + "',this)");


                hpcontractCode.Text = drv["CONTRACTNO"].ToString();

                //hpcontractCode.NavigateUrl = "~/Default.aspx?p=247&a=viewdetail&cn=" + drv["CONTRACTNO"].ToString();
                lblcustName.Text = drv["FULLNAME"].ToString();
                lblOpen.Text = drv["USERCREATE"].ToString();
                lblOpendate.Text = SmartPortal.Common.Utilities.Utility.IsDateTime2(drv["CREATEDATE"].ToString()).ToString("dd/MM/yyyy");
                lblClosedate.Text = SmartPortal.Common.Utilities.Utility.IsDateTime2(drv["ENDDATE"].ToString()).ToString("dd/MM/yyyy");

                DataSet dsUserType = new DataSet();
                dsUserType = new SmartPortal.SEMS.Services().GetAllUserType(drv["USERTYPE"].ToString().Trim(),"", ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    DataTable dtUserType = new DataTable();
                    dtUserType = dsUserType.Tables[0];

                    if (dtUserType.Rows.Count != 0)
                    {
                        lblcontractType.Text = dtUserType.Rows[0]["TYPENAME"].ToString();
                    }
                }
                else
                {
                    throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
                }
                

                switch (drv["STATUS"].ToString().Trim())
                {
                    case SmartPortal.Constant.IPC.NEW:
                        lblStatus.Text = "New";
                        break;
                    case SmartPortal.Constant.IPC.DELETE:
                        lblStatus.Text = "Deleted";
                        break;
                    case SmartPortal.Constant.IPC.ACTIVE:
                        lblStatus.Text = "Active";
                        break;
                    case SmartPortal.Constant.IPC.PENDING:
                        lblStatus.Text = "Pending to approve";
                        break;
                }
                if (drv["STATUS"].ToString().Trim() == SmartPortal.Constant.IPC.DELETE)
                {
                    hpEdit.Text = "<img src='widgets/semscontractlist/images/stop.gif'/>";
                    hpDelete.Text = "<img src='widgets/semscontractlist/images/stop.gif'/>";
                }
                else
                {
                    hpEdit.Text = "<img src='Widgets/Pages/view/images/icon_edit.gif'/>";
                    hpEdit.ToolTip = Resources.labels.edit;
                    hpEdit.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=248&a=edit&cn=" + drv["CONTRACTNO"].ToString() + "&returnUrl=" + SmartPortal.Common.Encrypt.EncryptData(HttpContext.Current.Request.Url.PathAndQuery));

                    hpDelete.Text = "<img src='Widgets/Pages/view/images/icon_delete.gif'/>";
                    hpDelete.ToolTip = Resources.labels.delete;
                    hpDelete.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=166&cn=" + drv["CONTRACTNO"].ToString() + "&returnUrl=" + SmartPortal.Common.Encrypt.EncryptData(HttpContext.Current.Request.Url.PathAndQuery));
                }
                  
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSCustomerList_Controls_Widget", "gvcontractList_RowDataBound", IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
        catch(Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSCustomerList_Controls_Widget", "gvcontractList_RowDataBound", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string DOB = txtBirth.Text.Trim();
            string ISSUEDATE = txtBirth.Text.Trim();

            if (DOB == "")
            {
                DOB = "";
            }

            if (ISSUEDATE == "")
            {
                ISSUEDATE = "";
            }

            DataSet ds = new SmartPortal.SEMS.Customer().Update(Utility.KillSqlInjection(txtCustID.Text.Trim()),Utility.KillSqlInjection(txtFullName.Text.Trim()),Utility.KillSqlInjection(txtShortName.Text.Trim()),Utility.KillSqlInjection(DOB),Utility.KillSqlInjection(txtResidentAddress.Text),Utility.KillSqlInjection(""),"","",Utility.KillSqlInjection(txtMobi.Text),Utility.KillSqlInjection(txtFAX.Text),Utility.KillSqlInjection(txtEmail.Text.Trim()),"",Utility.KillSqlInjection(txtIF.Text.Trim()),Utility.KillSqlInjection(ISSUEDATE),Utility.KillSqlInjection(txtIF.Text.Trim()),Utility.KillSqlInjection(txtDesc.Text.Trim()),Utility.KillSqlInjection(""),Utility.KillSqlInjection(""),Utility.KillSqlInjection(""),ddlCustType.SelectedValue,"",ref IPCERRORCODE,ref IPCERRORDESC);
            if (IPCERRORCODE != "0")
            {
                throw new IPCException(IPCERRORDESC);                
            }
           
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSCustomerList_Controls_Widget", "btnSave_Click", IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSCustomerList_Controls_Widget", "btnSave_Click", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=138"));
    }

    protected void Button6_Click(object sender, EventArgs e)
    {
        CheckBox cbxDelete;
        HyperLink hpContractNo;

        string Str_ContractNo = "";
        try
        {
            foreach (GridViewRow gvr in gvcontractList.Rows)
            {
                cbxDelete = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                if (cbxDelete.Checked == true)
                {
                    hpContractNo = (HyperLink)gvr.Cells[1].FindControl("hpcontractCode");
                    Str_ContractNo += hpContractNo.Text.Trim() + "#";
                }
            }           
            
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSCustomerList_Controls_Widget", "Button6_Click", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
        if (Str_ContractNo == "")
        {
            lblError.Text = "Bạn cần chọn hợp đồng để đóng.";
        }
        else
        {
            Session["_ContractNo"] = Str_ContractNo.Substring(0, Str_ContractNo.Length - 1);
           Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=166" + "&returnUrl=" + SmartPortal.Common.Encrypt.EncryptData(HttpContext.Current.Request.Url.PathAndQuery)));
        }
    }


    protected void btnDongBoKH_Click(object sender, EventArgs e)
    {
        try
        {
        string custName = "";
        string birth = "";
        string mobi = "";
        string email = "";
        string residentadd = "";
        string license = "";
        string issuedate = "";
        string issueplace = "";
        string officeAdd = "";
        string gender = "";

        Hashtable hasCustInfo = new Hashtable();
        //VuTran 07052015: sua loi dong bo contract tu core

        hasCustInfo = new SmartPortal.SEMS.Customer().GetCustInfo(lblCustCode.Text, hfCustType.Value, ref IPCERRORCODE, ref IPCERRORDESC);
        

        if (IPCERRORCODE != "0")
        {
            throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.CUSTNOTEXIST);
        }
        if (hasCustInfo[SmartPortal.Constant.IPC.CUSTCODE] == null)
        {
            throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.CUSTNOTEXIST);
        }
       

        if (hasCustInfo[SmartPortal.Constant.IPC.CUSTNAME] != null)
        {
            custName = hasCustInfo[SmartPortal.Constant.IPC.CUSTNAME].ToString();
        }

        

        if (hasCustInfo[SmartPortal.Constant.IPC.DOB] != null)
        {
            try
            {
                birth= SmartPortal.Common.Utilities.Utility.IsDateTime2(hasCustInfo[SmartPortal.Constant.IPC.DOB].ToString()).ToString("dd/MM/yyyy");

                
            }
            catch
            {
            }
        }

        if (hasCustInfo[SmartPortal.Constant.IPC.PHONE] != null)
        {
            mobi = hasCustInfo[SmartPortal.Constant.IPC.PHONE].ToString();

            
        }

        if (hasCustInfo[SmartPortal.Constant.IPC.EMAIL] != null)
        {
            email= hasCustInfo[SmartPortal.Constant.IPC.EMAIL].ToString();

            
        }

        if (hasCustInfo[SmartPortal.Constant.IPC.ADDRESS] != null)
        {
            residentadd = hasCustInfo[SmartPortal.Constant.IPC.ADDRESS].ToString();

            
        }

        if (hasCustInfo[SmartPortal.Constant.IPC.LICENSE] != null)
        {
           license = hasCustInfo[SmartPortal.Constant.IPC.LICENSE].ToString();

          
        }
        if (birth == "")
        {
            birth = "01/01/1900";
        }

        if (issuedate == "")
        {
            issuedate = "01/01/1900";
        }
        //cap nhat dong bo
        new SmartPortal.SEMS.Customer().DongBoKH(txtCustID.Text, custName, birth, mobi, email, residentadd, license, issuedate, issueplace, officeAdd, (gender == "1") ? "M" : "F");
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSCustomerList_Controls_Widget", "btnSave_Click", IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSCustomerList_Controls_Widget", "btnSave_Click", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
        lblError.Text = Resources.labels.dongbothanhcong;
    }
}
