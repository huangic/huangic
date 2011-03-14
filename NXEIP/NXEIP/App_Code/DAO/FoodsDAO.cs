using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using System.ComponentModel;
using System.Linq.Dynamic;
using System.Data.Objects.SqlClient;

namespace NXEIP.DAO
{
    /// <summary>
    /// FoodsDAO 的摘要描述
    /// </summary>
    [DataObject(true)]
    public class FoodsDAO
    {
        public FoodsDAO()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        private NXEIPEntities model = new NXEIPEntities();

        /// <summary>
        /// 取得某一類別之資料
        /// </summary>
        /// <param name="s06_no"></param>
        /// <returns></returns>
        public IQueryable<foods> GetS06No_Data(int s06_no)
        {
            return (from d in model.foods
                    where d.foo_status == "1" && d.foo_s06no == s06_no
                    orderby d.foo_createtime descending
                    select d);
        }

        /// <summary>
        /// 查詢符合相關字串之資料
        /// </summary>
        /// <param name="s06_no"></param>
        /// <returns></returns>
        public IQueryable<foods> GetData_Search(string name)
        {
            return (from d in model.foods
                    where d.foo_status == "1" && d.foo_name.Contains(name)
                    orderby d.foo_createtime descending
                    select d);
        }

        public void AddToFoods(foods d)
        {
            model.foods.AddObject(d);
        }

        public void Update()
        {
            model.SaveChanges();
        }

    }


}