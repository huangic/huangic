using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;
using NLog;

public partial class _30_300600_300602_1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.DepartTreeTextBox1.Clear();

            if (Request.QueryString["mode"] != null)
            {
                if (Request.QueryString["mode"] == "modify")
                {
                    this.Navigator1.SubFunc = "編輯管理者";
                    this.hidd_r05no.Value = Request.QueryString["r05_no"];
                    this.hidd_r01no.Value = Request.QueryString["r01_no"];

                    //this.DropDownList1.Enabled = false;

                    rep01 data = new Rep01DAO().GetRep01(int.Parse(this.hidd_r05no.Value), int.Parse(this.hidd_r01no.Value));

                    this.DropDownList1.DataBind();
                    this.DropDownList1.Items[this.DropDownList1.SelectedIndex].Selected = false;
                    this.DropDownList1.Items.FindByValue(data.r05_no.ToString()).Selected = true;

                    this.CheckBoxList1.Items[0].Selected = data.r01_type.Substring(0, 1) == "1" ? true : false;
                    this.CheckBoxList1.Items[1].Selected = data.r01_type.Substring(1, 1) == "1" ? true : false;
                    this.CheckBoxList1.Items[2].Selected = data.r01_type.Substring(2, 1) == "1" ? true : false;

                    this.DepartTreeTextBox1.Add(data.r01_peouid.Value);
                }
                else
                {
                    this.Navigator1.SubFunc = "新增管理者";

                }

            }
        }

    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        if (this.CheckInput())
        {
            string msg = "";

            Rep01DAO dao = new Rep01DAO();

            if (this.hidd_r01no.Value != "")
            {

                rep01 data = dao.GetRep01(int.Parse(this.hidd_r05no.Value), int.Parse(this.hidd_r01no.Value));
                //刪除原本
                dao.deleteRep01(data);
                dao.Update();

                //新增
                rep01 data2 = new rep01();
                data2.r01_no = dao.GetMAXr01NO(int.Parse(this.DropDownList1.SelectedValue)) + 1;
                data2.r05_no = int.Parse(this.DropDownList1.SelectedValue);
                data2.r01_peouid = int.Parse(this.DepartTreeTextBox1.Value);
                data2.r01_type = this.GetTypeStr();
                dao.addToRep01(data2);
                dao.Update();

                msg = "修改完成!";

                OperatesObject.OperatesExecute(300601, 3, string.Format("修改管理者 r05_no:{0} r01_no:{1}",data2.r05_no,data2.r01_no));
            }
            else
            {
                rep01 data = new rep01();
                data.r01_no = dao.GetMAXr01NO(int.Parse(this.DropDownList1.SelectedValue)) + 1;
                data.r05_no = int.Parse(this.DropDownList1.SelectedValue);
                data.r01_peouid = int.Parse(this.DepartTreeTextBox1.Value);
                data.r01_type = this.GetTypeStr();
                dao.addToRep01(data);
                dao.Update();

                msg = "新增完成!";

                OperatesObject.OperatesExecute(300601, 1, string.Format("新增管理者 r05_no:{0} r01_no:{1}", data.r05_no, data.r01_no));
            }

            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "closeThickBox", "self.parent.update('" + msg + "');", true);

        }


        
    }

    private void InsertData()
    {
        Rep01DAO dao = new Rep01DAO();
        rep01 data = new rep01();
        data.r01_no = dao.GetMAXr01NO(int.Parse(this.DropDownList1.SelectedValue)) + 1;
        data.r05_no = int.Parse(this.DropDownList1.SelectedValue);
        data.r01_peouid = int.Parse(this.DepartTreeTextBox1.Value);
        data.r01_type = this.GetTypeStr();
        dao.addToRep01(data);
        dao.Update();
    }

    private string GetTypeStr()
    {
        string str = "";
        for (int i = 0; i < this.CheckBoxList1.Items.Count; i++)
        {
            if (this.CheckBoxList1.Items[i].Selected)
            {
                str += "1";
            }
            else
            {
                str += "0";
            }
        }
        return str;
    }

    private bool CheckInput()
    {
        if (this.DropDownList1.SelectedValue == "0")
        {
            JsUtil.AlertJs(this, "請選擇類別!");
            return false;
        }

        if (this.DepartTreeTextBox1.Items.Count == 0)
        {
            JsUtil.AlertJs(this, "請選擇管理者!");
            return false;
        }

        return true;
    }
}