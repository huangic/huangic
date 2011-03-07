using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMMENSITY.SWFUploadAPI;
using System.Runtime.Serialization.Json;
using Entity;
using System.IO;
using System.Text.RegularExpressions;
using NXEIP.Lib;

public partial class _10_100100_100106_1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {





        SessionObject sessionObj = new SessionObject();

        //init
        if (!Page.IsPostBack)
        {

            //判斷新增還是修改

            String mode = Request["mode"];
            String id = Request["id"];
            int ipa_no = 0;
            int.TryParse(id, out ipa_no);


            this.lb_peo.Text = sessionObj.sessionUserName;
            this.lb_dep.Text = sessionObj.sessionUserDepartName;

            int peo_uid = int.Parse(sessionObj.sessionUserID);
            this.lb_date.Text = new ChangeObject()._ADtoROCDT(DateTime.Now);

        }

    }

    protected void btn_ok_Click(object sender, EventArgs e)
    {

        String msg = CheckFields();


        if (!String.IsNullOrEmpty(msg))
        {


            JsUtil.AlertJs(this, msg);

            return;
        }
        else
        {

            SessionObject sessionObj = new SessionObject();



            //寫入

          

            using (NXEIPEntities model = new NXEIPEntities())
            {

                //Add mode

                email mail = new email();
                mail.ema_peouid = int.Parse(sessionObj.sessionUserID);
                mail.ema_apply = DateTime.Now;
                mail.ema_depno = int.Parse(sessionObj.sessionUserDepartID);
                mail.ema_mail = this.tb_email.Text;
                mail.ema_status = "3";


                model.email.AddObject(mail);
                model.SaveChanges();

                msg = "新增成功";
            }

            JsUtil.UpdateParentJs(this, msg);

        }
    }





    private String CheckFields()
    {
        String msg = String.Empty;
        string email = this.tb_email.Text.Trim();

        if (String.IsNullOrWhiteSpace(email)|| (!ValidUtil.IsValidEmail(email)))
        {
            msg += "請輸入正確的電子郵件\\n";
            return msg;
        }

         
       
        
        //檢查有沒有重複 
        using (NXEIPEntities model = new NXEIPEntities()) {
            int dupEmail = (from d in model.people where d.peo_email == email select d.peo_email).Count();
            int applyEmail = (from d in model.email where d.ema_mail == email && d.ema_status != "2" select d.ema_mail).Count();

            int total = dupEmail + applyEmail;
            if (total > 0) {
                msg += "此電子郵件已有人申請使用\\n";
            }

        }

        


        return msg;
    }



}