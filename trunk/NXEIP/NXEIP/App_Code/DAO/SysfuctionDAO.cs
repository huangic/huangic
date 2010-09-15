using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using System.Linq.Dynamic;

namespace NXEIP.DAO
{
    /// <summary>
    /// SysfuctionDAO 的摘要描述
    /// </summary>
    public class SysfuctionDAO
    {
        public SysfuctionDAO()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        private NXEIPEntities model = new NXEIPEntities();

        public IQueryable<sysfuction> GetAll()
        {
            return (from s in model.sysfuction where s.sfu_parent > 0 orderby s.sys_no, s.sfu_no, s.sfu_order select s);
        }

        public IQueryable<sysfuction> GetAll(int startRowIndex, int maximumRows)
        {
            return GetAll().Skip(startRowIndex).Take(maximumRows);
        }

        public IQueryable<sysfuction> GetSubBySysNo(int sys_no)
        {
            return (from s in model.sysfuction where s.sys_no == sys_no && s.sfu_parent == 0 orderby s.sfu_no select s);
        }

        public int GetAllCount()
        {
            return GetAll().Count();
        }

        public sysfuction GetBySfuNo(int sfu_no)
        {
            return (from sysData in model.sysfuction where sysData.sfu_no == sfu_no select sysData).FirstOrDefault();
        }

        public void AddSysfuction(sysfuction sysfuction)
        {
            model.AddTosysfuction(sysfuction);
        }

        public int Update()
        {
            return model.SaveChanges();
        }
    }
}