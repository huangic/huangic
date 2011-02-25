using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;
using NLog;

public partial class _10_100200_100201 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.ObjectDataSource1.SelectParameters["peo_uid"].DefaultValue = new SessionObject().sessionUserID;

            this.GridView1.DataBind();
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("100201-1.aspx");
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int mes_no = int.Parse(this.GridView1.DataKeys[int.Parse(e.CommandArgument.ToString())].Value.ToString());

        if (e.CommandName.Equals("del"))
        {
            MessageDAO dao = new MessageDAO();
            message d = dao.GetDataByNo(mes_no);
            d.mes_status = "2";
            dao.Update();

            OperatesObject.OperatesExecute(100201, 4, "刪除個人訊息 mes_no:" + d.mes_no);

            this.GridView1.DataBind();
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            UtilityDAO dao = new UtilityDAO();

            int peo_uid = Convert.ToInt32(e.Row.Cells[1].Text);
            e.Row.Cells[1].Text = dao.Get_PeopleName(peo_uid);

            DateTime date = Convert.ToDateTime(e.Row.Cells[4].Text);
            e.Row.Cells[4].Text = new ChangeObject()._ADtoROC(date) + " " + date.ToString("HH:mm");

        }
    }

    protected void Button9_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < this.GridView1.Rows.Count; i++)
        {
            if (((CheckBox)(this.GridView1.Rows[i].FindControl("CheckBox1"))).Checked)
            {
                int mes_no = int.Parse(this.GridView1.DataKeys[i].Value.ToString());
                MessageDAO dao = new MessageDAO();
                message d = dao.GetDataByNo(mes_no);
                d.mes_status = "2";
                dao.Update();
                OperatesObject.OperatesExecute(100201, 4, "刪除個人訊息 mes_no:" + d.mes_no);
            }
        }

        this.GridView1.DataBind();
    }
}