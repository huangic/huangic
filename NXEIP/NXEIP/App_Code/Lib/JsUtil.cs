using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using NLog;

/// <summary>
/// JsUtil 的摘要描述
/// </summary>
public class JsUtil
{
    private static Logger logger = LogManager.GetCurrentClassLogger();
    
    
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

    /// <summary>
    /// redirect to url
    /// </summary>
    /// <param name="page"></param>
    /// <param name="url"></param>
    public static void  RedirectJs(Page page,String url)
    {

        CallRegigterClientScript(page, "Redirect", GetRedirectJs(url));

    }
    
    /// <summary>
    /// 呼叫Alart;
    /// window.location.href;
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="url"></param>
    /// <returns></returns>
    public static String GetAlertAndRedirectJs(String msg, String url) {
        String js = String.Format("alert('{0}');window.location.href='{1}';",msg,url);

        return  js;
    }
    public static void AlertAndRedirectJs(Page page,String msg, String url)
    {
        
        
        //page.ClientScript.RegisterClientScriptBlock(page,page.GetType(), "Redirect", GetAlertAndRedirectJs(msg, url), true);


        CallRegigterClientScript(page, "Redirect", GetAlertAndRedirectJs(msg, url));
    }





    public static String GetAlertJs(String msg)
    {
        String js = String.Format("alert('{0}');", msg);

        return js;
    }
    

    /// <summary>
    /// JS 的ALERT
    /// </summary>
    /// <param name="page"></param>
    /// <param name="msg"></param>
    public static void  AlertJs(Page page,String msg) {
       // ScriptManager.RegisterClientScriptBlock(page,page.GetType(), "Alert", GetAlertJs(msg), true);

        CallRegigterClientScript(page, "Alert", GetAlertJs(msg));
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
    
    
    /// <summary>
    /// JS ALERT AND UPDATE 父代頁面的UpdatePanel
    /// </summary>
    /// <param name="page"></param>
    /// <param name="msg"></param>
    /// <param name="url"></param>
    public static void AlertAndUpdateParentAndRedirectJs(Page page,String msg, String url)
    {
        CallRegigterClientScript(page, "Redirect", GetAlertAndUpdateParentAndRedirectJs(msg, url));

    }

    
    /// <summary>
    /// 直接呼叫傳入的JS
    /// </summary>
    /// <param name="page"></param>
    /// <param name="script"></param>
    public static void CallJs(Page page,String script){
        CallRegigterClientScript(page,"CallScript",script);
    }

    private static void CallRegigterClientScript(Page page, String scriptKey, String script) { 
        //檢查有沒有SCRIPTMANAGER
        bool hasSM = FindSriptManager(page);


        logger.Debug("has ScriptManager?:{0}", hasSM);

        if (hasSM)
        {
            
            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), scriptKey, script, true);
        }
        else {
            page.ClientScript.RegisterClientScriptBlock(page.GetType(), scriptKey, script, true);
        }

    }


    private static bool FindSriptManager(Control control){
        if (control.Controls.Count > 0) {
            foreach (Control c in control.Controls)
            {
                bool isSM;
                isSM=FindSriptManager(c);
                if (isSM) {
                    return isSM;
                }

            }
            return false;
        }

        if (control is ScriptManager)
        {
            return true;
        }
        else {
            return false;
        }
    
    }



    public static String GetUpdateParentJs(String msg)
    {
        String js = String.Format("self.parent.update('{0}');",msg);

        return js;
    }

    /// <summary>
    /// 更新父代的UpdatePanel
    /// </summary>
    /// <param name="page"></param>
    /// <param name="msg"></param>

    public static void UpdateParentJs(Page page, String msg)
    {
        CallRegigterClientScript(page, "Update", GetUpdateParentJs(msg));

    }


}