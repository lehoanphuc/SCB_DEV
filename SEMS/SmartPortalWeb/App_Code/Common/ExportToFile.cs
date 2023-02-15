using System;
using System.Data;
using System.Configuration;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using System.IO;

using System.Text;

using System.Diagnostics;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Collections;

namespace SmartPortal.Common
{
    /// <summary>
    /// Summary description for ExportToFile
    /// </summary>
    public class ExportToFile
    {

        public static void ExportStringToExcel(string fileName,string str)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.AddHeader("content-disposition",
             "attachment;filename=" + fileName + ".xls");
            HttpContext.Current.Response.Charset = "";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";


            //style to format numbers to string
            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            HttpContext.Current.Response.Write(style);
            HttpContext.Current.Response.Output.Write(str);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
       
        /// <summary>
        /// Xuất ra file Excel từ Datatable
        /// </summary>
        public static void ExportToExcel(DataTable dt,string fileName)
        {

            //Create a dummy GridView
            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.DataSource = dt;
            GridView1.DataBind();

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.AddHeader("content-disposition",
             "attachment;filename="+fileName+".xls");
            HttpContext.Current.Response.Charset = "";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                //Apply text style to each Row
                GridView1.Rows[i].Attributes.Add("class", "textmode");
            }
            GridView1.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            HttpContext.Current.Response.Write(style);
            HttpContext.Current.Response.Output.Write(sw.ToString());
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// Xuất ra file PDF từ Datatable
        /// </summary>
        public static void ExportToPDF(DataTable dt,string fileName)
        {

            //Create a dummy GridView
            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.DataSource = dt;
            GridView1.DataBind();

            HttpContext.Current.Response.ContentType = "application/pdf";
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename="+fileName+".pdf");
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView1.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);

            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
            pdfDoc.Open();

            BaseFont bf = BaseFont.CreateFont(@"C:\WINDOWS\Fonts\Tahoma.ttf", BaseFont.IDENTITY_H, true);
            iTextSharp.text.Font font = new iTextSharp.text.Font(bf);

            StyleSheet st = new StyleSheet();
            st.LoadTagStyle("body", "face", "Tahoma unicode ms");
            st.LoadTagStyle("body", "encoding", "Identity-H");
            st.LoadTagStyle("body", "leading", "12,0");

            //ArrayList p = HTMLWorker.ParseToList(sr,st);
            //for (int k = 0; k < p.Count; ++k)
            //    pdfDoc.Add((IElement)p[k]);
            var p = iTextSharp.text.html.simpleparser.HTMLWorker.ParseToList(sr, st);
            for (int k = 0; k < p.Count; ++k)
                pdfDoc.Add((IElement)p[k]);

            //htmlparser.Parse(sr);            

            pdfDoc.Close();
            HttpContext.Current.Response.ContentEncoding = Encoding.Unicode;
            HttpContext.Current.Response.Write(pdfDoc);
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// Xuất ra file CSV từ Datatable
        /// </summary>
        public static void ExportToCSV(DataTable dt,string fileName)
        {

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename="+fileName+".csv");
            HttpContext.Current.Response.Charset = "";
            HttpContext.Current.Response.ContentType = "application/text";


            StringBuilder sb = new StringBuilder();
            for (int k = 0; k < dt.Columns.Count; k++)
            {
                //add separator
                sb.Append(dt.Columns[k].ColumnName + ',');
            }
            //append new line
            sb.Append("\r\n");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int k = 0; k < dt.Columns.Count; k++)
                {
                    //add separator
                    sb.Append(dt.Rows[i][k].ToString().Replace(",", ";") + ',');
                }
                //append new line
                sb.Append("\r\n");
            }
            HttpContext.Current.Response.Output.Write(sb.ToString());
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// Xuất ra file word từ Datatable
        /// </summary>
        public static void ExportToWord(DataTable dt,string fileName)
        {

            //Create a dummy GridView
            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.DataSource = dt;
            GridView1.DataBind();

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename="+fileName+".doc");
            HttpContext.Current.Response.Charset = "";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-word ";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView1.RenderControl(hw);
            HttpContext.Current.Response.Output.Write(sw.ToString());
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// export gridview to excel
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="fileName"></param>
        public static void ExportGridViewToExcel(GridView gv, string fileName,DataSet tableExport)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;

            HttpContext.Current.Response.AddHeader("content-disposition",
            "attachment;filename="+fileName+".xls");
            HttpContext.Current.Response.Charset = "";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            //gv.DataSource = tableExport;
            

            gv.AllowPaging = false;
            //gv.DataBind();

            
            gv.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            HttpContext.Current.Response.Write(style);
            HttpContext.Current.Response.Output.Write(sw.ToString());
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
    }
}