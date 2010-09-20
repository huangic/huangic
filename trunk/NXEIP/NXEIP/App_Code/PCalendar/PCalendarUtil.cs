﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Data;

/// <summary>
/// PCalendarUtil 的摘要描述
/// </summary>
public class PCalendarUtil
{
    
	public PCalendarUtil()
	{
		//
		// TODO: 在此加入建構函式的程式碼
		//
	}

    #region 取得今天日期
    /// <summary>
    /// 取得今天日期
    /// </summary>
    /// <returns>回傳格式化的今天日期</returns>
    public static string GetToday()
    {
        string feedback = "";

        ChangeObject changeobj = new ChangeObject();
        feedback = "今天是 " + changeobj.ADDTtoROCDT(System.DateTime.Now.ToString("yyyy-MM-dd"))
            + " 星期" + changeobj.ChangeWeek(System.DateTime.Now.DayOfWeek);
        
        return feedback;
    }
    #endregion

    #region 取得日期於該月的週數
    /// <summary>
    /// 取得日期於該月的週數
    /// </summary>
    /// <param name="dt">西元年月日</param>
    /// <returns>傳入值於該月的週數</returns>
    public static int GetWeeksOfMonth(DateTime dt)
    {
        int feedback = 0;
        DateTime fdate = Convert.ToDateTime(dt.ToString("yyyy/MM/01"));
        int fweeks = 0, dweeks = 0;
        System.Globalization.GregorianCalendar gc = new GregorianCalendar();

        fweeks = gc.GetWeekOfYear(fdate, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
        dweeks = gc.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
        feedback = dweeks - fweeks + 1;

        return feedback;
    }
    #endregion

    #region 計算天數
    /// <summary>
    /// 計算天數
    /// </summary>
    /// <param name="DT1">開始日期</param>
    /// <param name="DT2">結束日期</param>
    /// <returns>天數</returns>
    public static int DateDiff(DateTime DT1, DateTime DT2)
    {
        int ret = 0;

        ret = DT2.DayOfYear - DT1.DayOfYear;

        return ret;
    }
    #endregion

    #region 轉頁時之script
    public static string ShowMsg_URL(string msg, string url)
    {
        string script="";
        if(msg.Trim().Length>0)
            script = "<script>window.alert('" + msg + "');location.href='" + url + "'</script>";
        else
            script = "<script>location.href='" + url + "'</script>";

        return script;
    }
    #endregion

    #region 找出所屬單位及其下單位編號
    public static string SearchPeopleDepartAndDown(string departs)
    {
        DBObject dbo_departs = new DBObject();
        string feedback = departs;
        string sqlstr_depart = "select dep_no from departments where dep_parentid=" + departs + " and dep_status='1'";
        DataTable dt_departs = new DataTable();
        dt_departs = dbo_departs.ExecuteQuery(sqlstr_depart);
        if (dt_departs.Rows.Count > 0)
        {
            for (int i = 0; i < dt_departs.Rows.Count;i++)
            {
                if (feedback.Length > 0) feedback += ",";
                feedback += SearchPeopleDepartAndDown(dt_departs.Rows[i]["dep_no"].ToString());
            }
        }
        return feedback;
    }
    #endregion

}