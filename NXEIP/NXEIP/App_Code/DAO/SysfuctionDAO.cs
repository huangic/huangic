﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using System.Linq.Dynamic;
using System.ComponentModel;

namespace NXEIP.DAO
{
    /// <summary>
    /// SysfuctionDAO 的摘要描述
    /// </summary>
    [DataObject(true)]
    public class SysfuctionDAO
    {
        public SysfuctionDAO()
        {
            
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

        public int GetAllCount()
        {
            return GetAll().Count();
        }

        public IQueryable<sysfuction> GetSubBySysNo(int sys_no)
        {
            return (from s in model.sysfuction
                    where s.sys_no == sys_no && s.sfu_parent == 0
                    orderby s.sfu_no
                    select s);
        }

        //使用中的功能
        public IQueryable<sysfuction> GetUse_sfuParent(int sys_no)
        {
            return (from s in model.sysfuction
                    where s.sys_no == sys_no && s.sfu_parent == 0 && s.sfu_status == "1"
                    orderby s.sfu_no
                    select s);
        }

        //使用中的子功能
        public IQueryable<sysfuction> GetUse_sfu(int sfu_no)
        {
            return (from s in model.sysfuction
                    where s.sfu_parent == sfu_no && s.sfu_status == "1"
                    orderby s.sfu_no
                    select s);
        }

        #region 類別管理

        public IQueryable<sysfuction> GetOpenData()
        {
            return (from s in model.sysfuction
                    where s.sys_open == "1"
                    orderby s.sys_no, s.sfu_no, s.sfu_order
                    select s);
        }

        public IQueryable<sysfuction> GetOpenData(int startRowIndex, int maximumRows)
        {
            return GetOpenData().Skip(startRowIndex).Take(maximumRows);
        }

        public int GetOpenDataCount()
        {
            return GetOpenData().Count();
        }

        #endregion

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

        public string GetNameByNO(int sfu_no)
        {
            return (from d in model.sysfuction where d.sfu_no == sfu_no select d.sfu_name).FirstOrDefault();
        }

       
    }
}