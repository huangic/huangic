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
    /// Rep04DAO 的摘要描述
    /// </summary>
    [DataObject(true)]
    public class Rep04DAO
    {
        public Rep04DAO()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }
        
        private NXEIPEntities model = new NXEIPEntities();

        public IQueryable<rep04> GetData()
        {
            return (from d in model.rep04 where d.r04_status == "1" orderby d.r04_no select d);
        }

        public IQueryable<rep04> GetData(int startRowIndex, int maximumRows)
        {
            return GetData().Skip(startRowIndex).Take(maximumRows);
        }

        public int GetDataCount()
        {
            return GetData().Count();
        }

        public rep04 GetRep04(int r04_no)
        {
            return (from d in model.rep04 where d.r04_no == r04_no select d).FirstOrDefault();
        }

        public void AddToRep04(rep04 d)
        {
            model.rep04.AddObject(d);
        }

        public void Update()
        {
            model.SaveChanges();
        }
    }

}