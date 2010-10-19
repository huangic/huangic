using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity;
using NXEIP.DAO;

public partial class _30_300300_300303_4 : System.Web.UI.Page
{
    private NXEIPEntities model = new NXEIPEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.Navigator1.SubFunc = "報名審核";

            if (Request["e02_no"] != null)
            {
                this.hidd_no.Value = Request["e02_no"];

                this.ObjectDataSource1.SelectParameters["e02_no"].DefaultValue = this.hidd_no.Value;
                this.GridView1.DataBind();
            }
        }
    }

    protected void btn_pass_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < this.GridView1.Rows.Count; i++)
        {
            if (((CheckBox)(this.GridView1.Rows[i].FindControl("cbox"))).Checked)
            {
                int e04_no = System.Convert.ToInt32(this.GridView1.DataKeys[i].Value.ToString());
                this.Updata(e04_no, "1", "");
            }
        }

        this.GridView1.DataBind();
    }

    protected void LinkButton1_Click1(object sender, EventArgs e)
    {
        //未核可事由
        string reason = this.hidd_reason.Value;
        for (int i = 0; i < this.GridView1.Rows.Count; i++)
        {
            if (((CheckBox)(this.GridView1.Rows[i].FindControl("cbox"))).Checked)
            {
                int e04_no = Convert.ToInt32(this.GridView1.DataKeys[i].Value);
                this.Updata(e04_no, "2",reason);
            }
        }

        this.GridView1.DataBind();
    }

    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(this.GetUrl("300303.aspx"));
    }

    protected void btn_cancel2_Click(object sender, EventArgs e)
    {
        Response.Redirect(this.GetUrl("300303-3.aspx"));
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = System.Convert.ToInt32(e.CommandArgument);
        int e04_no = System.Convert.ToInt32(this.GridView1.DataKeys[rowIndex].Value.ToString());

        if (e.CommandName.Equals("del"))
        {
            //變更為未審核狀態
            this.Updata(e04_no, "0", "");
        }

        if (e.CommandName.Equals("del2"))
        {
            //取消報名
            this.Updata(e04_no, "4", "");
        }

        this.GridView1.DataBind();
    }

    private void Updata(int id,string status,string reason)
    {
        e04 e04Data = (from d in model.e04 where d.e04_no == id select d).FirstOrDefault();
        e04Data.e04_check = status;
        e04Data.e04_reason = reason;
        e04Data.e04_checkuid = Convert.ToInt32(new SessionObject().sessionUserID);
        e04Data.e04_checkdate = DateTime.Now;
        if (status.Equals("1"))
        {
            
        }
        else
        {
            //e04Data.e04_checkdate = ;
        }
        model.SaveChanges();
        
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

            ChangeObject cboj = new ChangeObject();
            e.Row.Cells[4].Text = cboj._ADtoROC(Convert.ToDateTime(e.Row.Cells[4].Text));
            try
            {
                e.Row.Cells[5].Text = cboj._ADtoROC(Convert.ToDateTime(e.Row.Cells[5].Text));
            }
            catch { }

            string check = e.Row.Cells[6].Text;
            //0 未審 1 合格 2 不合格 3 取消報名
            if (check.Equals("0"))
            {
                e.Row.Cells[6].Text = "未審核";
            }
            if (check.Equals("1"))
            {
                e.Row.Cells[6].Text = "審核通過";
            }
            if (check.Equals("2"))
            {
                e.Row.Cells[6].Text = "審核未通過";
            }
            if (check.Equals("3"))
            {
                e.Row.Cells[6].Text = "取消報名";
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

    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        int e02_no = Convert.ToInt32(this.hidd_no.Value);
        var e02data = (from d in model.e02 where d.e02_no == e02_no select d).FirstOrDefault();
        int count = (from dd in model.e04 where dd.e04_check=="1" && dd.e02_no == e02_no select dd).Count();
        this.lab_titile.Text = "以下為報名『" + e02data.e02_name + "第" + e02data.e02_flag + "期』的成員列表(目前此班報名" + this.GridView1.Rows.Count + "人，已核准" + count + "人)";
    }
}