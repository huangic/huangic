using System;
using System.Web.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using tw.gov.tncg.emsg;
using Entity;
using NXEIP.DAO;



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
    /// <param name="to">寄給誰(account)</param>
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

        //if (!useWebService) {
        //    return "未啟用服務,請設定Web.config";
        //}


        bool sendWebService = false;
        bool sendMail = false;
        bool sendSMS = false;

        String serviceMsg = "";
        ArgumentsObject args = new ArgumentsObject();



        // --- 員工入口網 ---
          //EIP_General, /// 一般訊息
          //EIP_Todo_VerifyAccount, /// 待審核帳號
          //EIP_Todo_VerifyPlace, /// 待審核借用場地
          //EIP_Todo_VerifyNew, /// 待審核最新消息
          //EIP_Todo_TakeMaintain, /// 待維修事項



        Group g=Group.EIP_General;

        if (eipgroup == EIPGroup.EIP_General) {
            g = Group.EIP_General;
            String val=args.Get_argValue("Message_General");
            SetValue(val, out sendWebService,out  sendMail,out sendSMS);
            



        }

        if (eipgroup == EIPGroup.EIP_Todo_VerifyAccount) {
            g = Group.EIP_Todo_VerifyAccount;
            g = Group.EIP_General;
            String val = args.Get_argValue("Message_Todo_VerifyAccount");
            SetValue(val, out sendWebService, out  sendMail, out sendSMS);
        }

       
        if (eipgroup == EIPGroup.EIP_Todo_VerifyNew)
        {
            g = Group.EIP_Todo_VerifyNews;
            g = Group.EIP_General;
            String val = args.Get_argValue("Message_Todo_VerifyNew");
            SetValue(val, out sendWebService, out  sendMail, out sendSMS);
        }
        if (eipgroup == EIPGroup.EIP_Todo_VerifyPlace)
        {
            g = Group.EIP_Todo_VerifyPlace;
            g = Group.EIP_General;
            String val = args.Get_argValue("Message_VerifyPlace");
            SetValue(val, out sendWebService, out  sendMail, out sendSMS);
        }

        if (eipgroup == EIPGroup.EIP_Todo_TakeMaintain)
        {
            g = Group.EIP_Todo_TakeMaintain;
            g = Group.EIP_General;
            String val = args.Get_argValue("Message_Todo_TakeMaintain");
            SetValue(val, out sendWebService, out  sendMail, out sendSMS);
        }
        
        
        try
        {
            bool sendStatus = false;
          
            
            //if (useWebService)
            {
                using (WS_MyMessage MyMessage = new WS_MyMessage())
                {
                    //MyMessage.Url
                


                    Message msg = new Message();
                    msg.From = From.EIP;
                    msg.Subject = subject;
                    msg.To = to;
                    msg.Body = body;
                    msg.Group = g;


                    msg.Category = Category.ToDo;
                    msg.MessageDate = SData;
                    msg.MessageDueDate = EDate;
                    msg.PublishToEmail = sendMail;
                    msg.PublishToG2E = sendWebService;
                    msg.PublishToSMS = sendSMS;

                    if (! (sendStatus=true))
                    {
                        serviceMsg = "發送失敗!";
                    }


                    //return "OK!";
                }

            }
            //else {
            //    serviceMsg = "未啟用服務,請設定Web.config";
            //}


            //寫入資料庫

            UtilityDAO utilDao=new UtilityDAO();


            notifys ns = new notifys();

            ns.not_no = Guid.NewGuid().ToString("N");
            ns.not_peouid = utilDao.GetPeoUidByAccount(to);
            ns.not_phone = sendSMS ? "1" : "0";
            ns.not_egov = sendWebService ? "1" : "0";
            ns.not_email = sendMail ? "1" : "0";
            ns.not_group = g.ToString();
            ns.not_argument = url_param;
            ns.not_content = body;
            ns.not_link = url;
            ns.not_senduid = int.Parse(new SessionObject().sessionUserID);
            ns.not_subject = subject;
            ns.not_type = "2";
            ns.not_status = sendStatus ? "1" : "0";
            ns.not_sysname = g.ToString();
            ns.not_time = SData;
            ns.not_dateline = EDate;
            

            NXEIPEntities model = new NXEIPEntities();

            model.notifys.AddObject(ns);
            model.SaveChanges();


            return serviceMsg;
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



    public static String send(String subject, int toPeouid, String body, String url, String url_param, EIPGroup eipgroup) { 
        
        String account=new AccountsDAO().GetByPeoUID(toPeouid).acc_login;



        return send(subject, account, body, url, url_param, eipgroup);
    }



    public static String send(String subject, int toPeouid, String body,DateTime SData,DateTime EDate, String url, String url_param, EIPGroup eipgroup)
    {

        String account = new AccountsDAO().GetByPeoUID(toPeouid).acc_login;



        return send(subject, account, body,SData,EDate, url, url_param, eipgroup);
    }



    private static void SetValue(string val, out bool sendWebService,out bool sendMail,out bool sendSMS) {


        sendWebService = false;
        sendMail = false;
        sendSMS = false;


        if (val.Substring(0, 1) == "1")
        {
            sendWebService = true;

        }
        if (val.Substring(1, 1) == "1")
        {
            sendMail = true;
        }
        if (val.Substring(2, 1) == "1")
        {
            sendSMS = true;
        }
    
    
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