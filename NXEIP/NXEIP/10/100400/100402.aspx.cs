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

            this.lab_today.Text = System.DateTime.Now.ToString("yyyy-MM-dd");

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
    }
    #endregion
}