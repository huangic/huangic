using System;
using System.Web.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using tw.gov.tncg.emsg;



namespace NXEIP.MyGov{
/// <summary>
/// eMessageUtil 的摘要描述
/// </summary>
/// 

public class MyMessageUtil
{
	public MyMessageUtil()
	{
		//
		// TODO: 在此加入建構函式的程式碼
		//
	}



    /// <summary>
    /// E公務WebService
    /// </summary>
    /// <param name="subject">主旨</param>
    /// <param name="to">寄給誰</param>
    /// <param name="body">內文</param>
    /// <param name="SData">訊息開始日</param>
    /// <param name="EDate">訊息結束日</param>
    /// <param name="url">本訊息網址</param>
    /// <param name="url_param">網址參數</param>
    /// <param name="eipgroup">訊息種類</param>
    /// <returns></returns>
    public static String send(String subject,String to,String body,DateTime SData,DateTime EDate,String url,String url_param,EIPGroup eipgroup){
        //取參數看是否送出
        bool useWebService=bool.Parse(WebConfigurationManager.AppSettings["UseMyMsgWebService"]);

        if (!useWebService) {
            return "未啟用服務,請設定Web.config";
        }




        /// --- 員工入口網 ---
           //EIP_General, /// 一般訊息
           //EIP_Todo_VerifyAccount, /// 待審核帳號
          //EIP_Todo_VerifyPlace, /// 待審核借用場地
         //EIP_Todo_VerifyNew, /// 待審核最新消息
         //EIP_Todo_TakeMaintain, /// 待維修事項



        Group g=Group.EIP_General;

        if (eipgroup == EIPGroup.EIP_General) {
            g = Group.EIP_General;
        }
        if (eipgroup == EIPGroup.EIP_Todo_TakeMaintain)
        {
            g = Group.EIP_Todo_TakeMaintain;
        }
        if (eipgroup == EIPGroup.EIP_Todo_VerifyNew)
        {
            g = Group.EIP_Todo_VerifyNews;
        }
        if (eipgroup == EIPGroup.EIP_Todo_VerifyPlace)
        {
            g = Group.EIP_Todo_VerifyPlace;
        }
        try
        {
            using (WS_MyMessage MyMessage = new WS_MyMessage())
            {
                Message msg = new Message();
                msg.From = From.EIP;
                msg.Subject = subject;
                msg.To = to;
                msg.Body = body;
                msg.Group = g;


                msg.Category = Category.ToDo;
                msg.MessageDate = SData;
                msg.MessageDueDate = EDate;
                msg.PublishToEmail = false;
                msg.PublishToG2E = true;
                msg.PublishToSMS = false;

                //if (!MyMessage.SendMessage(msg))
                //{
                //    return "發送失敗!";
                //}
                //else
                //{
                //    return String.Empty;
                //}

                return "OK!";
            }
        }
        catch (Exception ex) {
            return ex.Message;
        }
      

    }


    /// <summary>
    /// E公務WebService(訊息只有今天有效)
    /// </summary>
    /// <param name="subject">主旨</param>
    /// <param name="to">寄給誰</param>
    /// <param name="body">內文</param>
    /// <param name="SData">訊息開始日</param>
    /// <param name="EDate">訊息結束日</param>
    /// <param name="url">本訊息網址</param>
    /// <param name="url_param">網址參數</param>
    /// <param name="eipgroup">訊息種類</param>
    /// <returns></returns>
    public static String send(String subject, String to, String body, String url, String url_param, EIPGroup eipgroup) { 
    
    
        return send(subject,to,body,DateTime.Now,DateTime.Now,url,url_param,eipgroup);
    }



}


/// <summary>
/// 訊息種類列舉值
/// </summary>
public enum  EIPGroup{
            /// <summary>
            /// 一般訊息
            /// </summary>
            EIP_General,
            /// <summary>
            /// 待審核帳號
            /// </summary>
    EIP_Todo_VerifyAccount, 
    /// <summary>
    /// 待審核借用場地
    /// </summary>
            EIP_Todo_VerifyPlace,
    /// <summary>
    /// 待審核最新消息
    /// </summary>
            EIP_Todo_VerifyNew, 
    /// <summary>
    /// 待維修事項
    /// </summary>
            EIP_Todo_TakeMaintain, 
    }
}