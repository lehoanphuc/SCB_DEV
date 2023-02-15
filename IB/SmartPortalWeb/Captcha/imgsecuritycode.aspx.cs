using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing.Text;
using System.Drawing;
using System.Drawing.Imaging;

public partial class imgsecuritycode : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        Bitmap objBMP = new Bitmap(55, 20);
        Graphics objGraphics = Graphics.FromImage(objBMP);
        objGraphics.Clear(Color.AliceBlue);
        objGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;

        //' Configure font to use for text
        Font objFont = new Font("Arial", 11, FontStyle.Bold);
        string randomStr = "";
        int[] myIntArray = new int[5];
        int x;

        //That is to create the random # and add it to our string
        Random autoRand = new Random();
        for (x = 0; x < 5; x++)
        {
            myIntArray[x] = System.Convert.ToInt32(autoRand.Next(0, 9));
            randomStr += (myIntArray[x].ToString());
        }

        //This is to add the string to session cookie, to be compared later
        Session.Add("randomStr", randomStr);

        //This session is use for the security image in the submit recipe page.
        Session.Add("randomstrsub", randomStr);

        //This session is use for the security image in the user registration page.
        Session.Add("randomstruserreg", randomStr);

        //This session is use for Internet Baking Login
        Session.Add("randomIBLogin", randomStr);

        //' Write out the text
        //objGraphics.DrawString(randomStr, objFont, Brushes.Maroon, 3, 3);

        //' Set the content type and return the image
        Response.ContentType = "image/GIF";
        objBMP = CreateImage(randomStr);
        objBMP.Save(Response.OutputStream, ImageFormat.Gif);

        //Release object from memory
        objFont.Dispose();
        objGraphics.Dispose();
        objBMP.Dispose();

    }

    protected Bitmap CreateImage(string strValidCode)
    {
        Bitmap map = new Bitmap(55, 20);
        try
        {
            int randangle = 90; // random rotation angle
            //Create drawing object graphics
            Graphics graph = Graphics.FromImage(map);
            graph.Clear(Color.AliceBlue); // clear the drawing screen and fill in the background color
            graph.DrawRectangle(new Pen(Color.Black, 0), 0, 0, map.Width - 1, map.Height - 1); // draw a border
            graph.DrawRectangle(new Pen(Color.Black, 2), 0, map.Height/2, map.Width - 1, 0); // draw a border
            graph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed; // mode
            Random rand = new Random();
            //Background noise generation
            Pen blackPen = new Pen(Color.LightGray, 0);
            for (int i = 0; i < 50; i++)
            {
                int x = rand.Next(0, map.Width);
                int y = rand.Next(0, map.Height);
                graph.DrawRectangle(blackPen, x, y, 1, 1);
            }
            //Captcha rotation to prevent machine recognition
            char[] chars = strValidCode.ToCharArray(); // splits the string into a single character array
                                                       //Center text
            StringFormat format = new StringFormat(StringFormatFlags.NoClip);
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;
            //Define colors
            Color[] c = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };
            //Define font
            String[] font = { "Verdana", "Microsoft sans serif", "Comic Sans MS", "Arial", "Song Ti" };
            for (int i = 0; i < chars.Length; i++)
            {
                int cindex = rand.Next(7);
                int findex = rand.Next(5);
                Font f = new Font("Arial", 11, FontStyle.Bold);
                Brush b = new System.Drawing.SolidBrush(c[cindex]);
                Point dot = new Point(8, 10);

                float angle = rand.Next(-randangle, randangle); // degree of rotation
                graph.TranslateTransform(dot.X, dot.Y); // move the cursor to the specified position
                graph.RotateTransform(angle);
                graph.DrawString(chars[i].ToString(), f, b, 1, 1, format);

                graph.RotateTransform(-angle); // turn back
                graph.TranslateTransform(2, -dot.Y); // move the cursor to the specified position
            }

        }
        catch (ArgumentException)
        {
          //  MessageBox.Show("captcha image creation error");
        }
        return map;
    }
}
