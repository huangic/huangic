using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;

public partial class _10_100100_100102 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            SessionObject sobj = new SessionObject();
            UtilityDAO udao = new UtilityDAO();

            int peo_uid = Convert.ToInt32(sobj.sessionUserID);

            //人員資料
            Entity.people pdata = new PeopleDAO().GetByPeoUID(peo_uid);

            this.lab_idcard.Text = pdata.peo_idcard;
            this.lab_name.Text = pdata.peo_name;
            this.lab_account.Text = pdata.peo_account;
            this.lab_workid.Text = pdata.peo_workid;

            this.DropDownList1.DataBind();
            if (pdata.peo_pfofess.HasValue)
            {
                ListItem myitem = this.DropDownList1.Items.FindByValue(pdata.peo_pfofess.Value.ToString());
                if (myitem != null)
                {
                    this.DropDownList1.Items.FindByValue(myitem.Value).Selected = true;
                }
            }

            this.lab_ptyname.Text = udao.Get_TypesCName(pdata.peo_ptype.Value);
            this.lab_depart.Text = sobj.sessionUserDepartName;

            try
            {
                this.calendar1._ADDate = (System.DateTime)pdata.peo_arrivedate;
            }
            catch { }

            try
            {
                this.calendar2._ADDate = (System.DateTime)pdata.peo_birthday;
            }
            catch {}

            this.tbox_addr.Text = pdata.peo_addr;
            this.tbox_mail.Text = pdata.peo_email;
            this.tbox_offext.Text = pdata.peo_extension;
            this.tbox_offtel.Text = pdata.peo_officetel;
            this.tbox_phone.Text = pdata.peo_cellphone;
            this.tbox_tel.Text = pdata.peo_tel;

            //照片
            this.FileUpload1.Show_Pic(peo_uid);

        }
    }
    
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime peo_arrivedate = this.calendar1._ADDate;
        }
        catch
        {
            JsUtil.AlertJs(this,"到職日期錯誤");
            return;
        }
        try
        {
            DateTime peo_birthday = this.calendar2._ADDate;
        }
        catch
        {
            JsUtil.AlertJs(this, "生日日期錯誤");
            return;
        }

        if (this.DropDownList1.SelectedValue == "0")
        {
            JsUtil.AlertJs(this,"謮選擇職稱!");
            return;
        }

        int peo_uid = Convert.ToInt32(new SessionObject().sessionUserID);

        PeopleDAO peopleDao = new PeopleDAO();
        people pdata = peopleDao.GetByPeoUID(peo_uid);

        pdata.peo_addr = this.tbox_addr.Text;
        pdata.peo_email = this.tbox_mail.Text;
        pdata.peo_extension = this.tbox_offext.Text;
        pdata.peo_officetel = this.tbox_offtel.Text;
        pdata.peo_cellphone = this.tbox_phone.Text;
        pdata.peo_tel = this.tbox_tel.Text;
        pdata.peo_arrivedate = this.calendar1._ADDate;
        pdata.peo_birthday = this.calendar2._ADDate;
        pdata.peo_pfofess = int.Parse(this.DropDownList1.SelectedValue);

        //照片
        if (!this.FileUpload1.Get_FileName.Equals(""))
        {
            pdata.peo_filename = this.FileUpload1.Get_FileName;
            pdata.peo_picture = this.FileUpload1.Get_FileByte;
        }

        //儲存
        peopleDao.Update();

        //操作記錄
        new OperatesObject().ExecuteOperates(100102, new SessionObject().sessionUserID, 3, "修改個人資訊");

        JsUtil.AlertAndRedirectJs(this, "修改完成!!", "100102.aspx");
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("100102.aspx");
    }
}