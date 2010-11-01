using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;
using System.Collections.Specialized;
using AjaxControlToolkit;
using System.Data.Objects.SqlClient;
using NLog;

public partial class _30_300500_300501_1 : System.Web.UI.Page
{
    private static Logger logger = LogManager.GetCurrentClassLogger();

    protected void Page_Load(object sender, EventArgs e)
    {
        //編輯模式
        if (!this.IsPostBack)
        {
            string mode = Request["mode"];
            string s06_no = Request["s06_no"];
            
            this.ddl_sysfun.DataBind();


            if (mode != null && mode.Equals("modify"))
            {
                this.Navigator1.SubFunc = "修改類別";
                this.HiddenField1.Value = s06_no;

                //取資料
                sys06 data = new Sys06DAO().GetByS06No(int.Parse(s06_no));

                this.ddl_sysfun.Items.FindByValue(data.sfu_no.ToString()).Selected = true;
                if (data.s06_parent.Value > 0)
                {
                    //代入父類別值
                    this.ddl_parent_CascadingDropDown.Category = data.s06_parent.ToString();
                }

                this.tbox_name.Text = data.s06_name;
                if (data.s06_order.HasValue)
                {
                    this.tbox_order.Text = data.s06_order.Value.ToString();
                }
            }
            else
            {
                //新增模式
                this.Navigator1.SubFunc = "新增";

            }
        }
    }

    protected void btn_ok_Click(object sender, EventArgs e)
    {
        int order = 0;
        if (this.tbox_order.Text.Length > 0)
        {
            try
            {
                order = int.Parse(this.tbox_order.Text);
            }
            catch
            {
                this.ShowMSG("排序位置請輸入數值!");
                return;
            }
        }

        if (this.tbox_name.Text.Trim().Length == 0)
        {
            this.ShowMSG("請輸入類別名稱!");
        }
        else
        {
            int opt_type = 1;
            string msg = "", opt_name = "";
            Sys06DAO dao = new Sys06DAO();
            sys06 data = null;

            if (this.HiddenField1.Value != "")
            {
                data = dao.GetByS06No(int.Parse(this.HiddenField1.Value));
                msg = "修改完成!";
                opt_name = "修改類別 s06_no:" + this.HiddenField1.Value;
                opt_type = 3;
            }
            else
            {
                data = new sys06();
                msg = "新增完成!";
                opt_name = "新增類別";
            }

            data.s06_createtime = DateTime.Now;
            data.s06_createuid = int.Parse(new SessionObject().sessionUserID);
            data.s06_name = this.tbox_name.Text;
            data.s06_status = "1";
            data.s06_order = order;
            data.sfu_no = int.Parse(this.ddl_sysfun.SelectedValue);
            if (this.ddl_parent.SelectedValue.Equals(""))
            {
                data.s06_parent = 0;
                data.s06_level = 1;
            }
            else
            {
                data.s06_parent = int.Parse(this.ddl_parent.SelectedValue);
                data.s06_level = 2;
            }

            if (this.HiddenField1.Value == "")
            {
                dao.AddSys(data);
            }
            else
            {
                //父代改系統功能,子代需全改
                using (NXEIPEntities model = new NXEIPEntities())
                {
                    int parentId = data.s06_no;
                    var child = (from d in model.sys06 where d.s06_parent == parentId select d);
                    foreach (var d in child)
                    {
                        Sys06DAO dao2 = new Sys06DAO();
                        sys06 s06data = dao.GetByS06No(d.s06_no);
                        s06data.sfu_no = int.Parse(this.ddl_sysfun.SelectedValue);
                        dao2.Update();
                    }
                }
            }

            dao.Update();

            OperatesObject.OperatesExecute(300501, new SessionObject().sessionUserID, opt_type, opt_name);

            this.Page.ClientScript.RegisterStartupScript(typeof(_30_300500_300501_1), "closeThickBox", "self.parent.update('" + msg + "');", true);
            
        }

    }

    private void ShowMSG(string msg)
    {
        this.ClientScript.RegisterStartupScript(this.GetType(), "MyMSG", "<script>alert('" + msg + "');</script>");
    }

    /// <summary>
    /// 下拉式連動選單
    /// </summary>
    /// <param name="knownCategoryValues"></param>
    /// <param name="category"></param>
    /// <returns></returns>
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static AjaxControlToolkit.CascadingDropDownNameValue[] GetDropDownContents2(string knownCategoryValues, string category)
    {
        try
        {
            using (NXEIPEntities model = new NXEIPEntities())
            {

                StringDictionary kv = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
                int parentId = int.Parse(kv["undefined"]);
                var child = (from d in model.sys06 where d.sfu_no == parentId && d.s06_parent == 0 select d);
                int i_category = -1;
                if (!category.Equals("sysfun"))
                {
                    i_category = int.Parse(category);
                }
                
                List<CascadingDropDownNameValue> sArray2 = (from d in child select new CascadingDropDownNameValue { isDefaultValue = d.s06_no == i_category, name = d.s06_name, value = SqlFunctions.StringConvert((double)d.s06_no) }).ToList();

                return sArray2.ToArray();
            }

        }
        catch(System.Exception ex)
        {
            logger.Debug(ex.ToString());
            return default(AjaxControlToolkit.CascadingDropDownNameValue[]);
        }
    }

    private void LoadChild()
    {

    }
}