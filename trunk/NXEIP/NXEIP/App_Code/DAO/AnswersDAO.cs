using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using System.ComponentModel;
using System.Data;

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

        #region 取得該答案對應的分數
        /// <summary>
        /// 取得該答案對應的分數
        /// </summary>
        /// <param name="que_no">問卷編號</param>
        /// <param name="the_no">題目編號</param>
        /// <param name="ans_no">答案編號</param>
        /// <returns>分數值</returns>
        public int GetFraction(int que_no,int the_no,int ans_no)
        {
            return (from tb in model.answers where tb.que_no == que_no && tb.the_no == the_no && tb.ans_no == ans_no select tb.ans_fraction).FirstOrDefault().Value;
        }
        #endregion



        #region 抓出來的結構
        public class CountAnswers
        {
            public int que_no { get; set; }
            public int the_no { get; set; }
            public int ans_no { get; set; }
            public string ans_name  { get; set; }
            public int ans_order { get; set; }
            public string the_type { get; set; }
            public int acount
            {
                get
                {
                    int qcount = 0;
                    if (the_type.Equals("1"))
                    {
                        string sqlstr = "SELECT COUNT(cas_no) AS recount FROM casework WHERE (que_no = " + que_no + ") AND (the_no = " + the_no + ") and (cas_answer='" + ans_no + "')";
                        DataTable dt = new DataTable();
                        dt = new DBObject().ExecuteQuery(sqlstr);
                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Rows[0]["recount"].ToString().Length > 0) qcount = Convert.ToInt32(dt.Rows[0]["recount"].ToString());
                        }
                        else
                            qcount = 0;
                    }
                    else if (the_type.Equals("2"))
                    {
                        string sqlstr = "SELECT COUNT(cas_no) AS recount FROM casework WHERE (que_no = " + que_no + ") AND (the_no = " + the_no + ") and (cas_answer like '" + ans_no + ",%' or cas_answer like '%," + ans_no + "' or cas_answer like '%," + ans_no + ",%' or cas_answer='" + ans_no + "')";
                        DataTable dt = new DataTable();
                        dt = new DBObject().ExecuteQuery(sqlstr);
                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Rows[0]["recount"].ToString().Length > 0) qcount = Convert.ToInt32(dt.Rows[0]["recount"].ToString());
                        }
                        else
                            qcount = 0;
                    }
                    else
                    {
                        qcount = 0;
                    }
                    return qcount;
                }
                set
                {
                    acount = value;
                }
                
            }

        }
        #endregion

        public IQueryable<CountAnswers> GetAnswersAll(int que_no,int the_no)
        {
            var itemColl = from tb in model.answers
                           where tb.que_no == que_no && tb.the_no == the_no
                           orderby tb.ans_order
                           select new CountAnswers
                           {
                               que_no = tb.que_no,
                               the_no = tb.the_no,
                               ans_no = tb.ans_no,
                               ans_name = tb.ans_name,
                               ans_order = tb.ans_order.Value,
                               the_type = tb.theme.the_type
                           };
            return itemColl;
        }

        public IQueryable<CountAnswers> GetAnswersAll(int que_no, int the_no, int startRowIndex, int maximumRows)
        {
            return GetAnswersAll(que_no, the_no).Skip(startRowIndex).Take(maximumRows);
        }

        public int GetAnswersAllCount(int que_no, int the_no)
        {
            return GetAnswersAll(que_no, the_no).Count();
        }
    }
}