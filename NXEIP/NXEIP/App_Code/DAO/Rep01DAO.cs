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
    /// Rep01DAO 的摘要描述
    /// </summary>
    [DataObject(true)]
    public class Rep01DAO
    {
        private NXEIPEntities model = new NXEIPEntities();

        public Rep01DAO()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        public IQueryable<rep01> GetData()
        {
            return (from d in model.rep01 
                    from r in model.rep05
                    where d.r05_no == r.r05_no && r.r05_status == "1"
                    orderby d.r05_no
                    select d);
        }

        public IQueryable<rep01> GetData(int startRowIndex, int maximumRows)
        {
            return GetData().Skip(startRowIndex).Take(maximumRows);
        }

        public int GetDataCount()
        {
            return GetData().Count();
        }

        public void addToRep01(rep01 d)
        {
            model.rep01.AddObject(d);
        }

        public void deleteRep01(rep01 d)
        {
            model.rep01.DeleteObject(d);
        }

        public void Update()
        {
            model.SaveChanges();
        }

        public rep01 GetRep01(int r05_no, int r01_no)
        {
            return (from d in model.rep01 where d.r01_no == r01_no && d.r05_no == r05_no select d).FirstOrDefault();
        }

        public int GetMAXr01NO(int r05_no)
        {
            try
            {
                return (from d in model.rep01 where d.r05_no == r05_no select d.r01_no).Max();
            }
            catch
            {
                return 0;
            }
        }
    }
}