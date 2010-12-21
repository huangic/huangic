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
/// 功能名稱：管理作業 / 問卷管理 / 問卷維護--新增、修改
/// 功能編號：30/300200/300201
/// 撰寫者：Lina
/// 撰寫時間：2010/12/20
/// </summary>
public partial class _30_300200_300201_1 : System.Web.UI.Page
{
    DBObject dbo = new DBObject();
    CheckObject checkobj = new CheckObject();
    SessionObject sobj = new SessionObject();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if (Request["mode"] != null) this.lab_mode.Text = Request["mode"];
            if (Request["no"] != null) this.lab_no.Text = Request["no"];

            #region 初始值
            if (this.lab_mode.Text.Equals("modify"))
            {
                this.Navigator1.SubFunc = "修改";
                Entity.questionary queData = new QuestionaryDAO().GetByNo(Convert.ToInt32(this.lab_no.Text));
                if (queData != null)
                {
                    this.txt_name.Text = queData.que_name;
                    this.txt_descript.Text = queData.que_descript;
                    this.txt_end.Text = queData.que_end;
                    this.rbl_register.Items.FindByValue(queData.que_register).Selected = true;
                    this.rbl_open.Items.FindByValue(queData.que_open).Selected = true;
                    this.rbl_status.Items.FindByValue(queData.que_status).Selected = true;
                    this.cl_sdate._ADDate = Convert.ToDateTime(queData.que_sdate.Value.ToString("yyyy/MM/dd"));
                    this.cl_edate._ADDate = Convert.ToDateTime(queData.que_edate.Value.ToString("yyyy/MM/dd"));
                    this.txt_shour.Text = queData.que_sdate.Value.ToString("HH");
                    this.txt_ehour.Text = queData.que_edate.Value.ToString("HH");
                    this.txt_smin.Text = queData.que_sdate.Value.ToString("mm");
                    this.txt_emin.Text = queData.que_edate.Value.ToString("mm");
                }
            }
            else if (this.lab_mode.Text.Equals("copy"))
            {
                this.Navigator1.SubFunc = "複製";
                Entity.questionary queData = new QuestionaryDAO().GetByNo(Convert.ToInt32(this.lab_no.Text));
                if (queData != null)
                {
                    this.txt_name.Text = queData.que_name;
                    this.txt_descript.Text = queData.que_descript;
                    this.txt_end.Text = queData.que_end;
                    this.rbl_register.Items.FindByValue(queData.que_register).Selected = true;
                    this.rbl_open.Items.FindByValue(queData.que_open).Selected = true;
                    this.rbl_status.Items.FindByValue(queData.que_status).Selected = true;
                    this.cl_sdate._ADDate = Convert.ToDateTime(queData.que_sdate.Value.ToString("yyyy/MM/dd"));
                    this.cl_edate._ADDate = Convert.ToDateTime(queData.que_edate.Value.ToString("yyyy/MM/dd"));
                    this.txt_shour.Text = queData.que_sdate.Value.ToString("HH");
                    this.txt_ehour.Text = queData.que_edate.Value.ToString("HH");
                    this.txt_smin.Text = queData.que_sdate.Value.ToString("mm");
                    this.txt_emin.Text = queData.que_edate.Value.ToString("mm");
                }
            }
            else
            {
                this.Navigator1.SubFunc = "新增";
                this.rbl_open.Items[0].Selected = true;
                this.rbl_register.Items[0].Selected = true;
                this.rbl_status.Items[0].Selected = true;
            }
            #endregion
        }
    }

    private bool CheckInputValue()
    {
        #region 輸入值檢查--問卷名稱
        if (string.IsNullOrEmpty(this.txt_name.Text))
        {
            ShowMSG("請輸入 問卷名稱");
            return false;
        }
        else if (!checkobj.IsValidLen(this.txt_name.Text.Trim(), 200))
        {
            ShowMSG("問卷名稱 長度不可超過200個數文字");
            return false;
        }
        #endregion
        #region 輸入值檢查--問卷說明
        if (string.IsNullOrEmpty(this.txt_descript.Text))
        {
            ShowMSG("請輸入 問卷說明");
            return false;
        }
        else if (!checkobj.IsValidLen(this.txt_descript.Text.Trim(), 500))
        {
            ShowMSG("問卷說明 長度不可超過500個數文字");
            return false;
        }
        #endregion
        #region 輸入值檢查--卷尾說明
        if (string.IsNullOrEmpty(this.txt_end.Text))
        {
            ShowMSG("請輸入 卷尾說明");
            return false;
        }
        else if (!checkobj.IsValidLen(this.txt_end.Text.Trim(), 500))
        {
            ShowMSG("卷尾說明 長度不可超過500個數文字");
            return false;
        }
        #endregion

        #region 調查時間
        if (this.cl_sdate._ADDate == null || this.cl_edate._ADDate == null)
        {
            ShowMSG("請選擇開始時間與結束時間");
            return false;
        }
        if (this.txt_shour.Text.Length != 2 || this.txt_smin.Text.Length != 2 || this.txt_ehour.Text.Length != 2 || this.txt_emin.Text.Length != 2)
        {
            ShowMSG("時間格式錯誤!!");
            return false;
        }
        DateTime sdate = new DateTime();
        DateTime edate = new DateTime();
        try
        {
            sdate = Convert.ToDateTime(this.cl_sdate._ADDate.ToString("yyyy/MM/dd") + " " + this.txt_shour.Text + ":" + this.txt_smin.Text);
        }
        catch
        {
            ShowMSG("開始時間錯誤!!");
            return false;
        }
        try
        {
            edate = Convert.ToDateTime(this.cl_edate._ADDate.ToString("yyyy/MM/dd") + " " + this.txt_ehour.Text + ":" + this.txt_emin.Text);
        }
        catch
        {
            ShowMSG("結束時間錯誤!!");
            return false;
        }

        if (sdate > edate)
        {
            ShowMSG("結束時間不得小於開始時間");
            return false;
        }
        #endregion
        return true;
    }

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
                    QuestionaryDAO queDAO1 = new QuestionaryDAO();
                    questionary newRow = queDAO1.GetByNo(Convert.ToInt32(this.lab_no.Text));
                    newRow.que_name = this.txt_name.Text;
                    newRow.que_descript = this.txt_descript.Text;
                    newRow.que_end = this.txt_end.Text;
                    newRow.que_register = this.rbl_register.SelectedValue;
                    newRow.que_open = this.rbl_open.SelectedValue;
                    newRow.que_status=this.rbl_status.SelectedValue;
                    newRow.que_sdate = Convert.ToDateTime(this.cl_sdate._ADDate.ToString("yyyy/MM/dd") + " " + this.txt_shour.Text + ":" + this.txt_smin.Text);
                    newRow.que_edate = Convert.ToDateTime(this.cl_edate._ADDate.ToString("yyyy/MM/dd") + " " + this.txt_ehour.Text + ":" + this.txt_emin.Text);
                    newRow.que_createtime = System.DateTime.Now;
                    newRow.que_createuid = Convert.ToInt32(sobj.sessionUserID);
                    queDAO1.Update();
                   
                    msg = "修改成功";
                    //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
                    new OperatesObject().ExecuteOperates(300201, sobj.sessionUserID, 3, "編號：" + this.lab_no.Text + ",問卷名稱：" + this.txt_name.Text.Trim());
                    #endregion
                }
                else if (this.lab_mode.Text.Equals("copy"))
                {
                    #region 複製
                    //加一筆新的主檔
                    QuestionaryDAO queDAO1 = new QuestionaryDAO();
                    questionary newRow = new questionary();
                    newRow.que_name = this.txt_name.Text;
                    newRow.que_descript = this.txt_descript.Text;
                    newRow.que_end = this.txt_end.Text;
                    newRow.que_register = this.rbl_register.SelectedValue;
                    newRow.que_open = this.rbl_open.SelectedValue;
                    newRow.que_status = this.rbl_status.SelectedValue;
                    newRow.que_sdate = Convert.ToDateTime(this.cl_sdate._ADDate.ToString("yyyy/MM/dd") + " " + this.txt_shour.Text + ":" + this.txt_smin.Text);
                    newRow.que_edate = Convert.ToDateTime(this.cl_edate._ADDate.ToString("yyyy/MM/dd") + " " + this.txt_ehour.Text + ":" + this.txt_emin.Text);
                    newRow.que_createtime = System.DateTime.Now;
                    newRow.que_createuid = Convert.ToInt32(sobj.sessionUserID);
                    queDAO1.AddQuestionary(newRow);
                    queDAO1.Update();

                    int que_no = newRow.que_no; //更換成新的編號

                    string sqlstr = "insert into theme select " + que_no + ",the_no,the_name,the_type,the_order,the_count,the_fraction,the_status,the_createuid,the_createtime from theme where que_no=" + this.lab_no.Text;
                    dbo.ExecuteNonQuery(sqlstr);
                    sqlstr = "insert into answers select " + que_no + ",the_no,ans_no,ans_name,ans_order,ans_fraction,ans_status,ans_createuid,ans_createtime from answers where que_no=" + this.lab_no.Text + " and ans_status='1'";
                    dbo.ExecuteNonQuery(sqlstr);

                    msg = "複製成功";
                    //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
                    new OperatesObject().ExecuteOperates(300401, sobj.sessionUserID, 1, "複製問卷, 名稱：" + this.txt_name.Text.Trim());
                    #endregion
                }
                else
                {
                    #region 新增
                    QuestionaryDAO queDAO1 = new QuestionaryDAO();
                    questionary newRow = new questionary();
                    newRow.que_name = this.txt_name.Text;
                    newRow.que_descript = this.txt_descript.Text;
                    newRow.que_end = this.txt_end.Text;
                    newRow.que_register = this.rbl_register.SelectedValue;
                    newRow.que_open = this.rbl_open.SelectedValue;
                    newRow.que_status = this.rbl_status.SelectedValue;
                    newRow.que_sdate = Convert.ToDateTime(this.cl_sdate._ADDate.ToString("yyyy/MM/dd") + " " + this.txt_shour.Text + ":" + this.txt_smin.Text);
                    newRow.que_edate = Convert.ToDateTime(this.cl_edate._ADDate.ToString("yyyy/MM/dd") + " " + this.txt_ehour.Text + ":" + this.txt_emin.Text);
                    newRow.que_createtime = System.DateTime.Now;
                    newRow.que_createuid = Convert.ToInt32(sobj.sessionUserID);
                    queDAO1.AddQuestionary(newRow);
                    queDAO1.Update();

                    msg = "新增成功";
                    //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
                    new OperatesObject().ExecuteOperates(300401, sobj.sessionUserID, 1, "問卷名稱：" + this.txt_name.Text.Trim());
                    #endregion
                }

                this.Page.ClientScript.RegisterStartupScript(typeof(_30_300200_300201_1), "closeThickBox", "self.parent.update('" + msg + "');", true);
            }
        }
        catch (Exception ex)
        {
            aMSG = "功能名稱：問卷管理-" + this.Navigator1.SubFunc + "<br>錯誤訊息:" + ex.ToString();
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