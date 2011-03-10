using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using Entity;



namespace NXEIP.DAO
{
    /// <summary>
    /// CooperDAO 的摘要描述
    /// </summary>
    [DataObject(true)]
    public class CooperDAO
    {
        public CooperDAO()
        {
           
        }

        NXEIPEntities model = new NXEIPEntities();

      

        /// <summary>
        /// 取合作社
        /// </summary>
        /// <param name="cat_no"></param>
        /// <param name="file"></param>
       /// <returns></returns>
        public IQueryable<cooperactive> GetSearchData(int? cat_no,string name)
        {

            var doc =
                
                from d in model.cooperactive
                where 
                d.coo_status=="1"
                && d.coo_s06no==cat_no.Value
                orderby d.coo_createtime
                select d ;

          


            if (!String.IsNullOrEmpty(name))
            {
                var files = doc.Where(x=>x.coo_name==name).OrderBy(x=>x.coo_createtime);




                return files;
            }

            return doc;

        }

        public int GetSearchDataCount(int? cat_no, string name)
        {
            return GetSearchData(cat_no, name).Count();
        }

        public IQueryable<cooperactive> GetSearchData(int? cat_no, string name, int startRowIndex, int maximumRows)
        {
            return GetSearchData(cat_no, name).Skip(startRowIndex).Take(maximumRows);
        }


    }
}