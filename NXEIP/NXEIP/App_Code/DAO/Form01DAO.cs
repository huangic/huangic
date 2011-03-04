using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using System.ComponentModel;
using NXEIP.DynamicForm;
using Newtonsoft.Json;



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
            return from d in model.form01 where d.peo_uid == peo_uid && d.f01_status != "3" select d;
        }


        #region 取所有表單

        /// <summary>
        /// 取所有狀態不為刪除的表單
        /// </summary>
        /// <returns></returns>
        public IQueryable<form01> GetAll()
        {
            return from d in model.form01 where d.f01_status != "3" orderby d.f01_no select d;
        }

        public int GetAllCount()
        {
            return GetAll().Count();
        }


        public IQueryable<form01> GetAll(int startRowIndex, int maximumRows)
        {
            return GetAll().Skip(startRowIndex).Take(maximumRows);
        }


        #endregion




        #region get all status=1 form 

        /// <summary>
        /// 取表單
        /// </summary>
        /// <param name="keyword">關鍵字</param>
        /// <returns></returns>
        public IQueryable<form01> GetForm(String keyword)
        {
            IQueryable<form01> f = from d in model.form01 where d.f01_status == "1" orderby d.f01_no select d;


            if (!String.IsNullOrEmpty(keyword)) {
                f = f.Where(x => x.f01_name.Contains(keyword));
            }

            return f;
        }

        public IQueryable<form01> GetForm(String keyword,int startRowIndex, int maximumRows)
        {
            return GetForm(keyword).Skip(startRowIndex).Take(maximumRows);
        }


        public int GetFormCount(String keyword)
        {
            return GetForm(keyword).Count();
        }
        #endregion




        #region get Column Object from Form_no
        public IQueryable<Column> GetColumnsByFormNO(int f01_no)
        {
            //字串轉型
            string json = (from d in model.form01 where d.f01_no == f01_no select d.f01_columns).First();

            if (!String.IsNullOrEmpty(json))
            {

                return (IQueryable<Column>)(JsonConvert.DeserializeObject<List<Column>>(json)).AsQueryable();
            }
            return new List<Column>().AsQueryable();
        }

        #endregion



        #region get footer column
        public IQueryable<Column> GetFooterByFormNO(int f01_no)
        {
            //字串轉型
            string json = (from d in model.form01 where d.f01_no == f01_no select d.f01_footer).First();

            if (!String.IsNullOrEmpty(json))
            {

                return (IQueryable<Column>)(JsonConvert.DeserializeObject<List<Column>>(json)).AsQueryable();
            }
            return new List<Column>().AsQueryable();
        }
        #endregion



        #region 取所有表單(加入人員與條件)

        /// <summary>
        /// 取所有狀態不為刪除的表單
        /// </summary>
        /// <returns></returns>
        public IQueryable<form01> GetFormByPeoAndKeyword(int peouid,string keyword)
        {
            IQueryable<form01> f = (from d in model.form01
                                    where
                                     d.f01_status != "3"
                                      && d.peo_uid == peouid
                                     orderby d.f01_no
                                       select d);
            if (!String.IsNullOrEmpty(keyword)) {
                f=f.Where(x => x.f01_name.Contains(keyword));
            }
            
            
            return f;
        }

        public int GetFormByPeoAndKeywordCount(int peouid, string keyword)
        {
            return GetFormByPeoAndKeyword(peouid,keyword).Count();
        }


        public IQueryable<form01> GetFormByPeoAndKeyword(int peouid,string keyword,int startRowIndex, int maximumRows)
        {
            return GetFormByPeoAndKeyword(peouid, keyword).Skip(startRowIndex).Take(maximumRows);
        }


        #endregion


        /// <summary>
        /// 取最新的表單
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public IQueryable<form01> GetNewForm(int num) {

            return (from d in model.form01 where d.f01_status == "1" orderby d.f01_createtime select d).Take(num);
        }


    }
}