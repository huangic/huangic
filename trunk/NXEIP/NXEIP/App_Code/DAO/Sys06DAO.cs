using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using System.ComponentModel;
using System.Linq.Dynamic;

namespace NXEIP.DAO
{
    /// <summary>
    /// Sys06 的摘要描述
    /// </summary>
    [DataObject(true)]
    public class Sys06DAO
    {
        public Sys06DAO()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        private NXEIPEntities model = new NXEIPEntities();

        public IQueryable<sys06> GetAll()
        {
            return (from s in model.sys06 
                    where s.s06_status == "1" 
                    orderby s.s06_no select s);
        }

        public IQueryable<sys06> GetAll(int startRowIndex, int maximumRows)
        {
            return GetAll().Skip(startRowIndex).Take(maximumRows);
        }

        public int GetAllCount()
        {
            return GetAll().Count();
        }

        public IQueryable<sys06> GetParentData()
        {
            return (from s in model.sys06
                    where s.s06_status == "1" && s.s06_parent == 0
                    orderby s.s06_no
                    select s);
        }

        public sys06 GetByS06No(int s06_no)
        {
            return (from d in model.sys06 where d.s06_no == s06_no select d).FirstOrDefault();
        }

        public void AddSys(sys06 sys06)
        {
            model.AddTosys06(sys06);
        }

        public int Update()
        {
            return model.SaveChanges();
        }
    }

}