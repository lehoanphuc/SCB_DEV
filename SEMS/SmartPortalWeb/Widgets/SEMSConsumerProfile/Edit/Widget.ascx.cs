﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


public partial class Widgets_SEMSContractList_Edit_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Widget1._TITLE = Resources.labels.ConsumerProfile + '-' + Resources.labels.edit;
    }
}
