using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using NXEIP.DAO;
using Entity;

public partial class _30_300200_300202_1 : System.Web.UI.Page
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
            string que_no = ((GridView)sender).DataKeys[e.Row.RowIndex].Values[0].ToString();
            string the_no = ((GridView)sender).DataKeys[e.Row.RowIndex].Values[1].ToString();
            string the_type = ((Label)e.Row.FindControl("lab_the_type")).Text;

            if (the_type.Equals("1"))
                ((Label)e.Row.FindControl("lab_the_name")).Text += "<font color=red>(單選)</font>";
            else if (the_type.Equals("2"))
                ((Label)e.Row.FindControl("lab_the_name")).Text += "<font color=red>(複選)</font>";
            else if (the_type.Equals("3"))
                ((Label)e.Row.FindControl("lab_the_name")).Text += "<font color=red>(填充)</font>";

            if (the_type.Equals("3"))
            {
                #region 填充
                ((GridView)e.Row.FindControl("GridView2")).Visible = false;
                ((Label)e.Row.FindControl("lab_the_name")).Text += "&nbsp;&nbsp;&nbsp;&nbsp;<a href=\"300202-a.aspx?que_no=" + que_no + "&the_no=" + the_no + "&height=450&width=800&TB_iframe=true&modal=true\" class=\"thickbox\">填寫者清單</a>";
                #endregion
            }
            else
            {
                #region 單選或複選
                this.SqlDataSource1.SelectParameters["que_no"].DefaultValue = que_no;
                this.SqlDataSource1.SelectParameters["the_no"].DefaultValue = the_no;
                ((GridView)e.Row.FindControl("GridView2")).DataSource = this.SqlDataSource1;
                ((GridView)e.Row.FindControl("GridView2")).DataBind();

                // 計算總票數
                int total = 0, mxcount = 0;
                for (int Gi = 0; Gi < ((GridView)e.Row.FindControl("GridView2")).Rows.Count; Gi++)
                {
                    total+=Convert.ToInt32(((GridView)e.Row.FindControl("GridView2")).Rows[Gi].Cells[1].Text);
                    if (mxcount < Convert.ToInt32(((GridView)e.Row.FindControl("GridView2")).Rows[Gi].Cells[1].Text))
                        mxcount = Convert.ToInt32(((GridView)e.Row.FindControl("GridView2")).Rows[Gi].Cells[1].Text);
                }
                
                //顯示圖片與票數
                for (int Gi = 0; Gi < ((GridView)e.Row.FindControl("GridView2")).Rows.Count; Gi++)
                {
                    int imgindex = (Gi % 10) + 1;
                    int icount = Convert.ToInt32(((GridView)e.Row.FindControl("GridView2")).Rows[Gi].Cells[1].Text);
                    string ans_name = ((GridView)e.Row.FindControl("GridView2")).Rows[Gi].Cells[0].Text;
                    if (icount > 0)
                    {
                        int widths = Convert.ToInt32(Math.Round(Convert.ToSingle(icount) / Convert.ToSingle(total) * 200));
                        string outtxt = "";
                        if (widths > 0) outtxt = "<img src=\"../../image/" + imgindex.ToString() + ".png\" height=\"20\" width=\"" + widths.ToString() + "\" align=\"absmiddle\" alt=\"" + ans_name + ":" + icount + "票\" title=\"" + ans_name + ":" + icount + "票\" style=\"border:1px solid #000000;padding:0px;margin-bottom:2px;margin-top:2px;\" />";
                        ((GridView)e.Row.FindControl("GridView2")).Rows[Gi].Cells[1].Text = icount.ToString() + "票&nbsp;" + outtxt;
                    }
                    else
                        ((GridView)e.Row.FindControl("GridView2")).Rows[Gi].Cells[1].Text = "0票";
                }
                #endregion
            }
        }
    }
    #endregion

    #region 票數
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string que_no = ((GridView)sender).DataKeys[e.Row.RowIndex].Values[0].ToString();
            string the_no = ((GridView)sender).DataKeys[e.Row.RowIndex].Values[1].ToString();
            string ans_no = ((GridView)sender).DataKeys[e.Row.RowIndex].Values[2].ToString();
            string the_type = ((Label)(((DataControlFieldCell)((GridView)sender).Parent).Parent).FindControl("lab_the_type")).Text;
            string sqlstrc = "";
            if (the_type.Equals("1"))
                sqlstrc = "select count(casework.cas_no) as recount from casework inner join botanize on casework.bot_no = botanize.bot_no where (casework.que_no =" + que_no + ") and (casework.the_no =" + the_no + ") and (casework.cas_answer = '" + ans_no + "') and (botanize.bot_status = '1')";
            else
                sqlstrc = "select count(casework.cas_no) as recount from casework inner join botanize on casework.bot_no = botanize.bot_no where (casework.que_no =" + que_no + ") and (casework.the_no =" + the_no + ") and (casework.cas_answer like '" + ans_no + ",%' or casework.cas_answer like '%," + ans_no + "' or casework.cas_answer like '%," + ans_no + ",%' or casework.cas_answer='" + ans_no + "')";
            DataTable dt = new DataTable();
            dt = dbo.ExecuteQuery(sqlstrc);
            e.Row.Cells[1].Text = "0";
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["recount"].ToString().Length > 0) e.Row.Cells[1].Text = dt.Rows[0]["recount"].ToString();
            }
        }
    }
    #endregion
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("AnswerList"))
        {
            string que_no = this.GridView1.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString();
            string the_no = this.GridView1.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[1].ToString();
            string ans_no = this.GridView1.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[2].ToString();
        }
    }
    #region 回上一頁
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        Response.Redirect("300202.aspx?count=" + new System.Random().Next(10000).ToString());
    }
    #endregion
}
