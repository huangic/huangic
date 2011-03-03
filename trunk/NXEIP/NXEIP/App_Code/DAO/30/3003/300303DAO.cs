﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using Entity;
using System.ComponentModel;

namespace NXEIP.DAO
{
    /// <summary>
    /// _300303DAO 的摘要描述
    /// </summary>
    [DataObject(true)]
    public class _300303DAO
    {
        private NXEIPEntities model = new NXEIPEntities();

        public _300303DAO()
        {

        }

        /// <summary>
        /// 依據條件查詢資料
        /// </summary>
        /// <returns></returns>
        public IQueryable<e02> GetData(string sdate, string edate, string type_1, string type_2, string e02_place, string e02_name, int? openuid)
        {
            DateTime sd = Convert.ToDateTime(sdate + " 00:00:00");
            

            var d = (from data in model.e02
                     where data.e02_status == "1"
                     select data);

            if (string.IsNullOrEmpty(edate))
            {
                d = d.Where(o => o.e02_sdate >= sd);
            }
            else
            {
                DateTime ed = Convert.ToDateTime(edate + " 23:59:59");
                d = d.Where(o => o.e02_sdate >= sd && o.e02_sdate <= ed);
            }
            
            //課程父類別
            if (type_1 != "0" && type_2.Equals("0"))
            {
                int typ_parent = Convert.ToInt32(type_1);
                int[] tdata = (from t in model.types where t.typ_parent == typ_parent select t.typ_no).ToArray();
                d.Where(o => tdata.Contains(o.typ_no));
            }

            //課程子類別
            if (type_2 != "0")
            {
                //條件值
                d = d.Where("typ_no = @0", Convert.ToInt32(type_2));
            }

            //上課地點
            if (!string.IsNullOrEmpty(e02_place))
            {
                //條件值
                d = d.Where(o => o.e02_place.Contains(e02_place));
            }

            //課程名稱e02_name
            if (e02_name != null && e02_name != "")
            {
                //條件值
                d = d.Where(o => o.e02_name.Contains(e02_name));
            }

            //建立人
            if (openuid.HasValue)
            {
                d = d.Where("e02_openuid = @0", openuid.Value);
            }
            
            //排序
            d = d.OrderBy(o => o.e02_sdate);

            return d;
        }

        /// <summary>
        /// 查詢符合筆數之資料
        /// </summary>
        /// <param name="startRowIndex">起始筆數</param>
        /// <param name="maximumRows">結束筆數</param>
        /// <returns></returns>
        public IQueryable<e02> GetData(string sdate, string edate, string type_1, string type_2, string e02_place, string e02_name, int? openuid, int startRowIndex, int maximumRows)
        {
            return GetData(sdate, edate, type_1, type_2, e02_place, e02_name, openuid).Skip(startRowIndex).Take(maximumRows);
        }

        /// <summary>
        /// 查詢總筆數
        /// </summary>
        /// <returns></returns>
        public int GetDataCount(string sdate, string edate, string type_1, string type_2, string e02_place, string e02_name, int? openuid)
        {
            return GetData(sdate, edate, type_1, type_2, e02_place, e02_name, openuid).Count();
        }

        /// <summary>
        /// 查詢所有資料
        /// </summary>
        /// <param name="e02_no"></param>
        /// <returns></returns>
        public IQueryable<e04> Get_e04Data(int e02_no)
        {
            //未審核,審核通過,審核未通過
            string[] check = { "0", "1", "2" };
            return (from d in model.e04 where d.e02_no == e02_no && check.Contains(d.e04_check) orderby d.e04_check,d.e04_depno,d.e04_applydate select d);
        }

        public IQueryable<e04> Get_e04Data(int e02_no, int startRowIndex, int maximumRows)
        {
            return Get_e04Data(e02_no).Skip(startRowIndex).Take(maximumRows);
        }

        public int Get_e04DataCount(int e02_no)
        {
            return Get_e04Data(e02_no).Count();
        }

        public IQueryable<e04> Get_e04Data_2(int e02_no)
        {
            //未審核,審核通過
            string[] check = { "0", "1" };
            return (from d in model.e04 where d.e02_no == e02_no && check.Contains(d.e04_check) orderby d.e04_depno, d.e04_applydate select d);
        }

        public IQueryable<e04> Get_e04Data_2(int e02_no, int startRowIndex, int maximumRows)
        {
            return Get_e04Data_2(e02_no).Skip(startRowIndex).Take(maximumRows);
        }

        public int Get_e04DataCount_2(int e02_no)
        {
            return Get_e04Data_2(e02_no).Count();
        }

        /// <summary>
        /// 查詢已審核資料
        /// </summary>
        /// <param name="e02_no"></param>
        /// <returns></returns>
        public IQueryable<e04> Get_e04Data_pass(int e02_no)
        {
            return (from d in model.e04 where d.e02_no == e02_no && d.e04_check == "1" orderby d.e04_check, d.e04_depno, d.e04_applydate select d);
        }

        public IQueryable<e04> Get_e04Data_pass(int e02_no, int startRowIndex, int maximumRows)
        {
            return Get_e04Data_pass(e02_no).Skip(startRowIndex).Take(maximumRows);
        }

        public int Get_e04DataCount_pass(int e02_no)
        {
            return Get_e04Data_pass(e02_no).Count();
        }

        /// <summary>
        /// 查詢符合條件資料
        /// </summary>
        /// <param name="e01_no"></param>
        /// <returns></returns>
        public e02 GetBye01NO(int e02_no)
        {
            return (from d in model.e02 where d.e02_no == e02_no select d).FirstOrDefault();
        }

        public void Adde02(e02 e02)
        {
            model.AddToe02(e02);
        }

        public int Update()
        {
            return model.SaveChanges();
        }
    }

}