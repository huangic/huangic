using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using Entity;


namespace NXEIP.DAO
{
    /// <summary>
    /// TurningDAO 的摘要描述
    /// </summary>
   [DataObject(true)]
    public class TurningDAO
    {
        public TurningDAO()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        NXEIPEntities model = new NXEIPEntities();

       public IQueryable<turning> GetDataByTreNO(int tre_no){
           var turnings = (from d in model.turning where d.tre_no == tre_no select d);

           return turnings;
       }
    }
}