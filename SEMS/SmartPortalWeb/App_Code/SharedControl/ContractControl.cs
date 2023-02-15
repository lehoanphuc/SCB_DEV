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

using SmartPortal.Security;
using System.Text;
using SmartPortal.ExceptionCollection;
using SmartPortal.SEMS;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

/// <summary>
/// VuTT: Sử dụng share class cho các page contract khác, không cần code lại
/// Warning: Chỉ khai báo các hàm client, ko được khai báo các hàm server tại đây
/// Ps: đang thử nghiệm chưa rõ hiệu năng
/// </summary>
public static class ContractControl
{
    static string IPCERRORCODE = string.Empty;
    static string IPCERRORDESC = string.Empty;
    public static DataTable CreateSMSNotifyDetailTable(DataTable tblUserAccount, DataTable tblContractAccount, string Status, string contractNo, DataTable dtCurrentRoles = null)
    {
        try
        {
            DataTable tblSMSNotify = new DataTable();
            DataColumn colAccountNo = new DataColumn("colAccountNo");
            DataColumn colContractNo = new DataColumn("colContractNo");
            DataColumn colRoleIDsms = new DataColumn("colRoleIDsms");
            DataColumn colIFCCD = new DataColumn("colIFCCD");
            DataColumn colSMSInfor = new DataColumn("colSMSInfor");
            DataColumn colStt = new DataColumn("colStt");

            //add vào table
            tblSMSNotify.Columns.Add(colAccountNo);
            tblSMSNotify.Columns.Add(colContractNo);
            tblSMSNotify.Columns.Add(colRoleIDsms);
            tblSMSNotify.Columns.Add(colIFCCD);
            tblSMSNotify.Columns.Add(colSMSInfor);
            tblSMSNotify.Columns.Add(colStt);


            List<string> lsRole = new List<string>();
            #region get role sms
            DataTable dtsmsrole = (new SmartPortal.SEMS.Transactions().DoStored("SEMS_LOADROLE_BYTYPE", new object[1] { "SNO" }, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
            if (IPCERRORCODE.Equals("0"))
            {
                foreach (DataRow dr in dtsmsrole.Rows)
                {
                    if (!lsRole.Contains(dr["RoleID"].ToString()))
                    {
                        lsRole.Add(dr["RoleID"].ToString());
                    }
                }
            }
            else
            {
                throw new IPCException(IPCERRORDESC);
            }
            #endregion
            //tao json detail
            foreach (DataRow dr in tblUserAccount.Rows)
            {
                string cStatus = Status;
                if (Status.Equals("U") && dtCurrentRoles != null)
                {
                    if (lsRole.Contains(dr["colRoleIDUC"].ToString()))
                    {
                        if (dtCurrentRoles.Select("ACCTNO = '" + dr["colAcctNoUC"].ToString() + "' and ROLEID = '" + dr["colRoleIDUC"].ToString() + "'").Length > 0)
                        {
                            break; //da dang ky sms no voi account nay
                        }
                        else
                        {
                            cStatus = "I"; //truong hop them user voi role moi
                        }
                    }
                }
                if (tblSMSNotify.Select("colAccountNo = '" + dr["colAcctNoUC"].ToString() + "' and colRoleIDsms = '" + dr["colRoleIDUC"].ToString() + "'").Length == 0)
                {
                    if (lsRole.Contains(dr["colRoleIDUC"].ToString()))
                    {
                        DataRow drs = tblSMSNotify.NewRow();
                        string IFCCD = string.Empty;
                        string jsonInfor = string.Empty;
                        string ccyid = "LAK";
                        //fee
                        DataTable dtsmsfee = (new SmartPortal.SEMS.Transactions().DoStored("SEMS_EBA_SEARCH_SMS_FEE", new object[4] { dr["colRoleIDUC"].ToString(), "", "", "" }, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
                        if (IPCERRORCODE.Equals("0"))
                        {
                            IFCCD = dtsmsfee.Rows[0]["Feeid"].ToString();
                        }
                        else
                        {
                            throw new IPCException(IPCERRORDESC);
                        }
                        //get ccyid
                        DataRow[] drcc = tblContractAccount.Select("colAcctNo = '" + dr["colAcctNoUC"].ToString() + "'");
                        if (drcc.Length > 0)
                        {
                            ccyid = drcc[0]["colCCYID"].ToString();
                        }
                        //sms infor
                        Hashtable hsinfor = new Hashtable();
                        DataTable dtsmscf = (new SmartPortal.SEMS.Transactions().DoStored("SEMS_SMS_NOTIFY_GETDETAILFOROPENCONTRACT", new object[2] { dr["colRoleIDUC"].ToString(), ccyid }, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
                        if (IPCERRORCODE.Equals("0"))
                        {
                            foreach (DataRow drhs in dtsmscf.Rows)
                            {
                                if (!hsinfor.ContainsKey(drhs["TranCode"].ToString()))
                                {
                                    Hashtable hstmp = new Hashtable();
                                    hstmp.Add(drhs["PARANAME"].ToString(), drhs["PARAVALUE"].ToString());
                                    hsinfor.Add(drhs["TranCode"].ToString(), hstmp);
                                }
                                else
                                {
                                    ((Hashtable)hsinfor[drhs["TranCode"].ToString()]).Add(drhs["PARANAME"].ToString(), drhs["PARAVALUE"].ToString());
                                }
                            }

                            jsonInfor = JsonConvert.SerializeObject(hsinfor);
                        }
                        else
                        {
                            throw new IPCException(IPCERRORDESC);
                        }

                        drs["colAccountNo"] = dr["colAcctNoUC"].ToString();
                        drs["colContractNo"] = contractNo;
                        drs["colRoleIDsms"] = dr["colRoleIDUC"].ToString();
                        drs["colIFCCD"] = IFCCD;
                        drs["colSMSInfor"] = jsonInfor;
                        drs["colStt"] = cStatus;

                        tblSMSNotify.Rows.Add(drs);
                    }
                }
            }

            //truong hop delete role
            if (!Status.Equals("I"))
            {
                foreach (DataRow dracct in tblContractAccount.Rows)
                {
                    if (lsRole.Count > 0)
                    {
                        DataRow drd = tblSMSNotify.NewRow();
                        string slsSMSRoles = "(" + string.Join(",", lsRole.ToArray()) + ")";
                        int cuSNO = dtCurrentRoles.Select("AcctNo = '" + dracct["colAcctNo"].ToString() + "' and RoleID IN " + slsSMSRoles).Length;
                        int nuSNO = tblSMSNotify.Select("colAccountNo = '" + dracct["colAcctNo"].ToString() + "' and colRoleIDsms IN " + slsSMSRoles).Length;
                        bool RegistedOnCore = true; //them doan check log neu can

                        if (cuSNO == 0 && nuSNO == 0 && RegistedOnCore)
                        {
                            drd["colAccountNo"] = dracct["colAcctNo"].ToString();
                            drd["colContractNo"] = contractNo;
                            drd["colRoleIDsms"] = "";
                            drd["colIFCCD"] = "";
                            drd["colSMSInfor"] = "";
                            drd["colStt"] = "D";

                            tblSMSNotify.Rows.Add(drd);
                        }
                    }
                }

                DataSet dsCt = new SmartPortal.SEMS.Transactions().DoStored("SEMS_EBA_CONTRACTACCOUNT_GETACCTNO", new object[2] { "", contractNo }, ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE.Equals("0") & dsCt.Tables.Count > 0)
                {
                    foreach (DataRow dracct in dsCt.Tables[0].Rows)
                    {
                        if (lsRole.Count > 0)
                        {
                            DataRow drd = tblSMSNotify.NewRow();
                            string slsSMSRoles = "(" + string.Join(",", lsRole.ToArray()) + ")";
                            int cuSNO = dtCurrentRoles.Select("AcctNo = '" + dracct["AcctNo"].ToString() + "' and RoleID IN " + slsSMSRoles).Length;
                            int nuSNO = tblSMSNotify.Select("colAccountNo = '" + dracct["AcctNo"].ToString() + "' and colRoleIDsms IN " + slsSMSRoles).Length;
                            bool RegistedOnCore = true; //them doan check log neu can

                            if (cuSNO == 0 && nuSNO == 0 && RegistedOnCore)
                            {
                                drd["colAccountNo"] = dracct["AcctNo"].ToString();
                                drd["colContractNo"] = contractNo;
                                drd["colRoleIDsms"] = "";
                                drd["colIFCCD"] = "";
                                drd["colSMSInfor"] = "";
                                drd["colStt"] = "D";

                                tblSMSNotify.Rows.Add(drd);
                            }
                        }
                    }
                }
            }
            if (tblSMSNotify.Rows.Count > 0)
            {
                tblSMSNotify = tblSMSNotify.AsEnumerable().GroupBy(r => new
                {
                    colAccountNo = r.Field<string>("colAccountNo"),
                    colContractNo = r.Field<string>("colContractNo"),
                    colRoleIDsms = r.Field<string>("colRoleIDsms"),
                    colIFCCD = r.Field<string>("colIFCCD"),
                    colSMSInfor = r.Field<string>("colSMSInfor"),
                    colStt = r.Field<string>("colStt")
                }).Select(g => g.First()).CopyToDataTable();
                return tblSMSNotify;
            }
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //public static bool ValidateSMSNotifyCurrentUser(string productID, TreeView tvSMS, ref string errorDesc, string AcctNo, List<DataTable> lsRoleOtherUsers = null, string colAccountName = "colAccount", string colRoleIDName = "colRoleID")
    public static bool ValidateSMSNotifyCurrentUser(string productID, TreeView tvSMS, ref string errorDesc, string AcctNo, List<String> listAccNo, List<DataTable> lsRoleOtherUsers = null, string colAccountName = "colAccount", string colRoleIDName = "colRoleID")
    {
        try
        {
            DataTable tblSS = (new SmartPortal.SEMS.Role().GetRoleDefaultByServiceID("SMS", productID, decimal.Parse(new SmartPortal.SEMS.Contract().GetContractLevelID(ref IPCERRORCODE, ref IPCERRORDESC)), string.Empty, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
            DataTable dtsmsrole = (new SmartPortal.SEMS.Transactions().DoStored("SEMS_LOADROLE_BYTYPE", new object[1] { "SNO" }, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
            List<string> lsRole = new List<string>();
            List<string> lsRoleSelected = new List<string>();
            if (IPCERRORCODE.Equals("0"))
            {
                //lsRole = dtsmsrole.Rows.OfType<DataRow>().Select(dr => dr.Field<string>("RoleID").ToString()).ToList();
                //eo hieu sao ko dung duoc linq
                foreach (DataRow dr in dtsmsrole.Rows)
                {
                    if (!lsRole.Contains(dr["RoleID"].ToString()))
                    {
                        lsRole.Add(dr["RoleID"].ToString());
                    }
                }
            }
            else
            {
                throw new IPCException(IPCERRORDESC);
            }

            //check nhieu role sms nofity
            int countnodeac = 0;
            foreach (TreeNode tn in tvSMS.Nodes)
            {
                if (lsRole.Contains(tn.Value) && tn.Checked)
                {
                    lsRoleSelected.Add(tn.Value);
                    countnodeac++;
                }
            }
            if (countnodeac > 1)
            {
                errorDesc = Resources.labels.oneaccountcansetonesmsright;
                return false;
            }
            //check nhieu role user khac
            countnodeac = 0;
            if (lsRoleOtherUsers != null)
            {
                foreach (DataTable dt in lsRoleOtherUsers)
                {
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0 && lsRoleSelected.Count > 0)
                        {
                            DataTable dtclone = dt.Select().CopyToDataTable().DefaultView.ToTable(false, colAccountName, colRoleIDName);
                            dtclone = dtclone.AsEnumerable().GroupBy(r => new { Col1 = r[colAccountName], Col2 = r[colRoleIDName] })
                                            .Select(g => g.OrderBy(r => r[colAccountName]).First())
                                            .CopyToDataTable();
                            foreach (DataRow dr in dtclone.Rows)
                            {
                                if (lsRole.Contains(dr[colRoleIDName].ToString()) && (!lsRoleSelected.Contains(dr[colRoleIDName].ToString())) && (string.IsNullOrEmpty(AcctNo) ? true : dr[colAccountName].ToString().Equals(AcctNo)))
                                {

                                    DataRow[] drr = dtsmsrole.Select("RoleID='" + dr[colRoleIDName].ToString() + "'");
                                    errorDesc = Resources.labels.oneaccountcansetonesmsright + " .The correct right of account " + dr[colAccountName].ToString() + " must be " + drr[0]["RoleName"].ToString();
                                    return false;
                                }
                            }
                        }
                    }

                }
            }

            //check config
            foreach (string roleID in lsRoleSelected)
            {
                DataTable dtsmsconfig = (new SmartPortal.SEMS.Transactions().DoStored("SEMS_SMS_NOTIFY_VALIDATEROLECONFIG", new object[1] { roleID }, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
                if (IPCERRORCODE.Equals("0"))
                {
                    if (dtsmsconfig.Rows.Count != 0)
                    {
                        errorDesc = Resources.labels.nosmsconfig;
                        return false;
                    }
                }
                else
                {
                    throw new IPCException(IPCERRORDESC);
                }
            }

            //check fee
            foreach (string roleID in lsRoleSelected)
            {
                DataTable dtsmsconfig = (new SmartPortal.SEMS.Transactions().DoStored("SEMS_EBA_SEARCH_SMS_FEE", new object[4] { roleID, "", "", "" }, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
                if (IPCERRORCODE.Equals("0"))
                {
                    if (dtsmsconfig.Rows.Count == 0)
                    {
                        errorDesc = Resources.labels.nofeeconfig;
                        return false;
                    }
                }
                else
                {
                    throw new IPCException(IPCERRORDESC);
                }
            }
            //check owned fee
            if (lsRoleSelected.Count > 0)
                if (!string.IsNullOrEmpty(AcctNo))
                {
                    Hashtable dtCheckOwedFee = new SmartPortal.SEMS.Fee().CheckSmsOwnedFee(AcctNo, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (!IPCERRORCODE.Equals("0"))
                    {
                        errorDesc = IPCERRORDESC;
                        return false;
                    }
                }
                else
                {
                    foreach (string accNo in listAccNo)
                    {
                        Hashtable dtCheckOwedFee = new SmartPortal.SEMS.Fee().CheckSmsOwnedFee(accNo, ref IPCERRORCODE, ref IPCERRORDESC);
                        if (!IPCERRORCODE.Equals("0"))
                        {
                            errorDesc = IPCERRORDESC;
                            return false;
                        }
                    }
                }

            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}