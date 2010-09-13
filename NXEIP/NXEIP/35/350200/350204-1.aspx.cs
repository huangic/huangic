using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.HPSF;
using NPOI.POIFS.FileSystem;

public partial class _35_350200_350204_1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.Navigator1.SubFunc = "人員列表";
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
            this.lab_sql.Text = this.SqlDataSource1.SelectCommand;
            this.GridView1.DataBind();

            if (Request["pageIndex"] != null)
            {
                this.GridView1.PageIndex = Convert.ToInt32(Request["pageIndex"]);
            }

        }
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
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = System.Convert.ToInt32(e.CommandArgument);

        if (e.CommandName.Equals("modify"))
        {
            string peo_uid = this.GridView1.DataKeys[rowIndex].Value.ToString();
            string pageIndex = this.GridView1.PageIndex.ToString();
            Response.Redirect("350204-2.aspx?peo_uid=" + peo_uid + "&jobtype=" + Request["jobtype"] + "&ptype=" + Request["ptype"] + "&workid=" + Request["workid"] + "&name=" + Request["name"] + "&account=" + Request["account"] + "&profess=" + Request["profess"] + "&depar=" + Request["depar"] + "&people=" + Request["people"] + "&pageIndex" + pageIndex);
        }

        //檢視
        if (e.CommandName.Equals("peruse"))
        {
            string peo_uid = this.GridView1.DataKeys[rowIndex].Value.ToString();
            string pageIndex = this.GridView1.PageIndex.ToString();
            //Response.Redirect("350204-2.aspx?peo_uid=" + peo_uid + "&jobtype=" + Request["jobtype"] + "&ptype=" + Request["ptype"] + "&workid=" + Request["workid"] + "&name=" + Request["name"] + "&account=" + Request["account"] + "&profess=" + Request["profess"] + "&depar=" + Request["depar"] + "&people=" + Request["people"] + "&pageIndex" + pageIndex);
        }
    }

    /// <summary>
    /// 匯Excel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();

        for (int i = 0; i < this.GridView1.Columns.Count - 2; i++)
        {
            dt.Columns.Add(this.GridView1.Columns[i].HeaderText);
        }

        for (int i = 0; i < this.GridView1.Rows.Count; i++)
        {
            DataRow row = dt.NewRow();
            for (int j = 0; j < this.GridView1.Columns.Count - 2; j++)
            {

                if (this.GridView1.Rows[i].Cells[j].Text.Equals("&nbsp;"))
                {
                    row[this.GridView1.Columns[j].HeaderText] = "";
                }
                else
                {
                    row[this.GridView1.Columns[j].HeaderText] = this.GridView1.Rows[i].Cells[j].Text;
                }
            }
            dt.Rows.Add(row);
        }

        string filename = System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm") + ".xls";

        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
        Response.Clear();

        Response.BinaryWrite(new ExcelObject().ExportExcel(dt).GetBuffer());
        Response.End();

        //操作記錄
        new OperatesObject().ExecuteOperates(350204, new SessionObject().sessionUserID, 2, "匯出人員Excel");
    }

    protected void GridView1_PageIndexChanged(object sender, EventArgs e)
    {
        this.SqlDataSource1.SelectCommand = this.lab_sql.Text;
        this.GridView1.DataBind();
    }
}