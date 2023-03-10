#region XD World Recipe V 3
// FileName: Utility.cs
// Author: Dexter Zafra
// Date Created: 5/19/2008
// Website: www.ex-designz.net
#endregion
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Globalization;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Text.RegularExpressions;

using SmartPortal.Common;
using SmartPortal.Common.Utilities;

using System.Collections;
using System.Net.Mail;
using System.Threading;
using System.Web.Configuration;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Xml;
using System.Net;
using System.Security.Cryptography;
using SmartPortal.Model;
using SmartPortal.BLL;

using SmartPortal.DAL;
using System.Data.SqlClient;
using Antlr3.ST;
using System.Linq;



namespace SmartPortal.Common.Utilities
{
    /// <summary>
    /// Object in this class manages utility function
    /// </summary>
    public static class Utility
    {
        public static string mappingCardType(string input, bool In2Out)
        {
            string output = "Unknown";
            try
            {
                Dictionary<string, string> stsList = new Dictionary<string, string>
            {
                //{ "CR","Credit" },
                { "C","Credit" },
                { "DE","Debit" },
                { "P","Prepaid" }
            };
                return In2Out ? stsList[input.Trim()] : stsList.FirstOrDefault(x => x.Value == input.Trim()).Key;
            }
            catch (Exception ex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSCard_View_Widget", "mappingCardType", ex.ToString(), "");
            }
            return output;
        }
        public static string mappingCardStatus(string input, bool In2Out)
        {
            string output = "Unknown";
            try
            {
                Dictionary<string, string> stsList = new Dictionary<string, string>
            {
                { "T","Terminated" },
                { "V","Voluntary Closed" },
                { "A","Active" },
                { "B","Block" },
                { "L","Lost" },
                { "S","Stolen" },
                { "U","Unactived" },
                { "X","Counterfeit" },
                { "D","Destroy" },
                { "R","Restricted Card" },
            };
                return In2Out ? stsList[input.Trim()] : stsList.FirstOrDefault(x => x.Value == input.Trim()).Key;
            }
            catch (Exception ex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSCard_View_Widget", "mappingCarType", ex.ToString(), "");
            }
            return output;
        }
        public static string MappingCurrency(string input)
        {
            try
            {
                int number;
                if (int.TryParse(input, out number))
                {
                    return DefinitionCollection.SingleOrDefault(d => d.Number == number).Code.ToString() ?? "";
                }
                else
                {
                    return DefinitionCollection.SingleOrDefault(d => d.Code == input.ToUpper()).Number.ToString() ?? "";
                }
            }
            catch
            {
                return "";
            }
        }
        public static bool CheckHTMLWidget(string widgetID)
        {
            DataTable iRead;

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@widgetid";
            p1.Value = widgetID;
            p1.SqlDbType = SqlDbType.VarChar;

            iRead = DataAccess.GetFromDataTable("HTMLWidget_Check", p1);

            if (iRead.Rows.Count != 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static string CheckRoleAdmin()
        {
            if (WidgetBaseAdmin(HttpContext.Current.Session["userName"].ToString()))
            {
                return "<a id=\"open\" class=\"open\" href=\"#\">" + Resources.labels.controlpanel + "</a>";
            }
            else
            {
                return "";
            }
        }
        public static string DisplaySlideTop()
        {
            if (WidgetBaseAdmin(HttpContext.Current.Session["userName"].ToString()))
            {
                return "block";
            }
            else
            {
                return "none";
            }
        }
        public static Boolean IsAdmin()
        {
            if (new SmartPortal.Common.PortalSettings().portalSetting.UserNameDefault.Trim() != HttpContext.Current.Session["userName"].ToString().Trim())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static Boolean IsGuest()
        {
            if (new SmartPortal.Common.PortalSettings().portalSetting.UserNameDefault.Trim() == HttpContext.Current.Session["userName"].ToString().Trim())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static Boolean WidgetBaseAdmin(string userName)
        {
            UsersBLL UB = new UsersBLL();
            DataTable iRead;

            iRead = UB.GetRole(userName, HttpContext.Current.Session["serviceID"].ToString());


            if (iRead.Select("RoleID=" + new SmartPortal.Common.PortalSettings().portalSetting.RoleAdminID.ToString()).Length != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool DisableFCK()
        {
            if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["type"] != null) { return true; } else { return false; };
        }
        //format dd/mm/yyyy HH:MM:ss 
        public static string FormatDatetime(string datetime, string formatInput, string formatOutput)
        {
            string dateout = "";

            dateout = DateTime.ParseExact(datetime, formatInput, null).ToString(formatOutput);

            return dateout;
        }

        public static string FormatDatetime(string datetime, string formatInput, DateTimeStyle style)
        {
            string dateout = "";
            if (string.IsNullOrEmpty(formatInput))
            {
                formatInput = "dd/MM/yyyy";
            }
            if (style == null)
            {
                style = DateTimeStyle.Date;
            }
            try
            {
                switch (style)
                {
                    case DateTimeStyle.Date:
                        dateout = DateTime.ParseExact(datetime, formatInput, null).ToString("dd/MM/yyyy");
                        break;
                    case DateTimeStyle.DateMMM:
                        dateout = DateTime.ParseExact(datetime, formatInput, null).ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture);
                        break;
                    case DateTimeStyle.ShortDateTime:
                        dateout = DateTime.ParseExact(datetime, formatInput, null).ToString("dd-MMM-yyyy HH:mm", CultureInfo.InvariantCulture);
                        break;
                    case DateTimeStyle.DateTime:
                        dateout = DateTime.ParseExact(datetime, formatInput, null).ToString("dd-MMM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                        break;
                }
                //if (DateTimeStyle.Date == style)
                //    dateout = DateTime.ParseExact(datetime, formatInput, null).ToString("dd-MMM-yyyy");
            }
            catch (Exception e)
            {
            }
            return dateout;
        }

        public static string FormatDatetime(string datetime, string formatOutput)
        {
            string dateout = "";

            dateout = DateTime.Parse(datetime).ToString(formatOutput);

            return dateout;
        }
        public static DateTime IsDateTime1(string s)
        {
            return DateTime.Parse(s, new CultureInfo("vi-VN", false));
        }
        public static DateTime IsDateTime2(string s)
        {
            return DateTime.Parse(s);
        }

        public static double isDouble(string s, bool isCulture)
        {

            if (isCulture)
            {
                CultureInfo dk = new CultureInfo("en-US");
                return double.Parse(s, dk);
            }
            else
            {
                return double.Parse(s);
            }

        }
        public static double CheckIsDouble(string s, bool isCulture = false)
        {
            try
            {
                if (isCulture)
                {
                    CultureInfo dk = new CultureInfo("en-US");
                    return double.Parse(s, dk);
                }
                else
                {
                    return double.Parse(s);
                }
            }
            catch { return 0; }

        }

        public static long isLong(string s, bool isCulture)
        {
            if (isCulture)
            {
                CultureInfo dk = new CultureInfo("en-US");
                return long.Parse(s, dk);
            }
            else
            {
                return long.Parse(s);
            }
        }
        public static decimal isDecimal(string s, bool isCulture)
        {
            if (string.IsNullOrEmpty(s))
            {
                return 0;
            }
            if (isCulture)
            {
                CultureInfo dk = new CultureInfo("en-US");
                return decimal.Parse(s, dk);
            }
            else
            {
                return decimal.Parse(s);
            }
        }

        /// <summary>
        /// ham nay format khi lay data tu database len
        /// </summary>
        /// <param name="m"></param>
        /// <param name="CCYID"></param>
        /// <returns></returns>
        public static string FormatMoney(string m, string CCYID)
        {
            try
            {
                CultureInfo dk = new CultureInfo("en-US");

                //vutran 03022015 fix error when convert myanmar money
                if (Thread.CurrentThread.CurrentCulture.Name == "lo-LA")
                {
                    m = m.Replace(".", ",");
                }

                string m1 = m;

                if (m == "" || m == "0" || m == "0,00" || m == "0.00")
                {
                    m1 = "0";
                }
                else
                {
                    m1 = double.Parse(m).ToString("N02", dk);
                }

                return m1;
            }
            catch (Exception)
            {
                return m;
            }
        }

        public static string FormatMoneyInputToView(string m, string CCYID)
        {
            try
            {
                CultureInfo dk = new CultureInfo("en-US");
                string m1 = m;

                if (m == "" || m == "0" || m == "0,00" || m == "0.00")
                {
                    m1 = "0";
                }
                else
                {
                    switch (CCYID)
                    {
                        case ("LAK"):
                            //return double.Parse(m).ToString(",", dk);
                            //return double.Parse(m,dk).ToString("#,##0.##");
                            //m1 = double.Parse(m,dk).ToString("#,#", dk); VND
                            m1 = double.Parse(m, dk).ToString("#,#0.00", dk);
                            //return string.Format(dk, "{0:0,0}", double.Parse(m,dk));
                            //return Double.Parse(m,dk.NumberFormat).ToString("N00", dk.NumberFormat);
                            //return String.Format(CultureInfo.CreateSpecificCulture("en-US"), "{0:C}",m);
                            break;
                        default:
                            //return double.Parse(m).ToString(",", dk);
                            //return double.Parse(m,dk).ToString("#,##0.##");
                            m1 = double.Parse(m, dk).ToString("#,#0.0000000", dk);
                            //return string.Format(dk, "{0:0,0}", double.Parse(m,dk));
                            //return Double.Parse(m,dk.NumberFormat).ToString("N00", dk.NumberFormat);
                            //return String.Format(CultureInfo.CreateSpecificCulture("en-US"), "{0:C}",m);
                            break;

                    }
                }

                return m1;
            }
            catch (Exception)
            {
                return m;
            }
        }

        /// <summary>
        /// ham nay format khi lay data tu bat ky dau ma so dang english
        /// </summary>
        /// <param name="m"></param>
        /// <param name="CCYID"></param>
        /// <returns></returns>
        public static string FormatMoneyInput(string m, string CCYID)
        {
            try
            {
                CultureInfo dk = new CultureInfo("en-US");
                string m1 = m;

                if (m == "" || m == "0" || m == "0,00" || m == "0.00")
                {
                    m1 = "0";
                }
                else
                {
                    switch (CCYID)
                    {
                        case ("LAK"):
                            //return double.Parse(m).ToString(",", dk);
                            //return double.Parse(m,dk).ToString("#,##0.##");
                            //m1 = double.Parse(m,dk).ToString("0,0", dk); VND
                            m1 = double.Parse(m, dk).ToString("0,0.00", dk);
                            //return string.Format(dk, "{0:0,0}", double.Parse(m,dk));
                            //return Double.Parse(m,dk.NumberFormat).ToString("N00", dk.NumberFormat);
                            //return String.Format(CultureInfo.CreateSpecificCulture("en-US"), "{0:C}",m);
                            break;
                        default:
                            //return double.Parse(m).ToString(",", dk);
                            //return double.Parse(m,dk).ToString("#,##0.##");
                            m1 = double.Parse(m, dk).ToString("0,0.00", dk);
                            //return string.Format(dk, "{0:0,0}", double.Parse(m,dk));
                            //return Double.Parse(m,dk.NumberFormat).ToString("N00", dk.NumberFormat);
                            //return String.Format(CultureInfo.CreateSpecificCulture("en-US"), "{0:C}",m);
                            break;

                    }
                }

                return m1.Replace(",", "");
            }
            catch (Exception)
            {
                return m;
            }
        }
        public static string LinkMultilang2(bool flag)
        {
            if (flag)
            {
                if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["l"] != null)
                {
                    return System.Web.HttpContext.Current.Request.RawUrl.ToString().Replace("&l=lo-LA", "").Replace("&l=zh-CN", "").Replace("&l=en-US", "");
                }
                else
                {
                    return "Default.aspx?";
                }
            }
            else
            {
                return System.Configuration.ConfigurationManager.AppSettings["homebank"] + "&";
            }

        }
        public static string LinkMultiLang(bool flag)
        {

            if (flag)
            {
                if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["p"] != null)
                {
                    return "Default.aspx?p=" + SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["p"] + "&";
                }
                else
                {
                    return "Default.aspx?";
                }
            }
            else
            {
                return SmartPortal.Common.Encrypt.EncryptURL("?p=129") + "&";
            }
        }
        #region Format Text, QueryString and Input Validation

        /// <summary>
        /// Performs querystring validation
        /// </summary>
        /// <returns>Validate for potential SQL and XSS injection</returns>
        public static string KillSqlInjection(string TexttoValidate)
        {
            string TextVal;

            TextVal = TexttoValidate;
            if (String.IsNullOrEmpty(TextVal))
            {
                return TextVal;
            }

            //Build an array of characters that need to be filter.
            string[] strDirtyQueryString = { "xp_", "$", "#", "%", "*", "^", "&", "!", ";", "--", "<", ">", "script", "iframe", "delete", "drop", "exec" };

            //Loop through all items in the array
            foreach (string item in strDirtyQueryString)
            {
                if (TextVal.IndexOf(item) != -1)
                {
                    TextVal = TextVal.Replace(item, "");
                }
            }

            return TextVal;
        }
        #endregion

        public static bool CheckSpecialCharacters(string text)
        {
            try
            {
                string[] strDirtyQueryString = { "xp_", "$", "#", "%", "*", "^", "&", "!", ";", "--", "<", ">", "script", "iframe", "delete", "drop", "exec" };
                foreach (string item in strDirtyQueryString)
                {
                    if (text.IndexOf(item) != -1)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public static int IsInt(string text)
        {
            int i = 0;
            i = int.Parse(text.Replace(",", "").Replace(".", ""));

            return i;
        }

        public static string GetError(int errorCode)
        {
            ErrorCodeModel EM = new ErrorCodeModel();
            EM = new ErrorBLL().Load(errorCode, System.Globalization.CultureInfo.CurrentCulture.ToString());


            return EM.ErrorDesc;
        }

        public static bool IsDateTime(string date)
        {
            try
            {
                DateTime.Parse(date);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool IsDateTimeViet(string date)
        {
            try
            {
                DateTime.Parse(date, new CultureInfo("vi-VN", false));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string FormatStringCore(string input)
        {
            //string oP= input.Split('.')[0];
            //return oP;

            return input;

        }

        public static string GenPassword(int lengt)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, lengt)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }
        public static string GetID(string prefix, string custcode, string custType, int length)
        {
            int a = length - (prefix + custcode + custType).Length;
            string pre = prefix + custcode + custType;

            if (a > 2)
            {
                string sub = "";
                for (int i = 0; i < a; i++)
                {
                    sub += System.Convert.ToInt32(new Random().Next(0, 9)).ToString();
                    System.Threading.Thread.Sleep(50);
                }

                return pre + sub;
            }
            else
            {
                return pre + new Random().Next(10000);
            }
        }
        static Int64 runningNumber = 1;

        public static string generateUniqueID()
        {
            if (runningNumber >= Int64.MaxValue - 3)
                runningNumber = 0;
            runningNumber = runningNumber + 1;
            return DateTime.Now.ToString("yyyyMMddHHmmssfff") + runningNumber.ToString();
        }
        public static string GetID(string Name, string CIFNo, string NIRC, params string[] preId)
        {
            string pre = string.Empty;
            List<string> lsPreIds = new List<string>();

            //Name = Name. Replace(" ", string.Empty);//trim customer fullname
            Name = Regex.Replace(Name, "[^0-9a-zA-Z]+", string.Empty);
            Name = Name.ToUpper();

            foreach (string item in preId)
            {
                lsPreIds.Add(item);
            }
            bool loop = true;
            do
            {
                pre = Name.Substring(0, 2);
                pre += CIFNo.Substring(CIFNo.Length - 2);
                pre += NIRC.Substring(NIRC.Length - 2);
                pre += new Random().Next(10, 99);
                SmartPortal.SEMS.User usr = new SEMS.User();
                DataTable dt = usr.GetUBID(pre);
                if (dt.Rows.Count == 0 && !lsPreIds.Contains(pre))
                    loop = false;
            } while (loop);

            return pre;
        }

        public static DataSet GetIDFromSQL(ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                object[] para = new object[] { };
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000113");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add("STORENAME", "SCHEDULE_GETID");
                hasInput.Add("PARA", para);


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

        public static string IntegerToWords(long inputNum)
        {
            int dig1, dig2, dig3, level = 0, lasttwo, threeDigits;

            string retval = "";
            string x = "";
            string[] ones ={
                "zero",
                "one",
                "two",
                "three",
                "four",
                "five",
                "six",
                "seven",
                "eight",
                "nine",
                "ten",
                "eleven",
                "twelve",
                "thirteen",
                "fourteen",
                "fifteen",
                "sixteen",
                "seventeen",
                "eighteen",
                "nineteen"
              };
            string[] tens ={
                "zero",
                "ten",
                "twenty",
                "thirty",
                "forty",
                "fifty",
                "sixty",
                "seventy",
                "eighty",
                "ninety"
              };
            string[] thou ={
                "",
                "thousand",
                "million",
                "billion",
                "trillion",
                "quadrillion",
                "quintillion"
              };

            bool isNegative = false;
            if (inputNum < 0)
            {
                isNegative = true;
                inputNum *= -1;
            }

            if (inputNum == 0)
                return ("zero");

            string s = inputNum.ToString();

            while (s.Length > 0)
            {
                // Get the three rightmost characters
                x = (s.Length < 3) ? s : s.Substring(s.Length - 3, 3);

                // Separate the three digits
                threeDigits = int.Parse(x);
                lasttwo = threeDigits % 100;
                dig1 = threeDigits / 100;
                dig2 = lasttwo / 10;
                dig3 = (threeDigits % 10);

                // append a "thousand" where appropriate
                if (level > 0 && dig1 + dig2 + dig3 > 0)
                {
                    retval = thou[level] + " " + retval;
                    retval = retval.Trim();
                }

                // check that the last two digits is not a zero
                if (lasttwo > 0)
                {
                    if (lasttwo < 20) // if less than 20, use "ones" only
                        retval = ones[lasttwo] + " " + retval;
                    else // otherwise, use both "tens" and "ones" array
                        retval = tens[dig2] + " " + ones[dig3] + " " + retval;
                }

                // if a hundreds part is there, translate it
                if (dig1 > 0)
                    retval = ones[dig1] + " hundred " + retval;

                s = (s.Length - 3) > 0 ? s.Substring(0, s.Length - 3) : "";
                level++;
            }

            while (retval.IndexOf("  ") > 0)
                retval = retval.Replace("  ", " ");

            retval = retval.Trim();

            if (isNegative)
                retval = "negative " + retval;
            retval = retval[0].ToString().ToUpper() + retval.Substring(1);
            return (retval);
        }
        public static DateTime parseExactDateTime(string strDate, string format = Constant.IPC.DEFAULTFORMATDATE)
        {
            return DateTime.ParseExact(strDate, format, System.Globalization.CultureInfo.InvariantCulture);
        }
        public static bool CheckIsDecimal(string value)
        {
            try
            {
                Convert.ToDecimal(value);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool isDateTimeStr(string strDate, string format = Constant.IPC.DEFAULTFORMATDATE)
        {
            try
            {
                DateTime date = DateTime.ParseExact(strDate, format, System.Globalization.CultureInfo.InvariantCulture);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        // Utility.Common
        private static byte[] CC_PP = Encoding.Unicode.GetBytes("CC");
        public static string EncryptCard(string sinput)
        {
            string result;
            try
            {
                byte[] bytes = Encoding.Unicode.GetBytes(sinput.ToString());
                HashAlgorithm hashAlgorithm = HashAlgorithm.Create("MD5");
                RijndaelManaged rijndaelManaged = new RijndaelManaged();
                rijndaelManaged.Key = hashAlgorithm.ComputeHash(CC_PP);
                ICryptoTransform transform = rijndaelManaged.CreateEncryptor(rijndaelManaged.Key, CC_IV);
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
                cryptoStream.Write(bytes, 0, bytes.Length);
                cryptoStream.FlushFinalBlock();
                byte[] inArray = memoryStream.ToArray();
                memoryStream.Close();
                cryptoStream.Close();
                rijndaelManaged.Clear();
                result = Convert.ToBase64String(inArray);
            }
            catch
            {
                result = "";
            }
            return result;
        }

        public static string DecryptCard(string sinput)
        {
            string result;
            try
            {
                byte[] array = Convert.FromBase64String(sinput.ToString());
                HashAlgorithm hashAlgorithm = HashAlgorithm.Create("MD5");
                RijndaelManaged rijndaelManaged = new RijndaelManaged();
                rijndaelManaged.Key = hashAlgorithm.ComputeHash(CC_PP);
                ICryptoTransform transform = rijndaelManaged.CreateDecryptor(rijndaelManaged.Key, CC_IV);
                MemoryStream memoryStream = new MemoryStream(array);
                CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Read);
                byte[] array2 = new byte[array.Length];
                cryptoStream.Read(array2, 0, array2.Length);
                memoryStream.Close();
                cryptoStream.Close();
                result = Encoding.Unicode.GetString(array2).Replace("\0", "");
            }
            catch
            {
                result = "";
            }
            return result;
        }
        private static byte[] CC_IV = new byte[]
{
    0,
    6,
    2,
    8,
    8,
    2,
    1,
    7,
    4,
    2,
    3,
    2,
    9,
    6,
    8,
    9
};
        public static string MaskDigits(string input)
        {
            try
            {
                string tmp = SmartPortal.Control.Security.DecryptCard(input.Trim());
                input = string.IsNullOrEmpty(tmp) ? input : tmp;
            }
            catch { }

            var cardNumber = input;
            int leftmask = int.Parse(ConfigurationManager.AppSettings["leftmask"].ToString());
            int rightmask = int.Parse(ConfigurationManager.AppSettings["rightmask"].ToString());


            var firstDigits = cardNumber.Substring(0, leftmask);
            var lastDigits = cardNumber.Substring(cardNumber.Length - 4, rightmask);

            var requiredMask = new String('*', cardNumber.Length - firstDigits.Length - lastDigits.Length);

            var maskedString = string.Concat(firstDigits, requiredMask, lastDigits);
            var maskedCardNumberWithSpaces = Regex.Replace(maskedString, ".{4}", "$0 ");
            return maskedString;
        }

        public static string NumtoWords(double n)
        {
            string words = "";
            double intPart;
            double decPart = 0;
            if (n == 0)
                return "zero";
            try
            {
                string[] splitter = n.ToString().Split('.');
                intPart = double.Parse(splitter[0]);
                decPart = double.Parse(splitter[1]);
            }
            catch
            {
                intPart = n;
            }

            words = NumWords(intPart);

            if (decPart > 0)
            {
                if (words != "")
                    words += " and ";
                int counter = decPart.ToString().Length;
                switch (counter)
                {
                    case 1: words += NumWords(decPart) + " tenths"; break;
                    case 2: words += NumWords(decPart) + " hundredths"; break;
                    case 3: words += NumWords(decPart) + " thousandths"; break;
                    case 4: words += NumWords(decPart) + " ten-thousandths"; break;
                    case 5: words += NumWords(decPart) + " hundred-thousandths"; break;
                    case 6: words += NumWords(decPart) + " millionths"; break;
                    case 7: words += NumWords(decPart) + " ten-millionths"; break;
                }
            }
            return words;
        }

        static String NumWords(double n) //converts double to words
        {
            string[] numbersArr = new string[] { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
            string[] tensArr = new string[] { "twenty", "thirty", "fourty", "fifty", "sixty", "seventy", "eighty", "ninty" };
            string[] suffixesArr = new string[] { "thousand", "million", "billion", "trillion", "quadrillion", "quintillion", "sextillion", "septillion", "octillion", "nonillion", "decillion", "undecillion", "duodecillion", "tredecillion", "Quattuordecillion", "Quindecillion", "Sexdecillion", "Septdecillion", "Octodecillion", "Novemdecillion", "Vigintillion" };
            string words = "";

            bool tens = false;

            if (n < 0)
            {
                words += "negative ";
                n *= -1;
            }

            int power = (suffixesArr.Length + 1) * 3;

            while (power > 3)
            {
                double pow = Math.Pow(10, power);
                if (n >= pow)
                {
                    if (n % pow > 0)
                    {
                        words += NumWords(Math.Floor(n / pow)) + " " + suffixesArr[(power / 3) - 1] + ", ";
                    }
                    else if (n % pow == 0)
                    {
                        words += NumWords(Math.Floor(n / pow)) + " " + suffixesArr[(power / 3) - 1];
                    }
                    n %= pow;
                }
                power -= 3;
            }
            if (n >= 1000)
            {
                if (n % 1000 > 0) words += NumWords(Math.Floor(n / 1000)) + " thousand, ";
                else words += NumWords(Math.Floor(n / 1000)) + " thousand";
                n %= 1000;
            }
            if (0 <= n && n <= 999)
            {
                if ((int)n / 100 > 0)
                {
                    words += NumWords(Math.Floor(n / 100)) + " hundred";
                    n %= 100;
                }
                if ((int)n / 10 > 1)
                {
                    if (words != "")
                        words += " ";
                    words += tensArr[(int)n / 10 - 2];
                    tens = true;
                    n %= 10;
                }

                if (n < 20 && n > 0)
                {
                    if (words != "" && tens == false)
                        words += " ";
                    words += (tens ? "-" + numbersArr[(int)n - 1] : numbersArr[(int)n - 1]);
                    n -= Math.Floor(n);
                }
            }

            return words;

        }
        public static string EncodeUserid(string userid)
        {
            Guid guid = new Guid(userid);
            string encoded = Convert.ToBase64String(guid.ToByteArray());
            encoded = encoded.Replace("/", "_").Replace("+", "-");
            return encoded.Substring(0, 22);
        }
        public static string DecodeUserid(string value)
        {
            value = value.Replace("_", "/").Replace("-", "+");
            byte[] buffer = Convert.FromBase64String(value + "==");
            return new Guid(buffer).ToString().ToUpper();
        }

        #region init Iso4217 currency data
        public class Iso4217Definition
        {
            private readonly string _code;
            private readonly int _number;
            private readonly int _exponent;
            public bool Found { get; set; }

            public string Code
            {
                get { return _code; }
            }

            public int Number
            {
                get { return _number; }
            }

            public int Exponent
            {
                get { return _exponent; }
            }

            private Iso4217Definition() { }

            public Iso4217Definition(string code, int number, int exponent)
            {
                _code = code;
                _number = number;
                _exponent = exponent;
                Found = true;
            }

            public static Iso4217Definition NotFound()
            {
                return new Iso4217Definition { Found = false };
            }
        }
        // http://en.wikipedia.org/wiki/ISO_4217
        private static readonly List<Iso4217Definition> DefinitionCollection = new List<Iso4217Definition> {
            new Iso4217Definition("AED", 784, 2),
            new Iso4217Definition("AFN", 971, 2),
            new Iso4217Definition("ALL", 8, 2),
            new Iso4217Definition("AMD", 51, 2),
            new Iso4217Definition("ANG", 532, 2),
            new Iso4217Definition("AOA", 973, 2),
            new Iso4217Definition("ARS", 32, 2),
            new Iso4217Definition("AUD", 36, 2),
            new Iso4217Definition("AWG", 533, 2),
            new Iso4217Definition("AZN", 944, 2),
            new Iso4217Definition("BAM", 977, 2),
            new Iso4217Definition("BBD", 52, 2),
            new Iso4217Definition("BDT", 50, 2),
            new Iso4217Definition("BGN", 975, 2),
            new Iso4217Definition("BHD", 48, 3),
            new Iso4217Definition("BIF", 108, 0),
            new Iso4217Definition("BMD", 60, 2),
            new Iso4217Definition("BND", 96, 2),
            new Iso4217Definition("BOB", 68, 2),
            new Iso4217Definition("BOV", 984, 2),
            new Iso4217Definition("BRL", 986, 2),
            new Iso4217Definition("BSD", 44, 2),
            new Iso4217Definition("BTN", 64, 2),
            new Iso4217Definition("BWP", 72, 2),
            new Iso4217Definition("BYR", 974, 0),
            new Iso4217Definition("BZD", 84, 2),
            new Iso4217Definition("CAD", 124, 2),
            new Iso4217Definition("CDF", 976, 2),
            new Iso4217Definition("CHE", 947, 2),
            new Iso4217Definition("CHF", 756, 2),
            new Iso4217Definition("CHW", 948, 2),
            new Iso4217Definition("CLF", 990, 0),
            new Iso4217Definition("CLP", 152, 0),
            new Iso4217Definition("CNY", 156, 2),
            new Iso4217Definition("COP", 170, 2),
            new Iso4217Definition("COU", 970, 2),
            new Iso4217Definition("CRC", 188, 2),
            new Iso4217Definition("CUC", 931, 2),
            new Iso4217Definition("CUP", 192, 2),
            new Iso4217Definition("CVE", 132, 0),
            new Iso4217Definition("CZK", 203, 2),
            new Iso4217Definition("DJF", 262, 0),
            new Iso4217Definition("DKK", 208, 2),
            new Iso4217Definition("DOP", 214, 2),
            new Iso4217Definition("DZD", 12, 2),
            new Iso4217Definition("EGP", 818, 2),
            new Iso4217Definition("ERN", 232, 2),
            new Iso4217Definition("ETB", 230, 2),
            new Iso4217Definition("EUR", 978, 2),
            new Iso4217Definition("FJD", 242, 2),
            new Iso4217Definition("FKP", 238, 2),
            new Iso4217Definition("GBP", 826, 2),
            new Iso4217Definition("GEL", 981, 2),
            new Iso4217Definition("GHS", 936, 2),
            new Iso4217Definition("GIP", 292, 2),
            new Iso4217Definition("GMD", 270, 2),
            new Iso4217Definition("GNF", 324, 0),
            new Iso4217Definition("GTQ", 320, 2),
            new Iso4217Definition("GYD", 328, 2),
            new Iso4217Definition("HKD", 344, 2),
            new Iso4217Definition("HNL", 340, 2),
            new Iso4217Definition("HRK", 191, 2),
            new Iso4217Definition("HTG", 332, 2),
            new Iso4217Definition("HUF", 348, 2),
            new Iso4217Definition("IDR", 360, 2),
            new Iso4217Definition("ILS", 376, 2),
            new Iso4217Definition("INR", 356, 2),
            new Iso4217Definition("IQD", 368, 3),
            new Iso4217Definition("IRR", 364, 0),
            new Iso4217Definition("ISK", 352, 0),
            new Iso4217Definition("JMD", 388, 2),
            new Iso4217Definition("JOD", 400, 3),
            new Iso4217Definition("JPY", 392, 0),
            new Iso4217Definition("KES", 404, 2),
            new Iso4217Definition("KGS", 417, 2),
            new Iso4217Definition("KHR", 116, 2),
            new Iso4217Definition("KMF", 174, 0),
            new Iso4217Definition("KPW", 408, 0),
            new Iso4217Definition("KRW", 410, 0),
            new Iso4217Definition("KWD", 414, 3),
            new Iso4217Definition("KYD", 136, 2),
            new Iso4217Definition("KZT", 398, 2),
            new Iso4217Definition("LAK", 418, 0),
            new Iso4217Definition("LBP", 422, 0),
            new Iso4217Definition("LKR", 144, 2),
            new Iso4217Definition("LRD", 430, 2),
            new Iso4217Definition("LSL", 426, 2),
            new Iso4217Definition("LTL", 440, 2),
            new Iso4217Definition("LVL", 428, 2),
            new Iso4217Definition("LYD", 434, 3),
            new Iso4217Definition("MAD", 504, 2),
            new Iso4217Definition("MDL", 498, 2),
            new Iso4217Definition("MGA", 969, 2),
            new Iso4217Definition("MKD", 807, 0),
            new Iso4217Definition("MMK", 104, 0),
            new Iso4217Definition("MNT", 496, 2),
            new Iso4217Definition("MOP", 446, 2),
            new Iso4217Definition("MRO", 478, 2),
            new Iso4217Definition("MUR", 480, 2),
            new Iso4217Definition("MVR", 462, 2),
            new Iso4217Definition("MWK", 454, 2),
            new Iso4217Definition("MXN", 484, 2),
            new Iso4217Definition("MXV", 979, 2),
            new Iso4217Definition("MYR", 458, 2),
            new Iso4217Definition("MZN", 943, 2),
            new Iso4217Definition("NAD", 516, 2),
            new Iso4217Definition("NGN", 566, 2),
            new Iso4217Definition("NIO", 558, 2),
            new Iso4217Definition("NOK", 578, 2),
            new Iso4217Definition("NPR", 524, 2),
            new Iso4217Definition("NZD", 554, 2),
            new Iso4217Definition("OMR", 512, 3),
            new Iso4217Definition("PAB", 590, 2),
            new Iso4217Definition("PEN", 604, 2),
            new Iso4217Definition("PGK", 598, 2),
            new Iso4217Definition("PHP", 608, 2),
            new Iso4217Definition("PKR", 586, 2),
            new Iso4217Definition("PLN", 985, 2),
            new Iso4217Definition("PYG", 600, 0),
            new Iso4217Definition("QAR", 634, 2),
            new Iso4217Definition("RON", 946, 2),
            new Iso4217Definition("RSD", 941, 2),
            new Iso4217Definition("RUB", 643, 2),
            new Iso4217Definition("RWF", 646, 0),
            new Iso4217Definition("SAR", 682, 2),
            new Iso4217Definition("SBD", 90, 2),
            new Iso4217Definition("SCR", 690, 2),
            new Iso4217Definition("SDG", 938, 2),
            new Iso4217Definition("SEK", 752, 2),
            new Iso4217Definition("SGD", 702, 2),
            new Iso4217Definition("SHP", 654, 2),
            new Iso4217Definition("SLL", 694, 0),
            new Iso4217Definition("SOS", 706, 2),
            new Iso4217Definition("SRD", 968, 2),
            new Iso4217Definition("SSP", 728, 2),
            new Iso4217Definition("STD", 678, 0),
            new Iso4217Definition("SYP", 760, 2),
            new Iso4217Definition("SZL", 748, 2),
            new Iso4217Definition("THB", 764, 2),
            new Iso4217Definition("TJS", 972, 2),
            new Iso4217Definition("TMT", 934, 2),
            new Iso4217Definition("TND", 788, 3),
            new Iso4217Definition("TOP", 776, 2),
            new Iso4217Definition("TRY", 949, 2),
            new Iso4217Definition("TTD", 780, 2),
            new Iso4217Definition("TWD", 901, 2),
            new Iso4217Definition("TZS", 834, 2),
            new Iso4217Definition("UAH", 980, 2),
            new Iso4217Definition("UGX", 800, 2),
            new Iso4217Definition("USD", 840, 2),
            new Iso4217Definition("USN", 997, 2),
            new Iso4217Definition("USS", 998, 2),
            new Iso4217Definition("UYI", 940, 0),
            new Iso4217Definition("UYU", 858, 2),
            new Iso4217Definition("UZS", 860, 2),
            new Iso4217Definition("VEF", 937, 2),
            new Iso4217Definition("VND", 704, 0),
            new Iso4217Definition("VUV", 548, 0),
            new Iso4217Definition("WST", 882, 2),
            new Iso4217Definition("XAF", 950, 0),
            new Iso4217Definition("XCD", 951, 2),
            new Iso4217Definition("XOF", 952, 0),
            new Iso4217Definition("XPF", 953, 0),
            new Iso4217Definition("YER", 886, 2),
            new Iso4217Definition("ZAR", 710, 2),
            new Iso4217Definition("ZMW", 967, 2)    };

        public static object O9Encryptpass { get; set; }
        #endregion





        #region Convert_Amount_to_Text_Laos
        public static string GetNumber2Text(double Amount)
        {
            var nav = Amount < 0 ? true : false;
            // var decPlaces = (long)(((double)Math.Abs(Amount) % 1) * 100);
            // var decPlaces = Amount.ToString().Split('.').Count() > 1 ? Amount.ToString().Split('.').ToList().ElementAt(1).ToString() : "0";

            var decPlaces = Amount.ToString().IndexOf(".") < 0 ? "0" : (Amount.ToString().Substring(Amount.ToString().IndexOf(".") + 1)).ToString();
            var flt = (long)(double)Math.Abs(Amount);

            var retVal = String.Empty;
            if (flt.ToString().Length > 13) return string.Empty;

            if (decPlaces == "0")
            {
                retVal = NbToTxt(flt);
            }
            else
            {
                retVal = NbToTxt(flt) + "ຈຸດ" + NbToTxt(Convert.ToDouble(decPlaces));
            }
            return (nav ? "ລົບ" : "") + retVal;
        }
        public static string GetNumber2Text(double Amount, string CCY)
        {
            var ccyName = string.Empty;
            switch (CCY == null ? "LAK" : CCY)
            {
                case "LAK": ccyName = "ກີບຖ້ວນ"; break;
                case "USD": ccyName = "ໂດລາສະຫະລັດຖ້ວນ"; break;
                case "THB": ccyName = "ບາດຖ້ວນ"; break;
            }

            var retVal = GetNumber2Text(Amount);
            return retVal == string.Empty ? "Unsupport!" : retVal + ccyName;
        }

        private static string NbToTxt(double nbr)
        {
            int L;
            String s = string.Empty;
            String sn = string.Empty;
            String snbr = string.Empty;
            String sunit = string.Empty;

            if (nbr == 0)
            {
                return "ສູນ";
            }
            snbr = nbr.ToString();
            L = snbr.Length;
            sn = "";
            for (int i = 1; i <= L; i++)
            {
                s = snbr.Substring((L - i), 1);
                switch (i)
                {
                    case 1:
                        if (L == 1)
                        {
                            sn = NbToTxt11(s);
                        }
                        else
                        {
                            if (snbr.Substring(L - 2 + 1, 1) != "0")
                                sn = NbToTxt1(s);
                            else
                                sn = NbToTxt11(s);
                        }
                        break;
                    case 2:
                        sn = (sn != "" || s != "0") ? NbToTxt2(s) + sn : "";
                        break;
                    case 3:
                        sn = NbToTxt11(s) + (s != "0" ? "ຮ້ອຍ" : "") + sn;
                        break;
                    case 4:
                        if (L == 4)
                        {
                            sn = NbToTxt11(s) + "ພັນ" + sn;
                        }
                        else if (L == 5)
                        {
                            if (snbr.Substring(L - 5 + 1, 1) != "0")
                                sn = NbToTxt1(s) + "ພັນ" + sn;
                            else
                                sn = NbToTxt11(s) + "ພັນ" + sn;
                        }
                        else if (L == 6)
                        {
                            //x00,xxx
                            if (snbr.Substring(L - 6 + 1, 1) == "0" && snbr.Substring(L - 6 + 2, 1) == "0")
                                sn = NbToTxt1(s) + "" + sn;
                            else if (snbr.Substring(L - 6 + 1, 1) != "0")
                                sn = NbToTxt1(s) + "ພັນ" + sn;
                            else
                                sn = NbToTxt11(s) + "ພັນ" + sn;
                        }
                        else if (L >= 7)
                        {
                            //x,x00,0xx
                            if (snbr.Substring(L - 5 + 1, 1) == "0" && snbr.Substring(L - 6 + 1, 1) == "0" && snbr.Substring(L - 4 + 1, 1) == "0")
                            {
                                if (snbr.Substring(L - 5 + 1, 1) != "0")
                                    sn = NbToTxt1(s) + sn;
                                else
                                    sn = NbToTxt11(s) + sn;
                            }
                            else
                            {
                                if (snbr.Substring(L - 5 + 1, 1) != "0")
                                    if (snbr.Substring(L - 5, 1) == "0")
                                        sn = NbToTxt11(s) + "ພັນ" + sn;
                                    else
                                        sn = NbToTxt1(s) + "ພັນ" + sn;
                                else
                                    //x,x00,xxx
                                    if (snbr.Substring(L - 5, 1) == "0" && snbr.Substring(L - 5 + 1, 1) == "0")
                                    sn = NbToTxt11(s) + "" + sn;
                                else
                                    sn = NbToTxt11(s) + "ພັນ" + sn;
                            }
                        }
                        break;
                    case 5:
                        sn = NbToTxt2(s) + sn;
                        break;
                    case 6:
                        sn = NbToTxt11(s) + (s != "0" ? "ແສນ" : "") + sn;
                        break;
                    case 7:
                        if (L == 7)
                            sn = NbToTxt11(s) + (s != "0" ? "ລ້ານ" : "") + sn;
                        else
                            if (snbr.Substring(L - 8 + 1, 1) != "0")
                            sn = NbToTxt1(s) + "ລ້ານ" + sn;
                        else
                            sn = NbToTxt11(s) + "ລ້ານ" + sn;
                        break;
                    case 8:
                        sn = NbToTxt2(s) + sn;
                        break;
                    case 9:
                        sn = NbToTxt11(s) + (s != "0" ? "ຮ້ອຍ" : "") + sn; //  ' like 3 : thousand 
                        break;
                    case 10:     //    ' like 4 thousands
                        if (L == i)
                            sn = NbToTxt11(s) + "ພັນ" + sn;
                        else if (L == i + 1)
                            sn = NbToTxt1(s) + "ພັນ" + sn;
                        else if (L == i + 2)

                            if (snbr.Substring(L - i - 1, 1) == "0" && snbr.Substring(L - i, 1) == "0")
                                sn = NbToTxt1(s) + "" + sn;
                            else if (snbr.Substring(L - i - 1, 1) == "0" && snbr.Substring(L - i, 1) == "1")
                                sn = NbToTxt11(s) + "ພັນ" + sn;
                            else
                                sn = NbToTxt1(s) + "ພັນ" + sn;
                        else if (L >= i + 3)
                            if (snbr.Substring(L - i - 1, 1) == "0" && snbr.Substring(L - i, 1) == "0")
                                sn = NbToTxt1(s) + "" + sn;
                            else if (snbr.Substring(L - i - 1, 1) == "0" && snbr.Substring(L - i, 1) == "1")
                                sn = NbToTxt11(s) + "ພັນ" + sn;
                            else
                                sn = NbToTxt1(s) + "ພັນ" + sn;
                        break;
                    case 11:
                        sn = NbToTxt2(s) + sn; //' like 5, 10 thousand
                        break;
                    case 12:
                        // sn = NbToTxt11(s) + (s != "0" ? "ຮ້ອຍ" : "") + sn; // 'like 6 100 thousands
                        sn = NbToTxt11(s) + (s != "0" ? "ແສນ" : "") + sn; // 'like 6 100 thousands
                        break;
                    case 13: //' like 7 million, so I have million of million. it is now the limit, 13 digit
                        if (L == 13)
                            sn = NbToTxt11(s) + (s != "0" ? "ລ້ານ" : "") + sn;
                        else
                            sn = snbr.Substring(L - 14 + 1, 1) != "0" ? NbToTxt1(s) + "ລ້ານ" + sn : NbToTxt11(s) + "ລ້ານ" + sn;
                        break;
                }
            }
            return sn;
        }

        static string NbToTxt1(string nbr)
        {
            string s = string.Empty;
            switch (nbr)
            {
                case "1": s = "ເອັດ"; break;
                case "2": s = "ສອງ"; break;
                case "3": s = "ສາມ"; break;
                case "4": s = "ສີ່"; break;
                case "5": s = "ຫ້າ"; break;
                case "6": s = "ຫົກ"; break;
                case "7": s = "ເຈັດ"; break;
                case "8": s = "ແປດ"; break;
                case "9": s = "ເກົ້າ"; break;
            }
            return s;
        }


        static string NbToTxt11(string nbr)
        {
            string s = string.Empty;
            switch (nbr)
            {
                case "1": s = "ໜຶ່ງ"; break;
                default: s = NbToTxt1(nbr); break;
            }
            return s;
        }

        static string NbToTxt2(string nbr)
        {
            string s = string.Empty;
            switch (nbr)
            {
                case "1": s = "ສິບ"; break;
                case "2": s = "ຊາວ"; break;
                default: s = NbToTxt1(nbr) + (NbToTxt1(nbr) != "" ? "ສິບ" : ""); break;
            }
            return s;
        }
        #endregion
    }
}
