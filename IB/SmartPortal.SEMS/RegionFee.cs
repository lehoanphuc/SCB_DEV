using SmartPortal.DAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SmartPortal.SEMS
{
   public  class RegionFee
    {
       #region Insert Region fee
       public RegionFee()
       { }
       public DataSet InsertRegionFee(string regionName, string description,string usercreate,string status, ref string errorCode, ref string errorDesc)
       {
           try
           {
               //Show hastable result from IPC
               Hashtable hasInput = new Hashtable();
               Hashtable hasOutput = new Hashtable();

               //add key into input
               hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSREGIONFEEINSERT");
               hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
               hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
               hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "ADD NEW REGION");

               
               hasInput.Add(SmartPortal.Constant.IPC.REGIONNAME, regionName);
               hasInput.Add(SmartPortal.Constant.IPC.DESCRIPTION, description);
               hasInput.Add(SmartPortal.Constant.IPC.USERCREATE, usercreate);
               hasInput.Add(SmartPortal.Constant.IPC.STATUS, status);
               

               hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

               DataSet ds = new DataSet();

               if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
               {
                   ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATASET];
                   errorCode = "0";
               }
               else
               {
                   errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                   errorDesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
               }

               return ds;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       #endregion
       public DataTable CheckRegionname(string regionid,string regionname)
       {
           try
           {
               DataTable RegionExist = new DataTable();

               SqlParameter p1 = new SqlParameter();
               p1.ParameterName = "@regionid";
               p1.Value = regionid;
               p1.SqlDbType = SqlDbType.Text;

               SqlParameter p2 = new SqlParameter();
               p2.ParameterName = "@regionname";
               p2.Value = regionname;
               p2.SqlDbType = SqlDbType.Text;


               RegionExist = DataAccess.GetFromDataTable("EBA_Region_check_exist", p1,p2);


               return RegionExist;
           }
           catch
           {
               return null;
           }
       }
       public DataTable CheckRegionID(string regionID)
       {
           try
           {
               DataTable RegionExist = new DataTable();

               SqlParameter p1 = new SqlParameter();
               p1.ParameterName = "@regionID";
               p1.Value = regionID;
               p1.SqlDbType = SqlDbType.Text;


               RegionExist = DataAccess.GetFromDataTable("EBA_Region_check_id_exist", p1);


               return RegionExist;
           }
           catch
           {
               return null;
           }
       }

       #region Search region fee by condition
       public DataSet SearchRegionFeeByCondition(string regionid, string regionname, string description, ref string errorCode, ref string errorDesc)
       {
           try
           {
               //Show hastable result from IPC
               Hashtable hasInput = new Hashtable();
               Hashtable hasOutput = new Hashtable();

               //add key into input
               hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSREGIONFEESEARCH");
               hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
               hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
               hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "lấy thông tin chi tiết branch by condition");

               hasInput.Add(SmartPortal.Constant.IPC.REGIONID, regionid);
               hasInput.Add(SmartPortal.Constant.IPC.REGIONNAME, regionname);
               hasInput.Add(SmartPortal.Constant.IPC.DESCRIPTION, description);
               

               hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

               DataSet ds = new DataSet();

               if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
               {
                   ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATASET];
                   errorCode = "0";
               }
               else
               {
                   errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                   errorDesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
               }

               return ds;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       #endregion

       public DataTable showbranchbyregionfeeid(string regionid)
       {
           try
           {
               DataTable RegionExist = new DataTable();

               SqlParameter p1 = new SqlParameter();
               p1.ParameterName = "@regionid";
               p1.Value = regionid;
               p1.SqlDbType = SqlDbType.Text;


               RegionExist = DataAccess.GetFromDataTable("EBA_show_branch_by_regionfeeid", p1);


               return RegionExist;
           }
           catch
           {
               return null;
           }
       }
       #region DELETE region fee
       public DataSet DeleteRegionFee(string regionid,string userid,string status, ref string errorCode, ref string errorDesc)
       {
           try
           {
               //Show hastable result from IPC
               Hashtable hasInput = new Hashtable();
               Hashtable hasOutput = new Hashtable();

               //add key into input
               hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSREGIONFEEDELETE");
               hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
               hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
               hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Xóa sản phẩm");
               hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");
               hasInput.Add(SmartPortal.Constant.IPC.REGIONID, regionid);
               hasInput.Add(SmartPortal.Constant.IPC.USERID, userid);
               hasInput.Add(SmartPortal.Constant.IPC.STATUS, status);



               hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

               DataSet ds = new DataSet();

               if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
               {
                   ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATASET];
                   errorCode = "0";
               }
               else
               {
                   errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                   errorDesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
               }

               return ds;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       #endregion

       #region Update region
       public DataSet Updateregionfee(string regionid, string regionname, string description,string usercreate,string status, ref string errorCode, ref string errorDesc)
       {
           try
           {
               //Show hastable result from IPC
               Hashtable hasInput = new Hashtable();
               Hashtable hasOutput = new Hashtable();

               //add key into input
               hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSREGIONFEEUPDATE");
               hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
               hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
               hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "lấy thông tin chi tiết region by condition");

               hasInput.Add(SmartPortal.Constant.IPC.REGIONID, regionid);
               hasInput.Add(SmartPortal.Constant.IPC.REGIONNAME, regionname);
               hasInput.Add(SmartPortal.Constant.IPC.DESCRIPTION, description);
               hasInput.Add(SmartPortal.Constant.IPC.USERCREATE, usercreate);
               hasInput.Add(SmartPortal.Constant.IPC.STATUS, status);
              

               hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

               DataSet ds = new DataSet();

               if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
               {
                   ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATASET];
                   errorCode = "0";
               }
               else
               {
                   errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                   errorDesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
               }

               return ds;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       #endregion
                #region check for delete regionfee
               public DataTable checkfordeleteRegionFee(string regionid)
               {
                   try
                   {
                       DataTable RegionExist = new DataTable();

                       SqlParameter p1 = new SqlParameter();
                       p1.ParameterName = "@regionid";
                       p1.Value = regionid;
                       p1.SqlDbType = SqlDbType.Text;


                       RegionExist = DataAccess.GetFromDataTable("EBA_checkfordeleteregionfee", p1);


                       return RegionExist;
                   }
                   catch
                   {
                       return null;
                   }
               }
                #endregion
    }
}
