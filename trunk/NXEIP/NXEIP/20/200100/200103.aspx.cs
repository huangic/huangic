using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using AjaxControlToolkit;
using NXEIP.DAO;
using System.Data.Objects.SqlClient;
using Entity;
using NLog;
using System.Data;

public partial class _20_200100_200103 : System.Web.UI.Page
{
    private static Logger logger = LogManager.GetCurrentClassLogger();
    
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack) { 
            //建立GRID VIEW
            //this.GridView1.DataBind();


            //NXEIPEntities model = new NXEIPEntities();


            initPanel(0);
            
        }




        //判斷來自JS 使用_doPostBack(updatePanel,"") 的情況 
        if (Request["__EVENTTARGET"] == this.UpdatePanel1.ClientID && String.IsNullOrEmpty(Request["__EVENTARGUMENT"]))
        {
           // this.GridView.DataBind();
        }

        

        
    }

  
    
   
    protected string GetDepartmentName(int dep_no)
    {
        using (NXEIPEntities model = new NXEIPEntities()) {
            var dep = (from d in model.departments where d.dep_no == dep_no select d).First();
            if (dep.dep_level > 1)
            {
                var parent_dep = (from d in model.departments where d.dep_no == dep.dep_parentid select d).First();

                return parent_dep.dep_name + "-" + dep.dep_name;
            }
            else {
                return dep.dep_name;
            }
        }
    
    }


    protected String GetSys06Name(object sys06_no) {
        String value = "";

        Sys06DAO dao = new Sys06DAO();

        sys06 sys=dao.GetByS06No(System.Convert.ToInt32(sys06_no));

        if (sys != null) {
            value = sys.s06_name;

        }

        return value;
    
    }
   

   
    /// <summary>
    /// 依條件查詢
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    
  

    private void initPanel(int status) {

        this.Panel_dep.Visible = false;
        this.Panel_dep_grid.Visible = false;
        this.Panel_personal.Visible = false;
        this.Panel_personal_grid.Visible = false;
        
        
        
        
        if (status == 1)
        {

            this.Panel_personal.Visible = true;
            this.Panel_personal_grid.Visible = true;
        }
        else {
            this.Panel_dep.Visible = true;
            this.Panel_dep_grid.Visible = true;
        }
    }

    private void initButton(int status) {
        this.btn_dep.CssClass = "a-input";
        this.btn_personal.CssClass = "a-input";

        if (status == 1)
        {
            this.btn_personal.CssClass = "b-input2";
        }
        else {
            this.btn_dep.CssClass = "b-input2";
        }
    }


    protected void btn_personal_Click(object sender, EventArgs e)
    {
        this.initPanel(1);
        this.initButton(1);
        this.GridView_dep.DataBind();


    }
    protected void btn_dep_Click(object sender, EventArgs e)
    {
        this.initPanel(0);
        this.initButton(0);
        this.GridView_peo.DataBind();
    }
    protected void btn_dep_search_Click(object sender, EventArgs e)
    {
        //部門的查詢
        this.ObjectDataSource_dep.SelectParameters[0].DefaultValue = this.ddl_unit.SelectedValue;
        this.ObjectDataSource_dep.SelectParameters[1].DefaultValue = this.tb_file.Text;

        this.GridView_dep.DataBind();

    }
    protected void btn_peo_search_Click(object sender, EventArgs e)
    {
        //人員的查詢

        this.ObjectDataSource_people.SelectParameters[0].DefaultValue = this.tb_people.Text;

        this.GridView_peo.DataBind();
    }
}