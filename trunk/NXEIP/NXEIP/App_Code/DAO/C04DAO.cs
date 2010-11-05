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
    }
}