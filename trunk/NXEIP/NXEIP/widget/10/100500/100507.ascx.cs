using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity;
using System.Web.UI.HtmlControls;
using System.Data.Objects.SqlClient;

public partial class widget_10_100500_100507 : NXEIP.Widget.WidgetBaseControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
         SessionObject sessionObj = new SessionObject();
        

        string user_login = sessionObj.sessionUserAccount;
        int peouid = int.Parse(sessionObj.sessionUserID);
        //取使用者所有的可用功能
        //this.ObjectDataSource1.SelectParameters[0].DefaultValue = user_login;


        //取使用者在SETTING設定的SFU

        using (NXEIPEntities model = new NXEIPEntities()) {

            var userappcodes = (from d in model.setting
                                where
                                d.peo_uid == peouid
                                && d.set_variable == "100507"
                                select d.set_value).Cast<Int32>();
            
            var apps = from d in userappcodes
                       from sfu in model.sysfuction
                       from s in model.sys

                       where 
                       d == sfu.sfu_no
                       && 
                       s.sys_no == sfu.sys_no
                       orderby s.sys_order, sfu.sfu_order
                       select sfu;
            
            int number=0;
            HtmlGenericControl ul_1 = new HtmlGenericControl("ul");
            HtmlGenericControl ul_2 = new HtmlGenericControl("ul");
            ul_2.Attributes.Add("class", "app_more");
            

            this.application.Controls.Add(ul_1);
            this.application.Controls.Add(ul_2);
            foreach (var s in apps) {

                
                number++;
                HtmlGenericControl li = new HtmlGenericControl("li");
                li.Attributes.Add("class", "dot_a51");
                HtmlAnchor a = new HtmlAnchor();
                a.InnerText = s.sfu_name;

                if (s.sfu_token != "1")
                {
                    a.HRef = "~/" + s.sfu_path;
                }
                else
                {
                    a.HRef = "~/External.aspx?sysId=" + s.sfu_no;
                }

                li.Controls.Add(a);


                if (number <= 5)
                {
                    ul_1.Controls.Add(li);
                }
                else {
                    ul_2.Controls.Add(li);
                }
            }


        }


    }

    public override string Name
    {
        get { return "ApplicationCenter"; }
    }

    public override void loadWidget()
    {
       
    }
}