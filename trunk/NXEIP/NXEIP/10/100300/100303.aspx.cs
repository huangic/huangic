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
/// 功能名稱：個人應用 / 行事曆 / 開放查看權限
/// 功能編號：10/100300/100303
/// 撰寫者：Lina
/// 撰寫時間：2010/11/04
/// </summary>
public partial class _10_100300_100303 : System.Web.UI.Page
{
    ChangeObject changeobj = new ChangeObject();
    DBObject dbo = new DBObject();
    SessionObject sobj = new SessionObject();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request["pageIndex"]))
            {
                this.GridView1.DataBind();
                this.GridView1.PageIndex = Convert.ToInt32(Request["pageIndex"]);
            }
            //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
            new OperatesObject().ExecuteOperates(100303, sobj.sessionUserID, 2, "開放查看權限");
        }
    }

    #region 調整輸出格式
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[3].Text.Equals("1"))
                e.Row.Cells[3].Text = "全體";
            else
                e.Row.Cells[3].Text = "單位(含子部門)";

            e.Row.Cells[4].Text = new PeopleDAO().GetPeopleNameByUid(Convert.ToInt32(e.Row.Cells[4].Text));
            e.Row.Cells[5].Text = changeobj.ADDTtoROCDT(e.Row.Cells[5].Text);
        }
    }
    #endregion

    #region 修改、刪除
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("del"))
        {
            string pkno = this.GridView1.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString();
            string pageIndex = this.GridView1.PageIndex.ToString();

            string sqlstr = "delete from c04 where c04_no=" + pkno;
            dbo.ExecuteNonQuery(sqlstr);

            //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
            new OperatesObject().ExecuteOperates(100303, sobj.sessionUserID, 3, "刪除 開放查看權限 編號:" + pkno);

            Response.Redirect("100303.aspx?pageIndex=" + pageIndex + "&count=" + new System.Random().Next(10000).ToString());
        }
    }
    #endregion

    #region 新增
    protected void btn_add_Click(object sender, EventArgs e)
    {
        string pageIndex = this.GridView1.PageIndex.ToString();
        Response.Redirect("100303-1.aspx?pageIndex=" + pageIndex + "&count=" + new System.Random().Next(10000).ToString());
    }
    #endregion
}