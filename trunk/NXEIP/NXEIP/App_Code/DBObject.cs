﻿using System;
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

    /// <summary>
    /// 新增操作記錄
    /// </summary>
    /// <param name="sfu_no">功能編號</param>
    /// <param name="peo_uid">人員編號</param>
    /// <param name="fuction">操作代碼1:新增 2:查詢 3:更新 4:刪除 5:保留</param>
    /// <param name="memo">備註</param>
    public void ExecuteOperates(int sfu_no, string peo_uid, int fuction, string memo)
    {
        string strSQL = "insert into operates (sfu_no,peo_uid,ope_logintime,ope_fuction,ope_memo) values (" + sfu_no + "," + peo_uid + ",GETDATE()," + fuction + ",'" + memo + "')";

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

}
