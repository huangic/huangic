using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;
using NLog;

/// <summary>
/// EMailUtil 的摘要描述
/// </summary>
public class EMailUtil
{
    private static Logger logger = LogManager.GetCurrentClassLogger();

	public EMailUtil()
	{
		//
		// TODO: 在此加入建構函式的程式碼
		//
	}

    public void SendMail(string subject, string body, string to)
    {
        string host = System.Configuration.ConfigurationManager.AppSettings["SMTP_Server"].ToString();
        string port = System.Configuration.ConfigurationManager.AppSettings["SMTP_Port"].ToString();
        string ssl = System.Configuration.ConfigurationManager.AppSettings["SMTP_SSL"].ToString();
        string account = System.Configuration.ConfigurationManager.AppSettings["AdminMail"].ToString();
        string passwd = System.Configuration.ConfigurationManager.AppSettings["AdminPass"].ToString();
        string displayname = System.Configuration.ConfigurationManager.AppSettings["WebName"].ToString();

        //smtp 設定
        SmtpClient smtp = new SmtpClient();
        smtp.Host = host;
        smtp.Port = int.Parse(port);
        smtp.Credentials = new System.Net.NetworkCredential(account, passwd);
        if (ssl.Equals("true"))
        {
            smtp.EnableSsl = true;
        }
        else
        {
            smtp.EnableSsl = false;
        }

        //mail內容
        
        MailMessage mail = new MailMessage(new MailAddress(account, displayname,System.Text.Encoding.UTF8), new MailAddress(to));
        mail.Subject = subject;
        mail.SubjectEncoding = System.Text.Encoding.UTF8;
        mail.IsBodyHtml = false;
        mail.Body = body;
        mail.BodyEncoding = System.Text.Encoding.UTF8;

        //發送mail
        try
        {
            smtp.Send(mail);
        }
        catch { }
        //smtp.SendAsync(mail, mail);
        //smtp.SendCompleted += new SendCompletedEventHandler(smtp_SendCompleted);
        smtp.Dispose();

    }

    private void smtp_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
    {
        if (e.Error != null)
        {
            logger.Debug("Error:{0}", e.Error.Message);
        }
    }
}