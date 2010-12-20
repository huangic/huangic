using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;

/// <summary>
/// 功能名稱：管理作業 / 問卷管理 / 問卷題目維護--新增、修改
/// 功能編號：30/300200/300201
/// 撰寫者：Lina
/// 撰寫時間：2010/12/20
/// </summary>
public partial class _30_300200_300201_3 : System.Web.UI.Page
{
    DBObject dbo = new DBObject();
    CheckObject checkobj = new CheckObject();
    SessionObject sobj = new SessionObject();
    ChangeObject changeobj = new ChangeObject();
    DS_30TableAdapters.answersTableAdapter ansTA = new DS_30TableAdapters.answersTableAdapter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if (Request["mode"] != null) this.lab_mode.Text = Request["mode"];
            if (Request["que_no"] != null) this.lab_queno.Text = Request["que_no"];
            if (Request["the_no"] != null) this.lab_theno.Text = Request["the_no"];
            
            #region 問卷資料
            Entity.questionary queData = new QuestionaryDAO().GetByNo(Convert.ToInt32(this.lab_queno.Text));
            if (queData != null)
            {
                this.lab_name.Text = queData.que_name;
                this.lab_descript.Text = queData.que_descript;
                this.lab_sdate.Text = changeobj.ADDTtoROCDT(queData.que_sdate.Value.ToString("yyyy-MM-dd HH:mm"));
                this.lab_edate.Text = changeobj.ADDTtoROCDT(queData.que_edate.Value.ToString("yyyy-MM-dd HH:mm"));
            }
            #endregion

