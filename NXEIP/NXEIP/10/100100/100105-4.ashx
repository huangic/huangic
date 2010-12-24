<%@ WebHandler Language="C#" Class="_10_100100_100105_3" %>

using System;
using System.Web;
using Entity;
using System.Linq;
using System.IO;

public class _10_100100_100105_3 : IHttpHandler
{

   private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
    
   public void ProcessRequest(HttpContext context)
	    {
            int d01_no = int.Parse(context.Request.QueryString["d01"]);
            int d02_no = int.Parse(context.Request.QueryString["d02"]);
       
       
       
	        //讀取DB
	        using (NXEIPEntities model=new NXEIPEntities())
	        {
	          try{
                //權限判斷
                
                
                //取檔案資訊
                var file = (from d1 in model.doc01
                            from d2 in model.doc02
                            where d2.d01_no == d1.d01_no 
                            && d2.d02_no==d02_no
                            && d2.d01_no==d01_no
                            select new {File=d1,Detail=d2}).First();
                
               
                    string filename = file.File.d01_file;
	 	            context.Response.Buffer = true;
	                context.Response.Clear();
	                context.Response.ContentType = "application/download";
	                context.Response.AddHeader("Content-Disposition", "attachment;   filename=" + HttpUtility.UrlEncode(filename, System.Text.Encoding.UTF8) + ";");
	                
                
                    //取資料庫的檔案根目錄參數
                    //取上傳目錄
                    ArgumentsObject args = new ArgumentsObject();

                    string path = args.Get_argValue("100105_dir");
                    string fileabspath=path+file.Detail.d02_path;
                
                    if (string.IsNullOrEmpty(path))
                    {
                        fileabspath = context.Server.MapPath(file.Detail.d02_path);
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