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
    public class TaolunDAO
    {
        public TaolunDAO()
        {
          
        }

        private NXEIPEntities model = new NXEIPEntities();

        /// <summary>
        /// 取藥省和的
        /// </summary>
        /// <returns></returns>
        public IQueryable<taolun> GetCheckAll()
        {
            return (from d in model.taolun where d.tao_status == "0" orderby d.tao_createtime select d);
        }

        public IQueryable<taolun> GetAll(int uid, int startRowIndex, int maximumRows)
        {
            return GetCheckAll().Skip(startRowIndex).Take(maximumRows);
        }

        public int GetCheckAllCount()
        {
            return GetCheckAll().Count();
        }
    }
}