<%@ WebHandler Language="C#" Class="_100103_1" %>

using System;
using System.Web;
using Entity;
using System.Linq;
using System.IO;



//抓圖片
public class _100103_1 : IHttpHandler {



    private String upload_dir = new ArgumentsObject().Get_argValue("100103_dir");
    
    
    
    public void ProcessRequest (HttpContext context) {



        String pic = context.Request["pic"];
        
        //取圖片ID
        int albumId=0;
            
        int.TryParse(context.Request["album"],out albumId);
        int phohoId = 0;
        int.TryParse(context.Request["photo"],out phohoId);
       
        
        //取圖片


        byte[] img_bin=null;

        using (NXEIPEntities model = new NXEIPEntities()) {

            try
            {
                var Photo = (from d in model.photo where d.alb_no == albumId && d.pho_no == phohoId select d).First();
                //File file=F

                if (pic == "org")
                {
                    img_bin = File.ReadAllBytes(this.upload_dir + Photo.pho_file);
                }
                else
                {
                    img_bin = File.ReadAllBytes(this.upload_dir + Photo.pho_thumb);
                }
                
            }
            catch
            {
                img_bin = File.ReadAllBytes(context.Server.MapPath("~/image/noimage.jpg"));
            
            }


           


           
        }
        
        
       
        
        
        
        context.Response.ContentType = "image/jpeg";
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