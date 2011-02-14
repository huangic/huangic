using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using Entity;

/// <summary>
/// _200601_2DAO 的摘要描述
/// </summary>
/// 
namespace NXEIP.DAO
{
    [DataObject(true)]
    public class _200601_9DAO
    {
        public _200601_9DAO()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        NXEIPEntities model = new NXEIPEntities();

        /// <summary>
        /// 取得申請會員
        /// </summary>
        /// <param name="tao_no">討論區編號</param>
        /// <returns></returns>
        public IQueryable<tao03> GetTao03(int tao_no)
        {



            IQueryable<tao03> taos = (from t in model.tao03 
                                      where t.tao_no == tao_no 
                                      && t.t03_status != "2" 
                                      orderby t.t03_status
                                      orderby t.t03_date descending
                                      select t);

                         

            



            return taos;
        }

        public IQueryable<tao03> GetTao03(int tao_no, int startRowIndex, int maximumRows)
        {
            return GetTao03(tao_no).Skip(startRowIndex).Take(maximumRows);
        }



        public int GetTao03Count(int tao_no)
        {
            return GetTao03(tao_no).Count();
        }


    }
    
}