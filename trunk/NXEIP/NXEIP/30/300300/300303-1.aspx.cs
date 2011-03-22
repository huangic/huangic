using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;

public partial class _30_300300_300303_1 : System.Web.UI.Page
{
    private NXEIPEntities model = new NXEIPEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.Navigator1.SubFunc = "部門限制";
            this.DepartTreeListBox1.Clear();

            if (Request["e02_no"] != null)
            {
                this.hidd_no.Value = Request["e02_no"];

                e02DAO dao = new e02DAO();
                e02 data = dao.GetBye02NO(Convert.ToInt32(this.hidd_no.Value));

                this.lab_code.Text = data.e02_code;
                this.lab_mechani.Text = data.e02_mechani;
                this.lab_name_flag.Text = data.e02_name + "(第" + data.e02_flag.ToString() + "期)";
                var typ_name = (from t in model.types where t.typ_no == data.typ_no select t.typ_cname).FirstOrDefault();
                this.lab_typ_name.Text = typ_name;

                this.ObjectDataSource1.SelectParameters["e02_no"].DefaultValue = this.hidd_no.Value;

                
            }
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        if (this.DepartTreeListBox1.Items.Count == 0)
        {
            JsUtil.AlertJs(this, "請選擇部門!");
            return;
        }
        if (this.tbox_people.Text.Length > 0)
        {
            try
            {
                int tmp = int.Parse(this.tbox_people.Text);
                if (tmp <= 0)
                {
                    JsUtil.AlertJs(this, "限制人數需大於0!");
                    return;
                }
            }
            catch
            {
                JsUtil.AlertJs(this, "限制人數請輸入數字!");
                return;
            }
        }
        else
        {
            JsUtil.AlertJs(this, "請輸入限制人數!");
            return;
        }

        e03DAO dao = new e03DAO();

        int e02no = int.Parse(this.hidd_no.Value);
        int people = int.Parse(this.tbox_people.Text);

        foreach (var d in this.DepartTreeListBox1.Items)
        {
            int dep_no = int.Parse(d.Key);

            //是否存在
            e03 data = dao.Get_e03_2(e02no, dep_no);
            if (data != null)
            {
                data.e03_people = people;
                dao.Update();
            }
            else
            {
                data = new e03();
                data.e03_depno = dep_no;
                data.e03_people = people;
                data.e02_no = e02no;
                data.e03_no = dao.Max_e03No(e02no) + 1;
                dao.addToe03(data);
                dao.Update();
            }
        }

        OperatesObject.OperatesExecute(300303, 3, "更新部門限制 e02_no:" + e02no);

        this.GridView1.DataBind();

    }

    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(this.GetUrl());
    }

    private string GetUrl()
    {
        string url = "300303.aspx";
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

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = int.Parse(e.CommandArgument.ToString());
        int e03_no = int.Parse(this.GridView1.DataKeys[rowIndex].Value.ToString());

        if (e.CommandName.Equals("del"))
        {
            e03DAO dao = new e03DAO();
            e03 d = dao.Get_e03(int.Parse(this.hidd_no.Value), e03_no);
            dao.delete(d);
            dao.Update();

            OperatesObject.OperatesExecute(300303, 3, "更新部門限制 e02_no:" + int.Parse(this.hidd_no.Value));

            this.GridView1.DataBind();
        }
    }
}