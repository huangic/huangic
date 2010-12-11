using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;
using NLog;

public partial class _30_300500_300502_1 : System.Web.UI.Page
{
    private static Logger logger = LogManager.GetCurrentClassLogger();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            String mode = Request.QueryString["mode"];
            String ID = Request.QueryString["ID"];

            this.hidden_flo_no.Value = ID;

           

            if (mode != null && mode.Equals("edit"))
            {
                this.Navigator1.SubFunc = "修改樓層部門";
                //設定要變更的欄位
                int flo_no=int.Parse(ID);
                using (NXEIPEntities model = new NXEIPEntities()) {
                    floors f = (from d in model.floors where d.flo_no == flo_no select d).First();
                    this.tbx_flo_level.Text = f.flo_level;
                    this.tbx_flo_name.Text = f.flo_name;
                    this.tbx_flo_ename.Text = f.flo_ename;
                    this.tbx_flo_order.Text = f.flo_order+"";
                    
                    this.ddl_spot.SelectedValue = f.spo_no+"";


                }






            }
            else
            {
                //新增模式
                logger.Info("mode:new");
                this.Navigator1.SubFunc = "新增樓層部門";
                this.tbx_flo_order.Text = "0";
                this.tbx_flo_level.Text = "1";
            }
        }
    }

    protected void btn_ok_Click(object sender, EventArgs e)
    {
        logger.Debug("OK");
        logger.Debug(Request["clientID"]);

        String msg = "";

        msg=CheckInput();

        if (!String.IsNullOrEmpty(msg)) { 
            JsUtil.AlertJs(this,msg);
            
            
        }else{


        //判斷模式
        if (this.hidden_flo_no.Value != "")
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
    }

    private void Adding()
    {

        try
        {

            using (NXEIPEntities model = new NXEIPEntities())
            {

                floors f = new floors();

                f.spo_no = int.Parse(this.ddl_spot.SelectedValue);
                f.flo_name = this.tbx_flo_name.Text;
                f.flo_ename = this.tbx_flo_ename.Text;
                f.flo_level = this.tbx_flo_level.Text;
                f.flo_createtime = DateTime.Now;
                f.flo_createuid = int.Parse(new SessionObject().sessionUserID);
                f.flo_status = "1";
                f.flo_order = int.Parse(this.tbx_flo_order.Text);
                model.floors.AddObject(f);
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

                floors f = new floors();

                f.flo_no = int.Parse(this.hidden_flo_no.Value);

                model.floors.Attach(f);

                f.spo_no = int.Parse(this.ddl_spot.SelectedValue);
                f.flo_name = this.tbx_flo_name.Text;
                f.flo_ename = this.tbx_flo_ename.Text;
                f.flo_level = this.tbx_flo_level.Text;
                f.flo_createtime = DateTime.Now;
                f.flo_createuid = int.Parse(new SessionObject().sessionUserID);
                f.flo_order = int.Parse(this.tbx_flo_order.Text);
                
                model.SaveChanges();
            }


                      
        }
        catch
        {
            
        }
        
    }

    private String CheckInput() {

        String msg = "";

        if (String.IsNullOrEmpty(this.tbx_flo_level.Text)) {
            msg += "請輸入樓層\\n";
        }

        if (String.IsNullOrEmpty(this.tbx_flo_name.Text)) {
            msg += "請輸入部門中文名稱\\n";
        }

        if (String.IsNullOrEmpty(this.tbx_flo_order.Text)) {
            this.tbx_flo_order.Text = "0";
        }

        if (!String.IsNullOrEmpty(msg)) {
            return msg;
            
        }

        return "";
    }

}
