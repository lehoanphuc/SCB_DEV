<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using System.Data;
using SmartPortal.IB;
using SmartPortal.Common.Utilities;
using System.Drawing;
using System.IO;
using System.Text;
using System.Drawing.Imaging;

public class Handler : IHttpHandler
{

    public void ProcessRequest(HttpContext ctx)
    {
        //string idimg = ctx.Request.QueryString["idimg"];
        //CreditCard objCrc = new CreditCard();
        //DataTable dtImg = objCrc.Cardimg_select();
        //DataRow[] row = dtImg.Select("ImgID='" + idimg + "'");
        //Image img;
        //if (row.Length > 0)
        //{
        //    img = Utility.BaseToImage(row[0]["Bin"].ToString());
        //}
        //else
        //{
        //    DataRow[] row1 = dtImg.Select("ImgID='00000'");
        //    img = Utility.BaseToImage(row1[0]["Bin"].ToString());
        //}
        //MemoryStream ms = new MemoryStream();
        //img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
        //byte[] baseImg = ms.ToArray();
        //img.Dispose();

        //ctx.Response.ContentType = "image/bmp";
        //ctx.Response.Cache.SetCacheability(HttpCacheability.Public);
        //ctx.Response.BufferOutput = false;

        //ctx.Response.BinaryWrite(baseImg);
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}