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

        public IQueryable<message> GetData(int peo_uid)
        {
            return (from d in model.message
                    where d.mes_peouid == peo_uid && d.mes_status == "1"
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

        public void AddToMessage(message d)
        {
            model.message.AddObject(d);
        }

        public void Update()
        {
            model.SaveChanges();
        }

    }

}