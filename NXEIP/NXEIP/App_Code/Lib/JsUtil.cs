using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

/// <summary>
/// JsUtil 的摘要描述
/// </summary>
public class JsUtil
{
	public JsUtil()
	{
		//
		// TODO: 在此加入建構函式的程式碼
		//
	}

    /// <summary>
    /// window.location.href;
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public static String GetRedirectJs(String url){
     
        String js="window.location.href='"+url+"'";

        return js;
    }

    public static void  RedirectJs(Page page,String url)
    {

        page.ClientScript.RegisterStartupScript(page.GetType(), "Redirect", GetRedirectJs(url), true);

    }
    
    /// <summary>
    /// 呼叫Alart;
    /// window.location.href;
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="url"></param>
    /// <returns></returns>
    public static String GetAlertAndRedirectJs(String msg, String url) {
        String js = String.Format("alert('{0}');window.location.href='{1}'",msg,url);

        return  js;
    }

    public static void  AlertAndRedirectJs(Page page,String msg, String url) {
        page.ClientScript.RegisterStartupScript(page.GetType(), "Redirect", GetAlertAndRedirectJs(msg,url), true);

    }

    /// <summary>
    /// 呼叫Alart;
    /// self.parent.updateStatus();
    /// window.location.href;
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="url"></param>
    /// <returns></returns>
    public static String GetAlertAndUpdateParentAndRedirectJs(String msg, String url)
    {
        String js = String.Format("alert('{0}');self.parent.updateStatus();window.location.href='{1}'", msg, url);

        return js;
    }
    public static void AlertAndUpdateParentAndRedirectJs(Page page,String msg, String url)
    {
        page.ClientScript.RegisterStartupScript(page.GetType(), "Redirect", GetAlertAndUpdateParentAndRedirectJs(msg, url), true);

    }

}