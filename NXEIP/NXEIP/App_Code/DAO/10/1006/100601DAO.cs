using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using Entity;
using System.Data.Objects;

namespace NXEIP.DAO
{
    /// <summary>
    /// _100601DAO 的摘要描述
    /// </summary>
    [DataObject(true)]
    public class _100601DAO
    {
        public _100601DAO()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        NXEIPEntities model = new NXEIPEntities();

        public IQueryable<meetings> GetData(string key, DateTime sdate, DateTime edate, string status)
        {
            int peo_uid = int.Parse(new SessionObject().sessionUserID);

            //找出會議連絡人或出席人為自己之資料
            int[] mee_no = (from p in model.attends
                            where p.peo_uid == peo_uid
                            select p.mee_no).ToArray();

            var data = (from d in model.meetings
                        where d.mee_peouid == peo_uid || mee_no.Contains(d.mee_no)
                        select d);

            //日期
            data = data.Where(o => o.mee_sdate >= sdate && o.mee_sdate <= edate);

            //關鍵字
            if (!string.IsNullOrEmpty(key))
            {
                data = data.Where(x => x.mee_reason.Contains(key));
            }

            //會議狀態
            if (status != "0")
            {
                data = data.Where(x => x.mee_status == status);
            }

            data = data.OrderByDescending(x => x.mee_sdate);

            return data;

        }

        public IQueryable<meetings> GetData(string key, DateTime sdate, DateTime edate, string status, int startRowIndex, int maximumRows)
        {
            return GetData(key, sdate, edate,status).Skip(startRowIndex).Take(maximumRows);
        }

        public int GetDataCount(string key, DateTime sdate, DateTime edate, string status)
        {
            return GetData(key, sdate, edate,status).Count();
        }

        public void AddToMeetings(meetings d)
        {
            model.meetings.AddObject(d);
        }

        public void DelToMeetings(int mee_no)
        {
            meetings m = (from d in model.meetings where d.mee_no == mee_no select d).FirstOrDefault();
            m.mee_status = "2";
            m.mee_createtime = DateTime.Now;
            this.Update();
        }

        public void Update()
        {
            model.SaveChanges();
        }

        public meetings GetMeetings(int mee_no)
        {
            return (from d in model.meetings where d.mee_no == mee_no select d).FirstOrDefault();
        }

        public IQueryable<attends> Get_AttendsPeople(int mee_no)
        {
            return (from d in model.attends
                    where d.att_status == "1" && d.mee_no == mee_no
                    orderby d.att_no
                    select d);
        }

        /// <summary>
        /// 會議出席
        /// </summary>
        /// <param name="mee_no"></param>
        /// <param name="peo_uid"></param>
        /// <returns></returns>
        public string GetAttendsStatus(int mee_no, int peo_uid)
        {
            return (from p in model.attends
                    where p.mee_no == mee_no && p.peo_uid == peo_uid
                    select p.att_status).FirstOrDefault();
        }

        /// <summary>
        /// 回傳會議紀錄檔案資料筆數
        /// </summary>
        /// <param name="mee_no"></param>
        /// <returns></returns>
        public int Check_ConferenFile(int mee_no)
        {
            return (from d in model.conferen where d.mee_no == mee_no select d).Count();
        }

        public IQueryable<conferen> Get_Conferen(int mee_no)
        {
            return (from d in model.conferen where d.mee_no == mee_no select d);
        }

        public IQueryable<huiyi> Get_Huiyi(int mee_no)
        {
            return (from d in model.huiyi where d.mee_no == mee_no select d);
        }

    }

}