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

            this.ObjectDataSource2.SelectParameters["peo_uid"].DefaultValue = new SessionObject().sessionUserID;
            this.GridView2.DataBind();
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

            //刪除明細
            var med = dao.GetMedetail(mes_no);
            foreach (var p in med)
            {
                p.med_status = "2";
            }
            dao.Update();
            
            this.GridView1.DataBind();
            this.GridView2.DataBind();
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DateTime date = Convert.ToDateTime(e.Row.Cells[3].Text);
            e.Row.Cells[3].Text = new ChangeObject()._ADtoROCDT(date);

        }
    }

    protected void Button9_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < this.GridView2.Rows.Count; i++)
        {
            if (((CheckBox)(this.GridView2.Rows[i].FindControl("CheckBox1"))).Checked)
            {
                int mes_no = int.Parse(this.GridView2.DataKeys[i].Value.ToString());
                MessageDAO dao = new MessageDAO();
                
                medetail d = dao.GetDataByNo2(mes_no, int.Parse(new SessionObject().sessionUserID));
                d.med_status = "2";
                dao.Update();
                
                OperatesObject.OperatesExecute(100201, 4, string.Format("刪除個人訊息明細 mes_no:{0} peo_uid:{1}", d.mes_no, new SessionObject().sessionUserID));

            }
        }

        this.GridView2.DataBind();
    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            UtilityDAO dao = new UtilityDAO();

            int peo_uid = Convert.ToInt32(e.Row.Cells[1].Text);
            e.Row.Cells[1].Text = dao.Get_PeopleName(peo_uid);

            DateTime date = Convert.ToDateTime(e.Row.Cells[5].Text);
            e.Row.Cells[5].Text = new ChangeObject()._ADtoROCDT(date);

        }
    }

    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int mes_no = int.Parse(this.GridView2.DataKeys[int.Parse(e.CommandArgument.ToString())].Value.ToString());

        if (e.CommandName.Equals("del"))
        {
            MessageDAO dao = new MessageDAO();
            medetail d = dao.GetDataByNo2(mes_no,int.Parse(new SessionObject().sessionUserID));
            d.med_status = "2";
            dao.Update();

            OperatesObject.OperatesExecute(100201, 4, string.Format("刪除個人訊息明細 mes_no:{0} peo_uid:{1}", d.mes_no, new SessionObject().sessionUserID));

            this.GridView2.DataBind();
        }
    }
}