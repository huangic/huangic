using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class lib_FileUpload : System.Web.UI.UserControl
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.div_pic.InnerHtml = "無圖示";
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (this.FileUpload1.HasFile)
        {

            string exi = this.FileUpload1.FileName.Split('.')[1].ToLower();

            if (exi.Equals("jpeg") || exi.Equals("jpg") || exi.Equals("png") || exi.Equals("bmp") || exi.Equals("gif"))
            {
                string filename = Guid.NewGuid().ToString() + "." + exi;

                string fileSavePath = Server.MapPath("~") + "\\PicTemp\\";

                this.FileUpload1.SaveAs(fileSavePath + filename);

                string src = ResolveClientUrl("~/PicTemp/" + filename + "?ran=" + new Random().Next(1000));

                this.div_pic.InnerHtml = "<img src=" + src + " />";

            }
            else
            {
                this.div_pic.InnerHtml = "無圖示";
            }
            
        }
        
    }

    public string Get_FileName
    {
        get
        {
            try
            {
                return this.FileUpload1.FileName;
            }
            catch
            {
                return "";
            }
        }
    }

    public byte[] Get_FileByte
    {
        get
        {
            try
            {
                return this.FileUpload1.FileBytes;
            }
            catch
            {
                return null;
            }
        }
    }
}
