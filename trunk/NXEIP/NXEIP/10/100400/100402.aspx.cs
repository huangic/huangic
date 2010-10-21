using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

/// <summary>
/// 功能名稱：個人應用 / 線上申請 / 場地申請
/// 功能編號：10/100400/100402
/// 撰寫者：Lina
/// 撰寫時間：2010/10/08
/// </summary>
public partial class _10_100400_100402 : System.Web.UI.Page
{
    ChangeObject changeobj = new ChangeObject();
    DBObject dbo = new DBObject();
    SessionObject sobj = new SessionObject();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
            new OperatesObject().ExecuteOperates(100402, sobj.sessionUserID, 2, "開始[場地申請]初始頁");

            this.lab_today.Text = System.DateTime.Now.ToString("yyyy/MM/dd");

            #region 所在地、場所 預設限制單位
            this.ddl_spot_CascadingDropDown.ContextKey = sobj.sessionUserDepartID;
            this.ddl_rooms_CascadingDropDown.ContextKey = sobj.sessionUserDepartID;
            #endregion

            #region 取得「申請核可方式」
            if (dbo.GetArguments("Rooms_PetitionSignType") != null)
                this.lab_PetitionSignType.Text = dbo.GetArguments("Rooms_PetitionSignType");
            else
                this.lab_PetitionSignType.Text = "1";
            #endregion

