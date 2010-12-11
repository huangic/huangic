using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;
using System.Data;

/// <summary>
/// 功能名稱：管理作業 / 場地管理 / 審核者設定
/// 功能編號：30/300400/300404
/// 撰寫者：Lina
/// 撰寫時間：2010/12/11
/// </summary>
public partial class _30_300400_300404 : System.Web.UI.Page
{
    ChangeObject changeobj = new ChangeObject();
    DBObject dbo = new DBObject();
    SessionObject sobj = new SessionObject();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if (Request["pageIndex"] != null) this.lab_pageIndex.Text = Request["pageIndex"];
            ShowDataList();

            
        }
    }

    #region 畫面：列表
    private void ShowDataList()
    {
        this.MultiView1.ActiveViewIndex = 0;
        this.Navigator1.SubFunc = "";
        if (this.lab_pageIndex.Text.Length > 0)
        {
            this.GridView1.DataBind();
            this.GridView1.PageIndex = Convert.ToInt32(this.lab_pageIndex.Text);
        }
        //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
        new OperatesObject().ExecuteOperates(300404, sobj.sessionUserID, 2, "審核者 列表");
    }
    #endregion

    #region 畫面：新增
    private void ShowDataInsert()
    {
        this.MultiView1.ActiveViewIndex = 1;
        this.ddl_rooms.SelectedItem.Selected = false;
        this.ddl_spot.SelectedItem.Selected = false;
        this.DepartTreeListBox1.Items.Clear();
    }
    #endregion

    #region 調整輸出格式
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[2].Text = new PeopleDAO().GetPeopleNameByUid(Convert.ToInt32(e.Row.Cells[2].Text));
            rooms rooms1 = new RoomsDAO().GetByRoomsNo(Convert.ToInt32(e.Row.Cells[1].Text));
            e.Row.Cells[1].Text = rooms1.roo_name;
            e.Row.Cells[0].Text = rooms1.spot.spo_name;
        }
    }
    #endregion

    #region 刪除
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("del"))
        {
            #region 執行刪除
            string pkno = this.GridView1.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString();
            this.lab_pageIndex.Text = this.GridView1.PageIndex.ToString();
            string sqlstr = "delete from checker where che_no=" + pkno;
            dbo.ExecuteNonQuery(sqlstr);
            //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
            new OperatesObject().ExecuteOperates(300404, sobj.sessionUserID, 3, "刪除 審核者 編號:" + pkno);
            #endregion
            ShowDataList(); //呼叫列表
        }
    }
    #endregion

    #region 呼叫新增畫面
    protected void btn_add_Click(object sender, EventArgs e)
    {
        this.lab_pageIndex.Text = this.GridView1.PageIndex.ToString();
        this.Navigator1.SubFunc = "新增";
        ShowDataInsert();
    }
    #endregion

    #region 顯示錯誤訊息
    private void ShowMSG(string msg)
    {
        string script = "<script>alert('" + msg + "');</script>";
        this.ClientScript.RegisterStartupScript(this.GetType(), "msg", script);
    }
    #endregion

    #region 檢查輸入值
    private bool CheckInputValue()
    {
        #region 場地名稱
        if (this.ddl_rooms.SelectedValue.Equals("0"))
        {
            ShowMSG("請選擇 場地名稱");
            return false;
        }
        #endregion

        #region 審核人
        if (this.DepartTreeListBox1.Items.Count <= 0 || this.DepartTreeListBox1.Items == null)
        {
            ShowMSG("請選擇 審核人");
            return false;
        }
        #endregion

        return true;
    }
    #endregion

    #region 確定
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        string aMSG = "";
        try
        {
            if (CheckInputValue())
            {
                int roo_no = Convert.ToInt32(this.ddl_rooms.SelectedValue);
                for (int i = 0; i < this.DepartTreeListBox1.Items.Count; i++)
                {
                    int count = new CheckerDAO().GetCount(roo_no, Convert.ToInt32(this.DepartTreeListBox1.Items[i].Key));
                    if (count <= 0)
                    {
                        #region 新增
                        string InsStr = "insert into checker (roo_no,che_peouid) values(" + roo_no + "," + this.DepartTreeListBox1.Items[i].Key + ")";
                        dbo.ExecuteNonQuery(InsStr);

                        //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
                        new OperatesObject().ExecuteOperates(300401, sobj.sessionUserID, 1, "場所：" + this.ddl_rooms.SelectedItem.Text + ",審核人：" + this.DepartTreeListBox1.Items[i].Value);
                        #endregion
                    }
                }
                Response.Write(PCalendarUtil.ShowMsg_URL("", "300404.aspx"));
            }
        }
        catch (Exception ex)
        {
            aMSG = "功能名稱：審核人設定-" + this.Navigator1.SubFunc + "<br>錯誤訊息:" + ex.ToString();
            Response.Write(aMSG);
        }
    }
    #endregion

    #region 取消
    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        ShowDataList(); //呼叫列表
    }
    #endregion

    #region 所在地更換時
    protected void ddl_spot_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.ddl_rooms.Items.Clear();
        if (!this.ddl_spot.SelectedValue.Equals("0"))
        {
            this.SqlDataSource2.SelectParameters["spo_no"].DefaultValue = this.ddl_spot.SelectedValue;
            this.ddl_rooms.DataBind();
        }
        ListItem selitem = new ListItem("請選擇", "0");
        this.ddl_rooms.Items.Insert(0, selitem);
    }
    #endregion
}