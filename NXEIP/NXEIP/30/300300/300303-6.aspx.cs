using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity;
using NXEIP.DAO;

public partial class _30_300300_300303_6 : System.Web.UI.Page
{
    private NXEIPEntities model = new NXEIPEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.Navigator1.SubFunc = "線上點名";

            if (Request["e02_no"] != null)
            {
                this.hidd_no.Value = Request["e02_no"];

                this.ObjectDataSource1.SelectParameters["e02_no"].DefaultValue = this.hidd_no.Value;
                this.GridView1.DataBind();

                int e02_no = Convert.ToInt32(this.hidd_no.Value);
                var e02data = (from d in model.e02 where d.e02_no == e02_no select d).FirstOrDefault();
                this.lab_titile.Text = "以下為報名『" + e02data.e02_name + "第" + e02data.e02_flag + "期』已核可之" + this.GridView1.Rows.Count + "位成員列表";
            }
        }
    }

    protected void btn_sign_Click(object sender, EventArgs e)
    {
        // 1:出席 2:未出席
        string arg = ((Button)(sender)).CommandArgument;
        SessionObject sobj = new SessionObject();

        bool check = false;
        for (int i = 0; i < this.GridView1.Rows.Count; i++)
        {
            if (((CheckBox)(this.GridView1.Rows[i].FindControl("cbox"))).Checked)
            {
                check = true;
                int e04_no = Convert.ToInt32(this.GridView1.DataKeys[i].Value);

                e04 _e = (from d in model.e04 where d.e04_no == e04_no select d).FirstOrDefault();
                _e.e04_signuid = Convert.ToInt32(sobj.sessionUserID);
                _e.e04_singdate = DateTime.Now;
                _e.e04_sign = arg;
            }
        }
        if (check)
        {
            model.SaveChanges();
            this.GridView1.DataBind();
            OperatesObject.OperatesExecute(300303, new SessionObject().sessionUserID, 3, "更新點名資料 e02_no:" + this.hidd_no.Value);
            this.ShowMsg("點名完成!");

            
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string arg = ((Button)(sender)).CommandArgument;
        bool check = true;
        if (arg.Equals("2"))
        {
            check = false;
        }
        for (int i = 0; i < this.GridView1.Rows.Count; i++)
        {
            ((CheckBox)(this.GridView1.Rows[i].FindControl("cbox"))).Checked = check;
        }
    }
    
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            UtilityDAO udao = new UtilityDAO();

            int tmp = Convert.ToInt32(e.Row.Cells[1].Text);
            e.Row.Cells[1].Text = udao.Get_DepartmentName(tmp);

            tmp = Convert.ToInt32(e.Row.Cells[2].Text);
            e.Row.Cells[2].Text = udao.Get_TypesCName(tmp);

            tmp = Convert.ToInt32(e.Row.Cells[3].Text);
            e.Row.Cells[3].Text = udao.Get_PeopleName(tmp);

            tmp = Convert.ToInt32(e.Row.Cells[4].Text);
            e.Row.Cells[4].Text = udao.Get_PeopleIDCard(tmp);

            string sign = e.Row.Cells[5].Text;
            //1 出席 2 未出席
            if (sign.Equals("1"))
            {
                e.Row.Cells[5].Text = "是";
            }
            if (sign.Equals("2"))
            {
                e.Row.Cells[5].Text = "否";
            }
        }
        
    }

    private string GetUrl(string tag)
    {
        string url = tag;
        url += "?sdate=" + Request["sdate"];
        url += "&edate=" + Request["edate"];
        url += "&type_1=" + Request["type_1"];
        url += "&type_2=" + Request["type_2"];
        url += "&e01_no=" + Request["e01_no"];
        url += "&e02_name=" + Request["e02_name"];
        url += "&e02_no=" + Request["e02_no"];
        url += "&model=" + Request["model"];
        url += "&pageIndex=" + Request["pageIndex"];
        return url;

    }
    private void ShowMsg(string msg)
    {
        string script = "<script>window.alert('" + msg + "');</script>";
        this.ClientScript.RegisterStartupScript(this.GetType(), "MyScript", script);
    }
    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(this.GetUrl("300303.aspx"));
    }
    protected void btn_cancel2_Click(object sender, EventArgs e)
    {
        Response.Redirect(this.GetUrl("300303-3.aspx"));
    }
}