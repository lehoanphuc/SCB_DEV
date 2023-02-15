using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using SmartPortal.DAL;
using System.IO;
using System.Linq;
using System.Web;
using SmartPortal.Model;
/// <summary>
/// Summary description for GenarateQR
/// </summary>
namespace SmartPortal.Common
{
    public class GenarateQRnew
    {
        public void GenQRCode(Dictionary<string, object> dicQR, ref string sourcevalue)
        {
            foreach (var item in dicQR)
            {
                string _tag = item.Key;
                string _data = string.Empty;
                if (item.Value is Dictionary<string, object>)
                {
                    GenQRCode((Dictionary<string, object>) item.Value, ref _data);
                }
                else
                {
                    _data = item.Value.ToString().Trim();
                }

                string _len =
                    _data.Length >= 10
                        ? _data.Length.ToString()
                        : string.Format("0{0}", _data.Length); /*$"0{_data.Length}";*/
                //string _tagdata = $"{_tag}{_len}{_data}";
                string _tagdata = string.Format("{0}{1}{2}", _tag, _len, _data);
                sourcevalue += _tagdata;
            }

        }

        public void GenConfigQrCode(ref Dictionary<string, object> dicQrcode, DataSet dsconfigqr, Hashtable Data,
            bool flag, string src = "")
        {
            var dtobjectnoCode = new List<DataRow>();
            if (flag)
            {
                dtobjectnoCode = dsconfigqr.Tables[0].AsEnumerable().Where(x => (string) x["CODE"] == "")
                    .OrderBy(y => (string) y["KEYQR"]).ToList();
            }
            else
            {
                dtobjectnoCode = dsconfigqr.Tables[0].AsEnumerable().Where(x => (string) x["CODE"] == src)
                    .OrderBy(y => (string) y["KEYQR"]).ToList();
            }

            for (int i = 0; i < dtobjectnoCode.Count; i++)
            {
                if (dtobjectnoCode[i]["SOURCETYPE"].ToString() == "VALUE")
                {
                    dicQrcode.Add(dtobjectnoCode[i]["KEYQR"].ToString(), dtobjectnoCode[i]["SOURCE"].ToString());
                }
                else if (dtobjectnoCode[i]["SOURCETYPE"].ToString() == "DATA")
                {
                    dicQrcode.Add(dtobjectnoCode[i]["KEYQR"].ToString(), Data[dtobjectnoCode[i]["SOURCE"].ToString()]);
                }
                else if (dtobjectnoCode[i]["SOURCETYPE"].ToString() == "GENQR")
                {
                    Dictionary<string, object> dicQrcodeObj = new Dictionary<string, object>();
                    GenConfigQrCode(ref dicQrcodeObj, dsconfigqr, Data, false, dtobjectnoCode[i]["SOURCE"].ToString());
                    dicQrcode.Add(dtobjectnoCode[i]["KEYQR"].ToString(), dicQrcodeObj);
                }
            }

        }
    }
}