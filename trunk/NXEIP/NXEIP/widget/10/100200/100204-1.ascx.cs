using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;

public partial class widget_10_100200_100204_1 : NXEIP.Widget.WidgetBaseControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        New01DAO dao = new New01DAO();

        int count = 0;

        //單位
        count = 0;
        var ndata2 = dao.GetData("1", "-1");
        if (ndata2.Count() > 0)
        {
            foreach (var d in ndata2)
            {
                TableCell cell1 = new TableCell();
                string subject = d.n01_subject.Length > 40 ? d.n01_subject.Substring(0, 40) + "..." : d.n01_subject;
                cell1.Text = "<li class='row1'><a href='../../10/100200/100204.aspx'>" + subject + "</a></li>";

                TableCell cell2 = new TableCell();
                cell2.Text = new ChangeObject()._ADtoROC(d.n01_date.Value);
                cell2.CssClass = "row2";

                TableRow row = new TableRow();
                row.Cells.Add(cell1);
                row.Cells.Add(cell2);

                this.Table2.Rows.Add(row);

                count++;
                if (count == 5)
                {
                    break;
                }
            }

            TableCell cell_more2_1 = new TableCell();
            cell_more2_1.Text = "&nbsp;";

            TableCell cell_more2_2 = new TableCell();
            cell_more2_2.Text = "<li><a href=\"../../10/100200/100204.aspx\">更多訊息</a></li>";

            TableRow row_more2 = new TableRow();
            row_more2.Cells.Add(cell_more2_1);
            row_more2.Cells.Add(cell_more2_2);

            this.Table2.Rows.Add(row_more2);
        }
        else
        {
            TableCell cell1 = new TableCell();
            cell1.Text = "<li class='row1'><a href='#'>目前無訊息</a></li>";

            TableCell cell2 = new TableCell();
            cell2.Text = "&nbsp;";
            cell2.CssClass = "row2";

            TableRow row = new TableRow();
            row.Cells.Add(cell1);
            row.Cells.Add(cell2);

            this.Table2.Rows.Add(row);
        }
    }

    public override string Name
    {
        get { return "news"; }
    }

    public override void loadWidget()
    {


    }
}