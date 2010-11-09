using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using System.ComponentModel;

namespace NXEIP.DAO
{
    /// <summary>
    /// 功能名稱：c01
    /// 功能描述：
    /// 撰寫者：Lina
    /// 撰寫時間：2010/10/31
    /// </summary>
    [DataObject(true)]
    public class C01DAO
    {
        public C01DAO()
        {
           
        }
        private NXEIPEntities model = new NXEIPEntities();

        #region 分頁列表使用
        public IQueryable<c01> GetAll(int peo_uid)
        {
            return (from tb in model.c01 where tb.peo_uid==peo_uid orderby tb.c01_no select tb);
        }

        public IQueryable<c01> GetAll(int peo_uid, int startRowIndex, int maximumRows)
        {
            return GetAll(peo_uid).Skip(startRowIndex).Take(maximumRows);
        }

        public int GetAllCount(int peo_uid)
        {
            return GetAll(peo_uid).Count();
        }
        #endregion

        #region 新增&修改
        public void AddC01(c01 tb)
        {
            model.AddToc01(tb);
        }

        public int Update()
        {
            return model.SaveChanges();
        }
        #endregion

        #region 由[編號]取得[資料]
        /// <summary>
        /// 由[編號]取得[資料]
        /// </summary>
        /// <param name="no">編號</param>
        /// <returns>整筆資料</returns>
        public c01 GetByC01No(int c01_no)
        {
            return (from tb in model.c01 where tb.c01_no == c01_no select tb).FirstOrDefault();
        }
        #endregion

        #region 由[c01_peouid、peo_uid]取得[資料]
        /// <summary>
        /// 由[c01_peouid、peo_uid]取得[資料]
        /// </summary>
        /// <param name="no">編號</param>
        /// <returns>整筆資料</returns>
        public c01 GetByC01PeoUid(int peo_uid, int c01_peouid)
        {
            return (from tb in model.c01 where tb.peo_uid == peo_uid && tb.c01_peouid == c01_peouid  select tb).FirstOrDefault();
        }
        #endregion

        #region 由[c01_peouid、peo_uid]取得[資料數量]
        /// <summary>
        /// 由[c01_peouid、peo_uid]取得[資料數量]
        /// </summary>
        /// <param name="no">編號</param>
        /// <returns>數量</returns>
        public int GetCountByC01PeoUid(int peo_uid,int c01_peouid)
        {
            return (from tb in model.c01 where tb.peo_uid == peo_uid && tb.c01_peouid == c01_peouid select tb).Count();
        }
        #endregion
    }
}