            #region 初始值
            if (this.lab_mode.Text.Equals("modify"))
            {
                this.Navigator1.SubFunc = "題目修改";
                Entity.theme theData = new ThemeDAO().GetByNo(Convert.ToInt32(this.lab_queno.Text), Convert.ToInt32(this.lab_theno.Text));
                if (theData != null)
                {
                    this.txt_name.Text = theData.the_name;
                    this.txt_order.Text = theData.the_order.ToString();
                    this.txt_fraction.Text = theData.the_fraction.ToString();
                    this.rbl_type.Items.FindByValue(theData.the_type).Selected = true;
                    this.rbl_count.Items.FindByValue(theData.the_count).Selected = true;
                }
                this.ObjectDataSource1.SelectParameters["que_no"].DefaultValue = this.lab_queno.Text;
                this.ObjectDataSource1.SelectParameters["the_no"].DefaultValue = this.lab_theno.Text;
                this.GridView1.DataSource = this.ObjectDataSource1;
                this.GridView1.DataBind();
            }
            else
            {
                this.Navigator1.SubFunc = "題目新增";
                this.rbl_type.Items[0].Selected = true;
                this.rbl_count.Items[0].Selected = true;

                this.ObjectDataSource1.SelectParameters["que_no"].DefaultValue = "0";
                this.ObjectDataSource1.SelectParameters["the_no"].DefaultValue = "0";
                this.GridView1.DataSource = this.ObjectDataSource1;
                this.GridView1.DataBind();
            }
            #endregion
        }
    }

    #region 檢查輸入值
    private bool CheckInputValue()
    {
        #region 題目名稱
        if (string.IsNullOrEmpty(this.txt_name.Text))
        {
            ShowMSG("請輸入 題目名稱");
            return false;
        }
        else if (!checkobj.IsValidLen(this.txt_name.Text.Trim(), 300))
        {
            ShowMSG("題目名稱 長度不可超過300個數文字");
            return false;
        }
        #endregion
        #region 題目順序
        if (string.IsNullOrEmpty(this.txt_order.Text))
        {
            ShowMSG("請輸入 題目順序");
            return false;
        }
        else 
        {
            try
            {
                int tmp = Convert.ToInt32(this.txt_order.Text);
            }
            catch
            {
                ShowMSG("問卷說明 長度不可超過500個數文字");
                return false;
            }
        }
        #endregion
        #region 是否計分
        if (this.rbl_count.SelectedValue.Equals("1"))
        {
            if (string.IsNullOrEmpty(this.txt_fraction.Text))
            {
                ShowMSG("請輸入 計分分數");
                return false;
            }
            //檢核該項目之總計
            if (this.rbl_type.SelectedValue.Equals("2"))
            {
                int subtotal = 0;
                for (int i = 0; i < this.GridView1.Rows.Count; i++)
                {
                    subtotal += Convert.ToInt32(this.GridView1.Rows[i].Cells[2].Text);
                }

                if (subtotal > Convert.ToInt32(this.txt_fraction.Text))
                {
                    ShowMSG("答案項目之總分，已超過本題之分數");
                    return false;
                }
            }
        }
        else
            this.txt_fraction.Text = "0";
        #endregion

        return true;
    }
    #endregion

    #region 確定
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        string aMSG = "";
        try
        {
            string msg = "";
            if (CheckInputValue())
            {
                if (this.lab_mode.Text.Equals("modify"))
                {
                    #region 修改
                    //更新 theme
                    ThemeDAO theDAO = new ThemeDAO();
                    theme newRow = theDAO.GetByNo(Convert.ToInt32(this.lab_queno.Text), Convert.ToInt32(this.lab_theno.Text));
                    newRow.the_count = this.rbl_count.SelectedValue;
                    newRow.the_createtime = System.DateTime.Now;
                    newRow.the_createuid = Convert.ToInt32(sobj.sessionUserID);
                    newRow.the_fraction = Convert.ToInt32(this.txt_fraction.Text);
                    newRow.the_name = this.txt_name.Text;
                    newRow.the_order = Convert.ToInt32(this.txt_order.Text);
                    newRow.the_type = this.rbl_type.SelectedValue;
                    theDAO.Update();
                    //更新answers
                    dbo.ExecuteNonQuery("update answers set ans_status='2',ans_createtime=getdate(),ans_createuid="+sobj.sessionUserID+" where que_no="+this.lab_queno.Text+" and the_no="+this.lab_theno.Text);
                    AnswersDAO ansDAO = new AnswersDAO();
                    for (int i = 0; i < this.GridView1.Rows.Count; i++)
                    {
	                    string ans_no= this.GridView1.DataKeys[i].Values[2].ToString();
                        answers bRow = ansDAO.GetByNo(Convert.ToInt32(this.lab_queno.Text), Convert.ToInt32(this.lab_theno.Text), Convert.ToInt32(ans_no));
                        if (bRow != null)
                        {
                            //修改
                            bRow.ans_createtime = System.DateTime.Now;
                            bRow.ans_createuid = Convert.ToInt32(sobj.sessionUserID);
                            bRow.ans_fraction = Convert.ToInt32(this.GridView1.Rows[i].Cells[2].Text);
                            bRow.ans_name = this.GridView1.Rows[i].Cells[0].Text;
                            bRow.ans_order = Convert.ToInt32(this.GridView1.Rows[i].Cells[1].Text);
                            bRow.ans_status = "1";
                            ansDAO.Update();
                        }
                        else
                        {
                            //新增
                            answers cRow = new answers();
                            cRow.que_no = Convert.ToInt32(this.lab_queno.Text);
                            cRow.the_no = Convert.ToInt32(this.lab_theno.Text);
                            cRow.ans_no = Convert.ToInt32(ans_no);
                            cRow.ans_createtime = System.DateTime.Now;
                            cRow.ans_createuid = Convert.ToInt32(sobj.sessionUserID);
                            cRow.ans_fraction = Convert.ToInt32(this.GridView1.Rows[i].Cells[2].Text);
                            cRow.ans_name = this.GridView1.Rows[i].Cells[0].Text;
                            cRow.ans_order = Convert.ToInt32(this.GridView1.Rows[i].Cells[1].Text);
                            cRow.ans_status = "1";
                            ansDAO.AddAnswers(cRow);
                            ansDAO.Update();
                        }
                    }

                    msg = "修改成功";
                    //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
                    //new OperatesObject().ExecuteOperates(300201, sobj.sessionUserID, 3, "編號：" + this.lab_no.Text + ",問卷名稱：" + this.txt_name.Text.Trim());
                    #endregion
                }
                else
                {
                    #region 新增
                    //取得the_no之編號
                    int the_no = new ThemeDAO().GetMaxTheNo(Convert.ToInt32(this.lab_queno.Text));
                    the_no++;
                    //新增 theme
                    ThemeDAO theDAO = new ThemeDAO();
                    theme newRow = new theme();
                    newRow.the_count = this.rbl_count.SelectedValue;
                    newRow.the_createtime = System.DateTime.Now;
                    newRow.the_createuid = Convert.ToInt32(sobj.sessionUserID);
                    newRow.the_fraction = Convert.ToInt32(this.txt_fraction.Text);
                    newRow.the_name = this.txt_name.Text;
                    newRow.the_no = the_no;
                    newRow.the_order = Convert.ToInt32(this.txt_order.Text);
                    newRow.the_status = "1";
                    newRow.the_type = this.rbl_type.SelectedValue;
                    newRow.que_no = Convert.ToInt32(this.lab_queno.Text);
                    theDAO.AddTheme(newRow);
                    theDAO.Update();
                    //新增 answers
                    AnswersDAO ansDAO = new AnswersDAO();
                    for (int i = 0; i < this.GridView1.Rows.Count; i++)
                    {
                        answers bRow = new answers();
                        bRow.que_no = Convert.ToInt32(this.lab_queno.Text);
                        bRow.the_no = the_no;
                        bRow.ans_no = i + 1;
                        bRow.ans_createtime = System.DateTime.Now;
                        bRow.ans_createuid = Convert.ToInt32(sobj.sessionUserID);
                        bRow.ans_fraction = Convert.ToInt32(this.GridView1.Rows[i].Cells[2].Text);
                        bRow.ans_name = this.GridView1.Rows[i].Cells[0].Text;
                        bRow.ans_order = Convert.ToInt32(this.GridView1.Rows[i].Cells[1].Text);
                        bRow.ans_status = "1";
                        ansDAO.AddAnswers(bRow);
                        ansDAO.Update();
                    }

                    msg = "新增成功";
                    //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
                    new OperatesObject().ExecuteOperates(300401, sobj.sessionUserID, 1, "題目名稱：" + this.txt_name.Text.Trim());
                    #endregion
                }

                this.Page.ClientScript.RegisterStartupScript(typeof(_30_300200_300201_3), "closeThickBox", "self.parent.update('" + msg + "');", true);
            }
        }
        catch (Exception ex)
        {
            aMSG = "功能名稱：問卷題目維護-" + this.Navigator1.SubFunc + "<br>錯誤訊息:" + ex.ToString();
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

    #region 檢查輸入值--項目
    private bool CheckSubInputValue()
    {
        #region 項目
        if (string.IsNullOrEmpty(this.txt_ansname.Text))
        {
            ShowMSG("請輸入 項目");
            return false;
        }
        else if (!checkobj.IsValidLen(this.txt_ansname.Text.Trim(), 100))
        {
            ShowMSG("項目 長度不可超過100個數文字");
            return false;
        }
        #endregion
        #region 順序
        if (string.IsNullOrEmpty(this.txt_ansorder.Text))
        {
            ShowMSG("請輸入 順序");
            return false;
        }
        else
        {
            try
            {
                int tmp = Convert.ToInt32(this.txt_ansorder.Text);
            }
            catch
            {
                ShowMSG("順序 需為數字");
                return false;
            }
        }
        #endregion
        #region 順序
        if (string.IsNullOrEmpty(this.txt_ansfraction.Text))
        {
            if (this.rbl_count.SelectedValue.Equals("1"))
            {
                ShowMSG("請輸入 分數");
                return false;
            }
            else
            {
                this.txt_ansfraction.Text = "0";
            }
        }
        else
        {
            try
            {
                int tmp = Convert.ToInt32(this.txt_ansfraction.Text);
            }
            catch
            {
                ShowMSG("分數 需為數字");
                return false;
            }
        }
        
        #endregion
        return true;
    }
    #endregion

    #region 加入項目
    protected void btn_additem_Click(object sender, EventArgs e)
    {
        string aMSG = "";
        try
        {
            if (CheckSubInputValue())
            {
                DS_30.answersDataTable ansDT = this.ansTA.GetData(0,0);
                bool added = false;
                #region 舊資料&檢查是否已新增過
                for (int i = 0; i < this.GridView1.Rows.Count; i++)
                {
                    DS_30.answersRow aRow = ansDT.NewanswersRow();
                    if (this.lab_queno.Text.Length > 0)
                        aRow.que_no = Convert.ToInt32(this.lab_queno.Text);
                    else
                        aRow.que_no = 0;

                    if (this.lab_theno.Text.Length > 0)
                        aRow.the_no = Convert.ToInt32(this.lab_theno.Text);
                    else
                        aRow.the_no = 0;
                    
                    aRow.ans_no = i + 1;
                    aRow.ans_name = this.GridView1.Rows[i].Cells[0].Text;
                    aRow.ans_order = Convert.ToInt32(this.GridView1.Rows[i].Cells[1].Text);
                    aRow.ans_fraction = Convert.ToInt32(this.GridView1.Rows[i].Cells[2].Text);
                    ansDT.AddanswersRow(aRow);

                    if (this.GridView1.Rows[i].Cells[0].Text.Equals(this.txt_ansname.Text))
                    {
                        added = true;
                    }
                }
                #endregion

                #region 新資料
                if (added.Equals(false))
                {
                    DS_30.answersRow bRow = ansDT.NewanswersRow();
                    if (this.lab_queno.Text.Length > 0)
                        bRow.que_no = Convert.ToInt32(this.lab_queno.Text);
                    else
                        bRow.que_no = 0;
                    
                    if (this.lab_theno.Text.Length > 0)
                        bRow.the_no = Convert.ToInt32(this.lab_theno.Text);
                    else
                        bRow.the_no = 0;

                    bRow.ans_no = this.GridView1.Rows.Count + 1;
                    bRow.ans_name = this.txt_ansname.Text;
                    bRow.ans_order = Convert.ToInt32(this.txt_ansorder.Text);
                    bRow.ans_fraction = Convert.ToInt32(this.txt_ansfraction.Text);
                    ansDT.AddanswersRow(bRow);
                }
                else
                {
                    Response.Write("<script>alert(\"此項目已加入\");</script>");
                    return;
                }
                #endregion
                this.GridView1.DataSource = ansDT;
                this.GridView1.DataBind();

                this.txt_ansfraction.Text = "";
                this.txt_ansorder.Text = "";
                this.txt_ansname.Text = "";
            }
        }
        catch (Exception ex)
        {
            aMSG = "功能名稱：問卷題目維護-" + this.Navigator1.SubFunc + "<br>錯誤訊息:" + ex.ToString();
            Response.Write(aMSG);
        }
    }
    #endregion

    #region 刪除項目
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("delitem"))
        {
            string aMSG = "";
            try
            {
                int itemindex = Convert.ToInt32(e.CommandArgument);
                DS_30.answersDataTable ansDT = this.ansTA.GetData(0, 0);
                #region 舊資料&檢查是否已新增過
                int icount = 0;
                for (int i = 0; i < this.GridView1.Rows.Count; i++)
                {
                    if (!(i == itemindex))
                    {
                        icount++;
                        DS_30.answersRow aRow = ansDT.NewanswersRow();
                        if (this.lab_queno.Text.Length > 0)
                            aRow.que_no = Convert.ToInt32(this.lab_queno.Text);
                        else
                            aRow.que_no = 0;

                        if (this.lab_theno.Text.Length > 0)
                            aRow.the_no = Convert.ToInt32(this.lab_theno.Text);
                        else
                            aRow.the_no = 0;

                        aRow.ans_no = i + 1;
                        aRow.ans_name = this.GridView1.Rows[i].Cells[0].Text;
                        aRow.ans_order = Convert.ToInt32(this.GridView1.Rows[i].Cells[1].Text);
                        aRow.ans_fraction = Convert.ToInt32(this.GridView1.Rows[i].Cells[2].Text);
                        ansDT.AddanswersRow(aRow);
                    }

                }
                #endregion
                this.GridView1.DataSource = ansDT;
                this.GridView1.DataBind();
            }
            catch (Exception ex)
            {
                aMSG = "功能名稱：問卷題目維護-" + this.Navigator1.SubFunc + "<br>錯誤訊息:" + ex.ToString();
                Response.Write(aMSG);
            }
        }
    }
    #endregion
}