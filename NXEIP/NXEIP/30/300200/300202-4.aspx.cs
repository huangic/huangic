using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using NXEIP.DAO;
using Entity;

public partial class _30_300200_300202_4 : System.Web.UI.Page
{
    ChangeObject changeobj = new ChangeObject();
    DBObject dbo = new DBObject();
    SessionObject sobj = new SessionObject();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if (Request["no"] != null) this.lab_no.Text = Request["no"];
            this.ObjectDataSource1.SelectParameters["que_no"].DefaultValue=this.lab_no.Text;
            this.ObjectDataSource1.SelectParameters["jobtype"].DefaultValue = new TypesDAO().GetNoByCodeNumber("work", "1").ToString();
            this.GridView1.DataBind();
            #region 問卷基本資料
            questionary que = new QuestionaryDAO().GetByNo(Convert.ToInt32(this.lab_no.Text));
            if (que != null)
            {
                this.lab_name.Text = que.que_name;
                this.lab_descript.Text = que.que_descript;
            }
            #endregion
        }
    }

    #region 回上一頁
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        Response.Redirect("300202.aspx?count=" + new System.Random().Next(10000).ToString());
    }
    #endregion
}