using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;

/// <summary>
/// TypesDAO 的摘要描述
/// </summary>
/// 


namespace NXEIP.DAO
{
    
    public class TypesDAO
    {
        public TypesDAO()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }


        private NXEIPEntities model = new NXEIPEntities();

        public types GetTypes(int typ_no)
        {
            return (from t in model.types where t.typ_no == typ_no select t).FirstOrDefault();

        }

        public IQueryable<types> GetAll(String type_code)
        {
            return (from t in model.types
                    where t.typ_status == "1" && t.typ_code == type_code orderby t.typ_order select t);
        }

        public IQueryable<types> GetAll(String type_code, int startRowIndex, int maximumRows)
        {
            return GetAll(type_code).Skip(startRowIndex).Take(maximumRows);
        }

        public int GetAllCount(String type_code)
        {
            return GetAll(type_code).Count();

        }


        public void AddTypes(types type)
        {
            model.AddTotypes(type);
        }

        public int Update()
        {
            return model.SaveChanges();
        }

    }
}