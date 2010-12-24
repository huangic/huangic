<%@ WebHandler Language="C#" Class="index_sso" %>

using System;
using System.Web;
using System.Linq;
using System.Web.SessionState;
using NXEIP.DAO;
using Entity;
using System.Web.Configuration;


public class index_sso : IHttpHandler, IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";

        SessionObject sessionObj = new SessionObject();

        //取帳號
        String mode = context.Request["mode"];
        String val = context.Request["val"];

        String login_url = WebConfigurationManager.AppSettings["SSO_LoginUrl"];
        

        if (String.IsNullOrEmpty(mode) && String.IsNullOrEmpty(val)) {




            context.Server.Transfer(login_url);
            
        }
        accounts accData = null;

        if (mode == "1")
        {


            accData = new AccountsDAO().GetByID(val);
        }


        if (mode == "2") {
            accData = new AccountsDAO().GetByPeoIdCard(val);
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
                new OperatesObject().ExecuteLogInLog(loginID, peoData.peo_uid, accData.acc_no,  sessionObj.GetIpAddress(), sessionObj.SessionID);

                //goto index
                context.Server.Transfer("Default.aspx");
            }

            //未啟用
            if (accData.acc_status.Equals("2"))
            {

                context.Server.Transfer(login_url);
                //this.ShowMessage("您的帳號尚未啟用，請洽貴單位系統管理員：，公務電話：");
            }

            //已停用
            if (accData.acc_status.Equals("3"))
            {

                context.Server.Transfer(login_url);
                //this.ShowMessage("您的帳號已被停用，請洽貴單位系統管理員：，公務電話：");
            }

        }
        else
        {
            context.Server.Transfer(login_url);
        }
        
        
        
        
        
        
        
        
        
        context.Response.Write("Hello World");
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}