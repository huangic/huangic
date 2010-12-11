using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using System.ComponentModel;

namespace NXEIP.DAO
{
    /// <summary>
    /// 功能名稱：c02
    /// 功能描述：取得、設定場地資料rooms資料表的內容
    /// 撰寫者：Lina
    /// 撰寫時間：2010/10/25
    /// </summary>
    [DataObject(true)]
    public class CheckerDAO
    {
        public CheckerDAO()
        {
        }
        private NXEIPEntities model = new NXEIPEntities();

        #region 分頁列表使用
        public IQueryable<checker> GetAll()
        {
            return (from tb in model.checker orderby tb.roo_no, tb.che_no select tb);
        }

        public IQueryable<checker> GetAll(int startRowIndex, int maximumRows)
        {
            return GetAll().Skip(startRowIndex).Take(maximumRows);
        }

        public int GetAllCount()
        {
            return GetAll().Count();
        }
        #endregion

        #region 新增&修改
        public void AddChecker(checker tb)
        {
            model.AddTochecker(tb);
        }

        public int Update()
        {
            return model.SaveChanges();
        }
        #endregion

        #region 由[場地編號]取得[資料]
        /// <summary>
        /// 由[場地編號]取得[資料]
        /// </summary>
        /// <param name="roo_no">編號</param>
        /// <returns>整筆資料</returns>
        public checker GetByRoomNo(int roo_no)
        {
            return (from tb in model.checker where tb.roo_no == roo_no select tb).FirstOrDefault();
        }
        #endregion

        #region 由[人員編號]取得[資料]
        /// <summary>
        /// 由[人員編號]取得[資料]
        /// </summary>
        /// <param name="peo_uid">編號</param>
        /// <returns>整筆資料</returns>
        public checker GetByPeoUid(int peo_uid)
        {
            return (from tb in model.checker where tb.che_peouid == peo_uid select tb).FirstOrDefault();
        }
        #endregion

        #region 由[場地編號、人員編號]取得[資料]
        /// <summary>
        /// 由[場地編號、人員編號]取得[資料]
        /// </summary>
        /// <param name="roo_no">場地編號</param>
        /// <param name="peo_uid">人員編號</param>
        /// <returns>整筆資料</returns>
        public int GetCount(int roo_no, int peo_uid)
        {
            return (from tb in model.checker where tb.che_peouid == peo_uid && tb.roo_no == roo_no select tb).Count();
        }
        #endregion
    }
}