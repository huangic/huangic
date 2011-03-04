using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using Entity;

/// <summary>
/// _200601DAO 的摘要描述
/// </summary>
/// 
namespace NXEIP.DAO
{
    [DataObject(true)]
    public class _200601WidgetDAO
    {
        public _200601WidgetDAO()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        NXEIPEntities model = new NXEIPEntities();



        /// <summary>
        /// 取出公開型的討論區，最新的10筆文章
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
     public IQueryable<tao01> GetNewTopic(int num){
        //取所有的公開討論區

         var topics = (from f in model.taolun
                      from t in model.tao01
                      where f.tao_type == "1" && f.tao_status == "1"
                      && f.tao_no == t.tao_no
                      && t.t01_status=="1"
                      && t.t01_parent == 0 orderby t.t01_date
                      select t).Take(num);
         return topics;
     }

       

        
    }
}