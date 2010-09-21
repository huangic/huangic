using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;

public partial class _35_350300_350302_1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            String mode = Request.QueryString["mode"];
            String arg_no = Request["arg_no"];

            this.hidden_arg_no.Value = arg_no;

            if (mode != null && mode.Equals("edit"))
            {
                this.Navigator1.SubFunc = "修改";
                
                //取資料
                arguments data = new ArgumentsDAO().GetByArgNo(Convert.ToInt32(this.hidden_arg_no.Value));

                this.tbox_des.Text = data.arg_describe;
                this.tbox_name.Text = data.arg_variable;
                this.tbox_var.Text = data.arg_value;

            }
            else
            {
                //新增模式
                this.Navigator1.SubFunc = "新增";
                
            }
        }
    }

    protected void btn_ok_Click(object sender, EventArgs e)
    {
        ArgumentsDAO dao = new ArgumentsDAO();
        
        //參數名稱是否重覆
        if (string.IsNullOrEmpty(this.tbox_var.Text))
        {
            this.ShowMsg("請輸入參數名稱!");
            return;
        }
        else
        {
            if (dao.GetByCheck(this.tbox_var.Text) > 0)
            {
                this.ShowMsg("參數名稱已存在!");
                return;
            }
        }

        if (string.IsNullOrEmpty(this.tbox_des.Text))
        {
            this.ShowMsg("請輸入參數說明!");
            return;
        }
        else
        {
            string msg = "";

            if (this.hidden_arg_no.Value != "")
            {
                arguments data = dao.GetByArgNo(Convert.ToInt32(this.hidden_arg_no.Value));

                data.arg_describe = this.tbox_des.Text;
                data.arg_variable = this.tbox_name.Text;
                data.arg_value = this.tbox_var.Text;

                dao.Update();

                msg = "修改完成!";
            }
            else
            {
                arguments data = new arguments();

                data.arg_describe = this.tbox_des.Text;
                data.arg_variable = this.tbox_name.Text;
                data.arg_value = this.tbox_var.Text;

                dao.AddArguments(data);
                dao.Update();

                msg = "新增完成!";
            }

            //呼叫UPATE()關閉此頁面 並且更新updatepanel (parent page 必須做一個UPDATE的FUNCTION)
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "closeThickBox", "self.parent.update('" + msg + "');", true);
        }

        


    }

    private void ShowMsg(string msg)
    {
        this.ClientScript.RegisterStartupScript(this.GetType(), "MyScript", "<script>alert('"+msg+"');</script>");
    }
}