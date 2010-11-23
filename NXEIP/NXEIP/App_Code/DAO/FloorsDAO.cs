using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using System.ComponentModel;



namespace NXEIP.DAO
{
    /// <summary>
    /// FloorsDAO 的摘要描述
    /// </summary>
    [DataObject(true)]
    public class FloorsDAO
    {
        public FloorsDAO()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        NXEIPEntities model = new NXEIPEntities();

        public IQueryable<floors> GetDataBySpotNo(int spot_no){
                return from d in model.floors where d.spo_no==spot_no select d;
        }
    }
}