using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using System.ComponentModel;

namespace NXEIP.DAO
{
    /// <summary>
    /// 功能名稱：Questionary
    /// 功能描述：
    /// 撰寫者：Lina
    /// 撰寫時間：2010/12/20
    /// </summary>
    [DataObject(true)]
    public class QuestionaryDAO
    {
        public QuestionaryDAO()
        {
           
        }

        private NXEIPEntities model = new NXEIPEntities();

        #region 分頁列表使用
        public IQueryable<questionary> GetAll()
        {
         
            return (from tb in model.questionary where tb.que_status !="3" orderby tb.que_no select tb);
        }

        public IQueryable<questionary> GetAll(int startRowIndex, int maximumRows)
        {
            return GetAll().Skip(startRowIndex).Take(maximumRows);
        }

        public int GetAllCount()
        {
            return GetAll().Count();
        }
        #endregion

        #region 新增&修改
        public void AddQuestionary(questionary tb)
        {
            model.AddToquestionary(tb);
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
        /// <param name="no">編號</param>
        /// <returns>整筆資料</returns>
        public questionary GetByNo(int no)
        {
            return (from tb in model.questionary where tb.que_no == no select tb).FirstOrDefault();
        }
        #endregion
    }
}