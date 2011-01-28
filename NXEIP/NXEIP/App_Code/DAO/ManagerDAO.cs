using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using Entity;



namespace NXEIP.DAO
{

    /// <summary>
    /// ManagerDAO 的摘要描述
    /// </summary>
    [DataObject(true)]
    public class ManagerDAO
    {
        public ManagerDAO()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        private NXEIPEntities model = new NXEIPEntities();

        /// <summary>
        /// 取總管理者
        /// </summary>
        /// <returns></returns>
        public List<people> GetRootManager() {
            var root = (from d in model.manager where d.man_type == "2" select d.people).DefaultIfEmpty().Distinct().ToList();

            return root;
        }

        public List<people> GetDepartManager(int dep_no) {
            var root = (from d in model.manager where d.man_type == "1" && d.dep_no==dep_no select d.people).DefaultIfEmpty().Distinct().ToList();

            return root;
        
        }



    }
}