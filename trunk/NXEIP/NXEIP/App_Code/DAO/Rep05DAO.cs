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
    /// Rep05DAO 的摘要描述
    /// </summary>
    [DataObject(true)]
    public class Rep05DAO
    {
        public Rep05DAO()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        private NXEIPEntities model = new NXEIPEntities();

        public string GetRep05Name(int r05_no)
        {
            return (from d in model.rep05 where d.r05_no == r05_no && d.r05_status == "1" select d.r05_name).FirstOrDefault();
        }

        public IQueryable<rep05> GetData()
        {
            return (from d in model.rep05 where d.r05_status == "1" orderby d.r05_no select d);
        }

        public IQueryable<rep05> GetData(int startRowIndex, int maximumRows)
        {
            return GetData().Skip(startRowIndex).Take(maximumRows);
        }

        public int GetDataCount()
        {
            return GetData().Count();
        }

        public rep05 GetRep05(int r05_no)
        {
            return (from d in model.rep05 where d.r05_no == r05_no select d).FirstOrDefault();
        }

        public void AddToRep05(rep05 d)
        {
            model.rep05.AddObject(d);
        }

        public void Update()
        {
            model.SaveChanges();
        }

    }
}