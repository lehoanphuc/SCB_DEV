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


public partial class Widgets_SEMSContractFee_Detail_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";

    public string _TITLE
    {
        get { return lblTitleProduct.Text; }
        set { lblTitleProduct.Text = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                lblTitleProduct.Text = Resources.labels.chitietphichohopdong;
                Session["printContractFee"] = null;
                LoadDll();
                BindData();
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
    void LoadDll()
    {
        ddlPayer.Items.Add(new ListItem(Resources.labels.nguoigui, "0"));
        ddlPayer.Items.Add(new ListItem(Resources.labels.nguoinhan, "1"));

        ddlTrans.DataSource = new SmartPortal.IB.Schedule().LoadTransferType(Utility.KillSqlInjection(SmartPortal.Constant.IPC.ISPRODUCTFEE), Utility.KillSqlInjection(SmartPortal.Constant.IPC.YES), ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE != "0")
        {
            throw new IPCException(IPCERRORDESC);
        }
        ddlTrans.DataTextField = "PAGENAME";
        ddlTrans.DataValueField = "TRANCODE";
        ddlTrans.DataBind();

        //load tien te vutran15122014
        ddlCCYID.DataSource = new SmartPortal.SEMS.Product().LoaddAllCCYID(ref IPCERRORCODE, ref IPCERRORDESC);
        ddlCCYID.DataTextField = "CCYID";
        ddlCCYID.DataValueField = "CCYID";
        ddlCCYID.DataBind();
    }
    void BindData()
    {
        try
        {
            string ID = GetParamsPage(IPC.ID)[0].Trim();
            string[] key = ID.Split('|');
            hdStatus.Value = key[4];
            DataSet ds = new SmartPortal.SEMS.Fee().SearchContractFee(key[0], key[1], key[2], key[3], "", key[4],"",
                "", 0, 0, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                DataTable productLMTable = ds.Tables[0];
                if (productLMTable.Rows.Count > 0)
                {
                    DataSet dtProLim = new DataSet();
                    dtProLim = new SmartPortal.SEMS.Contract().GetContractByCondition(productLMTable.Rows[0]["CONTRACTNO"].ToString(), "", "", "", "", "ALL", "ALL", "", "", string.Empty,"", 10, 0, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE != "0")
                    {
                        lblError.Text = IPCERRORDESC;
                    }

                    if (dtProLim != null)
                    {
                        if (dtProLim.Tables.Count != 0)
                        {
                            if (dtProLim.Tables[0].Rows.Count == 1)
                            {
                                lblCustName.Text = dtProLim.Tables[0].Rows[0]["FullName"].ToString();
                            }
                        }
                    }

                    txtContractNo.Text = productLMTable.Rows[0]["CONTRACTNO"].ToString();
                    ddlTrans.SelectedValue = productLMTable.Rows[0]["TRANCODE"].ToString();
                    txtDesc.Text = productLMTable.Rows[0]["DESCRIPTION"].ToString();
                    ddlCCYID.SelectedValue = productLMTable.Rows[0]["CCYID"].ToString();
                    DataSet dsFee = new SmartPortal.SEMS.Fee().SearchFee(string.Empty, string.Empty, string.Empty, Utility.KillSqlInjection(ddlCCYID.SelectedValue), 0, 0, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        ddlFee.DataSource = dsFee;
                        ddlFee.DataTextField = "FEENAME";
                        ddlFee.DataValueField = "FEEID";
                        ddlFee.DataBind();
                    }
                    else
                    {
                        throw new IPCException(IPCERRORDESC);
                    }
                    ddlFee.SelectedValue = productLMTable.Rows[0]["FEEID"].ToString();
                    ddlPayer.SelectedValue = productLMTable.Rows[0]["PAYER"].ToString();

                    //ghi vo session dung in
                    Hashtable hasPrint = new Hashtable();
                    hasPrint.Add("contractno", txtContractNo.Text);
                    hasPrint.Add("trantype", ddlTrans.SelectedItem.ToString());
                    hasPrint.Add("fee", ddlFee.SelectedItem.ToString());
                    hasPrint.Add("desc", txtDesc.Text);
                    hasPrint.Add("ccyid", ddlCCYID.SelectedItem.ToString());
                    hasPrint.Add("payer", ddlPayer.SelectedItem.ToString());
                    hasPrint.Add("datecreated", ((DateTime)(productLMTable.Rows[0]["datecreated"])).ToString("dd/MM/yyyy"));
                    if (productLMTable.Rows[0]["dateapproved"].ToString().Trim() != "")
                    {
                        hasPrint.Add("dateapproved", ((DateTime)(productLMTable.Rows[0]["dateapproved"])).ToString("dd/MM/yyyy"));
                    }
                    else
                    {
                        hasPrint.Add("dateapproved", "");
                    }
                    hasPrint.Add("usercreated", productLMTable.Rows[0]["usercreated"].ToString());
                    hasPrint.Add("userapproved", productLMTable.Rows[0]["userapproved"].ToString());
                    Session["printContractFee"] = hasPrint;
                }
            }
            else
            {
                lblError.Text = IPCERRORDESC;
            }
            pnAdd.Enabled = false;
            btback.Visible = true;
            btnPrint.Visible = true;
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
        RedirectBackToMainPage();
    }
}
