<%@ WebHandler Language="C#" Class="PermissionTreeMethod" %>

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

public class PermissionTreeMethod : IHttpHandler,IRequiresSessionState {

    
   

    private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        
        //取Request處理

        String root=context.Request["root"];

        String id = context.Request["id"];
        
        String mode = context.Request["mode"];
        String SessionName = context.Request["session"];
        //String json = context.Request.Params["para"];

       // context.Request.InputStream;
        
        
        logger.Debug(root);

        //取TreeNode
        if (mode == "select")
        {

         

            

            //取ROOT
            
            if (id=="0")
            {


                context.Response.Write(GetLevelOneNode());
                return;
            }
            else
            {

              
                
                
                
                //取每一層
                context.Response.Write(GetChildNodeJson(id));
                return;

                
                
            }
        }

        if (mode == "save") {
            //   





            JObject o = JsonLib.GetRequestJsonObject(context.Request);
            JArray jsonarray = (JArray)o["para"];
            List<KeyValuePair<String, String>> departs = new List<KeyValuePair<string, string>>();
            foreach(JObject jobj in jsonarray){
              KeyValuePair<String,String> key=new KeyValuePair<string,string>((String)jobj["value"],(String)jobj["text"]);

              
              //departs.Add(key);
            }
            
            //context.Session[SessionName]=departs;

            context.Response.Write("Success");
            return;
        }
        
        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }



  
    
    public String GetLevelOneNode(){

       //ArrayList list = new ArrayList();
       //TreeJson node = new TreeJson();
        
        ICollection<JsTreeJson> json=new LinkedList<JsTreeJson>();
        using (Entity.NXEIPEntities model = new Entity.NXEIPEntities())
        {

            //LINQ
            var root = (from d in model.departments where d.dep_parentid == 0 && d.dep_status == "1" select d).First();
            //node.hasChildren = true;
            json.Add(new PermissionTreeJson(root));
        }

        return Newtonsoft.Json.JsonConvert.SerializeObject(json);
    
    }
    
    
    public String GetChildNodeJson(String id){

  

        return Newtonsoft.Json.JsonConvert.SerializeObject(GetChildNode(id));
    }

    //取每一個部門的子部門與所屬的人員
    public ICollection<JsTreeJson> GetChildNode(String id)
    {

        ICollection<JsTreeJson> json = new LinkedList<JsTreeJson>();



        using (NXEIPEntities model = new NXEIPEntities())
        {
            int pid = System.Convert.ToInt32(id);
            //子部門
            var result = (from d in model.departments
                          where d.dep_parentid == pid && d.dep_status == "1"
                          orderby d.dep_order
                          select d);

           
            
            
            
            foreach (var depart in result)
            {
                //TreeJson node = new TreeJson();
                //node.text = depart.dep_name;
                //node.id = depart.dep_no.ToString();
                //node.classes="depart";
                
                
                // 
               


                PermissionTreeJson node = new PermissionTreeJson(depart);
                json.Add(node);
                
                var childPeopleResult = from p in model.people
                               from acc in model.accounts
                                        where p.departments.dep_no == depart.dep_no && p.peo_uid == acc.people.peo_uid && acc.acc_status == "1"
                               select p;



                if (depart.dep_son != "1" && childPeopleResult.Count() == 0)
                {
                    //node.PopulateOnDemand = true;
                    node.state = null;

                }

                
            }



            //所屬人員
            var peopleResult = from p in model.people
                               from acc in model.accounts
                               where p.departments.dep_no == pid && p.peo_uid == acc.people.peo_uid && acc.acc_status == "1"
                               select p;

                    foreach(var peo in peopleResult){
                        //TreeJson node = new TreeJson();
                        //node.text = peo.peo_name;
                        //node.id = peo.peo_uid.ToString();
                        //node.classes = "people";
                        
                        
                        json.Add(new PermissionTreeJson(peo));
                    }

        }

        return json;
    }

    

}

