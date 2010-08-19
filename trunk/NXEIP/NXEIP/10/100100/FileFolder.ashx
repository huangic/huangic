<%@ WebHandler Language="C#" Class="FileFolder"%>

using System;
using System.Web;
using System.Collections.Generic;
using Newtonsoft.Json;
using Entity;
using System.Linq;
using System.Web.SessionState;
using FileManager;
/// <summary>
/// 檔案處理
/// </summary>
public class FileFolder : IHttpHandler,IRequiresSessionState
{
    
    /// <summary>
    /// get Root dir
    /// </summary>
    /// <param name="pid"></param>
    /// <param name="peo_uid"></param>
    /// <returns></returns>
     private ICollection<FolderJSON> getRootFilder(int pid,int peo_uid){

         using (NXEIPEntities model = new NXEIPEntities())
         {

             ICollection<FolderJSON> fs = new List<FolderJSON>();

             var folders = from f in model.doc01 where f.d01_parentid == pid && f.people.peo_uid == peo_uid select f;
             try
             {
                 foreach (var folder in folders)
                 {

                     FolderJSON f = new EntityFolderJSON(folder);

                     fs.Add(f);

                 }
             }
             catch
             {
             }

             return fs;
         }
        }
    
    /// <summary>
    /// get child dir
    /// </summary>
    /// <param name="pid"></param>
    /// <returns></returns>
     private ICollection<FolderJSON> getFilder(int pid)
     {

         using (NXEIPEntities model = new NXEIPEntities())
         {

             ICollection<FolderJSON> fs = new List<FolderJSON>();

             var folders = from f in model.doc01 where f.d01_parentid == pid && !String.IsNullOrEmpty(f.d01_name) select f;

             foreach (var folder in folders)
             {

                 FolderJSON f = new EntityFolderJSON(folder);


                 fs.Add(f);

             }

             return fs;
         }
     }
    
    
    public void ProcessRequest (HttpContext context) {
        
        
       
        SessionObject sessionObj=new SessionObject();
        
        context.Response.ContentType = "text/plain";
        
        //取參數
        //operation=get_children&id=1

        String operation = context.Request["operation"];
        String id = context.Request["id"];
        
         int pid;
        
        
        if(int.TryParse(id,out pid)){
            
           
        
        //id=0 表示顯示自己可用的目錄第一層目錄與別人開放的
        if (id == "0")
        {
            //取使用者的目錄結構
            

           
            ICollection<FolderJSON> fs = new List<FolderJSON>();

            //回傳


            FolderJSON f = new FolderJSON();
            
           
            f.data = "使用者文件夾";
            
            f.attr.id = "0";
        
            
            //取第一層目錄
            f.children=getRootFilder(pid,System.Convert.ToInt32(sessionObj.sessionUserID));

            if (f.children.Count > 0) {
                f.state = "closed";
            }
            
            
            
            fs.Add(f);



          
            
            
            
            

            context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(fs));
        
        }
        else {

            ICollection<FolderJSON> fs;
           
            
            
                
            fs=getFilder(pid);
            
            //取使用者的目錄結構

            context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(fs));

            
           
        }
        
        }
        
        
        
    }








    public bool IsReusable
    {
        get
        {
            return false;
        }

    }
}