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
/// calendar 的摘要描述
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允許使用 ASP.NET AJAX 從指令碼呼叫此 Web 服務，請取消註解下一行。
[System.Web.Script.Services.ScriptService]
public class calendar : System.Web.Services.WebService {

    public calendar () {

        //如果使用設計的元件，請取消註解下行程式碼 
        //InitializeComponent(); 
    }

    [WebMethod]
    public CascadingDropDownNameValue[] GetViewDepart(string knownCategoryValues, string category, string contextKey)
    {
        DBObject dbo = new DBObject();
        DataTable dt = new DataTable();

        List<CascadingDropDownNameValue> values = new List<CascadingDropDownNameValue>();
        string c04_right = "3"; // 1:全體 2:單位(含子部門)  3:部門(自己本身的單位)
        if (contextKey.Length > 0)
        {
            string[] ckey = contextKey.Split(',');
            string peo_uid = "0";
            string qdep_no = "0";
            if (ckey.Length == 2)
            {
                peo_uid = ckey[0];
                qdep_no = ckey[1];
            }

            string sqlstr = "SELECT c04_no, c04_right FROM c04 WHERE (peo_uid = " + peo_uid + ")";
            dt = dbo.ExecuteQuery(sqlstr);
            if (dt.Rows.Count > 0)
            {
                c04_right = dt.Rows[0]["c04_right"].ToString();
            }
            if (c04_right.Equals("1"))
            {
                #region 1:全體
                sqlstr = "SELECT dep_no, dep_name FROM departments WHERE (dep_status='1') and dep_no>1 ORDER BY dep_level,dep_order";
                dt.Clear();
                dt = dbo.ExecuteQuery(sqlstr);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if(dt.Rows[i]["dep_no"].ToString().Equals(qdep_no))
                        values.Add(new CascadingDropDownNameValue(dt.Rows[i]["dep_name"].ToString(), dt.Rows[i]["dep_no"].ToString(),true));
                    else
                        values.Add(new CascadingDropDownNameValue(dt.Rows[i]["dep_name"].ToString(), dt.Rows[i]["dep_no"].ToString(),false));
                }
                #endregion
            }
            else if (c04_right.Equals("2"))
            {
                #region 2:單位(含子部門)
                string dep_no = PCalendarUtil.SearchPeopleDepartAndDown(peo_uid);
                sqlstr = "SELECT dep_no, dep_name FROM departments WHERE (dep_status='1') and dep_no>1 and dep_no in (" + dep_no + ") ORDER BY dep_level,dep_order";
                dt.Clear();
                dt = dbo.ExecuteQuery(sqlstr);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["dep_no"].ToString().Equals(qdep_no))
                        values.Add(new CascadingDropDownNameValue(dt.Rows[i]["dep_name"].ToString(), dt.Rows[i]["dep_no"].ToString(),true));
                    else
                        values.Add(new CascadingDropDownNameValue(dt.Rows[i]["dep_name"].ToString(), dt.Rows[i]["dep_no"].ToString(),false));
                }
                #endregion
            }
            else
            {
                #region 3:部門(自己本身的單位)
                sqlstr = "select people.peo_uid, people.dep_no, departments.dep_name from people INNER JOIN departments on people.dep_no = departments.dep_no where (people.peo_uid = " + peo_uid + ")";
                dt.Clear();
                dt = dbo.ExecuteQuery(sqlstr);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["dep_no"].ToString().Equals(qdep_no))
                        values.Add(new CascadingDropDownNameValue(dt.Rows[i]["dep_name"].ToString(), dt.Rows[i]["dep_no"].ToString(),true));
                    else
                        values.Add(new CascadingDropDownNameValue(dt.Rows[i]["dep_name"].ToString(), dt.Rows[i]["dep_no"].ToString(),false));
                }
                #endregion
            }
        }
       
        return values.ToArray();
    }

    [WebMethod]
    public CascadingDropDownNameValue[] GetViewPeople(string knownCategoryValues, string category, string contextKey)
    {
        DBObject dbo = new DBObject();
        StringDictionary kv = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
        List<CascadingDropDownNameValue> values = new List<CascadingDropDownNameValue>();
        if (!kv.ContainsKey("departs"))
        {
            return null;
        }
        else
        {
            DataTable dt = new DataTable();
            string sqlstr = "SELECT typ_no from types where (typ_code = 'work') AND (typ_number = '1') AND (typ_status = '1')";
            dt = dbo.ExecuteQuery(sqlstr);
            string typ_no = "0";
            if (dt.Rows.Count > 0) typ_no = dt.Rows[0]["typ_no"].ToString();
            sqlstr = "select people.peo_uid, people.peo_name FROM people left join types ON people.peo_pfofess = types.typ_no "
            + " WHERE (types.typ_code = 'profess') AND (people.dep_no = " + kv["departs"] + ") and (people.peo_jobtype="+typ_no+") ORDER BY types.typ_order, people.peo_name";
            dt.Clear();
            dt = dbo.ExecuteQuery(sqlstr);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (contextKey != null)
                {
                    if (contextKey.Equals(dt.Rows[i]["peo_uid"].ToString()))
                        values.Add(new CascadingDropDownNameValue(dt.Rows[i]["peo_name"].ToString(), dt.Rows[i]["peo_uid"].ToString(), true));
                    else
                        values.Add(new CascadingDropDownNameValue(dt.Rows[i]["peo_name"].ToString(), dt.Rows[i]["peo_uid"].ToString(), false));
                }
                else
                {
                    values.Add(new CascadingDropDownNameValue(dt.Rows[i]["peo_name"].ToString(), dt.Rows[i]["peo_uid"].ToString()));
                }
            }
        }
        return values.ToArray();
    }


    [WebMethod]
    public CascadingDropDownNameValue[] GetTimes(string knownCategoryValues, string category, string contextKey)
    {
        List<CascadingDropDownNameValue> values = new List<CascadingDropDownNameValue>();

        for (int sh = 6; sh < 24; sh++)
        {
            string item = sh.ToString("0#") + ":00";
            if (contextKey != null)
            {
                if (contextKey.Equals(item))
                    values.Add(new CascadingDropDownNameValue(item, item, true));
                else
                    values.Add(new CascadingDropDownNameValue(item, item, false));
            }
            else
            {
                values.Add(new CascadingDropDownNameValue(item, item));
            }
        }

        return values.ToArray();
    }

    [WebMethod]
    public CascadingDropDownNameValue[] GetC01(string knownCategoryValues, string category, string contextKey)
    {
        DBObject dbo = new DBObject();
        List<CascadingDropDownNameValue> values = new List<CascadingDropDownNameValue>();
        if (contextKey.Length <= 0)
        {
            return null;
            //values.Add(new CascadingDropDownNameValue(contextKey, contextKey));
        }
        else
        {
            DataTable dt = new DataTable();
            string sqlstr = "SELECT typ_no from types where (typ_code = 'work') AND (typ_number = '1') AND (typ_status = '1')";
            dt = dbo.ExecuteQuery(sqlstr);
            string typ_no = "0";
            if (dt.Rows.Count > 0) typ_no = dt.Rows[0]["typ_no"].ToString();
            sqlstr = "SELECT c01.peo_uid, people.peo_name FROM c01 INNER JOIN people ON c01.peo_uid = people.peo_uid "
            + " WHERE (c01.c01_peouid = " + contextKey + ") AND (people.peo_jobtype =" + typ_no + ") ORDER BY people.peo_name";
            dt.Clear();
            dt = dbo.ExecuteQuery(sqlstr);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    values.Add(new CascadingDropDownNameValue(dt.Rows[i]["peo_name"].ToString(), dt.Rows[i]["peo_uid"].ToString()));
                }
            }
        }
        return values.ToArray();
    }

    [WebMethod]
    public CascadingDropDownNameValue[] GetLeading(string knownCategoryValues, string category, string contextKey)
    {
        DBObject dbo = new DBObject();
        List<CascadingDropDownNameValue> values = new List<CascadingDropDownNameValue>();
        DataTable dt = new DataTable();
        string sqlstr = "select leading.lea_peouid, people.peo_name from leading inner join people on leading.lea_peouid = people.peo_uid order by leading.lea_no";
        dt = dbo.ExecuteQuery(sqlstr);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                values.Add(new CascadingDropDownNameValue(dt.Rows[i]["peo_name"].ToString(), dt.Rows[i]["lea_peouid"].ToString()));
            }
        }

        return values.ToArray();
    }
    
}
