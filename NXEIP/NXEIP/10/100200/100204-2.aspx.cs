using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;
using NLog;

public partial class _10_100200_100204_2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            int n01_no = int.Parse(Request.QueryString["n01_no"]);

            using (NXEIPEntities model = new NXEIPEntities())
            {
                UtilityDAO dao = new UtilityDAO();
                new01 data = (from d in model.new01 where d.n01_no == n01_no select d).FirstOrDefault();
                this.lab_dep.Text = dao.Get_DepartmentName(data.n01_depno.Value);
                this.lab_people.Text = dao.Get_PeopleName(data.n01_peouid.Value);
                this.div_date.InnerHtml = new ChangeObject()._ADtoROC(data.n01_date.Value);
                this.div_subject.InnerHtml = data.n01_subject;
                this.div_content.InnerHtml = data.n01_content;
                if (data.n01_use.Equals("2"))
                {
                    this.div_use.InnerHtml = "全府";
                }
                else
                {
                    this.div_use.InnerHtml = "單位";
                }
                if (data.s06_no.HasValue)
                {
                    this.div_sfu_name.InnerHtml = (from d in model.sys06 
                                                   where d.s06_no == data.s06_no.Value select d.s06_name).FirstOrDefault();
                }
                else
                {
                    this.div_sfu_name.InnerHtml = "最新消息";
                }

            }


            //連結
            this.ObjectDataSource3.SelectParameters["n01_no"].DefaultValue = n01_no.ToString();

            //檔案
            this.ObjectDataSource2.SelectParameters["n01_no"].DefaultValue = n01_no.ToString();
            
        }
    }
}