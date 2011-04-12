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

            //所在地
            this.cbox_spot.DataBind();
            if (this.cbox_spot.Items.Count == 0)
            {
                JsUtil.AlertJs(this, "請先設定維修類別所在地!");
                this.Button1.Enabled = false;
            }

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

                    IQueryable<spot> spot_data = new Rep07DAO().Get_spotData(data.r01_peouid.Value);

                    foreach (spot d in spot_data)
                    {
                        this.cbox_spot.Items.FindByValue(d.spo_no.ToString()).Selected = true;
                    }
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
            Rep07DAO r07_dao = new Rep07DAO();
            int peo_uid = int.Parse(new SessionObject().sessionUserID);

            if (this.hidd_r01no.Value != "")
            {

                rep01 r01_data = dao.GetRep01(int.Parse(this.hidd_r05no.Value), int.Parse(this.hidd_r01no.Value));
                
                //該使用者
                int r01_peouid = r01_data.r01_peouid.Value;

                //刪除所在地
                IQueryable<rep07> r07_data = r07_dao.Get_rep07Data(r01_peouid);
                foreach (rep07 d in r07_data)
                {
                    r07_dao.DeleteRep07(d);
                }
                r07_dao.Update();

                //刪除原本
                dao.deleteRep01(r01_data);
                dao.Update();

                OperatesObject.OperatesExecute(300601, 4, string.Format("刪除維修類別管理者 peo_uid:{0}", r01_peouid));
                
                msg = "修改完成!";
            }
            else
            {
                msg = "新增完成!";
            }

            //加入管理者
            rep01 data = new rep01();
            data.r01_no = dao.GetMAXr01NO(int.Parse(this.DropDownList1.SelectedValue)) + 1;
            data.r05_no = int.Parse(this.DropDownList1.SelectedValue);
            data.r01_peouid = int.Parse(this.DepartTreeTextBox1.Value);
            data.r01_type = this.GetTypeStr();
            dao.addToRep01(data);
            dao.Update();

            //管理所在地
            for (int i = 0; i < this.cbox_spot.Items.Count; i++)
            {
                if (this.cbox_spot.Items[i].Selected == true)
                {
                    rep07 data2 = new rep07();
                    data2.r01_no = data.r01_no;
                    data2.r05_no = data.r05_no;
                    data2.r07_spono = int.Parse(this.cbox_spot.Items[i].Value);

                    r07_dao.AddRep07(data2);
                    r07_dao.Update();
                }
            }

            OperatesObject.OperatesExecute(300601, 1, string.Format("新增維修類別管理者 r05_no:{0} r01_no:{1} peo_uid:{2}", data.r05_no, data.r01_no, data.r01_peouid));

            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "closeThickBox", "self.parent.update('" + msg + "');", true);

        }


        
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

        if (this.cbox_spot.SelectedIndex == -1)
        {
            JsUtil.AlertJs(this, "請選擇所在地!");
            return false;
        }

            return true;
    }
}