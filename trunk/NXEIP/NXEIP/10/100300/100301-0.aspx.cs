using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using NXEIP.DAO;
using Entity;

public partial class _10_100300_100301_0 : System.Web.UI.Page
{
    ChangeObject changeobj = new ChangeObject();
    SessionObject sobj = new SessionObject();
    DBObject dbo = new DBObject();
    CheckObject checkobj = new CheckObject();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
            new OperatesObject().ExecuteOperates(100301, sobj.sessionUserID, 2, "新增行事曆");

            #region 初始值--Panel
            this.Panel0.Visible = false;
            this.Panel1.Visible = false;
            this.Panel2.Visible = false;
            this.Panel3.Visible = false;
            this.Panel4.Visible = false;
            #endregion

            #region 初始值--日期人員編號
            if (Request["source"] != null) this.lab_source.Text = Request["source"];
            if (Request["today"] != null)
            {
                this.cl_sdate._ADDate = Convert.ToDateTime(changeobj.ROCDTtoADDT(Request["today"]));
                this.cl_edate._ADDate = Convert.ToDateTime(changeobj.ROCDTtoADDT(Request["today"]));
                this.cl_sdate1._ADDate = Convert.ToDateTime(changeobj.ROCDTtoADDT(Request["today"]));
                this.cl_edate1._ADDate = Convert.ToDateTime(changeobj.ROCDTtoADDT(Request["today"]));
                this.lab_today.Text = Request["today"];
                this.lab_depart.Text = Request["depart"];
            }
            bool isUpdate=false;

            if (Request["peo_uid"] != null)
            {
                this.lab_peo_uid.Text = Request["peo_uid"];
                this.lab_UserName.Text = new PeopleDAO().GetPeopleNameByUid(Convert.ToInt32(this.lab_peo_uid.Text));

                #region 檢查是否可新增或修改、或查看
                DataTable dt = new DataTable();
                string sqlstr = "SELECT c01_no from c01 where peo_uid="+sobj.sessionUserID+" and c02_peouid="+this.lab_peo_uid.Text;
                dt = dbo.ExecuteQuery(sqlstr);
                if (dt.Rows.Count > 0)
                {
                    isUpdate = true;
                }
                #endregion
            }
            else
            {
                this.lab_peo_uid.Text = sobj.sessionUserID;
                this.lab_UserName.Text = sobj.sessionUserName;
                isUpdate = true;
            }
            #endregion

