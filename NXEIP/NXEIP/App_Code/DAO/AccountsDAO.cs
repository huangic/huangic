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