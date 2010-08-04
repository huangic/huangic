<%@ WebHandler Language="C#" Class="FolderHandle"%>

using System;
using System.Web;
using System.Collections.Generic;
using Newtonsoft.Json;
using Entity;
using System.Linq;
using System.Web.SessionState;

public class FolderHandle : IHttpHandler, IRequiresSessionState
{
    
    private NXEIPEntities model=new NXEIPEntities();
    
    public void ProcessRequest (HttpContext context) {
        
        
        
        //目錄處理 
        
        //取處理方式
        
            //新增 
            //刪除
            //修改名稱
            //搬移
        
       
        SessionObject sessionObj=new SessionObject();
        
        context.Response.ContentType = "text/plain";
        
        //取參數
        //operation=get_children&id=1


        String handle = context.Request["handle"];


        int id, pid;
        
        
        int.TryParse(context.Request["id"],out id);
        int.TryParse(context.Request["pid"],out pid);
        
       
        
        
        if(!String.IsNullOrEmpty(handle)&&handle.Equals("move")){
        
            try{
           
             //目錄搬移
                
              
              
                
              doc01 folder=(from f in model.doc01 where f.d01_no==id select f).First();  
                
              
               int oldPid=folder.d01_parentid;
               
                
                folder.d01_parentid=pid;
                
                folder.d01_createuid=System.Convert.ToInt32(sessionObj.sessionUserID); 
                
                folder.d01_createtime=DateTime.Now;
                
                
                model.SaveChanges();
                
                
                //判斷舊目錄的有沒有下層目錄
                if (oldPid != 0)
                {
                    resetChildFolder(oldPid);
                }
                
                
                   //pid =0 表示他是根目路 不需要處理子目錄判斷
                if(!pid.Equals("0")){
                      
                doc01 parentFolder=(from f in model.doc01 where f.d01_no==pid select f).First();  
                    
                 //計算屬於下屬的目錄   
                resetChildFolder(pid);
               
              } 
                
                
                
                
            context.Response.Write("success");
            }catch(Exception ex){
            context.Response.Write(ex.Message);
            }
            return;
           
        }


        context.Response.Write("error handle");
        }
        
        
        
        
        
    








    public bool IsReusable
    {
        get
        {
            return false;
        }

    }
    
    /// <summary>
    /// 處理目錄是否有子目錄
    /// </summary>
    /// <param name="pid"></param>
    /// <returns></returns>
    private void resetChildFolder(int pid){

        doc01 parentFolder = (from f in model.doc01 where f.d01_no == pid select f).First();

        //計算屬於下屬的目錄   
        int count = (from f in model.doc01 where f.d01_parentid == pid && !String.IsNullOrEmpty(f.d01_name) select f).Count();

        parentFolder.d01_son = count > 0 ? 1 : 2;

        model.SaveChanges();
        
        
      
    }
    
}