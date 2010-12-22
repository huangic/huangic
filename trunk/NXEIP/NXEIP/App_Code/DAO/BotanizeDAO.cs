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