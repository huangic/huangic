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
    /// Rep07DAO 的摘要描述
    /// </summary>
    [DataObject(true)]
    public class Rep07DAO
    {
        public Rep07DAO()
        {

        }

        private NXEIPEntities model = new NXEIPEntities();

        /// <summary>
        /// 取得使用者所管理維修所在地-場地資料
        /// </summary>
        /// <param name="peo_uid"></param>
        /// <returns></returns>
        public IQueryable<spot> Get_spotData(int peo_uid)
        {
            var data = (from d in model.rep01
                        where d.r01_peouid == peo_uid
                        from r in model.rep07
                        where r.r01_no == d.r01_no && r.r05_no == d.r05_no
                        from s in model.spot
                        where s.spo_no == r.r07_spono
                        select s);
            return data;

        }

        /// <summary>
        /// 取得使用者所管理維修所在地
        /// </summary>
        /// <param name="peo_uid"></param>
        /// <returns></returns>
        public IQueryable<rep07> Get_rep07Data(int peo_uid)
        {

            var data = (from d in model.rep01
                        where d.r01_peouid == peo_uid
                        from r in model.rep07
                        where r.r01_no == d.r01_no && r.r05_no == d.r05_no
                        select r);
            return data;
        }



        public void AddRep07(rep07 d)
        {
            model.rep07.AddObject(d);
        }

        public void DeleteRep07(rep07 d)
        {
            model.rep07.DeleteObject(d);
        }

        public void Update()
        {
            model.SaveChanges();
        }

    }
}