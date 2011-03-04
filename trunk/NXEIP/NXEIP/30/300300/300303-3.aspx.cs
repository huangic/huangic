using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;
using System.Data;

public partial class _30_300300_300303_3 : System.Web.UI.Page
{
    private NXEIPEntities model = new NXEIPEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            //this.Navigator1.SubFunc = "";

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
                    if (d.e02_people.Value >= 9999)
                    {
                        this.lab_people.Text = "無限制";
                    }
                    else
                    {
                        this.lab_people.Text = d.e02_people.ToString();
                    }
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
                this.lab_signdate.Text = cboj._ROCtoROCYMD(cboj._ADtoROC(Convert.ToDateTime(d.e02_signdate.ToString()))) + " 至 " + cboj._ROCtoROCYMD(cboj._ADtoROC(Convert.ToDateTime(d.e02_signedate.ToString())));
                this.lab_date.Text = cboj._ROCtoROCYMD(cboj._ADtoROC(Convert.ToDateTime(d.e02_sdate.ToString()))) + cboj._ADtoTime(d.e02_sdate.Value) + " 至 " + cboj._ROCtoROCYMD(cboj._ADtoROC(Convert.ToDateTime(d.e02_edate.ToString()))) + cboj._ADtoTime(d.e02_edate.Value);

                OperatesObject.OperatesExecute(300303, new SessionObject().sessionUserID, 2, "檢視課程 e02_no:" + this.hidd_no.Value);
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
            ChangeObject cobj = new ChangeObject();
            UtilityDAO udao = new UtilityDAO();
            int e02_no = Convert.ToInt32(this.hidd_no.Value);
            //課程資料
            e02 e02Data = (from d in model.e02 where d.e02_no == e02_no select d).FirstOrDefault();

            DataTable dt = new DataTable();
            dt.Columns.Add("*身份證字號");
            dt.Columns.Add("*名稱");
            dt.Columns.Add("*起始日期");
            dt.Columns.Add("*姓名");
            dt.Columns.Add("*學位學分");
            dt.Columns.Add("*課程類別代碼");
            dt.Columns.Add("*上課縣市");
            dt.Columns.Add("期別");
            dt.Columns.Add("*終迄日期");
            dt.Columns.Add("*訓練總數");
            dt.Columns.Add("*訓練總數單位");
            dt.Columns.Add("訓練成績");
            dt.Columns.Add("證件字號");
            dt.Columns.Add("出勤上課狀況");
            dt.Columns.Add("生日");
            dt.Columns.Add("*學習性質");
            dt.Columns.Add("*數位時數");
            dt.Columns.Add("*實體時數");
            dt.Columns.Add("課程代碼");

            //此課程核可人員資料
            var data = (from dd in model.e04
                        where dd.e02_no == e02_no && dd.e04_check == "1"
                        orderby dd.e04_no
                        from p in model.people
                        where p.peo_uid == dd.e04_peouid
                        select new { dd,p.peo_idcard,p.peo_name,p.peo_birthday });
            foreach (var d in data)
            {
                DataRow row = dt.NewRow();
                row["*身份證字號"] = d.peo_idcard;
                row["*名稱"] = e02Data.e02_name;
                row["*起始日期"] = cobj.ROCto3ROC(cobj._ADtoROC(e02Data.e02_sdate.Value));
                row["*姓名"] = d.peo_name;
                row["*學位學分"] = "6";
                try
                {
                    row["*課程類別代碼"] = udao.Get_TypesNumber(e02Data.typ_no);
                }
                catch
                {
                    row["*課程類別代碼"] = "70";
                }
                row["*上課縣市"] = udao.Get_TypesNumber(e02Data.e02_city.Value);
                row["期別"] = e02Data.e02_flag.Value.ToString();
                row["*終迄日期"] = cobj.ROCto3ROC(cobj._ADtoROC(e02Data.e02_edate.Value));
                row["*訓練總數"] = e02Data.e02_hour.Value.ToString();
                row["*訓練總數單位"] = "1";
                if (d.dd.e04_result.HasValue)
                {
                    row["訓練成績"] = d.dd.e04_result.Value.ToString();
                }
                row["證件字號"] = "";
                row["出勤上課狀況"] = "";
                if (d.peo_birthday.HasValue)
                {
                    row["生日"] = cobj.ROCto3ROC(cobj._ADtoROC( d.peo_birthday.Value));
                }
                row["*學習性質"] = "2";
                row["*數位時數"] = "0";
                row["*實體時數"] = e02Data.e02_hour.Value.ToString();
                row["課程代碼"] = e02Data.e02_code;

                dt.Rows.Add(row);
            }

            string filename = e02Data.e02_name + "第" + e02Data.e02_flag + "期名冊.xls";

            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
            Response.Clear();

            Response.BinaryWrite(new ExcelObject().ExportExcel(dt).GetBuffer());
            Response.End();
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
}