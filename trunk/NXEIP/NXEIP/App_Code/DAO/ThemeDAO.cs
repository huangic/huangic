using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using System.ComponentModel;

namespace NXEIP.DAO
{
    /// <summary>
    /// 功能名稱：Theme
    /// 功能描述：
    /// 撰寫者：Lina
    /// 撰寫時間：2010/12/20
    /// </summary>
    [DataObject(true)]
    public class ThemeDAO
    {
        public ThemeDAO()
        {
        }

        private NXEIPEntities model = new NXEIPEntities();

        #region 分頁列表使用
        public IQueryable<theme> GetAll(int que_no)
        {
            return (from tb in model.theme where tb.que_no==que_no && tb.the_status == "1" orderby tb.the_order select tb);
        }

        public IQueryable<theme> GetAll(int que_no,int startRowIndex, int maximumRows)
        {
            return GetAll(que_no).Skip(startRowIndex).Take(maximumRows);
        }

        public int GetAllCount(int que_no)
        {
            return GetAll(que_no).Count();
        }
        #endregion

        #region 新增&修改
        public void AddTheme(theme tb)
        {
            model.AddTotheme(tb);
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
        /// <param name="no1">que_no</param>
        /// <param name="no2">the_no</param>
        /// <returns>整筆資料</returns>
        public theme GetByNo(int no1,int no2)
        {
            return (from tb in model.theme where tb.que_no == no1 && tb.the_no==no2 select tb).FirstOrDefault();
        }
        #endregion

        #region 取得該問卷題目編號最大值
        /// <summary>
        /// 由[que_no]取得[the_no]最大值
        /// </summary>
        /// <param name="que_no">que_no</param>
        /// <returns></returns>
        public int GetMaxTheNo(int que_no)
        {
            return (from tb in model.theme where tb.que_no == que_no orderby tb.the_no select tb.the_no).DefaultIfEmpty().Max();
        }
        #endregion
    }
}