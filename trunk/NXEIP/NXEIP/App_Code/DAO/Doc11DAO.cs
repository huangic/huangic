using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using Entity;



namespace NXEIP.DAO
{
    /// <summary>
    /// Doc11DAO 的摘要描述
    /// </summary>
    [DataObject(true)]
    public class Doc11DAO
    {
        public Doc11DAO()
        {
           
        }

        NXEIPEntities model = new NXEIPEntities();

        /// <summary>
        /// 取所有公文附件
        /// </summary>
        /// <returns></returns>
        public IQueryable<doc11> GetAll()
        {
            var doc = from d in model.doc11 orderby d.d11_createtime select d;


            return doc;
        }

        public IQueryable<doc11> GetAll(int startRowIndex, int maximumRows)
        {
            return GetAll().Skip(startRowIndex).Take(maximumRows);
        }

        public int GetAllCount()
        {
            return GetAll().Count();
        }


    


        public IQueryable<doc11> GetSearchData(string subject, string file)
        {

            var doc =
                from p in model.people
                from d in model.doc11
                where p.peo_uid == d.d11_peouid
                &&d.d11_status=="1"
                orderby d.d11_createtime
                select new { doc = d, people = p };



       
            if (!String.IsNullOrEmpty(subject))
            {
                doc = doc.Where(x => x.doc.d11_subject.Contains(subject));
            }



            if (!String.IsNullOrEmpty(file))
            {
                var files = from d12 in model.doc12
                            from d in doc
                            where d.doc.d11_no == d12.d11_no
                            && d12.d12_file.Contains(file)
                         select d;



                //doc = doc.Where(x => x.people.peo_name.Contains(keyword) || x.doc.d06_number.Contains(keyword));


                //doc=filenameDoc.Union(doc);
                return files.Select(x => x.doc).OrderBy(x => x.d11_createtime);
            }

            return doc.Select(x => x.doc);

        }

        public int GetSearchDataCount(string subject, string file)
        {
            return GetSearchData(subject, file).Count();
        }

        public IQueryable<doc11> GetSearchData(string subject, string file, int startRowIndex, int maximumRows)
        {
            return GetSearchData(subject, file).Skip(startRowIndex).Take(maximumRows);
        }


    }
}