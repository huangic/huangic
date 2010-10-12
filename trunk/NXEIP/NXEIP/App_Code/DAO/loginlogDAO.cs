using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using Entity;
using System.ComponentModel;

namespace NXEIP.DAO
{

    /// <summary>
    /// loginlogDAO 的摘要描述
    /// </summary>
    [DataObject(true)]
    public class loginlogDAO
    {
        public loginlogDAO()
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
        public IQueryable<loginlog> GetAll()
        {
            return (from data in model.loginlog orderby data.log_logintime descending select data);
        }

        /// <summary>
        /// 查詢符合筆數之資料
        /// </summary>
        /// <param name="startRowIndex">起始筆數</param>
        /// <param name="maximumRows">結束筆數</param>
        /// <returns></returns>
        public IQueryable<loginlog> GetAll(int startRowIndex, int maximumRows)
        {
            return GetAll().Skip(startRowIndex).Take(maximumRows);
        }

        public loginlog GetByLogNo(string logNo)
        {
            return (from d in model.loginlog where d.log_no == logNo select d).FirstOrDefault();
        }

        /// <summary>
        /// 查詢總筆數
        /// </summary>
        /// <returns></returns>
        public int GetAllCount()
        {
            return GetAll().Count();
        }

        public void Addloginlog(loginlog loginlog)
        {
            model.AddTologinlog(loginlog);
        }

        public int Update()
        {
            return model.SaveChanges();
        }
    }
}