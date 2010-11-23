using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using Entity;

/// <summary>
/// _200303DAO 的摘要描述
/// </summary>
/// 
namespace NXEIP.DAO
{
    [DataObject(true)]
    public class _200303DAO
    {
        public _200303DAO()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        NXEIPEntities model = new NXEIPEntities();

        /// <summary>
        /// 取樓誠場地
        /// </summary>
        /// <returns></returns>
        public IQueryable<spot> GetFloorsSpot(){
                return (from d in model.spot where d.spo_status=="1" && d.spo_function.Substring(1,1)=="1" select d);
        }

    }
}