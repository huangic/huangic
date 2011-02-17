using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity;
using System.Web.UI.HtmlControls;
using NLog;
using System.Data.Objects;

public partial class _10_100500_100507 : System.Web.UI.Page
{

    private static Logger logger = LogManager.GetCurrentClassLogger();
    
    
    protected void Page_Load(object sender, EventArgs e)
    {
        SessionObject sessionObj = new SessionObject();


        string user_login = sessionObj.sessionUserAccount;
        int peo_uid=int.Parse(sessionObj.sessionUserID);

        //取使用者所有的可用功能
        using (NXEIPEntities model = new NXEIPEntities())
        {

            //取CHECK的LIST

            var usercheck = from d in model.setting
                            where
                            d.set_variable == "100507"
                            && d.peo_uid == peo_uid
                            select d;



            var menu = (from s in model.sys
                        from sysfunc in model.sysfuction
                        from account in model.accounts
                        from roleacc in model.roleaccount
                        from rauth in model.rauthority
                        where sysfunc.sfu_status == "1"
                        && roleacc.acc_no == account.acc_no
                        && rauth.rol_no == roleacc.rol_no
                        && sysfunc.sfu_no == rauth.sfu_no
                        && account.acc_login == user_login
                        && s.sys_no == sysfunc.sys_no
                        orderby sysfunc.sfu_order
                        select new { sysFunc = sysfunc, sys = s }).Distinct();

           

            //grouping

            var groupmenu = (from m in menu
                             orderby m.sys.sys_order
                             group m.sysFunc by m.sys into ms
                             select ms);
                             

            //ObjectQuery q =groupmenu as ObjectQuery;
            //logger.Debug(q.ToTraceString());


            foreach (var m in groupmenu)
            {
                HtmlControl divBoxA = new HtmlGenericControl("div");

                divBoxA.Attributes.Add("class", "boxA");

                HtmlControl divBox = new HtmlGenericControl("div");
                divBox.Attributes.Add("class", "box");

                HtmlControl divBoxHeader = new HtmlGenericControl("div");
                divBoxHeader.Attributes.Add("class", "head");
                divBoxHeader.Controls.Add(new Literal() { Text = m.Key.sys_name.ToString() });
                divBoxA.Controls.Add(divBoxHeader);
                divBoxA.Controls.Add(divBox);


                Dictionary<String,HtmlControl> Headers = new Dictionary<string, HtmlControl>();

                var orderItem = from d in m orderby d.sfu_order  select d;

                
                //先見HEADER?
                //寫入KEYMAP

                foreach (var s in orderItem.Where(x => x.sfu_parent == 0)) {
                    HtmlControl divBoxContent = null;
                    divBoxContent = new HtmlGenericControl("div");
                    divBoxContent.Attributes.Add("class", "content");




                    HtmlControl divBoxB1 = new HtmlGenericControl("div");
                    HtmlAnchor a = new HtmlAnchor();
                    a.Title = s.sfu_name;
                    a.Attributes["alt"] = s.sfu_name;
                    a.InnerText = s.sfu_name;
                    a.Attributes.Add("class", "a-letter-t1");
                    HtmlGenericControl li = new HtmlGenericControl("li");
                    li.Attributes.Add("class", "ps1");
                    li.Controls.Add(a);

                    divBoxB1.Attributes.Add("class", "b2");
                    divBoxB1.Controls.Add(li);
                    divBoxContent.Controls.Add(divBoxB1);
                    divBox.Controls.Add(divBoxContent);

                    Headers.Add(s.sfu_no.ToString(), divBoxContent);
                }






                //建立內容
                foreach (var s in orderItem.Where(x => x.sfu_parent != 0))
                {


                       HtmlControl divBoxContent=Headers[s.sfu_parent.ToString()];
                  
                        HtmlAnchor a = new HtmlAnchor();

                        if (s.sfu_token != "1")
                        {
                            a.HRef = "~/" + s.sfu_path;
                        }
                        else {
                            a.HRef = "~/External.aspx?sysId=" + s.sfu_no;
                        }
                        
                        a.Attributes["alt"] = s.sfu_name;
                        a.Title = s.sfu_name;
                        a.InnerText = s.sfu_name;
                        a.Attributes.Add("class", "a-letter-t2");

                        HtmlGenericControl li = new HtmlGenericControl("li");
                        li.Attributes.Add("class", "ps2 border-bottom-block3");



                        //CHECKBOX

                        CheckBox cb = new CheckBox();

                        cb.ID = s.sfu_no.ToString();

                        //取TABLE 看是否為選擇的

                        bool isCheck = ((from d in usercheck 
                                       where 
                                       d.set_value==cb.ID
                                       select d).Count())>0;

                        cb.Checked = isCheck; 
                        
                        li.Controls.Add(cb);
                        
                        
                        
                        
                        
                        li.Controls.Add(a);



                        //divBoxContent.Controls.Add(cb);
                        divBoxContent.Controls.Add(li);
                   


                }

               
                


                this.application.Controls.Add(divBoxA);
            }

        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        
       
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {

        logger.Debug("CLICK!");

        //清掉所有設定

        //使用者編號"100507"

        int peo_uid=int.Parse(new SessionObject().sessionUserID);


        using (NXEIPEntities model = new NXEIPEntities()) { 
            var data =from d in model.setting where d.peo_uid==peo_uid && d.set_variable=="100507" select d;

            foreach(var d in data){
                model.setting.DeleteObject(d);
            }

            model.SaveChanges();

            FindControls(this.application,model);


            model.SaveChanges();
        }

        JsUtil.AlertJs(this,"存檔完成!!");

      
    }

    /// <summary>
    /// 找使用者點選的功能
    /// </summary>
    /// <param name="p_control"></param>
    /// <param name="model"></param>
    private void FindControls(Control p_control,NXEIPEntities model){

        foreach (Control c in p_control.Controls)
        {


            if (c is CheckBox)
            {
                CheckBox control = (c as CheckBox);

                logger.Debug("ID:{0},Selected:{1}", control.ID, control.Checked);

                //寫入使用者TABLE

                if (control.Checked)
                {
                    setting set = new setting();
                    set.set_variable = "100507";
                    set.set_value = control.ID;
                    set.peo_uid = int.Parse(new SessionObject().sessionUserID);

                    model.setting.AddObject(set);
                }



            }
            else
            {
                if (c.Controls.Count > 0)
                {
                    FindControls(c,model);
                }
            }

           
        }
       
    } 
}