using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;
using NLog;

public partial class _30_300900_300901_1 : System.Web.UI.Page
{
    private static Logger logger = LogManager.GetCurrentClassLogger();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            String mode = Request.QueryString["mode"];
            String ID = Request.QueryString["ID"];

            this.hidden_no.Value = ID;

            this.DepartTreeTextBox1.Clear();

            if (mode != null && mode.Equals("edit"))
            {
                this.Navigator1.SubFunc = "修改表單";
                //設定要變更的欄位
                int no=int.Parse(ID);
                using (NXEIPEntities model = new NXEIPEntities()) {
                    form01 f = (from d in model.form01 where d.f01_no == no select d).First();

                    this.tb_name.Text = f.f01_name;
                    this.tb_description.Text = f.f01_description;

                    this.DepartTreeTextBox1.Add(f.peo_uid);
                
                
                }






            }
            else
            {
                //新增模式
                logger.Info("mode:new");
                this.Navigator1.SubFunc = "新增表單";
                this.DepartTreeTextBox1.Add(new SessionObject().sessionUserID);
            }
        }
    }

    protected void btn_ok_Click(object sender, EventArgs e)
    {
        logger.Debug("OK");
        logger.Debug(Request["clientID"]);

        String msg = "";

        //判斷模式
        if (this.hidden_no.Value != "")
        {
            Editing();
            msg = "修改成功";
        }
        else
        {
            Adding();
            msg = "新增成功";
        }

        

        //呼叫UPATE()關閉此頁面 並且更新updatepanel (parent page 必須做一個UPDATE的FUNCTION) 
        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "closeThickBox", "self.parent.update('" + msg + "');", true);


    }

    private void Adding()
    {

        try
        {

            using (NXEIPEntities model = new NXEIPEntities())
            {

               
                model.SaveChanges();
            }
            
        }
        catch
        {

        }

    }

    private void Editing()
    {
        try
        {

            using (NXEIPEntities model = new NXEIPEntities())
            {

               
                
                model.SaveChanges();
            }


                      
        }
        catch
        {
            
        }
        
    }


    private void settingForm(){
    
    }
}
