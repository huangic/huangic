using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="peo_uid"></param>
        /// <returns></returns>
        public String GetPeopleNameByUid(int peo_uid)
        {
            return (from p in model.people
                    where p.peo_uid == peo_uid
                    select p.peo_name).FirstOrDefault();
        }

        public people GetByPeoUID(int peo_uid)
        {
            return (from peopleData in model.people where peopleData.peo_uid == peo_uid select peopleData).FirstOrDefault();
        }

        public people GetByPeoCardDN(string cardDN)
        {
            return (from peopleData in model.people where peopleData.peo_pincode == cardDN select peopleData).FirstOrDefault();
        }

        public void AddPeople(people people)
        {
            model.AddTopeople(people);
        }

        public int Update()
        {
            return model.SaveChanges();
        }
    }
}