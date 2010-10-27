<%@ WebHandler Language="C#" Class="_200104_4" %>

using System;
using System.Web;
using System.Web.SessionState;
using NXEIP;
using Entity;
using System.Linq;
public class _200104_4 : IHttpHandler, IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {

        int d06=int.Parse(context.Request["d06"]);
        int d07 = int.Parse(context.Request["d07"]);
        String msg = "";
        using (NXEIPEntities model = new NXEIPEntities()) {

            int peo_uid = int.Parse(new SessionObject().sessionUserID);

            int doc06 = (from d in model.doc06 where d.d06_no == d06 && d.d06_peouid == peo_uid select d).Count();

           
            
            if (doc06 > 0)
            {
               var doc07 = (from d in model.doc07 where d.d06_no == d06 && d.d07_no == d07 select d).First();
               model.doc07.DeleteObject(doc07);
               model.SaveChanges();
            }
            else {
                msg = "沒有權限";
            }
            
        }
        
        
        
       // context.Response.ContentType = "text/plain";
        context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(msg));
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}