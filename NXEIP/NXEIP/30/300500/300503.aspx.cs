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

public partial class _30_300500_300503 : System.Web.UI.Page
{
    private static Logger logger = LogManager.GetCurrentClassLogger();
    
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack) { 
           

            
            
        }




        //判斷來自JS 使用_doPostBack(updatePanel,"") 的情況 
        if (Request["__EVENTTARGET"] == this.UpdatePanel1.ClientID && String.IsNullOrEmpty(Request["__EVENTARGUMENT"]))
        {
           this.GridView_dep.DataBind();
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


    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = System.Convert.ToInt32(e.CommandArgument);
        int sys_no = System.Convert.ToInt32(this.GridView_dep.DataKeys[rowIndex].Value.ToString());

        if (e.CommandName.Equals("disable"))
        {
            delete(sys_no);
            return;
        }
    }




    private void delete(int off_no)
    {
        using (NXEIPEntities model = new NXEIPEntities())
        {
            officials o = new officials();
            o.off_no = off_no;

            model.officials.Attach(o);
            o.off_status = "2";

            model.SaveChanges();
        }

        this.GridView_dep.DataBind();

        //this.OkMessagebox1.showMessagebox("delete"+dep_no);
    }
   
    
    
  

  
   

   
    protected void btn_dep_search_Click(object sender, EventArgs e)
    {
        //部門的查詢
        this.ObjectDataSource_dep.SelectParameters[0].DefaultValue = this.ddl_unit.SelectedValue;
        this.ObjectDataSource_dep.SelectParameters[1].DefaultValue = this.tb_file.Text;

        this.GridView_dep.DataBind();

    }
  
}