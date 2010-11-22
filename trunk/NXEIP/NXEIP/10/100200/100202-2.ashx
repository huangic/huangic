<%@ WebHandler Language="C#" Class="_100202_2" %>

using System;
using System.Web;
using Entity;
using System.Linq;
using System.IO;
using System.Web.SessionState;

public class _100202_2 : IHttpHandler, IRequiresSessionState
{

   private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
    
   public void ProcessRequest(HttpContext context)
	    {
	        
       
       
       int id1 = int.Parse(context.Request.QueryString["id1"]);
       int id2 = int.Parse(context.Request.QueryString["id2"]);
	        //讀取DB
	        using (NXEIPEntities model=new NXEIPEntities())
	        {
	          try{
                
                
                //取檔案資訊
                var file =(from d in model.goback where d.tde_no==id1 && d.gob_no==id2 select d).First();





                    string filename =file.gob_file;
	 	            context.Response.Buffer = true;
	                context.Response.Clear();
	                context.Response.ContentType = "application/download";
	                context.Response.AddHeader("Content-Disposition", "attachment;   filename=" + HttpUtility.UrlEncode(filename, System.Text.Encoding.UTF8) + ";");
	                
                
                    //取資料庫的檔案根目錄參數
                    //取上傳目錄
                    ArgumentsObject args = new ArgumentsObject();

                    string path = args.Get_argValue("100202_dir");
                    string fileabspath=path+file.gob_path;
                
                    if (string.IsNullOrEmpty(path))
                    {
                        fileabspath = context.Server.MapPath(file.gob_path);
                    }
                
                 
                  
                  
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