using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using NXEIP.DAO;
using Entity;

public partial class _10_100400_100406_1 : System.Web.UI.Page
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
            Session["100406_value"] = null;
            //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
            new OperatesObject().ExecuteOperates(100406, sobj.sessionUserID, 2, "派車申請");
            if (Session["100406_1_CommandArgument"] != null)
            {
                string[] val = Session["100406_1_CommandArgument"].ToString().Split(',');
                this.lab_chekuan.Text = val[0];
                this.lab_car.Text = val[1];
                txtdate = val[2];
                txttime = val[3];

                if (val.Length == 5) this.lab_no.Text = val[4];
                #region 設備資料基本資料
                this.m02_chekuan.Text = new M01DAO().GetNameByNo(Convert.ToInt32(this.lab_chekuan.Text));
                string sqlstr = "SELECT m02_cc,m02_code,m02_peouid,m02_color,m02_mark,m02_memo,m02_number,m02_pictype FROM m02 WHERE (m02_no = " + this.lab_car.Text + ")";
                DataTable dt = new DataTable();
                dt = dbo.ExecuteQuery(sqlstr);
                if (dt.Rows.Count > 0)
                {
                    this.m02_cc.Text = dt.Rows[0]["m02_cc"].ToString();
                    this.m02_code.Text = dt.Rows[0]["m02_code"].ToString();
                    this.m02_peouid.Text = dt.Rows[0]["m02_peouid"].ToString();
                    this.m02_show.Text = new PeopleDAO().GetPeopleNameByUid(Convert.ToInt32(dt.Rows[0]["m02_peouid"].ToString()));
                    this.m02_color.Text = new M01DAO().GetNameByNo(Convert.ToInt32(dt.Rows[0]["m02_color"].ToString()));
                    this.m02_mark.Text = new M01DAO().GetNameByNo(Convert.ToInt32(dt.Rows[0]["m02_mark"].ToString()));
                    this.m02_memo.Text = dt.Rows[0]["m02_memo"].ToString();
                    this.m02_number.Text = dt.Rows[0]["m02_number"].ToString();
                    this.lab_etime.Text = "22:00";

                    #region 圖片
                    this.hl_pic1.Visible = false;
                    if (dt.Rows[0]["m02_pictype"].ToString().Trim().Length > 0)
                    {
                        this.hl_pic1.Visible = true;
                        this.hl_pic1.NavigateUrl = "../../lib/ShowPic.aspx?tb=m02&picorder=1&pkno=" + this.lab_car.Text;
                        this.hl_pic1.Attributes.Add("rel", "lytebox");
                        this.hl_pic1.Attributes.Add("title", "車輛圖片");
                        this.hl_pic1.Attributes.Add("alt", "車輛圖片");
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
                    Entity.m03 tbData = new M03DAO().GetByNo(Convert.ToInt32(this.lab_no.Text));
                    if (tbData != null)
                    {
                        DateTime m03_sdate = new DateTime();
                        DateTime m03_edate = new DateTime();
                        m03_sdate = tbData.m03_sdate.Value;
                        m03_edate = tbData.m03_edate.Value;

                        #region 申請日期
                        this.lab_today.Text = changeobj.ADDTtoROCDT(m03_sdate.ToString("yyyy-MM-dd"));
                        this.lab_week.Text = changeobj.ChangeWeek(m03_sdate.DayOfWeek);
                        this.lab_stime.Text = m03_sdate.ToString("HH:00");

                        #region 使用時限
                        DateTime stime1 = m03_sdate;
                        DateTime etime1 = Convert.ToDateTime(m03_sdate.ToString("yyyy/MM/dd") + " " + this.lab_etime.Text);
                        for (int i = 1; i <= (etime1.Hour - stime1.Hour); i++)
                        {
                            ListItem newitem = new ListItem(i.ToString(), i.ToString());
                            this.ddl_usehour.Items.Add(newitem);
                        }
                        #endregion

                        try
                        {
                            this.ddl_usehour.Items.FindByValue(tbData.m03_hour.Value.ToString()).Selected = true;
                        }
                        catch { }
                        #endregion

                        #region 登記者、借用單位、使用狀況、主持人、與會人數、承辦人電話、是否公開、申請事由
                        this.lab_applyuser.Text = new PeopleDAO().GetPeopleNameByUid(tbData.m03_peouid.Value);
                        this.lab_depart.Text = new DepartmentsDAO().GetByDepNo(tbData.m03_depno.Value).dep_name;
                        if (tbData.m03_verify.Equals("1"))
                            this.lab_apply.Text = "送審中";
                        else if (tbData.m03_verify.Equals("2"))
                            this.lab_apply.Text = "已審核";
                        this.txt_people.Text = tbData.m03_people.Value.ToString();
                        this.txt_place.Text = tbData.m03_place;
                        this.txt_reason.Text = tbData.m03_reason;
                        #endregion

                        #region 下面的按鈕可顯示多少個
                        if (!tbData.m03_peouid.Value.ToString().Equals(sobj.sessionUserID))
                        {
                            //非本人
                            this.btn_delete.Enabled = false;
                            this.btn_delete.Visible = false;
                        }
                        else
                        {
                            #region 本人
                            //是否過期
                            if (m03_edate < Convert.ToDateTime(System.DateTime.Now.ToString("yyyy/MM/dd HH:dd:00")))
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
        Session["100406_1_CommandArgument"] = null;
        Session["100406_value"] = this.lab_chekuan.Text + "," + this.lab_car.Text + "," + changeobj.ROCDTtoADDT(this.lab_today.Text);
        Response.Redirect("100406.aspx?count=" + new System.Random().Next(10000).ToString());
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
        #region 褡乘人數
        if (this.txt_people.Text.Trim().Length <= 0)
        {
            ShowMSG("請輸入 褡乘人數");
            return false;
        }
        else
        {
            try
            {
                int tmp = Convert.ToInt32(this.txt_people.Text);
            }
            catch
            {
                ShowMSG("登記者電話 長度為20碼數字");
                return false;
            }
        }
        #endregion
        #region 目的地
        if (this.txt_place.Text.Trim().Length <= 0)
        {
            ShowMSG("請輸入 目的地");
            return false;
        }
        else if (this.txt_place.Text.Trim().Length > 100)
        {
            ShowMSG("目的地 長度為100個中文字");
            return false;
        }
        #endregion
        #region 用車事由
        if (this.txt_reason.Text.Trim().Length <= 0)
        {
            ShowMSG("請輸入 用車事由");
            return false;
        }
        else if (this.txt_reason.Text.Trim().Length > 100)
        {
            ShowMSG("用車事由 長度為100個中文字");
            return false;
        }
        #endregion

        #region 判斷是否重複
        string m03_sdate = changeobj.ROCDTtoADDT(this.lab_today.Text) + " " + this.lab_stime.Text;
        string m03_edate = Convert.ToDateTime(changeobj.ROCDTtoADDT(this.lab_today.Text) + " " + this.lab_stime.Text).AddHours(Convert.ToInt32(this.ddl_usehour.SelectedValue)).ToString("yyyy/MM/dd HH:mm:ss");
        string sqlstr = "select m03_no from m03 where (m03_m02no = " + this.lab_car.Text + ") and (m03_verify in ('1', '2')) and ((m03_sdate<='" + m03_edate + "' and  m03_edate<='" + m03_edate + "' and m03_edate>'" + m03_sdate + "') or (m03_sdate>='" + m03_sdate + "' and m03_edate>='" + m03_sdate + "' and m03_sdate<'" + m03_edate + "') or (m03_sdate<'" + m03_sdate + "' and m03_edate>'" + m03_edate + "'))";
        //string sqlstr = "select m03_no from m03 where (m03_m02no = " + this.lab_car.Text + ") and (m03_verify in ('1', '2')) and (m03_sdate <= '" + changeobj.ROCDTtoADDT(this.lab_today.Text) + " " + this.lab_stime.Text + "') and (m03_edate >= '" + Convert.ToDateTime(changeobj.ROCDTtoADDT(this.lab_today.Text) + " " + this.lab_stime.Text).AddHours(Convert.ToInt32(this.ddl_usehour.SelectedValue)).ToString("yyyy/MM/dd HH:mm:ss") + "')";
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
            if (dbo.GetArguments("Car_BorrowsSignType") != null)
                this.lab_BorrowsSignType.Text = dbo.GetArguments("Car_BorrowsSignType");
            else
                this.lab_BorrowsSignType.Text = "1";
            #endregion

            if (CheckInputValue())
            {
                M03DAO tbDAO = new M03DAO();
                m03 newRow = new m03();
                newRow.m03_applydate = System.DateTime.Now;
                newRow.m03_applydepno = Convert.ToInt32(sobj.sessionUserDepartID);
                newRow.m03_applyuid = Convert.ToInt32(sobj.sessionUserID);
                newRow.m03_createtime = System.DateTime.Now;
                newRow.m03_createuid = Convert.ToInt32(sobj.sessionUserID);
                newRow.m03_depno = Convert.ToInt32(sobj.sessionUserDepartID);
                newRow.m03_driver = "1";
                newRow.m03_driveruid = 0;
                newRow.m03_edate = Convert.ToDateTime(changeobj.ROCDTtoADDT(this.lab_today.Text) + " " + this.lab_stime.Text).AddHours(Convert.ToInt32(this.ddl_usehour.SelectedValue));
                newRow.m03_hour = Convert.ToInt32(this.ddl_usehour.SelectedValue);
                newRow.m03_m02no = Convert.ToInt32(this.lab_car.Text);
                newRow.m03_night = "1";
                newRow.m03_people = Convert.ToInt32(this.txt_people.Text);
                newRow.m03_peouid = Convert.ToInt32(sobj.sessionUserID);
                newRow.m03_place = this.txt_place.Text;
                newRow.m03_reason= this.txt_reason.Text;
                newRow.m03_sdate=Convert.ToDateTime(changeobj.ROCDTtoADDT(this.lab_today.Text) + " " + this.lab_stime.Text);
                newRow.m03_type=Convert.ToInt32(this.lab_chekuan.Text);
                if (this.lab_BorrowsSignType.Text.Equals("1"))
                    newRow.m03_verify = "2";
                else
                    newRow.m03_verify = "1";
                tbDAO.AddM03(newRow);
                tbDAO.Update();

                this.lab_no.Text = newRow.m03_no.ToString();

                #region 發布訊息
                if (!this.lab_BorrowsSignType.Text.Equals("1"))
                {
                    DateTime sdate = Convert.ToDateTime(changeobj.ROCDTtoADDT(this.lab_today.Text) + " " + this.lab_stime.Text);
                    DateTime edate = Convert.ToDateTime(changeobj.ROCDTtoADDT(this.lab_today.Text) + " " + this.lab_stime.Text).AddHours(Convert.ToInt32(this.ddl_usehour.SelectedValue));

                    //人事室王小明於100-03-12 12:00-14:00 申請派用車號「xxx」，請進入派車審核作業進行審核
                    string smsg_title = this.lab_depart.Text + sobj.sessionUserName + "於 " + sdate.ToString("yyyy-MM-dd HH:mm") + " ~ " + edate.ToString("HH:ss") + " 申請派用車號「" + this.m02_number + "」";
                    string smsg_bodys = this.lab_depart.Text + sobj.sessionUserName + "於 " + sdate.ToString("yyyy-MM-dd HH:mm") + " ~ " + edate.ToString("HH:ss") + " 申請派用車號「" + this.m02_number + "」，請進入派車審核作業進行審核";
                    new PersonalMessageUtil().SendMessage(smsg_title, smsg_bodys, "", Convert.ToInt32(this.m02_peouid.Text), Convert.ToInt32(sobj.sessionUserID), true, false, false);
                }
                #endregion

                //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
                new OperatesObject().ExecuteOperates(100406, sobj.sessionUserID, 1, "確定派車申請，編號:" + this.lab_no.Text);

                Session["100406_1_CommandArgument"] = null;
                Session["100406_value"] = this.lab_chekuan.Text + "," + this.lab_car.Text + "," + changeobj.ROCDTtoADDT(this.lab_today.Text);
                Response.Redirect("100406.aspx?count=" + new System.Random().Next(10000).ToString());
            }
        }
        catch (Exception ex)
        {
            aMSG = "功能名稱:"+this.Navigator1.SubFunc+"-確定申請<br>錯誤訊息:" + ex.ToString();
            Response.Write(aMSG);
        }
    }
    #endregion

    #region 取消申請
    protected void btn_delete_Click(object sender, EventArgs e)
    {
        string sqlstr = "update m03 set m03_verify='4',m03_applyuid=" + sobj.sessionUserID + ",m03_applydate=getdate() where m03_no=" + this.lab_no.Text;
        dbo.ExecuteNonQuery(sqlstr);

        //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
        new OperatesObject().ExecuteOperates(100406, sobj.sessionUserID, 4, "取消派車申請，編號:" + this.lab_no.Text);

        Session["100406_1_CommandArgument"] = null;
        Session["100406_value"] = this.lab_chekuan.Text + "," + this.lab_car.Text + "," + changeobj.ROCDTtoADDT(this.lab_today.Text);
        Response.Redirect("100406.aspx?count=" + new System.Random().Next(10000).ToString());
    }
    #endregion
}