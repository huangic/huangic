<%@ WebHandler Language="C#" Class="FolderHandle" %>

using System;
using System.Web;
using System.Collections.Generic;
using Newtonsoft.Json;
using Entity;
using System.Linq;
using System.Web.SessionState;
using NXEIP.FileManager;

public class FolderHandle : IHttpHandler, IRequiresSessionState
{
    
    private SessionObject sessionObj = new SessionObject();
    public void ProcessRequest (HttpContext context) {
        
        
        
        //目錄分享處理 
        
        //取處理方式
        
            //新增分享 
            //刪除
            //修改名稱
            //搬移
        
       
        
        
        context.Response.ContentType = "text/plain";
        
        //取參數
        //operation=get_children&id=1

    
        String handle = context.Request["handle"];
       


        //取目前的值或是建立一個
        if (handle == "check") {

            this.CheckShare(context);   
            
            return;
        }

        
        //寫入資料庫
        if (handle == "add")
        {
            this.AddShare(context);
            return;
        }

        
        
        //刪除分享紀錄
        if (handle == "stop")
        {
            this.StopShare(context);
            return;
        }
        
        
        
        
        //context.Response.Write("error handle");
    }

    private void StopShare(HttpContext context)
    {
        int id;

        int.TryParse(context.Request["id"], out id);
               

        using (NXEIPEntities model = new NXEIPEntities())
        {
            var setting = (from d in model.doc14 where d.d01_no == id select d).FirstOrDefault();


            //找之前的設定
            if (setting != null)
            {
                //讀取設定

                model.doc14.DeleteObject(setting);
                model.SaveChanges();


            }
           

            

            context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(new { result = "" }));
            context.Response.End();
        }
    }

    private void AddShare(HttpContext context) {
        int id;

        int.TryParse(context.Request["id"], out id);
        
        string network = context.Request["network"], pwd = context.Request["pwd"];
        

        using (NXEIPEntities model = new NXEIPEntities()) {
            var setting = (from d in model.doc14 where d.d01_no == id select d).FirstOrDefault();


            
            
            
            //找之前的設定
            if (setting != null)
            {
                //讀取設定
                
                //network = setting.d14_network;
                setting.d14_passwd=pwd;
                
               
               
                
            }
            else { 
             //Add

               doc14 d=new doc14();
               d.d01_no = id;
                d.d14_network=network;
                d.d14_passwd=pwd;
                model.doc14.AddObject(d);
            }

            model.SaveChanges();

            context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(new { result=""}));
            context.Response.End();
        }
    }
     
    
    
    
    private void CheckShare(HttpContext context) {
        int id;

        int.TryParse(context.Request["id"], out id);

        using (NXEIPEntities model = new NXEIPEntities()) {
            var setting = (from d in model.doc14 where d.d01_no == id select d).FirstOrDefault();


            String root = context.Request.Url.GetLeftPart(UriPartial.Authority) + context.Request.ApplicationPath + (context.Request.ApplicationPath == "/" ? "" : "/")+"Share/";
            string network = "", pwd = "", status = "0";
            
            
            
            if (setting != null)
            {
                //讀取設定
                
                network = setting.d14_network;
                pwd = setting.d14_passwd;
                status = "1";
               
               
                
            }
            else { 
             //init

                network = Guid.NewGuid().ToString("N");
                pwd ="";
                status = "0";
                
            }



            context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(new { root=root,network=network,pwd=pwd,status=status}));
            //context.Response.End();
        }
        
    }
        
    








    public bool IsReusable
    {
        get
        {
            return false;
        }

    }

  
}