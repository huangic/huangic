using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;
using System.Data;


/// <summary>
/// 功能名稱：全府應用 / 業務資訊 / 相關網站
/// 功能編號：20/200500/200507
/// 撰寫者：Lina
/// 撰寫時間：2011/03/17
/// </summary>
public partial class _20_200500_200507 : System.Web.UI.Page
{
    ChangeObject changeobj = new ChangeObject();
    DBObject dbo = new DBObject();
    SessionObject sobj = new SessionObject();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
            new OperatesObject().ExecuteOperates(200507, sobj.sessionUserID, 2, "相關網站 列表");

            string sqlstr = "select s06_no, s06_name from sys06 where (sfu_no = 200507) and (s06_status = '1') order by s06_order";
            DataTable dt = new DataTable();
            dt = dbo.ExecuteQuery(sqlstr);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.ddl_sys06.Items.Add(new ListItem(dt.Rows[i]["s06_name"].ToString(), dt.Rows[i]["s06_no"].ToString()));
                }
            }
            this.ddl_sys06.Items.Insert(0, new ListItem("全部", "0"));

            ShowList();
        }

        //判斷來自JS 使用_doPostBack(updatePanel,"") 的情況 
        if (Request["__EVENTTARGET"] == this.UpdatePanel1.ClientID && String.IsNullOrEmpty(Request["__EVENTARGUMENT"]))
        {
            ShowList();
        }
    }

    #region 刪除
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("del"))
        {
            string pkno = this.GridView1.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString();
            string sqlstr = "update commend set com_status='2',com_createtime=getdate() where com_no=" + pkno;
            dbo.ExecuteNonQuery(sqlstr);

            //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
            new OperatesObject().ExecuteOperates(200507, sobj.sessionUserID, 3, "刪除 相關網站 編號:" + pkno);
            ShowList();
        }
    }
    #endregion

    #region 搜尋
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        string aMSG = "";
        try
        {
            ShowList();
        }
        catch (Exception ex)
        {
            aMSG = "功能名稱：相關網站--" + this.Navigator1.SubFunc + "<br>錯誤訊息:" + ex.ToString();
            Response.Write(aMSG);
        }
    }
    #endregion

    #region 列表
    private void ShowList()
    {
        this.ObjectDataSource1.SelectParameters["sys06no"].DefaultValue = this.ddl_sys06.SelectedValue;
        if (this.txt_keyword.Text.Trim().Length > 0)
            this.ObjectDataSource1.SelectParameters["keyword"].DefaultValue = this.txt_keyword.Text;
        else
            this.ObjectDataSource1.SelectParameters["keyword"].DefaultValue = "";
        this.GridView1.DataBind();
    }
    #endregion

    #region 調整輸出格式
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string pkno = ((GridView)sender).DataKeys[e.Row.RowIndex].Value.ToString();
            if(new CommendDAO().GetUidByNo(Convert.ToInt32(pkno))!=Convert.ToInt32(sobj.sessionUserID))
            {
                e.Row.Cells[2].Text = "&nbsp;";
                e.Row.Cells[3].Text = "&nbsp;";
            }
        }
    }
    #endregion
}