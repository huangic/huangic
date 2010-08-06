using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class _35_350100_350102 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.Panel1.Visible = true;
            this.Panel2.Visible = false;


        }
    }
    protected void rbl_people_CheckedChanged(object sender, EventArgs e)
    {
        if (((System.Web.UI.WebControls.RadioButton)sender).ID.Equals("rbl_people"))
        {
            this.Panel1.Visible = true;
            this.Panel2.Visible = false;

        }
        else
        {
            this.Panel1.Visible = false;
            this.Panel2.Visible = true;
        }
    }

    /// <summary>
    /// 確定
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void but_ok_Click(object sender, EventArgs e)
    {
        
        
        bool showMsg = false;

        //people
        if (this.rbl_people.Checked)
        {
            foreach (var people in this.jQueryPeopleTree1.Items)
            {
                this.Accounts_process(people.Key);
                showMsg = true;
            }
        }

        //部門
        if (this.rbl_dep.Checked)
        {
            foreach (var depart in this.jQueryDepartTree1.Items)
            {
                string dep_no = depart.Key;

                //部門人員
                DataTable mytable = new DBObject().ExecuteQuery("select peo_uid from people where dep_no = " + dep_no);
                for (int p = 0; p < mytable.Rows.Count; p++)
                {
                    this.Accounts_process(mytable.Rows[p]["peo_uid"].ToString());
                    showMsg = true;
                }
            }
        }

        if (showMsg)
        {
            this.ShowMsg("設定完成!");
            Response.Redirect("350102.aspx");
            
        }
    }

    private void Accounts_process(string peo_uid)
    {
        string sql = "";
        DBObject dbo = new DBObject();

        //尋找人員帳號acc_no
        string acc_no = dbo.ExecuteScalar("select acc_no from accounts where peo_uid = " + peo_uid);

        //尋找 rac_no 最大值
        sql = "select max(rac_no) as rac_no from roleaccount";
        int rac_no = 0;
        try
        {
            rac_no = Convert.ToInt32(dbo.ExecuteScalar(sql));
        }
        catch { }

        //所設定之角色
        for (int i = 0; i < this.lbox_set.Items.Count; i++)
        {
            string rol_no = this.lbox_set.Items[i].Value;
            sql = "select count(*) as total from roleaccount where acc_no = " + acc_no + " and rol_no = " + rol_no;

            //角色是否已存在 
            if (Convert.ToInt32(dbo.ExecuteScalar(sql)) == 0)
            {
                //新增角色至帳號
                rac_no++;
                sql = "insert into roleaccount (rol_no,rac_no,acc_no) values (" + rol_no + "," + rac_no + "," + acc_no + ")";
                dbo.ExecuteNonQuery(sql);
            }
        }
    }

    /// <summary>
    /// 取消設定
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (this.lbox_set.SelectedIndex >= 0)
        {
            this.lbox_noset.Items.Add(this.lbox_set.SelectedItem);
            this.lbox_set.Items.Remove(this.lbox_set.SelectedItem);

            
        }
    }

    /// <summary>
    /// 設定角色
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (this.lbox_noset.SelectedIndex >= 0)
        {
            this.lbox_set.Items.Add(this.lbox_noset.SelectedItem);
            this.lbox_noset.Items.Remove(this.lbox_noset.SelectedItem);
        }
    }

    protected void but_cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("350102.aspx");
    }

    private void ShowMsg(string msg)
    {
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "msg", "alert('" + msg + "');", true);
        
        //this.Page.ClientScript.RegisterStartupScript(typeof(_35_350100_350102), "msg", "alert('" + msg + "');", true);
        //Literal scr = new Literal();
        //scr.Text = "<script>alert('" + msg + "');</script>";
        //this.Page.Controls.Add(scr);
    }
}
