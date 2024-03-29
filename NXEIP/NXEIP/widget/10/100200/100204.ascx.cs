﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;

public partial class widget_10_100200_100204 : NXEIP.Widget.WidgetBaseControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        New01DAO dao = new New01DAO();

        //往前取幾天
        int days = 30;
        try
        {
            days = int.Parse(new ArgumentsObject().Get_argValue("100204_A_ShowDays"));
        }
        catch { }

        int count = 0;

        //全府
        string todays = DateTime.Now.AddDays(days * -1).ToString("yyyy-MM-dd");
        var ndata1 = dao.Get_DataForWidget("2", todays);

        if (ndata1.Count() > 0)
        {
            foreach (var d in ndata1)
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

                this.Table1.Rows.Add(row);

                count++;
                if (count == 5)
                {
                    break;
                }
            }

            TableCell cell_more1_1 = new TableCell();
            cell_more1_1.Text = "&nbsp;";

            TableCell cell_more1_2 = new TableCell();
            cell_more1_2.Text = "<li><a href=\"../../10/100200/100204.aspx\">更多訊息</a></li>";

            TableRow row_more1 = new TableRow();
            row_more1.Cells.Add(cell_more1_1);
            row_more1.Cells.Add(cell_more1_2);

            this.Table1.Rows.Add(row_more1);
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

            this.Table1.Rows.Add(row);
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