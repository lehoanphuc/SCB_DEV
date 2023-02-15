using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public class CustomRadio : UserControl
{
    public delegate void SubmitClickedHandler();
    [Category("Action")]
    [Description("Fires when the Submit button is clicked.")]
    public event SubmitClickedHandler SubmitClicked;
    public string id = string.Empty;
    public CustomRadio(string urlImage)
    {
        RadioButton radio = new RadioButton();
        radio.Text = "test";
        Image image = new Image();
        image.ImageUrl = urlImage;
        Controls.Add(radio);
        Controls.Add(image);

    }


}