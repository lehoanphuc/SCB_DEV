using System;
using System.Data;
using System.Web.UI.WebControls;
using SmartPortal.Common.Utilities;
using SmartPortal.ExceptionCollection;
using SmartPortal.Constant;
using SmartPortal.Model;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using SmartPortal.BLL;
using SmartPortal.SEMS;

public partial class Widgets_SEMSCorpMatrixUserGroup_Controls_Widget : WidgetBase
{
    string CONTRACTNO = string.Empty;
    string IPCERRORCODE = string.Empty;
    string IPCERRORDESC = string.Empty;
    private const string PAGELIST = "1089";

    private DataTable TableToInsertDBForGroup
    {
        get { return ViewState["TableToInsertDBForGroup"] as DataTable; }
        set
        {
            ViewState["TableToInsertDBForGroup"] = value;
        }
    }

    private DataTable USERTABLE
    {
        get { return ViewState["USERTABLE"] as DataTable; }
        set
        {
            ViewState["USERTABLE"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        lblError.Text = string.Empty;
        CONTRACTNO = GetParamsPage(IPC.ID)[0].Trim();
        if (!IsPostBack)
        {
            BindData();

            btnAdd.Visible = CheckPermitPageActionByPage(IPC.ACTIONPAGE.ADD,PAGELIST);
            btnNext.Visible = CheckPermitPageActionByPage(IPC.ACTIONPAGE.EDIT, PAGELIST);

            #region Lấy thông tin user của hỡp đồng
            DataTable userTable = new DataTable();
            userTable = (new SmartPortal.SEMS.Contract().GetUserByContractNo(CONTRACTNO, "", "4", ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
            try
            {
                userTable = userTable.Select("Status = 'A' or Status ='N' or Status = 'P'").CopyToDataTable();
            }
            catch (Exception exception)
            {
                userTable = null;
            }
            
            USERTABLE = userTable;
            gvUser.DataSource = userTable;
            gvUser.DataBind();
            hdfTotalRow.Value = gvUser.Rows.Count.ToString();
            #endregion

            DataSet ds = new SmartPortal.SEMS.ApprovalStructure().GetContractGroupDetail(CONTRACTNO, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE.Equals("0"))
            {
                DataTable dtRole = ds.Tables[0];
                ddlGroup.DataSource = dtRole;
                ddlGroup.DataValueField = "GroupID";
                ddlGroup.DataTextField = "GroupName";
                ddlGroup.DataBind();
            }
            else
            {
                lblError.Text = IPCERRORCODE;
            }
        }
    }

    private void BindData()
    {
        User user = new User();
        DataSet ds = user.GetUserGroupByContractNo(CONTRACTNO, string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE.Equals("0"))
        {
            DataTable dt = ds.Tables[0];
            gvUserGroup.DataSource = dt;
            gvUserGroup.DataBind();
            TableToInsertDBForGroup = dt;
        }
        else
        {
            lblError.Text = IPCERRORDESC;
        }
    }

    protected void btnNext_onclick(object sender, EventArgs e)
    {
        DataTable dt = TableToInsertDBForGroup;
        #region Tạo bảng chứa group của user 
        DataTable tblUserGroup = new DataTable();
        DataColumn colUGUserID = new DataColumn("UserID");
        DataColumn colUGGroupID = new DataColumn("GroupID");

        //add vào table
        tblUserGroup.Columns.Add(colUGUserID);
        tblUserGroup.Columns.Add(colUGGroupID);
        foreach (DataRow item in dt.Rows)
        {
            DataRow row = tblUserGroup.NewRow();
            row["UserID"] = item["UserID"];
            row["GroupID"] = item["GroupID"];
            tblUserGroup.Rows.Add(row);
        }
        #endregion


        #region Delete all
        DataTable tblDelUserGroup = new DataTable();
        DataColumn coldelUGUserID = new DataColumn("UserID");

        DataTable userTable = new DataTable();
        userTable = (new SmartPortal.SEMS.Contract().GetUserByContractNo(CONTRACTNO, "", "4", ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
        //add vào table
        tblDelUserGroup.Columns.Add(coldelUGUserID);
        foreach (DataRow item in userTable.Rows)
        {
            DataRow row = tblDelUserGroup.NewRow();
            row["UserID"] = item["UserID"];
            tblDelUserGroup.Rows.Add(row);
        }
        #endregion

        new SmartPortal.SEMS.Customer().InsertUserGroup(tblUserGroup, tblDelUserGroup, ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE != "0")
        {
            lblError.Text = IPCERRORDESC;
        }
        else
        {
            lblError.Text = Resources.labels.assignusertogroupsuccessfully;
            btnNext.Enabled = false;
            pnInfo.Enabled = false;
            ddlGroup.Enabled = false;
            btnAdd.Enabled = false;
            gvUserGroup.Columns[4].Visible = false;
        }
    }


    protected void btnBack_onclick(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            gvUser.PageIndex = 0;
            DataTable dt = USERTABLE;
            dt = USERTABLE.AsEnumerable().Where(r => r[IPC.USERID].ToString().ToUpper().Contains(txtSearch.Text.ToUpper().TrimStart().TrimEnd())
                || r[IPC.FULLNAME].ToString().ToUpper().Contains(txtSearch.Text.ToUpper().TrimStart().TrimEnd())).ToList().CopyToDataTable();

            gvUser.DataSource = dt;
            gvUser.DataBind();
        }
        catch
        {
            gvUser.DataSource = new DataTable();
            gvUser.DataBind();
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            CheckBox cbx;
            Label lblUserID;
            Label lblFullName;

            DataTable dt = TableToInsertDBForGroup;
            DataTable dtTemp = new DataTable();
            dtTemp.Columns.Add(IPC.USERID, typeof(string));
            dtTemp.Columns.Add(IPC.FULLNAME, typeof(string));
            foreach (GridViewRow gvr in gvUser.Rows)
            {
                cbx = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                if (cbx.Checked == true)
                {
                    DataRow dr = dtTemp.NewRow();
                    lblUserID = (Label)gvr.Cells[1].FindControl("lblUserID");
                    lblFullName = (Label)gvr.Cells[2].FindControl("lblFullName");
                    dr[IPC.USERID] = lblUserID.Text;
                    dr[IPC.FULLNAME] = lblFullName.Text;
                    dtTemp.Rows.Add(dr);
                }
            }
            if (dtTemp.Rows.Count > 0)
            {
                foreach (DataRow dr in dtTemp.Rows)
                {
                    if (dt.AsEnumerable().Where(r => r[IPC.USERID].ToString().Equals(dr[IPC.USERID].ToString())).FirstOrDefault() == null)
                    {
                        DataRow row = dt.NewRow();
                        row[IPC.USERID] = dr[IPC.USERID].ToString();
                        row[IPC.GROUPID] = ddlGroup.SelectedValue;
                        row["GroupName"] = ddlGroup.SelectedItem.Text;
                        row[IPC.FULLNAME] = dr[IPC.FULLNAME].ToString();
                        dt.Rows.Add(row);
                        gvUser.DataSource = USERTABLE;
                        gvUser.DataBind();
                        lblError.Text = lblError.Text.Equals(Resources.labels.userexistedingroup) ? Resources.labels.userexistedingroup : string.Empty;
                    }
                    else
                    {
                        lblError.Text = Resources.labels.userexistedingroup;
                    }
                }
            }
            else
            {
                lblError.Text = Resources.labels.pleaseselectuser1;
            }

            TableToInsertDBForGroup = dt;
            gvUserGroup.DataSource = TableToInsertDBForGroup;
            gvUserGroup.DataBind();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }


    protected void colGroupDelete_onclick(object sender, EventArgs e)
    {
        try
        {
            if (CheckPermitPageActionByPage(IPC.ACTIONPAGE.DELETE, PAGELIST))
            {
                string[] IDs = ((sender as LinkButton).CommandArgument).ToString().Split('|');
                DataTable dt = TableToInsertDBForGroup;
                DataRow[] rows = dt.Select("UserID = '" + IDs[0] + "' AND " + " GroupID = '" + IDs[1] + "'");
                if (rows.Length > 0)
                {
                    dt.Rows.Remove(rows[0]);
                }

                TableToInsertDBForGroup = dt;
                gvUserGroup.DataSource = dt;
                gvUserGroup.DataBind();

                gvUser.DataSource = USERTABLE;
                gvUser.DataBind();
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void gvUser_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        CheckBox cbxSelect; 
        LinkButton colGroupDelete;
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = (DataRowView)e.Row.DataItem;

                cbxSelect = (CheckBox)e.Row.FindControl("cbxSelect");
                colGroupDelete = (LinkButton)e.Row.FindControl("colGroupDelete");

                DataTable groupTable = TableToInsertDBForGroup;

                if (groupTable.AsEnumerable().Where(r => r[IPC.USERID].ToString().Equals(drv[IPC.USERID].ToString())).FirstOrDefault() != null)
                {
                    cbxSelect.Enabled = false;
                }

                if (!CheckPermitPageActionByPage(IPC.ACTIONPAGE.DELETE, PAGELIST))
                {
                    colGroupDelete.Enabled = false;
                    colGroupDelete.OnClientClick = string.Empty;
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    public bool CheckPermitPageActionByPage(string action, string pageid)
    {
        return PagesBLL.ChekcPermitPageAction(pageid, Session["userName"].ToString(), action);
    }

    protected void gvUser_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvUser.PageIndex = e.NewPageIndex;
            DataTable dt = USERTABLE;
            dt = USERTABLE.AsEnumerable().Where(r => r[IPC.USERID].ToString().ToUpper().Contains(txtSearch.Text.ToUpper().TrimStart().TrimEnd()) || r[IPC.FULLNAME].ToString().ToUpper().Contains(txtSearch.Text.ToUpper().TrimStart().TrimEnd())).ToList().CopyToDataTable();

            gvUser.DataSource = dt;
            gvUser.DataBind();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void gvUserGroup_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvUserGroup.PageIndex = e.NewPageIndex;
            DataTable dt = TableToInsertDBForGroup;
            gvUserGroup.DataSource = dt;
            gvUserGroup.DataBind();

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
}
