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
/// 功能名稱：管理作業 / 車輛管理 / 車輛資料
/// 功能編號：30/301100/301102
/// 撰寫者：Lina
/// 撰寫時間：2011/03/09
/// </summary>
public partial class _30_301100_301102 : System.Web.UI.Page
{
    ChangeObject changeobj = new ChangeObject();
    DBObject dbo = new DBObject();
    SessionObject sobj = new SessionObject();
    CheckObject checkobj = new CheckObject();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if (Request["pageIndex"] != null) this.lab_pageIndex.Text = Request["pageIndex"];
            if (Session["301102_value"] != null && Session["301102_value"].ToString().Length > 0 && Request["count"] != null)
            {
                string[] val = Session["301102_value"].ToString().Split(','); //0:mode,1:pkno,2:pageIndex
                this.lab_mode.Text = val[0];
                this.lab_no.Text = val[1];
                this.lab_pageIndex.Text = val[2];
                ShowDataModify();
                Session["301102_value"] = "";
            }
            else
            {
                ShowDataList();
            }
        }
    }

    #region 畫面：列表
    private void ShowDataList()
    {
        this.lab_no.Text = "";
        this.MultiView1.ActiveViewIndex = 0;
        this.Navigator1.SubFunc = "";
        if (this.lab_pageIndex.Text.Length > 0)
        {
            this.GridView1.DataBind();
            this.GridView1.PageIndex = Convert.ToInt32(this.lab_pageIndex.Text);
        }
        //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
        new OperatesObject().ExecuteOperates(301102, sobj.sessionUserID, 2, "車輛資料列表");
    }
    #endregion

    #region 清空值
    private void ClearControlValue()
    {
        this.txt_cc.Text = "";
        this.txt_code.Text = "";
        this.txt_count.Text = "";
        this.txt_engine.Text = "";
        this.txt_memo.Text = "";
        this.txt_number.Text = "";
        this.txt_type.Text = "";
        this.txt_useyear.Text = "";
        this.txt_year.Text = "";

        this.ddl_chekuan.Items.Clear();
        this.ddl_color.Items.Clear();
        this.ddl_energy.Items.Clear();
        this.ddl_factory.Items.Clear();
        this.ddl_mark.Items.Clear();
        this.ddl_platoon.Items.Clear();
        this.ddl_source.Items.Clear();

        this.cal_buydate.ClearValue();
        this.cal_cdate.ClearValue();
        this.cal_change.ClearValue();
        this.cal_overdate.ClearValue();
        this.cal_sdate.ClearValue();
        this.cal_testdate.ClearValue();

        this.DepartTreeTextBox1.Clear();
        this.ImageUpload1.Visible = false;
        this.ImageUpload1.ClearPrivew();
        this.lbtn_delpic1.Visible = false;
        this.Panel1.Visible = false;
    }
    #endregion

    #region 下拉式選單
    private void ShowM01()
    {
        string sqlstr = "select m01_no,m01_number,m01_name from m01 where m01_status='1' order by m01_number,m01_code";
        DataTable dt = new DataTable();
        dt = dbo.ExecuteQuery(sqlstr);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                try
                {
                    ((DropDownList)this.FindControl("ctl00$ContentPlaceHolder1$ddl_" + dt.Rows[i]["m01_number"].ToString())).Items.Add(new ListItem(dt.Rows[i]["m01_name"].ToString(), dt.Rows[i]["m01_no"].ToString()));
                }
                catch { }
            }
        }

        this.ddl_chekuan.Items.Insert(0,new ListItem("請選擇", "0"));
        this.ddl_color.Items.Insert(0, new ListItem("請選擇", "0"));
        this.ddl_energy.Items.Insert(0, new ListItem("請選擇", "0"));
        this.ddl_factory.Items.Insert(0, new ListItem("請選擇", "0"));
        this.ddl_mark.Items.Insert(0, new ListItem("請選擇", "0"));
        this.ddl_platoon.Items.Insert(0, new ListItem("請選擇", "0"));
        this.ddl_source.Items.Insert(0, new ListItem("請選擇", "0"));
    }
    #endregion

    #region 畫面：修改
    private void ShowDataModify()
    {
        string aMSG = "";
        try
        {
            this.MultiView1.ActiveViewIndex = 1;
            ClearControlValue();
            ShowM01();
            
            DataTable dt = new DataTable();
            this.Navigator1.SubFunc = "修改";
            Entity.m02 tbData = new M02DAO().GetByNo(Convert.ToInt32(this.lab_no.Text));

            #region textbox
            this.txt_cc.Text = tbData.m02_cc;
            this.txt_code.Text = tbData.m02_code;
            this.txt_count.Text = tbData.m02_count.Value.ToString();
            this.txt_engine.Text = tbData.m02_engine;
            this.txt_memo.Text = tbData.m02_memo;
            this.txt_number.Text = tbData.m02_number;
            this.txt_type.Text = tbData.m02_type;
            this.txt_useyear.Text = tbData.m02_useyear.Value.ToString();
            this.txt_year.Text = tbData.m02_year.Value.ToString();
            #endregion

            #region 下拉式選單、單選選單
            try
            {
                this.ddl_chekuan.Items.FindByValue(tbData.m02_chekuan.Value.ToString()).Selected = true;
            }
            catch { }
            try
            {
                this.ddl_color.Items.FindByValue(tbData.m02_color.Value.ToString()).Selected = true;
            }
            catch { }
            try
            {
                this.ddl_energy.Items.FindByValue(tbData.m02_energy.Value.ToString()).Selected = true;
            }
            catch { }
            try
            {
                this.ddl_factory.Items.FindByValue(tbData.m02_factory.Value.ToString()).Selected = true;
            }
            catch { }
            try
            {
                this.ddl_mark.Items.FindByValue(tbData.m02_mark.Value.ToString()).Selected = true;
            }
            catch { }
            try
            {
                this.ddl_platoon.Items.FindByValue(tbData.m02_platoon.Value.ToString()).Selected = true;
            }
            catch { }
            try
            {
                this.ddl_source.Items.FindByValue(tbData.m02_source.Value.ToString()).Selected = true;
            }
            catch { }

            try
            {
                this.rbl_status.Items.FindByValue(tbData.m02_status).Selected = true;
            }
            catch { }
            #endregion

            #region 保管人
            this.DepartTreeTextBox1.Add(tbData.m02_peouid.Value.ToString());
            #endregion

            #region 車輛圖片
            if (tbData.m02_pictype != null && tbData.m02_pictype.Length > 0)
            {
                string src = "../../lib/ShowPic.aspx?tb=m02&picorder=1&pkno=" + this.lab_no.Text;
                this.div_pic1.InnerHtml = "<a href=\"" + src + "\" rel=\"lytebox\" title=\"車輛圖片\" OnClick=\"return false;\" OnLoad=\"return true;\"><img src=" + src + " width=\"60\" height=\"50\"  /></a>";
                this.lbtn_delpic1.Visible = true;
                this.Panel1.Visible = true;
            }
            else
            {
                this.div_pic1.InnerHtml = "無圖示";
                this.ImageUpload1.Visible = true;
            }
            #endregion

            #region 日期
            if (tbData.m02_buydate != null) this.cal_buydate._ADDate = tbData.m02_buydate.Value;
            if (tbData.m02_cdate != null) this.cal_cdate._ADDate = tbData.m02_cdate.Value;
            if (tbData.m02_change != null) this.cal_change._ADDate = tbData.m02_change.Value;
            if (tbData.m02_overdate != null) this.cal_overdate._ADDate = tbData.m02_overdate.Value;
            if (tbData.m02_sdate != null) this.cal_sdate._ADDate = tbData.m02_sdate.Value;
            if (tbData.m02_testdate != null) this.cal_testdate._ADDate = tbData.m02_testdate.Value;
            #endregion
        }
        catch (Exception ex)
        {
            aMSG = "功能名稱：" + this.Navigator1.SubFunc + "<br>錯誤訊息:" + ex.ToString();
            Response.Write(aMSG);
        }
    }
    #endregion

    #region 畫面：新增
    private void ShowDataInsert()
    {
        this.MultiView1.ActiveViewIndex = 1;
        ClearControlValue();
        ShowM01();

        this.Navigator1.SubFunc = "新增";
        this.rbl_status.Items[0].Selected = true;
        this.ImageUpload1.Visible = true;
    }
    #endregion

    #region 調整輸出格式
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = new M01DAO().GetNameByNo(Convert.ToInt32(e.Row.Cells[0].Text));
            e.Row.Cells[4].Text = new M01DAO().GetNameByNo(Convert.ToInt32(e.Row.Cells[4].Text));
            e.Row.Cells[3].Text = new PeopleDAO().GetPeopleNameByUid(Convert.ToInt32(e.Row.Cells[3].Text));
            if (!e.Row.Cells[5].Text.Trim().Equals("&nbsp;")) e.Row.Cells[5].Text = changeobj.ADDTtoROCDT(e.Row.Cells[5].Text);
            if (!e.Row.Cells[6].Text.Trim().Equals("&nbsp;")) e.Row.Cells[6].Text = changeobj.ADDTtoROCDT(e.Row.Cells[6].Text);
            if (!e.Row.Cells[7].Text.Trim().Equals("&nbsp;")) e.Row.Cells[7].Text = changeobj.ADDTtoROCDT(e.Row.Cells[7].Text);
        }
    }
    #endregion

    #region 呼叫新增畫面
    protected void btn_add_Click(object sender, EventArgs e)
    {
        this.lab_pageIndex.Text = this.GridView1.PageIndex.ToString();
        ShowDataInsert();
    }
    #endregion

    #region 修改、刪除
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("modify"))
        {
            #region 呼叫修改
            //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
            new OperatesObject().ExecuteOperates(301102, sobj.sessionUserID, 2, "查詢 車輛 編號:" + this.lab_no.Text);
            Session["301102_value"] = "modify," + this.GridView1.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString() + "," + this.GridView1.PageIndex.ToString();
            Response.Redirect("301102.aspx?count=" + new System.Random().Next(10000).ToString());
            #endregion
        }
        else if (e.CommandName.Equals("del"))
        {
            #region 執行刪除
            string pkno = this.GridView1.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString();
            this.lab_pageIndex.Text = this.GridView1.PageIndex.ToString();
            string updstr = "update m02 set m02_status='4',m02_createuid=" + sobj.sessionUserID + ",m02_createtime=getdate() where m02_no=" + pkno;
            dbo.ExecuteNonQuery(updstr);
            //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
            new OperatesObject().ExecuteOperates(301102, sobj.sessionUserID, 3, "刪除 車輛資料 編號:" + pkno);
            ShowDataList(); //呼叫列表
            #endregion
        }
    }
    #endregion

    #region 顯示錯誤訊息
    private void ShowMsg(string msg)
    {
        string script = "<script>window.alert('" + msg + "');</script>";
        this.ClientScript.RegisterStartupScript(this.GetType(), "MSG", script);
    }
    #endregion

    #region 檢查輸入值
    private bool CheckInputValue()
    {
        bool feedback = true;

        #region 車輛編號
        if (string.IsNullOrEmpty(this.txt_code.Text.Trim()))
        {
            ShowMsg("車輛編號 不可為空白");
            feedback = false;
        }
        else if (this.txt_code.Text.Trim().Length > 20)
        {
            ShowMsg("車輛編號 長度不可超過20個中文字");
            feedback = false;
        }
        #endregion
        #region 保管人
        if (this.DepartTreeTextBox1.Items.Count <= 0 || this.DepartTreeTextBox1.Items == null)
        {
            ShowMsg("請選擇 保管人");
            feedback = false;
        }
        #endregion
        #region 牌照種類
        if (this.ddl_platoon.SelectedValue.Equals("0"))
        {
            ShowMsg("請選擇 牌照種類");
            feedback = false;
        }
        #endregion
        #region 牌照號碼
        if (string.IsNullOrEmpty(this.txt_number.Text.Trim()))
        {
            ShowMsg("牌照號碼 不可為空白");
            feedback = false;
        }
        else if (this.txt_number.Text.Trim().Length > 20)
        {
            ShowMsg("牌照號碼 長度不可超過20個中文字");
            feedback = false;
        }
        #endregion
        #region 發照日期
        try
        {
            if (this.cal_sdate._AD.Length <= 0)
            {
                ShowMsg("發照日期 不可為空白");
                feedback = false;
            }
        }
        catch
        {
            ShowMsg("發照日期 格式錯誤");
            feedback = false;
        }
        #endregion
        #region 換照日期
        try
        {
            string tmp = this.cal_change._AD;
        }
        catch
        {
            ShowMsg("換照日期 格式錯誤");
            feedback = false;
        }
        #endregion
        #region 車別名稱、能源種類、顏色
        if (this.ddl_chekuan.SelectedValue.Equals("0"))
        {
            ShowMsg("請選擇 車別名稱");
            feedback = false;
        }

        if (this.ddl_energy.SelectedValue.Equals("0"))
        {
            ShowMsg("請選擇 能源種類");
            feedback = false;
        }

        if (this.ddl_color.SelectedValue.Equals("0"))
        {
            ShowMsg("請選擇 顏色");
            feedback = false;
        }
        #endregion
        #region 引擎號碼
        if (string.IsNullOrEmpty(this.txt_engine.Text.Trim()))
        {
            ShowMsg("引擎號碼 不可為空白");
            feedback = false;
        }
        else if (this.txt_engine.Text.Trim().Length > 30)
        {
            ShowMsg("引擎號碼 長度不可超過30個中文字");
            feedback = false;
        }
        #endregion
        #region 廠牌
        if (this.ddl_mark.SelectedValue.Equals("0"))
        {
            ShowMsg("請選擇 廠牌");
            feedback = false;
        }
        #endregion
        #region 廠牌
        if (this.txt_type.Text.Trim().Length > 30)
        {
            ShowMsg("型式 長度不可超過30個數字");
            feedback = false;
        }
        #endregion
        #region 年份
        if (string.IsNullOrEmpty(this.txt_year.Text.Trim()))
        {
            ShowMsg("年份 不可空白");
            feedback = false;
        }
        else if (this.txt_year.Text.Trim().Length > 10)
        {
            ShowMsg("年份 長度不可超過10個數字");
            feedback = false;
        }
        else
        {
            try
            {
                int tmp = Convert.ToInt32(this.txt_year.Text);
            }
            catch
            {
                ShowMsg("年份 需為整數");
                feedback = false;
            }
        }
        #endregion
        #region 排氣量
        if (string.IsNullOrEmpty(this.txt_cc.Text.Trim()))
        {
            ShowMsg("排氣量 不可空白");
            feedback = false;
        }
        else if (this.txt_cc.Text.Trim().Length > 10)
        {
            ShowMsg("排氣量 長度不可超過10個數字");
            feedback = false;
        }
        #endregion
        #region 使用年限
        if (string.IsNullOrEmpty(this.txt_useyear.Text.Trim()))
        {
            this.txt_useyear.Text = "0";
        }
        else
        {
            try
            {
                int tmp = Convert.ToInt32(this.txt_useyear.Text);
            }
            catch
            {
                ShowMsg("使用年限 需為數字");
                feedback = false;
            }
        }
        #endregion
        #region 座位
        if (string.IsNullOrEmpty(this.txt_count.Text.Trim()))
        {
            ShowMsg("座位 不可為空白");
            feedback = false;
        }
        else
        {
            try
            {
                int tmp = Convert.ToInt32(this.txt_count.Text);
            }
            catch
            {
                ShowMsg("座位 需為整數");
                feedback = false;
            }
        }
        #endregion
        #region 車輛來源、廠商
        if (this.ddl_source.SelectedValue.Equals("0"))
        {
            ShowMsg("請選擇 車輛來源");
            feedback = false;
        }

        if (this.ddl_factory.SelectedValue.Equals("0"))
        {
            ShowMsg("請選擇 廠商");
            feedback = false;
        }
        #endregion
        #region 購入日期、移入日期、檢驗日期、報廢日期
        try
        {
            string tmp = this.cal_buydate._AD;
        }
        catch
        {
            ShowMsg("購入日期 格式錯誤");
            feedback = false;
        }
        try
        {
            string tmp = this.cal_cdate._AD;
        }
        catch
        {
            ShowMsg("移入日期 格式錯誤");
            feedback = false;
        }
        try
        {
            string tmp = this.cal_testdate._AD;
        }
        catch
        {
            ShowMsg("檢驗日期 格式錯誤");
            feedback = false;
        }
        try
        {
            string tmp = this.cal_overdate._AD;
        }
        catch
        {
            ShowMsg("報廢日期 格式錯誤");
            feedback = false;
        }
        #endregion
        #region 車輛備註
        if (this.txt_memo.Text.Trim().Length > 100)
        {
            ShowMsg("車輛備註 長度不可超過100個中文字");
            feedback = false;
        }
        #endregion
        #region 車輛圖片
        this.ImageUpload1.UploadPic();
        if (this.ImageUpload1.HasFile)
        {
            if (!this.ImageUpload1.CheckFileType)
            {
                ShowMsg("車輛圖片 檔案類型錯誤");
                feedback = false;
            }
            if (!this.ImageUpload1.CheckFileSize)
            {
                ShowMsg("車輛圖片 檔案大小錯誤");
                feedback = false;
            }
        }
        #endregion
        return feedback;
    }
    #endregion

    #region 刪除檔案
    protected void lbtn_delpic1_Click(object sender, EventArgs e)
    {
        string aMSG = "";
        try
        {
            #region m02
            M02DAO tbDAO = new M02DAO();
            m02 newRow = tbDAO.GetByNo(Convert.ToInt32(this.lab_no.Text));
            newRow.m02_createtime = System.DateTime.Now;
            newRow.m02_createuid = Convert.ToInt32(sobj.sessionUserID);
            newRow.m02_pic = null;
            newRow.m02_pictype = "";
            tbDAO.Update();
            #endregion
            ShowDataModify(); //顯示修改畫面
        }
        catch (Exception ex)
        {
            aMSG = "功能名稱：" + this.Navigator1.SubFunc + "<br>錯誤訊息:" + ex.ToString();
            Response.Write(aMSG);
        }
    }
    #endregion

    #region 確定
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        string aMSG = "";
        try
        {
            if (CheckInputValue())
            {
                if (this.lab_no.Text.Trim().Length > 0)
                {
                    #region 修改
                    string sqlstr = "select m02_no from m02 where m02_code='" + this.txt_code.Text + "' and m02_status<>'4' and m02_no<>" + this.lab_no.Text;
                    DataTable dt = new DataTable();
                    dt = dbo.ExecuteQuery(sqlstr);
                    if (dt.Rows.Count > 0)
                    {
                        ShowMsg("此 車輛編號[" + this.txt_code.Text + "] 已存在");
                        return;
                    }
                    else
                    {
                        #region m02
                        M02DAO tbDAO = new M02DAO();
                        m02 newRow = tbDAO.GetByNo(Convert.ToInt32(this.lab_no.Text));
                        if (this.cal_buydate._AD.Length > 0)
                            newRow.m02_buydate = this.cal_buydate._ADDate;
                        else
                            newRow.m02_buydate = null;
                        newRow.m02_cc = this.txt_cc.Text;
                        if (this.cal_cdate._AD.Length > 0)
                            newRow.m02_cdate = this.cal_cdate._ADDate;
                        else
                            newRow.m02_cdate = null;
                        if (this.cal_change._AD.Length > 0)
                            newRow.m02_change = this.cal_change._ADDate;
                        else
                            newRow.m02_change = null;
                        newRow.m02_chekuan = Convert.ToInt32(this.ddl_chekuan.SelectedValue);
                        newRow.m02_code = this.txt_code.Text;
                        newRow.m02_color = Convert.ToInt32(this.ddl_color.SelectedValue);
                        newRow.m02_count = Convert.ToInt32(this.txt_count.Text);
                        newRow.m02_createtime = System.DateTime.Now;
                        newRow.m02_createuid = Convert.ToInt32(sobj.sessionUserID);
                        newRow.m02_energy = Convert.ToInt32(this.ddl_energy.SelectedValue);
                        newRow.m02_engine = this.txt_engine.Text;
                        newRow.m02_factory = Convert.ToInt32(this.ddl_factory.SelectedValue);
                        newRow.m02_mark = Convert.ToInt32(this.ddl_mark.SelectedValue);
                        newRow.m02_memo = this.txt_memo.Text;
                        newRow.m02_number = this.txt_number.Text;
                        if (this.cal_overdate._AD.Length > 0)
                            newRow.m02_overdate = this.cal_overdate._ADDate;
                        else
                            newRow.m02_overdate = null;
                        if (this.DepartTreeTextBox1.Value.Length > 0)
                            newRow.m02_peouid = Convert.ToInt32(this.DepartTreeTextBox1.Value);
                        else
                            newRow.m02_peouid = 0;
                        if (this.ImageUpload1.HasFile)
                        {
                            newRow.m02_pic = this.ImageUpload1.GetFileBytes;
                            newRow.m02_pictype = this.ImageUpload1.GetExtension;
                        }
                        newRow.m02_platoon = Convert.ToInt32(this.ddl_platoon.SelectedValue);
                        if (this.cal_sdate._AD.Length > 0)
                            newRow.m02_sdate = this.cal_sdate._ADDate;
                        else
                            newRow.m02_sdate = null;
                        newRow.m02_source = Convert.ToInt32(this.ddl_source.SelectedValue);
                        newRow.m02_status = this.rbl_status.SelectedValue;
                        if (this.cal_testdate._AD.Length > 0)
                            newRow.m02_testdate = this.cal_testdate._ADDate;
                        else
                            newRow.m02_testdate = null;
                        newRow.m02_type = this.txt_type.Text;
                        newRow.m02_useyear = Convert.ToInt32(this.txt_useyear.Text);
                        newRow.m02_year = Convert.ToInt32(this.txt_year.Text);
                        tbDAO.Update();
                        #endregion

                        //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
                        new OperatesObject().ExecuteOperates(301102, sobj.sessionUserID, 3, "編號：" + this.lab_no.Text + ",所在地：" + this.ddl_platoon.SelectedItem.Text + ",設備名稱：" + this.txt_code.Text.Trim() + "...等");
                    }
                    #endregion
                }
                else
                {
                    #region 新增
                    string sqlstr = "select m02_no from m02 where m02_code='" + this.txt_code.Text + "' and m02_status<>'4'";
                    DataTable dt = new DataTable();
                    dt = dbo.ExecuteQuery(sqlstr);
                    if (dt.Rows.Count > 0)
                    {
                        ShowMsg("此 車輛編號[" + this.txt_code.Text + "] 已存在");
                        return;
                    }
                    else
                    {
                        #region m02
                        M02DAO tbDAO = new M02DAO();
                        m02 newRow = new m02();
                        if (this.cal_buydate._AD.Length > 0)
                        {
                            newRow.m02_buydate = this.cal_buydate._ADDate;
                        }
                        newRow.m02_cc = this.txt_cc.Text;
                        if (this.cal_cdate._AD.Length > 0)
                        {
                            newRow.m02_cdate = this.cal_cdate._ADDate;
                        }
                        if (this.cal_change._AD.Length > 0)
                        {
                            newRow.m02_change = this.cal_change._ADDate;
                        }
                        newRow.m02_chekuan = Convert.ToInt32(this.ddl_chekuan.SelectedValue);
                        newRow.m02_code = this.txt_code.Text;
                        newRow.m02_color = Convert.ToInt32(this.ddl_color.SelectedValue);
                        newRow.m02_count = Convert.ToInt32(this.txt_count.Text);
                        newRow.m02_createtime = System.DateTime.Now;
                        newRow.m02_createuid = Convert.ToInt32(sobj.sessionUserID);
                        newRow.m02_energy = Convert.ToInt32(this.ddl_energy.SelectedValue);
                        newRow.m02_engine = this.txt_engine.Text;
                        newRow.m02_factory = Convert.ToInt32(this.ddl_factory.SelectedValue);
                        newRow.m02_mark = Convert.ToInt32(this.ddl_mark.SelectedValue);
                        newRow.m02_memo = this.txt_memo.Text;
                        newRow.m02_number = this.txt_number.Text;
                        if (this.cal_overdate._AD.Length > 0)
                        {
                            newRow.m02_overdate = this.cal_overdate._ADDate;
                        }
                        if (this.DepartTreeTextBox1.Value.Length > 0)
                            newRow.m02_peouid = Convert.ToInt32(this.DepartTreeTextBox1.Value);
                        else
                            newRow.m02_peouid = 0;
                        if (this.ImageUpload1.HasFile)
                        {
                            newRow.m02_pic = this.ImageUpload1.GetFileBytes;
                            newRow.m02_pictype = this.ImageUpload1.GetExtension;
                        }
                        newRow.m02_platoon = Convert.ToInt32(this.ddl_platoon.SelectedValue);
                        if (this.cal_sdate._AD.Length > 0)
                        {
                            newRow.m02_sdate = this.cal_sdate._ADDate;
                        }
                        newRow.m02_source = Convert.ToInt32(this.ddl_source.SelectedValue);
                        newRow.m02_status = this.rbl_status.SelectedValue;
                        if (this.cal_testdate._AD.Length > 0)
                        {
                            newRow.m02_testdate = this.cal_testdate._ADDate;
                        }
                        newRow.m02_type = this.txt_type.Text;
                        newRow.m02_useyear = Convert.ToInt32(this.txt_useyear.Text);
                        newRow.m02_year = Convert.ToInt32(this.txt_year.Text);
                        tbDAO.AddM02(newRow);
                        tbDAO.Update();
                        #endregion
                        int equ_no = newRow.m02_no;

                        //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
                        new OperatesObject().ExecuteOperates(301102, sobj.sessionUserID, 1, "車輛編號：" + this.txt_code.Text + ",牌照號碼：" + this.txt_number.Text.Trim() + "....等");
                    }
                    #endregion
                }
                //ShowDataList(); //呼叫列表

                Response.Write(PCalendarUtil.ShowMsg_URL("", "301102.aspx?count=" + new System.Random().Next(10000).ToString() + "&pageIndex=" + this.lab_pageIndex.Text));
            }
        }
        catch (Exception ex)
        {
            aMSG = "功能名稱：" + this.Navigator1.SubFunc + "<br>錯誤訊息:" + ex.ToString();
            Response.Write(aMSG);
        }
    }
    #endregion

    #region 取消
    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        ShowDataList(); //呼叫列表
    }
    #endregion
}