using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;

public partial class _20_200700_200701_2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.DepartTreeListBox1.Clear();

            if (Request.QueryString["qat_no"] != null)
            {
                this.hidden_no.Value = Request.QueryString["qat_no"];
                this.ObjectDataSource1.SelectParameters["qat_no"].DefaultValue = this.hidden_no.Value;
                this.GridView1.DataBind();
            }
        }
    }

    protected void btn_ok_Click(object sender, EventArgs e)
    {
        if (this.DepartTreeListBox1.Items.Count == 0)
        {
            JsUtil.AlertJs(this, "請選擇人員!!");
        }
        else
        {
            int qat_no = int.Parse(this.hidden_no.Value);

            _200701DAO dao = new _200701DAO();

            foreach (var item in this.DepartTreeListBox1.ItemsValue)
            {
                int peo_uid = int.Parse(item);

                qamanager s = dao.Get_QAManagerData(qat_no, peo_uid);

                if (s == null)
                {
                    int max = dao.Get_QAManagerMax(qat_no) + 1;

                    qamanager d = new qamanager();
                    d.qam_no = max;
                    d.qat_no = qat_no;
                    d.qam_peouid = peo_uid;

                    dao.AddToQAManager(d);
                    dao.Update();

                    OperatesObject.OperatesExecute(200701, 1, string.Format("新增問答類別管理員 qat_no:{0} peo_uid:{1}", qat_no, peo_uid));
                }
            }

            this.GridView1.DataBind();
            this.DepartTreeListBox1.Clear();
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = int.Parse(e.CommandArgument.ToString());
        int peo_uid = int.Parse(this.GridView1.DataKeys[rowIndex].Value.ToString());
        int qat_no = int.Parse(this.hidden_no.Value);
        
        if (e.CommandName.Equals("del"))
        {
            _200701DAO dao = new _200701DAO();

            qamanager d = dao.Get_QAManagerData(qat_no,peo_uid);
            dao.DelToQAManager(d);
            dao.Update();

            OperatesObject.OperatesExecute(200701, 4, string.Format("刪除問答類別管理人員 qat_no:{0} peo_uid:{1}", qat_no, peo_uid));

            this.GridView1.DataBind();
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int peo_uid = int.Parse(e.Row.Cells[0].Text);

            e.Row.Cells[0].Text = new UtilityDAO().Get_PeopleName(peo_uid);
        }
    }
}