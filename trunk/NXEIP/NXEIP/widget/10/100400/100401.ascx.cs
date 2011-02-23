using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using System.Data;
using NXEIP.Widget;

public partial class widget_10_100400_100401 : WidgetBaseControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DBObject dbo = new DBObject();
        SessionObject sobj = new SessionObject();
        ChangeObject changeobj = new ChangeObject();
        string sqlstr_100401 = "select top 5 que_name from questionary where (que_status = '1') and (que_sdate <= '" + System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "') and (que_edate >= '" + System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "') order by que_sdate";
        DataTable dt_100401 = new DataTable();
        dt_100401 = dbo.ExecuteQuery(sqlstr_100401);
        if (dt_100401.Rows.Count > 0)
        {
            int rowcount = -1;
            for (int rowi = 0; rowi < dt_100401.Rows.Count; rowi++)
            {
                rowcount++;
                this.Literal1.Text += "<li class=\"dot_a51\"><a href=\"../../10/100400/100401.aspx\">" + dt_100401.Rows[rowi]["que_name"].ToString() + "</a></li><div class=\"border-bottom-block2\"></div>";
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