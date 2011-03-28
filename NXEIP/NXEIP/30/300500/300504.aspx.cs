using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;


/// <summary>
/// 功能名稱：管理作業 / 資料管理 / 電腦管理
/// 功能編號：30/300500/300504
/// 撰寫者：Lina
/// 撰寫時間：2011/03/28
/// </summary>
public partial class _30_300500_300504 : System.Web.UI.Page
{
    ChangeObject changeobj = new ChangeObject();
    DBObject dbo = new DBObject();
    SessionObject sobj = new SessionObject();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
            new OperatesObject().ExecuteOperates(300504, sobj.sessionUserID, 2, "電腦管理資料 列表");
        }

        //判斷來自JS 使用_doPostBack(updatePanel,"") 的情況 
        if (Request["__EVENTTARGET"] == this.UpdatePanel1.ClientID && String.IsNullOrEmpty(Request["__EVENTARGUMENT"]))
        {
            this.GridView1.DataBind();
        }
    }

    #region 調整輸出格式
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[3].Text = changeobj.ADDTtoROCDT(e.Row.Cells[3].Text);
            e.Row.Cells[5].Text = changeobj.ADDTtoROCDT(e.Row.Cells[5].Text);
            if (e.Row.Cells[6].Text.Equals("1"))
                e.Row.Cells[6].Text = "調撥";
            else
                e.Row.Cells[6].Text = "借用";
            if (e.Row.Cells[2].Text.Trim().Length > 0 && !e.Row.Cells[2].Text.Trim().Equals("&nbsp;"))
            {
                e.Row.Cells[2].Text = new PeopleDAO().GetPeopleNameByUid(Convert.ToInt32(e.Row.Cells[2].Text));
            }
            if (e.Row.Cells[4].Text.Trim().Length > 0 && !e.Row.Cells[4].Text.Trim().Equals("&nbsp;"))
            {
                e.Row.Cells[4].Text = new PeopleDAO().GetPeopleNameByUid(Convert.ToInt32(e.Row.Cells[4].Text));
            }
        }
    }
    #endregion

    #region 刪除、題目設定、預覽、複製
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("del"))
        {
            string pkno = this.GridView1.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString();
            string sqlstr = "update dispatch set dis_status='2',dis_createuid=" + sobj.sessionUserID + ",dis_createtime=getdate() where dis_no=" + pkno;
            dbo.ExecuteNonQuery(sqlstr);

            //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
            new OperatesObject().ExecuteOperates(300504, sobj.sessionUserID, 3, "刪除 電腦管理資料 編號:" + pkno);

            this.GridView1.DataBind();
        }
    }
    #endregion
}