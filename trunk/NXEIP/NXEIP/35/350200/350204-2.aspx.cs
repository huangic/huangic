using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using NXEIP.DAO;
using Entity;

public partial class _35_350200_350204_2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.Navigator1.SubFunc = "人員修改";

            this.calendar3.Visible = false;

            int peo_uid = Convert.ToInt32(Request["peo_uid"]);

            //人員資料
            Entity.people peopleData = new PeopleDAO().GetByPeoUID(peo_uid);

            this.tbox_cardid.Text = peopleData.peo_idcard;
            this.tbox_name.Text = peopleData.peo_name;
            this.tbox_account.Text = peopleData.peo_account;
            this.tbox_workid.Text = peopleData.peo_workid;

            //部門
            this.DepartTreeTextBox1.Clear();
            this.DepartTreeTextBox1.Add(peopleData.dep_no.ToString());

            try
            {
                this.calendar1._ADDate = (System.DateTime)peopleData.peo_arrivedate;
            }
            catch { }

            try
            {
                this.calendar2._ADDate = (System.DateTime)peopleData.peo_birthday;
            }
            catch
            {
            }

            this.tbox_mail.Text = peopleData.peo_email;
            this.tbox_addr.Text = peopleData.peo_addr;
            this.tbox_tel.Text = peopleData.peo_tel;
            this.tbox_otel.Text = peopleData.peo_officetel;
            this.tbox_extension.Text = peopleData.peo_extension;
            this.tbox_memo.Text = peopleData.peo_memo;

            //職稱
            this.ddl_profess.DataBind();
            try
            {
                this.ddl_profess.Items.FindByValue(peopleData.peo_pfofess.ToString()).Selected = true;
            }
            catch { }
            
            //人員類別
            this.ddl_ptype.DataBind();
            try
            {
                this.ddl_ptype.Items.FindByValue(peopleData.peo_ptype.ToString()).Selected = true;
            }
            catch { }

            //在職情況
            this.ddl_jobtype.DataBind();
            try
            {
                this.ddl_jobtype.Items.FindByValue(peopleData.peo_jobtype.ToString()).Selected = true;
            }
            catch { }

            string code = new UtilityDAO().Get_TypesNumber(peopleData.peo_jobtype.Value);
            if (!code.Equals("1"))
            {
                this.calendar3.Visible = true;
                this.calendar3._ADDate = (System.DateTime)peopleData.peo_leave;
            }

            //照片
            this.FileUpload1.Show_Pic(peo_uid);
        }
    }
    
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (this.Check_Input())
        {
            int peo_uid = Convert.ToInt32(Request["peo_uid"]);

            PeopleDAO peopleDao = new PeopleDAO();
            Entity.people peopleData = peopleDao.GetByPeoUID(peo_uid);

            peopleData.peo_idcard = this.tbox_cardid.Text;
            peopleData.peo_name = this.tbox_name.Text;
            peopleData.peo_account = this.tbox_account.Text;
            peopleData.peo_workid = this.tbox_workid.Text;

            //照片
            if (!this.FileUpload1.Get_FileName.Equals(""))
            {
                peopleData.peo_filename = this.FileUpload1.Get_FileName;
                peopleData.peo_picture = this.FileUpload1.Get_FileByte;
            }

            peopleData.peo_pfofess = Convert.ToInt32(this.ddl_profess.SelectedValue);
            peopleData.peo_ptype = Convert.ToInt32(this.ddl_ptype.SelectedValue);

            //部門
            peopleData.dep_no = Convert.ToInt32(this.DepartTreeTextBox1.Value);

            //
            peopleData.peo_arrivedate = this.calendar1._ADDate;

            try
            {
                peopleData.peo_birthday = this.calendar2._ADDate;
            }
            catch
            {
            }

            peopleData.peo_email = this.tbox_mail.Text;
            peopleData.peo_addr = this.tbox_addr.Text;
            peopleData.peo_tel = this.tbox_tel.Text;
            peopleData.peo_officetel = this.tbox_otel.Text;
            peopleData.peo_extension = this.tbox_extension.Text;
            peopleData.peo_memo = this.tbox_memo.Text;


            peopleData.peo_jobtype = Convert.ToInt32(this.ddl_jobtype.SelectedValue);
            string code = new UtilityDAO().Get_TypesNumber(int.Parse(this.ddl_jobtype.SelectedValue));
            if (!code.Equals("1"))
            {
                peopleData.peo_leave = this.calendar3._ADDate;
            }

            peopleData.peo_createuid = Convert.ToInt32(new SessionObject().sessionUserID);
            peopleData.peo_createtime = System.DateTime.Now;

            //save data
            peopleDao.Update();

            //操作記錄
            new OperatesObject().ExecuteOperates(350205, new SessionObject().sessionUserID, 3, "修改人員 姓名:" + peopleData.peo_name);

            string url = "350204-1.aspx?jobtype=" + Request["jobtype"] + "&ptype=" + Request["ptype"] + "&workid=" + Request["workid"] + "&name=" + Request["name"] + "&account=" + Request["account"] + "&profess=" + Request["profess"] + "&depar=" + Request["depar"] + "&people=" + Request["people"] + "&pageIndex=" + Request["pageIndex"];
            this.ShowMsg_URL("人員修改完成!", url);
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

        string code = new UtilityDAO().Get_TypesNumber(int.Parse(this.ddl_jobtype.SelectedValue));
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
        Response.Redirect("350204-1.aspx?jobtype=" + Request["jobtype"] + "&ptype=" + Request["ptype"] + "&workid=" + Request["workid"] + "&name=" + Request["name"] + "&account=" + Request["account"] + "&profess=" + Request["profess"] + "&depar=" + Request["depar"] + "&people=" + Request["people"] + "&pageIndex=" + Request["pageIndex"]);
    }

    protected void ddl_jobtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        string code = new UtilityDAO().Get_TypesNumber(int.Parse(this.ddl_jobtype.SelectedValue)); 
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
        this.ClientScript.RegisterStartupScript(this.GetType(),"msg", script);
    }

    private void ShowMsg_URL(string msg, string url)
    {
        string script = "<script>window.alert('" + msg + "');location.href='" + url + "'</script>";
        this.ClientScript.RegisterStartupScript(this.GetType(), "MyScript", script);
    }
}