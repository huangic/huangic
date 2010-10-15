<%@ WebHandler Language="C#" Class="PublicFolderTree" %>

using System;
using System.Web;
using Entity;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using NXEIP.JsTree;
using NXEIP.FileManager.Json;

using System.Web.SessionState;


/// <summary>
/// 讀取所有分享目錄的使用者
/// </summary>
public class PublicFolderTree : IHttpHandler,IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";

        int id =int.Parse( context.Request["id"]);
        int peo_id = int.Parse(context.Request["peoid"]);


        if (peo_id == -1) {
            ICollection<JsTreeJson> roots=GetRootFolder();
            context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(roots));
            context.Response.End();
        }



        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }


    private ICollection<JsTreeJson> GetRootFolder() { 
            //取開放檔案的使用者目錄
        
            
            using(NXEIPEntities model=new NXEIPEntities()){
        
             SessionObject sessionObj=new SessionObject();
                
        
            //取目前使用者id
             int peo_id = int.Parse(sessionObj.sessionUserID);
             int dep_id = int.Parse(sessionObj.sessionUserDepartID);   
                
                //找出有開放給這個使用者個的目錄
             var doc3_nos = (from doc5 in model.doc05 where doc5.d05_peouid == peo_id select doc5.d03_no).Union(
                         from doc4 in model.doc04 where doc4.d04_depno == dep_id select doc4.d03_no).Distinct();

             var folder_people = (from d03 in model.doc03
                                  from d01 in model.doc01
                                  where d01.d01_no == d03.d01_no && d01.d01_type == "1" && d01.peo_uid!=peo_id && doc3_nos.Contains(d03.d03_no)
                                  select d01.peo_uid).Distinct(); 
              var people=from p in model.people where folder_people.Contains(p.peo_uid) select p;

              ICollection<JsTreeJson> roots = new LinkedList<JsTreeJson>();

              foreach (var p in people) {
                  roots.Add(new PublicFolderJSON(p));
              
                
              }

              return roots;
                    
          }        
    }
    
}