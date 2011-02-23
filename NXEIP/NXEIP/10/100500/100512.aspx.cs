using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using NXEIP.DAO;
using Entity;

public partial class _10_100500_100512 : System.Web.UI.Page
{
    ChangeObject changeobj = new ChangeObject();
    SessionObject sobj = new SessionObject();
    DBObject dbo = new DBObject();
    protected void Page_Load(object sender, EventArgs e)
    {
        string aMSG = "";
        try
        {
            this.lab_today.Text = PCalendarUtil.GetToday(); //今天日期

            DataTable dt99 = new DataTable();
            string sqlstr99 = "";

            this.Table1.Rows.Clear();
            this.Table1.Dispose();
            int rowcount = -1;
            string sdate = System.DateTime.Today.ToString("yyyy/MM/dd") + " 00:00:00";
            string edate = System.DateTime.Today.ToString("yyyy/MM/dd") + " 23:59:59";

            #region 行事曆
            sqlstr99 = "select people.peo_name,c02.c02_sdate,c02.c02_edate, c02.c02_title, c02.c02_appointmen, c02.c02_check from c02 inner join people on c02.peo_uid = people.peo_uid inner join types on people.peo_pfofess = types.typ_no"
                + " where (people.dep_no = " + sobj.sessionUserDepartID + ") and (c02_check<>'2') and (c02.c02_sdate <= '" + System.DateTime.Today.ToString("yyyy/MM/dd") + " 23:59:59') and (c02.c02_edate <= '" + System.DateTime.Today.ToString("yyyy/MM/dd") + " 23:59:59') and (c02.c02_edate >= '" + System.DateTime.Today.ToString("yyyy/MM/dd") + " 00:00:00') or"
                + " (people.dep_no = " + sobj.sessionUserDepartID + ") and (c02_check<>'2') and (c02.c02_sdate >= '" + System.DateTime.Today.ToString("yyyy/MM/dd") + " 00:00:00') and (c02.c02_edate >= '" + System.DateTime.Today.ToString("yyyy/MM/dd") + " 00:00:00') and (c02.c02_sdate <= '" + System.DateTime.Today.ToString("yyyy/MM/dd") + " 23:59:59') or"
                + " (people.dep_no = " + sobj.sessionUserDepartID + ") and (c02_check<>'2') and (c02.c02_sdate < '" + System.DateTime.Today.ToString("yyyy/MM/dd") + " 00:00:00') and (c02.c02_edate > '" + System.DateTime.Today.ToString("yyyy/MM/dd") + " 23:59:59')"
                + " order by types.typ_order,people.peo_name,c02.c02_sdate, c02.c02_edate, c02.c02_no";

            dt99 = dbo.ExecuteQuery(sqlstr99);
            if (dt99.Rows.Count > 0)
            {
                for (int i = 0; i < dt99.Rows.Count; i++)
                {
                    if ((i == 0) || (i > 0 && !dt99.Rows[i]["peo_name"].ToString().Equals(dt99.Rows[i - 1]["peo_name"].ToString())))
                    {
                        rowcount++;
                        this.Table1.Rows.Add(new System.Web.UI.WebControls.TableRow());
                        this.Table1.Rows[rowcount].Cells.Add(new System.Web.UI.WebControls.TableCell());
                        this.Table1.Rows[rowcount].Cells.Add(new System.Web.UI.WebControls.TableCell());
                        this.Table1.Rows[rowcount].Cells[0].CssClass = "row_bg";
                        this.Table1.Rows[rowcount].Cells[1].CssClass = "row_bgc";
                        this.Table1.Rows[rowcount].Cells[0].Text = "<span class=\"row_time\">"+dt99.Rows[i]["peo_name"].ToString()+"</span>";
                    }

                    string stime = Convert.ToDateTime(dt99.Rows[i]["c02_sdate"].ToString()).ToString("HH:mm");
                    string etime = Convert.ToDateTime(dt99.Rows[i]["c02_edate"].ToString()).ToString("HH:mm");

                    this.Table1.Rows[rowcount].Cells[1].Text += "<li class=\"p1\">" + Display( stime + "~" + etime + " " + dt99.Rows[i]["c02_title"].ToString(), dt99.Rows[i]["c02_appointmen"].ToString(), dt99.Rows[i]["c02_check"].ToString()) + "</li>";
                }
            }
            #endregion

            #region 有值時
            if (rowcount < 0)
            {
                this.Table1.Visible = false;
                this.lab_msg.Text = "目前無排定任何行事曆";
            }
            else
                this.Table1.Visible = true;
            #endregion
        }
        catch (Exception ex)
        {
            aMSG = "功能名稱:單位行事曆<br>錯誤訊息:" + ex.ToString();
            Response.Write(aMSG);
        }
    }

    #region show行程
    private string Display(string txt, string appointmen, string checks)
    {
        string aMSG = "";
        string txt1 = "";
        try
        {
            if (appointmen.Equals("2"))
                txt1 = "<span class=\"row_schedule\">" + txt + "</span><br />";
            else
            {
                //預約行程
                if (checks.Equals("0"))
                    txt1 = "<span class=\"row_schedule\">" + txt + "(預約)</span><br />";
                else
                    txt1 = "<span class=\"row_schedule\">" + txt + "</span><br />";
            }
        }
        catch (Exception ex)
        {
            aMSG = "功能名稱:單位行事曆--Display<br>錯誤訊息:" + ex.ToString();
            Response.Write(aMSG);
        }
        return txt1;
    }
    #endregion
}