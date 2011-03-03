using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity;
using NXEIP.DAO;

public partial class _30_300300_300303_5 : System.Web.UI.Page
{
    private NXEIPEntities model = new NXEIPEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.Navigator1.SubFunc = "成績輸入";

            if (Request["e02_no"] != null)
            {
                this.hidd_no.Value = Request["e02_no"];

                this.ObjectDataSource1.SelectParameters["e02_no"].DefaultValue = this.hidd_no.Value;
                this.GridView1.DataBind();

                int e02_no = Convert.ToInt32(this.hidd_no.Value);
                var e02data = (from d in model.e02 where d.e02_no == e02_no select d).FirstOrDefault();
                int count = (from dd in model.e04 where dd.e04_sign == "1" && dd.e02_no == e02_no select dd).Count();
                this.lab_titile.Text = "以下為報名『" + e02data.e02_name + "第" + e02data.e02_flag + "期』的成員列表(目前此班報名" + this.GridView1.Rows.Count + "人，出席" + count + "人)";

                OperatesObject.OperatesExecute(300303, new SessionObject().sessionUserID, 2, "查詢成績輸入 e02_no:" + this.hidd_no.Value);
            }
        }
    }

    protected void btn_pass_Click(object sender, EventArgs e)
    {
        bool check = true;
        for (int i = 0; i < this.GridView1.Rows.Count; i++)
        {
            try
            {
                int tmp = Convert.ToInt32(((TextBox)(this.GridView1.Rows[i].FindControl("tbox"))).Text);
                if (tmp < 0)
                {
                    check = false;
                    this.ShowMsg("成績請輸入大於等於0之數值!");
                    break;
                }
            }
            catch
            {
                check = false;
                this.ShowMsg("成績請輸入數值!");
                break;
            }
        }

        if (check)
        {
            SessionObject sobj = new SessionObject();
            for (int i = 0; i < this.GridView1.Rows.Count; i++)
            {
                int tmp = Convert.ToInt32(((TextBox)(this.GridView1.Rows[i].FindControl("tbox"))).Text);
                int e04_no = Convert.ToInt32(this.GridView1.DataKeys[i].Values[0]);

                e04 _e = (from d in model.e04 where d.e04_no == e04_no select d).FirstOrDefault();
                _e.e04_resultuid = Convert.ToInt32(sobj.sessionUserID);
                _e.e04_resultdate = DateTime.Now;
                _e.e04_result = tmp;
            }
            model.SaveChanges();
            new OperatesObject().ExecuteOperates(300303, sobj.sessionUserID, 3, "更新課程成績資料 e02_no:"+this.hidd_no.Value);
            this.ShowMsg("成績儲存完成!!");
        }
    }

    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(this.GetUrl("300303.aspx"));
    }
    protected void btn_cancel2_Click(object sender, EventArgs e)
    {
        Response.Redirect(this.GetUrl("300303-3.aspx"));
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            UtilityDAO udao = new UtilityDAO();

            int tmp = Convert.ToInt32(e.Row.Cells[0].Text);
            e.Row.Cells[0].Text = udao.Get_DepartmentName(tmp);

            tmp = Convert.ToInt32(e.Row.Cells[1].Text);
            e.Row.Cells[1].Text = udao.Get_TypesCName(tmp);

            tmp = Convert.ToInt32(e.Row.Cells[2].Text);
            e.Row.Cells[2].Text = udao.Get_PeopleName(tmp);

            tmp = Convert.ToInt32(e.Row.Cells[3].Text);
            e.Row.Cells[3].Text = udao.Get_PeopleIDCard(tmp);

            string sign = e.Row.Cells[4].Text;
            //1 出席 2 未出席
            if (sign.Equals("1"))
            {
                e.Row.Cells[4].Text = "是";
            }
            if (sign.Equals("2"))
            {
                e.Row.Cells[4].Text = "否";
            }

            //成績
            try
            {
                ((TextBox)(e.Row.FindControl("tbox"))).Text = this.GridView1.DataKeys[e.Row.RowIndex].Values[1].ToString();
            }
            catch { }
        }

        
    }

    private string GetUrl(string tag)
    {
        string url = tag;
        url += "?sdate=" + Request["sdate"];
        url += "&edate=" + Request["edate"];
        url += "&type_1=" + Request["type_1"];
        url += "&type_2=" + Request["type_2"];
        url += "&e02_place=" + Request["e02_place"];
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
}