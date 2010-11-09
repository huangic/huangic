using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using System.ComponentModel;

namespace NXEIP.DAO
{
    /// <summary>
    /// New01DAO 的摘要描述
    /// </summary>
    [DataObject(true)]
    public class New01DAO
    {
        private NXEIPEntities model = new NXEIPEntities();

        public New01DAO()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        public IQueryable<new01> GetData(string use,string key)
        {
            if (!key.Equals("-1"))
            {
                return (from d in model.new01
                        where d.n01_status == "1" && d.n01_use == use && d.n01_subject.Contains(key)
                        orderby d.n01_date descending
                        select d);
            }
            else
            {
                return (from d in model.new01
                        where d.n01_status == "1" && d.n01_use == use
                        orderby d.n01_date descending
                        select d);
            }
        }

        public IQueryable<new01> GetData(string use, string key, int startRowIndex, int maximumRows)
        {
            return GetData(use, key).Skip(startRowIndex).Take(maximumRows);
        }

        public int GetDataCount(string use, string key)
        {
            return GetData(use, key).Count();
        }

        public IQueryable<new01> GetPeoData(int peo_uid,string status)
        {
            if (status == "0")
            {
                string[] n01_status = {"1","2","3" };
                return (from d in model.new01 where d.n01_peouid == peo_uid && n01_status.Contains(d.n01_status) 
                        orderby d.n01_date descending select d);
            }
            else
            {
                return (from d in model.new01 where d.n01_peouid == peo_uid && d.n01_status == status
                        orderby d.n01_date descending select d);
            }
        }

        public IQueryable<new01> GetPeoData(int peo_uid,string status, int startRowIndex, int maximumRows)
        {
            return GetPeoData(peo_uid, status).Skip(startRowIndex).Take(maximumRows);
        }

        public int GetPeoDataCount(int peo_uid,string status)
        {
            return GetPeoData(peo_uid, status).Count();
        }

        public IQueryable<new01> GetDataByS06No(int s06_no,string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return (from d in model.new01
                        where d.n01_status == "1" && d.s06_no == s06_no
                        orderby d.n01_date descending
                        select d);
            }
            else
            {
                return (from d in model.new01
                        where d.n01_status == "1" && d.s06_no == s06_no && d.n01_subject.Contains(key)
                        orderby d.n01_date descending
                        select d);
            }
        }

        public IQueryable<new01> GetDataBySysNo(int s06_no, string key, int startRowIndex, int maximumRows)
        {
            return GetDataByS06No(s06_no, key).Skip(startRowIndex).Take(maximumRows);
        }

        public int GetDataBySysNoCount(int s06_no, string key)
        {
            return GetDataByS06No(s06_no, key).Count();
        }

        public new01 GetByNo(int n01_no)
        {
            return (from d in model.new01 where d.n01_no == n01_no select d).FirstOrDefault();
        }

        public void addnew01(new01 d)
        {
            model.new01.AddObject(d);
        }

        public int SaveChang()
        {
            return model.SaveChanges();
        }
    }
}