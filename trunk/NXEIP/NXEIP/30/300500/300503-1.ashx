<%@ WebHandler Language="C#" Class="_300503_1" %>

using System;
using System.Web;
using Entity;
using System.Linq;
using System.IO;
using System.Web.SessionState;

public class _300503_1 : IHttpHandler, IRequiresSessionState
{

   private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
    
   public void ProcessRequest(HttpContext context)
	    {
	        
       
       
       int off_no = int.Parse(context.Request.QueryString["ID"]);
       
	        //讀取DB
	        using (NXEIPEntities model=new NXEIPEntities())
	        {
	         
             try{
                 officials o = (from d in model.officials where d.off_no == off_no select d).First();

                 context.Response.Buffer = true;
                 context.Response.Clear();
                 context.Response.ContentType = "application/download";
                 context.Response.AddHeader("Content-Disposition", "attachment;   filename=" + HttpUtility.UrlEncode(o.off_name, System.Text.Encoding.UTF8) + ";");


                 context.Response.BinaryWrite(o.off_file);
                 context.Response.Flush();
                 context.Response.End();
	          }catch(Exception ex){
               logger.Debug(ex.Message);
               //context.Response.StatusCode=300;
              }
	        }
   }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}