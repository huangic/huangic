<%@ WebHandler Language="C#" Class="TreeMethod" %>

using System;
using System.Web;
using System.Linq;

using System.Collections;
using System.Collections.Generic;
using Entity;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Web.SessionState;

public class TreeMethod : IHttpHandler,IRequiresSessionState {

    
    //private static String SessionName{get{return }};

    private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        
        //取Request處理

        String root=context.Request["root"];
        String mode = context.Request["mode"];
        String SessionName = context.Request["session"];
        
        //String json = context.Request.Params["para"];

       // context.Request.InputStream;
        
        
        logger.Debug(root);

        //取TreeNode
        if (mode == "select")
        {

         



            //取ROOT
            
            if (root == "source")
            {


                context.Response.Write(GetLevelOneNode());
                return;
            }
            else
            {
              //取每一層
                context.Response.Write(GetChildNodeJson(root));
                return;

            }
        }

        if (mode == "save") {
            //取REQUEST的JSON 字串
            HttpRequest request = context.Request;
            Stream stream = request.InputStream;
            string json = string.Empty;
            string responseJson = string.Empty;
            if (stream.Length != 0)
            {
                System.IO.StreamReader streamReader = new StreamReader(stream);
                json = streamReader.ReadToEnd();

                logger.Debug(json);
                
                //Person person = JSONHelper.Deserialize<Person>(json);
                //person.FirstName = "FN";
                //person.LastName = "LN";
                //responseJson = JSONHelper.Serialize<Person>(person);
            }

            JObject o = JObject.Parse(json);
            JArray jsonarray = (JArray)o["para"];
            List<KeyValuePair<String, String>> departs = new List<KeyValuePair<string, string>>();
            foreach(JObject jobj in jsonarray){
              KeyValuePair<String,String> key=new KeyValuePair<string,string>((String)jobj["value"],(String)jobj["text"]);

              
              departs.Add(key);
            }
            
            context.Session[SessionName]=departs;

            context.Response.Write("Success");
            return;
        }
        
        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }



    public String GetRootNode() {

       
        TreeJson node = new TreeJson();
        using (Entity.NXEIPEntities model = new Entity.NXEIPEntities())
        {

            //LINQ
            var root = (from d in model.departments where d.dep_parentid == 0 && d.dep_status == "1" select d).First();
            //node.hasChildren = true;
            node.id = root.dep_no.ToString();
            node.text = root.dep_name;
            node.classes = "";
           
           
        }

        return Newtonsoft.Json.JsonConvert.SerializeObject(node);
    }
    
    public String GetLevelOneNode(){

       ArrayList list = new ArrayList();
        TreeJson node = new TreeJson();
        using (Entity.NXEIPEntities model = new Entity.NXEIPEntities())
        {

            //LINQ
            var root = (from d in model.departments where d.dep_parentid == 0 && d.dep_status == "1" select d).First();
            //node.hasChildren = true;
            node.id = root.dep_no.ToString();
            node.text = root.dep_name;
            node.classes = "";
            node.expanded = true;
            node.children = GetChildNode(root.dep_no+"");
            list.Add(node);
        }

        return Newtonsoft.Json.JsonConvert.SerializeObject(node.children);
    
    }
    
    
    public String GetChildNodeJson(String id){

  

        return Newtonsoft.Json.JsonConvert.SerializeObject(GetChildNode(id));
    }


    public ArrayList GetChildNode(String id)
    {

        ArrayList list = new ArrayList();



        using (NXEIPEntities model = new NXEIPEntities())
        {
            int pid = System.Convert.ToInt32(id);

            var result = (from d in model.departments
                          where d.dep_parentid == pid && d.dep_status == "1"
                          orderby d.dep_order
                          select d);



            foreach (var depart in result)
            {
                TreeJson node = new TreeJson();
                node.text = depart.dep_name;
                node.id = depart.dep_no.ToString();
                if (depart.dep_son == "1")
                {
                    //node.PopulateOnDemand = true;
                    node.hasChildren = true;

                }

                list.Add(node);
            }



        }

        return list;
    }

    

}

