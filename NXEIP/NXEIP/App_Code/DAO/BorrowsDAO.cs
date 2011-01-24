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
    /// 功能名稱：borrows
    /// 功能描述：取得、設定設備借用申請單
    /// 撰寫者：Lina
    /// 撰寫時間：2011/01/21
    /// </summary>
    [DataObject(true)]
    public class BorrowsDAO
    {
        public BorrowsDAO()
        {

        }
        private NXEIPEntities model = new NXEIPEntities();
        ChangeObject changeobj = new ChangeObject();

        #region 抓出來的結構
        public class NewBorrows
        {
            public int bor_no { get; set; }
            public int spo_no { get; set; }
            public string spo_name { get; set; }
            public int equ_no { get; set; }
            public string equ_name { get; set; }
            public int bor_depno { get; set; }
            public int bor_applyuid { get; set; }
            public DateTime bor_stime { get; set; }
            public DateTime bor_etime { get; set; }
            public string bor_reason { get; set; }
            public string bor_apply { get; set; }
            public string stet
            {
                get
                {
                    if (bor_stime != null && bor_etime != null)
                    {
                        string st = new ChangeObject().ADDTtoROCDT(bor_stime.ToString("yyyy-MM-dd HH:mm")) + "~" + bor_etime.ToString("HH:mm");
                        return st;
                    }
                    else
                        return "";
                }
                set
                {
                    stet = value;
                }
            }

        }
        #endregion

        #region 分頁列表使用
        public IQueryable<NewBorrows> GetAll(string sdate, string edate, string status, int spots1, int equ1, int loginuser)
        {
            if (sdate != null && edate != null && status != null)
            {
                DateTime sd = Convert.ToDateTime(sdate + " 00:00:00");
                DateTime ed = Convert.ToDateTime(edate + " 23:59:59");
                if (status.Equals("0"))
                {
                    #region 當申請狀態選全部時
                    if (equ1 > 0)
                    {
                        #region 查某個場地 
                        var itemColl = from bor in model.borrows
                                       join equ in model.equipments on bor.equ_no equals equ.equ_no
                                       join spo in model.spot on equ.spo_no equals spo.spo_no
                                       where bor.bor_stime >= sd && bor.bor_etime <= ed && (bor.bor_apply == "1" || bor.bor_apply == "2") && bor.equ_no == equ1 && equ.peo_uid == loginuser
                                       orderby bor.bor_stime descending, bor.bor_etime descending
                                       select new NewBorrows
                                       {
                                           bor_no = bor.bor_no,
                                           spo_no = spo.spo_no,
                                           spo_name = spo.spo_name,
                                           equ_no = equ.equ_no,
                                           equ_name = equ.equ_name,
                                           bor_depno = bor.bor_depno.Value,
                                           bor_applyuid = bor.bor_applyuid.Value,
                                           bor_stime = bor.bor_stime.Value,
                                           bor_etime = bor.bor_etime.Value,
                                           bor_reason = bor.bor_reason,
                                           bor_apply = bor.bor_apply
                                       };
                        return itemColl;
                        #endregion
                    }
                    else if (spots1 > 0)
                    {
                        #region 查某個所在地
                        var itemColl = from bor in model.borrows
                                       join equ in model.equipments on bor.equ_no equals equ.equ_no
                                       join spo in model.spot on equ.spo_no equals spo.spo_no
                                       where bor.bor_stime >= sd && bor.bor_etime <= ed && (bor.bor_apply == "1" || bor.bor_apply == "2") && equ.spo_no == spots1 && equ.peo_uid == loginuser
                                       orderby bor.bor_stime descending, bor.bor_etime descending
                                       select new NewBorrows
                                       {
                                           bor_no = bor.bor_no,
                                           spo_no = spo.spo_no,
                                           spo_name = spo.spo_name,
                                           equ_no = equ.equ_no,
                                           equ_name = equ.equ_name,
                                           bor_depno = bor.bor_depno.Value,
                                           bor_applyuid = bor.bor_applyuid.Value,
                                           bor_stime = bor.bor_stime.Value,
                                           bor_etime = bor.bor_etime.Value,
                                           bor_reason = bor.bor_reason,
                                           bor_apply = bor.bor_apply
                                       };
                        return itemColl;
                        #endregion
                    }
                    else
                    {
                        #region 查全部
                        var itemColl = from bor in model.borrows
                                       join equ in model.equipments on bor.equ_no equals equ.equ_no
                                       join spo in model.spot on equ.spo_no equals spo.spo_no
                                       where bor.bor_stime >= sd && bor.bor_etime <= ed && (bor.bor_apply == "1" || bor.bor_apply == "2") && equ.peo_uid == loginuser
                                       orderby bor.bor_stime descending, bor.bor_etime descending
                                       select new NewBorrows
                                       {
                                           bor_no = bor.bor_no,
                                           spo_no = spo.spo_no,
                                           spo_name = spo.spo_name,
                                           equ_no = equ.equ_no,
                                           equ_name = equ.equ_name,
                                           bor_depno = bor.bor_depno.Value,
                                           bor_applyuid = bor.bor_applyuid.Value,
                                           bor_stime = bor.bor_stime.Value,
                                           bor_etime = bor.bor_etime.Value,
                                           bor_reason = bor.bor_reason,
                                           bor_apply = bor.bor_apply
                                       };
                        return itemColl;
                        #endregion
                    }
                    #endregion
                }
                else
                {
                    #region 當申請狀態選非全部時
                    if (equ1 > 0)
                    {
                        #region 查某個場地
                        var itemColl = from bor in model.borrows
                                       join equ in model.equipments on bor.equ_no equals equ.equ_no
                                       join spo in model.spot on equ.spo_no equals spo.spo_no
                                       where bor.bor_stime >= sd && bor.bor_etime <= ed && bor.bor_apply == status && bor.equ_no == equ1 && equ.peo_uid == loginuser
                                       orderby bor.bor_stime descending, bor.bor_etime descending
                                       select new NewBorrows
                                       {
                                           bor_no = bor.bor_no,
                                           spo_no = spo.spo_no,
                                           spo_name = spo.spo_name,
                                           equ_no = equ.equ_no,
                                           equ_name = equ.equ_name,
                                           bor_depno = bor.bor_depno.Value,
                                           bor_applyuid = bor.bor_applyuid.Value,
                                           bor_stime = bor.bor_stime.Value,
                                           bor_etime = bor.bor_etime.Value,
                                           bor_reason = bor.bor_reason,
                                           bor_apply = bor.bor_apply
                                       };
                        return itemColl;
                        #endregion
                    }
                    else if (spots1 > 0)
                    {
                        #region 查某個所在地
                        var itemColl = from bor in model.borrows
                                       join equ in model.equipments on bor.equ_no equals equ.equ_no
                                       join spo in model.spot on equ.spo_no equals spo.spo_no
                                       where bor.bor_stime >= sd && bor.bor_etime <= ed && bor.bor_apply == status && equ.spo_no == spots1 && equ.peo_uid == loginuser
                                       orderby bor.bor_stime descending, bor.bor_etime descending
                                       select new NewBorrows
                                       {
                                           bor_no = bor.bor_no,
                                           spo_no = spo.spo_no,
                                           spo_name = spo.spo_name,
                                           equ_no = equ.equ_no,
                                           equ_name = equ.equ_name,
                                           bor_depno = bor.bor_depno.Value,
                                           bor_applyuid = bor.bor_applyuid.Value,
                                           bor_stime = bor.bor_stime.Value,
                                           bor_etime = bor.bor_etime.Value,
                                           bor_reason = bor.bor_reason,
                                           bor_apply = bor.bor_apply
                                       };
                        return itemColl;
                        #endregion
                    }
                    else
                    {
                        #region 查全部
                        var itemColl = from bor in model.borrows
                                       join equ in model.equipments on bor.equ_no equals equ.equ_no
                                       join spo in model.spot on equ.spo_no equals spo.spo_no
                                       where bor.bor_stime >= sd && bor.bor_etime <= ed && bor.bor_apply == status && equ.peo_uid == loginuser
                                       orderby bor.bor_stime descending, bor.bor_etime descending
                                       select new NewBorrows
                                       {
                                           bor_no = bor.bor_no,
                                           spo_no = spo.spo_no,
                                           spo_name = spo.spo_name,
                                           equ_no = equ.equ_no,
                                           equ_name = equ.equ_name,
                                           bor_depno = bor.bor_depno.Value,
                                           bor_applyuid = bor.bor_applyuid.Value,
                                           bor_stime = bor.bor_stime.Value,
                                           bor_etime = bor.bor_etime.Value,
                                           bor_reason = bor.bor_reason,
                                           bor_apply = bor.bor_apply
                                       };
                        return itemColl;
                        #endregion
                    }
                    #endregion
                }
            }
            else
            {
                #region 查出全部
                var itemColl = from bor in model.borrows
                               join equ in model.equipments on bor.equ_no equals equ.equ_no
                               join spo in model.spot on equ.spo_no equals spo.spo_no
                               where equ.peo_uid == loginuser
                               orderby bor.bor_stime descending, bor.bor_etime descending
                               select new NewBorrows
                               {
                                   bor_no = bor.bor_no,
                                   spo_no = spo.spo_no,
                                   spo_name = spo.spo_name,
                                   equ_no = equ.equ_no,
                                   equ_name = equ.equ_name,
                                   bor_depno = bor.bor_depno.Value,
                                   bor_applyuid = bor.bor_applyuid.Value,
                                   bor_stime = bor.bor_stime.Value,
                                   bor_etime = bor.bor_etime.Value,
                                   bor_reason = bor.bor_reason,
                                   bor_apply = bor.bor_apply
                               };
                return itemColl;
                #endregion
            }
        }
        public IQueryable<NewBorrows> GetAll(string sdate, string edate, string status, int spots1, int equ1, int loginuser, int startRowIndex, int maximumRows)
        {
            return GetAll(sdate, edate, status, spots1, equ1, loginuser).Skip(startRowIndex).Take(maximumRows);
        }

        public int GetAllCount(string sdate, string edate, string status, int spots1, int equ1, int loginuser)
        {
            return GetAll(sdate, edate, status, spots1, equ1, loginuser).Count();
        }
        #endregion

        #region 新增&修改
        public void AddBorrows(borrows rowdata)
        {
            model.AddToborrows(rowdata);
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
        public borrows GetByNo(int no)
        {
            return (from tb in model.borrows where tb.bor_no == no select tb).FirstOrDefault();
        }
        #endregion
    }
}
