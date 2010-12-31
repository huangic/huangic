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

public partial class _10_100100_100103_5 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       



        
        SessionObject sessionObj=new SessionObject();

        //init
        if (!Page.IsPostBack) {
            
            //判斷新增還是修改

            String album = Request["album"];
            

            String photo=Request["photo"];
            
            
            int alb_no=0;
            int pho_no = 0;
            int.TryParse(album,out alb_no);
            int.TryParse(photo, out pho_no);

                      
               using(NXEIPEntities model=new NXEIPEntities()){

                   try
                   {

                       var a = (from d in model.photo where d.alb_no == alb_no && d.pho_no==pho_no select d).First();

                       this.tb_name.Text = a.pho_name;
                       this.tb_desc.Text = a.pho_desc;
                      
                      
                   
                   }
                   catch { 
                   
                   }
               }
                
               

            


        }

    }
   
    protected void btn_ok_Click(object sender, EventArgs e)
    {

        String album = Request["album"];


        String ph= Request["photo"];


        int alb_no = 0;
        int pho_no = 0;
        int.TryParse(album, out alb_no);
        int.TryParse(ph, out pho_no);


        String msg = CheckFields();
        if (String.IsNullOrEmpty(msg))
        {

            try
            {
                using (NXEIPEntities model = new NXEIPEntities())
                {
                    photo p = new photo();

                    p.alb_no = alb_no;
                    p.pho_no = pho_no;

                    model.photo.Attach(p);

                    p.pho_name = this.tb_name.Text;
                    p.pho_desc = this.tb_desc.Text;


                    model.SaveChanges();

                    msg = "修改完成";

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "closeThickBox", "self.parent.update('" + msg + "');", true);

                }
            }
            catch
            {

            }




       }
        else {
            JsUtil.AlertJs(this, msg);
        }
     }





   

    private String CheckFields() {
        String msg = String.Empty;

        if (String.IsNullOrWhiteSpace(this.tb_name.Text)) {
            msg = "請輸入相片名稱\\n";
        }



        return msg;
    }
}