using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using System.ComponentModel;

namespace NXEIP.DAO
{
    /// <summary>
    /// 功能名稱：M01
    /// 功能描述：
    /// 撰寫者：Lina
    /// 撰寫時間：2011/03/09
    /// </summary>
    [DataObject(true)]
    public class M01DAO
    {
        public M01DAO()
        {
        }

        private NXEIPEntities model = new NXEIPEntities();

        #region 分頁列表使用
        public IQueryable<m01> GetAll(string number)
        {
            return (from tb in model.m01 where tb.m01_status == "1" && tb.m01_number==number orderby tb.m01_code select tb);
        }

        public IQueryable<m01> GetAll(string number,int startRowIndex, int maximumRows)
        {
            return GetAll(number).Skip(startRowIndex).Take(maximumRows);
        }

        public int GetAllCount(string number)
        {
            return GetAll(number).Count();
        }
        #endregion

        #region 新增&修改
        public void AddM01(m01 tb)
        {
            model.AddTom01(tb);
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
        public m01 GetByNo(int no)
        {
            return (from tb in model.m01 where tb.m01_no == no select tb).FirstOrDefault();
        }
        #endregion

        #region 由[編號]取得[資料]
        /// <summary>
        /// 由[編號]取得[資料]
        /// </summary>
        /// <param name="no">編號</param>
        /// <returns>整筆資料</returns>
        public string GetNameByNo(int no)
        {
            return (from tb in model.m01 where tb.m01_no == no select tb.m01_name).FirstOrDefault();
        }
        #endregion


        #region 由[類別、代碼]取得[資料]
        public m01 GetByNumberCode(string number, string code)
        {
            return (from tb in model.m01 where tb.m01_number == number && tb.m01_code == code && tb.m01_status == "1" select tb).FirstOrDefault();
        }
        #endregion

        #region 由[類別、代碼]取得[數量]
        public int GetByNumberCodeCount(string number, string code,int no)
        {
            return (from tb in model.m01 where tb.m01_number == number && tb.m01_code == code && tb.m01_status == "1" && tb.m01_no!=no select tb).Count();
        }
        #endregion

        public string GetNumberName(string number)
        {
            string feedback="";

            if (number.Equals("platoon"))
                feedback = "排照種類";
            else if (number.Equals("chekuan"))
                feedback = "車別";
            else if (number.Equals("mark"))
                feedback = "車輛廠牌";
            else if (number.Equals("color"))
                feedback = "車輛顏色";
            else if (number.Equals("source"))
                feedback = "來源";
            else if (number.Equals("factory"))
                feedback = "廠商";
            else if (number.Equals("energy"))
                feedback = "能源種類";

            return feedback;
        }
    }
}