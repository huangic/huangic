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
using System.IO;

public partial class _10_100700_100701 : System.Web.UI.Page
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

                        String filePath="~/swf/" + s.sfu_no+".htm";
                       
                        a.HRef = filePath;
                       
                        
                        a.Attributes["alt"] = s.sfu_name;
                        a.Title = s.sfu_name;
                        a.InnerText = s.sfu_name;
                        a.Attributes.Add("class", "a-letter-t2");
                        a.Target = "_blank";

                        HtmlGenericControl li = new HtmlGenericControl("li");
                        li.Attributes.Add("class", "ps2 border-bottom-block3");


                        li.Controls.Add(a);
                        



                        //divBoxContent.Controls.Add(cb);
                        
                        //找HTML黨 沒有就不加入說明    

                       
                        String AbsFilePath = Server.MapPath(filePath);

                        FileInfo file = new FileInfo(AbsFilePath);
                        if (file.Exists)
                        {


                            divBoxContent.Controls.Add(li);
                        }


                }

               
                


                this.application.Controls.Add(divBoxA);
            }

        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        
       
    }

}