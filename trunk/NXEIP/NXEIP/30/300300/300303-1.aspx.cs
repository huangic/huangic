using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;

public partial class _30_300300_300303_1 : System.Web.UI.Page
{
    private NXEIPEntities model = new NXEIPEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.Navigator1.SubFunc = "部門限制";
            this.jQueryDepartTree1.Clear();

            if (Request["e02_no"] != null)
            {
                this.hidd_no.Value = Request["e02_no"];

                e02DAO dao = new e02DAO();
                e02 data = dao.GetBye02NO(Convert.ToInt32(this.hidd_no.Value));

                this.lab_code.Text = data.e02_code;
                this.lab_mechani.Text = data.e02_mechani;
                this.lab_name_flag.Text = data.e02_name + "(第" + data.e02_flag.ToString() + "期)";
                var typ_name = (from t in model.types where t.typ_no == data.typ_no select t.typ_cname).FirstOrDefault();
                this.lab_typ_name.Text = typ_name;

                //復原原有部門限制
                int e02no = Convert.ToInt32(this.hidd_no.Value);
                IQueryable<e03> e03data = (from d in model.e03 where d.e02_no == e02no select d);
                int idnum = 1;
                foreach (var e03 in e03data)
                {
                    string dep_name = (from d in model.departments where d.dep_no == e03.e03_depno select d.dep_name).FirstOrDefault();
                    string people = e03.e03_people.ToString();
                    
                    TableCell cell1 = new TableCell();
                    cell1.Text = "<input id='tbox_" + idnum + "' type='hidden' value='" + e03.e03_depno + "' />" + dep_name;

                    TableCell cell2 = new TableCell();
                    cell2.Text = people;
                    cell2.HorizontalAlign = HorizontalAlign.Center;

                    TableCell cell3 = new TableCell();
                    cell3.Text = "<img class='delete' src='../../image/delete.gif' />";
                    cell3.HorizontalAlign = HorizontalAlign.Center;
                    
                    TableRow row = new TableRow();
                    row.Cells.Add(cell1);
                    row.Cells.Add(cell2);
                    row.Cells.Add(cell3);

                    this.Table3.Rows.Add(row);

                    idnum++;
                }

                OperatesObject.OperatesExecute(300303, new SessionObject().sessionUserID, 2, "查詢課程部門限制 e02_no:" + e02no);
            }
        }
    }
    
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        if (this.hidd_data.Value != "")
        {
            //刪除原有部門限制
            int e02no = Convert.ToInt32(this.hidd_no.Value);
            int[] e03data = (from d in model.e03 where d.e02_no == e02no select d.e03_no).ToArray();
            foreach (int e03no in e03data)
            {
                e03 del = (from ed in model.e03 where ed.e03_no == e03no && ed.e02_no == e02no select ed).FirstOrDefault();
                if (del != null)
                {
                    model.e03.DeleteObject(del);
                }
            }
            model.SaveChanges();

            //新增部門限制
            string[] tmp = this.hidd_data.Value.Split(',');
            int max_e03no = 1;
            
            for (int i = 0; i < tmp.Length; i++)
            {
                e03 data = new e03();
                data.e03_depno = Convert.ToInt32(tmp[i].Split('_')[0]);
                data.e03_people = Convert.ToInt32(tmp[i].Split('_')[1]);
                data.e02_no = e02no;
                data.e03_no = max_e03no + i;
                model.AddToe03(data);
            }

            model.SaveChanges();
            OperatesObject.OperatesExecute(300303, new SessionObject().sessionUserID, 3, "更新部門限制 e02_no:" + e02no);
            this.ShowMsg_URL("部門限制完成!", this.GetUrl());
        }
    }

    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(this.GetUrl());
    }

    private string GetUrl()
    {
        string url = "300303.aspx";
        url += "?sdate=" + Request["sdate"];
        url += "&edate=" + Request["edate"];
        url += "&type_1=" + Request["type_1"];
        url += "&type_2=" + Request["type_2"];
        url += "&e01_no=" + Request["e01_no"];
        url += "&e02_name=" + Request["e02_name"];
        url += "&e02_no=" + Request["e02_no"];
        url += "&model=" + Request["model"];
        url += "&pageIndex=" + Request["pageIndex"];
        return url;

    }

    private void ShowMsg_URL(string msg, string url)
    {
        string script = "<script>window.alert('" + msg + "');location.replace('" + url + "')</script>";
        this.ClientScript.RegisterStartupScript(this.GetType(), "MyScript", script);
    }
}