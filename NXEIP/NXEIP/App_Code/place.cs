using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using AjaxControlToolkit;
using System.Data;
using System.Collections.Specialized;

/// <summary>
/// place 的摘要描述
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允許使用 ASP.NET AJAX 從指令碼呼叫此 Web 服務，請取消註解下一行。
[System.Web.Script.Services.ScriptService]
public class place : System.Web.Services.WebService {

    public place () {
        //如果使用設計的元件，請取消註解下行程式碼 
        //InitializeComponent(); 
    }

    [WebMethod]
    public CascadingDropDownNameValue[] GetSpot(string knownCategoryValues, string category, string contextKey)
    {
        List<CascadingDropDownNameValue> values = new List<CascadingDropDownNameValue>();
        DBObject dbo = new DBObject();
        DataTable dt = new DataTable();
        string sqlstr = "SELECT DISTINCT spot.spo_no, spot.spo_name FROM spot INNER JOIN rooms ON spot.spo_no = rooms.spo_no INNER JOIN government ON rooms.roo_no = government.roo_no "
            + "WHERE (rooms.roo_status='1') AND (rooms.roo_dep='1') AND (spot.spo_status='1') OR (rooms.roo_status='1') AND (rooms.roo_dep='2') AND (spot.spo_status='1') AND (government.gov_depno=" + contextKey + ")"
            +"ORDER BY spot.spo_no";
        dt = dbo.ExecuteQuery(sqlstr);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            values.Add(new CascadingDropDownNameValue(dt.Rows[i]["spo_name"].ToString(), dt.Rows[i]["spo_no"].ToString()));
        }
        return values.ToArray();
    }

    [WebMethod]
    public CascadingDropDownNameValue[] GetRooms(string knownCategoryValues, string category, string contextKey)
    {
        DBObject dbo = new DBObject();
        StringDictionary kv = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
        List<CascadingDropDownNameValue> values = new List<CascadingDropDownNameValue>();

        if (!kv.ContainsKey("spot"))
        {
            return null;
        }
        else
        {

            DataTable dt = new DataTable();
            string sqlstr1 = "SELECT DISTINCT rooms.roo_no, rooms.roo_name FROM rooms INNER JOIN government ON rooms.roo_no = government.roo_no"
                + " WHERE (rooms.roo_status = '1') AND (rooms.roo_dep = '1') AND (rooms.spo_no = " + kv["spot"] + ") "
                + " OR (rooms.roo_status = '1') AND (rooms.roo_dep = '2') AND (government.gov_depno = " + contextKey + ") AND (rooms.spo_no =" + kv["spot"] + ")"
                + " order by rooms.roo_no";
            string sqlstr = "select roo_no,roo_name from rooms where roo_status='1' and spo_no=" + kv["spot"] + " order by roo_no";
            dt = dbo.ExecuteQuery(sqlstr);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                values.Add(new CascadingDropDownNameValue(dt.Rows[i]["roo_name"].ToString(), dt.Rows[i]["roo_no"].ToString()));
            }
        }

        //if (contextKey.Length > 0)
        //{
        //    string[] ckey = contextKey.Split(',');
        //    DataTable dt = new DataTable();
        //    string sqlstr = "select roo_no,roo_name from rooms where roo_status='1' and spo_no=" + ckey[1] + " order by roo_no";
        //    dt = dbo.ExecuteQuery(sqlstr);
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        values.Add(new CascadingDropDownNameValue(dt.Rows[i]["roo_name"].ToString(), dt.Rows[i]["roo_no"].ToString()));
        //    }
        //}
        //else
        //{
            
        //}

        return values.ToArray();
    }
    
}
