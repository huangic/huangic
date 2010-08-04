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

    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        this.Panel2.Visible = false;
        this.Panel1.Visible = true;
    }
}
