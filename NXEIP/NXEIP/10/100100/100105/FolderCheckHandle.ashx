<%@ WebHandler Language="C#" Class="FolderCheckHandle" %>

using System;
using System.Web;
using System.Web.SessionState;
using NXEIP.FileManager;
using Entity;
using System.Linq;


/// <summary>
/// 權限檢驗
/// </summary>
public class FolderCheckHandle : IHttpHandler, IRequiresSessionState
{
    
    
    
    
    public void ProcessRequest (HttpContext context) {
        
        context.Response.ContentType = "text/plain";
        
        //取目錄ID
        
       
        int id = int.Parse(context.Request["id"]);
        int peo_uid = int.Parse(new SessionObject().sessionUserID);

        string permission = "false";
        
        
        
        //判斷目錄是否為空
        if (id != 0)
        {
            //不是跟目錄就用檔案目錄的資料判斷
            using (NXEIPEntities model = new NXEIPEntities())
            {

                var file = (from d in model.doc01 where d.d01_parentid == id select d).Count();


                if (file > 0)
                {
                    permission = "false";
                }
                else
                {

                    permission = "true";
                }
            }
            
            
            
        }
      




        context.Response.Write("{\"check\":" + permission + "}");
        context.Response.End();
    }
    
    
    
    
    
    
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}