using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

/// <summary>
/// Global 的摘要描述
/// </summary>
public partial class Global:HttpApplication
{
	public Global()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
   
     void RegisterRouters(RouteCollection routes)
	        {
	            //参数含义:
	            //第一个参数：路由名称--随便自己起
	            //第二个参数：路由规则
	            //第三个参数：该路由规则交给哪一个页面来处理
	            routes.MapPageRoute("FileShare", "Share/{code}", "~/public/100105.aspx");
	            //...当然，您还可以添加更多路由规则
	        }
    
    
    
    void Application_Start(object sender, EventArgs e)
    {
        // 在应用程序启动时运行的代码

        //檔案路由設定

        RegisterRouters(RouteTable.Routes);



    }
    void Application_End(object sender, EventArgs e)
    {
        // 在应用程序关闭时运行的代码
    }
    void Application_Error(object sender, EventArgs e)
    {
        // 在出现未处理的错误时运行的代码
    }
    void Session_Start(object sender, EventArgs e)
    {
        // 在新会话启动时运行的代码
    }
    void Session_End(object sender, EventArgs e)
    {
        // 在会话结束时运行的代码。
        // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
        // InProc 时，才会引发 Session_End 事件。如果会话模式设置为 StateServer
        // 或 SQLServer，则不会引发该事件。
    }

}