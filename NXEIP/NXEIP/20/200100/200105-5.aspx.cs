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

public partial class _20_200100_200105_5 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int size = 0;

        
        SessionObject sessionObj=new SessionObject();

        //init
        if (!Page.IsPostBack) {
            int peo_uid=int.Parse(sessionObj.sessionUserID);
            //取檔案

        

           

            
            
           
            
            this.hidden_doc11no.Value = Request["id"];



            using(NXEIPEntities model=new NXEIPEntities()){
                 int id=int.Parse(Request["id"]);

                 var d11 = (from d in model.doc11 where d.d11_no == id select d).First();

                 this.lb_edate.Text = new ChangeObject()._ADtoROC(d11.d11_edate.Value);

                 this.lb_peo.Text = new PeopleDAO().GetPeopleNameByUid(d11.d11_peouid);
                 this.lb_dep.Text = new DepartmentsDAO().GetByDepNo(d11.d11_depno).dep_name;


                 this.lb_subject.Text = d11.d11_subject;
                 this.lb_use.Text = d11.d11_use;
                
                              

            }
            

        }

    }


    protected string GetDepartment(int dep_no) {
        DepartmentsDAO dao = new DepartmentsDAO();

        return dao.GetByDepNo(dep_no).dep_name;
    }

    protected string GetPeople(int peo_uid) {
        return new PeopleDAO().GetPeopleNameByUid(peo_uid);
    }
   
}