using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using System.ComponentModel;

/// <summary>
/// DepartmentsDAO 的摘要描述
/// </summary>



namespace NXEIP.DAO{

    [DataObject(true)]
    public partial class DepartmentsDAO
    {
        public DepartmentsDAO()
        {
          
        }


        private NXEIPEntities model = new NXEIPEntities();


        public IQueryable<departments> GetAll()
        {

            return (from d in model.departments where d.dep_status == "1"  orderby d.dep_no select d);
        }


        public IQueryable<departments> GetAll(int startRowIndex, int maximumRows)
        {
            return GetAll().Skip(startRowIndex).Take(maximumRows);
        }


        public int GetAllCount() 
        {
            return GetAll().Count();
        }


        public void AddDepartment(departments department) 
        {
            model.AddTodepartments(department);
        }


        public int Update() 
        {
            return model.SaveChanges();
        }


        public departments GetByDepNo(int dep_no) 
        {
            return (from depart in model.departments where depart.dep_no == dep_no select depart).FirstOrDefault();
        }

        /// <summary>
        /// 取遞迴父帶目錄(有Detach)
        /// </summary>
        /// <param name="dep_id">目錄標號</param>
        /// <returns></returns>
        public ICollection<departments> GetRecursiveParentDeprtment(int dep_id)
        {
            ICollection<departments> deparCollection = new LinkedList<departments>();
            //取自己的目錄
            departments dep=null;
            int depNo=dep_id;

            while (dep == null || dep.dep_parentid.Value != 0) {
                dep = (from d in model.departments where d.dep_no == depNo select d).First();
                depNo = dep.dep_parentid.Value;
                model.Detach(dep);

                deparCollection.Add(dep);
            }
            



            return deparCollection;
        }

       
        
    }
}