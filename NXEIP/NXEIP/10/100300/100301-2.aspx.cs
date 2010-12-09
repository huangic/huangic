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

public partial class _10_100300_100301_2 : System.Web.UI.Page
{
    ChangeObject changeobj = new ChangeObject();
    SessionObject sobj = new SessionObject();
    DBObject dbo = new DBObject();
    protected string localurl = "100301-2.aspx";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
            new OperatesObject().ExecuteOperates(100301, sobj.sessionUserID, 2, "點選個人行事曆-月");

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
            this.lab_CYM.Text = Convert.ToString(this.Calendar1.VisibleDate.Year-1911) + "年";
            this.lab_today.Text = PCalendarUtil.GetToday(); //今天日期
            #endregion

            #region 左邊版面：可設定之他人行事曆、可查看之他人行事曆
            this.ddl_c01_CascadingDropDown.ContextKey = sobj.sessionUserID;
            string qrydepart = "0";
            if (Request["depart"] != null) qrydepart = Request["depart"];
            this.ddl_QryDepart_CascadingDropDown.ContextKey = sobj.sessionUserID + "," + qrydepart;
            #endregion            

            #region 右邊版面：預設日期、人員編號
            this.lab_show.Text = Convert.ToString(Convert.ToDateTime(changeobj.ROCDTtoADDT(this.lab_date.Text)).Year-1911) + "年"+ Convert.ToDateTime(changeobj.ROCDTtoADDT(this.lab_date.Text)).Month + "月";
            if (Request["peo_uid"] != null)
                this.lab_people.Text = Request["peo_uid"];
            else
                this.lab_people.Text = sobj.sessionUserID;//登入者(右)
            #endregion

            #region 判斷是否可新增
            string isAdd = "0";
            if (this.lab_people.Text.Equals(sobj.sessionUserID))
            {
                isAdd = "1";
            }
            else
            {
                if (PCalendarUtil.IsAdd(sobj.sessionUserID, this.lab_people.Text)) isAdd = "1";
            }
            if (isAdd.Equals("1"))
                this.Panel1.Visible = true;
            else
                this.Panel1.Visible = false;
            this.lab_isAdd.Text = isAdd;
            #endregion

