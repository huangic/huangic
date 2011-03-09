using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;

/// <summary>
/// 功能名稱：管理作業 / 問卷管理 / 問卷維護
/// 功能編號：30/300200/300201
/// 撰寫者：Lina
/// 撰寫時間：2010/12/20
/// </summary>
public partial class _30_300200_300201 : System.Web.UI.Page
{
    ChangeObject changeobj = new ChangeObject();
    DBObject dbo = new DBObject();
    SessionObject sobj = new SessionObject();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
            new OperatesObject().ExecuteOperates(300201, sobj.sessionUserID, 2, "問卷 列表");
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
            e.Row.Cells[1].Text = changeobj.ADDTtoROCDT(e.Row.Cells[1].Text);
            e.Row.Cells[2].Text = changeobj.ADDTtoROCDT(e.Row.Cells[2].Text);
            if (e.Row.Cells[3].Text.Equals("1"))
                e.Row.Cells[3].Text = "記名";
            else
                e.Row.Cells[3].Text = "不記名";
            if (e.Row.Cells[4].Text.Equals("1"))
                e.Row.Cells[4].Text = "上架";
            else
                e.Row.Cells[4].Text = "下架";
        }
    }
    #endregion

    #region 刪除、題目設定、預覽、複製
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("del"))
        {
            string pkno = this.GridView1.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString();
            string sqlstr = "update questionary set que_status='3',que_createuid=" + sobj.sessionUserID + ",que_createtime=getdate() where que_no=" + pkno;
            dbo.ExecuteNonQuery(sqlstr);
            sqlstr = "update theme set the_status='2',the_createuid=" + sobj.sessionUserID + ",the_createtime=getdate() where que_no=" + pkno;
            dbo.ExecuteNonQuery(sqlstr);
            sqlstr = "update answers set ans_status='2',ans_createuid=" + sobj.sessionUserID + ",ans_createtime=getdate() where que_no=" + pkno;
            dbo.ExecuteNonQuery(sqlstr);

            //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
            new OperatesObject().ExecuteOperates(300201, sobj.sessionUserID, 3, "刪除 問卷 編號:" + pkno);

            this.GridView1.DataBind();
        }
        else if (e.CommandName.Equals("setup"))
        {
            string pkno = this.GridView1.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString();
            Response.Redirect("300201-2.aspx?no=" + pkno + "&count=" + new System.Random().Next(10000).ToString());
        }
        else if (e.CommandName.Equals("preview"))
        {
            string pkno = this.GridView1.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString();
            Response.Redirect("300201-4.aspx?no=" + pkno + "&count=" + new System.Random().Next(10000).ToString());
        }
        
    }
    #endregion
}