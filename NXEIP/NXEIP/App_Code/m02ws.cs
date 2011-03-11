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
/// m02ws 的摘要描述
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允許使用 ASP.NET AJAX 從指令碼呼叫此 Web 服務，請取消註解下一行。
[System.Web.Script.Services.ScriptService]
public class m02ws : System.Web.Services.WebService {

    public m02ws () {

        //如果使用設計的元件，請取消註解下行程式碼 
        //InitializeComponent(); 
    }

    [WebMethod]
    public CascadingDropDownNameValue[] GetChekuan(string knownCategoryValues, string category, string contextKey)
    {
        List<CascadingDropDownNameValue> values = new List<CascadingDropDownNameValue>();
        DBObject dbo = new DBObject();
        DataTable dt = new DataTable();
        if (contextKey.Length > 0)
        {
            string sqlstr = "select m01_no, m01_name from m01 where (m01_number = 'chekuan') and (m01_status = '1') order by m01_code";
            dt = dbo.ExecuteQuery(sqlstr);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["m01_no"].ToString().Equals(contextKey))
                    values.Add(new CascadingDropDownNameValue(dt.Rows[i]["m01_name"].ToString(), dt.Rows[i]["m01_no"].ToString(), true));
                else
                    values.Add(new CascadingDropDownNameValue(dt.Rows[i]["m01_name"].ToString(), dt.Rows[i]["m01_no"].ToString(), false));
            }
        }
        return values.ToArray();
    }

    [WebMethod]
    public CascadingDropDownNameValue[] GetCar(string knownCategoryValues, string category, string contextKey)
    {
        DBObject dbo = new DBObject();
        StringDictionary kv = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
        List<CascadingDropDownNameValue> values = new List<CascadingDropDownNameValue>();

        if (!kv.ContainsKey("chekuan"))
        {
            return null;
        }
        else
        {
            if (contextKey.Length > 0)
            {
                DataTable dt = new DataTable();
                string sqlstr = "select m02_no,m02_number from m02 where (m02_chekuan=" + kv["chekuan"] + ") and (m02_status='1') order by m02_number";
                dt = dbo.ExecuteQuery(sqlstr);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["m02_no"].ToString().Equals(contextKey))
                        values.Add(new CascadingDropDownNameValue(dt.Rows[i]["m02_number"].ToString(), dt.Rows[i]["m02_no"].ToString(), true));
                    else
                        values.Add(new CascadingDropDownNameValue(dt.Rows[i]["m02_number"].ToString(), dt.Rows[i]["m02_no"].ToString(), false));
                }
            }
        }

        return values.ToArray();
    }
    
}
