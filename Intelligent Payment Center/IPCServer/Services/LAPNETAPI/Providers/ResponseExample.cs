using Swashbuckle.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LAPNETAPI.Providers
{

    /// <summary>
    /// 
    /// </summary>
    ///
    public class UpstreamCCTModelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return Transactions.GetInfoFromJson("UpstreamCCT");
        }
    }
    public class UpstreamCCTResModelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return Transactions.GetInfoFromJson("UpstreamCCTrespon");
        }
    }


    /// <summary>
    /// API2
    /// UpstreamNotificationToReceive
    /// </summary>
    public class UpstreamNotificationToReceiveModelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return Transactions.GetInfoFromJson("UpstreamNotificationToReceive");
        }
    }
    public class UpstreamNotificationToReceiveResModelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return Transactions.GetInfoFromJson("UpstreamNotificationToReceiverespon");
        }
    }



    /// <summary>
    /// API3
    /// UpstreamCCTFailValidation
    /// </summary>
    public class UpstreamCCTFailValidationModelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return Transactions.GetInfoFromJson("UpstreamCCTFailValidation");
        }
    }
    public class UpstreamCCTFailValidationResModelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return Transactions.GetInfoFromJson("UpstreamCCTFailValidationrespon");
        }
    }
    /// <summary>
    /// API4
    /// UpstreamCCTSuccessValidation
    /// </summary>
    public class UpstreamCCTSuccessValidationModelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return Transactions.GetInfoFromJson("UpstreamCCTSuccessValidation");
        }
    }
    public class UpstreamCCTSuccessValidationResModelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return Transactions.GetInfoFromJson("UpstreamCCTSuccessValidationrespon");
        }
    }


}