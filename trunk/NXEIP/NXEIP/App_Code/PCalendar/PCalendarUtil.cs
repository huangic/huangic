using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Data;

/// <summary>
/// 功能名稱：PCalendarUtil
/// 功能描述：行事曆專用function
/// 撰寫者：Lina
/// 撰寫時間：2010/11/03 修改
/// </summary>
public class PCalendarUtil
{
    
	public PCalendarUtil()
	{
		
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

    #region 計算時間
    /// <summary>
    /// 
    /// </summary>
    /// <param name="DT1">開始時間</param>
    /// <param name="DT2">結束時間</param>
    /// <param name="type">回傳單位(H:小時 m:分鐘)</param>
    /// <returns></returns>
    public static int TimeDiff(DateTime DT1, DateTime DT2, string type)
    {
        int ret = 0;
        TimeSpan ts1 = new TimeSpan(DT1.Ticks);
        TimeSpan ts2 = new TimeSpan(DT2.Ticks);
        TimeSpan ts = ts2.Subtract(ts1).Duration();
        if (type.Equals("H")) //小時制
        {
            ret = ts.Days * 24 + ts.Hours;
        }
        else
        {
            //分鐘制
            ret = (ts.Days * 24 + ts.Hours) * 60 + ts.Minutes;
        }
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

    #region 取得人員在職狀態的編號
    public static string GetPeoJobtype()
    {
        DBObject dbo_type = new DBObject();
        DataTable dt_type = new DataTable();
        string typ_no = "0";
        string sqlstr = "SELECT typ_no from types where (typ_code = 'work') AND (typ_number = '1') AND (typ_status = '1')";
        dt_type = dbo_type.ExecuteQuery(sqlstr);
        if (dt_type.Rows.Count > 0) typ_no = dt_type.Rows[0]["typ_no"].ToString();

        return typ_no;
    }
    #endregion

    #region 判斷是否可以新增行事曆
    public static bool IsAdd(string loginuserid, string setpeouid)
    {
        bool isadd = false;
        DBObject dbo_add = new DBObject();
        DataTable dt_add = new DataTable();
        string sqlstr = "SELECT c01.peo_uid, people.peo_name FROM c01 INNER JOIN people ON c01.peo_uid = people.peo_uid INNER JOIN"
            + " types ON people.peo_jobtype = types.typ_no WHERE (c01.c01_peouid = " + loginuserid + ") AND (types.typ_number = '1') AND (c01.peo_uid = " + setpeouid + ")";
        dt_add = dbo_add.ExecuteQuery(sqlstr);
        if (dt_add.Rows.Count > 0) isadd = true;

        return isadd;
    }
    #endregion

    #region 判斷是否可以查看行事曆
    public static bool IsShow(string loginuserid,string logindepno, string otherpeouid)
    {
        bool IsShow = false;
        if (loginuserid.Equals(otherpeouid))
        {
            IsShow = true;
        }
        else
        {
            DBObject dbo_show = new DBObject();
            DataTable dt_show = new DataTable();
            string sqlstr = "SELECT c04_no, c04_right from c04 where (peo_uid = " + loginuserid + ")";
            dt_show = dbo_show.ExecuteQuery(sqlstr);
            string c04_right = "0";
            if (dt_show.Rows.Count > 0)
            {
                c04_right = dt_show.Rows[0]["c04_right"].ToString();
            }
            if (c04_right.Equals("1"))
                IsShow = true;
            else if (c04_right.Equals("2"))
            {
                string dep_no = SearchPeopleDepartAndDown(logindepno);
                sqlstr = "select peo_uid from people where peo_uid=" + otherpeouid + " and dep_no in (" + dep_no + ")";
                dt_show.Clear();
                dt_show = dbo_show.ExecuteQuery(sqlstr);
                if (dt_show.Rows.Count > 0) IsShow = true;

            }
            else
            {
                sqlstr = "select peo_uid from people where peo_uid=" + otherpeouid + " and dep_no=" + logindepno;
                dt_show.Clear();
                dt_show = dbo_show.ExecuteQuery(sqlstr);
                if (dt_show.Rows.Count > 0) IsShow = true;

            }
        }

        return IsShow;
    }
    #endregion

    #region 取得開會資料
    public static string GetMeeting(string peo_uid,DateTime sdate,DateTime edate,string showtype)
    {
        string feedback = "";

        DBObject dbo_meeting = new DBObject();
        DataTable dt_meeting = new DataTable();
        string sqlstr = "select meetings.mee_no, meetings.mee_reason, meetings.mee_sdate, meetings.mee_edate from meetings inner join attends on meetings.mee_no = attends.mee_no"
            + " where (meetings.mee_status = '1') and (attends.att_status = '2') and (attends.peo_uid = " + peo_uid + ") and (meetings.mee_sdate >= '" + sdate.ToString("yyyy/MM/dd HH:mm:ss") + "') and (meetings.mee_edate <= '" + edate.ToString("yyyy/MM/dd HH:mm:ss") + "')";
        dt_meeting = dbo_meeting.ExecuteQuery(sqlstr);
        if (dt_meeting.Rows.Count > 0)
        {
            for (int dti = 0; dti < dt_meeting.Rows.Count; dti++)
            {
                if(showtype.Equals("1"))
                    feedback += "<br />■" + Convert.ToDateTime(dt_meeting.Rows[dti]["mee_sdate"].ToString()).ToString("HH:mm") + "~" + Convert.ToDateTime(dt_meeting.Rows[dti]["mee_edate"].ToString()).ToString("HH:mm") + "<br>" + dt_meeting.Rows[dti]["mee_reason"].ToString();
                else if (showtype.Equals("2"))
                    feedback += "<br />■" + Convert.ToDateTime(dt_meeting.Rows[dti]["mee_sdate"].ToString()).ToString("HH:mm") + "~" + Convert.ToDateTime(dt_meeting.Rows[dti]["mee_edate"].ToString()).ToString("HH:mm") + " " + dt_meeting.Rows[dti]["mee_reason"].ToString();
                else if(showtype.Equals("3"))
                    feedback += "■" + Convert.ToDateTime(dt_meeting.Rows[dti]["mee_sdate"].ToString()).ToString("HH:mm") + "~" + Convert.ToDateTime(dt_meeting.Rows[dti]["mee_edate"].ToString()).ToString("HH:mm") + " " + dt_meeting.Rows[dti]["mee_reason"].ToString() + "<br />";
                else if (showtype.Equals("4"))
                    feedback += "<li class=\"p1\"><span class=\"row_schedule\">" + Convert.ToDateTime(dt_meeting.Rows[dti]["mee_sdate"].ToString()).ToString("HH:mm") + "~" + Convert.ToDateTime(dt_meeting.Rows[dti]["mee_edate"].ToString()).ToString("HH:mm") + " " + dt_meeting.Rows[dti]["mee_reason"].ToString() + "</span></li>";
            }
        }

        return feedback;
    }
    #endregion

}