            if (this.lab_people.Text.Equals(sobj.sessionUserID))
            {
                this.hl_back.Visible = false;
            }
            else
            {
                this.hl_back.Text = "返回" + sobj.sessionUserName;
                this.hl_back.Visible = true;
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

                    this.Table1.Rows[i].Cells[j].Text = "<a href=\"" + localurl + "?peo_uid=" + this.lab_people.Text + "&today=" + year.ToString() + "-" + month1 + "-01" + "&depart=" + this.ddl_QryDepart.SelectedValue + "\" class=\"smonth\">" + month.ToString() + "月</a>";
                }
            }
            #endregion

            #region 連結位址更換
            //日、週、月、年、列表
            this.HyperLink1.NavigateUrl = "100301.aspx?today=" + this.lab_date.Text + "&peo_uid=" + this.lab_people.Text + "&depart=" + this.ddl_QryDepart.SelectedValue;
            this.HyperLink2.NavigateUrl = "100301-1.aspx?today=" + this.lab_date.Text + "&peo_uid=" + this.lab_people.Text + "&depart=" + this.ddl_QryDepart.SelectedValue;
            this.current.NavigateUrl = "100301-2.aspx?today=" + this.lab_date.Text + "&peo_uid=" + this.lab_people.Text + "&depart=" + this.ddl_QryDepart.SelectedValue;
            this.HyperLink4.NavigateUrl = "100301-3.aspx?today=" + this.lab_date.Text + "&peo_uid=" + this.lab_people.Text + "&depart=" + this.ddl_QryDepart.SelectedValue;
            this.HyperLink5.NavigateUrl = "100301-4.aspx?today=" + this.lab_date.Text + "&peo_uid=" + this.lab_people.Text + "&depart=" + this.ddl_QryDepart.SelectedValue;
            //上一個月、下一個月(箭頭)
            this.hl_Pre.NavigateUrl = localurl + "?today=" + changeobj.ADDTtoROCDT(this.Calendar1.VisibleDate.AddYears(-1).ToString("yyyy-MM-01")) + "&peo_uid=" + this.lab_people.Text + "&depart=" + this.ddl_QryDepart.SelectedValue;
            this.hl_Nxt.NavigateUrl = localurl + "?today=" + changeobj.ADDTtoROCDT(this.Calendar1.VisibleDate.AddYears(1).ToString("yyyy-MM-01")) + "&peo_uid=" + this.lab_people.Text + "&depart=" + this.ddl_QryDepart.SelectedValue;
            //列印、返回
            string ax = "100";
            string ay = "100";
            string script = "newwindow=window.open('100301-p.aspx?today=" + this.lab_date.Text + "&peo_uid=" + this.lab_people.Text + "&printtype=months','new_wealthy_calendar','height=580,width=700,toolbar=0,location=0,directories=0,status=0,menubar=1,scrollbars=1,resizable=1');newwindow.focus();newwindow.moveTo(" + ax + "," + ay + ")";
            this.hl_print.NavigateUrl = "#";
            this.hl_print.Attributes["onClick"] = script;
            this.hl_back.NavigateUrl = localurl + "?today=" + this.lab_date.Text + "&peo_uid=" + sobj.sessionUserID + "&depart=" + this.ddl_QryDepart.SelectedValue;
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

    #region 日曆建立時
    protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
    {
        string aMSG = "";
        try
        {
            if (e.Day.IsOtherMonth)
            {
                #region 其他月份
                if (this.lab_isAdd.Text.Equals("1"))
                    e.Cell.Text = "<a href=\"100301-0.aspx?today=" + changeobj.ADDTtoROCDT(e.Day.Date.ToString("yyyy-MM-dd")) + "&peo_uid=" + this.lab_people.Text + "&depart=" + this.ddl_QryDepart.SelectedValue + "&source=months&height=480&width=800&TB_iframe=true&modal=true\" class=\"thickbox othermonthT\"><font color=#E0E0E0>" + e.Day.Date.Day.ToString() + "</font></a>";
                else
                    e.Cell.Text = "<span class=\"othermonthT\">" + e.Day.Date.Day.ToString() + "</span>";
                #endregion
            }
            else
            {
                #region 本月
                string eDay = changeobj.ADDTtoROCDT(e.Day.Date.ToString("yyyy-MM-dd"));
                string celltxt = "";
                if (this.lab_isAdd.Text.Equals("1"))
                {
                    celltxt = "<a href=\"100301-0.aspx?today=" + eDay + "&peo_uid=" + this.lab_people.Text + "&depart=" + this.ddl_QryDepart.SelectedValue + "&source=months&height=480&width=800&TB_iframe=true&modal=true\" class=\"thickbox monthT\">" + e.Day.Date.Day.ToString() + "</a>";
                }
                else
                {
                    celltxt = "<span class=\"monthT\">" + e.Day.Date.Day.ToString() + "</span>";
                }

                #region 行事曆
                DataTable dt99 = new DataTable();
                string sqlstr99 = "";
                string sdate = e.Day.Date.ToString("yyyy/MM/dd") + " 00:00:00";
                string edate = e.Day.Date.ToString("yyyy/MM/dd") + " 23:59:59";
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

                        if (this.lab_isAdd.Text.Equals("1"))
                        {
                            if (this.lab_people.Text.Equals(sobj.sessionUserID) || dt99.Rows[i]["c02_setuid"].ToString().Equals(sobj.sessionUserID))
                                celltxt += "<br><a href=\"100301-0.aspx?no=" + dt99.Rows[i]["c02_no"].ToString() + "&peo_uid=" + this.lab_people.Text + "&today=" + eDay + "&depart=" + this.ddl_QryDepart.SelectedValue + "&source=months&height=480&width=800&TB_iframe=true&modal=true\" class=\"thickbox month\">■" + stime + "~" + etime + "<br>" + dt99.Rows[i]["c02_title"].ToString() + "</a>";
                            else
                                celltxt += "<br>■" + stime + "~" + etime + "<br>" + dt99.Rows[i]["c02_title"].ToString();
                        }
                        else
                        {
                            celltxt += "<br>■" + stime + "~" + etime + "<br>" + dt99.Rows[i]["c02_title"].ToString();
                        }
                    }
                }
                #endregion

                e.Cell.Text = celltxt;
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

            int newpk = new C02DAO().GetMaxNoByPeoUid(Convert.ToInt32(this.lab_people.Text)) + 1;

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

    #region 可查看之他人行事曆--搜尋
    protected void btn_QrySubmit_Click(object sender, EventArgs e)
    {
        if (this.ddl_QryPeople.SelectedValue.Equals("") || this.ddl_QryPeople.SelectedValue.Equals("0"))
        {
            ShowMSG("請選擇查看之人員");
        }
        else
        {
            this.lab_people.Text = this.ddl_QryPeople.SelectedValue;
            this.Response.Redirect(localurl + "?today=" + this.lab_date.Text + "&peo_uid=" + this.lab_people.Text + "&depart=" + this.ddl_QryDepart.SelectedValue);
        }
    }
    #endregion

    #region 可設定之他人行事曆--搜尋
    protected void btn_SetSubmit0_Click(object sender, EventArgs e)
    {
        if (this.ddl_c01.SelectedValue.Equals("") || this.ddl_c01.SelectedValue.Equals("0"))
        {
            ShowMSG("請選擇設定之人員");
        }
        else
        {
            this.lab_people.Text = this.ddl_c01.SelectedValue;
            this.Response.Redirect(localurl + "?today=" + this.lab_date.Text + "&peo_uid=" + this.lab_people.Text + "&depart=" + this.ddl_QryDepart.SelectedValue);
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