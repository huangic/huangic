using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;

namespace NXEIP.DAO
{
    /// <summary>
    /// e01DAO 的摘要描述
    /// </summary>
    public class e01DAO
    {
        public e01DAO()
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
        public IQueryable<e01> GetAll()
        {
            return (from data in model.e01 where data.e01_status == "1" orderby data.e01_order select data);
        }

        /// <summary>
        /// 查詢符合筆數之資料
        /// </summary>
        /// <param name="startRowIndex">起始筆數</param>
        /// <param name="maximumRows">結束筆數</param>
        /// <returns></returns>
        public IQueryable<e01> GetAll(int startRowIndex, int maximumRows)
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

        public e01 GetBye01NO(int e01_no)
        {
            return (from d in model.e01 where d.e01_no == e01_no select d).FirstOrDefault();
        }

        public void Adde01(e01 e01)
        {
            model.AddToe01(e01);
        }

        public int Update()
        {
            return model.SaveChanges();
        }
    }
}