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
            ShowDataList();
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
        this.MultiView1.ActiveViewIndex = 1;
        ClearControlValue();
        ShowM01();

        DataTable dt = new DataTable();
        this.Navigator1.SubFunc = "修改";
        Entity.m02 tbData = new M02DAO().GetByNo(Convert.ToInt32(this.lab_no.Text));
        
        #region textbox
        this.txt_cc.Text = tbData.m02_cc.Value.ToString();
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
        if(tbData.m02_buydate!=null) this.cal_buydate._ADDate = tbData.m02_buydate.Value;
        #endregion
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
            this.lab_pageIndex.Text = this.GridView1.PageIndex.ToString();
            this.lab_no.Text = this.GridView1.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString();
            ShowDataModify();
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

        #region 所在地
        if (this.ddl_platoon.SelectedValue.Equals("0"))
        {
            ShowMsg("請選擇 所在地");
            feedback = false;
        }
        #endregion
        #region 資產編號
        if (string.IsNullOrEmpty(this.txt_number.Text))
        {
            ShowMsg("請輸入 資產編號");
            feedback = false;
        }
        else if (!checkobj.IsValidLen(this.txt_number.Text.Trim(), 30))
        {
            ShowMsg("資產編號 長度不可超過30個字");
            feedback = false;
        }
        #endregion
        #region 設備名稱
        if (string.IsNullOrEmpty(this.txt_code.Text))
        {
            ShowMsg("請輸入 設備名稱");
            feedback = false;
        }
        else if (!checkobj.IsValidLen(this.txt_code.Text.Trim(), 40))
        {
            ShowMsg("設備名稱 長度不可超過40個字");
            feedback = false;
        }
        #endregion
        #region 可借用時間
        if (this.ddl_chekuan.SelectedValue.Equals("0"))
        {
            ShowMsg("請選擇 可借用時間-起");
            feedback = false;
        }
        if (this.ddl_energy.SelectedValue.Equals("0"))
        {
            ShowMsg("請選擇 可借用時間-迄");
            feedback = false;
        }
        if (Convert.ToDateTime(System.DateTime.Today.ToString("yyyy/MM/dd") + " " + this.ddl_chekuan.SelectedValue) >=
            Convert.ToDateTime(System.DateTime.Today.ToString("yyyy/MM/dd") + " " + this.ddl_energy.SelectedValue))
        {
            ShowMsg("可借用時間-起 不可大於等於 可借用時間-迄");
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
        #region 保管人電話&分機
        if (string.IsNullOrEmpty(this.txt_type.Text))
        {
            ShowMsg("請輸入 保管人電話");
            feedback = false;
        }
        else if (!checkobj.IsValidLen(this.txt_type.Text.Trim(), 20))
        {
            ShowMsg("保管人電話 長度不可超過20個數字");
            feedback = false;
        }
        if (!checkobj.IsValidLen(this.txt_engine.Text.Trim(), 10))
        {
            ShowMsg("保管人電話分機 長度不可超過10個數字");
            feedback = false;
        }
        #endregion

        #region 設備圖片
        this.ImageUpload1.UploadPic();
        if (this.ImageUpload1.HasFile)
        {
            if (!this.ImageUpload1.CheckFileType)
            {
                ShowMsg("設備圖片 檔案類型錯誤");
                feedback = false;
            }
            if (!this.ImageUpload1.CheckFileSize)
            {
                ShowMsg("設備圖片 檔案大小錯誤");
                feedback = false;
            }
        }
        #endregion
        #region 設備圖片
        if (!checkobj.IsValidLen(this.txt_memo.Text.Trim(), 200))
        {
            ShowMsg("設備描述 長度不可超過200個數文字");
            feedback = false;
        }
        #endregion

        return feedback;
    }
    #endregion

    #region 刪除檔案
    protected void lbtn_delpic1_Click(object sender, EventArgs e)
    {
        //string aMSG = "";
        //try
        //{
        //    #region equipments
        //    EquipmentsDAO equDAO = new EquipmentsDAO();
        //    equipments newRow = equDAO.GetByNo(Convert.ToInt32(this.lab_no.Text));

        //    newRow.equ_createtime = System.DateTime.Now;
        //    newRow.equ_createuid = Convert.ToInt32(sobj.sessionUserID);
        //    newRow.equ_pic = null;
        //    newRow.equ_pictype = "";
        //    equDAO.Update();
        //    #endregion

        //    ShowDataModify(); //顯示修改畫面
        //}
        //catch (Exception ex)
        //{
        //    aMSG = "功能名稱：" + this.Navigator1.SubFunc + "<br>錯誤訊息:" + ex.ToString();
        //    Response.Write(aMSG);
        //}
    }
    #endregion

    #region 確定
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        //string aMSG = "";
        //try
        //{
        //    if (CheckInputValue())
        //    {
        //        if (this.lab_no.Text.Trim().Length > 0)
        //        {
        //            #region 修改
        //            string sqlstr = "select equ_no from equipments where equ_number='" + this.txt_number.Text + "' and equ_status='1' and equ_no<>" + this.lab_no.Text;
        //            DataTable dt = new DataTable();
        //            dt = dbo.ExecuteQuery(sqlstr);
        //            if (dt.Rows.Count > 0)
        //            {
        //                ShowMsg("此 資產編號[" + this.txt_number.Text + "] 已存在");
        //                return;
        //            }
        //            else
        //            {
        //                #region equipments
        //                EquipmentsDAO equDAO = new EquipmentsDAO();
        //                equipments newRow = equDAO.GetByNo(Convert.ToInt32(this.lab_no.Text));
        //                newRow.spo_no = Convert.ToInt32(this.ddl_platoon.SelectedValue);
        //                newRow.equ_createtime = System.DateTime.Now;
        //                newRow.equ_createuid = Convert.ToInt32(sobj.sessionUserID);
        //                newRow.equ_descript = this.txt_memo.Text;
        //                newRow.equ_etime = this.ddl_energy.SelectedValue;
        //                newRow.equ_ext = this.txt_engine.Text;
        //                newRow.equ_name = this.txt_code.Text;
        //                newRow.equ_number = this.txt_number.Text;
        //                if (this.ImageUpload1.HasFile)
        //                {
        //                    newRow.equ_pic = this.ImageUpload1.GetFileBytes;
        //                    newRow.equ_pictype = this.ImageUpload1.GetExtension;
        //                }
        //                newRow.equ_stime = this.ddl_chekuan.SelectedValue;
        //                newRow.equ_tel = this.txt_type.Text;
        //                if (this.DepartTreeTextBox1.Value.Length > 0)
        //                    newRow.peo_uid = Convert.ToInt32(this.DepartTreeTextBox1.Value);
        //                else
        //                    newRow.peo_uid = 0;
        //                equDAO.Update();
        //                #endregion

        //                //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
        //                new OperatesObject().ExecuteOperates(301001, sobj.sessionUserID, 3, "編號：" + this.lab_no.Text + ",所在地：" + this.ddl_platoon.SelectedItem.Text + ",設備名稱：" + this.txt_code.Text.Trim() + "...等");
        //            }
        //            #endregion
        //        }
        //        else
        //        {
        //            #region 新增
        //            string sqlstr = "select equ_no from equipments where equ_number='" + this.txt_number.Text + "' and equ_status='1'";
        //            DataTable dt = new DataTable();
        //            dt = dbo.ExecuteQuery(sqlstr);
        //            if (dt.Rows.Count > 0)
        //            {
        //                ShowMsg("此 資產編號[" + this.txt_number.Text + "] 已存在");
        //                return;
        //            }
        //            else
        //            {
        //                #region equipments
        //                EquipmentsDAO equDAO = new EquipmentsDAO();
        //                equipments newRow = new equipments();
        //                newRow.spo_no = Convert.ToInt32(this.ddl_platoon.SelectedValue);
        //                newRow.equ_createtime = System.DateTime.Now;
        //                newRow.equ_createuid = Convert.ToInt32(sobj.sessionUserID);
        //                newRow.equ_descript = this.txt_memo.Text;
        //                newRow.equ_etime = this.ddl_energy.SelectedValue;
        //                newRow.equ_ext = this.txt_engine.Text;
        //                newRow.equ_name = this.txt_code.Text;
        //                newRow.equ_number = this.txt_number.Text;
        //                if (this.ImageUpload1.HasFile)
        //                {
        //                    newRow.equ_pic = this.ImageUpload1.GetFileBytes;
        //                    newRow.equ_pictype = this.ImageUpload1.GetExtension;
        //                }
        //                newRow.equ_status = "1";
        //                newRow.equ_stime = this.ddl_chekuan.SelectedValue;
        //                newRow.equ_tel = this.txt_type.Text;
        //                if (this.DepartTreeTextBox1.Value.Length > 0)
        //                    newRow.peo_uid = Convert.ToInt32(this.DepartTreeTextBox1.Value);
        //                else
        //                    newRow.peo_uid = 0;
        //                equDAO.AddEquipments(newRow);
        //                equDAO.Update();
        //                #endregion
        //                int equ_no = newRow.equ_no;

        //                //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
        //                new OperatesObject().ExecuteOperates(301001, sobj.sessionUserID, 1, "所在地：" + this.ddl_platoon.SelectedItem.Text + ",場地名稱：" + this.txt_code.Text.Trim() + "....等");
        //            }
        //            #endregion
        //        }
        //        //ShowDataList(); //呼叫列表

        //        Response.Write(PCalendarUtil.ShowMsg_URL("", "301001.aspx"));
        //    }
        //}
        //catch (Exception ex)
        //{
        //    aMSG = "功能名稱：" + this.Navigator1.SubFunc + "<br>錯誤訊息:" + ex.ToString();
        //    Response.Write(aMSG);
        //}
    }
    #endregion

    #region 取消
    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        ShowDataList(); //呼叫列表
    }
    #endregion
}