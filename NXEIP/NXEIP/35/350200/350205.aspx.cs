using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;
using System.Globalization;

public partial class _35_350200_350205 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.calendar3.Visible = false;

            
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
            newPeople.peo_picture = this.FileUpload1.Get_FileByte;
            newPeople.peo_filename = this.FileUpload1.Get_FileName;

            //部門
            int dep_no = Convert.ToInt32(this.jQueryDepartTree1.Items[0].Key);
            newPeople.departments = (from d in model.departments where d.dep_no == dep_no select d).First();

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
            if (!this.ddl_jobtype.SelectedValue.Equals("1"))
            {
                newPeople.peo_leave = this.calendar3._ADDate;
            }

            string peo_uid = new SessionObject().sessionUserID;
            newPeople.peo_createuid = Convert.ToInt32(peo_uid);
            newPeople.peo_createtime = System.DateTime.Now;

            //save data
            model.AddTopeople(newPeople);
            model.SaveChanges();

            //操作記錄
            new Operates().ExecuteOperates(350205, peo_uid, 1, "新增人員-peo_uid:" + newPeople.peo_uid);
            
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
        if (this.jQueryDepartTree1.Items.Count == 0)
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

        if (!this.ddl_jobtype.SelectedValue.Equals("1"))
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
        Server.Transfer("350205.aspx");
    }

    protected void ddl_jobtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddl_jobtype.SelectedValue.Equals("1"))
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
        this.Page.RegisterStartupScript("msg", "<script>alert('" + msg + "');</script>");
    }
}
