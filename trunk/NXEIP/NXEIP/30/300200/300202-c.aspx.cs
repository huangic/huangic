using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using NXEIP.DAO;
using Entity;

public partial class _30_300200_300202_c : System.Web.UI.Page
{
    DBObject dbo = new DBObject();
    CheckObject checkobj = new CheckObject();
    SessionObject sobj = new SessionObject();
    ChangeObject changeobj = new ChangeObject();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if (Request["bot_no"] != null) this.lab_botno.Text = Request["bot_no"];

            this.Navigator1.SubFunc = "詳細填寫內容";
            #region 問卷基本資料
            Entity.botanize botData = new BotanizeDAO().GetByNo(Convert.ToInt32(this.lab_botno.Text));
            if (botData != null)
            {
                this.lab_people.Text = botData.people.peo_name;
                this.lab_date.Text = changeobj.ADDTtoROCDT(botData.bot_date.Value.ToString("yyyy-MM-dd HH:mm:ss"));

                string sqlstr = "select TOP 1 casework.que_no, questionary.que_name, questionary.que_descript from casework INNER JOIN questionary ON casework.que_no = questionary.que_no"
                    +" where casework.bot_no = "+this.lab_botno.Text;
                DataTable dt = new DataTable();
                dt = dbo.ExecuteQuery(sqlstr);
                if (dt.Rows.Count > 0)
                {
                    this.lab_name.Text = dt.Rows[0]["que_name"].ToString();
                }
            }
            #endregion
        }
    }
    #region 調整輸出格式
    protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            string que_no = ((Label)e.Item.FindControl("lab_queno")).Text;
            string the_no = ((Label)e.Item.FindControl("lab_theno")).Text;
            string casanswer = ((Label)e.Item.FindControl("lab_casanswer")).Text;
            string the_type=((Label)e.Item.FindControl("lab_type")).Text;
            if (the_type.Equals("1"))
            {
                #region 單選
                string sqlstr = "SELECT ans_no, ans_name FROM answers WHERE (que_no = " + que_no + ") AND (the_no = " + the_no + ") and (ans_no=" + casanswer + ")";
                DataTable dt = new DataTable();
                dt = dbo.ExecuteQuery(sqlstr);
                if (dt.Rows.Count > 0) ((Label)e.Item.FindControl("lab_answer")).Text = "&nbsp;Ans：" + dt.Rows[0]["ans_name"].ToString();
                #endregion
            }
            else if (the_type.Equals("2"))
            {
                #region 複選
                string sqlstr = "SELECT ans_no, ans_name FROM answers WHERE (que_no = " + que_no + ") AND (the_no = " + the_no + ") AND (ans_no in (" + casanswer + ")) ORDER BY ans_order";
                DataTable dt = new DataTable();
                dt = dbo.ExecuteQuery(sqlstr);
                if (dt.Rows.Count > 0)
                {
                    string outxt = "";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (outxt.Length > 0) outxt += "、";
                        outxt += dt.Rows[i]["ans_name"].ToString();
                    }
                    ((Label)e.Item.FindControl("lab_answer")).Text = "&nbsp;Ans：" + outxt;
                }
                #endregion
            }
            else
            {
                #region 填充
                ((Label)e.Item.FindControl("lab_answer")).Text = "&nbsp;Ans：" + casanswer;
                #endregion
            }
        }
    }
    #endregion
}