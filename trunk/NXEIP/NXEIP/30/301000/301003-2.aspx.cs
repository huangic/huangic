using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;
using System.Data;

public partial class _30_301000_301003_2 : System.Web.UI.Page
{
    ChangeObject changeobj = new ChangeObject();
    SessionObject sobj = new SessionObject();
    DBObject dbo = new DBObject();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
            new OperatesObject().ExecuteOperates(301003, sobj.sessionUserID, 2, "點選設備借用記錄-列印");

            #region 初始值
            if (Request["today"] != null) this.lab_today.Text = Request["today"]; //西元年月日
            if (Request["spot"] != null) this.lab_spot.Text = Request["spot"];
            if (Request["equ"] != null) this.lab_equ.Text = Request["equ"];
            if (Request["printtype"] != null) this.lab_printtype.Text = Request["printtype"];
            if (this.lab_today.Text.Length == 0)
            {
                Response.Write("<script>alert(\"查無資料\");</script>");
                return;
            }
            #endregion
            this.Panel1.Visible = false;
            this.Panel2.Visible = false;
            this.lab_Month.Visible = false;

            if (this.lab_printtype.Text.Equals("weeks"))
                PrintWeek(); //星期
            else if (this.lab_printtype.Text.Equals("months"))
                PrintMonth(); //月
        }
    }

    #region PrintWeek
    private void PrintWeek()
    {
        string aMSG = "";
        try
        {
            this.Panel1.Visible = true;
            this.lab_Month.Text = (Convert.ToDateTime(this.lab_today.Text).Year - 1911) + " 年 " + Convert.ToDateTime(this.lab_today.Text).Month.ToString("0#") + " 月";
            this.lab_Month.Visible = true;

            DateTime sdate = new DateTime();
            DateTime edate = new DateTime();
            sdate = Convert.ToDateTime(this.lab_today.Text);
            sdate = sdate.AddDays(-(int)changeobj.ChangeWeek(sdate));
            edate = sdate.AddDays(7);

            while (sdate < edate)
            {
                int weeks = changeobj.ChangeWeek(sdate);
                ((HyperLink)this.FindControl("hl_" + weeks.ToString())).Text = changeobj.ADDTtoROCDT(sdate.ToString("yyyy-MM-dd"));

                string sdate1 = sdate.ToString("yyyy/MM/dd") + " 00:00:00";
                string edate1 = sdate.ToString("yyyy/MM/dd") + " 23:59:59";
                #region 設備記錄
                DataTable dt = new DataTable();
                string sqlstr = "SELECT borrows.bor_no, borrows.equ_no, equipments.equ_name, borrows.bor_stime, borrows.bor_etime, departments.dep_name, people.peo_name "
                    + " from borrows INNER JOIN equipments ON borrows.equ_no = equipments.equ_no INNER JOIN people ON borrows.bor_applyuid = people.peo_uid INNER JOIN departments ON borrows.bor_depno = departments.dep_no"
                    + " WHERE (borrows.bor_apply IN ('1', '2')) AND (borrows.bor_stime >= '" + sdate1 + "') AND (borrows.bor_etime <= '" + edate1 + "') ";
                if (!this.lab_spot.Text.Equals("0")) sqlstr += " and equipments.spo_no=" + this.lab_spot.Text;
                if (!this.lab_equ.Text.Equals("0")) sqlstr += " and borrows.equ_no=" + this.lab_equ.Text;
                sqlstr += " ORDER BY equipments.equ_number, borrows.bor_stime, borrows.bor_etime";
                dt = dbo.ExecuteQuery(sqlstr);
                string updepno = "0";
                if (dt.Rows.Count > 0)
                {
                    string outxt = "";
                    updepno = dt.Rows[0]["equ_no"].ToString();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (!updepno.Equals(dt.Rows[i]["equ_no"].ToString())) outxt += "<hr>";

                        DateTime pet_stime = Convert.ToDateTime(dt.Rows[i]["bor_stime"].ToString());
                        DateTime pet_etime = Convert.ToDateTime(dt.Rows[i]["bor_etime"].ToString());
                        //◆6樓會議室 08：00～12：00 人事室：鄭先生
                        outxt += "◆" + dt.Rows[i]["equ_name"].ToString() + " " + pet_stime.ToString("HH:mm") + "～" + pet_etime.ToString("HH:mm") + " " + dt.Rows[i]["dep_name"].ToString() + "：" + dt.Rows[i]["peo_name"].ToString() + "<br />";
                    }
                    ((Label)this.FindControl("lab_" + weeks.ToString())).Text = outxt;
                }
                else
                {
                    ((Label)this.FindControl("lab_" + weeks.ToString())).Text = "&nbsp;";
                }
                #endregion
                sdate = sdate.AddDays(1);
            }
        }
        catch (Exception ex)
        {
            aMSG = "功能名稱:週--列印<br>錯誤訊息:" + ex.ToString();
            Response.Write(aMSG);
        }
    }
    #endregion

    #region PrintMonth
    private void PrintMonth()
    {
        string aMSG = "";
        try
        {
            this.Panel2.Visible = true;
            this.lab_Month.Visible = true;

            DateTime today = Convert.ToDateTime(this.lab_today.Text);
            this.lab_Month.Text = Convert.ToString(today.Year - 1911) + "年" + today.Month.ToString() + "月";

            this.Calendar1.VisibleDate = today;
            this.Calendar1.TodaysDate = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd"));//月曆初始值(左)
        }
        catch (Exception ex)
        {
            aMSG = "功能名稱:月--列印<br>錯誤訊息:" + ex.ToString();
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
                string celltxt = "<span class=\"month\">" + e.Day.Date.Day.ToString() + "</span><br />";

                #region 設備記錄
                string sdate = e.Day.Date.ToString("yyyy/MM/dd") + " 00:00:00";
                string edate = e.Day.Date.ToString("yyyy/MM/dd") + " 23:59:59";
                DataTable dt = new DataTable();
                string sqlstr = "SELECT borrows.bor_no, borrows.equ_no, equipments.equ_name, borrows.bor_stime, borrows.bor_etime, departments.dep_name, people.peo_name "
                    + " from borrows INNER JOIN equipments ON borrows.equ_no = equipments.equ_no INNER JOIN people ON borrows.bor_applyuid = people.peo_uid INNER JOIN departments ON borrows.bor_depno = departments.dep_no"
                    + " WHERE (borrows.bor_apply IN ('1', '2')) AND (borrows.bor_stime >= '" + sdate + "') AND (borrows.bor_etime <= '" + edate + "') ";
                if (!this.lab_spot.Text.Equals("0")) sqlstr += " and equipments.spo_no=" + this.lab_spot.Text;
                if (!this.lab_equ.Text.Equals("0")) sqlstr += " and borrows.equ_no=" + this.lab_equ.Text;
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
            }
            else
            {
                e.Cell.Text = "<span class=\"othermonth\">" + e.Day.Date.Day.ToString() + "</span>";
            }
        }
        catch (Exception ex)
        {
            aMSG = "功能名稱:該月日曆初始化<br>錯誤訊息:" + ex.ToString();
            Response.Write(aMSG);
        }
    }
    #endregion
}