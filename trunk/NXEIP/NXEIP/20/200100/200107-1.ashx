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
			
	   
	   
	   int doc09_no = int.Parse(context.Request.QueryString["d09"]);
	   int doc10_no = int.Parse(context.Request.QueryString["d10"]);
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
				var file =(from d in model.doc10 where d.d09_no==doc09_no && d.d10_no==doc10_no select d).First();





					string filename =file.d10_file;
					context.Response.Buffer = true;
					context.Response.Clear();
					context.Response.ContentType = "application/download";
					context.Response.AddHeader("Content-Disposition", "attachment;   filename=" + HttpUtility.UrlEncode(filename, System.Text.Encoding.UTF8) + ";");
					
				
					//取資料庫的檔案根目錄參數
					//取上傳目錄
					ArgumentsObject args = new ArgumentsObject();

					string path = args.Get_argValue("200107_dir");
					string fileabspath=path+file.d10_path;
				
					if (string.IsNullOrEmpty(path))
					{
						fileabspath = context.Server.MapPath(file.d10_path);
					}
				
					//寫入下載人員
					file.d10_count++;
				  
				  
				  
				 
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