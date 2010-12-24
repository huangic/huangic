<%@ WebHandler Language="C#" Class="FileFolder"%>

using System;
using System.Web;
using System.Collections.Generic;
using Newtonsoft.Json;
using Entity;
using System.Linq;
using System.Web.SessionState;
using NXEIP.FileManager;
using NXEIP.FileManager.Json;
using NXEIP.DAO;
using NXEIP.JsTree;
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
    private ICollection<JsTreeJson> getPersonRootFilder(int pid, int peo_uid)
    {

         using (NXEIPEntities model = new NXEIPEntities())
         {

             ICollection<JsTreeJson> fs = new List<JsTreeJson>();
             //取自己的檔案目錄
             var folders = from f in model.doc01 where f.d01_parentid == pid && f.people.peo_uid == peo_uid && f.d01_type=="1" && !String.IsNullOrEmpty(f.d01_name) select f;
             try
             {
                 foreach (var folder in folders)
                 {

                     JsTreeJson f = new EntityFolderJSON(folder);

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
     private ICollection<JsTreeJson> getDepartRootFilder(int parent_id,int dep_id)
     {

         using (NXEIPEntities model = new NXEIPEntities())
         {

             ICollection<JsTreeJson> fs = new List<JsTreeJson>();
             //取部門的的檔案目錄
             var folders = from f in model.doc01 where f.d01_parentid==parent_id && f.dep_no == dep_id && f.d01_type == "2" && !String.IsNullOrEmpty(f.d01_name) select f;
             try
             {
                 foreach (var folder in folders)
                 {

                     JsTreeJson f = new EntityFolderJSON(folder);

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
     private ICollection<JsTreeJson> getFolder(int pid)
     {

         using (NXEIPEntities model = new NXEIPEntities())
         {

             ICollection<JsTreeJson> fs = new List<JsTreeJson>();

             var folders = from f in model.doc01 where f.d01_parentid == pid && !String.IsNullOrEmpty(f.d01_name) select f;

             foreach (var folder in folders)
             {

                 JsTreeJson f = new EntityFolderJSON(folder);


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



            ICollection<JsTreeJson> fs = new List<JsTreeJson>();

            //回傳


            JsTreeJson f = new JsTreeJson();
            
           
            f.data = "使用者文件夾";

            FolderAttrJson attr = new FolderAttrJson();
            
            
            
            attr.id = "0";
            attr.depid = sessionObj.sessionUserDepartID;
            attr.folderType = "1";

            f.attr = attr;
        
            
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
                if (dep.dep_parentid != 0)
                {
                JsTreeJson dep_f = new JsTreeJson();


                    dep_f.data = dep.dep_name + "文件夾";

                    FolderAttrJson newAttr = new FolderAttrJson();


                    newAttr.id = dep.dep_no+"_0";
                    newAttr.depid = dep.dep_no.ToString();
                    newAttr.folderType = "2";

                    dep_f.attr = newAttr;
                

                    //取第一層目錄
                    dep_f.children = getDepartRootFilder(pid, dep.dep_no);

                    if (dep_f.children.Count > 0)
                    {
                        dep_f.state = "closed";
                    }


                    fs.Add(dep_f);
                }
            }
            

            context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(fs));
        
        }
        else {

            ICollection<JsTreeJson> fs;




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