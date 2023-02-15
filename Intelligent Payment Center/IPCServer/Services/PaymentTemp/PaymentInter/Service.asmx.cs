using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Configuration;

namespace PaymentInter
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Service : System.Web.Services.WebService
    {
        private static capnuoccholon.Service svr = null;

        public Service()
        {
            try
            {
                if (svr == null)
                {
                    svr = new PaymentInter.capnuoccholon.Service();
                    svr.Url = ConfigurationManager.AppSettings["CLURL"].ToString();
                }
            }
            catch (Exception ex)
            {
                svr.Url = "http://remote.capnuoccholon.com.vn/Webservicethungan/Service.asmx";
            }
        }

        [WebMethod]
        public DataSet getW_Bill(string danhba, string ten, string matkhau)
        {
            return svr.getW_Bill(danhba, ten, matkhau); 
        }

        [WebMethod]
        public bool payW_Bill(string id, string ten, string matkhau)
        {
            return svr.payW_Bill(id, ten, matkhau);
        }
        [WebMethod]
        public DataSet getCustomerInfo(string danhba, string ten, string matkhau)
        {
            return svr.getCustomerInfo(danhba, ten, matkhau);
        }
    }
}
