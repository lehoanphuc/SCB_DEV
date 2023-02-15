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

using System.Data.OleDb;
using SmartPortal.ExceptionCollection;
using System.Text;
using System.Threading;

using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using System.Collections.Generic;

public partial class Widgets_IBDirectDebit_Widget : WidgetBase
{
    string errorCode = "";
    string errorDesc = "";
    private Object m_lock = new Object();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = "";
            if (!IsPostBack)
            {
                
                pnConfirm.Visible = false;
                pnOTP.Visible = false;
                pnPT.Visible = true;
                pnResult.Visible = false;

                #region load loai xac thuc
                SmartPortal.IB.Transactions objTran = new SmartPortal.IB.Transactions();
                DataTable dt = new DataTable();
                dt = objTran.LoadAuthenType(Session["userID"].ToString());
                ddlAuthenType.DataSource = dt;
                ddlAuthenType.DataTextField = "TYPENAME";
                ddlAuthenType.DataValueField = "AUTHENTYPE";
                ddlAuthenType.DataBind();
                #endregion

                #region load cms infor
                DataSet dsCorp = new SmartPortal.IB.Payment().GetCorpInfobyUserid(Session["userID"].ToString(), ref errorCode, ref errorDesc);
                if(errorCode.Equals("0"))
                {
                    if(dsCorp.Tables[0].Rows.Count >0)
                    {
                        ddlCorp.Items.Add(new ListItem(dsCorp.Tables[0].Rows[0]["CorpName"].ToString(), dsCorp.Tables[0].Rows[0]["CorpID"].ToString()));
                        txtCorpID.Text = ddlCorp.SelectedValue;

                        DataTable dtService = new DataTable();
                        dtService = new SmartPortal.IB.Payment().GetServicebyCorpID(ddlCorp.SelectedValue, ref errorCode, ref errorDesc);

                        if (dtService.Rows.Count == 0)
                        {
                            lblError.Text = Resources.labels.corporatenotsupportdd;
                            btnLNext.Enabled = false;
                        }
                        else
                        {
                            ddlService.DataSource = dtService;
                            ddlService.DataTextField = "SERNAME";
                            ddlService.DataValueField = "SERID";
                            ddlService.DataBind();
                            ddlService_Changed(null, null);
                        }

                        btnLNext.Enabled = true;
                    }
                    else
                    {
                        lblError.Text = Resources.labels.corporatenotsupportdd;
                        btnLNext.Enabled = false;
                    }
                }
                else
                {
                    throw new Exception(errorDesc);
                }

                #endregion
            }
            else
            {
                Type ty = sender.GetType();
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void ddlService_Changed(object s, EventArgs e)
    {
        try
        {
            txtServiceID.Text = ddlService.SelectedValue.Trim();
        }
        catch(Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private DataTable ReadFileExcel(string filePath,string query)
    {
        // Create connection string variable. Modify the "Data Source"
        // parameter as appropriate for your environment.
        //String sConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" +
        //    "Data Source=" + filePath + ";" +
        //    "Extended Properties=Excel 8.0;";
        String sConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + @";Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1""";
        
        // Create connection object by using the preceding connection string.
        OleDbConnection objConn = new OleDbConnection(sConnectionString);

        // Open connection with the database.
        objConn.Open();

        // The code to follow uses a SQL SELECT command to display the data from the worksheet.

        // Create new OleDbCommand to return data from worksheet.
        OleDbCommand objCmdSelect = new OleDbCommand(query, objConn);

        // Create new OleDbDataAdapter that is used to build a DataSet
        // based on the preceding SQL SELECT statement.
        OleDbDataAdapter objAdapter1 = new OleDbDataAdapter();

        // Pass the Select command to the adapter.
        objAdapter1.SelectCommand = objCmdSelect;

        // Create new DataSet to hold information from the worksheet.
        DataSet objDataset1 = new DataSet();

        // Fill the DataSet with the information from the worksheet.
        objAdapter1.Fill(objDataset1, "XLData");            

        // Clean up objects.
        objConn.Close();

        // Bind data to DataGrid control.
        return objDataset1.Tables[0];    
    }


    protected void btnLNext_Click(object sender, EventArgs e)
    {        
        try
        {
            hdfFileUri.Value = "";
            //upload file to host
            if (fuTransfer.HasFile)
            {
                string fileName = Session["userName"].ToString()+"_"+System.IO.Path.GetRandomFileName().Replace(".", "") + "_" + fuTransfer.FileName;
                string extension = System.IO.Path.GetExtension(fileName).ToLower();
                string filePath = Server.MapPath("~/widgets/IBDirectDebit/xlsFile/") + fileName;
                bool flag = true;

                switch (extension)
                {
                    case ".xls":
                        break;
                    //case ".xlsx":
                    //    break;

                    default:
                        throw new BusinessExeption("1", new Exception("Only Upload .xls .xlsx"));
                        break;
                }
                fuTransfer.SaveAs(filePath);

                //read file excel
                DataTable tab = ReadFileExcel(filePath, "SELECT * FROM [Sheet1$]");

                #region remove empty rows
                for (int i = tab.Rows.Count - 1; i >= 0; i += -1)
                {
                    DataRow row = tab.Rows[i];
                    if (row[0] == null)
                    {
                        tab.Rows.Remove(row);
                    }
                    else if (string.IsNullOrEmpty(row[0].ToString()))
                    {
                        tab.Rows.Remove(row);
                    }
                    //else if(row[0].ToString().Contains("'"))
                    //{
                    //    row[0] = row[0].ToString().Replace("'", "");
                    //}
                }
                #endregion

                #region validate
                //List<string> lsServices = new List<string>();
                //DataTable dtService = new SmartPortal.IB.Payment().GetServicebyCorpID(ddlCorp.SelectedValue.Trim(), ref errorCode, ref errorDesc);
                //if(errorCode.Equals("0"))
                //{
                //    foreach(DataRow drService in dtService.Rows)
                //    {
                //        lsServices.Add(drService["SERID"].ToString().Trim());
                //    }
                //}
                //else
                //{
                //    throw new Exception(errorDesc);
                //}

                //string corpID = ddlCorp.SelectedValue.Trim();
                //DataColumn dcErrorDesc = new DataColumn("Error Description");
                //dcErrorDesc.DefaultValue = "";
                //tab.Columns.Add(dcErrorDesc);
                
                //foreach(DataRow dr in tab.Rows)
                //{
                //    if (!dr[0].ToString().Trim().Equals(corpID))
                //    {
                //        dr["Error Description"] = Resources.labels.invalidcorpid;
                //        lblError.Text = Resources.labels.invalidcorpid + " " + dr[0].ToString().Trim();
                //        flag = false;
                //    }
                //    else if (!lsServices.Contains(dr[1].ToString().Trim()))
                //    {
                //        dr["Error Description"] = Resources.labels.invalidserviceid;
                //        lblError.Text = Resources.labels.invalidserviceid + " " + dr[1].ToString().Trim();
                //        flag = false;
                //    }
                //}
                #endregion
                gvConfirm.DataSource = tab;
                gvConfirm.DataBind();

                //foreach (GridViewRow gr in gvConfirm.Rows)
                //{
                //    string errDesc = gr.Cells[gr.Cells.Count - 1].Text.Trim().Replace("&nbsp;", "");
                //    if (!string.IsNullOrEmpty(errDesc))
                //    {
                //        gr.BackColor = System.Drawing.Color.Yellow;
                //    }
                //} 

                lblCorpIDCF.Text = ddlCorp.SelectedItem.Text.Trim();
                lblCorpNameCF.Text = ddlCorp.SelectedValue.Trim();
                lblServiceCF.Text = ddlService.Text;
                lblServiceIDCF.Text = ddlService.SelectedItem.Text.Trim();
                
                pnConfirm.Visible = true;
                pnOTP.Visible = false;
                pnPT.Visible = false;
                pnResult.Visible = false;
                btnConfirm.Enabled = flag;

                if(flag)
                {
                    DataTable dtcl = tab;
                    //dtcl.Columns.Remove(dcErrorDesc);
                    gvResult.DataSource = dtcl;
                    gvResult.DataBind();
                }

                hdfFileUri.Value = filePath;
            }
            else
            {
                lblError.Text = Resources.labels.pleasechooseddfile;
                goto FN;
            }
        }
        catch (BusinessExeption bex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["imageuploadec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, bex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["imageuploadec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
            //SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    FN:
        return;
    }
    
    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(hdfFileUri.Value))
            { 
                if (int.Parse(Session["userLevel"].ToString().Trim()) > 2 && Session["TypeID"].ToString().Equals(""))
                {
                    //chuyen khoan luon doi voi user doanh nghiep level3 tro len
                    btnOTP_Click(sender, e);
                }
                else
                {
                    pnConfirm.Visible = false;
                    pnOTP.Visible = true;
                    pnPT.Visible = false;
                    pnResult.Visible = false;
                }
            }
        }
        catch
        {
        }
    }
    protected void btnOTP_Click(object sender, EventArgs e)
    {
        lock (m_lock)
        {
            try
            {
                string OTPcode = txtAuthenCode.Text.Trim();
                txtAuthenCode.Text = "";
                btnOTP.Enabled = false;

                Byte[] bytes = File.ReadAllBytes(hdfFileUri.Value.Trim());
                String FileBase64 = Convert.ToBase64String(bytes);

                string fileName = Path.GetFileName(hdfFileUri.Value.Trim());
                string Desc = "Direct debit by user " + Session["userID"].ToString();

                Hashtable hsRs = new SmartPortal.IB.Payment().UploadDirectDebit(Session["userID"].ToString(), fileName, FileBase64, Desc, ddlAuthenType.SelectedValue, OTPcode,ddlCorp.SelectedValue.Trim(),ddlService.SelectedValue.Trim(), ref this.errorCode, ref errorDesc);
                //new SmartPortal.IB.Transactions().BatchTransfer(BatchTable, "", Session["userID"].ToString(), "", SmartPortal.Common.Utilities.Utility.FormatMoneyInput("", ""), ddlAuthenType.SelectedValue, OTPcode, ref this.errorCode, ref errorDesc);
                if (this.errorCode == "0")
                {
                    lblError.Text = Resources.labels.transactionsuccessful;
                }
                else
                {
                    switch (this.errorCode.ToString().Trim())
                    {
                        case SmartPortal.Constant.IPC.ERRORCODE.IPC:
                            throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.IPC);
                        case SmartPortal.Constant.IPC.ERRORCODE.OVERLIMIT:
                            lblError.Text = Resources.labels.amountovertransactionlimit;
                            return;
                        case SmartPortal.Constant.IPC.ERRORCODE.OTPINVALID:
                            lblError.Text = errorDesc;
                            return;
                        case SmartPortal.Constant.IPC.ERRORCODE.WATTINGBANKAPPROVE:
                            lblError.Text = Resources.labels.wattingbankapprove;
                            break;
                        case SmartPortal.Constant.IPC.ERRORCODE.WATTINGUSERAPPROVE:
                            lblError.Text = Resources.labels.chochutaikhoanduyet;
                            return;
                        case SmartPortal.Constant.IPC.ERRORCODE.STVQHMDG:
                            lblError.Text = Resources.labels.sotienvuothanmucgiaodich;
                            return;
                        case SmartPortal.Constant.IPC.ERRORCODE.STVQHMGDTN:
                            lblError.Text = Resources.labels.sotienvuotquatonghanmuctrongngay;
                            return;
                        case SmartPortal.Constant.IPC.ERRORCODE.SLGDVQSLGDTN:
                            lblError.Text = Resources.labels.solangiaodichvuotquasolangiaodichtrongngay;
                            return;
                        case "9909":
                            lblError.Text = Resources.labels.sotienvuotquatonghanmuctrongngay;
                            return;
                        case "9907":
                            lblError.Text = Resources.labels.solangiaodichvuotquasolangiaodichtrongngay;
                            return;
                        case "9906":
                            lblError.Text = Resources.labels.sotienquahanmuccuachinhanhghico;
                            return;
                        case "9905":
                            lblError.Text = Resources.labels.tongsotienvuotquahanmuccuachinhanhghico;
                            return;
                        case "9904":
                            lblError.Text = Resources.labels.sogiaodichtrongngayquahanmuccuachinhanhghico;
                            return;
                        case "-13524":
                            lblError.Text = Resources.labels.destacccountinvalid;
                            return;
                        default:
                            lblError.Text = string.IsNullOrEmpty(errorDesc.Trim()) ? Resources.labels.transactionerror : errorDesc;
                            return;
                    }
                }

                //clone
                lblTranIDFN.Text = hsRs[SmartPortal.Constant.IPC.IPCTRANSID].ToString();
                lblDateTimeFN.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                lblServerTimeFN.Text = hsRs["PCFID"].ToString();
                lblCorpNameFN.Text = lblCorpNameCF.Text;
                lblCorpIDFN.Text = lblCorpIDCF.Text;

                pnConfirm.Visible = false;
                pnOTP.Visible = false;
                pnPT.Visible = false;
                pnResult.Visible = true;

                /*
                //xuat len man hinh
                gvResult.DataBind();

                //vutt add print AYA request
                Hashtable hasPrint = new Hashtable();
                hasPrint.Add("status", Resources.labels.thanhcong);
                hasPrint.Add("senderAccount", "");
                hasPrint.Add("senderBalance", "");
                hasPrint.Add("ccyid", "");
                hasPrint.Add("senderName", "");
                //hasPrint.Add("recieverAccount", lblReceiverAccount.Text);
                //hasPrint.Add("recieverName", lblReceiverName.Text);
                //hasPrint.Add("transferType", lblHinhThuc.Text);
                hasPrint.Add("totalamount", "");
                hasPrint.Add("amountchu", SmartPortal.Common.Utilities.Utility.IntegerToWords((long)(double)Session["TotalBalance"]) + " " + "");
                hasPrint.Add("feeType", "Sender");
                hasPrint.Add("feeAmount", "");
                hasPrint.Add("desc", "".Trim());
                hasPrint.Add("tranID", "");
                hasPrint.Add("tranDate", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                hasPrint.Add("senderBranch", SmartPortal.Common.Utilities.Utility.FormatStringCore(""));
                //hasPrint.Add("receiverBranch", SmartPortal.Common.Utilities.Utility.FormatStringCore(lblReceiverBranch.Text));
                Session["print"] = hasPrint;
                */
                btnPrint.Visible = false;
                btnView.Visible = false;

                
            }
            catch (IPCException IPCex)
            {
                SmartPortal.Common.Log.RaiseError(IPCex.Message, this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.Message, Request.Url.Query);
                SmartPortal.Common.Log.GoToIBErrorPage(IPCex.Message, Request.Url.Query);
            }
            catch (Exception ex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
                SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
            }
            btnOTP.Enabled = true;
        }
    }
    protected void btnBack2_Click(object sender, EventArgs e)
    {
        try
        {
            pnConfirm.Visible = false;
            pnOTP.Visible = false;
            pnPT.Visible = true;
            pnResult.Visible = false;
        }
        catch
        {
        }
    }
    protected void btnBackA_Click(object sender, EventArgs e)
    {
        try
        {
            pnConfirm.Visible = true;
            pnOTP.Visible = false;
            pnPT.Visible = false;
            pnResult.Visible = false;
        }
        catch
        {
        }
    }
    
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?po=3&p=86"));
    }

    protected void DropDownListOTP_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAuthenType.SelectedValue.ToString() == "ESMSOTP")
        {
            btnSendOTP.Visible = true;
            if (btnSendOTP.Text == "ReSend")
            {
                btnOTP.Enabled = true;
            }
            else
            {
                btnOTP.Enabled = false;
            }
        }
        else
        {
            btnOTP.Enabled = true;
            btnSendOTP.Visible = false;
        }
    }

    protected void btnSendOTP_Click(object sender, EventArgs e)
    {
        string errorcode = "";
        string errorDesc = "";
        try
        {

            btnSendOTP.Text = "ReSend";
            SmartPortal.SEMS.OTP.SendSMSOTP(Session["userID"].ToString(), ref errorcode, ref errorDesc);
            switch (errorcode)
            {
                case "0": lblError.Text = "Send SMS OTP success."; btnOTP.Enabled = true;
                    break;
                case "7003": lblError.Text = "User does not register SMS OTP"; btnOTP.Enabled = false;
                    break;
                default: lblError.Text = errorDesc; btnOTP.Enabled = false;
                    break;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
}
