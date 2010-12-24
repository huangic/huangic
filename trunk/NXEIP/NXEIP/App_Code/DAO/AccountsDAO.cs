﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using Entity;
using System.ComponentModel;

namespace NXEIP.DAO
{
    /// <summary>
    /// AccountsDAO 的摘要描述
    /// </summary>
    [DataObject(true)]
    public class AccountsDAO
    {
        public AccountsDAO()
        {
          
        }

        private NXEIPEntities model = new NXEIPEntities();

        public accounts GetByPeoUID(int peo_uid)
        {
            return (from data in model.accounts where data.peo_uid == peo_uid select data).FirstOrDefault();
        }

        public accounts GetByPeoIdCard(string idcard)
        {
            return (from peo in model.people 
                    from data in model.accounts 
                    where 
                    data.peo_uid == peo.peo_uid 
                    && peo.peo_idcard==idcard
                    select data).FirstOrDefault();
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

        public accounts GetByID(string login)
        {
            return (from d in model.accounts where d.acc_login == login select d).FirstOrDefault();
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