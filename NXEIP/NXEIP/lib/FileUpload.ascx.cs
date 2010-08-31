using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using NXEIP.DAO;
using Entity;

public partial class lib_FileUpload : System.Web.UI.UserControl
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if (this.div_pic.InnerHtml.Equals(""))
            {
                this.div_pic.InnerHtml = "無圖示";
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (this.FileUpload1.HasFile)
        {

            string exi = this.FileUpload1.FileName.Split('.')[1].ToLower();

            if (exi.Equals("jpeg") || exi.Equals("jpg") || exi.Equals("png") || exi.Equals("bmp") || exi.Equals("gif"))
            {
                string filename = Guid.NewGuid().ToString("N") + "." + exi;

                string fileSavePath = Server.MapPath("~") + "\\PicTemp\\";

                this.FileUpload1.SaveAs(fileSavePath + filename);

                this.lab_filename.Text = this.FileUpload1.FileName;
                this.lab_path.Text = fileSavePath + filename;

                string src = ResolveClientUrl("~/PicTemp/" + filename + "?ran=" + new Random().Next(1000));

                this.div_pic.InnerHtml = "<img src=" + src + " />";

            }
            else
            {
                this.div_pic.InnerHtml = "無圖示";
            }
            
        }
        
    } 

    public void Show_Pic(int peo_uid)
    {

        Entity.people peopleData = new PeopleDAO().GetByPeoUID(peo_uid);


        if (String.IsNullOrEmpty(peopleData.peo_filename))
        {
            this.div_pic.InnerHtml = "無圖示";
        }
        else
        {
            string exi = peopleData.peo_filename.Split('.')[1].ToLower();
            string filename = Guid.NewGuid().ToString("N") + "." + exi;
            string savePath = Server.MapPath("~") + "\\PicTemp\\" + filename;

            //產生圖片
            using (FileStream fs = new FileStream(savePath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                fs.Write(peopleData.peo_picture, 0, peopleData.peo_picture.Length);
            }

            string src = ResolveClientUrl("~/PicTemp/" + filename + "?ran=" + new Random().Next(1000));
            this.div_pic.InnerHtml = "<img src=" + src + " />";
        }
    }

    public string Get_FileName
    {
        get
        {
            try
            {
                return this.lab_filename.Text;
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
                byte[] fileByte = null;
                using (FileStream fs = new FileStream(this.lab_path.Text, FileMode.OpenOrCreate, FileAccess.Read))
                {
                    fileByte = new byte[fs.Length];
                    fs.Read(fileByte, 0, fileByte.Length);
                }

                return fileByte;
            }
            catch
            {
                return null;
            }
        }
    }
}
