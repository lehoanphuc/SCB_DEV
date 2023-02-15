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

using SmartPortal.Common.Utilities;
using SmartPortal.Security;
using System.Text;
using SmartPortal.ExceptionCollection;
using SmartPortal.SEMS;
using System.Collections.Generic;
using System.Linq;

public partial class Widgets_SEMSContractList_ADD_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Widget1._TITLE = Resources.labels.ConsumerProfile + '-' +  Resources.labels.add ;
    }
}
