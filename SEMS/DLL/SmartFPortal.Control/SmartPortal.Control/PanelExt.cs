using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SmartPortal.Control
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:PanelExt runat=server></{0}:PanelExt>")]
    public class PanelExt : PlaceHolder
    {
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]

        private string _widgetZoneID;
        public string WidgetZoneID
        {
            get
            {
                return _widgetZoneID;
            }

            set
            {
                _widgetZoneID = value;
            }
        }

        protected override void Render(HtmlTextWriter output)
        {
            output.Write("<div class=\"portal-column \" id=\" " + WidgetZoneID + "\">");
            base.Render(output);
            output.Write("</div>");
        }
    }
}
