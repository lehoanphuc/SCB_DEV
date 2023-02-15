using InterbankTransferService.Models;
using InterbankTransferService.Providers;
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
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace InterbankTransferService.Controllers
{
    [DisplayName("Webservice")]
    public class WSController : ApiController
    {
        /// <summary>
        /// Get Session
        /// </summary>
        [HttpPost]
        [Route("api/nvp/version/40")]
        public Task<HttpResponseMessage> GetSession()
        {
            return Common.ForwardRequest(Request, ConfigurationManager.AppSettings.Get("MPGS_URL").ToString());
        }

    }
}