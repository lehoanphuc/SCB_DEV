using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SmartPortal.ExceptionCollection;
using System.Text;
using System.Configuration;
using SmartPortal.Common;
using SmartPortal.Common.Utilities;

public partial class Widgets_SEMSContractReview_RegisterContract : System.Web.UI.Page
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["userName"] == null || Session["userName"].ToString() == "guest")
                Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx"));
            if (!IsPostBack)
            {
                if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"] != null && SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["ct"] != null)
                {
                    BindData();
                }
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
            string contractNo = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString().Trim();
            string UserType = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["ct"].ToString().Trim();
            string ContractType = string.Empty;
            //if (new System.IO.FileInfo(Server.MapPath("~/widgets/semscontractlist/contractfile/"+contractNo+".html")).Exists==false)
            //{
            //lay lai thong tin hop dong
            DataSet dsUserType = new SmartPortal.SEMS.Services().GetUserType(UserType, "", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                DataTable dtUserType = new DataTable();
                dtUserType = dsUserType.Tables[0];

                if (dtUserType.Rows.Count != 0)
                {
                    ContractType = dtUserType.Rows[0]["USERCODE"].ToString();
                }
            }
            else
            {
                throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
            }

            if (ContractType == "01" || ContractType == "02" | ContractType == "03")
            {
                OldContractPersonal(contractNo);

            }
            else
            {
                OldContractCorp(contractNo);
            }

            BindData1();
        }

        catch
        { }
    }

    void BindData1()
    {
        string contractNo = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString().Trim();
        //string path = Server.MapPath("~/widgets/semscontractlist/contractfile/" + contractNo + ".html");
        string str = "<div><img src='../../../Images/logo.png' style='height:70px;' /><br/><br/></div>";
        str += ViewState["tmpl"].ToString();
        str += "<div><br/><p><a href='https://www.scb.co.th/en/personal-banking.html' target='blank'>www.scb.co.th/en/personal-banking.html</ a></p><span style='font-weight:bold;'>" + Resources.labels.AbankthankyouforusingAYAebankingservices + "!	</span></div>";
        lblView.Text = str;

    }

    void OldContractPersonal(string cn)
    {
        Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();
        tmpl = SmartPortal.Common.ST.GetStringTemplate("SEMSContractApprove", "ContractAttachment" + "en-US");

        //lay thong tin hop dong de gui mail

        try
        {

            string hpcontractNo = cn;
            string custID = "";
            tmpl.Reset();

            //lay thong tin hop dong boi contractno
            DataTable contractTable = (new SmartPortal.SEMS.Contract().GetContractByContractNo(hpcontractNo, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
            if (IPCERRORCODE != "0")
            {
                goto ERROR;
            }
            //gan thong tin hop dong vao stringtemplate
            if (contractTable.Rows.Count != 0)
            {
                tmpl.SetAttribute("CONTRACTNO", contractTable.Rows[0]["CONTRACTNO"].ToString());
                DataSet dsUserType = new DataSet();
                dsUserType = new SmartPortal.SEMS.Services().GetUserType(contractTable.Rows[0]["USERTYPE"].ToString().Trim(), "", ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    DataTable dtUserType = new DataTable();
                    dtUserType = dsUserType.Tables[0];

                    if (dtUserType.Rows.Count != 0)
                    {
                        tmpl.SetAttribute("CONTRACTTYPE", dtUserType.Rows[0]["USERTYPE"].ToString());
                    }
                }
                else
                {
                    throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
                }


                tmpl.SetAttribute("OPENDATE", SmartPortal.Common.Utilities.Utility.IsDateTime2(contractTable.Rows[0]["CREATEDATE"].ToString()).ToString("dd/MM/yyyy"));
                tmpl.SetAttribute("ENDDATE", SmartPortal.Common.Utilities.Utility.IsDateTime2(contractTable.Rows[0]["ENDDATE"].ToString()).ToString("dd/MM/yyyy"));


                tmpl.SetAttribute("PRODUCT", contractTable.Rows[0]["PRODUCTNAME"].ToString());
                switch (contractTable.Rows[0]["STATUS"].ToString().Trim())
                {
                    case SmartPortal.Constant.IPC.NEW:
                        tmpl.SetAttribute("STATUS", Resources.labels.connew);
                        break;
                    case SmartPortal.Constant.IPC.DELETE:
                        tmpl.SetAttribute("STATUS", Resources.labels.condelete);

                        break;
                    case SmartPortal.Constant.IPC.ACTIVE:
                        tmpl.SetAttribute("STATUS", Resources.labels.conactive);

                        break;
                    case SmartPortal.Constant.IPC.BLOCK:
                        tmpl.SetAttribute("STATUS", Resources.labels.conblock);

                        break;
                    case SmartPortal.Constant.IPC.PENDING:
                        tmpl.SetAttribute("STATUS", Resources.labels.conpending);

                        break;
                    case SmartPortal.Constant.IPC.PENDINGFORDELETE:
                        tmpl.SetAttribute("STATUS", Resources.labels.pendingfordelete);

                        break;
                    case SmartPortal.Constant.IPC.PENDINGFORACTIVE:
                        tmpl.SetAttribute("STATUS", Resources.labels.pendingforactive);

                        break;
                    case SmartPortal.Constant.IPC.REJECT:
                        tmpl.SetAttribute("STATUS", Resources.labels.conreject);

                        break;
                }


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
                tmpl.SetAttribute("CIF", custTable.Rows[0]["CUSTCODE"].ToString());
            }

            #region lay thong tin tai khoan cua chu tai khoan
            DataTable userTable = (new SmartPortal.SEMS.Contract().GetUserByContractNo(hpcontractNo, string.Empty, "", ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
            if (IPCERRORCODE != "0")
            {
                goto ERROR;
            }

            StringBuilder st = new StringBuilder();
            st.Append("<div style='font-weight: bold; background-color: #C0C0C0;width:100%;height:16px;'>" + Resources.labels.thongtinchutaikhoan.ToUpper() + "</div>");
            //gan thong tin user vao stringtemplate
            int i = 0;
            foreach (DataRow row in userTable.Rows)
            {

                st.Append("<table style='width:100%;'>");


                st.Append("<tr>");
                st.Append("<td width='25%'>");
                st.Append(Resources.labels.tendaydu + " ");
                st.Append("</td>");
                st.Append("<td width='25%'>");
                st.Append(row["FULLNAME"].ToString());
                st.Append("</td>");
                st.Append("<td width='25%'>");
                st.Append("Email ");
                st.Append("</td>");
                st.Append("<td width='25%'>");
                st.Append(row["EMAIL"].ToString());
                st.Append("</td>");
                st.Append("</tr>");

                st.Append("<tr>");
                st.Append("<td>");
                st.Append(Resources.labels.phone + " ");
                st.Append("</td>");
                st.Append("<td>");
                st.Append(row["PHONE"].ToString());
                st.Append("</td>");
                st.Append("<td>");
                st.Append(Resources.labels.status + " ");
                st.Append("</td>");
                st.Append("<td>");
                switch (contractTable.Rows[0]["STATUS"].ToString().Trim())
                {
                    case SmartPortal.Constant.IPC.NEW:
                        st.Append(Resources.labels.connew);
                        break;
                    case SmartPortal.Constant.IPC.DELETE:
                        st.Append(Resources.labels.condelete);

                        break;
                    case SmartPortal.Constant.IPC.ACTIVE:
                        st.Append(Resources.labels.conactive);

                        break;
                    case SmartPortal.Constant.IPC.BLOCK:
                        st.Append(Resources.labels.conblock);

                        break;
                    case SmartPortal.Constant.IPC.PENDING:
                        st.Append(Resources.labels.conpending);

                        break;
                    case SmartPortal.Constant.IPC.PENDINGFORDELETE:
                        tmpl.SetAttribute("STATUS", Resources.labels.pendingfordelete);

                        break;
                    case SmartPortal.Constant.IPC.PENDINGFORACTIVE:
                        tmpl.SetAttribute("STATUS", Resources.labels.pendingforactive);

                        break;
                    case SmartPortal.Constant.IPC.REJECT:
                        st.Append(Resources.labels.conreject);

                        break;
                }
                st.Append("</td>");
                st.Append("</tr>");


                //lay het các tai khoan Ibank cua user theo userID
                DataSet accountIBDataset = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.IB, row["USERID"].ToString().Trim(), string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);

                if (IPCERRORCODE != "0")
                {
                    goto ERROR;
                }

                DataTable accountIBTable = accountIBDataset.Tables[0];
                if (accountIBTable.Rows.Count != 0)
                {
                    if (accountIBTable.Rows[0]["ROLEID"].ToString().Trim() != "")
                    {
                        st.Append("<tr>");
                        st.Append("<td colspan='4'>");
                        st.Append("<br/>");
                        st.Append("</td>");
                        st.Append("</tr>");

                        st.Append("<tr>");
                        st.Append("<td colspan='4'>");
                        st.Append("<B>Internet Banking</B>");
                        st.Append("</td>");
                        st.Append("</tr>");

                        st.Append("<tr>");
                        st.Append("<td width='25%'>");
                        st.Append(Resources.labels.username + " :");
                        st.Append("</td>");
                        st.Append("<td width='25%'>");
                        st.Append(accountIBTable.Rows[0]["USERNAME"].ToString());
                        st.Append("</td>");
                        st.Append("<td width='25%'>");
                        st.Append(Resources.labels.password + " :");
                        st.Append("</td>");
                        st.Append("<td width='25%'>");
                        st.Append("########");
                        //st.Append("<b>" + SmartPortal.Security.Encryption.Decrypt(accountIBTable.Rows[0]["PASSWORD"].ToString()) + "</b>");
                        st.Append("</td>");
                        st.Append("</tr>");
                    }

                }

                //lay het các tai khoan SMS cua user theo userID
                DataSet accountSMSDataset = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.SMS, row["USERID"].ToString().Trim(), string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);

                if (IPCERRORCODE != "0")
                {
                    goto ERROR;
                }

                DataTable accountSMSTable = accountSMSDataset.Tables[0];
                if (accountSMSTable.Rows.Count != 0)
                {
                    if (accountSMSTable.Rows[0]["ROLEID"].ToString().Trim() != "")
                    {
                        st.Append("<tr>");
                        st.Append("<td colspan='4'>");
                        st.Append("<br/>");
                        st.Append("</td>");
                        st.Append("</tr>");

                        st.Append("<tr>");
                        st.Append("<td colspan='4'>");
                        st.Append("<B>SMS Banking</B>");
                        st.Append("</td>");
                        st.Append("</tr>");

                        st.Append("<tr>");
                        st.Append("<td width='25%'>");
                        st.Append(Resources.labels.phone + " :");
                        st.Append("</td>");
                        st.Append("<td width='25%'>");
                        st.Append(accountSMSTable.Rows[0]["UN"].ToString());
                        st.Append("</td>");
                        st.Append("<td width='25%'>");
                        st.Append(Resources.labels.taikhoanmacdinh + " :");
                        st.Append("</td>");
                        st.Append("<td width='25%'>");
                        st.Append(accountSMSTable.Rows[0]["DEFAULTACCTNO"].ToString());
                        st.Append("</td>");
                        st.Append("</tr>");
                    }
                }
                //lay het các tai khoan MB cua user theo userID
                // DataSet accountMBDataset = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.MB, row["USERID"].ToString().Trim(), SmartPortal.Constant.IPC.NORMAL, ref IPCERRORCODE, ref IPCERRORDESC);

                // if (IPCERRORCODE != "0")
                // {
                    // goto ERROR;
                // }

                // DataTable accountMBTable = accountMBDataset.Tables[0];
                // if (accountMBTable.Rows.Count != 0)
                // {
                    // if (accountMBTable.Rows[0]["ROLEID"].ToString().Trim() != "")
                    // {
                        // st.Append("<tr>");
                        // st.Append("<td colspan='4'>");
                        // st.Append("<br/>");
                        // st.Append("</td>");
                        // st.Append("</tr>");

                        // st.Append("<tr>");
                        // st.Append("<td colspan='4'>");
                        // st.Append("<hr/><B>  MOBILE</B>");
                        // st.Append("</td>");
                        // st.Append("</tr>");

                        // st.Append("<tr>");
                        // st.Append("<td colspan=1>  ");
                        // st.Append(Resources.labels.phone + " :");
                        // st.Append("</td>");
                        // st.Append("<td colspan=1>  ");
                        // st.Append("<b>" + accountMBTable.Rows[0]["UN"].ToString() + "</b>");
                        // st.Append("</td>");
                        // st.Append("<td colspan=1>  ");
                        // st.Append(Resources.labels.password + " :");
                        // st.Append("</td>");
                        // st.Append("<td colspan=1>  ");
                        // st.Append("<b>" + "######" + "</b>");
                        // st.Append("</td>");
                        // st.Append("</tr>");

                        // //st.Append("<tr>");
                        // //if (sendPincode())
                        // //{
                        // //    st.Append("<td colspan=1>  ");
                        // //    st.Append(Resources.labels.pincode + " :");
                        // //    st.Append("</td>");
                        // //    st.Append("<td colspan=1>  ");
                        // //    st.Append("<b>" + "######" + "</b>");
                        // //    st.Append("</td>");
                        // //    st.Append("<td colspan=1>  ");
                        // //    st.Append("");
                        // //    st.Append("</td>");
                        // //    st.Append("<td colspan=1>  ");
                        // //    st.Append("</td>");
                        // //}
                        // //st.Append("</tr>");
                    // }
                // }

                //lay het các tai khoan AM cua user theo userID
                DataSet accountAMDataset = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.AM, row["USERID"].ToString().Trim(), string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);

                if (IPCERRORCODE != "0")
                {
                    throw new IPCException(IPCERRORDESC);
                }

                DataTable accountAMTable = accountAMDataset.Tables[0];
                if (accountAMTable.Rows.Count != 0 && !string.IsNullOrEmpty(accountAMTable.Rows[0][0].ToString()))
                {
                    if (accountAMTable.Rows[0]["ROLEID"].ToString().Trim() != "")
                    {
                        //st.Append("<tr>");
                        //st.Append("<td colspan='4'>");
                        //st.Append("<br/>");
                        //st.Append("</td>");
                        //st.Append("</tr>");

                        //st.Append("<tr>");
                        //st.Append("<td colspan='4'>");
                        //st.Append("<B>MOBILE BANKING</B><hr/>");
                        //st.Append("</td>");
                        //st.Append("</tr>");

                        //st.Append("<tr>");
                        //st.Append("<td colspan=1>  ");
                        //st.Append(Resources.labels.phone + " :");
                        //st.Append("</td>");
                        //st.Append("<td colspan=1>  ");
                        //st.Append("<b>" + accountAMTable.Rows[0]["UN"].ToString());
                        //st.Append("</td>");
                        //st.Append("<td colspan=1>  ");
                        //st.Append(Resources.labels.password + " :");
                        //st.Append("</td>");
                        //st.Append("<td colspan=1>  ");
                        //st.Append("<b>" + "######" + "</b>");
                        //st.Append("</td>");
                        //st.Append("</tr>");

                        //st.Append("<tr>");
                        //if (sendPincode())
                        //{

                        //    st.Append("<td colspan=1>  ");
                        //    st.Append(Resources.labels.pincode + " :");
                        //    st.Append("</td>");
                        //    st.Append("<td colspan=1>  ");
                        //    st.Append("<b>" + "######" + "</b>");
                        //    st.Append("</td>");
                        //    st.Append("<td colspan=1>  ");
                        //    st.Append("");
                        //    st.Append("</td>");
                        //    st.Append("<td colspan=1>  ");
                        //    st.Append("</td>");
                        //}
                        //st.Append("</tr>");

                    }
                }
                st.Append("</table>");
                //i += 1;
                //if (i == userTable.Rows.Count)
                //{
                //    st.Append("<hr/>");
                //}
            }
            tmpl.SetAttribute("USERINFO", st.ToString());

            #endregion

            if (ViewState["NGUOIUYQUYEN"] != null)
            {
                DataTable tblNGUOIUYQUYEN = (DataTable)ViewState["NGUOIUYQUYEN"];
                if (tblNGUOIUYQUYEN.Rows.Count != 0)
                {

                    #region lay thong tin tai khoan cua nguoi dong so huu
                    DataTable nuyTable = (new SmartPortal.SEMS.Contract().GetUserByContractNo(hpcontractNo, SmartPortal.Constant.IPC.RP, "", ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
                    if (IPCERRORCODE != "0")
                    {
                        goto ERROR;
                    }

                    StringBuilder stNUY = new StringBuilder();
                    stNUY.Append("<div style='font-weight: bold; background-color: #C0C0C0;width:100%;height:16px;'>THÔNG TIN NGƯỜI ĐỒNG SỞ HỮU</div>");
                    //gan thong tin user vao stringtemplate
                    int j = 0;
                    foreach (DataRow row in nuyTable.Rows)
                    {

                        stNUY.Append("<table style='width:100%;'>");


                        stNUY.Append("<tr>");
                        stNUY.Append("<td width='25%'>");
                        stNUY.Append(Resources.labels.tendaydu);
                        stNUY.Append("</td>");
                        stNUY.Append("<td width='25%'>");
                        stNUY.Append(row["FULLNAME"].ToString());
                        stNUY.Append("</td>");
                        stNUY.Append("<td width='25%'>");
                        stNUY.Append("Email ");
                        stNUY.Append("</td>");
                        stNUY.Append("<td width='25%'>");
                        stNUY.Append(row["EMAIL"].ToString());
                        stNUY.Append("</td>");
                        stNUY.Append("</tr>");

                        stNUY.Append("<tr>");
                        stNUY.Append("<td>");
                        stNUY.Append(Resources.labels.phone);
                        stNUY.Append("</td>");
                        stNUY.Append("<td>");
                        stNUY.Append(row["PHONE"].ToString());
                        stNUY.Append("</td>");
                        stNUY.Append("<td>");
                        stNUY.Append(Resources.labels.status + " ");
                        stNUY.Append("</td>");
                        stNUY.Append("<td>");
                        switch (row["STATUS"].ToString().Trim())
                        {
                            case SmartPortal.Constant.IPC.NEW:
                                stNUY.Append(Resources.labels.connew);
                                break;
                            case SmartPortal.Constant.IPC.DELETE:
                                stNUY.Append(Resources.labels.condelete);

                                break;
                            case SmartPortal.Constant.IPC.ACTIVE:
                                stNUY.Append(Resources.labels.conactive);

                                break;
                            case SmartPortal.Constant.IPC.BLOCK:
                                stNUY.Append(Resources.labels.conblock);

                                break;
                            case SmartPortal.Constant.IPC.PENDING:
                                stNUY.Append(Resources.labels.conpending);

                                break;
                            case SmartPortal.Constant.IPC.REJECT:
                                stNUY.Append(Resources.labels.conreject);

                                break;
                        }
                        stNUY.Append("</td>");
                        stNUY.Append("</tr>");


                        //lay het các tai khoan Ibank cua user theo userID
                        DataSet accountIBDatasetNUY = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.IB, row["USERID"].ToString().Trim(), string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);

                        if (IPCERRORCODE != "0")
                        {
                            goto ERROR;
                        }

                        DataTable accountIBTableNUY = accountIBDatasetNUY.Tables[0];
                        if (accountIBTableNUY.Rows.Count != 0)
                        {
                            if (accountIBTableNUY.Rows[0]["ROLEID"].ToString().Trim() != "")
                            {
                                stNUY.Append("<tr>");
                                stNUY.Append("<td colspan='4'>");
                                stNUY.Append("<br/>");
                                stNUY.Append("</td>");
                                stNUY.Append("</tr>");

                                stNUY.Append("<tr>");
                                stNUY.Append("<td colspan='4'>");
                                stNUY.Append("<B>" + Resources.labels.internetbanking + "</B>");
                                stNUY.Append("</td>");
                                stNUY.Append("</tr>");

                                stNUY.Append("<tr>");
                                stNUY.Append("<td width='25%'>");
                                stNUY.Append(Resources.labels.username);
                                stNUY.Append("</td>");
                                stNUY.Append("<td width='25%'>");
                                stNUY.Append(accountIBTableNUY.Rows[0]["USERNAME"].ToString());
                                stNUY.Append("</td>");
                                stNUY.Append("<td width='25%'>");
                                stNUY.Append(Resources.labels.password);
                                stNUY.Append("</td>");
                                stNUY.Append("<td width='25%'>");
                                stNUY.Append("########");
                                //st.Append("<b>" + SmartPortal.Security.Encryption.Decrypt(accountIBTable.Rows[0]["PASSWORD"].ToString()) + "</b>");
                                stNUY.Append("</td>");
                                stNUY.Append("</tr>");
                            }
                        }
                        //lay het các tai khoan MB cua user theo userID
                        DataSet accountMBDatasetNUY = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.MB, row["USERID"].ToString().Trim(), string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);

                        if (IPCERRORCODE != "0")
                        {
                            goto ERROR;
                        }

                        DataTable accountMBTableNUY = accountMBDatasetNUY.Tables[0];
                        if (accountMBTableNUY.Rows.Count != 0)
                        {
                            if (accountMBTableNUY.Rows[0]["ROLEID"].ToString().Trim() != "")
                            {
                                stNUY.Append("<tr>");
                                stNUY.Append("<td colspan='4'>");
                                stNUY.Append("<br/>");
                                stNUY.Append("</td>");
                                stNUY.Append("</tr>");

                                stNUY.Append("<tr>");
                                stNUY.Append("<td colspan='4'>");
                                stNUY.Append("<B>" + Resources.labels.mobilebanking + "</B>");
                                stNUY.Append("</td>");
                                stNUY.Append("</tr>");

                                stNUY.Append("<tr>");
                                stNUY.Append("<td width='25%'>");
                                stNUY.Append(Resources.labels.username);
                                stNUY.Append("</td>");
                                stNUY.Append("<td width='25%'>");
                                stNUY.Append(accountMBTableNUY.Rows[0]["USERNAME"].ToString());
                                stNUY.Append("</td>");
                                stNUY.Append("<td width='25%'>");
                                stNUY.Append(Resources.labels.password);
                                stNUY.Append("</td>");
                                stNUY.Append("<td width='25%'>");
                                stNUY.Append("########");
                                //st.Append("<b>" + SmartPortal.Security.Encryption.Decrypt(accountMBTable.Rows[0]["PASS"].ToString()) + "</b>");
                                stNUY.Append("</td>");
                                stNUY.Append("</tr>");

                                stNUY.Append("<tr>");
                                stNUY.Append(Resources.labels.phone + " :");
                                stNUY.Append("</td>");
                                stNUY.Append("<td width='25%'>");
                                stNUY.Append(accountMBTableNUY.Rows[0]["UN"].ToString());
                                stNUY.Append("</td>");
                                stNUY.Append("</tr>");


                            }
                        }
                        //lay het các tai khoan WL cua user theo userID
                        DataSet accountWLDatasetNUY = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.EW, row["USERID"].ToString().Trim(), string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);

                        if (IPCERRORCODE != "0")
                        {
                            goto ERROR;
                        }

                        DataTable accountWLTableNUY = accountWLDatasetNUY.Tables[0];
                        if (accountWLTableNUY.Rows.Count != 0)
                        {
                            if (accountWLTableNUY.Rows[0]["ROLEID"].ToString().Trim() != "")
                            {
                                stNUY.Append("<tr>");
                                stNUY.Append("<td colspan='4'>");
                                stNUY.Append("<br/>");
                                stNUY.Append("</td>");
                                stNUY.Append("</tr>");

                                stNUY.Append("<tr>");
                                stNUY.Append("<td colspan='4'>");
                                stNUY.Append("<B>" + Resources.labels.walletbanking + "</B>");
                                stNUY.Append("</td>");
                                stNUY.Append("</tr>");

                                stNUY.Append("<tr>");
                                stNUY.Append("<td width='25%'>");
                                stNUY.Append(Resources.labels.phone);
                                stNUY.Append("</td>");
                                stNUY.Append("<td width='25%'>");
                                stNUY.Append(accountWLTableNUY.Rows[0]["UN"].ToString());
                                stNUY.Append("</td>");
                                stNUY.Append("<td width='25%'>");
                                stNUY.Append(Resources.labels.password);
                                stNUY.Append("</td>");
                                stNUY.Append("<td width='25%'>");
                                stNUY.Append("########");
                                //st.Append("<b>" + SmartPortal.Security.Encryption.Decrypt(accountMBTable.Rows[0]["PASS"].ToString()) + "</b>");
                                stNUY.Append("</td>");
                                stNUY.Append("</tr>");

                            }
                        }
                        //lay het các tai khoan SMS cua user theo userID
                        DataSet accountSMSDatasetNUY = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.SMS, row["USERID"].ToString().Trim(), string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);

                        if (IPCERRORCODE != "0")
                        {
                            goto ERROR;
                        }

                        DataTable accountSMSTableNUY = accountSMSDatasetNUY.Tables[0];
                        if (accountSMSTableNUY.Rows.Count != 0)
                        {
                            if (accountSMSTableNUY.Rows[0]["ROLEID"].ToString().Trim() != "")
                            {
                                stNUY.Append("<tr>");
                                stNUY.Append("<td colspan='4'>");
                                stNUY.Append("<br/>");
                                stNUY.Append("</td>");
                                stNUY.Append("</tr>");

                                stNUY.Append("<tr>");
                                stNUY.Append("<td colspan='4'>");
                                stNUY.Append("<B>" + Resources.labels.smsbanking + "</B>");
                                stNUY.Append("</td>");
                                stNUY.Append("</tr>");

                                stNUY.Append("<tr>");
                                stNUY.Append("<td width='25%'>");
                                stNUY.Append(Resources.labels.phone);
                                stNUY.Append("</td>");
                                stNUY.Append("<td width='25%'>");
                                stNUY.Append(accountSMSTableNUY.Rows[0]["UN"].ToString());
                                stNUY.Append("</td>");
                                stNUY.Append("<td width='25%'>");
                                stNUY.Append(Resources.labels.taikhoanmacdinh);
                                stNUY.Append("</td>");
                                stNUY.Append("<td width='25%'>");
                                stNUY.Append(accountSMSTableNUY.Rows[0]["DEFAULTACCTNO"].ToString());
                                stNUY.Append("</td>");
                                stNUY.Append("</tr>");
                            }
                        }



                        //lay het các tai khoan PHO cua user theo userID
                        DataSet accountPHODatasetNUY = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.PHO, row["USERID"].ToString().Trim(), string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);

                        if (IPCERRORCODE != "0")
                        {
                            goto ERROR;
                        }

                        DataTable accountPHOTableNUY = accountPHODatasetNUY.Tables[0];
                        if (accountPHOTableNUY.Rows.Count != 0)
                        {
                            if (accountPHOTableNUY.Rows[0]["ROLEID"].ToString().Trim() != "")
                            {
                                stNUY.Append("<tr>");
                                stNUY.Append("<td colspan='4'>");
                                stNUY.Append("<br/>");
                                stNUY.Append("</td>");
                                stNUY.Append("</tr>");

                                stNUY.Append("<tr>");
                                stNUY.Append("<td colspan='4'>");
                                stNUY.Append("<B>" + Resources.labels.phonebanking + "</B>");
                                stNUY.Append("</td>");
                                stNUY.Append("</tr>");

                                stNUY.Append("<tr>");
                                stNUY.Append("<td width='25%'>");
                                stNUY.Append(Resources.labels.username);
                                stNUY.Append("</td>");
                                stNUY.Append("<td width='25%'>");
                                stNUY.Append(accountPHOTableNUY.Rows[0]["UN"].ToString());
                                stNUY.Append("</td>");
                                stNUY.Append("<td width='25%'>");
                                stNUY.Append(Resources.labels.password);
                                stNUY.Append("</td>");
                                stNUY.Append("<td width='25%'>");
                                stNUY.Append("########");
                                //st.Append("<b>" + SmartPortal.Security.Encryption.Decrypt(accountPHOTable.Rows[0]["PASS"].ToString()) + "</b>");
                                stNUY.Append("</td>");
                                stNUY.Append("</tr>");
                            }
                        }

                        stNUY.Append("</table>");
                        j += 1;
                        if (j < nuyTable.Rows.Count)
                        {
                            stNUY.Append("<hr/>");
                        }
                    }
                    tmpl.SetAttribute("NGUOIUYQUYEN", stNUY.ToString());

                    #endregion

                }
            }

            //luu thong tin vao session de hien thi cho nguoi dung
            ViewState["tmpl"] = tmpl.ToString();

            //luu thong tin ban cung hop dong
            //new SmartPortal.SEMS.Contract().SaveContractReview(hpcontractNo, tmpl.ToString(), Server.MapPath("~/widgets/semscontractlist/contractfile/" + hpcontractNo + ".html"));

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

        ;
    }

    void OldContractCorp(string cn)
    {
        Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();
        tmpl = SmartPortal.Common.ST.GetStringTemplate("SEMSContractApprove", "ContractAttachment" + System.Globalization.CultureInfo.CurrentCulture.ToString());

        //lay thong tin hop dong de gui mail

        try
        {

            string hpcontractNo = cn;
            string custID = "";
            tmpl.Reset();

            //lay thong tin hop dong boi contractno
            DataTable contractTable = (new SmartPortal.SEMS.Contract().GetContractByContractNo(hpcontractNo, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
            if (IPCERRORCODE != "0")
            {
                goto ERROR;
            }
            //gan thong tin hop dong vao stringtemplate
            if (contractTable.Rows.Count != 0)
            {
                tmpl.SetAttribute("CONTRACTNO", contractTable.Rows[0]["CONTRACTNO"].ToString());
                DataSet dsUserType = new DataSet();
                string userType = LoadSubUserType(contractTable.Rows[0]["USERTYPE"].ToString());
                switch (userType)
                {
                    case "A":
                        tmpl.SetAttribute("CONTRACTTYPE", "Agent"); break;
                    case "M":
                        tmpl.SetAttribute("CONTRACTTYPE", "Merchant"); break;
                    case "C":
                        tmpl.SetAttribute("CONTRACTTYPE", "Consumer"); break;
                    case "CCO":
                        tmpl.SetAttribute("CONTRACTTYPE", "Corporate"); break;
                    default:
                        tmpl.SetAttribute("CONTRACTTYPE", "Consumer"); break;
                }
                tmpl.SetAttribute("OPENDATE", SmartPortal.Common.Utilities.Utility.IsDateTime2(contractTable.Rows[0]["CREATEDATE"].ToString()).ToString("dd/MM/yyyy"));
                tmpl.SetAttribute("ENDDATE", SmartPortal.Common.Utilities.Utility.IsDateTime2(contractTable.Rows[0]["ENDDATE"].ToString()).ToString("dd/MM/yyyy"));


                tmpl.SetAttribute("PRODUCT", contractTable.Rows[0]["PRODUCTNAME"].ToString());
                switch (contractTable.Rows[0]["STATUS"].ToString().Trim())
                {
                    case SmartPortal.Constant.IPC.NEW:
                        tmpl.SetAttribute("STATUS", Resources.labels.connew);
                        break;
                    case SmartPortal.Constant.IPC.DELETE:
                        tmpl.SetAttribute("STATUS", Resources.labels.condelete);

                        break;
                    case SmartPortal.Constant.IPC.ACTIVE:
                        tmpl.SetAttribute("STATUS", Resources.labels.conactive);

                        break;
                    case SmartPortal.Constant.IPC.BLOCK:
                        tmpl.SetAttribute("STATUS", Resources.labels.conblock);

                        break;
                    case SmartPortal.Constant.IPC.PENDING:
                        tmpl.SetAttribute("STATUS", Resources.labels.conpending);

                        break;
                    case SmartPortal.Constant.IPC.REJECT:
                        tmpl.SetAttribute("STATUS", Resources.labels.conreject);

                        break;
                }

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
                switch (contractTable.Rows[0]["USERTYPE"].ToString().Trim())
                {
                    case "0603":
                        tmpl.SetAttribute("CUSTTYPE", Resources.labels.doanhnghiep);
                        break;
                    default:
                        tmpl.SetAttribute("CUSTTYPE", Resources.labels.canhan);
                        break;
                }
                tmpl.SetAttribute("PHONE", custTable.Rows[0]["TEL"].ToString());
                tmpl.SetAttribute("EMAIL", custTable.Rows[0]["EMAIL"].ToString());
                tmpl.SetAttribute("CIF", custTable.Rows[0]["CUSTCODE"].ToString());
            }


            #region lay thong tin tai khoan 
            DataTable nqtTable = (new SmartPortal.SEMS.Contract().GetUserByContractNo(hpcontractNo, "0603", "", ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
            if (IPCERRORCODE != "0")
            {
                goto ERROR;
            }

            StringBuilder stNQT = new StringBuilder();
            if (nqtTable.Rows.Count != 0)
            {
                stNQT.Append("<div style='font-weight: bold; background-color: #C0C0C0;width:100%;height:16px;'>" + Resources.labels.thongtintaikhoan.ToUpper() + "</div>");
            }
            //gan thong tin user vao stringtemplate
            int j = 0;
            foreach (DataRow row in nqtTable.Rows)
            {
                stNQT.Append("<table style='width:100%;border-top: 1px solid #c0c0c0;padding-top: 8px;margin-top: 10px;'>");
                stNQT.Append("<tr>");
                stNQT.Append("<td width='25%'>");
                stNQT.Append(Resources.labels.tendaydu + " ");
                stNQT.Append("</td>");
                stNQT.Append("<td width='25%'>");
                stNQT.Append(row["FULLNAME"].ToString());
                stNQT.Append("</td>");
                stNQT.Append("<td width='25%'>");
                stNQT.Append("Email ");
                stNQT.Append("</td>");
                stNQT.Append("<td width='25%'>");
                stNQT.Append(row["EMAIL"].ToString());
                stNQT.Append("</td>");
                stNQT.Append("</tr>");

                stNQT.Append("<tr>");
                stNQT.Append("<td>");
                stNQT.Append(Resources.labels.dienthoai + " ");
                stNQT.Append("</td>");
                stNQT.Append("<td>");
                stNQT.Append(row["PHONE"].ToString());
                stNQT.Append("</td>");
                stNQT.Append("<td>");
                stNQT.Append(Resources.labels.status + " ");
                stNQT.Append("</td>");
                stNQT.Append("<td>");
                switch (row["STATUS"].ToString().Trim())
                {
                    case SmartPortal.Constant.IPC.NEW:
                        stNQT.Append(Resources.labels.connew);
                        break;
                    case SmartPortal.Constant.IPC.DELETE:
                        stNQT.Append(Resources.labels.condelete);

                        break;
                    case SmartPortal.Constant.IPC.ACTIVE:
                        stNQT.Append(Resources.labels.conactive);

                        break;
                    case SmartPortal.Constant.IPC.BLOCK:
                        stNQT.Append(Resources.labels.conblock);

                        break;
                    case SmartPortal.Constant.IPC.PENDING:
                        stNQT.Append(Resources.labels.conpending);

                        break;
                    case SmartPortal.Constant.IPC.REJECT:
                        stNQT.Append(Resources.labels.conreject);

                        break;
                }
                stNQT.Append("</td>");
                stNQT.Append("</tr>");


                //lay het các tai khoan Ibank cua user theo userID
                DataSet accountIBDatasetNQT = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.IB, row["USERID"].ToString().Trim(), string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);

                if (IPCERRORCODE != "0")
                {
                    goto ERROR;
                }

                DataTable accountIBTableNQT = accountIBDatasetNQT.Tables[0];
                if (accountIBTableNQT.Rows.Count != 0)
                {
                    if (accountIBTableNQT.Rows[0]["ROLEID"].ToString().Trim() != "")
                    {
                        stNQT.Append("<tr>");
                        stNQT.Append("<td colspan='4'>");
                        stNQT.Append("<br/>");
                        stNQT.Append("</td>");
                        stNQT.Append("</tr>");

                        stNQT.Append("<tr>");
                        stNQT.Append("<td colspan='4'>");
                        stNQT.Append("<B>Internet Banking</B>");
                        stNQT.Append("</td>");
                        stNQT.Append("</tr>");

                        stNQT.Append("<tr>");
                        stNQT.Append("<td width='25%'>");
                        stNQT.Append(Resources.labels.tendangnhap + " :");
                        stNQT.Append("</td>");
                        stNQT.Append("<td width='25%'>");
                        stNQT.Append(accountIBTableNQT.Rows[0]["USERNAME"].ToString());
                        stNQT.Append("</td>");
                        stNQT.Append("<td width='25%'>");
                        stNQT.Append(Resources.labels.matkhau + " :");
                        stNQT.Append("</td>");
                        stNQT.Append("<td width='25%'>");
                        stNQT.Append("########");
                        //st.Append("<b>" + SmartPortal.Security.Encryption.Decrypt(accountIBTable.Rows[0]["PASSWORD"].ToString()) + "</b>");
                        stNQT.Append("</td>");
                        stNQT.Append("</tr>");
                    }
                }

                //lay het các tai khoan MB cua user theo userID
                DataSet accountMBDatasetNQT = new SmartPortal.SEMS.User().GetUserSendInfor(SmartPortal.Constant.IPC.MB, row["USERID"].ToString().Trim(), ref IPCERRORCODE, ref IPCERRORDESC);

                if (IPCERRORCODE != "0")
                {
                    goto ERROR;
                }

                DataTable accountMBTableNQT = accountMBDatasetNQT.Tables[0];
                if (accountMBTableNQT.Rows.Count != 0)
                {

                        stNQT.Append("<tr>");
                        stNQT.Append("<td colspan='4'>");
                        stNQT.Append("<br/>");
                        stNQT.Append("</td>");
                        stNQT.Append("</tr>");

                        stNQT.Append("<tr>");
                        stNQT.Append("<td colspan='4'>");
                        stNQT.Append("<B>Mobile Banking</B>");
                        stNQT.Append("</td>");
                        stNQT.Append("</tr>");

                        stNQT.Append("<tr>");
                        stNQT.Append("<td width='25%'>");
                        stNQT.Append(Resources.labels.tendangnhap + " :");
                        stNQT.Append("</td>");
                        stNQT.Append("<td width='25%'>");
                        stNQT.Append(accountMBTableNQT.Rows[0]["UN"].ToString());
                        stNQT.Append("</td>");
                        stNQT.Append("<td width='25%'>");
                        stNQT.Append(Resources.labels.matkhau + " :");
                        stNQT.Append("</td>");
                        stNQT.Append("<td width='25%'>");
                        stNQT.Append("########");
                        //st.Append("<b>" + SmartPortal.Security.Encryption.Decrypt(accountMBTable.Rows[0]["PASS"].ToString()) + "</b>");
                        stNQT.Append("</td>");
                        stNQT.Append("</tr>");

                }
            }
            tmpl.SetAttribute("USERINFO", stNQT.ToString());

            #endregion


            #region lay thong tin tai khoan cua nguoi dong so huu
            DataTable ctkTable = (new SmartPortal.SEMS.Contract().GetUserByContractNo(hpcontractNo, SmartPortal.Constant.IPC.CHUTAIKHOAN, "", ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
            if (IPCERRORCODE != "0")
            {
                goto ERROR;
            }

            StringBuilder stctk = new StringBuilder();
            if (ctkTable.Rows.Count != 0)
            {
                stctk.Append("<div style='font-weight: bold; background-color: #C0C0C0;width:100%;height:16px;'>" + Resources.labels.thongtinchutaikhoan.ToUpper() + "</div>");
            }
            //gan thong tin user vao stringtemplate
            int j1 = 0;
            foreach (DataRow row in ctkTable.Rows)
            {

                stctk.Append("<table style='width:100%;'>");


                stctk.Append("<tr>");
                stctk.Append("<td width='25%'>");
                stctk.Append(Resources.labels.tendaydu + " ");
                stctk.Append("</td>");
                stctk.Append("<td width='25%'>");
                stctk.Append(row["FULLNAME"].ToString());
                stctk.Append("</td>");
                stctk.Append("<td width='25%'>");
                stctk.Append("Email ");
                stctk.Append("</td>");
                stctk.Append("<td width='25%'>");
                stctk.Append(row["EMAIL"].ToString());
                stctk.Append("</td>");
                stctk.Append("</tr>");

                stctk.Append("<tr>");
                stctk.Append("<td>");
                stctk.Append(Resources.labels.dienthoai + " ");
                stctk.Append("</td>");
                stctk.Append("<td>");
                stctk.Append(row["PHONE"].ToString());
                stctk.Append("</td>");
                stctk.Append("<td>");
                stctk.Append(Resources.labels.status + " ");
                stctk.Append("</td>");
                stctk.Append("<td>");
                switch (row["STATUS"].ToString().Trim())
                {
                    case SmartPortal.Constant.IPC.NEW:
                        stctk.Append(Resources.labels.connew);
                        break;
                    case SmartPortal.Constant.IPC.DELETE:
                        stctk.Append(Resources.labels.condelete);

                        break;
                    case SmartPortal.Constant.IPC.ACTIVE:
                        stctk.Append(Resources.labels.conactive);

                        break;
                    case SmartPortal.Constant.IPC.BLOCK:
                        stctk.Append(Resources.labels.conblock);

                        break;
                    case SmartPortal.Constant.IPC.PENDING:
                        stctk.Append(Resources.labels.conpending);

                        break;
                    case SmartPortal.Constant.IPC.REJECT:
                        stctk.Append(Resources.labels.conreject);

                        break;
                }
                stctk.Append("</td>");
                stctk.Append("</tr>");


                //lay het các tai khoan Ibank cua user theo userID
                DataSet accountIBDatasetctk = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.IB, row["USERID"].ToString().Trim(), string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);

                if (IPCERRORCODE != "0")
                {
                    goto ERROR;
                }

                DataTable accountIBTablectk = accountIBDatasetctk.Tables[0];
                if (accountIBTablectk.Rows.Count != 0)
                {
                    if (accountIBTablectk.Rows[0]["ROLEID"].ToString().Trim() != "")
                    {
                        stctk.Append("<tr>");
                        stctk.Append("<td colspan='4'>");
                        stctk.Append("<br/>");
                        stctk.Append("</td>");
                        stctk.Append("</tr>");

                        stctk.Append("<tr>");
                        stctk.Append("<td colspan='4'>");
                        stctk.Append("<B>Internet Banking</B>");
                        stctk.Append("</td>");
                        stctk.Append("</tr>");

                        stctk.Append("<tr>");
                        stctk.Append("<td width='25%'>");
                        stctk.Append(Resources.labels.tendangnhap + " :");
                        stctk.Append("</td>");
                        stctk.Append("<td width='25%'>");
                        stctk.Append(accountIBTablectk.Rows[0]["USERNAME"].ToString());
                        stctk.Append("</td>");
                        stctk.Append("<td width='25%'>");
                        stctk.Append(Resources.labels.matkhau + " :");
                        stctk.Append("</td>");
                        stctk.Append("<td width='25%'>");
                        stctk.Append("########");
                        //st.Append("<b>" + SmartPortal.Security.Encryption.Decrypt(accountIBTable.Rows[0]["PASSWORD"].ToString()) + "</b>");
                        stctk.Append("</td>");
                        stctk.Append("</tr>");
                    }
                }

                //lay het các tai khoan SMS cua user theo userID
                DataSet accountSMSDatasetctk = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.SMS, row["USERID"].ToString().Trim(), string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);

                if (IPCERRORCODE != "0")
                {
                    goto ERROR;
                }

                DataTable accountSMSTablectk = accountSMSDatasetctk.Tables[0];
                if (accountSMSTablectk.Rows.Count != 0)
                {
                    if (accountSMSTablectk.Rows[0]["ROLEID"].ToString().Trim() != "")
                    {
                        stctk.Append("<tr>");
                        stctk.Append("<td colspan='4'>");
                        stctk.Append("<br/>");
                        stctk.Append("</td>");
                        stctk.Append("</tr>");

                        stctk.Append("<tr>");
                        stctk.Append("<td colspan='4'>");
                        stctk.Append("<B>SMS Banking</B>");
                        stctk.Append("</td>");
                        stctk.Append("</tr>");

                        stctk.Append("<tr>");
                        stctk.Append("<td width='25%'>");
                        stctk.Append(Resources.labels.sodienthoai + " :");
                        stctk.Append("</td>");
                        stctk.Append("<td width='25%'>");
                        stctk.Append(accountSMSTablectk.Rows[0]["UN"].ToString());
                        stctk.Append("</td>");
                        stctk.Append("<td width='25%'>");
                        stctk.Append(Resources.labels.taikhoanmacdinh + " :");
                        stctk.Append("</td>");
                        stctk.Append("<td width='25%'>");
                        stctk.Append(accountSMSTablectk.Rows[0]["DEFAULTACCTNO"].ToString());
                        stctk.Append("</td>");
                        stctk.Append("</tr>");
                    }
                }

                //lay het các tai khoan MB cua user theo userID
                DataSet accountMBDatasetctk = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.MB, row["USERID"].ToString().Trim(), string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);

                if (IPCERRORCODE != "0")
                {
                    goto ERROR;
                }

                DataTable accountMBTablectk = accountMBDatasetctk.Tables[0];
                if (accountMBTablectk.Rows.Count != 0)
                {
                    if (accountMBTablectk.Rows[0]["ROLEID"].ToString().Trim() != "")
                    {
                        stctk.Append("<tr>");
                        stctk.Append("<td colspan='4'>");
                        stctk.Append("<br/>");
                        stctk.Append("</td>");
                        stctk.Append("</tr>");

                        stctk.Append("<tr>");
                        stctk.Append("<td colspan='4'>");
                        stctk.Append("<B>Mobile Banking</B>");
                        stctk.Append("</td>");
                        stctk.Append("</tr>");

                        stctk.Append("<tr>");
                        stctk.Append("<td width='25%'>");
                        stctk.Append(Resources.labels.tendangnhap + " :");
                        stctk.Append("</td>");
                        stctk.Append("<td width='25%'>");
                        stctk.Append(accountMBTablectk.Rows[0]["UN"].ToString());
                        stctk.Append("</td>");
                        stctk.Append("<td width='25%'>");
                        stctk.Append(Resources.labels.matkhau + " :");
                        stctk.Append("</td>");
                        stctk.Append("<td width='25%'>");
                        stctk.Append("########");
                        //st.Append("<b>" + SmartPortal.Security.Encryption.Decrypt(accountMBTable.Rows[0]["PASS"].ToString()) + "</b>");
                        stctk.Append("</td>");
                        stctk.Append("</tr>");

                    }
                }

                //lay het các tai khoan PHO cua user theo userID
                DataSet accountPHODatasetctk = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.PHO, row["USERID"].ToString().Trim(), string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);

                if (IPCERRORCODE != "0")
                {
                    goto ERROR;
                }

                DataTable accountPHOTablectk = accountPHODatasetctk.Tables[0];
                if (accountPHOTablectk.Rows.Count != 0)
                {
                    if (accountPHOTablectk.Rows[0]["ROLEID"].ToString().Trim() != "")
                    {
                        stctk.Append("<tr>");
                        stctk.Append("<td colspan='4'>");
                        stctk.Append("<br/>");
                        stctk.Append("</td>");
                        stctk.Append("</tr>");

                        stctk.Append("<tr>");
                        stctk.Append("<td colspan='4'>");
                        stctk.Append("<B>Phone Banking</B>");
                        stctk.Append("</td>");
                        stctk.Append("</tr>");

                        stctk.Append("<tr>");
                        stctk.Append("<td width='25%'>");
                        stctk.Append(Resources.labels.tendangnhap + " :");
                        stctk.Append("</td>");
                        stctk.Append("<td width='25%'>");
                        stctk.Append(accountPHOTablectk.Rows[0]["UN"].ToString());
                        stctk.Append("</td>");
                        stctk.Append("<td width='25%'>");
                        stctk.Append(Resources.labels.matkhau + " :");
                        stctk.Append("</td>");
                        stctk.Append("<td width='25%'>");
                        stctk.Append("########");
                        //st.Append("<b>" + SmartPortal.Security.Encryption.Decrypt(accountPHOTable.Rows[0]["PASS"].ToString()) + "</b>");
                        stctk.Append("</td>");
                        stctk.Append("</tr>");
                    }
                }

                stctk.Append("</table>");
                j1 += 1;
                if (j1 < ctkTable.Rows.Count)
                {
                    stctk.Append("<hr/>");
                }
            }
            tmpl.SetAttribute("NGUOIUYQUYEN", stctk.ToString());

            #endregion


            #region lay thong tin tai khoan cua nguoi dong so huu
            DataTable nuqTable = (new SmartPortal.SEMS.Contract().GetUserByContractNo(hpcontractNo, SmartPortal.Constant.IPC.NGUOIUYQUYEN, "", ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
            if (IPCERRORCODE != "0")
            {
                goto ERROR;
            }

            StringBuilder stnuq = new StringBuilder();
            if (nuqTable.Rows.Count != 0)
            {
                stnuq.Append("<div style='font-weight: bold; background-color: #C0C0C0;width:100%;height:16px;'>" + Resources.labels.thongtinnguoiuyquyen.ToUpper() + "</div>");
            }
            //gan thong tin user vao stringtemplate
            int j2 = 0;
            foreach (DataRow row in nuqTable.Rows)
            {

                stnuq.Append("<table style='width:100%;'>");


                stnuq.Append("<tr>");
                stnuq.Append("<td width='25%'>");
                stnuq.Append(Resources.labels.tendaydu + " ");
                stnuq.Append("</td>");
                stnuq.Append("<td width='25%'>");
                stnuq.Append(row["FULLNAME"].ToString());
                stnuq.Append("</td>");
                stnuq.Append("<td width='25%'>");
                stnuq.Append("Email ");
                stnuq.Append("</td>");
                stnuq.Append("<td width='25%'>");
                stnuq.Append(row["EMAIL"].ToString());
                stnuq.Append("</td>");
                stnuq.Append("</tr>");

                stnuq.Append("<tr>");
                stnuq.Append("<td>");
                stnuq.Append(Resources.labels.dienthoai + " ");
                stnuq.Append("</td>");
                stnuq.Append("<td>");
                stnuq.Append(row["PHONE"].ToString());
                stnuq.Append("</td>");
                stnuq.Append("<td>");
                stnuq.Append(Resources.labels.status + " ");
                stnuq.Append("</td>");
                stnuq.Append("<td>");
                switch (row["STATUS"].ToString().Trim())
                {
                    case SmartPortal.Constant.IPC.NEW:
                        stnuq.Append(Resources.labels.connew);
                        break;
                    case SmartPortal.Constant.IPC.DELETE:
                        stnuq.Append(Resources.labels.condelete);

                        break;
                    case SmartPortal.Constant.IPC.ACTIVE:
                        stnuq.Append(Resources.labels.conactive);

                        break;
                    case SmartPortal.Constant.IPC.BLOCK:
                        stnuq.Append(Resources.labels.conblock);

                        break;
                    case SmartPortal.Constant.IPC.PENDING:
                        stnuq.Append(Resources.labels.conpending);

                        break;
                    case SmartPortal.Constant.IPC.REJECT:
                        stnuq.Append(Resources.labels.conreject);

                        break;
                }
                stnuq.Append("</td>");
                stnuq.Append("</tr>");


                //lay het các tai khoan Ibank cua user theo userID
                DataSet accountIBDatasetnuq = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.IB, row["USERID"].ToString().Trim(), string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);

                if (IPCERRORCODE != "0")
                {
                    goto ERROR;
                }

                DataTable accountIBTablenuq = accountIBDatasetnuq.Tables[0];
                if (accountIBTablenuq.Rows.Count != 0)
                {
                    if (accountIBTablenuq.Rows[0]["ROLEID"].ToString().Trim() != "")
                    {
                        stnuq.Append("<tr>");
                        stnuq.Append("<td colspan='4'>");
                        stnuq.Append("<br/>");
                        stnuq.Append("</td>");
                        stnuq.Append("</tr>");

                        stnuq.Append("<tr>");
                        stnuq.Append("<td colspan='4'>");
                        stnuq.Append("<B>Internet Banking</B>");
                        stnuq.Append("</td>");
                        stnuq.Append("</tr>");

                        stnuq.Append("<tr>");
                        stnuq.Append("<td width='25%'>");
                        stnuq.Append(Resources.labels.tendangnhap + " :");
                        stnuq.Append("</td>");
                        stnuq.Append("<td width='25%'>");
                        stnuq.Append(accountIBTablenuq.Rows[0]["USERNAME"].ToString());
                        stnuq.Append("</td>");
                        stnuq.Append("<td width='25%'>");
                        stnuq.Append(Resources.labels.matkhau + " :");
                        stnuq.Append("</td>");
                        stnuq.Append("<td width='25%'>");
                        stnuq.Append("########");
                        //st.Append("<b>" + SmartPortal.Security.Encryption.Decrypt(accountIBTable.Rows[0]["PASSWORD"].ToString()) + "</b>");
                        stnuq.Append("</td>");
                        stnuq.Append("</tr>");
                    }
                }

                //lay het các tai khoan SMS cua user theo userID
                DataSet accountSMSDatasetnuq = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.SMS, row["USERID"].ToString().Trim(), string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);

                if (IPCERRORCODE != "0")
                {
                    goto ERROR;
                }

                DataTable accountSMSTablenuq = accountSMSDatasetnuq.Tables[0];
                if (accountSMSTablenuq.Rows.Count != 0)
                {
                    if (accountSMSTablenuq.Rows[0]["ROLEID"].ToString().Trim() != "")
                    {
                        stnuq.Append("<tr>");
                        stnuq.Append("<td colspan='4'>");
                        stnuq.Append("<br/>");
                        stnuq.Append("</td>");
                        stnuq.Append("</tr>");

                        stnuq.Append("<tr>");
                        stnuq.Append("<td colspan='4'>");
                        stnuq.Append("<B>SMS Banking</B>");
                        stnuq.Append("</td>");
                        stnuq.Append("</tr>");

                        stnuq.Append("<tr>");
                        stnuq.Append("<td width='25%'>");
                        stnuq.Append(Resources.labels.sodienthoai + " :");
                        stnuq.Append("</td>");
                        stnuq.Append("<td width='25%'>");
                        stnuq.Append(accountSMSTablenuq.Rows[0]["UN"].ToString());
                        stnuq.Append("</td>");
                        stnuq.Append("<td width='25%'>");
                        stnuq.Append(Resources.labels.taikhoanmacdinh + " :");
                        stnuq.Append("</td>");
                        stnuq.Append("<td width='25%'>");
                        stnuq.Append(accountSMSTablenuq.Rows[0]["DEFAULTACCTNO"].ToString());
                        stnuq.Append("</td>");
                        stnuq.Append("</tr>");
                    }
                }

                //lay het các tai khoan MB cua user theo userID
                DataSet accountMBDatasetnuq = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.MB, row["USERID"].ToString().Trim(), string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);

                if (IPCERRORCODE != "0")
                {
                    goto ERROR;
                }

                DataTable accountMBTablenuq = accountMBDatasetnuq.Tables[0];
                if (accountMBTablenuq.Rows.Count != 0)
                {
                    if (accountMBTablenuq.Rows[0]["ROLEID"].ToString().Trim() != "")
                    {
                        stnuq.Append("<tr>");
                        stnuq.Append("<td colspan='4'>");
                        stnuq.Append("<br/>");
                        stnuq.Append("</td>");
                        stnuq.Append("</tr>");

                        stnuq.Append("<tr>");
                        stnuq.Append("<td colspan='4'>");
                        stnuq.Append("<B>Mobile Banking</B>");
                        stnuq.Append("</td>");
                        stnuq.Append("</tr>");

                        stnuq.Append("<tr>");
                        stnuq.Append("<td width='25%'>");
                        stnuq.Append(Resources.labels.tendangnhap + " :");
                        stnuq.Append("</td>");
                        stnuq.Append("<td width='25%'>");
                        stnuq.Append(accountMBTablenuq.Rows[0]["UN"].ToString());
                        stnuq.Append("</td>");
                        stnuq.Append("<td width='25%'>");
                        stnuq.Append(Resources.labels.matkhau + " :");
                        stnuq.Append("</td>");
                        stnuq.Append("<td width='25%'>");
                        stnuq.Append("########");
                        //st.Append("<b>" + SmartPortal.Security.Encryption.Decrypt(accountMBTable.Rows[0]["PASS"].ToString()) + "</b>");
                        stnuq.Append("</td>");
                        stnuq.Append("</tr>");
                    }

                }

                //lay het các tai khoan PHO cua user theo userID
                DataSet accountPHODatasetnuq = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.PHO, row["USERID"].ToString().Trim(), string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);

                if (IPCERRORCODE != "0")
                {
                    goto ERROR;
                }

                DataTable accountPHOTablenuq = accountPHODatasetnuq.Tables[0];
                if (accountPHOTablenuq.Rows.Count != 0)
                {
                    if (accountPHOTablenuq.Rows[0]["ROLEID"].ToString().Trim() != "")
                    {
                        stnuq.Append("<tr>");
                        stnuq.Append("<td colspan='4'>");
                        stnuq.Append("<br/>");
                        stnuq.Append("</td>");
                        stnuq.Append("</tr>");

                        stnuq.Append("<tr>");
                        stnuq.Append("<td colspan='4'>");
                        stnuq.Append("<B>Phone Banking</B>");
                        stnuq.Append("</td>");
                        stnuq.Append("</tr>");

                        stnuq.Append("<tr>");
                        stnuq.Append("<td width='25%'>");
                        stnuq.Append(Resources.labels.tendangnhap + " :");
                        stnuq.Append("</td>");
                        stnuq.Append("<td width='25%'>");
                        stnuq.Append(accountPHOTablenuq.Rows[0]["UN"].ToString());
                        stnuq.Append("</td>");
                        stnuq.Append("<td width='25%'>");
                        stnuq.Append(Resources.labels.matkhau + " :");
                        stnuq.Append("</td>");
                        stnuq.Append("<td width='25%'>");
                        stnuq.Append("########");
                        //st.Append("<b>" + SmartPortal.Security.Encryption.Decrypt(accountPHOTable.Rows[0]["PASS"].ToString()) + "</b>");
                        stnuq.Append("</td>");
                        stnuq.Append("</tr>");
                    }
                }

                stnuq.Append("</table>");
                j2 += 1;
                if (j2 < nuqTable.Rows.Count)
                {
                    stnuq.Append("<hr/>");
                }
            }
            tmpl.SetAttribute("NUQ", stnuq.ToString());

            #endregion

            #region lay thong tin tai khoan cua nguoi dong so huu
            DataTable l2Table = (new SmartPortal.SEMS.Contract().GetUserByContractNo(hpcontractNo, SmartPortal.Constant.IPC.NGUOIDUNGCAP2, "", ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
            if (IPCERRORCODE != "0")
            {
                goto ERROR;
            }

            StringBuilder stl2 = new StringBuilder();
            if (l2Table.Rows.Count != 0)
            {
                stl2.Append("<div style='font-weight: bold; background-color: #C0C0C0;width:100%;height:16px;'>" + Resources.labels.thongtinnguoidungcaphai.ToUpper() + "</div>");
            }
            //gan thong tin user vao stringtemplate
            int j3 = 0;
            foreach (DataRow row in l2Table.Rows)
            {

                stl2.Append("<table style='width:100%;'>");


                stl2.Append("<tr>");
                stl2.Append("<td width='25%'>");
                stl2.Append(Resources.labels.tendaydu + " ");
                stl2.Append("</td>");
                stl2.Append("<td width='25%'>");
                stl2.Append(row["FULLNAME"].ToString());
                stl2.Append("</td>");
                stl2.Append("<td width='25%'>");
                stl2.Append("Email ");
                stl2.Append("</td>");
                stl2.Append("<td width='25%'>");
                stl2.Append(row["EMAIL"].ToString());
                stl2.Append("</td>");
                stl2.Append("</tr>");

                stl2.Append("<tr>");
                stl2.Append("<td>");
                stl2.Append(Resources.labels.dienthoai + " ");
                stl2.Append("</td>");
                stl2.Append("<td>");
                stl2.Append(row["PHONE"].ToString());
                stl2.Append("</td>");
                stl2.Append("<td>");
                stl2.Append(Resources.labels.status + " ");
                stl2.Append("</td>");
                stl2.Append("<td>");
                switch (row["STATUS"].ToString().Trim())
                {
                    case SmartPortal.Constant.IPC.NEW:
                        stl2.Append(Resources.labels.connew);
                        break;
                    case SmartPortal.Constant.IPC.DELETE:
                        stl2.Append(Resources.labels.condelete);

                        break;
                    case SmartPortal.Constant.IPC.ACTIVE:
                        stl2.Append(Resources.labels.conactive);

                        break;
                    case SmartPortal.Constant.IPC.BLOCK:
                        stl2.Append(Resources.labels.conblock);

                        break;
                    case SmartPortal.Constant.IPC.PENDING:
                        stl2.Append(Resources.labels.conpending);

                        break;
                    case SmartPortal.Constant.IPC.REJECT:
                        stl2.Append(Resources.labels.conreject);

                        break;
                }
                stl2.Append("</td>");
                stl2.Append("</tr>");


                //lay het các tai khoan Ibank cua user theo userID
                DataSet accountIBDatasetl2 = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.IB, row["USERID"].ToString().Trim(), string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);

                if (IPCERRORCODE != "0")
                {
                    goto ERROR;
                }

                DataTable accountIBTablel2 = accountIBDatasetl2.Tables[0];
                if (accountIBTablel2.Rows.Count != 0)
                {
                    if (accountIBTablel2.Rows[0]["ROLEID"].ToString().Trim() != "")
                    {
                        stl2.Append("<tr>");
                        stl2.Append("<td colspan='4'>");
                        stl2.Append("<br/>");
                        stl2.Append("</td>");
                        stl2.Append("</tr>");

                        stl2.Append("<tr>");
                        stl2.Append("<td colspan='4'>");
                        stl2.Append("<B>Internet Banking</B>");
                        stl2.Append("</td>");
                        stl2.Append("</tr>");

                        stl2.Append("<tr>");
                        stl2.Append("<td width='25%'>");
                        stl2.Append(Resources.labels.tendangnhap + " :");
                        stl2.Append("</td>");
                        stl2.Append("<td width='25%'>");
                        stl2.Append(accountIBTablel2.Rows[0]["USERNAME"].ToString());
                        stl2.Append("</td>");
                        stl2.Append("<td width='25%'>");
                        stl2.Append(Resources.labels.matkhau + " :");
                        stl2.Append("</td>");
                        stl2.Append("<td width='25%'>");
                        stl2.Append("########");
                        //st.Append("<b>" + SmartPortal.Security.Encryption.Decrypt(accountIBTable.Rows[0]["PASSWORD"].ToString()) + "</b>");
                        stl2.Append("</td>");
                        stl2.Append("</tr>");
                    }
                }

                //lay het các tai khoan SMS cua user theo userID
                DataSet accountSMSDatasetl2 = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.SMS, row["USERID"].ToString().Trim(), string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);

                if (IPCERRORCODE != "0")
                {
                    goto ERROR;
                }

                DataTable accountSMSTablel2 = accountSMSDatasetl2.Tables[0];
                if (accountSMSTablel2.Rows.Count != 0)
                {
                    if (accountSMSTablel2.Rows[0]["ROLEID"].ToString().Trim() != "")
                    {
                        stl2.Append("<tr>");
                        stl2.Append("<td colspan='4'>");
                        stl2.Append("<br/>");
                        stl2.Append("</td>");
                        stl2.Append("</tr>");

                        stl2.Append("<tr>");
                        stl2.Append("<td colspan='4'>");
                        stl2.Append("<B>SMS Banking</B>");
                        stl2.Append("</td>");
                        stl2.Append("</tr>");

                        stl2.Append("<tr>");
                        stl2.Append("<td width='25%'>");
                        stl2.Append(Resources.labels.sodienthoai + " :");
                        stl2.Append("</td>");
                        stl2.Append("<td width='25%'>");
                        stl2.Append(accountSMSTablel2.Rows[0]["UN"].ToString());
                        stl2.Append("</td>");
                        stl2.Append("<td width='25%'>");
                        stl2.Append(Resources.labels.taikhoanmacdinh + " :");
                        stl2.Append("</td>");
                        stl2.Append("<td width='25%'>");
                        stl2.Append(accountSMSTablel2.Rows[0]["DEFAULTACCTNO"].ToString());
                        stl2.Append("</td>");
                        stl2.Append("</tr>");
                    }
                }

                //lay het các tai khoan MB cua user theo userID
                DataSet accountMBDatasetl2 = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.MB, row["USERID"].ToString().Trim(), string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);

                if (IPCERRORCODE != "0")
                {
                    goto ERROR;
                }

                DataTable accountMBTablel2 = accountMBDatasetl2.Tables[0];
                if (accountMBTablel2.Rows.Count != 0)
                {
                    if (accountMBTablel2.Rows[0]["ROLEID"].ToString().Trim() != "")
                    {
                        stl2.Append("<tr>");
                        stl2.Append("<td colspan='4'>");
                        stl2.Append("<br/>");
                        stl2.Append("</td>");
                        stl2.Append("</tr>");

                        stl2.Append("<tr>");
                        stl2.Append("<td colspan='4'>");
                        stl2.Append("<B>Mobile Banking</B>");
                        stl2.Append("</td>");
                        stl2.Append("</tr>");

                        stl2.Append("<tr>");
                        stl2.Append("<td width='25%'>");
                        stl2.Append(Resources.labels.tendangnhap + " :");
                        stl2.Append("</td>");
                        stl2.Append("<td width='25%'>");
                        stl2.Append(accountMBTablel2.Rows[0]["UN"].ToString());
                        stl2.Append("</td>");
                        stl2.Append("<td width='25%'>");
                        stl2.Append(Resources.labels.matkhau + " :");
                        stl2.Append("</td>");
                        stl2.Append("<td width='25%'>");
                        stl2.Append("########");
                        //st.Append("<b>" + SmartPortal.Security.Encryption.Decrypt(accountMBTable.Rows[0]["PASS"].ToString()) + "</b>");
                        stl2.Append("</td>");
                        stl2.Append("</tr>");
                    }

                }

                //lay het các tai khoan PHO cua user theo userID
                DataSet accountPHODatasetl2 = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.PHO, row["USERID"].ToString().Trim(), string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);

                if (IPCERRORCODE != "0")
                {
                    goto ERROR;
                }

                DataTable accountPHOTablel2 = accountPHODatasetl2.Tables[0];
                if (accountPHOTablel2.Rows.Count != 0)
                {
                    if (accountPHOTablel2.Rows[0]["ROLEID"].ToString().Trim() != "")
                    {
                        stl2.Append("<tr>");
                        stl2.Append("<td colspan='4'>");
                        stl2.Append("<br/>");
                        stl2.Append("</td>");
                        stl2.Append("</tr>");

                        stl2.Append("<tr>");
                        stl2.Append("<td colspan='4'>");
                        stl2.Append("<B>Phone Banking</B>");
                        stl2.Append("</td>");
                        stl2.Append("</tr>");

                        stl2.Append("<tr>");
                        stl2.Append("<td width='25%'>");
                        stl2.Append(Resources.labels.tendangnhap + " :");
                        stl2.Append("</td>");
                        stl2.Append("<td width='25%'>");
                        stl2.Append(accountPHOTablel2.Rows[0]["UN"].ToString());
                        stl2.Append("</td>");
                        stl2.Append("<td width='25%'>");
                        stl2.Append(Resources.labels.matkhau + " :");
                        stl2.Append("</td>");
                        stl2.Append("<td width='25%'>");
                        stl2.Append("########");
                        //st.Append("<b>" + SmartPortal.Security.Encryption.Decrypt(accountPHOTable.Rows[0]["PASS"].ToString()) + "</b>");
                        stl2.Append("</td>");
                        stl2.Append("</tr>");
                    }
                }

                stl2.Append("</table>");
                j3 += 1;
                if (j3 < l2Table.Rows.Count)
                {
                    stl2.Append("<hr/>");
                }
            }
            tmpl.SetAttribute("LEVEL2", stl2.ToString());

            #endregion


            ViewState["tmpl"] = tmpl.ToString();

            //luu thong tin ban cung hop dong
            new SmartPortal.SEMS.Contract().SaveContractReview(hpcontractNo, tmpl.ToString(), Server.MapPath("~/widgets/semscontractlist/contractfile/" + hpcontractNo + ".html"));

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

        ;
    }
    private string LoadSubUserType(string SubUserType)
    {
        DataTable dsUserType = new SmartPortal.SEMS.Services().GetUserType(SubUserType, string.Empty, ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
        if (IPCERRORCODE.Equals("0"))
        {
            return dsUserType.Rows[0]["TYPE"].ToString();
        }
        else
        {
            return string.Empty;
        }
    }
    protected override void InitializeCulture()
    {

        try
        {
            string culture;

            if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["l"] == null)
            {
                if (Session["langID"] == null)
                {
                    culture = new PortalSettings().portalSetting.DefaultLang;
                }
                else
                {
                    culture = Session["langID"].ToString();
                }
            }
            else
            {
                culture = Utility.KillSqlInjection(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["l"].ToString());
                Session["langID"] = culture;
            }

            //OR This

            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo(culture);
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            System.Threading.Thread.CurrentThread.CurrentUICulture = ci;

            base.InitializeCulture();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Template_Face1_Default", "InitializeCulture", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }

    }
    public bool sendPincode()
    {
        try
        {
            string result = new SmartPortal.SEMS.User().CheckSendPincodeSysvar(ref IPCERRORCODE, ref IPCERRORDESC);
            if (result.Equals("1"))
                return true;
            else
                return false;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
        return false;
    }
}
