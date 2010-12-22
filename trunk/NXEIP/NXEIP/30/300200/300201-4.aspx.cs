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
/// 功能名稱：管理作業 / 問卷管理 / 問卷維護--預覽
/// 功能編號：30/300200/300201
/// 撰寫者：Lina
/// 撰寫時間：2010/12/20
/// </summary>
public partial class _30_300200_300201_4 : System.Web.UI.Page
{
    ChangeObject changeobj = new ChangeObject();
    DBObject dbo = new DBObject();
    SessionObject sobj = new SessionObject();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if(Request["no"]!=null) this.lab_no.Text=Request["no"];
            //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
            new OperatesObject().ExecuteOperates(300201, sobj.sessionUserID, 2, "問卷 預覽 no="+this.lab_no.Text);
            #region 問卷基本資料
            questionary que = new QuestionaryDAO().GetByNo(Convert.ToInt32(this.lab_no.Text));
            if (que != null)
            {
                this.lab_quename.Text = que.que_name;
                this.lab_descript.Text = que.que_descript;
                this.lab_sdate.Text = changeobj.ADDTtoROCDT(que.que_sdate.Value.ToString("yyyy-MM-dd HH:mm"));
                this.lab_edate.Text = changeobj.ADDTtoROCDT(que.que_edate.Value.ToString("yyyy-MM-dd HH:mm"));
                this.lab_end.Text = que.que_end;
            }
            #endregion
        }
    }
    #region 回上一頁
    protected void btn_goback_Click(object sender, EventArgs e)
    {
        Response.Redirect("300201.aspx?count=" + new System.Random().Next(10000).ToString());
    }
    #endregion

    #region 調整格式
    protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType==ListItemType.AlternatingItem)
        {
            string the_no = ((DataList)sender).DataKeys[e.Item.ItemIndex].ToString();
            if (((Label)e.Item.FindControl("lab_type")).Text.Equals("1"))
            {
                #region 單選
                string sqlstr = "SELECT ans_no, ans_name FROM answers WHERE (que_no = " + this.lab_no.Text + ") AND (the_no = " + the_no + ") AND (ans_status = '1') ORDER BY ans_order";
                DataTable dt = new DataTable();
                dt = dbo.ExecuteQuery(sqlstr);
                if (dt.Rows.Count > 0)
                {
                    string outxt = "";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        outxt += System.Environment.NewLine + "<div class=\"b3\"><input type=\"radio\" name=\"theme_" + the_no + "\" id=\"theme_" + the_no + "\" value=\"" + dt.Rows[i]["ans_no"].ToString() + "\" class=\"a-letter-t1\" /> <span class=\"a-letter-t1\">" + dt.Rows[i]["ans_name"].ToString() + "</span></div>";
                    }
                    ((Label)e.Item.FindControl("lab_answer")).Text = outxt;
                }
                #endregion
            }
            else if (((Label)e.Item.FindControl("lab_type")).Text.Equals("2"))
            {
                #region 複選
                string sqlstr = "SELECT ans_no, ans_name FROM answers WHERE (que_no = " + this.lab_no.Text + ") AND (the_no = " + the_no + ") AND (ans_status = '1') ORDER BY ans_order";
                DataTable dt = new DataTable();
                dt = dbo.ExecuteQuery(sqlstr);
                if (dt.Rows.Count > 0)
                {
                    string outxt = "";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        outxt += System.Environment.NewLine + "<div class=\"b3\"><input type=\"checkbox\" name=\"theme_" + the_no + "\" id=\"theme_" + the_no + "\" value=\"" + dt.Rows[i]["ans_no"].ToString() + "\" class=\"a-letter-t1\" /> <span class=\"a-letter-t1\">" + dt.Rows[i]["ans_name"].ToString() + "</span></div>";
                    }
                    ((Label)e.Item.FindControl("lab_answer")).Text = outxt;
                }
                #endregion
            }
            else
            {
                #region 填充
                ((Label)e.Item.FindControl("lab_answer")).Text = "<div class=\"b3\"><input type=\"text\" name=\"theme_"+the_no+"\" id=\"theme_"+the_no+"\" size=\"60\" /></div>";
                #endregion
            }
        }
    }
    #endregion
}