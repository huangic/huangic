using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using Entity;



namespace NXEIP.DAO
{
    /// <summary>
    /// Doc06DAO 的摘要描述
    /// </summary>
    [DataObject(true)]
    public class Doc06DAO
    {
        public Doc06DAO()
        {
            
        }

        NXEIPEntities model = new NXEIPEntities();

        /// <summary>
        /// 取所有公文附件
        /// </summary>
        /// <returns></returns>
        public IQueryable<doc06> GetAll()
        {
            var doc = from d in model.doc06 orderby d.d06_createtime select d;


            return doc;
        }

        public IQueryable<doc06> GetAll(int startRowIndex, int maximumRows)
        {
            return GetAll().Skip(startRowIndex).Take(maximumRows);
        }

        public int GetAllCount()
        {
            return GetAll().Count();
        }

        
        public IQueryable<doc06> GetSearchData(int? dep_no,string keyword){

            var doc =
                from p in model.people
                from d in model.doc06
                where p.peo_uid == d.d06_peouid
                where d.d06_status=="1"
                orderby d.d06_createtime
                select new { doc = d, people = p };

            if (dep_no != null) {
                //判斷單位級
                var depart=(from dep in model.departments where dep_no==dep.dep_no select dep).First();
                if (depart.dep_level == 1) {
                    var deps = (from d in model.departments where d.dep_parentid == depart.dep_no || d.dep_no==dep_no select d.dep_no);
                    

                doc=doc.Where(x => deps.Contains(x.doc.d06_depno.Value));
                }else{
                doc=doc.Where(x => x.doc.d06_depno == dep_no);
                }




               
            }

            if (!String.IsNullOrEmpty(keyword)) {
                 var files = from d07 in model.doc07
                                  from d in doc
                                  where d.doc.d06_no == d07.d06_no 
                                  && d07.d07_file.Contains(keyword)
                                  || d.people.peo_name.Contains(keyword) 
                                  || d.doc.d06_number.Contains(keyword)
                                  select d;
                
                
                
                //doc = doc.Where(x => x.people.peo_name.Contains(keyword) || x.doc.d06_number.Contains(keyword));


                //doc=filenameDoc.Union(doc);
                 return files.Select(x => x.doc).Distinct().OrderBy(x=>x.d06_createtime);
            }

            return doc.Select(x=>x.doc);
           
    }

        public int GetSearchDataCount(int? dep_no, string keyword) {
            return GetSearchData(dep_no, keyword).Count();
        }

        public IQueryable<doc06> GetSearchData(int? dep_no, string keyword, int startRowIndex, int maximumRows) {
            return GetSearchData(dep_no, keyword).Skip(startRowIndex).Take(maximumRows);
        }



        ////


        public IQueryable<doc06> GetSearchData(int? dep_no, string number,string file)
        {

            var doc =
                from p in model.people
                from d in model.doc06
                where p.peo_uid == d.d06_peouid
                &&d.d06_status=="1"
                orderby d.d06_createtime
                select new { doc = d, people = p };

            if (dep_no != null)
            {
                //判斷單位級
                var depart = (from dep in model.departments where dep_no == dep.dep_no select dep).First();
                if (depart.dep_level == 1)
                {
                    var deps = (from d in model.departments where d.dep_parentid == depart.dep_no || d.dep_no == dep_no select d.dep_no);


                    doc = doc.Where(x => deps.Contains(x.doc.d06_depno.Value));
                }
                else
                {
                    doc = doc.Where(x => x.doc.d06_depno == dep_no);
                }





            }

            if (!String.IsNullOrEmpty(number))
            {
                doc = doc.Where(x => x.doc.d06_number.Contains(number));
            }



            if (!String.IsNullOrEmpty(file))
            {
                var files = from d07 in model.doc07
                            from d in doc
                            where d.doc.d06_no == d07.d06_no
                            && d07.d07_file.Contains(file)
                         select d;



                //doc = doc.Where(x => x.people.peo_name.Contains(keyword) || x.doc.d06_number.Contains(keyword));


                //doc=filenameDoc.Union(doc);
                return files.Select(x => x.doc).OrderBy(x => x.d06_createtime);
            }

            return doc.Select(x => x.doc);

        }

        public int GetSearchDataCount(int? dep_no, string number, string file)
        {
            return GetSearchData(dep_no, number,file).Count();
        }

        public IQueryable<doc06> GetSearchData(int? dep_no, string number, string file, int startRowIndex, int maximumRows)
        {
            return GetSearchData(dep_no, number, file).Skip(startRowIndex).Take(maximumRows);
        }


        public IQueryable<doc06> GetPublicData(string number,string peo_name) {
            var doc =
                   from p in model.people
                   from d in model.doc06
                   where p.peo_uid == d.d06_peouid
                   && d.d06_status == "1"
                   && d.d06_open=="2"
                   && d.d06_number.Contains(number)
                   && p.peo_name.Equals(peo_name)
                   orderby d.d06_createtime
                   select d;

            return doc;
        }

    }
}