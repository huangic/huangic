<%@ WebHandler Language="C#" Class="FileDownload" %>

using System;
using System.Web;
using Entity;
using System.Linq;

public class FileDownload : IHttpHandler {
    
   public void ProcessRequest(HttpContext context)
	    {
	        string downloadCode = context.Request.QueryString["code"];
	        //讀取DB
	        using (NXEIPEntities model=new NXEIPEntities())
	        {
	          
                //權限判斷
                
                
                //取檔案資訊
                doc02 file = (from d1 in model.doc01
                            where d1.d01_url == downloadCode
                            from d2 in model.doc02
                            where d2.d01_no == d1.d01_no && d2.d02_public == "1"
                            select d2).First();
                
                
                
                
                
               // string sql = "select * from [fileupload] where guid=@guid";
	           // SqlCommand cmd = new SqlCommand(sql, conn);
	           // cmd.Parameters.Add("guid", SqlDbType.Char, 36).Value = guid;
	           // conn.Open();
	            //SqlDataReader dr = cmd.ExecuteReader();
	           // if (dr.Read())
	           // {
	           //     string filename = dr["filename"].ToString();
	 	                context.Response.Buffer = true;
	                context.Response.Clear();
	                context.Response.ContentType = "application/download";
	                context.Response.AddHeader("Content-Disposition", "attachment;   filename=" + HttpUtility.UrlEncode(filename, System.Text.Encoding.UTF8) + ";");
	                context.Response.BinaryWrite(File.ReadAllBytes(context.Server.MapPath(string.Format(@"file\{0}.{1}", guid, Path.GetExtension(filename)))));
	                context.Response.Flush();
	                context.Response.End();
	            //}
	           // dr.Close();
	        }
   }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}