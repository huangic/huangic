using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _35_350300_350301 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.DepartTreeListBox_people.Clear();
            this.DepartTreeListBox_depart.Clear();

            this.calendar1._ADDate = DateTime.Now;
            this.calendar2._ADDate = DateTime.Now;

            this.ddl_sfu_parent.DataBind();
            this.ddl_sfu_parent.Items.Clear();
            this.ddl_sfu_parent.Items.Add(new ListItem("全部", "0"));
            this.ddl_sfu_parent.Enabled = false;

            this.ddl_sfu_no.DataBind();
            this.ddl_sfu_no.Items.Clear();
            this.ddl_sfu_no.Items.Add(new ListItem("全部", "0"));
            this.ddl_sfu_no.Enabled = false;
            
        }
    }

    protected void btn_ok_Click(object sender, EventArgs e)
    {
        if (this.CheckUI())
        {
            string date = this.calendar1._AD + "," + this.calendar2._AD;
            string sfu = this.ddl_sys.SelectedValue + "," + this.ddl_sfu_parent.SelectedValue + "," + this.ddl_sfu_no.SelectedValue;
            string opt = this.ddl_opt_status.SelectedValue;

            int key = 0;
            string value = "";
            if (this.rb_all.Checked)
            {
                key = (int)Key.All;
            }
            if (this.rb_workid.Checked)
            {
                key = (int)Key.workId;
                value = this.tbox_workid.Text;
            }
            if (this.rb_account.Checked)
            {
                key = (int)Key.account;
                value = this.tbox_account.Text;
            }
            if (this.rb_people.Checked)
            {
                key = (int)Key.people;
                value = string.Join(",", this.DepartTreeListBox_people.ItemsValue);
            }
            if (this.rb_depart.Checked)
            {
                key = (int)Key.depart;
                value = string.Join(",", this.DepartTreeListBox_depart.ItemsValue);
            }

            string url = "350301-1.aspx?date=" + date + "&sfu=" + sfu + "&opt=" + opt + "&key=" + key + "&value=" + value;
            JsUtil.RedirectJs(this, url);
            
        }
    }


    private enum Key
    {
        All = 1, workId = 2, account = 3, people = 4, depart = 5
    }

    private bool CheckUI()
    {
        try
        {
            DateTime sd = this.calendar1._ADDate;
            DateTime ed = this.calendar2._ADDate;
            if (sd > ed)
            {
                JsUtil.AlertJs(this, "起始日期需早於迄日期!");
                return false;
            }
        }
        catch
        {
            JsUtil.AlertJs(this,"日期錯誤!");
            return false;
        }

        if (this.rb_workid.Checked && this.tbox_workid.Text.Trim().Length == 0)
        {
            JsUtil.AlertJs(this, "請輸入人事編號!");
            return false;
        }

        if (this.rb_account.Checked && this.tbox_account.Text.Trim().Length == 0)
        {
            JsUtil.AlertJs(this, "請輸入員工帳號!");
            return false;
        }

        if (this.rb_people.Checked && this.DepartTreeListBox_people.Items.Count == 0)
        {
            JsUtil.AlertJs(this, "請選擇人員!");
            return false;
        }

        if (this.rb_depart.Checked && this.DepartTreeListBox_depart.Items.Count == 0)
        {
            JsUtil.AlertJs(this, "請選擇單位!");
            return false;
        }

        return true;
    }

    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        JsUtil.RedirectJs(this, "350301.aspx");
    }

    protected void ddl_sys_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddl_sys.SelectedValue == "0")
        {
            this.ddl_sfu_parent.Items.Clear();
            this.ddl_sfu_parent.Items.Add(new ListItem("全部", "0"));
            this.ddl_sfu_parent.Enabled = false;
        }
        else
        {
            this.ObjectDataSource2.SelectParameters["sys_no"].DefaultValue = this.ddl_sys.SelectedValue;
            this.ddl_sfu_parent.Items.Clear();
            this.ddl_sfu_parent.DataBind();
            this.ddl_sfu_parent.Items.Insert(0,new ListItem("全部", "0"));
            this.ddl_sfu_parent.Enabled = true;
        }

        this.ddl_sfu_no.Items.Clear();
        this.ddl_sfu_no.Items.Add(new ListItem("全部", "0"));
        this.ddl_sfu_no.Enabled = false;

    }
    protected void ddl_sfu_parent_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (this.ddl_sfu_parent.SelectedValue == "0")
        {
            this.ddl_sfu_no.Items.Clear();
            this.ddl_sfu_no.Items.Add(new ListItem("全部", "0"));
            this.ddl_sfu_no.Enabled = false;
        }
        else
        {
            this.ObjectDataSource3.SelectParameters["sfu_no"].DefaultValue = this.ddl_sfu_parent.SelectedValue;
            this.ddl_sfu_no.Items.Clear();
            this.ddl_sfu_no.DataBind();
            this.ddl_sfu_no.Items.Insert(0, new ListItem("全部", "0"));
            this.ddl_sfu_no.Enabled = true;
        }
    }
}