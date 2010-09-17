using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;

/// <summary>
/// 功能名稱：管理作業 / 場地管理 / 所在地管理
/// 功能編號：30/300400/300401
/// 撰寫者：Lina
/// 撰寫時間：2010/09/17
/// </summary>
public partial class _30_300400_300401 : System.Web.UI.Page
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
        }
    }

    #region 調整輸出格式
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Text = new PeopleDAO().GetPeopleNameByUid(Convert.ToInt32(e.Row.Cells[1].Text));
            e.Row.Cells[2].Text = changeobj.ADDTtoROCDT(e.Row.Cells[2].Text);
        }
    }
    #endregion

    #region 新增
    protected void btn_add_Click(object sender, EventArgs e)
    {
        Response.Write(PCalendarUtil.ShowMsg_URL("", "300401-1.aspx?mode=new&count="+new System.Random().Next(10000).ToString()));
    }
    #endregion

    #region 修改、刪除
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("modify"))
        {
            string pkno = this.GridView1.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString();
            string pageIndex = this.GridView1.PageIndex.ToString();

            //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
            new OperatesObject().ExecuteOperates(300401, sobj.sessionUserID, 2, "查詢 所在地 編號:" + pkno);
            Response.Write(PCalendarUtil.ShowMsg_URL("", "300401-1.aspx?mode=modify&no=" + pkno + "&pageIndex=" + pageIndex + "&count=" + new System.Random().Next(10000).ToString()));
        }
        else if (e.CommandName.Equals("del"))
        {
            string pkno = this.GridView1.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString();
            string pageIndex = this.GridView1.PageIndex.ToString();

            string sqlstr = "update spot set spo_status='2' where spo_no=" + pkno;
            dbo.ExecuteNonQuery(sqlstr);

            //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
            new OperatesObject().ExecuteOperates(300401, sobj.sessionUserID, 3, "刪除 所在地 編號:" + pkno);
            Response.Write(PCalendarUtil.ShowMsg_URL("", "300401.aspx?pageIndex=" + pageIndex + "&count=" + new System.Random().Next(10000).ToString()));
        }
    }
    #endregion

}