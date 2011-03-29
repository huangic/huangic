using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NLog;
using Entity;

public partial class public_100105 : System.Web.UI.Page
{
    private static Logger logger = LogManager.GetCurrentClassLogger();


     
    
    
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
          //驗證CODE 

            string code = "";
            try
            {
                code = Page.RouteData.Values["code"].ToString();
            }
            catch {
                code = Request["code"];
            }

            using (NXEIPEntities model = new NXEIPEntities()) {
                var share = (from d in model.doc14 where d.d14_network == code select d).Count();


                if (share == 0) {
                    JsUtil.AlertJs(this, "分享網址錯誤");

                    this.UpdatePanel1.Visible = false;
                }
            }



        }
        this.GridView1.DataBind();
    }


  

    protected static string GetDepartmentName(int dep_no)
    {
        using (NXEIPEntities model = new NXEIPEntities())
        {
            var dep = (from d in model.departments where d.dep_no == dep_no select d).First();
            if (dep.dep_level > 1)
            {
                var parent_dep = (from d in model.departments where d.dep_no == dep.dep_parentid select d).First();

                return parent_dep.dep_name + "-" + dep.dep_name;
            }
            else
            {
                return dep.dep_name;
            }
        }

    }

        
    /// <summary>
    /// 依條件查詢
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {

        String network = "";
        String pwd = "";
        

              

        network= Page.RouteData.Values["code"].ToString();

        pwd = this.tb_number.Text;
       

        this.ObjectDataSource3.SelectParameters[0].DefaultValue = network;
        this.ObjectDataSource3.SelectParameters[1].DefaultValue = pwd;
        
      

        this.GridView1.DataBind();
    }
   
   
}