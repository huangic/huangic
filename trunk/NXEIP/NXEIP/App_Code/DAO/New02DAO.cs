using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using System.ComponentModel;

namespace NXEIP.DAO
{
    /// <summary>
    /// New02DAO 的摘要描述
    /// </summary>
    [DataObject(true)]
    public class New02DAO
    {
        private NXEIPEntities model = new NXEIPEntities();

        public New02DAO()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        public IQueryable<new02> GetData(int n01_no)
        {
            return (from d in model.new02 where d.n01_no == n01_no select d);
        }


    }



}