using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity;
using System.Data;

public partial class _35_350100_350101_3 : System.Web.UI.Page
{
    private NXEIPEntities model = new NXEIPEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            int rol_no = Convert.ToInt32(Request["rol_no"]);

            //取角色資料
            
            role roletabe = (from roleData in model.role where roleData.rol_no == rol_no select roleData).FirstOrDefault();
            this.Label1.Text = roletabe.rol_name;
            this.Label2.Text = roletabe.rol_memo;

            this.GridView1.DataBind();

            #region 合併相同之大功能項
            int k, n;
            for (int l = 0; l < this.GridView1.Rows.Count; l++)
            {
                n = 1;
                for (k = l + 1; k < this.GridView1.Rows.Count; k++)
                {
                    //比對sys_no是否一致?
                    if (this.GridView1.DataKeys[l].Values[0].ToString().Equals(this.GridView1.DataKeys[k].Values[0].ToString()))
                    {
                        n += 1;
                        this.GridView1.Rows[l].Cells[0].RowSpan = n;
                        this.GridView1.Rows[k].Cells[0].Visible = false;
                    }
                    else
                    {
                        break;
                    }
                }

                l = k - 1;
            }
            #endregion

        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex != -1)
            {
                string rol_no = Request["rol_no"];

                string sfu_no = this.GridView1.DataKeys[e.Row.RowIndex].Values[1].ToString();

                string sql = "SELECT count(sysfuction.sfu_no) as total FROM rauthority INNER JOIN sysfuction ON rauthority.sfu_no = sysfuction.sfu_no WHERE (rauthority.rol_no = " + rol_no + ") AND (sysfuction.sfu_status = '1') AND (sysfuction.sfu_no = " + sfu_no + ")";

                int count = Convert.ToInt32(new DBObject().ExecuteScalar(sql));
                if (count > 0)
                {
                    ((System.Web.UI.WebControls.CheckBox)e.Row.FindControl("cbox1")).Checked = true;
                }
                else
                {
                    ((System.Web.UI.WebControls.CheckBox)e.Row.FindControl("cbox1")).Checked = false;
                }

                //子系統
                this.SqlDataSource2.SelectParameters["sfu_parent"].DefaultValue = sfu_no;
                ((System.Web.UI.WebControls.GridView)e.Row.FindControl("GridView2")).DataBind();

                for (int i = 0; i < ((System.Web.UI.WebControls.GridView)e.Row.FindControl("GridView2")).Rows.Count; i++)
                {
                    string sfu_no2 = ((System.Web.UI.WebControls.GridView)e.Row.FindControl("GridView2")).DataKeys[i].Value.ToString();
                    string sql2 = "SELECT count(sysfuction.sfu_no) as total FROM rauthority INNER JOIN sysfuction ON rauthority.sfu_no = sysfuction.sfu_no WHERE (rauthority.rol_no = " + rol_no + ") AND (sysfuction.sfu_status = '1') AND (sysfuction.sfu_no = " + sfu_no2 + ")";

                    int count2 = Convert.ToInt32(new DBObject().ExecuteScalar(sql2));
                    if (count2 > 0)
                    {
                        ((System.Web.UI.WebControls.CheckBox)((System.Web.UI.WebControls.GridView)e.Row.FindControl("GridView2")).Rows[i].FindControl("cbox2")).Checked = true;
                    }
                    else
                    {
                        ((System.Web.UI.WebControls.CheckBox)((System.Web.UI.WebControls.GridView)e.Row.FindControl("GridView2")).Rows[i].FindControl("cbox2")).Checked = false;
                    }
                    
                }
            }
        }
    }

    /// <summary>
    /// 確定
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        int rol_no = Convert.ToInt32(Request["rol_no"]);
        string sql = "";
        DBObject dbo = new DBObject();

        #region 1.刪除rauthority

        dbo.ExecuteNonQuery("Delete from rauthority where rol_no=" + rol_no);
        
        #endregion

        #region 2.新增權限
        for (int i = 0; i < this.GridView1.Rows.Count; i++)
        {
            if (((System.Web.UI.WebControls.CheckBox)this.GridView1.Rows[i].FindControl("cbox1")).Checked)
            {
                string rau_sys = this.GridView1.DataKeys[i].Values[0].ToString();
                string sfu_no = this.GridView1.DataKeys[i].Values[1].ToString();

                sql = "insert into rauthority (rol_no,sfu_no,rau_sys) values (" + rol_no + "," + sfu_no + "," + rau_sys + ")";
                dbo.ExecuteNonQuery(sql);

                DataKeyArray dk = ((System.Web.UI.WebControls.GridView)this.GridView1.Rows[i].FindControl("GridView2")).DataKeys;

                //子系統
                for (int j = 0; j < ((System.Web.UI.WebControls.GridView)this.GridView1.Rows[i].FindControl("GridView2")).Rows.Count; j++)
                {
                    if (((System.Web.UI.WebControls.CheckBox)((System.Web.UI.WebControls.GridView)this.GridView1.Rows[i].FindControl("GridView2")).Rows[j].FindControl("cbox2")).Checked)
                    {
                        string rau_sys2 = this.GridView1.DataKeys[i].Values[0].ToString();
                        string sfu_no2 = ((System.Web.UI.WebControls.GridView)this.GridView1.Rows[i].FindControl("GridView2")).Rows[j].Cells[2].Text;

                        sql = "insert into rauthority (rol_no,sfu_no,rau_sys) values (" + rol_no + "," + sfu_no2 + "," + rau_sys2 + ")";
                        dbo.ExecuteNonQuery(sql);
                    }
                }
            }

            
        }
        #endregion

        #region 記錄log

        new DBObject().ExecuteOperates(350101, new SessionObject().sessionUserID, 3, "");
        
        #endregion

        this.ShowMsg("設定完成!");

        Response.Redirect("350101.aspx");
    }

    /// <summary>
    /// 取消
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("350101.aspx",true);
    }

    protected void GridView1_DataBound(object sender, EventArgs e)
    {
       
    }

    /// <summary>
    /// 開啟子選項
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("show"))
        {
            if (((System.Web.UI.WebControls.GridView)this.GridView1.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("GridView2")).Visible)
            {
                ((System.Web.UI.WebControls.GridView)this.GridView1.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("GridView2")).Visible = false;
            }
            else
            {
                ((System.Web.UI.WebControls.GridView)this.GridView1.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("GridView2")).Visible = true;
            }
        }
    }

    private void ShowMsg(string msg)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + msg + "');", true);
    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[2].Visible = false;
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex != -1)
            {
                e.Row.Cells[2].Visible = false;
            }

        }
    }
}
