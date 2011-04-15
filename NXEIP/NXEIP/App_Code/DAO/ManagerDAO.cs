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
        public List<int> GetRootManager() {
            var root = (from d in model.manager where d.man_type == "2" select d.people.peo_uid).DefaultIfEmpty().Distinct().ToList();

            return root;
        }

        public List<int> GetDepartManager(int dep_no) {
            var root = (from d in model.manager where d.man_type == "1" && d.dep_no==dep_no select d.people.peo_uid).DefaultIfEmpty().Distinct().ToList();

            return root;
        
        }



        /// <summary>
        /// Gets the manager.
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 1. Small P ,2011/4/15-下午 01:40, Created
        /// </history>
        public IQueryable<manager> GetManager() {
           return  (from d in model.manager orderby d.dep_no select d);
        }

        /// <summary>
        /// Gets the manager.
        /// </summary>
        /// <param name="startRowIndex">Start index of the row.</param>
        /// <param name="maximumRows">The maximum rows.</param>
        /// <returns></returns>
        /// <history>
        /// 1. Small P ,2011/4/15-下午 01:42, Created
        /// </history>
        public IQueryable<manager> GetManager(int startRowIndex, int maximumRows)
        {
            return GetManager().Skip(startRowIndex).Take(maximumRows);
        }


        /// <summary>
        /// Gets the manager count.
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 1. Small P ,2011/4/15-下午 01:45, Created
        /// </history>
        public int GetManagerCount() {
            return GetManager().Count();
        }

    }
}