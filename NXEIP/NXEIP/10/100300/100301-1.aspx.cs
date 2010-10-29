﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using NXEIP.DAO;
using Entity;

public partial class _10_100300_100301_1 : System.Web.UI.Page
{
    ChangeObject changeobj = new ChangeObject();
    SessionObject sobj = new SessionObject();
    DBObject dbo = new DBObject();
    protected string localurl = "100301-1.aspx";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
            new OperatesObject().ExecuteOperates(100301, sobj.sessionUserID, 2, "點選個人行事曆-週");

            ListItem newitem = new ListItem("請選擇", "0");
            DataTable dt = new DataTable();

            #region 左邊版面：月曆、今天日期
            //左上，月曆
            //Response.Write(Request["today"]);
            if (Request["today"] != null)
                this.Calendar1.VisibleDate = Convert.ToDateTime(changeobj.ROCDTtoADDT(Request["today"]));
            else
                this.Calendar1.VisibleDate = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd"));//月曆初始值(左)

            this.Calendar1.TodaysDate = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd"));//月曆初始值(左)

            this.lab_CYM.Text = this.Calendar1.VisibleDate.Year.ToString() + "年" + this.Calendar1.VisibleDate.Month.ToString("0#") + "月";
            this.hl_Pre.NavigateUrl = localurl + "?today=" + changeobj.ADDTtoROCDT(this.Calendar1.VisibleDate.AddMonths(-1).ToString("yyyy-MM-01"));
            this.hl_Nxt.NavigateUrl = localurl + "?today=" + changeobj.ADDTtoROCDT(this.Calendar1.VisibleDate.AddMonths(1).ToString("yyyy-MM-01"));
            //今天日期
            this.lab_today.Text = PCalendarUtil.GetToday();
            #endregion

            #region 左邊版面：新增行事曆
            for (int sh = 6; sh < 24; sh++)
            {
                ListItem newitem1 = new ListItem(sh.ToString("0#") + ":00", sh.ToString("0#") + ":00");
                this.ddl_stime.Items.Add(newitem1);
                this.ddl_etime.Items.Add(newitem1);
            }
            this.ddl_stime.Items.Insert(0, newitem);
            this.ddl_etime.Items.Insert(0, newitem);
            #endregion

