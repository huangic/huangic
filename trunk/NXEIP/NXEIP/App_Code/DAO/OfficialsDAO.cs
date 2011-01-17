using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using Entity;


namespace NXEIP.DAO
{
    /// <summary>
    /// AlbumDAO 的摘要描述
    /// </summary>
    [DataObject]
    public class OfficialsDAO
    {

        private NXEIPEntities model = new NXEIPEntities();


        public OfficialsDAO()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        /// <summary>
        /// 取所有的公務電話
        /// </summary>
        /// <returns></returns>
        public IQueryable<officials> GetOfficials(String off_type,String keyword){
            IQueryable<officials> official = from d in model.officials where d.off_status == "1"select d;

            if (!String.IsNullOrEmpty(off_type)) {
                official = official.Where(x => x.off_type == off_type);
            }

            if (!String.IsNullOrEmpty(keyword)) {
                official = official.Where(x => x.off_name.Contains(keyword));
            }


            official=official.OrderBy(x => x.off_createtime).OrderBy(x => x.off_type);

            return official;
        }


        public int GetOfficialsCount(String off_type, String keyword)
        {
            return GetOfficials(off_type,keyword).Count();
        }


        public IQueryable<officials> GetOfficials(String off_type, String keyword, int startRowIndex, int maximumRows)
        {
            return this.GetOfficials(off_type, keyword).Skip(startRowIndex).Take(maximumRows);
        }

    }
}