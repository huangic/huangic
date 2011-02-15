using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using NXEIP.DAO;
using Entity;


public partial class _10_100300_100301_p : System.Web.UI.Page
{
    ChangeObject changeobj = new ChangeObject();
    SessionObject sobj = new SessionObject();
    DBObject dbo = new DBObject();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
            new OperatesObject().ExecuteOperates(100301, sobj.sessionUserID, 2, "點選個人行事曆-列印");

            #region 初始值
            if (Request["peo_uid"] != null) this.lab_people.Text = Request["peo_uid"];
            if (Request["today"] != null) this.lab_today.Text = Request["today"];
            if (Request["printtype"] != null) this.lab_printtype.Text = Request["printtype"];
            if (this.lab_today.Text.Length == 0 || this.lab_people.Text.Length == 0)
            {
                Response.Write("<script>alert(\"查無資料\");</script>");
                return;
            }
            #endregion
            this.lab_name.Text = new PeopleDAO().GetPeopleNameByUid(Convert.ToInt32(this.lab_people.Text)); //姓名
            this.Panel1.Visible = false;
            this.Panel2.Visible = false;
            this.Panel3.Visible = false;
            this.Panel4.Visible = false;
            this.lab_Month.Visible = false;

            bool isShow = PCalendarUtil.IsShow(sobj.sessionUserID, sobj.sessionUserDepartID, this.lab_people.Text);

