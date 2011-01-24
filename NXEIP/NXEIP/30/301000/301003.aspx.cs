using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class _30_301000_301003 : System.Web.UI.Page
{
    ChangeObject changeobj = new ChangeObject();
    DBObject dbo = new DBObject();
    SessionObject sobj = new SessionObject();
    CheckObject checkobj = new CheckObject();

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Navigator1.SubFunc = "月";
        if (!this.IsPostBack)
        {
            //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
            new OperatesObject().ExecuteOperates(301003, sobj.sessionUserID, 2, "點選設備使用記錄-月");

            #region 設備管理之所在地、設備
            this.ddl_spot.Items.Add(new ListItem("全部", "0"));
            this.ddl_equ.Items.Add(new ListItem("全部", "0"));
            string sqlstr = "select distinct spot.spo_no, spot.spo_name from spot inner join equipments on spot.spo_no = equipments.spo_no "
                + " where (spot.spo_function like '_____1%') and (spot.spo_status = '1') and (equipments.equ_status = '1') ";
            DataTable dt = new DataTable();
            dt = dbo.ExecuteQuery(sqlstr);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.ddl_spot.Items.Add(new ListItem(dt.Rows[i]["spo_name"].ToString(), dt.Rows[i]["spo_no"].ToString()));
                }
            }
            #endregion

            #region 預設值(設備管理之所在地、設備)
            if (Request["spot"] != null && Request["spot"].Length > 0)
            {
                try
                {
                    this.ddl_spot.SelectedItem.Selected = false;
                    this.ddl_spot.Items.FindByValue(Request["spot"]).Selected = true;
                    if (!this.ddl_spot.SelectedValue.Equals("0"))
                    {
                        sqlstr = "select equ_no, equ_name from equipments where (equ_status = '1') and spo_no=" + this.ddl_spot.SelectedValue+" order by equ_number,equ_no";
                        dt.Clear();
                        dt = dbo.ExecuteQuery(sqlstr);
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                this.ddl_equ.Items.Add(new ListItem(dt.Rows[i]["equ_name"].ToString(), dt.Rows[i]["equ_no"].ToString()));
                            }
                        }
                    }
                }
                catch { }
            }
            if (Request["equ"] != null && Request["equ"].Length > 0)
            {
                try
                {
                    this.ddl_equ.SelectedItem.Selected = false;
                    this.ddl_equ.Items.FindByValue(Request["equ"]).Selected = true;
                }
                catch { }
            }
            #endregion

            #region 日期
            if (Request["today"] != null && Request["today"].Length > 0)
                this.Calendar1.VisibleDate = Convert.ToDateTime(Request["today"]);
            else
                this.Calendar1.VisibleDate = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd"));
            this.Calendar1.TodaysDate = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd"));
            ShowYearMonth();
            ChangeWeeksLink();
            #endregion
        }
    }

    #region 所在地更換時
    protected void ddl_spot_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.ddl_equ.Items.Clear();
        this.ddl_equ.Items.Add(new ListItem("全部", "0"));
        #region 場所
        if (!this.ddl_spot.SelectedValue.Equals("0"))
        {
            string sqlstr = "select equ_no, equ_name from equipments where (equ_status = '1') and spo_no=" + this.ddl_spot.SelectedValue + " order by equ_number,equ_no";
            DataTable dt = new DataTable();
            dt = dbo.ExecuteQuery(sqlstr);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.ddl_equ.Items.Add(new ListItem(dt.Rows[i]["equ_name"].ToString(), dt.Rows[i]["equ_no"].ToString()));
                }
            }
        }
        #endregion

        this.Calendar1.DataBind();
        ChangeWeeksLink();
    }
    #endregion

    #region 設備切換時
    protected void ddl_equ_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Calendar1.DataBind();
        ChangeWeeksLink();
    }
    #endregion

    #region 月曆初始化
    protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
    {
        string aMSG = "";
        try
        {
            if (e.Day.IsOtherMonth)
            {
                #region 其他月份
                e.Cell.Text = "<span class=\"othermonth\">" + e.Day.Date.Day.ToString() + "</span>";
                #endregion
            }
            else
            {
                #region 本月
                string eDay = changeobj.ADDTtoROCDT(e.Day.Date.ToString("yyyy-MM-dd"));
                string celltxt = "<span class=\"month\">" + e.Day.Date.Day.ToString() + "</span><br />";

                #region 設備借用記錄
                string sdate = e.Day.Date.ToString("yyyy/MM/dd") + " 00:00:00";
                string edate = e.Day.Date.ToString("yyyy/MM/dd") + " 23:59:59";
                DataTable dt = new DataTable();
                string sqlstr = "SELECT borrows.bor_no, borrows.equ_no, equipments.equ_name, borrows.bor_stime, borrows.bor_etime, departments.dep_name, people.peo_name "
                    + " from borrows INNER JOIN equipments ON borrows.equ_no = equipments.equ_no INNER JOIN people ON borrows.bor_applyuid = people.peo_uid INNER JOIN departments ON borrows.bor_depno = departments.dep_no"
                    + " WHERE (borrows.bor_apply IN ('1', '2')) AND (borrows.bor_stime >= '" + sdate + "') AND (borrows.bor_etime <= '" + edate + "') ";
                if (!this.ddl_spot.SelectedValue.Equals("0")) sqlstr += " and equipments.spo_no=" + this.ddl_spot.SelectedValue;
                if (!this.ddl_equ.SelectedValue.Equals("0")) sqlstr += " and borrows.equ_no=" + this.ddl_equ.SelectedValue;
                sqlstr += " ORDER BY equipments.equ_number, borrows.bor_stime, borrows.bor_etime";
                dt = dbo.ExecuteQuery(sqlstr);
                string updepno = "0";
                if (dt.Rows.Count > 0)
                {
                    updepno = dt.Rows[0]["equ_no"].ToString();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (!updepno.Equals(dt.Rows[i]["equ_no"].ToString()))
                        {
                            celltxt += "<hr>";
                            updepno = dt.Rows[i]["equ_no"].ToString();
                        }

                        DateTime pet_stime = Convert.ToDateTime(dt.Rows[i]["bor_stime"].ToString());
                        DateTime pet_etime = Convert.ToDateTime(dt.Rows[i]["bor_etime"].ToString());
                        //6樓會議室 08：00～12：00 人事室：鄭先生
                        celltxt += "<span class=\"a-letter\">◆" + dt.Rows[i]["equ_name"].ToString() + " " + pet_stime.ToString("HH:mm") + "～" + pet_etime.ToString("HH:mm") + " " + dt.Rows[i]["dep_name"].ToString() + "：" + dt.Rows[i]["peo_name"].ToString() + "</span><br />";

                    }
                }
                #endregion

                e.Cell.Text = celltxt;
                #endregion
            }
        }
        catch (Exception ex)
        {
            aMSG = "功能名稱:設備使用記錄<br>錯誤訊息:" + ex.ToString();
            Response.Write(aMSG);
        }
    }
    #endregion

    #region 顯示年月
    public void ShowYearMonth()
    {
        //年份
        int thisyear = this.Calendar1.VisibleDate.Year - 1911;
        this.ddl_year.Items.Clear();
        for (int sy = thisyear - 1; sy <= thisyear + 1; sy++)
        {
            ListItem newyear = new ListItem(sy.ToString(), sy.ToString());
            this.ddl_year.Items.Add(newyear);
        }
        this.ddl_year.Items.FindByValue(thisyear.ToString()).Selected = true;

        //月份
        this.ddl_month.SelectedItem.Selected = false;
        this.ddl_month.Items.FindByValue(this.Calendar1.VisibleDate.Month.ToString("0#")).Selected = true;
    }
    #endregion

    #region 上個月、本月、下個月
    protected void ChangeMonth_Click(object sender, EventArgs e)
    {
        if (((LinkButton)sender).CommandArgument.Equals("Previous"))
            this.Calendar1.VisibleDate = this.Calendar1.VisibleDate.AddMonths(-1);
        else if (((LinkButton)sender).CommandArgument.Equals("This"))
            this.Calendar1.VisibleDate = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd"));
        else if (((LinkButton)sender).CommandArgument.Equals("Next"))
            this.Calendar1.VisibleDate = this.Calendar1.VisibleDate.AddMonths(1);

        this.Calendar1.TodaysDate = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd"));
        ShowYearMonth();
        ChangeWeeksLink();
    }
    #endregion

    #region 年、月 切換時
    protected void ddl_YearOrMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        int years = Convert.ToInt32(this.ddl_year.SelectedValue) + 1911;
        string months = this.ddl_month.SelectedValue;
        this.Calendar1.VisibleDate = Convert.ToDateTime(years.ToString() + "-" + months + "-01");
        this.Calendar1.TodaysDate = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd"));
        ShowYearMonth();
        ChangeWeeksLink();
    }
    #endregion

    #region 重新產生「周」的連結網址
    private void ChangeWeeksLink()
    {
        this.hl_weeks.NavigateUrl = "301003-1.aspx?today=" + this.Calendar1.VisibleDate.ToString("yyyy/MM/dd") + "&spot=" + this.ddl_spot.SelectedValue + "&equ=" + this.ddl_equ.SelectedValue;
        string ax = "100";
        string ay = "100";
        string script = "newwindow=window.open('301003-2.aspx?today=" + this.Calendar1.VisibleDate.ToString("yyyy/MM/dd") + "&spot=" + this.ddl_spot.SelectedValue + "&equ=" + this.ddl_equ.SelectedValue + "&printtype=months','new_301003','height=580,width=700,toolbar=0,location=0,directories=0,status=0,menubar=1,scrollbars=1,resizable=1');newwindow.focus();newwindow.moveTo(" + ax + "," + ay + ")";
        this.hl_print.NavigateUrl = "#";
        this.hl_print.Attributes["onClick"] = script;
    }
    #endregion
}