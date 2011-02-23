using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using System.Data;
using NXEIP.Widget;

public partial class widget_10_100300_100301 : WidgetBaseControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DBObject dbo = new DBObject();
        SessionObject sobj = new SessionObject();
        ChangeObject changeobj = new ChangeObject();
        string sqlstr_100301 = "SELECT top 5 c02_sdate, c02_title,c02_appointmen,c02_check FROM c02 where  (c02_check<>'2')  and (peo_uid = " + sobj.sessionUserID + ")"
                + " and (c02_edate >= '" + System.DateTime.Today.ToString("yyyy/MM/dd") + " 00:00:00') ORDER BY c02_sdate, c02_edate, c02_no";
        DataTable dt_100301 = new DataTable();
        dt_100301 = dbo.ExecuteQuery(sqlstr_100301);
        if (dt_100301.Rows.Count > 0)
        {
            int rowcount = -1;
            for (int rowi = 0; rowi < dt_100301.Rows.Count; rowi++)
            {
                rowcount++;
                this.Table_100301.Rows.Add(new System.Web.UI.WebControls.TableRow());
                this.Table_100301.Rows[rowcount].Cells.Add(new System.Web.UI.WebControls.TableCell());
                this.Table_100301.Rows[rowcount].Cells.Add(new System.Web.UI.WebControls.TableCell());
                this.Table_100301.Rows[rowcount].Cells[0].CssClass = "dot_a14-3";

                string today=Convert.ToDateTime(dt_100301.Rows[rowi]["c02_sdate"].ToString()).ToString("yyyy-MM-dd");
                string peo_uid=sobj.sessionUserID;
                if (dt_100301.Rows[rowi]["c02_appointmen"].ToString().Equals("1") && dt_100301.Rows[rowi]["c02_check"].ToString().Equals("0"))
                    this.Table_100301.Rows[rowcount].Cells[1].Text = "<div class=\"row_2\"><a href=\"../../10/100300/100301.aspx?today=" + changeobj.ADDTtoROCDT(today) + "&peo_uid=" + peo_uid + "\">" + dt_100301.Rows[rowi]["c02_title"].ToString() + " " + today.Replace("-", "/") + "(預約)</a></div>";
                else
                    this.Table_100301.Rows[rowcount].Cells[1].Text = "<div class=\"row_2\"><a href=\"../../10/100300/100301.aspx?today=" + changeobj.ADDTtoROCDT(today) + "&peo_uid=" + peo_uid + "\">" + dt_100301.Rows[rowi]["c02_title"].ToString() + " " + today.Replace("-", "/") + "</a></div>";
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