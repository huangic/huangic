using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


/// <summary>
/// 功能名稱：管理作業 / 車輛管理 / 派車審核
/// 功能編號：30/301100/301103
/// 撰寫者：Lina
/// 撰寫時間：2011/03/11
/// </summary>
public partial class _30_301100_301103_1 : System.Web.UI.Page
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
            string sqlstr = "SELECT m03.m03_sdate, m03.m03_edate, m02.m02_number, m01.m01_name, departments.dep_name, people.peo_name,m03.m03_peouid "
            + " from m03 INNER JOIN m02 ON m03.m03_m02no = m02.m02_no INNER JOIN m01 ON m03.m03_type = m01.m01_no INNER JOIN"
            + " departments ON m03.m03_depno = departments.dep_no INNER JOIN people ON m03.m03_peouid = people.peo_uid"
            + " where m03.m03_no=" + this.lab_no.Text;
            DataTable dt = new DataTable();
            dt = dbo.ExecuteQuery(sqlstr);
            if (dt.Rows.Count > 0)
            {
                this.lab_chekuan.Text = dt.Rows[0]["m01_name"].ToString();
                this.lab_car.Text = dt.Rows[0]["m02_number"].ToString();
                this.lab_depart.Text = dt.Rows[0]["dep_name"].ToString();
                this.lab_applyuser.Text = dt.Rows[0]["peo_name"].ToString();
                this.lab_applyuid.Text = dt.Rows[0]["m03_peouid"].ToString();
                this.lab_sdate.Text = changeobj.ADDTtoROCDT(Convert.ToDateTime(dt.Rows[0]["m03_sdate"].ToString()).ToString("yyyy-MM-dd HH:mm"));
                this.lab_edate.Text = Convert.ToDateTime(dt.Rows[0]["m03_edate"].ToString()).ToString("HH:mm");
            }
        }
    }

    #region 確定
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        string aMSG = "";
        try
        {
            string msg = "";

            string UpdStr = "update m03 set m03_verify='" + this.rbl_apply.SelectedValue + "',m03_createuid=" + sobj.sessionUserID + ",m03_createtime=getdate() where m03_no=" + this.lab_no.Text;
            dbo.ExecuteNonQuery(UpdStr);

            #region 寄訊息
            string subject = "", body = "";
            if (this.rbl_apply.SelectedValue.Equals("2"))
            {
                //您於 100-03-12 12:00-14:00 申請派用車號「xxx」管理者已審核通過。
                subject = "您於 " + this.lab_sdate.Text + " ~ " + this.lab_edate.Text + " 申請派用車號「" + this.lab_car.Text + "」管理者已審核通過!";
                body = "您於 " + this.lab_sdate.Text + " ~ " + this.lab_edate.Text + " 預約「" + this.lab_car.Text + "」管理者已審核通過。";
            }
            else
            {
                //您於 100-03-12 12:00-14:00 申請派用車號「xxx」管理者審核不通過，請洽車輛保管者。 
                subject = "您於 " + this.lab_sdate.Text + " ~ " + this.lab_edate.Text + " 申請派用車號「" + this.lab_car.Text + "」管理者審核不通過，請洽車輛保管者。";
                body = "您於 " + this.lab_sdate.Text + " ~ " + this.lab_edate.Text + " 申請派用車號「" + this.lab_car.Text + "」管理者審核不通過，請洽車輛保管者。";
            }
            #endregion

            new PersonalMessageUtil().SendMessage(subject, body, "", Convert.ToInt32(this.lab_applyuid.Text), Convert.ToInt32(sobj.sessionUserID), true, false, false);

            msg = "審核完成";
            //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
            new OperatesObject().ExecuteOperates(301103, sobj.sessionUserID, 3, "編號：" + this.lab_no.Text + ",審核狀態：" + this.rbl_apply.SelectedItem.Text);

            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "closeThickBox", "self.parent.update('" + msg + "');", true);

        }
        catch (Exception ex)
        {
            aMSG = "功能名稱：派車審核" + this.Navigator1.SubFunc + "<br>錯誤訊息:" + ex.ToString();
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