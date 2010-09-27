using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;

public partial class _30_300300_300301_1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //編輯模式
        if (!this.IsPostBack)
        {
            string mode = Request["mode"];
            string e01_no = Request["e01_no"];

            e01DAO dao = new e01DAO();

            if (mode != null && mode.Equals("modify"))
            {
                this.navigator1.SubFunc = "修改";
                this.HiddenField1.Value = e01_no;

                //取資料
                e01 _e01 = dao.GetBye01NO(System.Convert.ToInt32(e01_no));
                this.tbox_name.Text = _e01.e01_name;
                this.tbox_order.Text = _e01.e01_order.ToString();
            }
            else
            {
                //新增模式
                this.navigator1.SubFunc = "新增";

            }
        }
    }
    protected void btn_ok_Click(object sender, EventArgs e)
    {
        if (this.CheckUI())
        {
            string msg = "";

            SessionObject sessionObj = new SessionObject();

            e01DAO dao = new e01DAO();

            if (this.HiddenField1.Value != "")
            {
                e01 _e01 = dao.GetBye01NO(Convert.ToInt32(this.HiddenField1.Value));

                _e01.e01_name = this.tbox_name.Text;
                _e01.e01_order = Convert.ToInt32(this.tbox_order.Text);
                _e01.e01_createtime = System.DateTime.Now;
                try
                {
                    _e01.e01_createuid = System.Convert.ToInt32(sessionObj.sessionUserID);
                }
                catch
                {
                }

                dao.Update();

                msg = "修改完成!";
            }
            else
            {

                e01 _e01 = new e01();

                _e01.e01_name = this.tbox_name.Text;
                _e01.e01_order = Convert.ToInt32(this.tbox_order.Text);
                _e01.e01_createtime = System.DateTime.Now;
                _e01.e01_status = "1";
                try
                {
                    _e01.e01_createuid = System.Convert.ToInt32(sessionObj.sessionUserID);
                }
                catch
                {
                }

                dao.Adde01(_e01);
                dao.Update();

                msg = "新增完成!";
            }

            this.Page.ClientScript.RegisterStartupScript(typeof(_30_300300_300301_1), "closeThickBox", "self.parent.update('" + msg + "');", true);
        }
    }

    private bool CheckUI()
    {
        bool check = true;

        if (string.IsNullOrEmpty(this.tbox_name.Text))
        {
            check = false;
            this.ShowMSG("請輸入上課地點!");
        }

        if (string.IsNullOrEmpty(this.tbox_order.Text))
        {
            check = false;
            this.ShowMSG("請輸入排列順序!");
        }
        else
        {
            try
            {
                Convert.ToInt32(this.tbox_order.Text);
            }
            catch
            {
                check = false;
                this.ShowMSG("排列順序請輸入數字!");
            }
        }

        return check;
    }

    private void ShowMSG(string msg)
    {
        this.ClientScript.RegisterStartupScript(this.GetType(), "MyMSG", "<script>alert('" + msg + "');</script>");
    }
}