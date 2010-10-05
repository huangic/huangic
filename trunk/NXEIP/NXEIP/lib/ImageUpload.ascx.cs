using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using NXEIP.DAO;
using Entity;
using System.ComponentModel;

public partial class lib_ImageUpload : System.Web.UI.UserControl
{
    #region 設定或取得圖片表頭
    [Category("Behavior")]
    [Themeable(true)]
    public String PicTitle { get; set; }
    #endregion

    #region 設定或取得圖片類型
    [Category("Behavior")]
    [Themeable(true)]
    public String PicType
    {
        get {
            return this.lab_pictype.Text;
        }
        set { this.lab_pictype.Text = value; }
    }
    #endregion

    #region 設定或取得圖片大小
    [Category("Behavior")]
    [Themeable(true)]
    public int PicSize
    {
        get
        {
            if (this.lab_size.Text.Length > 0)
                return Convert.ToInt32(this.lab_size.Text);
            else
                return 0;
        }
        set
        {
            this.lab_size.Text = value.ToString();
        }
    }
    #endregion

    #region 設定或取得圖片-寬
    [Category("Behavior")]
    [Themeable(true)]
    public int PicWidth { get; set; }
    #endregion

    #region 設定或取得圖片-高
    [Category("Behavior")]
    [Themeable(true)]
    public int PicHeight { get; set; }
    #endregion

    #region 設定或取得是否縮圖
    [Category("Behavior")]
    [Themeable(true)]
    public Boolean Thumbnail { get; set; }
    #endregion

    #region 設定或取得縮圖方式
    public enum ThumbnailModetype { 
        CUT=0,
        Height=1,
        HeightWidth=2,
        Width=3
    };
    [Category("Behavior")]
    [Themeable(true)]
    public ThumbnailModetype ThumbnailMode { get; set; }
    #endregion

    #region 取得檔名
    public string GetFileName
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
    #endregion

    #region 取得副檔名
    public string GetExtension
    {
        get
        {
            try
            {
                return this.lab_ext.Text;
            }
            catch
            {
                return "";
            }
        }
    }
    #endregion

    #region 取得二進位檔案
    public byte[] GetFileBytes
    {
        get
        {
            try
            {
                byte[] fileByte = null;
                using (FileStream fs = new FileStream(this.lab_path.Text, FileMode.Open, FileAccess.Read))
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
    #endregion

    #region 是否有檔案
    public bool HasFile
    {
        get
        {
            if (this.lab_ext.Text.Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    #endregion

    #region 是否超過檔案大小
    public bool CheckFileSize { 
        get
        {
            if (this.lab_checksize.Text.Equals("true"))
                return true;
            else
                return false;
        }
        set
        {
            if (value)
                this.lab_checksize.Text = "true";
            else
                this.lab_checksize.Text = "false";
        }
    }
    #endregion

    #region 檔案類型是否正確
    public bool CheckFileType {
        get
        {
            if (this.lab_checkftype.Text.Equals("true"))
                return true;
            else
                return false;
        }
        set
        {
            if (value)
                this.lab_checkftype.Text = "true";
            else
                this.lab_checkftype.Text = "false";
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if (this.div_pic.InnerHtml.Equals(""))
            {
                this.div_pic.InnerHtml = "無圖示";
            }
            this.lab_width.Text = PicWidth.ToString();
            this.lab_height.Text = PicHeight.ToString();
        }
    }

    #region 處理
    public void UploadPic()
    {
        if (this.FileUpload1.HasFile)
        {
            string FileSavePath = this.Server.MapPath(@"~") + "\\PicTemp\\";
            string exi = System.IO.Path.GetExtension(this.FileUpload1.FileName).Replace(".", "").ToLower();
            this.lab_ext.Text = exi;
            if (IsValidFileType(exi, this.lab_pictype.Text.ToLower()))
            {
                this.lab_checkftype.Text = "true";
                int filesize = this.FileUpload1.PostedFile.ContentLength;
                if (filesize <= Convert.ToInt32(this.lab_size.Text) * 1000)
                {
                    this.lab_checksize.Text = "true";
                    string filename = Guid.NewGuid().ToString("N") + "." + exi;
                    #region 處理縮圖
                    if (Thumbnail) //true 縮圖 false 不縮
                    {
                        if (PicWidth > 0 && PicHeight > 0)
                        {
                            string src = FileSavePath + this.FileUpload1.FileName;
                            this.FileUpload1.SaveAs(src);
                            PicObject.MakeThumbnail(src, FileSavePath + filename, PicWidth, PicHeight, ThumbnailMode.ToString());
                        }
                        else
                        {
                            this.FileUpload1.SaveAs(FileSavePath + filename);
                        }
                    }
                    else
                    {
                        this.FileUpload1.SaveAs(FileSavePath + filename);
                    }
                    #endregion
                    this.lab_filename.Text = filename;
                    this.lab_path.Text = FileSavePath + filename;
                    string src1 = ResolveClientUrl("~/PicTemp/" + filename + "?ran=" + new Random().Next(1000));
                    this.div_pic.InnerHtml = "<a href=\"" + src1 + "\" rel=\"lytebox\" title=\"" + PicTitle + "\" OnClick=\"return false;\" OnLoad=\"return true;\"><img src=" + src1 + " width=\"60\" height=\"50\"  /></a>";
                }
                else
                {
                    this.div_pic.InnerHtml = "檔案大小(" + this.lab_size.Text + "KB)過大：" + filesize;
                    this.lab_checksize.Text = "false";
                }
            }
            else
            {
                this.div_pic.InnerHtml = "檔案類型(" + this.lab_pictype.Text + ")錯誤：" + this.lab_ext.Text;
                this.lab_checkftype.Text = "false";
            }
        }

    }
    #endregion

    #region 預覽
    protected void btn_preview_Click(object sender, EventArgs e)
    {
        if (this.FileUpload1.HasFile)
        {
            UploadPic();
        }
    }
    #endregion

    #region 檔案類型是否正確(true:是 false:否)
    /// <summary>
    /// 檔案類型是否正確(true:是 false:否)
    /// </summary>
    /// <param name="filetype">副檔名</param>
    /// <param name="validfiletype">檔案類型字串(以,區隔)</param>
    /// <returns></returns>
    public bool IsValidFileType(string filetype, string validfiletype)
    {
        string[] vft = validfiletype.Split(',');
        int vft_count = 0;
        for (int i = 0; i < vft.Length; i++)
        {
            if (filetype.Equals(vft[i].ToString()))
            {
                vft_count++;
            }
        }
        if (vft_count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion
}
