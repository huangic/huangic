using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using Entity;
using System.ComponentModel;

namespace NXEIP.DAO
{
    /// <summary>
    /// PeopleDAO 的摘要描述
    /// </summary>
    [DataObject(true)]
    public class PeopleDAO
    {
        public PeopleDAO()
        {
            
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

        public IQueryable<people> GetPeopleOfficials(String keyword) {
            IQueryable<people> peoples = from d in model.people where d.peo_jobtype == 1 select d;

            if (!String.IsNullOrEmpty(keyword)) {
                peoples = peoples.Where(x => x.peo_name.Contains(keyword));
            }

            peoples=peoples.OrderBy(x => x.dep_no);


            return peoples;
        }


        public int GetPeopleOfficialsCount(String keyword) {
            return this.GetPeopleOfficials(keyword).Count();
        }

        public IQueryable<people> GetPeopleOfficials(String keyword, int startRowIndex, int maximumRows) {
            return this.GetPeopleOfficials(keyword).Skip(startRowIndex).Take(maximumRows);
        }

    }
}