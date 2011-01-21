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
        //ChangeObject changeobj = new ChangeObject();

        #region 抓出來的結構
        /*public class NewPetition
        {
            public int pet_no { get; set; }
            public int spo_no { get; set; }
            public string spo_name { get; set; }
            public int roo_no { get; set; }
            public string roo_name { get; set; }
            public int pet_depno { get; set; }
            public int pet_applyuid { get; set; }
            public DateTime pet_stime { get; set; }
            public DateTime pet_etime { get; set; }
            public string pet_host { get; set; }
            public int pet_count { get; set; }
            public string pet_reason { get; set; }
            public string pet_apply { get; set; }
            public string stet
            {
                get
                {
                    if (pet_stime != null && pet_etime != null)
                    {
                        string st = new ChangeObject().ADDTtoROCDT(pet_stime.ToString("yyyy-MM-dd HH:mm")) + "~" + pet_etime.ToString("HH:mm");
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

        }*/
        #endregion

        #region 分頁列表使用
        /*
        public IQueryable<NewPetition> GetAll(string sdate, string edate, string status, int spots1, int rooms1, int loginuser)
        {
            if (sdate != null && edate != null && status != null)
            {
                DateTime sd = Convert.ToDateTime(sdate + " 00:00:00");
                DateTime ed = Convert.ToDateTime(edate + " 23:59:59");
                if (status.Equals("0"))
                {
                    #region 當申請狀態選全部時
                    if (rooms1 > 0)
                    {
                        #region 查某個場地
                        var itemColl = from pet in model.petition
                                       join room in model.rooms on pet.roo_no equals room.roo_no
                                       join spo in model.spot on room.spo_no equals spo.spo_no
                                       join checkertb in model.checker on room.roo_no equals checkertb.roo_no
                                       where pet.pet_stime >= sd && pet.pet_etime <= ed && (pet.pet_apply == "1" || pet.pet_apply == "2") && pet.roo_no == rooms1 && checkertb.che_peouid == loginuser
                                       orderby pet.pet_stime descending, pet.pet_etime descending
                                       select new NewPetition
                                       {
                                           pet_no = pet.pet_no,
                                           spo_no = spo.spo_no,
                                           spo_name = spo.spo_name,
                                           roo_no = room.roo_no,
                                           roo_name = room.roo_name,
                                           pet_depno = pet.pet_depno.Value,
                                           pet_applyuid = pet.pet_applyuid.Value,
                                           pet_stime = pet.pet_stime.Value,
                                           pet_etime = pet.pet_etime.Value,
                                           pet_host = pet.pet_host,
                                           pet_count = pet.pet_count.Value,
                                           pet_reason = pet.pet_reason,
                                           pet_apply = pet.pet_apply
                                       };
                        return itemColl;
                        #endregion
                    }
                    else if (spots1 > 0)
                    {
                        #region 查某個所在地
                        var itemColl = from pet in model.petition
                                       join room in model.rooms on pet.roo_no equals room.roo_no
                                       join spo in model.spot on room.spo_no equals spo.spo_no
                                       join checkertb in model.checker on room.roo_no equals checkertb.roo_no
                                       where pet.pet_stime >= sd && pet.pet_etime <= ed && (pet.pet_apply == "1" || pet.pet_apply == "2") && room.spo_no == spots1 && checkertb.che_peouid == loginuser
                                       orderby pet.pet_stime descending, pet.pet_etime descending
                                       select new NewPetition
                                       {
                                           pet_no = pet.pet_no,
                                           spo_no = spo.spo_no,
                                           spo_name = spo.spo_name,
                                           roo_no = room.roo_no,
                                           roo_name = room.roo_name,
                                           pet_depno = pet.pet_depno.Value,
                                           pet_applyuid = pet.pet_applyuid.Value,
                                           pet_stime = pet.pet_stime.Value,
                                           pet_etime = pet.pet_etime.Value,
                                           pet_host = pet.pet_host,
                                           pet_count = pet.pet_count.Value,
                                           pet_reason = pet.pet_reason,
                                           pet_apply = pet.pet_apply
                                       };
                        return itemColl;
                        #endregion
                    }
                    else
                    {
                        #region 查全部
                        var itemColl = from pet in model.petition
                                       join room in model.rooms on pet.roo_no equals room.roo_no
                                       join spo in model.spot on room.spo_no equals spo.spo_no
                                       join checkertb in model.checker on room.roo_no equals checkertb.roo_no
                                       where pet.pet_stime >= sd && pet.pet_etime <= ed && (pet.pet_apply == "1" || pet.pet_apply == "2") && checkertb.che_peouid == loginuser
                                       orderby pet.pet_stime descending, pet.pet_etime descending
                                       select new NewPetition
                                       {
                                           pet_no = pet.pet_no,
                                           spo_no = spo.spo_no,
                                           spo_name = spo.spo_name,
                                           roo_no = room.roo_no,
                                           roo_name = room.roo_name,
                                           pet_depno = pet.pet_depno.Value,
                                           pet_applyuid = pet.pet_applyuid.Value,
                                           pet_stime = pet.pet_stime.Value,
                                           pet_etime = pet.pet_etime.Value,
                                           pet_host = pet.pet_host,
                                           pet_count = pet.pet_count.Value,
                                           pet_reason = pet.pet_reason,
                                           pet_apply = pet.pet_apply
                                       };
                        return itemColl;
                        #endregion
                    }
                    #endregion
                }
                else
                {
                    #region 當申請狀態選非全部時
                    if (rooms1 > 0)
                    {
                        #region 查某個場地
                        var itemColl = from pet in model.petition
                                       join room in model.rooms on pet.roo_no equals room.roo_no
                                       join spo in model.spot on room.spo_no equals spo.spo_no
                                       join checkertb in model.checker on room.roo_no equals checkertb.roo_no
                                       where pet.pet_stime >= sd && pet.pet_etime <= ed && pet.pet_apply == status && pet.roo_no == rooms1 && checkertb.che_peouid == loginuser
                                       orderby pet.pet_stime descending, pet.pet_etime descending
                                       select new NewPetition
                                       {
                                           pet_no = pet.pet_no,
                                           spo_no = spo.spo_no,
                                           spo_name = spo.spo_name,
                                           roo_no = room.roo_no,
                                           roo_name = room.roo_name,
                                           pet_depno = pet.pet_depno.Value,
                                           pet_applyuid = pet.pet_applyuid.Value,
                                           pet_stime = pet.pet_stime.Value,
                                           pet_etime = pet.pet_etime.Value,
                                           pet_host = pet.pet_host,
                                           pet_count = pet.pet_count.Value,
                                           pet_reason = pet.pet_reason,
                                           pet_apply = pet.pet_apply
                                       };
                        return itemColl;
                        #endregion
                    }
                    else if (spots1 > 0)
                    {
                        #region 查某個所在地
                        var itemColl = from pet in model.petition
                                       join room in model.rooms on pet.roo_no equals room.roo_no
                                       join spo in model.spot on room.spo_no equals spo.spo_no
                                       join checkertb in model.checker on room.roo_no equals checkertb.roo_no
                                       where pet.pet_stime >= sd && pet.pet_etime <= ed && pet.pet_apply == status && room.spo_no == spots1 && checkertb.che_peouid == loginuser
                                       orderby pet.pet_stime descending, pet.pet_etime descending
                                       select new NewPetition
                                       {
                                           pet_no = pet.pet_no,
                                           spo_no = spo.spo_no,
                                           spo_name = spo.spo_name,
                                           roo_no = room.roo_no,
                                           roo_name = room.roo_name,
                                           pet_depno = pet.pet_depno.Value,
                                           pet_applyuid = pet.pet_applyuid.Value,
                                           pet_stime = pet.pet_stime.Value,
                                           pet_etime = pet.pet_etime.Value,
                                           pet_host = pet.pet_host,
                                           pet_count = pet.pet_count.Value,
                                           pet_reason = pet.pet_reason,
                                           pet_apply = pet.pet_apply
                                       };
                        return itemColl;
                        #endregion
                    }
                    else
                    {
                        #region 查全部
                        var itemColl = from pet in model.petition
                                       join room in model.rooms on pet.roo_no equals room.roo_no
                                       join spo in model.spot on room.spo_no equals spo.spo_no
                                       join checkertb in model.checker on room.roo_no equals checkertb.roo_no
                                       where pet.pet_stime >= sd && pet.pet_etime <= ed && pet.pet_apply == status && checkertb.che_peouid == loginuser
                                       orderby pet.pet_stime descending, pet.pet_etime descending
                                       select new NewPetition
                                       {
                                           pet_no = pet.pet_no,
                                           spo_no = spo.spo_no,
                                           spo_name = spo.spo_name,
                                           roo_no = room.roo_no,
                                           roo_name = room.roo_name,
                                           pet_depno = pet.pet_depno.Value,
                                           pet_applyuid = pet.pet_applyuid.Value,
                                           pet_stime = pet.pet_stime.Value,
                                           pet_etime = pet.pet_etime.Value,
                                           pet_host = pet.pet_host,
                                           pet_count = pet.pet_count.Value,
                                           pet_reason = pet.pet_reason,
                                           pet_apply = pet.pet_apply
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
                var itemColl = from pet in model.petition
                               join room in model.rooms on pet.roo_no equals room.roo_no
                               join spo in model.spot on room.spo_no equals spo.spo_no
                               join checkertb in model.checker on room.roo_no equals checkertb.roo_no
                               where checkertb.che_peouid == loginuser
                               orderby pet.pet_stime descending, pet.pet_etime descending
                               select new NewPetition
                               {
                                   pet_no = pet.pet_no,
                                   spo_no = spo.spo_no,
                                   spo_name = spo.spo_name,
                                   roo_no = room.roo_no,
                                   roo_name = room.roo_name,
                                   pet_depno = pet.pet_depno.Value,
                                   pet_applyuid = pet.pet_applyuid.Value,
                                   pet_stime = pet.pet_stime.Value,
                                   pet_etime = pet.pet_etime.Value,
                                   pet_host = pet.pet_host,
                                   pet_count = pet.pet_count.Value,
                                   pet_reason = pet.pet_reason,
                                   pet_apply = pet.pet_apply
                               };
                return itemColl;
                #endregion
            }
        }
        public IQueryable<NewPetition> GetAll(string sdate, string edate, string status, int spots1, int rooms1, int loginuser, int startRowIndex, int maximumRows)
        {
            return GetAll(sdate, edate, status, spots1, rooms1, loginuser).Skip(startRowIndex).Take(maximumRows);
        }

        public int GetAllCount(string sdate, string edate, string status, int spots1, int rooms1, int loginuser)
        {
            return GetAll(sdate, edate, status, spots1, rooms1, loginuser).Count();
        }*/
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
