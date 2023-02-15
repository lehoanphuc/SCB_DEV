using DownloadImageAPI.Models;
using DownloadImageAPI.Providers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Swashbuckle.Examples;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace DownloadImageAPI.Controllers
{
    [DisplayName("Webservice")]
    public class WSController : ApiController
    {
      
        /// <summary>
        /// Get Session
        /// </summary>
        [HttpGet]
        [Route("{*imagename}")]
        public HttpResponseMessage GetFileDocument(string imagename)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            DateTime dateTime = DateTime.UtcNow.Date;
            string documentname = Transactions.DecryptData(imagename);
            if(documentname.Equals(string.Empty))
            {   
                result.StatusCode = HttpStatusCode.NotFound;
                result.Content = new StringContent("Can not find this file");
                return result;
            }
            string[] document = documentname.Split(new string[] { "#" }, StringSplitOptions.None);
            if (Transactions.CheckPermission(document[0], document[1]))
            {
                string[] extension = document[2].Split(new string[] { "." }, StringSplitOptions.None);
                string folder = Transactions.GetParameter("PATHDOCUMENT").Tables[0].Rows[0]["VARVALUE"].ToString();
                string File = folder + document[2];            
                string filetype = string.Empty;
                switch (extension[1])
                {
                    case "png":
                        filetype = "image/png";
                        break;
                    case "jpg":
                        filetype = "image/jpg";
                        break;
                    case "jpeg":
                        filetype = "image/jpeg";
                        break;
                    case "pdf":
                        filetype = "application/pdf";
                        break;
                    default:
                        filetype = "image/png";
                        break;
                }
                
                byte[] imageArray = System.IO.File.ReadAllBytes(File);
                if (filetype.Equals(string.Empty) || imageArray.Length < 0)
                {
                    result.StatusCode = HttpStatusCode.NotFound;
                    result.Content = new StringContent("Can not find this file");
                    return result;
                }
                MemoryStream ms = new MemoryStream(imageArray);
                result.Content = new StreamContent(ms);
                result.Content.Headers.ContentType = new
                System.Net.Http.Headers.MediaTypeHeaderValue(filetype);
                return result;
            }
            else
            {
                result.StatusCode = HttpStatusCode.Forbidden;
                result.Content = new StringContent("You don't have permission!");
                return result;
            }         
        }

    }
}