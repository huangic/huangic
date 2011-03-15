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
/// 功能名稱：管理作業 / 設備管理 / 設備資料管理
/// 功能編號：30/301000/301001
/// 撰寫者：Lina
/// 撰寫時間：2011/01/20
/// 修改時間：2011/01/20
/// </summary>
public partial class _30_301000_301001 : System.Web.UI.Page
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
            if (Session["301001_value"] != null && Session["301001_value"].ToString().Length > 0 && Request["count"] != null)
            {
                string[] val = Session["301001_value"].ToString().Split(','); //0:mode,1:pkno,2:pageIndex
                this.lab_mode.Text = val[0];
                this.lab_no.Text = val[1];
                this.lab_pageIndex.Text = val[2];
                ShowDataModify();
                Session["301001_value"] = "";
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
        new OperatesObject().ExecuteOperates(301001, sobj.sessionUserID, 2, "設備資料列表");
    }
    #endregion

    #region 清空值
    private void ClearControlValue()
    {
        this.ddl_spot.Items.Clear();
        this.txt_describe.Text = "";
        this.txt_ext.Text = "";
        this.txt_name.Text = "";
        this.txt_tel.Text = "";
        this.txt_number.Text = "";

        this.DepartTreeTextBox1.Clear();
        this.ImageUpload1.Visible = false;
        this.ImageUpload1.ClearPrivew();
        this.lbtn_delpic1.Visible = false;
        this.Panel1.Visible = false;

        this.ddl_stime_CascadingDropDown.ContextKey = "08:00";
        this.ddl_etime_CascadingDropDown.ContextKey = "18:00";
    }
    #endregion

    #region 所在地列表及新增或修改預設值
    private void ShowSpot()
    {
        ListItem selectitem = new ListItem("請選擇", "0");
        string sqlstr = "select spo_no,spo_name from spot where spo_status='1' and (spo_function LIKE '_____1%') order by spo_no";
        DataTable dt = new DataTable();
        dt = dbo.ExecuteQuery(sqlstr);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.ddl_spot.Items.Add(new ListItem(dt.Rows[i]["spo_name"].ToString(), dt.Rows[i]["spo_no"].ToString()));
            }
        }
        this.ddl_spot.Items.Insert(0, selectitem);
    }
    #endregion

    #region 畫面：修改
    private void ShowDataModify()
    {
        this.MultiView1.ActiveViewIndex = 1;
        ClearControlValue();
        ShowSpot();

        DataTable dt = new DataTable();
        this.Navigator1.SubFunc = "修改";
        Entity.equipments equData = new EquipmentsDAO().GetByNo(Convert.ToInt32(this.lab_no.Text));

        #region textbox
        this.txt_number.Text = equData.equ_number;
        this.txt_name.Text = equData.equ_name;
        this.txt_tel.Text = equData.equ_tel;
        this.txt_ext.Text = equData.equ_ext;
        this.txt_describe.Text = equData.equ_descript;
        #endregion

        #region 下拉式選單
        try
        {
            this.ddl_spot.Items.FindByValue(equData.spo_no.ToString()).Selected = true;
        }
        catch { }

        this.ddl_stime_CascadingDropDown.ContextKey = equData.equ_stime;
        this.ddl_etime_CascadingDropDown.ContextKey = equData.equ_etime;
        #endregion

        #region 保管人
        this.DepartTreeTextBox1.Add(equData.peo_uid.ToString());
        #endregion

        #region 設備圖片
        if (equData.equ_pictype != null && equData.equ_pictype.Length > 0)
        {
            string src = "../../lib/ShowPic.aspx?tb=equipments&picorder=1&pkno=" + this.lab_no.Text;
            this.div_pic1.InnerHtml = "<a href=\"" + src + "\" rel=\"lytebox\" title=\"設備圖片\" OnClick=\"return false;\" OnLoad=\"return true;\"><img src=" + src + " width=\"60\" height=\"50\"  /></a>";
            this.lbtn_delpic1.Visible = true;
            this.Panel1.Visible = true;
        }
        else
        {
            this.div_pic1.InnerHtml = "無圖示";
            this.ImageUpload1.Visible = true;
        }
        #endregion
    }
    #endregion

    #region 畫面：新增
    private void ShowDataInsert()
    {
        this.MultiView1.ActiveViewIndex = 1;
        ClearControlValue();
        ShowSpot();

        this.Navigator1.SubFunc = "新增";
        this.ImageUpload1.Visible = true;
    }
    #endregion

    #region 調整輸出格式
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = new SpotDAO().GetNameBySpoNo(Convert.ToInt32(e.Row.Cells[0].Text));
            e.Row.Cells[3].Text = new PeopleDAO().GetPeopleNameByUid(Convert.ToInt32(e.Row.Cells[3].Text));
            e.Row.Cells[5].Text = e.Row.Cells[5].Text.Replace(System.Environment.NewLine, "<br />");
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
            new OperatesObject().ExecuteOperates(301001, sobj.sessionUserID, 2, "查詢 設備資料 編號:" + this.lab_no.Text);
            Session["301001_value"] = "modify," + this.GridView1.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString() + "," + this.GridView1.PageIndex.ToString();
            Response.Redirect("301001.aspx?count=" + new System.Random().Next(10000).ToString());
            #endregion
        }
        else if (e.CommandName.Equals("del"))
        {
            #region 執行刪除
            string pkno = this.GridView1.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString();
            this.lab_pageIndex.Text = this.GridView1.PageIndex.ToString();
            string sqlstr = "update equipments set equ_status='2',equ_createuid=" + sobj.sessionUserID + ",equ_createtime=getdate() where equ_no=" + pkno;
            dbo.ExecuteNonQuery(sqlstr);
            //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
            new OperatesObject().ExecuteOperates(301001, sobj.sessionUserID, 3, "刪除 設備資料 編號:" + pkno);
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
        if (string.IsNullOrEmpty(this.txt_name.Text))
        {
            ShowMsg("請輸入 設備名稱");
            feedback = false;
        }
        else if (!checkobj.IsValidLen(this.txt_name.Text.Trim(), 40))
        {
            ShowMsg("設備名稱 長度不可超過40個字");
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
        #region 保管人
        if (this.DepartTreeTextBox1.Items.Count <= 0 || this.DepartTreeTextBox1.Items == null)
        {
            ShowMsg("請選擇 保管人");
            feedback = false;
        }
        #endregion
        #region 保管人電話&分機
        if (string.IsNullOrEmpty(this.txt_tel.Text))
        {
            ShowMsg("請輸入 保管人電話");
            feedback = false;
        }
        else if (!checkobj.IsValidLen(this.txt_tel.Text.Trim(), 20))
        {
            ShowMsg("保管人電話 長度不可超過20個數字");
            feedback = false;
        }
        if (!checkobj.IsValidLen(this.txt_ext.Text.Trim(), 10))
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
        if (!checkobj.IsValidLen(this.txt_describe.Text.Trim(), 200))
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
        string aMSG = "";
        try
        {
            #region equipments
            EquipmentsDAO equDAO = new EquipmentsDAO();
            equipments newRow = equDAO.GetByNo(Convert.ToInt32(this.lab_no.Text));
            
            newRow.equ_createtime = System.DateTime.Now;
            newRow.equ_createuid = Convert.ToInt32(sobj.sessionUserID);
            newRow.equ_pic = null;
            newRow.equ_pictype = "";
            equDAO.Update();
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
                    string sqlstr = "select equ_no from equipments where equ_number='" + this.txt_number.Text + "' and equ_status='1' and equ_no<>" + this.lab_no.Text;
                    DataTable dt = new DataTable();
                    dt = dbo.ExecuteQuery(sqlstr);
                    if (dt.Rows.Count > 0)
                    {
                        ShowMsg("此 資產編號[" + this.txt_number.Text + "] 已存在");
                        return;
                    }
                    else
                    {
                        #region equipments
                        EquipmentsDAO equDAO = new EquipmentsDAO();
                        equipments newRow = equDAO.GetByNo(Convert.ToInt32(this.lab_no.Text));
                        newRow.spo_no = Convert.ToInt32(this.ddl_spot.SelectedValue);
                        newRow.equ_createtime = System.DateTime.Now;
                        newRow.equ_createuid = Convert.ToInt32(sobj.sessionUserID);
                        newRow.equ_descript = this.txt_describe.Text;
                        newRow.equ_etime = this.ddl_etime.SelectedValue;
                        newRow.equ_ext = this.txt_ext.Text;
                        newRow.equ_name = this.txt_name.Text;
                        newRow.equ_number = this.txt_number.Text;
                        if (this.ImageUpload1.HasFile)
                        {
                            newRow.equ_pic = this.ImageUpload1.GetFileBytes;
                            newRow.equ_pictype = this.ImageUpload1.GetExtension;
                        }
                        newRow.equ_stime = this.ddl_stime.SelectedValue;
                        newRow.equ_tel = this.txt_tel.Text;
                        if (this.DepartTreeTextBox1.Value.Length > 0)
                            newRow.peo_uid = Convert.ToInt32(this.DepartTreeTextBox1.Value);
                        else
                            newRow.peo_uid = 0;
                        equDAO.Update();
                        #endregion

                        //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
                        new OperatesObject().ExecuteOperates(301001, sobj.sessionUserID, 3, "編號：" + this.lab_no.Text + ",所在地：" + this.ddl_spot.SelectedItem.Text + ",設備名稱：" + this.txt_name.Text.Trim() + "...等");
                    }
                    #endregion
                }
                else
                {
                    #region 新增
                    string sqlstr = "select equ_no from equipments where equ_number='" + this.txt_number.Text + "' and equ_status='1'";
                    DataTable dt = new DataTable();
                    dt = dbo.ExecuteQuery(sqlstr);
                    if (dt.Rows.Count > 0)
                    {
                        ShowMsg("此 資產編號[" + this.txt_number.Text + "] 已存在");
                        return;
                    }
                    else
                    {
                        #region equipments
                        EquipmentsDAO equDAO = new EquipmentsDAO();
                        equipments newRow = new equipments();
                        newRow.spo_no = Convert.ToInt32(this.ddl_spot.SelectedValue);
                        newRow.equ_createtime = System.DateTime.Now;
                        newRow.equ_createuid = Convert.ToInt32(sobj.sessionUserID);
                        newRow.equ_descript = this.txt_describe.Text;
                        newRow.equ_etime = this.ddl_etime.SelectedValue;
                        newRow.equ_ext = this.txt_ext.Text;
                        newRow.equ_name = this.txt_name.Text;
                        newRow.equ_number = this.txt_number.Text;
                        if (this.ImageUpload1.HasFile)
                        {
                            newRow.equ_pic = this.ImageUpload1.GetFileBytes;
                            newRow.equ_pictype = this.ImageUpload1.GetExtension;
                        }
                        newRow.equ_status = "1";
                        newRow.equ_stime = this.ddl_stime.SelectedValue;
                        newRow.equ_tel = this.txt_tel.Text;
                        if (this.DepartTreeTextBox1.Value.Length > 0)
                            newRow.peo_uid = Convert.ToInt32(this.DepartTreeTextBox1.Value);
                        else
                            newRow.peo_uid = 0;
                        equDAO.AddEquipments(newRow);
                        equDAO.Update();
                        #endregion
                        int equ_no = newRow.equ_no;

                        //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
                        new OperatesObject().ExecuteOperates(301001, sobj.sessionUserID, 1, "所在地：" + this.ddl_spot.SelectedItem.Text + ",場地名稱：" + this.txt_name.Text.Trim() + "....等");
                    }
                    #endregion
                }
                //ShowDataList(); //呼叫列表

                Response.Write(PCalendarUtil.ShowMsg_URL("", "301001.aspx?count=" + new System.Random().Next(10000).ToString() + "&pageIndex=" + this.lab_pageIndex.Text));
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