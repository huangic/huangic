using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;
using NLog;
using NXEIP.DynamicForm;
using Newtonsoft.Json;

public partial class _30_300900_300901_7 : System.Web.UI.Page
{
    private static Logger logger = LogManager.GetCurrentClassLogger();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            String mode = Request.QueryString["mode"];
            String ID = Request.QueryString["ID"];
            String UID = Request.QueryString["UID"];

            this.hidden_id.Value = ID;
            this.hidden_uid.Value = UID;
           

            if (mode != null && mode.Equals("edit"))
            {
                this.Navigator1.SubFunc = "修改表尾欄位";
                //設定要變更的欄位
                int no=int.Parse(ID);
            
                
                //取欄位
                Form01DAO dao = new Form01DAO();

                var column=dao.GetFooterByFormNO(no);

                Form f = new Form();

                f.Footer = column.ToList();

                Column c = f.GetFooter(UID);

                this.tb_name.Text = c.Name;
                this.tb_description.Text = c.Description;

              

               
             
                

            }
            else
            {
                //新增模式
                logger.Info("mode:new");
                this.Navigator1.SubFunc = "新增表尾欄位";
                //this.DepartTreeTextBox1.Add(new SessionObject().sessionUserID);
            }
        }
    }

    protected void btn_ok_Click(object sender, EventArgs e)
    {
        logger.Debug("OK");
        logger.Debug(Request["clientID"]);

        String msg = "";

        //判斷模式
        if (this.hidden_uid.Value != "")
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
        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "closeThickBox", "self.parent.update('2','" + msg + "');", true);


    }

    private void Adding()
    {

        try
        {

            

            using (NXEIPEntities model = new NXEIPEntities())
            {
                int id = int.Parse(this.hidden_id.Value);
                Form01DAO dao = new Form01DAO();
                var columns=dao.GetFooterByFormNO(id).ToList();
              

                Column col = new Column();

                col.UID = Guid.NewGuid().ToString();
                col.Name = this.tb_name.Text;
                col.Description = this.tb_description.Text;
                col.MinLength = 0;
               

                columns.Add(col);

                form01 form = new form01 { f01_no = id };

                model.form01.Attach(form);


                form.f01_footer = JsonConvert.SerializeObject(columns);

                    
                
                //取COLUMN


               
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
                int id = int.Parse(this.hidden_id.Value);
                Form01DAO dao = new Form01DAO();
                var columns = dao.GetFooterByFormNO(id).ToList();

                Form f = new Form();

                f.Footer = columns.ToList();

                Column col = f.GetFooter(this.hidden_uid.Value);

               
                col.Name = this.tb_name.Text;
                col.Description = this.tb_description.Text;
                col.MinLength = 0;
                

                





                

                form01 form = new form01 { f01_no = id };

                model.form01.Attach(form);


                form.f01_footer = JsonConvert.SerializeObject(f.Footer);



                //取COLUMN



                model.SaveChanges();
            }

                      
        }
        catch
        {
            
        }
        
    }


   
   

   
    

   
   
    protected void btn_countinue_Click(object sender, EventArgs e)
    {
        string msg = "";
        //判斷模式
        if (this.hidden_uid.Value != "")
        {
            Editing();
            msg = "修改成功";
        }
        else
        {
            Adding();
            msg = "新增成功";
        }

        //清空為新增
       
        String url = String.Format("300901-7.aspx?ID={0}", this.hidden_id.Value);

        //轉址

        String script = String.Format("self.parent.updateStatus('2','{0}');window.location.href='{1}'", msg, url);



        JsUtil.CallJs(this, script);              

        //JsUtil.AlertAndUpdateParentAndRedirectJs(this, msg, url);
        
        
    }
}
