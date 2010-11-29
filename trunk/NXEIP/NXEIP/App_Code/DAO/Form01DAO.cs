﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using System.ComponentModel;



namespace NXEIP.DAO
{
    /// <summary>
    /// Form01DAO 的摘要描述
    /// </summary>
    [DataObject(true)]
    public class Form01DAO
    {
        public Form01DAO()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        NXEIPEntities model = new NXEIPEntities();

        public IQueryable<form01> GetDataByPeoUid(int peo_uid){
            return from d in model.form01 where d.peo_uid == peo_uid && d.f01_status != "2" select d;
        }


        public IQueryable<form01> GetAll()
        {
            return from d in model.form01 where d.f01_status != "2" orderby d.f01_no select d;
        }

        public int GetAllCount() {
            return GetAll().Count();
        }

        public IQueryable<form01> GetAll(int startRowIndex, int maximumRows)
        {
            return GetAll().Skip(startRowIndex).Take(maximumRows);
        }
    
    }
}