using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;

public partial class _30_300300_300303_3 : System.Web.UI.Page
{
    private NXEIPEntities model = new NXEIPEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.Navigator1.SubFunc = "課程檢視";

            if (Request["e02_no"] != null)
            {
                this.hidd_no.Value = Request["e02_no"];
                e02 d = new e02DAO().GetBye02NO(Convert.ToInt32(this.hidd_no.Value));
                this.lab_mechani.Text = d.e02_mechani;
                this.lab_code.Text = d.e02_code;
                this.lab_typ_name.Text = (from t in model.types where t.typ_no == d.typ_no select t.typ_cname).FirstOrDefault();
                this.lab_name_flag.Text = d.e02_name + "(第" + d.e02_flag + "期)";
                this.lab_memo.Text = d.e02_memo;
                this.lab_limit.Text = d.e02_limit;
                this.lab_e01_name.Text = (from tt in model.e01 where tt.e01_no == d.e01_no select tt.e01_name).FirstOrDefault();
                this.lab_teacher.Text = d.e02_teacher;
                if (d.e02_hour.HasValue)
                {
                    this.lab_hour.Text = d.e02_hour.ToString();
                }
                if (d.e02_people.HasValue)
                {
                    this.lab_people.Text = d.e02_people.ToString();
                }
                if (d.e02_check.Equals("1"))
                {
                    this.lab_check.Text = "審核";
                }
                else
                {
                    this.lab_check.Text = "不審核";
                }

                ChangeObject cboj = new ChangeObject();
                this.lab_opendate.Text = cboj._ROCtoROCYMD(cboj._ADtoROC(Convert.ToDateTime(d.e02_opendate.ToString())));
                this.lab_signdate.Text = cboj._ROCtoROCYMD(cboj._ADtoROC(Convert.ToDateTime(d.e02_signdate.ToString()))) + "至" + cboj._ROCtoROCYMD(cboj._ADtoROC(Convert.ToDateTime(d.e02_signedate.ToString())));
                this.lab_date.Text = cboj._ROCtoROCYMD(cboj._ADtoROC(Convert.ToDateTime(d.e02_sdate.ToString()))) + "至" + cboj._ROCtoROCYMD(cboj._ADtoROC(Convert.ToDateTime(d.e02_edate.ToString())));
            }
        }
    }
    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(this.GetUrl("300303.aspx"));
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string arg = ((Button)sender).CommandArgument;

        //報名審核
        if (arg.Equals("1"))
        {
            Response.Redirect(this.GetUrl("300303-4.aspx"));
        }

        //簽到表
        if (arg.Equals("2"))
        {
            Response.Redirect(this.GetUrl("300303-7.aspx"));
        }

        //線上點名
        if (arg.Equals("3"))
        {
            Response.Redirect(this.GetUrl("300303-6.aspx"));
        }

        //成績輸入
        if (arg.Equals("4"))
        {
            Response.Redirect(this.GetUrl("300303-5.aspx"));
        }

        //檔案下載
        if (arg.Equals("5"))
        {

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
}