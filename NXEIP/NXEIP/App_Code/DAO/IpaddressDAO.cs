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
    public class IpaddressDAO
    {
        public IpaddressDAO()
        {
           
        }

        NXEIPEntities model = new NXEIPEntities();

        /// <summary>
        /// 取所有公文附件
        /// </summary>
        /// <returns></returns>
        public IQueryable<ipaddress> GetAll()
        {
            var doc = from d in model.ipaddress orderby d.people.dep_no,d.peo_uid select d;
            
            return doc;
        }

        public IQueryable<ipaddress> GetAll(int startRowIndex, int maximumRows)
        {
            return GetAll().Skip(startRowIndex).Take(maximumRows);
        }

        public int GetAllCount()
        {
            return GetAll().Count();
        }





        public IQueryable<ipaddress> GetSearchData(string ip, string name)
        {

            var doc =
                from d in model.ipaddress
                where d.ipa_status=="1"
                orderby d.people.dep_no,d.people.peo_uid
                select d;



       
            if (!String.IsNullOrEmpty(ip))
            {
                doc = (IOrderedQueryable<ipaddress>)doc.Where(x => x.ipa_start==ip);
            }



            if (!String.IsNullOrEmpty(name))
            {
                doc = (IOrderedQueryable<ipaddress>)doc.Where(x => x.people.peo_name.Contains(name));


               
            }

            return doc;

        }

        public int GetSearchDataCount(string ip, string name)
        {
            return GetSearchData(ip, name).Count();
        }

        public IQueryable<ipaddress> GetSearchData(string ip, string name, int startRowIndex, int maximumRows)
        {
            return GetSearchData(ip, name).Skip(startRowIndex).Take(maximumRows);
        }


    }
}