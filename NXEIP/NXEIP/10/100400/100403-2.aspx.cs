using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;
using NLog;

public partial class _10_100400_100403_2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if (Request["r02_no"] != null)
            {
                this.hidd_r02no.Value = Request["r02_no"];

                UtilityDAO udao = new UtilityDAO();
                _100403DAO dao = new _100403DAO();

                rep02 data = dao.GetRep02ByNo(int.Parse(this.hidd_r02no.Value));

                this.lab_dep.Text = udao.Get_DepartmentName(data.r02_depno.Value);
                this.lab_people.Text = udao.Get_PeopleName(data.peo_uid);
                this.lab_date.Text = new ChangeObject()._ADtoROC(data.r02_date.Value) + " " + data.r02_date.Value.ToString("HH:mm");
                this.lab_place.Text = dao.GetSpotName(data.r02_spono.Value) + " " + data.r02_floor;
                this.lab_reason.Text = data.r02_reason;
                this.lab_status.Text = this.changStr(data.r02_status);

                if (data.r02_repairuid.HasValue)
                {
                    this.lab_reply_people.Text = udao.Get_PeopleName(data.r02_repairuid.Value);
                    this.lab_reply_date.Text = new ChangeObject()._ADtoROC(data.r02_rdate.Value) + " " + data.r02_rdate.Value.ToString("HH:mm");
                    this.lab_reply.Text = data.r02_reply;
                }

                if (data.r02_status != "3")
                {
                    this.Button1.Enabled = false;
                }

            }

        }
    }

    private string changStr(string status)
    {
        string val = "";

        if (status == "1")
        {
            val = "未處理";
        }
        if (status == "2")
        {
            val = "進行中";
        }
        if (status == "3")
        {
            val = "已完成";
        }

        return val;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (this.hidd_r02no.Value != "")
        {
            int r02_no = int.Parse(this.hidd_r02no.Value);

            _100403DAO dao = new _100403DAO();
            rep03 d;
            if (dao.CheckRep03(r02_no) > 0)
            {
                d = dao.GetRep03ByNo(r02_no);
                d.r03_date = DateTime.Now;
                d.r03_peouid = int.Parse(new SessionObject().sessionUserID);
                d.r03_item = this.rbl_rep03.SelectedValue;

                OperatesObject.OperatesExecute(100403, 3, "評分維修紀錄 r02_no:" + r02_no);
            }
            else
            {
                d = new rep03();
                d.r03_no = dao.MaxRep03No(r02_no) + 1;
                d.r02_no = r02_no;
                d.r03_date = DateTime.Now;
                d.r03_peouid = int.Parse(new SessionObject().sessionUserID);
                d.r03_item = this.rbl_rep03.SelectedValue;
                dao.addToRep03(d);

                OperatesObject.OperatesExecute(100403, 1, "評分維修紀錄 r02_no:" + r02_no);
            }
            dao.UpData();

            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "closeThickBox", "self.parent.update('評分完成!');", true);

        }
    }
}