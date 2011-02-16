using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using NXEIP.DAO;
using Entity;

public partial class _10_100300_100301_5 : System.Web.UI.Page
{
    ChangeObject changeobj = new ChangeObject();
    SessionObject sobj = new SessionObject();
    DBObject dbo = new DBObject();
    CheckObject checkobj = new CheckObject();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
            new OperatesObject().ExecuteOperates(100301, sobj.sessionUserID, 2, "預約首長行程審核");

            #region 初始值--日期人員編號
            if (Request["source"] != null) this.lab_source.Text = Request["source"];
            if (Request["today"] != null)
            {
                this.lab_today.Text = Request["today"];
                this.lab_depart.Text = Request["depart"];
            }
            bool isUpdate = false;

            if (Request["peo_uid"] != null)
            {
                this.lab_peo_uid.Text = Request["peo_uid"];
                this.lab_UserName.Text = new PeopleDAO().GetPeopleNameByUid(Convert.ToInt32(this.lab_peo_uid.Text));

                #region 檢查是否可新增或修改、或查看
                DataTable dt = new DataTable();
                string sqlstr = "SELECT c01_no from c01 where peo_uid=" + sobj.sessionUserID + " and c02_peouid=" + this.lab_peo_uid.Text;
                dt = dbo.ExecuteQuery(sqlstr);
                if (dt.Rows.Count > 0)
                {
                    isUpdate = true;
                }
                #endregion
            }
            else
            {
                this.lab_peo_uid.Text = sobj.sessionUserID;
                this.lab_UserName.Text = sobj.sessionUserName;
                isUpdate = true;
            }
            #endregion

            #region 初始值--時間、單據編號
            if (isUpdate)
            {
                if (Request["no"] != null)
                {
                    this.Navigator1.SubFunc = "修改";
                    #region 修改
                    this.lab_no.Text = Request["no"];

                    Entity.c02 c02Data = new C02DAO().GetByC02No(Convert.ToInt32(this.lab_peo_uid.Text), Convert.ToInt32(this.lab_no.Text));
                    if (c02Data != null)
                    {
                        if (c02Data.c02_setuid == Convert.ToInt32(sobj.sessionUserID) || c02Data.peo_uid == Convert.ToInt32(sobj.sessionUserID))
                        {
                            this.lab_sdate.Text = changeobj.ADDTtoROCDT(c02Data.c02_sdate.ToString("yyyy-MM-dd HH:mm"));
                            this.lab_edate.Text = changeobj.ADDTtoROCDT(c02Data.c02_edate.ToString("yyyy-MM-dd HH:mm"));
                            this.lab_bgcolor.Text = c02Data.c02_bgcolor;
                            this.lab_title.Text = c02Data.c02_title;
                            this.lab_place.Text = c02Data.c02_place;
                            if (c02Data.c02_project != null) this.lab_project.Text = c02Data.c02_project;
                            if (c02Data.c02_result != null) this.lab_result.Text = c02Data.c02_result;
                        }
                    }
                    #endregion
                }
                else
                {
                    this.btn_submit.Visible = false;
                    this.btn_submit.Enabled = false;
                }
            }
            else
            {
                this.btn_submit.Visible = false;
                this.btn_submit.Enabled = false;
            }
            #endregion
        }
    }

    #region 取消
    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        this.Page.ClientScript.RegisterStartupScript(typeof(_10_100300_100301_5), "closeThickBox", "self.parent.location.reload(true);self.parent.tb_remove();", true);
    }
    #endregion

    #region 確定
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        string aMSG = "";   //記錄錯誤訊息
        try
        {
            #region 檢查輸入值--審核狀態、原因
            if (this.rbl_check.SelectedIndex < 0)
            {
                ShowMSG("請選擇　審核狀態");
                return;
            }
            else if (this.rbl_check.SelectedValue.Equals("2"))
            {
                if (this.txt_reason.Text.Trim().Length <= 0)
                {
                    ShowMSG("原因 不可空白");
                    return;
                }
                else if (this.txt_reason.Text.Length > 100)
                {
                    ShowMSG("原因 長度不可超過50個中文字");
                    return;
                }
            }
            #endregion

            #region 單一行程
            if (this.lab_no.Text.Trim().Length > 0)
            {
                #region 修改
                C02DAO c02DAO1 = new C02DAO();
                c02 newRow = c02DAO1.GetByC02No(Convert.ToInt32(this.lab_peo_uid.Text), Convert.ToInt32(this.lab_no.Text));
                newRow.c02_createtime = System.DateTime.Now;
                newRow.c02_createuid = Convert.ToInt32(sobj.sessionUserID);
                newRow.c02_check = this.rbl_check.SelectedValue;
                newRow.c02_reason = this.txt_reason.Text;
                c02DAO1.Update();
                #endregion

                if (this.rbl_check.SelectedValue.Equals("1"))
                {
                    int newpk = new C02DAO().GetMaxNoByPeoUid(newRow.c02_setuid.Value) + 1;

                    string InsStr="insert into c02  (peo_uid, c02_no, c02_sdate, c02_edate, c02_title, c02_place, c02_project, c02_result, c02_bgcolor, c02_setuid, c02_createuid, c02_createtime, c02_appointdate, c02_appointmen, c02_check)"
                        + " select " + newRow.c02_setuid.Value + "," + newpk + ",c02_sdate,c02_edate,c02_title,c02_place,c02_project,c02_result,c02_bgcolor,c02_setuid,c02_createuid,c02_createtime,c02_appointdate,c02_appointmen,c02_check from c02 where peo_uid=" + this.lab_peo_uid.Text + " and c02_no=" + this.lab_no.Text;
                    dbo.ExecuteNonQuery(InsStr);
                    //您於 2011-01-24 08:00 ~ 09:00 預約主管行程「標題xxx」主管xxx已同意。
                    string smsg = "您於 "+newRow.c02_sdate.ToString("yyyy-MM-dd HH:mm")+" ~ "+newRow.c02_edate.ToString("HH:mm")+" 預約主管行程「"+this.lab_title.Text+"」主管"+this.lab_UserName.Text+"已同意。";
                    new PersonalMessageUtil().SendMessage(smsg, smsg,"",newRow.c02_setuid.Value,newRow.peo_uid,true,false,false);
                }
                else
                {
                    //您於 2011-01-24 08:00 ~ 09:00 預約主管行程「標題xxx」主管xxx不同意，原因為:a。 
                    string smsg = "您於 " + newRow.c02_sdate.ToString("yyyy-MM-dd HH:mm") + " ~ " + newRow.c02_edate.ToString("HH:mm") + " 預約主管行程「" + this.lab_title.Text + "」主管" + this.lab_UserName.Text + "不同意，原因為："+this.txt_reason.Text;
                    new PersonalMessageUtil().SendMessage(smsg, smsg, "", newRow.c02_setuid.Value, newRow.peo_uid, true, false, false);
                }

                //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
                new OperatesObject().ExecuteOperates(100301, sobj.sessionUserID, 3, "預約首長行程審核 peo_uid" + this.lab_peo_uid.Text + ",c02_no=" + this.lab_no.Text);
            }
            #endregion

            this.Page.ClientScript.RegisterStartupScript(typeof(_10_100300_100301_5), "closeThickBox", "self.parent.location.reload(true);self.parent.tb_remove();", true);
        }
        catch (Exception ex)
        {
            aMSG = "功能名稱:預約首長行程審核--核可<br>錯誤訊息:" + ex.ToString();
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