using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using Entity;
using System.ComponentModel;

namespace NXEIP.DAO
{
    /// <summary>
    /// e04DAO 的摘要描述
    /// </summary>
    [DataObject(true)]
    public class e04DAO
    {
        private NXEIPEntities model = new NXEIPEntities();

        public e04DAO()
        {
           
        }

        public IQueryable<e02> GetPeopleData(string sdate, string edate, string type_1, string type_2, string e01_no, string e02_name, int peo_uid)
        {
            DateTime sd = Convert.ToDateTime(sdate + " 00:00:00");
            DateTime ed = Convert.ToDateTime(edate + " 23:59:59");

            var d = (from data in model.e02
                     where data.e02_status == "1" && data.e02_sdate >= sd && data.e02_edate <= ed
                     select data);

            //課程父類別
            if (type_1 != "0" && type_2.Equals("0"))
            {
                int typ_parent = Convert.ToInt32(type_1);
                int[] tdata = (from t in model.types where t.typ_parent == typ_parent select t.typ_no).ToArray();
                d.Where(o => tdata.Contains(o.typ_no));
            }

            //課程子類別
            if (type_2 != "0")
            {
                //條件值
                d = d.Where("typ_no = @0", Convert.ToInt32(type_2));
            }

            //上課地點
            if (e01_no != null && e01_no != "0")
            {
                //條件值
                d = d.Where("e01_no = @0", Convert.ToInt32(e01_no));
            }

            //課程名稱e02_name
            if (e02_name != null && e02_name != "")
            {
                //條件值
                d = d.Where(o => o.e02_name.Contains(e02_name));
            }

            //排序
            //d = d.OrderByDescending(o => o.e02_signdate);
            //取得符合條件課程之ID
            int[] e02_no = (from x in d select x.e02_no).ToArray();

            //取得符合課程之人員報名資料之課程ID
            string[] check = { "0","1"};
            int[] user_e02no = (from e04D in model.e04 where e02_no.Contains(e04D.e02_no) && check.Contains(e04D.e04_check) && e04D.e04_peouid == peo_uid select e04D.e02_no).ToArray();

            //取回課課資料
            var e02Data = (from dd in model.e02 where user_e02no.Contains(dd.e02_no) orderby dd.e02_signdate select dd);

            return e02Data;
        }

        public IQueryable<e02> GetPeopleData(string sdate, string edate, string type_1, string type_2, string e01_no, string e02_name, int peo_uid, int startRowIndex, int maximumRows)
        {
            return GetPeopleData(sdate, edate, type_1, type_2, e01_no, e02_name, peo_uid).Skip(startRowIndex).Take(maximumRows);
        }

        public int GetPeopleDataCount(string sdate, string edate, string type_1, string type_2, string e01_no, string e02_name, int peo_uid)
        {
            return GetPeopleData(sdate, edate, type_1, type_2, e01_no, e02_name, peo_uid).Count();
        }
    }

    
}