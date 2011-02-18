using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;

public partial class _10_100600_100601 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            this.calendar1._ADDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-01-01"));
            this.calendar2._ADDate = DateTime.Now.AddMonths(1);

            this.ObjectDataSource1.SelectParameters["sdate"].DefaultValue = this.calendar1._ADDate.ToString("yyyy-MM-dd 00:00:00");
            this.ObjectDataSource1.SelectParameters["edate"].DefaultValue = this.calendar2._ADDate.ToString("yyyy-MM-dd 23:59:59");
            this.ObjectDataSource1.SelectParameters["key"].DefaultValue = "";
            this.ObjectDataSource1.SelectParameters["status"].DefaultValue = "0";
            this.GridView1.DataBind();

        }


        //判斷來自JS 使用_doPostBack(updatePanel,"") 的情況 
        if (Request["__EVENTTARGET"] == this.UpdatePanel1.ClientID && String.IsNullOrEmpty(Request["__EVENTARGUMENT"]))
        {
            this.GridView1.DataBind();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (this.calendar1.CheckDateTime() && this.calendar2.CheckDateTime())
        {
            this.ObjectDataSource1.SelectParameters["sdate"].DefaultValue = this.calendar1._ADDate.ToString("yyyy-MM-dd 00:00:00");
            this.ObjectDataSource1.SelectParameters["edate"].DefaultValue = this.calendar2._ADDate.ToString("yyyy-MM-dd 23:59:59");
            this.ObjectDataSource1.SelectParameters["key"].DefaultValue = this.tbox_reason.Text.Trim();
            this.ObjectDataSource1.SelectParameters["status"].DefaultValue = this.ddl_status.SelectedValue;
            this.GridView1.DataBind();
        }
        else
        {
            JsUtil.AlertJs(this, "請輸入正確日期!");
        }
    }

    protected static bool GetModifyVisible(int peo_uid)
    {
        SessionObject session = new SessionObject();
        return (int.Parse(session.sessionUserID) == peo_uid);

    }

    protected static bool GetFileVisible(int peo_uid, String status)
    {
        SessionObject session = new SessionObject();
        if (status == "1")
        {
            return (int.Parse(session.sessionUserID) == peo_uid);
        }
        else
        {
            return false;
        }
    }

    protected static bool GetStatusVisible(int mee_no,String status)
    {
        _100601DAO dao = new _100601DAO();
        string att_status = dao.GetAttendsStatus(mee_no, int.Parse(new SessionObject().sessionUserID));
        if (status == "1")
        {
            if (string.IsNullOrEmpty(att_status))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 出席狀況
    /// </summary>
    /// <param name="mee_no"></param>
    /// <returns></returns>
    protected static string GetAttendsStatus(int mee_no)
    {
        _100601DAO dao = new _100601DAO();
        
        string att_status = dao.GetAttendsStatus(mee_no, int.Parse(new SessionObject().sessionUserID));

        if (att_status == "1")
        {
            return "尚未回覆";
        }
        else if (att_status == "2")
        {
            return "出席";
        }
        else if (att_status == "3")
        {
            return "不出席";
        }
        else
        {
            return "";
        }
    }

    protected static string GetConferenFile(int mee_no,DateTime ed,String status)
    {
        _100601DAO dao = new _100601DAO();

        if (status == "1")
        {
            if (DateTime.Now > ed)
            {
                //查詢是否有上傳會議記錄
                if (dao.Check_ConferenFile(mee_no) > 0)
                {
                    return "已上傳";
                }
                else
                {
                    return "未上傳";
                }
            }
            else
            {
                return "";
            }
        }
        else
        {
            return "會議取消";
        }
    }


    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            int mee_no = int.Parse(this.GridView1.DataKeys[int.Parse(e.CommandArgument.ToString())].Values[0].ToString());
            
            new _100601DAO().DelToMeetings(mee_no);
            
            OperatesObject.OperatesExecute(100601, 4, String.Format("刪除會議 mee_no:{0}", mee_no));

            this.GridView1.DataBind();
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ChangeObject cObj = new ChangeObject();
            UtilityDAO udao = new UtilityDAO();

            DateTime sd = Convert.ToDateTime(this.GridView1.DataKeys[e.Row.RowIndex].Values[1].ToString());
            DateTime ed = Convert.ToDateTime(this.GridView1.DataKeys[e.Row.RowIndex].Values[2].ToString());

            e.Row.Cells[2].Text = cObj._ADtoROCDT(sd) + "~" + cObj._ADtoROCDT(ed);
            
            //主持人
            e.Row.Cells[3].Text = udao.Get_PeopleName(int.Parse(e.Row.Cells[3].Text));

            //聯絡人
            e.Row.Cells[4].Text = udao.Get_PeopleName(int.Parse(e.Row.Cells[4].Text));
            
        }
    }
}