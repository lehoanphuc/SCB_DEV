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
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;

public partial class Widgets_IBCorpUserLimit_Widget : WidgetBase
{
    public static bool isAscend = false;
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ltrError.Text = "";

            if (!IsPostBack)
            {
                //load user
                DataSet ds = new SmartPortal.IB.Transactions().GetUserOfContractNoByUID(Utility.KillSqlInjection(Session["userID"].ToString()), "IB", ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE != "0")
                {
                    throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORCODE);
                }

                DataTable deptTable = new DataTable();
                deptTable = ds.Tables[0];

                lstDept.DataSource = deptTable;
                lstDept.DataTextField = "FULLNAME";
                lstDept.DataValueField = "USERID";
                lstDept.DataBind();

                if (lstDept.Items.Count != 0)
                {
                    lstDept.Items[0].Selected = true;
                }
            }
            BindData();
            goto EXIT;
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.Message, this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_IBCorpUserLimit_Widget", "Page_Load", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }


        EXIT:;

    }
    void BindData()
    {
        try
        {
            ltrError.Text = "";

            DataSet dtLimitTeller = new DataSet();
            dtLimitTeller = new SmartPortal.IB.Transactions().GetAllLimitUser(Utility.KillSqlInjection(lstDept.SelectedValue), "IB", "","","", ref IPCERRORCODE, ref IPCERRORDESC);


            if (IPCERRORCODE == "0")
            {
                rptUser.DataSource = dtLimitTeller.Tables[0];
                rptUser.DataBind();
            }
            else
            {
                throw new IPCException(IPCERRORDESC);
            }

            //if (dtLimitTeller.Tables[0].Rows.Count != 0)
            //{
            //    litPager.Text = Resources.labels.danghienthi+" <b>" + (((gvUser.PageIndex) * gvUser.PageSize) + gvUser.Rows.Count).ToString() + "</b> "+Resources.labels.cua+" <b>" + dtLimitTeller.Tables[0].Rows.Count.ToString() + "</b> "+Resources.labels.dong;
            //}
            //else
            //{
            //    ltrError.Text = "<table rules='all' cellspacing='0' cellpadding='3' border='1' style='background-color: White; border: 1px solid rgb(204, 204, 204); width: 100%; border-collapse: collapse;' id='ctl00_ctl11_gvUser'><tbody><tr style='color: White; background-color: rgb(52, 154, 192); font-weight: bold;'><th scope='col' class='gvHeader'><input type='checkbox' onclick='SelectCbx(this);' name='ctl00$ctl11$gvUser$ctl01$cbxSelectAll' id='ctl00_ctl11_gvUser_ctl01_cbxSelectAll'></th><th scope='col' class='gvHeader'>"+Resources.labels.giaodich+"</th><th scope='col' class='gvHeader'>"+Resources.labels.hanmuctrengiaodich+"</th><th scope='col' class='gvHeader'>"+Resources.labels.tonghanmuctrenngay+"</th><th scope='col' class='gvHeader'>"+Resources.labels.sogiaodichtrenngay+"</th><th scope='col' class='gvHeader'>"+Resources.labels.tiente+"</th><th scope='col' class='gvHeader'>"+Resources.labels.sua+"</th><th scope='col' class='gvHeader'>"+Resources.labels.xoa+"</th></tr></tbody><tr><th colspan='8'><center><span style='font-weight:bold;color:red;'>"+Resources.labels.khongtimthaydulieu+"</span></center></th></tr></table>";
            //}


            if (lstDept.Items.Count == 0)
            {
                lbUserInsert.Visible = false;
                lbDeleteLimit.Visible = false;
            }
            else
            {
                lbUserInsert.Visible = true;

                if (dtLimitTeller.Tables[0].Rows.Count == 0)
                {
                    lbDeleteLimit.Visible = false;
                }
                else
                {
                    lbDeleteLimit.Visible = true;
                }
            }

        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.Message, this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_IBCorpUserLimit_Widget", "BindData", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void lstDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = "";
            BindData();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSSetLimitTeller_Widget", "lstDept_SelectedIndexChanged", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }


    }
    protected void lbUserInsert_Click(object sender, EventArgs e)
    {
        RedirectToActionPage(IPC.ACTIONPAGE.ADD, "&uid=" + lstDept.SelectedValue, false);
    }
    protected void rptUser_OnItemCommandommand(object source, RepeaterCommandEventArgs e)
    {
        string commandName = e.CommandName;
        if (!IBCheckPermitPageAction(commandName)) return;

        string commandArg = e.CommandArgument.ToString();
        string[] pros = commandArg.Split('|');
        switch (commandName)
        {
            case IPC.ACTIONPAGE.EDIT:
                RedirectToActionPage(IPC.ACTIONPAGE.EDIT, "&uid=" + pros[0] , false);
                break;
            case IPC.ACTIONPAGE.DELETE:
                new SmartPortal.IB.Transactions().DeleteCorpUserLimit(pros[0], pros[1], pros[2], ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    BindData();
                    lblError.Text = Resources.labels.xoahanmucthanhcong;
                }
                else
                {
                    if (IPCERRORCODE == "91780")
                    {
                        lblError.Text = IPCERRORDESC;
                    }
                    else
                    {
                        throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.IPC);
                    }
                }
                break;
            default:
                return;
        }
    }
    protected void rptUser_OnItemDataBounddaBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            RepeaterItem item = e.Item;
            if (item.ItemType == ListItemType.AlternatingItem || item.ItemType == ListItemType.Item)
            {
                LinkButton lbEdit = (LinkButton)e.Item.FindControl("lbEdit");
                LinkButton lbDelete = (LinkButton)e.Item.FindControl("lbDelete");

                if (!IBCheckPermitPageAction(IPC.ACTIONPAGE.EDIT))
                {
                    lbEdit.Enabled = false;
                    lbEdit.OnClientClick = string.Empty;
                }

                if (!IBCheckPermitPageAction(IPC.ACTIONPAGE.DELETE))
                {
                    lbDelete.Enabled = false;
                    lbDelete.OnClientClick = string.Empty;
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    protected void lbDeleteLimit_OnClick(object sender, EventArgs e)
    {
        try
        {
            if (!IBCheckPermitPageAction(IPC.ACTIONPAGE.DELETE)) return;
            string lst = hdId.Value;
            if (lst.Length > 0)
            {
                string[] arrSplit = lst.Split('#');
                foreach (string item in arrSplit)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        string[] pros = item.Split('|');
                        //new SmartPortal.IB.CorpUser().DeleteCorpUserlevel(pros[0], pros[1], ref IPCERRORCODE,
                        //    ref IPCERRORDESC);
                        new SmartPortal.IB.Transactions().DeleteCorpUserLimit(pros[0], pros[1], pros[2], ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE == "0")
                        {
                            BindData();
                            lblError.Text = Resources.labels.xoahanmucthanhcong;
                        }
                        else
                        {
                            if (IPCERRORCODE == "91780")
                            {
                                lblError.Text = IPCERRORDESC;
                                hdId.Value = string.Empty;
                            }
                            else
                            {
                                throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC
                                    .ERRORCODE.IPC);
                            }

                            return;
                        }
                    }
                }
            }
            else
            {
                lblError.Text = Resources.labels.chooserecordtodelete;
                lblError.Visible = true;
            }

            hdId.Value = string.Empty;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
}
