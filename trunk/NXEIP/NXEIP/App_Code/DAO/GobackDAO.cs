using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using System.ComponentModel;


namespace NXEIP.DAO
{
    /// <summary>
    /// Goback 的摘要描述
    /// </summary>
    [DataObject(true)]
    public class GobackDAO
    {
        public GobackDAO()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        NXEIPEntities model = new NXEIPEntities();

        public IQueryable<goback> GetDataByTdeNo(int tde_no) {
            return (from d in model.goback where d.tde_no == tde_no select d);
        }
    }
}