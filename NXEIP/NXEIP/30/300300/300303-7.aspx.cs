using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity;
using NXEIP.DAO;

public partial class _30_300300_300303_7 : System.Web.UI.Page
{
    private NXEIPEntities model = new NXEIPEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.Navigator1.SubFunc = "簽到表";

            if (Request["e02_no"] != null)
            {
                this.hidd_no.Value = Request["e02_no"];

                int e02_no = Convert.ToInt32(this.hidd_no.Value);
                var e02data = (from d in model.e02 where d.e02_no == e02_no select d).FirstOrDefault();
                int count = (from dd in model.e04 where dd.e02_no == e02_no && dd.e04_check == "1" select dd).Count();
                this.lab_titile.Text = "以下為報名『" + e02data.e02_name + "第" + e02data.e02_flag + "期』已核可之" + count + "位成員列表";
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //列印
        //Response.Write("<script>newwindow=window.open('HUM0206-p27.aspx?e02_no=&arg=&Count="+new System.Random().Next(1000)+"','new_eip','menubar=yes,scrollbars=yes,resizable=yes, width=700,height=400')</script>");
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        //下載

    }
    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(this.GetUrl("300303.aspx"));
    }
    protected void btn_cancel2_Click(object sender, EventArgs e)
    {
        Response.Redirect(this.GetUrl("300303-3.aspx"));
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
}