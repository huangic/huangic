using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Globalization;


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


        //public IQueryable<petition> GetAll(string sdate, string edate, string status, string spot, string rooms)
        //{
        //    DateTime sd = Convert.ToDateTime(sdate + " 00:00:00");
        //    DateTime ed = Convert.ToDateTime(edate + " 23:59:59");
        //    var d;
        //    if (status.Equals("0"))
        //    {
        //        d = (from tb in model.petition where (tb.pet_apply == "1" || tb.pet_apply == "2") && tb.pet_stime >= sd && tb.pet_etime <= ed
        //             select tb);
        //    }
        //    else
        //    {
        //        d = (from tb in model.petition where (tb.pet_apply == status) && tb.pet_stime >= sd && tb.pet_etime <= ed
        //             select tb);
        //    }

        //    //rooms
        //    if (rooms != "0")
        //    {
        //        d = d.Where("roo_no = @0", Convert.ToInt32(rooms));

        //    }
        //    else if (spot != "0")
        //    {
        //        int typ_parent = Convert.ToInt32(spot);
        //        int[] tdata = (from t in model.rooms where t.spot == typ_parent select t.roo_no).ToArray();
        //        d.Where(o => tdata.Contains(o.typ_no));
        //    }

        //    //排序
        //    //d = d.OrderByDescending(o => o.e02_signdate);
        //    //取得符合條件課程之ID
        //    int[] e02_no = (from x in d select x.e02_no).ToArray();

        //    //取得符合課程之人員報名資料之課程ID
        //    string[] check = { "0", "1" };
        //    int[] user_e02no = (from e04D in model.e04 where e02_no.Contains(e04D.e02_no) && check.Contains(e04D.e04_check) && e04D.e04_peouid == peo_uid select e04D.e02_no).ToArray();

        //    //取回課課資料
        //    var e02Data = (from dd in model.e02 where user_e02no.Contains(dd.e02_no) orderby dd.e02_signdate select dd);

        //    return e02Data;
        //}

        //public IQueryable<e02> GetPeopleData(string sdate, string edate, string type_1, string type_2, string e01_no, string e02_name, int peo_uid, int startRowIndex, int maximumRows)
        //{
        //    return GetPeopleData(sdate, edate, type_1, type_2, e01_no, e02_name, peo_uid).Skip(startRowIndex).Take(maximumRows);
        //}

        //public int GetPeopleDataCount(string sdate, string edate, string type_1, string type_2, string e01_no, string e02_name, int peo_uid)
        //{
        //    return GetPeopleData(sdate, edate, type_1, type_2, e01_no, e02_name, peo_uid).Count();
        //}



        #region 分頁列表使用
        public IQueryable<petition> GetAll()
        {

            //DataTable contacts = ds.Tables["Contact"];
            //DataTable orders = ds.Tables["SalesOrderHeader"];

            //var query =
            //    from contact in contacts.AsEnumerable()
            //    join order in orders.AsEnumerable()
            //    on contact.Field<Int32>("ContactID") equals
            //    order.Field<Int32>("ContactID")
            //    select new
            //    {
            //        ContactID = contact.Field<Int32>("ContactID"),
            //        SalesOrderID = order.Field<Int32>("SalesOrderID"),
            //        FirstName = contact.Field<string>("FirstName"),
            //        Lastname = contact.Field<string>("Lastname"),
            //        TotalDue = order.Field<decimal>("TotalDue")
            //    };


            //var d=
            //    from petition in petition

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