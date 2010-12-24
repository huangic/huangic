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
    
    private NXEIPEntities model=new NXEIPEntities();

    private SessionObject sessionObj = new SessionObject();
    public void ProcessRequest (HttpContext context) {
        
        
        
        //目錄處理 
        
        //取處理方式
        
            //新增 
            //刪除
            //修改名稱
            //搬移
        
       
        
        
        context.Response.ContentType = "text/plain";
        
        //取參數
        //operation=get_children&id=1


        String handle = context.Request["handle"];
        string newname = context.Request["name"];
         
        
        
        int id, pid,depid;
        string folderType;
        
        
        int.TryParse(context.Request["id"],out id);
        //int.TryParse(context.Request["pid"],out pid);

        pid = FileManagerUtil.GetParentId(context.Request["pid"]);
        
        int.TryParse(context.Request["depid"], out depid);
        folderType=context.Request["folderType"]??"";


        //如果PID=0那就用傳過來的參數DEP_NO 跟FOLDER TYPR 不然就要抓資料庫目前的值 

        if (pid != 0)
        {
            doc01 parentFolder = (from f in model.doc01 where f.d01_no == pid select f).First();

            depid = parentFolder.dep_no;
            folderType = parentFolder.d01_type;
        }
        
        
        
        
        if(!String.IsNullOrEmpty(handle)&&handle.Equals("move")){
        
            try{
           
             //目錄搬移
                
             
                
                
                
                
                
              doc01 folder=(from f in model.doc01 where f.d01_no==id select f).First();  
                
              
               int oldPid=folder.d01_parentid;
               
                
                folder.d01_parentid=pid;
                
                folder.d01_createuid=System.Convert.ToInt32(sessionObj.sessionUserID); 
                
                folder.d01_createtime=DateTime.Now;

                folder.dep_no = depid;
                folder.d01_type = folderType.ToString();
                
                
                model.SaveChanges();
                
                
                //判斷舊目錄的有沒有下層目錄
                if (oldPid != 0)
                {
                    ResetChildFolder(oldPid);
                }
                
                
                   //pid =0 表示他是根目路 不需要處理子目錄判斷
                if(pid!=0){
                      
                doc01 parentFolder=(from f in model.doc01 where f.d01_no==pid select f).First();  
                    
                 //計算屬於下屬的目錄   
                ResetChildFolder(pid);
                    
                    
               
              }

                ChangeFolderType(id, depid, folderType.ToString());
                model.SaveChanges();

                OperatesObject.OperatesExecute(100105, 3, String.Format("目錄搬移 doc01_no:{0}, doc01_parentid:{1}", id, pid));
                      
                
            context.Response.Write("success");
            }catch(Exception ex){
            context.Response.Write(ex.Message);
            }
            return;
           
        }


        if (!String.IsNullOrEmpty(handle) && handle.Equals("create")) {
            try
            {
                
                //TODO:要多加部門目錄的判斷
                
                //新增一個目錄節點
                doc01 newFolder = new doc01();
               
                newFolder.d01_name = "新資料夾";
                newFolder.d01_parentid = pid;
                newFolder.d01_createuid = System.Convert.ToInt32(sessionObj.sessionUserID);
                newFolder.d01_createtime = DateTime.Now;
                newFolder.d01_son = 2;
                newFolder.peo_uid = System.Convert.ToInt32(sessionObj.sessionUserID);
                newFolder.dep_no = depid;
                newFolder.d01_type = folderType.ToString();
                
                model.doc01.AddObject(newFolder);
                model.SaveChanges();
                String new_id = newFolder.d01_no.ToString();

                ResetChildFolder(pid);
                context.Response.Write("{\"process\":\"success\",\"id\":\"" + new_id + "\"}");
                OperatesObject.OperatesExecute(100105, 1, String.Format("目錄建立 doc01_no:{0}, doc01_parentid:{1}", id, pid));
                
                return;
            }
            catch (Exception ex)
            {
                
                context.Response.Write(ex.Message);
                return;
            }
        }



        if (!String.IsNullOrEmpty(handle) && handle.Equals("rename"))
        {
            try
            {
                //找目錄節點
                doc01 newFolder = (from f in model.doc01 where f.d01_no==id select f).First();

                if (newFolder != null)
                {

                    newFolder.d01_name = newname;
                    newFolder.d01_createuid = System.Convert.ToInt32(sessionObj.sessionUserID);
                    newFolder.d01_createtime = DateTime.Now;
                    //newFolder.d01_son = 2;
                    newFolder.peo_uid = System.Convert.ToInt32(sessionObj.sessionUserID);
                    //model.doc01.AddObject(newFolder);
                    model.SaveChanges();

                    

                    context.Response.Write("{'process':'success','id':" + id + "}");
                    OperatesObject.OperatesExecute(100105, 3, String.Format("目錄修改名稱 doc01_no:{0}, doc01_name", id, newname));
                
                    return;
                }
            }
            catch (Exception ex)
            {

                context.Response.Write(ex.Message);
                return;
            }
        }


        if (!String.IsNullOrEmpty(handle) && handle.Equals("delete"))
        {
            try
            {
                //找目錄節點
                doc01 newFolder = (from f in model.doc01 where f.d01_no == id select f).First();

                if (newFolder != null)
                {

                    model.doc01.DeleteObject(newFolder);

                    model.SaveChanges();
                    //TODO 目錄下的所有檔案 要怎麼處理?



                    OperatesObject.OperatesExecute(100105, 3, String.Format("目錄刪除 doc01_no:{0}",id));
                
                    context.Response.Write("{'process':'success','id':" + id + "}");
                    return;
                }
            }
            catch (Exception ex)
            {

                context.Response.Write(ex.Message);
                return;
            }
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

    #region 處理目錄是否有子目錄
    /// <summary>
    /// 處理目錄是否有子目錄
    /// </summary>
    /// <param name="pid"></param>
    /// <returns></returns>
    private void ResetChildFolder(int pid){
        if (pid != 0)
        {
            doc01 parentFolder = (from f in model.doc01 where f.d01_no == pid select f).First();

            //計算屬於下屬的目錄   
            int count = (from f in model.doc01 where f.d01_parentid == pid && !String.IsNullOrEmpty(f.d01_name) select f).Count();

            parentFolder.d01_son = count > 0 ? 1 : 2;

            model.SaveChanges();
        }


    }
    #endregion



   
    
    
    /// <summary>
    /// 遞迴改變子代目錄的屬性項目(目錄所屬部門 與目錄型態)
    /// </summary>
    /// <param name="docNo"></param>
    /// <param name="depNo"></param>
    /// <param name="type"></param>
    private void ChangeFolderType(int parentDocNo,int depNo,string docType) { 
        //取工作目錄
        // currentDoc = (from d in model.doc01 where d.d01_no == docNo select d).First();

        //currentDoc.d01_type = type;
        //currentDoc.dep_no = depNo;
        
        //取子代
        var childs = (from d in model.doc01 where d.d01_parentid == parentDocNo select d);

        foreach (doc01 child in childs) {
            child.d01_type = docType;
            child.dep_no = depNo;

            
            //連檔案也要改屬性
            //if (child.d01_son != 2)
            //{
                ChangeFolderType(child.d01_no, depNo, docType);
            //}
            
        }


        
    }
}