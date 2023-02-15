using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using SmartPortal.ExceptionCollection;
using System.Drawing.Printing;
using SmartPortal.IB;

public partial class Widgets_IBBuyTopup_viewprint : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();

            tmpl = SmartPortal.Common.ST.GetStringTemplate("IBWesternUnion", "WesternUnionSucsessfull");
            Hashtable hasPrint = new Hashtable();
            hasPrint = (Hashtable)Session["print"];
            //ghi vo session dung in
            tmpl.SetAttribute("tranID", hasPrint["IPCTRANSID"].ToString());
            tmpl.SetAttribute("tranDate", hasPrint["TRANDATE"].ToString());
            tmpl.SetAttribute("senderName", hasPrint["SENDERNAME"].ToString());
            tmpl.SetAttribute("senderAccount", hasPrint["SENDERACCNO"].ToString());

            tmpl.SetAttribute("PAYCOUNTRY", hasPrint["PAYCOUNTRY"].ToString());

            tmpl.SetAttribute("SENDERUNIT", hasPrint["SENDERUNIT"].ToString());
            tmpl.SetAttribute("SENDERDISTRICT", hasPrint["SENDERDISTRICT"].ToString());
            tmpl.SetAttribute("SENDERCITY", hasPrint["SENDERCITY"].ToString());
            tmpl.SetAttribute("SENDERPROVINCE", hasPrint["SENDERPROVINCE"].ToString());
            tmpl.SetAttribute("SENDERCOUNTRY", hasPrint["SENDERCOUNTRY"].ToString());
            tmpl.SetAttribute("POSTCODE", hasPrint["POSTCODE"].ToString());
            tmpl.SetAttribute("COUNTRYCODE", hasPrint["COUNTRYCODE"].ToString());
            tmpl.SetAttribute("TELEPHONE", hasPrint["TELEPHONE"].ToString());
            tmpl.SetAttribute("MOBILEPHONECODE", hasPrint["MOBILEPHONECODE"].ToString());
            tmpl.SetAttribute("PHONENO", hasPrint["PHONENO"].ToString());
            tmpl.SetAttribute("EMAIL", hasPrint["EMAIL"].ToString());

            tmpl.SetAttribute("ICTYPE", hasPrint["ICTYPE"].ToString());
            tmpl.SetAttribute("ICNUMBER", hasPrint["ICNUMBER"].ToString());
            tmpl.SetAttribute("ISSUECOUNTRY", "LA");
            tmpl.SetAttribute("EXDATE", hasPrint["EXDATE"].ToString());
            tmpl.SetAttribute("ISSUEDATE", hasPrint["ISSUEDATE"].ToString());
            tmpl.SetAttribute("DATEOFBIRTH", hasPrint["DATEOFBIRTH"].ToString());
            tmpl.SetAttribute("OFFICEPHONE", hasPrint["OFFICEPHONE"].ToString());
            tmpl.SetAttribute("OCCUPATION", hasPrint["OCCUPATION"].ToString());
            tmpl.SetAttribute("PURPOSE", hasPrint["PURPOSE"].ToString());
            tmpl.SetAttribute("SOURCEOFUND", hasPrint["SOURCEOFUND"].ToString());
            tmpl.SetAttribute("ICUNIT", hasPrint["ICUNIT"].ToString());
            tmpl.SetAttribute("ICDISTRICT", hasPrint["ICDISTRICT"].ToString());
            tmpl.SetAttribute("ICPROVINCE", hasPrint["ICPROVINCE"].ToString());
            tmpl.SetAttribute("ICCOUNTRY", hasPrint["ICCOUNTRY"].ToString());
            tmpl.SetAttribute("BIRTHCOUTRY", hasPrint["BIRTHCOUTRY"].ToString());
            tmpl.SetAttribute("NATIONLITY", hasPrint["NATIONLITY"].ToString());
            tmpl.SetAttribute("GENDER", hasPrint["GENDER"].ToString());
            tmpl.SetAttribute("COMMENT", hasPrint["COMMENT"].ToString());
            tmpl.SetAttribute("FILE", hasPrint["FILE"].ToString());

            tmpl.SetAttribute("receiverFirstName", hasPrint["FIRSTNAME"].ToString());
            tmpl.SetAttribute("receiverLastName", hasPrint["LASTNAME"].ToString());
            tmpl.SetAttribute("amount", hasPrint["AMOUNT"].ToString());
            tmpl.SetAttribute("feeType", "Sender");
            tmpl.SetAttribute("feeAmount", hasPrint["feeSenderAmt"].ToString());
            tmpl.SetAttribute("transtotal", hasPrint["TOTAL"].ToString());
            tmpl.SetAttribute("ccyid", "USD");
            tmpl.SetAttribute("reccyid", hasPrint["RCCYID"].ToString());

            
            tmpl.SetAttribute("status", hasPrint["STATUS"].ToString());

            ltrPrint.Text = tmpl.ToString();

        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.Message, this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
}
