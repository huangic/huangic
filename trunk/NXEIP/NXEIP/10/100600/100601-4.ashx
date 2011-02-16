<%@ WebHandler Language="C#" Class="_100601_4" %>

using System;
using System.Web;
using Entity;
using System.Linq;
using System.IO;
using System.Web.SessionState;

public class _100601_4 : IHttpHandler, IRequiresSessionState
{
    private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
    
    public void ProcessRequest (HttpContext context) 
    {
        string type = context.Request.QueryString["type"];
        int mee_no = int.Parse(context.Request.QueryString["mee_no"]);

        //讀取DB
        using (NXEIPEntities model = new NXEIPEntities())
        {
            try
            {
                string filename = string.Empty;
                string filepath = string.Empty;
                
                //取檔案資訊
                if (type == "1")
                {
                    //會前資料
                    int hui_no = int.Parse(context.Request.QueryString["hui_no"]);
                    var file = (from d in model.huiyi where d.mee_no == mee_no && d.hui_no == hui_no select d).First();
                    filename = file.hui_file;
                    filepath = file.hui_path;
                }
                else
                {
                    //會議紀錄
                    int con_no = int.Parse(context.Request.QueryString["con_no"]);
                    var file = (from d in model.conferen where d.mee_no == mee_no && d.con_no == con_no select d).First();
                    filename = file.con_file;
                    filepath = file.con_path;
                }
                
                context.Response.Buffer = true;
                context.Response.Clear();
                context.Response.ContentType = "application/download";
                context.Response.AddHeader("Content-Disposition", "attachment;   filename=" + HttpUtility.UrlEncode(filename, System.Text.Encoding.UTF8) + ";");

                //取資料庫的檔案根目錄參數
                //取上傳目錄
                ArgumentsObject args = new ArgumentsObject();

                string path = args.Get_argValue("100601_dir");

                string fileabspath = path + filepath;

                if (string.IsNullOrEmpty(path))
                {
                    fileabspath = context.Server.MapPath(filepath);
                }

                //輸出檔案
                context.Response.BinaryWrite(File.ReadAllBytes(fileabspath));
                context.Response.Flush();
                context.Response.End();
            }
            catch (Exception ex)
            {
                logger.Debug(ex.Message);
            }
        }
        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}