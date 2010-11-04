using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using NXEIP.DAO;
using Entity;

public partial class _10_100300_100302_1 : System.Web.UI.Page
{
    SessionObject sobj = new SessionObject();
    DBObject dbo = new DBObject();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
            new OperatesObject().ExecuteOperates(100302, sobj.sessionUserID, 2, "新增開放人員");

            ListItem selectitem = new ListItem("請選擇", "0");

            DataTable dt = new DataTable();
            string sqlstr = "SELECT dep_no, dep_name FROM departments WHERE (dep_level > 0) AND (dep_status = '1') ORDER BY dep_code";
            dt = dbo.ExecuteQuery(sqlstr);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ListItem newitem = new ListItem(dt.Rows[i]["dep_name"].ToString(), dt.Rows[i]["dep_no"].ToString());
                    this.ddl_depart.Items.Add(newitem);
                }
            }
            this.ddl_depart.Items.Insert(0, selectitem);
            this.ddl_people.Items.Insert(0, selectitem);

        }
    }

    #region 部門改變時
    protected void ddl_depart_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.ddl_people.Items.Clear();   
        ListItem selectitem = new ListItem("請選擇", "0");
        if (!this.ddl_depart.SelectedValue.Equals("0"))
        {
            DataTable dt = new DataTable();
            string jobtype = PCalendarUtil.GetPeoJobtype();
            string sqlstr = "SELECT people.peo_uid, people.peo_name FROM people INNER JOIN types ON people.peo_pfofess = types.typ_no"
                + " WHERE (people.peo_jobtype = " + jobtype + ") AND (people.dep_no = " + this.ddl_depart.SelectedValue + ") ORDER BY types.typ_order, people.peo_name";
            dt = dbo.ExecuteQuery(sqlstr);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ListItem newitem = new ListItem(dt.Rows[i]["peo_name"].ToString(), dt.Rows[i]["peo_uid"].ToString());
                    this.ddl_people.Items.Add(newitem);
                }
            }
        }
        this.ddl_people.Items.Insert(0, selectitem);
    }
    #endregion

    #region 確定
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        string aMSG = "";   //記錄錯誤訊息
        try
        {
            if (this.ddl_people.SelectedValue.Equals("0"))
            {
                ShowMSG("請選擇開放人員");
            }
            else
            {
                int icount = new C01DAO().GetCountByC01PeoUid(Convert.ToInt32(sobj.sessionUserID), Convert.ToInt32(this.ddl_people.SelectedValue));
                if (icount > 0)
                {
                    ShowMSG("此人員已在名單中");
                }
                else
                {
                    string InsStr = "insert into c01 (peo_uid,c01_peouid,c01_createtime) values(" + this.ddl_people.SelectedValue + "," + sobj.sessionUserID + ",getdate())";
                    dbo.ExecuteNonQuery(InsStr);
                    this.Page.ClientScript.RegisterStartupScript(typeof(_10_100300_100302_1), "closeThickBox", "self.parent.location.reload(true);self.parent.tb_remove();", true);
                }
            }
        }
        catch (Exception ex)
        {
            aMSG = "功能名稱:新增開放人員--確定<br>錯誤訊息:" + ex.ToString();
            Response.Write(aMSG);
        }
    }
    #endregion

    #region 顯示錯誤訊息
    private void ShowMSG(string msg)
    {
        string script = "<script>alert('" + msg + "');</script>";
        this.ClientScript.RegisterStartupScript(this.GetType(), "msg", script);
    }
    #endregion
}