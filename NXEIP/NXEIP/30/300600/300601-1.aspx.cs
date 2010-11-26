using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;
using NLog;

public partial class _30_300600_300601_1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if (Request["r02_no"] != null)
            {
                this.hidd_r02no.Value = Request["r02_no"];
                _100403DAO dao = new _100403DAO();
                UtilityDAO udao = new UtilityDAO();

                rep02 data = dao.GetRep02ByNo(int.Parse(this.hidd_r02no.Value));

                this.lab_dep.Text = udao.Get_DepartmentName(data.r02_depno.Value);

                this.lab_people.Text = udao.Get_PeopleName(data.peo_uid);

                this.ddl_spot.DataBind();
                this.ddl_spot.Items.FindByValue(data.r02_spono.Value.ToString()).Selected = true;

                this.ddl_floor.Items.FindByValue(data.r02_floor).Selected = true;

                this.ddl_rep05.DataBind();
                this.ddl_rep05.Items.FindByValue(data.r05_no.ToString()).Selected = true;

                this.tbox_reason.Text = data.r02_reason;
            }
            else
            {
                this.Button1.Enabled = false;
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (CheckInput() && this.hidd_r02no.Value != "")
        {
            _100403DAO dao = new _100403DAO();
            rep02 data = dao.GetRep02ByNo(int.Parse(this.hidd_r02no.Value));
            data.r05_no = int.Parse(this.ddl_rep05.SelectedValue);
            data.r02_spono = int.Parse(this.ddl_spot.SelectedValue);
            data.r02_floor = this.ddl_floor.SelectedValue;
            data.r02_reason = this.tbox_reason.Text;
            data.r02_createtime = DateTime.Now;
            data.r02_createuid = int.Parse(new SessionObject().sessionUserID);
            dao.UpData();

            OperatesObject.OperatesExecute(300601, 3, "修改叫修紀錄 r02_no:" + data.r02_no);

            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "closeThickBox", "self.parent.update('修改完成!');", true);
        }
    }

    private bool CheckInput()
    {
        if (this.ddl_spot.SelectedValue.Equals("0"))
        {
            this.ShowMsg("請選擇地點");
            return false;
        }
        if (this.ddl_floor.SelectedValue.Equals("0"))
        {
            this.ShowMsg("請選擇樓層");
            return false;
        }

        if (this.ddl_rep05.SelectedValue.Equals("0"))
        {
            this.ShowMsg("請選擇維修類別");
            return false;
        }

        if (this.tbox_reason.Text.Trim().Length == 0)
        {
            this.ShowMsg("請輸入故障原因");
            return false;
        }

        return true;
    }

    private void ShowMsg(string msg)
    {
        string script = "<script>window.alert('" + msg + "');</script>";
        this.ClientScript.RegisterStartupScript(this.GetType(), "MSG", script);
    }
}