using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using Entity;

namespace NXEIP.DAO
{
    /// <summary>
    /// AccountsDAO 的摘要描述
    /// </summary>
    public class AccountsDAO
    {
        public AccountsDAO()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        private NXEIPEntities model = new NXEIPEntities();

        public accounts GetByPeoUID(int peo_uid)
        {
            return (from data in model.accounts where data.peo_uid == peo_uid select data).FirstOrDefault();
        }

        /// <summary>
        /// 驗證帳號密碼
        /// </summary>
        /// <param name="login"></param>
        /// <param name="pw"></param>
        /// <returns></returns>
        public accounts GetByIDPW(string login, string pw)
        {
            return (from d in model.accounts where d.acc_login == login && d.acc_passwd == pw select d).FirstOrDefault();
        }

        public void Addaccounts(accounts accounts)
        {
            model.AddToaccounts(accounts);
        }

        public int Update()
        {
            return model.SaveChanges();
        }
    }
}