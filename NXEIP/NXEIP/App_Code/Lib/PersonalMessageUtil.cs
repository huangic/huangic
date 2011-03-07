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
            int mes_no = 0;
            
            MessageDAO dao = new MessageDAO();

            //找詢是否有相同之訊息
            message search = dao.Search(subject, body, link,me);

            if (search == null)
            {
                message d = new message();

                d.mes_subject = subject;
                d.mes_content = body;
                d.mes_link = link;
                d.mes_peouid = 0;
                d.mes_senduid = me;
                d.mes_status = "1";
                d.mes_datetime = DateTime.Now;
                d.mes_type = (sysMsg == true ? "1" : "0") + (email == true ? "1" : "0") + (phone == true ? "1" : "0");

                dao.AddToMessage(d);
                dao.Update();

                mes_no = d.mes_no;

            }
            else
            {
                mes_no = search.mes_no;
            }

            //加入訊息明細
            int max_medno = dao.maxMedNO(mes_no) + 1;
            medetail x = new medetail();
            x.mes_no = mes_no;
            x.med_no = max_medno;
            x.med_peouid = to;
            x.med_status = "1";
            dao.AddToMedetail(x);
            dao.Update();

            //email 通知
            if (email)
            {
                people p = new PeopleDAO().GetByPeoUID(to);

                if (p.peo_email.Length > 0)
                {
                    EMailUtil myamil = new EMailUtil();
                    myamil.SendMail(subject, body, p.peo_email);
                }
            }

            //手機通知
            //if (phone)
            //{

            //}
        }
        catch (Exception ex)
        {
            logger.Debug("Error:{0}", ex.ToString());
        }
    }
}