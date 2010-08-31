using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity;
using System.Linq.Dynamic;

public partial class _35_350200_350204 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {

        }
    }

    private void ShowMSG(string msg)
    {
        this.Page.RegisterStartupScript("msg", "<script>alert('" + msg + "');</script>");
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (!this.cbox_account.Checked && !this.cbox_dearp.Checked && !this.cbox_name.Checked && !this.cbox_people.Checked && !this.cbox_profess.Checked && !this.cbox_ptype.Checked && !this.cbox_work.Checked && !this.cbox_workid.Checked)
        {
            this.ShowMSG("請至少勾選一項條件!");
        }
        else
        { 
            
            string jobtype="",ptype = "", workid = "", name = "",account="",profess="",depar="",people="";

            //在職狀況
            if (this.cbox_work.Checked)
            {
                jobtype = this.ddl_work.SelectedValue;
            }

            //姓名
            if (this.cbox_name.Checked)
            {
                if (string.IsNullOrEmpty(this.tbox_name.Text))
                {
                    this.ShowMSG("請輸入姓名關鍵字!");
                    return;
                }
                else
                {
                    name = this.tbox_name.Text;
                }
            }

            //人事編號
            if (this.cbox_workid.Checked)
            {
                if (string.IsNullOrEmpty(this.tbox_workid.Text))
                {
                    this.ShowMSG("請輸入人事編號!");
                    return;
                }
                else
                {
                    workid = this.tbox_workid.Text;
                }
            }

            //員工帳號
            if (this.cbox_account.Checked)
            {
                if (string.IsNullOrEmpty(this.tbox_account.Text))
                {
                    this.ShowMSG("請輸入員工帳號!");
                    return;
                }
                else
                {
                    account = this.tbox_account.Text;
                }
            }

            //人員類別
            if (this.cbox_ptype.Checked)
            {
                ptype = this.ddl_ptype.SelectedValue;
            }

            //職稱
            if (this.cbox_profess.Checked)
            {
                profess = this.ddl_profess.SelectedValue;
            }

            //部門
            if (this.cbox_dearp.Checked)
            {
                if (this.jQueryDepartTree1.Items.Count == 0)
                {
                    this.ShowMSG("請選擇任一部門!");
                    return;
                }
                else
                {
                    foreach (var d in this.jQueryDepartTree1.Items)
                    {
                        if (depar.Length == 0)
                        {
                            depar = d.Key;
                        }
                        else
                        {
                            depar += "," + d.Key;
                        }
                    }
                }
            }

            //人員
            if (this.cbox_people.Checked)
            {
                if (this.jQueryPeopleTree1.Items.Count == 0)
                {
                    this.ShowMSG("請選擇任一人員!");
                    return;
                }
                else
                {
                    foreach (var d in this.jQueryPeopleTree1.Items)
                    {
                        if (people.Length == 0)
                        {
                            people = d.Key;
                        }
                        else
                        {
                            people += "," + d.Key;
                        }
                    }
                }
            }

            //操作記錄
            new OperatesObject().ExecuteOperates(350204, new SessionObject().sessionUserID, 2, "");

            Response.Redirect("350204-1.aspx?jobtype=" + jobtype + "&ptype=" + ptype + "&workid=" + workid + "&name=" + name + "&account=" + account + "&profess=" + profess + "&depar=" + depar + "&people=" + people);
        }


    }
    protected void Button2_Click(object sender, EventArgs e)
    {

    }
}