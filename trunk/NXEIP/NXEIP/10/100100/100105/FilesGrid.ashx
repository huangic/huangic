<%@ WebHandler Language="C#" Class="FilesGrid"%>

using System;
using System.Web;
using System.Collections.Generic;
using Newtonsoft.Json;
using Entity;
using System.Linq;
using System.Web.SessionState;
using NXEIP.FileManager;
using NXEIP.FileManager.Json;

public class FilesGrid : IHttpHandler,IRequiresSessionState
{
    
  
    
    /// <summary>
    /// get child dir
    /// </summary>
    /// <param name="pid"></param>
    /// <returns></returns>
    private JqGridJSON getFiles(int pid,int depNo,string folderType,String field,String order)
    {

        using (NXEIPEntities model = new NXEIPEntities())
        {


            JqGridJSON grid = new JqGridJSON();


            

            //取目錄的所有檔案 (少欄位)
            var files = from f in model.doc01
                        from f2 in model.doc02

                        where
                        f.d01_no == f2.d01_no &&
                        f2.d02_open == "2" &&
                        f.d01_parentid == pid 
                        &&f.d01_type==folderType &&f.dep_no==depNo 
                        && !String.IsNullOrEmpty(f.d01_file)
                        select new { doc1 = f, doc2 = f2 };


            int total = files.Count();





            grid.total = files.Count();

            grid.rows = new List<JqGridItem>();


            //排序

            if (field.Equals("name"))
            {
                if (order.Equals("desc"))
                {
                    files = files.OrderByDescending(o => o.doc1.d01_file);
                }
                else
                {
                    files = files.OrderBy(o => o.doc1.d01_file);
                }

            }


            if (field.Equals("date"))
            {
                if (order.Equals("desc"))
                {
                    files = files.OrderByDescending(o => o.doc2.d02_date);
                }
                else
                {
                    files = files.OrderBy(o => o.doc2.d02_date);
                }

            }

            if (field.Equals("size"))
            {
                if (order.Equals("desc"))
                {
                    files = files.OrderByDescending(o => o.doc2.d02_KB);
                }
                else
                {
                    files = files.OrderBy(o => o.doc2.d02_KB);
                }

            }

            if (field.Equals("type"))
            {
                if (order.Equals("desc"))
                {
                    files = files.OrderByDescending(o => o.doc2.d02_type);
                }
                else
                {
                    files = files.OrderBy(o => o.doc2.d02_type);
                }

            }




            //files.OrderBy(o =>o.doc1.).;



            foreach (var f in files)
            {
                JqGridItem item = FileItem.GetPermissionFileItem(f.doc1, f.doc2,int.Parse(new SessionObject().sessionUserID));

                grid.rows.Add(item);

            }

            return grid;
        }
        }
    
    
    public void ProcessRequest (HttpContext context) {
        
        
       
        SessionObject sessionObj=new SessionObject();
        
        context.Response.ContentType = "text/plain";
        
        //取參數
        //operation=get_children&id=1


        String order = context.Request["sord"];
        String field = context.Request["sidx"];
        
       
        String id = context.Request["id"];
       
        int depid=0; 
        String folderType="";

        try
        {
            depid = int.Parse(context.Request["depid"]);
            folderType = context.Request["folderType"];
        }
        catch { 
        
        }


        int pid = GetParentId(id);
        
        
        



            JqGridJSON file=null;


            if (pid != 0)
            {
                using (NXEIPEntities model = new NXEIPEntities())
                {
                    doc01 parent = (from d in model.doc01 where d.d01_no == pid select d).First();

                    depid = parent.dep_no;
                    folderType = parent.d01_type;
                }
            }
            else {
                if (folderType == "1") {
                    depid = int.Parse(sessionObj.sessionUserDepartID);
                }
            
            }
            
            
            //if(pid)
            

            file = getFiles(pid,depid,folderType,field,order);
            
            //取使用者的目錄結構


                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(file));
            
      
       
    }


    public bool IsReusable
    {
        get
        {
            return false;
        }

    }

    private int GetParentId(String pid)
    {
        //如果可以轉INT 那就不用處理
        int result;

        if (int.TryParse(pid, out result))
        {

            return result;
        }
        else
        {
            String[] value = pid.Split('_');

            return int.Parse(value[1]);
        }


        //無法轉INT 就取_後面的數字

    }
}