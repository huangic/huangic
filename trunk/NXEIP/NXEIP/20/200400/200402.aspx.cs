using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;
using NLog;

public partial class _20_200400_200402 : System.Web.UI.Page
{
    private static Logger logger = LogManager.GetCurrentClassLogger();

    private NXEIPEntities model = new NXEIPEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.ODS_type_2.SelectParameters["typ_parent"].DefaultValue = "-1";

            this.calendar1._ADDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-01-01"));
            this.calendar2._ADDate = DateTime.Now;

            if (Request["sdate"] != null && Request["edate"] != null)
            {
                string sdate = "", edate = "";
                if (Request["sdate"] == DateTime.Now.ToString("yyyy-01-01") && Request["edate"] == DateTime.Now.ToString("yyyy-MM-dd"))
                {
                    sdate = DateTime.Now.ToString("yyyy-MM-dd");
                    edate = "";
                }
                else
                {
                    sdate = Request["sdate"];
                    edate = Request["edate"];
                }

                this.LoadData(sdate, edate, Request["type_1"], Request["type_2"], Request["e01_no"], Request["e02_name"], Request["pageIndex"]);
            }
            else
            {
                this.LoadData(DateTime.Now.ToString("yyyy-MM-dd"), "", this.ddl_type_1.SelectedValue, this.ddl_type_2.SelectedValue, this.ddl_e01.SelectedValue, this.tbox_name.Text, "0");
            }

