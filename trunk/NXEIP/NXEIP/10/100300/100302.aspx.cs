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
/// 功能名稱：個人應用 / 行事曆 / 開放他人設定
/// 功能編號：10/100300/100302
/// 撰寫者：Lina
/// 撰寫時間：2010/11/01
/// </summary>
public partial class _10_100300_100302 : System.Web.UI.Page
{
    ChangeObject changeobj = new ChangeObject();
    DBObject dbo = new DBObject();
    SessionObject sobj = new SessionObject();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            //if (!string.IsNullOrEmpty(Request["pageIndex"]))
            //{
            //    this.GridView1.DataBind();
            //    this.GridView1.PageIndex = Convert.ToInt32(Request["pageIndex"]);
            //}
            //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
            new OperatesObject().ExecuteOperates(100302, sobj.sessionUserID, 2, "開放他人設定");
        }
              
    }

    #region 調整輸出格式
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string sqlstr = "SELECT people.peo_uid, people.peo_name, departments.dep_name, types.typ_cname FROM people INNER JOIN departments ON people.dep_no = departments.dep_no LEFT OUTER JOIN types ON people.peo_pfofess = types.typ_no"
                + " WHERE (people.peo_uid = " + e.Row.Cells[2].Text + ")";
            DataTable dt = new DataTable();
            dt = dbo.ExecuteQuery(sqlstr);
            if (dt.Rows.Count > 0)
            {
                e.Row.Cells[0].Text = dt.Rows[0]["dep_name"].ToString();
                e.Row.Cells[1].Text = dt.Rows[0]["typ_cname"].ToString();
                e.Row.Cells[2].Text = dt.Rows[0]["peo_name"].ToString();
            }
            else
            {
                e.Row.Cells[2].Text = "&nbsp;";
            }
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

            string sqlstr = "delete from c01 where c01_no=" + pkno;
            dbo.ExecuteNonQuery(sqlstr);

            //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
            new OperatesObject().ExecuteOperates(100302, sobj.sessionUserID, 3, "刪除 開放他人設定 編號:" + pkno);
            this.GridView1.DataBind();
            //Response.Redirect("100302.aspx?pageIndex=" + pageIndex + "&count=" + new System.Random().Next(10000).ToString());
        }
    }
    #endregion
}