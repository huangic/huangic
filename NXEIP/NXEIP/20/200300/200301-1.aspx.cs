using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;
using NLog;

public partial class _20_200300_200301_1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (this.ddl_s06.SelectedValue.Equals("0"))
        {
            JsUtil.AlertJs(this, "請選擇所屬分類!");
            return;
        }
        if (this.tbox_desc.Text.Trim().Length == 0)
        {
            JsUtil.AlertJs(this, "請填入商店介紹!");
            return;
        }

        if (this.tbox_name.Text.Trim().Length == 0)
        {
            JsUtil.AlertJs(this, "請填入商店名稱!");
            return;
        }
        if (this.tbox_area.Text.Trim().Length == 0)
        {
            JsUtil.AlertJs(this, "請填入商店所在地!");
            return;
        }

        foods d = new foods();

        d.foo_area = this.tbox_area.Text.Trim();
        d.foo_createtime = DateTime.Now;
        d.foo_createuid = int.Parse(new SessionObject().sessionUserID);
        d.foo_descript = this.tbox_desc.Text.Trim();
        d.foo_name = this.tbox_name.Text;
        d.foo_s06no = int.Parse(this.ddl_s06.SelectedValue);
        d.foo_status = "1";
        d.foo_tel = this.tbox_tel.Text.Trim();
        d.foo_www = this.tbox_www.Text.Trim();

        FoodsDAO dao = new FoodsDAO();
        dao.AddToFoods(d);
        dao.Update();

        OperatesObject.OperatesExecute(200301,1,string.Format("新增美食區 foo_no=",d.foo_no));

        JsUtil.AlertAndRedirectJs(this, "新增資料完成", "200301.aspx");

    }

    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("200301.aspx");
    }
}