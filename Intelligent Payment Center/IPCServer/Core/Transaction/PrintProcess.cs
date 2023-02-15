using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBConnection;
using Interfaces;
using Utility;

namespace Transaction
{
    public class PrintProcess
    {
        public bool CreatePrint(TransactionInfo tran, string parmlist)
        {
            Connection con = new Connection();

            try
            {
                string template = string.Empty;
                string[] array = parmlist.Split('|');
                string tranCode = tran.Data.ContainsKey(array[0]) ? tran.Data[array[0]].ToString() : array[0];
                string transId = array.Length > 1 ? tran.Data.ContainsKey(array[1]) ? tran.Data[array[1]].ToString() : tran.IPCTransID.ToString() : tran.IPCTransID.ToString();
                string sourceId = tran.Data[Common.KEYNAME.SOURCEID].ToString();
                string userId = tran.Data[Common.KEYNAME.USERID].ToString();
                string lang = tran.Data.ContainsKey(Common.KEYNAME.LANG) ? tran.Data[Common.KEYNAME.LANG].ToString() : "en-US";

                DataTable dtTemplate = con.FillDataTable(Common.ConStr, "IPC_GETPRINTTEMPLATE", new object[3] { sourceId, tranCode, lang });
                if (dtTemplate.Rows.Count > 0)
                {
                    template = dtTemplate.Rows[0][Common.KEYNAME.RESPONSETEMPLATE].ToString();
                }

                if (string.IsNullOrEmpty(template))
                {
                    Utility.ProcessLog.LogError(new Exception($"Template Print " + tranCode + " - " + lang + " is empty!"), System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name + "(" + tran.IPCTransID.ToString() + ")");
                    return true;
                }

                DataTable dtDefineOut = con.FillDataTable(Common.ConStr, "IPC_GETOUTPUTDEFINEPRINT", new object[2] { sourceId, tranCode });

                foreach (DataRow dr in dtDefineOut.Rows)
                {
                    string ValueStyle = dr["VALUESTYLE"].ToString();
                    string ValueName = dr["VALUENAME"].ToString();
                    // Get Value
                    object value = Formatters.Formatter.GetFieldValue(tran, ValueStyle, ValueName, tran.Data, tran.parm, null);

                    Formatters.Formatter.FormatFieldValue(ref value, dr["FORMATTYPE"].ToString(), dr["FORMATOBJECT"].ToString(), dr["FORMATFUNCTION"].ToString(), dr["FORMATPARM"].ToString());

                    template = template.Replace("{" + dr["FIELDNAME"].ToString() + "}", value.ToString());
                }

                try
                {
                    con.ExecuteNonquery(Common.ConStr, "IPC_PRINTOUT_INSERT", transId, sourceId, userId, tranCode, template);
                }
                catch (Exception ex)
                {
                    Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                    tran.SetErrorInfo(ex);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(),
                    System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return true;
        }
    }
}
