using SmartPortal.DAL;
using SmartPortal.Model;
using SmartPortal.RemotingServices;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace SmartPortal.SEMS
{
	public class USERPOLICY
	{
		public DataSet PolicyInsert(UserPolicyModel policy, string usercreate, ref string errorCode, ref string errorDesc)
		{
			DataSet result;
			try
			{
				Hashtable hasInput = new Hashtable();
				Hashtable hasOutput = new Hashtable();
				hasInput.Add("IPCTRANCODE", "SEMSPOLICYINSERT");
				hasInput.Add("SOURCEID", "SEMS");
				hasInput.Add("SOURCETRANREF", "010");
				hasInput.Add("TRANDESC", "Tạo policy mới");
				hasInput.Add("SERVICEID", policy.serviceID);
				hasInput.Add("POLICYDESC", policy.descr);
				hasInput.Add("ISDEFAULT", policy.isdefault);
				hasInput.Add("EFFROM", policy.effrom);
				hasInput.Add("EFFTO", policy.efto);
				hasInput.Add("PWDHIS", policy.pwdhis);
				hasInput.Add("PWDAGEMAX", policy.pwdagemax);
				hasInput.Add("MINPWDLEN", policy.minpwdlen);
				hasInput.Add("PWDCPLX", policy.pwdcplx);
				hasInput.Add("PWDCPLXLC", policy.pwdcplxlc);
				hasInput.Add("PWDCPLXUC", policy.pwdcplxuc);
				hasInput.Add("PWDCPLXSC", policy.pwdcplxsc);
				hasInput.Add("PWDCPLXNC", policy.pwdcplxsn);
				hasInput.Add("TIMELGINRQ", policy.timelginrequire);
				hasInput.Add("TIMELGINFR", policy.lginfr);
				hasInput.Add("TIMELGINTO", policy.lginto);
				hasInput.Add("LKOUTTHRS", policy.lkoutthrs);
				hasInput.Add("RESETLKOUT", policy.resetlkout);
				hasInput.Add("USERCREATE", usercreate);
				hasOutput = AT.DBTRAN().ProcessTransHAS(hasInput);
				if (hasOutput["IPCERRORCODE"].ToString() == "0")
				{
					errorCode = "0";
				}
				else
				{
					errorCode = hasOutput["IPCERRORCODE"].ToString();
					errorDesc = hasOutput["IPCERRORDESC"].ToString();
				}
				result = null;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}

		public DataTable Checkpolicyname(string policyid, string policyname)
		{
			DataTable result;
			try
			{
				DataTable RegionExist = new DataTable();
				RegionExist = DataAccess.GetFromDataTable("EBA_policy_check_name_exist", new SqlParameter[]
				{
					new SqlParameter
					{
						ParameterName = "@policyid",
						Value = policyid,
						SqlDbType = SqlDbType.Text
					},
					new SqlParameter
					{
						ParameterName = "@policyname",
						Value = policyname,
						SqlDbType = SqlDbType.Text
					}
				});
				result = RegionExist;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		public DataTable Checkpolicyservice(string policyid, string serviceid)
		{
			DataTable result;
			try
			{
				DataTable RegionExist = new DataTable();
				RegionExist = DataAccess.GetFromDataTable("EBA_policy_check_service_exist", new SqlParameter[]
				{
					new SqlParameter
					{
						ParameterName = "@policyid",
						Value = policyid,
						SqlDbType = SqlDbType.Text
					},
					new SqlParameter
					{
						ParameterName = "@serviceid",
						Value = serviceid,
						SqlDbType = SqlDbType.Text
					}
				});
				result = RegionExist;
			}
			catch
			{
				result = null;
			}
			return result;
		}

        public DataSet GetPolicybyCondition(string policyid, string desc, string fromdate, string todate, string serviceid, string userid, ref string errorCode, ref string errorDesc)
        {
            return GetPolicybyCondition( policyid,  desc,  fromdate,  todate,  serviceid,  userid, 0, 0, ref errorCode, ref errorDesc);
        }

		public DataSet GetPolicybyCondition(string policyid, string desc, string fromdate, string todate, string serviceid, string userid, int recPerPage, int recIndex, ref string errorCode, ref string errorDesc)
		{
			DataSet result;
			try
			{
				Hashtable hasInput = new Hashtable();
				Hashtable hasOutput = new Hashtable();
				hasInput.Add("IPCTRANCODE", "SEMSPOLICYSEARCH");
				hasInput.Add("SOURCEID", "SEMS");
				hasInput.Add("SOURCETRANREF", "010");
				hasInput.Add("TRANDESC", "Tìm kiếm sản phẩm Ebanking");
				hasInput.Add("REVERSAL", "N");
				hasInput.Add("POLICYID", policyid);
				hasInput.Add("DESC", desc);
				hasInput.Add("FROMDATE", fromdate);
				hasInput.Add("TODATE", todate);
				hasInput.Add("SERVICEID", serviceid);
				hasInput.Add("USERID", userid);
                hasInput.Add("RECPERPAGE", recPerPage);
                hasInput.Add("RECINDEX", recIndex);
				hasOutput = AT.DBTRAN().ProcessTransHAS(hasInput);
				DataSet ds = new DataSet();
				if (hasOutput["IPCERRORCODE"].ToString() == "0")
				{
					ds = (DataSet)hasOutput["DATASET"];
					errorCode = "0";
				}
				else
				{
					errorCode = hasOutput["IPCERRORCODE"].ToString();
					errorDesc = hasOutput["IPCERRORDESC"].ToString();
				}
				result = ds;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}

		public DataSet GetPolicybyConditionforreset(string policyid, string desc, string fromdate, string todate, string serviceid, string userid, ref string errorCode, ref string errorDesc)
		{
			DataSet result;
			try
			{
				Hashtable hasInput = new Hashtable();
				Hashtable hasOutput = new Hashtable();
				hasInput.Add("IPCTRANCODE", "SEMSPOLICYSEARCHRS");
				hasInput.Add("SOURCEID", "SEMS");
				hasInput.Add("SOURCETRANREF", "010");
				hasInput.Add("TRANDESC", "Tìm kiếm sản phẩm Ebanking");
				hasInput.Add("REVERSAL", "N");
				hasInput.Add("POLICYID", policyid);
				hasInput.Add("DESC", desc);
				hasInput.Add("FROMDATE", fromdate);
				hasInput.Add("TODATE", todate);
				hasInput.Add("SERVICEID", serviceid);
				hasInput.Add("USERID", userid);
				hasOutput = AT.DBTRAN().ProcessTransHAS(hasInput);
				DataSet ds = new DataSet();
				if (hasOutput["IPCERRORCODE"].ToString() == "0")
				{
					ds = (DataSet)hasOutput["DATASET"];
					errorCode = "0";
				}
				else
				{
					errorCode = hasOutput["IPCERRORCODE"].ToString();
					errorDesc = hasOutput["IPCERRORDESC"].ToString();
				}
				result = ds;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}

		public DataSet PolicyUpdate(UserPolicyModel policy, string usercreate, ref string errorCode, ref string errorDesc)
		{
			DataSet result;
			try
			{
				Hashtable hasInput = new Hashtable();
				Hashtable hasOutput = new Hashtable();
				hasInput.Add("IPCTRANCODE", "SEMSPOLICYUPDATE");
				hasInput.Add("SOURCEID", "SEMS");
				hasInput.Add("SOURCETRANREF", "010");
				hasInput.Add("TRANDESC", "update policy");
				hasInput.Add("POLICYID", policy.policyid);
				hasInput.Add("SERVICEID", policy.serviceID);
				hasInput.Add("POLICYDESC", policy.descr);
				hasInput.Add("ISDEFAULT", policy.isdefault);
				hasInput.Add("EFFROM", policy.effrom);
				hasInput.Add("EFFTO", policy.efto);
				hasInput.Add("PWDHIS", policy.pwdhis);
				hasInput.Add("PWDAGEMAX", policy.pwdagemax);
				hasInput.Add("MINPWDLEN", policy.minpwdlen);
				hasInput.Add("PWDCPLX", policy.pwdcplx);
				hasInput.Add("PWDCPLXLC", policy.pwdcplxlc);
				hasInput.Add("PWDCPLXUC", policy.pwdcplxuc);
				hasInput.Add("PWDCPLXSC", policy.pwdcplxsc);
				hasInput.Add("PWDCPLXNC", policy.pwdcplxsn);
				hasInput.Add("TIMELGINRQ", policy.timelginrequire);
				hasInput.Add("TIMELGINFR", policy.lginfr);
				hasInput.Add("TIMELGINTO", policy.lginto);
				hasInput.Add("LKOUTTHRS", policy.lkoutthrs);
				hasInput.Add("RESETLKOUT", policy.resetlkout);
				hasInput.Add("USERCREATE", usercreate);
				hasOutput = AT.DBTRAN().ProcessTransHAS(hasInput);
				if (hasOutput["IPCERRORCODE"].ToString() == "0")
				{
					errorCode = "0";
				}
				else
				{
					errorCode = hasOutput["IPCERRORCODE"].ToString();
					errorDesc = hasOutput["IPCERRORDESC"].ToString();
				}
				result = null;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}

		public DataSet PolicyDelete(int policyid, string serviceid, ref string errorCode, ref string errorDesc)
		{
			DataSet result;
			try
			{
				Hashtable hasInput = new Hashtable();
				Hashtable hasOutput = new Hashtable();
				hasInput.Add("IPCTRANCODE", "SEMSPOLICYDELETE");
				hasInput.Add("SOURCEID", "SEMS");
				hasInput.Add("SOURCETRANREF", "010");
				hasInput.Add("TRANDESC", "Xóa policy");
				hasInput.Add("REVERSAL", "N");
				hasInput.Add("POLICYID", policyid);
				hasInput.Add("SERVICEID", serviceid);
				hasOutput = AT.DBTRAN().ProcessTransHAS(hasInput);
				DataSet ds = new DataSet();
				if (hasOutput["IPCERRORCODE"].ToString() == "0")
				{
					ds = (DataSet)hasOutput["DATASET"];
					errorCode = "0";
				}
				else
				{
					errorCode = hasOutput["IPCERRORCODE"].ToString();
					errorDesc = hasOutput["IPCERRORDESC"].ToString();
				}
				result = ds;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}

		public DataSet GetUsernamebycondition(string username, string fullname, string contractno, string serviceid, bool registerpolicy, ref string errorCode, ref string errorDesc)
		{
			DataSet result;
			try
			{
				Hashtable hasInput = new Hashtable();
				Hashtable hasOutput = new Hashtable();
				hasInput.Add("IPCTRANCODE", "SEMSPOLICYUSERSEARCH");
				hasInput.Add("SOURCEID", "SEMS");
				hasInput.Add("SOURCETRANREF", "010");
				hasInput.Add("TRANDESC", "Tìm kiếm sản phẩm Ebanking");
				hasInput.Add("REVERSAL", "N");
				hasInput.Add("REGISTERPOLICY", registerpolicy);
				hasInput.Add("USERNAME", username);
				hasInput.Add("FULLNAME", fullname);
				hasInput.Add("CONTRACTNO", contractno);
				hasInput.Add("SERVICEID", serviceid);
				hasOutput = AT.DBTRAN().ProcessTransHAS(hasInput);
				DataSet ds = new DataSet();
				if (hasOutput["IPCERRORCODE"].ToString() == "0")
				{
					ds = (DataSet)hasOutput["DATASET"];
					errorCode = "0";
				}
				else
				{
					errorCode = hasOutput["IPCERRORCODE"].ToString();
					errorDesc = hasOutput["IPCERRORDESC"].ToString();
				}
				result = ds;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}

		public DataSet PolicyUserInsert(string username, string serviceid, string policyid, ref string errorCode, ref string errorDesc)
		{
			DataSet result;
			try
			{
				Hashtable hasInput = new Hashtable();
				Hashtable hasOutput = new Hashtable();
				hasInput.Add("IPCTRANCODE", "SEMSPOLICYUSERINSERT");
				hasInput.Add("SOURCEID", "SEMS");
				hasInput.Add("SOURCETRANREF", "010");
				hasInput.Add("TRANDESC", "add policy for user");
				hasInput.Add("SERVICEID", serviceid);
				hasInput.Add("USERNAME", username);
				hasInput.Add("POLICYID", policyid);
				hasOutput = AT.DBTRAN().ProcessTransHAS(hasInput);
				if (hasOutput["IPCERRORCODE"].ToString() == "0")
				{
					errorCode = "0";
				}
				else
				{
					errorCode = hasOutput["IPCERRORCODE"].ToString();
					errorDesc = hasOutput["IPCERRORDESC"].ToString();
				}
				result = null;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}

		public DataSet PolicyUserUpdate(string username, string serviceid, string policyid, ref string errorCode, ref string errorDesc)
		{
			DataSet result;
			try
			{
				Hashtable hasInput = new Hashtable();
				Hashtable hasOutput = new Hashtable();
				hasInput.Add("IPCTRANCODE", "SEMSPOLICYUSERUPDATE");
				hasInput.Add("SOURCEID", "SEMS");
				hasInput.Add("SOURCETRANREF", "010");
				hasInput.Add("TRANDESC", "add policy for user");
				hasInput.Add("SERVICEID", serviceid);
				hasInput.Add("USERNAME", username);
				hasInput.Add("POLICYID", policyid);
				hasOutput = AT.DBTRAN().ProcessTransHAS(hasInput);
				if (hasOutput["IPCERRORCODE"].ToString() == "0")
				{
					errorCode = "0";
				}
				else
				{
					errorCode = hasOutput["IPCERRORCODE"].ToString();
					errorDesc = hasOutput["IPCERRORDESC"].ToString();
				}
				result = null;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}

		public DataSet PolicyUserDelete(string username, string serviceid, string policyid, ref string errorCode, ref string errorDesc)
		{
			DataSet result;
			try
			{
				Hashtable hasInput = new Hashtable();
				Hashtable hasOutput = new Hashtable();
				hasInput.Add("IPCTRANCODE", "SEMSPOLICYUSERDELETE");
				hasInput.Add("SOURCEID", "SEMS");
				hasInput.Add("SOURCETRANREF", "010");
				hasInput.Add("TRANDESC", "add policy for user");
				hasInput.Add("SERVICEID", serviceid);
				hasInput.Add("USERNAME", username);
				hasInput.Add("POLICYID", policyid);
				hasOutput = AT.DBTRAN().ProcessTransHAS(hasInput);
				if (hasOutput["IPCERRORCODE"].ToString() == "0")
				{
					errorCode = "0";
				}
				else
				{
					errorCode = hasOutput["IPCERRORCODE"].ToString();
					errorDesc = hasOutput["IPCERRORDESC"].ToString();
				}
				result = null;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}

		public DataTable Checkdefaultpolicy(string policyid, string serviceid)
		{
			DataTable result;
			try
			{
				DataTable RegionExist = new DataTable();
				RegionExist = DataAccess.GetFromDataTable("EBA_policy_check_default_exist", new SqlParameter[]
				{
					new SqlParameter
					{
						ParameterName = "@policyid",
						Value = policyid,
						SqlDbType = SqlDbType.Text
					},
					new SqlParameter
					{
						ParameterName = "@serviceid",
						Value = serviceid,
						SqlDbType = SqlDbType.Text
					}
				});
				result = RegionExist;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		public static bool hasLowerCharacter(string str)
		{
			bool result;
			for (int i = 0; i <= str.Length - 1; i++)
			{
				if (char.IsLower(str[i]))
				{
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}

		public static bool hasUpperCharacter(string str)
		{
			bool result;
			for (int i = 0; i <= str.Length - 1; i++)
			{
				if (char.IsUpper(str[i]))
				{
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}

		public static bool hasNumberCharacter(string str)
		{
			bool result;
			for (int i = 0; i <= str.Length - 1; i++)
			{
				if (char.IsDigit(str[i]))
				{
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}
        
		public static bool hasSymbolCharacter(string str)
		{
			string symbol = "`~!@#$%^&*()-_=+[]{}\\|;:'\",<.>/?";
			bool result;
			for (int i = 0; i <= str.Length - 1; i++)
			{
				if (symbol.IndexOf(str[i]) >= 0)
				{
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}

		public DataTable Checkpolicyused(string policyid, string serviceid)
		{
			DataTable result;
			try
			{
				DataTable RegionExist = new DataTable();
				RegionExist = DataAccess.GetFromDataTable("EBA_policy_check_used", new SqlParameter[]
				{
					new SqlParameter
					{
						ParameterName = "@policyid",
						Value = policyid,
						SqlDbType = SqlDbType.Text
					},
					new SqlParameter
					{
						ParameterName = "@serviceid",
						Value = serviceid,
						SqlDbType = SqlDbType.Text
					}
				});
				result = RegionExist;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		public DataTable GetUserbyservice(string serviceid)
		{
			DataTable result;
			try
			{
				DataTable RegionExist = new DataTable();
				RegionExist = DataAccess.GetFromDataTable("Eba_get_user_by_service", new SqlParameter[]
				{
					new SqlParameter
					{
						ParameterName = "@serviceid",
						Value = serviceid,
						SqlDbType = SqlDbType.Text
					}
				});
				result = RegionExist;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		public DataTable GetPolicyBaseonthispolicy(string policyid, string Serviceid)
		{
			DataTable result;
			try
			{
				DataTable RegionExist = new DataTable();
				RegionExist = DataAccess.GetFromDataTable("EBA_POLICY_GET_POLICY_BASEONTHISPOLICY", new SqlParameter[]
				{
					new SqlParameter
					{
						ParameterName = "@policyid",
						Value = policyid,
						SqlDbType = SqlDbType.Text
					},
					new SqlParameter
					{
						ParameterName = "@Serviceid",
						Value = Serviceid,
						SqlDbType = SqlDbType.Text
					}
				});
				result = RegionExist;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		public DataSet GetUserOnlinebycondition(string username, string fullname, string contractno, string serviceid, int pagesize, int pageindex, ref string errorCode, ref string errorDesc)
		{
			DataSet result;
			try
			{
				Hashtable hasInput = new Hashtable();
				Hashtable hasOutput = new Hashtable();
				hasInput.Add("IPCTRANCODE", "SEMSUSERONLINESEARCH");
				hasInput.Add("SOURCEID", "SEMS");
				hasInput.Add("SOURCETRANREF", "010");
				hasInput.Add("TRANDESC", "load user online");
				hasInput.Add("REVERSAL", "N");
				hasInput.Add("USERNAME", username);
				hasInput.Add("FULLNAME", fullname);
				hasInput.Add("CONTRACTNO", contractno);
				hasInput.Add("SERVICEID", serviceid);
				hasInput.Add("PAGESIZE", pagesize);
				hasInput.Add("PAGEINDEX", pageindex);
				hasOutput = AT.DBTRAN().ProcessTransHAS(hasInput);
				DataSet ds = new DataSet();
				if (hasOutput["IPCERRORCODE"].ToString() == "0")
				{
					ds = (DataSet)hasOutput["DATASET"];
					errorCode = "0";
				}
				else
				{
					errorCode = hasOutput["IPCERRORCODE"].ToString();
					errorDesc = hasOutput["IPCERRORDESC"].ToString();
				}
				result = ds;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}

		public DataSet useronlinekickout(string username, string serviceid, ref string errorCode, ref string errorDesc)
		{
			DataSet result;
			try
			{
				Hashtable hasInput = new Hashtable();
				Hashtable hasOutput = new Hashtable();
				hasInput.Add("IPCTRANCODE", "SEMSUSERKICKOUT");
				hasInput.Add("SOURCEID", "SEMS");
				hasInput.Add("SOURCETRANREF", "010");
				hasInput.Add("TRANDESC", "add policy for user");
				hasInput.Add("SERVICEID", serviceid);
				hasInput.Add("USERNAME", username);
				hasOutput = AT.DBTRAN().ProcessTransHAS(hasInput);
				if (hasOutput["IPCERRORCODE"].ToString() == "0")
				{
					errorCode = "0";
				}
				else
				{
					errorCode = hasOutput["IPCERRORCODE"].ToString();
					errorDesc = hasOutput["IPCERRORDESC"].ToString();
				}
				result = null;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}

        public DataTable GetUsersbyservice(string isregistered,string username,string fullname,string contractno, string Serviceid,int pagesize,int pageindex)
        {
            DataTable result;
            try
            {
                DataTable RegionExist = new DataTable();
                RegionExist = DataAccess.GetFromDataTable("SEMS_GET_USER_BY_SERVICE", new SqlParameter[]
				{
					new SqlParameter
					{
						ParameterName = "@isregistered",
						Value = isregistered,
						SqlDbType = SqlDbType.Text
					},
                    new SqlParameter
					{
						ParameterName = "@username",
						Value = username,
						SqlDbType = SqlDbType.Text
					},
                    new SqlParameter
					{
						ParameterName = "@fullname",
						Value = fullname,
						SqlDbType = SqlDbType.Text
					},
                    new SqlParameter
					{
						ParameterName = "@contractno",
						Value = contractno,
						SqlDbType = SqlDbType.Text
					},
					new SqlParameter
					{
						ParameterName = "@Serviceid",
						Value = Serviceid,
						SqlDbType = SqlDbType.Text
					},
                    new SqlParameter
					{
						ParameterName = "@pagesize",
						Value = pagesize,
						SqlDbType = SqlDbType.Int
					},
                    new SqlParameter
					{
						ParameterName = "@pageindex",
						Value = pageindex,
						SqlDbType = SqlDbType.Int
					}
				});
                result = RegionExist;
            }
            catch
            {
                result = null;
            }
            return result;
        }
        public DataTable UpadatePassNew(string USERNAME,string PASSWORD,string SERVICEID)
        {
            DataTable result;
            try
            {
                DataTable RegionExist = new DataTable();
                RegionExist = DataAccess.GetFromDataTable("USERS_CHANGEPASSWORDS_NEWFORM", new SqlParameter[]
				{
					new SqlParameter
					{
						ParameterName = "@USERNAME",
						Value = USERNAME,
						SqlDbType = SqlDbType.Text
					},
                    new SqlParameter
					{
						ParameterName = "@PASSWORD",
						Value = PASSWORD,
						SqlDbType = SqlDbType.Text
					},
                    new SqlParameter
					{
						ParameterName = "@SERVICEID",
						Value = SERVICEID,
						SqlDbType = SqlDbType.Text
					}
				});
                result = RegionExist;
            }
            catch
            {
                result = null;
            }
            return result;
        }

	}
}
