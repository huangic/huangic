using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;
using System.IO;
using NLog;

public partial class _10_100600_100601_1 : System.Web.UI.Page
{
    private static Logger logger = LogManager.GetCurrentClassLogger();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.DepartTreeListBox1.Clear();
            this.DepartTreeTextBox1.Clear();
            this.DepartTreeTextBox2.Clear();

            this.calendar1._ADDate = DateTime.Now;
            this.calendar2._ADDate = DateTime.Now;

            for (int i = 0; i < 15; i++)
            {
                string val = (i + 8).ToString("0#");
                this.ddl_stime_h.Items.Insert(i, new ListItem(val, val));
                this.ddl_etime_h.Items.Insert(i, new ListItem(val, val));
            }

            UtilityDAO udao = new UtilityDAO();
            people p = udao.Get_People(int.Parse(new SessionObject().sessionUserID));

            this.lab_peoname.Text = p.peo_name;
            this.tbox_email.Text = p.peo_email;
            this.tbox_tel.Text = p.peo_officetel;
            

        }
    }

    protected void btn_ok_Click(object sender, EventArgs e)
    {
        if (CheckInput())
        {
            
            using (NXEIPEntities model = new NXEIPEntities())
            {
                int peo_uid = int.Parse(new SessionObject().sessionUserID);
                
                //會議資料
                meetings meet = new meetings();

                meet.mee_createtime = DateTime.Now;
                meet.mee_createuid = peo_uid;
                meet.mee_reason = this.tbox_reason.Text;
                meet.mee_sdate = Convert.ToDateTime(this.calendar1._AD + " " + this.ddl_stime_h.SelectedValue + ":" + this.ddl_stime_m.SelectedValue);
                meet.mee_edate = Convert.ToDateTime(this.calendar2._AD + " " + this.ddl_etime_h.SelectedValue + ":" + this.ddl_etime_m.SelectedValue);
                meet.mee_place = this.tbox_place.Text;
                meet.mee_facility = this.tbox_facility.Text;
                meet.mee_host = int.Parse(this.DepartTreeTextBox1.Value);
                meet.mee_recorduid = int.Parse(this.DepartTreeTextBox2.Value);
                meet.mee_tel = this.tbox_tel.Text;
                meet.mee_email = this.tbox_email.Text;
                meet.mee_fax = this.tbox_fax.Text;
                meet.mee_peouid = peo_uid;
                meet.mee_memo = this.tbox_memo.Text;
                meet.mee_status = "1";
                meet.mee_invite = this.rbl_invite.SelectedValue;

                model.meetings.AddObject(meet);
                model.SaveChanges();

                int mee_no = meet.mee_no;

                //出席人員
                int count = 1;
                foreach (var d in this.DepartTreeListBox1.Items)
                {
                    int uid = int.Parse(d.Key);

                    attends at = new attends();
                    at.mee_no = mee_no;
                    at.att_no = count++;
                    at.peo_uid = uid;//出席人員
                    at.att_status = "1";//尚未回覆
                    model.attends.AddObject(at);
                }
                model.SaveChanges();

                //傳送開會訊息至個人訊息,點選發訊息才發
                if (this.rbl_invite.SelectedValue == "1")
                {
                    PersonalMessageUtil msg = new PersonalMessageUtil();
                    foreach (var d in this.DepartTreeListBox1.Items)
                    {
                        int to = int.Parse(d.Key);
                        string body = string.Format("{0}邀請您出席「{1}」會議，請您至會議管理功能進行出席回覆!", new SessionObject().sessionUserName, this.tbox_reason.Text);
                        msg.SendMessage("會議通知", body, "", to, peo_uid, true, false, false);
                    }
                }
                

                //會前資料
                String FilePath = "/upload/100601/";
                String uploadDir = new ArgumentsObject().Get_argValue("100601_dir");
                Directory.CreateDirectory(uploadDir + FilePath);

                int count2 = 1;
                for (int i = 1; i <= 5; i++)
                {
                    FileUpload fu = (FileUpload)this.FindControl("FileUpload" + i);

                    if (fu.HasFile && !string.IsNullOrEmpty(uploadDir))
                    {
                        string filename = DateTime.Now.ToString("yMdhhmmssfff") + Path.GetExtension(fu.FileName);

                        //上傳檔案
                        try
                        {
                            fu.SaveAs(uploadDir + FilePath + filename);
                        }
                        catch (Exception ex)
                        {
                            logger.Debug(ex.Message);
                        }

                        huiyi file = new huiyi();

                        file.mee_no = mee_no;
                        file.hui_no = count2++;
                        file.hui_file = fu.FileName;//原始檔名
                        file.hui_type = Path.GetExtension(fu.FileName);
                        file.hui_path = FilePath + filename;
                        String desc = String.Empty;
                        try
                        {
                            desc = ((TextBox)this.FindControl("tbox_file" + i)).Text;
                        }
                        catch
                        {
                        }
                        file.hui_memo = desc;

                        model.huiyi.AddObject(file);
                    }
                }

                model.SaveChanges();

                OperatesObject.OperatesExecute(100601, 1, string.Format("新增會議 mee_no={0}", mee_no));
            }

            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "closeThickBox", "self.parent.update('新增完成!');", true);
            
        }
    }

    private bool CheckInput()
    {
        if (this.tbox_reason.Text.Trim().Length == 0)
        {
            JsUtil.AlertJs(this,"請輸入開會事由!");
            return false;
        }
        if (!this.calendar1.CheckDateTime() || !this.calendar2.CheckDateTime())
        {
            JsUtil.AlertJs(this, "請輸入正確日期!");
            return false;
        }
        if (this.calendar1._ADDate > this.calendar2._ADDate)
        {
            JsUtil.AlertJs(this, "請輸入正確起訖日期順序!");
            return false;
        }
        if (this.tbox_place.Text.Trim().Length == 0)
        {
            JsUtil.AlertJs(this, "請輸入開會地點!");
            return false;
        }
        if (this.DepartTreeTextBox1.Items.Count == 0)
        {
            JsUtil.AlertJs(this, "請選擇聯絡人!");
            return false;
        }
        if (this.DepartTreeTextBox2.Items.Count == 0)
        {
            JsUtil.AlertJs(this, "請選擇記錄人員!");
            return false;
        }
        if (this.tbox_tel.Text.Trim().Length == 0)
        {
            JsUtil.AlertJs(this, "請輸入聯絡人電話!");
            return false;
        }
        if (this.tbox_email.Text.Trim().Length == 0)
        {
            JsUtil.AlertJs(this, "請輸入聯絡人E-Mail!");
            return false;
        }

        //是否發送訊息
        if (this.rbl_invite.SelectedValue == "1")
        {
            if (this.DepartTreeListBox1.Items.Count == 0)
            {
                JsUtil.AlertJs(this, "請選擇出席人員!");
                return false;
            }
        }
        

        return true;

    }
    
}