<%@ WebHandler Language="C#" Class="_100204_3" %>

using System;
using System.Web;
using Entity;
using System.Linq;
using System.IO;
using System.Web.SessionState;

public class _100204_3 : IHttpHandler, IRequiresSessionState
{

    private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

    public void ProcessRequest(HttpContext context)
    {
        int n01_no = int.Parse(context.Request.QueryString["n01_no"]);
        int n02_no = int.Parse(context.Request.QueryString["n02_no"]);

        using (NXEIPEntities model = new NXEIPEntities())
        {
            try
            {
                new02 data = (from d in model.new02 where d.n01_no == n01_no && d.n02_no == n02_no select d).FirstOrDefault();

                //SESSION 判斷
                SessionObject sessionObj = new SessionObject();

                //if (String.IsNullOrEmpty(sessionObj.sessionUserID))
                //{
                //    context.Response.StatusCode = 403;
                //    context.Response.Flush();
                //    context.Response.End();
                //}

                //取上傳目錄
                ArgumentsObject args = new ArgumentsObject();

                string path = args.Get_argValue("100204_dir");
                string fileabspath = path + data.n02_path;
                string filename = data.n02_file;
                
                if (string.IsNullOrEmpty(path))
                {
                    fileabspath = context.Server.MapPath(data.n02_path);
                }
                
                context.Response.Buffer = true;
                context.Response.Clear();
                context.Response.ContentType = "application/download";
                context.Response.AddHeader("Content-Disposition", "attachment;   filename=" + HttpUtility.UrlEncode(filename, System.Text.Encoding.UTF8) + ";");
                context.Response.BinaryWrite(File.ReadAllBytes(fileabspath));
                context.Response.Flush();
                context.Response.End();
                
                
            }
            catch(System.Exception ex)
            {
                logger.Debug(ex.Message);
                context.Response.StatusCode = 403;
                context.Response.Flush();
                context.Response.End();
            }
        }

    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}