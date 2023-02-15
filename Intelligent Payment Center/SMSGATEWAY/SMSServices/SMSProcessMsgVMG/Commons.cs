using DBConnection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace SMSProcessMsgMT
{
    class Commons
    {
        public static object GetFieldValue(string ValueStyle, string ValueName, DataRow datarow)
        {
            object result = null;
            try
            {
                switch (ValueStyle)
                {
                    case "VALUE":
                        return ValueName;
                    case "DATA":
                        return datarow[ValueName];
                    case "RUNTIME":
                        switch (ValueName)
                        {
                            case "DATETIME":
                                return DateTime.Now.ToString();
                            case "DATE":
                                return DateTime.Now.ToString("dd/MM/yyyy");
                            case "TIME":
                                return DateTime.Now.ToString("HH:mm:ss");
                        }
                        return "";
                    case "SCALARSQL":
                        Connection conScalarSQL = new Connection();
                        result = conScalarSQL.ExecuteScalarSQL(Common.ConStr, ValueName);
                        conScalarSQL = null;
                        return result;
                    case "SQLSTR":
                        Connection con = new Connection();
                        string sql = FormatStringParams(datarow, ValueName);
                        result = con.ExecuteScalarSQL(Common.ConStr, sql);

                        if (result is DataTable)
                        {
                            DataTable dtSQL = (DataTable)result;
                            if (dtSQL.Rows.Count > 0)
                                result = dtSQL.Rows[0][0];
                        }

                        return result;
                }
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogInformation(ex.Message, Utility.Common.FILELOGTYPE.LOGFILEPATH);
            }
            return result;
        }

        private static string FormatStringParams(DataRow dataRow, string str, bool isPassKeyToValue = false)
        {
            try
            {
                if (str.Contains("{"))
                {
                    List<string> param = GetStringParams(str);
                    foreach (string p in param)
                    {
                        string value = (dataRow.Table.Columns.Contains(p)) ? dataRow[p].ToString() : isPassKeyToValue ? p : "";
                        str = str.Replace("{" + p + "}", value);
                    }
                }
                return str;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static List<string> GetStringParams(string str)
        {
            List<string> param = new List<string>();
            string[] kk = str.Split('{');
            foreach (string k in kk)
            {
                if (k.Contains("}"))
                {
                    param.Add(k.Split('}')[0].Trim());
                }
            }
            return param;
        }

        public static void FormatFieldValue(ref object FieldValue, string FormatType, string FormatObject, string FormatFunction, string FormatParm)
        {
            try
            {
                switch (FormatType)
                {
                    case "":
                        return;
                    case "NUMBER":
                        #region Format Type Number
                        double noValue = 0;
                        if (double.TryParse(FieldValue.ToString(), System.Globalization.NumberStyles.Any, null, out noValue))
                        {
                            FieldValue = noValue.ToString(FormatParm);
                        }
                        #endregion
                        break;
                    case "DT":
                        string[] dttemp = FormatParm.Split('|');
                        if (dttemp.Length > 1)
                        {
                            try
                            {
                                FieldValue = DateTime.ParseExact(FieldValue.ToString(), dttemp[0], null).ToString(dttemp[1]);
                            }
                            catch
                            {
                                DateTime.ParseExact(FieldValue.ToString(), "dd/MM/yyyy", null).ToString(dttemp[1]);
                            }
                        }
                        else if (dttemp.Length > 0)
                        {
                            FieldValue = DateTime.Parse(FieldValue.ToString()).ToString(dttemp[0]);
                        }
                        break;
                    case "SQL":
                        Connection con = new Connection();
                        FieldValue = con.ExecuteScalarSQL(Common.ConStr, String.Format(FormatFunction, FieldValue)).ToString();
                        con = null;
                        break;
                    case "DECIMAL":
                        try
                        {
                            decimal number;
                            if (FieldValue.ToString().EndsWith("F", StringComparison.OrdinalIgnoreCase))
                            {
                                FieldValue = FieldValue.ToString().Substring(0, FieldValue.ToString().Length - 1);
                            }
                            float f;
                            double d;
                            if (decimal.TryParse(FieldValue.ToString(), NumberStyles.Any, new NumberFormatInfo() { NumberDecimalSeparator = "." }, out number))
                            {
                            }
                            else if (double.TryParse(FieldValue.ToString(), out d))
                            {
                                number = (decimal)d;
                            }
                            else if (float.TryParse(FieldValue.ToString(), out f))
                            {
                                number = (decimal)f;
                            }

                            if (!number.ToString().Contains("."))
                            {
                                FieldValue = number.ToString("0.00", new NumberFormatInfo() { NumberDecimalSeparator = "." });
                            }
                            else
                            {
                                FieldValue = number;
                            }
                        }
                        catch { }
                        break;
                }
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogInformation(ex.Message, Utility.Common.FILELOGTYPE.LOGFILEPATH);
            }
        }

        public static string GetFullFieldValueXML(object value, string FieldStyle,
                        string FieldName, string FormatType, string FormatObject, string FormatFunction,
                        string FormatParm)
        {
            string fullValue = "";
            try
            {
                switch (FieldStyle)
                {
                    case "VALUE":
                        FormatFieldValue(ref value, FormatType, FormatObject, FormatFunction, FormatParm);
                        fullValue = value.ToString();
                        break;
                    case "TAG":
                        FormatFieldValue(ref value, FormatType, FormatObject, FormatFunction, FormatParm);
                        fullValue = String.Format("<{0}><![CDATA[{1}]]></{0}>", FieldName, value);
                        break;
                    case "TAGNC":
                        FormatFieldValue(ref value, FormatType, FormatObject, FormatFunction, FormatParm);

                        if (value == null || (value.GetType() == typeof(string) && value.Equals("")))
                        {
                            fullValue = String.Format("<{0} />", FieldName);
                        }
                        else
                        {
                            try
                            {
                                if (value.GetType() == typeof(string))
                                {
                                    value = (value as string).Replace("&", "&amp;");
                                    value = (value as string).Replace("'", "&apos;");
                                    value = (value as string).Replace("\"", "&quot;");
                                    value = (value as string).Replace(">", "&gt;");
                                    value = (value as string).Replace("<", "&lt;");
                                }
                            }
                            catch { }

                            fullValue = String.Format("<{0}>{1}</{0}>", FieldName, value);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogInformation(ex.Message, Utility.Common.FILELOGTYPE.LOGFILEPATH);
            }
            return fullValue;
        }

        public static object GetFullFieldValueJSON(object value, string FieldStyle,
                        string FieldName, string FormatType, string FormatObject, string FormatFunction,
                        string FormatParm)
        {
            object fullValue = "";
            try
            {
                switch (FieldStyle)
                {
                    case "VALUE":
                        FormatFieldValue(ref value, FormatType, FormatObject, FormatFunction, FormatParm);
                        fullValue = value;
                        break;
                    case "TAG":
                        FormatFieldValue(ref value, FormatType, FormatObject, FormatFunction, FormatParm);
                        Dictionary<string, object> dicJson = new Dictionary<string, object>();
                        dicJson.Add(FieldName, value);
                        fullValue =
                        JsonConvert.SerializeObject(dicJson);
                        //vutt 20180528 fix issue build json multi node
                        fullValue = fullValue.ToString().TrimStart('{').TrimEnd('}');
                        break;
                    default:
                        fullValue = value;
                        break;
                }
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return fullValue;
        }
    }
}
