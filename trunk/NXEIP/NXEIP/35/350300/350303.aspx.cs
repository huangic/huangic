using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using NXEIP.DAO;
using Entity;

/// <summary>
/// 功能名稱：帳號管理 / 系統管理 / 舊行事曆匯入
/// 功能編號：35/350300/350303
/// 撰寫者：Lina
/// 撰寫時間：2011/03/16
/// </summary>
/// 
public partial class _35_350300_350303 : System.Web.UI.Page
{
    ChangeObject changeobj = new ChangeObject();
    DBObject dbo = new DBObject();
    SessionObject sobj = new SessionObject();
    CheckObject checkobj = new CheckObject();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            DataTable dt = new DataTable();
            string sqlstr = "select count(*) as recount from eip_calendar";
            dt.Clear();
            dt = dbo.ExecuteQuery(sqlstr);
            if (dt.Rows.Count > 0) this.lab_total.Text = dt.Rows[0]["recount"].ToString();

            sqlstr = "select count(*) as recount from eip_calendar where exporti='1'";
            dt.Clear();
            dt = dbo.ExecuteQuery(sqlstr);
            if (dt.Rows.Count > 0) this.lab_OkCount.Text = dt.Rows[0]["recount"].ToString();

            sqlstr = "select count(*) as recount from eip_calendar where exporti='0'";
            dt.Clear();
            dt = dbo.ExecuteQuery(sqlstr);
            if (dt.Rows.Count > 0) this.lab_NoCount.Text = dt.Rows[0]["recount"].ToString();
        }
    }

    #region 開始匯入
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        string aMSG = "";
        try
        {
            string sqlstr1 = "";
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            C02DAO c02DAO1 = new C02DAO();
            int icount = 0;
            string startno = "0";
            this.lab_outxt.Text += "開始匯入-----------------------------------------<br />";
            for (int jj = 0; jj < 28; jj++)
            {
                #region 取得五百筆資料
                sqlstr1 = "SELECT top 500 serial_no, c_date, time_start, time_end, event_title, event_content, event_place,  c_uid, c_name, event_record from eip_calendar where exporti='0' and serial_no>" + startno + " ORDER BY serial_no";
                dt1 = dbo.ExecuteQuery(sqlstr1);
                if (dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        
                        startno = dt1.Rows[i]["serial_no"].ToString();
                        #region 查看看是不是有此人名
                        string sqlstr2 = "SELECT peo_uid FROM people WHERE (peo_name = N'" + dt1.Rows[i]["c_name"].ToString().Trim() + "')";
                        dt2.Clear();
                        dt2 = dbo.ExecuteQuery(sqlstr2);
                        if (dt2.Rows.Count > 0)
                        {
                            int newpk = new C02DAO().GetMaxNoByPeoUid(Convert.ToInt32(dt2.Rows[0]["peo_uid"].ToString())) + 1;
                            DateTime sdate = Convert.ToDateTime(Convert.ToDateTime(dt1.Rows[i]["c_date"].ToString()).ToString("yyyy-MM-dd") + " " + dt1.Rows[i]["time_start"].ToString());
                            DateTime edate = Convert.ToDateTime(Convert.ToDateTime(dt1.Rows[i]["c_date"].ToString()).ToString("yyyy-MM-dd") + " " + dt1.Rows[i]["time_end"].ToString());

                            #region 單一筆新增
                            c02 newRow = new c02();
                            newRow.peo_uid = Convert.ToInt32(dt2.Rows[0]["peo_uid"].ToString());
                            newRow.c02_bgcolor = "#FFFFFF";
                            newRow.c02_createtime = System.DateTime.Now;
                            newRow.c02_createuid = Convert.ToInt32(sobj.sessionUserID);
                            newRow.c02_edate = edate;
                            newRow.c02_no = newpk;
                            newRow.c02_place = dt1.Rows[i]["event_place"].ToString();
                            newRow.c02_project = dt1.Rows[i]["event_content"].ToString().Replace("\r\n", System.Environment.NewLine);
                            newRow.c02_result = dt1.Rows[i]["event_record"].ToString().Replace("\r\n", System.Environment.NewLine);
                            newRow.c02_sdate = sdate;
                            newRow.c02_setuid = Convert.ToInt32(dt2.Rows[0]["peo_uid"].ToString());
                            newRow.c02_title = dt1.Rows[i]["event_title"].ToString();
                            newRow.c02_check = "1";
                            newRow.c02_appointmen = "2";
                            c02DAO1.AddC02(newRow);
                            c02DAO1.Update();
                            #endregion

                            //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
                            new OperatesObject().ExecuteOperates(100301, sobj.sessionUserID, 1, "舊行事曆匯入 peo_uid" + dt2.Rows[0]["peo_uid"].ToString() + ",c02_no=" + newpk);

                            dbo.ExecuteNonQuery("update eip_calendar set exporti='1' where serial_no=" + dt1.Rows[i]["serial_no"].ToString());
                            this.lab_outxt.Text += "匯入--" + dt1.Rows[i]["c_name"].ToString() + " 於 " + dt1.Rows[i]["c_date"].ToString() + " " + dt1.Rows[i]["time_start"].ToString() + "~" + dt1.Rows[i]["time_end"].ToString() + " " + dt1.Rows[i]["event_title"].ToString() + " 之行事曆：Success<br />";
                            //this.lab_outxt.Text += "<br />";
                            icount++;
                        }
                        else
                        {
                            //this.lab_outxt.Text += "error(找不到peo_uid)<br />";
                        }
                        #endregion
                    }
                }
                #endregion
            }
            this.lab_outxt.Text += "<br />此次總計匯筆數為：" + icount.ToString();
        }
        catch (Exception ex)
        {
            aMSG = "功能名稱：" + this.Navigator1.SubFunc + "<br>錯誤訊息:" + ex.ToString();
            Response.Write(aMSG);
        }
    }
    #endregion
}