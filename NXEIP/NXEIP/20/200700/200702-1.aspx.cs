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
                var sfu_no = dao.Get_qatype("2").Select(o => o.qat_s06no).ToArray();
                var sysfuc = (from d in model.sysfuction where sfu_no.Contains(d.sfu_no) select d);
                foreach (var p in sysfuc)
                {
                    ListItem item = new ListItem(p.sfu_name, p.sfu_no.ToString());
                    this.ddl_sysfun.Items.Add(item);
                }

                //維修類別
                var r05_no = dao.Get_qatype("3").Select(o => o.qat_r05no).ToArray();
                var r05 = (from d in model.rep05 where r05_no.Contains(d.r05_no) select d);
                foreach (var p in r05)
                {
                    ListItem item = new ListItem(p.r05_name, p.r05_no.ToString());
                    this.ddl_r05.Items.Add(item);
                }
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
                qat_no = this.ddl_self.SelectedValue;
            }
            if (rb_s06no.Checked)
            {
                qat_no = this.ddl_sysfun.SelectedValue;
            }
            if (rb_r05no.Checked)
            {
                qat_no = this.ddl_r05.SelectedValue;
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