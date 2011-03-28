using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using System.ComponentModel;

namespace NXEIP.DAO
{
    /// <summary>
    /// 功能名稱：Dispatch
    /// 功能描述：
    /// 撰寫者：Lina
    /// 撰寫時間：2011/03/28
    /// </summary>
    [DataObject(true)]
    public class DispatchDAO
    {
        public DispatchDAO()
        {
        }
        private NXEIPEntities model = new NXEIPEntities();

        #region 分頁列表使用
        public IQueryable<dispatch> GetAll()
        {
            return (from tb in model.dispatch where tb.dis_status == "1" orderby tb.dis_outdate, tb.dis_outpeouid, tb.dis_name select tb);
        }

        public IQueryable<dispatch> GetAll(int startRowIndex, int maximumRows)
        {
            return GetAll().Skip(startRowIndex).Take(maximumRows);
        }

        public int GetAllCount()
        {
            return GetAll().Count();
        }
        #endregion

        #region 新增&修改
        public void AddDispatch(dispatch tb)
        {
            model.AddTodispatch(tb);
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
        public dispatch GetByNo(int no)
        {
            return (from tb in model.dispatch where tb.dis_no == no select tb).FirstOrDefault();
        }
        #endregion
    }
}