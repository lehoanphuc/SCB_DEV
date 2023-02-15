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

using SmartPortal.ExceptionCollection;

public partial class rptContractList_Widget : ReportBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region hien thị status
            rptstatus.Items.Add(new ListItem(Resources.labels.connew, SmartPortal.Constant.IPC.NEW));
            rptstatus.Items.Add(new ListItem(Resources.labels.conactive, SmartPortal.Constant.IPC.ACTIVE));
            rptstatus.Items.Add(new ListItem(Resources.labels.condelete, SmartPortal.Constant.IPC.DELETE));
            rptstatus.Items.Add(new ListItem(Resources.labels.conblock, SmartPortal.Constant.IPC.BLOCK));
            rptstatus.Items.Add(new ListItem(Resources.labels.conpending, SmartPortal.Constant.IPC.PENDING));
            rptstatus.Items.Add(new ListItem(Resources.labels.conreject, SmartPortal.Constant.IPC.REJECT));
            rptstatus.Items.Insert(0, new ListItem(Resources.labels.all, SmartPortal.Constant.IPC.ALL));
            rptimper.Value = Session["Username"].ToString();

            DataSet dsBranch = new DataSet();
            dsBranch = new SmartPortal.SEMS.Branch().SearchBranchByCondition("","","","",ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                DataTable dtBranch = new DataTable();
                dtBranch = dsBranch.Tables[0];

                rptbranchid.DataSource = dtBranch;
                rptbranchid.DataTextField = "branchname";
                rptbranchid.DataValueField = "branchid";
                rptbranchid.DataBind();

                rptbranchid.Items.Insert(0, new ListItem(Resources.labels.all, ""));
            }
            else
            {
                throw new IPCException(IPCERRORDESC);
            }
            #endregion
           this.ClearSession();
           //rptpageid.Text = "6";
           //this.SetParam();
        }
    }
    public override void ClearSession()
    {
        base.ClearSession();
    }
    public override void SetParam()
    {
        base.SetParam();
    }
    protected void btnViewReport_Click(object sender, EventArgs e)
    {
       this.SetParam();
    }
    protected void rptbranchid_SelectedIndexChanged(object sender, EventArgs e)
    {
        rptbranchname.Value = rptbranchid.SelectedItem.Text;
    }
}
