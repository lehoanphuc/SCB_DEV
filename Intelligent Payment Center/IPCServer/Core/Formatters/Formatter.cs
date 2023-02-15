using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Xml;
using DBConnection;
using Utility;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using Newtonsoft.Json;
using System.Globalization;
using System.Xml.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using JWT;
using JWT.Serializers;
using JWT.Algorithms;
using JWT.Exceptions;
using System.Configuration;

namespace Formatters
{
    public class Formatter
    {
        #region Private Variable
        #endregion

        #region Constructor
        public Formatter()
        {

        }
        #endregion

        #region Private Function
        private static object GetParmValue(string element, object objParm)
        {
            try
            {
                string[] listvaluename = element.Split('|');
                object parmtemp = objParm;
                for (int j = 0; j < listvaluename.Length; j++)
                {
                    string[] valueindex = listvaluename[j].Split(',');
                    if (valueindex.Length == 1)
                    {
                        parmtemp = ((object[])parmtemp)[int.Parse(valueindex[0])];
                    }
                    else if (valueindex.Length == 2)
                    {
                        parmtemp = ((object[,])parmtemp)[int.Parse(valueindex[0]), int.Parse(valueindex[1])];
                    }
                    else if (valueindex.Length == 3)
                    {
                        parmtemp = ((object[,,])parmtemp)[int.Parse(valueindex[0]), int.Parse(valueindex[1]), int.Parse(valueindex[2])];
                    }
                }
                return parmtemp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static string CalExpression(string expression, Hashtable Data, object Parm,
                                        Hashtable DataSub, object ParmSub, DataRow objRow)
        {
            Connection con = new Connection();
            try
            {
                expression = ReplaceExpression(expression, Data, Parm, DataSub, ParmSub, objRow);
                return con.ExecuteScalarSQL(Common.ConStr, "SELECT (" + expression + ") RESULT").ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con = null;
            }
        }

        private static string ReplaceExpression(string expression, Hashtable Data, object Parm,
                                            Hashtable DataSub, object ParmSub, DataRow objRow)
        {
            string tempexpression = expression.Trim();
            if (tempexpression == "") return "";
            Hashtable ListReplace = new Hashtable();
            try
            {
                for (int i = 0; i < tempexpression.Length - 1; i++)
                {
                    string temp = tempexpression.Substring(i, 1);
                    int index = 0;
                    switch (temp)
                    {
                        case "#":
                            #region Replace by DATA
                            if (tempexpression.Substring(i + 1, 1) == "#")
                            {
                                index = tempexpression.IndexOf("##", i + 2);
                                if (index <= 0) break;
                                temp = tempexpression.Substring(i, index - i + 2);
                                ListReplace.Add(temp, DataSub[temp.Substring(2, temp.Length - 4)]);
                                i = index + 2;
                            }
                            else
                            {
                                index = tempexpression.IndexOf("#", i + 1);
                                if (index <= 0) break;
                                temp = tempexpression.Substring(i, index - i + 1);
                                ListReplace.Add(temp, Data[temp.Substring(1, temp.Length - 2)]);
                                i = index + 1;
                            }
                            break;
                        #endregion
                        case "[":
                            #region Replace by PARM
                            if (tempexpression.Substring(i + 1, 1) == "[")
                            {
                                index = tempexpression.IndexOf("]]", i + 2);
                                if (index <= 0) break;
                                temp = tempexpression.Substring(i, index - i + 2);
                                ListReplace.Add(temp, GetParmValue(temp.Substring(2, temp.Length - 4), ParmSub));
                                i = index + 2;
                            }
                            else
                            {
                                index = tempexpression.IndexOf("]", i + 1);
                                if (index <= 0) break;
                                temp = tempexpression.Substring(i, index - i + 1);
                                ListReplace.Add(temp, GetParmValue(temp.Substring(1, temp.Length - 2), Parm));
                                i = index + 1;
                            }
                            break;
                            #endregion
                    }
                }
                foreach (string key in ListReplace.Keys)
                {
                    if (ListReplace[key] != null)
                    {
                        tempexpression = tempexpression.Replace(key, ListReplace[key].ToString());
                    }
                    else
                    {
                        tempexpression = tempexpression.Replace(key, "");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ListReplace = null;
            }
            return tempexpression;
        }

        private static string ReplaceExpressionOld(string expression, Hashtable Data, object Parm,
                                            Hashtable DataSub, object ParmSub, DataRow objRow)
        {
            expression = expression.Trim();
            if (expression == "") return "";
            int len = expression.Length;
            string tempExpression = "";
            Hashtable subExpressionList = new Hashtable();
            try
            {
                #region Analyze Sub Expression
                int pos = 0;
                int start = 0;
                int countchar = 0;
                while (pos < len)
                {
                    string tempchar = expression.Substring(pos, 1);
                    if (tempchar == "(")
                    {
                        if (countchar == 0)
                        {
                            if (pos > start) subExpressionList.Add(subExpressionList.Count.ToString(), expression.Substring(start, pos - start));
                            start = pos;
                        }
                        countchar++;
                    }
                    else if (tempchar == ")")
                    {
                        if (countchar == 1)
                        {
                            if (pos > start) subExpressionList.Add(subExpressionList.Count.ToString(), expression.Substring(start, pos - start + 1));
                            start = pos + 1;
                        }
                        else if (countchar == 0)
                        {
                            throw new Exception("Expression error");
                        }
                        countchar--;
                    }
                    pos++;
                }
                if (pos > start) subExpressionList.Add(subExpressionList.Count.ToString(), expression.Substring(start, pos - start));
                #endregion

                #region Replace Expression
                if (subExpressionList.Count > 1)
                {
                    for (int i = 0; i < subExpressionList.Count; i++)
                    {
                        tempExpression = tempExpression + ReplaceExpression(subExpressionList[i.ToString()].ToString(), Data, Parm, DataSub, ParmSub, objRow);
                    }
                }
                else
                {
                    if (expression.Substring(0, 1) == "(")
                    {
                        tempExpression = "(" + ReplaceExpression(expression.Substring(1, expression.Length - 2), Data, Parm, DataSub, ParmSub, objRow) + ")";
                    }
                    else
                    {
                        #region Analyze Operator
                        char operators = '*';
                        string[] listParm = expression.Split(operators);
                        if (listParm.Length == 1)
                        {
                            operators = '/';
                            listParm = expression.Split(operators);
                            if (listParm.Length == 1)
                            {
                                operators = '+';
                                listParm = expression.Split(operators);
                                if (listParm.Length == 1)
                                {
                                    operators = '-';
                                    listParm = expression.Split(operators);
                                }
                            }
                        }
                        #endregion

                        #region Replace Para
                        if (listParm.Length == 1)
                        {
                            if (expression.Substring(0, 2) == "[[")
                            {
                                tempExpression = GetParmValue(expression.Substring(2, expression.Length - 4), ParmSub).ToString();
                            }
                            else if (expression.Substring(0, 2) == "##")
                            {
                                tempExpression = DataSub[expression.Substring(2, expression.Length - 4)].ToString();
                            }
                            else if (expression.Substring(0, 1) == "[")
                            {
                                tempExpression = GetParmValue(expression.Substring(1, expression.Length - 2), Parm).ToString();
                            }
                            else if (expression.Substring(0, 1) == "#")
                            {
                                tempExpression = Data[expression.Substring(1, expression.Length - 2)].ToString();
                            }
                            else
                            {
                                tempExpression = expression;
                                /*tempExpression = expression.Replace("A", "0");
                                tempExpression = tempExpression.Replace("B", "1");
                                tempExpression = tempExpression.Replace("C", "2");
                                tempExpression = tempExpression.Replace("D", "3");
                                tempExpression = tempExpression.Replace("E", "4");
                                tempExpression = tempExpression.Replace("F", "5");
                                tempExpression = tempExpression.Replace("G", "6");
                                tempExpression = tempExpression.Replace("H", "7");
                                tempExpression = tempExpression.Replace("I", "8");
                                tempExpression = tempExpression.Replace("J", "9");*/
                            }
                        }
                        else
                        {
                            for (int i = 0; i < listParm.Length; i++)
                            {
                                tempExpression = tempExpression + ReplaceExpression(listParm[i], Data, Parm, DataSub, ParmSub, objRow) + operators;
                            }
                            tempExpression = tempExpression.Substring(0, tempExpression.Length - 1);
                        }
                        #endregion
                    }
                }
                return tempExpression;
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static object GetFieldValue(TransactionInfo tran, string ValueStyle, string ValueName,
                                                    Hashtable DataSub, object ParmSub, DataRow objRow)
        {
            object result = null;
            try
            {
                switch (ValueStyle)
                {
                    case "VALUE":
                        return ValueName;
                    case "DATA":
                        return tran.Data[ValueName];
                    case "PARM":
                        return GetParmValue(ValueName, tran.parm);
                    case "DATASUB":
                        return DataSub[ValueName];
                    case "PARMSUB":
                        return GetParmValue(ValueName, ParmSub);
                    case "SYSVAR":
                        return Common.SYSVAR[ValueName];
                    case "DBSYSVAR":
                        Connection conSysVar = new Connection();
                        result = conSysVar.ExecuteScalarSQL(Common.ConStr, $"SELECT VARVALUE FROM IPCSYSVAR (NOLOCK) WHERE VARNAME = '{ValueName}'");
                        if (result is DataTable)
                        {
                            DataTable dtSQL = (DataTable)result;
                            if (dtSQL.Rows.Count > 0)
                                result = dtSQL.Rows[0][0];
                        }
                        return result;
                    case "ROWDATA":
                        return objRow[ValueName];
                    case "CALC":
                        return CalExpression(ValueName, tran.Data, tran.parm, DataSub, ParmSub, objRow);
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
                    case "SCALARPRO":
                        Connection conScalarPRO = new Connection();
                        object[] parameterScalarPRO = GetParmListForProcedure(tran, ValueName, DataSub, ParmSub, objRow);
                        result = conScalarPRO.ExecuteScalar(Common.ConStr, ValueName, parameterScalarPRO);
                        conScalarPRO = null;
                        return result;
                    case "DATATABLESQL":
                        Connection conDataTableSQL = new Connection();
                        result = conDataTableSQL.FillDataTableSQL(Common.ConStr, ValueName);
                        conDataTableSQL = null;
                        return result;
                    case "DATATABLEPRO":
                        Connection conDataTablePRO = new Connection();
                        object[] parameterDataTablePRO = GetParmListForProcedure(tran, ValueName, DataSub, ParmSub, objRow);
                        result = conDataTablePRO.FillDataTable(Common.ConStr, ValueName, parameterDataTablePRO);
                        conDataTablePRO = null;
                        return result;
                    case "IPCTRANCODESOURCE":
                        #region IPCTRANCODESOURCE
                        switch (tran.MessageTypeSource)
                        {
                            case Common.MESSAGETYPE.XML:
                                result = CreateMessageXML(tran, ValueName, DataSub, ParmSub, objRow);
                                break;
                            case Common.MESSAGETYPE.ISO:
                                result = CreateMessageISO(tran, ValueName, DataSub, ParmSub, objRow);
                                break;
                            case Common.MESSAGETYPE.SEP:
                                result = CreateMessageSEP(tran, ValueName, DataSub, ParmSub, objRow);
                                break;
                            case Common.MESSAGETYPE.SMS:
                                result = CreateMessageSMS(tran, ValueName, DataSub, ParmSub, objRow);
                                break;
                            case Common.MESSAGETYPE.HAS:
                                Hashtable tempHashtable = new Hashtable();
                                CreateDataHAS(tempHashtable, tran, ValueName, DataSub, ParmSub, objRow);
                                result = tempHashtable;
                                break;
                            case Common.MESSAGETYPE.PAR:
                                object[] tempParm = new object[0];
                                CreateDataParm(tran, ref tempParm, ValueName, "-1", DataSub, ParmSub, objRow);
                                result = tempParm;
                                break;
                        }
                        #endregion
                        return result;
                    case "IPCTRANCODEDEST":
                        #region IPCTRANCODESOURCE
                        switch (tran.MessageTypeDest)
                        {
                            case Common.MESSAGETYPE.XML:
                                result = CreateMessageXML(tran, ValueName, DataSub, ParmSub, objRow);
                                break;
                            case Common.MESSAGETYPE.ISO:
                                result = CreateMessageISO(tran, ValueName, DataSub, ParmSub, objRow);
                                break;
                            case Common.MESSAGETYPE.SEP:
                                result = CreateMessageSEP(tran, ValueName, DataSub, ParmSub, objRow);
                                break;
                            case Common.MESSAGETYPE.SMS:
                                result = CreateMessageSMS(tran, ValueName, DataSub, ParmSub, objRow);
                                break;
                            case Common.MESSAGETYPE.HAS:
                                Hashtable tempHashtable = new Hashtable();
                                CreateDataHAS(tempHashtable, tran, ValueName, DataSub, ParmSub, objRow);
                                result = tempHashtable;
                                break;
                            case Common.MESSAGETYPE.PAR:
                                object[] tempParm = new object[0];
                                CreateDataParm(tran, ref tempParm, ValueName, "-1", DataSub, ParmSub, objRow);
                                result = tempParm;
                                break;
                        }
                        #endregion
                        return result;
                    case "DATASETSTRING":
                        DataSet dsValue = (DataSet)tran.Data[ValueName];
                        result = JsonConvert.SerializeObject(dsValue);
                        return result;
                    case "ENSHA2":
                        string[] param = ValueName.Split('|');
                        result = Utility.O9Encryptpass.sha_sha256(tran.Data[param[0]].ToString(), tran.Data[param[1]].ToString());
                        return result;
                    case "REPLDATA":
                        string[] arrVal = ValueName.Trim().Split('|');
                        string[] paramVal = new string[arrVal.Length - 1];
                        for (int i = 1; i < arrVal.Length; i++)
                        {
                            if (tran.Data.ContainsKey(arrVal[i]))
                            {
                                paramVal[i - 1] = tran.Data[arrVal[i]].ToString();
                            }
                            else
                            {
                                paramVal[i - 1] = arrVal[i];
                            }
                        }
                        result = string.Format(arrVal[0], paramVal);
                        return result;
                    case "SQLSTR":
                        Connection con = new Connection();
                        string sql = FormatStringParams(tran, ValueName);
                        result = con.ExecuteScalarSQL(Common.ConStr, sql);

                        if (result is DataTable)
                        {
                            DataTable dtSQL = (DataTable)result;
                            if (dtSQL.Rows.Count > 0)
                                result = dtSQL.Rows[0][0];
                        }

                        return result;
                    case "JSONMSG":
                        result = CreateMessageHTTP(tran, ValueName, DataSub, ParmSub, objRow);
                        if (result != null)
                        {
                            JObject jo = Common.NewParse(result.ToString());
                            result = jo.SelectToken(Common.KEYNAME.HTTPBODY).ToString(Newtonsoft.Json.Formatting.None);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                if (tran.ErrorCode == Common.ERRORCODE.OK)
                {
                    //tranh tran log, giao dich autobalance goi lien tuc nen tran log rat nhanh
                    if (tran.Data[Common.KEYNAME.IPCTRANCODE].ToString().Equals("SMS00014"))
                    {
                        Utility.ProcessLog.LogInformation(String.Format("Error Get FieldValue with ValueStyle='{0}' and ValueName = '{1}'\r\nError: {2}", ValueStyle, ValueName, ex.Message));
                    }
                }
            }
            return result;
        }
        private static object[] GetParmListForProcedure(TransactionInfo tran, string TranCode,
                                    Hashtable DataSub, object ParmSub, DataRow objRow)
        {
            object[] result = null;
            try
            {
                string condition = " SOURCEID = '" + tran.Data[Common.KEYNAME.SOURCEID].ToString() + "'";
                condition += " AND DESTID = '" + tran.Data[Common.KEYNAME.DESTID].ToString() + "'";
                condition += " AND IPCTRANCODE = '" + TranCode + "'";
                condition += " AND ONLINE = " + (tran.Online == true ? "1" : "0");
                DataRow[] dr = new DataRow[0];
                switch (tran.MessageTypeSource)
                {
                    case Common.MESSAGETYPE.XML:
                        dr = Common.DBIOUTPUTDEFINEXML.Select(condition, "FIELDNO");
                        break;
                    case Common.MESSAGETYPE.ISO:
                        dr = Common.DBIOUTPUTDEFINEISO.Select(condition, "FIELDNO");
                        break;
                    case Common.MESSAGETYPE.SEP:
                        dr = Common.DBIOUTPUTDEFINESEP.Select(condition, "FIELDNO");
                        break;
                    case Common.MESSAGETYPE.SMS:
                        dr = Common.DBIOUTPUTDEFINESMS.Select(condition, "FIELDNO");
                        break;
                    case Common.MESSAGETYPE.HAS:
                        dr = Common.DBIOUTPUTDEFINEHAS.Select(condition, "FIELDNO");
                        break;
                    case Common.MESSAGETYPE.PAR:
                        dr = Common.DBIOUTPUTDEFINEPAR.Select(condition, "FIELDNO");
                        break;
                }
                //
                if (dr.Length > 0)
                {
                    result = new object[dr.Length];
                    for (int i = 0; i < dr.Length; i++)
                    {
                        string ValueStyle = dr[i]["VALUESTYLE"].ToString();
                        string ValueName = dr[i]["VALUENAME"].ToString();
                        // Get Value
                        result[i] = GetFieldValue(tran, ValueStyle, ValueName, DataSub, ParmSub, objRow);
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return result;
        }
        #endregion

        #region Public Function
        public static bool AddDataDefine(TransactionInfo tran)
        {
            try
            {
                string condition = "(SOURCEID = '' OR SOURCEID = '" + tran.Data[Common.KEYNAME.SOURCEID].ToString() + "')";
                condition += " AND (IPCTRANCODE = '' OR IPCTRANCODE = '" + tran.Data[Common.KEYNAME.IPCTRANCODE].ToString() + "')";
                DataRow[] dr = Common.DBIDATADEFINE.Select(condition, "SOURCEID DESC, IPCTRANCODE DESC");

                for (int i = 0; i < dr.Length; i++)
                {
                    string FieldName = dr[i]["FIELDNAME"].ToString();
                    if (!tran.Data.ContainsKey(FieldName))
                    {
                        string ValueStyle = dr[i]["VALUESTYLE"].ToString();
                        string ValueName = dr[i]["VALUENAME"].ToString();
                        // Get Value
                        object value = GetFieldValue(tran, ValueStyle, ValueName, tran.Data, tran.parm, null);
                        // Format Field Value
                        FormatFieldValue(ref value,
                                dr[i]["FORMATTYPE"].ToString(), dr[i]["FORMATOBJECT"].ToString(),
                                dr[i]["FORMATFUNCTION"].ToString(), dr[i]["FORMATPARM"].ToString());
                        // Add Field Value
                        tran.Data.Add(dr[i]["FIELDNAME"].ToString(), value);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogInformation(ex.ToString());
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
        }

        public static void FormatFieldValue(ref object FieldValue, string FormatType, string FormatObject,
                                            string FormatFunction, string FormatParm)
        {
            try
            {
                switch (FormatType)
                {
                    case "":
                        return;
                    case "FDATE":
                        string forDate = FieldValue.ToString().Substring(0, FieldValue.ToString().IndexOf("T"));
                        DateTime date;
                        try
                        {
                            date = DateTime.ParseExact(forDate.Trim(), "dd/MM/yyyy HH:mm:ss tt", null);
                            //date = DateTime.ParseExact(forDate.Trim(), "dd/MM/yyyy HH:mm:ss", null);
                        }
                        catch
                        {
                            date = DateTime.ParseExact(forDate.Trim(), "dd/MM/yyyy", null);
                        }
                        forDate = date.ToString("dd/MM/yyyy");
                        FieldValue = forDate.Replace("/", "");
                        break;
                    case "PHONE":
                        switch (FormatParm)
                        {
                            case "MPT":
                                FieldValue = FieldValue.ToString().Substring(1, FieldValue.ToString().Length - 1);
                                FieldValue = "tel:95" + FieldValue.ToString().Trim();
                                break;
                            case "OOREDOO":
                                FieldValue = FieldValue.ToString().Substring(1, FieldValue.ToString().Length - 1);
                                FieldValue = Utility.Common.Base64Encode(FieldValue.ToString()).Trim();
                                break;
                            case "TELENOR":
                                FieldValue = FieldValue.ToString().Substring(1, FieldValue.ToString().Length - 1).Trim();
                                break;
                            case "MYTEL":
                                FieldValue = FieldValue.ToString().Substring(1, FieldValue.ToString().Length - 1);
                                FieldValue = "95" + FieldValue.ToString().Trim();
                                break;
                        }
                        break;
                    case "BASE64":
                        FieldValue = Utility.Common.Base64Encode(FieldValue.ToString());
                        break;
                    case "AES256":
                        string[] listvaluename = FormatParm.Split('|');
                        FieldValue = Utility.Common.Encrypt(FieldValue.ToString(), listvaluename[0], listvaluename[1]);
                        break;
                    case "ENCRYPTPIN":
                        listvaluename = FormatParm.Split('|');
                        FieldValue = encryptPin(FieldValue.ToString(), listvaluename[0], listvaluename[1]);
                        break;
                    case "AN":
                        #region Format Type "AN"
                        switch (FormatObject)
                        {
                            case "STRING":
                                switch (FormatFunction)
                                {
                                    case "TRIM":
                                        FieldValue = FieldValue.ToString().Trim();
                                        break;
                                    case "SUBSTRING":
                                        int start = 0;
                                        int len = FieldValue.ToString().Length;
                                        string[] temp = FormatParm.Split('|');
                                        if (temp.Length <= 0) return;
                                        if (temp[0] == "R")
                                        {
                                            if (temp.Length < 2) return;
                                            if (!int.TryParse(temp[1], out len)) return;
                                            start = FieldValue.ToString().Length - len;
                                        }
                                        else
                                        {
                                            if (!int.TryParse(temp[0], out start)) return;
                                            if (temp.Length > 1 && !int.TryParse(temp[1], out len)) return;
                                        }
                                        FieldValue = FieldValue.ToString().Substring(start, len);
                                        break;
                                    case "PADLEFT":
                                        temp = FormatParm.Split('|');
                                        if (temp.Length > 1)
                                        {
                                            FieldValue = FieldValue.ToString().PadLeft(int.Parse(temp[0]), temp[1][0]);
                                        }
                                        else
                                        {
                                            FieldValue = FieldValue.ToString().PadLeft(int.Parse(temp[0]));
                                        }
                                        break;
                                    case "PADRIGHT":
                                        temp = FormatParm.Split('|');
                                        if (temp.Length > 1)
                                        {
                                            FieldValue = FieldValue.ToString().PadRight(int.Parse(temp[0]), temp[1][0]);
                                        }
                                        else
                                        {
                                            FieldValue = FieldValue.ToString().PadRight(int.Parse(temp[0]));
                                        }
                                        break;
                                    case "PADLEFTFIXLEN":
                                        temp = FormatParm.Split('|');
                                        if (FieldValue.ToString().Length < int.Parse(temp[0]))
                                        {
                                            if (temp.Length > 1)
                                            {
                                                FieldValue = FieldValue.ToString().PadLeft(int.Parse(temp[0]), temp[1][0]);
                                            }
                                            else
                                            {
                                                FieldValue = FieldValue.ToString().PadLeft(int.Parse(temp[0]));
                                            }
                                        }
                                        else
                                        {
                                            FieldValue = FieldValue.ToString().Substring(0, int.Parse(temp[0]));
                                        }
                                        break;
                                    case "PADRIGHTFIXLEN":
                                        temp = FormatParm.Split('|');
                                        if (FieldValue.ToString().Length < int.Parse(temp[0]))
                                        {
                                            if (temp.Length > 1)
                                            {
                                                FieldValue = FieldValue.ToString().PadRight(int.Parse(temp[0]), temp[1][0]);
                                            }
                                            else
                                            {
                                                FieldValue = FieldValue.ToString().PadRight(int.Parse(temp[0]));
                                            }
                                        }
                                        else
                                        {
                                            FieldValue = FieldValue.ToString().Substring(0, int.Parse(temp[0]));
                                        }
                                        break;
                                    //vutt yeu cau cua STB
                                    case "HIDENCHAR":
                                        temp = FormatParm.Split('|');
                                        if (FieldValue.ToString().Length > int.Parse(temp[2].ToString()))
                                        {
                                            StringBuilder sb = new StringBuilder(FieldValue.ToString());
                                            char ch = temp[0].ToCharArray()[0];
                                            for (int i = int.Parse(temp[1]) - 1; i <= int.Parse(temp[2]) - 1; i++)
                                            {
                                                sb[i] = ch;
                                            }
                                            FieldValue = sb.ToString();
                                        }
                                        break;
                                }
                                break;
                        }
                        #endregion
                        break;
                    case "NU":
                        #region Format Type "NU"
                        int precision = 0;
                        long value;
                        if (long.TryParse(FieldValue.ToString(), System.Globalization.NumberStyles.Any, null, out value))
                        {
                            if (int.TryParse(FormatParm, out precision))
                            {
                                FieldValue = (value / Math.Pow(10, precision)).ToString();
                                FieldValue = FieldValue.ToString().Replace(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator, ".");
                            }
                        }
                        #endregion
                        break;
                    case "IN":
                        #region Format Type "IN"
                        precision = 0;
                        double dblvalue = 0;
                        string[] format = FormatParm.Split('#');
                        if (double.TryParse(FieldValue.ToString(), System.Globalization.NumberStyles.Any, null, out dblvalue))
                        {
                            if (int.TryParse(format[0], out precision))
                            {
                                dblvalue = Math.Round(dblvalue, precision);
                                FieldValue = dblvalue.ToString("###.".PadRight(precision + 4, '0')).Replace(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator, "");
                            }
                        }
                        if (format.Length > 1)
                        {
                            FormatFieldValue(ref FieldValue, "AN", FormatObject, FormatFunction, format[1]);
                        }
                        #endregion
                        break;
                    case "DN":
                        #region Format Type "DN"
                        precision = 0;
                        double dnValue = 0;
                        string[] fm = FormatParm.Split('#');
                        if (double.TryParse(FieldValue.ToString(), System.Globalization.NumberStyles.Any, null, out dnValue))
                        {
                            FieldValue = dnValue.ToString("N0" + fm[0]);
                        }
                        if (fm.Length > 1)
                        {
                            FormatFieldValue(ref FieldValue, "AN", FormatObject, FormatFunction, fm[1]);
                        }
                        #endregion
                        break;
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
                    case "JOINSUBSTR":
                        string[] parms = FormatParm.Split('|');
                        FieldValue = parms[0] + FieldValue.ToString().Substring(int.Parse(parms[1]), int.Parse(parms[2]));
                        break;
                    case "ENCC":
                        FieldValue = Utility.Common.EncryptCard(FieldValue.ToString());
                        break;
                    case "DECC":
                        FieldValue = Utility.Common.DecryptCard(FieldValue.ToString());
                        break;
                    case "JSONSINGLEQUOTE":
                        FieldValue = FieldValue.ToString().Replace("\"", "'").Replace("\r\n", "");
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
                    case "MAPSTATUS":
                        try
                        {
                            con = new Connection();
                            FieldValue = con.FillDataTable(Common.ConStr, "ABankMapAcctStatus", FieldValue).Rows[0]["STTID"];
                        }
                        catch { }
                        finally
                        {
                            con = null;
                        }
                        break;
                    case "2C2PPIN":
                        try
                        {
                            // String key = "ofOYVhQiGwjuvKPleKboHjFOOKP/jS7HnQeX933Ohoo=";
                            //string key = Common.SYSVAR["2C2PAESKEY"].ToString().Trim();
                            con = new Connection();
                            string key = con.FillDataTableSQL(Common.ConStr, "SELECT VARVALUE FROM EBASYSVAR (NOLOCK) WHERE VARNAME = '2C2PAESKEY'").Rows[0]["VARVALUE"].ToString();
                            byte[] bytesToBeDecrypted = Convert.FromBase64String(FieldValue.ToString());
                            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
                            keyBytes = SHA256.Create().ComputeHash(keyBytes);
                            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
                            byte[] decryptedBytes = null;
                            using (MemoryStream ms = new MemoryStream())
                            {
                                using (RijndaelManaged AES = new RijndaelManaged())
                                {
                                    AES.KeySize = 256;
                                    AES.BlockSize = 128;
                                    var key2c2p = new Rfc2898DeriveBytes(keyBytes, saltBytes, 1000);
                                    AES.Key = key2c2p.GetBytes(AES.KeySize / 8);
                                    AES.IV = key2c2p.GetBytes(AES.BlockSize / 8);
                                    AES.Mode = CipherMode.CBC;
                                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                                    {
                                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                                        cs.Close();
                                    }
                                    decryptedBytes = ms.ToArray();
                                }
                            }
                            FieldValue = Encoding.UTF8.GetString(decryptedBytes);
                        }
                        catch (Exception ex)
                        {
                            Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                        }
                        finally
                        {
                            con = null;
                        }
                        break;
                    case "REGEX":
                        FieldValue = Regex.Replace(FieldValue.ToString(), FormatObject, FormatParm);
                        break;
                    case "JSONCONVERTDATASET":
                        string[] tagNames = FieldValue.ToString().Split('[');
                        if (String.IsNullOrEmpty(tagNames[0].ToString()))
                        {
                            string data = @"{""TABLE"":" + FieldValue.ToString() + "}";
                            FieldValue = JsonConvert.DeserializeObject<DataSet>(data);
                        }
                        else
                        {
                            FieldValue = JsonConvert.DeserializeObject<DataSet>(FieldValue.ToString());
                        }
                        break;
                    case "JSONCONVERTTOJSONSTRING":
                        FieldValue = FieldValue.ToString().Replace("\r\n", "").Replace("\\", "");  
                        break;
                }
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogInformation(ex.ToString());
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);

            }
        }
        public static bool FormatXMLToDataset(TransactionInfo tran)
        {
            //vutt dont know why exec com can not use optional var
            return FormatXMLToDataset(tran, "");
        }
        public static bool FormatXMLToDataset(TransactionInfo tran, string ParmList = "")
        {
            DataSet result = new DataSet();
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(tran.InputData);

                //vutt support load ds, dt not in lastchild, must pust tagname of ds, dt to ParmList
                if (!string.IsNullOrEmpty(ParmList.Trim()))
                {
                    XmlNodeList xnl = doc.GetElementsByTagName(ParmList.Trim());
                    if (xnl.Count != 0)
                    {
                        foreach (XmlNode xn in xnl)
                        {
                            DataSet dsTemp = GetDataSetFromXML("<" + ParmList.Trim() + ">" + xn.InnerXml + "</" + ParmList.Trim() + ">");
                            result.Merge(dsTemp);
                        }
                    }
                    else
                    {
                        result = GetDataSetFromXML(doc.LastChild.InnerText);
                    }
                }
                else
                {
                    result = GetDataSetFromXML(doc.LastChild.InnerText);
                }
                //end vutt edit
                if (tran.Data.ContainsKey(Common.KEYNAME.DATARESULT))
                {
                    tran.Data[Common.KEYNAME.DATARESULT] = result;
                }
                else
                {
                    tran.Data.Add(Common.KEYNAME.DATARESULT, result);
                }
                return true;
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }

        }
        private static DataSet GetDataSetFromXML(string xml)
        {
            DataSet result = new DataSet();

            byte[] byteArray = Encoding.UTF8.GetBytes(xml);
            MemoryStream stream = new MemoryStream(byteArray);
            StreamReader reader = new StreamReader(stream);
            result.ReadXml(reader);

            return result;
        }
        //public static bool FormatXMLToDataset(TransactionInfo tran)
        //{
        //    DataSet result = new DataSet();
        //    try
        //    {
        //        XmlDocument doc = new XmlDocument();
        //        doc.LoadXml(tran.InputData);
        //        byte[] byteArray = Encoding.ASCII.GetBytes(doc.LastChild.InnerText);
        //        MemoryStream stream = new MemoryStream(byteArray);
        //        StreamReader reader = new StreamReader(stream);
        //        result.ReadXml(reader);
        //        if (tran.Data.ContainsKey(Common.KEYNAME.DATARESULT))
        //        {
        //            tran.Data[Common.KEYNAME.DATARESULT] = result;
        //        }
        //        else
        //        {
        //            tran.Data.Add(Common.KEYNAME.DATARESULT, result);
        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
        //        return false;
        //    }

        //}

        public static bool FormatXMLToValue(TransactionInfo tran)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(tran.InputData);
                if (tran.Data.ContainsKey(Common.KEYNAME.DATARESULT))
                {
                    tran.Data[Common.KEYNAME.DATARESULT] = doc.LastChild.InnerText;
                }
                else
                {
                    tran.Data.Add(Common.KEYNAME.DATARESULT, doc.LastChild.InnerText);
                }
                return true;
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
        }

        public static bool MappingColumnName(TransactionInfo tran, string dsName)
        {
            try
            {
                DataSet ds = new DataSet();
                if (tran.Data.ContainsKey(dsName) == false || tran.Data[dsName].GetType() != ds.GetType())
                {
                    //tranh tran log, giao dich autobalance goi lien tuc nen tran log rat nhanh
                    if (!tran.Data[Common.KEYNAME.IPCTRANCODE].ToString().Equals("SMS00014"))
                    {
                        Utility.ProcessLog.LogInformation(string.Format("MappingColumnName error ds {0} not found or not dataset", dsName));
                    }
                    else
                    {
                        tran.ErrorCode = Common.ERRORCODE.SYSTEM;
                    }
                    return false;
                }
                else
                {
                    ds = (DataSet)tran.Data[dsName];
                    if (ds == null || ds.Tables.Count == 0)
                    {
                        //tranh tran log, giao dich autobalance goi lien tuc nen tran log rat nhanh
                        if (!tran.Data[Common.KEYNAME.IPCTRANCODE].ToString().Equals("SMS00014"))
                        {
                            Utility.ProcessLog.LogInformation(string.Format("MappingColumnName error ds {0} haven't any table", dsName));
                        }
                        else
                        {
                            tran.ErrorCode = Common.ERRORCODE.SYSTEM;
                        }
                        return false;
                    }
                }
                Connection dbObj = new Connection();
                DataTable rs = new DataTable();
                rs = dbObj.FillDataTable(Common.ConStr, "IPCGETMAPPINGCOLNAME", tran.Data[Common.KEYNAME.IPCTRANCODEDEST].ToString(),
                    tran.Data[Common.KEYNAME.DESTID].ToString());
                if (rs == null || rs.Rows.Count == 0)
                    return true;

                foreach (DataRow row in rs.Rows)
                {
                    ds.Tables[row["TABLENAME"].ToString()].Columns[row["TAGETNAME"].ToString()].ColumnName = row["MAPPINGNAME"].ToString();
                }
                tran.Data[dsName] = ds;
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
            return true;
        }
        #endregion

        #region Public Function XML
        private static void AnalyzeXMLNode(TransactionInfo tran, XmlNode node)
        {
            try
            {
                while (node != null)
                {
                    if (node.HasChildNodes && node.FirstChild.NodeType != XmlNodeType.Text)
                    {
                        AnalyzeXMLNode(tran, node.FirstChild);
                    }
                    else
                    {
                        if (tran.Data.ContainsKey(node.Name))
                        {
                            tran.Data[node.Name] = node.InnerText;
                        }
                        else
                        {
                            tran.Data.Add(node.Name, node.InnerText);
                        }
                    }
                    node = node.NextSibling;
                }

            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        private static string CreateMessageXML(TransactionInfo tran, string TranCode,
                                    Hashtable DataSub, object ParmSub, DataRow objRow)
        {
            string result = "";
            try
            {
                string condition = " SOURCEID = '" + tran.Data[Common.KEYNAME.SOURCEID].ToString() + "'";
                condition += " AND DESTID = '" + tran.Data[Common.KEYNAME.DESTID].ToString() + "'";
                condition += " AND IPCTRANCODE = '" + TranCode + "'";
                condition += " AND ONLINE = " + (tran.Online == true ? "1" : "0");
                DataRow[] dr = Common.DBIOUTPUTDEFINEXML.Select(condition, "FIELDNO");
                for (int i = 0; i < dr.Length; i++)
                {
                    string FieldNo = dr[i]["FIELDNO"].ToString();
                    string FieldDesc = dr[i]["FIELDDESC"].ToString();
                    string FieldStyle = dr[i]["FIELDSTYLE"].ToString();
                    string FieldName = dr[i]["FIELDNAME"].ToString();
                    string ValueStyle = dr[i]["VALUESTYLE"].ToString();
                    string ValueName = dr[i]["VALUENAME"].ToString();
                    // Get Value
                    object value = GetFieldValue(tran, ValueStyle, ValueName, DataSub, ParmSub, objRow);
                    // Get Full Value
                    value = GetFullFieldValueXML(tran, value, FieldStyle, FieldName, dr[i]["FORMATTYPE"].ToString(),
                                    dr[i]["FORMATOBJECT"].ToString(), dr[i]["FORMATFUNCTION"].ToString(),
                                    dr[i]["FORMATPARM"].ToString(), DataSub, ParmSub, objRow);
                    result += value.ToString();
                }

                //dungvt hardcode tam cho ABank transfer de fix loi amount = 0 khong ban dc sang corebanking
                try
                {
                    string parentNode = "PartTrnRec";
                    string childNode = "amountValue";
                    XElement elements = XElement.Parse(result);
                    List<XElement> entries = new List<XElement>();
                    foreach (XElement element in elements.Descendants())
                    {
                        if (element.Name.LocalName.Equals(parentNode))
                        {
                            foreach (XElement elm in element.Descendants())
                            {
                                if (elm.Name.LocalName.Equals(childNode))
                                {
                                    double amountvalue = double.Parse(elm.Value);
                                    if (amountvalue == 0)
                                    {
                                        entries.Add(element);
                                    }
                                }
                            }
                        }
                    }

                    foreach (XElement node in entries)
                    {
                        node.Remove();
                    }
                    result = elements.ToString();
                }
                catch
                {

                }
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return result;
        }

        private static string GetFullFieldValueXML(TransactionInfo tran, object value, string FieldStyle,
                        string FieldName, string FormatType, string FormatObject, string FormatFunction,
                        string FormatParm, Hashtable DataSub, object ParmSub, DataRow objRow)
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
                        //fullValue = String.Format("<{0}>{1}</{0}>", FieldName, value);
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
                    case "ARRAY1":
                        value = CreateMessageXML(tran, FieldName, DataSub, value, objRow);
                        FormatFieldValue(ref value, FormatType, FormatObject, FormatFunction, FormatParm);
                        fullValue = value.ToString();
                        break;
                    case "ARRAY2":
                        int row = ((object[,])value).GetLength(0);
                        int column = ((object[,])value).GetLength(1);
                        object[] arrayTemp = new object[column];
                        //
                        for (int j = 0; j < row; j++)
                        {
                            for (int k = 0; k < row; k++)
                            {
                                arrayTemp[k] = ((object[,])value)[j, k];
                            }
                            fullValue += CreateMessageXML(tran, FieldName, DataSub, arrayTemp, objRow);
                        }
                        value = fullValue;
                        FormatFieldValue(ref value, FormatType, FormatObject, FormatFunction, FormatParm);
                        fullValue = value.ToString();
                        break;
                    case "ARRAY12":
                        string[] temp = FieldName.Split('|');
                        column = int.Parse(temp[1]);
                        row = ((object[])value).Length / column;
                        arrayTemp = new object[column];
                        for (int j = 0; j < row; j++)
                        {
                            Array.Copy((object[])value, j * column + 1, arrayTemp, 0, column);
                            fullValue += CreateMessageXML(tran, temp[0], DataSub, arrayTemp, objRow);
                        }
                        value = fullValue;
                        FormatFieldValue(ref value, FormatType, FormatObject, FormatFunction, FormatParm);
                        fullValue = value.ToString();
                        break;
                    case "DATATABLE":
                        DataTable dataTemp = (DataTable)(value);
                        for (int j = 0; j < dataTemp.Rows.Count; j++)
                        {
                            fullValue += CreateMessageXML(tran, FieldName, DataSub, ParmSub, dataTemp.Rows[j]);
                        }
                        value = fullValue;
                        FormatFieldValue(ref value, FormatType, FormatObject, FormatFunction, FormatParm);
                        fullValue = value.ToString();
                        break;
                    case "HASHTABLE":
                        Hashtable hashTemp = (Hashtable)(value);
                        value = CreateMessageXML(tran, FieldName, hashTemp, ParmSub, objRow);
                        FormatFieldValue(ref value, FormatType, FormatObject, FormatFunction, FormatParm);
                        fullValue = value.ToString();
                        break;
                    case "NOCDATA":
                        FormatFieldValue(ref value, FormatType, FormatObject, FormatFunction, FormatParm);
                        fullValue = String.Format("<{0}>{1}</{0}>", FieldName, value);
                        break;
                    case "CONDITION":
                        if (!Common.CheckCondition(tran, FormatParm))
                        {
                            fullValue = "";
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return fullValue;
        }

        public static bool AnalyzeRequestXML(TransactionInfo tran)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(tran.InputData);
                AnalyzeXMLNode(tran, doc.FirstChild);
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
            finally
            {
                doc = null;
            }
            return true;
        }

        public static string CreateResponseXML(TransactionInfo tran)
        {
            try
            {
                MappingSourceErrorCode(tran);
                tran.OutputData = "<IPCRESULT>";
                tran.OutputData += "<IPCDATA>";
                tran.OutputData += CreateMessageXML(tran, tran.Data[Common.KEYNAME.IPCTRANCODE].ToString(),
                                                                            tran.Data, tran.parm, null);
                tran.OutputData += "</IPCDATA>";
                tran.OutputData += "<IPCMESSAGEINFO>";
                tran.OutputData += tran.MessageInfo;
                tran.OutputData += "</IPCMESSAGEINFO>";
                tran.OutputData += "<IPCERRORCODE>";
                tran.OutputData += tran.ErrorCode;
                tran.OutputData += "</IPCERRORCODE>";
                tran.OutputData += "<IPCERRORDESC>";
                tran.OutputData += tran.ErrorDesc;
                tran.OutputData += "</IPCERRORDESC>";
                tran.OutputData += "</IPCRESULT>";
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return tran.OutputData;
        }

        public static bool CreateRequestXML(TransactionInfo tran)
        {
            try
            {
                tran.MessageTypeDest = Common.MESSAGETYPE.XML;
                if (tran.Data.ContainsKey(Common.KEYNAME.IPCTRANCODEDEST))
                {
                    tran.OutputData = CreateMessageXML(tran, tran.Data[Common.KEYNAME.IPCTRANCODEDEST].ToString(),
                                                                                tran.Data, tran.parm, null);
                }
                else
                {
                    tran.OutputData += CreateMessageXML(tran, tran.Data[Common.KEYNAME.IPCTRANCODE].ToString(),
                                                                                tran.Data, tran.parm, null);
                }

            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return true;
        }
        public static bool AnalyzeResultXML(TransactionInfo tran)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(tran.InputData);
                try
                {
                    doc.LoadXml(doc.LastChild.InnerText);
                    AnalyzeXMLNode(tran, doc.FirstChild);
                }
                catch //for string result, not XML
                {
                    doc = new XmlDocument();
                    doc.LoadXml(tran.InputData);
                    if (tran.Data.ContainsKey(Common.KEYNAME.STRDESTRESULT))
                    {
                        tran.Data[Common.KEYNAME.STRDESTRESULT] = doc.LastChild.InnerText;
                    }
                    else
                    {
                        tran.Data.Add(Common.KEYNAME.STRDESTRESULT, doc.LastChild.InnerText);
                    }
                }
                //Begin Hard return dest error code = desterrorcode + 500
                try
                {
                    if (tran.Data.ContainsKey(Common.KEYNAME.ERRORCODE))
                    {
                        if (double.Parse(tran.Data[Common.KEYNAME.ERRORCODE].ToString()) != 0)
                        {
                            tran.ErrorCode = (500 + double.Parse(tran.Data[Common.KEYNAME.ERRORCODE].ToString())).ToString();
                            tran.ErrorDesc = tran.Data[Common.KEYNAME.ERRORDESC].ToString();
                        }
                        tran.Data[Common.KEYNAME.DESTERRORCODE] = tran.Data[Common.KEYNAME.ERRORCODE];
                    }
                    if (tran.Data.ContainsKey(Common.KEYNAME.IPCERRORCODE))
                    {
                        if (double.Parse(tran.Data[Common.KEYNAME.IPCERRORCODE].ToString()) != 0)
                        {
                            //add 
                            tran.ErrorCode = (double.Parse(tran.Data[Common.KEYNAME.IPCERRORCODE].ToString())).ToString().PadRight(7, '0');
                            tran.ErrorDesc = tran.Data[Common.KEYNAME.IPCERRORDESC].ToString();
                        }
                        tran.Data[Common.KEYNAME.DESTERRORCODE] = tran.Data[Common.KEYNAME.IPCERRORCODE];
                    }
                }
                catch (Exception ex)
                {
                    Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                }

            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
            finally
            {
                doc = null;
            }
            return true;
        }

        public static bool AnalyzeResponseXML(TransactionInfo tran)
        {
            XmlDocument doc = new XmlDocument();
            string trancode = string.Empty;
            try
            {
                doc.LoadXml(tran.InputData);
                try
                {
                    doc.LoadXml(doc.LastChild.InnerText);
                    AnalyzeXMLNode(tran, doc.FirstChild);
                }
                catch //for string result, not XML
                {
                    doc = new XmlDocument();
                    doc.LoadXml(tran.InputData);
                    if (tran.Data.ContainsKey(Common.KEYNAME.STRDESTRESULT))
                    {
                        tran.Data[Common.KEYNAME.STRDESTRESULT] = doc.LastChild.InnerText;
                    }
                    else
                    {
                        tran.Data.Add(Common.KEYNAME.STRDESTRESULT, doc.LastChild.InnerText);
                    }
                }
                //Begin Hard return dest error code = desterrorcode + 500
                try
                {
                    trancode = tran.Data[Common.KEYNAME.IPCTRANCODE].ToString();
                    DataRow[] mapping = Common.DBIINPUTDEFINEXML.Select("IPCTRANCODE = '" + tran.Data[Common.KEYNAME.IPCTRANCODE].ToString() +
                        "' OR IPCTRANCODE = '' AND IPCDESTTRANCODE = '" + tran.Data[Common.KEYNAME.IPCTRANCODEDEST].ToString() + "'");
                    if (mapping.Length > 0)
                    {
                        try
                        {
                            foreach (DataRow row in mapping)
                            {
                                if (tran.Data.ContainsKey(row["KEYNAME"].ToString()))
                                {
                                    tran.Data[row["KEYNAME"].ToString()] = doc.GetElementsByTagName(row["TAGNAME"].ToString())[0].InnerText;
                                }
                                else
                                {
                                    tran.Data.Add(row["KEYNAME"].ToString(), doc.GetElementsByTagName(row["TAGNAME"].ToString())[0].InnerText);
                                }
                            }
                        }
                        catch { }
                    }
                    if (tran.Data.ContainsKey(Common.KEYNAME.ERRORCODE))
                    {
                        //vutran 24062015: hardcode hub error message, ben HUB ko sua duoc ben IB danh sua vay
                        if (tran.Data[Common.KEYNAME.ERRORCODE].ToString().Contains(Common.KEYNAME.HUBERRORCODEPREFIX))
                        {
                            tran.Data[Common.KEYNAME.ERRORCODE] = tran.Data[Common.KEYNAME.ERRORCODE].ToString().Replace(Common.KEYNAME.HUBERRORCODEPREFIX, "");
                        }

                        //VuTT 20180530 add map error code
                        tran.ErrorCode = tran.Data[Common.KEYNAME.ERRORCODE].ToString();
                        tran.MappingDestErrorCode();

                        //cong them 500 tranh trung ma loi he thong
                        if (double.Parse(tran.Data[Common.KEYNAME.ERRORCODE].ToString()) != 0)
                        {
                            tran.ErrorCode = (500 + double.Parse(tran.Data[Common.KEYNAME.ERRORCODE].ToString())).ToString();
                            tran.ErrorDesc = tran.Data[Common.KEYNAME.ERRORDESC].ToString();
                        }
                        //tran.Data[Common.KEYNAME.DESTERRORCODE] = tran.Data[Common.KEYNAME.ERRORCODE];
                    }
                    //if (tran.Data.ContainsKey(Common.KEYNAME.IPCERRORCODE))
                    //{
                    //    if (double.Parse(tran.Data[Common.KEYNAME.IPCERRORCODE].ToString()) != 0)
                    //    {
                    //        //add 
                    //        tran.ErrorCode = (double.Parse(tran.Data[Common.KEYNAME.IPCERRORCODE].ToString())).ToString().PadRight(6, '0');
                    //        tran.ErrorDesc = tran.Data[Common.KEYNAME.IPCERRORDESC].ToString();
                    //    }
                    //    tran.Data[Common.KEYNAME.DESTERRORCODE] = tran.Data[Common.KEYNAME.IPCERRORCODE];
                    //}
                }
                catch (Exception ex)
                {
                    Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name + "IPCTRANCODE=" + trancode);
                }

            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name + "IPCTRANCODE=" + trancode);
                return false;
            }
            finally
            {
                doc = null;
            }
            return true;
        }
        #endregion

        #region Public Function ISO
        private static string FormatLogMessageInfo(string value)
        {
            string result = value;
            try
            {
                result = result + new string('\t', 5 - (int)(result.Length / 8)) + ": ";
            }
            catch
            {
            }
            return result;
        }

        private static string CreateMessageISO(TransactionInfo tran, string TranCode,
                                    Hashtable DataSub, object ParmSub, DataRow objRow)
        {
            string result = "";
            try
            {
                string condition = " SOURCEID = '" + tran.Data[Common.KEYNAME.SOURCEID].ToString() + "'";
                condition += " AND DESTID = '" + tran.Data[Common.KEYNAME.DESTID].ToString() + "'";
                condition += " AND IPCTRANCODE = '" + TranCode + "'";
                condition += " AND ONLINE = " + (tran.Online == true ? "1" : "0");
                DataRow[] dr = Common.DBIOUTPUTDEFINEISO.Select(condition, "FIELDNO");
                for (int i = 0; i < dr.Length; i++)
                {
                    string FieldNo = dr[i]["FIELDNO"].ToString();
                    string FieldDesc = dr[i]["FIELDDESC"].ToString();
                    string FieldStyle = dr[i]["FIELDSTYLE"].ToString();
                    string FieldName = dr[i]["FIELDNAME"].ToString();
                    string ValueStyle = dr[i]["VALUESTYLE"].ToString();
                    string ValueName = dr[i]["VALUENAME"].ToString();
                    // Get Value
                    object value = GetFieldValue(tran, ValueStyle, ValueName, DataSub, ParmSub, objRow);
                    if (value == null) value = "";
                    // Get Full Value
                    value = GetFullFieldValueISO(tran, value, FieldStyle, FieldName, dr[i]["FORMATTYPE"].ToString(),
                                    dr[i]["FORMATOBJECT"].ToString(), dr[i]["FORMATFUNCTION"].ToString(),
                                    dr[i]["FORMATPARM"].ToString(), DataSub, ParmSub, objRow);
                    result += value.ToString();
                    if (TranCode == tran.Data[Common.KEYNAME.IPCTRANCODE].ToString())
                    {
                        string MessageInfoTemp = "[" + FieldNo.PadLeft(3, '0') + "]";
                        if (FieldDesc != "")
                        {
                            MessageInfoTemp += "[" + FieldDesc + "]";
                        }
                        tran.MessageInfo += FormatLogMessageInfo(MessageInfoTemp) + value.ToString() + "\r\n";
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return result;
        }

        private static string GetFullFieldValueISO(TransactionInfo tran, object value, string FieldStyle,
                        string FieldName, string FormatType, string FormatObject, string FormatFunction,
                        string FormatParm, Hashtable DataSub, object ParmSub, DataRow objRow)
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
                        fullValue = value.ToString();
                        break;
                    case "ARRAY1":
                        fullValue = ((object[])value).Length.ToString().PadLeft(2, '0');
                        fullValue += CreateMessageISO(tran, FieldName, DataSub, value, objRow);
                        value = fullValue;
                        FormatFieldValue(ref value, FormatType, FormatObject, FormatFunction, FormatParm);
                        fullValue = value.ToString();
                        break;
                    case "ARRAY2":
                        int row = ((object[,])value).GetLength(0);
                        int column = ((object[,])value).GetLength(1);
                        object[] arrayTemp = new object[column];
                        //
                        fullValue = row.ToString().PadLeft(2, '0');
                        for (int j = 0; j < row; j++)
                        {
                            for (int k = 0; k < row; k++)
                            {
                                arrayTemp[k] = ((object[,])value)[j, k];
                            }
                            fullValue += CreateMessageISO(tran, FieldName, DataSub, arrayTemp, objRow);
                        }
                        value = fullValue;
                        FormatFieldValue(ref value, FormatType, FormatObject, FormatFunction, FormatParm);
                        fullValue = value.ToString();
                        break;
                    case "ARRAY12":
                        string[] temp = FieldName.Split('|');
                        column = int.Parse(temp[1]);
                        row = ((object[])value).Length / column;
                        arrayTemp = new object[column];
                        //
                        fullValue = row.ToString().PadLeft(2, '0');
                        for (int j = 0; j < row; j++)
                        {
                            Array.Copy((object[])value, j * column + 1, arrayTemp, 0, column);
                            fullValue += CreateMessageISO(tran, temp[0], DataSub, arrayTemp, objRow);
                        }
                        value = fullValue;
                        FormatFieldValue(ref value, FormatType, FormatObject, FormatFunction, FormatParm);
                        fullValue = value.ToString();
                        break;
                }
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return fullValue;
        }

        public static bool AnalyzeRequestISO(TransactionInfo tran)
        {
            int rowindex;
            try
            {
                tran.MessageInfo = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff: ");
                tran.MessageInfo += "<<<<<<<<<<<<<<<<<<<<IPCService Begin Process Message>>>>>>>>>>>>>>>>>>>\r\n";
                tran.MessageInfo += "INPUT: " + tran.InputData + "\r\n";
                // Hard Code To Get Message Type And ProcessCode And BitMap
                string MsgType = tran.InputData.Substring(7, 4);
                string ProcessCode = "";
                string BitMap = "11111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111";
                // Get Input Define ISO
                string filter = String.Format("MSGTYPE = '{0}' AND PROCESSCODE = '{1}'", MsgType, ProcessCode);
                DataRow[] row = Common.DBIINPUTDEFINEISO.Select(filter, "FIELDNO");
                // Analyze Message
                string InputDataTemp = tran.InputData;
                string MapIPCTranCode = "";
                object FieldValue;
                for (rowindex = 0; rowindex < row.Length; rowindex++)
                {
                    int FieldNo = (int)row[rowindex]["FIELDNO"];
                    if (BitMap.Substring(FieldNo, 1) == "1")
                    {
                        string MessageInfoTemp = "[" + FieldNo.ToString().PadLeft(3, '0') + "]";
                        // Get Field Value
                        FieldValue = null;
                        if ((bool)row[rowindex]["FIXLENGTH"])
                        {
                            FieldValue = InputDataTemp.Substring(0, (int)row[rowindex]["LENGTH"]);
                            InputDataTemp = InputDataTemp.Remove(0, (int)row[rowindex]["LENGTH"]);
                        }
                        else
                        {
                            int FieldLen = int.Parse(InputDataTemp.Substring(0, (int)row[rowindex]["LENGTH"]));
                            InputDataTemp = InputDataTemp.Remove(0, (int)row[rowindex]["LENGTH"]);
                            FieldValue = InputDataTemp.Substring(0, FieldLen);
                            InputDataTemp = InputDataTemp.Remove(0, FieldLen);
                        }
                        // Format Field Value
                        FormatFieldValue(ref FieldValue, row[rowindex]["FORMATTYPE"].ToString(), row[rowindex]["FORMATOBJECT"].ToString(),
                                                    row[rowindex]["FORMATFUNCTION"].ToString(), row[rowindex]["FORMATPARM"].ToString());
                        // Add Field Value To Data
                        if (row[rowindex]["FIELDMAP"] != null && row[rowindex]["FIELDMAP"].ToString() != "")
                        {
                            tran.Data[row[rowindex]["FIELDMAP"]] = FieldValue;
                            MessageInfoTemp += "[" + row[rowindex]["FIELDMAP"] + "]";
                        }
                        tran.MessageInfo += FormatLogMessageInfo(MessageInfoTemp) + FieldValue + "\r\n";
                        // Check Mapping IPCTranCode
                        if ((bool)row[rowindex]["ISMAPIPCTRANCODE"])
                        {
                            MapIPCTranCode += FieldValue;
                        }
                    }
                }
                // Get Mapping IPCTranCode
                row = Common.DBIMAPIPCTRANCODEISO.Select(String.Format("MAPVALUE = '{0}'", MapIPCTranCode));
                if (row.Length > 0)
                {
                    tran.Data[Common.KEYNAME.IPCTRANCODE] = row[0][Common.KEYNAME.IPCTRANCODE];
                    tran.Data[Common.KEYNAME.MSGTYPERP] = row[0][Common.KEYNAME.MSGTYPERP];
                    tran.Data[Common.KEYNAME.PROCESSCODERP] = row[0][Common.KEYNAME.PROCESSCODERP];
                    tran.MessageInfo += FormatLogMessageInfo("[999][IPCTRANCODE]") + tran.Data[Common.KEYNAME.IPCTRANCODE] + "\r\n";
                }
                else
                {
                    // Hard code to check reversal transaction
                    if (tran.Data[Common.KEYNAME.MSGTYPE].ToString() == "RVRQ")
                    {
                        tran.Data[Common.KEYNAME.MSGTYPERP] = "RVRP";
                        tran.Data[Common.KEYNAME.PROCESSCODERP] = "";
                        tran.Data[Common.KEYNAME.REVERSAL] = "Y";
                        Connection con = new Connection();
                        string sql = "SELECT IPCTRANCODE,NUM01 FROM IPCLOGTRANS WHERE SOURCEID = '{0}' AND SOURCETRANREF = '{1}'";
                        sql = String.Format(sql, tran.Data[Common.KEYNAME.SOURCEID].ToString(), tran.Data[Common.KEYNAME.SOURCETRANREF].ToString());
                        DataTable result = (DataTable)con.FillDataTableSQL(Common.ConStr, sql);
                        con = null;
                        if (result == null || result.Rows.Count <= 0)
                        {
                            tran.Data[Common.KEYNAME.IPCTRANCODE] = "RVRP";
                            tran.Data[Common.KEYNAME.DESTID] = "RBS";
                            tran.ErrorCode = Common.ERRORCODE.SOURCETRANREF_NOTEXIST; // So Ref can Reversal ko ton tai
                            tran.ErrorDesc = "Source transaction reference number is not exist";
                            return false;
                        }
                        else
                        {
                            double revAmount = Convert.ToDouble(tran.Data["AMOUNT"].ToString());
                            double oriAmount = Convert.ToDouble(result.Rows[0]["NUM01"].ToString());
                            if (revAmount == oriAmount)
                            {
                                tran.Data[Common.KEYNAME.IPCTRANCODE] = result.Rows[0]["IPCTRANCODE"].ToString();
                                tran.MessageInfo += FormatLogMessageInfo("[999][IPCTRANCODE]") + tran.Data[Common.KEYNAME.IPCTRANCODE] + "\r\n";
                            }
                            else
                            {
                                tran.Data[Common.KEYNAME.IPCTRANCODE] = "RVRP";
                                tran.Data[Common.KEYNAME.DESTID] = "RBS";
                                tran.ErrorCode = Common.ERRORCODE.INVALID_AMOUNT; // So tien can reversal khong dung
                                tran.ErrorDesc = "The reversal amount is invalid";
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
            finally
            {
                tran.MessageInfo += "<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<************************>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>\r\n";
                if (tran.Data[Common.KEYNAME.IPCTRANCODE] == null || tran.Data[Common.KEYNAME.IPCTRANCODE].ToString() == "")
                {
                    if (tran.Data[Common.KEYNAME.MSGTYPE] != null && tran.Data[Common.KEYNAME.MSGTYPE].ToString() != "")
                    {
                        DataRow[] row = Common.DBIMAPIPCTRANCODEISO.Select(String.Format("MAPVALUE = '{0}'", tran.Data[Common.KEYNAME.MSGTYPE]));
                        if (row.Length > 0)
                        {
                            tran.Data[Common.KEYNAME.IPCTRANCODE] = row[0][Common.KEYNAME.MSGTYPERP];
                            tran.Data[Common.KEYNAME.MSGTYPERP] = row[0][Common.KEYNAME.MSGTYPERP];
                            tran.Data[Common.KEYNAME.DESTID] = "RBS";
                            tran.Data[Common.KEYNAME.PROCESSCODERP] = row[0][Common.KEYNAME.PROCESSCODERP];
                            tran.MessageInfo += FormatLogMessageInfo("[999][IPCTRANCODE]") + tran.Data[Common.KEYNAME.IPCTRANCODE] + "\r\n";
                        }
                    }
                    if (tran.Data[Common.KEYNAME.IPCTRANCODE] == null)
                    {
                        tran.Data[Common.KEYNAME.IPCTRANCODE] = "";
                        tran.Data[Common.KEYNAME.MSGTYPERP] = "";
                        tran.Data[Common.KEYNAME.PROCESSCODERP] = "";
                        tran.Data[Common.KEYNAME.DESTID] = "";
                    }
                    tran.ErrorCode = Common.ERRORCODE.INVALID_MESSAGE_REQUEST;
                    tran.ErrorDesc = "Invalid message request";
                }
            }
            return true;
        }

        public static string CreateResponseISO(TransactionInfo tran)
        {
            MappingSourceErrorCode(tran);
            string MessgeInfoTemp = tran.MessageInfo;
            tran.MessageInfo = "";

            try
            {
                // Hard Code Nhieu IPCTranCode tuong ung voi 1 Message Response
                tran.Data[Common.KEYNAME.IPCTRANCODE] = tran.Data[Common.KEYNAME.MSGTYPERP];

                // Get Output Data
                tran.OutputData = CreateMessageISO(tran, tran.Data[Common.KEYNAME.IPCTRANCODE].ToString(),
                                                                            tran.Data, tran.parm, null);
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                tran.MessageInfo = MessgeInfoTemp + "OUTPUT: " + tran.OutputData + "\r\n" + tran.MessageInfo;
                if (tran.ErrorCode != Common.ERRORCODE.OK)
                {
                    tran.MessageInfo += FormatLogMessageInfo("[999][ERRORDESC]") + tran.ErrorDesc + "\r\n";
                }
                tran.MessageInfo += DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff: ");
                tran.MessageInfo += "<<<<<<<<<<<<<<<<<<<<IPCService End  Process Message>>>>>>>>>>>>>>>>>>>>\r\n";
                tran.MessageInfo += "************************************************************************************************";
            }
            if (tran.OutputData == "") tran.OutputData = tran.InputData;
            return tran.OutputData;
        }

        public static bool CreateRequestISO(TransactionInfo tran)
        {
            try
            {
                string MessageInfoTemp = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff: ");
                MessageInfoTemp += "<<<<<<<<<<<<<<<<<<<<IPCService Begin Create Message>>>>>>>>>>>>>>>>>>>>\r\n";
                MessageInfoTemp += "OUTPUT: ";
                // Get Output Data
                tran.MessageTypeDest = Common.MESSAGETYPE.ISO;
                tran.OutputData = CreateMessageISO(tran, tran.Data[Common.KEYNAME.IPCTRANCODE].ToString(),
                                                                            tran.Data, tran.parm, null);
                tran.MessageInfo = MessageInfoTemp + tran.OutputData + "\r\n" + tran.MessageInfo;
                tran.MessageInfo += "<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<************************>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>\r\n";
                return true;
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
        }

        public static bool AnalyzeResponseISO(TransactionInfo tran)
        {
            int rowindex;
            try
            {
                tran.MessageInfo += "INPUT: " + tran.InputData + "\r\n";
                // Hard Code To Get Message Type And ProcessCode And BitMap
                string MsgType = tran.InputData.Substring(7, 4);
                string ProcessCode = "";
                string BitMap = "11111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111";
                // Get Input Define ISO
                string filter = String.Format("MSGTYPE = '{0}' AND PROCESSCODE = '{1}'", MsgType, ProcessCode);
                DataRow[] row = Common.DBIINPUTDEFINEISO.Select(filter, "FIELDNO");
                // Analyze Message
                string InputDataTemp = tran.InputData;
                object FieldValue;
                for (rowindex = 0; rowindex < row.Length; rowindex++)
                {
                    int FieldNo = (int)row[rowindex]["FIELDNO"];
                    if (BitMap.Substring(FieldNo, 1) == "1")
                    {
                        string MessageInfoTemp = "[" + FieldNo.ToString().PadLeft(3, '0') + "]";
                        // Get Field Value
                        FieldValue = null;
                        if ((bool)row[rowindex]["FIXLENGTH"])
                        {
                            FieldValue = InputDataTemp.Substring(0, (int)row[rowindex]["LENGTH"]);
                            InputDataTemp = InputDataTemp.Remove(0, (int)row[rowindex]["LENGTH"]);
                        }
                        else
                        {
                            int FieldLen = int.Parse(InputDataTemp.Substring(0, (int)row[rowindex]["LENGTH"]));
                            InputDataTemp = InputDataTemp.Remove(0, (int)row[rowindex]["LENGTH"]);
                            FieldValue = InputDataTemp.Substring(0, FieldLen);
                            InputDataTemp = InputDataTemp.Remove(0, FieldLen);
                        }
                        // Format Field Value
                        FormatFieldValue(ref FieldValue, row[rowindex]["FORMATTYPE"].ToString(), row[rowindex]["FORMATOBJECT"].ToString(),
                                                    row[rowindex]["FORMATFUNCTION"].ToString(), row[rowindex]["FORMATPARM"].ToString());
                        // Add Field Value To Data
                        if (row[rowindex]["FIELDMAP"] != null && row[rowindex]["FIELDMAP"].ToString() != "")
                        {
                            tran.Data[row[rowindex]["FIELDMAP"]] = FieldValue;
                            MessageInfoTemp += "[" + row[rowindex]["FIELDMAP"] + "]";
                        }
                        tran.MessageInfo += FormatLogMessageInfo(MessageInfoTemp) + FieldValue + "\r\n";
                    }
                }
                // Mapping Dest Error Code
                if (tran.Data.ContainsKey(Common.KEYNAME.ERRORCODE))
                {
                    tran.ErrorCode = tran.Data[Common.KEYNAME.ERRORCODE].ToString();
                    tran.MappingDestErrorCode();
                }
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
            finally
            {
                tran.MessageInfo += DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff: ");
                tran.MessageInfo += "<<<<<<<<<<<<<<<<<<<<IPCService End  Analyze Message>>>>>>>>>>>>>>>>>>>>\r\n";
                tran.MessageInfo += "************************************************************************************************";
                Utility.ProcessLog.LogATMDetail(tran.MessageInfo);
            }
            return true;
        }
        #endregion

        #region Public Function SEP
        private static string CreateMessageSEP(TransactionInfo tran, string TranCode,
                                    Hashtable DataSub, object ParmSub, DataRow objRow)
        {
            string result = "";
            try
            {
                string condition = " SOURCEID = '" + tran.Data[Common.KEYNAME.SOURCEID].ToString() + "'";
                condition += " AND DESTID = '" + tran.Data[Common.KEYNAME.DESTID].ToString() + "'";
                condition += " AND IPCTRANCODE = '" + TranCode + "'";
                condition += " AND ONLINE = " + (tran.Online == true ? "1" : "0");
                DataRow[] dr = Common.DBIOUTPUTDEFINESEP.Select(condition, "FIELDNO");
                for (int i = 0; i < dr.Length; i++)
                {
                    string FieldNo = dr[i]["FIELDNO"].ToString();
                    string FieldDesc = dr[i]["FIELDDESC"].ToString();
                    string FieldStyle = dr[i]["FIELDSTYLE"].ToString();
                    string FieldName = dr[i]["FIELDNAME"].ToString();
                    string ValueStyle = dr[i]["VALUESTYLE"].ToString();
                    string ValueName = dr[i]["VALUENAME"].ToString();
                    // Get Value
                    object value = GetFieldValue(tran, ValueStyle, ValueName, DataSub, ParmSub, objRow);
                    // Get Full Value
                    value = GetFullFieldValueSEP(tran, value, FieldStyle, FieldName, dr[i]["FORMATTYPE"].ToString(),
                                    dr[i]["FORMATOBJECT"].ToString(), dr[i]["FORMATFUNCTION"].ToString(),
                                    dr[i]["FORMATPARM"].ToString(), DataSub, ParmSub, objRow);
                    result += value.ToString();
                }
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return result;
        }

        private static string GetFullFieldValueSEP(TransactionInfo tran, object value, string FieldStyle,
                        string FieldName, string FormatType, string FormatObject, string FormatFunction,
                        string FormatParm, Hashtable DataSub, object ParmSub, DataRow objRow)
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
                        fullValue = FieldName + "$" + value.ToString() + "#";
                        break;
                    case "ARRAY1":
                        value = CreateMessageSEP(tran, FieldName, DataSub, value, objRow);
                        FormatFieldValue(ref value, FormatType, FormatObject, FormatFunction, FormatParm);
                        fullValue = value.ToString();
                        break;
                    case "ARRAY2":
                        int row = ((object[,])value).GetLength(0);
                        int column = ((object[,])value).GetLength(1);
                        object[] arrayTemp = new object[column];
                        //
                        for (int j = 0; j < row; j++)
                        {
                            for (int k = 0; k < row; k++)
                            {
                                arrayTemp[k] = ((object[,])value)[j, k];
                            }
                            fullValue += CreateMessageSEP(tran, FieldName, DataSub, arrayTemp, objRow);
                        }
                        value = fullValue;
                        FormatFieldValue(ref value, FormatType, FormatObject, FormatFunction, FormatParm);
                        fullValue = value.ToString();
                        break;
                    case "ARRAY12":
                        string[] temp = FieldName.Split('|');
                        column = int.Parse(temp[1]);
                        row = ((object[])value).Length / column;
                        arrayTemp = new object[column];
                        //
                        fullValue = row.ToString().PadLeft(2, '0');
                        for (int j = 0; j < row; j++)
                        {
                            Array.Copy((object[])value, j * column + 1, arrayTemp, 0, column);
                            fullValue += CreateMessageSEP(tran, temp[0], DataSub, arrayTemp, objRow);
                        }
                        value = fullValue;
                        FormatFieldValue(ref value, FormatType, FormatObject, FormatFunction, FormatParm);
                        fullValue = value.ToString();
                        break;
                    case "DATATABLE":
                        DataTable dataTemp = (DataTable)(value);
                        for (int j = 0; j < dataTemp.Rows.Count; j++)
                        {
                            fullValue += CreateMessageSEP(tran, FieldName, DataSub, ParmSub, dataTemp.Rows[j]);
                        }
                        value = fullValue;
                        FormatFieldValue(ref value, FormatType, FormatObject, FormatFunction, FormatParm);
                        fullValue = value.ToString();
                        break;
                    case "HASHTABLE":
                        Hashtable hashTemp = (Hashtable)(value);
                        value = CreateMessageSEP(tran, FieldName, hashTemp, ParmSub, objRow);
                        FormatFieldValue(ref value, FormatType, FormatObject, FormatFunction, FormatParm);
                        fullValue = value.ToString();
                        break;
                }
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return fullValue;
        }

        public static bool AnalyzeRequestSEP(TransactionInfo tran)
        {
            try
            {
                string[] ListField = tran.InputData.Split('#');
                for (int i = 0; i < ListField.Length; i++)
                {
                    string[] FieldInfo = ListField[i].Split('$');
                    switch (FieldInfo.Length)
                    {
                        case 2:
                            tran.Data.Add(FieldInfo[0], FieldInfo[1]);
                            break;
                        case 1:
                            tran.Data.Add(FieldInfo[0], "");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
            return true;
        }

        public static string CreateResponseSEP(TransactionInfo tran)
        {
            try
            {
                MappingSourceErrorCode(tran);
                tran.OutputData = "";
                tran.OutputData += Common.KEYNAME.IPCERRORCODE + "$" + tran.ErrorCode + "#";
                tran.OutputData += Common.KEYNAME.IPCERRORDESC + "$" + tran.ErrorDesc + "#";
                tran.OutputData += CreateMessageSEP(tran, tran.Data[Common.KEYNAME.IPCTRANCODE].ToString(),
                                                                            tran.Data, tran.parm, null);
                tran.OutputData = tran.OutputData.Substring(0, tran.OutputData.Length - 1);
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return tran.OutputData;
        }
        #endregion

        #region Public Function SMS
        private static string CreateMessageSMS(TransactionInfo tran, string TranCode,
                                    Hashtable DataSub, object ParmSub, DataRow objRow)
        {
            string result = "";
            try
            {
                string condition = " SOURCEID = '" + tran.Data[Common.KEYNAME.SOURCEID].ToString() + "'";
                condition += " AND DESTID = '" + tran.Data[Common.KEYNAME.DESTID].ToString() + "'";
                condition += " AND IPCTRANCODE = '" + TranCode + "'";
                condition += " AND ONLINE = " + (tran.Online == true ? "1" : "0");
                DataRow[] dr = Common.DBIOUTPUTDEFINESMS.Select(condition, "FIELDNO");
                if (dr.Length <= 0)
                {
                    tran.ErrorCode = Common.ERRORCODE.SYSTEM;
                    return result;
                }
                result = tran.Data[Common.KEYNAME.RESPONSETEMPLATE].ToString();
                for (int i = 0; i < dr.Length; i++)
                {
                    string FieldNo = dr[i]["FIELDNO"].ToString();
                    string FieldDesc = dr[i]["FIELDDESC"].ToString();
                    string FieldStyle = dr[i]["FIELDSTYLE"].ToString();
                    string FieldName = dr[i]["FIELDNAME"].ToString();
                    string ValueStyle = dr[i]["VALUESTYLE"].ToString();
                    string ValueName = dr[i]["VALUENAME"].ToString();
                    // Get Value
                    object value = GetFieldValue(tran, ValueStyle, ValueName, DataSub, ParmSub, objRow);
                    if ((value == null || value.ToString().Equals("")) && tran.Data[Common.KEYNAME.IPCTRANCODE].ToString() != "SMS00007")
                    {
                        if (tran.ErrorCode.Equals(Common.ERRORCODE.OK))
                        {
                            tran.ErrorCode = Common.ERRORCODE.SYSTEM;
                            DataRow[] errorMapping = Common.DBIERRORLISTSOURCE.Select(
                            "(SYSERRORCODE = '" + tran.ErrorCode + "' OR SYSERRORCODE = '" + Common.ERRORCODE.SYSTEM + "')" +
                            " AND (IPCTRANCODE = '" + tran.Data[Common.KEYNAME.SMSTRANCODE].ToString() + "' OR IPCTRANCODE = '') " +
                            " AND (SOURCEID = '" + tran.Data[Common.KEYNAME.SOURCEID].ToString() + "' OR SOURCEID = '')",
                                            "IPCTRANCODE DESC, SYSERRORCODE");
                            if (errorMapping.Length > 0)
                            {
                                result = errorMapping[0]["SOURCEERRORDESC"].ToString();
                            }
                            return result = Common.IPCWorkDate + " " + tran.IPCTransID + " " + result;
                        }
                    }
                    else
                    {
                        // Get Full Value
                        value = GetFullFieldValueSMS(tran, value, FieldStyle, FieldName, dr[i]["FORMATTYPE"].ToString(),
                                        dr[i]["FORMATOBJECT"].ToString(), dr[i]["FORMATFUNCTION"].ToString(),
                                        dr[i]["FORMATPARM"].ToString(), DataSub, ParmSub, objRow);
                        result = result.Replace("[" + FieldName + "]", value.ToString().Trim());
                    }
                }
                result = Common.IPCWorkDate + " " + Common.RemoveSign(result);
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return result;
        }

        private static string GetFullFieldValueSMS(TransactionInfo tran, object value, string FieldStyle,
                        string FieldName, string FormatType, string FormatObject, string FormatFunction,
                        string FormatParm, Hashtable DataSub, object ParmSub, DataRow objRow)
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
                    case "ARRAY1":
                        value = CreateMessageSMS(tran, FieldName, DataSub, value, objRow);
                        FormatFieldValue(ref value, FormatType, FormatObject, FormatFunction, FormatParm);
                        fullValue = value.ToString();
                        break;
                    case "ARRAY2":
                        int row = ((object[,])value).GetLength(0);
                        int column = ((object[,])value).GetLength(1);
                        object[] arrayTemp = new object[column];
                        //
                        for (int j = 0; j < row; j++)
                        {
                            for (int k = 0; k < row; k++)
                            {
                                arrayTemp[k] = ((object[,])value)[j, k];
                            }
                            fullValue += CreateMessageSMS(tran, FieldName, DataSub, arrayTemp, objRow);
                        }
                        value = fullValue;
                        FormatFieldValue(ref value, FormatType, FormatObject, FormatFunction, FormatParm);
                        fullValue = value.ToString();
                        break;
                    case "ARRAY12":
                        string[] temp = FieldName.Split('|');
                        column = int.Parse(temp[1]);
                        row = ((object[])value).Length / column;
                        arrayTemp = new object[column];
                        //
                        for (int j = 0; j < row; j++)
                        {
                            Array.Copy((object[])value, j * column + 1, arrayTemp, 0, column);
                            fullValue += CreateMessageSMS(tran, temp[0], DataSub, arrayTemp, objRow);
                        }
                        value = fullValue;
                        FormatFieldValue(ref value, FormatType, FormatObject, FormatFunction, FormatParm);
                        fullValue = value.ToString();
                        break;
                    case "DATATABLE":
                        DataTable dataTemp = (DataTable)(value);
                        for (int j = 0; j < dataTemp.Rows.Count; j++)
                        {
                            fullValue += CreateMessageSMS(tran, FieldName, DataSub, ParmSub, dataTemp.Rows[j]);
                        }
                        value = fullValue;
                        FormatFieldValue(ref value, FormatType, FormatObject, FormatFunction, FormatParm);
                        fullValue = value.ToString();
                        break;
                    case "HASHTABLE":
                        Hashtable hashTemp = (Hashtable)(value);
                        value = CreateMessageSMS(tran, FieldName, hashTemp, ParmSub, objRow);
                        FormatFieldValue(ref value, FormatType, FormatObject, FormatFunction, FormatParm);
                        fullValue = value.ToString();
                        break;
                    case "DATASET":
                        DataSet dtSetTemp = (DataSet)(value);
                        foreach (DataTable dt in dtSetTemp.Tables)
                        {
                            int a = 1;
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                fullValue = fullValue + " " + a++ + ">" + dt.Rows[i][FieldName].ToString();
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return fullValue;
        }

        public static bool AnalyzeRequestSMS(TransactionInfo tran)
        {
            try
            {
                string[] FieldInfo = tran.InputData.Split(' ');
                if (FieldInfo.Length < 7)
                {
                    tran.SetErrorInfo(Common.ERRORCODE.INVALID_MESSAGE_REQUEST, "");
                    return false;
                }
                tran.Data.Add(Common.KEYNAME.SOURCEID, FieldInfo[0].ToString());
                tran.Data.Add(Common.KEYNAME.PHONENO, FieldInfo[1].ToString());
                tran.Data.Add(Common.KEYNAME.MSGDATE, FieldInfo[2].ToString());
                tran.Data.Add(Common.KEYNAME.MSGTIME, FieldInfo[3].ToString());
                tran.Data.Add(Common.KEYNAME.MSGID, FieldInfo[4].ToString());
                tran.Data.Add(Common.KEYNAME.PREFIX, FieldInfo[5].ToString());
                // Get Input Define SMS
                string filter = String.Format("SMSTRANCODE = '{0}'", FieldInfo[6].ToString());
                DataRow[] dr = Common.DBIINPUTDEFINESMS.Select(filter, "FIELDNO");
                // AnalyzeMessage
                string MapIPCTranCode = "";
                for (int rowindex = 0; rowindex < dr.Length; rowindex++)
                {
                    if ((int)dr[rowindex]["FIELDNO"] + 4 < FieldInfo.Length)
                    {
                        object FieldValue = FieldInfo[(int)dr[rowindex]["FIELDNO"] + 4];
                        // Format Field Value
                        FormatFieldValue(ref FieldValue, dr[rowindex]["FORMATTYPE"].ToString(), dr[rowindex]["FORMATOBJECT"].ToString(),
                                                    dr[rowindex]["FORMATFUNCTION"].ToString(), dr[rowindex]["FORMATPARM"].ToString());
                        // Add Field Value To Data
                        if (dr[rowindex]["FIELDMAP"] != null && dr[rowindex]["FIELDMAP"].ToString() != "")
                        {
                            tran.Data[dr[rowindex]["FIELDMAP"]] = FieldValue;
                        }
                        // Check Mapping IPCTranCode
                        if ((bool)dr[rowindex]["ISMAPIPCTRANCODE"])
                        {
                            MapIPCTranCode += FieldValue;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                // Get Mapping IPCTranCode
                dr = Common.DBIMAPIPCTRANCODESMS.Select(String.Format("SMSTRANCODE = '{0}' AND NUMBERPARAM = '{1}'", MapIPCTranCode, FieldInfo.Length.ToString()));
                if (dr.Length > 0)
                {
                    tran.Data[Common.KEYNAME.IPCTRANCODE] = dr[0][Common.KEYNAME.IPCTRANCODE];
                    tran.Data[Common.KEYNAME.RESPONSETEMPLATE] = dr[0][Common.KEYNAME.RESPONSETEMPLATE];
                }
                else
                {
                    tran.Data[Common.KEYNAME.IPCTRANCODE] = "";
                    tran.ErrorCode = Common.ERRORCODE.INVALID_MESSAGE_REQUEST;
                    return false;
                }
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
            return true;
        }

        public static string CreateResponseSMS(TransactionInfo tran)
        {
            try
            {
                if (tran.ErrorCode == Common.ERRORCODE.INVALID_MESSAGE_REQUEST)
                {
                    return tran.OutputData = Common.IPCWorkDate + " " + tran.IPCTransID + " " + "Your message is invalid.";
                }

                if (tran.ErrorCode != Common.ERRORCODE.OK)
                {
                    if (tran.ErrorDesc.Equals(""))
                    {
                        tran.OutputData = "Transaction error.";
                    }
                    else
                    {
                        //return tran.OutputData = Common.IPCWorkDate + " " + tran.IPCTransID + " " + tran.ErrorDesc.ToString();
                    }

                    DataRow[] errorMapping = Common.DBIERRORLISTSOURCE.Select(
                    "(SYSERRORCODE = '" + tran.ErrorCode + "' OR SYSERRORCODE = '" + Common.ERRORCODE.SYSTEM + "')" +
                    " AND (IPCTRANCODE = '" + tran.Data[Common.KEYNAME.SMSTRANCODE].ToString() + "' OR IPCTRANCODE = '') " +
                    " AND (SOURCEID = '" + tran.Data[Common.KEYNAME.SOURCEID].ToString() + "' OR SOURCEID = '')",
                                            "IPCTRANCODE DESC, SYSERRORCODE");
                    if (errorMapping.Length > 0)
                    {
                        tran.OutputData = errorMapping[0]["SOURCEERRORDESC"].ToString();
                    }
                    else
                    {
                        tran.OutputData = tran.ErrorDesc.ToString();
                    }

                    tran.OutputData = Common.IPCWorkDate + " " + tran.IPCTransID + " " + tran.OutputData.ToString();

                }
                else
                {
                    tran.OutputData = CreateMessageSMS(tran, tran.Data[Common.KEYNAME.IPCTRANCODE].ToString(),
                                                                            tran.Data, tran.parm, null);
                }
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return tran.OutputData;
        }
        #endregion

        #region public function push notification
        public static Hashtable CreatePushNotification(TransactionInfo tran, string pushType)
        {
            try
            {
                // Get Output Data
                if (tran.ErrorCode.Equals(Common.ERRORCODE.OK))
                {
                    return CreateDataPushNotification(tran, tran.Data[Common.KEYNAME.IPCTRANCODE].ToString(),
                                                                    tran.Data, tran.parm, null, pushType);
                }
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return null;
        }
        private static Hashtable CreateDataPushNotification(TransactionInfo tran, string TranCode,
                                                Hashtable DataSub, object ParmSub, DataRow objRow, string pushType)
        {
            string _title = string.Empty;
            string _body = string.Empty;
            string _detail = string.Empty;
            string _data = string.Empty;
            string _img = string.Empty;
            string _url = string.Empty;
            string _group = string.Empty;
            string _action = string.Empty;
            Dictionary<string, string> dicData = new Dictionary<string, string>();

            try
            {
                //TrungTQ 20221005 send notification for autocashback
               
                DataRow[] drDesttran = Common.DBIMAPIPCTRANCODEPUSH.Select(String.Format("IPCTRANCODE = '{0}' AND PUSHTYPE = '{1}' AND IPCDESTTRANCODE = '{2}'", tran.Data[Common.KEYNAME.IPCTRANCODE].ToString().Trim(), pushType, tran.Data.ContainsKey(Common.KEYNAME.IPCTRANCODEDEST)  ? tran.Data[Common.KEYNAME.IPCTRANCODEDEST].ToString().Trim() : string.Empty ));

                DataRow[] drTemp = drDesttran.Length > 0 ? drDesttran: Common.DBIMAPIPCTRANCODEPUSH.Select(String.Format("IPCTRANCODE = '{0}' AND PUSHTYPE = '{1}'", tran.Data[Common.KEYNAME.IPCTRANCODE].ToString().Trim(), pushType));

                //String.IsNullOrEmpty(tran.Data[Common.KEYNAME.IPCTRANCODEDEST].ToString().Trim()) == true ? Common.DBIMAPIPCTRANCODEPUSH.Select(String.Format("IPCTRANCODE = '{0}' AND PUSHTYPE = '{1}'", tran.Data[Common.KEYNAME.IPCTRANCODE].ToString().Trim(), pushType)) : Common.DBIMAPIPCTRANCODEPUSH.Select(String.Format("IPCTRANCODE = '{0}' AND PUSHTYPE = '{1}' AND IPCDESTTRANCODE = '{2}'", tran.Data[Common.KEYNAME.IPCTRANCODE].ToString().Trim(), pushType, tran.Data[Common.KEYNAME.IPCTRANCODEDEST].ToString().Trim()));

                //DataRow[] drTemp = Common.DBIMAPIPCTRANCODEPUSH.Select(String.Format("IPCTRANCODE = '{0}' AND PUSHTYPE = '{1}'", tran.Data[Common.KEYNAME.IPCTRANCODE].ToString().Trim(), pushType));
                //DataRow[] drTemp = Common.DBIMAPIPCTRANCODEPUSH.Select(String.Format("IPCTRANCODE = '{0}' AND PUSHTYPE = '{1}' AND IPCDESTTRANCODE = '{2}'", tran.Data[Common.KEYNAME.IPCTRANCODE].ToString().Trim(), pushType, tran.Data[Common.KEYNAME.IPCTRANCODEDEST].ToString().Trim()));
                if (drTemp.Length > 0)
                {
                    _title = drTemp[0][Common.KEYNAME.TITLE].ToString();
                    _body = drTemp[0][Common.KEYNAME.BODY].ToString();
                    _detail = drTemp[0][Common.KEYNAME.DETAIL].ToString();
                    _group = drTemp[0][Common.KEYNAME.GROUP].ToString();
                    _action = drTemp[0][Common.KEYNAME.ACTION].ToString();
                    //_img = drTemp[0][Common.KEYNAME.IMGURL].ToString();
                    //_url = drTemp[0][Common.KEYNAME.LINK].ToString();
                }

                string condition = " SOURCEID = '" + tran.Data[Common.KEYNAME.SOURCEID].ToString().Trim() + "'";
                condition += " AND DESTID = '" + tran.Data[Common.KEYNAME.DESTID].ToString() + "'";
                condition += " AND IPCTRANCODE = '" + tran.Data[Common.KEYNAME.IPCTRANCODE].ToString() + "'";
                condition += " AND ISONLINE = " + (tran.Online == true ? "1" : "0");
                //TrungTQ 20221005 send notification for autocashback
                if (tran.Data.ContainsKey(Common.KEYNAME.IPCTRANCODEDEST))
                {
                    if (drDesttran.Length > 0 && !String.IsNullOrEmpty(tran.Data[Common.KEYNAME.IPCTRANCODEDEST].ToString().Trim()))
                    {
                        condition += " AND IPCDESTTRANCODE = '" + tran.Data[Common.KEYNAME.IPCTRANCODEDEST].ToString().Trim() + "'";
                    }
                }
                DataRow[] dr = Common.DBIOUTPUTDEFINEPUSH.Select(condition, "FIELDNO");
                


                for (int i = 0; i < dr.Length; i++)
                {
                    string FieldNo = dr[i]["FIELDNO"].ToString();
                    string FieldDesc = dr[i]["FIELDDESC"].ToString();
                    string FieldStyle = dr[i]["FIELDSTYLE"].ToString();
                    string FieldName = dr[i]["FIELDNAME"].ToString();
                    string ValueStyle = dr[i]["VALUESTYLE"].ToString();
                    string ValueName = dr[i]["VALUENAME"].ToString();
                    // Get Value
                    object value = GetFieldValue(tran, ValueStyle, ValueName, DataSub, tran.parm, objRow);

                    Formatters.Formatter.FormatFieldValue(ref value, dr[i]["FORMATTYPE"].ToString(), dr[i]["FORMATOBJECT"].ToString(), dr[i]["FORMATFUNCTION"].ToString(), dr[i]["FORMATPARM"].ToString());

                    _title = _title.Replace("{" + dr[i]["FIELDNAME"].ToString() + "}", value.ToString());
                    _body = _body.Replace("{" + dr[i]["FIELDNAME"].ToString() + "}", value.ToString());
                    _detail = _detail.Replace("{" + dr[i]["FIELDNAME"].ToString() + "}", value.ToString());

                    //_data += dr[i]["FIELDNAME"].ToString() + "$" + ((value == null) ? "" : value.ToString()) + "#";
                    dicData.Add(dr[i]["FIELDNAME"].ToString(), (value == null) ? "" : value.ToString());
                }

                //if (!string.IsNullOrEmpty(_data))
                //    _data = _data.Substring(0, _data.Length - 1);
                _data = JsonConvert.SerializeObject(dicData);

                Hashtable hsRs = new Hashtable();
                hsRs.Add(Common.KEYNAME.TITLE, _title);
                hsRs.Add(Common.KEYNAME.BODY, _body);
                hsRs.Add(Common.KEYNAME.DETAIL, _detail);
                hsRs.Add(Common.KEYNAME.DATA, _data);
                hsRs.Add(Common.KEYNAME.IMGURL, _img);
                hsRs.Add(Common.KEYNAME.LINK, _url);
                hsRs.Add(Common.KEYNAME.GROUP, _group);
                hsRs.Add(Common.KEYNAME.ACTION, _action);

                return hsRs;
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name + " " + tran.IPCTransID.ToString() + "IPC trancode=" + TranCode);
                return null;
            }
        }
        #endregion

        #region Public Function HAS
        private static void CreateDataHAS(Hashtable Output, TransactionInfo tran, string TranCode,
                                                Hashtable DataSub, object ParmSub, DataRow objRow)
        {
            try
            {
                string condition = " SOURCEID = '" + tran.Data[Common.KEYNAME.SOURCEID].ToString() + "'";
                condition += " AND DESTID = '" + tran.Data[Common.KEYNAME.DESTID].ToString() + "'";
                condition += " AND IPCTRANCODE = '" + TranCode + "'";
                condition += " AND ONLINE = " + (tran.Online == true ? "1" : "0");
                DataRow[] dr = Common.DBIOUTPUTDEFINEHAS.Select(condition, "FIELDNO");
                for (int i = 0; i < dr.Length; i++)
                {
                    string FieldNo = dr[i]["FIELDNO"].ToString();
                    string FieldDesc = dr[i]["FIELDDESC"].ToString();
                    string FieldStyle = dr[i]["FIELDSTYLE"].ToString();
                    string FieldName = dr[i]["FIELDNAME"].ToString();
                    string ValueStyle = dr[i]["VALUESTYLE"].ToString();
                    string ValueName = dr[i]["VALUENAME"].ToString();
                    // Get Value
                    object value = GetFieldValue(tran, ValueStyle, ValueName, DataSub, ParmSub, objRow);
                    // Get Full Value
                    GetFullFieldValueHAS(Output, tran, value, FieldStyle, FieldName, dr[i]["FORMATTYPE"].ToString(),
                                    dr[i]["FORMATOBJECT"].ToString(), dr[i]["FORMATFUNCTION"].ToString(),
                                    dr[i]["FORMATPARM"].ToString(), DataSub, ParmSub, objRow);

                    if (TranCode == tran.Data[Common.KEYNAME.IPCTRANCODE].ToString())
                    {
                        tran.OutputData += FieldName + "$" + value.ToString() + "#";
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name + " " + tran.IPCTransID.ToString() + "IPC trancode=" + TranCode);
            }
        }

        private static void GetFullFieldValueHAS(Hashtable Output, TransactionInfo tran, object value, string FieldStyle,
                        string FieldName, string FormatType, string FormatObject, string FormatFunction,
                        string FormatParm, Hashtable DataSub, object ParmSub, DataRow objRow)
        {
            try
            {
                switch (FieldStyle)
                {
                    case "VALUE":
                        FormatFieldValue(ref value, FormatType, FormatObject, FormatFunction, FormatParm);
                        Output[FieldName] = value;
                        break;
                    case "TAG":
                        FormatFieldValue(ref value, FormatType, FormatObject, FormatFunction, FormatParm);
                        Output[FieldName] = value;
                        break;
                }
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        public static bool AnalyzeRequestHAS(TransactionInfo tran, Hashtable InputData)
        {
            try
            {
                tran.InputData = "";
                foreach (string key in InputData.Keys)
                {
                    tran.Data.Add(key, InputData[key]);
                    tran.InputData += key + "$" +  (InputData[key] == null ? "" : InputData[key].ToString()) + "#";
                }
                if (tran.InputData.Length > 0)
                {
                    tran.InputData = tran.InputData.Substring(0, tran.InputData.Length - 1);
                }
                if (tran.Data.ContainsKey(Common.KEYNAME.SOURCEID) == false)
                {
                    tran.Data.Add(Common.KEYNAME.SOURCEID, Common.IPCINSTANCE);
                }
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
            return true;
        }

        public static Hashtable CreateResponseHAS(TransactionInfo tran)
        {
            Hashtable result = new Hashtable();
            try
            {
                string _lang = string.Empty;
                try
                {
                    if (tran.Data.ContainsKey(Common.KEYNAME.SOURCEID) && tran.Data.ContainsKey(Common.KEYNAME.USERID))
                    {
                        if (tran.Data[Common.KEYNAME.SOURCEID].ToString().Equals(Common.KEYNAME.SOURCEIBVALUE))
                        {
                            Connection con = new Connection();
                            DataTable dt = con.FillDataTable(Common.ConStr, "IPC_GETLANGBYUSERID", new object[1] { tran.Data[Common.KEYNAME.USERID].ToString() });
                            _lang = dt.Rows[0][Common.KEYNAME.LANG].ToString();
                            if (!tran.Data.ContainsKey(Common.KEYNAME.LANG))
                            {
                                tran.Data.Add(Common.KEYNAME.LANG, _lang);
                            }
                            else
                            {
                                tran.Data[Common.KEYNAME.LANG] = _lang;
                            }
                        }
                    }
                }
                catch { }
                MappingSourceErrorCode(tran);
                tran.OutputData += Common.KEYNAME.IPCERRORCODE + "$" + tran.ErrorCode + "#";
                tran.OutputData += Common.KEYNAME.IPCERRORDESC + "$" + tran.ErrorDesc + "#";
                result[Common.KEYNAME.IPCERRORCODE] = tran.ErrorCode;
                result[Common.KEYNAME.IPCERRORDESC] = tran.ErrorDesc;
                // Get Output Data
                if (tran.ErrorCode.Equals(Common.ERRORCODE.OK))
                {
                    CreateDataHAS(result, tran, tran.Data[Common.KEYNAME.IPCTRANCODE].ToString(),
                                                                    tran.Data, tran.parm, null);
                    tran.OutputData = tran.OutputData.Substring(0, tran.OutputData.Length - 1);
                }
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return result;
        }
        #endregion

        #region Public Function PAR
        private static bool CreateDataParm(TransactionInfo tran, ref object[] parm, string TranCode,
                            string ParentNode, Hashtable DataSub, object ParmSub, DataRow objRow)
        {
            int i = 0;
            try
            {
                string condition = " SOURCEID = '" + tran.Data[Common.KEYNAME.SOURCEID].ToString() + "'";
                condition += " AND DESTID = '" + tran.Data[Common.KEYNAME.DESTID].ToString() + "'";
                condition += " AND IPCTRANCODE = '" + TranCode + "'";
                condition += " AND ONLINE = " + (tran.Online == true ? "1" : "0");
                condition += "' AND PARENTNODE = '" + ParentNode + "'";
                DataRow[] dr = Common.DBIOUTPUTDEFINEPAR.Select(condition, "FIELDNO");
                for (i = 0; i < dr.Length; i++)
                {
                    int Position = int.Parse(dr[i]["POSITION"].ToString());
                    string FieldNo = dr[i]["FIELDNO"].ToString();
                    string FieldDesc = dr[i]["FIELDDESC"].ToString();
                    string FieldStyle = dr[i]["FIELDSTYLE"].ToString();
                    string FieldName = dr[i]["FIELDNAME"].ToString();
                    string ValueStyle = dr[i]["VALUESTYLE"].ToString();
                    string ValueName = dr[i]["VALUENAME"].ToString();
                    // Get Value
                    object value = GetFieldValue(tran, ValueStyle, ValueName, DataSub, ParmSub, objRow);
                    if (value == null) value = "";
                    FormatFieldValue(ref value, dr[i]["FORMATTYPE"].ToString(),
                                                dr[i]["FORMATOBJECT"].ToString(),
                                                dr[i]["FORMATFUNCTION"].ToString(),
                                                dr[i]["FORMATPARM"].ToString());
                    //
                    switch (FieldStyle)
                    {
                        case "VALUE":
                            parm[Position] = value;
                            break;
                        case "ARRAY1":
                            if (Position == -1)
                            {
                                parm = new object[int.Parse(FieldName)];
                                if (CreateDataParm(tran, ref parm, TranCode, FieldNo, tran.Data, tran.parm, null) == false) return false;
                            }
                            else
                            {
                                object[] temp = new object[int.Parse(FieldName)];
                                parm[Position] = temp;
                                if (CreateDataParm(tran, ref temp, TranCode, FieldNo, tran.Data, tran.parm, null) == false) return false;
                            }
                            break;
                    }
                    //
                    string MessageInfoTemp = "[" + FieldNo + "]";
                    if (FieldDesc != "")
                    {
                        MessageInfoTemp += "[" + FieldDesc + "]";
                    }
                    tran.MessageInfo += FormatLogMessageInfo(MessageInfoTemp) + value.ToString() + "\r\n";
                }
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name + "[" + i.ToString() + "]");
                return false;
            }
            return true;
        }

        public static bool CreateRequestPAR(TransactionInfo tran)
        {
            try
            {
                tran.MessageInfo = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff: ");
                tran.MessageInfo += "<<<<<<<<<<<<<<<<<<<<<IPCService Begin Create Param>>>>>>>>>>>>>>>>>>>>>\r\n";
                tran.MessageTypeDest = Common.MESSAGETYPE.PAR;
                CreateDataParm(tran, ref tran.parm, tran.Data[Common.KEYNAME.IPCTRANCODE].ToString(),
                                                            "-1", tran.Data, tran.parm, null);
                tran.MessageInfo += "<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<************************>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>\r\n";
                return true;
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
        }
        #endregion

        #region Public Fincation File Transfer base64
        public static bool FormatFileOutput(TransactionInfo tran)
        {
            int i = 0;
            try
            {
                string condition = " SOURCEID = '" + tran.Data[Common.KEYNAME.SOURCEID].ToString() + "'";
                condition += " AND DESTID = '" + tran.Data[Common.KEYNAME.DESTID].ToString() + "'";
                condition += " AND IPCTRANCODE = '" + tran.Data[Common.KEYNAME.IPCTRANCODE] + "'";
                condition += " AND ONLINE = " + (tran.Online == true ? "1" : "0");
                //DataRow[] dr = Common.DBIOUTPUTDEFINEFILE.Select(condition, "FIELDNO");
                //for (i = 0; i < dr.Length; i++)
                //{
                //    string FieldNo = dr[i]["FIELDNO"].ToString();
                //    string FieldDesc = dr[i]["FIELDDESC"].ToString();
                //    string FieldStyle = dr[i]["FIELDSTYLE"].ToString();
                //    string FieldName = dr[i]["FIELDNAME"].ToString();
                //    string ValueStyle = dr[i]["VALUESTYLE"].ToString();
                //    string ValueName = dr[i]["VALUENAME"].ToString();
                //    // Get file path
                //    object value = GetFieldValue(tran, ValueStyle, ValueName, null, null, null);
                //    if (value == null) value = "";
                //    FormatFieldValue(ref value, dr[i]["FORMATTYPE"].ToString(),
                //                                dr[i]["FORMATOBJECT"].ToString(),
                //                                dr[i]["FORMATFUNCTION"].ToString(),
                //                                dr[i]["FORMATPARM"].ToString());

                //    //add data to tran
                //    tran.OutputData = value.ToString();
                //}

            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return true;
        }
        public static byte[] FileToByteArray(string filename)
        {
            byte[] returnByteArray = null;
            System.IO.FileStream readFileStream = new System.IO.FileStream(filename, System.IO.FileMode.Open, System.IO.FileAccess.Read);

            System.IO.BinaryReader readBinaryReader = new System.IO.BinaryReader(readFileStream);

            long fileByteSize = new System.IO.FileInfo(filename).Length;

            returnByteArray = readBinaryReader.ReadBytes((Int32)fileByteSize);

            readFileStream.Close();
            readFileStream.Dispose();
            readBinaryReader.Close();
            return returnByteArray;
        }
        #endregion

        #region Public Funcation Http Post vutran 19062015

        public static bool CreateRequestHttpPost(TransactionInfo tran)
        {
            try
            {
                tran.MessageTypeDest = Common.MESSAGETYPE.JSON;
                if (tran.Data.ContainsKey(Common.KEYNAME.IPCTRANCODEDEST))
                {
                    tran.OutputData = CreateMessagePost(tran, tran.Data[Common.KEYNAME.IPCTRANCODEDEST].ToString(),
                                                                                tran.Data, tran.parm, null);
                }
                else
                {
                    tran.OutputData += CreateMessagePost(tran, tran.Data[Common.KEYNAME.IPCTRANCODE].ToString(),
                                                                                tran.Data, tran.parm, null);
                }
                return true;
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name + tran.IPCTransID.ToString());
                return false;
            }
        }
        public static bool AnalyzeResponseHttpPost(TransactionInfo tran)
        {
            try
            {
                JObject jo = new JObject();
                string response = tran.InputData;
                if (string.IsNullOrEmpty(response))
                {
                    throw new Exception("Response cannot be null");
                }

                try
                {
                    //get 1st item if response is array
                    if (response.Trim().StartsWith("["))
                    {
                        response = JArray.Parse(response)[0].ToString();
                    }
                    jo = Common.NewParse(response);
                }
                catch //for string result, not json
                {
                    if (tran.Data.ContainsKey(Common.KEYNAME.STRDESTRESULT))
                    {
                        tran.Data[Common.KEYNAME.STRDESTRESULT] = response;
                    }
                    else
                    {
                        tran.Data.Add(Common.KEYNAME.STRDESTRESULT, response);
                    }
                    return true;
                }
                try
                {
                    DataRow[] mapping = Common.DBIINPUTDEFINEHTTP.Select("IPCTRANCODE = '" + tran.Data[Common.KEYNAME.IPCTRANCODE].ToString() +
                        "' OR IPCTRANCODE = '' AND IPCDESTTRANCODE = '" + tran.Data[Common.KEYNAME.IPCTRANCODEDEST].ToString() + "'");
                    if (mapping.Length > 0)
                    {
                        foreach (DataRow row in mapping)
                        {
                            try
                            {
                                if (tran.Data.ContainsKey(row["KEYNAME"].ToString()))
                                {
                                    tran.Data[row["KEYNAME"].ToString()] = jo.SelectToken(row["TAGNAME"].ToString()).Value<string>();
                                }
                                else
                                {
                                    tran.Data.Add(row["KEYNAME"].ToString(), jo.SelectToken(row["TAGNAME"].ToString()).Value<string>());
                                }
                            }
                            catch
                            {
                                Utility.ProcessLog.LogInformation(tran.IPCTransID.ToString() + "AnalyzeResponseHttpPost - Can't when get json value : " + row["TAGNAME"].ToString());
                            }
                        }
                    }

                    //cong error code them 1000
                    if (tran.Data.ContainsKey(Common.KEYNAME.ERRORCODE))
                    {
                        tran.ErrorCode = tran.Data[Common.KEYNAME.ERRORCODE].ToString();
                        tran.MappingDestErrorCode();

                        if (double.Parse(tran.Data[Common.KEYNAME.ERRORCODE].ToString()) != 0)
                        {
                            tran.ErrorCode = (1000 + double.Parse(tran.Data[Common.KEYNAME.ERRORCODE].ToString())).ToString();
                            tran.ErrorDesc = tran.Data[Common.KEYNAME.ERRORDESC].ToString();
                        }
                        tran.Data[Common.KEYNAME.DESTERRORCODE] = tran.Data[Common.KEYNAME.ERRORCODE];
                    }
                }
                catch (Exception ex)
                {
                    Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name + tran.IPCTransID.ToString());
                }
                return true;
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name + tran.IPCTransID.ToString());
                return false;
            }
        }
        private static string CreateMessagePost(TransactionInfo tran, string TranCode,
                                    Hashtable DataSub, object ParmSub, DataRow objRow)
        {
            string result = "";
            try
            {
                string condition = " SOURCEID = '" + tran.Data[Common.KEYNAME.SOURCEID].ToString() + "'";
                condition += " AND DESTID = '" + tran.Data[Common.KEYNAME.DESTID].ToString() + "'";
                condition += " AND IPCTRANCODE = '" + TranCode + "'";
                condition += " AND ONLINE = " + (tran.Online == true ? "1" : "0");
                DataRow[] dr = Common.DBIOUTPUTDEFINEHTTP.Select(condition, "FIELDNO");
                for (int i = 0; i < dr.Length; i++)
                {
                    string FieldNo = dr[i]["FIELDNO"].ToString();
                    string FieldDesc = dr[i]["FIELDDESC"].ToString();
                    string FieldStyle = dr[i]["FIELDSTYLE"].ToString();
                    string FieldName = dr[i]["FIELDNAME"].ToString();
                    string ValueStyle = dr[i]["VALUESTYLE"].ToString();
                    string ValueName = dr[i]["VALUENAME"].ToString();
                    // Get Value
                    object value = GetFieldValue(tran, ValueStyle, ValueName, DataSub, ParmSub, objRow);
                    result += FieldName + "=" + value + "&";
                }
                result = result.Substring(0, result.Length - 1);
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return result;
        }
        #endregion

        #region Encrypt PIN for Telenor login PhongTT 31072017
        public static string encryptPin(String sessionID, String salt, String pin)
        {
            string encrypted;
            encrypted = getSHA(sessionID + getSHA((salt + pin).ToLower()).ToLower());
            return encrypted;
        }

        private static char[] hexits = "0123456789ABCDEF".ToCharArray();
        public static String getSHA(String data)
        {
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(data);
            HashAlgorithm sha = new SHA1CryptoServiceProvider();
            byte[] result = sha.ComputeHash(bs);
            System.Text.StringBuilder s = new System.Text.StringBuilder();

            StringBuilder sb = new StringBuilder(result.Length * 2);
            for (int i = 0; i < result.Length; i++)
            {
                sb.Append(hexits[(((int)result[i] & 0xFF) / 16) & 0x0F]);
                sb.Append(hexits[((int)result[i] & 0xFF) % 16]);
            }
            return sb.ToString();
        }
        #endregion

        #region Public Funcation Http Post VuTT

        public static bool CreateRequestHTTP(TransactionInfo tran)
        {
            try
            {
                tran.MessageTypeDest = Common.MESSAGETYPE.JSON;
                if (tran.Data.ContainsKey(Common.KEYNAME.IPCTRANCODEDEST))
                {
                    tran.OutputData = CreateMessageHTTP(tran, tran.Data[Common.KEYNAME.IPCTRANCODEDEST].ToString(),
                                                                                tran.Data, tran.parm, null);
                }
                else
                {
                    tran.OutputData += CreateMessageHTTP(tran, tran.Data[Common.KEYNAME.IPCTRANCODE].ToString(),
                                                                                tran.Data, tran.parm, null);
                }
                return true;
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name + tran.IPCTransID.ToString());
                return false;
            }
        }
        public static bool CreateRequestText(TransactionInfo tran)
        {
            try
            {
                tran.MessageTypeDest = Common.MESSAGETYPE.JSON;
                if (tran.Data.ContainsKey(Common.KEYNAME.IPCTRANCODEDEST))
                {
                    tran.OutputData = CreateMessageText(tran, tran.Data[Common.KEYNAME.IPCTRANCODEDEST].ToString(),
                                                                                tran.Data, tran.parm, null);
                }
                else
                {
                    tran.OutputData += CreateMessageText(tran, tran.Data[Common.KEYNAME.IPCTRANCODE].ToString(),
                                                                                tran.Data, tran.parm, null);
                }
                return true;
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name + tran.IPCTransID.ToString());
                return false;
            }
        }
        public static bool FormatJsonToDataSet(TransactionInfo tran)
        {
            return FormatJsonToDataSet(tran, "");
        }
        public static bool FormatJsonToDataSet(TransactionInfo tran, string parentNode = "")
        {
            if (FormatJsonToDataTable(tran, parentNode))
            {
                DataSet ds = new DataSet();
                ds.Tables.Add((DataTable)tran.Data[Common.KEYNAME.DATARESULT]);

                if (tran.Data.ContainsKey(Common.KEYNAME.DATASET))
                {
                    tran.Data[Common.KEYNAME.DATASET] = ds;
                }
                else
                {
                    tran.Data.Add(Common.KEYNAME.DATASET, ds);
                }
                return true;
            }
            else
            {
                return false;
            }

        }
        public static bool FormatJsonToDataTable(TransactionInfo tran)
        {
            return FormatJsonToDataTable(tran, "");
        }
        public static bool FormatJsonToDataTable(TransactionInfo tran, string parentNode = "")
        {
            try
            {
                JArray ja = new JArray();
                DataTable dtResult = new DataTable();
                string response = tran.InputData.Trim();
                if (string.IsNullOrEmpty(response))
                {
                    return false;
                }

                try
                {
                    if (string.IsNullOrEmpty(parentNode))
                    {
                        ja = JArray.Parse(response);
                    }
                    else
                    {
                        JObject jo = Common.NewParse(response);
                        ja = JArray.Parse(jo.SelectToken(parentNode).Value<object>().ToString());
                    }
                }
                catch //for string result, not json
                {
                    if (tran.Data.ContainsKey(Common.KEYNAME.STRDESTRESULT))
                    {
                        tran.Data[Common.KEYNAME.STRDESTRESULT] = response;
                    }
                    else
                    {
                        tran.Data.Add(Common.KEYNAME.STRDESTRESULT, response);
                    }
                    return true;
                }
                try
                {
                    if (tran.Data.ContainsKey(Common.KEYNAME.STRDESTRESULT))
                    {
                        tran.Data[Common.KEYNAME.STRDESTRESULT] = response;
                    }
                    else
                    {
                        tran.Data.Add(Common.KEYNAME.STRDESTRESULT, response);
                    }
                }
                catch
                { }
                try
                {
                    DataRow[] mapping = Common.DBIINPUTDEFINEHTTP.Select("IPCTRANCODE = '" + tran.Data[Common.KEYNAME.IPCTRANCODE].ToString() +
                        "' OR IPCTRANCODE = '' AND IPCDESTTRANCODE = '" + tran.Data[Common.KEYNAME.IPCTRANCODEDEST].ToString() + "'");
                    //create table
                    foreach (DataRow row in mapping)
                    {
                        dtResult.Columns.Add(row["KEYNAME"].ToString());
                    }

                    if (mapping.Length > 0)
                    {
                        foreach (JObject jo in ja.Children<JObject>())
                        {
                            DataRow drRs = dtResult.NewRow();
                            foreach (DataRow row in mapping)
                            {
                                try
                                {
                                    object value = null;
                                    if (string.IsNullOrEmpty(row["TAGNAME"].ToString()))
                                    {
                                        value = jo.ToString();
                                    }
                                    else
                                    {
                                        value = jo.SelectToken(row["TAGNAME"].ToString()).Value<object>().ToString();
                                        switch (row["FORMAT"].ToString())
                                        {
                                            case "TEXT":
                                                value = value.ToString().Trim();
                                                break;
                                            case "NUMBER":
                                                value = Decimal.Parse(value.ToString().Trim());
                                                break;
                                        }
                                    }

                                    drRs[row["KEYNAME"].ToString()] = value;
                                }
                                catch
                                {
                                    //Utility.ProcessLog.LogInformation(tran.IPCTransID.ToString() + "AnalyzeResponseHttpPost - Can't when get json value : " + row["TAGNAME"].ToString());
                                    //to support FBE
                                    if (row["DEFAULTVALUE"] != null)
                                    {
                                        drRs[row["KEYNAME"].ToString()] = row["DEFAULTVALUE"].ToString();
                                    }
                                }
                            }
                            dtResult.Rows.Add(drRs);
                        }
                    }

                    if (tran.Data.ContainsKey(Common.KEYNAME.DATARESULT))
                    {
                        tran.Data[Common.KEYNAME.DATARESULT] = dtResult;
                    }
                    else
                    {
                        tran.Data.Add(Common.KEYNAME.DATARESULT, dtResult);
                    }

                    //cong error code them 1000
                    if (tran.Data.ContainsKey(Common.KEYNAME.ERRORCODE))
                    {
                        if (double.Parse(tran.Data[Common.KEYNAME.ERRORCODE].ToString()) != 0)
                        {
                            tran.ErrorCode = (1000 + double.Parse(tran.Data[Common.KEYNAME.ERRORCODE].ToString())).ToString();
                            tran.ErrorDesc = tran.Data[Common.KEYNAME.ERRORDESC].ToString();
                        }
                        tran.Data[Common.KEYNAME.DESTERRORCODE] = tran.Data[Common.KEYNAME.ERRORCODE];
                    }
                }
                catch (Exception ex)
                {
                    Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name + tran.IPCTransID.ToString());
                }
                return true;
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name + tran.IPCTransID.ToString());
                return false;
            }
        }
        private static string FormatStringParams(TransactionInfo tran, string str, bool isPassKeyToValue = false)
        {
            try
            {
                if (str.Contains("{"))
                {
                    List<string> param = GetStringParams(str);
                    foreach (string p in param)
                    {
                        string value = (tran.Data.ContainsKey(p)) ? tran.Data[p].ToString() : isPassKeyToValue ? p : "";
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
        public static bool SelectJsonArrayNode(TransactionInfo tran, string ParmList)
        {
            try
            {
                string response = tran.InputData.Trim();

                if (string.IsNullOrEmpty(response))
                {
                    throw new Exception("IPCERROR=8999#Quote message is empty");
                }

                string[] parms = ParmList.Split('&');
                Dictionary<string, string> dicQuerry = new Dictionary<string, string>();
                foreach (string parm in parms)
                {
                    string[] q = parm.Trim().Split('=');
                    dicQuerry.Add(FormatStringParams(tran, q[0].Trim()), FormatStringParams(tran, q[1].Trim()));
                }

                JArray ja = JArray.Parse(response);
                response = ja.First().ToString();

                foreach (JToken jt in ja.Children())
                {
                    bool flag = true;
                    foreach (KeyValuePair<string, string> pair in dicQuerry)
                    {
                        if (!jt.SelectToken(pair.Key.Trim()).Value<object>().ToString().Trim().Equals(pair.Value.Trim()))
                        {
                            flag = false;
                        }
                    }
                    if (flag)
                    {
                        tran.InputData = jt.ToString();
                        return true;
                    }
                }

                throw new Exception("IPCERROR=8998#Currency pair doesn't exist, please contact administrator");
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name + tran.IPCTransID.ToString());
                return false;
            }
        }
        public static bool AnalyzeResponseHTTP(TransactionInfo tran)
        {
            try
            {
                JObject jo = new JObject();
                string response = tran.InputData.Trim();
                try
                {
                    if (tran.Data.ContainsKey(Common.KEYNAME.STRDESTRESULT))
                    {
                        tran.Data[Common.KEYNAME.STRDESTRESULT] = response;
                    }
                    else
                    {
                        tran.Data.Add(Common.KEYNAME.STRDESTRESULT, response);
                    }
                }
                catch
                { }
                if (string.IsNullOrEmpty(response))
                {
                    return false;
                }
                else if (response.StartsWith("[") && response.EndsWith("]"))
                {
                    JArray ja = JArray.Parse(response);
                    //code tam cho phan array, nho sua lai sau khi confirm voi khach hang
                    //response = response.Substring(1, response.Length - 2);
                    //van giu phan nay cho test enviroiment ko co du currency
                    response = ja.First().ToString();
                }
                try
                {
                    jo = Common.NewParse(response);
                }
                catch //for string result, not json
                {
                    if (tran.Data.ContainsKey(Common.KEYNAME.STRDESTRESULT))
                    {
                        tran.Data[Common.KEYNAME.STRDESTRESULT] = response;
                    }
                    else
                    {
                        tran.Data.Add(Common.KEYNAME.STRDESTRESULT, response);
                    }
                    return true;
                }
                try
                {
                    DataRow[] mapping = Common.DBIINPUTDEFINEHTTP.Select("IPCTRANCODE = '" + tran.Data[Common.KEYNAME.IPCTRANCODE].ToString() +
                        "' OR IPCTRANCODE = '' AND IPCDESTTRANCODE = '" + tran.Data[Common.KEYNAME.IPCTRANCODEDEST].ToString() + "'");
                    if (mapping.Length > 0)
                    {
                        foreach (DataRow row in mapping)
                        {
                            try
                            {
                                object value = null;

                                if (string.IsNullOrEmpty(row["TAGNAME"].ToString()))
                                    value = response;
                                else
                                {
                                    string tagName = row["TAGNAME"].ToString();
                                    if (tagName.Contains(">"))
                                    {
                                        string[] tagNames = tagName.Split('>');
                                        JObject joL = jo;
                                        for (int i = 0; i < tagNames.Length; i++)
                                        {
                                            if (i < tagNames.Length - 1)
                                            {
                                                joL = Common.NewParse(jo.SelectToken(tagNames[i]).Value<object>().ToString());
                                            }
                                            else
                                            {
                                                value = joL.SelectToken(tagNames[i]).Value<object>().ToString();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        value = jo.SelectToken(tagName).Value<object>().ToString();
                                    }
                                }

                                FormatFieldValue(ref value, row["FORMATTYPE"].ToString(), row["FORMATOBJECT"].ToString(), row["FORMATFUNCTION"].ToString(), row["FORMATPARM"].ToString());

                                Common.HashTableAddOrSet(tran.Data, row["KEYNAME"].ToString(), value);
                            }
                            catch
                            {
                                Utility.ProcessLog.LogInformation(tran.IPCTransID.ToString() + "AnalyzeResponseHttpPost - Can't when get json value : " + row["TAGNAME"].ToString());
                                //to support FBE
                                if (row["DEFAULTVALUE"] != null)
                                    Common.HashTableAddOrSet(tran.Data, row["KEYNAME"].ToString(), row["DEFAULTVALUE"].ToString());
                            }
                        }
                    }
                    //tam hardcode phan mapping erro de sit
                    try
                    {
                        if (jo.SelectToken("error.body.error_type") != null || jo.SelectToken("error_type") != null)
                        {
                            string errorcode = "9999";
                            string errordesc = "Transaction error";
                            if (jo.SelectToken("error.body.error_type") != null)
                            {
                                errorcode = Common.ERRORCODE.DESTSYSTEM;
                                tran.ErrorDesc = jo.SelectToken("error.body.error_type").ToString() + ": " + jo.SelectToken("error.body.message").ToString();
                            }
                            else
                            {
                                errorcode = Common.ERRORCODE.DESTSYSTEM;
                                errordesc = jo.SelectToken("error_type").ToString() + ": " + jo.SelectToken("message").ToString();
                            }
                            tran.ErrorCode = errorcode;
                            tran.ErrorDesc = errordesc.Length > 100 ? errordesc.Substring(0, 100) + "..." : errordesc;

                            if (tran.Data.ContainsKey(Common.KEYNAME.ERRORCODE))
                                tran.Data[Common.KEYNAME.ERRORCODE] = tran.ErrorCode;
                            else
                                tran.Data.Add(Common.KEYNAME.ERRORCODE, tran.ErrorCode);

                            if (tran.Data.ContainsKey(Common.KEYNAME.ERRORDESC))
                                tran.Data[Common.KEYNAME.ERRORDESC] = tran.ErrorDesc;
                            else
                                tran.Data.Add(Common.KEYNAME.ERRORDESC, tran.ErrorDesc);
                        }
                    }
                    catch { }

                    //cong error code them 1000
                    if (tran.Data.ContainsKey(Common.KEYNAME.ERRORCODE))
                    {
                        if (double.Parse(tran.Data[Common.KEYNAME.ERRORCODE].ToString()) != 0)
                        {
                            tran.ErrorCode = (1000 + double.Parse(tran.Data[Common.KEYNAME.ERRORCODE].ToString())).ToString();
                            tran.ErrorDesc = tran.Data[Common.KEYNAME.ERRORDESC].ToString();
                        }
                        tran.Data[Common.KEYNAME.DESTERRORCODE] = tran.Data[Common.KEYNAME.ERRORCODE];
                    }
                }
                catch (Exception ex)
                {
                    Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name + tran.IPCTransID.ToString());
                }
                return true;
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name + tran.IPCTransID.ToString());
                return false;
            }
        }
        private static string CreateMessageHTTPPost(TransactionInfo tran, string TranCode,
                                    Hashtable DataSub, object ParmSub, DataRow objRow)
        {
            string result = "";
            try
            {
                string condition = " SOURCEID = '" + tran.Data[Common.KEYNAME.SOURCEID].ToString() + "'";
                condition += " AND DESTID = '" + tran.Data[Common.KEYNAME.DESTID].ToString() + "'";
                condition += " AND IPCTRANCODE = '" + TranCode + "'";
                condition += " AND ONLINE = " + (tran.Online == true ? "1" : "0");
                DataRow[] dr = Common.DBIOUTPUTDEFINEHTTP.Select(condition, "FIELDNO");
                for (int i = 0; i < dr.Length; i++)
                {
                    string FieldNo = dr[i]["FIELDNO"].ToString();
                    string FieldDesc = dr[i]["FIELDDESC"].ToString();
                    string FieldStyle = dr[i]["FIELDSTYLE"].ToString();
                    string FieldName = dr[i]["FIELDNAME"].ToString();
                    string ValueStyle = dr[i]["VALUESTYLE"].ToString();
                    string ValueName = dr[i]["VALUENAME"].ToString();
                    // Get Value
                    object value = GetFieldValue(tran, ValueStyle, ValueName, DataSub, ParmSub, objRow);
                    result += FieldName + "=" + value + "&";
                }
                result = result.Substring(0, result.Length - 1);
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return result;
        }
        private static string CreateMessageText(TransactionInfo tran, string TranCode,
                                    Hashtable DataSub, object ParmSub, DataRow objRow)
        {
            string result = "";
            try
            {
                //build header
                string resultHeader = "";
                string conditionHeader = " SOURCEID = '" + tran.Data[Common.KEYNAME.SOURCEID].ToString() + "'";
                conditionHeader += " AND DESTID = '" + tran.Data[Common.KEYNAME.DESTID].ToString() + "'";
                conditionHeader += " AND IPCTRANCODE = '" + TranCode + "'";
                conditionHeader += " AND ONLINE = " + (tran.Online == true ? "1" : "0");
                DataRow[] drHeader = Common.DBIOUTPUTDEFINEHTTPHEADER.Select(conditionHeader, "FIELDNO");
                for (int i = 0; i < drHeader.Length; i++)
                {
                    string FieldNo = drHeader[i]["FIELDNO"].ToString();
                    string FieldDesc = drHeader[i]["FIELDDESC"].ToString();
                    string FieldStyle = drHeader[i]["FIELDSTYLE"].ToString();
                    string FieldName = drHeader[i]["FIELDNAME"].ToString();
                    string ValueStyle = drHeader[i]["VALUESTYLE"].ToString();
                    string ValueName = drHeader[i]["VALUENAME"].ToString();
                    // Get Value
                    object value = GetFieldValue(tran, ValueStyle, ValueName, DataSub, ParmSub, objRow);
                    value = GetFullFieldValueJSON(tran, value, FieldStyle, FieldName, drHeader[i]["FORMATTYPE"].ToString(),
                                    drHeader[i]["FORMATOBJECT"].ToString(), drHeader[i]["FORMATFUNCTION"].ToString(),
                                    drHeader[i]["FORMATPARM"].ToString(), DataSub, ParmSub, objRow);
                    resultHeader += value.ToString() + ",";
                }
                if (!string.IsNullOrEmpty(resultHeader))
                    resultHeader = "{" + resultHeader.Substring(0, resultHeader.Length - 1) + "}";

                //build Body
                string jBody = "grant_type=client_credentials";
                //if (!string.IsNullOrEmpty(resultBody))
                //    resultBody = "{" + resultBody.Substring(0, resultBody.Length - 1) + "}";

                Dictionary<string, object> dicResult = new Dictionary<string, object>();
                try
                {
                    dicResult.Add(Common.KEYNAME.HTTPHEADER, string.IsNullOrEmpty(resultHeader) ? new JObject() : Common.NewParse(resultHeader));
                    dicResult.Add(Common.KEYNAME.HTTPBODY, jBody);
                }
                catch (Exception exp)
                {
                    Utility.ProcessLog.LogError(exp, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                    dicResult = new Dictionary<string, object>();
                    dicResult.Add(Common.KEYNAME.HTTPHEADER, resultHeader);
                    dicResult.Add(Common.KEYNAME.HTTPBODY, jBody);
                }

                result = JsonConvert.SerializeObject(dicResult);
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return result;
        }
        private static string CreateMessageHTTP(TransactionInfo tran, string TranCode,
Hashtable DataSub, object ParmSub, DataRow objRow)
        {
            string result = "";
            try
            {
                //build header
                string resultHeader = "";
                string conditionHeader = " SOURCEID = '" + tran.Data[Common.KEYNAME.SOURCEID].ToString() + "'";
                conditionHeader += " AND DESTID = '" + tran.Data[Common.KEYNAME.DESTID].ToString() + "'";
                conditionHeader += " AND IPCTRANCODE = '" + TranCode + "'";
                conditionHeader += " AND ONLINE = " + (tran.Online == true ? "1" : "0");
                DataRow[] drHeader = Common.DBIOUTPUTDEFINEHTTPHEADER.Select(conditionHeader, "FIELDNO");
                for (int i = 0; i < drHeader.Length; i++)
                {
                    string FieldNo = drHeader[i]["FIELDNO"].ToString();
                    string FieldDesc = drHeader[i]["FIELDDESC"].ToString();
                    string FieldStyle = drHeader[i]["FIELDSTYLE"].ToString();
                    string FieldName = drHeader[i]["FIELDNAME"].ToString();
                    string ValueStyle = drHeader[i]["VALUESTYLE"].ToString();
                    string ValueName = drHeader[i]["VALUENAME"].ToString();
                    // Get Value
                    object value = GetFieldValue(tran, ValueStyle, ValueName, DataSub, ParmSub, objRow);
                    value = GetFullFieldValueJSON(tran, value, FieldStyle, FieldName, drHeader[i]["FORMATTYPE"].ToString(),
                    drHeader[i]["FORMATOBJECT"].ToString(), drHeader[i]["FORMATFUNCTION"].ToString(),
                    drHeader[i]["FORMATPARM"].ToString(), DataSub, ParmSub, objRow);
                    resultHeader += value.ToString() + ",";
                }
                if (!string.IsNullOrEmpty(resultHeader))
                    resultHeader = "{" + resultHeader.Substring(0, resultHeader.Length - 1) + "}";

                //build Body
                //string resultBody = "";
                string conditionBody = " SOURCEID = '" + tran.Data[Common.KEYNAME.SOURCEID].ToString() + "'";
                conditionBody += " AND DESTID = '" + tran.Data[Common.KEYNAME.DESTID].ToString() + "'";
                conditionBody += " AND IPCTRANCODE = '" + TranCode + "'";
                conditionBody += " AND ONLINE = " + (tran.Online == true ? "1" : "0");
                DataRow[] drBody = Common.DBIOUTPUTDEFINEHTTP.Select(conditionBody, "FIELDNO");

                JObject jBody = new JObject();
                for (int i = 0; i < drBody.Length; i++)
                {
                    bool isJarray = false;

                    string FieldNo = drBody[i]["FIELDNO"].ToString();
                    string FieldDesc = drBody[i]["FIELDDESC"].ToString();
                    string FieldStyle = drBody[i]["FIELDSTYLE"].ToString();
                    string FieldName = drBody[i]["FIELDNAME"].ToString();
                    string ValueStyle = drBody[i]["VALUESTYLE"].ToString();
                    string ValueName = drBody[i]["VALUENAME"].ToString();
                    // Get Value
                    object value = GetFieldValue(tran, ValueStyle, ValueName, DataSub, ParmSub, objRow);
                    value = GetFullFieldValueJSON(tran, value, FieldStyle, FieldName, drBody[i]["FORMATTYPE"].ToString(),
                    drBody[i]["FORMATOBJECT"].ToString(), drBody[i]["FORMATFUNCTION"].ToString(),
                    drBody[i]["FORMATPARM"].ToString(), DataSub, ParmSub, objRow);

                    JObject jCurrent = jBody;
                    string[] arrJsonPath = FieldName.Trim().Split('.');

                    for (int j = 0; j < arrJsonPath.Length; j++)
                    {
                        string strPath = arrJsonPath[j].Trim();
                        JObject jSelect = (JObject)jCurrent.SelectToken(strPath);
                        if (jSelect == null)
                        {
                            if (strPath.EndsWith("]"))
                            {
                                string pathKey = strPath.Split('[')[0];
                                int pathPos = int.Parse(strPath.Split('[')[1].Substring(0, strPath.Split('[')[1].Length - 1));
                                if (jCurrent.SelectToken(pathKey) != null)
                                {
                                    JArray jArr = (JArray)jCurrent.SelectToken(pathKey);
                                    if (jArr.Count <= pathPos)
                                    {
                                        for (int kk = jArr.Count; kk <= pathPos; kk++)
                                        {
                                            if (j != arrJsonPath.Length - 1)
                                            {
                                                jArr.Add(new JObject());
                                            }
                                            else
                                            {
                                                jArr.Add(value);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    jSelect = jCurrent;
                                    jSelect.Add(pathKey, new JArray());
                                    JArray jArr = (JArray)jSelect.SelectToken(pathKey);
                                    if (jArr.Count <= pathPos)
                                    {
                                        for (int kk = jArr.Count; kk <= pathPos; kk++)
                                        {
                                            if (j != arrJsonPath.Length - 1)
                                            {
                                                jArr.Add(new JObject());
                                            }
                                            else
                                            {
                                                jArr.Add(value);
                                            }
                                        }

                                    }
                                }

                                if (j != arrJsonPath.Length - 1)
                                {
                                    jSelect = (JObject)jCurrent.SelectToken(strPath);
                                }
                            }
                            else
                            {
                                jSelect = jCurrent;
                                if (arrJsonPath.Length == j + 1)
                                {
                                    jSelect.Add(new JProperty(strPath, value));
                                }
                                else
                                {
                                    jSelect.Add(strPath, new JObject());
                                    jSelect = (JObject)jSelect.SelectToken(strPath);
                                }
                            }
                        }

                        jCurrent = jSelect;
                    }
                }

                //if (!string.IsNullOrEmpty(resultBody))
                // resultBody = "{" + resultBody.Substring(0, resultBody.Length - 1) + "}";

                Dictionary<string, object> dicResult = new Dictionary<string, object>();
                try
                {
                    dicResult.Add(Common.KEYNAME.HTTPHEADER, string.IsNullOrEmpty(resultHeader) ? new JObject() : JObject.Parse(resultHeader));
                    dicResult.Add(Common.KEYNAME.HTTPBODY, jBody);
                }
                catch (Exception exp)
                {
                    Utility.ProcessLog.LogError(exp, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                    dicResult = new Dictionary<string, object>();
                    dicResult.Add(Common.KEYNAME.HTTPHEADER, resultHeader);
                    dicResult.Add(Common.KEYNAME.HTTPBODY, jBody);
                }

                result = JsonConvert.SerializeObject(dicResult);
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return result;
        }


        private static object GetFullFieldValueJSON(TransactionInfo tran, object value, string FieldStyle,
                        string FieldName, string FormatType, string FormatObject, string FormatFunction,
                        string FormatParm, Hashtable DataSub, object ParmSub, DataRow objRow)
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
        #endregion

        #region FormatXMLToFields
        public static bool FormatXMLToFields(TransactionInfo tran)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(tran.InputData);

                DataRow[] mapping = Common.DBIMAPXMLTOFIELD.Select("(IPCTRANCODE = '" + tran.Data[Common.KEYNAME.IPCTRANCODEDEST].ToString() +
                        "' OR IPCTRANCODE = '') AND SOURCEID = '" + tran.Data[Common.KEYNAME.SOURCEID].ToString() +
                        "' AND (DESTID = '" + tran.Data[Common.KEYNAME.DESTID].ToString() + "' OR DESTID = '')");
                DataSet dsrs = new DataSet();
                DataTable dtrs = new DataTable();
                foreach (DataRow drConfig in mapping)
                {
                    try
                    {
                        string fieldstyle = drConfig["FIELDSTYLE"].ToString();
                        string fieldname = drConfig["FIELDNAME"].ToString();
                        string valuename = drConfig["VALUENAME"].ToString();
                        string valuestyle = drConfig["VALUESTYLE"].ToString();
                        string FormatType = drConfig["FORMATTYPE"].ToString();
                        string FormatObject = drConfig["FORMATOBJECT"].ToString();
                        string FormatFunction = drConfig["FORMATFUNCTION"].ToString();
                        string FormatParm = drConfig["FORMATPARM"].ToString();
                        bool returnlist = false;
                        string conditionvalue = drConfig["CONDITIONVALUE"].ToString();
                        string conditionstyle = drConfig["CONDITIONSTYLE"].ToString();
                        int PosConfig = int.Parse(drConfig["POS"].ToString());
                        string[] valList = null;
                        if (!string.IsNullOrEmpty(conditionstyle))
                        {
                            switch (conditionstyle)
                            {
                                case "COMPARE":
                                    if (!string.IsNullOrEmpty(conditionvalue))
                                        valList = conditionvalue.Split('|');
                                    break;
                                case "ALL":
                                    returnlist = true;
                                    break;
                            }
                            if (valList != null && valList.Length > 0)
                            {
                                if (valList.Length > 1)
                                {
                                    switch (valList[1])
                                    {
                                        case "1":
                                            returnlist = false;
                                            break;
                                        case "0":
                                            returnlist = true;
                                            break;
                                    }
                                }
                            }

                        }
                        object valuers = null;
                        switch (valuestyle)
                        {
                            case "TAG":
                                string conPath = string.Empty;
                                string conValue = string.Empty;
                                if (valList != null && valList.Length > 0)
                                {
                                    string[] splitPar = valList[0].Split('#');
                                    if (splitPar != null && splitPar.Length > 1)
                                    {
                                        conPath = splitPar[0];
                                        conValue = splitPar[1];
                                    }
                                    else if (splitPar != null && splitPar.Length > 0)
                                    {
                                        conPath = splitPar[0];
                                        conValue = "";
                                    }
                                }
                                valuers = GetValueFromXML(doc, valuename, conPath, conValue, returnlist);
                                break;
                            case "VALUE":
                                valuers = valuename;
                                break;
                            case "DATA":
                                valuers = tran.Data.ContainsKey(valuename) ? tran.Data[valuename] : valuename;
                                break;
                        }
                        if (valuers == null) throw new Exception();
                        if (valuers is IList)
                        {
                            for (int i = 0; i < (valuers as List<string>).Count; i++)
                            {
                                object str = (valuers as List<string>)[i];
                                FormatFieldValue(ref str, FormatType, FormatObject, FormatFunction, FormatParm);
                                (valuers as List<string>)[i] = str.ToString();
                            }
                        }
                        else
                        {
                            FormatFieldValue(ref valuers, FormatType, FormatObject, FormatFunction, FormatParm);
                        }

                        switch (fieldstyle)
                        {
                            case "DATA":

                                if (valuers is IList)
                                {
                                    PosConfig = PosConfig > -1 ? PosConfig : (valuers as IList).Count - 1;
                                    if (tran.Data.ContainsKey(fieldname))
                                    {
                                        tran.Data[fieldname] = (valuers as IList)[PosConfig];
                                    }
                                    else
                                    {
                                        tran.Data.Add(fieldname, (valuers as IList)[PosConfig]);
                                    }
                                }
                                else
                                {
                                    if (tran.Data.ContainsKey(fieldname))
                                    {
                                        tran.Data[fieldname] = valuers;
                                    }
                                    else
                                    {
                                        tran.Data.Add(fieldname, valuers);
                                    }
                                }


                                break;
                            case "DATASET":
                                try
                                {
                                    dtrs.Columns.Add(fieldname);
                                    if (valuers is IList)
                                    {
                                        List<string> listsr = valuers as List<string>;
                                        for (int i = 0; i < listsr.Count; i++)
                                        {
                                            try
                                            {
                                                dtrs.Rows[i][fieldname] = listsr[i];
                                            }
                                            catch
                                            {
                                                DataRow row = dtrs.NewRow();
                                                row[fieldname] = listsr[i];
                                                dtrs.Rows.Add(row);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (dtrs.Rows.Count > 0)
                                        {
                                            for (int i = 0; i < dtrs.Rows.Count; i++)
                                            {
                                                try
                                                {
                                                    dtrs.Rows[i][fieldname] = valuers;
                                                }
                                                catch
                                                {
                                                    DataRow row = dtrs.NewRow();
                                                    row[fieldname] = valuers;
                                                    dtrs.Rows.Add(row);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            DataRow row = dtrs.NewRow();
                                            row[fieldname] = valuers;
                                            dtrs.Rows.Add(row);
                                        }
                                    }
                                }
                                catch { }
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        ProcessLog.LogInformation(tran.IPCTransID + "| Can not found " + drConfig["VALUENAME"].ToString() + " in XML Response", Common.FILELOGTYPE.LOGXMLTOFIELD);
                    }
                }
                if (dtrs.Rows.Count > 0)
                {
                    dsrs.Tables.Add(dtrs);
                    if (tran.Data.ContainsKey(Common.KEYNAME.DATARESULT))
                    {
                        tran.Data[Common.KEYNAME.DATARESULT] = dsrs;
                    }
                    else
                    {
                        tran.Data.Add(Common.KEYNAME.DATARESULT, dsrs);
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
            return true;
        }

        private static object GetValueFromXML(XmlDocument doc, string valuename, string conditionPath, string conditionValue, bool returnList = false)
        {
            List<string> listValue = new List<string>();
            listValue = GetXmlNode(doc, valuename);
            if (listValue == null || listValue.Count == 0) return null;
            if (!string.IsNullOrEmpty(conditionPath))
            {
                List<string> listCondition = new List<string>();
                if (valuename.Equals(conditionPath))
                {
                    listCondition = listValue;
                }
                else
                {
                    listCondition = GetXmlNode(doc, conditionPath);
                }

                if (returnList)
                {

                    if (listCondition != null && listCondition.Count > 0)
                    {
                        List<int> indexes = new List<int>();
                        if (!string.IsNullOrEmpty(conditionValue))
                        {
                            indexes = listCondition.Select((value, index) => new { value, index }).Where(x => x.value == conditionValue).Select(x => x.index).ToList();
                            if (indexes != null && indexes.Count > 0)
                            {
                                List<string> listrs = new List<string>();
                                foreach (int idx in indexes)
                                {
                                    listrs.Add(listValue[idx]);
                                }
                                return listrs;
                            }
                            else
                            {
                                return listValue;
                            }
                        }
                        else
                        {
                            return listValue;
                        }
                    }
                    else
                    {
                        return listValue;
                    }

                }
                else
                {
                    int pos = -1;
                    pos = listCondition.IndexOf(conditionValue);
                    if (pos > -1) return listValue[pos];
                    else return null;
                }
            }
            else
            {
                if (returnList) return listValue;
                else return listValue[0];
            }

        }

        private static List<string> GetXmlNode(XmlDocument doc, string valuename)
        {
            string[] tags = valuename.Split('.');
            var xmlNodelist = doc.GetElementsByTagName(tags[tags.Length - 1]);
            List<string> listValue = new List<string>();

            for (int k = 0; k < xmlNodelist.Count; k++)
            {
                int j = -1;
                XmlNode xmlnodetmp = xmlNodelist[k];
                for (int i = tags.Length - 2; i >= 0; i--)
                {
                    if (xmlnodetmp.ParentNode.Name.Equals(tags[i]))
                    {
                        xmlnodetmp = xmlnodetmp.ParentNode;
                    }
                    else
                    {
                        j = -1;
                        break;
                    }
                    j = i;
                }
                if (j != -1)
                {
                    listValue.Add(xmlNodelist[k].InnerText);
                }
            }
            return listValue;
        }

        #endregion

        public static Hashtable CreatePushNotificationV2(TransactionInfo tran)
        {
            try
            {
                // Get Output Data
                if (tran.ErrorCode.Equals(Common.ERRORCODE.OK))
                {
                    return CreateDataPushNotificationV2(tran, tran.Data[Common.KEYNAME.IPCTRANCODE].ToString(),
                                                                    tran.Data, tran.parm, null);
                }
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return null;
        }
        private static Hashtable CreateDataPushNotificationV2(TransactionInfo tran, string TranCode,
                                                Hashtable DataSub, object ParmSub, DataRow objRow)
        {
            string _title = string.Empty;
            string _body = string.Empty;
            string _detail = string.Empty;
            string _data = string.Empty;
            string _img = string.Empty;
            string _url = string.Empty;
            string _group = string.Empty;
            string _sendtype = string.Empty;
            Dictionary<string, string> dicData = new Dictionary<string, string>();

            try
            {

                Connection con = new Connection();
                DataTable drTemp = con.FillDataTable(Common.ConStr, "EBA_PUSHNOTIFICATION_GETBYSCHEDULEID", new object[1] { tran.Data[Common.KEYNAME.SCHEDULEID].ToString().Trim() });

                if (drTemp.Rows.Count > 0)
                {
                    _title = drTemp.Rows[0][Common.KEYNAME.TITLE].ToString();
                    _body = drTemp.Rows[0][Common.KEYNAME.BODY].ToString();
                    _detail = drTemp.Rows[0]["DETAILS"].ToString();
                    _sendtype = drTemp.Rows[0]["SENDTYPE"].ToString();

                    if (tran.Data.ContainsKey(Common.KEYNAME.ID))
                        tran.Data[Common.KEYNAME.ID] = drTemp.Rows[0][Common.KEYNAME.ID].ToString();
                    else
                        tran.Data.Add(Common.KEYNAME.ID, drTemp.Rows[0][Common.KEYNAME.ID].ToString());

                    if (tran.Data.ContainsKey(Common.KEYNAME.TITLE))
                        tran.Data[Common.KEYNAME.TITLE] = _title;
                    else
                        tran.Data.Add(Common.KEYNAME.TITLE, _title);

                    if (tran.Data.ContainsKey(Common.KEYNAME.BODY))
                        tran.Data[Common.KEYNAME.BODY] = _body;
                    else
                        tran.Data.Add(Common.KEYNAME.BODY, _body);

                    if (tran.Data.ContainsKey("DETAILS"))
                        tran.Data["DETAILS"] = _detail;
                    else
                        tran.Data.Add("DETAILS", _detail);

                    if (tran.Data.ContainsKey("SENDTYPE"))
                        tran.Data["SENDTYPE"] = _sendtype;
                    else
                        tran.Data.Add("SENDTYPE", _sendtype);
                }

                string condition = " SOURCEID = '" + tran.Data[Common.KEYNAME.SOURCEID].ToString().Trim() + "'";
                condition += " AND DESTID = '" + tran.Data[Common.KEYNAME.DESTID].ToString() + "'";
                condition += " AND IPCTRANCODE = '" + tran.Data[Common.KEYNAME.IPCTRANCODE].ToString() + "'";
                condition += " AND ISONLINE = " + (tran.Online == true ? "1" : "0");
                DataRow[] dr = Common.DBIOUTPUTDEFINEPUSH.Select(condition, "FIELDNO");

                for (int i = 0; i < dr.Length; i++)
                {
                    string FieldNo = dr[i]["FIELDNO"].ToString();
                    string FieldDesc = dr[i]["FIELDDESC"].ToString();
                    string FieldStyle = dr[i]["FIELDSTYLE"].ToString();
                    string FieldName = dr[i]["FIELDNAME"].ToString();
                    string ValueStyle = dr[i]["VALUESTYLE"].ToString();
                    string ValueName = dr[i]["VALUENAME"].ToString();
                    // Get Value
                    object value = GetFieldValue(tran, ValueStyle, ValueName, DataSub, tran.parm, objRow);

                    Formatters.Formatter.FormatFieldValue(ref value, dr[i]["FORMATTYPE"].ToString(), dr[i]["FORMATOBJECT"].ToString(), dr[i]["FORMATFUNCTION"].ToString(), dr[i]["FORMATPARM"].ToString());

                    _title = _title.Replace("{" + dr[i]["FIELDNAME"].ToString() + "}", value.ToString());
                    _body = _body.Replace("{" + dr[i]["FIELDNAME"].ToString() + "}", value.ToString());
                    _detail = _detail.Replace("{" + dr[i]["FIELDNAME"].ToString() + "}", value.ToString());

                    dicData.Add(dr[i]["FIELDNAME"].ToString(), (value == null) ? "" : value.ToString());
                }

                _data = JsonConvert.SerializeObject(dicData);

                Hashtable hsRs = new Hashtable();
                hsRs.Add(Common.KEYNAME.TITLE, _title);
                hsRs.Add(Common.KEYNAME.BODY, _body);
                hsRs.Add(Common.KEYNAME.DETAIL, _detail);
                hsRs.Add(Common.KEYNAME.DATA, _data);
                hsRs.Add(Common.KEYNAME.IMGURL, _img);
                hsRs.Add(Common.KEYNAME.LINK, _url);
                hsRs.Add(Common.KEYNAME.GROUP, _group);

                return hsRs;
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name + " " + tran.IPCTransID.ToString() + "IPC trancode=" + TranCode);
                return null;
            }
        }

        public static void MappingSourceErrorCode(TransactionInfo tran)
        {
            try
            {
                Connection con = new Connection();
                string lang = "en-US";
                if (tran.Data.ContainsKey(Common.KEYNAME.LANG))
                {
                    lang = tran.Data[Common.KEYNAME.LANG].ToString();
                }

                DataRow[] errorMapping = Common.DBIERRORLISTSOURCE.Select("SYSERRORCODE = '" + tran.ErrorCode + "'" +
                                                                  "AND (IPCTRANCODE = '" + tran.Data[Common.KEYNAME.IPCTRANCODE].ToString() + "' OR IPCTRANCODE = '') " +
                                                                  "AND (LANG = '" + lang + "' OR LANG = '') " +
                                                                  "AND (SOURCEID = '" + tran.Data[Common.KEYNAME.SOURCEID].ToString() + "' OR SOURCEID = '')", "IPCTRANCODE DESC");
                if (errorMapping == null || errorMapping.Length == 0)
                {
                    errorMapping = Common.DBIERRORLISTSOURCE.Select("SYSERRORCODE = '" + tran.ErrorCode + "'" +
                                                                  "AND (IPCTRANCODE = '" + tran.Data[Common.KEYNAME.IPCTRANCODE].ToString() + "' OR IPCTRANCODE = '') " +
                                                                  "AND (LANG = 'en-US' OR LANG = '') " +
                                                                  "AND (SOURCEID = '" + tran.Data[Common.KEYNAME.SOURCEID].ToString() + "' OR SOURCEID = '')", "IPCTRANCODE DESC");
                }
                if (errorMapping.Length > 0)
                {
                    string errorDesc = string.IsNullOrEmpty(errorMapping[0]["SOURCEERRORDESC"].ToString())
                        ? tran.ErrorDesc
                        : errorMapping[0]["SOURCEERRORDESC"].ToString();
                    DataTable dtDefineOut = con.FillDataTable(Common.ConStr, "IPC_GETOUTPUTDEFINEEERROR", new object[4] { tran.Data[Common.KEYNAME.SOURCEID].ToString(), tran.Data[Common.KEYNAME.IPCTRANCODE].ToString(), tran.Data[Common.KEYNAME.LANG].ToString(), tran.ErrorCode });
                    FormatContentMessages(ref errorDesc, dtDefineOut, tran);
                    tran.Data[Common.KEYNAME.ERRORCODE] = errorMapping[0]["SOURCEERRORCODE"].ToString();
                    tran.Data[Common.KEYNAME.ERRORDESC] = errorDesc;
                    tran.ErrorCode = errorMapping[0]["SOURCEERRORCODE"].ToString();
                    tran.ErrorDesc = errorDesc;
                }

                tran.Data[Common.KEYNAME.IPCERRORCODE] = tran.ErrorCode;
                tran.Data[Common.KEYNAME.IPCERRORDESC] = tran.ErrorDesc;
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        public static void FormatContentMessages(ref string contentMessages, DataTable dtDefineOut, TransactionInfo tran)
        {
            if (contentMessages.Contains("{"))
            {
                List<string> param = GetParams(contentMessages);
                foreach (string p in param)
                {
                    DataRow[] drConfigs = dtDefineOut.Select($"FIELDNAME = '{p}'");
                    object value = "";
                    if (drConfigs.Count() > 0)
                    {
                        value = GetFieldValue(tran, drConfigs[0]["VALUESTYLE"].ToString(), drConfigs[0]["VALUENAME"].ToString(), tran.Data, tran.parm, null);

                        FormatFieldValue(ref value, drConfigs[0]["FORMATTYPE"].ToString(), drConfigs[0]["FORMATOBJECT"].ToString(), drConfigs[0]["FORMATFUNCTION"].ToString(), drConfigs[0]["FORMATPARM"].ToString());
                    }
                    else
                    {
                        value = (tran.Data.ContainsKey(p) ? tran.Data[p].ToString() : "");
                    }

                    contentMessages = contentMessages.Replace("{" + p + "}", value.ToString().Replace("\r\n", "<br/>").Replace("\\r\\n", "<br/>"));
                }
            }
        }
        public static List<string> GetParams(string content)
        {
            List<string> param = new List<string>();
            string[] kk = content.Split('{');
            foreach (string k in kk)
            {
                if (k.Contains("}"))
                {
                    param.Add(k.Split('}')[0].Trim());
                }
            }
            return param;
        }

        public static bool GenarateJWTToken(TransactionInfo tran)
        {
            JObject jo = new JObject();
            jo = Common.NewParse(tran.OutputData);
            JObject jbody = Common.NewParse(jo.SelectToken(Common.KEYNAME.HTTPBODY).ToString());

            string secretKey = ConfigurationManager.AppSettings["SecretKey2C2PBackendNoti"].ToString();
            secretKey = Common.DecryptData(secretKey);

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm(); // symmetric
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            var token = encoder.Encode(jbody, secretKey);
            jo["HTTPBODY"] = @"{""payload"":""" + token + @"""}";
            tran.OutputData = jo.ToString();
            return true;
        }

        public static bool DecodeJWTToken(TransactionInfo tran)
        {
            JObject jo = new JObject();
            try
            {
                jo = Common.NewParse(tran.Data["2C2PBODYREQ"] != null ? tran.Data["2C2PBODYREQ"].ToString() : tran.InputData);
                string payload = jo.SelectToken("payload").ToString();

                string secretKey = ConfigurationManager.AppSettings["SecretKey2C2PBackendNoti"].ToString();
                secretKey = Common.DecryptData(secretKey);

                IJsonSerializer serializer = new JsonNetSerializer();
                var provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtAlgorithm algorithm = new HMACSHA256Algorithm(); // symmetric
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder, algorithm);
                var obj = decoder.Decode(payload, secretKey, verify: true);
                tran.InputData = obj;
                return true;
            }
            catch (TokenExpiredException)
            {
                return false;
            }
            catch (SignatureVerificationException)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool FormatDataTableToBase64Excel(TransactionInfo tran, string parmList)
        {
            try
            {
                string[] parm = parmList.Split('|');
                DataTable dataTable = (DataTable)tran.Data[parm[0].Trim()];
                string keyName = parm[1].Trim();

                string strBase64 = Common.DataTable2Base64Excel(dataTable);
                string fileName = tran.Data[Common.KEYNAME.USERID].ToString() + tran.IPCTransID + "_input.xls";

                if (!string.IsNullOrEmpty(strBase64))
                {
                    if (tran.Data.Contains(keyName))
                    {
                        tran.Data[keyName] = strBase64;
                    }
                    else
                    {
                        tran.Data.Add(keyName, strBase64);
                    }

                    if (tran.Data.Contains(Common.KEYNAME.FILENAME))
                    {
                        tran.Data[Common.KEYNAME.FILENAME] = fileName;
                    }
                    else
                    {
                        tran.Data.Add(Common.KEYNAME.FILENAME, fileName);
                    }
                }
                else
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
        }

        public static bool LoadTranHisbyUser(TransactionInfo tran, string dsName)
        {
            try
            {
                DataSet ds = new DataSet();
                if (tran.Data.ContainsKey(dsName) == false || tran.Data[dsName].GetType() != ds.GetType())
                {
                    //tranh tran log, giao dich autobalance goi lien tuc nen tran log rat nhanh
                    if (!tran.Data[Common.KEYNAME.IPCTRANCODE].ToString().Equals("SMS00014"))
                    {
                        Utility.ProcessLog.LogInformation(string.Format("LoadTranHisbyUser error ds {0} not found or not dataset", dsName));
                    }
                    else
                    {
                        tran.ErrorCode = Common.ERRORCODE.SYSTEM;
                    }
                    return false;
                }
                else
                {
                    ds = (DataSet)tran.Data[dsName];
                    if (ds == null || ds.Tables.Count == 0)
                    {
                        //tranh tran log, giao dich autobalance goi lien tuc nen tran log rat nhanh
                        if (!tran.Data[Common.KEYNAME.IPCTRANCODE].ToString().Equals("SMS00014"))
                        {
                            Utility.ProcessLog.LogInformation(string.Format("LoadTranHisbyUser error ds {0} haven't any table", dsName));
                        }
                        else
                        {
                            tran.ErrorCode = Common.ERRORCODE.SYSTEM;
                        }
                        return false;
                    }
                }
                Connection dbObj = new Connection();

                #region tao bang chua giao dich cua user
                DataTable dtmapcol = new DataTable();
                DataTable result = new DataTable();
                dtmapcol = dbObj.FillDataTable(Common.ConStr, "IPCGETMAPPINGCOLNAME", tran.Data[Common.KEYNAME.IPCTRANCODE].ToString(),tran.Data[Common.KEYNAME.DESTID].ToString());
                if (dtmapcol.Rows.Count == 0)
                {
                    return true;
                }
                    foreach (DataRow drcol in dtmapcol.Rows)
                {
                    DataColumn dtcolumn = new DataColumn(drcol["TAGETNAME"].ToString());
                    result.Columns.Add(dtcolumn);
                }
                #endregion

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DataTable rs = new DataTable();
                    rs = dbObj.FillDataTable(Common.ConStr, "IPCMAPTRANHISBYUSERID", tran.Data[Common.KEYNAME.USERID].ToString(),
                    dr["Txref"].ToString().Trim(), tran.Data[Common.KEYNAME.IPCTRANCODE].ToString());
                    if(rs.Rows.Count > 0)
                    {
                        if (rs.Rows[0]["CHECKTYPE"].Equals("Y"))
                        {
                            return true;
                        }
                        DataRow row = result.NewRow();
                        foreach (DataRow drcolresult in dtmapcol.Rows)
                        {
                            row[drcolresult["TAGETNAME"].ToString()] = ds.Tables[0].Columns.Contains(drcolresult["MAPPINGNAME"].ToString()) ?  dr[drcolresult["MAPPINGNAME"].ToString()].ToString() : rs.Rows[0][drcolresult["MAPPINGNAME"].ToString()].ToString();
                        }
                        result.Rows.Add(row);
                    }
                }
                DataSet dsResult = new DataSet();
                dsResult.Tables.Add(result);
                tran.Data[dsName] = dsResult;
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
            return true;
        }
    }
}