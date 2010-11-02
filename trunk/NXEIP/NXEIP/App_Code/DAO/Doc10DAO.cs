using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using Entity;


namespace NXEIP.DAO
{
    /// <summary>
    /// Doc07DAO 的摘要描述
    /// </summary>
    [DataObject(true)]
    public class Doc10DAO
    {
        NXEIPEntities model = new NXEIPEntities();
        
        
        public Doc10DAO()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        public IQueryable<doc10> GetAllWithDoc09No(int doc09_no) {
            var doc10 = from d in model.doc10 where d.d09_no == doc09_no orderby d.d09_no select d;
            return doc10;
        }

        public IQueryable<doc10> GetDoc10FromE05(int e02_no)
        {
            return (from e05d in model.e05
                    where e05d.e02_no == e02_no
                    from dc09 in model.doc09
                    where dc09.d09_no == e05d.e05_d09no
                    from dc10 in model.doc10
                    where dc10.d09_no == dc09.d09_no
                    orderby dc10.d10_no
                    select dc10);
        }

        public IQueryable<doc10> GetDoc10FromE05(int e02_no, int startRowIndex, int maximumRows)
        {
            return GetDoc10FromE05(e02_no).Skip(startRowIndex).Take(maximumRows);
        }

        public int GetDoc10FromE05Count(int e02_no)
        {
            return GetDoc10FromE05(e02_no).Count();
        }

        public IQueryable<doc10> GetDoc10ByS06NO(int s06_no,int peo_uid)
        {
            return (from dc09 in model.doc09
                    where dc09.s06_no == s06_no && dc09.d09_peouid == peo_uid
                    from dc10 in model.doc10
                    where dc10.d09_no == dc09.d09_no
                    orderby dc09.d09_date descending
                    select dc10);
        }
    }
}