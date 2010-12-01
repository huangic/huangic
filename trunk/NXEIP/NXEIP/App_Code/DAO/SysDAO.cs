using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using Entity;
using System.ComponentModel;

namespace NXEIP.DAO
{
    /// <summary>
    /// SysDAO 的摘要描述
    /// </summary>
    [DataObject(true)]
    public class SysDAO
    {
        public SysDAO()
        {
           
        }

        private NXEIPEntities model = new NXEIPEntities();

        public IQueryable<sys> GetAll()
        {
            return (from s in model.sys orderby s.sys_order select s);
        }

        public IQueryable<sys> GetAll(int startRowIndex, int maximumRows)
        {
            return GetAll().Skip(startRowIndex).Take(maximumRows);
        }

        public int GetAllCount()
        {
            return GetAll().Count();
        }

        /// <summary>
        /// 選取使用中的系統分類
        /// </summary>
        /// <returns></returns>
        public IQueryable<sys> GetAll_2()
        {
            return (from s in model.sys where s.sys_status == "1" orderby s.sys_order select s);
        }

        

        public sys GetBySysNo(int sys_no)
        {
            return (from sysData in model.sys where sysData.sys_no == sys_no select sysData).FirstOrDefault();
        }

        public string GetNameBySysNo(int sys_no)
        {
            return (from sysData in model.sys where sysData.sys_no == sys_no select sysData.sys_name).FirstOrDefault();
        }

        public void AddSys(sys sys)
        {
            model.AddTosys(sys);
        }

        public int Update()
        {
            return model.SaveChanges();
        }


        public IQueryable<sys> GetAvailableSys(String user_login) {

            var menu = (from s in model.sys
                        from sysfunc in model.sysfuction
                        from account in model.accounts
                        from roleacc in model.roleaccount
                        from rauth in model.rauthority
                        where sysfunc.sfu_status == "1"
                        && roleacc.acc_no == account.acc_no
                        && rauth.rol_no == roleacc.rol_no
                        && sysfunc.sfu_no == rauth.sfu_no
                        && account.acc_login == user_login
                        && s.sys_no == sysfunc.sys_no
                        orderby s.sys_order
                        select s).Distinct();

            return menu;
        }
    }


}