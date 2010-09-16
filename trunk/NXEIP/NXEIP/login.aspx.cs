using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.SessionState;

public partial class login : SessionObject
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.ImageButton1.Attributes.Add("onmouseout", "MM_swapImgRestore()");
            this.ImageButton1.Attributes.Add("onmouseover", "MM_swapImage('ImageButton1','','image/login-06-1.gif',1)");

            this.ImageButton2.Attributes.Add("onmouseout", "MM_swapImgRestore()");
            this.ImageButton2.Attributes.Add("onmouseover", "MM_swapImage('ImageButton2','','image/login-07-1.gif',1)");

            this.Image1.Attributes.Add("onmouseout", "MM_swapImgRestore()");
            this.Image1.Attributes.Add("onmouseover", "MM_swapImage('Image1','','image/login_ID.jpg',1)");

            CacheUtil.Clear();
        }
    }

    
    /// <summary>
    /// 使用者登入
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        this.CheckUI();
    }

    private void CheckUI()
    {
        if (this.CheckCode())
        {
            if (this.tbox_id.Text.Trim().Length > 0 && this.tbox_pw.Text.Trim().Length > 0)
            {
                string sql = "", peo_uid = "";
                
                using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection())
                {
                    conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["NXEIPConnectionString"].ConnectionString;
                    using (System.Data.SqlClient.SqlCommand com = new System.Data.SqlClient.SqlCommand())
                    {
                        sql = "select peo_uid from accounts where acc_login = @acc_login and acc_passwd = @acc_passwd and acc_status='1'";

                        com.Connection = conn;
                        com.Connection.Open();
                        com.CommandText = sql;
                        com.Parameters.Add("@acc_login", System.Data.SqlDbType.VarChar).Value = this.tbox_id.Text;
                        com.Parameters.Add("@acc_passwd", System.Data.SqlDbType.VarChar).Value = this.tbox_pw.Text;
                        try
                        {
                            peo_uid = com.ExecuteScalar().ToString();
                        }
                        catch { }
                        com.Connection.Close();

                    }
                }

                try
                {
                    if (Convert.ToInt32(peo_uid) > 0)
                    {
                        //取回人員資料
                        sql = "SELECT people.peo_uid, people.dep_no, people.peo_workid, people.peo_name, departments.dep_name FROM people INNER JOIN departments ON people.dep_no = departments.dep_no WHERE (people.peo_uid = " + peo_uid + ")";
                        System.Data.DataTable mytable = new DBObject().ExecuteQuery(sql);

                        //save session data
                        this.sessionUserID = mytable.Rows[0]["peo_uid"].ToString();
                        this.sessionUserName = mytable.Rows[0]["peo_name"].ToString();
                        this.sessionUserDepartID = mytable.Rows[0]["dep_no"].ToString();
                        this.sessionUserDepartName = mytable.Rows[0]["dep_name"].ToString();
                        this.sessionUserAccount = this.tbox_id.Text;
                        //login log


                        //go index
                        Server.Transfer("Default.aspx");
                    }
                }
                catch
                {
                    this.ShowMessage("帳號或密碼錯誤!");
                }
            }
            else
            {
                this.ShowMessage("請輸入帳號密碼!");
            }
        }

    }

    /// <summary>
    /// 檢查驗證碼
    /// </summary>
    /// <returns></returns>
    private bool CheckCode()
    {
        bool ret = false;

        if (Session["CheckCode"] != null)
        {

            DateTime checkDateTime = Convert.ToDateTime(Session["CheckCode_DateTime"].ToString()).AddMinutes(Convert.ToDouble(Session["CheckCode_limit"]));

            if (checkDateTime > System.DateTime.Now)
            {
                if (String.Compare(Session["CheckCode"].ToString(), this.tbox_code.Text, true) == 0)
                {
                    ret = true;
                }
                else
                {
                    this.ShowMessage("驗證碼輸入錯誤!");
                }
            }
            else
            {
                this.ShowMessage("驗證碼過期!");
            }
        }
        else
        {
            this.ShowMessage("驗證碼過期!");
        }

        if (!ret)
        {
            this.img1.Src = "lib/ValidateCode.ashx?ran=" + new Random().Next(1000);
        }

        this.tbox_code.Text = "";

        return ret;
    }

}
