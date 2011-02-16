<%@ WebHandler Language="C#" Class="_200601_13" %>

using System;
using System.Web;
using Entity;
using System.Linq;
using System.IO;
using System.Web.SessionState;

public class _200601_13 : IHttpHandler, IRequiresSessionState
{

   private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
    
   public void ProcessRequest(HttpContext context)
	    {
	        
       
       
       int tao_no = int.Parse(context.Request.QueryString["tao_no"]);
       int t01_no = int.Parse(context.Request.QueryString["t01_no"]);
       
             
       
	        //讀取DB
	        using (NXEIPEntities model=new NXEIPEntities())
	        {
	         
             try{
                 tao01 o = (from d in model.tao01 where d.tao_no == tao_no && d.t01_no==t01_no select d).First();

                 context.Response.Buffer = true;
                 context.Response.Clear();
                 context.Response.ContentType = "application/download";
                 context.Response.AddHeader("Content-Disposition", "attachment;   filename=" + HttpUtility.UrlEncode(o.t01_file, System.Text.Encoding.UTF8) + ";");


                 //取資料庫的檔案根目錄參數
                 //取上傳目錄
                 ArgumentsObject args = new ArgumentsObject();

                 string path = args.Get_argValue("200601_dir");
                 string fileabspath = path + o.t01_path;

                 if (string.IsNullOrEmpty(path))
                 {
                     fileabspath = context.Server.MapPath(o.t01_path);
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