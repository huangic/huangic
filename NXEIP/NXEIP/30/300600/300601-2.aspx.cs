using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;
using NLog;

public partial class _30_300600_300601_2 : System.Web.UI.Page
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
                this.lab_people.Text = udao.Get_PeopleName(data.peo_uid) + " 分機：" + udao.Get_PeopleExtension(data.peo_uid);
                this.lab_reason.Text = data.r02_reason;
                this.lab_spot.Text = dao.GetSpotName(data.r02_spono.Value) + " " + data.r02_floor;

                this.ObjectDataSource2.SelectParameters["r05_no"].DefaultValue = data.r05_no.ToString();
                this.ddl_rep06_par.DataBind();

                //回覆類別
                if (data.r06_no.HasValue)
                {
                    rep06 r06_data = new Rep06DAO().GetRep06(data.r06_no.Value);

                    this.ddl_rep06_par.Items.FindByValue(r06_data.r06_parent.Value.ToString()).Selected = true;

                    this.ObjectDataSource3.SelectParameters["r06_no"].DefaultValue = r06_data.r06_parent.Value.ToString();
                    this.ddl_rep06_son.DataBind();
                    this.ddl_rep06_son.Items.FindByValue(data.r06_no.Value.ToString()).Selected = true;
                }

                this.lab_replyname.Text = udao.Get_PeopleName(int.Parse(new SessionObject().sessionUserID));

                this.tbox_reply.Text = data.r02_reply;
                
            }
            else
            {
                this.Button1.Enabled = false;
            }
        }
    }
    
    protected void ddl_rep06_par_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.ddl_rep06_son.Items.Clear();

        if (!this.ddl_rep06_par.SelectedValue.Equals("0"))
        {
            this.ObjectDataSource3.SelectParameters["r06_no"].DefaultValue = this.ddl_rep06_par.SelectedValue;

            this.ddl_rep06_son.DataBind();

        }

        this.ddl_rep06_son.Items.Insert(0, new ListItem("請選擇", "0"));
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (CheckInput())
        {
            _100403DAO dao = new _100403DAO();
            rep02 data = dao.GetRep02ByNo(int.Parse(this.hidd_r02no.Value));

            data.r02_repairuid = int.Parse(new SessionObject().sessionUserID);
            data.r02_rdate = DateTime.Now;
            data.r02_status = this.ddl_status.SelectedValue;
            data.r02_reply = this.tbox_reply.Text;
            data.r06_no = int.Parse(this.ddl_rep06_son.SelectedValue);

            dao.UpData();

            OperatesObject.OperatesExecute(300601, 3, "回覆叫修紀錄 r02_no:" + data.r02_no);

            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "closeThickBox", "self.parent.update('回覆完成!');", true);
        }


    }

    private bool CheckInput()
    {
        if (this.tbox_reply.Text.Trim().Length == 0)
        {
            this.ShowMsg("請輸入維修回覆!!");
            return false;
        }

        if (this.ddl_rep06_son.SelectedValue == "0")
        {
            this.ShowMsg("請選擇問題分類!!");
            return false;
        }

        return true;

    }

    private void ShowMsg(string msg)
    {
        JsUtil.AlertJs(this, msg);
    }
}