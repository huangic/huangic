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

public partial class _20_200100_200102_1 : System.Web.UI.Page
{
    ChangeObject changeobj = new ChangeObject();
    SessionObject sobj = new SessionObject();
    DBObject dbo = new DBObject();
    protected string localurl = "200102-1.aspx";
    protected string parem = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
            new OperatesObject().ExecuteOperates(100301, sobj.sessionUserID, 2, "點選首長行程-週");

            ListItem newitem = new ListItem("請選擇", "0");
            DataTable dt = new DataTable();

            #region 左邊版面：月曆、今天日期
            //左上，月曆
            //Response.Write(Request["today"]);
            if (Request["today"] != null)
            {
                this.Calendar1.VisibleDate = Convert.ToDateTime(changeobj.ROCDTtoADDT(Request["today"]));
                this.lab_date.Text = Request["today"];
            }
            else
            {
                this.Calendar1.VisibleDate = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd"));//月曆初始值(左)
                this.lab_date.Text = changeobj.ADDTtoROCDT(System.DateTime.Now.ToString("yyyy-MM-dd"));
            }

            this.Calendar1.TodaysDate = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd"));//月曆初始值(左)
            this.lab_CYM.Text = Convert.ToString(this.Calendar1.VisibleDate.Year - 1911) + "年" + this.Calendar1.VisibleDate.Month.ToString("0#") + "月";
            this.lab_today.Text = PCalendarUtil.GetToday(); //今天日期
            #endregion

            #region 右邊版面：預設日期、首長人事編號、判斷是否可新增
            this.lab_show.Text = Convert.ToString(Convert.ToDateTime(changeobj.ROCDTtoADDT(this.lab_date.Text)).Year - 1911) + "年" + Convert.ToDateTime(changeobj.ROCDTtoADDT(this.lab_date.Text)).Month + "月";
            if (Request["peo_uid"] != null)
                this.lab_people.Text = Request["peo_uid"];
            else
            {
                this.lab_people.Text = "0";
                string sqlstr = "select lea_peouid from leading order by lea_no";
                DataTable dt_lea = new DataTable();
                dt_lea = dbo.ExecuteQuery(sqlstr);
                if (dt_lea.Rows.Count > 0) this.lab_people.Text = dt_lea.Rows[0]["lea_peouid"].ToString();
            }
            #endregion

            Show();
        }
    }

    #region 顯示出首長行程(右邊版面)
    private void Show()
    {
        string aMSG = "";
        try
        {
            DataTable dt99 = new DataTable();
            string sqlstr99 = "";
            this.lab_name.Text = new PeopleDAO().GetPeopleNameByUid(Convert.ToInt32(this.lab_people.Text)); //姓名

            #region 週首長行程初始化--清空
            for (int i = 0; i < 7; i++)
            {
                ((Label)this.Master.FindControl("ContentPlaceHolder1").FindControl("lab_" + i.ToString())).Text = "";
            }
            #endregion

            #region 週首長行程
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
                ((HyperLink)this.Master.FindControl("ContentPlaceHolder1").FindControl("hl_" + weeks.ToString())).NavigateUrl = "100301-0.aspx?today=" + sdate1 + "&" + parem + "&stime=06:00&source=weeks&height=480&width=800&TB_iframe=true&modal=true";
                #endregion

                #region 首長行程
                string sdate2 = sdate.ToString("yyyy/MM/dd") + " 00:00:00";
                string edate2 = sdate.ToString("yyyy/MM/dd") + " 23:59:59";
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
                            stime = Convert.ToDateTime(dt99.Rows[i]["c02_sdate"].ToString()).ToString("HH:mm");
                        else
                            stime = "00:00";
                        if (Convert.ToDateTime(dt99.Rows[i]["c02_edate"].ToString()).ToString("yyyy-MM-dd").Equals(sdate.ToString("yyyy-MM-dd")))
                            etime = Convert.ToDateTime(dt99.Rows[i]["c02_edate"].ToString()).ToString("HH:mm");
                        else
                            etime = "24:00";

                        ((Label)this.Master.FindControl("ContentPlaceHolder1").FindControl("lab_" + changeobj.ChangeWeek(sdate).ToString())).Text += "■" + stime + "~" + etime + " " + dt99.Rows[i]["c02_title"].ToString();

                    }
                }
                #endregion
                //抓取會議資料
                ((Label)this.Master.FindControl("ContentPlaceHolder1").FindControl("lab_" + changeobj.ChangeWeek(sdate).ToString())).Text += PCalendarUtil.GetMeeting(this.lab_people.Text, Convert.ToDateTime(sdate2), Convert.ToDateTime(edate2),"2"); //取得會議資料
                sdate = sdate.AddDays(1);
            }
            #endregion

            #region 連結位址更換
            //日、週、月、年、列表
            this.HyperLink1.NavigateUrl = "200102.aspx?today=" + this.lab_date.Text + "&" + parem;
            this.current.NavigateUrl = "200102-1.aspx?today=" + this.lab_date.Text + "&" + parem;
            this.HyperLink3.NavigateUrl = "200102-2.aspx?today=" + this.lab_date.Text + "&" + parem;
            //上一個月、下一個月(箭頭)
            this.hl_Pre.NavigateUrl = localurl + "?today=" + changeobj.ADDTtoROCDT(this.Calendar1.VisibleDate.AddMonths(-1).ToString("yyyy-MM-01")) + "&" + parem;
            this.hl_Nxt.NavigateUrl = localurl + "?today=" + changeobj.ADDTtoROCDT(this.Calendar1.VisibleDate.AddMonths(1).ToString("yyyy-MM-01")) + "&" + parem;
            #endregion
        }
        catch (Exception ex)
        {
            aMSG = "功能名稱:首長行程-日--顯示出首長行程(右邊版面)<br>錯誤訊息:" + ex.ToString();
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
            if (e.Day.IsOtherMonth)
            {
                #region 其他月份
                e.Cell.Text = "<a href=\"" + localurl + "?today=" + changeobj.ADDTtoROCDT(e.Day.Date.ToString("yyyy-MM-dd")) + "&peo_uid=" + this.lab_people.Text + "\" class=\"othermonth\">" + e.Day.Date.Day.ToString() + "</a>";
                #endregion
            }
            else
            {
                #region 本月
                e.Cell.Text = "<a href=\"" + localurl + "?today=" + changeobj.ADDTtoROCDT(e.Day.Date.ToString("yyyy-MM-dd")) + "&peo_uid=" + this.lab_people.Text + "\" class=\"month\">" + e.Day.Date.Day.ToString() + "</a>";
                #endregion
            }
        }
        catch (Exception ex)
        {
            aMSG = "功能名稱:個人首長行程/本月日曆初始化<br>錯誤訊息:" + ex.ToString();
            Response.Write(aMSG);
        }
    }
    #endregion

    #region 首長行程切換
    protected void btn_Change_Click(object sender, EventArgs e)
    {
        this.lab_people.Text = this.ddl_leading.SelectedValue;
        this.Response.Redirect(localurl + "?today=" + this.lab_date.Text + "&peo_uid=" + this.lab_people.Text);
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