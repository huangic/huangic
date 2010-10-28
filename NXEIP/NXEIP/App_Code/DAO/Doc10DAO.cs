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
    public class Doc10DAO
    {
        NXEIPEntities model = new NXEIPEntities();
        
        
        public Doc10DAO()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        public IQueryable<doc10> GetAllWithDoc09No(int doc09_no) {
            var doc10 = from d in model.doc10 where d.d09_no == doc09_no orderby d.d09_no select d;
            return doc10;
        }
    }
}