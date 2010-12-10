using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using System.ComponentModel;

namespace NXEIP.DAO
{
    /// <summary>
    /// 功能名稱：petition
    /// 功能描述：取得、設定場地申請單
    /// 撰寫者：Lina
    /// 撰寫時間：2010/11/11
    /// </summary>
    [DataObject(true)]
    public class PetitionDAO
    {
        public PetitionDAO()
        {
        }

        private NXEIPEntities model = new NXEIPEntities();

        #region 分頁列表使用
        public IQueryable<petition> GetAll()
        {
            return (from tb in model.petition where tb.pet_apply=="1" || tb.pet_apply=="2" orderby tb.pet_stime, tb.pet_etime select tb);
        }

        public IQueryable<petition> GetAll(int startRowIndex, int maximumRows)
        {
            return GetAll().Skip(startRowIndex).Take(maximumRows);
        }

        public int GetAllCount()
        {
            return GetAll().Count();
        }
        #endregion

        #region 新增&修改
        public void AddPetition(petition rowdata)
        {
            model.AddTopetition(rowdata);
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
        public petition GetByPetNo(int no)
        {
            return (from tb in model.petition where tb.pet_no == no select tb).FirstOrDefault();
        }
        #endregion
    }
}