using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using System.ComponentModel;

namespace NXEIP.DAO
{
    /// <summary>
    /// 功能名稱：c01
    /// 功能描述：
    /// 撰寫者：Lina
    /// 撰寫時間：2010/10/31
    /// </summary>
    [DataObject(true)]
    public class BotanizeDAO
    {
        public BotanizeDAO()
        {
        }
        private NXEIPEntities model = new NXEIPEntities();

        #region 抓出來的結構
        public class NewBotanize
        {
            public int bot_no { get; set; }
            public string dep_name { get; set; }
            public string pro_name { get; set; }
            public string peo_name { get; set; }
            public DateTime bot_date { get; set; }
            public int dep_order { get; set; }
            public int typ_order { get; set; }
        }
        #endregion

        #region 分頁列表使用
        public IQueryable<NewBotanize> GetAll(int que_no)
        {
            var itemColl = (from tb1 in model.botanize
                            join tb2 in model.people on tb1.peo_uid equals tb2.peo_uid
                            join tb3 in model.departments on tb2.dep_no equals tb3.dep_no
                            join tb4 in model.types on tb2.peo_pfofess equals tb4.typ_no
                            join tb5 in model.casework on tb1.bot_no equals tb5.bot_no
                            where tb5.que_no == que_no 
                            orderby tb3.dep_order ascending, tb4.typ_order ascending, tb2.peo_name ascending
                            select new NewBotanize
                             {
                                 bot_no = tb1.bot_no,
                                 dep_name = tb3.dep_name,
                                 pro_name = tb4.typ_cname,
                                 peo_name = tb2.peo_name,
                                 bot_date = tb1.bot_date.Value,
                                 dep_order = tb3.dep_order.Value,
                                 typ_order = tb4.typ_order.Value
                             }).Distinct().OrderBy(x=>x.peo_name).OrderBy(x=>x.typ_order).OrderBy(x=>x.dep_order);
            return itemColl;
        }
        public IQueryable<NewBotanize> GetAll(int que_no, int startRowIndex, int maximumRows)
        {
            return GetAll(que_no).Skip(startRowIndex).Take(maximumRows);
        }

        public int GetAllCount(int que_no)
        {
            return GetAll(que_no).Count();
        }
        #endregion


        #region 新增&修改
        public void AddBotanize(botanize tb)
        {
            model.AddTobotanize(tb);
        }

        public int Update()
        {
            return model.SaveChanges();
        }
        #endregion

        #region 由[問卷編號]取得[問卷數量]
        /// <summary>
        /// 由[問卷編號]取得[問卷數量]
        /// </summary>
        /// <param name="que_no">編號</param>
        /// <returns>有效問卷數量</returns>
        public int GetCountByQueNo(int que_no)
        {
            return (from bot in model.botanize
                    join cas in model.casework on bot.bot_no equals cas.bot_no
                    where bot.bot_status == "1" && cas.que_no == que_no
                    group bot by bot.bot_no into boted
                    select boted).Count();
        }
        #endregion

        #region 由[問卷編號、人員編號]取得[卷號]
        /// <summary>
        /// 由[問卷編號、人員編號]取得[卷號]
        /// </summary>
        /// <param name="que_no">問卷編號</param>
        /// <param name="peo_uid">人員編號</param>
        /// <returns>卷號</returns>
        public int GetNoByQuePeoNO(int que_no, int peo_uid)
        {
            return (from tb1 in model.botanize
                    join tb2 in model.casework on tb1.bot_no equals tb2.bot_no
                    where tb2.que_no == que_no && tb1.bot_status == "1" && tb1.peo_uid == peo_uid
                    group tb1 by tb1.bot_no into tb1ed
                    select tb1ed.Key).FirstOrDefault();
        }
        #endregion
    }
}