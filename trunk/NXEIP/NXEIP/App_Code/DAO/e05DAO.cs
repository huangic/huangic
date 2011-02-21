using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using System.ComponentModel;

namespace NXEIP.DAO
{
    /// <summary>
    /// e05DAO 的摘要描述
    /// </summary>
    [DataObject(true)]
    public class e05DAO
    {
        private NXEIPEntities model = new NXEIPEntities();

        public e05DAO()
        {
           
        }

        public IQueryable<e05> GetDataBye02no(int e02_no)
        {
            var data = (from d in model.e05
                        where d.e02_no == e02_no
                        orderby d.e05_no
                        select d);

            return data;

        }

        public IQueryable<e05> GetDataBye02no(int e02_no, int startRowIndex, int maximumRows)
        {
            return GetDataBye02no(e02_no).Skip(startRowIndex).Take(maximumRows);
        }

        public int GetDataBye02noCount(int e02_no)
        {
            return GetDataBye02no(e02_no).Count();
        }

    }

    



}