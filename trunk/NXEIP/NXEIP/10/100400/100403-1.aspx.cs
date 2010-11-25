using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;
using NLog;

public partial class _10_100400_100403_1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {  
            UtilityDAO udao = new UtilityDAO();
            SessionObject sobj = new SessionObject();

            this.lab_dep.Text = udao.Get_DepartmentName(int.Parse(sobj.sessionUserDepartID));
            this.lab_people.Text = udao.Get_PeopleName(int.Parse(sobj.sessionUserID));
            
        }
    }
   

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (CheckInput())
        {
            SessionObject sobj = new SessionObject();

            rep02 data = new rep02();
            data.r05_no = int.Parse(this.ddl_rep05.SelectedValue);
            data.peo_uid = int.Parse(sobj.sessionUserID);
            data.r02_depno = int.Parse(sobj.sessionUserDepartID);
            data.r02_date = DateTime.Now;
            data.r02_spono = int.Parse(this.ddl_spot.SelectedValue);
            data.r02_floor = this.ddl_floor.SelectedValue;
            data.r02_reason = this.tbox_reason.Text;
            data.r02_status = "1";
            data.r02_createtime = DateTime.Now;
            data.r02_createuid = int.Parse(sobj.sessionUserID);

            _100403DAO dao = new _100403DAO();
            dao.addToRep02(data);
            dao.UpData();

            OperatesObject.OperatesExecute(100403, 1, "叫修紀錄 r02_no:" + data.r02_no);
            
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "closeThickBox", "self.parent.update('新增完成!');", true);
        }
    }

    private bool CheckInput()
    {
        if (this.ddl_spot.SelectedValue.Equals("0"))
        {
            this.ShowMsg("請選擇地點");
            return false;
        }
        if (this.ddl_floor.SelectedValue.Equals("0"))
        {
            this.ShowMsg("請選擇樓層");
            return false;
        }

        if (this.ddl_rep05.SelectedValue.Equals("0"))
        {
            this.ShowMsg("請選擇維修類別");
            return false;
        }

        if (this.tbox_reason.Text.Trim().Length == 0)
        {
            this.ShowMsg("請輸入故障原因");
            return false;
        }

        return true;
    }

    private void ShowMsg(string msg)
    {
        string script = "<script>window.alert('" + msg + "');</script>";
        this.ClientScript.RegisterStartupScript(this.GetType(), "MSG", script);
    }
    
}