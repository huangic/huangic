using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using Entity;


namespace NXEIP.DAO
{
    /// <summary>
    /// Doc07DAO 的摘要描述
    /// </summary>
    [DataObject(true)]
    public class Doc07DAO
    {
        NXEIPEntities model = new NXEIPEntities();
        
        
        public Doc07DAO()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        public IQueryable<doc07> GetAllWithDoc06No(int doc06_no) {
            var doc07 = from d in model.doc07 where d.d06_no == doc06_no orderby d.d07_no select d;
            return doc07;
        }
    }
}