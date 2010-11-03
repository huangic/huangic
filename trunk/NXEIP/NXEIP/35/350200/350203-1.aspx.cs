using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;
using NLog;

public partial class _35_350200_350203_1 : System.Web.UI.Page
{
    private static Logger logger = LogManager.GetCurrentClassLogger();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            String mode = Request.QueryString["mode"];
            String ID = Request.QueryString["ID"];

            this.hidden_dep_no.Value = ID;

            //預設角色
            this.ddl_role.DataBind();
            this.ddl_role.Items.Insert(0, "無預設角色");
            this.ddl_role.Items[0].Selected = true;

            if (mode != null && mode.Equals("edit"))
            {
                this.Navigator1.SubFunc = "修改部門";
                //設定要變更的欄位
                DepartmentsDAO dao = new DepartmentsDAO();

                departments modifyDepart = dao.GetByDepNo(Convert.ToInt32(ID));

                this.tbx_dep_addr.Text = modifyDepart.dep_addr;
                this.tbx_dep_code.Text = modifyDepart.dep_code;
                this.tbx_dep_ename.Text = modifyDepart.dep_ename;
                this.tbx_dep_fax.Text = modifyDepart.dep_fax;
                this.tbx_dep_name.Text = modifyDepart.dep_name;
                this.tbx_dep_order.Text = modifyDepart.dep_order.ToString();
                this.tbx_dep_tel.Text = modifyDepart.dep_tel;

                //父代部門
                this.ddl_depart.DataBind();
                this.ddl_depart.Items[this.ddl_depart.SelectedIndex].Selected = false;

                try
                {
                    this.ddl_depart.Items.FindByValue(modifyDepart.dep_parentid.ToString()).Selected = true;
                }
                catch { 
                
                }


                //預設角色
                string sql = "select rol_no from roldefault where dep_no = " + ID;
                string tem_depno = new DBObject().ExecuteScalar(sql);
                if (tem_depno.Length > 0)
                {
                    this.ddl_role.Items[0].Selected = false;
                    this.ddl_role.Items.FindByValue(tem_depno).Selected = true;
                }
            }
            else
            {
                //新增模式
                logger.Info("mode:new");
                this.Navigator1.SubFunc = "新增部門";
            }
        }
    }

    protected void btn_ok_Click(object sender, EventArgs e)
    {
        logger.Debug("OK");
        logger.Debug(Request["clientID"]);

        String msg = "";

        //判斷模式
        if (this.hidden_dep_no.Value != "")
        {
            Editing();
            msg = "修改成功";
        }
        else
        {
            Adding();
            msg = "新增成功";
        }

        

        //呼叫UPATE()關閉此頁面 並且更新updatepanel (parent page 必須做一個UPDATE的FUNCTION) 
        this.Page.ClientScript.RegisterStartupScript(typeof(_35_350200_350203_1), "closeThickBox", "self.parent.update('" + msg + "');", true);


    }

    private void Adding()
    {

        DepartmentsDAO dao = new DepartmentsDAO();
        departments depart;

        try
        {

            depart = new departments();

            //Saving Department

            //找父代物件取他的LEVEL
            departments paretntDepart = dao.GetByDepNo(System.Convert.ToInt32(this.ddl_depart.SelectedValue));

            depart.dep_name = this.tbx_dep_name.Text;
            depart.dep_parentid = System.Convert.ToInt32(this.ddl_depart.SelectedValue);
            depart.dep_order = System.Convert.ToInt32(this.tbx_dep_order.Text);
            depart.dep_fax = this.tbx_dep_fax.Text;
            depart.dep_code = this.tbx_dep_code.Text;
            depart.dep_ename = this.tbx_dep_ename.Text;
            depart.dep_level = paretntDepart.dep_level + 1;
            depart.dep_addr = this.tbx_dep_addr.Text;
            depart.dep_createuid = 1;
            depart.dep_createtime = DateTime.Now;
            depart.dep_son = "0";
            depart.dep_tel = this.tbx_dep_tel.Text;
            depart.dep_status = "1";

            dao.AddDepartment(depart);
            dao.Update();

            //預設角色
            if (!this.ddl_role.SelectedValue.Equals("無預設角色"))
            {
                new DBObject().ExecuteNonQuery("insert into roldefault (rol_no,dep_no) values (" + this.ddl_role.SelectedValue + "," + depart.dep_no + ")");
            }
            
        }
        catch
        {

        }

    }

    private void Editing()
    {
        try
        {
            DepartmentsDAO dao = new DepartmentsDAO();
            departments depart;
            int dep_no = System.Convert.ToInt32(this.hidden_dep_no.Value);

            depart = dao.GetByDepNo(dep_no);

            //Saving Department

            //找父代物件取他的LEVEL
            departments paretntDepart = dao.GetByDepNo(System.Convert.ToInt32(this.ddl_depart.SelectedValue));

            depart.dep_name = this.tbx_dep_name.Text;
            depart.dep_parentid = System.Convert.ToInt32(this.ddl_depart.SelectedValue);
            depart.dep_order = System.Convert.ToInt32(this.tbx_dep_order.Text);
            depart.dep_fax = this.tbx_dep_fax.Text;
            depart.dep_code = this.tbx_dep_code.Text;
            depart.dep_ename = this.tbx_dep_ename.Text;
            depart.dep_level = paretntDepart.dep_level + 1;
            depart.dep_addr = this.tbx_dep_addr.Text;
            depart.dep_tel = this.tbx_dep_tel.Text;

            dao.Update();
            DBObject odb = new DBObject();

            //預設角色
            if (this.ddl_role.SelectedValue.Equals("無預設角色"))
            {
                odb.ExecuteNonQuery("delete from roldefault where dep_no = " + dep_no);
            }
            else
            {
                if (Convert.ToInt32(odb.ExecuteScalar("select count(*) as total from roldefault where dep_no = " + dep_no)) > 0)
                {
                    odb.ExecuteNonQuery("update roldefault set rol_no = " + this.ddl_role.SelectedValue + " where dep_no = " + dep_no);
                }
                else
                {
                    odb.ExecuteNonQuery("insert into roldefault (rol_no,dep_no) values (" + this.ddl_role.SelectedValue + "," + dep_no + ")");
                }
            }
        }
        catch
        {
            
        }
        
    }
}
