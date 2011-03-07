using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using Entity;



namespace NXEIP.DAO
{
    /// <summary>
    /// IP ADDRESS 的摘要描述
    /// </summary>
    [DataObject(true)]
    public class EmailDAO
    {
        public EmailDAO()
        {
           
        }

        NXEIPEntities model = new NXEIPEntities();

        /// <summary>
        /// 取所有公文附件
        /// </summary>
        /// <returns></returns>
        public IQueryable<email> GetAll()
        {
            var doc = from d in model.email orderby d.ema_no select d;
            
            return doc;
        }

        public IQueryable<email> GetAll(int startRowIndex, int maximumRows)
        {
            return GetAll().Skip(startRowIndex).Take(maximumRows);
        }

        public int GetAllCount()
        {
            return GetAll().Count();
        }

        /// <summary>
        /// 取使用者申請的電子郵件清單
        /// </summary>
        /// <param name="peo_uid">使用者編號</param>
        /// <returns></returns>
        public IQueryable<email> GetUserApplyEmail(int peo_uid){
            var doc = from d in model.email where d.ema_peouid==peo_uid  orderby d.ema_no select d;
            
            return doc;
        }

        /// <summary>
        /// 取使用者申請的電子郵件清單數量
        /// </summary>
        /// <param name="peo_uid">使用者編號</param>
        /// <returns></returns>
        public int GetUserApplyEmailCount(int peo_uid)
        {
            

            return this.GetUserApplyEmail(peo_uid).Count() ;
        }


        public IQueryable<email> GetUserApplyEmail(int peo_uid, int startRowIndex, int maximumRows)
        {
            return GetUserApplyEmail(peo_uid).Skip(startRowIndex).Take(maximumRows);
        }


        public IQueryable<email> GetApplyEmail()
        {
            var doc = from d in model.email where d.ema_status=="3" orderby d.ema_no select d;

            return doc;
        }
        public int GetApplyEmailCount()
        {


            return this.GetApplyEmail().Count();
        }
        public IQueryable<email> GetApplyEmail(int startRowIndex, int maximumRows)
        {
            return GetApplyEmail().Skip(startRowIndex).Take(maximumRows);
        }


    }
}