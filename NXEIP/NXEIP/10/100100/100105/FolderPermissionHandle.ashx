<%@ WebHandler Language="C#" Class="FolderPermissionHandle" %>

using System;
using System.Web;
using System.Web.SessionState;
using NXEIP.FileManager;
using Entity;
using System.Linq;


/// <summary>
/// 權限檢驗
/// </summary>
public class FolderPermissionHandle : IHttpHandler, IRequiresSessionState
{
    
    
    
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        
        //取目錄ID
        int fid=FileManagerUtil.GetParentId(context.Request["pid"]);
        int dep = int.Parse(context.Request["depid"]);
        int folderType = int.Parse(context.Request["folderType"]);
        int peo_uid = int.Parse(new SessionObject().sessionUserID);

        string permission = "false";
        
        
        
        //判斷部門目錄權限
        if (fid != 0)
        {
            //不是跟目錄就用檔案目錄的資料判斷
            using (NXEIPEntities model = new NXEIPEntities())
            {

                var file = (from d in model.doc01 where d.d01_no == fid select d).First();
                
                int manager = (from d in model.manager where d.peo_uid == peo_uid && d.dep_no == file.dep_no select d).Count();
                if (manager > 0)
                {
                    permission = "true";
                }
            }
            
            
            
        }
        else { 
            //直接用DEP 判斷
            using (NXEIPEntities model = new NXEIPEntities())
            {

                int manager = (from d in model.manager where d.peo_uid == peo_uid && d.dep_no == dep select d).Count();
                if (manager > 0)
                {
                    permission = "true";
                }
            }
            
            
        }




        context.Response.Write("{\"permission\":" + permission + "}");
        context.Response.End();
    }
    
    
    
    
    
    
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}