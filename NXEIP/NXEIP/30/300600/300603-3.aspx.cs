using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;
using NLog;

public partial class _30_300600_300603_3 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.hidd_r05no.Value = Request.QueryString["r05_no"];
            this.ObjectDataSource1.SelectParameters["r05_no"].DefaultValue = this.hidd_r05no.Value;

            this.Navigator1.SubFunc = new Rep05DAO().GetRep05Name(int.Parse(this.hidd_r05no.Value));

            if (Request.QueryString["mode"] == "modify")
            {
                this.Navigator1.SubFunc += " - 編輯類別";

                this.hidd_r06no.Value = Request.QueryString["r06_no"];

                rep06 data = new Rep06DAO().GetRep06(int.Parse(this.hidd_r06no.Value));

                if (data.r06_parent.Value > 0)
                {
                    this.DropDownList1.DataBind();
                    this.DropDownList1.Items[this.DropDownList1.SelectedIndex].Selected = false;
                    this.DropDownList1.Items.FindByValue(data.r06_parent.ToString()).Selected = true;
                }

                this.tbox_name.Text = data.r06_name;
                this.tbox_order.Text = data.r06_order.Value.ToString();

            }
            else
            {
                this.Navigator1.SubFunc += " - 新增類別";
            }

        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (CheckInput())
        {
            Rep06DAO dao = new Rep06DAO();
            string msg = "";

            if (this.hidd_r06no.Value != "")
            {
                rep06 data = dao.GetRep06(int.Parse(this.hidd_r06no.Value));

                data.r06_name = this.tbox_name.Text.Trim();
                data.r06_order = int.Parse(this.tbox_order.Text);
                data.r06_parent = int.Parse(this.DropDownList1.SelectedValue);
                if (this.DropDownList1.SelectedValue == "0")
                {
                    data.r06_level = 1;
                }
                else
                {
                    data.r06_level = 2;
                }
                data.r06_createtime = DateTime.Now;
                data.r06_createuid = int.Parse(new SessionObject().sessionUserID);

                dao.Update();

                msg = "修改完成!";

                OperatesObject.OperatesExecute(300603, 3, string.Format("修改維修類別 r06_no:{0} r05_no:{1}", data.r06_no, data.r05_no));

            }
            else
            {
                rep06 data = new rep06();

                data.r05_no = int.Parse(this.hidd_r05no.Value);
                data.r06_name = this.tbox_name.Text.Trim();
                data.r06_order = int.Parse(this.tbox_order.Text);
                data.r06_status = "1";
                data.r06_parent = int.Parse(this.DropDownList1.SelectedValue);
                if (this.DropDownList1.SelectedValue == "0")
                {
                    data.r06_level = 1;
                }
                else
                {
                    data.r06_level = 2;
                }
                data.r06_createtime = DateTime.Now;
                data.r06_createuid = int.Parse(new SessionObject().sessionUserID);

                dao.AddToRep06(data);
                dao.Update();

                msg = "新增完成!";

                OperatesObject.OperatesExecute(300603, 1, string.Format("修改維修類別 r06_no:{0} r05_no:{1}", data.r06_no, data.r05_no));
            }

            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "closeThickBox", "self.parent.update('" + msg + "');", true);
        }
    }

    private bool CheckInput()
    {
        if (this.tbox_name.Text.Trim().Length == 0)
        {
            JsUtil.AlertJs(this, "請輸入類別名稱!!");
            return false;
        }

        try
        {
            int a = int.Parse(this.tbox_order.Text);
        }
        catch
        {
            JsUtil.AlertJs(this, "類別排序請輸入數值!");
            return false;
        }

        return true;
    }
}