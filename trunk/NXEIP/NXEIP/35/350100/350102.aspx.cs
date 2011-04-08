using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using NXEIP.DAO;
using Entity;

public partial class _35_350100_350102 : System.Web.UI.Page
{

    private NXEIPEntities model = new NXEIPEntities();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.Panel1.Visible = true;
            this.Panel2.Visible = false;

            this.DepartTreeListBox_depart.Clear();
            this.DepartTreeListBox_people.Clear();
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
            foreach (var people in this.DepartTreeListBox_people.Items)
            {
                this.Accounts_process(int.Parse(people.Key));
                showMsg = true;
            }
        }

        //部門
        if (this.rbl_dep.Checked)
        {
            foreach (var depart in this.DepartTreeListBox_depart.Items)
            {
                int dep_no = Convert.ToInt32(depart.Key);

                //部門人員
                var data = (from d in model.people where d.dep_no == dep_no select d);
                foreach (var item in data)
                {
                    this.Accounts_process(item.peo_uid);
                    showMsg = true;
                }
            }
        }

        if (showMsg)
        {
            this.ShowMsg_URL("設定完成!", "350102.aspx");
        }
        else
        {
            this.ShowMsg("設定失敗!");
        }
    }

    private void Accounts_process(int peo_uid)
    {

        //尋找人員帳號acc_no
        int acc_no = (from d in model.accounts where d.peo_uid == peo_uid select d.acc_no).FirstOrDefault();

        //刪除人員帳號權限
        var roleData = (from d in model.roleaccount where d.acc_no == acc_no select d);
        foreach (var d in roleData)
        {
            model.roleaccount.DeleteObject(d);
        }
        model.SaveChanges();

        //尋找 rac_no 最大值
        int rac_no = 0;
        try
        {
            rac_no = (from d in model.roleaccount select d.rac_no).Max();
        }
        catch { }

        //所設定之角色
        for (int i = 0; i < this.lbox_set.Items.Count; i++)
        {
            //新增角色至帳號
            int rol_no = int.Parse(this.lbox_set.Items[i].Value);
            rac_no++;
            roleaccount data = new roleaccount();
            data.rol_no = rol_no;
            data.rac_no = rac_no;
            data.acc_no = acc_no;
            model.roleaccount.AddObject(data);
            model.SaveChanges();
        }
    }

    /// <summary>
    /// 取消設定
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (this.lbox_set.SelectedItem != null)
        {
            this.lbox_noset.Items.Add(this.lbox_set.SelectedItem);
            this.lbox_set.Items.Remove(this.lbox_set.SelectedItem);

            //取消選擇狀態
            this.lbox_noset.SelectedItem.Selected = false;
        }
    }

    /// <summary>
    /// 設定角色
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (this.lbox_noset.SelectedItem != null)
        {
            this.lbox_set.Items.Add(this.lbox_noset.SelectedItem);
            this.lbox_noset.Items.Remove(this.lbox_noset.SelectedItem);

            //取消選擇狀態
            this.lbox_set.SelectedItem.Selected = false;

        }
    }

    protected void but_cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("350102.aspx");
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
