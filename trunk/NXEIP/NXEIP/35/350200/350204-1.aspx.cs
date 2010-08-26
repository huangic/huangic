using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _35_350200_350204_1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.Panel1.Visible = true;
            this.Panel2.Visible = false;

            //Response.Redirect("350204-1.aspx?jobtype=" + jobtype + "&ptype=" + ptype + "&workid=" + workid + "&name=" + name + "&account=" + account + "&profess=" + profess + "&depar=" + depar + "&people=" + people);
            string sql = "";

            if (Request["jobtype"].Equals(""))
            {
                //在職人員
                string pty_no = new DBObject().ExecuteScalar("select typ_no from types where typ_code = 'work' and typ_number = '1' and typ_status='1'");
                sql = " where people.peo_jobtype = '"+pty_no+"'";
            }
            else
            {
                sql = " where people.peo_jobtype = '" + Request["jobtype"] + "'";
            }

            if (!Request["ptype"].Equals(""))
            {
                sql += " and people.peo_ptype = '" + Request["ptype"] + "'";
            }

            if (!Request["workid"].Equals(""))
            {
                sql += " and people.peo_workid = '" + Request["workid"] + "'";
            }

            if (!Request["name"].Equals(""))
            {
                sql += " and people.peo_name like N '%" + Request["name"] + "%'";
            }

            if (!Request["account"].Equals(""))
            {
                sql += " and people.peo_account = '" + Request["account"] + "'";
            }

            if (!Request["profess"].Equals(""))
            {
                sql += " and people.peo_pfofess = " + Request["profess"];
            }

            if (!Request["depar"].Equals(""))
            {
                sql += " and people.dep_no in (" + Request["depar"] + ")";
            }

            if (!Request["people"].Equals(""))
            {
                sql += " and people.peo_uid in (" + Request["people"] + ")";
            }

            string sql_order = " order by departments.dep_order";

            this.SqlDataSource1.SelectCommand += sql + sql_order;

        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string pro_no = e.Row.Cells[1].Text;
            string pty_no = e.Row.Cells[3].Text;

            DBObject dbo = new DBObject();
            string sql = "";

            sql = "select typ_cname from types where typ_no = " + pro_no;
            e.Row.Cells[1].Text = dbo.ExecuteScalar(sql);

            sql = "select typ_cname from types where typ_no = " + pty_no;
            e.Row.Cells[3].Text = dbo.ExecuteScalar(sql);

        }
    }
}