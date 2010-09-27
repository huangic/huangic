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
            //
            // TODO: 在此加入建構函式的程式碼
            //
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
            if (Application.Context.Request.CurrentExecutionFilePathExtension.Equals(".aspx"))
            {
                 if ((!url.Contains("login")))
                 {
                
                
                    String userID = (String)Application.Context.Session["UserID"];
            
                    if (String.IsNullOrEmpty(userID))
                    {
                     Application.Response.Redirect("~/login.aspx",false);
                     HttpContext.Current.ApplicationInstance.CompleteRequest();
                    }
                }
            }
        }
    }
}
