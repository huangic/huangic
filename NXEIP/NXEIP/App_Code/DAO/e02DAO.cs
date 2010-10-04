using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;

namespace NXEIP.DAO
{
    /// <summary>
    /// e02DAO 的摘要描述
    /// </summary>
    public class e02DAO
    {
        public e02DAO()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        private NXEIPEntities model = new NXEIPEntities();

        /// <summary>
        /// 查詢所有資料
        /// </summary>
        /// <returns></returns>
        public IQueryable<e02> GetAll()
        {
            return (from data in model.e02 
                    where data.e02_status == "1" 
                    orderby data.e02_signdate descending, data.e02_sdate descending 
                    select data);
        }

        /// <summary>
        /// 查詢符合筆數之資料
        /// </summary>
        /// <param name="startRowIndex">起始筆數</param>
        /// <param name="maximumRows">結束筆數</param>
        /// <returns></returns>
        public IQueryable<e02> GetAll(int startRowIndex, int maximumRows)
        {
            return GetAll().Skip(startRowIndex).Take(maximumRows);
        }

        /// <summary>
        /// 查詢總筆數
        /// </summary>
        /// <returns></returns>
        public int GetAllCount()
        {
            return GetAll().Count();
        }

        /// <summary>
        /// 查詢符合條件資料
        /// </summary>
        /// <param name="e01_no"></param>
        /// <returns></returns>
        public e02 GetBye01NO(int e02_no)
        {
            return (from d in model.e02 where d.e02_no == e02_no select d).FirstOrDefault();
        }

        public void Adde02(e02 e02)
        {
            model.AddToe02(e02);
        }

        public int Update()
        {
            return model.SaveChanges();
        }
    }

}