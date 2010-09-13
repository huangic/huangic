using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class _35_350100_350101_1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            String mode = Request.QueryString["mode"];
            String rol_no = Request.QueryString["ID"];

            this.hidden_role_no.Value = rol_no;

            if (mode != null && mode.Equals("edit"))
            {

                //取角色資料
                string sql = "select rol_name,rol_memo from role where rol_no = " + rol_no;
                DataTable mytable = new DBObject().ExecuteQuery(sql);

                this.tbx_role_name.Text = mytable.Rows[0]["rol_name"].ToString();
                this.tbx_role_memo.Text = mytable.Rows[0]["rol_memo"].ToString();

                this.Navigator1.SubFunc = "修改角色";
            }
            else
            {
                //新增模式
                this.Navigator1.SubFunc = "新增角色";
            }
        }
    }

    protected void btn_ok_Click(object sender, EventArgs e)
    {
        String msg = "";

        //判斷模式
        if (this.hidden_role_no.Value != "")
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
        this.Page.ClientScript.RegisterStartupScript(typeof(_35_350100_350101_1), "closeThickBox", "self.parent.update('" + msg + "');", true);

    }

    private void Adding()
    {
        if (this.tbx_role_name.Text.Trim().Length == 0)
        {
            this.ShowMsg("請輸入角色名稱");
        }
        else
        {
            string peo_uid = new SessionObject().sessionUserID;
            string sql = "insert into role (rol_name,rol_memo,rol_createuid,rol_createtime) values ('" + this.tbx_role_name.Text.Trim() + "','" + this.tbx_role_memo.Text + "'," + peo_uid + ",GETDATE())";

            new DBObject().ExecuteNonQuery(sql);

        }
    }

    private void Editing()
    {
        if (this.tbx_role_name.Text.Trim().Length == 0)
        {
            this.ShowMsg("請輸入角色名稱");
        }
        else
        {
            string peo_uid = new SessionObject().sessionUserID;
            string sql = "update role set rol_name='" + this.tbx_role_name.Text.Trim() + "',rol_memo='" + this.tbx_role_memo.Text + "',rol_createuid=" + peo_uid + ",rol_createtime=GETDATE() where rol_no = " + this.hidden_role_no.Value;

            new DBObject().ExecuteNonQuery(sql);

        }
    }

    private void ShowMsg(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "msg", "alert('" + msg + "');", true);
    }
}
