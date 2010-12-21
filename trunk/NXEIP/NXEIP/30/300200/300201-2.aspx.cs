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
public partial class _30_300200_300201_2 : System.Web.UI.Page
{
    ChangeObject changeobj = new ChangeObject();
    DBObject dbo = new DBObject();
    SessionObject sobj = new SessionObject();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
            new OperatesObject().ExecuteOperates(300201, sobj.sessionUserID, 2, "問卷題目 列表");

            //alt="300201-3.aspx?height=450&width=800&TB_iframe=true&modal=true"
            this.btn_insert.Attributes["alt"] = "300201-3.aspx?que_no="+Request["no"]+"&height=450&width=800&TB_iframe=true&modal=true";
            Entity.questionary queData = new QuestionaryDAO().GetByNo(Convert.ToInt32(Request["no"]));
            if (queData != null)
            {
                this.lab_name.Text = queData.que_name;
                this.lab_descript.Text = queData.que_descript;
                this.lab_sdate.Text = changeobj.ADDTtoROCDT(queData.que_sdate.Value.ToString("yyyy-MM-dd HH:mm"));
                this.lab_edate.Text = changeobj.ADDTtoROCDT(queData.que_edate.Value.ToString("yyyy-MM-dd HH:mm"));
            }
        }
    }

    #region 調整輸出格式
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[1].Text.Equals("1"))
                e.Row.Cells[1].Text = "單選";
            else if (e.Row.Cells[1].Text.Equals("2"))
                e.Row.Cells[1].Text = "複選";
            else
                e.Row.Cells[1].Text = "填充";

            if (e.Row.Cells[2].Text.Equals("1"))
                e.Row.Cells[2].Text = "計分";
            else
                e.Row.Cells[2].Text = "不計分";
        }
    }
    #endregion

    #region 刪除
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("del"))
        {
            string pkno1 = this.GridView1.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString();
            string pkno2 = this.GridView1.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[1].ToString();
            string sqlstr = "update theme set the_status='2',the_createuid=" + sobj.sessionUserID + ",the_createtime=getdate() where que_no=" + pkno1+" and the_no="+pkno2;
            dbo.ExecuteNonQuery(sqlstr);
            sqlstr = "update answers set ans_status='2',ans_createuid="+sobj.sessionUserID+",ans_createtime=getdate() where que_no="+pkno1+" and the_no="+pkno2;
            dbo.ExecuteNonQuery(sqlstr);

            //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
            new OperatesObject().ExecuteOperates(300201, sobj.sessionUserID, 3, "刪除 問卷題目 que_no:" + pkno1+",the_no="+pkno2);
            this.GridView1.DataBind();
        }
    }
    #endregion

    #region 回上一頁
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        Response.Redirect("300201.aspx?count=" + new System.Random().Next(10000).ToString());
    }
    #endregion
}