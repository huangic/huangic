using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using System.ComponentModel;

namespace NXEIP.DAO
{
    /// <summary>
    /// MessageDAO 的摘要描述
    /// </summary>
    [DataObject(true)]
    public class MessageDAO
    {
        public MessageDAO()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        private NXEIPEntities model = new NXEIPEntities();

        public message GetDataByNo(int mes_no)
        {
            return (from d in model.message where d.mes_no == mes_no select d).FirstOrDefault();
        }

        public medetail GetDataByNo2(int mes_no, int peo_uid)
        {
            return (from d in model.medetail where d.mes_no == mes_no && d.med_peouid == peo_uid select d).FirstOrDefault();
        }

        public IQueryable<medetail> GetMedetail(int mes_no)
        {
            return (from d in model.medetail where d.mes_no == mes_no select d);
        }

        public message Search(string subject, string content, string link,int peo_uid)
        {
            DBObject s = new DBObject();
            
            return (from d in model.message
                    where d.mes_subject == subject && d.mes_content.Contains(content) && d.mes_senduid.Value == peo_uid && d.mes_status == "1"
                    select d).FirstOrDefault();
        }

        /// <summary>
        /// 個人本身所發送
        /// </summary>
        /// <param name="peo_uid"></param>
        /// <returns></returns>
        public IQueryable<message> GetData(int peo_uid)
        {
            return (from d in model.message
                    where d.mes_senduid == peo_uid && d.mes_status == "1"
                    orderby d.mes_datetime descending
                    select d);
        }

        public IQueryable<message> GetData(int peo_uid, int startRowIndex, int maximumRows)
        {
            return GetData(peo_uid).Skip(startRowIndex).Take(maximumRows);
        }

        public int GetDataCount(int peo_uid)
        {
            return GetData(peo_uid).Count();
        }

        /// <summary>
        /// 傳送給我的
        /// </summary>
        /// <param name="peo_uid"></param>
        /// <returns></returns>
        public IQueryable<message> GetData2(int peo_uid)
        {
            int[] mes_no = (from d in model.medetail 
                            where d.med_peouid == peo_uid && d.med_status == "1" 
                            select d.mes_no).ToArray();

            return (from d in model.message
                    where mes_no.Contains(d.mes_no) && d.mes_status == "1"
                    orderby d.mes_datetime descending
                    select d);
        }

        public IQueryable<message> GetData2(int peo_uid, int startRowIndex, int maximumRows)
        {
            return GetData2(peo_uid).Skip(startRowIndex).Take(maximumRows);
        }

        public int GetDataCount2(int peo_uid)
        {
            return GetData2(peo_uid).Count();
        }

        public int maxMedNO(int mes_no)
        {
            var x = (from d in model.medetail where d.mes_no == mes_no select d.med_no);

            if (x.Count() > 0)
            {
                return x.Max();
            }
            else
            {
                return 0;
            }
        }

        public void AddToMessage(message d)
        {
            model.message.AddObject(d);
        }

        public void AddToMedetail(medetail d)
        {
            model.medetail.AddObject(d);
        }

        public void Update()
        {
            model.SaveChanges();
        }

    }

}