            OperatesObject.OperatesExecute(200402, new SessionObject().sessionUserID, 2, "查詢線上報名");
        }
    }

    /// <summary>
    /// 撈資料
    /// </summary>
    private void LoadData(string sdate, string edate, string type_1, string type_2, string e01_no, string e02_name, string pageIndex)
    {
        this.ODS_1.SelectParameters["sdate"].DefaultValue = sdate;
        this.ODS_1.SelectParameters["edate"].DefaultValue = edate;
        this.ODS_1.SelectParameters["type_1"].DefaultValue = type_1;
        this.ODS_1.SelectParameters["type_2"].DefaultValue = type_2;
        this.ODS_1.SelectParameters["e01_no"].DefaultValue = e01_no;
        this.ODS_1.SelectParameters["e02_name"].DefaultValue = e02_name;
        //建立者
        //this.ODS_1.SelectParameters["openuid"].DefaultValue = openuid;
        this.GridView1.DataBind();
        this.GridView1.PageIndex = Convert.ToInt32(pageIndex);
    }
    protected void btn_ok_Click(object sender, EventArgs e)
    {
        if (this.CheckUI())
        {
            this.LoadData(this.calendar1._ADDate.ToString("yyyy-MM-dd"), this.calendar2._ADDate.ToString("yyyy-MM-dd"), this.ddl_type_1.SelectedValue, this.ddl_type_2.SelectedValue, this.ddl_e01.SelectedValue, this.tbox_name.Text, "0");
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
    private void ShowMsg(string msg)
    {
        string script = "<script>window.alert('" + msg + "');</script>";
        this.ClientScript.RegisterStartupScript(this.GetType(), "MSG", script);
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = System.Convert.ToInt32(e.CommandArgument);
        int e02_no = System.Convert.ToInt32(this.GridView1.DataKeys[rowIndex].Values[0]);

        //報名
        if (e.CommandName.Equals("applic"))
        {
            Response.Redirect(this.GetUrl("200402-1.aspx", e02_no.ToString(), ""));
        }

        //取消報名
        if (e.CommandName.Equals("cancel"))
        {
            string[] _check = { "0", "1" };
            int peo_uid = Convert.ToInt32(new SessionObject().sessionUserID);
            int e04_no = (from t2 in model.e04 where t2.e02_no == e02_no && t2.e04_peouid == peo_uid && _check.Contains(t2.e04_check) select t2.e04_no).FirstOrDefault();
            logger.Debug("e04_no="+e04_no.ToString());
            e04 e04Data = (from t2 in model.e04 where t2.e02_no == e02_no && t2.e04_no == e04_no && t2.e04_peouid == peo_uid select t2).FirstOrDefault();
            if (e04Data != null)
            {
                e04Data.e04_check = "3";
                e04Data.e04_checkuid = peo_uid;
                e04Data.e04_checkdate = DateTime.Now;
                model.SaveChanges();
                OperatesObject.OperatesExecute(200402, peo_uid.ToString(), 4, string.Format("自行取消課程報名 e02_no:{0},e04_no:{1}", e02_no, e04_no));
                this.GridView1.DataBind();
            }
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ChangeObject cobj = new ChangeObject();
            int rowIndex = e.Row.RowIndex;
            int e02_no = System.Convert.ToInt32(this.GridView1.DataKeys[rowIndex].Values[0]);

            e.Row.Cells[0].Text += "(第" + this.GridView1.DataKeys[rowIndex].Values[3].ToString() + "期)";

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

            //活動狀態
            SessionObject sobj = new SessionObject();
            int user_depno = Convert.ToInt32(sobj.sessionUserDepartID);
            string[] _check = { "0", "1" };//未審,審核通過
            ((LinkButton)e.Row.FindControl("linkBut_1")).Visible = false;
            ((LinkButton)e.Row.FindControl("linkBut_2")).Visible = false;
            e02 e02Data = (from d in model.e02 where d.e02_no == e02_no select d).FirstOrDefault();

            if (DateTime.Now >= e02Data.e02_signdate.Value && DateTime.Now <= e02Data.e02_signedate.Value)
            {
                //報名人數
                int _count = (from dd in model.e04 where dd.e02_no == e02_no && _check.Contains(dd.e04_check) select dd).Count();
                if (e02Data.e02_people.HasValue && _count >= e02Data.e02_people.Value)
                {
                    ((Label)e.Row.FindControl("lab_msg")).Text = "人數已達上限";
                }
                else
                {
                    //部門限制人數
                    int _dep_count = (from dt in model.e03 where dt.e03_depno == user_depno && dt.e02_no == e02_no select dt.e03_people.Value).FirstOrDefault();
                    //所屬部門已報名之人數
                    int _dep_app = (from t1 in model.e04 where t1.e02_no == e02_no && t1.e04_depno == user_depno && _check.Contains(t1.e04_check) select t1).Count();
                    if (_dep_count > 0 && _dep_app >= _dep_count)
                    {
                        ((Label)e.Row.FindControl("lab_msg")).Text = "部門人數已達上限";
                    }
                    else
                    {
                        //是否已經報名
                        int peo_uid = Convert.ToInt32(sobj.sessionUserID);
                        int e04_no = (from t2 in model.e04 where t2.e02_no == e02_no && t2.e04_peouid == peo_uid && _check.Contains(t2.e04_check) select t2.e04_no).FirstOrDefault();
                        if (e04_no == 0)
                        {
                            ((LinkButton)e.Row.FindControl("linkBut_1")).Visible = true;
                        }
                        else
                        {
                            ((LinkButton)e.Row.FindControl("linkBut_2")).Visible = true;
                        }
                    }
                }

            }
            else
            {
                if (DateTime.Now < e02Data.e02_signdate.Value)
                {
                    ((Label)e.Row.FindControl("lab_msg")).Text = "報名尚未開始";
                }
                else if (DateTime.Now > e02Data.e02_signedate.Value)
                {
                    ((Label)e.Row.FindControl("lab_msg")).Text = "報名已截止";
                }
                else
                {
                    ((Label)e.Row.FindControl("lab_msg")).Text = "";
                }
            }

        }
    }

    private string GetUrl(string tag, string e02_no, string model)
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


            //加入父類別
            this.ddl_type_2.Items.Insert(1, new ListItem(this.ddl_type_1.SelectedItem.Text, this.ddl_type_1.SelectedValue));
        }
        else
        {
            this.ddl_type_2.Items.Clear();
            this.ddl_type_2.Items.Insert(0, new ListItem("請選擇", "0"));
        }
    }
}