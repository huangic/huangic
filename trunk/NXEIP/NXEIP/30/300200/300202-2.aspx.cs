using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using NXEIP.DAO;
using Entity;

public partial class _30_300200_300202_2 : System.Web.UI.Page
{
    ChangeObject changeobj = new ChangeObject();
    DBObject dbo = new DBObject();
    SessionObject sobj = new SessionObject();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if (Request["no"] != null) this.lab_no.Text = Request["no"];
            #region 問卷基本資料
            questionary que = new QuestionaryDAO().GetByNo(Convert.ToInt32(this.lab_no.Text));
            if (que != null)
            {
                this.lab_name.Text = que.que_name;
                this.lab_descript.Text = que.que_descript;
            }
            #endregion
        }
    }

     #region 調整輸出格式
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[3].Text = changeobj.ADDTtoROCDT(e.Row.Cells[3].Text);
        }
    }
     #endregion

    #region 回上一頁
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        Response.Redirect("300202.aspx?count=" + new System.Random().Next(10000).ToString());
    }
    #endregion
}