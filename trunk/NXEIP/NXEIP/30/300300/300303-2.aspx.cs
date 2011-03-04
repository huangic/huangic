using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity;
using NXEIP.DAO;

public partial class _30_300300_300303_2 : System.Web.UI.Page
{
    private NXEIPEntities model = new NXEIPEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.Navigator1.SubFunc = "課程新增";

            this.ddl_type_1.DataBind();
            this.ddl_type_1.Items.Insert(0, new ListItem("請選擇", "0"));
            this.ddl_type_1.Items[0].Selected = true;

            this.ddl_type_2.DataBind();
            this.ddl_type_2.Items.Clear();
            this.ddl_type_2.Items.Insert(0, new ListItem("請選擇", "0"));
            this.ddl_type_2.Items[0].Selected = true;

            string dep_name = (from d in model.departments where d.dep_parentid == 0 select d.dep_name).FirstOrDefault().ToString();
            this.lab_mechani.Text = dep_name;

            if (Request["model"] != null)
            {
                this.hidd_model.Value = Request["model"];
            }

            //修改
            if (this.hidd_model.Value.Equals("modify"))
            {
                this.hidd_no.Value = Request["e02_no"];
                //取資料
                e02 edata = new e02DAO().GetBye02NO(Convert.ToInt32(this.hidd_no.Value));

                //期別
                this.tbox_flag.Text = edata.e02_flag.ToString();

                //課程名稱
                int? typ_no_1 = (from t in model.types
                                 where t.typ_no == edata.typ_no && t.typ_status == "1"
                                 select t.typ_parent).FirstOrDefault();

                //課程類別是否被刪除
                if (typ_no_1.HasValue)
                {
                    if (typ_no_1 == 0)
                    {   //屬於父類別

                        //定位父類別
                        this.ddl_type_1.Items[0].Selected = false;
                        this.ddl_type_1.Items.FindByValue(edata.typ_no.ToString()).Selected = true;

                        //帶入父類別參數
                        this.ODS_type_2.SelectParameters[0].DefaultValue = this.ddl_type_1.SelectedValue;
                        this.ddl_type_2.Items.Clear();
                        this.ddl_type_2.DataBind();
                        this.ddl_type_2.Items.Insert(0, new ListItem("請選擇", "0"));

                        //加入父類別,定位子類別
                        this.ddl_type_2.Items.Insert(1, new ListItem(this.ddl_type_1.SelectedItem.Text, this.ddl_type_1.SelectedValue));
                        this.ddl_type_2.Items[1].Selected = true;
                    }
                    else
                    {   //屬於子類別處理

                        //定位父類別
                        this.ddl_type_1.Items[0].Selected = false;
                        this.ddl_type_1.Items.FindByValue(typ_no_1.ToString()).Selected = true;

                        //帶入父類別參數
                        this.ODS_type_2.SelectParameters[0].DefaultValue = this.ddl_type_1.SelectedValue;
                        this.ddl_type_2.Items.Clear();
                        this.ddl_type_2.DataBind();
                        this.ddl_type_2.Items.Insert(0, new ListItem("請選擇", "0"));
                        //加入父類別,定位子類別
                        this.ddl_type_2.Items.Insert(1, new ListItem(this.ddl_type_1.SelectedItem.Text, this.ddl_type_1.SelectedValue));
                        this.ddl_type_2.Items.FindByValue(edata.typ_no.ToString()).Selected = true;
                    }
                }

                //課程名稱,課程簡介,資格說明
                this.tbox_name.Text = edata.e02_name;
                this.tbox_memo.Text = edata.e02_memo;
                this.tbox_limit.Text = edata.e02_limit;

                //上課地點
                if (!string.IsNullOrEmpty(edata.e02_place))
                {
                    this.tbox_place.Text = edata.e02_place;
                }

                //名額上限
                if (edata.e02_people.HasValue)
                {
                    this.tbox_people.Text = edata.e02_people.ToString();
                }
                //認證時數
                if (edata.e02_hour.HasValue)
                {
                    this.tbox_hour.Text = edata.e02_hour.ToString();
                }
                //審核
                this.rbl_check.SelectedItem.Selected = false;
                this.rbl_check.Items.FindByValue(edata.e02_check).Selected = true;
                //講師
                this.tbox_teacher.Text = edata.e02_teacher;

                //開放日期
                this.cal_opendate._ADDate = edata.e02_opendate.Value;
                this.cal_signsdate._ADDate = edata.e02_signdate.Value;
                this.cal_signedate._ADDate = edata.e02_signedate.Value;
                
                //上課開始日期
                this.cal_sdate._ADDate = edata.e02_sdate.Value;
                this.ddl_sh.SelectedItem.Selected = false;
                this.ddl_sh.Items.FindByText(edata.e02_sdate.Value.ToString("HH")).Selected = true;
                this.ddl_sm.SelectedItem.Selected = false;
                this.ddl_sm.Items.FindByText(edata.e02_sdate.Value.ToString("mm")).Selected = true;

                this.cal_edate._ADDate = edata.e02_edate.Value;
                this.ddl_eh.SelectedItem.Selected = false;
                this.ddl_eh.Items.FindByText(edata.e02_edate.Value.ToString("HH")).Selected = true;
                this.ddl_em.SelectedItem.Selected = false;
                this.ddl_em.Items.FindByText(edata.e02_edate.Value.ToString("mm")).Selected = true;
                
                this.Panel2.Visible = false;
            }
            else
            {

            }
        }
    }

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        //批次開班
        //Response.Write("hidd_date = "+this.hidd_date.Value);return;

        if (this.hidd_model.Value.Equals("new"))
        {
            //儲存第1期課程資料
            this.saveData("1",null,null,null,null,null);

            //批次開班
            if (this.hidd_date.Value != "")
            {
                //第2期_99-10-14_99-10-20_99-10-25 08:00_99-10-30 17:00,第3期_99-10-14_99-10-20_99-10-25 08:00_99-10-30 17:00
                string[] cdate = this.hidd_date.Value.Split(',');
                ChangeObject cObj = new ChangeObject();

                for (int i = 0; i < cdate.Length; i++)
                {
                    string[] d = cdate[i].Split('_');
                    int flag = Convert.ToInt32(d[0].Substring(1, d[0].Length - 2));
                    DateTime sign_sdate = cObj._ROCtoAD(d[1]);
                    DateTime sign_edate = cObj._ROCtoAD(d[2]);
                    DateTime sdate = Convert.ToDateTime(cObj.ROCDTtoADDT(d[3].Split(' ')[0]) + " " + d[3].Split(' ')[1]);
                    DateTime edate = Convert.ToDateTime(cObj.ROCDTtoADDT(d[4].Split(' ')[0]) + " " + d[4].Split(' ')[1]);

                    //儲存第N期課程資料
                    this.saveData("2", flag, sign_sdate, sign_edate, sdate, edate);
                }
            }

            this.ShowMsg_URL("新增完成!", this.GetUrl());
        }

        if (this.hidd_model.Value.Equals("modify"))
        {
            this.ModifyData();
            this.ShowMsg_URL("修改完成!", this.GetUrl());
        }
    }

    private void saveData(string type,int? flag,DateTime? sign_sdate,DateTime? sign_edate,DateTime? sdate,DateTime? edate)
    {
        e02 data = new e02();

        data.e02_code = this.Get_e02Code();
        data.e02_name = this.tbox_name.Text;
        data.e02_memo = this.tbox_memo.Text;
        data.e02_limit = this.tbox_limit.Text;

        //名額限制
        if (this.tbox_people.Text != "")
        {
            data.e02_people = Convert.ToInt32(this.tbox_people.Text);
        }
        else
        {
            data.e02_people = 9999;
        }

        if (this.tbox_hour.Text != "")
        {
            data.e02_hour = Convert.ToInt32(this.tbox_hour.Text);
        }
        else
        {
            data.e02_hour = 0;
        }

        data.e02_teacher = this.tbox_teacher.Text;
        data.e02_mechani = this.lab_mechani.Text;
        data.e02_money = 0;
        data.e02_type = "2";

        //開始報名日期
        data.e02_opendate = this.cal_opendate._ADDate;

        if (type.Equals("1"))
        {
            //期別,報名日期,上課日期
            data.e02_flag = Convert.ToInt32(this.tbox_flag.Text);
            data.e02_signdate = this.cal_signsdate._ADDate;
            data.e02_signedate = Convert.ToDateTime(this.cal_signedate._ADDate.ToString("yyyy-MM-dd 23:59:59"));
            data.e02_sdate = Convert.ToDateTime(this.cal_sdate._ADDate.ToString("yyyy-MM-dd") + " " + this.ddl_sh.SelectedValue + ":" + this.ddl_sm.SelectedValue);
            data.e02_edate = Convert.ToDateTime(this.cal_edate._ADDate.ToString("yyyy-MM-dd") + " " + this.ddl_eh.SelectedValue + ":" + this.ddl_em.SelectedValue);
        }
        else
        {
            //批次開班
            data.e02_flag = flag;
            data.e02_signdate = sign_sdate;
            data.e02_signedate = Convert.ToDateTime(sign_edate.Value.ToString("yyyy-MM-dd 23:59:59"));
            data.e02_sdate = sdate;
            data.e02_edate = edate;
        }

        //變動人員
        data.e02_createuid = Convert.ToInt32(new SessionObject().sessionUserID);
        data.e02_createtime = DateTime.Now;

        //審核
        data.e02_check = this.rbl_check.SelectedValue;

        //開立課程人員
        data.e02_openuid = Convert.ToInt32(new SessionObject().sessionUserID);
        data.e02_applydate = DateTime.Now;

        //狀態
        data.e02_status = "1";

        //上課地點,改由使用者輸入
        data.e01_no = 1;//舊的上課地點欄位,固定輸入1
        data.e02_place = this.tbox_place.Text.Trim();

        //課程類別
        data.typ_no = Convert.ToInt32(this.ddl_type_2.SelectedValue);

        //上課縣市代碼
        try
        {
            int typ_no = (from d in model.arguments
                          where d.arg_variable == "city_code"
                          from t in model.types
                          where t.typ_number == d.arg_value
                          select t.typ_no).FirstOrDefault();
            data.e02_city = typ_no;
        }
        catch { }
        

        //存資料庫
        e02DAO dao = new e02DAO();
        dao.Adde02(data);
        dao.Update();

        OperatesObject.OperatesExecute(300303, new SessionObject().sessionUserID, 1, "新增課程");
    }

    private void ModifyData()
    {
        e02DAO dao = new e02DAO();
        e02 data = dao.GetBye02NO(Convert.ToInt32(this.hidd_no.Value));

        data.e02_name = this.tbox_name.Text;
        data.e02_memo = this.tbox_memo.Text;
        data.e02_limit = this.tbox_limit.Text;
        if (this.tbox_people.Text != "")
        {
            data.e02_people = Convert.ToInt32(this.tbox_people.Text);
        }
        if (this.tbox_hour.Text != "")
        {
            data.e02_hour = Convert.ToInt32(this.tbox_hour.Text);
        }
        data.e02_teacher = this.tbox_teacher.Text;
        data.e02_type = "2";

        //開始報名日期
        data.e02_opendate = this.cal_opendate._ADDate;

        //期別,報名日期,上課日期
        data.e02_flag = Convert.ToInt32(this.tbox_flag.Text);
        data.e02_signdate = this.cal_signsdate._ADDate;
        data.e02_signedate = this.cal_signedate._ADDate;
        data.e02_sdate = Convert.ToDateTime(this.cal_sdate._ADDate.ToString("yyyy-MM-dd") + " " + this.ddl_sh.SelectedValue + ":" + this.ddl_sm.SelectedValue);
        data.e02_edate = Convert.ToDateTime(this.cal_edate._ADDate.ToString("yyyy-MM-dd") + " " + this.ddl_eh.SelectedValue + ":" + this.ddl_em.SelectedValue);

        //變動人員
        data.e02_createuid = Convert.ToInt32(new SessionObject().sessionUserID);
        data.e02_createtime = DateTime.Now;

        //審核
        data.e02_check = this.rbl_check.SelectedValue;

        //上課地點
        data.e02_place = this.tbox_place.Text.Trim();

        //課程類別
        data.typ_no = Convert.ToInt32(this.ddl_type_2.SelectedValue);

        dao.Update();

        OperatesObject.OperatesExecute(300303, new SessionObject().sessionUserID, 3, "更新課程 e02_no:" + this.hidd_no.Value);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private string Get_e02Code()
    {
        //課程代碼 編碼為tn+三碼年+五碼流水號
        //取得目前最大
        string code = (from d in model.e02 select d.e02_code).Max();
        string roc = Convert.ToString(Convert.ToInt32(DateTime.Now.ToString("yyyy")) - 1911);
        if (Convert.ToInt32(roc) < 100)
        {
            roc = "0" + roc;
        }

        if (code != null)
        {
            int c = Convert.ToInt32(code.Substring(5, 5)) + 1;
            string tmp = "";
            for (int i = 1; i <= 5 - c.ToString().Length; i++)
            {
                tmp += "0";
            }
            code = "tn" + roc + tmp + c.ToString();
        }
        else
        {
            code = "tn" + roc + "00001";
        }

        return code;
    }

    

    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        Server.Transfer(this.GetUrl());
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

    protected void ddl_type_1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddl_type_1.SelectedValue != "0")
        {
            //帶入父類別參數
            this.ODS_type_2.SelectParameters[0].DefaultValue = this.ddl_type_1.SelectedValue;

            this.ddl_type_2.Items.Clear();
            this.ddl_type_2.DataBind();
            this.ddl_type_2.Items.Insert(0, new ListItem("請選擇","0"));
            this.ddl_type_2.Items[0].Selected = true;

            //加入父類別
            this.ddl_type_2.Items.Insert(1, new ListItem(this.ddl_type_1.SelectedItem.Text, this.ddl_type_1.SelectedValue));
        }
        else
        {
            this.ddl_type_2.Items.Clear();
            this.ddl_type_2.Items.Insert(0, new ListItem("請選擇", "0"));
            this.ddl_type_2.Items[0].Selected = true;
        }
    }

    private void ShowMsg_URL(string msg, string url)
    {
        //string script = "<script>window.alert('" + msg + "');location.replace('" + url + "')</script>";
        //this.ClientScript.RegisterStartupScript(this.GetType(), "MyScript", script);
        JsUtil.AlertAndRedirectJs(this, msg, url);
    }
}