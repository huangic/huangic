using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity;
using NXEIP.DAO;

public partial class _20_200700_200702_1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            using (NXEIPEntities model = new NXEIPEntities())
            {
                //業務類別
                _200702DAO dao = new _200702DAO();
                var sfu_no = dao.Get_qatype("2");
                foreach (var p in sfu_no)
                {
                    string sfu_name = (from d in model.sysfuction
                                     where d.sfu_no == p.qat_s06no
                                     select d.sfu_name).FirstOrDefault();
                    ListItem item = new ListItem(sfu_name,p.qat_no.ToString());
                    this.ddl_sysfun.Items.Add(item);
                }
                this.ddl_sysfun.Items.Insert(0, new ListItem("請選擇", "0"));

                //維修類別
                var r05_no = dao.Get_qatype("3");
                foreach (var p in r05_no)
                {
                    string r05_name = (from d in model.rep05
                                       where d.r05_no == p.qat_r05no
                                       select d.r05_name).FirstOrDefault();

                    ListItem item = new ListItem(r05_name, p.qat_no.ToString());
                    this.ddl_r05.Items.Add(item);
                }
                this.ddl_r05.Items.Insert(0, new ListItem("請選擇", "0"));
            }

            SessionObject sobj = new SessionObject();

            this.lab_date.Text = new ChangeObject()._ADtoROC(DateTime.Now);
            this.lab_depname.Text = sobj.sessionUserDepartName;
            this.lab_peoname.Text = sobj.sessionUserName;
        }
    }

    protected void btn_ok_Click(object sender, EventArgs e)
    {
        if (this.tbox_question.Text.Trim().Length == 0)
        {
            JsUtil.AlertJs(this, "請輸入問題!!");
        }
        else
        {
            SessionObject sobj = new SessionObject();

            string qat_no = "0";

            if (rb_self.Checked)
            {
                if (this.ddl_self.SelectedValue == "0")
                {
                    JsUtil.AlertJs(this, "請選擇其它類別");
                    return;
                }
                else
                {
                    qat_no = this.ddl_self.SelectedValue;
                }
            }
            if (rb_s06no.Checked)
            {
                if (this.ddl_sysfun.SelectedValue == "0")
                {
                    JsUtil.AlertJs(this, "請選擇業務資訊類別");
                    return;
                }
                else
                {
                    qat_no = this.ddl_sysfun.SelectedValue;
                }
                
            }
            if (rb_r05no.Checked)
            {
                if (this.ddl_r05.SelectedValue == "0")
                {
                    JsUtil.AlertJs(this, "請選擇維修類別");
                    return;
                }
                else
                {
                    qat_no = this.ddl_r05.SelectedValue;
                }
            }

            ask d = new ask();

            d.qat_no = int.Parse(qat_no);
            d.ask_peouid = int.Parse(sobj.sessionUserID);
            d.ask_depno = int.Parse(sobj.sessionUserDepartID);
            d.ask_question = this.tbox_question.Text.Trim();
            d.ask_date = DateTime.Now;
            d.ask_status = "1";
            d.ask_createuid = int.Parse(sobj.sessionUserID);
            d.ask_createtime = DateTime.Now;

            _200702DAO dao = new _200702DAO();
            dao.AddToAsk(d);
            dao.Update();

            OperatesObject.OperatesExecute(200702, 1, "新增Q&A問題 ask_no:" + d.ask_no);

            //呼叫UPATE()關閉此頁面 並且更新updatepanel (parent page 必須做一個UPDATE的FUNCTION)
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "closeThickBox", "self.parent.update('新增完成!');", true);

        }
    }
}