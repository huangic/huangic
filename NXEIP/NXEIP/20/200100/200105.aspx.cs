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

public partial class _20_200100_200105 : System.Web.UI.Page
{
    private static Logger logger = LogManager.GetCurrentClassLogger();
    
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack) { 
            //建立GRID VIEW
            //this.GridView1.DataBind();


            //NXEIPEntities model = new NXEIPEntities();

            

            
        }
    }

  
    
   
    protected static string GetDepartmentName(int dep_no)
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

           


            var v = (doc06)e.Row.DataItem;

            ods.SelectParameters[0].DefaultValue = v.d06_no.ToString();

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
        String subject = this.tb_subject.Text;
            String file="";


        file=this.tb_file.Text;

        this.ObjectDataSource_d11.SelectParameters[0].DefaultValue = subject;
        this.ObjectDataSource_d11.SelectParameters[1].DefaultValue = file;

        this.GridView1.DataBind();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del") {
            int index = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;

            int id = Convert.ToInt32(this.GridView1.DataKeys[index].Value);


            using (NXEIPEntities model = new NXEIPEntities()) {
                doc06 d06 = new doc06();
                d06.d06_no = id;
                model.doc06.Attach(d06);

                    var d07=(from d in model.doc07 where d.d06_no==id select d);
                    var d08 = (from d in model.doc08 where d.d06_no == id select d);


                    foreach (var d in d08)
                    {
                        model.doc08.DeleteObject(d);
                    }

                    foreach (var d in d07)
                    {
                        model.doc07.DeleteObject(d);
                    }
                    model.doc06.DeleteObject(d06);
                    model.SaveChanges();
            }
            this.GridView1.DataBind();
        }
    }
}