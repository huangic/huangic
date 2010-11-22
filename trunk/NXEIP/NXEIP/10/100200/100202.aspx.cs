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

public partial class _10_100200_100202 : System.Web.UI.Page
{
    private static Logger logger = LogManager.GetCurrentClassLogger();
    
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack) { 
            //建立GRID VIEW
            //this.GridView1.DataBind();


            //NXEIPEntities model = new NXEIPEntities();


            this.ObjectDataSource_treat.SelectParameters[0].DefaultValue = this.DropDownList1.SelectedValue;
            this.ObjectDataSource_treat.SelectParameters[1].DefaultValue = new SessionObject().sessionUserID;
            this.ObjectDataSource_treat.SelectParameters[2].DefaultValue = this.tb_keyword.Text;

            
            
        }
     
            this.GridView1.DataBind();
            
    }

  
    
    protected static bool GetModifyVisible(int peo_uid) { 
        
        SessionObject session = new SessionObject();
        return (int.Parse(session.sessionUserID) == peo_uid);
            
        
    }

    protected static bool GetReportVisible(int peo_uid)
    {

        SessionObject session = new SessionObject();
        return (int.Parse(session.sessionUserID) == peo_uid);


    }


    protected static string GetTreatStatus(int tra_peouid, int tde_peouid) {
        if (tra_peouid == tde_peouid)
        {
            return "待辦";
        }
        else {
            return "交辦"; 
        }
    }


    protected static string GetStatus(string status,DateTime dt) {
        switch (status) { 
            case "1":
                if (dt < DateTime.Now)
                {
                    return "執行中(逾期)";
                }
                else {
                    return "執行中";
                }             
                
                
                
                
            case "2":
                return "已完成";
            
        }
        return "";
    }

   
    /// <summary>
    /// 依條件查詢
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {

        string status = this.DropDownList1.SelectedValue;
        string keyword=this.tb_keyword.Text;
       

         this.ObjectDataSource_treat.SelectParameters[0].DefaultValue = this.DropDownList1.SelectedValue;
         this.ObjectDataSource_treat.SelectParameters[1].DefaultValue = new SessionObject().sessionUserID;
         this.ObjectDataSource_treat.SelectParameters[2].DefaultValue = this.tb_keyword.Text;


         this.GridView1.DataSourceID = "ObjectDataSource_treat";
         this.GridView1.DataBind();

         OperatesObject.OperatesExecute(200105, 2, "查詢待辦事項 狀態:{0},關鍵字{1}", status, keyword);
         
        this.GridView1.DataBind();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del") {
            int index = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;

            treatdetail detail = (treatdetail)(this.GridView1.DataKeys[index].Value);


            using (NXEIPEntities model = new NXEIPEntities()) {
                treatdetail d = new treatdetail();
                d.tde_no = detail.tde_no;

                model.treatdetail.Attach(d);

                d.tde_status = "3";

                model.SaveChanges();
            }
            OperatesObject.OperatesExecute(200105, 4, String.Format("刪除代辦 tde_no:{0}", detail.tde_no));
        
            this.GridView1.DataBind();
        }
    }
    protected void ShowPost_Click(object sender, EventArgs e)
    {

        this.ObjectDataSource_mytreat.SelectParameters[0].DefaultValue = new SessionObject().sessionUserID;
        
        
        this.GridView1.DataSourceID = "ObjectDataSource_mytreat";
        this.GridView1.DataBind();
        OperatesObject.OperatesExecute(200105, 2, "查詢交辦待辦事項");
    }
}