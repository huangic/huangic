using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

/// <summary>
/// DBObject 的摘要描述
/// </summary>
public class DBObject
{
    private static string connStr = string.Empty;

	public DBObject()
	{
        connStr = System.Configuration.ConfigurationManager.ConnectionStrings["NXEIPConnectionString"].ConnectionString;
	}

    #region 回傳DataTable
    /// <summary>
    /// 回傳DataTable
    /// </summary>
    /// <param name="strSQL"></param>
    /// <returns></returns>
    public DataTable ExecuteQuery(string strSQL)
    {
        SqlCommand com = new SqlCommand(strSQL, new SqlConnection(connStr));
        DataTable mytable = new DataTable("mytable");
        try
        {
            if (com.Connection.State != ConnectionState.Open)
            {
                com.Connection.Open();
            }

            SqlDataReader dr = com.ExecuteReader();

            for (int i = 0; i < dr.FieldCount; i++)
            {
                mytable.Columns.Add(dr.GetName(i), typeof(string));
            }

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    DataRow row = mytable.NewRow();
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        row[dr.GetName(i)] = dr[dr.GetName(i)].ToString();
                    }
                    mytable.Rows.Add(row);
                }
            }
            dr.Close();
        }
        catch (System.Exception ex)
        {
            mytable.Columns.Add("ErrorMsg", typeof(string));
            DataRow row = mytable.NewRow();
            row[0] = ex.ToString();
            mytable.Rows.Add(row);
        }
        finally
        {
            if (com.Connection.State != ConnectionState.Closed)
            {
                com.Connection.Close();
            }
            com.Dispose();
        }

        return mytable;
    }
    #endregion

    #region Delete,Update,Insert
    /// <summary>
    /// Delete,Update,Insert
    /// </summary>
    /// <param name="strSQL"></param>
    public void ExecuteNonQuery(string strSQL)
    {
        SqlCommand com = new SqlCommand(strSQL, new SqlConnection(connStr));
        try
        {
            if (com.Connection.State != ConnectionState.Open)
            {
                com.Connection.Open();
            }

            com.ExecuteNonQuery();

        }
        finally
        {
            if (com.Connection.State != ConnectionState.Closed)
            {
                com.Connection.Close();
            }
            com.Dispose();
        }
    }
    #endregion

    #region 回傳受影響的資料列數目
    /// <summary>
    /// 回傳受影響的資料列數目
    /// </summary>
    /// <param name="strSQL"></param>
    public int ExecuteIntQuery(string strSQL)
    {
        int ret = 0;
        SqlCommand com = new SqlCommand(strSQL, new SqlConnection(connStr));
        try
        {
            if (com.Connection.State != ConnectionState.Open)
            {
                com.Connection.Open();
            }

            ret = com.ExecuteNonQuery();

        }
        finally
        {
            if (com.Connection.State != ConnectionState.Closed)
            {
                com.Connection.Close();
            }
            com.Dispose();
        }

        return ret;
    }
    #endregion

    #region 傳回第一個資料列裡的第一個資料行
    /// <summary>
    /// 傳回第一個資料列裡的第一個資料行
    /// </summary>
    /// <param name="pty_code"></param>
    /// <returns></returns>
    public string ExecuteScalar(string strSQL)
    {
        SqlCommand com = new SqlCommand(strSQL, new SqlConnection(connStr));
        string ret = "";

        try
        {
            if (com.Connection.State != ConnectionState.Open)
            {
                com.Connection.Open();
            }
            try
            {
                ret = com.ExecuteScalar().ToString();
            }
            catch
            {
            }

        }
        finally
        {
            if (com.Connection.State != ConnectionState.Closed)
            {
                com.Connection.Close();
            }
            com.Dispose();
        }

        return ret;
    }
    #endregion

    #region 取得資料庫參數值
    /// <summary>
    /// 取得資料庫參數值
    /// </summary>
    /// <param name="VarName">參數名稱</param>
    /// <returns>參數值</returns>
    public string GetArguments(string VarName)
    {
        string sqlstr = "select arg_value from arguments where arg_variable='" + VarName + "'";
        DataTable dt = ExecuteQuery(sqlstr);
        if (dt.Rows.Count > 0)
        {
            return dt.Rows[0][0].ToString();
        }
        else
        {
            return null;
        }
    }
    #endregion
}
