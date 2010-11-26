using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using System.Linq.Dynamic;
using System.ComponentModel;

namespace NXEIP.DAO
{
    /// <summary>
    /// _100403 的摘要描述
    /// </summary>
    [DataObject(true)]
    public class _100403DAO
    {
        public _100403DAO()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        private NXEIPEntities model = new NXEIPEntities();

        #region 叫修紀錄
        /// <summary>
        /// 叫修紀錄
        /// </summary>
        /// <param name="type">1:全府 2:單位 3:個人</param>
        /// <param name="sd">起日期</param>
        /// <param name="ed">迄日期</param>
        /// <param name="peo_uid"></param>
        /// <param name="dep_no"></param>
        /// <returns></returns>
        public IQueryable<rep02> GetRep02Data(string type, DateTime sd, DateTime ed, int peo_uid, int dep_no)
        {
            //全部資料
            var data = (from d in model.rep02
                        where d.r02_status != "4" && d.r02_date >= sd && d.r02_date <= ed
                        select d);

            if (type.Equals("3"))
            {
                data = data.Where(o => o.peo_uid == peo_uid);
            }

            if (type.Equals("2"))
            {
                data = data.Where(o => o.r02_depno == dep_no);
            }

            data = data.OrderByDescending(o => o.r02_date);

            return data;
        }

        public IQueryable<rep02> GetRep02Data(string type, DateTime sd, DateTime ed, int peo_uid, int dep_no, int startRowIndex, int maximumRows)
        {
            return GetRep02Data(type, sd, ed, peo_uid, dep_no).Skip(startRowIndex).Take(maximumRows);
        }

        public int GetRep02DataCount(string type, DateTime sd, DateTime ed, int peo_uid, int dep_no)
        {
            return GetRep02Data(type, sd, ed, peo_uid, dep_no).Count();
        }


        #endregion

        #region 叫修紀錄 - 管理
        /// <summary>
        /// 叫修紀錄
        /// </summary>
        /// <param name="r05_no">分類</param>
        /// <param name="sd">起日期</param>
        /// <param name="ed">迄日期</param>
        /// <param name="peo_uid"></param>
        /// <param name="dep_no"></param>
        /// <returns></returns>
        public IQueryable<rep02> GetRep02Data2(int r05_no, DateTime sd, DateTime ed)
        {
            //全部資料
            var data = (from d in model.rep02
                        where d.r02_status != "4" && d.r02_date >= sd && d.r02_date <= ed && d.r05_no == r05_no
                        orderby d.r02_date descending
                        select d);
            return data;
        }

        public IQueryable<rep02> GetRep02Data2(int r05_no, DateTime sd, DateTime ed, int startRowIndex, int maximumRows)
        {
            return GetRep02Data2(r05_no, sd, ed).Skip(startRowIndex).Take(maximumRows);
        }

        public int GetRep02DataCount2(int r05_no, DateTime sd, DateTime ed)
        {
            return GetRep02Data2(r05_no, sd, ed).Count();
        }


        #endregion

        #region 查詢叫修管理者擁有之分類
        public IQueryable<rep05> SearchRep05Root(int peo_uid)
        {
            int[] r05_no = (from d in model.rep01 where d.r01_peouid == peo_uid select d.r05_no).ToArray();

            return (from d in model.rep05 where r05_no.Contains(d.r05_no) && d.r05_status == "1" orderby d.r05_name select d);
        }

        public IQueryable<rep05> SearchRep05Root(int peo_uid, int startRowIndex, int maximumRows)
        {
            return SearchRep05Root(peo_uid).Skip(startRowIndex).Take(maximumRows);
        }

        public int SearchRep05RootCount(int peo_uid)
        {
            return SearchRep05Root(peo_uid).Count();
        }

        //子分類
        public IQueryable<rep06> SearchRep06Parent(int r05_no)
        {
            return (from d in model.rep06
                    where d.r06_status == "1" && d.r05_no == r05_no && d.r06_level == 1
                    orderby d.r06_order
                    select d);
        }

        //子子分類
        public IQueryable<rep06> SearchRep06Son(int r06_no)
        {
            return (from d in model.rep06
                    where d.r06_status == "1" && d.r06_parent == r06_no && d.r06_level == 2
                    orderby d.r06_order
                    select d);
        }


        public IQueryable<rep05> GetRep05Data()
        {
            return (from d in model.rep05 where d.r05_status == "1" orderby d.r05_name select d);
        }

        #endregion

        

        public IQueryable<spot> GetSpot()
        {
            return (from d in model.spot where d.spo_status == "1" && d.spo_function.Substring(3, 1) == "1" select d);
        }

        public string GetSpotName(int spo_no)
        {
            return (from d in model.spot where d.spo_no == spo_no select d.spo_name).FirstOrDefault();
        }

        public IQueryable<floors> GetFloors(int spo_no)
        {
            return (from d in model.floors where d.flo_status == "1" && d.spo_no == spo_no
                    orderby d.flo_order
                    select d);
        }

        public rep02 GetRep02ByNo(int r02_no)
        {
            return (from d in model.rep02 where d.r02_no == r02_no select d).FirstOrDefault();
        }

        public void addToRep02(rep02 d)
        {
            model.rep02.AddObject(d);
        }

        public void UpData()
        {
            model.SaveChanges();
        }

        #region 維修評分

        public rep03 GetRep03ByNo(int r02_no)
        {
            return (from d in model.rep03 where d.r02_no == r02_no select d).FirstOrDefault();
        }

        public int CheckRep03(int r02_no)
        {
            return (from d in model.rep03 where d.r02_no == r02_no select d).Count();
        }

        public int MaxRep03No(int r02_no)
        {
            int max = (from d in model.rep03
                       where d.r02_no == r02_no
                       orderby d.r03_no descending
                       select d.r03_no).FirstOrDefault();
            return max;
            
        }

        public void addToRep03(rep03 d)
        {
            model.rep03.AddObject(d);
        }

        #endregion

    }
}