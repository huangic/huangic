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

            using(NXEIPEntities model=new NXEIPEntities()){

            sys06 sys = (from d in model.sys06 where d.s06_status == "1" && d.sfu_no == 200107 orderby d.s06_order orderby d.s06_no select d).First();


            this.hidden_cat.Value = sys.s06_no.ToString();

            }



            this.Search();
        }
        else { 
            
        
        }

        ShowStatus();
       

        this.GridView1.DataBind();
    }




    private void ShowStatus() {

        //判斷
        if (this.hidden_show_myfile.Value != "1")
        {
            this.GridView1.Columns[6].Visible = false;
            this.GridView1.Columns[7].Visible = false;
            this.lb_status.Visible = false;
            this.DropDownList1.Visible = false;
        }
        else
        {
            this.GridView1.Columns[6].Visible = true;
            this.GridView1.Columns[7].Visible = true;
            this.lb_status.Visible = true;
            this.DropDownList1.Visible = true;
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

        if (this.hidden_show_myfile.Value != "1")
        {

            Search();
        }
        else {
            SearchMyData();
        }
    }


    private void Search() {
       
        
        String dep_no = this.hidden_depno.Value;
        //String keyword = "";
        String cat = "";
        String file = "";

        cat = String.IsNullOrEmpty(this.hidden_childcat.Value) ? this.hidden_cat.Value : this.hidden_childcat.Value;


        //keyword = this.tb_word.Text;


        file = this.tb_file.Text;


        this.GridView1.DataSourceID = "ObjectDataSource3";

        this.ObjectDataSource3.SelectParameters[0].DefaultValue = dep_no;
        this.ObjectDataSource3.SelectParameters[1].DefaultValue = cat;
        this.ObjectDataSource3.SelectParameters[2].DefaultValue = file;

        ShowStatus();
        OperatesObject.OperatesExecute(200107, 2, String.Format("查詢檔案區 條件 部門:{0},分類:{1},檔名{0}", dep_no, cat, file));
                
        this.GridView1.DataBind();
    }

    private void SearchMyData()
    {


        SessionObject sessionObj = new SessionObject();


        string status = this.DropDownList1.SelectedValue;
        String cat = "";
        String file = "";

        cat = String.IsNullOrEmpty(this.hidden_childcat.Value) ? this.hidden_cat.Value : this.hidden_childcat.Value;


        //keyword = this.tb_word.Text;


        file = this.tb_file.Text;


        this.GridView1.DataSourceID = "ObjectDataSource_mydata";

        this.ObjectDataSource_mydata.SelectParameters[0].DefaultValue = sessionObj.sessionUserID;
        this.ObjectDataSource_mydata.SelectParameters[1].DefaultValue = cat;
        
        this.ObjectDataSource_mydata.SelectParameters[2].DefaultValue = file;
        this.ObjectDataSource_mydata.SelectParameters[3].DefaultValue = status;

        ShowStatus();
        OperatesObject.OperatesExecute(200107, 2, String.Format("查詢檔案區我的檔案 條件 狀態:{0},分類:{1},檔名{0}", status, cat, file));
        
        this.GridView1.DataBind();
    }


    protected void lv_cat_ItemCommand(object sender, ListViewCommandEventArgs e) {
        if (e.CommandName == "click_cat") { 
            int index=Convert.ToInt32(e.Item.DataItemIndex);


            this.hidden_cat.Value = this.lv_cat.DataKeys[index].Value.ToString();
            this.hidden_childcat.Value = "";

            //e.Item
            //LinkButton lb =(LinkButton)e.Item.Parent;

            //lb.CssClass = "a-letter-s1";
           
           this.lv_child.DataBind();

        

          //顯示子項目的DIV
               this.childDiv.Visible = this.lv_child.Items.Count > 0;
          


           this.lv_cat.DataBind();

           this.tb_file.Text = "";

           if (hidden_show_myfile.Value != "1")
           {

               this.Search();
           }
           else {
               this.SearchMyData();
           }

        }
    }


    protected void lv_child_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        if (e.CommandName == "click_childcat")
        {
            int index = Convert.ToInt32(e.Item.DataItemIndex);


            this.hidden_childcat.Value = this.lv_child.DataKeys[index].Value.ToString();

            //e.Item
            //LinkButton lb =(LinkButton)e.Item.Parent;

            //lb.CssClass = "a-letter-s1";
            this.lv_child.DataBind();
            if (hidden_show_myfile.Value != "1")
            {

                this.Search();
            }
            else
            {
                this.SearchMyData();
            }
        }
    }
    protected void btn_all_Click(object sender, EventArgs e)
    {
        this.hidden_depno.Value = "";
        this.hidden_show_myfile.Value = "";
        this.Search();
      
    }
    protected void btn_dep_Click(object sender, EventArgs e)
    {
        SessionObject sessionObj = new SessionObject();
        this.hidden_depno.Value = sessionObj.sessionUserDepartID;
        this.hidden_show_myfile.Value = "";
        this.Search();
      
       
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            int index = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;

            int id = Convert.ToInt32(this.GridView1.DataKeys[index].Value);


            using (NXEIPEntities model = new NXEIPEntities())
            {
                doc09 d09 = new doc09();
                d09.d09_no = id;
                model.doc09.Attach(d09);
                d09.d09_status = "4";


                /*
                var d10 = (from d in model.doc10 where d.d09_no == id select d);



                foreach (var d in d10)
                {
                    model.doc10.DeleteObject(d);
                }


                model.doc09.DeleteObject(d09);
                */
                OperatesObject.OperatesExecute(200107, 4, String.Format("刪除檔案區 d09_no:{0}", id));
                
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


    protected string GetStatus(string status) {
        if (status == "1") {
            return "通過";
        }
        if (status == "2")
        {
            return "未通過";
        }
        if (status == "3")
        {
            return "審核中";
        }
        if (status == "4")
        {
            return "刪除";
        }
        return "";
    }

    protected void btn_my_Click(object sender, EventArgs e)
    {
        this.hidden_show_myfile.Value = "1";
       
     
        this.SearchMyData();
    }
}