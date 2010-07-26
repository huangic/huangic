using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using NXEIP.DAO;
using Entity;

public partial class _35_350200_350203_1 : System.Web.UI.Page
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            String mode = Request.QueryString["mode"];
            String ID = Request.QueryString["ID"];

            this.hidden_dep_no.Value = ID;

            if (mode != null && mode.Equals("edit"))
            {
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

                this.ddl_depart.DataBind();
                this.ddl_depart.Items[this.ddl_depart.SelectedIndex].Selected = false;
                //this.ddl_depart.SelectedIndex = -1;
                this.ddl_depart.Items.FindByValue(modifyDepart.dep_parentid.ToString()).Selected = true;

                //(this.ddl_depart.Items.FindByValue(modifyDepart.dep_parentid.ToString())).Selected = true;
            }
            else
            {
                //新增模式
                logger.Info("mode:new");
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
        }
        catch
        {
            
        }
        
    }
}
