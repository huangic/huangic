using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using System.ComponentModel;

namespace NXEIP.DAO
{
    /// <summary>
    /// 功能名稱：rooms
    /// 功能描述：取得、設定場地資料rooms資料表的內容
    /// 撰寫者：Lina
    /// 撰寫時間：2010/09/23
    /// </summary>
    [DataObject(true)]
    public class EquipmentsDAO
    {
        public EquipmentsDAO()
        {
        }
        private NXEIPEntities model = new NXEIPEntities();

        #region 分頁列表使用
        public IQueryable<equipments> GetAll()
        {
            return (from tb in model.equipments where tb.equ_status == "1" orderby tb.spo_no,tb.equ_number, tb.equ_no select tb);
        }

        public IQueryable<equipments> GetAll(int startRowIndex, int maximumRows)
        {
            return GetAll().Skip(startRowIndex).Take(maximumRows);
        }

        public int GetAllCount()
        {
            return GetAll().Count();
        }
        #endregion

        #region 新增&修改
        public void AddEquipments(equipments equ)
        {
            model.AddToequipments(equ);
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
        public equipments GetByNo(int no)
        {
            return (from tb in model.equipments where tb.equ_no == no select tb).FirstOrDefault();
        }
        #endregion

        #region 由[編號]取得[場地]
        /// <summary>
        /// 由[編號]取得[場地]
        /// </summary>
        /// <param name="no">編號</param>
        /// <returns>場地</returns>
        public string GetNameByNo(int no)
        {
            return (from tb in model.equipments where tb.equ_no == no select tb.equ_name).FirstOrDefault();
        }
        #endregion

        #region 由[場地]取得[編號]
        /// <summary>
        /// 由[場地]取得[編號]
        /// </summary>
        /// <param name="name">場地</param>
        /// <returns>編號</returns>
        public int GetNoByName(string name)
        {
            return (from tb in model.equipments where tb.equ_name == name select tb.equ_no).FirstOrDefault();
        }
        #endregion
    }
}