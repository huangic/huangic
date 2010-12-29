﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMMENSITY.SWFUploadAPI;
using System.Runtime.Serialization.Json;
using Entity;
using System.IO;

public partial class _10_100100_100103_2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       



        
        SessionObject sessionObj=new SessionObject();

        //init
        if (!Page.IsPostBack) {
            


        }

    }
   
    protected void btn_ok_Click(object sender, EventArgs e)
    {

        String msg = CheckFields();


        if (!String.IsNullOrEmpty(msg))
        {


            JsUtil.AlertJs(this, msg);

            //Response.End();
            return;
        }
        else
        {

            SessionObject sessionObj=new SessionObject();
            ArgumentsObject args = new ArgumentsObject();



            //寫入相簿

            album a = new album();

            a.alb_createuid = int.Parse(sessionObj.sessionUserID);
            a.alb_createtime = DateTime.Now;

            a.alb_dep = int.Parse(sessionObj.sessionUserDepartID);
            a.alb_desc = this.tb_desc.Text;
            a.alb_name = this.tb_name.Text;
            a.alb_public = this.RadioButtonList1.SelectedValue;

            a.peo_uid = int.Parse(sessionObj.sessionUserID);

            //如果公布全府就要審核
            if (a.alb_public == "3")
            {
                //檢查參數
                String check = args.Get_argValue("100103_check");
                if (check != "2")
                {
                    a.alb_status = "3";
                }
                else {
                    a.alb_status = "1";
                }
            }
            else
            {
                a.alb_status = "1";
            }

            using (NXEIPEntities model = new NXEIPEntities()) {
                model.AddToalbum(a);
                model.SaveChanges();
            }


            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "closeThickBox", "self.parent.update('新增成功');", true);
        }
    }


    private String CheckFields() {
        String msg = String.Empty;

        if (String.IsNullOrWhiteSpace(this.tb_name.Text)) {
            msg = "請輸入相簿名稱\\n";
        }



        return msg;
    }
}