            #region 初始值--時間、單據編號
            if (isUpdate)
            {
                if (Request["no"] != null)
                {
                    this.Navigator1.SubFunc = "修改";
                    #region 修改
                    this.lab_no.Text = Request["no"];
                    this.cb_update.Visible = false;
                    this.btn_del.Visible = true;
                    this.btn_del.Enabled = true;

                    Entity.c02 c02Data = new C02DAO().GetByC02No(Convert.ToInt32(this.lab_peo_uid.Text), Convert.ToInt32(this.lab_no.Text));
                    if (c02Data != null)
                    {
                        if (c02Data.c02_setuid == Convert.ToInt32(sobj.sessionUserID) || c02Data.peo_uid == Convert.ToInt32(sobj.sessionUserID))
                        {
                            this.cl_sdate._ADDate = c02Data.c02_sdate;
                            this.cl_edate._ADDate = c02Data.c02_edate;
                            try
                            {
                                this.ddl_stime.Items.FindByValue(c02Data.c02_sdate.ToString("HH:mm")).Selected = true;
                                this.ddl_etime.Items.FindByValue(c02Data.c02_edate.ToString("HH:mm")).Selected = true;
                            }
                            catch { }
                            this.txt_bgcolor.Text = c02Data.c02_bgcolor;
                            this.txt_title.Text = c02Data.c02_title;
                            this.txt_place.Text = c02Data.c02_place;
                            if (c02Data.c02_project != null) this.txt_project.Text = c02Data.c02_project;
                            if (c02Data.c02_result != null) this.txt_result.Text = c02Data.c02_result;

                            #region 週期
                            if (c02Data.c03_no != null)
                            {
                                Entity.c03 c03data = new C03DAO().GetByC03No(Convert.ToInt32(c02Data.c03_no));
                                if (c03data != null)
                                {
                                    this.Panel0.Visible = true;
                                    this.CB_c03.Checked = true;
                                    this.cb_update.Visible = true;
                                    this.cb_update.Checked = true;
                                    this.lab_c03_no.Text = c03data.c03_no.ToString();
                                    this.rbl_cycle.Items.FindByValue(c03data.c03_cycle).Selected = true;
                                    #region 循環
                                    if (c03data.c03_cycle.Equals("1"))
                                    {
                                        #region 日循環(type:0,rule:n天)
                                        this.Panel1.Visible = true;
                                        this.txt_1.Text = c03data.c03_rule;
                                        #endregion
                                    }
                                    else if (c03data.c03_cycle.Equals("2"))
                                    {
                                        #region 週循環(type:週數 rule:星期)
                                        this.Panel2.Visible = true;
                                        this.txt_21.Text = c03data.c03_type;
                                        for (int i = 0; i < this.cbl_22.Items.Count; i++)
                                        {
                                            if (c03data.c03_rule.IndexOf(this.cbl_22.Items[i].Value) > -1)
                                            {
                                                this.cbl_22.Items[i].Selected = true;
                                            }
                                        }
                                        #endregion
                                    }
                                    else if (c03data.c03_cycle.Equals("3"))
                                    {
                                        #region 月循環(type:類型 rule:(1)月,日(2)月,第n個週,週幾)
                                        this.Panel3.Visible = true;
                                        if (c03data.c03_type.Equals("1"))
                                        {
                                            this.rb_31.Checked = true;
                                            string[] ruleArray = c03data.c03_rule.Split(',');
                                            if (ruleArray.Length > 0) this.txt_311.Text = ruleArray[0].ToString();
                                            if (ruleArray.Length > 1) this.txt_312.Text = ruleArray[1].ToString();
                                        }
                                        else
                                        {
                                            this.rb_32.Checked = true;
                                            string[] ruleArray = c03data.c03_rule.Split(',');
                                            if (ruleArray.Length > 0) this.txt_321.Text = ruleArray[0].ToString();
                                            if (ruleArray.Length > 1) this.ddl_322.Items.FindByValue(ruleArray[1].ToString()).Selected = true;
                                            if (ruleArray.Length > 2) this.ddl_323.Items.FindByValue(ruleArray[2].ToString()).Selected = true;
                                        }
                                        #endregion
                                    }
                                    else
                                    {
                                        #region 年循環(typ:類型 rule:(1)月,日(2)月,第n個週,週幾)
                                        this.Panel4.Visible = true;
                                        if (c03data.c03_type.Equals("1"))
                                        {
                                            this.rb_41.Checked = true;
                                            string[] ruleArray = c03data.c03_rule.Split(',');
                                            if (ruleArray.Length > 0) this.ddl_411.Items.FindByValue(ruleArray[0].ToString()).Selected = true;
                                            if (ruleArray.Length > 1) this.txt_412.Text = ruleArray[1];

                                        }
                                        else
                                        {
                                            this.rb_42.Checked = true;
                                            string[] ruleArray = c03data.c03_rule.Split(',');
                                            if (ruleArray.Length > 0) this.ddl_421.Items.FindByValue(ruleArray[0].ToString()).Selected = true;
                                            if (ruleArray.Length > 1) this.ddl_422.Items.FindByValue(ruleArray[1].ToString()).Selected = true;
                                            if (ruleArray.Length > 2) this.ddl_423.Items.FindByValue(ruleArray[2].ToString()).Selected = true;
                                        }
                                        #endregion
                                    }
                                    #endregion
                                    #region 範圍
                                    this.cl_sdate1._ADDate = Convert.ToDateTime(c03data.c03_sdate);
                                    if (c03data.c03_ctype.Equals("1"))
                                    {
                                        this.rb_51.Checked = true;
                                        this.txt_qty.Text = c03data.c03_crule;
                                    }
                                    else
                                    {
                                        this.rb_52.Checked = true;
                                        this.cl_edate1._ADDate = Convert.ToDateTime(changeobj.ROCDTtoADDT(c03data.c03_crule));
                                    }
                                    #endregion

                                }
                            }
                            #endregion
                        }
                    }
                    #endregion
                }
                else
                {
                    this.Navigator1.SubFunc = "新增";
                    #region 新增
                    this.cb_update.Visible = false;
                    this.btn_del.Visible = false;
                    this.btn_del.Enabled = false;
                    this.txt_bgcolor.Text = "#FFFFFF";
                    if (Request["stime"] != null)
                    {
                        try
                        {
                            this.ddl_stime.Items.FindByValue(Request["stime"]).Selected = true;
                            this.ddl_etime.Items.FindByValue(Request["stime"]).Selected = true;
                        }
                        catch { }
                    }
                    #endregion
                }
            }
            else
            {
                this.btn_del.Visible = false;
                this.btn_del.Enabled = false;
                this.btn_submit.Visible = false;
                this.btn_submit.Enabled = false;
            }
            #endregion
        }
    }
    #region 取消
    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "closeThickBox", "self.parent.location.reload(true);self.parent.tb_remove();", true);
    }
    #endregion

    #region 確定
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        string aMSG = "";   //記錄錯誤訊息
        try
        {
            DateTime sdate = new DateTime();
            DateTime edate = new DateTime();
            DateTime c03_sdate = new DateTime();
            string c03_type = "";//規則別或週數
            string c03_rule = "";//循環規則
            string c03_ctype = "";//規則別
            string c03_crule = "";//循環範圍規則
            #region 檢查輸入值--基本(日期、時間、標題、地點)
            try
            {
                sdate = Convert.ToDateTime(this.cl_sdate._ADDate.ToString("yyyy/MM/dd") + " " + this.ddl_stime.SelectedValue + ":00");
            }
            catch
            {
                ShowMSG("請選擇開始時間");
                return;
            }
            try
            {
                edate = Convert.ToDateTime(this.cl_edate._ADDate.ToString("yyyy/MM/dd") + " " + this.ddl_etime.SelectedValue + ":00");
            }
            catch
            {
                ShowMSG("請選擇結束時間");
                return;
            }
            

            if (sdate > edate)
            {
                ShowMSG("結束時間不得小於開始時間");
                return;
            }
            if (this.txt_bgcolor.Text.Trim().Length <= 0)
            {
                this.txt_bgcolor.Text = "#FFFFFF";
            }

            if (this.txt_title.Text.Trim().Length <= 0)
            {
                ShowMSG("標題 不可為空白");
                return;
            }
            else if (this.txt_title.Text.Length > 100)
            {
                ShowMSG("標題 長度不可超過100個中文字");
                return;
            }
            //if (this.txt_place.Text.Trim().Length <= 0)
            //{
            //    ShowMSG("地點 不可為空白");
            //    return;
            //}
            if (this.txt_place.Text.Length > 100)
            {
                ShowMSG("地點 長度不可超過100個中文字");
                return;
            }
            #endregion

            #region 檢查輸入值--週期性行程及取得值
            if (this.Panel0.Visible)
            {
                if (this.rbl_cycle.SelectedIndex < 0)
                {
                    ShowMSG("請選擇 週期");
                    return;
                }
                else if (this.rbl_cycle.SelectedValue.Equals("1"))
                {
                    #region 日循環(type:0,rule:n天)
                    if (this.txt_1.Text.Trim().Length <= 0)
                    {
                        ShowMSG("天數不可空白");
                        return;
                    }
                    else if (!checkobj.IsInt(this.txt_1.Text.Trim()))
                    {
                        ShowMSG("天數 需為數字");
                        return;
                    }
                    else
                    {
                        c03_type = "0";
                        c03_rule = this.txt_1.Text;
                    }
                    #endregion
                }
                else if (this.rbl_cycle.SelectedValue.Equals("2"))
                {
                    #region 週循環(type:週數 rule:星期)
                    if (this.txt_21.Text.Trim().Length <= 0)
                    {
                        ShowMSG("週數不可空白");
                        return;
                    }
                    else if (!checkobj.IsInt(this.txt_21.Text.Trim()))
                    {
                        ShowMSG("週數 需為數字");
                        return;
                    }
                    c03_type = this.txt_21.Text; //週數
                    for (int i = 0; i < this.cbl_22.Items.Count; i++)
                    {
                        if (this.cbl_22.Items[i].Selected)
                        {
                            c03_rule += this.cbl_22.Items[i].Value;
                        }
                    }
                    if (c03_rule.Length <= 0)
                    {
                        ShowMSG("請選擇 星期");
                        return;
                    }
                    #endregion
                }
                else if (this.rbl_cycle.SelectedValue.Equals("3"))
                {
                    #region 月循環(type:類型 rule:(1)月,日(2)月,第n個週,週幾)
                    if (this.rb_31.Checked)
                    {
                        c03_type = "1";
                        if (this.txt_311.Text.Trim().Length <= 0)
                        {
                            ShowMSG("月 不可空白");
                            return;
                        }
                        else if (!checkobj.IsInt(this.txt_311.Text.Trim()))
                        {
                            ShowMSG("月 需為數字");
                            return;
                        }
                        if (this.txt_312.Text.Trim().Length <= 0)
                        {
                            ShowMSG("日 不可空白");
                            return;
                        }
                        else if (!checkobj.IsInt(this.txt_312.Text.Trim()))
                        {
                            ShowMSG("日 需為數字");
                            return;
                        }
                        c03_rule = this.txt_311.Text + "," + this.txt_312.Text;
                    }
                    else if (this.rb_32.Checked)
                    {
                        c03_type = "2";
                        if (this.txt_321.Text.Trim().Length <= 0)
                        {
                            ShowMSG("月 不可空白");
                            return;
                        }
                        else if (!checkobj.IsInt(this.txt_321.Text.Trim()))
                        {
                            ShowMSG("月 需為數字");
                            return;
                        }
                        c03_rule = this.txt_321.Text + "," + this.ddl_322.SelectedValue + "," + this.ddl_323.SelectedValue;
                    }
                    else
                    {
                        ShowMSG("請選擇 月循環類型");
                        return;
                    }
                    #endregion
                }
                else
                {
                    #region 年循環(typ:類型 rule:(1)月,日(2)月,第n個週,週幾)
                    if (this.rb_41.Checked)
                    {
                        if (this.txt_412.Text.Trim().Length <= 0)
                        {
                            ShowMSG("日 不可空白");
                            return;
                        }
                        else if (!checkobj.IsInt(this.txt_412.Text.Trim()))
                        {
                            ShowMSG("日 需為數字");
                            return;
                        }
                        else
                        {
                            try
                            {
                                DateTime tmp = Convert.ToDateTime(System.DateTime.Now.Year + "/" + this.ddl_411.SelectedValue + "/" + this.txt_412.Text);
                            }
                            catch
                            {
                                ShowMSG(this.ddl_411.SelectedItem.Text + this.txt_412.Text + "日 不存在");
                                return;
                            }
                        }
                        c03_type = "1";
                        c03_rule = this.ddl_411.SelectedValue + "," + this.txt_412.Text;

                    }
                    else if (this.rb_42.Checked)
                    {
                        c03_type = "2";
                        c03_rule = this.ddl_421.SelectedValue + "," + this.ddl_422.SelectedValue + "," + this.ddl_423.SelectedValue;
                    }
                    else
                    {
                        ShowMSG("請選擇 年循環類型");
                        return;
                    }
                    #endregion
                }
            }
            #endregion

            #region 檢查輸入值--循環範圍及取得值
            if (this.Panel0.Visible)
            {
                if(cl_sdate1._ADDate==null)
                {
                    ShowMSG("請檢查 循環範圍-開始日期 日期格式");
                    return;
                }
                c03_sdate = this.cl_sdate1._ADDate;

                if (this.rb_51.Checked)
                {
                    c03_ctype = "1";
                    c03_crule = this.txt_qty.Text;
                }
                else if (this.rb_52.Checked)
                {
                    c03_ctype = "2";
                    try
                    {
                        c03_crule = changeobj.ADDTtoROCDT(this.cl_edate1._ADDate.ToString("yyyy-MM-dd"));
                    }
                    catch
                    {
                        ShowMSG("請選擇 截止日期");
                        return;
                    }
                }
                else
                {
                    ShowMSG("請選擇 循環範圍");
                    return;
                }
            }
            #endregion

            if (this.Panel0.Visible)
            {
                #region 週期性行程
                int c03_no;
                if (this.lab_c03_no.Text.Trim().Length > 0)
                {
                    #region 修改
                    c03_no = Convert.ToInt32(this.lab_c03_no.Text);
                    if (cb_update.Checked)
                    {
                        C03DAO c03DAO1 = new C03DAO();
                        c03 newRow = c03DAO1.GetByC03No(c03_no);
                        newRow.c03_type = c03_type;
                        newRow.c03_sdate = c03_sdate;
                        newRow.c03_rule = c03_rule;
                        newRow.c03_cycle = this.rbl_cycle.SelectedValue;
                        newRow.c03_ctype = c03_ctype;
                        newRow.c03_crule = c03_crule;
                        c03DAO1.Update();
                    }
                    #endregion

                    //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
                    new OperatesObject().ExecuteOperates(100301, sobj.sessionUserID, 3, "計畫--週期行程修改 peo_uid" + this.lab_peo_uid.Text + ",c03_no=" + c03_no);
                }
                else
                {
                    #region 新增
                    C03DAO C03DAO1 = new C03DAO();
                    c03 newRow = new c03();
                    newRow.c03_crule = c03_crule;
                    newRow.c03_ctype = c03_ctype;
                    newRow.c03_cycle = this.rbl_cycle.SelectedValue;
                    //newRow.c03_no=0;
                    newRow.c03_rule = c03_rule;
                    newRow.c03_sdate = c03_sdate;
                    newRow.c03_type = c03_type;
                    C03DAO1.AddC03(newRow);
                    C03DAO1.Update();

                    c03_no = newRow.c03_no;
                    #endregion

                    //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
                    new OperatesObject().ExecuteOperates(100301, sobj.sessionUserID, 1, "計畫--週期行程新增 peo_uid" + this.lab_peo_uid.Text + ",c03_no=" + c03_no);
                }

                int recordcount = 0;
                if (this.lab_c03_no.Text.Trim().Length > 0)
                {
                    #region 先刪除舊的行程
                    string delStr = "";
                    delStr = "delete c02 where peo_uid=" + this.lab_peo_uid.Text + " and c02_no=" + this.lab_no.Text;
                    dbo.ExecuteNonQuery(delStr);

                    if (this.cb_update.Checked)
                    {
                        delStr = "delete c02 where c03_no=" + this.lab_c03_no.Text;
                        dbo.ExecuteNonQuery(delStr);
                    }
                    #endregion
                }

                if (this.rbl_cycle.SelectedValue.Equals("1"))
                    recordcount = InsertCycleDay(c03_no);
                else if (this.rbl_cycle.SelectedValue.Equals("2"))
                    recordcount = InsertCycleWeek(c03_no);
                else if (this.rbl_cycle.SelectedValue.Equals("3"))
                    recordcount = InsertCycleMonth(c03_no);
                else
                    recordcount = InsertCycleYear(c03_no);
                #endregion
            }
            else
            {
                #region 單一行程
                if (this.lab_no.Text.Trim().Length > 0)
                {
                    #region 修改
                    C02DAO c02DAO1 = new C02DAO();
                    c02 newRow = c02DAO1.GetByC02No(Convert.ToInt32(this.lab_peo_uid.Text),Convert.ToInt32(this.lab_no.Text));
                    if (this.txt_bgcolor.Text.ElementAt(0).Equals("#"))
                        newRow.c02_bgcolor = this.txt_bgcolor.Text;
                    else
                        newRow.c02_bgcolor = "#" + this.txt_bgcolor.Text;
                    newRow.c02_createtime = System.DateTime.Now;
                    newRow.c02_createuid = Convert.ToInt32(sobj.sessionUserID);
                    newRow.c02_edate = edate;
                    newRow.c02_place = this.txt_place.Text;
                    newRow.c02_project = this.txt_project.Text;
                    newRow.c02_result = this.txt_result.Text;
                    newRow.c02_sdate = sdate;
                    newRow.c02_setuid = Convert.ToInt32(sobj.sessionUserID);
                    newRow.c02_title = this.txt_title.Text;
                    newRow.c02_appointmen = "2";
                    newRow.c02_check = "1";
                    c02DAO1.Update();
                    #endregion

                    //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
                    new OperatesObject().ExecuteOperates(100301, sobj.sessionUserID, 3, "計畫--修改 peo_uid" + this.lab_peo_uid.Text + ",c02_no=" + this.lab_no.Text);
                }
                else
                {
                    int newpk = new C02DAO().GetMaxNoByPeoUid(Convert.ToInt32(this.lab_peo_uid.Text)) + 1;

                    InsertDB_C02(newpk, sdate, edate, 0); //新增

                    //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
                    new OperatesObject().ExecuteOperates(100301, sobj.sessionUserID, 1, "計畫--新增 peo_uid" + this.lab_peo_uid.Text + ",c02_no=" + newpk);
                }
                #endregion
            }

            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "closeThickBox", "self.parent.location.reload(true);self.parent.tb_remove();", true);
        }
        catch (Exception ex)
        {
            aMSG = "功能名稱:行事曆--確定新增或修改<br>錯誤訊息:" + ex.ToString();
            Response.Write(aMSG);
        }        
    }
    #endregion

    #region 勾選週期時
    protected void CB_c03_CheckedChanged(object sender, EventArgs e)
    {
        if (this.CB_c03.Checked)
            this.Panel0.Visible = true;
        else
            this.Panel0.Visible = false;
    }
    #endregion

    #region 週期類別
    protected void rbl_cycle_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Panel1.Visible = false;
        this.Panel2.Visible = false;
        this.Panel3.Visible = false;
        this.Panel4.Visible = false;

        #region 如果開始日期沒填，則預設今天
        if (this.cl_sdate._ADDate ==null)
        {
            this.cl_sdate._ADDate = System.DateTime.Today;
        }
        #endregion
        DateTime cdate = this.cl_sdate._ADDate;

        #region 週期預設值
        if (this.rbl_cycle.SelectedValue.Equals("1"))
        {
            this.Panel1.Visible = true;
        }
        else if (this.rbl_cycle.SelectedValue.Equals("2"))
        {
            this.Panel2.Visible = true;
            if (this.lab_c03_no.Text.Trim().Length <= 0)
            {
                //當新增或是原沒週期時，抓開始日期的星期當預設值\
                try
                {
                    this.cbl_22.Items.FindByValue(changeobj.ChangeWeek(cdate).ToString()).Selected = true;
                }
                catch { }
            }
        }
        else if (this.rbl_cycle.SelectedValue.Equals("3"))
        {
            this.Panel3.Visible = true;
            if (this.lab_c03_no.Text.Trim().Length <= 0)
            {
                this.txt_312.Text = cdate.Day.ToString();
                int weeks = PCalendarUtil.GetWeeksOfMonth(cdate);
                if (weeks > 5) weeks = 5;
                this.ddl_322.SelectedItem.Selected = false;
                this.ddl_322.Items.FindByValue(weeks.ToString()).Selected = true;
                this.ddl_323.SelectedItem.Selected = false;
                try
                {
                    this.ddl_323.Items.FindByValue(changeobj.ChangeWeek(cdate).ToString()).Selected = true;
                }
                catch { }
            }
        }
        else
        {
            this.Panel4.Visible = true;
            if (this.lab_c03_no.Text.Trim().Length <= 0)
            {
                this.ddl_411.SelectedItem.Selected = false;
                this.ddl_411.Items.FindByValue(cdate.Month.ToString("0#")).Selected = true;
                this.txt_412.Text = cdate.Day.ToString();
                this.ddl_421.SelectedItem.Selected = false;
                this.ddl_421.Items.FindByValue(cdate.Month.ToString("0#")).Selected = true;
                int weeks = PCalendarUtil.GetWeeksOfMonth(cdate);
                if (weeks > 5) weeks = 5;
                this.ddl_422.SelectedItem.Selected = false;
                this.ddl_422.Items.FindByValue(weeks.ToString()).Selected = true;
                this.ddl_423.SelectedItem.Selected = false;
                this.ddl_423.Items.FindByValue(changeobj.ChangeWeek(cdate).ToString()).Selected = true;
            }
        }
        #endregion

        #region 循環範圍
        if (this.lab_c03_no.Text.Trim().Length <= 0)
        {
            this.txt_qty.Text = "10";
            this.cl_edate._ADDate = this.cl_edate._ADDate;
        }
        #endregion
    }
    #endregion

    #region 新增資料庫
    private void InsertDB_C02(int c02_no, DateTime sdate, DateTime edate, int c03_no)
    {
        string aMSG = "";   //記錄錯誤訊息
        try
        {
            C02DAO c02DAO1=new C02DAO();
            c02 newRow=new c02();
            newRow.peo_uid=Convert.ToInt32(this.lab_peo_uid.Text);
            if(this.txt_bgcolor.Text.ElementAt(0).Equals("#"))
                newRow.c02_bgcolor = this.txt_bgcolor.Text;
            else
                newRow.c02_bgcolor = "#"+this.txt_bgcolor.Text;
            newRow.c02_createtime=System.DateTime.Now;
            newRow.c02_createuid = Convert.ToInt32(sobj.sessionUserID);
            newRow.c02_edate = edate;
            newRow.c02_no = c02_no;
            newRow.c02_place = this.txt_place.Text;
            newRow.c02_project = this.txt_project.Text;
            newRow.c02_result = this.txt_result.Text;
            newRow.c02_sdate = sdate;
            newRow.c02_setuid = Convert.ToInt32(sobj.sessionUserID);
            newRow.c02_title = this.txt_title.Text;
            newRow.c02_appointmen = "2";
            newRow.c02_check = "1";
            if (c03_no > 0)
            {
                newRow.c03_no = c03_no;
            }
            c02DAO1.AddC02(newRow);
            c02DAO1.Update();
        }
        catch (Exception ex)
        {
            aMSG = "功能名稱:行事曆-新增資料庫C02<br>錯誤訊息:" + ex.ToString();
            Response.Write(aMSG);
        }
    }
    #endregion

    #region 日循環
    private int InsertCycleDay(int c03_no)
    {
        int newpk = new C02DAO().GetMaxNoByPeoUid(Convert.ToInt32(this.lab_peo_uid.Text)) + 1;
        int count = 0;

        DateTime sdate1 = new DateTime();
        DateTime edate1 = new DateTime();
        DateTime c03_sdate = new DateTime();
        c03_sdate = Convert.ToDateTime(this.cl_sdate1._ADDate);

        int adddays = Convert.ToInt32(this.txt_1.Text);
        int addays1 = PCalendarUtil.DateDiff(Convert.ToDateTime(this.cl_sdate._ADDate), Convert.ToDateTime(this.cl_edate._ADDate));

        if (this.rb_51.Checked)
        {
            #region 次數循環
            for (int i = 0; i < Convert.ToInt32(this.txt_qty.Text); i++)
            {
                sdate1 = Convert.ToDateTime(c03_sdate.AddDays(adddays * i).ToString("yyyy/MM/dd") + " " + this.ddl_stime.SelectedValue + ":00");
                edate1 = Convert.ToDateTime(c03_sdate.AddDays(adddays * i + addays1).ToString("yyyy/MM/dd") + " " + this.ddl_etime.SelectedValue + ":00");

                InsertDB_C02(newpk + count, sdate1, edate1, c03_no);
                count++;
            }
            #endregion
        }
        else
        {
            DateTime enddate = Convert.ToDateTime(this.cl_edate1._ADDate);
            #region 截止日期
            DateTime tmpdate = c03_sdate;
            while (tmpdate <= enddate)
            {
                sdate1 = Convert.ToDateTime(c03_sdate.AddDays(adddays * count).ToString("yyyy/MM/dd") + " " + this.ddl_stime.SelectedValue + ":00");
                edate1 = Convert.ToDateTime(c03_sdate.AddDays(adddays * count + addays1).ToString("yyyy/MM/dd") + " " + this.ddl_etime.SelectedValue + ":00");

                InsertDB_C02(newpk + count, sdate1, edate1, c03_no);
                count++;

                tmpdate = c03_sdate.AddDays(adddays * count);
            }
            #endregion
        }
        return count;
    }
    #endregion

    #region 週循環
    private int InsertCycleWeek(int c03_no)
    {
        int newpk = new C02DAO().GetMaxNoByPeoUid(Convert.ToInt32(this.lab_peo_uid.Text)) + 1;
        int count = 0;
        DateTime sdate1 = new DateTime();
        DateTime edate1 = new DateTime();
        DateTime c03_sdate = new DateTime();
        c03_sdate = Convert.ToDateTime(this.cl_sdate1._ADDate);
        int addays1 = PCalendarUtil.DateDiff(Convert.ToDateTime(this.cl_sdate._ADDate), Convert.ToDateTime(this.cl_edate._ADDate));

        if (this.rb_51.Checked)
        {
            DateTime tmpdate = c03_sdate.AddDays(-(int)changeobj.ChangeWeek(c03_sdate));
            #region 次數循環
            for (int i = 0; i < Convert.ToInt32(this.txt_qty.Text); i++)
            {
                for (int j = 0; j < this.cbl_22.Items.Count; j++)
                {
                    //0日 1:一 2：二 3:三 4:四 5:五 6:六)
                    if (this.cbl_22.Items[j].Selected)
                    {
                        sdate1 = Convert.ToDateTime(tmpdate.AddDays(j).ToString("yyyy/MM/dd") + " " + this.ddl_stime.SelectedValue + ":00");
                        edate1 = Convert.ToDateTime(tmpdate.AddDays(j + addays1).ToString("yyyy/MM/dd") + " " + this.ddl_etime.SelectedValue + ":00");

                        if (sdate1 >= c03_sdate)
                        {
                            InsertDB_C02(newpk + count, sdate1, edate1, c03_no);
                            count++;
                        }
                    }
                }
                tmpdate = tmpdate.AddDays(7 * Convert.ToInt32(this.txt_21.Text));
            }
            #endregion
        }
        else
        {
            DateTime enddate = Convert.ToDateTime(this.cl_edate1._ADDate);
            #region 截止日期
            DateTime tmpdate = c03_sdate.AddDays(-(int)changeobj.ChangeWeek(c03_sdate));//日:0 1~6
            while (tmpdate <= enddate)
            {
                for (int j = 0; j < this.cbl_22.Items.Count; j++)
                {
                    //0日 1:一 2：二 3:三 4:四 5:五 6:六)
                    if (this.cbl_22.Items[j].Selected)
                    {
                        sdate1 = Convert.ToDateTime(tmpdate.AddDays(j).ToString("yyyy/MM/dd") + " " + this.ddl_stime.SelectedValue + ":00");
                        edate1 = Convert.ToDateTime(tmpdate.AddDays(j + addays1).ToString("yyyy/MM/dd") + " " + this.ddl_etime.SelectedValue + ":00");

                        if (sdate1 >= c03_sdate && sdate1 <= enddate)
                        {
                            InsertDB_C02(newpk + count, sdate1, edate1, c03_no);
                            count++;
                        }
                    }
                }
                tmpdate = tmpdate.AddDays(7 * Convert.ToInt32(this.txt_21.Text));
            }
            #endregion
        }

        return count;
    }
    #endregion

    #region 月循環
    private int InsertCycleMonth(int c03_no)
    {
        int newpk = new C02DAO().GetMaxNoByPeoUid(Convert.ToInt32(this.lab_peo_uid.Text)) + 1;
        int count = 0;
        DateTime sdate1 = new DateTime();
        DateTime edate1 = new DateTime();
        DateTime c03_sdate = new DateTime();
        c03_sdate = Convert.ToDateTime(this.cl_sdate1._ADDate);
        int addays1 = PCalendarUtil.DateDiff(Convert.ToDateTime(this.cl_sdate._ADDate), Convert.ToDateTime(this.cl_edate._ADDate));

        if (this.rb_51.Checked)
        {
            #region 次數循環
            if (this.rb_31.Checked)
            {
                #region 每n個月，日期循環
                DateTime tmpdate = Convert.ToDateTime(c03_sdate.ToString("yyyy/MM/01"));
                int months = Convert.ToInt32(this.txt_311.Text);
                int days = Convert.ToInt32(this.txt_312.Text) - 1;

                for (int i = 0; i < Convert.ToInt32(this.txt_qty.Text); i++)
                {
                    sdate1 = Convert.ToDateTime(tmpdate.AddMonths(months * i).AddDays(days).ToString("yyyy/MM/dd") + " " + this.ddl_stime.SelectedValue + ":00");
                    edate1 = Convert.ToDateTime(tmpdate.AddMonths(months * i).AddDays(days + addays1).ToString("yyyy/MM/dd") + " " + this.ddl_etime.SelectedValue + ":00");

                    if (sdate1 >= c03_sdate)
                    {
                        if (sdate1.Month == tmpdate.AddMonths(months * i).Month)
                        {
                            InsertDB_C02(newpk + count, sdate1, edate1, c03_no);
                            count++;
                        }
                        else
                        {
                            sdate1 = Convert.ToDateTime(sdate1.ToString("yyyy/MM/01 HH:mm:ss")).AddDays(-1);
                            edate1 = Convert.ToDateTime(edate1.ToString("yyyy/MM/01 HH:mm:ss")).AddDays(-1);
                            InsertDB_C02(newpk + count, sdate1, edate1, c03_no);
                            count++;
                        }
                    }
                }
                #endregion
            }
            else
            {
                #region 每n個月，第n週，星期幾
                DateTime tmpdate = Convert.ToDateTime(c03_sdate.ToString("yyyy/MM/01"));
                int months = Convert.ToInt32(this.txt_321.Text);
                int weeks1 = Convert.ToInt32(this.ddl_322.SelectedValue);
                int weeks2 = Convert.ToInt32(this.ddl_323.SelectedValue);

                for (int i = 0; i < Convert.ToInt32(this.txt_qty.Text); i++)
                {
                    DateTime tmpdate1 = tmpdate.AddMonths(months * i);

                    if (weeks2 > changeobj.ChangeWeek(tmpdate1))
                    {
                        tmpdate1 = tmpdate1.AddDays(-(int)changeobj.ChangeWeek(tmpdate1)).AddDays(weeks2);
                    }
                    else if (weeks2 < changeobj.ChangeWeek(tmpdate1))
                    {
                        tmpdate1 = tmpdate1.AddDays(7).AddDays(-(int)changeobj.ChangeWeek(tmpdate1));
                        tmpdate1 = tmpdate1.AddDays(weeks2);
                    }

                    if (weeks1 > 4) weeks1 = PCalendarUtil.GetWeeksOfMonth(tmpdate1.AddMonths(1).AddDays(-1));
                    tmpdate1 = tmpdate1.AddDays((weeks1 - 1) * 7);

                    sdate1 = Convert.ToDateTime(tmpdate1.AddDays(0).ToString("yyyy/MM/dd") + " " + this.ddl_stime.SelectedValue + ":00");
                    edate1 = Convert.ToDateTime(tmpdate1.AddDays(addays1).ToString("yyyy/MM/dd") + " " + this.ddl_etime.SelectedValue + ":00");
                    if (sdate1 >= c03_sdate)
                    {
                        if (sdate1.Month == tmpdate.AddMonths(months * i).Month)
                        {
                            InsertDB_C02(newpk + count, sdate1, edate1, c03_no);
                            count++;
                        }
                    }
                }
                #endregion
            }
            #endregion
        }
        else
        {
            DateTime enddate = Convert.ToDateTime(this.cl_edate1._ADDate);
            #region 截止日期
            if (this.rb_31.Checked)
            {
                #region 每n個月日期循環
                DateTime tmpdate = Convert.ToDateTime(c03_sdate.ToString("yyyy/MM/01"));
                int months = Convert.ToInt32(this.txt_311.Text);
                int days = Convert.ToInt32(this.txt_312.Text) - 1;

                while (tmpdate <= enddate)
                {
                    sdate1 = Convert.ToDateTime(tmpdate.AddDays(days).ToString("yyyy/MM/dd") + " " + this.ddl_stime.SelectedValue + ":00");
                    edate1 = Convert.ToDateTime(tmpdate.AddDays(days + addays1).ToString("yyyy/MM/dd") + " " + this.ddl_etime.SelectedValue + ":00");

                    if (sdate1 >= c03_sdate && sdate1 <= enddate)
                    {
                        if (sdate1.Month == tmpdate.Month)
                        {
                            InsertDB_C02(newpk + count, sdate1, edate1, c03_no);
                            count++;
                        }
                        else
                        {
                            sdate1 = Convert.ToDateTime(sdate1.ToString("yyyy/MM/01 HH:mm:ss")).AddDays(-1);
                            edate1 = Convert.ToDateTime(edate1.ToString("yyyy/MM/01 HH:mm:ss")).AddDays(-1);
                            InsertDB_C02(newpk + count, sdate1, edate1, c03_no);
                            count++;
                        }
                    }
                    tmpdate = tmpdate.AddMonths(months);
                }
                #endregion
            }
            else
            {
                #region 每n個月，第n週，星期幾
                DateTime tmpdate = Convert.ToDateTime(c03_sdate.ToString("yyyy/MM/01"));
                int months = Convert.ToInt32(this.txt_321.Text);
                int weeks1 = Convert.ToInt32(this.ddl_322.SelectedValue);
                int weeks2 = Convert.ToInt32(this.ddl_323.SelectedValue);

                while (tmpdate <= enddate)
                {
                    DateTime tmpdate1 = tmpdate;
                    if (weeks2 > changeobj.ChangeWeek(tmpdate1))
                    {
                        tmpdate1 = tmpdate1.AddDays(-(int)changeobj.ChangeWeek(tmpdate1)).AddDays(weeks2);
                    }
                    else if (weeks2 < changeobj.ChangeWeek(tmpdate1))
                    {
                        tmpdate1 = tmpdate1.AddDays(7).AddDays(-(int)changeobj.ChangeWeek(tmpdate1));
                        tmpdate1 = tmpdate1.AddDays(weeks2);
                    }

                    if (weeks1 > 4) weeks1 = PCalendarUtil.GetWeeksOfMonth(tmpdate1.AddMonths(1).AddDays(-1));
                    tmpdate1 = tmpdate1.AddDays((weeks1 - 1) * 7);

                    sdate1 = Convert.ToDateTime(tmpdate1.AddDays(0).ToString("yyyy/MM/dd") + " " + this.ddl_stime.SelectedValue + ":00");
                    edate1 = Convert.ToDateTime(tmpdate1.AddDays(addays1).ToString("yyyy/MM/dd") + " " + this.ddl_etime.SelectedValue + ":00");
                    if (sdate1 >= c03_sdate && sdate1 <= enddate)
                    {
                        if (sdate1.Month == tmpdate.Month)
                        {
                            InsertDB_C02(newpk + count, sdate1, edate1, c03_no);
                            count++;
                        }
                    }
                    tmpdate = tmpdate.AddMonths(months);
                }
                #endregion
            }
            #endregion
        }
        return count;
    }
    #endregion

    #region 年循環
    private int InsertCycleYear(int c03_no)
    {
        int count = 0;
        int newpk = new C02DAO().GetMaxNoByPeoUid(Convert.ToInt32(this.lab_peo_uid.Text)) + 1;
        DateTime sdate1 = new DateTime();
        DateTime edate1 = new DateTime();
        DateTime c03_sdate = new DateTime();
        c03_sdate = Convert.ToDateTime(this.cl_sdate1._ADDate);
        int addays1 = PCalendarUtil.DateDiff(Convert.ToDateTime(this.cl_sdate._ADDate), Convert.ToDateTime(this.cl_edate._ADDate));

        if (this.rb_51.Checked)
        {
            #region 次數循環
            if (this.rb_41.Checked)
            {
                #region 每年n月，n日期
                DateTime tmpdate = Convert.ToDateTime(c03_sdate.Year + "/" + this.ddl_411.SelectedValue + "/" + this.txt_412.Text);

                for (int i = 0; i < Convert.ToInt32(this.txt_qty.Text); i++)
                {
                    sdate1 = Convert.ToDateTime(tmpdate.ToString("yyyy/MM/dd") + " " + this.ddl_stime.SelectedValue + ":00");
                    edate1 = Convert.ToDateTime(tmpdate.AddDays(addays1).ToString("yyyy/MM/dd") + " " + this.ddl_etime.SelectedValue + ":00");

                    if (sdate1 >= c03_sdate)
                    {
                        InsertDB_C02(newpk + count, sdate1, edate1, c03_no);
                        count++;
                    }
                    tmpdate = tmpdate.AddYears(1);
                }
                #endregion
            }
            else
            {
                #region 每年n月，第n週，星期幾
                DateTime tmpdate = Convert.ToDateTime(c03_sdate.Year + "/" + this.ddl_421.SelectedValue + "/01");
                int weeks1 = Convert.ToInt32(this.ddl_422.SelectedValue);
                int weeks2 = Convert.ToInt32(this.ddl_423.SelectedValue);

                for (int i = 0; i < Convert.ToInt32(this.txt_qty.Text); i++)
                {
                    DateTime tmpdate1 = tmpdate.AddYears(i);
                    if (weeks2 > changeobj.ChangeWeek(tmpdate1))
                    {
                        tmpdate1 = tmpdate1.AddDays(-(int)changeobj.ChangeWeek(tmpdate1)).AddDays(weeks2);
                    }
                    else if (weeks2 < changeobj.ChangeWeek(tmpdate1))
                    {
                        tmpdate1 = tmpdate1.AddDays(7).AddDays(-(int)changeobj.ChangeWeek(tmpdate1));
                        tmpdate1 = tmpdate1.AddDays(weeks2);
                    }

                    if (weeks1 > 4) weeks1 = PCalendarUtil.GetWeeksOfMonth(tmpdate1.AddMonths(1).AddDays(-1));
                    tmpdate1 = tmpdate1.AddDays((weeks1 - 1) * 7);

                    sdate1 = Convert.ToDateTime(tmpdate1.AddDays(0).ToString("yyyy/MM/dd") + " " + this.ddl_stime.SelectedValue + ":00");
                    edate1 = Convert.ToDateTime(tmpdate1.AddDays(addays1).ToString("yyyy/MM/dd") + " " + this.ddl_etime.SelectedValue + ":00");
                    if (sdate1 >= c03_sdate)
                    {
                        InsertDB_C02(newpk + count, sdate1, edate1, c03_no);
                        count++;
                    }
                }
                #endregion
            }
            #endregion
        }
        else
        {
            DateTime enddate = Convert.ToDateTime(this.cl_edate1._ADDate);
            #region 截止日期
            if (this.rb_41.Checked)
            {
                #region 每年n月，n日期
                DateTime tmpdate = Convert.ToDateTime(c03_sdate.Year + "/" + this.ddl_411.SelectedValue + "/" + this.txt_412.Text);

                while (tmpdate <= enddate)
                {
                    sdate1 = Convert.ToDateTime(tmpdate.ToString("yyyy/MM/dd") + " " + this.ddl_stime.SelectedValue + ":00");
                    edate1 = Convert.ToDateTime(tmpdate.AddDays(addays1).ToString("yyyy/MM/dd") + " " + this.ddl_etime.SelectedValue + ":00");

                    if (sdate1 >= c03_sdate)
                    {
                        InsertDB_C02(newpk + count, sdate1, edate1, c03_no);
                        count++;
                    }
                    tmpdate = tmpdate.AddYears(1);
                }
                #endregion
            }
            else
            {
                #region 每年n月，第n週，星期幾
                DateTime tmpdate = Convert.ToDateTime(c03_sdate.Year + "/" + this.ddl_421.SelectedValue + "/01");
                int weeks1 = Convert.ToInt32(this.ddl_422.SelectedValue);
                int weeks2 = Convert.ToInt32(this.ddl_423.SelectedValue);

                while (tmpdate <= enddate)
                {
                    DateTime tmpdate1 = tmpdate;
                    if (weeks2 > changeobj.ChangeWeek(tmpdate1))
                    {
                        tmpdate1 = tmpdate1.AddDays(-(int)changeobj.ChangeWeek(tmpdate1)).AddDays(weeks2);
                    }
                    else if (weeks2 < changeobj.ChangeWeek(tmpdate1))
                    {
                        tmpdate1 = tmpdate1.AddDays(7).AddDays(-(int)changeobj.ChangeWeek(tmpdate1));
                        tmpdate1 = tmpdate1.AddDays(weeks2);
                    }

                    if (weeks1 > 4) weeks1 = PCalendarUtil.GetWeeksOfMonth(tmpdate1.AddMonths(1).AddDays(-1));
                    tmpdate1 = tmpdate1.AddDays((weeks1 - 1) * 7);

                    sdate1 = Convert.ToDateTime(tmpdate1.AddDays(0).ToString("yyyy/MM/dd") + " " + this.ddl_stime.SelectedValue + ":00");
                    edate1 = Convert.ToDateTime(tmpdate1.AddDays(addays1).ToString("yyyy/MM/dd") + " " + this.ddl_etime.SelectedValue + ":00");
                    if (sdate1 >= c03_sdate)
                    {
                        InsertDB_C02(newpk + count, sdate1, edate1, c03_no);
                        count++;
                    }
                    tmpdate = tmpdate.AddYears(1);
                }
                #endregion
            }
            #endregion
        }
        return count;
    }
    #endregion

    #region 刪除
    protected void btn_del_Click(object sender, EventArgs e)
    {
        string aMSG = "";   //記錄錯誤訊息
        try
        {
            string delStr = "";
            delStr = "delete c02 where peo_uid=" + this.lab_peo_uid.Text + " and c02_no=" + this.lab_no.Text;
            dbo.ExecuteNonQuery(delStr);

            if (this.cb_update.Checked && this.lab_c03_no.Text.Trim().Length > 0)
            {
                //先刪除舊的行程
                delStr = "delete c02 where c03_no=" + this.lab_c03_no.Text;
                dbo.ExecuteNonQuery(delStr);

                delStr = "delete c03 where c03_no=" + this.lab_c03_no.Text;
                dbo.ExecuteNonQuery(delStr);
            }

            //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
            new OperatesObject().ExecuteOperates(100301, sobj.sessionUserID, 1, "計畫--刪除 peo_uid" + this.lab_peo_uid.Text + ",c03_no=" + this.lab_c03_no.Text + "或c02_no=" + this.lab_no.Text);

            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "closeThickBox", "self.parent.location.reload(true);self.parent.tb_remove();", true);
        }
        catch (Exception ex)
        {
            aMSG = "功能名稱:行事曆-刪除<br>錯誤訊息:" + ex.ToString();
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