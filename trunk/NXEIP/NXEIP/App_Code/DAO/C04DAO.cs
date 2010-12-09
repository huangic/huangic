using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Entity;
using System.ComponentModel;

namespace NXEIP.DAO
{
    /// <summary>
    /// 功能名稱：c04
    /// 功能描述：
    /// 撰寫者：Lina
    /// 撰寫時間：2010/11/04
    /// </summary>
    [DataObject(true)]
    public class C04DAO
    {
        public C04DAO()
        {
           
        }
        private NXEIPEntities model = new NXEIPEntities();

        #region 分頁列表使用
        public IQueryable<View_c04> GetAll()
        {
            return (from tb in model.View_c04 orderby tb.dep_order,tb.typ_order,tb.peo_name select tb);
        }

        public IQueryable<View_c04> GetAll(int startRowIndex, int maximumRows)
        {
            return GetAll().Skip(startRowIndex).Take(maximumRows);
        }

        public int GetAllCount()
        {
            return GetAll().Count();
        }
        #endregion

        #region 新增&修改
        public void AddC04(c04 tb)
        {
            model.AddToc04(tb);
        }

        public int Update()
        {
            return model.SaveChanges();
        }
        #endregion

        #region 由[peo_uid]取得[編號]
        /// <summary>
        /// 由[編號]取得[資料]
        /// </summary>
        /// <param name="no">編號</param>
        /// <returns>整筆資料</returns>
        public int GetNoByPeoUid(int peo_uid)
        {
            return (from tb in model.c04 where tb.peo_uid == peo_uid select tb.c04_no).FirstOrDefault();
        }
        #endregion
    }
}