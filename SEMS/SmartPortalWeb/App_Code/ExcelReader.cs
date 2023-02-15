using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Linq;
using MyExcelExport;
using System.Data;
using System.Text.RegularExpressions;
using System.Collections;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Threading;
using System.Web;
using SmartPortal.DAL;


//delegate void OnCellData(char Column, int RowNumber, string value);                                             // Callback that will process our data. Warning MAX NUMBER OF COLUMNS is 25. From 'A' to  'Z'

public class ExcelReader
{
    public OoXml source;
    public gSheet sheet;
    bool IsText;
    string NumCol;
    public int NumRow;
    //OnCellData OnData;

    public ExcelReader(string filename)
    {
        source = new OoXml(filename);                                                                           // Open the excel file
        sheet = source.sheets.Values.First<gSheet>();                                                           // Get the first sheet
    }

    public void Process(string service, int numper, int limit, ref Queue<DataTable> ds, int skip)
    {
        //OnData = ondata;                                                                                        // This is the callback that will receive the data
        Stream inStream = sheet.GetStream();                                                                    // This is the data stream
        XmlReaderSettings settings = new XmlReaderSettings();
        settings.LinePositionOffset = 0;
        settings.LineNumberOffset = 10;
        using (XmlReader reader = XmlReader.Create(inStream, settings))                                                   // We will read the stream as XML
            LoopSheet(reader, service, ref ds, numper, 1, limit, skip);
    }

    private void GetCellInfo(XmlReader r)
    {
        IsText = false;                                                                                         // Let's assume this is a numeric cell
        NumCol = " ";                                                                                           // With no column
        while (r.MoveToNextAttribute())                                                                         // Loop all attributes
        {
            if (r.Name == "t") if (r.Value == "s") IsText = true;                                               // If there is a [t="s"] attribute  then this is a TEXT cell   
            if (r.Name == "r") if (r.Value.Length > 1)
                    //NumCol = r.Value[0];
                    NumCol = Regex.Replace(r.Value, @"[\d-]", string.Empty);
        }
    }
    //public bool IsImport
    //{
    //    get { return HttpContext.Current.Session["IsImport"] != null ? (bool)HttpContext.Current.Session["IsImport"] : true; }
    //    set { value = HttpContext.Current.Session["IsImport"] != null ? (bool)HttpContext.Current.Session["IsImport"] : true; }
    //}
    private void LoopSheet(XmlReader r, string service, ref Queue<DataTable> ds, int numItem, int min = 1, int limit = 0, int skip = 0)
    {
        bool InValue = false;
        // Flag that tells if we are in a data node 
        DataTable dt = CreateDataTable();
        while (r.Read() && (limit == 0 || limit + min >= NumRow))
        {
            switch (r.NodeType)
            {
                case XmlNodeType.Element:
                    InValue = false;
                    if (r.Name == "row")
                    {
                        if (NumRow < min)
                        {
                            NumRow++;
                            continue;
                        }
                        else
                        {
                            if ((NumRow - min) % numItem == 0 && NumRow - min != 0)
                            {
                                DataTable dtCopy = dt.Copy();
                                if (NumRow <= numItem + 1) //first dt, remove skip line
                                {
                                    for (int i = 1; i <= skip; i++)
                                        if (dtCopy.Rows.Count > 0)
                                            dtCopy.Rows.RemoveAt(0);
                                }
                                ds.Enqueue(dtCopy);
                                dt.Reset();
                                dt = CreateDataTable();
                                //dt = ConvertDataTableToBill.DataTableToBill(service);
                            }
                            DataRow row = dt.NewRow();
                            dt.Rows.Add(row);
                            NumRow++;
                        }
                        //OnData('-', NumRow, "");|
                        break;
                    }
                    if (r.Name == "c") { GetCellInfo(r); break; }
                    if (r.Name == "v") InValue = true;
                    break;
                case XmlNodeType.EndElement:
                    InValue = false;
                    //if (r.Name == "row") 
                    //OnData('#', NumRow, "");
                    break;
                case XmlNodeType.Text:

                    if (NumRow < min + 1)
                    {
                        continue;
                    }
                    if (!InValue) break;
                    if (dt.Columns.Contains(NumCol))
                    {
                        string s = r.Value;
                        s = (IsText ? source.words[Int32.Parse(s)] : s);
                        dt.Rows[((NumRow - min - 1) % numItem)][NumCol] = s;
                    }
                    //OnData(NumCol, NumRow, s);
                    break;
            }
        }
        if (NumRow - min < numItem || dt.Rows.Count > 1)
        {
            if (NumRow <= numItem) //first dt, remove skip line
            {
                for (int i = 1; i <= skip; i++)
                    if (dt.Rows.Count > 0)
                        dt.Rows.RemoveAt(0);
            }
            ds.Enqueue(dt);
        }
    }
    private DataTable CreateDataTable()
    {
        DataTable dt = new DataTable();
        for (char c = 'A'; c <= 'Z'; c++)
        {
            DataColumn col = new DataColumn(c.ToString());
            dt.Columns.Add(col);
        }
        return dt;
    }
}
//public class ImportData
//{
//    public static double DataHandle(string output_file,int numper,int limit, string service)
//    {
//        Queue<DataTable> queue = new Queue<DataTable>();
//        Task read = Task.Factory.StartNew(() => ReadExcel(output_file, service,numper,limit, ref queue));


//        SqlConnection cn = new SqlConnection(SmartPortal.DAL.DataAccess.ConnectionString);
//        SqlCommand cmd = new SqlCommand("SYS_BILLINFORMATION_ADD", cn);
//        cmd.CommandText = "SYS_BILLINFORMATION_ADD";
//        cmd.CommandType = CommandType.StoredProcedure;
//        DateTime t1 = DateTime.Now;
//        Console.WriteLine("Start Insertdata: " + t1.ToString("HH:mm:ss"));
//        while (queue.Count > 0 || !read.IsCompleted)
//        {
//            if ((bool)HttpContext.Current.Session["CancelImport"])
//            {
//                cn.Close();
//                DataAccess.ExecuteSQL("truncate table SYS_BILLINFORMATION_TEMP");
//                break;
//            }
//            if (queue.Count == 0)
//            {
//                Thread.Sleep(1000);
//                continue;
//            }
//            if (cmd.Parameters.Count > 0)
//                cmd.Parameters.Clear();
//            cmd.Parameters.Add(new SqlParameter("@Data", queue.Dequeue()));

//            if (cn != null && cn.State == ConnectionState.Closed)
//                cn.Open();
//            try
//            {
//                cmd.ExecuteNonQuery();
//            }
//            catch (Exception ex)
//            {
//                if (cn != null) cn.Close();
//                break;
//            }

//        }
//        if (cn != null && cn.State != ConnectionState.Closed)
//            cn.Close();

//        cmd = null;
//        cn = null;
//        return (DateTime.Now - t1).TotalSeconds;
//    }
//    private static Task<int> ReadExcel(string output_file, string service,int numper,int limit, ref Queue<DataTable> ds)
//    {

//        ExcelReader r = new ExcelReader(output_file);

//        r.Process(service,numper,limit, ref ds);
//        return Task.Factory.StartNew(() => { return r.NumRow; });
//    }
//}

