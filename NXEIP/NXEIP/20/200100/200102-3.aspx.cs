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

public partial class _20_200100_200102_3 : System.Web.UI.Page
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
            new OperatesObject().ExecuteOperates(200102, sobj.sessionUserID, 2, "點選首長行程-預約記錄");

            ListItem newitem = new ListItem("請選擇", "0");
            DataTable dt = new DataTable();

            #region 左邊版面：月曆、今天日期
            //左上，月曆
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

            #region 左邊版面：首長人事編號
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
            parem += "peo_uid=" + this.lab_people.Text;
            #endregion

            #region 左邊版面：連結位址更換
            //日、週、月、年、列表
            this.HyperLink1.NavigateUrl = "200102.aspx?today=" + this.lab_date.Text + "&" + parem;
            this.HyperLink2.NavigateUrl = "200102-1.aspx?today=" + this.lab_date.Text + "&" + parem;
            this.HyperLink3.NavigateUrl = "200102-2.aspx?today=" + this.lab_date.Text + "&" + parem;
            this.HyperLink4.NavigateUrl = "200102-3.aspx?today=" + this.lab_date.Text + "&" + parem;
            //上一個月、下一個月(箭頭)
            this.hl_Pre.NavigateUrl = localurl + "?today=" + changeobj.ADDTtoROCDT(this.Calendar1.VisibleDate.AddMonths(-1).ToString("yyyy-MM-01")) + "&" + parem;
            this.hl_Nxt.NavigateUrl = localurl + "?today=" + changeobj.ADDTtoROCDT(this.Calendar1.VisibleDate.AddMonths(1).ToString("yyyy-MM-01")) + "&" + parem;
            #endregion

            #region 右邊 查詢列
            this.calendar3._ADDate = Convert.ToDateTime(System.DateTime.Today.ToString("yyyy-01-01"));
            this.calendar4._ADDate = Convert.ToDateTime(System.DateTime.Today.ToString("yyyy-12-31"));
            #endregion

            #region 列表
            this.ObjectDataSource1.SelectParameters["sdate"].DefaultValue = this.calendar3._ADDate.ToString("yyyy/MM/dd");
            this.ObjectDataSource1.SelectParameters["edate"].DefaultValue = this.calendar4._ADDate.ToString("yyyy/MM/dd");
            this.ObjectDataSource1.SelectParameters["status"].DefaultValue = this.rbl_check.SelectedValue;
            this.ObjectDataSource1.SelectParameters["loginuser"].DefaultValue = sobj.sessionUserID;
            this.GridView1.DataBind();
            #endregion
        }
    }

    #region 調整輸出格式
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[3].Text.Equals("0"))
            {
                e.Row.Cells[3].Text = "預約中";
            }
            else if (e.Row.Cells[3].Text.Equals("1"))
                e.Row.Cells[3].Text = "核可";
            else if (e.Row.Cells[3].Text.Equals("2"))
                e.Row.Cells[3].Text = "退回";
        }
    }
    #endregion

    #region 檢查輸入值
    private bool CheckInputValue()
    {
        bool feedback = true;

        #region 日期區間
        if (this.calendar3._ADDate == null || this.calendar4._ADDate == null)
        {
            ShowMSG("請選擇開始時間與結束時間");
            feedback = false;
        }
        if (this.calendar3._ADDate > this.calendar4._ADDate)
        {
            ShowMSG("結束時間不得小於開始時間");
            feedback = false;
        }
        #endregion

        return feedback;
    }
    #endregion

    #region 查詢頁面確定
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        string aMSG = "";
        try
        {
            if (CheckInputValue())
            {
                #region 列表
                this.ObjectDataSource1.SelectParameters["sdate"].DefaultValue = this.calendar3._ADDate.ToString("yyyy/MM/dd");
                this.ObjectDataSource1.SelectParameters["edate"].DefaultValue = this.calendar4._ADDate.ToString("yyyy/MM/dd");
                this.ObjectDataSource1.SelectParameters["status"].DefaultValue = this.rbl_check.SelectedValue;
                this.ObjectDataSource1.SelectParameters["loginuser"].DefaultValue = sobj.sessionUserID;
                this.GridView1.DataBind();
                #endregion
            }
        }
        catch (Exception ex)
        {
            aMSG = "功能名稱：" + this.Navigator1.SubFunc + "<br>錯誤訊息:" + ex.ToString();
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
                // 其他月份
                e.Cell.Text = "<a href=\"200102-1.aspx?today=" + changeobj.ADDTtoROCDT(e.Day.Date.ToString("yyyy-MM-dd")) + "&peo_uid=" + this.lab_people.Text + "\" class=\"othermonth\">" + e.Day.Date.Day.ToString() + "</a>";
            }
            else
            {
                //本月
                e.Cell.Text = "<a href=\"200102-1.aspx?today=" + changeobj.ADDTtoROCDT(e.Day.Date.ToString("yyyy-MM-dd")) + "&peo_uid=" + this.lab_people.Text + "\" class=\"month\">" + e.Day.Date.Day.ToString() + "</a>";
            }
        }
        catch (Exception ex)
        {
            aMSG = "功能名稱:本月日曆初始化<br>錯誤訊息:" + ex.ToString();
            Response.Write(aMSG);
        }
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