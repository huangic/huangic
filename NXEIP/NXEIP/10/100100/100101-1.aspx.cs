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

public partial class _10_100100_100101_1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       



        
        SessionObject sessionObj=new SessionObject();

        //init
        if (!Page.IsPostBack) {
            
            //判斷新增還是修改

            String mode = Request["mode"];
            String id=Request["id"];
            int ipa_no=0;
            int.TryParse(id, out ipa_no);


            this.lb_peo.Text = sessionObj.sessionUserName;
            this.lb_dep.Text = sessionObj.sessionUserDepartName;

            int peo_uid = int.Parse(sessionObj.sessionUserID);


            //this.lb_tel
            using (NXEIPEntities model = new NXEIPEntities())
            {
                var people = (from p in model.people where p.peo_uid == peo_uid select p).First();
                this.lb_tel.Text = people.peo_tel+"Ext "+people.peo_extension;
               
            }



            if (mode == "edit")
            {
               
                //隱藏範圍的部分
                this.lb_rager.Visible = false;
                this.tb_ip5.Visible = false;


                
                using(NXEIPEntities model=new NXEIPEntities()){

                   try
                   {

                       var a = (from d in model.ipaddress where d.ipa_no == ipa_no select d).First();

                       this.tb_memo.Text = a.ipa_memo;
                       this.tb_pcname.Text = a.ipa_name;
                       this.tb_group.Text = a.ipa_group;

                       String[] ip = a.ipa_start.Split('.');
                       this.tb_ip1.Text = ip[0];
                       this.tb_ip2.Text = ip[1];
                       this.tb_ip3.Text = ip[2];
                       this.tb_ip4.Text = ip[3];




                       this.Navigator1.SubFunc = "修改IP";
                   
                   }
                   catch { 
                   
                   }
               }
                
               

            }


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
          



            String mode = Request["mode"];
            String id = Request["id"];
            int ipa_no = 0;
            int.TryParse(id, out ipa_no);

            //寫入相簿

            ipaddress a = new ipaddress();

            using (NXEIPEntities model = new NXEIPEntities())
            {
            //修改模式
                if (mode == "edit")
                {
                    a.ipa_no = ipa_no;

                    model.ipaddress.Attach(a);

                    a.ipa_createuid = int.Parse(sessionObj.sessionUserID);
                    a.ipa_createtime = DateTime.Now;

                    //
                    String ip = this.tb_ip1.Text + "." + this.tb_ip2.Text + "." + this.tb_ip3.Text + "." + this.tb_ip4.Text;
                    a.ipa_start = ip;
                    a.ipa_group = this.tb_group.Text;
                    a.ipa_memo = this.tb_memo.Text;
                    a.ipa_name = this.tb_pcname.Text;
                    //a.
                    msg = "修改成功";

                }
                else { 
                //Add mode
                    int start, end;
                    start = int.Parse(this.tb_ip4.Text);
                    try
                    {
                     end = int.Parse(this.tb_ip5.Text);
                    }
                    catch {
                        end = start;
                    }

                    for (int i = start; i <= end; i++) {
                        ipaddress new_ip = new ipaddress();
                        new_ip.ipa_createuid = int.Parse(sessionObj.sessionUserID);
                        new_ip.ipa_createtime = DateTime.Now;
                        new_ip.peo_uid = int.Parse(sessionObj.sessionUserID);

                        //
                        String ip = this.tb_ip1.Text + "." + this.tb_ip2.Text + "." + this.tb_ip3.Text + "." + i;
                        new_ip.ipa_start = ip;
                        new_ip.ipa_group = this.tb_group.Text;
                        new_ip.ipa_memo = this.tb_memo.Text;
                        new_ip.ipa_name = this.tb_pcname.Text;
                        new_ip.ipa_status = "1";





                        model.ipaddress.AddObject(new_ip);
                    }




                        
                    msg = "新增成功";
                }




            

            



           



          
                
                model.SaveChanges();
            }


            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "closeThickBox", "self.parent.update('"+msg+"');", true);
        }
    }


      


    private String CheckFields() {
        String msg = String.Empty;

        if (String.IsNullOrWhiteSpace(this.tb_pcname.Text)) {
            msg+= "請輸入電腦名稱\\n";
        }

        
        if (String.IsNullOrWhiteSpace(this.tb_group.Text))
        {
            msg += "請輸入群組名稱\\n";
        }


        String ip = this.tb_ip1.Text + "." + this.tb_ip2.Text + "." + this.tb_ip3.Text + "." + this.tb_ip4.Text;

        if (!this.IsValidIP(ip))
        {
            msg += "請輸入正確IP位置\\n";
            return msg;
        }



        if (!String.IsNullOrWhiteSpace(this.tb_ip5.Text)) {
            int start = int.Parse(this.tb_ip4.Text);
            int end = int.Parse(this.tb_ip5.Text);

            if (!(start < end && end < 256))
            {
                msg += "輸入範圍IP必須介於"+start+"~255之間\\n";    
            }
            
        }
                

        return msg;
    }


    /// <summary>
    /// method to validate an IP address
    /// using regular expressions. The pattern
    /// being used will validate an ip address
    /// with the range of 1.0.0.0 to 255.255.255.255
    /// </summary>
    /// <param name="addr">Address to validate</param>
    /// <returns></returns>
    public bool IsValidIP(string addr)
    {
        //create our match pattern
        string pattern = @"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$";
        //create our Regular Expression object
        Regex check = new Regex(pattern);
        //boolean variable to hold the status
        bool valid = false;
        //check to make sure an ip address was provided
        if (addr == "")
        {
            //no address provided so return false
            valid = false;
        }
        else
        {
            //address provided so use the IsMatch Method
            //of the Regular Expression object
            valid = check.IsMatch(addr, 0);
        }
        //return the results
        return valid;
    }
}