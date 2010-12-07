using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;
using System.Globalization;
using System.IO;
using System.Data;

public partial class _35_350200_350205 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.calendar3.Visible = false;
            this.DepartTreeTextBox1.Clear();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (this.Check_Input())
        {
            NXEIPEntities model = new NXEIPEntities();


            Entity.people newPeople = new Entity.people();

            newPeople.peo_idcard = this.tbox_cardid.Text;
            newPeople.peo_name = this.tbox_name.Text;
            newPeople.peo_account = this.tbox_account.Text;
            newPeople.peo_workid = this.tbox_workid.Text;

            //照片
            newPeople.peo_filename = this.FileUpload1.Get_FileName;
            if (!this.FileUpload1.Get_FileName.Equals(""))
            {
                newPeople.peo_picture = this.FileUpload1.Get_FileByte;
            }

            newPeople.peo_pfofess = Convert.ToInt32(this.ddl_profess.SelectedValue);
            newPeople.peo_ptype = Convert.ToInt32(this.ddl_ptype.SelectedValue);

            //部門
            int dep_no = Convert.ToInt32(DepartTreeTextBox1.Value);
            
            newPeople.dep_no = dep_no;
            //newPeople.departments = (from d in model.departments where d.dep_no == dep_no select d).First();

            //
            newPeople.peo_arrivedate = this.calendar1._ADDate;

            try
            {
                newPeople.peo_birthday = this.calendar2._ADDate;
            }
            catch
            {
            }

            newPeople.peo_email = this.tbox_mail.Text;
            newPeople.peo_addr = this.tbox_addr.Text;
            newPeople.peo_tel = this.tbox_tel.Text;
            newPeople.peo_officetel = this.tbox_otel.Text;
            newPeople.peo_extension = this.tbox_extension.Text;
            newPeople.peo_memo = this.tbox_memo.Text;


            newPeople.peo_jobtype = Convert.ToInt32(this.ddl_jobtype.SelectedValue);
            string code = new DBObject().ExecuteScalar("select typ_number from types where typ_no =" + this.ddl_jobtype.SelectedValue);
            if (!code.Equals("1"))
            {
                newPeople.peo_leave = this.calendar3._ADDate;
            }

            string peo_uid = new SessionObject().sessionUserID;
            newPeople.peo_createuid = Convert.ToInt32(peo_uid);
            newPeople.peo_createtime = System.DateTime.Now;

            //save data
            model.AddTopeople(newPeople);
            model.SaveChanges();

            //預設帳號{1:人事編號,2:員工帳號}  密碼，{1:身份證末4碼,2:同人事編號}
            string arg = "", acc_login = "", acc_passwd = "";
            arg = new ArgumentsObject().Get_argValue("320205_accounts");
            if (arg.Equals("2"))
            {
                acc_login = this.tbox_account.Text;
            }
            else
            {
                acc_login = this.tbox_workid.Text;
            }

            arg = new ArgumentsObject().Get_argValue("320205_passwd");
            if (arg.Equals("1"))
            {
                acc_passwd = this.tbox_cardid.Text.Substring(this.tbox_cardid.Text.Length - 4, 4);
            }
            else
            {
                acc_passwd = this.tbox_workid.Text;
            }

            accounts accountable = new accounts();

            accountable.acc_login = acc_login;
            accountable.acc_passwd = acc_passwd;
            accountable.acc_status = "1";
            accountable.acc_createuid = Convert.ToInt32(peo_uid);
            accountable.acc_createtime = System.DateTime.Now;
            accountable.people = newPeople;
            model.AddToaccounts(accountable);
            model.SaveChanges();

            DBObject dbo = new DBObject();

            //角色權限 1.部門預設角色 2.系統預設角色
            string rol_no = "";
            string defrole = dbo.ExecuteScalar("select rol_no from roldefault where dep_no =" + dep_no);
            if (!defrole.Equals(""))
            {
                rol_no = defrole;
            }
            else
            {
                rol_no = dbo.ExecuteScalar("select rol_no from role where rol_default='1'");
            }
            //尋找 rac_no 最大值
            int rac_no = 1;
            try
            {
                rac_no = Convert.ToInt32(dbo.ExecuteScalar("select max(rac_no) as rac_no from roleaccount")) + 1;
            }
            catch { }

            dbo.ExecuteNonQuery("insert into roleaccount (rol_no,rac_no,acc_no) values (" + rol_no + "," + rac_no + "," + accountable.acc_no + ")");

            //操作記錄
            try
            {
                new OperatesObject().ExecuteOperates(350205, peo_uid, 1, "新增人員-peo_uid:" + newPeople.peo_uid);
            }
            catch
            {
            }

            this.ShowMsg_URL("人員新增完成!", "350205.aspx?Count=" + new Random().Next(1000));

        }

    }

    private bool Check_Input()
    {
        bool ret = true;

        if (this.tbox_cardid.Text.Trim().Length == 0)
        {
            this.ShowMSG("請輸入身份證!");
            ret = false;
        }
        else
        {
            if (!new CheckObject().CheckIDCard(this.tbox_cardid.Text))
            {
                this.ShowMSG("身份證字號錯誤!");
                ret = false;
            }
            
        }

        if (this.tbox_name.Text.Trim().Length == 0)
        {
            this.ShowMSG("請輸入姓名!");
            ret = false;
        }
        if (this.tbox_account.Text.Trim().Length == 0)
        {
            this.ShowMSG("請輸入員工帳號!");
            ret = false;
        }
        if (this.tbox_workid.Text.Trim().Length == 0)
        {
            this.ShowMSG("請輸入人事編號!");
            ret = false;
        }
        if (string.IsNullOrEmpty(this.DepartTreeTextBox1.Value))
        {
            this.ShowMSG("請選擇部門!");
            ret = false;
        }
        try
        {
           DateTime peo_arrivedate = this.calendar1._ADDate;
        }
        catch
        {
            this.ShowMSG("到職日期錯誤");
            ret = false;
        }

        string code = new DBObject().ExecuteScalar("select typ_number from types where typ_no =" + this.ddl_jobtype.SelectedValue);
        if (!code.Equals("1"))
        {
            try
            {
                DateTime peo_arrivedate = this.calendar3._ADDate;
            }
            catch
            {
                this.ShowMSG("在職日期錯誤");
                ret = false;
            }
        }


        return ret;
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        Server.Transfer("350205.aspx?Count=" + new Random().Next(1000));
    }

    protected void ddl_jobtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        string code = new DBObject().ExecuteScalar("select typ_number from types where typ_no =" + this.ddl_jobtype.SelectedValue);
        if (code.Equals("1"))
        {
            this.calendar3.Visible = false;
        }
        else
        {
            this.calendar3.Visible = true;
        }
    }

    private void ShowMSG(string msg)
    {
        string script = "<script>alert('" + msg + "');</script>";
        this.ClientScript.RegisterStartupScript(this.GetType(), "msg", script);
    }

    private void ShowMsg_URL(string msg, string url)
    {
        string script = "<script>window.alert('" + msg + "');location.href='" + url + "'</script>";
        this.ClientScript.RegisterStartupScript(this.GetType(), "MyScript", script);
    }
    
}
