using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;

public partial class _30_300300_300303 : System.Web.UI.Page
{
    private NXEIPEntities model = new NXEIPEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.ddl_e01.DataBind();
            this.ddl_e01.Items.Insert(0, new ListItem("請選擇", "0"));
            this.ddl_e01.Items[0].Selected = true;

            this.ddl_type_1.DataBind();
            this.ddl_type_1.Items.Insert(0, new ListItem("請選擇", "0"));
            this.ddl_type_1.Items[0].Selected = true;

            this.ddl_type_2.DataBind();
            this.ddl_type_2.Items.Clear();
            this.ddl_type_2.Items.Insert(0, new ListItem("請選擇", "0"));
            this.ddl_type_2.Items[0].Selected = true;

            this.calendar1._ADDate = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-01-01"));
            this.calendar2._ADDate = System.DateTime.Now;

            if (Request["sdate"] != null && Request["edate"] != null)
            {
                this.LoadData(Request["sdate"], Request["edate"], Request["type_1"], Request["type_2"], Request["e01_no"], Request["e02_name"], new SessionObject().sessionUserID, Request["pageIndex"]);
            }
            else
            {
                this.LoadData(this.calendar1._ADDate.ToString("yyyy-MM-dd"), this.calendar2._ADDate.ToString("yyyy-MM-dd"), this.ddl_type_1.SelectedValue, this.ddl_type_2.SelectedValue, this.ddl_e01.SelectedValue, this.tbox_name.Text, new SessionObject().sessionUserID,"0");
            }
        }
    }

    protected void btn_ok_Click(object sender, EventArgs e)
    {
        if (this.CheckUI())
        {
            this.LoadData(this.calendar1._ADDate.ToString("yyyy-MM-dd"), this.calendar2._ADDate.ToString("yyyy-MM-dd"),this.ddl_type_1.SelectedValue,this.ddl_type_2.SelectedValue,this.ddl_e01.SelectedValue,this.tbox_name.Text,new SessionObject().sessionUserID,"0");
        }
    }

    private bool CheckUI()
    {
        bool check = true;

        try
        {
            if (this.calendar1._ADDate > this.calendar2._ADDate)
            {
                this.ShowMsg("起始日期需小於結束日期!");
                check = false;
            }
        }
        catch
        {
            this.ShowMsg("請輸入正確日期!");
            check = false;
        }

        return check;
    }

    /// <summary>
    /// 撈資料
    /// </summary>
    private void LoadData(string sdate, string edate, string type_1, string type_2, string e01_no, string e02_name, string openuid,string pageIndex)
    {
        this.ODS_1.SelectParameters["sdate"].DefaultValue = sdate;
        this.ODS_1.SelectParameters["edate"].DefaultValue = edate;
        this.ODS_1.SelectParameters["type_1"].DefaultValue = type_1;
        this.ODS_1.SelectParameters["type_2"].DefaultValue = type_2;
        this.ODS_1.SelectParameters["e01_no"].DefaultValue = e01_no;
        this.ODS_1.SelectParameters["e02_name"].DefaultValue = e02_name;
        this.ODS_1.SelectParameters["openuid"].DefaultValue = openuid;
        this.GridView1.DataBind();
        this.GridView1.PageIndex = Convert.ToInt32(pageIndex);
    }

    private void ShowMsg(string msg)
    {
        string script = "<script>window.alert('" + msg + "');</script>";
        this.ClientScript.RegisterStartupScript(this.GetType(), "MSG", script);
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = System.Convert.ToInt32(e.CommandArgument);
        int e02_no = System.Convert.ToInt32(this.GridView1.DataKeys[rowIndex].Values[0]);

        //刪除
        if (e.CommandName.Equals("disable"))
        {
            e02DAO dao = new e02DAO();
            e02 edata = dao.GetBye02NO(e02_no);
            edata.e02_status = "2";
            dao.Update();

            int pgIndex = this.GridView1.PageIndex;
            this.GridView1.DataBind();
            this.GridView1.PageIndex = pgIndex;
        }

        //修改
        if (e.CommandName.Equals("edit"))
        {
            string url = this.GetUrl("300303-2.aspx",e02_no.ToString(),"modify");
            Response.Redirect(url);
        }

        //講義
        if (e.CommandName.Equals("book"))
        {

        }

        //限制條件
        if (e.CommandName.Equals("cond"))
        {
            string url = this.GetUrl("300303-1.aspx", e02_no.ToString(), "");
            Response.Redirect(url);
        }


    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ChangeObject cobj = new ChangeObject();
            int rowIndex = e.Row.RowIndex;
            int e02_no = System.Convert.ToInt32(this.GridView1.DataKeys[rowIndex].Values[0]);

            string url = this.GetUrl("300303-3.aspx", e02_no.ToString(), "");
            e.Row.Cells[0].Text += "(第" + this.GridView1.DataKeys[rowIndex].Values[3].ToString() + "期)";
            e.Row.Cells[0].Text = "<a href=" + url + ">" + e.Row.Cells[0].Text + "</a>";

            string sdate = "", edate = "";
            sdate = cobj._ADtoROC(Convert.ToDateTime(e.Row.Cells[2].Text));
            edate = cobj._ADtoROC(Convert.ToDateTime(this.GridView1.DataKeys[rowIndex].Values[1]));
            e.Row.Cells[2].Text = sdate + "~" + edate;

            sdate = cobj._ADtoROC(Convert.ToDateTime(e.Row.Cells[3].Text));
            edate = cobj._ADtoROC(Convert.ToDateTime(this.GridView1.DataKeys[rowIndex].Values[2]));
            e.Row.Cells[3].Text = sdate + "~" + edate;

            //報名10人，已核准5人
            string[] check = { "0", "1", "2" };
            int count = (from d in model.e04 where d.e02_no == e02_no && check.Contains(d.e04_check) select d).Count();
            int check_count = (from dd in model.e04 where dd.e02_no == e02_no && dd.e04_check == "1" select dd).Count();
            e.Row.Cells[4].Text = "報名" + count + "人，已核准" + check_count + "人";

        }
    }

    private string GetUrl(string tag,string e02_no,string model)
    {
        string url = tag;
        url += "?sdate=" + this.calendar1._ADDate.ToString("yyyy-MM-dd");
        url += "&edate=" + this.calendar2._ADDate.ToString("yyyy-MM-dd");
        url += "&type_1=" + this.ddl_type_1.SelectedValue;
        url += "&type_2=" + this.ddl_type_2.SelectedValue;
        url += "&e01_no=" + this.ddl_e01.SelectedValue;
        url += "&e02_name=" + this.tbox_name.Text;
        url += "&e02_no=" + e02_no;
        url += "&model=" + model;
        url += "&pageIndex=" + this.GridView1.PageIndex;
        return url;

    }

    protected void ddl_type_1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddl_type_1.SelectedValue != "0")
        {
            //帶入父類別參數
            this.ODS_type_2.SelectParameters[0].DefaultValue = this.ddl_type_1.SelectedValue;

            this.ddl_type_2.Items.Clear();
            this.ddl_type_2.DataBind();
            this.ddl_type_2.Items.Insert(0, new ListItem("請選擇", "0"));
            this.ddl_type_2.Items[0].Selected = true;

            //加入父類別
            this.ddl_type_2.Items.Insert(1,new ListItem(this.ddl_type_1.SelectedItem.Text,this.ddl_type_1.SelectedValue));
        }
        else
        {
            this.ddl_type_2.Items.Clear();
            this.ddl_type_2.Items.Insert(0, new ListItem("請選擇", "0"));
            this.ddl_type_2.Items[0].Selected = true;
        }
    }

    protected void Button_new_Click(object sender, EventArgs e)
    {
        string url = this.GetUrl("300303-2.aspx", "", "new");
        Response.Redirect(url);
    }
}