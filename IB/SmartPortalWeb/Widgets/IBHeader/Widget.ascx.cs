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

using System.Text;
using SmartPortal.Common.Utilities;
using System.Web.Services;


public partial class Widgets_IBHeader_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["accType"].ToString() != "IND")
            {
                StringBuilder sB1 = new StringBuilder();
                sB1.Append("<link rel='stylesheet' type='text/css' href='widgets/IBHeader/cssCo.css' />");
                Literalcss.Text = sB1.ToString();
            }
            else
            {
                StringBuilder sB1 = new StringBuilder();
                sB1.Append("<link rel='stylesheet' type='text/css' href='widgets/IBHeader/css.css' />");
                Literalcss.Text = sB1.ToString();
            }
            if (SmartPortal.Common.Utilities.Utility.IsGuest())
            {
                litInfo.Text = "";
            }
            else
            {
                if (Session["userName"] != null)
                {
                    StringBuilder st = new StringBuilder();

                    st.Append("<div class='panel_login_info'>");
                    st.Append("<li style='font-weight:bold;'>" + Resources.labels.wellcomlogin + " " + Session["fullName"].ToString() + "</li>");
                    st.Append("<li>" + Resources.labels.landangnhaptruoc + ": " + Utility.FormatDatetime(Session["lastLoginTime"].ToString(), "dd/MM/yyyy HH:mm:ss", DateTimeStyle.ShortDateTime) + "</li>");
                    st.Append("<li>" + Resources.labels.ngaylamviec + ": " + Utility.FormatDatetime(SmartPortal.Constant.IPC.LoadWorkingDate(), "dd/MM/yyyy", DateTimeStyle.DateMMM) + "</li>");
                    st.Append("</div>");

                    litInfo.Text = st.ToString();

                    //lấy Contracttype theo user name
                    string IPCERRORCODE = string.Empty;
                    string IPCERRORDESC = string.Empty;
                    DataSet ds = new SmartPortal.SEMS.Contract().GetAccount(Session["userName"].ToString().Trim(), ref IPCERRORCODE, ref IPCERRORDESC);



                    string contractype = string.Empty;
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];
                    contractype = dt.Rows[0]["ContractType"].ToString();

                    //if (contractype == "IND")
                    //{
                    //    imglogo.ImageUrl = "Images/img_individual.png";
                    //    //System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "changeImage()", true);
                    //}
                    //else
                    //{
                    //    imglogo.ImageUrl = "Images/img_corporate.png";
                    //}
                }
            }
            //Init_Notify();
        }
        catch (Exception ex)
        {
        }
    }

    void Init_Notify()
    {
        //try
        //{
        //    if (Session["NotifyStatus"]==null)
        //    {
        //        string content = GetData();
        //        {
        //            if(!string.IsNullOrEmpty(content))
        //            {
        //                lblNotify.Text = content;
        //                Session["NotifyStatus"] = "1";
        //                Session["LastTimeSetNotify"] = DateTime.Now;
        //            }
        //        }
        //    }
        //    else if(Session["NotifyStatus"].ToString().Equals("1"))
        //    {
        //        lblNotify.Text = Session["strNotification"].ToString();
        //        if(Session["LastTimeSetNotify"] !=null)
        //        {
        //            DateTime lastDate = (DateTime)Session["LastTimeSetNotify"];
        //            double milisecond = (DateTime.Now - lastDate).TotalMilliseconds;
        //            lblNotify.Style.Add("margin-left", "-" + (milisecond / 12.5 - 50).ToString() + "px");
        //            Session["LastTimeSetNotify"] = DateTime.Now.AddMilliseconds(-milisecond);
        //        }
        //    }
        //    else if (Session["NotifyStatus"].ToString().Equals("3"))
        //    {
        //        divmarquee.Visible = false;
        //        Session["strNotification"] = "";
        //    }

        //    if(string.IsNullOrEmpty(Session["strNotification"].ToString()))
        //    {
        //        divmarquee.Visible = false;
        //    }
        //}
        //catch { }
    }
    private void ShowPopUpMsg(string msg)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("alert('");
        sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"));
        sb.Append("');");
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showalert", sb.ToString(), true);
    }
    //string GetData()
    //{
    //    try
    //    {
    //        StringBuilder st = new StringBuilder();
    //        DataTable dtno = new DataTable();

    //        if (Session["strNotification"] == null)
    //        {
    //            dtno = new SmartPortal.IB.Transactions().GetNotify("ADV");
    //            if (dtno.Rows.Count > 0)
    //            {
    //                divmarquee.Visible = true;
    //                foreach (DataRow dr in dtno.Rows)
    //                {
    //                    for (int i = 1; i <= int.Parse(dr["VARVALUE3"].ToString()); i++)
    //                    {
    //                        st.Append("<a href=\"");
    //                        st.Append(dr["VARVALUE2"].ToString());
    //                        st.Append("\" target=\"_blank\" style=\"color: yellow\">");

    //                        //st.Append("</pre>");
    //                        st.Append(dr["VARVALUE1"].ToString());
    //                        //st.Append(new String(' ', 100));
    //                        st.Append("</a> ");
    //                        st.Append(Spaces(50, "&nbsp;"));

    //                    }
    //                }
    //            }
    //            Session["strNotification"] = st.ToString();
    //        }
    //        return Session["strNotification"].ToString();
    //    }
    //    catch
    //    {
    //        return "";
    //    }
    //}
    string Spaces(int n,string v)
    {
        string t = "";
        for(int i=0;i<n;i++)
        {
            t+=v;
        }
        return t;
    }
}
