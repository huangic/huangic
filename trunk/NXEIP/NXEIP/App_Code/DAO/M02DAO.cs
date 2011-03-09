using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Globalization;

namespace NXEIP.DAO
{
    /// <summary>
    /// 功能名稱：m02
    /// 功能描述：取得、設定車輛資料
    /// 撰寫者：Lina
    /// 撰寫時間：2011/03/09
    /// </summary>
    [DataObject(true)]
    public class M02DAO
    {
        public M02DAO()
        {
        }

        private NXEIPEntities model = new NXEIPEntities();

        #region 分頁列表使用
        public IQueryable<m02> GetAll()
        {
            return (from tb in model.m02 where tb.m02_status != "4" orderby tb.m02_number select tb);
        }
        public IQueryable<m02> GetAll(int startRowIndex, int maximumRows)
        {
            return GetAll().Skip(startRowIndex).Take(maximumRows);
        }

        public int GetAllCount()
        {
            return GetAll().Count();
        }
        #endregion

        #region 新增&修改
        public void AddM02(m02 rowdata)
        {
            model.AddTom02(rowdata);
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
        public m02 GetByNo(int no)
        {
            return (from tb in model.m02 where tb.m02_no == no select tb).FirstOrDefault();
        }
        #endregion
    }
}