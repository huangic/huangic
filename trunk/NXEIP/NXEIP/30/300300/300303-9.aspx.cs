using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;
using NLog;

public partial class _30_300300_300303_9 : System.Web.UI.Page
{
    private NXEIPEntities model = new NXEIPEntities();
    private Logger logger = LogManager.GetCurrentClassLogger();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.Navigator1.SubFunc = "講義選擇";

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

                //課程所屬講義
                this.ODS_2.SelectParameters["e02_no"].DefaultValue = this.hidd_no.Value;

                //子類別選擇
                this.ods_level2.SelectParameters["s06_no"].DefaultValue = "0";

                string cat_no = Get_CatNO();
                if(!string.IsNullOrEmpty(cat_no)){
                    this.ddl_level_1.SelectedValue = cat_no;
                    this.ddl_level_1.Enabled = false;
                    this.ods_level2.SelectParameters["s06_no"].DefaultValue = cat_no;
                }

                

                OperatesObject.OperatesExecute(300303, new SessionObject().sessionUserID, 2, "查詢課程講義 e02_no:" + this.hidd_no.Value);
            }

            
        }

        if (Request["__EVENTTARGET"] == this.UpdatePanel1.ClientID && String.IsNullOrEmpty(Request["__EVENTARGUMENT"]))
        {
            this.GridView1.DataBind();
        }
    }

    public string Get_CatNO()
    {
        return new ArgumentsObject().Get_argValue("300303_CatNo");
    }

    protected void ddl_level_1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddl_level_1.SelectedValue != "0")
        {
            this.ods_level2.SelectParameters["s06_no"].DefaultValue = this.ddl_level_1.SelectedValue;
            this.ddl_level_2.Items.Clear();
            this.ddl_level_2.DataBind();
            this.ddl_level_2.Items.Insert(0, new ListItem("請選擇", "0"));

            if (this.ddl_level_2.Items.Count == 1 && int.Parse(this.ddl_level_1.SelectedValue) > 0)
            {
                this.ODS_1.SelectParameters["s06_no"].DefaultValue = this.ddl_level_1.SelectedValue;
                this.ODS_1.SelectParameters["peo_uid"].DefaultValue = new SessionObject().sessionUserID;
                this.GridView1.DataBind();
            }
        }
        else
        {
            this.ddl_level_2.Items.Clear();
            this.ddl_level_2.Items.Insert(0, new ListItem("請選擇", "0"));
        }

    }
    protected void ddl_level_2_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.ODS_1.SelectParameters["s06_no"].DefaultValue = this.ddl_level_2.SelectedValue;
        this.ODS_1.SelectParameters["peo_uid"].DefaultValue = new SessionObject().sessionUserID;
        this.GridView1.DataBind();
    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        int e02_no = int.Parse(this.hidd_no.Value);
        int e05no_max = 1;
        try
        {
            e05no_max = (from d in model.e05 where d.e02_no == e02_no select d.e05_no).Max() + 1;
        }
        catch { }

        for (int i = 0; i < this.GridView1.Rows.Count; i++)
        {
            if (((CheckBox)this.GridView1.Rows[i].FindControl("cbox")).Checked)
            {
                int dc09_no = int.Parse(this.GridView1.DataKeys[i].Values[0].ToString());
                int dc10_no = int.Parse(this.GridView1.DataKeys[i].Values[1].ToString());

                e05 data = new e05();
                data.e05_no = e05no_max++;
                data.e02_no = e02_no;
                data.e05_d09no = dc09_no;
                data.e05_d10no = dc10_no;
                model.AddToe05(data);
                model.SaveChanges();

                OperatesObject.OperatesExecute(300303, new SessionObject().sessionUserID, 1, string.Format("新增課程講義 e02_no:{0} e05_d09no:{1} e05_d10no:{2}", this.hidd_no.Value, dc09_no, dc10_no));
            }
        }

        this.GridView2.DataBind();

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

    private void ShowMsg(string msg)
    {
        string script = "<script>window.alert('" + msg + "');</script>";
        this.ClientScript.RegisterStartupScript(this.GetType(), "MyScript", script);
    }

    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            int e02_no = int.Parse(this.hidd_no.Value);
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            int e05_no = int.Parse(this.GridView2.DataKeys[rowIndex].Values[0].ToString());

            e05 data = (from d in model.e05
                        where d.e02_no == e02_no && d.e05_no == e05_no
                        select d).FirstOrDefault();
            model.e05.DeleteObject(data);
            model.SaveChanges();
            this.GridView2.DataBind();

            OperatesObject.OperatesExecute(300303, new SessionObject().sessionUserID, 4, string.Format("刪除課程講義 e02_no:{0} e05_no:{1}", this.hidd_no.Value, e05_no));
        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int dc09_no = int.Parse(this.GridView2.DataKeys[e.Row.RowIndex].Values[1].ToString());
            int dc10_no = int.Parse(this.GridView2.DataKeys[e.Row.RowIndex].Values[2].ToString());

            e.Row.Cells[0].Text = (from d in model.doc10 where d.d09_no == dc09_no && d.d10_no == dc10_no select d.d10_file).FirstOrDefault();
        }
    }
}