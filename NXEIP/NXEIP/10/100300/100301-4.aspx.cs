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

public partial class _10_100300_100301_4 : System.Web.UI.Page
{
    ChangeObject changeobj = new ChangeObject();
    SessionObject sobj = new SessionObject();
    DBObject dbo = new DBObject();
    protected string localurl = "100301-4.aspx";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
            new OperatesObject().ExecuteOperates(100301, sobj.sessionUserID, 2, "點選個人行事曆-列表");

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
            this.lab_CYM.Text = Convert.ToString(this.Calendar1.VisibleDate.Year-1911) + "年" + this.Calendar1.VisibleDate.Month.ToString("0#") + "月";
            this.lab_today.Text = PCalendarUtil.GetToday(); //今天日期

            #endregion

            #region 左邊版面：可設定之他人行事曆、可查看之他人行事曆
            this.ddl_c01_CascadingDropDown.ContextKey = sobj.sessionUserID;
            string qrydepart = "0";
            if (Request["depart"] != null) qrydepart = Request["depart"];
            this.ddl_QryDepart_CascadingDropDown.ContextKey = sobj.sessionUserID + "," + qrydepart;
            #endregion

            #region 右邊版面：預設日期、人員編號
            this.lab_show.Text = Convert.ToString(Convert.ToDateTime(changeobj.ROCDTtoADDT(this.lab_date.Text)).Year-1911) + "年"
                + Convert.ToDateTime(changeobj.ROCDTtoADDT(this.lab_date.Text)).Month + "月";

            if (Request["peo_uid"] != null)
                this.lab_people.Text = Request["peo_uid"];
            else
                this.lab_people.Text = sobj.sessionUserID;//登入者(右)
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
            DataTable dt99 = new DataTable();
            string sqlstr99 = "";

            this.lab_name.Text = new PeopleDAO().GetPeopleNameByUid(Convert.ToInt32(this.lab_people.Text)); //姓名

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
            #endregion

            #region 基本表格
            this.Table1.Rows.Clear();
            this.Table1.Dispose();
            int rowcount = -1;
            string sdate = this.Calendar1.VisibleDate.ToString("yyyy/MM/01") + " 00:00:00";
            string edate = this.Calendar1.VisibleDate.AddMonths(1).AddDays(-1).ToString("yyyy/MM/dd") + " 23:59:59";
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
                        this.Table1.Rows.Add(new System.Web.UI.WebControls.TableRow());
                        this.Table1.Rows[rowcount].Cells.Add(new System.Web.UI.WebControls.TableCell());
                        this.Table1.Rows[rowcount].Cells.Add(new System.Web.UI.WebControls.TableCell());
                        this.Table1.Rows[rowcount].Cells[0].CssClass = "row_bg";
                        this.Table1.Rows[rowcount].Cells[1].CssClass = "row_bgc";

