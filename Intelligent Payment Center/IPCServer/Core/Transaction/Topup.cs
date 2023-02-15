using System;
using System.Collections.Generic;
using System.Collections;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Data;
using DBConnection;
using Microsoft.VisualBasic.FileIO;
using Transaction;
using Utility;
using Interfaces;
using System.Reflection;
using System.IO;


namespace Transaction
{
    public class Topup
    {
        public bool ImportTopup(TransactionInfo tran)
        {
            string rptpath = "";
            try
            {
                DataTable dt = new DataTable("TOP_Topup");
                DataTable dtrpt=new DataTable("Topup_Report");
                DataTable tmp = GetDataTabletFromCSVFile(tran.Data[Common.KEYNAME.PATH].ToString());

                dt.Columns.Add("TopupID", typeof(string));
                dt.Columns.Add("TelcoID", typeof(Int32));
                dt.Columns.Add("CardType", typeof(string));
                dt.Columns.Add("SoftPin", typeof(String));
                dt.Columns.Add("SerialNo", typeof(String));
                dt.Columns.Add("BuyRate", typeof(String));
                dt.Columns.Add("DateCreated", typeof(String));
                dt.Columns.Add("UserCreated", typeof(String));
                dt.Columns.Add("ExpireTime", typeof(String));
                dt.Columns.Add("DateModified", typeof(String));
                dt.Columns.Add("UserModified", typeof(String));
                dt.Columns.Add("Status", typeof(String));
                dt.Columns.Add("IsUsed", typeof(bool));

                dtrpt.Columns.Add("SoftPin", typeof(String));
                dtrpt.Columns.Add("SerialNo", typeof(String));
                dtrpt.Columns.Add("DateCreated", typeof(String));
                dtrpt.Columns.Add("UserCreated", typeof(String));
                dtrpt.Columns.Add("ExpireTime", typeof(String));
                dtrpt.Columns.Add("Status", typeof(String));

                foreach (DataRow dr in tmp.Rows)
                {
                    //dt import vao data base
                    DataRow workRow = dt.NewRow();
                    workRow["TopupID"] = dr["SerialNo"];
                    workRow["TelcoID"] = tran.Data[Common.KEYNAME.TELCO];
                    workRow["CardType"] = tran.Data[Common.KEYNAME.AMOUNT];
                    workRow["SoftPin"] = Utility.Common.EncryptData(dr["SoftPin"].ToString());
                    workRow["SerialNo"] = dr["SerialNo"];
                    workRow["BuyRate"] = tran.Data[Common.KEYNAME.BUYRATE].ToString();
                    workRow["DateCreated"] = DateTime.Now.ToShortDateString();
                    workRow["UserCreated"] = tran.Data[Common.KEYNAME.USERID];
                    workRow["ExpireTime"] = dr["ExpireTime"];
                    workRow["DateModified"] = "";
                    workRow["UserModified"] = "";
                    workRow["Status"] = "A";
                    workRow["IsUsed"] = false;
                    dt.Rows.Add(workRow);

                    //dt report
                    string sp = dr["SoftPin"].ToString();
                    sp = "********" + sp.Substring(sp.Length - 8, 8);

                    DataRow workRowrpt = dtrpt.NewRow();
                    workRowrpt["SoftPin"] = sp;
                    workRowrpt["SerialNo"] = dr["SerialNo"].ToString();
                    workRowrpt["DateCreated"] = DateTime.Now.ToShortDateString();
                    workRowrpt["UserCreated"] = tran.Data[Common.KEYNAME.USERID].ToString();
                    workRowrpt["ExpireTime"] = dr["ExpireTime"].ToString();
                    workRowrpt["Status"] = "Success";
                    dtrpt.Rows.Add(workRowrpt);

                }
                tran.Data.Add(Common.KEYNAME.DATATABLE, dt);
                //rptpath = CreateReportFile(dtrpt, tran.Data[Common.KEYNAME.PATH].ToString());
                tran.Data.Add(Common.KEYNAME.RPTPATH, rptpath);
                

                return true;
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name + "(" + tran.IPCTransID.ToString() + ")");
                tran.SetErrorInfo(ex);
                return false;
            }
        }
        public string CreateReportFile(DataTable dtrpt, string uri)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                uri = uri.Substring(0, uri.LastIndexOf(".")) + "_rpt_" + DateTime.Now.ToString("dd-MM-yyyy H-mm-ss") + ".csv";

                string[] columnNames = dtrpt.Columns.Cast<DataColumn>().
                                                  Select(column => column.ColumnName).
                                                  ToArray();
                sb.AppendLine(string.Join(",", columnNames));

                foreach (DataRow row in dtrpt.Rows)
                {
                    string[] fields = row.ItemArray.Select(field => field.ToString()).
                                                    ToArray();
                    sb.AppendLine(string.Join(",", fields));
                }
                File.WriteAllText(uri, sb.ToString());

                return uri;
            }
            catch (Exception ex)
            {
                return ex.ToString() + uri;
            }
        }
        public bool DecryptSoftpin(TransactionInfo tran)
        {
            tran.Data["SOFTPIN"] = Utility.Common.DecryptData(tran.Data["SOFTPIN"].ToString());
            return true;
        }
        private static DataTable GetDataTabletFromCSVFile(string csv_file_path)
        {
            DataTable csvData = new DataTable();
            try
            {
              using(TextFieldParser csvReader = new TextFieldParser(csv_file_path))
                 {
                    csvReader.SetDelimiters(new string[] { "," });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    string[] colFields = csvReader.ReadFields();
                    foreach (string column in colFields)
                    {
                        DataColumn datecolumn = new DataColumn(column);
                        datecolumn.AllowDBNull = true;
                        csvData.Columns.Add(datecolumn);
                    }
                    while (!csvReader.EndOfData)
                    {
                        string[] fieldData = csvReader.ReadFields();
                        //Making empty value as null
                        for (int i = 0; i < fieldData.Length; i++)
                        {
                            if (fieldData[i] == "")
                            {
                                fieldData[i] = null;
                            }
                        }
                        csvData.Rows.Add(fieldData);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return csvData;
        }
      }
}
