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
/// 功能名稱：管理作業 / 場地管理 / 場地資料管理
/// 功能編號：30/300400/300402
/// 撰寫者：Lina
/// 撰寫時間：2010/09/23
/// 修改時間：2010/12/09
/// </summary>
public partial class _30_300400_300402 : System.Web.UI.Page
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
            this.ViewModify.Visible = false;
            this.ViewList.Visible = false;

            ShowDataList();
        }
        else
            return;
    }

    #region 畫面：列表
    private void ShowDataList()
    {
        this.lab_no.Text = "";
        this.ViewList.Visible = true;
        this.ViewModify.Visible = false;
        this.Navigator1.SubFunc = "";
        this.GridView1.DataBind();
        if (this.lab_pageIndex.Text.Length > 0) this.GridView1.PageIndex = Convert.ToInt32(this.lab_pageIndex.Text);
        //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
        new OperatesObject().ExecuteOperates(300402, sobj.sessionUserID, 2, "場地資料列表");
    }
    #endregion

    #region 清空值
    private void ClearControlValue()
    {
        this.ddl_spot.Items.Clear();
        this.txt_count.Text = "";
        this.txt_describe.Text = "";
        this.txt_ext1.Text = "";
        this.txt_ext2.Text = "";
        this.txt_floor.Text = "";
        this.txt_human.Text = "";
        this.txt_name.Text = "";
        this.txt_tel1.Text = "";
        this.txt_tel2.Text = "";
        this.txt_telephone.Text = "";
        
        this.rb_01.Checked = false;
        this.rb_02.Checked = false;
        this.DepartTreeTextBox1.Clear();
        this.DepartTreeTextBox2.Clear();
        this.DepartTreeListBox1.Clear();
        this.ImageUpload1.Visible = false;
        this.ImageUpload2.Visible = false;
        this.ImageUpload1.ClearPrivew();
        this.ImageUpload2.ClearPrivew();
        this.lbtn_delpic1.Visible = false;
        this.lbtn_delpic2.Visible = false;
        this.Panel1.Visible = false;
        this.Panel2.Visible = false;

        this.ddl_stime_CascadingDropDown.ContextKey = "08:00";
        this.ddl_etime_CascadingDropDown.ContextKey = "18:00";
    }
    #endregion

    #region 所在地列表及新增或修改預設值
    private void ShowSpot()
    {
        this.ddl_spot.Items.Clear();
        string sqlstr = "select spo_no,spo_name from spot where spo_status='1' and (spo_function LIKE '____1%') order by spo_no";
        DataTable dt = new DataTable();
        dt = dbo.ExecuteQuery(sqlstr);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ListItem newitem = new ListItem(dt.Rows[i]["spo_name"].ToString(), dt.Rows[i]["spo_no"].ToString());
                this.ddl_spot.Items.Add(newitem);
            }
        }
        this.ddl_spot.Items.Insert(0, new ListItem("請選擇", "0"));
    }
    #endregion

    #region 畫面：修改
    private void ShowDataModify(int pkno)
    {
        this.ViewList.Visible = false;
        this.ViewModify.Visible = true;
        ClearControlValue();
        ShowSpot();
        DataTable dt = new DataTable();
        this.Navigator1.SubFunc = "修改";
        Entity.rooms roomsData = new RoomsDAO().GetByRoomsNo(pkno);

        #region textbox
        this.txt_name.Text = roomsData.roo_name;
        this.txt_tel1.Text = roomsData.roo_tel;
        this.txt_tel2.Text = roomsData.roo_twotel;
        this.txt_ext1.Text = roomsData.roo_ext;
        this.txt_ext2.Text = roomsData.roo_twoext;
        this.txt_human.Text = roomsData.roo_human.ToString();
        this.txt_floor.Text = roomsData.roo_floor.ToString();
        this.txt_count.Text = roomsData.roo_count.ToString();
        this.txt_describe.Text = roomsData.roo_describe;
        this.txt_telephone.Text = roomsData.roo_telephone;
        #endregion

        #region 下拉式選單
        try
        {
            this.ddl_spot.Items.FindByValue(roomsData.spo_no.ToString()).Selected = true;
        }
        catch { }

        this.ddl_stime_CascadingDropDown.ContextKey = roomsData.roo_stime;
        this.ddl_etime_CascadingDropDown.ContextKey = roomsData.roo_etime;

        if (roomsData.roo_dep.Equals("1"))
            this.rb_01.Checked = true;
        else
        {
            this.rb_02.Checked = true;
            string sqlstr = "select gov_depno from government where roo_no=" + this.lab_no.Text;
            dt.Clear();
            dt = dbo.ExecuteQuery(sqlstr);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                    this.DepartTreeListBox1.Add(dt.Rows[i]["gov_depno"].ToString());
            }
        }
        #endregion

        #region 保管人
        if (roomsData.roo_oneuid.HasValue)
        {
            this.DepartTreeTextBox1.Add(roomsData.roo_oneuid.ToString());
        }
        if (roomsData.roo_twouid.HasValue)
        {
            this.DepartTreeTextBox2.Add(roomsData.roo_twouid.ToString());
        }
        #endregion

        #region 場地圖片
        if (roomsData.roo_pictype != null && roomsData.roo_pictype.Length > 0)
        {
            string src = "../../lib/ShowPic.aspx?tb=rooms&picorder=1&pkno=" + this.lab_no.Text;
            this.div_pic1.InnerHtml = "<a href=\"" + src + "\" rel=\"lytebox\" title=\"場地圖片\" OnClick=\"return false;\" OnLoad=\"return true;\"><img src=" + src + " width=\"60\" height=\"50\"  /></a>";
            this.lbtn_delpic1.Visible = true;
            this.Panel1.Visible = true;
        }
        else
        {
            this.div_pic1.InnerHtml = "無圖示";
            this.ImageUpload1.Visible = true;
        }
        #endregion

        #region 場地平面圖
        if (roomsData.roo_planetype != null && roomsData.roo_planetype.Length > 0)
        {
            string src = "../../lib/ShowPic.aspx?tb=rooms&picorder=2&pkno=" + this.lab_no.Text;
            this.div_pic2.InnerHtml = "<a href=\"" + src + "\" rel=\"lytebox\" title=\"場地平面圖\" OnClick=\"return false;\" OnLoad=\"return true;\"><img src=" + src + " width=\"60\" height=\"50\"  /></a>";
            this.lbtn_delpic2.Visible = true;
            this.Panel2.Visible = true;
        }
        else
        {
            this.div_pic2.InnerHtml = "無圖示";
            this.ImageUpload2.Visible = true;
        }
        #endregion
    }
    #endregion

    #region 畫面：新增
    private void ShowDataInsert()
    {
        this.ViewList.Visible = false;
        this.ViewModify.Visible = true;
        ClearControlValue();
        ShowSpot();

        this.Navigator1.SubFunc = "新增";
        this.rb_01.Checked = true;
        this.ImageUpload1.Visible = true;
        this.ImageUpload2.Visible = true;
    }
    #endregion

    #region 調整輸出格式
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = new SpotDAO().GetNameBySpoNo(Convert.ToInt32(e.Row.Cells[0].Text));
            e.Row.Cells[3].Text = new PeopleDAO().GetPeopleNameByUid(Convert.ToInt32(e.Row.Cells[3].Text));
            ((Button)e.Row.FindControl("btn_modify")).CommandArgument = ((GridView)sender).DataKeys[e.Row.RowIndex].Value.ToString();
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
            new OperatesObject().ExecuteOperates(300402, sobj.sessionUserID, 2, "查詢 場地資料 編號:" + this.lab_no.Text);
            this.lab_pageIndex.Text = this.GridView1.PageIndex.ToString();
            this.lab_no.Text = e.CommandArgument.ToString();
            ShowDataModify(Convert.ToInt32(this.lab_no.Text));
            #endregion
        }
        else if (e.CommandName.Equals("del"))
        {
            #region 執行刪除
            string pkno = this.GridView1.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString();
            this.lab_pageIndex.Text = this.GridView1.PageIndex.ToString();
            string sqlstr = "update rooms set roo_status='2' where roo_no=" + pkno;
            dbo.ExecuteNonQuery(sqlstr);
            //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
            new OperatesObject().ExecuteOperates(300402, sobj.sessionUserID, 3, "刪除 場地資料 編號:" + pkno);
            #endregion
            ShowDataList(); //呼叫列表
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
        if (this.ddl_spot.SelectedValue.Equals("0"))
        {
            ShowMsg("請選擇 所在地");
            feedback = false;
        }
        #endregion
        #region 場地名稱
        if (string.IsNullOrEmpty(this.txt_name.Text))
        {
            ShowMsg("請輸入 場地名稱");
            feedback = false;
        }
        else if (!checkobj.IsValidLen(this.txt_name.Text.Trim(), 30))
        {
            ShowMsg("場地名稱 長度不可超過30個數文字");
            feedback = false;
        }
        #endregion
        #region 場地電話
        if (string.IsNullOrEmpty(this.txt_telephone.Text))
        {
            ShowMsg("請輸入 場地電話");
            feedback = false;
        }
        else if (!checkobj.IsValidLen(this.txt_telephone.Text.Trim(), 20))
        {
            ShowMsg("場地電話 長度不可超過20個數字");
            feedback = false;
        }
        #endregion
        #region 第一保管人
        if (this.DepartTreeTextBox1.Items.Count <= 0 || this.DepartTreeTextBox1.Items == null)
        {
            ShowMsg("請選擇 第一保管人");
            feedback = false;
        }
        #endregion
        #region 第一保管人電話&分機
        if (string.IsNullOrEmpty(this.txt_tel1.Text))
        {
            ShowMsg("請輸入 第一保管人電話");
            feedback = false;
        }
        else if (!checkobj.IsValidLen(this.txt_tel1.Text.Trim(), 20))
        {
            ShowMsg("第一保管人電話 長度不可超過20個數字");
            feedback = false;
        }
        if (!checkobj.IsValidLen(this.txt_ext1.Text.Trim(), 10))
        {
            ShowMsg("第一保管人電話分機 長度不可超過10個數字");
            feedback = false;
        }
        #endregion
        #region 第二保管人電話&分機
        if (!checkobj.IsValidLen(this.txt_tel2.Text.Trim(), 20))
        {
            ShowMsg("第二保管人電話 長度不可超過20個數字");
            feedback = false;
        }
        if (!checkobj.IsValidLen(this.txt_ext2.Text.Trim(), 10))
        {
            ShowMsg("第二保管人電話分機 長度不可超過10個數字");
            feedback = false;
        }
        #endregion
        #region 容納人數
        if (string.IsNullOrEmpty(this.txt_human.Text))
        {
            ShowMsg("請輸入  容納人數");
            feedback = false;
        }
        else if (!checkobj.IsInt(this.txt_human.Text))
        {
            ShowMsg("容納人數 需為數字");
            feedback = false;
        }
        #endregion
        #region 所在樓層
        if (string.IsNullOrEmpty(this.txt_floor.Text))
        {
            ShowMsg("請輸入 所在樓層");
            feedback = false;
        }
        else if (!checkobj.IsInt(this.txt_floor.Text))
        {
            ShowMsg("所在樓層 需為數字");
            feedback = false;
        }
        #endregion
        #region 可借用時間
        if (this.ddl_stime.SelectedValue.Equals("0"))
        {
            ShowMsg("請選擇 可借用時間-起");
            feedback = false;
        }
        if (this.ddl_etime.SelectedValue.Equals("0"))
        {
            ShowMsg("請選擇 可借用時間-迄");
            feedback = false;
        }
        if (Convert.ToDateTime(System.DateTime.Today.ToString("yyyy/MM/dd") + " " + this.ddl_stime.SelectedValue) >=
            Convert.ToDateTime(System.DateTime.Today.ToString("yyyy/MM/dd") + " " + this.ddl_etime.SelectedValue))
        {
            ShowMsg("可借用時間-起 不可大於等於 可借用時間-迄");
            feedback = false;
        }
        #endregion
        #region 最低與會人數
        if (string.IsNullOrEmpty(this.txt_count.Text))
        {
            this.txt_count.Text = "0";
        }
        else if (!checkobj.IsInt(this.txt_count.Text))
        {
            ShowMsg("最低與會人數 需為數字");
            feedback = false;
        }
        #endregion
        #region 場地圖片
        this.ImageUpload1.UploadPic();
        if (this.ImageUpload1.HasFile)
        {
            if (!this.ImageUpload1.CheckFileType)
            {
                ShowMsg("場地圖片 檔案類型錯誤");
                feedback = false;
            }
            if (!this.ImageUpload1.CheckFileSize)
            {
                ShowMsg("場地圖片 檔案大小錯誤");
                feedback = false;
            }
        }
        #endregion
        #region 場地平面圖
        this.ImageUpload2.UploadPic();
        if (this.ImageUpload2.HasFile)
        {
            if (!this.ImageUpload2.CheckFileType)
            {
                ShowMsg("場地平面圖 檔案類型錯誤");
                feedback = false;
            }
            if (!this.ImageUpload2.CheckFileSize)
            {
                ShowMsg("場地平面圖 檔案大小錯誤");
                feedback = false;
            }
        }
        #endregion
        #region 場地描述
        if (!checkobj.IsValidLen(this.txt_describe.Text.Trim(), 500))
        {
            ShowMsg("場地描述 長度不可超過500個數文字");
            feedback = false;
        }
        #endregion
        #region 場地開放單位
        if (this.rb_02.Checked)
        {
            if (this.DepartTreeListBox1.Items.Count <= 0 || this.DepartTreeListBox1.Items == null)
            {
                ShowMsg("請選擇 場地開放單位");
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
            #region rooms
            RoomsDAO RoomsDAO1 = new RoomsDAO();
            rooms newRow = RoomsDAO1.GetByRoomsNo(Convert.ToInt32(this.lab_no.Text));
            newRow.roo_createtime = System.DateTime.Now;
            newRow.roo_createuid = Convert.ToInt32(sobj.sessionUserID);
            newRow.roo_picture = null;
            newRow.roo_pictype = "";
            RoomsDAO1.Update();
            #endregion

            ShowDataModify(Convert.ToInt32(this.lab_no.Text)); //顯示修改畫面
        }
        catch (Exception ex)
        {
            aMSG = "功能名稱：" + this.Navigator1.SubFunc + "<br>錯誤訊息:" + ex.ToString();
            Response.Write(aMSG);
        }
    }

    protected void lbtn_delpic2_Click(object sender, EventArgs e)
    {
        string aMSG = "";
        try
        {
            #region rooms
            RoomsDAO RoomsDAO1 = new RoomsDAO();
            rooms newRow = RoomsDAO1.GetByRoomsNo(Convert.ToInt32(this.lab_no.Text));
            newRow.roo_createtime = System.DateTime.Now;
            newRow.roo_createuid = Convert.ToInt32(sobj.sessionUserID);
            newRow.roo_plane = null;
            newRow.roo_planetype = "";
            RoomsDAO1.Update();
            #endregion

            ShowDataModify(Convert.ToInt32(this.lab_no.Text)); //顯示修改畫面
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
                if (this.lab_no.Text.Trim().Length>0)
                {
                    #region 修改
                    string sqlstr = "select roo_no from rooms where roo_name=N'" + this.txt_name.Text + "' and roo_status='1' and roo_no<>" + this.lab_no.Text;
                    DataTable dt = new DataTable();
                    dt = dbo.ExecuteQuery(sqlstr);
                    if (dt.Rows.Count > 0)
                    {
                        ShowMsg("此 場地[" + this.txt_name.Text + "] 已存在");
                        return;
                    }
                    else
                    {
                        #region rooms
                        RoomsDAO RoomsDAO1 = new RoomsDAO();
                        rooms newRow = RoomsDAO1.GetByRoomsNo(Convert.ToInt32(this.lab_no.Text));
                        newRow.spo_no = Convert.ToInt32(this.ddl_spot.SelectedValue);
                        newRow.roo_count = Convert.ToInt32(this.txt_count.Text);
                        newRow.roo_createtime = System.DateTime.Now;
                        newRow.roo_createuid = Convert.ToInt32(sobj.sessionUserID);
                        if (this.rb_01.Checked)
                            newRow.roo_dep = "1";
                        else
                            newRow.roo_dep = "2";
                        newRow.roo_describe = this.txt_describe.Text;
                        newRow.roo_etime = this.ddl_etime.SelectedValue;
                        newRow.roo_ext = this.txt_ext1.Text;
                        newRow.roo_floor = Convert.ToInt32(this.txt_floor.Text);
                        newRow.roo_human = Convert.ToInt32(this.txt_human.Text);
                        newRow.roo_name = this.txt_name.Text;
                        newRow.roo_oneuid = Convert.ToInt32(this.DepartTreeTextBox1.Value);
                        if (this.ImageUpload1.HasFile)
                        {
                            newRow.roo_picture = this.ImageUpload1.GetFileBytes;
                            newRow.roo_pictype = this.ImageUpload1.GetExtension;
                        }
                        if (this.ImageUpload2.HasFile)
                        {
                            newRow.roo_plane = this.ImageUpload2.GetFileBytes;
                            newRow.roo_planetype = this.ImageUpload2.GetExtension;
                        }
                        newRow.roo_stime = this.ddl_stime.SelectedValue;
                        newRow.roo_tel = this.txt_tel1.Text;
                        newRow.roo_twoext = this.txt_ext2.Text;
                        newRow.roo_twotel = this.txt_tel2.Text;
                        if (this.DepartTreeTextBox2.Value.Length > 0)
                            newRow.roo_twouid = Convert.ToInt32(this.DepartTreeTextBox2.Value);
                        else
                            newRow.roo_twouid = 0;
                        newRow.roo_telephone = this.txt_telephone.Text;
                        RoomsDAO1.Update();
                        #endregion

                        #region government
                        string DelStr = "delete from government where roo_no=" + this.lab_no.Text;
                        dbo.ExecuteNonQuery(DelStr);

                        if (this.rb_02.Checked)
                        {
                            for (int i = 0; i < this.DepartTreeListBox1.Items.Count; i++)
                            {
                                string InsStr = "insert into government (roo_no,gov_no,gov_depno) values(" + this.lab_no.Text + "," + Convert.ToString(i + 1) + "," + this.DepartTreeListBox1.Items[i].Key + ")";
                                dbo.ExecuteNonQuery(InsStr);
                            }
                        }
                        #endregion

                        //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
                        new OperatesObject().ExecuteOperates(300402, sobj.sessionUserID, 3, "編號：" + this.lab_no.Text + ",所在地：" + this.ddl_spot.SelectedItem.Text + ",場所名稱：" + this.txt_name.Text.Trim() + "...等");
                    }
                    #endregion
                }
                else
                {
                    #region 新增
                    string sqlstr = "select roo_no from rooms where roo_name=N'" + this.txt_name.Text + "' and roo_status='1'";
                    DataTable dt = new DataTable();
                    dt = dbo.ExecuteQuery(sqlstr);
                    if (dt.Rows.Count > 0)
                    {
                        ShowMsg("此 場地[" + this.txt_name.Text + "] 已存在");
                        return;
                    }
                    else
                    {
                        #region rooms
                        RoomsDAO RoomsDAO1 = new RoomsDAO();
                        rooms newRow = new rooms();
                        newRow.spo_no = Convert.ToInt32(this.ddl_spot.SelectedValue);
                        newRow.roo_count = Convert.ToInt32(this.txt_count.Text);
                        newRow.roo_createtime = System.DateTime.Now;
                        newRow.roo_createuid = Convert.ToInt32(sobj.sessionUserID);
                        if (this.rb_01.Checked)
                            newRow.roo_dep = "1";
                        else
                            newRow.roo_dep = "2";
                        newRow.roo_describe = this.txt_describe.Text;
                        newRow.roo_etime = this.ddl_etime.SelectedValue;
                        newRow.roo_ext = this.txt_ext1.Text;
                        newRow.roo_floor = Convert.ToInt32(this.txt_floor.Text);
                        newRow.roo_human = Convert.ToInt32(this.txt_human.Text);
                        newRow.roo_name = this.txt_name.Text;
                        newRow.roo_oneuid = Convert.ToInt32(this.DepartTreeTextBox1.Value);
                        if (this.ImageUpload1.HasFile)
                        {
                            newRow.roo_picture = this.ImageUpload1.GetFileBytes;
                            newRow.roo_pictype = this.ImageUpload1.GetExtension;
                        }
                        if (this.ImageUpload2.HasFile)
                        {
                            newRow.roo_plane = this.ImageUpload2.GetFileBytes;
                            newRow.roo_planetype = this.ImageUpload2.GetExtension;
                        }
                        newRow.roo_status = "1";
                        newRow.roo_stime = this.ddl_stime.SelectedValue;
                        newRow.roo_tel = this.txt_tel1.Text;
                        newRow.roo_twoext = this.txt_ext2.Text;
                        newRow.roo_twotel = this.txt_tel2.Text;
                        if (this.DepartTreeTextBox2.Value.Length > 0)
                            newRow.roo_twouid = Convert.ToInt32(this.DepartTreeTextBox2.Value);
                        else
                            newRow.roo_twouid = 0;
                        newRow.roo_telephone = this.txt_telephone.Text;
                        RoomsDAO1.AddRooms(newRow);
                        RoomsDAO1.Update();
                        #endregion
                        int roo_no = newRow.roo_no;
                        #region government
                        if (this.rb_02.Checked)
                        {
                            for (int i = 0; i < this.DepartTreeListBox1.Items.Count; i++)
                            {
                                string InsStr = "insert into government (roo_no,gov_no,gov_depno) values(" + roo_no + "," + Convert.ToString(i + 1) + "," + this.DepartTreeListBox1.Items[i].Key + ")";
                                dbo.ExecuteNonQuery(InsStr);
                            }
                        }
                        #endregion

                        //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
                        new OperatesObject().ExecuteOperates(300402, sobj.sessionUserID, 1, "所在地：" + this.ddl_spot.SelectedItem.Text + ",場地名稱：" + this.txt_name.Text.Trim() + "....等");
                    }
                    #endregion
                }
                //ShowDataList(); //呼叫列表

                Response.Write(PCalendarUtil.ShowMsg_URL("", "300402.aspx"));
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