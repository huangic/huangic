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

public partial class _20_200100_200107 : System.Web.UI.Page
{
    private static Logger logger = LogManager.GetCurrentClassLogger();
    
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
             
               
        }
        else { 
            
        
        }
    }

  
  
    protected static string GetDepartmentName(int dep_no)
    {
        using (NXEIPEntities model = new NXEIPEntities()) {
            var dep = (from d in model.departments where d.dep_no == dep_no select d).First();
            
                return dep.dep_name;
           
        }
    
    }

    protected static string GetCatName(int cat_no) { 
        using(NXEIPEntities model=new NXEIPEntities()){
            var cat = (from c in model.sys06 where c.s06_no == cat_no select c).First();

            if (cat.s06_level == 1)
            {
                return cat.s06_name;
            }
            else {
                var p_cat = (from c in model.sys06 where c.s06_no == cat.s06_parent select c).First();
                return p_cat.s06_name;
            }
             
            

        }
    }

    protected static string GetCatChildName(int cat_no)
    {
        using (NXEIPEntities model = new NXEIPEntities())
        {
            var cat = (from c in model.sys06 where c.s06_no == cat_no select c).First();

            if (cat.s06_level == 1)
            {
                return "";
            }
            else
            {
                return cat.s06_name;
            }



        }
    }

     


    protected static bool GetModifyVisible(int peo_uid) { 
        //HttpContext.Current.Session[""]
        SessionObject session = new SessionObject();
        return (int.Parse(session.sessionUserID) == peo_uid);
            
        
    }

   

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //Bind內部的DataSource
        if (e.Row.RowType == DataControlRowType.DataRow) {
           ObjectDataSource ods = (ObjectDataSource)e.Row.FindControl("ObjectDataSource2");

           


            var v = (doc09)e.Row.DataItem;

            ods.SelectParameters[0].DefaultValue = v.d09_no.ToString();

        }

    }
    /// <summary>
    /// 依條件查詢
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        String dep_no = "";
        //String keyword = "";
        String number = "";
            String file="";


        

        //keyword = this.tb_word.Text;

        
        file=this.tb_file.Text;

        this.ObjectDataSource3.SelectParameters[0].DefaultValue = dep_no;
        this.ObjectDataSource3.SelectParameters[1].DefaultValue = number;
        this.ObjectDataSource3.SelectParameters[2].DefaultValue = file;

        this.GridView1.DataBind();
    }
}