                        this.Table1.Rows[rowcount].Cells[0].Text = "<span class=\"row_time\">"+nowdate.ToString("MM-dd") + "<br />星期" +
                        changeobj.ChangeWeek(Convert.ToDateTime(dt99.Rows[i]["c02_sdate"].ToString()).DayOfWeek)+"</span>";
                    }
                    update = nowdate.ToString("MM-dd");
                    //if(this.Table1.Rows[rowcount].Cells[1].Text.Length>0) this.Table1.Rows[rowcount].Cells[1].Text+="<hr>";
                    string stime = nowdate.ToString("HH:mm");
                    string etime = Convert.ToDateTime(dt99.Rows[i]["c02_edate"].ToString()).ToString("HH:mm");

                    this.Table1.Rows[rowcount].Cells[1].Text += "<li class=\"p1\">" + Display(changeobj.ADDTtoROCDT(nowdate.ToString("yyyy-MM-dd")), stime + "~" + etime + " " + dt99.Rows[i]["c02_title"].ToString(), Convert.ToInt32(dt99.Rows[i]["c02_no"].ToString()), Convert.ToInt32(dt99.Rows[i]["c02_setuid"].ToString())) + "</li>";
                }
                this.Table1.Visible = true;
                this.hl_print.Visible = true;
            }
            else
            {
                this.Table1.Visible = false;
                this.lab_msg.Text = "目前無排定任何行事曆";
                this.hl_print.Visible = false;
            }
            #endregion

            #region 連結位址更換
            //日、週、月、年、列表
            this.HyperLink1.NavigateUrl = "100301.aspx?today=" + this.lab_date.Text + "&peo_uid=" + this.lab_people.Text + "&depart=" + this.ddl_QryDepart.SelectedValue;
            this.HyperLink2.NavigateUrl = "100301-1.aspx?today=" + this.lab_date.Text + "&peo_uid=" + this.lab_people.Text + "&depart=" + this.ddl_QryDepart.SelectedValue;
            this.HyperLink3.NavigateUrl = "100301-2.aspx?today=" + this.lab_date.Text + "&peo_uid=" + this.lab_people.Text + "&depart=" + this.ddl_QryDepart.SelectedValue;
            this.HyperLink4.NavigateUrl = "100301-3.aspx?today=" + this.lab_date.Text + "&peo_uid=" + this.lab_people.Text + "&depart=" + this.ddl_QryDepart.SelectedValue;
            this.current.NavigateUrl = "100301-4.aspx?today=" + this.lab_date.Text + "&peo_uid=" + this.lab_people.Text + "&depart=" + this.ddl_QryDepart.SelectedValue;
            //上一個月、下一個月(箭頭)
            this.hl_Pre.NavigateUrl = localurl + "?today=" + changeobj.ADDTtoROCDT(this.Calendar1.VisibleDate.AddMonths(-1).ToString("yyyy-MM-01")) + "&peo_uid=" + this.lab_people.Text + "&depart=" + this.ddl_QryDepart.SelectedValue;
            this.hl_Nxt.NavigateUrl = localurl + "?today=" + changeobj.ADDTtoROCDT(this.Calendar1.VisibleDate.AddMonths(1).ToString("yyyy-MM-01")) + "&peo_uid=" + this.lab_people.Text + "&depart=" + this.ddl_QryDepart.SelectedValue;
            //列印、返回
            string ax = "100";
            string ay = "100";
            string script = "newwindow=window.open('100301-p.aspx?today=" + this.lab_date.Text + "&peo_uid=" + this.lab_people.Text + "&printtype=lists','new_wealthy_calendar','height=580,width=700,toolbar=0,location=0,directories=0,status=0,menubar=1,scrollbars=1,resizable=1');newwindow.focus();newwindow.moveTo(" + ax + "," + ay + ")";
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

    #region show行程
    private string Display(string today, string txt, int no, int setuid)
    {
        string aMSG = "";
        string txt1 = "";
        try
        {
            if (no > 0)
            {
                if (this.lab_people.Text.Equals(sobj.sessionUserID) || setuid.ToString().Equals(sobj.sessionUserID))
                    txt1 = "<a href=\"100301-0.aspx?no=" + no.ToString() + "&peo_uid=" + this.lab_people.Text + "&today=" + today + "&depart=" + this.ddl_QryDepart.SelectedValue + "&source=lists&height=480&width=800&TB_iframe=true&modal=true\" class=\"thickbox row_schedule\">" + txt + "</a>" + "<br />";
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

    #region 日曆建立時
    protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
    {
        string aMSG = "";
        try
        {
            if (e.Day.IsOtherMonth)
            {
                #region 其他月份
                e.Cell.Text = "<a href=\"" + localurl + "?today=" + changeobj.ADDTtoROCDT(e.Day.Date.ToString("yyyy-MM-dd")) + "&peo_uid=" + this.lab_people.Text + "&depart=" + this.ddl_QryDepart.SelectedValue + "\" class=\"othermonth\">" + e.Day.Date.Day.ToString() + "</a>";
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