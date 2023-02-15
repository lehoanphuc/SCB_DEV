using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;

namespace MBService.MasterCard
{
    public partial class Result : System.Web.UI.Page
    {
        static ITransaction.AutoTrans autoTrans;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (autoTrans == null)
                {
                    autoTrans = (ITransaction.AutoTrans)Activator.GetObject(
                        typeof(ITransaction.AutoTrans),
                        "tcp://" + ConfigurationManager.AppSettings["RemotingAddress"].ToString() + ":" +
                        ConfigurationManager.AppSettings["RemotingPort"].ToString() + "/IPCAutoTrans");
                }

                string url = Request.RawUrl;
                string TransID = HttpUtility.ParseQueryString(url).Get(0);
                string UserID = HttpUtility.ParseQueryString(url).Get(1);
                string SessionID = HttpUtility.ParseQueryString(url).Get(2);
                string TranCode = HttpUtility.ParseQueryString(url).Get(3);
                string SourceID = HttpUtility.ParseQueryString(url).Get(4);

                Hashtable InputData = new Hashtable();
                Common.HashTableAddOrSet(InputData, "IPCTRANCODE", TranCode);
                Common.HashTableAddOrSet(InputData, "SOURCEID", SourceID);
                Common.HashTableAddOrSet(InputData, "SERVICEID", SourceID);
                Common.HashTableAddOrSet(InputData, "API", "RETRIEVE_ORDER");
                Common.HashTableAddOrSet(InputData, "INVOINO", TransID);
                Common.HashTableAddOrSet(InputData, "USERID", UserID);
                Common.HashTableAddOrSet(InputData, "SESSION", SessionID);
                Hashtable OutputData = autoTrans.ProcessTransHAS(InputData);

                if (OutputData["IPCERRORCODE"].ToString() == "0")
                {
                    if (OutputData["RESULT"].ToString() == "SUCCESS")
                    {
                        lbresult.Text = "Your Payment was successful!";
                        lbmerchant.Text = OutputData["MERCHANT"].ToString();
                        lbAmount.Text = OutputData["AMOUNT"].ToString();
                        lbccyid.Text = OutputData["CCYID"].ToString();
                        lbid.Text = TransID;

                        var res = new PaymentResponse {Event = "CLICK"};
                        var result = new JavaScriptSerializer().Serialize(res).ToString();
                        hdValue.Value = result.ToString();
                    }
                    else
                    {
                        lbresult.Text = "Your Payment was Unsuccessful!";
                    }
                }
                else
                {
                    Response.Redirect("~/500.aspx");
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public class PaymentResponse
        {
            public string Event { get; set; }
        }
    }
}