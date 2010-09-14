using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMMENSITY.SWFUploadAPI;
using System.Runtime.Serialization.Json;
using Entity;
using System.Security.Cryptography;
using System.Text;

public partial class _10_100100_100105_1 : System.Web.UI.Page
{

    NLog.Logger logger = NLog.LogManager.GetLogger("_10_100100_100105_1");

    SessionObject sessionObj = new SessionObject();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        



            this.UC_SWFUpload1.SwfUploadInfo = new SWFUploadInfo()
            {
                UploadMode = UpMode.LIST,
                File_size_limit = 1,
                Path="/upload/"+sessionObj.sessionUserID+"/",
                  
                Upload_url="~/lib/SWFUpload/uploadFileManager.aspx",
                
                SubmitButtonId = this.Button1.ClientID
            };



            SWFUploadFileInfo uf = new SWFUploadFileInfo();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(SWFUploadFileInfo));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            serializer.WriteObject(ms, uf);   

            //顯示工作目錄

            if (!this.IsPostBack) {
                try
                {
                    int pid = int.Parse(Request.Cookies["jstree_select"].Value.Replace("%23", ""));

                    using (NXEIPEntities model = new NXEIPEntities())
                    {

                        //取父代目錄

                        doc01 parentFolder = (from f in model.doc01 where f.d01_no == pid select f).FirstOrDefault();

                        string work_path = "/" + parentFolder.d01_name;

                        while (parentFolder.d01_parentid != 0)
                        {
                            parentFolder = (from f in model.doc01 where f.d01_no == parentFolder.d01_parentid select f).FirstOrDefault();

                            work_path = "/" + parentFolder.d01_name + work_path;
                        }
                        this.path.Text = work_path;
                    }
                }
                catch { 
                
                }
            }

        
    }
    /// <summary>
    /// 取消
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button2_Click(object sender, EventArgs e)
    {

        //移除上傳過的東西

        SWFUploadFile uf = new SWFUploadFile();

        foreach (var f in UC_SWFUpload1.SWFUploadFileInfoList) {
            logger.Debug(uf.Delete(f.Path,f.FileName,true));
        
        }



        this.Page.ClientScript.RegisterStartupScript(typeof(_10_100100_100105_1), "closeThickBox", "self.parent.update();", true);

    }
    /// <summary>
    /// 確定
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {

        //TODO: 根目錄要處理一下

        //取COOKIES 的父代目錄
        int pid = 0;
        try
        {
            pid = int.Parse(Request.Cookies["jstree_select"].Value.Replace("%23", ""));
        }catch{
        
        }
        using(NXEIPEntities model = new NXEIPEntities()){

        //取父代目錄

        doc01 parentFolder = (from f in model.doc01 where f.d01_no == pid select f).FirstOrDefault();


        
        
        //存檔
        //UC_SWFUpload1.
        foreach (var f in UC_SWFUpload1.SWFUploadFileInfoList)
        {
            logger.Debug(f);

            doc01 newStruts = new doc01();

            newStruts.peo_uid = parentFolder.peo_uid;
            newStruts.d01_parentid = pid;
            newStruts.d01_file = f.OriginalFileName;
            newStruts.d01_createuid = int.Parse(sessionObj.sessionUserID);
            newStruts.d01_createtime = DateTime.Now;


            //網址做MD5編碼
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            String md5String = BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(f.FileName)));
            md5String = md5String.Replace("-", "");


            newStruts.d01_url = md5String;

            //存檔
            model.doc01.AddObject(newStruts);

            model.SaveChanges();


            //文檔內文 
            doc02 newFile = new doc02();

            newFile.d01_no = newStruts.d01_no;
            newFile.d02_path = f.Path + f.FileName;
            newFile.d02_depname = sessionObj.sessionUserDepartName;
            newFile.d02_creator = sessionObj.sessionUserName;
            newFile.d02_no = 1;
            newFile.d02_public = "1";
            newFile.d02_version = 1;
            newFile.d02_KB = f.Size;
            newFile.d02_format = f.Extension;
            newFile.d02_open = "2";
            newFile.d02_date = DateTime.Now;
            //查詢用內文(需要讀檔)

            model.doc02.AddObject(newFile);
            model.SaveChanges();

        }
        }


        this.Page.ClientScript.RegisterStartupScript(typeof(_10_100100_100105_1), "closeThickBox", "self.parent.update();", true);

    }
}