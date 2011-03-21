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

        public IQueryable<people> GetPeopleOfficials(String keyword)
        {
            //在職
            int typ_no = new UtilityDAO().Get_TypesTypNo("work", "1");
            IQueryable<people> peoples = from d in model.people where d.peo_jobtype == typ_no select d;

            if (!String.IsNullOrEmpty(keyword))
            {
                peoples = peoples.Where(x => x.peo_name.Contains(keyword));
            }

            peoples = peoples.OrderBy(x => x.dep_no);


            return peoples;
        }


        public int GetPeopleOfficialsCount(String keyword) {
            return this.GetPeopleOfficials(keyword).Count();
        }

        public IQueryable<people> GetPeopleOfficials(String keyword, int startRowIndex, int maximumRows) {
            return this.GetPeopleOfficials(keyword).Skip(startRowIndex).Take(maximumRows);
        }




        /// <summary>
        /// 新增人員登入權限
        /// </summary>
        /// <param name="peo_uid"></param>
        /// <param name="dep_no"></param>
        /// <param name="acc_no"></param>
        public void addRoleAccount(int peo_uid,int dep_no,int acc_no)
        {
            DBObject dbo = new DBObject();

            //角色權限 1.部門預設角色 2.系統預設角色
            var defrole = (from d in model.roldefault where d.dep_no == dep_no select d).FirstOrDefault();
            int rol_no = 0;
            if (defrole != null)
            {
                rol_no = defrole.rol_no;
            }
            else
            {
                rol_no = (from d in model.role where d.rol_default == "1" select d.rol_no).FirstOrDefault();

            }
            //尋找 rac_no 最大值
            int rac_no = 1;
            try
            {
                rac_no = (from d in model.roleaccount select d.rac_no).Max() + 1;
            }
            catch { }

            roleaccount data = new roleaccount();

            data.rac_no = rac_no;
            data.rol_no = rol_no;
            data.acc_no = acc_no;

            model.roleaccount.AddObject(data);
            model.SaveChanges();

        }

    }
}