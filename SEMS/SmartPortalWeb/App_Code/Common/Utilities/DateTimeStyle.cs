using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DateTimeStyle
/// </summary>
namespace SmartPortal.Common.Utilities
{
    public enum DateTimeStyle
    {
        Date,
        DateMMM, //Example dd-MMM-yyyy: 05-APR-2013
        ShortDateTime,
        DateTime
    }
}