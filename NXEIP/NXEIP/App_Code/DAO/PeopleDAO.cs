using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;




namespace NXEIP.DAO
{
    /// <summary>
    /// PeopleDAO 的摘要描述
    /// </summary>
    public class PeopleDAO
    {
        public PeopleDAO()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }


        private NXEIPEntities model = new NXEIPEntities();

        public String GetPeopleNameByUid(int peo_uid)
        {
            return (from p in model.people
                    where p.peo_uid == peo_uid
                    select p.peo_name).FirstOrDefault();
        }

    }
}