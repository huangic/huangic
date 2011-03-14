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
using NXEIP.Lib;

public partial class _20_200800_200802_1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        
        
        
       


        SessionObject sessionObj=new SessionObject();

        //init
        if (!Page.IsPostBack) {
           

            //因為上傳套件會跟樹狀鬼打牆 上傳檔案會觸發!PageIsPostBack
           
            this.PreEdit();
               

            


        }

    }
  
   


    private void PreEdit() {
        int id = int.Parse(Request["ID"]);

        this.Image1.Visible = true;
        this.Image1.ImageUrl = String.Format("200801-1.ashx?id={0}", id);
        

        //設定欄位

        UtilityDAO utilDAO = new UtilityDAO();


        using(NXEIPEntities model=new NXEIPEntities()){
            //取 未婚資料
            unmarried u = (from d in model.unmarried where d.unm_no == id select d).FirstOrDefault();

            people peo = (from d in model.people where d.peo_name == u.unm_name && u.unm_depno == u.unm_depno select d).FirstOrDefault();

            //this.DepartTreeTextBox1.Add(peo.peo_uid);

            this.lb_name.Text = u.unm_name;

            this.lb_dep.Text = utilDAO.Get_DepartmentName(u.unm_depno.Value);

            this.lb_title.Text = utilDAO.Get_TypesCName(u.unm_typno.Value);
            
            this.lb_sex.Text = u.unm_sex=="1"?"男":"女";

            this.lb_height.Text = u.unm_height;
            this.lb_weight.Text = u.unm_weight;
            this.lb_age.Text = u.unm_age;
           
            this.lb_school.Text = u.unm_school;
            this.lb_interest.Text = u.unm_interest;
            this.lb_contact.Text = u.unm_contact;
            this.lb_condition.Text = u.unm_condition;
            this.lb_introduce.Text = u.unm_introduce;



        }



    }


   
}