<%@ WebHandler Language="C#" Class="FileFolder"%>

using System;
using System.Web;
using System.Collections.Generic;
using Newtonsoft.Json;
using Entity;
using System.Linq;
using System.Web.SessionState;
using FileManager;
using NXEIP.DAO;
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
     private ICollection<FolderJSON> getPersonRootFilder(int pid,int peo_uid){

         using (NXEIPEntities model = new NXEIPEntities())
         {

             ICollection<FolderJSON> fs = new List<FolderJSON>();
             //取自己的檔案目錄
             var folders = from f in model.doc01 where f.d01_parentid == pid && f.people.peo_uid == peo_uid && f.d01_type=="1" && !String.IsNullOrEmpty(f.d01_name) select f;
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
     /// get Depart Root dir
     /// </summary>
     /// <param name="pid"></param>
     /// <param name="peo_uid"></param>
     /// <returns></returns>
     private ICollection<FolderJSON> getDepartRootFilder(int parent_id,int dep_id)
     {

         using (NXEIPEntities model = new NXEIPEntities())
         {

             ICollection<FolderJSON> fs = new List<FolderJSON>();
             //取部門的的檔案目錄
             var folders = from f in model.doc01 where f.d01_parentid==parent_id && f.dep_no == dep_id && f.d01_type == "2" && !String.IsNullOrEmpty(f.d01_name) select f;
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
     private ICollection<FolderJSON> getFolder(int pid)
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
            f.attr.depid = sessionObj.sessionUserDepartID;
            f.attr.folderType = "1";
        
            
            //取第一層目錄
            f.children = getPersonRootFilder(pid, System.Convert.ToInt32(sessionObj.sessionUserID));

            if (f.children.Count > 0) {
                f.state = "closed";
            }
            
            
            
            fs.Add(f);


            //取部門文件夾

            DepartmentsDAO depDao = new DepartmentsDAO();

            ICollection<departments> departs=depDao.GetRecursiveParentDeprtment(int.Parse(sessionObj.sessionUserDepartID));

            foreach (departments dep in departs)
            {
               // if (dep.dep_parentid != 0)
               // {
                    FolderJSON dep_f = new FolderJSON();


                    dep_f.data = dep.dep_name + "文件夾";

                    dep_f.attr.id = "0";
                    dep_f.attr.depid = dep.dep_no.ToString();
                    dep_f.attr.folderType = "2";


                    //取第一層目錄
                    dep_f.children = getDepartRootFilder(pid, dep.dep_no);

                    if (dep_f.children.Count > 0)
                    {
                        dep_f.state = "closed";
                    }


                    fs.Add(dep_f);
               // }
            }
            

            context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(fs));
        
        }
        else {

            ICollection<FolderJSON> fs;




            fs = getFolder(pid);
            
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