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
/// 功能名稱：管理作業 / 問卷管理 / 線上問卷
/// 功能編號：10/100400/100401
/// 撰寫者：Lina
/// 撰寫時間：2010/12/22
/// </summary>
public partial class _10_100400_100401 : System.Web.UI.Page
{
    ChangeObject changeobj = new ChangeObject();
    DBObject dbo = new DBObject();
    SessionObject sobj = new SessionObject();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
            new OperatesObject().ExecuteOperates(100401, sobj.sessionUserID, 2, "線上問卷");
        }
    }
    #region 調整輸出格式
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string pkno = ((GridView)sender).DataKeys[e.Row.RowIndex].Value.ToString();
            e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1); //編號
            //問卷主題，檢查是否為調查期間，是否已有填寫
            DateTime sdate = new DateTime();
            DateTime edate = new DateTime();
            sdate = Convert.ToDateTime(e.Row.Cells[3].Text);
            edate = Convert.ToDateTime(e.Row.Cells[4].Text);
            if ((sdate <= System.DateTime.Now) && (System.DateTime.Now <= edate))
            {
                int bot_no = new BotanizeDAO().GetNoByQuePeoNO(Convert.ToInt32(pkno), Convert.ToInt32(sobj.sessionUserID));
                if (bot_no == 0) e.Row.Cells[1].Text = "<a href=\"100401-1.aspx?no=" + pkno + "\" class=\"login-a\">" + e.Row.Cells[1].Text + "</a>";
            }

            e.Row.Cells[2].Text = new BotanizeDAO().GetCountByQueNo(Convert.ToInt32(pkno)).ToString()+"份";
            e.Row.Cells[3].Text = changeobj.ADDTtoROCDT(e.Row.Cells[3].Text);
            e.Row.Cells[4].Text = changeobj.ADDTtoROCDT(e.Row.Cells[4].Text);
            if (e.Row.Cells[5].Text.Equals("1"))
                e.Row.Cells[5].Text = "記名";
            else
                e.Row.Cells[5].Text = "不記名";
            e.Row.Cells[6].Text = new PeopleDAO().GetPeopleNameByUid(Convert.ToInt32(e.Row.Cells[6].Text));
        }
    }
    #endregion
}