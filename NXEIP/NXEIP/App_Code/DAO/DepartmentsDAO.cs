using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;

/// <summary>
/// DepartmentsDAO 的摘要描述
/// </summary>



namespace NXEIP.DAO{

    public partial class DepartmentsDAO
    {
        public DepartmentsDAO()
        {
          
        }


        private NXEIPEntities model = new NXEIPEntities();


        public IQueryable<departments> GetAll()
        {


            
            
            
            return (from d in model.departments where d.dep_status != "0"  orderby d.dep_no select d);

            



        }


        public IQueryable<departments> GetAll(int startRowIndex, int maximumRows)
        {


            return GetAll().Skip(startRowIndex).Take(maximumRows);



            

        }


        public int GetAllCount() {


            return GetAll().Count();

                

           
            
        }


        public void AddDepartment(departments department) {
            model.AddTodepartments(department);
            
         
        }


        public int Update() {
           
            
            return model.SaveChanges();
        }


        public departments GetByDepNo(int dep_no) {
            return (from depart in model.departments where depart.dep_no == dep_no select depart).FirstOrDefault();
        }

    }
}