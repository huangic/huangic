using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class _35_350100_350103 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.Panel2.Visible = false;
        }
    }

    /// <summary>
    /// 確定 - 選人員
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void but_ok_Click(object sender, EventArgs e)
    {
        if (this.jQueryPeopleTree1.Items.Count > 0)
        {
            DBObject dbo = new DBObject();

            this.Panel2.Visible = true;

            //人員
            string peo_uid = this.jQueryPeopleTree1.Items[0].Key;
            this.lab_name.Text = this.jQueryPeopleTree1.Items[0].Value;
            this.lab_workid.Text = dbo.ExecuteScalar("select peo_workid from people where peo_uid = " + peo_uid);

            //帳號
            DataTable table_accounts = dbo.ExecuteQuery("select acc_no,acc_login,acc_passwd,acc_status from accounts where peo_uid =" + peo_uid);

            this.lab_accno.Text = table_accounts.Rows[0]["acc_no"].ToString();
            this.lab_oldaccount.Text = table_accounts.Rows[0]["acc_login"].ToString();
            this.lab_oldpasswd.Text = table_accounts.Rows[0]["acc_passwd"].ToString();

            this.tbox_accounts.Text = this.lab_oldaccount.Text;
            this.tbox_passwd.Text = "";

            if (table_accounts.Rows[0]["acc_status"].ToString().Equals("1"))
            {
                this.ddl_status.SelectedIndex = 0;
            }
            else
            {
                this.ddl_status.SelectedIndex = 1;
            }

            this.Panel1.Visible = false;
        }


        
    }

    protected void but_cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("350103.aspx");
    }

    /// <summary>
    /// 確定 - 修改資料
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        DBObject dbo = new DBObject();

        //帳號
        if (this.tbox_accounts.Text.Length > 0)
        {
            if (!this.lab_oldaccount.Text.Equals(this.tbox_accounts.Text))
            {
                if (Convert.ToInt32(dbo.ExecuteScalar("select count(*) as total from accounts where acc_login = '" + this.tbox_accounts.Text + "'")) > 0)
                {
                    this.lab_msg.Text = "已有相同帳號，請重新輸入！";
                    return;
                }
                else
                {
                    dbo.ExecuteNonQuery("update accounts set acc_login = '" + this.tbox_accounts.Text + "' where acc_no = " + this.lab_accno.Text);
                }
            }
        }
        else
        {
            this.lab_msg.Text = "請輸入帳號！";
            return;
        }

        //密碼
        if (this.tbox_passwd.Text.Length > 0)
        {
            if (this.tbox_passwd.Text.Length < 4)
            {
                this.lab_msg.Text = "密碼長度需介於4~20字元!";
                return;
            }
            else
            {
                dbo.ExecuteNonQuery("update accounts set acc_passwd = '" + this.tbox_passwd.Text + "' where acc_no = " + this.lab_accno.Text);
            }
        }

        //狀態
        dbo.ExecuteNonQuery("update accounts set acc_status = '" + this.ddl_status.SelectedValue + "' where acc_no = " + this.lab_accno.Text);

        Response.Redirect("350103.aspx");
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        this.Panel2.Visible = false;
        this.Panel1.Visible = true;
    }
}
