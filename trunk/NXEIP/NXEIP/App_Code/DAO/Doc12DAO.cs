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
    public class Doc12DAO
    {
        NXEIPEntities model = new NXEIPEntities();
        
        
        public Doc12DAO()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        public IQueryable<doc12> GetAllWithDoc11No(int doc11_no) {
            var doc11 = from d in model.doc12 where d.d11_no == doc11_no orderby d.d12_no select d;
            return doc11;
        }
    }
}