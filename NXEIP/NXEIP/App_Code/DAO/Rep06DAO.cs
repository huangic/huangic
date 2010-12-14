using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using System.Linq.Dynamic;
using System.ComponentModel;

namespace NXEIP.DAO
{
    /// <summary>
    /// Rep06DAO 的摘要描述
    /// </summary>
    [DataObject(true)]
    public class Rep06DAO
    {
        public Rep06DAO()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        private NXEIPEntities model = new NXEIPEntities();

        public rep06 GetRep06(int r06_no)
        {
            return (from d in model.rep06 where d.r06_no == r06_no select d).FirstOrDefault();
        }

        public string GetRep06Name(int r06_no)
        {
            return (from d in model.rep06 where d.r06_no == r06_no select d.r06_name).FirstOrDefault();
        }

        public IQueryable<rep06> GetRep06Parent(int r05_no)
        {
            return (from d in model.rep06
                    where d.r05_no == r05_no && d.r06_status == "1" && d.r06_parent == 0
                    orderby d.r06_order
                    select d);
        }

        public IQueryable<rep06> GetData(int r05_no)
        {
            return (from d in model.rep06
                    where d.r05_no == r05_no && d.r06_status == "1"
                    orderby d.r06_parent, d.r06_order
                    select d);
        }

        public IQueryable<rep06> GetData(int r05_no, int startRowIndex, int maximumRows)
        {
            return GetData(r05_no).Skip(startRowIndex).Take(maximumRows);
        }

        public int GetDataCount(int r05_no)
        {
            return GetData(r05_no).Count();
        }

        public void AddToRep06(rep06 d)
        {
            model.rep06.AddObject(d);
        }

        public void Update()
        {
            model.SaveChanges();
        }


    }
}