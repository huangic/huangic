using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using System.Data;
using NXEIP.Widget;
using NLog;

public partial class widget_10_100300_100301_1 : WidgetBaseControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DBObject dbo = new DBObject();
        SessionObject sobj = new SessionObject();
        ChangeObject changeobj = new ChangeObject();
        string sqlstr_100301_1 = "select top 5 people.peo_name,c02.c02_sdate,c02.c02_edate, c02.c02_title, c02.c02_appointmen, c02.c02_check from c02 inner join people on c02.peo_uid = people.peo_uid"
            + " where (people.dep_no = " + sobj.sessionUserDepartID + ") and (c02_check<>'2') and (c02.c02_sdate <= '" + System.DateTime.Today.ToString("yyyy/MM/dd") + " 23:59:59') and (c02.c02_edate <= '" + System.DateTime.Today.ToString("yyyy/MM/dd") + " 23:59:59') and (c02.c02_edate >= '" + System.DateTime.Today.ToString("yyyy/MM/dd") + " 00:00:00') or"
            + " (people.dep_no = " + sobj.sessionUserDepartID + ") and (c02_check<>'2') and (c02.c02_sdate >= '" + System.DateTime.Today.ToString("yyyy/MM/dd") + " 00:00:00') and (c02.c02_edate >= '" + System.DateTime.Today.ToString("yyyy/MM/dd") + " 00:00:00') and (c02.c02_sdate <= '" + System.DateTime.Today.ToString("yyyy/MM/dd") + " 23:59:59') or"
            + " (people.dep_no = " + sobj.sessionUserDepartID + ") and (c02_check<>'2') and (c02.c02_sdate < '" + System.DateTime.Today.ToString("yyyy/MM/dd") + " 00:00:00') and (c02.c02_edate > '" + System.DateTime.Today.ToString("yyyy/MM/dd") + " 23:59:59')"
            + " order by c02.c02_sdate, c02.c02_edate, c02.c02_no";
        DataTable dt_100301_1 = new DataTable();
        dt_100301_1 = dbo.ExecuteQuery(sqlstr_100301_1);
        if (dt_100301_1.Rows.Count > 0)
        {
            int rowcount = -1;
            rowcount++;
            this.Table_100301_1.Rows.Add(new System.Web.UI.WebControls.TableRow());
            this.Table_100301_1.Rows[rowcount].Cells.Add(new System.Web.UI.WebControls.TableCell());
            this.Table_100301_1.Rows[rowcount].Cells.Add(new System.Web.UI.WebControls.TableCell());
            this.Table_100301_1.Rows[rowcount].Cells[1].CssClass = "row_2";
            this.Table_100301_1.Rows[rowcount].Cells[1].Text = "今天是 "+new ChangeObject().ADDTtoROCDT(System.DateTime.Today.ToString("yyyy-MM-dd"))+" 星期"+new ChangeObject().ChangeWeek(System.DateTime.Today.DayOfWeek);
            
            for (int rowi = 0; rowi < dt_100301_1.Rows.Count; rowi++)
            {
                rowcount++;
                this.Table_100301_1.Rows.Add(new System.Web.UI.WebControls.TableRow());
                this.Table_100301_1.Rows[rowcount].Cells.Add(new System.Web.UI.WebControls.TableCell());
                this.Table_100301_1.Rows[rowcount].Cells.Add(new System.Web.UI.WebControls.TableCell());
                this.Table_100301_1.Rows[rowcount].Cells[0].CssClass = "dot_a14-3";

                string today = Convert.ToDateTime(dt_100301_1.Rows[rowi]["c02_sdate"].ToString()).ToString("yyyy-MM-dd");
                string stime = Convert.ToDateTime(dt_100301_1.Rows[rowi]["c02_sdate"].ToString()).ToString("HH:mm");
                string etime = Convert.ToDateTime(dt_100301_1.Rows[rowi]["c02_edate"].ToString()).ToString("HH:mm");
                string peo_uid = sobj.sessionUserID;
                if (dt_100301_1.Rows[rowi]["c02_appointmen"].ToString().Equals("1") && dt_100301_1.Rows[rowi]["c02_check"].ToString().Equals("0"))
                    this.Table_100301_1.Rows[rowcount].Cells[1].Text = "<div class=\"row_2\"><a href=\"100512.aspx?height=400&width=650&TB_iframe=true&modal=true\" class=\"thickbox\">" + dt_100301_1.Rows[rowi]["peo_name"].ToString() + " " + stime + "~" + etime + " " + dt_100301_1.Rows[rowi]["c02_title"].ToString() + " " + "(預約)</a></div>";
                else
                    this.Table_100301_1.Rows[rowcount].Cells[1].Text = "<div class=\"row_2\"><a href=\"100512.aspx?height=400&width=650&TB_iframe=true&modal=true\" class=\"thickbox\">" + dt_100301_1.Rows[rowi]["peo_name"].ToString() + " " + stime + "~" + etime + " " + dt_100301_1.Rows[rowi]["c02_title"].ToString() + " " + "</a></div>";
            }
        }
    }

    public override string Name
    {
        get { return "PCalendar"; }
    }

    public override void loadWidget()
    {
    }
}