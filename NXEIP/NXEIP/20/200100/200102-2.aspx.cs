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

public partial class _20_200100_200102_2 : System.Web.UI.Page
{
    ChangeObject changeobj = new ChangeObject();
    SessionObject sobj = new SessionObject();
    DBObject dbo = new DBObject();
    protected string localurl = "200102-2.aspx";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
            new OperatesObject().ExecuteOperates(200102, sobj.sessionUserID, 2, "點選首長行程-日");

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

            #region 右邊版面：預設日期、首長人事編號、判斷是否可新增
            this.lab_show.Text = changeobj.ROCtoChi(this.lab_date.Text);
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
        }
        Show();
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

            #region 基本表格
            int a = 6;
            this.Table1.Rows.Clear();
            this.Table1.Dispose();
            #region 表頭
            this.Table1.Rows.Add(new System.Web.UI.WebControls.TableRow());
            this.Table1.Rows[0].Cells.Add(new System.Web.UI.WebControls.TableCell());
            this.Table1.Rows[0].Cells.Add(new System.Web.UI.WebControls.TableCell());

            this.Table1.Rows[0].Cells[0].Text = "<span class=\"title\">時間</span>";
            this.Table1.Rows[0].Cells[0].CssClass = "title_time_bg";
            this.Table1.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;

            this.Table1.Rows[0].Cells[1].Text = "<span class=\"title\">行程</span>";
            this.Table1.Rows[0].Cells[1].CssClass = "title_schedule_bg";
            this.Table1.Rows[0].Cells[1].HorizontalAlign = HorizontalAlign.Center;
            #endregion

            #region 表身(清空)
            for (int i = 0; i < 18; i++)
            {
                this.Table1.Rows.Add(new System.Web.UI.WebControls.TableRow());
                this.Table1.Rows[i + 1].BackColor = Color.White;

                this.Table1.Rows[i + 1].Cells.Add(new System.Web.UI.WebControls.TableCell());
                this.Table1.Rows[i + 1].Cells.Add(new System.Web.UI.WebControls.TableCell());

                this.Table1.Rows[i + 1].Cells[0].CssClass = "row_bg";
                this.Table1.Rows[i + 1].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                
                if ((a + i) < 10)
                    this.Table1.Rows[i + 1].Cells[0].Text = "<a href=\"200102-0.aspx?today=" + this.lab_date.Text + "&peo_uid=" + this.lab_people.Text + "&stime=0" + (a + i) + ":00&source=days&height=480&width=800&TB_iframe=true&modal=true\" class=\"thickbox row_time\">0" + (a + i) + ":00</a>";
                else
                    this.Table1.Rows[i + 1].Cells[0].Text = "<a href=\"200102-0.aspx?today=" + this.lab_date.Text + "&peo_uid=" + this.lab_people.Text + "&stime=" + (a + i) + ":00&source=days&height=480&width=800&TB_iframe=true&modal=true\" class=\"thickbox row_time\">" + (a + i) + ":00</a>";
                
                this.Table1.Rows[i + 1].Cells[1].Text = "";
                this.Table1.Rows[i + 1].Cells[1].RowSpan = 0;
                this.Table1.Rows[i + 1].Cells[1].Visible = true;
                this.Table1.Rows[i + 1].Cells[1].BackColor = Color.White;
                this.Table1.Rows[i + 1].Cells[1].CssClass = "row_bgc";
            }
            #endregion
            #endregion

            string sdate = changeobj.ROCDTtoADDT(this.lab_date.Text) + " 00:00:00";
            string edate = changeobj.ROCDTtoADDT(this.lab_date.Text) + " 23:59:59";
            #region 行事曆
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
                    Display(stime, etime, "■" + stime + "~" + etime + " " + dt99.Rows[i]["c02_title"].ToString(), dt99.Rows[i]["c02_bgcolor"].ToString(), Convert.ToInt32(dt99.Rows[i]["c02_no"].ToString()), Convert.ToInt32(dt99.Rows[i]["c02_setuid"].ToString()), "1");
                }
            }
            #endregion

            #region 會議資料
            dt99.Clear();
            sqlstr99 = "select meetings.mee_no, meetings.mee_reason, meetings.mee_sdate, meetings.mee_edate from meetings inner join attends on meetings.mee_no = attends.mee_no "
            + " WHERE (meetings.mee_status = '1') and (attends.att_status = '2') and (attends.peo_uid = " + this.lab_people.Text + ") and (meetings.mee_sdate <= '" + edate + "') AND (meetings.mee_edate <= '" + edate + "') AND (meetings.mee_edate >= '" + sdate + "') "
            + " OR (meetings.mee_status = '1') and (attends.att_status = '2') and (attends.peo_uid = " + this.lab_people.Text + ") and (meetings.mee_sdate >= '" + sdate + "') AND (meetings.mee_edate >= '" + sdate + "') AND (meetings.mee_sdate <= '" + edate + "') "
            + " OR (meetings.mee_status = '1') and (attends.att_status = '2') and (attends.peo_uid = " + this.lab_people.Text + ") and (meetings.mee_sdate < '" + sdate + "') AND (meetings.mee_edate > '" + edate + "') ORDER BY meetings.mee_sdate, meetings.mee_edate, meetings.mee_no";
            //Response.Write(sqlstr99);
            dt99 = dbo.ExecuteQuery(sqlstr99);
            if (dt99.Rows.Count > 0)
            {
                for (int i = 0; i < dt99.Rows.Count; i++)
                {
                    string stime = "";
                    string etime = "";
                    if (Convert.ToDateTime(dt99.Rows[i]["mee_sdate"].ToString()).ToString("yyyy-MM-dd").Equals(Convert.ToDateTime(sdate).ToString("yyyy-MM-dd")))
                    {
                        stime = Convert.ToDateTime(dt99.Rows[i]["mee_sdate"].ToString()).ToString("HH:mm");
                    }
                    else
                    {
                        stime = "06:00";
                    }
                    if (Convert.ToDateTime(dt99.Rows[i]["mee_edate"].ToString()).ToString("yyyy-MM-dd").Equals(Convert.ToDateTime(sdate).ToString("yyyy-MM-dd")))
                    {
                        etime = Convert.ToDateTime(dt99.Rows[i]["mee_edate"].ToString()).ToString("HH:mm");
                    }
                    else
                    {
                        etime = "23:00";
                    }
                    Display(stime, etime, "■" + stime + "~" + etime + " " + dt99.Rows[i]["mee_reason"].ToString(), "#FFFFFF", Convert.ToInt32(dt99.Rows[i]["mee_no"].ToString()), Convert.ToInt32(this.lab_people.Text), "2");
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

            #region 連結位址更換
            //日、週、月、年、列表
            this.HyperLink1.NavigateUrl = "200102.aspx?today=" + this.lab_date.Text + "&peo_uid=" + this.lab_people.Text;
            this.HyperLink2.NavigateUrl = "200102-1.aspx?today=" + this.lab_date.Text + "&peo_uid=" + this.lab_people.Text;
            this.current.NavigateUrl = "200102-2.aspx?today=" + this.lab_date.Text + "&peo_uid=" + this.lab_people.Text;
            //上一個月、下一個月(箭頭)
            this.hl_Pre.NavigateUrl = localurl + "?today=" + changeobj.ADDTtoROCDT(this.Calendar1.VisibleDate.AddMonths(-1).ToString("yyyy-MM-01")) + "&peo_uid=" + this.lab_people.Text;
            this.hl_Nxt.NavigateUrl = localurl + "?today=" + changeobj.ADDTtoROCDT(this.Calendar1.VisibleDate.AddMonths(1).ToString("yyyy-MM-01")) + "&peo_uid=" + this.lab_people.Text;
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
    private void Display(string stime, string etime, string txt2, string bgcolors, int no, int setuid,string showtype)
    {
        string aMSG = "";
        try
        {
            DateTime sdate1 = Convert.ToDateTime(changeobj.ROCDTtoADDT(this.lab_date.Text) + " " + stime + ":00");
            DateTime edate1 = Convert.ToDateTime(changeobj.ROCDTtoADDT(this.lab_date.Text) + " " + etime + ":00");
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
                            //this.Table1.Rows[i-6+1].Cells[1].Text=txt;
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
                            if (showtype.Equals("3"))
                            {
                                if (setuid.ToString().Equals(sobj.sessionUserID)) //要調
                                {
                                    this.Table1.Rows[i - 6 + 1].Cells[cel].Text = "<a href=\"200102-0.aspx?no=" + no.ToString() + "&peo_uid=" + this.lab_people.Text + "&today=" + this.lab_date.Text + "&source=days&height=480&width=800&TB_iframe=true&modal=true\" class=\"thickbox row_schedule\">" + txt2 + "</a>";
                                }
                                else
                                {
                                    this.Table1.Rows[i - 6 + 1].Cells[cel].Text = txt2;
                                }
                            }
                            else
                            {
                                this.Table1.Rows[i - 6 + 1].Cells[cel].Text = txt2;    
                            }

                            this.Table1.Rows[i - 6 + 1].Cells[cel].BackColor = Color.FromName(bgcolors);
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < 19; i++)
                {
                    this.Table1.Rows[i].Cells.Add(new System.Web.UI.WebControls.TableCell());
                }
                cel = this.Table1.Rows[0].Cells.Count - 1;

                for (int i = begin; i <= end; i++)
                {
                    if (i >= 6 && i <= 23)
                    {
                        if (this.Table1.Rows[i - 6 + 1].Cells[cel].Text.Equals(""))
                        {
                            if (showtype.Equals("3"))
                            {
                                if (setuid.ToString().Equals(sobj.sessionUserID))
                                {
                                    this.Table1.Rows[i - 6 + 1].Cells[cel].Text = "<a href=\"200102-0.aspx?no=" + no.ToString() + "&peo_uid=" + this.lab_people.Text + "&today=" + this.lab_date.Text + "&source=days&height=480&width=800&TB_iframe=true&modal=true\" class=\"thickbox row_schedule\">" + txt2 + "</a>";
                                }
                                else
                                {
                                    this.Table1.Rows[i - 6 + 1].Cells[cel].Text = txt2;
                                }
                            }
                            else
                            {
                                this.Table1.Rows[i - 6 + 1].Cells[cel].Text = txt2;
                            }
                            this.Table1.Rows[i - 6 + 1].Cells[cel].BackColor = Color.FromName(bgcolors);
                        }
                    }
                }
            }
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
            if (e.Day.IsOtherMonth)
            {
                // 其他月份
                e.Cell.Text = "<a href=\"" + localurl + "?today=" + changeobj.ADDTtoROCDT(e.Day.Date.ToString("yyyy-MM-dd")) + "&peo_uid=" + this.lab_people.Text + "\" class=\"othermonth\">" + e.Day.Date.Day.ToString() + "</a>";
            }
            else
            {
                //本月
                e.Cell.Text = "<a href=\"" + localurl + "?today=" + changeobj.ADDTtoROCDT(e.Day.Date.ToString("yyyy-MM-dd")) + "&peo_uid=" + this.lab_people.Text + "\" class=\"month\">" + e.Day.Date.Day.ToString() + "</a>";
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