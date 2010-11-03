<%@ WebHandler Language="C#" Class="_200107_1" %>

using System;
using System.Web;
using Entity;
using System.Linq;
using System.IO;
using System.Web.SessionState;

public class _200107_1 : IHttpHandler, IRequiresSessionState
{

   private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
    
   public void ProcessRequest(HttpContext context)
	    {
	        
       
       
       int doc11_no = int.Parse(context.Request.QueryString["d11"]);
       int doc13_no = int.Parse(context.Request.QueryString["d13"]);
	        //讀取DB
	        using (NXEIPEntities model=new NXEIPEntities())
	        {
	          try{
                //權限判斷
                
                  //SESSION 判斷 沒有就不用做下面了
                  //SessionObject sessionObj = new SessionObject();


                  //if (String.IsNullOrEmpty(sessionObj.sessionUserID)) {
                  //    context.Response.StatusCode = 403;
                  //    context.Response.Flush();
                 //     context.Response.End();
                 // }
                
                //取檔案資訊
                var file =(from d in model.doc13 where d.d11_no==doc11_no && d.d13_no==doc13_no select d).First();





                    string filename =file.d13_name;
	 	            context.Response.Buffer = true;
	                context.Response.Clear();
	                context.Response.ContentType = "application/download";
	                context.Response.AddHeader("Content-Disposition", "attachment;   filename=" + HttpUtility.UrlEncode(filename, System.Text.Encoding.UTF8) + ";");
	                
                
                    //取資料庫的檔案根目錄參數
                    //取上傳目錄
                    ArgumentsObject args = new ArgumentsObject();

                    string path = args.Get_argValue("200105_dir");
                    string fileabspath=path+file.d13_path;
                
                    if (string.IsNullOrEmpty(path))
                    {
                        fileabspath = context.Server.MapPath(file.d13_path);
                    }
                
                    //寫入下載人員
                    //file.d13_count++;
                  
                  
                  
                 
                  //model.SaveChanges();
                  
                  
                    context.Response.BinaryWrite(File.ReadAllBytes(fileabspath));
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