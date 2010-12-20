using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using System.ComponentModel;

namespace NXEIP.DAO
{
    /// <summary>
    /// 功能名稱：Theme
    /// 功能描述：
    /// 撰寫者：Lina
    /// 撰寫時間：2010/12/20
    /// </summary>
    [DataObject(true)]
    public class AnswersDAO
    {
        public AnswersDAO()
        {

        }

        private NXEIPEntities model = new NXEIPEntities();

        #region 分頁列表使用
        public IQueryable<answers> GetAll(int que_no,int the_no)
        {
            return (from tb in model.answers where tb.que_no == que_no && tb.the_no==the_no && tb.ans_status == "1" orderby tb.ans_order select tb);
        }

        public IQueryable<answers> GetAll(int que_no, int the_no, int startRowIndex, int maximumRows)
        {
            return GetAll(que_no, the_no).Skip(startRowIndex).Take(maximumRows);
        }

        public int GetAllCount(int que_no, int the_no)
        {
            return GetAll(que_no, the_no).Count();
        }
        #endregion

        #region 新增&修改
        public void AddAnswers(answers tb)
        {
            model.AddToanswers(tb);
        }

        public int Update()
        {
            return model.SaveChanges();
        }
        #endregion

        #region 由[編號]取得[資料]
        /// <summary>
        /// 由[編號]取得[資料]
        /// </summary>
        /// <param name="no1">que_no</param>
        /// <param name="no2">the_no</param>
        /// <param name="no3">ans_no</param>
        /// <returns>整筆資料</returns>
        public answers GetByNo(int no1, int no2,int no3)
        {
            return (from tb in model.answers where tb.que_no == no1 && tb.the_no == no2 && tb.ans_no==no3 select tb).FirstOrDefault();
        }
        #endregion

        #region 取得該問卷題目答案編號最大值
        /// <summary>
        /// 由[que_no,the_no]取得[ans_no]最大值
        /// </summary>
        /// <param name="que_no">que_no</param>
        /// <param name="the_no">the_no</param>
        /// <returns></returns>
        public int GetMaxAnsNo(int que_no,int the_no)
        {
            return (from tb in model.answers where tb.que_no == que_no && tb.the_no==the_no orderby tb.ans_no select tb.ans_no).DefaultIfEmpty().Max();
        }
        #endregion
    }
}