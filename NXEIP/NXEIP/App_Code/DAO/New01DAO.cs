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
        /// <summary>
        /// 查詢核審資料
        /// </summary>
        /// <param name="dep_no"></param>
        /// <param name="sd"></param>
        /// <param name="ed"></param>
        /// <returns></returns>
        public IQueryable<new01> Get_CheckData(int dep_no, DateTime sd, DateTime? ed)
        {
            var data = (from d in model.new01
                        where d.n01_status != "4" && d.n01_depno == dep_no && d.n01_use == "2"
                        select d);

            if (ed.HasValue)
            {
                data = data.Where(o => o.n01_date >= sd && o.n01_date <= ed);
            }
            else
            {
                data = data.Where(o => o.n01_date >= sd);
            }

            return data.OrderByDescending(o => o.n01_createtime);

        }

        public IQueryable<new01> Get_CheckData(int dep_no, DateTime sd, DateTime? ed, int startRowIndex, int maximumRows)
        {
            return Get_CheckData(dep_no, sd, ed).Skip(startRowIndex).Take(maximumRows);
        }
        public int Get_CheckDataCount(int dep_no, DateTime sd, DateTime? ed)
        {
            return Get_CheckData(dep_no, sd, ed).Count();
        }

        public IQueryable<new01> GetData(string use,string key)
        {
            DateTime today = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));

            var data = from d in model.new01
                       where d.n01_status == "1" && d.n01_use == use
                       && today >= d.n01_sdate && today <= d.n01_edate
                       select d;

            if (!key.Equals("-1"))
            {
                data = data.Where(o => o.n01_subject.Contains(key));
            }

            data = data.OrderByDescending(o => o.n01_sdate).OrderBy(o => o.n01_top);

            return data;
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

                return from d in model.new01
                       where d.n01_peouid == peo_uid && n01_status.Contains(d.n01_status)
                       orderby d.n01_top, d.n01_sdate descending
                       select d;
            }
            else
            {

                return from d in model.new01
                       where d.n01_peouid == peo_uid && d.n01_status == status
                       orderby d.n01_top, d.n01_sdate descending
                       select d;
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
                        orderby d.n01_top, d.n01_sdate descending
                        select d);
            }
            else
            {
                return (from d in model.new01
                        where d.n01_status == "1" && d.s06_no == s06_no && d.n01_subject.Contains(key)
                        orderby d.n01_top, d.n01_sdate descending
                        select d);
            }
        }

        public IQueryable<new01> Get_DataForWidget(string use, string date)
        {
            DateTime today = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            DateTime n01_date = Convert.ToDateTime(date);

            var data = from d in model.new01
                       where d.n01_status == "1" && d.n01_use == use
                       && today >= d.n01_sdate && today <= d.n01_edate && d.n01_date >= n01_date
                       orderby d.n01_top, d.n01_sdate descending
                       select d;

            return data;
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