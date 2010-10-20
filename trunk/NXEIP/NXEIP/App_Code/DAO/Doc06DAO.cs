using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using Entity;



namespace NXEIP.DAO
{
    /// <summary>
    /// Doc06DAO 的摘要描述
    /// </summary>
    [DataObject(true)]
    public class Doc06DAO
    {
        public Doc06DAO()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        NXEIPEntities model = new NXEIPEntities();

        /// <summary>
        /// 取所有公文附件
        /// </summary>
        /// <returns></returns>
        public IQueryable<doc06> GetAll()
        {
            var doc = from d in model.doc06 orderby d.d06_createtime select d;


            return doc;
        }

        public IQueryable<doc06> GetAll(int startRowIndex, int maximumRows)
        {
            return GetAll().Skip(startRowIndex).Take(maximumRows);
        }

        public int GetAllCount()
        {
            return GetAll().Count();
        }
    }
}