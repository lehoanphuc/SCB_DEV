using System;

using SmartPortal.Constant;
using System.Data;
using System.Data.SqlClient;
using SmartPortal.Model;
using SmartPortal.DAL;
using SmartPortal.ExceptionCollection;

namespace SmartPortal.BLL
{
    public class PagesBLL
    {
        #region LanNTH - 13082018 - update page
        public PagesModel GetPageInfo(string pageID)
        {
            try
            {
                PagesModel PM = new PagesModel();
                IDataReader iRead;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@pageid";
                p1.Value = pageID;
                p1.SqlDbType = SqlDbType.VarChar;

                iRead = DataAccess.GetFromReader("Pages_GetPageInfo", p1);
                while (iRead.Read())    
                { 
                    PM.PageID = iRead["PageID"].ToString();
                    PM.PageName = iRead["PageName"].ToString();
                    PM.MasterPage = iRead["MasterPagePath"].ToString(); 
                    PM.Theme = iRead["FileName"].ToString();
                    PM.Description = iRead["PageDescription"].ToString();
                    PM.Title = iRead["PageTitle"].ToString();   
                    PM.IsShow = bool.Parse(iRead["IsShow"].ToString()); 
                    PM.IsDefault = (iRead["IsDefault"].ToString()); 
                    PM.IsApprove = (iRead["IsApprove"].ToString());
                    PM.IsNotification = (iRead["IsNotification"].ToString());
                    PM.IsSchedule = (iRead["IsSchedule"].ToString());
                    PM.IsTemplate = (iRead["IsTemplate"].ToString());
                    PM.IsReceive = (iRead["IsReceive"].ToString());
                    PM.IsProductFee = (iRead["IsProductFee"].ToString());  
                    PM.IsReport = (iRead["IsReport"].ToString());
                    PM.IsViewReport = (iRead["IsViewReport"].ToString());  
                    PM.Isbeneficiary = (iRead["IsBeneficiary"].ToString());
                    PM.IsReversal = (iRead["IsReversal"].ToString());
                    PM.Trancode = iRead["TranCode"].ToString(); 
                    PM.LinkApprove = iRead["LinkApprove"].ToString();
                    PM.UserModified = iRead["UserModified"].ToString();
                    PM.MasterPageID = int.Parse(iRead["MasterPageID"].ToString());
                    PM.ThemeID = int.Parse(iRead["ThemeID"].ToString());
                    PM.SubSystem = int.Parse(iRead["SubSystemID"].ToString());
                    PM.ServiceID = iRead["ServiceID"].ToString();
                    PM.UserModified = iRead["UserModified"].ToString();
                    PM.DateModified = iRead["DateModified"].ToString();
                    PM.Action = iRead["Action"].ToString();
                    PM.PageReference = iRead["Reference"].ToString();

                }
                iRead.Close();
                return PM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //insert
        public int Insert(string pageId, string pageName, string pageDesc, string serviceID, int themeID
            , string pageTitle,Boolean isShow, string isDefault, string isApprove, string isNotification, string isSchedule, string isTemplate
            , string isReceive, string isProductFee, string isReport, string isViewReport, string isbeneficiary, string isReversal,string  trancode, string linkApprove, 
            string author, int masterpageID, int subsystemid, string reference, string action) 
        {  
            try 
            { 
                int strErr = 0;
                SqlParameter p0 = new SqlParameter();
                p0.ParameterName = "@pageid";  
                p0.Value = pageId; 
                p0.SqlDbType = SqlDbType.VarChar;

                SqlParameter p1 = new SqlParameter(); 
                p1.ParameterName = "@pagename";
                p1.Value = pageName;
                p1.SqlDbType = SqlDbType.NVarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@pagedesc";
                p2.Value = pageDesc;
                p2.SqlDbType = SqlDbType.NVarChar;

                SqlParameter p3 = new SqlParameter();
                p3.ParameterName = "@serviceID";
                p3.Value = serviceID;
                p3.SqlDbType = SqlDbType.VarChar;

                SqlParameter p4 = new SqlParameter();
                p4.ParameterName = "@theme";
                p4.Value = themeID;
                p4.SqlDbType = SqlDbType.Int;

                SqlParameter p5 = new SqlParameter();
                p5.ParameterName = "@pagetitle";
                p5.Value = pageTitle;
                p5.SqlDbType = SqlDbType.NVarChar;

                SqlParameter p6 = new SqlParameter();
                p6.ParameterName = "@isshow"; 
                p6.Value = isShow; 
                p6.SqlDbType = SqlDbType.Bit;

                SqlParameter p61 = new SqlParameter();
                p61.ParameterName = "@isDefault";
                p61.Value = isDefault;
                p61.SqlDbType = SqlDbType.VarChar;

                SqlParameter p62 = new SqlParameter();
                p62.ParameterName = "@isApprove";
                p62.Value = isApprove;
                p62.SqlDbType = SqlDbType.VarChar;

                SqlParameter p63 = new SqlParameter(); 
                p63.ParameterName = "@isNotification";
                p63.Value = isNotification;
                p63.SqlDbType = SqlDbType.VarChar;

                SqlParameter p64 = new SqlParameter();
                p64.ParameterName = "@isSchedule";
                p64.Value = isSchedule;
                p64.SqlDbType = SqlDbType.VarChar; 

                SqlParameter p65 = new SqlParameter();
                p65.ParameterName = "@isTemplate";
                p65.Value = isTemplate;
                p65.SqlDbType = SqlDbType.VarChar;

                SqlParameter p66 = new SqlParameter();
                p66.ParameterName = "@isReceive";
                p66.Value = isReceive;
                p66.SqlDbType = SqlDbType.VarChar;

                SqlParameter p67 = new SqlParameter();
                p67.ParameterName = "@isProductFee";
                p67.Value = isProductFee;
                p67.SqlDbType = SqlDbType.VarChar;

                SqlParameter p68 = new SqlParameter();
                p68.ParameterName = "@isReport";
                p68.Value = isReport;
                p68.SqlDbType = SqlDbType.VarChar;

                SqlParameter p69 = new SqlParameter();
                p69.ParameterName = "@isViewReport";
                p69.Value = isViewReport; 
                p69.SqlDbType = SqlDbType.VarChar;

                SqlParameter p6a = new SqlParameter(); 
                p6a.ParameterName = "@isbeneficiary";
                p6a.Value = isbeneficiary;
                p6a.SqlDbType = SqlDbType.VarChar; 

                SqlParameter p6b = new SqlParameter();
                p6b.ParameterName = "@isReversal";
                p6b.Value = isReversal; 
                p6b.SqlDbType = SqlDbType.VarChar; 

                SqlParameter p6c = new SqlParameter();
                p6c.ParameterName = "@trancode";
                p6c.Value = trancode; 
                p6c.SqlDbType = SqlDbType.VarChar;
                SqlParameter p6d = new SqlParameter();
                p6d.ParameterName = "@linkApprove";
                p6d.Value = linkApprove;
                p6d.SqlDbType = SqlDbType.VarChar;  
                //  string  trancode, string linkApprove,

                SqlParameter p7 = new SqlParameter();
                p7.ParameterName = "@author";
                p7.Value = author;
                p7.SqlDbType = SqlDbType.Text;

                SqlParameter p8 = new SqlParameter();
                p8.ParameterName = "@masterpageid";
                p8.Value = masterpageID;
                p8.SqlDbType = SqlDbType.Int;

                SqlParameter p9 = new SqlParameter();
                p9.ParameterName = "@subsystemid";
                p9.Value = subsystemid;
                p9.SqlDbType = SqlDbType.Int;


                SqlParameter p10 = new SqlParameter();
                p10.ParameterName = "@reference";
                p10.Value = reference; 
                p10.SqlDbType = SqlDbType.VarChar;

                SqlParameter p11 = new SqlParameter();
                p11.ParameterName = "@action"; 
                p11.Value = action;
                p11.SqlDbType = SqlDbType.VarChar; 
                 
                strErr = DataAccess.Execute("Pages_Insert"
                    , p0, p1, p2, p3, p4, p5, p6, p61, p62, p63, p64, p65, p66, p67, p68, p69, p6a, p6b, p6c, p6d, p7, p8, p9, p10, p11);
                if (strErr == 0)
                {
                    throw new BusinessExeption("Unable Insert Page");
                }
                else
                {
                    return strErr;
                }
            }
            catch (BusinessExeption bex)
            {
                throw bex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //update
        public int Update( DateTime dateModified, string pageName, string pageDesc
            , string serviceID, int themeID, string pageTitle 
            , Boolean isShow, string isDefault, string isApprove, string isNotification, string isSchedule
            , string isTemplate , string isReceive, string isProductFee, string isReport, string isViewReport
            , string isbeneficiary, string isReversal, string trancode, string linkApprove,
            string userModified, int masterpageID, int subsystemid, string reference, string action, string PageID)
        {
            try  
            { 
                int strErr = 0;
                 
                SqlParameter p0 = new SqlParameter();
                p0.ParameterName = "@datemodified";
                p0.Value = dateModified;
                p0.SqlDbType = SqlDbType.DateTime;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@pagename";
                p1.Value = pageName;
                p1.SqlDbType = SqlDbType.NVarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@pagedesc";
                p2.Value = pageDesc;
                p2.SqlDbType = SqlDbType.NVarChar;
                 
                SqlParameter p3 = new SqlParameter(); 
                p3.ParameterName = "@serviceID";
                p3.Value = serviceID;
                p3.SqlDbType = SqlDbType.VarChar; 

                SqlParameter p4 = new SqlParameter();
                p4.ParameterName = "@theme";
                p4.Value = themeID;
                p4.SqlDbType = SqlDbType.Int;

                SqlParameter p5 = new SqlParameter();
                p5.ParameterName = "@pagetitle";
                p5.Value = pageTitle;
                p5.SqlDbType = SqlDbType.NVarChar;


                SqlParameter p6 = new SqlParameter();
                p6.ParameterName = "@isshow";
                p6.Value = isShow;
                p6.SqlDbType = SqlDbType.Bit;

                SqlParameter p61 = new SqlParameter();
                p61.ParameterName = "@isDefault";
                p61.Value = isDefault;
                p61.SqlDbType = SqlDbType.VarChar;

                SqlParameter p62 = new SqlParameter();
                p62.ParameterName = "@isApprove";
                p62.Value = isApprove;
                p62.SqlDbType = SqlDbType.VarChar;

                SqlParameter p63 = new SqlParameter();
                p63.ParameterName = "@isNotification";
                p63.Value = isNotification;
                p63.SqlDbType = SqlDbType.VarChar;

                SqlParameter p64 = new SqlParameter();
                p64.ParameterName = "@isSchedule";
                p64.Value = isSchedule;
                p64.SqlDbType = SqlDbType.VarChar;

                SqlParameter p65 = new SqlParameter();
                p65.ParameterName = "@isTemplate";
                p65.Value = isTemplate;
                p65.SqlDbType = SqlDbType.VarChar;

                SqlParameter p66 = new SqlParameter();
                p66.ParameterName = "@isReceive";
                p66.Value = isReceive;
                p66.SqlDbType = SqlDbType.VarChar;

                SqlParameter p67 = new SqlParameter();
                p67.ParameterName = "@isProductFee";
                p67.Value = isProductFee;
                p67.SqlDbType = SqlDbType.VarChar;

                SqlParameter p68 = new SqlParameter();
                p68.ParameterName = "@isReport";
                p68.Value = isReport;
                p68.SqlDbType = SqlDbType.VarChar;

                SqlParameter p69 = new SqlParameter();
                p69.ParameterName = "@isViewReport";
                p69.Value = isViewReport;
                p69.SqlDbType = SqlDbType.VarChar;

                SqlParameter p6a = new SqlParameter();
                p6a.ParameterName = "@isbeneficiary";
                p6a.Value = isbeneficiary;
                p6a.SqlDbType = SqlDbType.VarChar;

                SqlParameter p6b = new SqlParameter();
                p6b.ParameterName = "@isReversal";
                p6b.Value = isReversal;
                p6b.SqlDbType = SqlDbType.VarChar;

                SqlParameter p6c = new SqlParameter();
                p6c.ParameterName = "@trancode";
                p6c.Value = trancode;
                p6c.SqlDbType = SqlDbType.VarChar;
                SqlParameter p6d = new SqlParameter();
                p6d.ParameterName = "@linkApprove";
                p6d.Value = linkApprove;
                p6d.SqlDbType = SqlDbType.VarChar;


                SqlParameter p7 = new SqlParameter();
                p7.ParameterName = "@usermodified";
                p7.Value = userModified;
                p7.SqlDbType = SqlDbType.Text;

                SqlParameter p8 = new SqlParameter();
                p8.ParameterName = "@masterpageid";
                p8.Value = masterpageID;
                p8.SqlDbType = SqlDbType.Int;

                SqlParameter p9 = new SqlParameter();
                p9.ParameterName = "@subsystemid";
                p9.Value = subsystemid;
                p9.SqlDbType = SqlDbType.Int;

                SqlParameter p10 = new SqlParameter();
                p10.ParameterName = "@reference";
                p10.Value = reference;
                p10.SqlDbType = SqlDbType.VarChar;

                SqlParameter p11 = new SqlParameter();
                p11.ParameterName = "@action";
                p11.Value = action;
                p11.SqlDbType = SqlDbType.VarChar;

                SqlParameter p = new SqlParameter();
                p.ParameterName = "@pageid";
                p.Value = PageID;
                p.SqlDbType = SqlDbType.VarChar; 
                 
                strErr = DataAccess.Execute("Pages_Update", p0, p1, p2, p3, p4, p5, p6, p61, p62, p63, p64, p65, p66, p67, p68, p69, p6a, p6b, p6c, p6d, p7, p8, p9, p10, p11, p);
                if (strErr == 0) 
                {
                    throw new BusinessExeption("Unable Update Pages");
                }
                else
                {
                    return strErr;
                }
            }
            catch (BusinessExeption bex)
            {
                throw bex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //get all page reference
        public DataTable GetAllPageReference(string pageid)
        {
            DataTable dt = new DataTable();
            try
            {

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@pageid";
                p1.Value = pageid;
                p1.SqlDbType = SqlDbType.VarChar;
                

                dt = DataAccess.GetFromDataTable("SMP_AllPageReference", p1);
            }
            catch (Exception ex)
            {
                //throw ex;
            }
            return dt;
        }
        //get reference page list
        public DataTable GetPageReference()
        {
            try
            {
                return DataAccess.GetFromDataTable("SMP_PageReference");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //get action page list
        public DataTable GetAction_Page(string serviceid)
        {
            try
            {
                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@serviceid";
                p1.Value = serviceid;
                p1.SqlDbType = SqlDbType.VarChar;

                return DataAccess.GetFromDataTable("SMP_ActionLoad", p1);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        // get link action 
        public static string GetLinkAction_Page(string referencepage, string action)
        {
            string link = "";
            try
            {
                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@reference";
                p1.Value = referencepage;
                p1.SqlDbType = SqlDbType.VarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@action";
                p2.Value = action;
                p2.SqlDbType = SqlDbType.VarChar;

                DataTable dt = DataAccess.GetFromDataTable("SMP_PageByAction", p1, p2);
                link = "/default.aspx?p=" + dt.Rows[0]["PageID"] + "&a=" + action;

            }
            catch (Exception ex)
            {
                //throw ex;
            }
            return link;
        }

        //get link list (use when go back)
        public static string GetLinkMaster_Page(string referencepage)
        {
            string link = "";
            try
            {
                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@reference";
                p1.Value = referencepage;
                p1.SqlDbType = SqlDbType.VarChar;

                DataTable dt = DataAccess.GetFromDataTable("SMP_PageByReference", p1);
                if(dt.Rows.Count > 0)
                {
                    link = "/default.aspx?p=" + dt.Rows[0]["PageID"];
                }
                else
                {
                    link = "/default.aspx?p=129";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return link;
        }
        public static string GetActionPage(string pageid)
        {
            string action = IPC.ACTIONPAGE.NOACTION;
            try
            {
                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@pageid";
                p1.Value = pageid;
                p1.SqlDbType = SqlDbType.VarChar;

                DataTable dt = DataAccess.GetFromDataTable("SMP_ActionByPage", p1);
                action = dt.Rows[0]["Action"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return action;
        }
        //delete
        public int Delete(string pageID)
        {
            try
            {
                int strErr = 0;

                SqlParameter p = new SqlParameter();
                p.ParameterName = "@pageid";
                p.Value = pageID;
                p.SqlDbType = SqlDbType.VarChar;

                strErr = DataAccess.Execute("Pages_Delete", p);
                if (strErr == 0)
                {
                    throw new BusinessExeption("Unable Delete Pages");
                }
                else
                {
                    return strErr;
                }
            }
            catch (BusinessExeption bex)
            {
                throw bex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        //search
        public DataTable Search(string keyword)
        {
            try
            {
                SqlParameter p = new SqlParameter();
                p.ParameterName = "@keyword";
                p.Value = keyword;
                p.SqlDbType = SqlDbType.NVarChar;

                return DataAccess.GetFromDataTable("Pages_Search", p);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //load all info
        public DataTable Load()
        {
            try
            {
                return DataAccess.GetFromDataTable("Pages_Load", null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //check page permit
        public bool CheckPermit(string pageID, string userName, string serviceID)
        {
            try
            {
                DataTable tblPermit = new DataTable();

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@pageid";
                p1.Value = pageID;
                p1.SqlDbType = SqlDbType.VarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@userName";
                p2.Value = userName;
                p2.SqlDbType = SqlDbType.VarChar;

                SqlParameter p3 = new SqlParameter();
                p3.ParameterName = "@serviceID";
                p3.Value = serviceID;
                p3.SqlDbType = SqlDbType.VarChar;

                tblPermit = DataAccess.GetFromDataTable("Pages_CheckPermit", p1, p2, p3);

                if (tblPermit.Rows.Count != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool ChekcPermitPageAction(string pageMain, string userName, string action)
        {
            {
                try
                {
                    DataTable tblPermit = new DataTable();

                    SqlParameter p1 = new SqlParameter();
                    p1.ParameterName = "@pagemain";
                    p1.Value = pageMain;
                    p1.SqlDbType = SqlDbType.VarChar;

                    SqlParameter p2 = new SqlParameter();
                    p2.ParameterName = "@userName";
                    p2.Value = userName;
                    p2.SqlDbType = SqlDbType.VarChar;

                    SqlParameter p3 = new SqlParameter();
                    p3.ParameterName = "@action";
                    p3.Value = action;
                    p3.SqlDbType = SqlDbType.VarChar;

                    tblPermit = DataAccess.GetFromDataTable("Pages_CheckPermitPageAction", p1, p2, p3);

                    if (tblPermit.Rows.Count != 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        //get Page By SubSystemID
        public DataTable GetPageBySS(int SSID, string serviceID, string ROLEID)
        {
            try
            {
                SqlParameter p = new SqlParameter();
                p.ParameterName = "@ssid";
                p.Value = SSID;
                p.SqlDbType = SqlDbType.Int;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@serviceID";
                p1.Value = serviceID;
                p1.SqlDbType = SqlDbType.VarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@ROLEID";
                p2.Value = ROLEID;
                p2.SqlDbType = SqlDbType.VarChar;

                return DataAccess.GetFromDataTable("Pages_GetPageBySS", p, p1, p2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetPageReference(int SSID, string serviceID, string pagid, string ROLEID)
        {
            try
            {
                SqlParameter p = new SqlParameter();
                p.ParameterName = "@ssid";
                p.Value = SSID;
                p.SqlDbType = SqlDbType.Int;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@serviceID";
                p1.Value = serviceID;
                p1.SqlDbType = SqlDbType.VarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@pageID";
                p2.Value = pagid;
                p2.SqlDbType = SqlDbType.VarChar;

                SqlParameter p3 = new SqlParameter();
                p3.ParameterName = "@ROLEID";
                p3.Value = ROLEID;
                p3.SqlDbType = SqlDbType.VarChar;

                return DataAccess.GetFromDataTable("Pages_GetPageReference", p, p1, p2,p3);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //get Page By SubSystemID
        public DataTable GetPageByContract(int SSID, string contractNo, string serviceID)
        {
            try
            {
                SqlParameter p = new SqlParameter();
                p.ParameterName = "@ssid";
                p.Value = SSID;
                p.SqlDbType = SqlDbType.Int;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@contractno";
                p1.Value = contractNo;
                p1.SqlDbType = SqlDbType.VarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@serviceID";
                p2.Value = serviceID;
                p2.SqlDbType = SqlDbType.VarChar;

                return DataAccess.GetFromDataTable("Pages_GetPageByContract", p, p1, p2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //hunglt 04/07/2020 AddNew Get serviceid by pageid
        public static string GetServiceidByPageId(string pageId)
        {
            string serviceid = "";
            try
            {
                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@pageid";
                p1.Value = pageId;
                p1.SqlDbType = SqlDbType.VarChar;
                serviceid = DataAccess.GetFromDataTable("Pages_GetPageInfo", p1).Rows[0]["ServiceID"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return serviceid;

        }

        public DataTable LoadService()
        {
            try
            {
                return DataAccess.GetFromDataTable("SmartPortal_Load", null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
