using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using NXEIP.DAO;
using NLog;

/// <summary>
/// PersonalMessageUtil 的摘要描述
/// </summary>
public class PersonalMessageUtil
{
    private static Logger logger = LogManager.GetCurrentClassLogger();

	public PersonalMessageUtil()
	{
		//
		// TODO: 在此加入建構函式的程式碼
		//
	}

    /// <summary>
    /// 傳送個人訊息
    /// </summary>
    /// <param name="subject">主旨</param>
    /// <param name="body">訊息內容</param>
    /// <param name="link">連結</param>
    /// <param name="to">送給誰(peo_uid)</param>
    /// <param name="me">發送人(peo_uid)</param>
    /// <param name="sysMsg">系統內部發送</param>
    /// <param name="email">使用email發送</param>
    /// <param name="phone">使用手機發送</param>
    public void SendMessage(string subject, string body, string link, int to, int me,bool sysMsg, bool email,bool phone)
    {
        try
        {
            message d = new message();

            d.mes_subject = subject;
            d.mes_content = body;
            d.mes_link = link;
            d.mes_peouid = to;
            d.mes_senduid = me;
            d.mes_status = "1";
            d.mes_datetime = DateTime.Now;
            d.mes_type = (sysMsg == true ? "1" : "0") + (email == true ? "1" : "0") + (phone == true ? "1" : "0");

            MessageDAO dao = new MessageDAO();
            dao.AddToMessage(d);
            dao.Update();

            //email 通知
            if (email)
            {

            }

            //手機通知
            //if (phone)
            //{

            //}
        }
        catch (Exception ex)
        {
            logger.Debug("Error:{0}", ex.Message);
        }
    }
}