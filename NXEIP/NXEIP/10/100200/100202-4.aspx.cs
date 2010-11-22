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
using NXEIP.DAO;

public partial class _10_100200_100202_4 : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {

       

        //init
        if (!Page.IsPostBack) {
            int id = int.Parse(Request["id"]);


            //取單
            using (NXEIPEntities model = new NXEIPEntities()) {
                var treatdetail = (from d in model.treatdetail where d.tde_no == id select d).First();

                PeopleDAO peopleDAO = new PeopleDAO();
                ChangeObject changeObj = new ChangeObject();

                //塞待辦單

                treat t=treatdetail.treat;


                this.lb_name.Text = t.tre_name;
                this.lb_sdate.Text = changeObj._ADtoROC(t.tre_sdate.Value);
                this.lb_edate.Text = changeObj._ADtoROC(t.tre_edate.Value);
                this.lb_tre_peo.Text = peopleDAO.GetPeopleNameByUid(t.peo_uid);
                this.lb_work.Text = t.tre_work;
                this.ObjectDataSource_turning.SelectParameters[0].DefaultValue = t.tre_no.ToString();
                this.ListView1.DataBind();



                //塞代辦人
                this.lb_status.Text = GetStatus(treatdetail.tde_status);
                this.lb_tde_peo.Text = peopleDAO.GetPeopleNameByUid(treatdetail.peo_uid);
                this.lb_description.Text = treatdetail.tde_description;
                this.lb_achieved.Text = treatdetail.tde_achieved.ToString()+"%";
                this.ObjectDataSource_goback.SelectParameters[0].DefaultValue = id.ToString();
                this.ListView2.DataBind();



            }
          

        }



      

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        

        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "closeThickBox", "self.parent.update();", true);

    }
   

    private String GetStatus(string status){
          switch (status) { 
            case "1":
                return "執行中";
            case "2":
                return "已完成";
            
        }
        return "";
    }

   
}