using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using NXEIP.DAO;
using Entity;

public partial class _35_350100_350101_1 : System.Web.UI.Page
{
    private NXEIPEntities model = new NXEIPEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            String mode = Request.QueryString["mode"];
            int rol_no = int.Parse(Request.QueryString["ID"]);

            this.hidden_role_no.Value = rol_no.ToString();

            if (mode != null && mode.Equals("edit"))
            {
                //取角色資料
                role data = (from d in model.role where d.rol_no == rol_no select d).FirstOrDefault();
                this.tbx_role_name.Text = data.rol_name;
                this.tbx_role_memo.Text = data.rol_memo;

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
        if (this.tbx_role_memo.Text.Length > 100)
        {
            this.ShowMsg("角色備註字數過長，限制字數為100字元");
            return;
        }
        if (this.tbx_role_name.Text.Trim().Length == 0)
        {
            this.ShowMsg("請輸入角色名稱");
        }
        else
        {
            String msg = "";
            role d = null;

            //判斷模式
            if (this.hidden_role_no.Value != "")
            {
                msg = "修改成功";
                int rol_no = int.Parse(this.hidden_role_no.Value);
                d = (from x in model.role where x.rol_no == rol_no select x).FirstOrDefault();
            }
            else
            {
                msg = "新增成功";
                d = new role();
            }

            d.rol_createuid = int.Parse(new SessionObject().sessionUserID);
            d.rol_createtime = DateTime.Now;
            d.rol_name = this.tbx_role_name.Text;
            d.rol_memo = this.tbx_role_memo.Text;
            if (this.hidden_role_no.Value == "")
            {
                model.role.AddObject(d);
            }
            model.SaveChanges();

            //呼叫UPATE()關閉此頁面 並且更新updatepanel (parent page 必須做一個UPDATE的FUNCTION) 
            this.Page.ClientScript.RegisterStartupScript(typeof(_35_350100_350101_1), "closeThickBox", "self.parent.update('" + msg + "');", true);
        }
    }

    private void ShowMsg(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "msg", "alert('" + msg + "');", true);
    }
}
