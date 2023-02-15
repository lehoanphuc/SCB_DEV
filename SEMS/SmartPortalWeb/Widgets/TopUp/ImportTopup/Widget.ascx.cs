using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Web;
using System.Web.Management;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.BLL;
using SmartPortal.ExceptionCollection;
using SmartPortal.Common.Utilities;
using SmartPortal.Security;
using SmartPortal.SEMS;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;

public partial class Widgets_ImportTopUp_Widget : WidgetBase
{

    private string errorcode = "";
    private string errordesc = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //lay danh sach telco
                DataSet ds = new SmartPortal.SEMS.Topup().GetTecoList(ref errorcode, ref errordesc);
                btDownload.Visible = false;

                ddlTelco.DataTextField = "TelcoName";
                ddlTelco.DataValueField = "TelcoID";
                ddlTelco.DataSource = ds;
                ddlTelco.DataBind();

                //lay danh sach menh gia the theo telco
                LoadCardAmount(ddlTelco.SelectedValue.ToString());
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void ddlTelco_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCardAmount(ddlTelco.SelectedValue.ToString());
    }

    protected void LoadCardAmount(string selected)
    {
        DataSet dsamount = new SmartPortal.SEMS.Topup().GetAmountbyTelco(selected, "1", ref errorcode, ref errordesc);
        dsamount.Tables[0].Columns.Add("CardandCCYID", typeof(string), "CardAmount +' '+ CCYID");
        ddlAmount.DataTextField = "CardandCCYID";
        ddlAmount.DataValueField = "CardID";
        ddlAmount.DataSource = dsamount;
        ddlAmount.DataBind();
    }

    protected void btsave_Click(object sender, EventArgs e)
    {
        string unzipPath = "";
        string filepath = "";
        string buyrate = "";
        string sellrate = "";
        double j;
        
        if (FileUpload1.HasFile && FileUpload1.PostedFile.FileName.Contains(".zip"))
        {
            if (txtBuyRate.Text != "" && txtSellRate.Text != "" && double.TryParse(txtSellRate.Text, out j) && double.TryParse(txtBuyRate.Text, out j))
            {
                buyrate = txtBuyRate.Text;
                sellrate = txtSellRate.Text;

                filepath = SaveFile(FileUpload1.PostedFile);
                if (filepath != "")
                {
                    unzipPath = UnZipFiles(filepath, Server.MapPath("/Widgets/Topup/ImportTopup/Unzip/"), ipPassword.Text, true);
                    if (unzipPath != "")
                    {
                        Session["rptpath"] = new SmartPortal.SEMS.Topup().ImportTopup(Session["username"].ToString(), unzipPath, ddlTelco.SelectedValue.Trim(), ddlAmount.SelectedValue.Trim(), buyrate.Trim(),sellrate.Trim(), ref errorcode, ref errordesc);
                        switch (errorcode)
                        {
                            case "0":
                                lblAlert.Text = Resources.labels.topupimportsuccess;
                                btDownload.Visible = true;
                                break;
                            case "4999":
                                lblAlert.Text = Resources.labels.topupbitrungdulieu;
                                break;
                            case "4997":
                                lblAlert.Text = Resources.labels.topupcsvdaidinhdang;
                                break;
                            default:
                                lblAlert.Text = errordesc;
                                break;
                        }
                    }
                    else
                    {
                        lblAlert.Text = Resources.labels.topupgiainenthatbai;
                    }
                }
            }
            else
            {
                lblAlert.Text = "Please input correct buy rate value.";
            }
        }
        else
        {
            lblAlert.Text = "Please select correct file to import";
        }
    }

    public void btDownload_Click(object sender,EventArgs e)
    {
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(Session["rptpath"].ToString()));
        Response.WriteFile(Session["rptpath"].ToString());
        Response.End();
    }

    protected void btback_Click(object sender, EventArgs e)
    {
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=424"));
    }
    string SaveFile(HttpPostedFile file)
    {
        try
        {
            string savePath = Server.MapPath("/Widgets/Topup/ImportTopup/Uploads/");
            string fileName = FileUpload1.FileName;
            string pathToCheck = savePath + fileName;
            string tempfileName = "";
            if (System.IO.File.Exists(pathToCheck))
            {
                int counter = 2;
                while (System.IO.File.Exists(pathToCheck))
                {
                    tempfileName = counter.ToString() + fileName;
                    pathToCheck = savePath + tempfileName;
                    counter++;
                }
                fileName = tempfileName;
            }
            savePath += fileName;
            FileUpload1.SaveAs(savePath);
            return savePath;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public string UnZipFiles(string zipPathAndFile, string outputFolder, string password, bool deleteZipFile)
    {
        try
        {
            string fullPath = "";
            ZipInputStream s = new ZipInputStream(File.OpenRead(zipPathAndFile));
            if (password != null && password != String.Empty)
                s.Password = password;
            ZipEntry theEntry;
            string tmpEntry = String.Empty;
            while ((theEntry = s.GetNextEntry()) != null)
            {
                string directoryName = outputFolder;
                string fileName = Path.GetFileName(theEntry.Name);
                // create directory 
                if (directoryName != "")
                {
                    Directory.CreateDirectory(directoryName);
                }
                if (fileName != String.Empty)
                {
                    if (theEntry.Name.IndexOf(".ini") < 0)
                    {
                        fullPath = directoryName + @"\" + theEntry.Name;
                        fullPath = fullPath.Replace(@"\ ", @"\");
                        fullPath = fullPath.Replace(@"\\", @"\");
                        string fullDirPath = Path.GetDirectoryName(fullPath);
                        if (!Directory.Exists(fullDirPath)) Directory.CreateDirectory(fullDirPath);
                        FileStream streamWriter = File.Create(fullPath);
                        int size = 2048;
                        byte[] data = new byte[2048];
                        while (true)
                        {
                            size = s.Read(data, 0, data.Length);
                            if (size > 0)
                            {
                                streamWriter.Write(data, 0, size);
                            }
                            else
                            {
                                break;
                            }
                        }
                        streamWriter.Close();
                    }
                }
            }
            s.Close();
            if (deleteZipFile)
                File.Delete(zipPathAndFile);
            return fullPath;
        }
        catch (Exception)
        {
            return "";
        }
    }
}
