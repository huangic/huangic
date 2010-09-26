using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using NXEIP.DAO;
using Entity;

public partial class lib_AutoComplete_ACDataSrc : System.Web.UI.Page
{
    private NXEIPEntities model = new NXEIPEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        //使用者目前輸入的文字預設以q傳入
        string q = Request["q"] ?? "";
        if (q.Length > 0)
        {
            DataTable t = getStockData2();
            DataView dv = new DataView(t);
            //利用LIKE做查詢
            dv.RowFilter = "Key LIKE '" + q + "%'";
            dv.Sort = "Key,dep_order,peo_workid,peo_name";
            //dv.Sort = "Key";
            List<string> lst = new List<string>();
            lst.Add("");
            foreach (DataRowView drv in dv)
            {
                DataRow r = drv.Row;

                //組裝出前端要用的欄位
                lst.Add(string.Format("{0}|{1}|{2}|{3}|{4}|{5}", r["Key"], r["dep_name"], r["peo_name"], r["peo_workid"], r["peo_uid"], r["dep_no"]));
                //lst.Add(string.Format("{0}|{1}|{2}", r["key"], r["symbol"], r["cname"]));
                if (lst.Count >= 10) break;
            }

            //每筆資料間以換行分隔
            Response.Write(string.Join("\n", lst.ToArray()));
        }
    }

    private DataTable getStockData2()
    {

        //如果資料量未多到誇張，將DataTable Cached住
        string CACHE_ACData = "ACDataTable";

        if (Cache[CACHE_ACData] == null)
        {
            //建立回傳表格
            DataTable t = new DataTable();
            t.Columns.Add("Key", typeof(string));
            t.Columns.Add("dep_name", typeof(string));
            t.Columns.Add("peo_name", typeof(string));
            t.Columns.Add("peo_workid", typeof(string));
            t.Columns.Add("peo_uid", typeof(string));
            t.Columns.Add("dep_no", typeof(string));
            t.Columns.Add("dep_order", typeof(string));

            //取得在職人員資料
            var pData = (from d in model.people where d.peo_jobtype == 1 select d);
            foreach (var p in pData)
            {
                t.Rows.Add(p.peo_name, p.departments.dep_name, p.peo_name, p.peo_workid, p.peo_uid, p.dep_no, p.departments.dep_order);
                t.Rows.Add(p.peo_workid, p.departments.dep_name, p.peo_name, p.peo_workid, p.peo_uid, p.dep_no, p.departments.dep_order);
            }


            //放入Cache，保存兩小時
            Cache.Add(CACHE_ACData, t, null, DateTime.Now.AddHours(2),
                System.Web.Caching.Cache.NoSlidingExpiration,
                System.Web.Caching.CacheItemPriority.Normal, null);
        }


        return Cache[CACHE_ACData] as DataTable;
    }

}