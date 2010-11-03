using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using System.ComponentModel;

namespace NXEIP.DAO
{
    /// <summary>
    /// 功能名稱：LINQ spot
    /// 功能描述：取得、設定所在地spot資料表的內容
    /// 撰寫者：Lina
    /// 撰寫時間：2010/09/17
    /// </summary>
    [DataObject(true)]
    public class SpotDAO
    {
        public SpotDAO()
        {
           
        }

        private NXEIPEntities model = new NXEIPEntities();

        #region 分頁列表使用
        public IQueryable<spot> GetAll()
        {
            return (from tb in model.spot where tb.spo_status == "1" orderby tb.spo_no select tb);
            //return (from tb1 in model.spot join tb2 in model.people on tb1.spo_createuid equals tb2.peo_uid
            //        where tb1.spo_status == "1"
            //        select tb1);
        }

        public IQueryable<spot> GetAll(int startRowIndex, int maximumRows)
        {
            return GetAll().Skip(startRowIndex).Take(maximumRows);
        }

        public int GetAllCount()
        {
            return GetAll().Count();
        }
        #endregion

        #region 新增&修改
        public void AddSpot(spot spot)
        {
            model.AddTospot(spot);
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
        public spot GetBySpotNo(int no)
        {
            return (from tb in model.spot where tb.spo_no == no select tb).FirstOrDefault();
        }
        #endregion

        #region 由[編號]取得[所在地]
        /// <summary>
        /// 由[編號]取得[所在地]
        /// </summary>
        /// <param name="no">編號</param>
        /// <returns>所在地</returns>
        public string GetNameBySpoNo(int no)
        {
            return (from tb in model.spot where tb.spo_no == no select tb.spo_name).FirstOrDefault();
        }
        #endregion

        #region 由[所在地]取得[編號]
        /// <summary>
        /// 由[所在地]取得[編號]
        /// </summary>
        /// <param name="name">所在地</param>
        /// <returns>編號</returns>
        public int GetNoByName(string name)
        {
            return (from tb in model.spot where tb.spo_name == name select tb.spo_no).FirstOrDefault();
        }
        #endregion

    }
}