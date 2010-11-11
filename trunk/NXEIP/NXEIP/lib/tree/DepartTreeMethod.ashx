<%@ WebHandler Language="C#" Class="DepartTreeMethod" %>

using System;
using System.Web;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Entity;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Web.SessionState;
using NXEIP.JsTree;
using NXEIP.FileManager.Json;
using NXEIP.DAO;
using NXEIP.Lib;
using NXEIP.Tree;
using Newtonsoft.Json;



/// <summary>
/// 部門樹的HANDLE
/// </summary>
public class DepartTreeMethod : IHttpHandler, IRequiresSessionState
{

    
   

    private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        
        //取Request處理 //取參數

        String root=context.Request["root"];

        int id = 0 ;
        int.TryParse(context.Request["id"],out id);

       
        
        String mode = context.Request["mode"];
        
       
        
        
        
        logger.Debug(root);

        //取TreeNode
        if (mode == "select")
        {

            


            DepartTreeEnum setting = new DepartTreeEnum(context.Request);

           
            
            
            
             //建立部門樹物件

            DepartTreeNode DepartTree=DepartTreeNodeFactory.CreateDepartTreeNode(setting);

            
            
            
            

            //取ROOT
            
            if (id==0)
            {

                context.Response.Write( JsonConvert.SerializeObject(DepartTree.GetLevelOneNode()));
               
            }
            else
            {

                //取每一層
                context.Response.Write(JsonConvert.SerializeObject(DepartTree.GetChildNode(id)));
                return;

            }
        }

        if (mode == "save") { 
            //存檔物件
            //取REQUEST的JSON 字串
            String SessionName = context.Request["session"];
        
            JObject o = JsonLib.GetRequestJsonObject(context.Request);
            JArray jsonarray = (JArray)o["para"];
            List<KeyValuePair<String, String>> departs = new List<KeyValuePair<string, string>>();
            foreach (JObject jobj in jsonarray)
            {
                KeyValuePair<String, String> key = new KeyValuePair<string, string>((String)jobj["value"], (String)jobj["text"]);


                departs.Add(key);
            }

            context.Session[SessionName] = departs;

            context.Response.Write("Success");
            return;
        }


        if (mode == "saveSession") {
            String SessionName = context.Request["session"];
            String ParentSessionName = context.Request["parentSession"];

            context.Session[ParentSessionName] = context.Session[SessionName];
            context.Response.Write("Success");
            return;
        }
        
        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }



  
   
}

