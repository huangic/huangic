using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

public partial class _10_100300_100301 : System.Web.UI.Page
{
    ChangeObject changeobj = new ChangeObject();
    SessionObject sobj = new SessionObject();
    DBObject dbo = new DBObject();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
            new OperatesObject().ExecuteOperates(100301, sobj.sessionUserID, 2, "點選個人行事曆");

            ListItem newitem = new ListItem("請選擇", "0");
            DataTable dt = new DataTable();

            #region 左邊版面：月曆、今天日期
            //左上，月曆
            if (Request["today"] != null)
                this.Calendar1.VisibleDate = Convert.ToDateTime(changeobj.ROCDTtoADDT(Request["today"]));
            else
                this.Calendar1.VisibleDate = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd"));//月曆初始值(左)

            this.Calendar1.TodaysDate = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd"));//月曆初始值(左)

            this.lab_CYM.Text = this.Calendar1.VisibleDate.Year.ToString() + "年" + this.Calendar1.VisibleDate.Month.ToString("0#") + "月";
            this.lab_Pre.Text = "<a href=\"?todays=" + this.Calendar1.VisibleDate.AddMonths(-1).ToString("yyyy-MM-01") + "\"><div class=\"h1\"></div></a>";
            this.lab_Nxt.Text = "<a href=\"?todays=" + this.Calendar1.VisibleDate.AddMonths(1).ToString("yyyy-MM-01") + "\"><div class=\"h3\"></div></a>";
            //今天日期
            this.lab_today.Text = PCalendarUtil.GetToday();          

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
                this.Panel1.Visible = true;
            }
            else
            {
                this.Panel1.Visible = false;
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
                            this.ddl_QryPeople.Items.Insert(0, newitem);
                        }
                    }
                }
                catch { }
            }
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
            if (dt99.Rows.Count > 0)
            {
                this.lab_name.Text = dt99.Rows[0]["peo_name"].ToString();
            }
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

            #region 基本表格
            int a = 6;
            this.Table1.Rows.Clear();
            this.Table1.Dispose();
            #region 表頭
            this.Table1.Rows.Add(new System.Web.UI.WebControls.TableRow());
            this.Table1.Rows[0].Cells.Add(new System.Web.UI.WebControls.TableCell());
            this.Table1.Rows[0].Cells.Add(new System.Web.UI.WebControls.TableCell());

            this.Table1.Rows[0].Cells[0].Text = "時間";
            this.Table1.Rows[0].Cells[1].Text = "行程";
            this.Table1.Rows[0].Cells[0].CssClass="row_time";
            this.Table1.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            this.Table1.Rows[0].Cells[1].HorizontalAlign = HorizontalAlign.Center;
            #endregion

            #region 表身(清空)
            for (int i = 0; i < 16; i++)
            {
                this.Table1.Rows.Add(new System.Web.UI.WebControls.TableRow());
                this.Table1.Rows[i + 1].BackColor = Color.White;

                this.Table1.Rows[i + 1].Cells.Add(new System.Web.UI.WebControls.TableCell());
                this.Table1.Rows[i + 1].Cells.Add(new System.Web.UI.WebControls.TableCell());

                this.Table1.Rows[i + 1].Cells[0].CssClass = "row_time";
                this.Table1.Rows[i + 1].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                if (isAdd.Equals("1"))
                {
                    if ((a + i) < 10)
                        this.Table1.Rows[i + 1].Cells[0].Text = "<a href=\"100301-0.aspx?today=" + this.lab_date.Text + "&peo_uid=" + this.lab_people.Text + "&depart=" + this.ddl_QryDepart.SelectedValue + "&stime=0" + (a + i) + ":00&source=1\" class=\"timecss1\">0" + (a + i) + ":00</a>";
                    else
                        this.Table1.Rows[i + 1].Cells[0].Text = "<a href=\"100301-1.aspx?today=" + this.lab_date.Text + "&peo_uid=" + this.lab_people.Text + "&depart=" + this.ddl_QryDepart.SelectedValue + "&stime=" + (a + i) + ":00&source=1\" class=\"timecss1\">" + (a + i) + ":00</a>";
                }
                else
                {
                    if ((a + i) < 10)
                        this.Table1.Rows[i + 1].Cells[0].Text = "0" + (a + i) + ":00";
                    else
                        this.Table1.Rows[i + 1].Cells[0].Text = (a + i) + ":00";
                }
                this.Table1.Rows[i + 1].Cells[1].Text = "";
                this.Table1.Rows[i + 1].Cells[1].RowSpan = 0;
                this.Table1.Rows[i + 1].Cells[1].Visible = true;
                this.Table1.Rows[i + 1].Cells[1].BackColor = Color.White;
            }
            #endregion
            #endregion

            #region 行事曆
            string sdate = changeobj.ROCDTtoADDT(this.lab_date.Text) + " 00:00:00";
            string edate = changeobj.ROCDTtoADDT(this.lab_date.Text) + " 23:59:59";
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
                    stime = Convert.ToDateTime(dt99.Rows[i]["c02_sdate"].ToString()).ToString("HH:mm");
                    if (Convert.ToDateTime(dt99.Rows[i]["c02_sdate"].ToString()).ToString("yyyy-MM-dd").Equals(Convert.ToDateTime(dt99.Rows[i]["c02_edate"].ToString()).ToString("yyyy-MM-dd")))
                    {
                        etime = Convert.ToDateTime(dt99.Rows[i]["c02_edate"].ToString()).ToString("HH:mm");
                    }
                    else
                    {
                        etime = "23:00";
                    }
                    //Display(stime, etime, "■" + stime + "~" + etime + " " + this.dataSet_days1.c02[i].c02_title, this.dataSet_days1.c02[i].c02_bgcolor, this.dataSet_days1.c02[i].c02_no, this.dataSet_days1.c02[i].c02_setuid);
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
                if (e.Day.IsWeekend) e.Cell.CssClass = "othermonthholiday"; //假日

                //e.Cell.Text = "<a href=\"cal0101.aspx?today=" + lib.WealthyBR.ADDTtoROCDT(e.Day.Date.ToString("yyyy-MM-dd")) + "&peo_uid=" + this.lab_peo_uid.Text + "&depart=" + this.ddl_depart.SelectedValue + "\"class=\"no_\">" + e.Day.Date.Day.ToString() + "</a>";
                #endregion
            }
            else
            {
                #region 本月
                //e.Cell.Text = "<a href=\"cal0101.aspx?today=" + lib.WealthyBR.ADDTtoROCDT(e.Day.Date.ToString("yyyy-MM-dd")) + "&peo_uid=" + this.lab_peo_uid.Text + "&depart=" + this.ddl_depart.SelectedValue + "\"class=\"no_\"><font color=#E0E0E0>" + e.Day.Date.Day.ToString() + "</font></a>";
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
}