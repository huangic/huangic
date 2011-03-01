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

        int depid = 0;
        try
        {
            depid = int.Parse(Request.Cookies["depid"].Value);
        }
        catch {
            depid = int.Parse(sessionObj.sessionUserDepartID);
        }



        string folderType = "1";
        try{
         folderType=Request.Cookies["folderType"].Value;
        }catch{
        
        }

         string uploadDir=null;
         using (NXEIPEntities model = new NXEIPEntities())
         {

             int pid = 0;

             try
             {
                 //取父代目錄
                 pid = int.Parse(Request.Cookies["jstree_select"].Value.Replace("%23", ""));
             }
             catch { 
             
             }
             
             
             if (pid != 0)
             {
                 doc01 parentFolder = (from f in model.doc01 where f.d01_no == pid select f).FirstOrDefault();
                 depid = parentFolder.dep_no;
                 folderType = parentFolder.d01_type;
             }

             if (folderType == "2")
             {
                 uploadDir = "/upload/100105/department/" + depid;
             }

         }


         int size = 0;

         int.TryParse(new ArgumentsObject().Get_argValue("100105_size"),out size);
         this.tb_size.Text = String.Format("(單一檔案限制{0}MB)",size);

        this.UC_SWFUpload1.SwfUploadInfo = new SWFUploadInfo()
        {
            UploadMode = UpMode.LIST,
            File_size_limit = size,
            


            //人員目錄
            Path = uploadDir??"/upload/100105/people/" + sessionObj.sessionUserID + "/",
            //部門目錄  


            Upload_url = "~/lib/SWFUpload/uploadFileManager.aspx",

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
                    int pid = 0;
                        try{
                        pid= int.Parse(Request.Cookies["jstree_select"].Value.Replace("%23", ""));
                        }catch{
                        
                        }
                    using (NXEIPEntities model = new NXEIPEntities())
                    {
                        if (pid != 0)
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
                        
                         //根目錄顯示
                            if (folderType == "1")
                            {
                                this.path.Text = "使用者文件夾/"+this.path.Text;
                            }
                            else { 
                                //
                                departments dep = (from d in model.departments where d.dep_no == depid select d).First();

                                this.path.Text = dep.dep_name + "文件夾/" + this.path.Text;


                            }


                       


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

            String del_msg = uf.Delete(f.Path, f.FileName, true);

            logger.Debug(del_msg);
        
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
         int depid = int.Parse(Request.Cookies["depid"].Value);
         string folderType = Request.Cookies["folderType"].Value;
             
        try
        {
            pid = int.Parse(Request.Cookies["jstree_select"].Value.Replace("%23", ""));

           
        }catch{
        
        }
        using(NXEIPEntities model = new NXEIPEntities()){

        //取父代目錄

        doc01 parentFolder = (from f in model.doc01 where f.d01_no == pid select f).FirstOrDefault();


        if (pid != 0) {
            depid = parentFolder.dep_no;
            folderType = parentFolder.d01_type;
        }
        
        //存檔
        //UC_SWFUpload1.
        foreach (var f in UC_SWFUpload1.SWFUploadFileInfoList)
        {
            logger.Debug(f);

            doc01 newStruts = new doc01();


            if (pid != 0)
            {
                newStruts.peo_uid = parentFolder.peo_uid;
            }
            else {
                newStruts.peo_uid = int.Parse(sessionObj.sessionUserID);
            }
            newStruts.d01_parentid = pid;
            newStruts.d01_file = f.OriginalFileName;
            newStruts.d01_createuid = int.Parse(sessionObj.sessionUserID);
            newStruts.d01_createtime = DateTime.Now;
            newStruts.d01_type = folderType;
            newStruts.dep_no = depid;



            //網址做MD5編碼
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            String md5String = BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(f.FileName)));
            md5String = md5String.Replace("-", "");


            newStruts.d01_url = md5String;

            //存檔
            model.doc01.AddObject(newStruts);

           


            //文檔內文 
            doc02 newFile = new doc02();

            newFile.d01_no = newStruts.d01_no;
            newFile.d02_path = f.Path+"/" + f.FileName;
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


             OperatesObject.OperatesExecute(100105, 1, String.Format("新增檔案 doc01_no:{0},doc02_no:{1}",+newFile.d01_no,newFile.d02_no));

        }
        model.SaveChanges();

        }


        this.Page.ClientScript.RegisterStartupScript(typeof(_10_100100_100105_1), "closeThickBox", "self.parent.update();", true);

    }
}