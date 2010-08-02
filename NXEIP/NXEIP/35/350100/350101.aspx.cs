using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;

public partial class _35_350100_350101 : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = System.Convert.ToInt32(e.CommandArgument);
        int rol_no = System.Convert.ToInt32(this.GridView1.DataKeys[rowIndex].Value.ToString());

        //刪除
        if (e.CommandName.Equals("disable"))
        {
            new DBObject().ExecuteNonQuery("delete from role where rol_no = " + rol_no);

            this.GridView1.DataBind();
        }

        //設定權限
        if (e.CommandName.Equals("set"))
        {
            Response.Redirect("350101-3.aspx?rol_no=" + rol_no, true);
        }

        //設定角色為預設值
        if (e.CommandName.Equals("default"))
        {
            //加為預設值
            new DBObject().ExecuteNonQuery("update role set rol_default='1' where rol_no = " + rol_no);

            //消除原有預設值
            for (int i = 0; i < this.GridView1.Rows.Count; i++)
            {
                if (this.GridView1.Rows[i].Cells[4].Text.Equals("預設角色"))
                {
                    new DBObject().ExecuteNonQuery("update role set rol_default = null where rol_no = " + this.GridView1.DataKeys[i].Value.ToString());
                }
            }

            this.GridView1.DataBind();
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // 置換UID 為PEOPLE_NAME
            PeopleDAO dao = new PeopleDAO();
            int uid = System.Convert.ToInt32(e.Row.Cells[2].Text);
            e.Row.Cells[2].Text = dao.GetPeopleNameByUid(uid);

            //轉換民國年
            e.Row.Cells[3].Text = new ChangeObject().ADDTtoROCDT(e.Row.Cells[3].Text);

            if (e.Row.Cells[4].Text.Equals("1"))
            {
                e.Row.Cells[4].Text = "預設角色";
            }
            else
            {
                e.Row.Cells[4].Text = "--";
            }

        }



    }
    
}
