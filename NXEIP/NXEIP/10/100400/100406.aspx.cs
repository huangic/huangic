using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data;
using NXEIP.DAO;
using Entity;


/// <summary>
/// 功能名稱：個人應用 / 線上申請 / 派車申請
/// 功能編號：10/100400/100406
/// 撰寫者：Lina
/// 撰寫時間：2011/03/11
/// </summary>
public partial class _10_100400_100406 : System.Web.UI.Page
{
    ChangeObject changeobj = new ChangeObject();
    DBObject dbo = new DBObject();
    SessionObject sobj = new SessionObject();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
            new OperatesObject().ExecuteOperates(100406, sobj.sessionUserID, 2, "開始[派車申請]初始頁");
            Session["100406_CommandArgument"] = "";
            this.lab_chekuan.Text = "0";
            this.lab_car.Text = "0";

            if (Session["100406_value"] != null && Session["100406_value"].ToString().Length > 0 && Request["count"] != null)
            {
                string[] val = Session["100406_value"].ToString().Split(','); //0:m01,1:m02,2:today
                this.lab_chekuan.Text = val[0];
                this.lab_car.Text = val[1];
                this.lab_today.Text = val[2];//日期：民國年月日
            }
            if (Request["count"] == null) Session["100406_value"] = "";

            if (this.lab_today.Text.Trim().Length <= 0) this.lab_today.Text = System.DateTime.Now.ToString("yyyy-MM-dd");//日期：民國年月日
            #region 車輛種類、車輛 預設限制單位
            this.ddl_chekuan_CascadingDropDown.ContextKey = this.lab_chekuan.Text;
            this.ddl_car_CascadingDropDown.ContextKey = this.lab_car.Text;
            #endregion

