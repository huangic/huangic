using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using System.ComponentModel;

namespace NXEIP.DAO
{
    /// <summary>
    /// 功能名稱：c03
    /// 功能描述：取得、設定場地資料rooms資料表的內容
    /// 撰寫者：Lina
    /// 撰寫時間：2010/10/25
    /// </summary>
    [DataObject(true)]
    public class C03DAO
    {
        
        public C03DAO()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }
        private NXEIPEntities model = new NXEIPEntities();

        #region 分頁列表使用
        public IQueryable<c03> GetAll()
        {
            return (from tb in model.c03 orderby tb.c03_no select tb);
        }

        public IQueryable<c03> GetAll(int startRowIndex, int maximumRows)
        {
            return GetAll().Skip(startRowIndex).Take(maximumRows);
        }

        public int GetAllCount()
        {
            return GetAll().Count();
        }
        #endregion

        #region 新增&修改
        public void AddC03(c03 tb)
        {
            model.AddToc03(tb);
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
        public c03 GetByC03No(int c03_no)
        {
            return (from tb in model.c03 where tb.c03_no == c03_no select tb).FirstOrDefault();
        }
        #endregion
    }
}