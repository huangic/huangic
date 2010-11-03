using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using System.Linq.Dynamic;
using System.ComponentModel;

namespace NXEIP.DAO
{
    /// <summary>
    /// ArgumentsDAO 的摘要描述
    /// </summary>
    [DataObject(true)]
    public class ArgumentsDAO
    {
        public ArgumentsDAO()
        {
           
        }

        private NXEIPEntities model = new NXEIPEntities();

        /// <summary>
        /// 查詢所有資料
        /// </summary>
        /// <returns></returns>
        public IQueryable<arguments> GetAll()
        {
            return (from data in model.arguments orderby data.arg_no select data);
        }

        /// <summary>
        /// 查詢符合筆數之資料
        /// </summary>
        /// <param name="startRowIndex">起始筆數</param>
        /// <param name="maximumRows">結束筆數</param>
        /// <returns></returns>
        public IQueryable<arguments> GetAll(int startRowIndex, int maximumRows)
        {
            return GetAll().Skip(startRowIndex).Take(maximumRows);
        }

        /// <summary>
        /// 查詢符合arg_no欄位的資料
        /// </summary>
        /// <param name="arg_no"></param>
        /// <returns></returns>
        public arguments GetByArgNo(int arg_no)
        {
            return (from data in model.arguments where data.arg_no == arg_no select data).FirstOrDefault();
        }

        /// <summary>
        /// 查詢欄位arg_describe值符合字串之資料
        /// </summary>
        /// <param name="str">欲查詢之字串</param>
        /// <returns></returns>
        public IQueryable<arguments> GetBySearch(string str)
        {
            return (from data in model.arguments where data.arg_describe.Contains(str) select data);
        }

        public IQueryable<arguments> GetBySearch(string str, int startRowIndex, int maximumRows)
        {
            return GetBySearch(str).Skip(startRowIndex).Take(maximumRows);
        }

        /// <summary>
        /// 查詢是否有相同arg_var之資料
        /// </summary>
        /// <param name="arg_var"></param>
        /// <returns></returns>
        public int GetByCheck(string arg_var)
        {
            return (from data in model.arguments where data.arg_variable == arg_var select data).Count();
        }

        public string GetValueByVariable(string arg_variable)
        {
            return (from data in model.arguments where data.arg_variable == arg_variable select data.arg_value).FirstOrDefault();
        }

        public int GetAllCount()
        {
            return GetAll().Count();
        }

        public void AddArguments(arguments arguments)
        {
            model.AddToarguments(arguments);
        }

        public void DeleteArguments(arguments arguments)
        {
            model.arguments.DeleteObject(arguments);
        }

        public int Update()
        {
            return model.SaveChanges();
        }
    }



}