using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using System.ComponentModel;

namespace NXEIP.DAO
{
    /// <summary>
    /// _200702DAO 的摘要描述
    /// </summary>
    [DataObject(true)]
    public class _200702DAO
    {
        private NXEIPEntities model = new NXEIPEntities();

        public _200702DAO()
        {
        }

        //類別
        public IQueryable<qatype> Get_qatype(string self)
        {
            return (from d in model.qatype
                    where d.qat_self == self && d.qat_status == "1"
                    select d);
        }

        //可回覆類別
        public int[] Get_MyQAtype(int peo_uid)
        {
           return (from d in model.qamanager where d.qam_peouid == peo_uid select d.qat_no).ToArray();
        }

        public IQueryable<ask> Get_askData(string self,int? qat_no,string key)
        {
            var data = (from d in model.ask where d.ask_status == "1" select d);

            if (!string.IsNullOrEmpty(self))
            {
                int[] qat_array = (from d in model.qatype
                                   where d.qat_self == self
                                   orderby d.qat_name, d.qat_s06no, d.qat_r05no
                                   select d.qat_no).ToArray();
                data = (from d in model.ask
                        where qat_array.Contains(d.qat_no) && d.ask_status == "1"
                        select d);
            }

            if (qat_no.HasValue)
            {
                data = (from d in model.ask
                        where d.qat_no == qat_no && d.ask_status == "1"
                        select d);
            }

            if (!string.IsNullOrEmpty(key))
            {
                data = data.Where(o => o.ask_question.Contains(key));
            }

            data = data.OrderByDescending(o => o.ask_createtime.Value);

            return data;
        }

        public IQueryable<ask> Get_askData(string self, int? qat_no, string key, int startRowIndex, int maximumRows)
        {
           return Get_askData(self, qat_no, key).Skip(startRowIndex).Take(maximumRows);
        }

        public int Get_askDataCount(string self, int? qat_no, string key)
        {
            return Get_askData(self, qat_no, key).Count();
        }

        public ask Get_ask(int ask_no)
        {
            return (from d in model.ask where d.ask_no == ask_no select d).FirstOrDefault();
        }


        public void AddToAsk(ask d)
        {
            model.ask.AddObject(d);
        }

        public void Update()
        {
            model.SaveChanges();
        }


    }

}