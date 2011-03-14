using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using Entity;



namespace NXEIP.DAO
{
    /// <summary>
    /// IP ADDRESS 的摘要描述
    /// </summary>
    [DataObject(true)]
    public class UnmarriedDAO
    {
        public UnmarriedDAO()
        {
           
        }

        NXEIPEntities model = new NXEIPEntities();

        /// <summary>
        /// 取所有未婚
        /// </summary>
        /// <returns></returns>
        public IQueryable<unmarried> GetAll()
        {
            var doc = from d in model.unmarried orderby d.unm_no select d;
            
            return doc;
        }

        public IQueryable<unmarried> GetAll(int startRowIndex, int maximumRows)
        {
            return GetAll().Skip(startRowIndex).Take(maximumRows);
        }

        public int GetAllCount()
        {
            return GetAll().Count();
        }





        public IQueryable<unmarried> GetSearchData(string name, string sex)
        {

            var doc =
                from d in model.unmarried
                where d.unm_open=="1"
                orderby d.unm_order
                select d;



       
            if (!String.IsNullOrEmpty(name))
            {
                doc = (IOrderedQueryable<unmarried>)doc.Where(x => x.unm_name.Contains(name));
            }



            if (!String.IsNullOrEmpty(sex))
            {
                doc = (IOrderedQueryable<unmarried>)doc.Where(x => x.unm_sex==sex);


               
            }

            return doc;

        }

        public int GetSearchDataCount(string name, string sex)
        {
            return GetSearchData(name, sex).Count();
        }

        public IQueryable<unmarried> GetSearchData(string name, string sex, int startRowIndex, int maximumRows)
        {
            return GetSearchData(name, sex).Skip(startRowIndex).Take(maximumRows);
        }


    }
}