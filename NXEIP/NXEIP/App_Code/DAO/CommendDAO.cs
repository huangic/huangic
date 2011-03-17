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
    /// 功能名稱：commend
    /// 功能描述：相關網站
    /// 撰寫者：Lina
    /// 撰寫時間：2011/03/17
    /// </summary>
    [DataObject(true)]
    public class CommendDAO
    {
        public CommendDAO()
        {
        }
        private NXEIPEntities model = new NXEIPEntities();
        ChangeObject changeobj = new ChangeObject();

        #region 抓出來的結構
        public class NewCommends
        {
            public int com_no { get; set; }
            public string com_name { get; set; }
            public string com_www { get; set; }
            public int com_sys06no { get; set; }
            public string s06_name { get; set; }
            public int com_createuid { get; set; }
        }
        #endregion

        #region 分頁列表使用
        public IQueryable<NewCommends> GetAll(int sys06no, string keyword)
        {
            if (sys06no > 0 && keyword != null && keyword.Trim().Length > 0)
            {
                #region 查某分類某網站名稱
                var itemColl = from tb1 in model.commend
                               join tb2 in model.sys06 on tb1.com_sys06no equals tb2.s06_no
                               where tb2.s06_no == sys06no && tb1.com_name.Contains(keyword) && tb2.s06_status == "1" && tb1.com_status == "1"
                               orderby tb2.s06_order ascending, tb1.com_name ascending
                               select new NewCommends
                               {
                                   com_no = tb1.com_no,
                                   com_name = tb1.com_name,
                                   com_www = tb1.com_www,
                                   com_sys06no = tb1.com_sys06no.Value,
                                   s06_name = tb2.s06_name,
                                   com_createuid=tb1.com_createuid.Value
                               };
                return itemColl;
                #endregion
            }
            else if (sys06no > 0)
            {
                #region 查某分類
                var itemColl = from tb1 in model.commend
                               join tb2 in model.sys06 on tb1.com_sys06no equals tb2.s06_no
                               where tb2.s06_no == sys06no && tb2.s06_status == "1" && tb1.com_status == "1"
                               orderby tb2.s06_order ascending, tb1.com_name ascending
                               select new NewCommends
                               {
                                   com_no = tb1.com_no,
                                   com_name = tb1.com_name,
                                   com_www = tb1.com_www,
                                   com_sys06no = tb1.com_sys06no.Value,
                                   s06_name = tb2.s06_name,
                                   com_createuid = tb1.com_createuid.Value
                               };
                return itemColl;
                #endregion
            }
            else if (keyword!=null && keyword.Trim().Length > 0)
            {
                #region 某網站名稱
                var itemColl = from tb1 in model.commend
                               join tb2 in model.sys06 on tb1.com_sys06no equals tb2.s06_no
                               where tb1.com_name.Contains(keyword) && tb2.s06_status == "1" && tb1.com_status == "1"
                               orderby tb2.s06_order ascending, tb1.com_name ascending
                               select new NewCommends
                               {
                                   com_no = tb1.com_no,
                                   com_name = tb1.com_name,
                                   com_www = tb1.com_www,
                                   com_sys06no = tb1.com_sys06no.Value,
                                   s06_name = tb2.s06_name,
                                   com_createuid = tb1.com_createuid.Value
                               };
                return itemColl;
                #endregion
            }
            else
            {
                #region 全部
                var itemColl = from tb1 in model.commend
                               join tb2 in model.sys06 on tb1.com_sys06no equals tb2.s06_no
                               where tb2.s06_status == "1" && tb1.com_status == "1"
                               orderby tb2.s06_order ascending, tb1.com_name ascending
                               select new NewCommends
                               {
                                   com_no = tb1.com_no,
                                   com_name = tb1.com_name,
                                   com_www = tb1.com_www,
                                   com_sys06no = tb1.com_sys06no.Value,
                                   s06_name = tb2.s06_name,
                                   com_createuid = tb1.com_createuid.Value
                               };
                return itemColl;
                #endregion
            }

        }
        public IQueryable<NewCommends> GetAll(int sys06no, string keyword, int startRowIndex, int maximumRows)
        {
            return GetAll(sys06no, keyword).Skip(startRowIndex).Take(maximumRows);
        }

        public int GetAllCount(int sys06no, string keyword)
        {
            return GetAll(sys06no, keyword).Count();
        }
        #endregion

        #region 新增&修改
        public void AddCommend(commend rowdata)
        {
            model.AddTocommend(rowdata);
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
        public commend GetByNo(int no)
        {
            return (from tb in model.commend where tb.com_no == no select tb).FirstOrDefault();
        }
        #endregion

        public int GetUidByNo(int no)
        {
            return (from tb in model.commend where tb.com_no == no select tb.com_createuid).FirstOrDefault().Value;
        }
    }
}