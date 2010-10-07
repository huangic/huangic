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
    public CascadingDropDownNameValue[] GetSpot(string knownCategoryValues, string category)
    {
        DBObject dbo = new DBObject();
        List<CascadingDropDownNameValue> values = new List<CascadingDropDownNameValue>();
        DataTable dt = new DataTable();
        string sqlstr = "select spo_no,spo_name from spot where spo_status='1' order by spo_no";
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
        if (contextKey.Length > 0)
        {
            string[] ckey = contextKey.Split(',');
            DataTable dt = new DataTable();
            string sqlstr = "select roo_no,roo_name from rooms where roo_status='1' and spo_no=" + ckey[1] + " order by roo_no";
            dt = dbo.ExecuteQuery(sqlstr);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                values.Add(new CascadingDropDownNameValue(dt.Rows[i]["roo_name"].ToString(), dt.Rows[i]["roo_no"].ToString()));
            }
        }
        else
        {
            if (!kv.ContainsKey("spot"))
            {
                return null;
            }
            else
            {

                DataTable dt = new DataTable();
                string sqlstr = "select roo_no,roo_name from rooms where roo_status='1' and spo_no=" + kv["spot"] + " order by roo_no";
                dt = dbo.ExecuteQuery(sqlstr);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    values.Add(new CascadingDropDownNameValue(dt.Rows[i]["roo_name"].ToString(), dt.Rows[i]["roo_no"].ToString()));
                }
            }
        }

        return values.ToArray();
    }
    
}
