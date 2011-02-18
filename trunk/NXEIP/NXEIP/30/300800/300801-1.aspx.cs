using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;

public partial class _30_300800_300801_1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.hidd_no.Value = Request.QueryString["n01_no"];
            int n01_no = int.Parse(this.hidd_no.Value);

            using (NXEIPEntities model = new NXEIPEntities())
            {
                new01 data = (from d in model.new01 where d.n01_no == n01_no select d).First();

                this.lab_people.Text = new UtilityDAO().Get_PeopleName(data.n01_peouid.Value);
                this.lab_date.Text = new ChangeObject()._ADtoROC(data.n01_date.Value);
                this.lab_subject.Text = data.n01_subject;
                this.lab_content.Text = data.n01_content;
                this.tbox_reason.Text = data.n01_reason;

                if (data.n01_status != "3")
                {
                    this.rbl_status.SelectedItem.Selected = false;
                    this.rbl_status.Items.FindByValue(data.n01_status).Selected = true;
                }
            }

        }
    }

    protected void btn_ok_Click(object sender, EventArgs e)
    {
        if (this.rbl_status.SelectedValue.Equals("2") && this.tbox_reason.Text.Trim().Length == 0)
        {
            JsUtil.AlertJs(this, "如未通過,請輸入事由!");
        }
        else
        {
            using (NXEIPEntities model = new NXEIPEntities())
            {
                int n01_no = int.Parse(this.hidd_no.Value);
                int peo_uid = int.Parse(new SessionObject().sessionUserID);
                new01 d = (from p in model.new01
                           where p.n01_no == n01_no
                           select p).FirstOrDefault();

                if (d != null)
                {
                    d.n01_status = this.rbl_status.SelectedValue;
                    d.n01_reason = this.tbox_reason.Text;
                    d.n01_checkuid = peo_uid;
                    d.n01_checkdate = DateTime.Now;

                    model.SaveChanges();

                    OperatesObject.OperatesExecute(300801, 3, string.Format("最新消息審核回覆 n01_no={0} peo_uid={1}", n01_no, peo_uid));
                }
            }

            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "closeThickBox", "self.parent.update('審核完成!');", true);
        }
    }
}