using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.Lib;
using System.Text.RegularExpressions;
using NXEIP.DAO;
using Entity;

public partial class applyAccount : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.DepartTreeTextBox1.Clear();


        }
    }

    protected void btn_ok_Click(object sender, EventArgs e)
    {
        if (CheckUI())
        {
            using (NXEIPEntities mode = new NXEIPEntities())
            {
                applys data = new applys();

                data.app_idcard = this.tbox_idcard.Text.ToUpper();
                data.app_name = this.tbox_name.Text;
                data.app_depno = int.Parse(this.DepartTreeTextBox1.Value);
                data.app_profess = int.Parse(this.ddl_pro.SelectedValue);
                data.app_email = this.tbox_mail.Text;
                data.app_login = this.tbox_account.Text;
                data.app_password = this.tbox_pass.Text;
                data.app_date = DateTime.Now;
                data.app_cellphone = this.tbox_cellph.Text;
                data.app_officetel = this.tbox_officetel.Text;
                data.app_check = "0";

                mode.applys.AddObject(data);
                mode.SaveChanges();

                //發送審核通知
                PersonalMessageUtil pMsg = new PersonalMessageUtil();
                string body = string.Format("{0} {1}於{2}日申請系統帳號，請您至帳號審核功能進行審核", this.DepartTreeTextBox1.Items[0].Value, this.tbox_name.Text, new ChangeObject()._ADtoROCDT(DateTime.Now));
                int[] to = (from d in mode.manager where d.man_type == "2" select d.peo_uid).ToArray();

                for (int i = 0; i < to.Length; i++)
                {
                    pMsg.SendMessage("帳號申請審核", body, "", to[i], 1, true, false, false);
                }

                JsUtil.UpdateParentJs(this, "帳號申請完成，請等待管理員審核通知!!");

            }

        }

    }

    private bool CheckUI()
    {
        UtilityDAO dao = new UtilityDAO();

        if (!this.tbox_idcard.Text.IsIDNumber())
        {
            JsUtil.AlertJs(this, "請輸入正確身份證!");
            return false;
        }
        else
        {
            //檢查身份證是否重覆
            if (dao.CheckIDCard(this.tbox_idcard.Text))
            {
                JsUtil.AlertJs(this, "身份證字號重覆!");
                return false;
            }
        }

        //檢查帳號
        if (!this.tbox_account.Text.IsPowerAccount())
        {
            JsUtil.AlertJs(this, "帳號需各包含一個數字,英文字母,字串長度在4~12碼!");
            return false;
        }
        else
        {
            //檢查是否重覆
            if (dao.CheckAccount(this.tbox_account.Text))
            {
                JsUtil.AlertJs(this, "帳號重覆!");
                return false;
            }
        }

        //檢查密碼
        if (!this.tbox_pass.Text.IsPowerPassWD())
        {
            JsUtil.AlertJs(this, "密碼需各包含一個數字,英文字母,特殊符號,字串長度在4~12碼!");
            return false;
        }

        if (string.IsNullOrEmpty(this.DepartTreeTextBox1.Value))
        {
            JsUtil.AlertJs(this, "請選擇服務單位!");
            return false;
        }

        if (this.ddl_pro.SelectedValue == "0")
        {
            JsUtil.AlertJs(this, "請選擇職稱!");
            return false;
        }

        //電話檢查
        if (!string.IsNullOrEmpty(this.tbox_officetel.Text))
        {
            //2331111#1234
            Regex regex = new Regex(@"^(\d{7,8})#(\d{1,4})$");
            if (!regex.IsMatch(this.tbox_officetel.Text))
            {
                JsUtil.AlertJs(this, "請輸入正確電話格式!!");
                return false;
            }
        }

        return true;
    }
}