<%@ WebHandler Language="C#" Class="PermissionHandle" %>

using System;
using System.Web;
using System.Web.SessionState;



/// <summary>
/// 權限檢驗
/// </summary>
public class PermissionHandle : IHttpHandler,IRequiresSessionState {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Write("Hello World");
    }
    
    
    
    
    
    
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}