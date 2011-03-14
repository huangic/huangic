<%@ WebHandler Language="C#" Class="_200801_1" %>

using System;
using System.Web;
using Entity;
using System.Linq;
using System.IO;



//抓圖片
public class _200801_1 : IHttpHandler {



    private String upload_dir = new ArgumentsObject().Get_argValue("200801_dir");
    
    
    
    public void ProcessRequest (HttpContext context) {



        int id = 0;
        int.TryParse(context.Request["id"], out id);
       
        
        //取圖片


        byte[] img_bin=null;

        using (NXEIPEntities model = new NXEIPEntities()) {

            try
            {
                var Photo = (from d in model.unmarried where d.unm_no == id select d).First();
                //File file=F

               
               
                    img_bin = File.ReadAllBytes(this.upload_dir + Photo.unm_path);
                    context.Response.ContentType = "image/"+Photo.unm_type.Substring(1);
                
            }
            catch
            {
                img_bin = File.ReadAllBytes(context.Server.MapPath("~/image/noimage.jpg"));
                context.Response.ContentType = "image/jpeg";
            }


           


           
        }
        
        
       
        
        
        
       
        //context.Response.Write("Hello World");





        context.Response.BinaryWrite(img_bin);
        context.Response.Flush();
        context.Response.End();
        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}