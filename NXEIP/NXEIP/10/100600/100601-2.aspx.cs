using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;

public partial class _10_100600_100601_2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            int mee_no = int.Parse(Request.QueryString["mee_no"]);
            this.hidd_meeno.Value = mee_no.ToString();

            meetings d = new _100601DAO().GetMeetings(mee_no);
            ChangeObject cobj = new ChangeObject();
            UtilityDAO udao = new UtilityDAO();

            this.lab_reason.Text = d.mee_reason;
            this.lab_place.Text = d.mee_place;
            this.lab_host.Text = udao.Get_PeopleName(d.mee_host.Value);
            this.lab_date.Text = cobj._ADtoROCDT(d.mee_sdate.Value) + "~" + cobj._ADtoROCDT(d.mee_edate.Value);
            this.lab_peoname.Text = udao.Get_PeopleName(d.mee_peouid.Value);
            this.lab_tel.Text = d.mee_tel;

            if (DateTime.Now > d.mee_sdate)
            {
                this.btn_ok.Enabled = false;
                this.rbl_status.Enabled = false;
                this.tbox_reason.Enabled = false;
                JsUtil.AlertJs(this, "開會時間已過!");
            }


        }
    }

    protected void btn_ok_Click(object sender, EventArgs e)
    {
        if (this.rbl_status.SelectedValue.Equals("3") && this.tbox_reason.Text.Trim().Length == 0)
        {
            JsUtil.AlertJs(this, "如未出席,請輸入事由!");
        }
        else
        {
            using (NXEIPEntities model = new NXEIPEntities())
            {
                int mee_no = int.Parse(this.hidd_meeno.Value);
                int peo_uid = int.Parse(new SessionObject().sessionUserID);

                meetings meet = (from p in model.meetings
                                 where p.mee_no == mee_no 
                                 select p).FirstOrDefault();

                attends d = (from p in model.attends
                             where p.mee_no == mee_no && p.peo_uid == peo_uid
                             select p).FirstOrDefault();

                if (d != null)
                {
                    d.att_status = this.rbl_status.SelectedValue;
                    d.att_reason = this.tbox_reason.Text;
                    d.att_date = DateTime.Now;

                    model.SaveChanges();

                    //會議回覆通知
                    PersonalMessageUtil msg = new PersonalMessageUtil();
                    string status = "", reason = "";
                    if (this.rbl_status.SelectedValue == "2")
                    {
                        status = "出席";
                        reason = "。";
                    }
                    else
                    {
                        status = "不出席";
                        reason = "，事由:" + this.tbox_reason.Text;
                    }

                    int to = meet.mee_peouid.Value;
                    string body = string.Format("{0}將{1}您所舉行「{2}」會議{3}", new SessionObject().sessionUserName, status, meet.mee_reason, reason);
                    msg.SendMessage("會議出席回覆", body, "", to, peo_uid, true, false, false);

                    OperatesObject.OperatesExecute(100601, 3, string.Format("會議出席回覆 mee_no={0} peo_uid={1}",mee_no,peo_uid));
                }
            }

            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "closeThickBox", "self.parent.update('回覆完成!');", true);
        }
    }
}