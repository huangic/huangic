using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity;
using NXEIP.DAO;

public partial class _35_350300_350302 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = System.Convert.ToInt32(e.CommandArgument);
        int arg_no = System.Convert.ToInt32(this.GridView1.DataKeys[rowIndex].Value.ToString());

        if (e.CommandName.Equals("del"))
        {
            ArgumentsDAO dao = new ArgumentsDAO();
            arguments data = dao.GetByArgNo(arg_no);
            try
            {
                new OperatesObject().ExecuteOperates(350302, new SessionObject().sessionUserID, 4, "刪除參數:" + data.arg_variable);
            }
            catch
            {
            }

            dao.DeleteArguments(data);
            dao.Update();
            
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
        }
    }

    /// <summary>
    /// 查詢關鍵字
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button2_Click(object sender, EventArgs e)
    {
        this.ODS_arguments.SelectMethod = "GetBySearch";
        this.ODS_arguments.SelectParameters.Clear();
        this.ODS_arguments.SelectParameters.Add("str", this.tbox_key.Text);
        this.ODS_arguments.EnablePaging = false;

        this.GridView1.AllowPaging = false;
        this.GridView1.DataBind();


    }

    /// <summary>
    /// 查詢全部
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button3_Click(object sender, EventArgs e)
    {
        this.ODS_arguments.SelectMethod = "GetAll";
        this.ODS_arguments.SelectParameters.Clear();
        this.ODS_arguments.EnablePaging = true;

        this.GridView1.AllowPaging = true;
        this.GridView1.DataBind();
    }

    protected void UpdatePanel1_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(this.tbox_key.Text))
        {
            this.ODS_arguments.SelectMethod = "GetBySearch";
            this.ODS_arguments.SelectParameters.Clear();
            this.ODS_arguments.SelectParameters.Add("str", this.tbox_key.Text);
            this.ODS_arguments.EnablePaging = false;

            this.GridView1.AllowPaging = false;
            this.GridView1.DataBind();
        }
    }
}