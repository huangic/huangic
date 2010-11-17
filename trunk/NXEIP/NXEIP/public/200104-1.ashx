<%@ WebHandler Language="C#" Class="_200104_1" %>

using System;
using System.Web;
using Entity;
using System.Linq;
using System.IO;
using System.Web.SessionState;

public class _200104_1 : IHttpHandler, IRequiresSessionState
{

   private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
    
   public void ProcessRequest(HttpContext context)
	    {
	        
       
       
       int doc06_no = int.Parse(context.Request.QueryString["d06"]);
       int doc07_no = int.Parse(context.Request.QueryString["d07"]);
	        //讀取DB
	        using (NXEIPEntities model=new NXEIPEntities())
	        {
	          try{
                //權限判斷
                
                
                
                //取檔案資訊
                var file =(from d in model.doc07 where d.d06_no==doc06_no && d.d07_no==doc07_no select d).First();





                    string filename =file.d07_file;
	 	            context.Response.Buffer = true;
	                context.Response.Clear();
	                context.Response.ContentType = "application/download";
	                context.Response.AddHeader("Content-Disposition", "attachment;   filename=" + HttpUtility.UrlEncode(filename, System.Text.Encoding.UTF8) + ";");
	                
                
                    //取資料庫的檔案根目錄參數
                    //取上傳目錄
                    ArgumentsObject args = new ArgumentsObject();

                    string path = args.Get_argValue("200104_dir");
                    string fileabspath=path+file.d07_path;
                
                    if (string.IsNullOrEmpty(path))
                    {
                        fileabspath = context.Server.MapPath(file.d07_path);
                    }
                
                    //寫入下載人員
                    file.d07_count++;
                  
                   
                  
                 
                  model.SaveChanges();
                  
                  
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