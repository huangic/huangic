using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;

public partial class _30_300300_300302_1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //編輯模式
        if (!this.IsPostBack)
        {
            string mode = Request["mode"];
            string typ_no = Request["typ_no"];

            TypesDAO dao = new TypesDAO();

            if (mode != null && mode.Equals("modify"))
            {
                this.navigator1.SubFunc = "修改";
                this.HiddenField1.Value = typ_no;

                //取資料
                types t = dao.GetTypes(System.Convert.ToInt32(typ_no));
                this.tbox_number.Text = t.typ_number;
                this.tbox_name.Text = t.typ_cname;
                this.tbox_order.Text = t.typ_order.ToString();
            }
            else
            {
                //新增模式
                this.navigator1.SubFunc = "新增";

            }
        }
    }
    protected void btn_ok_Click(object sender, EventArgs e)
    {
        string msg = "";

        SessionObject sessionObj = new SessionObject();

        if (this.HiddenField1.Value != "")
        {
            TypesDAO dao = new TypesDAO();
            String typ_number = this.tbox_number.Text;
            String typ_cname = this.tbox_name.Text;

            Entity.types newType = dao.GetTypes(System.Convert.ToInt32(this.HiddenField1.Value));

            newType.typ_number = typ_number;
            newType.typ_cname = typ_cname;
            newType.typ_order = Convert.ToInt32(this.tbox_order.Text);
            newType.typ_createtime = DateTime.Now;
            try
            {
                newType.typ_createuid = System.Convert.ToInt32(sessionObj.sessionUserID);
            }
            catch
            {
            }

            dao.Update();
            OperatesObject.OperatesExecute(300302, new SessionObject().sessionUserID, 3, "修改課程類別 typ_no:" + this.HiddenField1.Value);
            msg = "修改完成!";
        }
        else
        {
            
            String typ_code = "class";
            String typ_number = this.tbox_number.Text;
            String typ_cname = this.tbox_name.Text;


            //新增模式
            Entity.types newType = new Entity.types();

            newType.typ_code = typ_code;
            newType.typ_cname = typ_cname;
            newType.typ_number = typ_number;
            newType.typ_order = Convert.ToInt32(this.tbox_order.Text);
            newType.typ_status = "1";
            newType.typ_parent = 0;
            newType.typ_createtime = DateTime.Now;
            try
            {
                newType.typ_createuid = System.Convert.ToInt32(sessionObj.sessionUserID);
            }
            catch
            {
            }

            //Dao
            TypesDAO dao = new TypesDAO();
            dao.AddTypes(newType);
            dao.Update();
            OperatesObject.OperatesExecute(300302, new SessionObject().sessionUserID, 1, "新增課程類別");
            msg = "新增完成!";
        }

        this.Page.ClientScript.RegisterStartupScript(typeof(_30_300300_300302_1), "closeThickBox", "self.parent.update('" + msg + "');", true);
    }
    
}