using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;
using System.Collections.Specialized;
using AjaxControlToolkit;
using System.Data.Objects.SqlClient;
using NLog;

public partial class _20_200400_200404 : System.Web.UI.Page
{
    private static Logger logger = LogManager.GetCurrentClassLogger();

    private NXEIPEntities model = new NXEIPEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.calendar1._ADDate = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-01-01"));
            this.calendar2._ADDate = System.DateTime.Now;

            this.ODS_type_2.SelectParameters["typ_parent"].DefaultValue = "-1";
            this.LoadData(this.calendar1._ADDate.ToString("yyyy-MM-dd"), this.calendar2._ADDate.ToString("yyyy-MM-dd"), this.ddl_type_1.SelectedValue, this.ddl_type_2.SelectedValue, this.ddl_e01.SelectedValue, this.tbox_name.Text, new SessionObject().sessionUserID, "0");


            OperatesObject.OperatesExecute(200404, new SessionObject().sessionUserID, 2, "查詢個人學習記錄");


        }
    }

    /// <summary>
    /// 撈資料
    /// </summary>
    private void LoadData(string sdate, string edate, string type_1, string type_2, string e01_no, string e02_name, string peo_uid, string pageIndex)
    {
        this.ODS_1.SelectParameters["sdate"].DefaultValue = sdate;
        this.ODS_1.SelectParameters["edate"].DefaultValue = edate;
        this.ODS_1.SelectParameters["type_1"].DefaultValue = type_1;
        this.ODS_1.SelectParameters["type_2"].DefaultValue = type_2;
        this.ODS_1.SelectParameters["e01_no"].DefaultValue = e01_no;
        this.ODS_1.SelectParameters["e02_name"].DefaultValue = e02_name;
        this.ODS_1.SelectParameters["peo_uid"].DefaultValue = peo_uid;
        this.GridView1.DataBind();
        this.GridView1.PageIndex = Convert.ToInt32(pageIndex);
    }

    protected void btn_ok_Click(object sender, EventArgs e)
    {
        if (this.CheckUI())
        {
            this.LoadData(this.calendar1._ADDate.ToString("yyyy-MM-dd"), this.calendar2._ADDate.ToString("yyyy-MM-dd"), this.ddl_type_1.SelectedValue, this.ddl_type_2.SelectedValue, this.ddl_e01.SelectedValue, this.tbox_name.Text, new SessionObject().sessionUserID, "0");
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

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ChangeObject cobj = new ChangeObject();
            int rowIndex = e.Row.RowIndex;
            int e02_no = System.Convert.ToInt32(this.GridView1.DataKeys[rowIndex].Values[0]);

            e.Row.Cells[0].Text += "(第" + this.GridView1.DataKeys[rowIndex].Values[2].ToString() + "期)";

            string sdate = "", edate = "";
            sdate = cobj._ADtoROC(Convert.ToDateTime(e.Row.Cells[3].Text));
            edate = cobj._ADtoROC(Convert.ToDateTime(this.GridView1.DataKeys[rowIndex].Values[1]));
            e.Row.Cells[3].Text = sdate + "~" + edate;

            //成績
            int peo_uid = int.Parse(new SessionObject().sessionUserID);
            string[] check = { "0", "1" };
            int? score = (from e04D in model.e04 where check.Contains(e04D.e04_check) && e04D.e04_peouid == peo_uid select e04D.e04_result).FirstOrDefault();
            if (score.HasValue)
            {
                e.Row.Cells[4].Text = score.Value.ToString();
            }
            else
            {
                e.Row.Cells[4].Text = "&nbsp;";
            }

        }
    }
}