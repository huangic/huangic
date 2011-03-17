using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;

/// <summary>
/// 功能名稱：管理作業 / 問卷管理 / 問卷統計
/// 功能編號：30/300200/300202
/// 撰寫者：Lina
/// 撰寫時間：2010/12/22
/// </summary>
public partial class _30_300200_300202 : System.Web.UI.Page
{
    ChangeObject changeobj = new ChangeObject();
    DBObject dbo = new DBObject();
    SessionObject sobj = new SessionObject();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
            new OperatesObject().ExecuteOperates(300202, sobj.sessionUserID, 2, "問卷統計列表");
        }
    }

    #region 調整輸出格式
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string pkno = ((GridView)sender).DataKeys[e.Row.RowIndex].Value.ToString();
            e.Row.Cells[1].Text = new BotanizeDAO().GetCountByQueNo(Convert.ToInt32(pkno)).ToString() + "份";
            e.Row.Cells[2].Text = changeobj.ADDTtoROCDT(e.Row.Cells[2].Text);
            e.Row.Cells[3].Text = changeobj.ADDTtoROCDT(e.Row.Cells[3].Text);
            if (e.Row.Cells[4].Text.Equals("1"))
                e.Row.Cells[4].Text = "上架";
            else
                e.Row.Cells[4].Text = "下架";
        }
    }
    #endregion

    #region 票數統計、問卷填寫者、圓餅圖
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("count"))
        {
            string pkno = this.GridView1.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString();
            Response.Redirect("300202-1.aspx?no=" + pkno + "&count=" + new System.Random().Next(10000).ToString());
        }
        else if (e.CommandName.Equals("botlist"))
        {
            string pkno = this.GridView1.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString();
            Response.Redirect("300202-2.aspx?no=" + pkno + "&count=" + new System.Random().Next(10000).ToString());
        }
        else if (e.CommandName.Equals("PieChart"))
        {
            string pkno = this.GridView1.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString();
            Response.Redirect("300202-3.aspx?no=" + pkno + "&count=" + new System.Random().Next(10000).ToString());
        }
        else if (e.CommandName.Equals("Unfilled"))
        {
            string pkno = this.GridView1.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString();
            Response.Redirect("300202-4.aspx?no=" + pkno + "&count=" + new System.Random().Next(10000).ToString());
        }
    }
    #endregion
}