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
/// 撰寫時間：2010/12/22
/// </summary>
public partial class _10_100400_100401_1 : System.Web.UI.Page
{
    ChangeObject changeobj = new ChangeObject();
    DBObject dbo = new DBObject();
    SessionObject sobj = new SessionObject();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if (Request["no"] != null) this.lab_no.Text = Request["no"];
            //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
            new OperatesObject().ExecuteOperates(300201, sobj.sessionUserID, 2, "問卷 預覽 no=" + this.lab_no.Text);
            

            this.Navigator1.SubFunc = "問卷填寫";
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

            if (CanInsert(Convert.ToInt32(this.lab_no.Text), Convert.ToInt32(sobj.sessionUserID)))
            {
                this.btn_submit.Enabled = true;
                this.btn_submit.Visible = true;
                this.btn_cancel.Enabled = true;
                this.btn_cancel.Visible = true;
            }
            else
            {
                this.btn_submit.Enabled = false;
                this.btn_submit.Visible = false;
                this.btn_cancel.Enabled = false;
                this.btn_cancel.Visible = false;
            }
        }
        this.DataList1.DataBind();

        this.btn_submit.Attributes["onclick"] = "javascript:this.disabled=true;" + this.Page.ClientScript.GetPostBackEventReference(this.btn_submit, "");
        this.btn_cancel.Attributes["onclick"] = "javascript:this.disabled=true;" + this.Page.ClientScript.GetPostBackEventReference(this.btn_cancel, "");
        this.btn_goback.Attributes["onclick"] = "javascript:this.disabled=true;" + this.Page.ClientScript.GetPostBackEventReference(this.btn_goback, "");
    }

    #region 調整格式
    protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
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
                        string str1 = "";
                        if (Request["theme_" + the_no] != null)
                        {
                            if (Request["theme_" + the_no].Equals(dt.Rows[i]["ans_no"].ToString())) str1 = " checked";
                        }
                        outxt += System.Environment.NewLine + "<div class=\"b3\"><input type=\"radio\" name=\"theme_" + the_no + "\" value=\"" + dt.Rows[i]["ans_no"].ToString() + "\" class=\"a-letter-t1\""+str1+" /> <span class=\"a-letter-t1\">" + dt.Rows[i]["ans_name"].ToString() + "</span></div>";
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
                        string str2 = "";
                        if (Request["theme_" + the_no] != null)
                        {
                            string[] val = Request["theme_" + the_no].Split(',');
                            for (int j = 0; j < val.Length; j++)
                            {
                                if (val[j].Equals(dt.Rows[i]["ans_no"].ToString())) str2 = " checked";
                            }
                        }
                        outxt += System.Environment.NewLine + "<div class=\"b3\"><input type=\"checkbox\" name=\"theme_" + the_no + "\" value=\"" + dt.Rows[i]["ans_no"].ToString() + "\" class=\"a-letter-t1\"" + str2 + " /> <span class=\"a-letter-t1\">" + dt.Rows[i]["ans_name"].ToString() + "</span></div>";
                    }
                    ((Label)e.Item.FindControl("lab_answer")).Text = outxt;
                }
                #endregion
            }
            else
            {
                #region 填充
                ((Label)e.Item.FindControl("lab_answer")).Text = "<div class=\"b3\"><input type=\"text\" name=\"theme_" + the_no + "\" size=\"60\" value=\"" + Request["theme_" + the_no] + "\" /></div>";
                #endregion
            }
        }
    }
    #endregion

    #region 回上一頁
    protected void btn_goback_Click(object sender, EventArgs e)
    {
        Response.Redirect("100401.aspx?count=" + new System.Random().Next(10000).ToString());
    }
    #endregion

    #region 取消
    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("100401.aspx?count=" + new System.Random().Next(10000).ToString());
    }
    #endregion

    #region 顯示錯誤訊息
    private void ShowMSG(string msg)
    {
        string script = "<script>alert('" + msg + "');</script>";
        this.ClientScript.RegisterStartupScript(this.GetType(), "msg", script);
    }
    #endregion

    #region 檢查輸入值
    private bool CheckInputValue()
    {
        int total = 0;
        #region 0.檢查是否已過期，且是否已填寫
        if (!CanInsert(Convert.ToInt32(this.lab_no.Text), Convert.ToInt32(sobj.sessionUserID)))
        {
            ShowMSG("填寫期限已過或您已填寫過了~");
            this.btn_submit.Enabled = false;
            this.btn_submit.Visible = false;
            return false;
        }
        #endregion
        #region 1.檢查是否都有作答了 2.計算總分
        string sqlstr = "SELECT the_no, the_name, the_type, the_order, the_count, the_fraction FROM theme WHERE (que_no = "+this.lab_no.Text+") AND (the_status = '1') ORDER BY the_order, the_no";
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        dt = dbo.ExecuteQuery(sqlstr);
        this.lab_count.Text = dt.Rows.Count.ToString();
        if (dt.Rows.Count > 0)
        {
            for (int x = 0; x < dt.Rows.Count; x++)
            {
                if (Request["theme_" + dt.Rows[x]["the_no"].ToString()] == null)
                {
                    ShowMSG("第" + Convert.ToString(x + 1) + "題：" + dt.Rows[x]["the_name"].ToString() + " 未作答!");
                    return false;
                }
                else
                {
                    if (Request["theme_" + dt.Rows[x]["the_no"].ToString()].Length <= 0)
                    {
                        ShowMSG("第" + Convert.ToString(x + 1) + "題：" + dt.Rows[x]["the_name"].ToString() + " 不可空白!");
                        return false;
                    }
                    else
                    {
                        
                        if (dt.Rows[x]["the_count"].ToString().Equals("1"))
                        {
                            #region 計分
                            if (dt.Rows[x]["the_type"].ToString().Equals("1"))
                            {
                                #region 單選
                                int ans_fraction = new AnswersDAO().GetFraction(Convert.ToInt32(this.lab_no.Text), Convert.ToInt32(dt.Rows[x]["the_no"].ToString()), Convert.ToInt32(Request["theme_" + dt.Rows[x]["the_no"].ToString()]));
                                total += ans_fraction;
                                #endregion
                            }
                            else if (dt.Rows[x]["the_type"].ToString().Equals("2"))
                            {
                                #region 複選
                                string sqlstr1 = "SELECT SUM(ans_fraction) AS ans_fraction FROM answers WHERE (que_no = " + this.lab_no.Text + ") AND (the_no = " + dt.Rows[x]["the_no"].ToString() + ") AND (ans_status = '1') and ans_no in (" + Request["theme_" + dt.Rows[x]["the_no"].ToString()] + ")";
                                dt1.Clear();
                                dt1 = dbo.ExecuteQuery(sqlstr1);
                                if (dt1.Rows.Count > 0)
                                {
                                    if (dt1.Rows[0]["ans_fraction"].ToString().Length > 0) total += Convert.ToInt32(dt1.Rows[0]["ans_fraction"].ToString());
                                }
                                #endregion
                            }
                            #endregion
                        }
                    }
                }
            }
        }
        this.lab_total.Text = total.ToString();
        #endregion

        return true;
    }
    #endregion

    #region 確定送出
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        string aMSG = "";
        try
        {
            if (CheckInputValue())
            {
                BotanizeDAO botDAO = new BotanizeDAO();
                botanize newRow = new botanize();
                newRow.bot_createtime = System.DateTime.Now;
                newRow.bot_createuid = Convert.ToInt32(sobj.sessionUserID);
                newRow.bot_date = System.DateTime.Now;
                newRow.bot_status = "1";
                newRow.bot_total = Convert.ToInt32(this.lab_total.Text);
                newRow.peo_uid = Convert.ToInt32(sobj.sessionUserID);
                botDAO.AddBotanize(newRow);
                botDAO.Update();
                int bot_no = newRow.bot_no;

                string sqlstr = "SELECT the_no, the_name, the_type, the_order, the_count, the_fraction FROM theme WHERE (que_no = " + this.lab_no.Text + ") AND (the_status = '1') ORDER BY the_order, the_no";
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();
                dt = dbo.ExecuteQuery(sqlstr);
                if (dt.Rows.Count > 0)
                {
                    for (int x = 0; x < dt.Rows.Count; x++)
                    {
                        int ans_fraction = 0;
                        if (dt.Rows[x]["the_count"].ToString().Equals("1"))
                        {
                            #region 計分
                            if (dt.Rows[x]["the_type"].ToString().Equals("1"))
                            {
                                #region 單選
                                ans_fraction = new AnswersDAO().GetFraction(Convert.ToInt32(this.lab_no.Text), Convert.ToInt32(dt.Rows[x]["the_no"].ToString()), Convert.ToInt32(Request["theme_" + dt.Rows[x]["the_no"].ToString()]));
                                #endregion
                            }
                            else if (dt.Rows[x]["the_type"].ToString().Equals("2"))
                            {
                                #region 複選
                                string sqlstr1 = "SELECT SUM(ans_fraction) AS ans_fraction FROM answers WHERE (que_no = " + this.lab_no.Text + ") AND (the_no = " + dt.Rows[x]["the_no"].ToString() + ") AND (ans_status = '1') and ans_no in (" + Request["theme_" + dt.Rows[x]["the_no"].ToString()] + ")";
                                dt1.Clear();
                                dt1 = dbo.ExecuteQuery(sqlstr1);
                                if (dt1.Rows.Count > 0)
                                {
                                    if (dt1.Rows[0]["ans_fraction"].ToString().Length > 0) ans_fraction = Convert.ToInt32(dt1.Rows[0]["ans_fraction"].ToString());
                                }
                                #endregion
                            }
                            #endregion
                        }

                        #region 新增語法
                        string InsStr = "insert into casework (bot_no,cas_no,que_no,the_no,cas_answer,cas_fraction) values ("
                            + bot_no.ToString() + "," + Convert.ToString(x + 1) + "," + this.lab_no.Text + ","+dt.Rows[x]["the_no"].ToString()+","
                            + "N'" + Request["theme_" + dt.Rows[x]["the_no"].ToString()] + "'," + ans_fraction + ")";
                        dbo.ExecuteNonQuery(InsStr);
                        #endregion
                    }
                }
                //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
                new OperatesObject().ExecuteOperates(100401, sobj.sessionUserID, 1, "主題：" + this.lab_quename.Text + ",填寫人：" + sobj.sessionUserName + ",bot_no=" + bot_no.ToString());

                Response.Write(PCalendarUtil.ShowMsg_URL("新增成功", "100401.aspx?count=" + new System.Random().Next(10000).ToString()));
            }
        }
        catch (Exception ex)
        {
            aMSG = "功能名稱：線上問卷-" + this.Navigator1.SubFunc + "<br>錯誤訊息:" + ex.ToString();
            Response.Write(aMSG);
        }
    }
    #endregion

    #region 是否可新增
    public bool CanInsert(int que_no,int peo_uid)
    {
        bool feedbak = false;
        DateTime sdate = new DateTime();
        DateTime edate = new DateTime();

        sdate = Convert.ToDateTime(changeobj.ROCDTtoADDT(this.lab_sdate.Text));
        edate = Convert.ToDateTime(changeobj.ROCDTtoADDT(this.lab_edate.Text));
        #region 檢查是否已填過
        if ((sdate <= System.DateTime.Now) && (System.DateTime.Now <= edate))
        {
            int bot_no = new BotanizeDAO().GetNoByQuePeoNO(que_no, peo_uid);
            if (bot_no == 0) feedbak = true;
        }
        #endregion
        return feedbak;
    }
    #endregion
}