using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using NXEIP.SSO;
using NXEIP.DAO;
using Entity;



namespace NXEIP.SSO
{
    /// <summary>
    /// TokenAuth 的摘要描述
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    // 若要允許使用 ASP.NET AJAX 從指令碼呼叫此 Web 服務，請取消註解下一行。
    // [System.Web.Script.Services.ScriptService]
    public class TokenAuth : System.Web.Services.WebService
    {

        public TokenAuth()
        {

            //如果使用設計的元件，請取消註解下行程式碼 
            //InitializeComponent(); 
        }
        /// <summary>
        /// 驗證Token與取回使用者資料
        /// </summary>
        /// <param name="token">token</param>
        /// <param name="sysId">系統編號</param>
        /// <returns></returns>
        [WebMethod]
        public UserData Auth(String token,String sysId)
        {

            //驗證TOKEN

            UserData  u= new UserData();

            loginlogDAO dao = new loginlogDAO();

            loginlog loginData = dao.GetByLogNo(token);


            if (loginData == null)
            {
                u.isAuth = false;
                u.Message = "不合法的TOKEN!";

                return u;
            }


            if (loginData.log_status == "2") {
                u.isAuth = false;
                u.Message = "使用者以登出!";

                return u;
            }


           
                u.isAuth = true;
                u.Message = "驗證成功!";

                //取使用者資料
                using (NXEIPEntities model = new NXEIPEntities()) {

                    var user = (from d in model.accounts
                               from p in model.people
                               where
                               d.peo_uid == p.peo_uid
                               &&
                               d.acc_no == loginData.log_accno
                               select new { account = d, peo = p }).First();


                    u.Account = user.account.acc_login;
                    u.UID = user.peo.peo_idcard;
                    u.eMail = user.peo.peo_email;


                }

                            

            return u;
            //return "Hello World";
        }

    }
}