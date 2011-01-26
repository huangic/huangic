using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _10_100200_100201_1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.lab_name.Text = new SessionObject().sessionUserName;

            this.DepartTreeListBox_people.Clear();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        bool check = true;
        for (int i = 0; i < this.CheckBoxList1.Items.Count; i++)
        {
            if (this.CheckBoxList1.Items[i].Selected)
            {
                check = false;
            }
        }

        if (check)
        {
            JsUtil.AlertJs(this, "請選擇發送型式!");
            return;
        }

        if (this.tbox_subject.Text.Trim().Length == 0)
        {
            JsUtil.AlertJs(this, "請輸入訊息主旨!");
            return;
        }
        if (this.tbox_subject.Text.Trim().Length > 100)
        {
            JsUtil.AlertJs(this, "訊息主旨文字長度為100字!");
            return;
        }
        if (this.tbox_body.Text.Trim().Length == 0)
        {
            JsUtil.AlertJs(this, "請輸入訊息內容!");
            return;
        }
        if (this.tbox_link.Text.Trim().Length > 100)
        {
            JsUtil.AlertJs(this, "連結網址文字長度為100字!");
            return;
        }

        if (this.DepartTreeListBox_people.Items == null || this.DepartTreeListBox_people.Items.Count == 0)
        {
            JsUtil.AlertJs(this,"請選擇任一人員!");
            return;
        }

        PersonalMessageUtil msgUtil = new PersonalMessageUtil();
        
        int me = int.Parse(new SessionObject().sessionUserID);
        bool sysMsg = this.CheckBoxList1.Items[0].Selected;
        bool email = this.CheckBoxList1.Items[1].Selected;
        bool phone = false;
        
        for (int i = 0; i < this.DepartTreeListBox_people.Items.Count; i++)
        {
            int to = int.Parse(this.DepartTreeListBox_people.Items[i].Key);
            msgUtil.SendMessage(this.tbox_subject.Text, this.tbox_body.Text, this.tbox_link.Text, to, me, sysMsg, email, phone);

        }

        JsUtil.AlertAndRedirectJs(this, "發送完成!", "100201.aspx");




    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("100201.aspx");
    }
}