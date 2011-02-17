using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using NXEIP.MyGov;

/// <summary>
/// 功能名稱：管理作業 / 場地管理 / 場地審核
/// 功能編號：30/300400/300403
/// 撰寫者：Lina
/// 撰寫時間：2010/12/21
/// </summary>
public partial class _30_300400_300403_1 : System.Web.UI.Page
{
    DBObject dbo = new DBObject();
    CheckObject checkobj = new CheckObject();
    SessionObject sobj = new SessionObject();
    ChangeObject changeobj = new ChangeObject();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if (Request["no"] != null) this.lab_no.Text = Request["no"];

            this.Navigator1.SubFunc = "審核";
            string sqlstr = "SELECT petition.pet_stime, petition.pet_etime, rooms.roo_name, spot.spo_name, departments.dep_name, people.peo_name,petition.pet_applyuid "
            + " from petition INNER JOIN rooms ON petition.roo_no = rooms.roo_no INNER JOIN spot ON rooms.spo_no = spot.spo_no INNER JOIN"
            + " departments ON petition.pet_depno = departments.dep_no INNER JOIN people ON petition.pet_applyuid = people.peo_uid"
            + " where petition.pet_no=" + this.lab_no.Text;
            DataTable dt = new DataTable();
            dt = dbo.ExecuteQuery(sqlstr);
            if (dt.Rows.Count > 0)
            {
                this.lab_spot.Text = dt.Rows[0]["spo_name"].ToString();
                this.lab_rooms.Text = dt.Rows[0]["roo_name"].ToString();
                this.lab_depart.Text = dt.Rows[0]["dep_name"].ToString();
                this.lab_applyuser.Text = dt.Rows[0]["peo_name"].ToString();
                this.lab_applyuid.Text = dt.Rows[0]["pet_applyuid"].ToString();
                this.lab_sdate.Text = changeobj.ADDTtoROCDT(Convert.ToDateTime(dt.Rows[0]["pet_stime"].ToString()).ToString("yyyy-MM-dd HH:mm"));
                this.lab_edate.Text = Convert.ToDateTime(dt.Rows[0]["pet_etime"].ToString()).ToString("HH:mm");
            }
        }
    }
    #region 輸入值檢查
    private bool CheckInputValue()
    {
        #region 不核可原因
        if (this.rbl_apply.SelectedValue.Equals("3"))
        {
            if (string.IsNullOrEmpty(this.txt_signmemo.Text))
            {
                ShowMSG("請輸入 不核可原因");
                return false;
            }
            else if (!checkobj.IsValidLen(this.txt_signmemo.Text.Trim(), 200))
            {
                ShowMSG("不核可原因 長度不可超過200個數文字");
                return false;
            }
        }
        #endregion

        return true;
    }
    #endregion

    #region 確定
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        string aMSG = "";
        try
        {
            string msg = "";
            if (CheckInputValue())
            {
                string UpdStr = "update petition set pet_apply='" + this.rbl_apply.SelectedValue + "',pet_signuid=" + sobj.sessionUserID + ",pet_signdate=getdate(),pet_signmemo=N'" + this.txt_signmemo.Text + "' where pet_no=" + this.lab_no.Text;
                dbo.ExecuteNonQuery(UpdStr);

                #region 寄訊息
                string subject = "", body = "";
                if (this.rbl_apply.SelectedValue.Equals("2"))
                {
                    subject = "您於 " + this.lab_sdate.Text + " ~ " + this.lab_edate.Text + " 預約的場地「" + this.lab_rooms.Text + "」已被管理者審核通過!";
                    body = "您於 " + this.lab_sdate.Text + " ~ " + this.lab_edate.Text + " 預約的場地「" + this.lab_rooms.Text + "」已被管理者審核通過！請見「場地申請」瀏覽場地預約狀態。";
                }
                else
                {
                    subject = "您於 " + this.lab_sdate.Text + " ~ " + this.lab_edate.Text + " 預約的場地「" + this.lab_rooms.Text + "」已被管理者審核不通過!";
                    body = "您於 " + this.lab_sdate.Text + " ~ " + this.lab_edate.Text + " 預約的場地「" + this.lab_rooms.Text + "」已被管理者審核不通過！（原因為:"+this.txt_signmemo.Text+"）請見「場地管理」瀏覽場地預約狀態或洽場地管理者。";
                }
                MyMessageUtil.send(subject, Convert.ToInt32(this.lab_applyuid.Text), body, "", "", EIPGroup.EIP_Todo_VerifyPlace);
                #endregion
                msg = "審核完成";
                //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
                new OperatesObject().ExecuteOperates(300403, sobj.sessionUserID, 3, "編號：" + this.lab_no.Text + ",審核狀態：" + this.rbl_apply.SelectedItem.Text + ",原因：" + this.txt_signmemo.Text);

                this.Page.ClientScript.RegisterStartupScript(typeof(_30_300400_300403_1), "closeThickBox", "self.parent.update('" + msg + "');self.parent.location.reload(true);self.parent.tb_remove();", true);
            }
        }
        catch (Exception ex)
        {
            aMSG = "功能名稱：場地審核-" + this.Navigator1.SubFunc + "<br>錯誤訊息:" + ex.ToString();
            Response.Write(aMSG);
        }
    }
    #endregion

    #region 顯示錯誤訊息
    private void ShowMSG(string msg)
    {
        string script = "<script>alert('" + msg + "');</script>";
        this.ClientScript.RegisterStartupScript(this.GetType(), "msg", script);
    }
    #endregion
}