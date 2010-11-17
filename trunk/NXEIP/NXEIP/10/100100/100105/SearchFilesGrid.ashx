<%@ WebHandler Language="C#" Class="SearchFilesGrid" %>

using System;
using System.Web;
using System.Collections.Generic;
using Newtonsoft.Json;
using Entity;
using System.Linq;
using System.Web.SessionState;
using NXEIP.FileManager;
using NXEIP.FileManager.Json;

public class SearchFilesGrid : IHttpHandler, IRequiresSessionState
{

    SessionObject sessionObj = new SessionObject();



    


    public void ProcessRequest(HttpContext context)
    {



        SessionObject sessionObj = new SessionObject();

        context.Response.ContentType = "text/plain";

        //取參數
        //operation=get_children&id=1


        String order = context.Request["sord"];
        String field = context.Request["sidx"];


        String name = context.Request["n"];
        String contexts = context.Request["c"];
       


               
            JqGridJSON file = null;
                     
            //if(pid)
            file = getFiles(name, contexts, field, order);

            //取使用者的目錄結構
            OperatesObject.OperatesExecute(100105, 2, String.Format("目錄查詢 條件檔名:{0},內容:{1}", name, contexts));
                

            context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(file));

        

    }


    public bool IsReusable
    {
        get
        {
            return false;
        }

    }


   /// <summary>
   /// getChildFile
   /// </summary>
   /// <param name="pid">人員編號</param>
   /// <param name="folderId">目錄ID</param>
   /// <param name="field">欄位</param>
   /// <param name="order">順序</param>
   /// <returns></returns>
    private JqGridJSON getFiles(string name, string context, String field, String order)
    {
        int peo_uid = int.Parse(sessionObj.sessionUserID);
        using (NXEIPEntities model = new NXEIPEntities())
        {


            JqGridJSON grid = new JqGridJSON();


            var userfiles=from f in model.doc01
                        from f2 in model.doc02
                        where
                        f.d01_no == f2.d01_no
                        && f2.d02_open == "2"
                        && f.d01_type == "1"
                        && !String.IsNullOrEmpty(f.d01_file)
                        && f.d01_file.Contains(name)
                          select new { doc1 = f, doc2 = f2 };
                        
            //取使用者分享的所有檔案
            var publicfiles = from f in model.doc01
                        from f2 in model.doc02
                        from f3 in model.doc03
                        from f5 in model.doc05

                        where
                        f.d01_no == f2.d01_no
                        && f3.d01_no == f.d01_no
                        && f3.d03_no == f5.d03_no
                        && f5.d05_peouid == peo_uid

                        && f2.d02_open == "2"
                            //&& f.d01_parentid == pid 
                        && f.d01_type == "1"
                            //&&f.dep_no==depNo 
                        && !String.IsNullOrEmpty(f.d01_file)
                        && f.d01_file.Contains(name)
                        select new { doc1 = f, doc2 = f2 };

            var files=userfiles;
            
            
            
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
                JqGridItem item = new FileItem(f.doc1, f.doc2);

                grid.rows.Add(item);

            }

            return grid;
        }
    }
    
}