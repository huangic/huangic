using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using NXEIP.DAO;
using Entity;


/// <summary>
/// 功能名稱：管理作業 / 場地管理 / 場地申請審核
/// 功能編號：30/300400/300403
/// 撰寫者：Lina
/// 撰寫時間：2010/12/11
/// </summary>
/// 
public partial class _30_300400_300403 : System.Web.UI.Page
{
    ChangeObject changeobj = new ChangeObject();
    DBObject dbo = new DBObject();
    SessionObject sobj = new SessionObject();
    CheckObject checkobj = new CheckObject();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            //第一部份：查詢項目
            this.calendar1._ADDate = Convert.ToDateTime(System.DateTime.Today.ToString("yyyy-MM-01"));
            this.calendar2._ADDate = Convert.ToDateTime((System.DateTime.Today.Year+1)+"-01-01").AddDays(-1);
            ListItem allitem = new ListItem("全部", "0");
            #region 場管之所在地、場所
            string sqlstr = "select spot.spo_no, spot.spo_name from spot inner join rooms on spot.spo_no = rooms.spo_no inner join checker on rooms.roo_no = checker.roo_no"
                + " where (spot.spo_function like '____1%') and (spot.spo_status = '1') and (rooms.roo_status = '1') and (checker.che_peouid = " + sobj.sessionUserID + ")";
            DataTable dt = new DataTable();
            dt = dbo.ExecuteQuery(sqlstr);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ListItem newitem = new ListItem(dt.Rows[i]["spo_name"].ToString(), dt.Rows[i]["spo_no"].ToString());
                    this.ddl_spot.Items.Add(newitem);
                }
            }
            this.ddl_spot.Items.Insert(0, allitem);
            this.ddl_rooms.Items.Insert(0, allitem);
            #endregion

            ShowDataList();
        }
    }

    #region 畫面：List
    private void ShowDataList()
    {
        if (this.calendar1._AD.Length > 0 && this.calendar2._AD.Length > 0)
        {
            this.ObjectDataSource1.SelectParameters["sdate"].DefaultValue = this.calendar1._AD;
            this.ObjectDataSource1.SelectParameters["edate"].DefaultValue = this.calendar2._AD;
            this.ObjectDataSource1.SelectParameters["status"].DefaultValue = this.rbl_status.SelectedValue;
            this.ObjectDataSource1.SelectParameters["spots1"].DefaultValue = this.ddl_spot.SelectedValue;
            this.ObjectDataSource1.SelectParameters["rooms1"].DefaultValue = this.ddl_rooms.SelectedValue;
            this.ObjectDataSource1.SelectParameters["loginuser"].DefaultValue = sobj.sessionUserID;
            this.GridView1.DataBind();
            if (this.lab_pageIndex.Text.Length > 0) this.GridView1.PageIndex = Convert.ToInt32(this.lab_pageIndex.Text);

            //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
            new OperatesObject().ExecuteOperates(300403, sobj.sessionUserID, 2, "場地資料列表");
        }
    }
    #endregion

    #region 調整輸出格式
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string pkno = ((GridView)sender).DataKeys[e.Row.RowIndex].Value.ToString();
            e.Row.Cells[2].Text = new DepartmentsDAO().GetNameByNo(Convert.ToInt32(e.Row.Cells[2].Text));
            e.Row.Cells[3].Text = new PeopleDAO().GetPeopleNameByUid(Convert.ToInt32(e.Row.Cells[3].Text));
            if (e.Row.Cells[8].Text.Equals("1"))
            {
                e.Row.Cells[8].Text = "送審中";
                string c9 = "<a href=\"300403-1.aspx?no=" + pkno + "&height=450&width=800&TB_iframe=true&modal=true\" class=\"thickbox imageButton alter\"><span>審核</span></a>";
                e.Row.Cells[9].Text = c9;
            }
            else if (e.Row.Cells[8].Text.Equals("2"))
                e.Row.Cells[8].Text = "核可";
            else if (e.Row.Cells[8].Text.Equals("3"))
                e.Row.Cells[8].Text = "不核可";
            else
                e.Row.Cells[8].Text = "自行取消";
        }
    }
    #endregion

    #region 顯示錯誤訊息
    private void ShowMsg(string msg)
    {
        string script = "<script>window.alert('" + msg + "');</script>";
        this.ClientScript.RegisterStartupScript(this.GetType(), "MSG", script);
    }
    #endregion

    #region 檢查輸入值
    private bool CheckInputValue()
    {
        bool feedback = true;

        #region 日期區間
        try
        {
            if (this.calendar1._ADDate > this.calendar2._ADDate)
            {
                this.ShowMsg("結束時間不得小於開始時間!");
                feedback = false;
            }
        }
        catch
        {
            this.ShowMsg("請輸入正確日期!");
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
                ShowDataList(); //呼叫列表
            }
        }
        catch (Exception ex)
        {
            aMSG = "功能名稱：" + this.Navigator1.SubFunc + "<br>錯誤訊息:" + ex.ToString();
            Response.Write(aMSG);
        }
    }
    #endregion

    #region 所在地更換時
    protected void ddl_spot_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.ddl_rooms.Items.Clear();
        ListItem allitem = new ListItem("全部","0");
        #region 場所
        string sqlstr = "select rooms.roo_no, rooms.roo_name from rooms inner join checker on rooms.roo_no = checker.roo_no where (rooms.roo_status = '1') and (checker.che_peouid = " + sobj.sessionUserID + ")";
        DataTable dt = new DataTable();
        dt = dbo.ExecuteQuery(sqlstr);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ListItem newitem = new ListItem(dt.Rows[i]["roo_name"].ToString(), dt.Rows[i]["roo_no"].ToString());
                this.ddl_rooms.Items.Add(newitem);
            }
        }
        this.ddl_rooms.Items.Insert(0, allitem);
        #endregion
    }
    #endregion
}