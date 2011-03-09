using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;


/// <summary>
/// 功能名稱：管理作業 / 車輛管理 / 選項設定
/// 功能編號：30/301100/301101
/// 撰寫者：Lina
/// 撰寫時間：2011/03/09
/// </summary>
public partial class _30_301100_301101 : System.Web.UI.Page
{
    ChangeObject changeobj = new ChangeObject();
    DBObject dbo = new DBObject();
    SessionObject sobj = new SessionObject();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.rbl_number.Items[0].Selected = true;
            ShowList();
            
            //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
            new OperatesObject().ExecuteOperates(301101, sobj.sessionUserID, 2, "選項設定列表");
        }

        //判斷來自JS 使用_doPostBack(updatePanel,"") 的情況 
        if (Request["__EVENTTARGET"] == this.UpdatePanel1.ClientID && String.IsNullOrEmpty(Request["__EVENTARGUMENT"]))
        {
            ShowList();
        }
    }

    #region 調整輸出格式
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = new M01DAO().GetNumberName(e.Row.Cells[0].Text);
        }
    }
    #endregion

    #region 刪除
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("del"))
        {
            string pkno = this.GridView1.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString();
            string sqlstr = "update m01 set m01_status='2',m01_createuid=" + sobj.sessionUserID + ",m01_createtime=getdate() where m01_no=" + pkno;
            dbo.ExecuteNonQuery(sqlstr);

            //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
            new OperatesObject().ExecuteOperates(301101, sobj.sessionUserID, 3, "刪除 選項 編號:" + pkno);

            ShowList();
        }
    }
    #endregion

    #region 選項類別變換時
    protected void rbl_number_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowList();
    }
    #endregion

    #region 執行列表動作
    private void ShowList()
    {
        this.lab_number.Text = this.rbl_number.SelectedItem.Text;
        this.ObjectDataSource1.SelectParameters["number"].DefaultValue = this.rbl_number.SelectedValue;
        this.GridView1.DataBind();
    }
    #endregion
}