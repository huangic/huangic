using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;
using NLog;

public partial class _30_300600_300603_1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if (Request.QueryString["mode"] == "modify")
            {
                this.Navigator1.SubFunc = "編輯維修項目";
                this.hidd_r05no.Value = Request.QueryString["r05_no"];

                Rep05DAO dao = new Rep05DAO();

                this.TextBox1.Text = dao.GetRep05Name(int.Parse(this.hidd_r05no.Value));

            }
            else
            {
                this.Navigator1.SubFunc = "新增維修項目";

            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (this.TextBox1.Text.Trim().Length == 0)
        {
            JsUtil.AlertJs(this,"請輸入維修項目名稱!");
            return;
        }

        if (this.TextBox1.Text.Trim().Length > 20)
        {
            JsUtil.AlertJs(this, "項目名稱長度不得大於20!");
            return;
        }

        Rep05DAO dao = new Rep05DAO();
        string msg = "";

        if (this.hidd_r05no.Value != "")
        {

            rep05 data = dao.GetRep05(int.Parse(this.hidd_r05no.Value));

            data.r05_name = this.TextBox1.Text.Trim();
            data.r05_createuid = int.Parse(new SessionObject().sessionUserID);
            data.r05_createtime = DateTime.Now;
            dao.Update();

            msg = "修改完成!";

            OperatesObject.OperatesExecute(300601, 3, string.Format("修改維修項目 r05_no:{0}", data.r05_no));
        }
        else
        {
            rep05 data = new rep05();
            data.r05_name = this.TextBox1.Text.Trim();
            data.r05_createuid = int.Parse(new SessionObject().sessionUserID);
            data.r05_createtime = DateTime.Now;
            data.r05_status = "1";
            dao.AddToRep05(data);
            dao.Update();

            msg = "新增完成!";

            OperatesObject.OperatesExecute(300601, 1, string.Format("新增維修項目 r05_no:{0}", data.r05_no));
        }

        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "closeThickBox", "self.parent.update('" + msg + "');", true);
    }
}