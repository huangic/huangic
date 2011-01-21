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
/// equipments 的摘要描述
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允許使用 ASP.NET AJAX 從指令碼呼叫此 Web 服務，請取消註解下一行。
[System.Web.Script.Services.ScriptService]
public class equborrow : System.Web.Services.WebService
{
    public equborrow () {

        //如果使用設計的元件，請取消註解下行程式碼 
        //InitializeComponent(); 
    }

    [WebMethod]
    public CascadingDropDownNameValue[] GetSpot(string knownCategoryValues, string category, string contextKey)
    {
        List<CascadingDropDownNameValue> values = new List<CascadingDropDownNameValue>();
        DBObject dbo = new DBObject();
        DataTable dt = new DataTable();
        if (contextKey.Length > 0)
        {
            string sqlstr = "select distinct spot.spo_no, spot.spo_name from spot inner join equipments on spot.spo_no = equipments.spo_no"
                + " where (spot.spo_status = '1') and (spot.spo_function like '_____1%') and (equipments.equ_status = '1')"
                + " order by spot.spo_no";
            dt = dbo.ExecuteQuery(sqlstr);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["spo_no"].ToString().Equals(contextKey))
                    values.Add(new CascadingDropDownNameValue(dt.Rows[i]["spo_name"].ToString(), dt.Rows[i]["spo_no"].ToString(), true));
                else
                    values.Add(new CascadingDropDownNameValue(dt.Rows[i]["spo_name"].ToString(), dt.Rows[i]["spo_no"].ToString(), false));
            }
        }
        return values.ToArray();
    }

    [WebMethod]
    public CascadingDropDownNameValue[] GetEquipments(string knownCategoryValues, string category, string contextKey)
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
            if (contextKey.Length > 0)
            {
                DataTable dt = new DataTable();
                string sqlstr = "select equ_no, equ_name from equipments where (equ_status = '1' ) and (spo_no=" + kv["spot"] + ") order by equ_number, equ_name";
                dt = dbo.ExecuteQuery(sqlstr);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["equ_no"].ToString().Equals(contextKey))
                        values.Add(new CascadingDropDownNameValue(dt.Rows[i]["equ_name"].ToString(), dt.Rows[i]["equ_no"].ToString(), true));
                    else
                        values.Add(new CascadingDropDownNameValue(dt.Rows[i]["equ_name"].ToString(), dt.Rows[i]["equ_no"].ToString(), false));
                }
            }
        }

        return values.ToArray();
    }

    
}
