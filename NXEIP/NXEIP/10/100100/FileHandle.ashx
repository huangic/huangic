<%@ WebHandler Language="C#" Class="FileHandle" %>

using System;
using System.Web;
using System.Collections.Generic;
using Newtonsoft.Json;
using Entity;
using System.Linq;
using System.Web.SessionState;
using Newtonsoft.Json.Linq;
using System.IO;
using ComLib.Reflection;
using System.Security.Cryptography;
using System.Text;


public class FileHandle : IHttpHandler, IRequiresSessionState
{


    private NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
    private NXEIPEntities model=new NXEIPEntities();

    private SessionObject sessionObj = new SessionObject();
    public void ProcessRequest (HttpContext context) {
        
        
        
        //檔案處理 
        
        //取處理方式
        
           
            //刪除
            //複製
            //搬移
        
       
        
        
        context.Response.ContentType = "text/plain";
        
        //取JSON參數
        JObject json=JsonLib.GetRequestJsonObject(context.Request);

        string handle = (string)json["handle"];

        if (handle == "move")
        {
            this.MoveFile(context, json);
            return;
        }

        if (handle == "copy")
        {
            this.CopyFile(context, json);
            return;
        }

        if (handle == "delete")
        {
            this.DeleteFile(context, json);
            return;
        }
        
        
        
        context.Response.Write("{msg:error handle}");
        }
        
        
        
        
        
    








    public bool IsReusable
    {
        get
        {
            return false;
        }

    }

   private void MoveFile(HttpContext context,JObject json){
        //把FILE的父代替換
       //直接用UPDATE 更新
       JArray files = (JArray)json["files"];
       int parent_id = int.Parse((string)json["folderId"]);

       
       using(NXEIPEntities model=new NXEIPEntities()){
       
          foreach (var fid in files) {
             int id = int.Parse((string)fid);
             var file = (from f in model.doc01 where f.d01_no == id select f).First();
             file.d01_parentid = parent_id;
           }

       model.SaveChanges();
       context.Response.Write("{\"msg\":\"success\"}");
       }
       
   }


   //todo: 儲存檔案目錄參數
   private void CopyFile(HttpContext context, JObject json) {

       //把FILE的父代替換
       //直接用UPDATE 更新
       JArray files = (JArray)json["files"];
       int parent_id = int.Parse((string)json["folderId"]);
       string Msg = String.Empty;

       string upload_dir = GetPathArg();
       
           using (NXEIPEntities model = new NXEIPEntities())
           {

               //取父帶目錄資料
               
               var parentFolder=(from f in model.doc01 where f.d01_no==parent_id select f).First();
               int FolderPeopleUid=parentFolder.peo_uid;
               String newPath = "/upload/" + FolderPeopleUid;
               
               foreach (var fid in files)
               {

                   
                   
                   
                   
                   
                   int id = int.Parse((string)fid);
                   var fileDetail = (from f2 in model.doc02
                                    from f in model.doc01
                                    where f.d01_no == id && f2.d01_no == f.d01_no && f2.d02_open == "2"
                                    select new { file = f, detail = f2 }).First();


                   //找重複黨名(重複就跳出迴圈)
                     int countfile=(from f in model.doc01 where f.d01_file==fileDetail.file.d01_file &&f.d01_parentid==parent_id select f).Count();
                     if (countfile > 0) {
                         Msg += fileDetail.file.d01_file+"檔案重複\\n";
                         continue;
                     }
                   
                    
                   //路徑的判斷 路徑是使用員工UID的 所以要以複製過去的目錄PEO_UID做為實體檔案目錄
                   //string folder_peoUid=fileDetail.file



                   String newFilename = DateTime.Now.ToString("yMdhhmmssfff") + "." + fileDetail.detail.d02_format;
                   try
                   {
                       String sourcePath=upload_dir + fileDetail.detail.d02_path;
                      
                       String filePath = newPath + "/" + newFilename ;
                       String distPath = upload_dir + filePath;
                       //複製檔案

                       File.Copy(sourcePath,distPath,true);



                       doc01 newFileDoc = new doc01();
                       doc02 newFileDetail = new doc02();
                       
                     
                       //屬性複製
                       EntityLib.CopyProperties(fileDetail.file, newFileDoc);

                       //Entity 無法使用REFLECTION直接複製 會有很多屬性有問題
                       
                       //改變屬性
                     

                       newFileDoc.d01_name = fileDetail.file.d01_name;
                       newFileDoc.d01_file = fileDetail.file.d01_file;
                       newFileDoc.d01_no = 0;
                       newFileDoc.d01_parentid=parent_id;
                       newFileDoc.peo_uid=FolderPeopleUid;
                       newFileDoc.d01_createuid = int.Parse(sessionObj.sessionUserID);
                       newFileDoc.d01_createtime = DateTime.Now;
                       
                       //網址做MD5編碼
                       MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                       String md5String = BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(newFilename)));
                       md5String = md5String.Replace("-", "");
                       newFileDoc.d01_url = md5String;
                       
                       
                       
                       model.doc01.AddObject(newFileDoc);


                       EntityLib.CopyProperties(fileDetail.detail, newFileDetail);
                       
                     

                       
                       newFileDetail.d02_open = "2";
                       newFileDetail.d02_path = filePath;
                       newFileDetail.d02_no = 1;
                       newFileDetail.d02_version = 1;
                       newFileDetail.d02_date = DateTime.Now;
                       newFileDoc.doc02.Add(newFileDetail);

                      
                       model.SaveChanges();
                       
                       

                       
                      



                       
                   }catch(Exception ex){
                       context.Response.Write("{\"msg\":\""+ex.Message+"\"}");
                   }
               }

               //model.SaveChanges();
               
           }
           //資料庫存檔
           if (String.IsNullOrEmpty(Msg))
           {
               context.Response.Write("{\"msg\":\"success\"}");
           }
           else
           {
               context.Response.Write("{\"msg\":\"" + Msg + "\"}");
           }
       
       }

   private string GetPathArg()
   {
       ArgumentsObject args = new ArgumentsObject();
       string upload_dir = args.Get_argValue("upload_dir");

       //抓系統上傳路徑(空就用系統根木路)
       if (String.IsNullOrEmpty(upload_dir))
       {
           upload_dir = System.Web.HttpContext.Current.Server.MapPath("~/");
       }

       logger.Debug("上傳根目錄:" + upload_dir);
       return upload_dir;
   }
      
   private void DeleteFile(HttpContext context,JObject json){
       JArray files = (JArray)json["files"];
       try
       {
           using (NXEIPEntities model = new NXEIPEntities())
           {

               string FilePath = GetPathArg();
               foreach (var fid in files)
               {
                   int id = int.Parse((string)fid);
                   var file = (from f in model.doc01 where f.d01_no == id select f).First();
                   var fileDetial = from f2 in model.doc02 where f2.d01_no == id select f2;
                   //檔案刪除
                   //檔案路徑
                   //
                   model.DeleteObject(file);
                   foreach (var d in fileDetial)
                   {

                       File.Delete(FilePath + d.d02_path);
                       model.DeleteObject(d);
                   }
                   //資料庫刪除
               }

               model.SaveChanges();
               context.Response.Write("{\"msg\":\"success\"}");
           }
       }
       catch (Exception ex) {
           context.Response.Write("{\"msg\":\""+ex.Message+"\"}");
       }
       
   }    
       
       
   
   

}