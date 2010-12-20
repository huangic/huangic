using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using NXEIP.DAO;
using Entity;
using NXEIP.MyGov;


public partial class _10_100400_100402_1 : System.Web.UI.Page
{
    ChangeObject changeobj = new ChangeObject();
    SessionObject sobj = new SessionObject();
    DBObject dbo = new DBObject();
    CheckObject checkobj = new CheckObject();
    string txtdate = "";
    string txttime = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            Session["100402_value"] = null;
            //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
            new OperatesObject().ExecuteOperates(100402, sobj.sessionUserID, 2, "場地申請");
            if (Session["100402_1_CommandArgument"] != null)
            {
                string[] val = Session["100402_1_CommandArgument"].ToString().Split(',');
                this.lab_spot.Text = val[0];
                this.lab_rooms.Text = val[1];
                txtdate = val[2];
                txttime = val[3];
                
                if (val.Length == 5) this.lab_no.Text = val[4];

                #region 場地資料基本資料
                this.lab_sponame.Text = new SpotDAO().GetNameBySpoNo(Convert.ToInt32(this.lab_spot.Text));
                string sqlstr = "SELECT *  FROM rooms WHERE (roo_no = " + this.lab_rooms.Text + ")";
                DataTable dt = new DataTable();
                dt = dbo.ExecuteQuery(sqlstr);
                if (dt.Rows.Count > 0)
                {
                    this.lab_floor.Text = dt.Rows[0]["roo_floor"].ToString() + " 樓";
                    this.lab_rooname.Text = dt.Rows[0]["roo_name"].ToString();
                    this.lab_oneuid.Text = new PeopleDAO().GetPeopleNameByUid(Convert.ToInt32(dt.Rows[0]["roo_oneuid"].ToString()));
                    this.lab_tel1.Text = dt.Rows[0]["roo_tel"].ToString();
                    this.lab_ext1.Text = dt.Rows[0]["roo_ext"].ToString();
                    this.lab_twouid.Text = new PeopleDAO().GetPeopleNameByUid(Convert.ToInt32(dt.Rows[0]["roo_twouid"].ToString()));
                    this.lab_tel2.Text = dt.Rows[0]["roo_twotel"].ToString();
                    this.lab_ext2.Text = dt.Rows[0]["roo_twoext"].ToString();
                    this.lab_human.Text = dt.Rows[0]["roo_human"].ToString() + " 人";
                    this.lab_describe.Text = dt.Rows[0]["roo_describe"].ToString();
                    this.lab_usetime.Text = dt.Rows[0]["roo_stime"].ToString() + " ～ " + dt.Rows[0]["roo_etime"].ToString();
                    this.lab_telephone.Text = dt.Rows[0]["roo_telephone"].ToString();
                    this.lab_etime.Text = dt.Rows[0]["roo_etime"].ToString();

                    #region 圖片
                    this.hl_pic1.Visible = false;
                    this.hl_pic2.Visible = false;
                    if (dt.Rows[0]["roo_pictype"].ToString().Trim().Length > 0)
                    {
                        this.hl_pic1.Visible = true;
                        this.hl_pic1.NavigateUrl = "../../lib/ShowPic.aspx?tb=rooms&picorder=1&pkno=" + this.lab_rooms.Text;
                        this.hl_pic1.Attributes.Add("rel", "lytebox");
                        this.hl_pic1.Attributes.Add("title", "場地圖片");
                        this.hl_pic1.Attributes.Add("alt", "場地圖片");
                        this.hl_pic1.Attributes.Add("OnClick", "return false;");
                        this.hl_pic1.Attributes.Add("OnLoad", "return true;");
                    }
                    if (dt.Rows[0]["roo_planetype"].ToString().Trim().Length > 0)
                    {
                        this.hl_pic2.Visible = true;
                        this.hl_pic2.NavigateUrl = "../../lib/ShowPic.aspx?tb=rooms&picorder=2&pkno=" + this.lab_rooms.Text;
                        this.hl_pic2.Attributes.Add("rel", "lytebox");
                        this.hl_pic2.Attributes.Add("title", "場地圖片");
                        this.hl_pic2.Attributes.Add("alt", "場地圖片");
                        this.hl_pic2.Attributes.Add("OnClick", "return false;");
                        this.hl_pic2.Attributes.Add("OnLoad", "return true;");
                    }
                    #endregion

                }
                #endregion

                if (this.lab_no.Text.Trim().Length <= 0)
                {
                    this.Navigator1.SubFunc = "新增";
                    this.btn_delete.Enabled = false;
                    this.btn_delete.Visible = false;

                    #region 新增
                    this.lab_today.Text = changeobj.ADDTtoROCDT(txtdate);
                    this.lab_week.Text = changeobj.ChangeWeek(Convert.ToDateTime(changeobj.ROCDTtoADDT(this.lab_today.Text)).DayOfWeek);
                    this.lab_stime.Text = txttime;
                    this.lab_applyuser.Text = sobj.sessionUserName;
                    this.lab_depart.Text = sobj.sessionUserDepartName;
                    this.rbl_open.Items[0].Selected = true;
                    this.cl_mdate._ADDate = Convert.ToDateTime(changeobj.ROCDTtoADDT(this.lab_today.Text));
                    #endregion

                    #region 使用時限
                    DateTime stime1 = Convert.ToDateTime(this.lab_today.Text + " " + this.lab_stime.Text);
                    DateTime etime1 = Convert.ToDateTime(this.lab_today.Text + " " + this.lab_etime.Text);
                    for (int i = 1; i <= (etime1.Hour - stime1.Hour); i++)
                    {
                        ListItem newitem = new ListItem(i.ToString(), i.ToString());
                        this.ddl_usehour.Items.Add(newitem);
                    }
                    #endregion
                }
                else
                {
                    this.Navigator1.SubFunc = "查看";
                    this.btn_apply.Enabled = false;
                    this.btn_apply.Visible = false;
                    #region 查看
                    Entity.petition sorData = new PetitionDAO().GetByPetNo(Convert.ToInt32(this.lab_no.Text));
                    if (sorData != null)
                    {
                        DateTime pet_stime = new DateTime();
                        DateTime pet_etime = new DateTime();
                        pet_stime = sorData.pet_stime.Value;
                        pet_etime = sorData.pet_etime.Value;

                        #region 申請日期
                        this.lab_today.Text = changeobj.ADDTtoROCDT(pet_stime.ToString("yyyy-MM-dd"));
                        this.lab_week.Text = changeobj.ChangeWeek(pet_stime.DayOfWeek);
                        this.lab_stime.Text = pet_stime.ToString("HH:00");

                        #region 使用時限
                        DateTime stime1 = pet_stime;
                        DateTime etime1 = Convert.ToDateTime(pet_stime.ToString("yyyy/MM/dd") + " " + this.lab_etime.Text);
                        for (int i = 1; i <= (etime1.Hour - stime1.Hour); i++)
                        {
                            ListItem newitem = new ListItem(i.ToString(), i.ToString());
                            this.ddl_usehour.Items.Add(newitem);
                        }
                        #endregion

                        int usehour = pet_etime.Hour - pet_stime.Hour;
                        try
                        {
                            this.ddl_usehour.Items.FindByValue(usehour.ToString()).Selected = true;
                        }
                        catch { }
                        #endregion

                        #region 登記者、借用單位、使用狀況、主持人、與會人數、承辦人電話、是否公開、申請事由
                        this.lab_applyuser.Text = new PeopleDAO().GetPeopleNameByUid(sorData.pet_applyuid.Value);
                        this.lab_depart.Text = new DepartmentsDAO().GetByDepNo(sorData.pet_depno.Value).dep_name;
                        if (sorData.pet_apply.Equals("1"))
                            this.lab_apply.Text = "送審中";
                        else if (sorData.pet_apply.Equals("2"))
                            this.lab_apply.Text = "已審核";
                        this.txt_host.Text = sorData.pet_host;
                        this.txt_count.Text = sorData.pet_count.Value.ToString();
                        this.txt_tel.Text = sorData.pet_tel;
                        this.rbl_open.Items.FindByValue(sorData.pet_open).Selected = true;
                        this.txt_reason.Text = sorData.pet_reason;
                        #endregion

                        #region 開會開始時間
                        this.cl_mdate._ADDate = sorData.pet_meeting.Value;
                        this.ddl_hour.Items.FindByValue(sorData.pet_meeting.Value.ToString("HH")).Selected = true;
                        this.ddl_min.Items.FindByValue(sorData.pet_meeting.Value.ToString("ss")).Selected = true;
                        #endregion

                        #region 下面的按鈕可顯示多少個
                        if (!sorData.pet_applyuid.Value.ToString().Equals(sobj.sessionUserID))
                        {
                            //非本人
                            this.btn_delete.Enabled = false;
                            this.btn_delete.Visible = false;
                        }
                        else
                        {
                            #region 本人
                            //是否過期
                            if (pet_etime < Convert.ToDateTime(System.DateTime.Now.ToString("yyyy/MM/dd HH:dd:00")))
                            {
                                this.btn_delete.Enabled = false;
                                this.btn_delete.Visible = false;
                            }
                            #endregion
                        }
                        #endregion
                    }
                    #endregion
                }
            }
        }
    }

    #region 顯示錯誤訊息
    private void ShowMSG(string msg)
    {
        string script = "<script>alert('" + msg + "');</script>";
        this.ClientScript.RegisterStartupScript(this.GetType(), "msg", script);
    }
    #endregion

    #region 回上一頁
    protected void btn_goback_Click(object sender, EventArgs e)
    {
        Session["100402_1_CommandArgument"] = null;
        Session["100402_value"] = this.lab_spot.Text + "," + this.lab_rooms.Text + "," + changeobj.ROCDTtoADDT(this.lab_today.Text);
        Response.Redirect("100402.aspx?count=" + new System.Random().Next(10000).ToString());
        //Response.Redirect("100402.aspx?spot=" + this.lab_spot.Text + "&rooms=" + this.lab_rooms.Text + "&today=" + changeobj.ROCDTtoADDT(this.lab_today.Text) + "&count=" + new System.Random().Next(10000).ToString());
    }
    #endregion

    #region 檢查輸入值
    private bool CheckInputValue()
    {
        #region 人事編號
        if (this.txt_host.Text.Trim().Length <= 0)
        {
            ShowMSG("請輸入 主持人");
            return false;
        }
        else if (this.txt_host.Text.Trim().Length > 30)
        {
            ShowMSG("主持人 長度為30個中文字");
            return false;
        }
        #endregion
        #region 與會人數
        if (this.txt_count.Text.Trim().Length <= 0)
        {
            ShowMSG("請輸入 與會人數");
            return false;
        }
        else
        {
            try
            {
                int tmp = Convert.ToInt32(this.txt_count.Text);
            }
            catch
            {
                ShowMSG("與會人數 需為數字");
                return false;
            }
        }
        #endregion
        #region 承辦人電話
        if (this.txt_tel.Text.Trim().Length <= 0)
        {
            ShowMSG("請輸入 承辦人電話");
            return false;
        }
        else if (this.txt_tel.Text.Trim().Length > 20)
        {
            ShowMSG("承辦人電話 長度為20碼數字");
            return false;
        }
        #endregion

        #region 開會開始時間
        if (this.cl_mdate._ADDate == null || this.cl_mdate._ADDate == null)
        {
            ShowMSG("請選擇 開會開始時間之日期");
            return false;
        }
        if (this.ddl_hour.SelectedValue.Equals("0"))
        {
            ShowMSG("請選擇 開會開始時間之時數");
            return false;
        }
        if (this.ddl_min.SelectedValue.Equals("0"))
        {
            ShowMSG("請選擇 開會開始時間之分鐘");
            return false;
        }
        #endregion

        #region 事由
        if (this.txt_reason.Text.Trim().Length > 200)
        {
            ShowMSG("申請事由 長度為200個中文字");
            return false;
        }
        #endregion

        return true;
    }
    #endregion

    #region 確定申請
    protected void btn_apply_Click(object sender, EventArgs e)
    {
        string aMSG = "";   //記錄錯誤訊息
        try
        {
            if (CheckInputValue())
            {
                #region 取得「申請核可方式」：1表不需審核，直接核可，但不可重覆;2表需審核，但不可重覆;3表需審核，但可重覆
                if (dbo.GetArguments("Rooms_PetitionSignType") != null)
                    this.lab_PetitionSignType.Text = dbo.GetArguments("Rooms_PetitionSignType");
                else
                    this.lab_PetitionSignType.Text = "1";
                #endregion

                PetitionDAO petDAO = new PetitionDAO();
                petition newRow = new petition();
                newRow.roo_no = Convert.ToInt32(this.lab_rooms.Text);
                if (this.lab_PetitionSignType.Text.Equals("1"))
                    newRow.pet_apply = "2";
                else
                    newRow.pet_apply = "1";
                newRow.pet_applydate = System.DateTime.Now;
                newRow.pet_applyuid = Convert.ToInt32(sobj.sessionUserID);
                newRow.pet_count = Convert.ToInt32(this.txt_count.Text);
                newRow.pet_createtime = System.DateTime.Now;
                newRow.pet_createuid = Convert.ToInt32(sobj.sessionUserID);
                newRow.pet_depno = Convert.ToInt32(sobj.sessionUserDepartID);
                newRow.pet_etime = Convert.ToDateTime(changeobj.ROCDTtoADDT(this.lab_today.Text) + " " + this.lab_stime.Text).AddHours(Convert.ToInt32(this.ddl_usehour.SelectedValue));
                newRow.pet_host = this.txt_host.Text;
                newRow.pet_meeting = Convert.ToDateTime(this.cl_mdate._ADDate.ToString("yyyy-MM-dd")+" "+this.ddl_hour.SelectedValue+":"+this.ddl_min.SelectedValue);
                newRow.pet_name = sobj.sessionUserName;
                newRow.pet_open = this.rbl_open.SelectedValue;
                newRow.pet_reason = this.txt_reason.Text;
                newRow.pet_stime = Convert.ToDateTime(changeobj.ROCDTtoADDT(this.lab_today.Text) + " " + this.lab_stime.Text);
                newRow.pet_tel = this.txt_tel.Text;
                petDAO.AddPetition(newRow);
                petDAO.Update();

                this.lab_no.Text = newRow.pet_no.ToString();

                #region 發布訊息
                if (!this.lab_PetitionSignType.Text.Equals("1"))
                {
                    string sqlstr = "select people.peo_email,people.peo_uid from checker inner join people on checker.che_peouid = people.peo_uid inner join types on people.peo_jobtype = types.typ_no"
                        + " where (checker.roo_no = " + this.lab_rooms.Text + ") and (types.typ_code = 'work') and (types.typ_number = '1') and (people.peo_email <> '')";
                    DataTable dt = new DataTable();
                    string subject = sobj.sessionUserName + "於 " + this.lab_today.Text + " " + this.lab_stime.Text + " ~ " + Convert.ToDateTime(changeobj.ROCDTtoADDT(this.lab_today.Text) + " " + this.lab_stime.Text).AddHours(Convert.ToInt32(this.ddl_usehour.SelectedValue)).ToString("HH:mm") + " 預約場地「" + this.lab_rooname.Text + "」";
                    string body = sobj.sessionUserName + "於 " + this.lab_today.Text + " " + this.lab_stime.Text + " ~ " + Convert.ToDateTime(changeobj.ROCDTtoADDT(this.lab_today.Text) + " " + this.lab_stime.Text).AddHours(Convert.ToInt32(this.ddl_usehour.SelectedValue)).ToString("HH:mm") + " 預約場地「" + this.lab_rooname.Text + "」";

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        MyMessageUtil.send(subject, Convert.ToInt32(dt.Rows[i]["peo_uid"].ToString()), body, "", "", EIPGroup.EIP_Todo_VerifyPlace);
                    }
                }
                #endregion

                //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
                new OperatesObject().ExecuteOperates(100402, sobj.sessionUserID, 1, "確定申請場地，編號:" + this.lab_no.Text);

                Session["100402_1_CommandArgument"] = null;
                Session["100402_value"] = this.lab_spot.Text + "," + this.lab_rooms.Text + "," + changeobj.ROCDTtoADDT(this.lab_today.Text);
                Response.Redirect("100402.aspx?count=" + new System.Random().Next(10000).ToString());
                //Response.Redirect("100402.aspx?spot=" + this.lab_spot.Text + "&rooms=" + this.lab_rooms.Text + "&today=" + changeobj.ROCDTtoADDT(this.lab_today.Text) + "&count=" + new System.Random().Next(10000).ToString());
            }
        }
        catch (Exception ex)
        {
            aMSG = "功能名稱:場地申請--確定申請<br>錯誤訊息:" + ex.ToString();
            Response.Write(aMSG);
        }
    }
    #endregion

    #region 取消申請
    protected void btn_delete_Click(object sender, EventArgs e)
    {
        string sqlstr = "update petition set pet_apply='4',pet_createuid=" + sobj.sessionUserID + ",pet_createtime=getdate() where pet_no=" + this.lab_no.Text;
        dbo.ExecuteNonQuery(sqlstr);

        //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
        new OperatesObject().ExecuteOperates(100402, sobj.sessionUserID, 4, "取消申請場地，編號:" + this.lab_no.Text);

        Session["100402_1_CommandArgument"] = null;
        Session["100402_value"] = this.lab_spot.Text + "," + this.lab_rooms.Text + "," + changeobj.ROCDTtoADDT(this.lab_today.Text);
        Response.Redirect("100402.aspx?count=" + new System.Random().Next(10000).ToString());
        //Response.Redirect("100402.aspx?spot=" + this.lab_spot.Text + "&rooms=" + this.lab_rooms.Text + "&today=" + changeobj.ROCDTtoADDT(this.lab_today.Text) + "&count=" + new System.Random().Next(10000).ToString());
    }
    #endregion
}