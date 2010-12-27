using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// SessionCheckModule 的摘要描述
/// </summary>
/// 
namespace NXEIP.HttpModule
{
    public class SessionCheckModule : IHttpModule
    {
        public SessionCheckModule()
        {
           
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public void Init(HttpApplication context)
        {
            //throw new NotImplementedException();
            context.AcquireRequestState += new EventHandler(Application_AcquireRequestState);

        }

        private void Application_AcquireRequestState(Object source, EventArgs e)
        {
            HttpApplication Application = (HttpApplication)source;

            String url=Application.Context.Request.AppRelativeCurrentExecutionFilePath;


            //TODO:取WebConfig 的略過設定




            //在LIB與PUBLIC內的網頁略過檢查
            if (url.Contains("~/lib")) {
                 return;
            }
             if (url.Contains("~/public")) {
                return;
            }


            if (Application.Context.Request.CurrentExecutionFilePathExtension.Equals(".aspx"))
            {
                 if ((!url.Contains("login"))&&(!url.Contains("index")))
                 {
                
                
                    String userID = (String)Application.Context.Session["UserID"];
            
                    if (String.IsNullOrEmpty(userID))
                    {
                     
                     //取AP的網址SSO LOGIN
                     String loginUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["SSO_LoginUrl"];
                     Application.Response.Redirect("~/login.aspx",false);
                     HttpContext.Current.ApplicationInstance.CompleteRequest();
                    }
                }
            }
            

        }
    }
}
