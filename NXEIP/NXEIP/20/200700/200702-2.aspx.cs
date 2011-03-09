using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;

public partial class _20_200700_200702_2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if (Request.QueryString["ask_no"] != null)
            {
                this.hidden_no.Value = Request.QueryString["ask_no"];
                UtilityDAO udao = new UtilityDAO();
                _200702DAO dao = new _200702DAO();

                ask d = dao.Get_ask(int.Parse(this.hidden_no.Value));

                this.lab_question.Text = d.ask_question;
                this.lab_peoname.Text = udao.Get_PeopleName(d.ask_peouid.Value);
                this.lab_depname.Text = udao.Get_DepartmentName(d.ask_depno.Value);
                this.lab_date.Text = new ChangeObject()._ADtoROCDT(d.ask_date.Value);

                if (!string.IsNullOrEmpty(d.ask_answer))
                {
                    this.tbox_ans.Text = d.ask_answer;
                }
            }
        }
    }

    protected void btn_ok_Click(object sender, EventArgs e)
    {
        if (this.tbox_ans.Text.Trim().Length == 0)
        {
            JsUtil.AlertJs(this, "請輸入回覆!!");
            return;
        }
        else
        {
            SessionObject sobj = new SessionObject();
            _200702DAO dao = new _200702DAO();

            ask d = dao.Get_ask(int.Parse(this.hidden_no.Value));

            d.ask_rdate = DateTime.Now;
            d.ask_rdepno = int.Parse(sobj.sessionUserDepartID);
            d.ask_rpeouid = int.Parse(sobj.sessionUserID);
            d.ask_answer = this.tbox_ans.Text.Trim();

            dao.Update();

            OperatesObject.OperatesExecute(200702, 3, "回覆問題 ask_no:" + this.hidden_no.Value);

            //呼叫UPATE()關閉此頁面 並且更新updatepanel (parent page 必須做一個UPDATE的FUNCTION)
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "closeThickBox", "self.parent.update('回覆完成!');", true);
        }
    }
}