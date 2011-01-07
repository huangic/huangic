using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using NXEIP.DAO;
using Entity;
using System.Web.UI.DataVisualization.Charting;


public partial class _30_300200_300202_3 : System.Web.UI.Page
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
            //int subtotal = 0;
            #region 單選或複選
            string sqlstr = "SELECT que_no, the_no, ans_no, ans_name FROM answers WHERE (que_no = " + que_no + ") AND (the_no = " + the_no + ") AND (ans_status = '1') ORDER BY ans_order";
            DataTable dt = new DataTable();
            dt = dbo.ExecuteQuery(sqlstr);
            if (dt.Rows.Count > 0)
            {
                string[] xValues = new string[dt.Rows.Count];
                double[] yValues = new double[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string ans_no = dt.Rows[i]["ans_no"].ToString();
                    xValues[i] = dt.Rows[i]["ans_name"].ToString();
                    string sqlstrc = "";
                    if (the_type.Equals("1"))
                        sqlstrc = "select count(casework.cas_no) as recount from casework inner join botanize on casework.bot_no = botanize.bot_no where (casework.que_no =" + que_no + ") and (casework.the_no =" + the_no + ") and (casework.cas_answer = '" + ans_no + "') and (botanize.bot_status = '1')";
                    else
                        sqlstrc = "select count(casework.cas_no) as recount from casework inner join botanize on casework.bot_no = botanize.bot_no where (casework.que_no =" + que_no + ") and (casework.the_no =" + the_no + ") and (casework.cas_answer like '" + ans_no + ",%' or casework.cas_answer like '%," + ans_no + "' or casework.cas_answer like '%," + ans_no + ",%' or casework.cas_answer='" + ans_no + "')";
                    DataTable dt1 = new DataTable();
                    dt1 = dbo.ExecuteQuery(sqlstrc);
                    if (dt1.Rows.Count > 0)
                    {
                        if (dt1.Rows[0]["recount"].ToString().Length > 0)
                        {
                            //subtotal += Convert.ToInt32(dt1.Rows[0]["recount"].ToString());
                            yValues[i] = Convert.ToDouble(dt1.Rows[0]["recount"].ToString());
                        }
                    }
                    else yValues[i] = 0.0;
                }
                ((Chart)e.Row.FindControl("Chart1")).Series["Default"].Points.DataBindXY(xValues, yValues);//Bind
                ((Chart)e.Row.FindControl("Chart1")).ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true; //3D圖
                for (var i = 0; i < ((Chart)e.Row.FindControl("Chart1")).Series.Count; i++)
                    for (var j = 0; j < ((Chart)e.Row.FindControl("Chart1")).Series[i].Points.Count; j++)
                    {
                        if(((Chart)e.Row.FindControl("Chart1")).Series[i].Points[j].YValues[0]==0)
                        ((Chart)e.Row.FindControl("Chart1")).Series[i].Points[j]["PieLabelStyle"] = "Disabled"; //當數值是0時，就不顯示標籤
                    }

                //((Chart)e.Row.FindControl("Chart1")).Series["Default"]["PieLabelStyle"] = "Outside"; //Inside(預設),Outside,Disabled
                //((Chart)e.Row.FindControl("Chart1")).BorderSkin.SkinStyle = BorderSkinStyle.None; //外框樣式
            }
            else ((Chart)e.Row.FindControl("Chart1")).Visible = false;
            #endregion

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