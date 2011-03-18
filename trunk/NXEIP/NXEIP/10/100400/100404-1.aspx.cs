using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity;
using NXEIP.DAO;
using IMMENSITY.SWFUploadAPI;
using System.Runtime.Serialization.Json;
using NXEIP.DynamicForm;
using NLog;
using Newtonsoft.Json;

public partial class _10_100400_100404_1 : System.Web.UI.Page
{

    private static Logger logger = LogManager.GetCurrentClassLogger();

    
    protected void Page_Load(object sender, EventArgs e)
    {
       
            //取ID 
            AbstractFormFactory factory = new EntityFormFactory(); 
            Form form= factory.GetInstance( Request["ID"]);

                this.lb_description.Text = form.Description;
                this.lb_name.Text = form.Name;
                this.lb_people.Text = form.HandleUser;
            


                //建立動態物件


                ColumnFactory ColumnFactoy = new ColumnFactory();
                List<WebControl[]> list = ColumnFactoy.ConvertColumsToWebControl(form.Columns);
                //使用者控制群
            
                //加入欄位

                //一欄是
                foreach (WebControl[] w in list) {
                    TableRow tr = new TableRow();
                    TableHeaderCell th = new TableHeaderCell();
                    th.Width = new Unit("200px");
                    th.Controls.Add(w[0]);
                    TableCell td = new TableCell();
                    td.Controls.Add(w[1]);
                    tr.Controls.Add(th);
                    tr.Controls.Add(td);

                    this.DynamicTable.Controls.Add(tr);
                }

        

        
       
    }




    protected void btn_ok_Click(object sender, EventArgs e)
    {
        //this
        AbstractFormFactory factory = new EntityFormFactory();
        Form form = factory.GetInstance(Request["ID"]);
        //取欄位
        ColumnFactory ColumnFactoy = new ColumnFactory();
        List<Column> list = ColumnFactoy.GetWebControlValue(this.DynamicTable, form.Columns);


        //欄位判斷

        String msg = "";

        foreach(var c in list ){
            if (c.Required) {
                if (c.Value == null || c.Value.Count == 0) {
                    msg += String.Format("{0}為必填欄位\\n", c.Name);
                }
            }
        }

        if (!String.IsNullOrEmpty(msg)) {
            JsUtil.AlertJs(this,msg);
            return;
        }



        //檔案存檔

        form02 f = new form02();

        f.peo_uid = int.Parse(new SessionObject().sessionUserID);
        f.f01_no = int.Parse(Request["ID"]);
        f.f02_context = JsonConvert.SerializeObject(list);
        f.f02_createtime = DateTime.Now;
        f.f02_createuid = int.Parse(new SessionObject().sessionUserID);


       // logger.Debug(JsonConvert.SerializeObject(list));
        using (NXEIPEntities model = new NXEIPEntities()) {
            model.form02.AddObject(f);
            model.SaveChanges();
        }
        

        OperatesObject.OperatesExecute(200108,1,"提交表單 ID:{0}",f.f01_no);

        //Response.Redirect();

        JsUtil.AlertAndRedirectJs(this, "新增成功", String.Format("100404.aspx"));

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        JsUtil.RedirectJs(this, String.Format("100404.aspx"));
    }
}