            #region 取得「派車申請核可方式」:1表不需審核，直接核可(不可重覆);2表需審核，(不可重覆)
            if (dbo.GetArguments("Car_BorrowsSignType") != null)
                this.lab_BorrowsSignType.Text = dbo.GetArguments("Car_BorrowsSignType");
            else
                this.lab_BorrowsSignType.Text = "1";
            #endregion
        }
        ShowData(); //顯示車輛資料
        ShowBorrows();
    }

    #region 牌照號碼
    protected void ddl_car_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddl_car.Items.Count > 0)
        {
            if (!this.ddl_car.SelectedValue.Equals("0") && !this.ddl_car.SelectedValue.Equals(""))
            {
                this.lab_chekuan.Text = this.ddl_chekuan.SelectedValue;
                this.lab_car.Text = this.ddl_car.SelectedValue;

            }
            else
            {
                this.ddl_chekuan.Text = "0";
                this.ddl_car.Text = "0";
            }
        }
        ShowData();
        ShowBorrows();
    }
    #endregion

    #region 顯示車輛資料
    private void ShowData()
    {
        this.lab_cc.Text = "";
        this.lab_code.Text = "";
        this.lab_color.Text = "";
        this.lab_peouid.Text = "";
        this.lab_mark.Text = "";
        this.lab_memo.Text = "";
        this.lab_stime.Text = "";
        this.lab_etime.Text = "";

        if (this.lab_car.Text.Length > 0 && !this.lab_car.Text.Equals("0"))
        {
            string sqlstr = "select m02_no, m02_code, m02_peouid, m02_chekuan, m02_color, m02_mark, m02_cc, m02_memo from m02 where (m02_no = " + this.lab_car.Text + ")";
            DataTable dt = new DataTable();
            dt = dbo.ExecuteQuery(sqlstr);
            if (dt.Rows.Count > 0)
            {
                this.lab_peouid.Text = new PeopleDAO().GetPeopleNameByUid(Convert.ToInt32(dt.Rows[0]["m02_peouid"].ToString()));
                this.lab_cc.Text = dt.Rows[0]["m02_cc"].ToString();
                this.lab_code.Text = dt.Rows[0]["m02_code"].ToString();
                this.lab_memo.Text = dt.Rows[0]["m02_memo"].ToString();
                this.lab_color.Text = new M01DAO().GetNameByNo(Convert.ToInt32(dt.Rows[0]["m02_color"].ToString()))+" 色";
                this.lab_mark.Text = new M01DAO().GetNameByNo(Convert.ToInt32(dt.Rows[0]["m02_mark"].ToString()));
            }
        }
    }
    #endregion

    #region 顯示申請班表
    private void ShowBorrows()
    {
        string aMSG = "";
        try
        {
            this.Table1.Rows.Clear();
            this.Table1.Dispose();
            #region 當未選擇車輛時，預設為 06:00~22:00
            if (this.lab_stime.Text.Trim().Length <= 0) this.lab_stime.Text = "06:00";
            if (this.lab_etime.Text.Trim().Length <= 0) this.lab_etime.Text = "22:00";
            #endregion

            DateTime stime = new DateTime();
            DateTime etime = new DateTime();
            stime = Convert.ToDateTime(this.lab_today.Text + " " + this.lab_stime.Text);
            etime = Convert.ToDateTime(this.lab_today.Text + " " + this.lab_etime.Text);

            DateTime sdate = new DateTime();
            DateTime edate = new DateTime();
            sdate = Convert.ToDateTime(this.lab_today.Text);
            int weeks=changeobj.ChangeWeek(sdate);
            if(weeks==0) weeks=7;
            sdate = sdate.AddDays(-(int)weeks).AddDays(1);
            edate = sdate.AddDays(7);
            int Rows = -1;

            #region 表頭
            Rows++;//第0行
            this.Table1.Rows.Add(new System.Web.UI.WebControls.TableRow());
            for (int i = 0; i < 8; i++)
            {
                this.Table1.Rows[Rows].Cells.Add(new System.Web.UI.WebControls.TableCell());
                this.Table1.Rows[Rows].Cells[i].HorizontalAlign = HorizontalAlign.Center;
                if (i == 0)
                {
                    this.Table1.Rows[Rows].Cells[i].Width = Unit.Percentage(9);
                    this.Table1.Rows[Rows].Cells[i].Text = "時間";
                    this.Table1.Rows[Rows].Cells[i].RowSpan = 2;
                    this.Table1.Rows[Rows].Cells[i].ColumnSpan = 0;
                    this.Table1.Rows[Rows].Cells[i].CssClass = "headtitle";

                }
                else
                {
                    this.Table1.Rows[Rows].Cells[i].Width = Unit.Percentage(13);
                    this.Table1.Rows[Rows].Cells[i].Text = changeobj.ChangeWeek(sdate.AddDays(i - 1).DayOfWeek);
                    this.Table1.Rows[Rows].Cells[i].RowSpan = 0;
                    this.Table1.Rows[Rows].Cells[i].ColumnSpan = 0;
                    this.Table1.Rows[Rows].Cells[i].CssClass = "headtitle";
                }
            }

            Rows++; //第1行
            this.Table1.Rows.Add(new System.Web.UI.WebControls.TableRow());
            for (int i = 0; i < 7; i++)
            {
                this.Table1.Rows[Rows].Cells.Add(new System.Web.UI.WebControls.TableCell());
                this.Table1.Rows[Rows].Cells[i].HorizontalAlign = HorizontalAlign.Center;
                this.Table1.Rows[Rows].Cells[i].Text = sdate.AddDays(i).ToString("MM-dd");
                this.Table1.Rows[Rows].Cells[i].RowSpan = 0;
                this.Table1.Rows[Rows].Cells[i].ColumnSpan = 0;
                this.Table1.Rows[Rows].Cells[i].CssClass = "headtitle";
            }
            #endregion

            #region 表身
            while (stime < etime)
            {
                Rows++;
                this.Table1.Rows.Add(new System.Web.UI.WebControls.TableRow());

                string strtime = stime.ToString("HH:00");

                for (int j = 0; j < 8; j++)
                {
                    this.Table1.Rows[Rows].Cells.Add(new System.Web.UI.WebControls.TableCell());
                    this.Table1.Rows[Rows].VerticalAlign = VerticalAlign.Top;
                    if (j == 0)
                    {
                        #region 第0行
                        this.Table1.Rows[Rows].Cells[j].Text = strtime;
                        this.Table1.Rows[Rows].Cells[j].CssClass = "rowtime_bg row";
                        this.Table1.Rows[Rows].Cells[j].RowSpan = 0;
                        this.Table1.Rows[Rows].Cells[j].ColumnSpan = 0;
                        #endregion
                    }
                    else
                    {
                        this.Table1.Rows[Rows].Cells[j].Text = "0";
                        #region 非第0行
                        if (this.lab_chekuan.Text.Length > 0 && !this.lab_car.Text.Equals("0"))
                        {
                            string txt_date = Convert.ToDateTime(this.lab_today.Text).Year + "-" + this.Table1.Rows[1].Cells[j - 1].Text;
                            string sqlstr = "SELECT m03.m03_no, departments.dep_name, m03.m03_peouid,m03.m03_verify FROM m03 INNER JOIN departments ON m03.m03_depno = departments.dep_no"
                            + " WHERE (m03.m03_verify in ('1','2')) AND (m03.m03_m02no = " + this.lab_car.Text + ") "
                            + " and (m03.m03_sdate<='" + txt_date + " " + this.Table1.Rows[Rows].Cells[0].Text + "') AND ('" + txt_date + " " + this.Table1.Rows[Rows].Cells[0].Text + "'<m03.m03_edate)";
                            DataTable dt = new DataTable();
                            dt = dbo.ExecuteQuery(sqlstr);
                            if (dt.Rows.Count > 0)
                            {
                                string celltxt = "";
                                int icount = 0;
                                for (int k = 0; k < dt.Rows.Count; k++)
                                {
                                    if (celltxt.Length > 0) celltxt += ",";
                                    if (dt.Rows[k]["m03_verify"].ToString().Equals("2")) icount++;
                                    celltxt += dt.Rows[k]["m03_no"].ToString();
                                }
                                this.Table1.Rows[Rows].Cells[j].Text = celltxt;
                            }
                        }
                        #region 假日、非假日的css
                        if (sdate.AddDays(j - 1).DayOfWeek == DayOfWeek.Saturday || sdate.AddDays(j - 1).DayOfWeek == DayOfWeek.Sunday)
                            this.Table1.Rows[Rows].Cells[j].CssClass = "holiday_bg";
                        else
                            this.Table1.Rows[Rows].Cells[j].CssClass = "Nholiday_bg";
                        #endregion
                        this.Table1.Rows[Rows].Cells[j].RowSpan = 0;
                        this.Table1.Rows[Rows].Cells[j].ColumnSpan = 0;
                        #endregion
                    }
                }
                stime = stime.AddHours(1);
            }
            #endregion

            #region 表格合併
            for (int x = 1; x < 8; x++)
            {
                int z, n;
                for (int y = 2; y < this.Table1.Rows.Count; y++)
                {
                    n = 1;
                    for (z = y + 1; z < this.Table1.Rows.Count; z++)
                    {
                        if (this.Table1.Rows[y].Cells[x].Text.Trim().Equals(this.Table1.Rows[z].Cells[x].Text.Trim()) && !this.Table1.Rows[y].Cells[x].Text.Trim().Equals("0"))
                        {
                            n += 1;
                            #region 合併表格
                            this.Table1.Rows[y].Cells[x].RowSpan = n;
                            this.Table1.Rows[z].Cells[x].Visible = false;
                            #endregion
                        }
                        else break;
                    }
                    y = z - 1;
                }
            }
            #endregion

            #region 顯示表格內容
            for (int a = 2; a < this.Table1.Rows.Count; a++)
            {
                for (int b = 1; b < 8; b++)
                {
                    if (this.Table1.Rows[a].Cells[b].Visible)
                    {
                        string txtdate = Convert.ToDateTime(this.lab_today.Text).Year + "-" + this.Table1.Rows[1].Cells[b - 1].Text;//西元年月日
                        string txttime = this.Table1.Rows[a].Cells[0].Text;
                        string no = this.Table1.Rows[a].Cells[b].Text;

                        LinkButton lbtn_apply = new LinkButton();
                        lbtn_apply.Text = "申請";
                        lbtn_apply.ID = "lbtn_" + txtdate.Replace("-", "") + txttime.Replace(":", "");
                        lbtn_apply.CommandArgument = this.lab_chekuan.Text + "," + this.lab_car.Text + "," + txtdate + "," + txttime;
                        lbtn_apply.CssClass = "row";
                        lbtn_apply.Click += new EventHandler(lbtnfunction);

                        string sqlstr = "SELECT m03.m03_no,departments.dep_name, m03.m03_peouid,m03.m03_verify FROM m03 INNER JOIN departments ON m03.m03_depno = departments.dep_no"
                            + " WHERE m03.m03_no in (" + no + ") order by m03.m03_no";
                        DataTable dt = new DataTable();
                        dt = dbo.ExecuteQuery(sqlstr);
                        if (dt.Rows.Count > 0)
                        {
                            this.Table1.Rows[a].Cells[b].Controls.Clear();
                            this.Table1.Rows[a].Cells[b].CssClass = "Nholiday_bg2";
                            //Rooms_PetitionSignType 1表不需審核，直接核可，但不可重覆;2表需審核，但不可重覆;3表需審核，但可重覆
                            string celltxt = "";
                            int icount = 0;
                            for (int c = 0; c < dt.Rows.Count; c++)
                            {
                                if (celltxt.Length > 0) celltxt += "<br />";
                                string status = "";
                                if (dt.Rows[c]["m03_verify"].ToString().Equals("1"))
                                    status = "(送審中)";
                                else
                                    icount++;

                                #region 放值
                                Control ctlNewTrial = this.Page.LoadControl("../../lib/people/PeopleDetail.ascx");
                                SetUserControlProperty(ctlNewTrial, "ID", "Usrctrl_" + txtdate.Replace("-", "") + txttime.Replace(":", "") + dt.Rows[c]["m03_no"].ToString());
                                SetUserControlProperty(ctlNewTrial, "peo_uid", dt.Rows[c]["m03_peouid"].ToString());
                                this.Table1.Rows[a].Cells[b].Controls.Add(ctlNewTrial);

                                LinkButton lbtn_applied = new LinkButton();
                                lbtn_applied.Text = new PeopleDAO().GetPeopleNameByUid(Convert.ToInt32(dt.Rows[c]["m03_peouid"].ToString())) + status;
                                lbtn_applied.ID = "lbtn_d" + txtdate.Replace("-", "") + txttime.Replace(":", "") + dt.Rows[c]["m03_no"].ToString();
                                lbtn_applied.CommandArgument = this.lab_chekuan.Text + "," + this.lab_car.Text + "," + txtdate + "," + txttime + "," + dt.Rows[c]["m03_no"].ToString();
                                lbtn_applied.CssClass = "row";
                                lbtn_applied.Click += new EventHandler(lbtnfunction);
                                this.Table1.Rows[a].Cells[b].Controls.Add(lbtn_applied);
                                #endregion
                            }
                        }
                        else
                        {
                            #region 沒人申請過的
                            if (Convert.ToDateTime(txtdate + " " + txttime) >= Convert.ToDateTime(System.DateTime.Now.ToString("yyyy/MM/dd HH:00")) && !this.lab_car.Text.Equals("0"))
                            {
                                this.Table1.Rows[a].Cells[b].Controls.Clear();
                                this.Table1.Rows[a].Cells[b].Controls.Add(lbtn_apply);
                            }
                            else
                            {
                                this.Table1.Rows[a].Cells[b].Text = "";
                                this.Table1.Rows[a].Cells[b].Controls.Clear();
                            }
                            #endregion
                        }
                    }
                }
            }
            #endregion
        }
        catch (Exception ex)
        {
            aMSG = "功能名稱：" + this.Navigator1.SubFunc + "<br>錯誤訊息:" + ex.ToString();
            Response.Write(aMSG);
        }
    }
    #endregion

    #region 人員名稱點選物件
    public void SetUserControlProperty(Control vobjControl, string vstrPropertyName, object vobjValue)
    {
        vobjControl.GetType().GetProperty(vstrPropertyName).SetValue(vobjControl, vobjValue, null);
    }
    #endregion

    #region 上個月
    protected void lbtn_PMonth_Click(object sender, EventArgs e)
    {
        this.lab_today.Text = Convert.ToDateTime(this.lab_today.Text).AddMonths(-1).ToString("yyyy-MM-dd");
        ShowData();
        ShowBorrows();
    }
    #endregion

    #region 上星期
    protected void lbtn_PWeeks_Click(object sender, EventArgs e)
    {
        this.lab_today.Text = Convert.ToDateTime(this.lab_today.Text).AddDays(-7).ToString("yyyy-MM-dd");
        ShowData();
        ShowBorrows();
    }
    #endregion

    #region 下星期
    protected void lbtn_NWeeks_Click(object sender, EventArgs e)
    {
        this.lab_today.Text = Convert.ToDateTime(this.lab_today.Text).AddDays(7).ToString("yyyy-MM-dd");
        ShowData();
        ShowBorrows();
    }
    #endregion

    #region 下個月
    protected void lbtn_NMonth_Click(object sender, EventArgs e)
    {
        this.lab_today.Text = Convert.ToDateTime(this.lab_today.Text).AddMonths(1).ToString("yyyy-MM-dd");
        ShowData();
        ShowBorrows();
    }
    #endregion

    #region 點選
    protected void lbtnfunction(object sender, EventArgs e)
    {
        Session["100406_1_CommandArgument"] = ((LinkButton)sender).CommandArgument;
        this.Response.Redirect("100406-1.aspx");
    }
    #endregion
}