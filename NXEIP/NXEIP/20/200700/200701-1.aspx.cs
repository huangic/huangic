using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;

public partial class _20_200700_200701_1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if (Request.QueryString["qat_no"] != null)
            {
                this.hidden_no.Value = Request.QueryString["qat_no"];

                if (Request.QueryString["mode"] == "modify")
                {
                    this.Navigator1.SubFunc = "修改";

                    qatype data = new _200701DAO().Get_qatype(int.Parse(this.hidden_no.Value));

                    this.tbox_note.Text = data.qat_note;

                    if (data.qat_self == "1")
                    {
                        this.tbox_name.Text = data.qat_name;
                    }
                    else
                    {
                        this.rb_self.Checked = false;

                        if (data.qat_r05no.HasValue)
                        {
                            this.rb_r05no.Checked = true;
                            this.ddl_r05.DataBind();
                            this.ddl_r05.Items.FindByValue(data.qat_r05no.Value.ToString()).Selected = true;
                        }

                        if (data.qat_s06no.HasValue)
                        {
                            this.rb_s06no.Checked = true;
                            this.ddl_sysfun.DataBind();
                            this.ddl_sysfun.Items.FindByValue(data.qat_s06no.Value.ToString()).Selected = true;
                        }
                    }
                }
                else
                {
                    this.Navigator1.SubFunc = "新增";
                }
            }
        }
    }

    protected void btn_ok_Click(object sender, EventArgs e)
    {
        if (this.rb_self.Checked && this.tbox_name.Text.Trim().Length == 0)
        {
            JsUtil.AlertJs(this, "請輸入類別名稱!");
            return;
        }

        string msg = "";
        if (this.hidden_no.Value != "")
        {
            this.Modify();
            msg = "修改完成!";
        }
        else
        {
            if (this.Insert())
            {
                msg = "新增完成!";
            }
            else
            {
                return;
            }
        }

        //呼叫UPATE()關閉此頁面 並且更新updatepanel (parent page 必須做一個UPDATE的FUNCTION)
        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "closeThickBox", "self.parent.update('" + msg + "');", true);
    }

    private void Modify()
    {
        _200701DAO dao = new _200701DAO();

        qatype d = dao.Get_qatype(int.Parse(this.hidden_no.Value));

        d.qat_createuid = int.Parse(new SessionObject().sessionUserID);
        d.qat_createtime = DateTime.Now;
        d.qat_note = this.tbox_note.Text.Trim();
        if (rb_self.Checked)
        {
            d.qat_self = "1";
            d.qat_name = this.tbox_name.Text.Trim();
            d.qat_r05no = null;
            d.qat_s06no = null;
        }
        if (rb_r05no.Checked)
        {
            d.qat_name = "";
            d.qat_self = "3";
            d.qat_s06no = null;
            d.qat_r05no = int.Parse(this.ddl_r05.SelectedValue);
        }
        if (rb_s06no.Checked)
        {
            d.qat_name = "";
            d.qat_self = "2";
            d.qat_r05no = null;
            d.qat_s06no = int.Parse(this.ddl_sysfun.SelectedValue);
        }

        dao.Update();

        OperatesObject.OperatesExecute(200701, 3, "修改問答類別 qat_no:" + d.qat_no);
    }

    private bool Insert()
    {
        bool ret;
        _200701DAO dao = new _200701DAO();
        qatype d = null;

        //查詢是否有相同
        if (rb_self.Checked)
        {
            d = dao.Search_qatype(this.tbox_name.Text.Trim(), null, null);
        }
        if (rb_r05no.Checked)
        {
            d = dao.Search_qatype("", null, int.Parse(this.ddl_r05.SelectedValue));
        }
        if (rb_s06no.Checked)
        {
            d = dao.Search_qatype("", int.Parse(this.ddl_sysfun.SelectedValue),null);
        }

        if (d == null)
        {
            d = new qatype();

            d.qat_status = "1";
            d.qat_createuid = int.Parse(new SessionObject().sessionUserID);
            d.qat_createtime = DateTime.Now;
            d.qat_note = this.tbox_note.Text.Trim();

            if (rb_self.Checked)
            {
                d.qat_self = "1";
                d.qat_name = this.tbox_name.Text.Trim();
                d.qat_r05no = null;
                d.qat_s06no = null;
            }
            if (rb_r05no.Checked)
            {
                d.qat_name = "";
                d.qat_self = "3";
                d.qat_s06no = null;
                d.qat_r05no = int.Parse(this.ddl_r05.SelectedValue);
            }
            if (rb_s06no.Checked)
            {
                d.qat_name = "";
                d.qat_self = "2";
                d.qat_r05no = null;
                d.qat_s06no = int.Parse(this.ddl_sysfun.SelectedValue);
            }

            dao.AddToQAtype(d);
            dao.Update();

            OperatesObject.OperatesExecute(200701, 1, "新增問答類別 qat_no:" + d.qat_no);

            ret = true;
        }
        else
        {
            JsUtil.AlertJs(this, "已有相同類別");
            ret = false;
        }

        return ret;

    }

}