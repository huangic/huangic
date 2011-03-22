using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using System.ComponentModel;

namespace NXEIP.DAO
{
    /// <summary>
    /// e03DAO 的摘要描述
    /// </summary>
    [DataObject(true)]
    public class e03DAO
    {
        public e03DAO()
        {
        }

        private NXEIPEntities model = new NXEIPEntities();

        public IQueryable<e03> Get_Data(int e02_no)
        {
            return from d in model.e03 where d.e02_no == e02_no orderby d.e03_no select d;
        }

        public int Max_e03No(int e02_no)
        {
            var data = (from d in model.e03 where d.e02_no == e02_no select d.e03_no);

            if (data.Count() > 0)
            {
                return data.Max();
            }
            else
            {
                return 0;
            }
        }

        public e03 Get_e03(int e02_no, int e03_no)
        {
            return (from d in model.e03 where d.e02_no == e02_no && d.e03_no == e03_no select d).FirstOrDefault();
        }

        public e03 Get_e03_2(int e02_no, int dep_no)
        {
            return (from d in model.e03 where d.e02_no == e02_no && d.e03_depno == dep_no select d).FirstOrDefault();
        }

        public void addToe03(e03 d)
        {
            model.e03.AddObject(d);
        }

        public void delete(e03 d)
        {
            model.e03.DeleteObject(d);
        }

        public void Update()
        {
            model.SaveChanges();
        }
         

    }
}