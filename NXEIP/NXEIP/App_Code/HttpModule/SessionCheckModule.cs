using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using Entity;
using tw.gov.tainan.login;
using NXEIP.DAO;

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

                String userID = (String)Application.Context.Session["UserID"];

                if (String.IsNullOrEmpty(userID))
                {

                    #region 驗證TOKEN 這邊是作為由其他系統代登入

                    //取 欄位驗證
                    CheckSSO(Application.Context.Request, Application.Context.Response);

                    #endregion
                }
                
                if ((!url.Contains("login"))&&(!url.Contains("index")))
                 {
                
                
                    userID = (String)Application.Context.Session["UserID"];
            



                    if (String.IsNullOrEmpty(userID))
                    {

                       





                        //取AP的網址SSO LOGIN
                     String loginUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["SSO_LoginUrl"];
                     Application.Response.Redirect(loginUrl, false);
                     HttpContext.Current.ApplicationInstance.CompleteRequest();
                    }
                }
            }
            

        }



        private void CheckSSO(HttpRequest Request, HttpResponse Response) {


            SessionObject sessionObj = new SessionObject();
            
            
            String val = Request["acct"];
            String token = Request["token"];

            String login_url = WebConfigurationManager.AppSettings["SSO_LoginUrl"];


            if (String.IsNullOrEmpty(token) && String.IsNullOrEmpty(val))
            {

                return;
                
                //Response.Redirect(login_url);

            }
            accounts accData = null;



            //System.Net.NetworkInformation.Ping pingWS = new System.Net.NetworkInformation.Ping();
            //System.Net.NetworkInformation.PingReply pingreplyWS = pingWS.Send("172.16.99.58", 2000);

            //驗證TOKEN是否有效
            String ws_url = WebConfigurationManager.AppSettings["tw.gov.tainan.login.WS_SSO"];
            try
            {

                using (WS_SSO client = new WS_SSO())
                {

                    //client.
                    client.Url = ws_url;
                    client.Discover();

                    Guid g;
                    Guid.TryParse(token, out g);


                    ACCOUNT t = client.SSO_Auth(g, "eip", val);
                    //t.
                    //判斷WS是否可用不可用就導入一般登入頁
                    //sessionObj.sessionUserID = "Test";

                    if (t.VerifiedResult)
                    {

                        accData = new AccountsDAO().GetByID(val);

                       

                    }
                }

            }
            catch (Exception ex)
            {
                //出錯表示WS不通


                login_url = "login.aspx";
            }






            if (accData != null)
            {
                if (accData.acc_status.Equals("1"))
                {
                    //取回人員資料
                    people peoData = new PeopleDAO().GetByPeoUID(accData.peo_uid);

                    string loginID = Guid.NewGuid().ToString("N");




                    //save session data
                    sessionObj.sessionUserID = peoData.peo_uid.ToString();
                    sessionObj.sessionUserName = peoData.peo_name;
                    sessionObj.sessionUserDepartID = peoData.dep_no.ToString();
                    sessionObj.sessionUserDepartName = new UtilityDAO().Get_DepartmentName(peoData.dep_no);
                    sessionObj.sessionUserAccount = accData.acc_login;
                    sessionObj.sessionLogInID = loginID;


                    //login log
                    new OperatesObject().ExecuteLogInLog(loginID, peoData.peo_uid, accData.acc_no, sessionObj.GetIpAddress(), sessionObj.SessionID);

                    //goto index
                    //Response.Redirect("Default.aspx");
                }

                //未啟用
                if (accData.acc_status.Equals("2"))
                {

                    //Response.Redirect(login_url);
                    //this.ShowMessage("您的帳號尚未啟用，請洽貴單位系統管理員：，公務電話：");
                    return;
                }

                //已停用
                if (accData.acc_status.Equals("3"))
                {

                    //Response.Redirect(login_url);
                    //this.ShowMessage("您的帳號已被停用，請洽貴單位系統管理員：，公務電話：");
                    return;
                }

            }
            else
            {
                //Response.Redirect(login_url);
                return;
            }
        
        }


    }
}
