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
    //protected string localurl = "100402.aspx";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
            new OperatesObject().ExecuteOperates(100402, sobj.sessionUserID, 2, "開始[場地申請]初始頁");
            Session["100402_CommandArgument"] = "";
            this.lab_spot1.Text = "0";
            this.lab_rooms1.Text = "0";

            if (Session["100402_value"] != null && Session["100402_value"].ToString().Length>0 && Request["count"]!=null)
            {
                string[] val = Session["100402_value"].ToString().Split(','); //0:spot,1:rooms,2:today
                this.lab_spot1.Text = val[0];
                this.lab_rooms1.Text = val[1];
                this.lab_today.Text = val[2];//日期：民國年月日
                //Session["100402_value"] = "";
            }
            if (Request["count"] == null) Session["100402_value"] = "";

            if (this.lab_today.Text.Trim().Length <= 0) this.lab_today.Text = System.DateTime.Now.ToString("yyyy-MM-dd");//日期：民國年月日

            #region 所在地、場所 預設限制單位
            this.ddl_spot_CascadingDropDown.ContextKey = sobj.sessionUserDepartID + "," + this.lab_spot1.Text;
            this.ddl_rooms_CascadingDropDown.ContextKey = sobj.sessionUserDepartID + "," + this.lab_rooms1.Text;
            #endregion

            #region 取得「申請核可方式」：1表不需審核，直接核可，但不可重覆;2表需審核，但不可重覆;3表需審核，但可重覆
            if (dbo.GetArguments("Rooms_PetitionSignType") != null)
                this.lab_PetitionSignType.Text = dbo.GetArguments("Rooms_PetitionSignType");
            else
                this.lab_PetitionSignType.Text = "1";
            #endregion
        }
        ShowRoomsData(); //顯示場地資料
        ShowPetition();
    }

    #region 場所改變時
    protected void ddl_rooms_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddl_rooms.Items.Count > 0)
        {
            if (!this.ddl_rooms.SelectedValue.Equals("0") && !this.ddl_rooms.SelectedValue.Equals(""))
            {
                this.lab_spot1.Text = this.ddl_spot.SelectedValue;
                this.lab_rooms1.Text = this.ddl_rooms.SelectedValue;

            }
            else
            {
                this.lab_spot1.Text = "0";
                this.lab_rooms1.Text = "0";
            }
        }
        ShowRoomsData();
        ShowPetition();
    }
    #endregion

    #region 顯示場地資料
    private void ShowRoomsData()
    {
        this.lab_spot.Text = "";
        this.lab_floor.Text = "";
        this.lab_oneuid.Text = "";
        this.lab_ext.Text = "";
        this.lab_human.Text = "";
        this.lab_describe.Text = "";
        this.lab_telephone.Text = "";
        this.lab_stime.Text = "";
        this.lab_etime.Text = "";

        if (this.lab_rooms1.Text.Length > 0 && !this.lab_rooms1.Text.Equals("0"))
        {
            string sqlstr = "SELECT rooms.roo_no,rooms.spo_no, rooms.roo_name, rooms.roo_oneuid, rooms.roo_ext, rooms.roo_human, rooms.roo_floor, rooms.roo_describe, "
                + " rooms.roo_stime, rooms.roo_etime, people.peo_name,rooms.roo_telephone FROM rooms INNER JOIN people ON rooms.roo_oneuid = people.peo_uid WHERE (rooms.roo_no = " + this.lab_rooms1.Text + ")";
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
                this.lab_telephone.Text = dt.Rows[0]["roo_telephone"].ToString();

                this.lab_spot.Text = new SpotDAO().GetNameBySpoNo(Convert.ToInt32(dt.Rows[0]["spo_no"].ToString()));
            }
        }
        
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
            #region 當未選擇場地時，預設為 08:00~18:00
            if (this.lab_stime.Text.Trim().Length <= 0) this.lab_stime.Text = "08:00";
            if (this.lab_etime.Text.Trim().Length <= 0) this.lab_etime.Text = "18:00";
            #endregion

            DateTime stime = new DateTime();
            DateTime etime = new DateTime();
            stime = Convert.ToDateTime(this.lab_today.Text + " " + this.lab_stime.Text);
            etime = Convert.ToDateTime(this.lab_today.Text + " " + this.lab_etime.Text);

            DateTime sdate = new DateTime();
            DateTime edate = new DateTime();
            sdate = Convert.ToDateTime(this.lab_today.Text);
            int weeks = changeobj.ChangeWeek(sdate);
            if (weeks == 0) weeks = 7;
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
            while(stime<etime)
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
                        if (this.lab_rooms1.Text.Length > 0 && !this.lab_rooms1.Text.Equals("0"))
                        {
                            string txt_date=Convert.ToDateTime(this.lab_today.Text).Year + "-" + this.Table1.Rows[1].Cells[j - 1].Text;
                            string sqlstr = "SELECT petition.pet_no, departments.dep_name, petition.pet_name,petition.pet_applyuid,petition.pet_apply FROM petition INNER JOIN departments ON petition.pet_depno = departments.dep_no"
                            + " WHERE (petition.pet_apply in ('1','2')) AND (petition.roo_no = " + this.lab_rooms1.Text + ") "
                            + " and (petition.pet_stime<='" + txt_date + " " + this.Table1.Rows[Rows].Cells[0].Text + "') AND ('" + txt_date + " " + this.Table1.Rows[Rows].Cells[0].Text + "'<petition.pet_etime)";
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
                stime=stime.AddHours(1);
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
                        lbtn_apply.CommandArgument = this.lab_spot1.Text + "," + this.lab_rooms1.Text + "," + txtdate + "," + txttime;
                        lbtn_apply.CssClass = "row";
                        lbtn_apply.Click+=new EventHandler(lbtnfunction);

                        string sqlstr = "SELECT petition.pet_no,departments.dep_name, petition.pet_name,petition.pet_applyuid,petition.pet_apply FROM petition INNER JOIN departments ON petition.pet_depno = departments.dep_no"
                            + " WHERE petition.pet_no in (" + no + ") order by petition.pet_no";
                        DataTable dt = new DataTable();
                        dt = dbo.ExecuteQuery(sqlstr);
                        if (dt.Rows.Count > 0)
                        {
                            this.Table1.Rows[a].Cells[b].Controls.Clear();
                            //Rooms_PetitionSignType 1表不需審核，直接核可，但不可重覆;2表需審核，但不可重覆;3表需審核，但可重覆
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

                                #region 放值
                                Control ctlNewTrial = this.Page.LoadControl("../../lib/people/PeopleDetail.ascx");
                                SetUserControlProperty(ctlNewTrial, "ID", "Usrctrl_" + txtdate.Replace("-", "") + txttime.Replace(":", "") + dt.Rows[c]["pet_no"].ToString());
                                SetUserControlProperty(ctlNewTrial, "peo_uid", dt.Rows[c]["pet_applyuid"].ToString());
                                this.Table1.Rows[a].Cells[b].Controls.Add(ctlNewTrial);

                                LinkButton lbtn_applied = new LinkButton();
                                lbtn_applied.Text = dt.Rows[c]["pet_name"].ToString() + status;
                                lbtn_applied.ID = "lbtn_d" + txtdate.Replace("-", "") + txttime.Replace(":", "") + dt.Rows[c]["pet_no"].ToString();
                                lbtn_applied.CommandArgument = this.lab_spot1.Text + "," + this.lab_rooms1.Text + "," + txtdate  +"," + txttime + "," + dt.Rows[c]["pet_no"].ToString();
                                lbtn_applied.CssClass = "row";
                                lbtn_applied.Click += new EventHandler(lbtnfunction);
                                this.Table1.Rows[a].Cells[b].Controls.Add(lbtn_applied);
                                #endregion
                            }
                            #region 可不可以同時申請(未審核的狀態下)
                            if (this.lab_PetitionSignType.Text.Equals("3") && icount == 0 && !this.lab_rooms1.Text.Equals("0"))
                            {
                                if (Convert.ToDateTime(txtdate + " " + txttime) >= Convert.ToDateTime(System.DateTime.Now.ToString("yyyy/MM/dd HH:00")) && !this.lab_rooms1.Text.Equals("0"))
                                {
                                    //this.Table1.Rows[a].Cells[b].Controls.Add(lbtn_apply);
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            #region 沒人申請過的
                            if (Convert.ToDateTime(txtdate + " " + txttime) >= Convert.ToDateTime(System.DateTime.Now.ToString("yyyy/MM/dd HH:00")) && !this.lab_rooms1.Text.Equals("0"))
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
        ShowRoomsData();
        ShowPetition();
    }
    #endregion

    #region 上星期
    protected void lbtn_PWeeks_Click(object sender, EventArgs e)
    {
        this.lab_today.Text = Convert.ToDateTime(this.lab_today.Text).AddDays(-7).ToString("yyyy-MM-dd");
        ShowRoomsData();
        ShowPetition();
    }
    #endregion

    #region 下星期
    protected void lbtn_NWeeks_Click(object sender, EventArgs e)
    {
        this.lab_today.Text = Convert.ToDateTime(this.lab_today.Text).AddDays(7).ToString("yyyy-MM-dd");
        ShowRoomsData();
        ShowPetition();
    }
    #endregion

    #region 下個月
    protected void lbtn_NMonth_Click(object sender, EventArgs e)
    {
        this.lab_today.Text = Convert.ToDateTime(this.lab_today.Text).AddMonths(1).ToString("yyyy-MM-dd");
        ShowRoomsData();
        ShowPetition();
    }
    #endregion

    #region 點選
    protected void lbtnfunction(object sender, EventArgs e)
    {
        Session["100402_1_CommandArgument"] = ((LinkButton)sender).CommandArgument;
        this.Response.Redirect("100402-1.aspx");
    }
    #endregion
}