            if (this.lab_printtype.Text.Equals("days"))
                PrintDays(isShow); //日
            else if (this.lab_printtype.Text.Equals("weeks"))
                PrintWeek(isShow); //星期
            else if (this.lab_printtype.Text.Equals("months"))
                PrintMonth(isShow); //月
            else if (this.lab_printtype.Text.Equals("lists"))
                PrintList(isShow); //列表
        }
    }

    #region PrintDays出行事曆
    private void PrintDays(bool isShow)
    {
        string aMSG = "";
        try
        {
            this.Panel1.Visible = true;
            this.lab_date.Text = this.lab_today.Text;
            DataTable dt99 = new DataTable();
            string sqlstr99 = "";

            #region 表身(清空)
            int a = 6;
            for (int i = 0; i < 18; i++)
            {
                this.Table1.Rows.Add(new System.Web.UI.WebControls.TableRow());
                this.Table1.Rows[i + 1].BackColor = Color.White;

                this.Table1.Rows[i + 1].Cells.Add(new System.Web.UI.WebControls.TableCell());
                this.Table1.Rows[i + 1].Cells.Add(new System.Web.UI.WebControls.TableCell());

                this.Table1.Rows[i + 1].Cells[0].CssClass = "timecss2";
                this.Table1.Rows[i + 1].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                this.Table1.Rows[i + 1].Cells[1].CssClass = "timecss2";
                this.Table1.Rows[i + 1].Cells[0].Height = Unit.Pixel(24);

                if ((a + i) < 10)
                    this.Table1.Rows[i + 1].Cells[0].Text = "0" + (a + i) + ":00";
                else
                    this.Table1.Rows[i + 1].Cells[0].Text = (a + i) + ":00";

            }
            #endregion

            #region 行事曆
            string sdate = changeobj.ROCDTtoADDT(this.lab_today.Text) + " 00:00:00";
            string edate = changeobj.ROCDTtoADDT(this.lab_today.Text) + " 23:59:59";
            if (isShow)
            {
                sqlstr99 = "SELECT peo_uid, c02_no, c02_sdate, c02_edate, c02_title, c02_bgcolor, c02_setuid FROM c02 "
                + " WHERE (peo_uid = " + this.lab_people.Text + ") AND (c02_sdate <= '" + edate + "') AND (c02_edate <= '" + edate + "') AND (c02_edate >= '" + sdate + "') "
                + " OR (peo_uid = " + this.lab_people.Text + ") AND (c02_sdate >= '" + sdate + "') AND (c02_edate >= '" + sdate + "') AND (c02_sdate <= '" + edate + "') "
                + " OR (peo_uid = " + this.lab_people.Text + ") AND (c02_sdate < '" + sdate + "') AND (c02_edate > '" + edate + "') ORDER BY c02_sdate, c02_edate, c02_no";
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
                        Display1(stime, etime, "■" + stime + "~" + etime + " " + dt99.Rows[i]["c02_title"].ToString());
                    }
                }
            }
            #endregion

            #region 表格合併
            int k, n;
            for (int i = 1; i < this.Table1.Rows[0].Cells.Count; i++)
            {
                for (int l = 1; l < this.Table1.Rows.Count; l++)
                {
                    n = 1;
                    for (k = l + 1; k < this.Table1.Rows.Count; k++)
                    {

                        if (this.Table1.Rows[l].Cells[i].Text.Trim().Equals(this.Table1.Rows[k].Cells[i].Text.Trim()) && !this.Table1.Rows[l].Cells[i].Text.Equals(""))
                        {
                            n += 1;
                            this.Table1.Rows[l].Cells[i].RowSpan = n;
                            this.Table1.Rows[k].Cells[i].Visible = false;
                        }
                        else break;
                    }
                    l = k - 1;
                }
            }
            #endregion

            #region 表頭合併
            if (this.Table1.Rows[0].Cells.Count > 2)
            {
                this.Table1.Rows[0].Cells[1].ColumnSpan = this.Table1.Rows[0].Cells.Count - 1;
                for (int i = 1; i < this.Table1.Rows[0].Cells.Count - 1; i++)
                {
                    this.Table1.Rows[0].Cells[1 + i].Visible = false;
                }
            }
            #endregion
        }
        catch (Exception ex)
        {
            aMSG = "功能名稱:行事曆-日--列印<br>錯誤訊息:" + ex.ToString();
            Response.Write(aMSG);
        }
    }
    #endregion

    #region Display1
    private void Display1(string stime, string etime, string txt1)
    {
        string aMSG = "";
        try
        {
            DateTime sdate1 = Convert.ToDateTime(changeobj.ROCDTtoADDT(this.lab_today.Text) + " " + stime + ":00");
            DateTime edate1 = Convert.ToDateTime(changeobj.ROCDTtoADDT(this.lab_today.Text) + " " + etime + ":00");
            int begin = sdate1.Hour;
            int end = edate1.Hour;
            if (begin < end)
            {
                if (edate1.Minute == 0) end = end - 1;
            }

            int count = 0;
            int cel = 0;

            for (int j = 1; j < this.Table1.Rows[0].Cells.Count; j++)
            {
                int count1 = 0;
                for (int i = begin; i <= end; i++)
                {
                    if (i >= 6 && i <= 23)
                    {
                        if (this.Table1.Rows[i - 6 + 1].Cells[j].Text.Equals(""))
                        {
                        }
                        else
                        {
                            count1++;
                        }
                    }
                }
                if (count1 == 0)
                {
                    cel = j;
                    count = 0;
                    break;
                }
                else
                {
                    count++;
                }
            }
            if (count == 0)
            {
                for (int i = begin; i <= end; i++)
                {
                    if (i >= 6 && i <= 23)
                    {
                        if (this.Table1.Rows[i - 6 + 1].Cells[cel].Text.Equals(""))
                        {
                            this.Table1.Rows[i - 6 + 1].Cells[cel].Text = txt1;
                            this.Table1.Rows[i - 6 + 1].Cells[cel].CssClass = "timecss2";
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < 19; i++)
                {
                    this.Table1.Rows[i].Cells.Add(new System.Web.UI.WebControls.TableCell());
                    this.Table1.Rows[i].Cells[1].CssClass = "timecss2";
                }
                cel = this.Table1.Rows[0].Cells.Count - 1;
                for (int i = 0; i < 19; i++)
                {
                    this.Table1.Rows[i].Cells[cel].CssClass = "timecss2";
                }

                for (int i = begin; i <= end; i++)
                {
                    if (i >= 6 && i <= 23)
                    {
                        if (this.Table1.Rows[i - 6 + 1].Cells[cel].Text.Equals(""))
                        {
                            this.Table1.Rows[i - 6 + 1].Cells[cel].Text = txt1;
                            this.Table1.Rows[i - 6 + 1].Cells[cel].CssClass = "timecss2";
                        }
                    }
                }

            }
        }
        catch (Exception ex)
        {
            aMSG = "功能名稱:行事曆-日--列印--Display1<br>錯誤訊息:" + ex.ToString();
            Response.Write(aMSG);
        }
    }
    #endregion

    #region PrintWeek顯示行事曆
    private void PrintWeek(bool isShow)
    {
        string aMSG = "";
        try
        {
            DataTable dt99 = new DataTable();
            string sqlstr99 = "";

            this.Panel2.Visible = true;
            #region 初始化
            for (int i = 0; i < 7; i++)
            {
                ((Label)this.FindControl("lab_" + i.ToString())).Text = "";
            }
            #endregion

            #region 週行事曆
            DateTime sdate = new DateTime();
            DateTime edate = new DateTime();
            sdate = Convert.ToDateTime(changeobj.ROCDTtoADDT(this.lab_today.Text));
            sdate = sdate.AddDays(-(int)changeobj.ChangeWeek(sdate));
            edate = sdate.AddDays(7);

            this.lab_Month.Text = (Convert.ToDateTime(changeobj.ROCDTtoADDT(this.lab_today.Text)).Year - 1911) + " 年 " + Convert.ToDateTime(changeobj.ROCDTtoADDT(this.lab_today.Text)).Month.ToString("0#") + " 月";
            this.lab_Month.Visible = true;

            while (sdate < edate)
            {
                string sdate1 = changeobj.ADDTtoROCDT(sdate.ToString("yyyy-MM-dd"));

                #region 日期、連結
                int weeks = changeobj.ChangeWeek(sdate);
                ((HyperLink)this.FindControl("hl_" + weeks.ToString())).Text = sdate1;
                #endregion

                #region 行事曆
                string sdate2 = sdate.ToString("yyyy/MM/dd") + " 00:00:00";
                string edate2 = sdate.ToString("yyyy/MM/dd") + " 23:59:59";
                if (isShow)
                {
                    sqlstr99 = "SELECT peo_uid, c02_no, c02_sdate, c02_edate, c02_title, c02_bgcolor, c02_setuid FROM c02 "
                    + " WHERE (peo_uid = " + this.lab_people.Text + ") AND (c02_sdate <= '" + edate2 + "') AND (c02_edate <= '" + edate2 + "') AND (c02_edate >= '" + sdate2 + "') "
                    + " OR (peo_uid = " + this.lab_people.Text + ") AND (c02_sdate >= '" + sdate2 + "') AND (c02_edate >= '" + sdate2 + "') AND (c02_sdate <= '" + edate2 + "') "
                    + " OR (peo_uid = " + this.lab_people.Text + ") AND (c02_sdate < '" + sdate2 + "') AND (c02_edate > '" + edate2 + "') ORDER BY c02_sdate, c02_edate, c02_no";
                    dt99.Clear();
                    dt99 = dbo.ExecuteQuery(sqlstr99);
                    if (dt99.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt99.Rows.Count; i++)
                        {
                            string stime = "";
                            string etime = "";
                            if (Convert.ToDateTime(dt99.Rows[i]["c02_sdate"].ToString()).ToString("yyyy-MM-dd").Equals(sdate.ToString("yyyy-MM-dd")))
                            {
                                stime = Convert.ToDateTime(dt99.Rows[i]["c02_sdate"].ToString()).ToString("HH:mm");
                            }
                            else
                            {
                                stime = "00:00";
                            }
                            if (Convert.ToDateTime(dt99.Rows[i]["c02_edate"].ToString()).ToString("yyyy-MM-dd").Equals(sdate.ToString("yyyy-MM-dd")))
                            {
                                etime = Convert.ToDateTime(dt99.Rows[i]["c02_edate"].ToString()).ToString("HH:mm");
                            }
                            else
                            {
                                etime = "24:00";
                            }
                            Display2(sdate1, changeobj.ChangeWeek(sdate), "■" + stime + "~" + etime + " " + dt99.Rows[i]["c02_title"].ToString());
                        }
                    }
                }
                #endregion

                sdate = sdate.AddDays(1);
            }
            #endregion
        }
        catch (Exception ex)
        {
            aMSG = "功能名稱:行事曆-週--列印<br>錯誤訊息:" + ex.ToString();
            Response.Write(aMSG);
        }
    }
    #endregion

    #region Display2
    private void Display2(string today, int weeks, string txt)
    {
        string aMSG = "";
        try
        {
            string txt1 = txt + "<br />";

            ((Label)this.FindControl("lab_" + weeks.ToString())).Text += txt1;
        }
        catch (Exception ex)
        {
            aMSG = "功能名稱:行事曆-週--列印--display2<br>錯誤訊息:" + ex.ToString();
            Response.Write(aMSG);
        }
    }
    #endregion

    #region PrintMonth顯示行事曆
    private void PrintMonth(bool isShow)
    {
        string aMSG = "";
        try
        {
            this.Panel3.Visible = true;
            this.lab_date.Visible = false;
            this.lab_Month.Visible = true;

            DateTime today = Convert.ToDateTime(changeobj.ROCDTtoADDT(this.lab_today.Text));
            this.lab_Month.Text = Convert.ToString(today.Year-1911) + "年" + today.Month.ToString() + "月";
            this.lab_date.Text = this.lab_today.Text;

            this.Calendar1.VisibleDate = Convert.ToDateTime(changeobj.ROCDTtoADDT(this.lab_today.Text));
            this.Calendar1.TodaysDate = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd"));//月曆初始值(左)
        }
        catch (Exception ex)
        {
            aMSG = "功能名稱:行事曆-月--列印<br>錯誤訊息:" + ex.ToString();
            Response.Write(aMSG);
        }
    }
    #endregion

    #region 該月日曆初始化
    protected void Calendar1_DayRender(object sender, System.Web.UI.WebControls.DayRenderEventArgs e)
    {
        string aMSG = "";
        try
        {
            if (!e.Day.IsOtherMonth)
            {
                string eDay = changeobj.ADDTtoROCDT(e.Day.Date.ToString("yyyy-MM-dd"));
                string celltxt = "<b>" + e.Day.Date.Day.ToString() + "</b>";
                bool isShow = PCalendarUtil.IsShow(sobj.sessionUserID, sobj.sessionUserDepartID, this.lab_people.Text);

                #region 行事曆
                DataTable dt99 = new DataTable();
                string sqlstr99 = "";
                string sdate = e.Day.Date.ToString("yyyy/MM/dd") + " 00:00:00";
                string edate = e.Day.Date.ToString("yyyy/MM/dd") + " 23:59:59";
                if (isShow)
                {
                    sqlstr99 = "SELECT peo_uid, c02_no, c02_sdate, c02_edate, c02_title, c02_bgcolor, c02_setuid FROM c02 "
                    + " WHERE (peo_uid = " + this.lab_people.Text + ") AND (c02_sdate <= '" + edate + "') AND (c02_edate <= '" + edate + "') AND (c02_edate >= '" + sdate + "') "
                    + " OR (peo_uid = " + this.lab_people.Text + ") AND (c02_sdate >= '" + sdate + "') AND (c02_edate >= '" + sdate + "') AND (c02_sdate <= '" + edate + "') "
                    + " OR (peo_uid = " + this.lab_people.Text + ") AND (c02_sdate < '" + sdate + "') AND (c02_edate > '" + edate + "') ORDER BY c02_sdate, c02_edate, c02_no";
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

                            celltxt += "<br>■" + stime + "~" + etime + "<br>" + dt99.Rows[i]["c02_title"].ToString();
                        }
                    }
                }
                #endregion

                e.Cell.Text = "<div class=\"month\">" + celltxt+"</div>";
            }
            else
            {
                e.Cell.Text = "<div class=\"month\"><b>" + e.Day.Date.Day.ToString() + "</b></div>";
            }
        }
        catch (Exception ex)
        {
            aMSG = "功能名稱:行事曆-月--該月日曆初始化<br>錯誤訊息:" + ex.ToString();
            Response.Write(aMSG);
        }
    }
    #endregion

    #region PrintList顯示行事曆
    private void PrintList(bool isShow)
    {
        string aMSG = "";
        try
        {
            this.Panel4.Visible = true;
            this.lab_date.Visible = false;
            this.lab_Month.Visible = true;

            DateTime today = Convert.ToDateTime(changeobj.ROCDTtoADDT(this.lab_today.Text));
            this.lab_Month.Text = Convert.ToString(today.Year-1911) + "年" + today.Month.ToString() + "月";
            this.lab_date.Text = this.lab_today.Text;

            DataTable dt99 = new DataTable();
            string sqlstr99 = "";

            #region 基本表格
            this.Table2.Rows.Clear();
            this.Table2.Dispose();
            int rowcount = -1;
            string sdate = today.ToString("yyyy/MM/01") + " 00:00:00";
            string edate = today.AddMonths(1).AddDays(-1).ToString("yyyy/MM/dd") + " 23:59:59";
            if (isShow)
            {
                sqlstr99 = "SELECT peo_uid, c02_no, c02_sdate, c02_edate, c02_title, c02_bgcolor, c02_setuid FROM c02 "
                + " WHERE (peo_uid = " + this.lab_people.Text + ") AND (c02_sdate <= '" + edate + "') AND (c02_edate <= '" + edate + "') AND (c02_edate >= '" + sdate + "') "
                + " OR (peo_uid = " + this.lab_people.Text + ") AND (c02_sdate >= '" + sdate + "') AND (c02_edate >= '" + sdate + "') AND (c02_sdate <= '" + edate + "') "
                + " OR (peo_uid = " + this.lab_people.Text + ") AND (c02_sdate < '" + sdate + "') AND (c02_edate > '" + edate + "') ORDER BY c02_sdate, c02_edate, c02_no";
                dt99 = dbo.ExecuteQuery(sqlstr99);
                if (dt99.Rows.Count > 0)
                {
                    string update = "";
                    for (int i = 0; i < dt99.Rows.Count; i++)
                    {
                        DateTime nowdate = Convert.ToDateTime(dt99.Rows[i]["c02_sdate"].ToString());
                        if (!update.Equals(Convert.ToDateTime(dt99.Rows[i]["c02_sdate"].ToString()).ToString("MM-dd")))
                        {
                            rowcount++;
                            this.Table2.Rows.Add(new System.Web.UI.WebControls.TableRow());
                            this.Table2.Rows[rowcount].Cells.Add(new System.Web.UI.WebControls.TableCell());
                            this.Table2.Rows[rowcount].Cells.Add(new System.Web.UI.WebControls.TableCell());
                            this.Table2.Rows[rowcount].Cells[0].CssClass = "timecss2";
                            this.Table2.Rows[rowcount].Cells[1].CssClass = "timecss2";
                            this.Table2.Rows[rowcount].Cells[0].Height = Unit.Pixel(60);
                            this.Table2.Rows[rowcount].Cells[0].Width = Unit.Pixel(80);
                            this.Table2.Rows[rowcount].Cells[0].HorizontalAlign = HorizontalAlign.Center;

                            this.Table2.Rows[rowcount].Cells[0].Text = "<span class=\"row_time\">" + nowdate.ToString("MM-dd") + "<br />星期" +
                            changeobj.ChangeWeek(Convert.ToDateTime(dt99.Rows[i]["c02_sdate"].ToString()).DayOfWeek) + "</span>";
                        }
                        update = nowdate.ToString("MM-dd");
                        string stime = nowdate.ToString("HH:mm");
                        string etime = Convert.ToDateTime(dt99.Rows[i]["c02_edate"].ToString()).ToString("HH:mm");

                        this.Table2.Rows[rowcount].Cells[1].Text += "<li class=\"p1\">" + Display3(changeobj.ADDTtoROCDT(nowdate.ToString("yyyy-MM-dd")), stime + "~" + etime + " " + dt99.Rows[i]["c02_title"].ToString(), Convert.ToInt32(dt99.Rows[i]["c02_no"].ToString()), Convert.ToInt32(dt99.Rows[i]["c02_setuid"].ToString())) + "</li>";
                    }
                }
            }
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
    private string Display3(string today, string txt, int no, int setuid)
    {
        string aMSG = "";
        string txt1 = "";
        try
        {
            if (no > 0)
            {
                if (this.lab_people.Text.Equals(sobj.sessionUserID) || setuid.ToString().Equals(sobj.sessionUserID))
                    txt1 = "<span class=\"row_schedule\">" + txt + "</span>" + "<br />";
                else
                    txt1 = txt + "<br />";
            }
            else
            {
                txt1 = txt + "<br />";
            }
        }
        catch (Exception ex)
        {
            aMSG = "功能名稱:行事曆-日--行事曆-日--Display<br>錯誤訊息:" + ex.ToString();
            Response.Write(aMSG);
        }
        return txt1;
    }
    #endregion
}