            ShowRoomsData(); //顯示場地資料
            ShowPetition();
        }
    }

    #region 顯示場地資料
    private void ShowRoomsData()
    {
        this.lab_spot.Text = "";
        this.lab_floor.Text = "";
        this.lab_oneuid.Text = "";
        this.lab_ext.Text = "";
        this.lab_human.Text = "";
        this.lab_describe.Text = "";

        if (this.ddl_rooms.Items.Count > 0)
        {
            if (!this.ddl_rooms.SelectedValue.Equals("0") && !this.ddl_rooms.SelectedValue.Equals(""))
            {
                this.lab_spot.Text = this.ddl_spot.SelectedItem.Text;
                string sqlstr = "SELECT rooms.roo_no, rooms.roo_name, rooms.roo_oneuid, rooms.roo_ext, rooms.roo_human, rooms.roo_floor, rooms.roo_describe, "
                    + " rooms.roo_stime, rooms.roo_etime, people.peo_name FROM rooms INNER JOIN people ON rooms.roo_oneuid = people.peo_uid WHERE (rooms.roo_no = " + this.ddl_rooms.SelectedValue + ")";
                DataTable dt = new DataTable();
                dt = dbo.ExecuteQuery(sqlstr);
                if (dt.Rows.Count > 0)
                {
                    this.lab_floor.Text = dt.Rows[0]["roo_floor"].ToString() + " 樓";
                    this.lab_oneuid.Text = dt.Rows[0]["peo_name"].ToString();
                    this.lab_ext.Text = dt.Rows[0]["roo_ext"].ToString();
                    this.lab_human.Text = dt.Rows[0]["roo_human"].ToString() + " 人";
                    this.lab_describe.Text = dt.Rows[0]["roo_describe"].ToString();
                    this.lab_stime.Text = dt.Rows[0]["roo_stime"].ToString();
                    this.lab_etime.Text = dt.Rows[0]["roo_etime"].ToString();
                }
            }
        }
    }
    #endregion

    #region 場所改變時
    protected void ddl_rooms_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowRoomsData();
        ShowPetition();
    }
    #endregion

    #region 顯示申請班表
    private void ShowPetition()
    {
        string aMSG = "";
        try
        {
            this.Table1.Rows.Clear();
            this.Table1.Dispose();

            DateTime sdate = new DateTime();
            DateTime edate = new DateTime();
            sdate = Convert.ToDateTime(this.lab_today.Text);
            sdate = sdate.AddDays(-(int)changeobj.ChangeWeek(sdate)).AddDays(1);
            edate = sdate.AddDays(7);
            int Rows = 0;

            #region 表頭
            //第0行
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
                    this.Table1.Rows[Rows].Cells[i].CssClass = "title";
                    
                }
                else
                {
                    this.Table1.Rows[Rows].Cells[i].Width = Unit.Percentage(13);
                    this.Table1.Rows[Rows].Cells[i].Text = changeobj.ChangeWeek(sdate.AddDays(i - 1).DayOfWeek);
                    this.Table1.Rows[Rows].Cells[i].RowSpan = 0;
                    this.Table1.Rows[Rows].Cells[i].ColumnSpan = 0;
                    this.Table1.Rows[Rows].Cells[i].CssClass = "title";
                }
            }

            Rows++; //第1行
            this.Table1.Rows.Add(new System.Web.UI.WebControls.TableRow());
            for (int i = 0; i < 7; i++)
            {
                this.Table1.Rows[Rows].Cells.Add(new System.Web.UI.WebControls.TableCell());
                this.Table1.Rows[Rows].Cells[i].HorizontalAlign = HorizontalAlign.Center;
                this.Table1.Rows[Rows].Cells[i].Text = sdate.AddDays(i).ToString("MM/dd");
                this.Table1.Rows[Rows].Cells[i].RowSpan = 0;
                this.Table1.Rows[Rows].Cells[i].ColumnSpan = 0;
                this.Table1.Rows[Rows].Cells[i].CssClass = "title";
            }
            #endregion

            #region 表身
            for (int i = 0; i < 18; i++)
            {
                Rows++;
                this.Table1.Rows.Add(new System.Web.UI.WebControls.TableRow());

                string strtime = Convert.ToString(6 + i);
                if (strtime.Length < 2) strtime = "0" + strtime;
                strtime += ":00";

                for (int j = 0; j < 8; j++)
                {
                    this.Table1.Rows[Rows].Cells.Add(new System.Web.UI.WebControls.TableCell());
                    this.Table1.Rows[Rows].VerticalAlign = VerticalAlign.Top;
                    if (j == 0)
                    {
                        #region 第0行
                        this.Table1.Rows[Rows].Cells[j].Text = strtime;
                        this.Table1.Rows[Rows].Cells[j].CssClass = "time";
                        this.Table1.Rows[Rows].Cells[j].RowSpan = 0;
                        this.Table1.Rows[Rows].Cells[j].ColumnSpan = 0;
                        #endregion
                    }
                    else
                    {
                        string roo_no = "0";
                        if (!this.ddl_rooms.SelectedValue.Equals("0") && !this.ddl_rooms.SelectedValue.Equals(""))
                        {
                            roo_no = this.ddl_rooms.SelectedValue;
                        }
                        #region 非第0行
                        string sqlstr = "SELECT petition.pet_no, departments.dep_name, petition.pet_name,petition.pet_applyuid,petition.pet_apply FROM petition INNER JOIN departments ON petition.pet_depno = departments.dep_no"
                            + " WHERE (petition.pet_apply in ('1','2')) AND (petition.roo_no = " + roo_no + ") "
                            + " and (petition.pet_applydate = '" + sdate.AddDays(j - 1).ToString("yyyy/MM/dd") + "') AND (petition.pet_stime<='" + this.Table1.Rows[Rows].Cells[0].Text + "') AND ('" + this.Table1.Rows[Rows].Cells[0].Text + "'<petition.pet_etime)";
                        DataTable dt = new DataTable();
                        dt = dbo.ExecuteQuery(sqlstr);
                        if (dt.Rows.Count > 0)
                        {
                            string celltxt = "";
                            int icount = 0;
                            for (int k = 0; k < dt.Rows.Count; k++)
                            {
                                if (celltxt.Length > 0) celltxt += ",";
                                if (dt.Rows[k]["pet_apply"].ToString().Equals("2")) icount++;
                                celltxt += dt.Rows[k]["pet_no"].ToString();
                            }
                            this.Table1.Rows[Rows].Cells[j].Text = celltxt;
                        }
                        else
                        {
                            this.Table1.Rows[Rows].Cells[j].Text = "0";
                        }
                        if (sdate.AddDays(j - 1).DayOfWeek == DayOfWeek.Saturday || sdate.AddDays(j - 1).DayOfWeek == DayOfWeek.Sunday)
                            this.Table1.Rows[Rows].Cells[j].CssClass = "holiday";
                        else
                            this.Table1.Rows[Rows].Cells[j].CssClass = "";

                        this.Table1.Rows[Rows].Cells[j].RowSpan = 0;
                        this.Table1.Rows[Rows].Cells[j].ColumnSpan = 0;
                        #endregion
                    }
                }
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
            string spot = "", rooms = "";
            if (this.ddl_spot.Items.Count > 0) spot = this.ddl_spot.SelectedValue;
            if (this.ddl_rooms.Items.Count > 0) rooms = this.ddl_rooms.SelectedValue;

            for (int a = 2; a < this.Table1.Rows.Count; a++)
            {
                for (int b = 1; b < 8; b++)
                {
                    if (this.Table1.Rows[a].Cells[b].Visible)
                    {
                        string txtdate = Convert.ToDateTime(this.lab_today.Text).Year + "-" + this.Table1.Rows[1].Cells[b - 1].Text.Replace("/", "-");
                        string txttime = this.Table1.Rows[a].Cells[0].Text;
                        string no = this.Table1.Rows[a].Cells[b].Text;

                        string sqlstr = "SELECT petition.pet_no,departments.dep_name, petition.pet_name,petition.pet_applyuid,petition.pet_apply FROM petition INNER JOIN departments ON petition.pet_depno = departments.dep_no"
                            + " WHERE petition.pet_no in (" + no + ") order by petition.pet_no";
                        DataTable dt = new DataTable();
                        dt = dbo.ExecuteQuery(sqlstr);
                        if (dt.Rows.Count > 0)
                        {
                            string celltxt = "";
                            int icount = 0;
                            for (int c = 0; c < dt.Rows.Count; c++)
                            {
                                if (celltxt.Length > 0) celltxt += "<br />";
                                string status = "";
                                if (dt.Rows[c]["pet_apply"].ToString().Equals("1"))
                                    status = "(送審中)";
                                else
                                    icount++;

                                #region 判斷是不是該申請人的
                                if (dt.Rows[c]["pet_applyuid"].ToString().Equals(sobj.sessionUserID))
                                {
                                    if (Convert.ToDateTime(txtdate) >= Convert.ToDateTime(System.DateTime.Now.ToString("yyyy/MM/dd")))
                                    {
                                        celltxt += "<a href=\"100402-1.aspx?spot=" + spot + "&rooms=" + rooms + "&sdate=" + changeobj.ADDTtoROCDT(txtdate) + "&stime=" + txttime + "&no=" + dt.Rows[c]["pet_no"].ToString() + "\">" + dt.Rows[c]["dep_name"].ToString() + "：" + dt.Rows[c]["pet_name"].ToString() + status + "</a>";
                                    }
                                    else
                                    {
                                        celltxt += dt.Rows[c]["dep_name"].ToString() + "：" + dt.Rows[c]["pet_name"].ToString() + status;
                                    }
                                }
                                else
                                {
                                    celltxt += dt.Rows[c]["dep_name"].ToString() + "：" + dt.Rows[c]["pet_name"].ToString() + status;
                                }
                                #endregion
                            }
                            #region 可不可以同時申請(未審核的狀態下)
                            if (this.lab_PetitionSignType.Text.Equals("3") && icount == 0 && this.ddl_rooms.Items.Count > 0)
                            {
                                if (Convert.ToDateTime(txtdate) >= Convert.ToDateTime(System.DateTime.Now.ToString("yyyy/MM/dd")) && rooms.Length>0)
                                {
                                    celltxt += "<br />" + "<a href=\"100402-1.aspx?spot=" + spot + "&rooms=" + rooms + "&sdate=" + changeobj.ADDTtoROCDT(txtdate) + "&stime=" + txttime + "\">申請</a>";
                                }
                            }
                            #endregion
                            this.Table1.Rows[a].Cells[b].Text = celltxt;
                        }
                        else
                        {
                            #region 沒人申請過的
                            if (Convert.ToDateTime(txtdate) >= Convert.ToDateTime(System.DateTime.Now.ToString("yyyy/MM/dd")) && rooms.Length > 0)
                            {
                                this.Table1.Rows[a].Cells[b].Text = "<a href=\"100402-1.aspx?spot=" + spot + "&rooms=" + rooms + "&sdate=" + changeobj.ADDTtoROCDT(txtdate) + "&stime=" + txttime + "\">申請</a>";
                            }
                            else
                            {
                                this.Table1.Rows[a].Cells[b].Text = "";
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

    #region 上個月
    protected void lbtn_premonth_Click(object sender, EventArgs e)
    {
        this.lab_today.Text = Convert.ToDateTime(this.lab_today.Text).AddMonths(-1).ToString("yyyy/MM/dd");
    }
    #endregion

    #region 上星期
    protected void lbtn_preweek_Click(object sender, EventArgs e)
    {
        this.lab_today.Text = Convert.ToDateTime(this.lab_today.Text).AddDays(-7).ToString("yyyy/MM/dd");
    }
    #endregion

    #region 下星期
    protected void lbtn_nextweek_Click(object sender, EventArgs e)
    {
        this.lab_today.Text = Convert.ToDateTime(this.lab_today.Text).AddDays(7).ToString("yyyy/MM/dd");
    }
    #endregion

    #region 下個月
    protected void lbtn_nextmonth_Click(object sender, EventArgs e)
    {
        this.lab_today.Text = Convert.ToDateTime(this.lab_today.Text).AddMonths(1).ToString("yyyy/MM/dd");
    }
    #endregion
}