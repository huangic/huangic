using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using System.ComponentModel;
using System.Linq.Dynamic;
using System.Data.Objects.SqlClient;

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

        /// <summary>
        /// 取得某功能之所有類別
        /// </summary>
        /// <param name="sfu_no"></param>
        /// <returns></returns>
        public IQueryable<sys06> GetS06FromSufNOAll(int sfu_no)
        {
            return (from d in model.sys06 where d.sfu_no == sfu_no && d.s06_status == "1" select d);
        }

        /// <summary>
        /// 取得某功能之所有類別(level = 1)
        /// </summary>
        /// <param name="suf_no"></param>
        /// <returns></returns>
        public IQueryable<sys06> GetS06FromSufNO(int suf_no) { 
            return (from d in model.sys06 where d.s06_level==1 && d.sfu_no==suf_no && d.s06_status=="1" select d);
        }
        
        public IQueryable<sys06> GetS06FromParentS06(int s06_no)
        {
            if (s06_no != 0) { 
            
            return (from d in model.sys06 where d.s06_parent==s06_no && d.s06_status == "1" select d);
            }
            return null;
        }

        public IQueryable<sys06> GetS06_Level1()
        {
            return (from d in model.sys06 where d.s06_level == 1 && d.s06_status == "1" select d);
        }

        public IQueryable<sys06> GetS06_Level2(int s06_no)
        {
            if (s06_no != 0)
            {
                return (from d in model.sys06 where d.s06_parent == s06_no && d.s06_level == 2 && d.s06_status == "1" select d);
            }
            return null;
        }

        public IQueryable<sys06> Get_Data_By_NO(string s06no)
        {
            string[] s06_no = s06no.Split(',').ToArray();
            return (from d in model.sys06 where s06_no.Contains(SqlFunctions.StringConvert((double)d.s06_no)) select d);
        }
    }

}