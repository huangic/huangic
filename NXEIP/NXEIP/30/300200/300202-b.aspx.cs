using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using NXEIP.DAO;
using Entity;

public partial class _30_300200_300202_b : System.Web.UI.Page
{
    DBObject dbo = new DBObject();
    CheckObject checkobj = new CheckObject();
    SessionObject sobj = new SessionObject();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if (Request["que_no"] != null) this.lab_queno.Text = Request["que_no"];
            if (Request["the_no"] != null) this.lab_theno.Text = Request["the_no"];
            if (Request["ans_no"] != null) this.lab_ansno.Text = Request["ans_no"];

            this.Navigator1.SubFunc = "清單";
            string the_type = "";
            #region 問卷基本資料
            Entity.answers ansData = new AnswersDAO().GetByNo(Convert.ToInt32(this.lab_queno.Text), Convert.ToInt32(this.lab_theno.Text), Convert.ToInt32(this.lab_ansno.Text));
            if (ansData != null)
            {
                this.lab_ansname.Text = ansData.ans_name;
                this.lab_thename.Text = ansData.theme.the_name;
                this.lab_quename.Text = ansData.theme.questionary.que_name;
                the_type = ansData.theme.the_type;
            }
            #endregion

            #region 投票者
            string sqlstr = "";
            if(the_type.Equals("1")) //單選
                sqlstr = "SELECT departments.dep_name, types.typ_cname, people.peo_name, botanize.bot_date FROM casework INNER JOIN botanize ON casework.bot_no = botanize.bot_no INNER JOIN people ON botanize.peo_uid = people.peo_uid INNER JOIN departments ON people.dep_no = departments.dep_no INNER JOIN types ON people.peo_pfofess = types.typ_no WHERE (botanize.bot_status = '1') AND (1 = @model) and (que_no = "+this.lab_queno.Text+") AND (the_no = "+this.lab_theno.Text+") and (cas_answer='" + this.lab_ansno.Text + "') ORDER BY departments.dep_order, types.typ_order, people.peo_name";
            else
                sqlstr = "SELECT departments.dep_name, types.typ_cname, people.peo_name, botanize.bot_date FROM casework INNER JOIN botanize ON casework.bot_no = botanize.bot_no INNER JOIN people ON botanize.peo_uid = people.peo_uid INNER JOIN departments ON people.dep_no = departments.dep_no INNER JOIN types ON people.peo_pfofess = types.typ_no WHERE (botanize.bot_status = '1') AND (1 = @model) and (que_no = " + this.lab_queno.Text + ") AND (the_no = " + this.lab_theno.Text + ") and (cas_answer like '" + this.lab_ansno.Text + ",%' or cas_answer like '%," + this.lab_ansno.Text + "' or cas_answer like '%," + this.lab_ansno.Text + ",%' or cas_answer='" + this.lab_ansno.Text + "') ORDER BY departments.dep_order, types.typ_order, people.peo_name";
            this.SqlDataSource1.SelectCommand = sqlstr;
            this.SqlDataSource1.SelectParameters["model"].DefaultValue = "1";
            this.GridView1.DataBind();
            #endregion
        }
    }
    #region 調整輸出格式
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[3].Text = new ChangeObject().ADDTtoROCDT(e.Row.Cells[3].Text);
        }
    }
    #endregion
}