using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using System.ComponentModel;


namespace NXEIP.DAO
{

    /// <summary>
    /// TermsDAO 的摘要描述
    /// </summary>
    [DataObject(true)]
    public class TermsDAO
    {
        public TermsDAO()
        {
          
        }

        private NXEIPEntities model = new NXEIPEntities();

        public IQueryable<terms> GetAll(int uid)
        {
            return (from d in model.terms where d.peo_uid == uid orderby d.ter_name select d);
        }

        public IQueryable<terms> GetAll(int uid, int startRowIndex, int maximumRows)
        {
            return GetAll(uid).Skip(startRowIndex).Take(maximumRows);
        }

        public int GetAllCount(int uid)
        {
            return GetAll(uid).Count();
        }
    }
}