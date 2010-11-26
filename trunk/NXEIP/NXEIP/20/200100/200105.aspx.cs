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




        //判斷來自JS 使用_doPostBack(updatePanel,"") 的情況 
        if (Request["__EVENTTARGET"] == this.UpdatePanel1.ClientID && String.IsNullOrEmpty(Request["__EVENTARGUMENT"]))
        {
            this.GridView1.DataBind();
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

           


            var v = (doc11)e.Row.DataItem;

            ods.SelectParameters[0].DefaultValue = v.d11_no.ToString();

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

        OperatesObject.OperatesExecute(200105, 2, String.Format("查詢回傳檔案 主旨:{0},檔名{1}", subject, file));
         
        this.GridView1.DataBind();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del") {
            int index = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;

            int id = Convert.ToInt32(this.GridView1.DataKeys[index].Value);


            using (NXEIPEntities model = new NXEIPEntities()) {
                doc11 d11 = new doc11();
                d11.d11_no = id;

                model.doc11.Attach(d11);

                d11.d11_status = "2";

                model.SaveChanges();
            }
            OperatesObject.OperatesExecute(200105, 4, String.Format("刪除回傳檔案 d11_no:{0}", id));
        
            this.GridView1.DataBind();
        }
    }
}