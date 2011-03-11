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

public partial class _20_200300_200304 : System.Web.UI.Page
{
    private static Logger logger = LogManager.GetCurrentClassLogger();
    
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {

            using(NXEIPEntities model=new NXEIPEntities()){

            sys06 sys = (from d in model.sys06 where d.s06_status == "1" && d.sfu_no == 200304 orderby d.s06_order orderby d.s06_no select d).FirstOrDefault();

            try
            {
                this.hidden_cat.Value = sys.s06_no.ToString();
            }
            catch { 
            
            }
            }



            this.Search();
        }
        else { 
            
        
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

   

     


    protected static bool GetModifyVisible(int peo_uid) { 
        //HttpContext.Current.Session[""]
        SessionObject session = new SessionObject();
        return (int.Parse(session.sessionUserID) == peo_uid);
            
        
    }

   

    
    /// <summary>
    /// 依條件查詢
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {

        

            Search();
       
        
    }


    private void Search() {
       
        
        
        String cat = "";
        String file = "";

        cat = this.hidden_cat.Value;


        //keyword = this.tb_word.Text;


        file = this.tb_file.Text;


        

        //this.ObjectDataSource3.SelectParameters[0].DefaultValue = dep_no;
        this.ObjectDataSource3.SelectParameters[0].DefaultValue = cat;
        this.ObjectDataSource3.SelectParameters[1].DefaultValue = file;

        
        OperatesObject.OperatesExecute(200107, 2, String.Format("查詢合作社 條件 分類:{1},商品名{0}", cat, file));
                
        this.GridView1.DataBind();
    }

   


    protected void lv_cat_ItemCommand(object sender, ListViewCommandEventArgs e) {
        if (e.CommandName == "click_cat") { 
            int index=Convert.ToInt32(e.Item.DataItemIndex);


            this.hidden_cat.Value = this.lv_cat.DataKeys[index].Value.ToString();



            this.lv_cat.DataBind();
        

          //顯示子項目的DIV
           

               this.Search();
           
        }
    }



    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            int index = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;

            int id = Convert.ToInt32(this.GridView1.DataKeys[index].Value);


            using (NXEIPEntities model = new NXEIPEntities())
            {
                cooperactive c = new cooperactive();
                c.coo_no = id;
                model.cooperactive.Attach(c);
                c.coo_status = "2";


               


               
                OperatesObject.OperatesExecute(200107, 4, String.Format("刪除合作社商品 coo_no:{0}", id));
                
                model.SaveChanges();
            }
            this.GridView1.DataBind();
        }
    }

    protected string GetPeopleName(int? peoUid) {
        if (peoUid.HasValue)
        {
            return new PeopleDAO().GetPeopleNameByUid(peoUid.Value);
        }
        else {
            return "";
        }
    }

    protected string GetROCDate(DateTime? date)
    {
        if (date.HasValue)
        {
            return new ChangeObject()._ADtoROC(date.Value).ToString() ;
        }
        else
        {
            return "";
        }
    }


   

  
}