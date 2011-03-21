using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity;
using NXEIP.DAO;

public partial class _30_300700_300704 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.ObjectDataSource1.SelectParameters["type"].DefaultValue = "1";
            this.GridView1.DataBind();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //同意
        this.UpData(true);
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        //不同意
        this.UpData(false);
    }

    private void UpData(bool check)
    {
        ApplysDAO dao = new ApplysDAO();
        PeopleDAO peoDao = new PeopleDAO();
        AccountsDAO accDao = new AccountsDAO();
        ChangeObject cobj = new ChangeObject();
        EMailUtil mail = new EMailUtil();

        int check_uid = int.Parse(new SessionObject().sessionUserID);
        string checkName = new SessionObject().sessionUserName;

        UtilityDAO udao = new UtilityDAO();
        //人員類別,在職
        int ptype = udao.Get_TypesTypNo("ptype", "1");
        int jobtype = udao.Get_TypesTypNo("work", "1");

        for (int i = 0; i < this.GridView1.Rows.Count; i++)
        {
            CheckBox cbox = (CheckBox)(this.GridView1.Rows[i].FindControl("cbox_1"));
            if (cbox.Checked)
            {
                int app_no = Convert.ToInt32(this.GridView1.DataKeys[i].Value);

                applys data = dao.Get_applys(app_no);

                data.app_check = check ? "1" : "2";
                data.app_checkuid = check_uid;
                data.app_checkdate = DateTime.Now;

                dao.Update();

                //新增人員,帳號
                if (check)
                {
                    people pdata = new people();

                    pdata.peo_cellphone = data.app_cellphone;
                    pdata.peo_createtime = DateTime.Now;
                    pdata.peo_createuid = check_uid;
                    pdata.peo_email = data.app_email;
                    pdata.peo_idcard = data.app_idcard;
                    pdata.peo_jobtype = jobtype;
                    pdata.peo_name = data.app_name;
                    if (data.app_officetel.Length > 0)
                    {
                        try
                        {
                            pdata.peo_officetel = data.app_officetel.Split('#')[0];
                            pdata.peo_extension = data.app_officetel.Split('#')[1];
                        }
                        catch { }
                    }
                    pdata.peo_pfofess = data.app_profess;
                    pdata.peo_ptype = ptype;
                    pdata.peo_workid = data.app_login;
                    pdata.peo_account = data.app_login;

                    pdata.dep_no = data.app_depno.Value;

                    peoDao.AddPeople(pdata);
                    peoDao.Update();

                    //帳號
                    accounts accData = new accounts();
                    accData.acc_createtime = DateTime.Now;
                    accData.acc_createuid = check_uid;
                    accData.acc_login = data.app_login;
                    accData.acc_passwd = data.app_password;
                    accData.acc_status = "1";
                    accData.peo_uid = pdata.peo_uid;

                    accDao.Addaccounts(accData);
                    accDao.Update();

                    //登入權限
                    peoDao.addRoleAccount(pdata.peo_uid, pdata.dep_no, accData.acc_no);

                }

                //寄發EMAIL通知
                try
                {
                    
                    string subject = "申請帳號審核通知";
                    string body = string.Format("您在{0}所申請員工入口網帳號:{1}，經管理員{2}於{3}已進行審核，審核結果為{4}。", cobj._ADtoROCDT(data.app_date.Value), data.app_login, checkName, cobj._ADtoROCDT(data.app_checkdate.Value), (check ? "同意" : "不同意"));
                    mail.SendMail(subject, body, data.app_email);
                }
                catch { }

                OperatesObject.OperatesExecute(300704, 3, string.Format("{0} {1} 所申請之系統帳號 checkUID:{2}", (check ? "同意" : "不同意"), data.app_name, check_uid));

            }
        }

        JsUtil.AlertJs(this, "審核完成!");

        this.GridView1.DataBind();
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.ObjectDataSource1.SelectParameters["type"].DefaultValue = this.DropDownList1.SelectedValue;
        this.GridView1.DataBind();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        UtilityDAO udao = new UtilityDAO();
        ChangeObject cdao = new ChangeObject();

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Text = udao.Get_DepartmentName(int.Parse(e.Row.Cells[1].Text));
            e.Row.Cells[2].Text = udao.Get_TypesCName(int.Parse(e.Row.Cells[2].Text));
            e.Row.Cells[5].Text = cdao.ADDTtoROCDT(e.Row.Cells[5].Text);

            if (e.Row.Cells[6].Text == "0")
            {
                e.Row.Cells[6].Text = "送審中";
            }
            else
            {
                if (e.Row.Cells[6].Text == "1")
                {
                    e.Row.Cells[6].Text = "同意";
                }
                else
                {
                    e.Row.Cells[6].Text = "不同意";
                }
                e.Row.Cells[7].Text = udao.Get_PeopleName(int.Parse(e.Row.Cells[7].Text));
                e.Row.Cells[8].Text = cdao.ADDTtoROCDT(e.Row.Cells[8].Text);
            }

        }
    }
}