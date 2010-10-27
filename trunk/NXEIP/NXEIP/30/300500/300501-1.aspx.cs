using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;

public partial class _30_300500_300501_1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //編輯模式
        if (!this.IsPostBack)
        {
            string mode = Request["mode"];
            string s06_no = Request["s06_no"];
            
            this.ddl_sysfun.DataBind();

            this.ddl_parent.DataBind();
            this.ddl_parent.Items.Insert(0, new ListItem("無", "0"));

            if (mode != null && mode.Equals("modify"))
            {
                this.Navigator1.SubFunc = "修改類別";
                this.HiddenField1.Value = s06_no;

                //取資料
                sys06 data = new Sys06DAO().GetByS06No(int.Parse(s06_no));

                this.ddl_sysfun.Items.FindByValue(data.sfu_no.ToString()).Selected = true;
                if (data.s06_parent.Value > 0)
                {
                    this.ddl_parent.Items.FindByValue(data.s06_parent.ToString()).Selected = true;
                }
                this.tbox_name.Text = data.s06_name;
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
        if (this.tbox_name.Text.Trim().Length == 0)
        {
            this.ShowMSG("請輸入類別名稱!");
        }
        else
        {
            int opt_type = 1;
            string msg = "", opt_name = "";
            Sys06DAO dao = new Sys06DAO();
            sys06 data = null;

            if (this.HiddenField1.Value != "")
            {
                data = dao.GetByS06No(int.Parse(this.HiddenField1.Value));
                msg = "修改完成!";
                opt_name = "修改類別 s06_no:" + this.HiddenField1.Value;
                opt_type = 3;
            }
            else
            {
                data = new sys06();
                msg = "新增完成!";
                opt_name = "新增類別";
            }

            data.s06_createtime = DateTime.Now;
            data.s06_createuid = int.Parse(new SessionObject().sessionUserID);
            data.s06_name = this.tbox_name.Text;
            data.s06_status = "1";
            data.sfu_no = int.Parse(this.ddl_sysfun.SelectedValue);
            if (this.ddl_parent.SelectedValue.Equals("0"))
            {
                data.s06_parent = 0;
                data.s06_level = 1;
            }
            else
            {
                data.s06_parent = int.Parse(this.ddl_parent.SelectedValue);
                data.s06_level = 2;
            }

            if (this.HiddenField1.Value == "")
            {
                dao.AddSys(data);
            }

            dao.Update();

            OperatesObject.OperatesExecute(300501, new SessionObject().sessionUserID, opt_type, opt_name);

            this.Page.ClientScript.RegisterStartupScript(typeof(_30_300500_300501_1), "closeThickBox", "self.parent.update('" + msg + "');", true);

        }

    }

    private void ShowMSG(string msg)
    {
        this.ClientScript.RegisterStartupScript(this.GetType(), "MyMSG", "<script>alert('" + msg + "');</script>");
    }
}