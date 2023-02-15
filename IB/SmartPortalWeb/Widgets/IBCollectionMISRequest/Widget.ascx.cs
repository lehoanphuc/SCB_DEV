using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Globalization;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using SmartPortal.ExceptionCollection;
using SmartPortal.Common.Utilities;
using Newtonsoft.Json.Linq;

public partial class Widgets_CollectionMISRequest_Widget : WidgetBase
{
    string IPCERRORCODE;
    string IPCERRORDESC;
    DataSet dsReceiverList = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblTextError.Text = "";

            //hide panel
            if (!IsPostBack)
            {
                DataSet dsCorpInfor = new DataSet();
                dsCorpInfor = new SmartPortal.IB.Payment().GetCorpInfobyUserid(Session["userid"].ToString(),ref IPCERRORCODE, ref IPCERRORDESC);

                txtCorpName.Text = dsCorpInfor.Tables[0].Rows[0]["CorpName"].ToString();
                hdfCorpID.Value = dsCorpInfor.Tables[0].Rows[0]["CorpID"].ToString();
                txtEmail.Text = dsCorpInfor.Tables[0].Rows[0]["Email"].ToString();

                //load provider...
                GetServiceList();
            }
        }
        catch(Exception ex )
        {
        }
    }
    protected void GetServiceList()
    {
        DataTable dtService = new DataTable();
        dtService = new SmartPortal.IB.Payment().GetServicebyCorpID(hdfCorpID.Value, ref IPCERRORCODE, ref IPCERRORDESC);

        if (dtService.Rows.Count == 0)
        {
            ddlservice.Items.Clear();
            ddlservice.Items.Insert(0, new ListItem(Resources.labels.khongconhacungcapnao, ""));
        }
        else
        {
            ddlservice.DataSource = dtService;
            ddlservice.DataTextField = "SERNAME";
            ddlservice.DataValueField = "SERID";
            ddlservice.DataBind();
        }
    }

    protected void btnRequest_Click(object sender, EventArgs e)
    {
        try
        {
            object obj = new SmartPortal.IB.Payment().GetMis(hdfCorpID.Value.ToString(), ddlservice.SelectedValue.ToString(), txtEmail.Text.Trim(), txtFromDateLN.Text.Trim(), txtToDateLN.Text.Trim(), ref IPCERRORCODE, ref IPCERRORDESC);
            SmartPortal.Common.Log.WriteLogFile("Mis request", "", "", "errorcode:" + IPCERRORCODE + "errordesc" + IPCERRORDESC);
            if (IPCERRORCODE.Equals("0"))
            {
                if (obj != null)
                {
                    JObject jobj = JObject.Parse(obj.ToString());
                    string errcodefromMIS = jobj.SelectToken("ERRORCODE").Value<string>();
                    string errdescfromMIS = jobj.SelectToken("ERRORDESC").Value<string>();
                    if (!errcodefromMIS.Equals("0"))
                    {
                        //lblTextError.Text = errdescfromMIS;
                        SmartPortal.Common.Log.WriteLogFile("", "", "", errdescfromMIS);
                        lblTextError.Text = "Corporate no have data in this time.";
                    }
                    else
                    {
                        lblTextError.Text = Resources.labels.laymisthanhcong;
                    }
                }
            }
            else
            {
                lblTextError.Text = IPCERRORDESC;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.WriteLogFile("", "", "", ex.ToString());
            lblTextError.Text = "Request Failed! Please contact with Admin Bank";
        }
    }

}
