using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using Entity;
using System.ComponentModel;
using System.Data.Objects;

namespace NXEIP.DAO
{
    /// <summary>
    /// OperatesDAO 的摘要描述
    /// </summary>
    [DataObject(true)]
    public class OperatesDAO
    {
        public OperatesDAO()
        {
           
        }

        private NXEIPEntities model = new NXEIPEntities();

        /// <summary>
        /// 查詢所有資料
        /// </summary>
        /// <returns></returns>
        public IQueryable<operates> GetAll()
        {
            return (from data in model.operates orderby data.ope_logintime descending select data);
        }

        /// <summary>
        /// 查詢符合筆數之資料
        /// </summary>
        /// <param name="startRowIndex">起始筆數</param>
        /// <param name="maximumRows">結束筆數</param>
        /// <returns></returns>
        public IQueryable<operates> GetAll(int startRowIndex, int maximumRows)
        {
            return GetAll().Skip(startRowIndex).Take(maximumRows);
        }

        /// <summary>
        /// 查詢總筆數
        /// </summary>
        /// <returns></returns>
        public int GetAllCount()
        {
            return GetAll().Count();
        }

        public void Addoperates(operates operates)
        {
            model.AddTooperates(operates);
        }

        public int Update()
        {
            return model.SaveChanges();
        }

        public IQueryable<operates> SearchData(string date, string sfu, string opt, string key, string value)
        {
            //日期
            DateTime sd = Convert.ToDateTime(date.Split(',')[0] + " 00:00:00.000");
            DateTime ed = Convert.ToDateTime(date.Split(',')[1] + " 23:59:59.999");

            //功能
            int[] sfuList = null;
            string[] sfuStr = sfu.Split(',');
            if (sfuStr[0] == "0" && sfuStr[1] == "0" && sfuStr[2] == "0")
            {
                sfuList = (from d in model.sysfuction
                           where d.sfu_status == "1" && d.sfu_parent != 0
                           orderby d.sys_no, d.sfu_order
                           select d.sfu_no).ToArray();
            }
            else if (sfuStr[0] != "0" && sfuStr[1] == "0" && sfuStr[2] == "0")
            {
                int sys_no = int.Parse(sfuStr[0]);
                sfuList = (from d in model.sysfuction
                           where d.sfu_status == "1" && d.sfu_parent != 0 && d.sys_no == sys_no
                           orderby d.sys_no, d.sfu_order
                           select d.sfu_no).ToArray();
            }
            else if (sfuStr[0] != "0" && sfuStr[1] != "0" && sfuStr[2] == "0")
            {
                int sys_no = int.Parse(sfuStr[0]);
                int sfu_parent = int.Parse(sfuStr[1]);
                sfuList = (from d in model.sysfuction
                           where d.sfu_status == "1" && d.sfu_parent == sfu_parent && d.sys_no == sys_no
                           orderby d.sys_no, d.sfu_order
                           select d.sfu_no).ToArray();
            }
            else
            {
                //(sfuStr[0] != "0" && sfuStr[1] != "0" && sfuStr[2] != "0")
                int sfu_no = int.Parse(sfuStr[2]);
                sfuList = (from d in model.sysfuction
                           where d.sfu_status == "1" && d.sfu_no == sfu_no
                           select d.sfu_no).ToArray();
            }

            //取資料
            var data = (from d in model.operates
                        where d.ope_logintime.Value >= sd && d.ope_logintime.Value <= ed && sfuList.Contains(d.sfu_no)
                        select d);

            //操作模式
            if (opt != "0")
            {
                int fuc = int.Parse(opt);
                data = data.Where(o => o.ope_fuction.Value == fuc);
            }

            //人員
            int[] peo_uid = null;
            if (key == "2")
            {
                peo_uid = (from d in model.people where d.peo_workid == value select d.peo_uid).ToArray();   
            }
            if (key == "3")
            {
                peo_uid = (from d in model.people where d.peo_account == value select d.peo_uid).ToArray();
            }
            if (key == "4")
            {
                peo_uid = value.Split(',').Select(x => int.Parse(x)).ToArray();
            }
            if (key == "5")
            {
                int[] dep_no = value.Split(',').Select(x=>int.Parse(x)).ToArray();
                
                peo_uid = (from d in model.people
                           where dep_no.Contains(d.dep_no) && d.peo_jobtype == 1
                           select d.peo_uid).ToArray();
            }
            if (peo_uid != null)
            {
                data = data.Where(o => peo_uid.Contains(o.peo_uid));
            }

            //排序
            data = data.OrderBy(o => o.sfu_no).OrderByDescending(o=>o.ope_logintime).OrderBy(o=>o.ope_fuction);
            return data;
        }

        public IQueryable<operates> SearchData(string date, string sfu, string opt, string key, string value, int startRowIndex, int maximumRows)
        {
            return this.SearchData(date, sfu, opt, key, value).Skip(startRowIndex).Take(maximumRows);
        }

        public int SearchDataCount(string date, string sfu, string opt, string key, string value)
        {
            return this.SearchData(date, sfu, opt, key, value).Count();
        }
       
    }
}