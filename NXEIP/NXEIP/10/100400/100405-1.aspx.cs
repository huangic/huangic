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

public partial class _10_100400_100405_1 : System.Web.UI.Page
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
            Session["100405_value"] = null;
            //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
            new OperatesObject().ExecuteOperates(100405, sobj.sessionUserID, 2, "設備借用");
            if (Session["100405_1_CommandArgument"] != null)
            {
                string[] val = Session["100405_1_CommandArgument"].ToString().Split(',');
                this.lab_spot.Text = val[0];
                this.lab_equ.Text = val[1];
                txtdate = val[2];
                txttime = val[3];

                if (val.Length == 5) this.lab_no.Text = val[4];

                #region 設備資料基本資料
                this.lab_sponame.Text = new SpotDAO().GetNameBySpoNo(Convert.ToInt32(this.lab_spot.Text));
                string sqlstr = "SELECT *  FROM equipments WHERE (equ_no = " + this.lab_equ.Text + ")";
                DataTable dt = new DataTable();
                dt = dbo.ExecuteQuery(sqlstr);
                if (dt.Rows.Count > 0)
                {
                    this.lab_number.Text = dt.Rows[0]["equ_number"].ToString();
                    this.lab_equname.Text = dt.Rows[0]["equ_name"].ToString();
                    this.lab_peouid.Text = new PeopleDAO().GetPeopleNameByUid(Convert.ToInt32(dt.Rows[0]["peo_uid"].ToString()));
                    this.lab_tel.Text = dt.Rows[0]["equ_tel"].ToString();
                    this.lab_ext.Text = dt.Rows[0]["equ_ext"].ToString();
                    this.lab_descript.Text = dt.Rows[0]["equ_descript"].ToString();
                    this.lab_usetime.Text = dt.Rows[0]["equ_stime"].ToString() + " ～ " + dt.Rows[0]["equ_etime"].ToString();
                    this.lab_etime.Text = dt.Rows[0]["equ_etime"].ToString();

                    #region 圖片
                    this.hl_pic1.Visible = false;
                    if (dt.Rows[0]["equ_pictype"].ToString().Trim().Length > 0)
                    {
                        this.hl_pic1.Visible = true;
                        this.hl_pic1.NavigateUrl = "../../lib/ShowPic.aspx?tb=equipments&picorder=1&pkno=" + this.lab_equ.Text;
                        this.hl_pic1.Attributes.Add("rel", "lytebox");
                        this.hl_pic1.Attributes.Add("title", "設備圖片");
                        this.hl_pic1.Attributes.Add("alt", "設備圖片");
                        this.hl_pic1.Attributes.Add("OnClick", "return false;");
                        this.hl_pic1.Attributes.Add("OnLoad", "return true;");
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
                    Entity.borrows sorData = new BorrowsDAO().GetByNo(Convert.ToInt32(this.lab_no.Text));
                    if (sorData != null)
                    {
                        DateTime bor_stime = new DateTime();
                        DateTime bor_etime = new DateTime();
                        bor_stime = sorData.bor_stime.Value;
                        bor_etime = sorData.bor_etime.Value;

                        #region 申請日期
                        this.lab_today.Text = changeobj.ADDTtoROCDT(bor_stime.ToString("yyyy-MM-dd"));
                        this.lab_week.Text = changeobj.ChangeWeek(bor_stime.DayOfWeek);
                        this.lab_stime.Text = bor_stime.ToString("HH:00");

                        #region 使用時限
                        DateTime stime1 = bor_stime;
                        DateTime etime1 = Convert.ToDateTime(bor_stime.ToString("yyyy/MM/dd") + " " + this.lab_etime.Text);
                        for (int i = 1; i <= (etime1.Hour - stime1.Hour); i++)
                        {
                            ListItem newitem = new ListItem(i.ToString(), i.ToString());
                            this.ddl_usehour.Items.Add(newitem);
                        }
                        #endregion

                        int usehour = bor_etime.Hour - bor_stime.Hour;
                        try
                        {
                            this.ddl_usehour.Items.FindByValue(usehour.ToString()).Selected = true;
                        }
                        catch { }
                        #endregion

                        #region 登記者、借用單位、使用狀況、主持人、與會人數、承辦人電話、是否公開、申請事由
                        this.lab_applyuser.Text = new PeopleDAO().GetPeopleNameByUid(sorData.bor_applyuid.Value);
                        this.lab_depart.Text = new DepartmentsDAO().GetByDepNo(sorData.bor_depno.Value).dep_name;
                        if (sorData.bor_apply.Equals("1"))
                            this.lab_apply.Text = "送審中";
                        else if (sorData.bor_apply.Equals("2"))
                            this.lab_apply.Text = "已審核";
                        this.txt_tel.Text = sorData.bor_tel;
                        this.txt_reason.Text = sorData.bor_reason;
                        #endregion

                        #region 下面的按鈕可顯示多少個
                        if (!sorData.bor_applyuid.Value.ToString().Equals(sobj.sessionUserID))
                        {
                            //非本人
                            this.btn_delete.Enabled = false;
                            this.btn_delete.Visible = false;
                        }
                        else
                        {
                            #region 本人
                            //是否過期
                            if (bor_etime < Convert.ToDateTime(System.DateTime.Now.ToString("yyyy/MM/dd HH:dd:00")))
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

        this.btn_apply.Attributes["onclick"] = "javascript:this.disabled=true;" + this.Page.ClientScript.GetPostBackEventReference(this.btn_apply, "");
        this.btn_delete.Attributes["onclick"] = "javascript:this.disabled=true;" + this.Page.ClientScript.GetPostBackEventReference(this.btn_delete, "");
        this.btn_goback.Attributes["onclick"] = "javascript:this.disabled=true;" + this.Page.ClientScript.GetPostBackEventReference(this.btn_goback, "");
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
        Session["100405_1_CommandArgument"] = null;
        Session["100405_value"] = this.lab_spot.Text + "," + this.lab_equ.Text + "," + changeobj.ROCDTtoADDT(this.lab_today.Text);
        Response.Redirect("100405.aspx?count=" + new System.Random().Next(10000).ToString());
    }
    #endregion

    #region 檢查輸入值
    private bool CheckInputValue()
    {
        #region 使用時數
        if (this.ddl_usehour.SelectedValue.Equals("0"))
        {
            ShowMSG("請選擇 使用時數");
            return false;
        }
        #endregion
        #region 登記者電話
        if (this.txt_tel.Text.Trim().Length <= 0)
        {
            ShowMSG("請輸入 登記者電話");
            return false;
        }
        else if (this.txt_tel.Text.Trim().Length > 20)
        {
            ShowMSG("登記者電話 長度為20碼數字");
            return false;
        }
        #endregion

        #region 事由
        if (this.txt_reason.Text.Trim().Length <= 0)
        {
            ShowMSG("請輸入 申請事由");
            return false;
        }
        else if (this.txt_reason.Text.Trim().Length > 200)
        {
            ShowMSG("申請事由 長度為200個中文字");
            return false;
        }
        #endregion

        #region 判斷是否重複
        string bor_stime = changeobj.ROCDTtoADDT(this.lab_today.Text) + " " + this.lab_stime.Text;
        string bor_etime = Convert.ToDateTime(changeobj.ROCDTtoADDT(this.lab_today.Text) + " " + this.lab_stime.Text).AddHours(Convert.ToInt32(this.ddl_usehour.SelectedValue)).ToString("yyyy/MM/dd HH:mm:ss");
        string sqlstr = "select bor_no from borrows where (equ_no = " + this.lab_equ.Text + ") and (bor_apply in ('1', '2')) and ((bor_stime<='" + bor_etime + "' and  bor_etime<='" + bor_etime + "' and bor_etime>'" + bor_stime + "') or (bor_stime>='" + bor_stime + "' and bor_etime>='" + bor_stime + "' and bor_stime<'" + bor_etime + "') or (bor_stime<'" + bor_stime + "' and bor_etime>'" + bor_etime + "'))";
        //string sqlstr = "select bor_no from  borrows where (equ_no = " + this.lab_equ.Text + ") and (bor_apply in ('1', '2')) and (bor_stime <= '" + changeobj.ROCDTtoADDT(this.lab_today.Text) + " " + this.lab_stime.Text + "') and (bor_etime >= '" + Convert.ToDateTime(changeobj.ROCDTtoADDT(this.lab_today.Text) + " " + this.lab_stime.Text).AddHours(Convert.ToInt32(this.ddl_usehour.SelectedValue)).ToString("yyyy/MM/dd HH:mm:ss") + "')";
        DataTable dt = new DataTable();
        dt = dbo.ExecuteQuery(sqlstr);
        if (dt.Rows.Count > 0)
        {
            ShowMSG("此時間已有人申請借用");
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
            #region 取得「場地申請核可方式」:1表不需審核，直接核可(不可重覆);2表需審核，(不可重覆)
            if (dbo.GetArguments("Equipments_BorrowsSignType") != null)
                this.lab_BorrowsSignType.Text = dbo.GetArguments("Equipments_BorrowsSignType");
            else
                this.lab_BorrowsSignType.Text = "1";
            #endregion

            if (CheckInputValue())
            {
                BorrowsDAO borDAO = new BorrowsDAO();
                borrows newRow = new borrows();
                newRow.equ_no = Convert.ToInt32(this.lab_equ.Text);
                if (this.lab_BorrowsSignType.Text.Equals("1"))
                    newRow.bor_apply = "2";
                else
                    newRow.bor_apply = "1";
                newRow.bor_applydate = System.DateTime.Now;
                newRow.bor_applyuid = Convert.ToInt32(sobj.sessionUserID);
                newRow.bor_createtime = System.DateTime.Now;
                newRow.bor_createuid = Convert.ToInt32(sobj.sessionUserID);
                newRow.bor_depno = Convert.ToInt32(sobj.sessionUserDepartID);
                newRow.bor_etime = Convert.ToDateTime(changeobj.ROCDTtoADDT(this.lab_today.Text) + " " + this.lab_stime.Text).AddHours(Convert.ToInt32(this.ddl_usehour.SelectedValue));
                newRow.bor_reason = this.txt_reason.Text;
                newRow.bor_stime = Convert.ToDateTime(changeobj.ROCDTtoADDT(this.lab_today.Text) + " " + this.lab_stime.Text);
                newRow.bor_tel = this.txt_tel.Text;
                borDAO.AddBorrows(newRow);
                borDAO.Update();

                this.lab_no.Text = newRow.bor_no.ToString();

                #region 發布訊息
                if (!this.lab_BorrowsSignType.Text.Equals("1"))
                {
                    string sqlstr = "select people.peo_email,people.peo_uid from checker inner join people on checker.che_peouid = people.peo_uid inner join types on people.peo_jobtype = types.typ_no"
                        + " where (checker.roo_no = " + this.lab_equ.Text + ") and (types.typ_code = 'work') and (types.typ_number = '1') ";
                    DataTable dt = new DataTable();
                    dt = dbo.ExecuteQuery(sqlstr);
                    string subject = sobj.sessionUserName + "於 " + this.lab_today.Text + " " + this.lab_stime.Text + " ~ " + Convert.ToDateTime(changeobj.ROCDTtoADDT(this.lab_today.Text) + " " + this.lab_stime.Text).AddHours(Convert.ToInt32(this.ddl_usehour.SelectedValue)).ToString("HH:mm") + " 預約「" + this.lab_equname.Text + "」，請至設備審核功能進行審核作業";
                    string body = sobj.sessionUserName + "於 " + this.lab_today.Text + " " + this.lab_stime.Text + " ~ " + Convert.ToDateTime(changeobj.ROCDTtoADDT(this.lab_today.Text) + " " + this.lab_stime.Text).AddHours(Convert.ToInt32(this.ddl_usehour.SelectedValue)).ToString("HH:mm") + " 預約「" + this.lab_equname.Text + "」，請至設備審核功能進行審核作業";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        MyMessageUtil.send(subject, Convert.ToInt32(dt.Rows[i]["peo_uid"].ToString()), body, "", "", EIPGroup.EIP_Message_Equipment);
                    }
                }
                #endregion

                //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
                new OperatesObject().ExecuteOperates(100405, sobj.sessionUserID, 1, "確定設備借用，編號:" + this.lab_no.Text);

                Session["100405_1_CommandArgument"] = null;
                Session["100405_value"] = this.lab_spot.Text + "," + this.lab_equ.Text + "," + changeobj.ROCDTtoADDT(this.lab_today.Text);
                Response.Redirect("100405.aspx?count=" + new System.Random().Next(10000).ToString());
            }
        }
        catch (Exception ex)
        {
            aMSG = "功能名稱:設備借用--確定申請<br>錯誤訊息:" + ex.ToString();
            Response.Write(aMSG);
        }
    }
    #endregion

    #region 取消申請
    protected void btn_delete_Click(object sender, EventArgs e)
    {
        string sqlstr = "update borrows set bor_apply='4',bor_createuid=" + sobj.sessionUserID + ",bor_createtime=getdate() where bor_no=" + this.lab_no.Text;
        dbo.ExecuteNonQuery(sqlstr);

        //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
        new OperatesObject().ExecuteOperates(100405, sobj.sessionUserID, 4, "取消設備借用，編號:" + this.lab_no.Text);

        Session["100405_1_CommandArgument"] = null;
        Session["100405_value"] = this.lab_spot.Text + "," + this.lab_equ.Text + "," + changeobj.ROCDTtoADDT(this.lab_today.Text);
        Response.Redirect("100405.aspx?count=" + new System.Random().Next(10000).ToString());
    }
    #endregion
}