﻿using System;
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

        #region 課程類別
        /// <summary>
        /// 查詢課程類別資料
        /// </summary>
        /// <param name="typ_parent">父層代碼</param>
        /// <returns></returns>
        public IQueryable<types> GetClassData(int typ_parent)
        {
            return (from t in model.types
                    where t.typ_status == "1" && t.typ_code == "class" && t.typ_parent == typ_parent
                    orderby t.typ_order
                    select t);
        }

        /// <summary>
        /// 查詢筆數
        /// </summary>
        /// <param name="typ_parent"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public IQueryable<types> GetClassData(int typ_parent,int startRowIndex, int maximumRows)
        {
            return GetClassData(typ_parent).Skip(startRowIndex).Take(maximumRows);
        }

        /// <summary>
        /// 課程類別
        /// </summary>
        /// <param name="typ_parent"></param>
        /// <returns></returns>
        public int GetClassDataCount(int typ_parent)
        {
            return GetClassData(typ_parent).Count();
        }
        #endregion

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