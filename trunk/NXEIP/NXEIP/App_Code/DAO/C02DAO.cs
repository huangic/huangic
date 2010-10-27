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
    public class C02DAO
    {
        public C02DAO()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }
        private NXEIPEntities model = new NXEIPEntities();

        #region 分頁列表使用
        public IQueryable<c02> GetAll()
        {
            return (from tb in model.c02 orderby tb.c02_sdate, tb.c02_edate select tb);
        }

        public IQueryable<c02> GetAll(int startRowIndex, int maximumRows)
        {
            return GetAll().Skip(startRowIndex).Take(maximumRows);
        }

        public int GetAllCount()
        {
            return GetAll().Count();
        }
        #endregion

        #region 新增&修改
        public void AddC02(c02 tb)
        {
            model.AddToc02(tb);
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
        public c02 GetByC02No(int peo_uid, int c02_no)
        {
            return (from tb in model.c02 where tb.peo_uid == peo_uid && tb.c02_no == c02_no select tb).FirstOrDefault();
        }
        #endregion
        
        /// <summary>
        /// 由人員編號(peo_uid)取得最大c02_no
        /// </summary>
        /// <param name="peo_uid">人員編號</param>
        /// <returns>c02_no</returns>
        public int GetMaxNoByPeoUid(int peo_uid)
        {
            return (from tb in model.c02 where tb.peo_uid == peo_uid select tb.c02_no).DefaultIfEmpty().Max();
        }
    }
}