            #region 左邊版面：可設定之他人行事曆
            this.ddl_c01.Items.Add(newitem);
            string sqlstr = "SELECT c01.peo_uid, people.peo_name FROM c01 INNER JOIN people ON c01.peo_uid = people.peo_uid WHERE (c01.c01_peouid = " + sobj.sessionUserID + ") AND (people.peo_jobtype = '1') ORDER BY people.peo_name";
            dt.Clear();
            dt = dbo.ExecuteQuery(sqlstr);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ListItem additem = new ListItem(dt.Rows[i]["peo_name"].ToString(), dt.Rows[i]["peo_uid"].ToString());
                    this.ddl_c01.Items.Add(additem);
                }
            }
            #endregion

            #region 左邊版面：可查看之他人行事曆
            sqlstr = "SELECT c04_no, c04_right FROM c04 WHERE (peo_uid = " + sobj.sessionUserID + ")";
            string sqlstr1 = "";
            dt.Clear();
            dt = dbo.ExecuteQuery(sqlstr);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["c04_right"].ToString().Equals("2"))
                {
                    string dep_no = PCalendarUtil.SearchPeopleDepartAndDown(sobj.sessionUserDepartID);
                    sqlstr1 = "SELECT dep_no, dep_name FROM departments WHERE (dep_status='1') and dep_no>1 and dep_no in (" + dep_no + ") ORDER BY dep_level,dep_order";
                }
                else
                {
                    sqlstr1 = "SELECT dep_no, dep_name FROM departments WHERE (dep_status='1') and dep_no>1 ORDER BY dep_level,dep_order";
                }
                DataTable dt1 = new DataTable();
                dt1 = dbo.ExecuteQuery(sqlstr1);
                if (dt1.Rows.Count > 0)
                {
                    for (int j = 0; j < dt1.Rows.Count; j++)
                    {
                        ListItem additem = new ListItem(dt1.Rows[j]["dep_name"].ToString(), dt1.Rows[j]["dep_no"].ToString());
                        this.ddl_QryDepart.Items.Add(additem);
                    }
                }
                this.ddl_QryDepart.Items.Insert(0, newitem);
                this.ddl_QryPeople.Items.Insert(0, newitem);
            }
            else
            {
                ListItem additem = new ListItem(sobj.sessionUserDepartName, sobj.sessionUserDepartID);
                this.ddl_QryDepart.Items.Add(additem);
                this.ddl_QryDepart.Items.Insert(0, newitem);
            }

            if (Request["depart"] != null)
            {
                try
                {
                    this.ddl_QryDepart.Items.FindByValue(Request["depart"]).Selected = true;
                    this.ddl_QryPeople.Items.Clear();
                    sqlstr = "SELECT people.peo_uid, people.peo_name FROM people INNER JOIN profess ON people.pro_no = profess.pro_no WHERE (people.peo_jobtype='1') and (people.dep_no in (" + this.ddl_QryDepart.SelectedValue + ")) ORDER BY  profess.pro_order, people.peo_name";
                    dt.Clear();
                    dt = dbo.ExecuteQuery(sqlstr);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            ListItem additem = new ListItem(dt.Rows[i]["peo_name"].ToString(), dt.Rows[i]["peo_uid"].ToString());
                            this.ddl_QryPeople.Items.Add(additem);
                        }
                    }
                }
                catch { }
            }
            this.ddl_QryPeople.Items.Insert(0, newitem);
            #endregion

            #region 右邊版面：預設日期、人員編號
            if (Request["today"] != null)
                this.lab_date.Text = Request["today"];
            else
                this.lab_date.Text = changeobj.ADDTtoROCDT(System.DateTime.Now.ToString("yyyy-MM-dd")); //行事曆(右)

            if (Request["peo_uid"] != null)
                this.lab_people.Text = Request["peo_uid"];
            else
                this.lab_people.Text = sobj.sessionUserID;//登入者(右)
            #endregion

            if (this.lab_people.Text.Equals(sobj.sessionUserID))
            {
                this.btn_back.Visible = false;
            }
            else
            {
                this.btn_back.Text = "返回" + sobj.sessionUserName;
                this.btn_back.Visible = true;
            }

            Show();
        }
    }

    #region 顯示出行事曆(右邊版面)
    private void Show()
    {
        string aMSG = "";
        try
        {
            DataTable dt99 = new DataTable();
            string sqlstr99 = "";

            #region 抓出右邊行事曆資料
            sqlstr99 = "SELECT peo_uid, peo_name FROM people where peo_uid=" + this.lab_people.Text;
            dt99 = dbo.ExecuteQuery(sqlstr99);
            if (dt99.Rows.Count > 0) this.lab_name.Text = dt99.Rows[0]["peo_name"].ToString();
            #endregion

            #region 判斷是否可新增
            string isAdd = "0";
            if (this.lab_people.Text.Equals(sobj.sessionUserID))
            {
                isAdd = "1";
            }
            else
            {
                for (int i = 1; i < this.ddl_c01.Items.Count; i++)
                {
                    if (this.lab_people.Text.Equals(this.ddl_c01.Items[i].Value))
                    {
                        isAdd = "1";
                    }
                }
            }
            #endregion

            #region 週行事曆初始化--清空
            for (int i = 0; i < 7; i++)
            {
                ((Label)this.Master.FindControl("ContentPlaceHolder1").FindControl("lab_" + i.ToString())).Text = "";
            }
            #endregion

            #region 週行事曆
            DateTime sdate = new DateTime();
            DateTime edate = new DateTime();
            sdate = Convert.ToDateTime(changeobj.ROCDTtoADDT(this.lab_date.Text));
            sdate = sdate.AddDays(-(int)changeobj.ChangeWeek(sdate));
            edate = sdate.AddDays(7);

            while (sdate < edate)
            {
                string sdate1 = changeobj.ADDTtoROCDT(sdate.ToString("yyyy-MM-dd"));
                #region 日期、連結
                int weeks = changeobj.ChangeWeek(sdate);
                ((HyperLink)this.Master.FindControl("ContentPlaceHolder1").FindControl("hl_" + weeks.ToString())).Text = sdate1;
                if (isAdd.Equals("1"))
                    ((HyperLink)this.Master.FindControl("ContentPlaceHolder1").FindControl("hl_" + weeks.ToString())).NavigateUrl = "100301-0.aspx?today=" + sdate1 + "&peo_uid=" + this.lab_people.Text + "&depart=" + this.ddl_QryDepart.SelectedValue + "&stime=06:00&source=2&height=480&width=800&TB_iframe=true&modal=true";

                else
                    ((HyperLink)this.Master.FindControl("ContentPlaceHolder1").FindControl("hl_" + weeks.ToString())).NavigateUrl = "";
                #endregion

                #region 行事曆
                string sdate2 = changeobj.ROCDTtoADDT(this.lab_date.Text) + " 00:00:00";
                string edate2 = changeobj.ROCDTtoADDT(this.lab_date.Text) + " 23:59:59";
                sqlstr99 = "SELECT peo_uid, c02_no, c02_sdate, c02_edate, c02_title, c02_bgcolor, c02_setuid FROM c02 "
                + " WHERE (peo_uid = " + this.lab_people.Text + ") AND (c02_sdate <= '" + edate2 + "') AND (c02_edate <= '" + edate2 + "') AND (c02_edate >= '" + sdate2 + "') "
                + " OR (peo_uid = " + this.lab_people.Text + ") AND (c02_sdate >= '" + sdate2 + "') AND (c02_edate >= '" + sdate2 + "') AND (c02_sdate <= '" + edate2 + "') "
                + " OR (peo_uid = " + this.lab_people.Text + ") AND (c02_sdate < '" + sdate2 + "') AND (c02_edate > '" + edate2 + "') ORDER BY c02_sdate, c02_edate, c02_no";
                dt99 = dbo.ExecuteQuery(sqlstr99);
                if (dt99.Rows.Count > 0)
                {
                    for (int i = 0; i < dt99.Rows.Count; i++)
                    {
                        string stime = "";
                        string etime = "";
                        if (Convert.ToDateTime(dt99.Rows[i]["c02_sdate"].ToString()).ToString("yyyy-MM-dd").Equals(Convert.ToDateTime(sdate).ToString("yyyy-MM-dd")))
                        {
                            stime = Convert.ToDateTime(dt99.Rows[i]["c02_sdate"].ToString()).ToString("HH:mm");
                        }
                        else
                        {
                            stime = "06:00";
                        }
                        if (Convert.ToDateTime(dt99.Rows[i]["c02_edate"].ToString()).ToString("yyyy-MM-dd").Equals(Convert.ToDateTime(sdate).ToString("yyyy-MM-dd")))
                        {
                            etime = Convert.ToDateTime(dt99.Rows[i]["c02_edate"].ToString()).ToString("HH:mm");
                        }
                        else
                        {
                            etime = "23:00";
                        }
                        Display(sdate1, changeobj.ChangeWeek(sdate), "■" + stime + "~" + etime + " " + dt99.Rows[i]["c02_title"].ToString(), Convert.ToInt32(dt99.Rows[i]["c02_no"].ToString()), Convert.ToInt32(dt99.Rows[i]["c02_setuid"].ToString()));
                    }
                }
                #endregion

                sdate = sdate.AddDays(1);
            }
            #endregion

            #region 修正左上切換版之連結參數
            this.HyperLink1.NavigateUrl = "100301.aspx?today=" + this.lab_date.Text + "&peo_uid=" + this.lab_people.Text + "&depart=" + this.ddl_QryDepart.SelectedValue;
            this.current.NavigateUrl = "100301-1.aspx?today=" + this.lab_date.Text + "&peo_uid=" + this.lab_people.Text + "&depart=" + this.ddl_QryDepart.SelectedValue;
            this.HyperLink2.NavigateUrl = "100301-2.aspx?today=" + this.lab_date.Text + "&peo_uid=" + this.lab_people.Text + "&depart=" + this.ddl_QryDepart.SelectedValue;
            this.HyperLink3.NavigateUrl = "100301-3.aspx?today=" + this.lab_date.Text + "&peo_uid=" + this.lab_people.Text + "&depart=" + this.ddl_QryDepart.SelectedValue;
            this.HyperLink4.NavigateUrl = "100301-4.aspx?today=" + this.lab_date.Text + "&peo_uid=" + this.lab_people.Text + "&depart=" + this.ddl_QryDepart.SelectedValue;
            #endregion

            #region 快速新增--日期預設值
            this.cl_date._ADDate = Convert.ToDateTime(changeobj.ROCDTtoADDT(this.lab_date.Text));
            #endregion
        }
        catch (Exception ex)
        {
            aMSG = "功能名稱:行事曆-日--顯示出行事曆(右邊版面)<br>錯誤訊息:" + ex.ToString();
            Response.Write(aMSG);
        }
    }
    #endregion

    #region show行程
    private void Display(string today, int weeks, string txt, int no, int setuid)
    {
        string aMSG = "";
        try
        {
            string txt1 = "";
            if (no > 0)
            {
                if (this.lab_people.Text.Equals(sobj.sessionUserID) || setuid.ToString().Equals(sobj.sessionUserID))
                {
                    txt1 = "<a href=\"100301-0.aspx?no=" + no.ToString() + "&peo_uid=" + this.lab_people.Text + "&today=" + today + "&depart=" + this.ddl_QryDepart.SelectedValue + "&source=2&height=480&width=800&TB_iframe=true&modal=true\" class=\"thickbox\">" + txt + "</a>" + "<br />";
                }
                else
                {
                    txt1 = txt + "<br />";
                }
            }
            else
            {
                txt1 = txt + "<br />";
            }

            ((Label)this.Master.FindControl("ContentPlaceHolder1").FindControl("lab_" + weeks.ToString())).Text += txt1;
        }
        catch (Exception ex)
        {
            aMSG = "功能名稱:行事曆-日--行事曆-日--Display<br>錯誤訊息:" + ex.ToString();
            Response.Write(aMSG);
        }
    }
    #endregion

    #region 日曆建立時
    protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
    {
        string aMSG = "";
        try
        {
            if (e.Day.IsToday)
                e.Cell.CssClass = "today"; //今日
            else if (e.Day.IsWeekend)
                e.Cell.CssClass = "holiday_bg"; //假日
            else
                e.Cell.CssClass = "Nholiday_bg"; //非假日

            if (e.Day.IsOtherMonth)
            {
                #region 其他月份
                e.Cell.Text = "<a href=\"" + localurl + "?today=" + changeobj.ADDTtoROCDT(e.Day.Date.ToString("yyyy-MM-dd")) + "&peo_uid=" + this.lab_people.Text + "&depart=" + this.ddl_QryDepart.SelectedValue + "\" class=\"othermonth\">" + e.Day.Date.Day.ToString() + "</a>";
                //e.Cell.Text = e.Day.Date.Day.ToString();
                #endregion
            }
            else
            {
                #region 本月
                e.Cell.Text = "<a href=\"" + localurl + "?today=" + changeobj.ADDTtoROCDT(e.Day.Date.ToString("yyyy-MM-dd")) + "&peo_uid=" + this.lab_people.Text + "&depart=" + this.ddl_QryDepart.SelectedValue + "\" class=\"month\">" + e.Day.Date.Day.ToString() + "</a>";
                #endregion
            }
        }
        catch (Exception ex)
        {
            aMSG = "功能名稱:個人行事曆/本月日曆初始化<br>錯誤訊息:" + ex.ToString();
            Response.Write(aMSG);
        }
    }
    #endregion

    #region 確定
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        string aMSG = "";   //記錄錯誤訊息
        try
        {
            DateTime sdate = new DateTime();
            DateTime edate = new DateTime();
            #region 檢查輸入值
            if (this.txt_title.Text.Trim().Length <= 0)
            {
                ShowMSG("標題 不可為空白");
                return;
            }
            else if (this.txt_title.Text.Length > 100)
            {
                ShowMSG("標題 長度不可超過100個中文字");
                return;
            }
            if (this.cl_date._ADDate == null)
            {
                ShowMSG("請選擇 日期");
                return;
            }
            if (this.ddl_stime.SelectedValue.Equals("0"))
            {
                ShowMSG("請選擇 開始時間");
                return;
            }
            if (this.ddl_etime.SelectedValue.Equals("0"))
            {
                ShowMSG("請選擇 結束時間");
                return;
            }
            sdate = Convert.ToDateTime(this.cl_date._ADDate.ToString("yyyy/MM/dd") + " " + this.ddl_stime.SelectedValue + ":00");
            edate = Convert.ToDateTime(this.cl_date._ADDate.ToString("yyyy/MM/dd") + " " + this.ddl_etime.SelectedValue + ":00");
            if (sdate > edate)
            {
                ShowMSG("結束日期不得小於開始日期");
                return;
            }
            #endregion

            int newpk = 0;
            #region 取得pk
            if (new C02DAO().GetMaxNoByPeoUid(Convert.ToInt32(this.lab_people.Text)) != null)
            {
                newpk = new C02DAO().GetMaxNoByPeoUid(Convert.ToInt32(this.lab_people.Text)) + 1;
            }
            else
            {
                newpk = 1;
            }
            #endregion

            #region 單一筆新增
            C02DAO c02DAO1 = new C02DAO();
            c02 newRow = new c02();
            newRow.peo_uid = Convert.ToInt32(this.lab_people.Text);
            newRow.c02_bgcolor = "#FFFFFF";
            newRow.c02_createtime = System.DateTime.Now;
            newRow.c02_createuid = Convert.ToInt32(sobj.sessionUserID);
            newRow.c02_edate = edate;
            newRow.c02_no = newpk;
            newRow.c02_place = "";
            newRow.c02_project = "";
            newRow.c02_result = "";
            newRow.c02_sdate = sdate;
            newRow.c02_setuid = Convert.ToInt32(sobj.sessionUserID);
            newRow.c02_title = this.txt_title.Text;
            c02DAO1.AddC02(newRow);
            c02DAO1.Update();
            #endregion

            //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
            new OperatesObject().ExecuteOperates(100301, sobj.sessionUserID, 1, "行事曆快速新增 peo_uid" + this.lab_people.Text + ",c02_no=" + newpk);

            Response.Write(PCalendarUtil.ShowMsg_URL("", localurl + "?today=" + changeobj.ADDTtoROCDT(this.cl_date._ADDate.ToString("yyyy-MM-dd")) + "&peo_uid=" + this.lab_people.Text + "&depart=" + this.ddl_QryDepart.SelectedValue));
        }
        catch (Exception ex)
        {
            aMSG = "功能名稱:行事曆--快速新增<br>錯誤訊息:" + ex.ToString();
            Response.Write(aMSG);
        }
    }
    #endregion

    #region 部門更換時
    protected void ddl_QryDepart_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.ddl_QryPeople.Items.Clear();
        string sqlstr = "SELECT people.peo_uid, people.peo_name FROM people LEFT OUTER JOIN types ON people.peo_pfofess = types.typ_no WHERE (types.typ_code = 'profess') AND (people.dep_no = " + this.ddl_QryDepart.SelectedValue + ") ORDER BY types.typ_order, people.peo_name";
        DataTable dt = new DataTable();
        dt = dbo.ExecuteQuery(sqlstr);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ListItem newitem = new ListItem(dt.Rows[i]["peo_name"].ToString(), dt.Rows[i]["peo_uid"].ToString());
                this.ddl_QryPeople.Items.Add(newitem);
            }
        }
        ListItem selitem = new ListItem("請選擇", "0");
        this.ddl_QryPeople.Items.Insert(0, selitem);

        Show();
    }
    #endregion

    #region 月曆改變時
    protected void Calendar1_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
    {
        Show();
    }
    #endregion

    #region 列印
    protected void btn_print_Click(object sender, EventArgs e)
    {
        //string ax = "100";
        //string ay = "100";
        //Response.Write("<script>newwindow=window.open('cal0101-print.aspx?today=" + this.lab_date.Text + "&peo_uid=" + this.lab_peo_uid.Text + "&printtype=1','new_wealthy_calendar','height=580,width=700,toolbar=0,location=0,directories=0,status=0,menubar=1,scrollbars=1,resizable=1');newwindow.focus();newwindow.moveTo(" + ax + "," + ay + ")</script>");

        //Show();
    }
    #endregion

    #region 返回原使用者
    protected void btn_back_Click(object sender, EventArgs e)
    {
        Response.Write(PCalendarUtil.ShowMsg_URL("", localurl + "?today=" + this.lab_date.Text + "&peo_uid=" + sobj.sessionUserID + "&depart=" + this.ddl_QryDepart.SelectedValue));
    }
    #endregion

    #region 可查看之他人行事曆--搜尋
    protected void btn_QrySubmit_Click(object sender, EventArgs e)
    {
        if (this.ddl_QryPeople.SelectedValue.Equals("0"))
        {
            ShowMSG("請選擇查看之人員");
        }
        else
        {
            this.lab_people.Text = this.ddl_QryPeople.SelectedValue;
            this.btn_back.Visible = true;
            this.Calendar1.VisibleDate = Convert.ToDateTime(changeobj.ROCDTtoADDT(this.lab_date.Text));
        }
        Response.Write(PCalendarUtil.ShowMsg_URL("", localurl + "?today=" + this.lab_date.Text + "&peo_uid=" + this.lab_people.Text + "&depart=" + this.ddl_QryDepart.SelectedValue));
    }
    #endregion

    #region 可設定之他人行事曆--搜尋
    protected void btn_SetSubmit0_Click(object sender, EventArgs e)
    {
        if (this.ddl_c01.SelectedValue.Equals("0"))
        {
            ShowMSG("請選擇設定之人員");
        }
        else
        {
            this.lab_people.Text = this.ddl_c01.SelectedValue;
            this.btn_back.Visible = true;
            this.Calendar1.VisibleDate = Convert.ToDateTime(changeobj.ROCDTtoADDT(this.lab_date.Text));
        }
        Response.Write(PCalendarUtil.ShowMsg_URL("", localurl + "?today=" + this.lab_date.Text + "&peo_uid=" + this.lab_people.Text + "&depart=" + this.ddl_QryDepart.SelectedValue));
    }
    #endregion

    #region 顯示錯誤訊息
    private void ShowMSG(string msg)
    {
        string script = "<script>alert('" + msg + "');</script>";
        this.ClientScript.RegisterStartupScript(this.GetType(), "msg", script);
    }
    #endregion
}