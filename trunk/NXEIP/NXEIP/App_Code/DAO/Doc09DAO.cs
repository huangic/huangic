using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using Entity;



namespace NXEIP.DAO
{
    /// <summary>
    /// Doc09DAO 的摘要描述
    /// </summary>
    [DataObject(true)]
    public class Doc09DAO
    {
        public Doc09DAO()
        {
           
        }

        NXEIPEntities model = new NXEIPEntities();

        /// <summary>
        /// 取所有公文附件
        /// </summary>
        /// <returns></returns>
        public IQueryable<doc09> GetAll()
        {
            var doc = from d in model.doc09 orderby d.d09_createtime select d;


            return doc;
        }

        public IQueryable<doc09> GetAll(int startRowIndex, int maximumRows)
        {
            return GetAll().Skip(startRowIndex).Take(maximumRows);
        }

        public int GetAllCount()
        {
            return GetAll().Count();
        }


       

        /// <summary>
        /// 取檔案區檔案
        /// </summary>
        /// <param name="dep_no"></param>
        /// <param name="cat_no"></param>
        /// <param name="file"></param>
       /// <returns></returns>
        public IQueryable<doc09> GetSearchData(int? dep_no, int? cat_no,string file)
        {

            var doc =
                from p in model.people
                from d in model.doc09
                where p.peo_uid == d.d09_peouid
                orderby d.d09_createtime
                select new { doc = d, people = p };

            if (dep_no != null)
            {
                doc = doc.Where(x => x.doc.d09_open == "2");
                
                //判斷單位級
                var depart = (from dep in model.departments where dep_no == dep.dep_no select dep).First();
                if (depart.dep_level == 1)
                {
                    var deps = (from d in model.departments where d.dep_parentid == depart.dep_no || d.dep_no == dep_no select d.dep_no);


                    doc = doc.Where(x => deps.Contains(x.doc.d09_depno));
                }
                else
                {
                    doc = doc.Where(x => x.doc.d09_depno == dep_no);
                }





            }
            else {
                doc = doc.Where(x => x.doc.d09_open == "1");
            
            }

            if (cat_no!=null)
            {
                //判斷類別等級 然後決定要加入那些
                var cat = (from c in model.sys06 where c.s06_no == cat_no select c).First();

                if (cat.s06_level == 1)
                {
                    var cats = (from c in model.sys06 where c.s06_parent == cat_no || c.s06_no == cat_no select c.s06_no);
                    doc = doc.Where(x =>cats.Contains(x.doc.s06_no));
                }
                else {
                    doc = doc.Where(x => x.doc.s06_no==cat_no);
                }

                
                
            }



            if (!String.IsNullOrEmpty(file))
            {
                var files = from d10 in model.doc10
                            from d in doc
                            where d.doc.d09_no == d10.d09_no
                            && d10.d10_file.Contains(file)
                         select d;



                //doc = doc.Where(x => x.people.peo_name.Contains(keyword) || x.doc.d06_number.Contains(keyword));


                //doc=filenameDoc.Union(doc);
                return files.Select(x => x.doc).OrderBy(x => x.d09_createtime);
            }

            return doc.Select(x => x.doc);

        }

        public int GetSearchDataCount(int? dep_no, int? cat_no, string file)
        {
            return GetSearchData(dep_no, cat_no, file).Count();
        }

        public IQueryable<doc09> GetSearchData(int? dep_no, int? cat_no, string file, int startRowIndex, int maximumRows)
        {
            return GetSearchData(dep_no, cat_no, file).Skip(startRowIndex).Take(maximumRows);
        }


    }
}