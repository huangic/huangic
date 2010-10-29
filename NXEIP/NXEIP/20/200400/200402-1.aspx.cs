using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;

public partial class _20_200400_200402_1 : System.Web.UI.Page
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
                this.hidd_check.Value = d.e02_check;
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

    
    protected void Button1_Click(object sender, EventArgs e)
    {
        SessionObject sobj = new SessionObject();
        int user_depno = Convert.ToInt32(sobj.sessionUserDepartID);
        int peo_uid = Convert.ToInt32(sobj.sessionUserID);
        string[] _check = { "0", "1" };//未審,審核通過
        int e02_no = Convert.ToInt32(this.hidd_no.Value);
        string msg = "";

        //部門限制人數
        int _dep_count = (from dt in model.e03 where dt.e03_depno == user_depno && dt.e02_no == e02_no select dt.e03_people.Value).FirstOrDefault();
        //所屬部門已報名之人數
        int _dep_app = (from t1 in model.e04 where t1.e02_no == e02_no && t1.e04_depno == user_depno && _check.Contains(t1.e04_check) select t1).Count();
        if (_dep_app >= _dep_count)
        {
            this.ShowMsg("很抱歉，該課程部門人數已達上限");
        }
        else
        {
            e04 e04Data = new e04();
            e04Data.e04_peouid = peo_uid;
            e04Data.e04_applydate = DateTime.Now;
            e04Data.e04_depno = user_depno;
            e04Data.e04_prono = new UtilityDAO().Get_TypesNo(peo_uid);
            if (this.hidd_check.Value.Equals("1"))
            {
                e04Data.e04_check = "0";
                msg = "報名已完成，請等待管員者審核！";
            }
            else
            {
                e04Data.e04_check = "1";
                msg = "報名已完成！";
            }
            int e04_no = 1;
            try
            {
                e04_no = (from t1 in model.e04 where t1.e02_no == e02_no select t1.e04_no).Max() + 1;
            }
            catch { };
            e04Data.e04_no = e04_no;
            e04Data.e02_no = e02_no;

            model.AddToe04(e04Data);
            model.SaveChanges();

            //送訊息給管理員

            OperatesObject.OperatesExecute(200402, peo_uid.ToString(), 1, "報名課程 e02_no:" + this.hidd_no.Value);
            this.ShowMsg_URL(msg, this.GetUrl("200402.aspx"));
        }

    }

    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(this.GetUrl("200402.aspx"));
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
        this.ClientScript.RegisterStartupScript(this.GetType(), "MSG", script);
    }

    private void ShowMsg_URL(string msg, string url)
    {
        string script = "<script>window.alert('" + msg + "');location.replace('" + url + "')</script>";
        this.ClientScript.RegisterStartupScript(this.GetType(), "MyScript", script);
    }
}