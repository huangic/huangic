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
    public class Doc13DAO
    {
        NXEIPEntities model = new NXEIPEntities();
        
        
        public Doc13DAO()
        {
          
        }

        public IQueryable<doc13> GetAllWithDoc11No(int doc11_no) {
            var doc11 = from d in model.doc13 where d.d11_no == doc11_no orderby d.d13_no select d;
            return doc11;
        }
    }
}