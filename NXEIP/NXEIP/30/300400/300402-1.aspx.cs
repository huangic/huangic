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
/// 功能名稱：管理作業 / 場地管理 / 場地資料--新增、修改
/// 功能編號：30/300400/300402
/// 撰寫者：Lina
/// 撰寫時間：2010/09/27
/// </summary>
public partial class _30_300400_300402_1 : System.Web.UI.Page
{
    DBObject dbo = new DBObject();
    CheckObject checkobj = new CheckObject();
    SessionObject sobj = new SessionObject();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if (Request["pageIndex"] != null) this.lab_pageIndex.Text = Request["pageIndex"];
            if (Request["mode"] != null) this.lab_mode.Text = Request["mode"];
            if (Request["no"] != null) this.lab_no.Text = Request["no"];

            ListItem selectitem = new ListItem("請選擇", "0");
            #region 所在地
            string sqlstr = "select spo_no,spo_name from spot where spo_status='1' order by spo_no";
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
            this.ddl_spot.Items.Insert(0, selectitem);
            #endregion

            if (this.lab_mode.Text.Equals("modify"))
            {
                this.Navigator1.SubFunc = "修改";
                this.jQueryDepartTree1.Clear();
                this.jQueryPeopleTree1.Clear();
                this.jQueryPeopleTree2.Clear();
                Entity.rooms roomsData = new RoomsDAO().GetByRoomsNo(Convert.ToInt32(this.lab_no.Text));

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
                try
                {
                    this.ddl_stime.Items.FindByValue(roomsData.roo_stime).Selected = true;
                }
                catch { }
                try
                {
                    this.ddl_etime.Items.FindByValue(roomsData.roo_etime).Selected = true;
                }
                catch { }

                if (roomsData.roo_dep.Equals("1"))
                    this.rb_01.Checked = true;
                else
                {
                    this.rb_02.Checked = true;
                    sqlstr = "select gov_depno from government where roo_no=" + this.lab_no.Text;
                    dt.Clear();
                    dt = dbo.ExecuteQuery(sqlstr);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            this.jQueryDepartTree1.Add(dt.Rows[i]["gov_depno"].ToString());
                        }
                    }
                }
                #endregion

                #region 保管人
                if (roomsData.roo_oneuid.HasValue)
                {
                    this.jQueryPeopleTree1.Add(roomsData.roo_oneuid.ToString());
                }
                if (roomsData.roo_twouid.HasValue)
                {
                    this.jQueryPeopleTree2.Add(roomsData.roo_twouid.ToString());
                }
                #endregion

                #region 場地圖片
                if (roomsData.roo_pictype!=null && roomsData.roo_pictype.Length>0)
                {
                    string src = "../../lib/ShowPic.aspx?tb=rooms&picorder=1&pkno=" + this.lab_no.Text;
                    this.div_pic1.InnerHtml = "<a href=\"" + src + "\" rel=\"lytebox\" title=\"場地圖片\" OnClick=\"return false;\" OnLoad=\"return true;\"><img src=" + src + " width=\"60\" height=\"50\"  /></a>";
                    this.lbtn_delpic1.Visible = true;
                    this.ImageUpload1.Visible = false;
                    this.Panel1.Visible = true;
                }
                else
                {
                    this.lbtn_delpic1.Visible = false;
                    this.ImageUpload1.Visible = true;
                    this.Panel1.Visible = false;
                }
                #endregion

                #region 場地平面圖
                if (roomsData.roo_planetype != null && roomsData.roo_planetype.Length > 0)
                {
                    string src = "../../lib/ShowPic.aspx?tb=rooms&picorder=2&pkno=" + this.lab_no.Text;
                    this.div_pic2.InnerHtml = "<a href=\"" + src + "\" rel=\"lytebox\" title=\"場地平面圖\" OnClick=\"return false;\" OnLoad=\"return true;\"><img src=" + src + " width=\"60\" height=\"50\"  /></a>";
                    this.lbtn_delpic2.Visible = true;
                    this.ImageUpload2.Visible = false;
                    this.Panel2.Visible = true;
                }
                else
                {
                    this.lbtn_delpic2.Visible = false;
                    this.ImageUpload2.Visible = true;
                    this.Panel2.Visible = false;
                }
                #endregion
            }
            else
            {
                this.Navigator1.SubFunc = "新增";
                this.rb_01.Checked = true;

                this.lbtn_delpic1.Visible = false;
                this.ImageUpload1.Visible = true;

                this.lbtn_delpic2.Visible = false;
                this.ImageUpload2.Visible = true;
                
            }
        }
    }

    #region 顯示錯誤訊息
    private void ShowMsg(string msg)
    {
        string script = "<script>window.alert('" + msg + "');</script>";
        this.ClientScript.RegisterStartupScript(this.GetType(), "MSG", script);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "msg", "alert('" + msg + "');", true);
    }
    #endregion

    #region 檢查輸入值
    private bool CheckInputValue()
    {
        bool feedback=true;

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
        if (this.jQueryPeopleTree1.Items.Count <= 0 || this.jQueryPeopleTree1.Items == null)
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
        if (this.ddl_stime.SelectedIndex >= this.ddl_etime.SelectedIndex)
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
            if (this.jQueryDepartTree1.Items.Count <= 0 || this.jQueryDepartTree1.Items == null)
            {
                ShowMsg("請選擇 場地開放單位");
                feedback = false;
            }
        }
        #endregion

        return feedback;
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
                if (this.lab_mode.Text.Equals("modify"))
                {
                    #region 修改
                    string sqlstr = "select roo_no from rooms where roo_name=N'" + this.txt_name.Text + "' and roo_status='1' and roo_no<>" + this.lab_no.Text;
                    DataTable dt = new DataTable();
                    dt = dbo.ExecuteQuery(sqlstr);
                    if (dt.Rows.Count > 0)
                    {
                        Response.Write("<script>alert(\"此 場地[" + this.txt_name.Text + "] 已存在\");</script>");
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
                        newRow.roo_oneuid = Convert.ToInt32(this.jQueryPeopleTree1.Items[0].Key);
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
                        if (this.jQueryPeopleTree2.Items != null && this.jQueryPeopleTree2.Items.Count > 0)
                            newRow.roo_twouid = Convert.ToInt32(this.jQueryPeopleTree2.Items[0].Key);
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
                            for (int i = 0; i < this.jQueryDepartTree1.Items.Count; i++)
                            {
                                string InsStr = "insert into government (roo_no,gov_no,gov_depno) values(" + this.lab_no.Text + "," + Convert.ToString(i + 1) + "," + this.jQueryDepartTree1.Items[i].Key + ")";
                                dbo.ExecuteNonQuery(InsStr);
                            }
                        }
                        #endregion
                    }
                    #endregion

                    //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
                    new OperatesObject().ExecuteOperates(300402, sobj.sessionUserID, 3, "編號：" + this.lab_no.Text + ",所在地：" + this.ddl_spot.SelectedItem.Text + ",場所名稱：" + this.txt_name.Text.Trim() + "...等");
                }
                else
                {
                    #region 新增
                    string sqlstr = "select roo_no from rooms where roo_name=N'" + this.txt_name.Text + "' and roo_status='1'";
                    DataTable dt = new DataTable();
                    dt = dbo.ExecuteQuery(sqlstr);
                    if (dt.Rows.Count > 0)
                    {
                        Response.Write("<script>alert(\"此 場地[" + this.txt_name.Text + "] 已存在\");</script>");
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
                        newRow.roo_oneuid = Convert.ToInt32(this.jQueryPeopleTree1.Items[0].Key);
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
                        if (this.jQueryPeopleTree2.Items.Count > 0)
                            newRow.roo_twouid = Convert.ToInt32(this.jQueryPeopleTree2.Items[0].Key);
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
                            for (int i = 0; i < this.jQueryDepartTree1.Items.Count; i++)
                            {
                                string InsStr = "insert into government (roo_no,gov_no,gov_depno) values(" + roo_no + "," + Convert.ToString(i + 1) + "," + this.jQueryDepartTree1.Items[i].Key + ")";
                                dbo.ExecuteNonQuery(InsStr);
                            }
                        }
                        #endregion
                    }
                    #endregion

                    //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
                    new OperatesObject().ExecuteOperates(300402, sobj.sessionUserID, 1, "所在地：" + this.ddl_spot.SelectedItem.Text + ",場地名稱：" + this.txt_name.Text.Trim() + "....等");
                }

                Response.Write(PCalendarUtil.ShowMsg_URL("", "300402.aspx?pageIndex=" + this.lab_pageIndex.Text + "&count=" + new System.Random().Next(10000).ToString()));
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
        Response.Write(PCalendarUtil.ShowMsg_URL("", "300402.aspx?pageIndex=" + this.lab_pageIndex.Text + "&count=" + new System.Random().Next(10000).ToString()));
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

            Response.Write(PCalendarUtil.ShowMsg_URL("", "300402-1.aspx?no="+this.lab_no.Text+"&mode="+this.lab_mode.Text+"&pageIndex=" + this.lab_pageIndex.Text + "&count=" + new System.Random().Next(10000).ToString()));
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

            Response.Write(PCalendarUtil.ShowMsg_URL("", "300402-1.aspx?no=" + this.lab_no.Text + "&mode=" + this.lab_mode.Text + "&pageIndex=" + this.lab_pageIndex.Text + "&count=" + new System.Random().Next(10000).ToString()));
        }
        catch (Exception ex)
        {
            aMSG = "功能名稱：" + this.Navigator1.SubFunc + "<br>錯誤訊息:" + ex.ToString();
            Response.Write(aMSG);
        }
    }
    #endregion

    
}