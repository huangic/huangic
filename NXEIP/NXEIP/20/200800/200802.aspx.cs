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

public partial class _20_200800_200802 : System.Web.UI.Page
{
    private static Logger logger = LogManager.GetCurrentClassLogger();
    
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack) { 
           

            
            
        }




        //判斷來自JS 使用_doPostBack(updatePanel,"") 的情況 
        if (Request["__EVENTTARGET"] == this.UpdatePanel1.ClientID && String.IsNullOrEmpty(Request["__EVENTARGUMENT"]))
        {
           this.GridView1.DataBind();
        }

        

        
    }

    protected String GetTitleName(int type_no) {
        return new UtilityDAO().Get_TypesCName(type_no);
    
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
        int sys_no = System.Convert.ToInt32(this.GridView1.DataKeys[rowIndex].Value.ToString());

        if (e.CommandName.Equals("disable"))
        {
            delete(sys_no);
            return;
        }
    }




    private void delete(int unm_no)
    {
        using (NXEIPEntities model = new NXEIPEntities())
        {
            unmarried o = new unmarried();
            o.unm_no = unm_no;

            model.unmarried.Attach(o);
            o.unm_open = "2";

            model.SaveChanges();
        }

        this.GridView1.DataBind();

        //this.OkMessagebox1.showMessagebox("delete"+dep_no);
    }




    protected void btn_search_Click(object sender, EventArgs e)
    {
        this.ObjectDataSource_unm.SelectParameters[0].DefaultValue = this.tb_name.Text;
        this.ObjectDataSource_unm.SelectParameters[1].DefaultValue = this.ddl_sex.SelectedValue;
    }
}