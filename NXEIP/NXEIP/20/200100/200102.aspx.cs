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

public partial class _20_200100_200102 : System.Web.UI.Page
{
    ChangeObject changeobj = new ChangeObject();
    SessionObject sobj = new SessionObject();
    DBObject dbo = new DBObject();
    protected string localurl = "200102.aspx";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
            new OperatesObject().ExecuteOperates(200102, sobj.sessionUserID, 2, "點選首長行程-月");

            ListItem newitem = new ListItem("請選擇", "0");
            DataTable dt = new DataTable();

            #region 左邊版面：月曆、今天日期
            //左上，月曆
            if (Request["today"] != null)
                this.lab_date.Text = Request["today"];
            else
                this.lab_date.Text = changeobj.ADDTtoROCDT(System.DateTime.Now.ToString("yyyy-MM-dd"));

            this.Calendar1.VisibleDate = Convert.ToDateTime(changeobj.ROCDTtoADDT(this.lab_date.Text));
            this.Calendar1.TodaysDate = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd"));//月曆初始值(右)
            this.lab_CYM.Text = Convert.ToString(this.Calendar1.VisibleDate.Year - 1911) + "年";
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
            DateTime today = Convert.ToDateTime(changeobj.ROCDTtoADDT(this.lab_date.Text));
            this.lab_name.Text = new PeopleDAO().GetPeopleNameByUid(Convert.ToInt32(this.lab_people.Text)); //姓名

            #region 左邊月份選單
            int month = 0;
            int year = today.Year - 1911;
            string month1 = "";
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    month++;
                    if (today.Month == month)
                        this.Table1.Rows[i].Cells[j].CssClass = "today";
                    else
                        this.Table1.Rows[i].Cells[j].CssClass = "smonth_bg";
                    if (month < 10)
                        month1 = "0" + month.ToString();
                    else
                        month1 = month.ToString();

                    this.Table1.Rows[i].Cells[j].Text = "<a href=\"" + localurl + "?peo_uid=" + this.lab_people.Text + "&today=" + year.ToString() + "-" + month1 + "-01" + "\" class=\"smonth\">" + month.ToString() + "月</a>";
                }
            }
            #endregion

            #region 連結位址更換
            //月、週、日
            this.current.NavigateUrl = "200102.aspx?today=" + this.lab_date.Text + "&peo_uid=" + this.lab_people.Text;
            this.HyperLink2.NavigateUrl = "200102-1.aspx?today=" + this.lab_date.Text + "&peo_uid=" + this.lab_people.Text;
            this.HyperLink3.NavigateUrl = "200102-2.aspx?today=" + this.lab_date.Text + "&peo_uid=" + this.lab_people.Text;
            this.HyperLink4.NavigateUrl = "200102-3.aspx?today=" + this.lab_date.Text + "&peo_uid=" + this.lab_people.Text;
            //上一個月、下一個月(箭頭)
            this.hl_Pre.NavigateUrl = localurl + "?today=" + changeobj.ADDTtoROCDT(this.Calendar1.VisibleDate.AddYears(-1).ToString("yyyy-MM-01")) + "&peo_uid=" + this.lab_people.Text;
            this.hl_Nxt.NavigateUrl = localurl + "?today=" + changeobj.ADDTtoROCDT(this.Calendar1.VisibleDate.AddYears(1).ToString("yyyy-MM-01")) + "&peo_uid=" + this.lab_people.Text;
            #endregion
        }
        catch (Exception ex)
        {
            aMSG = "功能名稱:首長行程-月--顯示(右邊版面)<br>錯誤訊息:" + ex.ToString();
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
                e.Cell.Text = "<a href=\"200102-0.aspx?today=" + changeobj.ADDTtoROCDT(e.Day.Date.ToString("yyyy-MM-dd")) + "&peo_uid=" + this.lab_people.Text  + "&source=months&height=480&width=800&TB_iframe=true&modal=true\" class=\"thickbox othermonthT\"><font color=#E0E0E0>" + e.Day.Date.Day.ToString() + "</font></a>";
                #endregion
            }
            else
            {
                #region 本月
                string eDay = changeobj.ADDTtoROCDT(e.Day.Date.ToString("yyyy-MM-dd"));
                string celltxt = "";
                celltxt = "<a href=\"200102-0.aspx?today=" + eDay + "&peo_uid=" + this.lab_people.Text + "&source=months&height=480&width=800&TB_iframe=true&modal=true\" class=\"thickbox monthT\">" + e.Day.Date.Day.ToString() + "</a>";

                #region 行事曆
                DataTable dt99 = new DataTable();
                string sqlstr99 = "";
                string sdate = e.Day.Date.ToString("yyyy/MM/dd") + " 00:00:00";
                string edate = e.Day.Date.ToString("yyyy/MM/dd") + " 23:59:59";
                sqlstr99 = "SELECT peo_uid, c02_no, c02_sdate, c02_edate, c02_title, c02_bgcolor, c02_setuid,c02_appointmen,c02_check FROM c02 "
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
                            stime = Convert.ToDateTime(dt99.Rows[i]["c02_sdate"].ToString()).ToString("HH:mm");
                        else
                            stime = "06:00";
                        if (Convert.ToDateTime(dt99.Rows[i]["c02_edate"].ToString()).ToString("yyyy-MM-dd").Equals(Convert.ToDateTime(sdate).ToString("yyyy-MM-dd")))
                            etime = Convert.ToDateTime(dt99.Rows[i]["c02_edate"].ToString()).ToString("HH:mm");
                        else
                            etime = "23:00";

                        if (dt99.Rows[i]["c02_appointmen"].ToString().Equals("1"))
                        {
                            if (dt99.Rows[i]["c02_check"].ToString().Equals("0"))
                            {
                                #region 未審核
                                if (dt99.Rows[i]["c02_setuid"].ToString().Equals(sobj.sessionUserID))
                                    celltxt += "<br><a href=\"200102-0.aspx?no=" + dt99.Rows[i]["c02_no"].ToString() + "&peo_uid=" + this.lab_people.Text + "&today=" + eDay + "&source=months&height=480&width=800&TB_iframe=true&modal=true\" class=\"thickbox month\">■" + stime + "~" + etime + "<br>" + dt99.Rows[i]["c02_title"].ToString() + "(審核中)</a>";
                                else
                                    celltxt += "<br>■" + stime + "~" + etime + "<br>" + dt99.Rows[i]["c02_title"].ToString() + "(審核中)";
                                #endregion
                            }
                            else if (dt99.Rows[i]["c02_check"].ToString().Equals("1"))
                                celltxt += "<br>■" + stime + "~" + etime + "<br>" + dt99.Rows[i]["c02_title"].ToString();
                        }else
                            celltxt += "<br>■" + stime + "~" + etime + "<br>" + dt99.Rows[i]["c02_title"].ToString(); 
                    }
                }
                #endregion

                celltxt += PCalendarUtil.GetMeeting(this.lab_people.Text, Convert.ToDateTime(e.Day.Date.ToString("yyyy/MM/dd 00:00:00")), Convert.ToDateTime(e.Day.Date.ToString("yyyy/MM/dd 23:59:59")),"1"); //取得會議資料

                e.Cell.Text = celltxt;
                #endregion
            }
        }
        catch (Exception ex)
        {
            aMSG = "功能名稱:首長行程/本月日曆初始化<br>錯誤訊息:" + ex.ToString();
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