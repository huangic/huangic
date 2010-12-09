using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;

/// <summary>
/// 功能名稱：管理作業 / 場地管理 / 場地資料管理
/// 功能編號：30/300400/300402
/// 撰寫者：Lina
/// 撰寫時間：2010/09/23
/// </summary>
public partial class _30_300400_300402 : System.Web.UI.Page
{
    ChangeObject changeobj = new ChangeObject();
    DBObject dbo = new DBObject();
    SessionObject sobj = new SessionObject();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if (Session["300402_pageIndex"] != null)
            {
                this.GridView1.DataBind();
                this.GridView1.PageIndex = Convert.ToInt32(Session["300402_pageIndex"].ToString());
            }
            else
            {
                Session.Add("300402_pageIndex", "");
                Session.Add("300402_no", "");
                Session.Add("300402_mode", "");
            }

            //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
            new OperatesObject().ExecuteOperates(300402, sobj.sessionUserID, 2, "場地資料列表");
        }
    }

    #region 調整輸出格式
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = new SpotDAO().GetNameBySpoNo(Convert.ToInt32(e.Row.Cells[0].Text));
            e.Row.Cells[3].Text = new PeopleDAO().GetPeopleNameByUid(Convert.ToInt32(e.Row.Cells[3].Text));
        }
    }
    #endregion

    #region 新增
    protected void btn_add_Click(object sender, EventArgs e)
    {
        string pageIndex = this.GridView1.PageIndex.ToString();
        Response.Write(PCalendarUtil.ShowMsg_URL("", "300402-1.aspx?mode=new&pageIndex=" + pageIndex + "&count=" + new System.Random().Next(10000).ToString()));
    }
    #endregion

    #region 修改、刪除
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("modify"))
        {
            Session["300402_no"] = this.GridView1.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString();
            Session["300402_pageIndex"] = this.GridView1.PageIndex.ToString();
            Session["300402_mode"] = "modify";


            //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
            new OperatesObject().ExecuteOperates(300402, sobj.sessionUserID, 2, "查詢 場地資料 編號:" + Session["300402_no"].ToString());
            Response.Redirect("300402-1.aspx");
        }
        else if (e.CommandName.Equals("del"))
        {
            string pkno = this.GridView1.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString();
            Session["300402_pageIndex"] = this.GridView1.PageIndex.ToString();

            string sqlstr = "update rooms set roo_status='2' where roo_no=" + pkno;
            dbo.ExecuteNonQuery(sqlstr);

            //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
            new OperatesObject().ExecuteOperates(300402, sobj.sessionUserID, 3, "刪除 場地資料 編號:" + pkno);
            Response.Redirect("300402.aspx");
        }
    }
    #endregion
}