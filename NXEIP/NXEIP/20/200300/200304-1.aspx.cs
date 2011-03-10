using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMMENSITY.SWFUploadAPI;
using System.Runtime.Serialization.Json;
using Entity;
using System.IO;
using System.Collections.Specialized;
using AjaxControlToolkit;
using NXEIP.DAO;
using System.Data.Objects.SqlClient;

public partial class _20_200300_200304_1 : System.Web.UI.Page
{

    SessionObject sessionObj = new SessionObject();
    


    protected void Page_Load(object sender, EventArgs e)
    {

        String mode = Request["mode"];

        //init
        if (!Page.IsPostBack) {
            int peo_uid=int.Parse(sessionObj.sessionUserID);
                
            this.Label2.Text = sessionObj.sessionUserName;


            //edit
            if (!String.IsNullOrEmpty(mode)) {
                int id = int.Parse(Request["id"]);
                
                
                
                using (NXEIPEntities model = new NXEIPEntities()) {
                    var c = (from d in model.cooperactive where d.coo_no == id select d).FirstOrDefault();
                    this.tb_name.Text = c.coo_name;
                    this.tb_price.Text = c.coo_price.Value+"";
                    this.ddl_cat.SelectedValue = c.coo_s06no + "";
                
                }
            }



           
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        
        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "closeThickBox", "self.parent.update();", true);

    }
    protected void btn_ok_Click(object sender, EventArgs e)
    {
        
            //欄位判斷

        String msg = CheckFields();
        if (!String.IsNullOrEmpty(msg)) {
            JsUtil.AlertJs(this, msg);
            return;
        }
        String mode = Request["mode"];

        if (String.IsNullOrEmpty(mode))
        {

            Add();
        }
        else {
            Edit();
        }
           




            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "closeThickBox", "self.parent.update();", true);

      
    }


    private void Add() { 
    
         string cat = this.ddl_cat.SelectedValue;

            int cat_no = int.Parse(cat);

            
            //存檔
            using (NXEIPEntities model = new NXEIPEntities())
            {
                cooperactive c = new cooperactive();
                c.coo_createtime = DateTime.Now;
                c.coo_createuid = int.Parse(sessionObj.sessionUserID);
                c.coo_name = this.tb_name.Text;
                c.coo_price = int.Parse(this.tb_price.Text);
                c.coo_s06no = cat_no;
                c.coo_status = "1";
                model.cooperactive.AddObject(c);
                model.SaveChanges();
                

                OperatesObject.OperatesExecute(200304, 1, String.Format("新增商品 coo_no:{0}", c.coo_no));
            }
                //文檔存檔
                
    }

    private void Edit()
    {

        string cat = this.ddl_cat.SelectedValue;

        int cat_no = int.Parse(cat);
        int id = int.Parse(Request["id"]);

        //存檔
        using (NXEIPEntities model = new NXEIPEntities())
        {
            cooperactive c = new cooperactive();
            c.coo_no=id;
            model.cooperactive.Attach(c);

            c.coo_createtime = DateTime.Now;
            c.coo_createuid = int.Parse(sessionObj.sessionUserID);
            c.coo_name = this.tb_name.Text;
            c.coo_price = int.Parse(this.tb_price.Text);
            c.coo_s06no = cat_no;
            //c.coo_status = "1";
            
            model.SaveChanges();


            OperatesObject.OperatesExecute(200304, 1, String.Format("修改商品 coo_no:{0}", c.coo_no));
        }
        //文檔存檔

    }


    private String CheckFields() { 
    
        String msg=String.Empty;


        if (String.IsNullOrEmpty(this.ddl_cat.SelectedValue))
        {
            msg += "請選擇類別\\n";
        }
        
        
        
        
        if (String.IsNullOrEmpty(this.tb_name.Text))
        {
            msg += "請輸入商品名稱\\n";
        }
        
        
        try
        {
            int.Parse(this.tb_price.Text);

        }
        catch {
            msg+= "價格請輸入數字\\n";
        }


        return msg;

    }
   
}