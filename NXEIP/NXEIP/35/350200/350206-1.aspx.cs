using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NXEIP.DAO;
using Entity;

public partial class _35_350200_350201_1 : System.Web.UI.Page
{

    private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
    

    protected void Page_Load(object sender, EventArgs e)
    {
        //編輯模式

      



        //送出後DISABLE按鈕
        this.btn_ok.Attributes.Add("onclick", "this.value='送出中...';this.disabled=true;" + 
            ClientScript.GetPostBackEventReference(this.btn_ok, "").ToString());

        if (!Page.IsPostBack)
        {
            this.DepartTreeTextBox1.Clear();
            this.peopleTreeTextBox2.Clear();
            
            
            String mode = Request.QueryString["mode"];
            
                        

            if (mode != null && mode.Equals("edit"))
            {
                logger.Info("mode:edit");
                int dep_no = int.Parse(Request.QueryString["dep_no"]);
                int man_no = int.Parse(Request["man_no"]);

                using (NXEIPEntities model = new NXEIPEntities())
                {

                    var m = (from d in model.manager where d.man_no == man_no && d.dep_no == dep_no select d).First();

                    this.RadioButtonList1.SelectedValue = m.man_type;
                    this.DepartTreeTextBox1.Add(m.dep_no);
                    this.peopleTreeTextBox2.Add(m.peo_uid);

                }
                

                



            }
            else
            {
                //新增模式
                this.Navigator1.SubFunc = "新增";
                logger.Info("mode:new");
            }

        }

    }
    protected void btn_ok_Click(object sender, EventArgs e)
    {
        logger.Debug("OK");

        String mode = Request.QueryString["mode"];

        String msg = "";

        if (CheckField(out msg))
        {




            if (mode == "edit")
            {
                logger.Debug("EDIT");
                int dep_no = int.Parse(Request.QueryString["dep_no"]);
                int man_no = int.Parse(Request["man_no"]);


                Edit(dep_no, man_no);
                msg = "修改成功";
            }
            else
            {
                Add();
                msg = "新增成功";
            }
            //呼叫UPATE()關閉此頁面 並且更新updatepanel (parent page 必須做一個UPDATE的FUNCTION)
            JsUtil.UpdateParentJs(this, msg);
        }
        else {
            JsUtil.AlertJs(this, msg);
        }

        

    }

    public void Add()
    {
       //
        using (NXEIPEntities model = new NXEIPEntities())
        {
            int dep_no=int.Parse( DepartTreeTextBox1.Value);
            
            
            manager m = new manager();
            m.dep_no = dep_no;

            int max = 0;
            try
            {
                max = (from d in model.manager where d.dep_no == dep_no select d.man_no).Max();
            }
            catch { 
            }

            max++;

            m.man_no = max;

            m.peo_uid = int.Parse(peopleTreeTextBox2.Value);
            m.man_type = RadioButtonList1.SelectedValue;

            model.manager.AddObject(m);
            model.SaveChanges();

        }
    }



    public void Edit(int dep_no,int man_no)
    {
        //刪除後再加入

        manager m = new manager();
        m.dep_no = dep_no;
        m.man_no = man_no;


        using (NXEIPEntities model = new NXEIPEntities()) {
            model.manager.Attach(m);

            model.manager.DeleteObject(m);

            model.SaveChanges();
        
        }


        Add();

        
    }

    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.RadioButtonList1.SelectedValue == "2") {
            this.DepartTreeTextBox1.Clear();
            this.DepartTreeTextBox1.Add(1);
        }
    }



    private bool CheckField(out String msg) {
        string error_msg = "";
        bool flag = true;

        if (this.DepartTreeTextBox1.Value.Trim().Equals("")) {

            error_msg+="請選擇部門\\n";
            flag = false;
        }

        if (this.peopleTreeTextBox2.Value.Trim().Equals(""))
        {

            error_msg += "請選擇人員\\n";
            flag = false;
        }



        msg = error_msg;
        return flag;
    }
}
