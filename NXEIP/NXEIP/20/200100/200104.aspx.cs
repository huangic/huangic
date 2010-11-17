using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using AjaxControlToolkit;
using NXEIP.DAO;
using System.Data.Objects.SqlClient;
using Entity;
using NLog;
using System.Data;

public partial class _20_200100_200104 : System.Web.UI.Page
{
    private static Logger logger = LogManager.GetCurrentClassLogger();
    
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack) { 
            //建立GRID VIEW
            //this.GridView1.DataBind();


            //NXEIPEntities model = new NXEIPEntities();
           
           

            
        }
        this.GridView1.DataBind();
    }

  
    /// <summary>
    /// 下拉選單連動的METHOD
    /// </summary>
    /// <param name="knownCategoryValues"></param>
    /// <param name="category"></param>
    /// <returns></returns>
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static AjaxControlToolkit.CascadingDropDownNameValue[] GetDropDownContents2(string knownCategoryValues, string category)
    {
        try
        {
            StringDictionary kv = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);



            int parentId = int.Parse(kv["undefined"]);

            DepartmentsDAO dao = new DepartmentsDAO();

            var child = dao.GetChildDepartment(parentId);

            List<CascadingDropDownNameValue> sArray = (from d in child select new CascadingDropDownNameValue { name = d.dep_name, value = SqlFunctions.StringConvert((double)d.dep_no) }).ToList();



            return sArray.ToArray();
        }
        catch {
            return default(AjaxControlToolkit.CascadingDropDownNameValue[]);
        }
    }

    protected static string GetDepartmentName(int dep_no)
    {
        using (NXEIPEntities model = new NXEIPEntities()) {
            var dep = (from d in model.departments where d.dep_no == dep_no select d).First();
            if (dep.dep_level > 1)
            {
                var parent_dep = (from d in model.departments where d.dep_no == dep.dep_parentid select d).First();

                return parent_dep.dep_name + "-" + dep.dep_name;
            }
            else {
                return dep.dep_name;
            }
        }
    
    }

    protected static bool GetModifyVisible(int peo_uid) { 
        //HttpContext.Current.Session[""]
        SessionObject session = new SessionObject();
        return (int.Parse(session.sessionUserID) == peo_uid);
            
        
    }
    

   

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //Bind內部的DataSource
        if (e.Row.RowType == DataControlRowType.DataRow) {
           ObjectDataSource ods = (ObjectDataSource)e.Row.FindControl("ObjectDataSource2");

           


            var v = (doc06)e.Row.DataItem;

            ods.SelectParameters[0].DefaultValue = v.d06_no.ToString();

        }

    }
    /// <summary>
    /// 依條件查詢
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        String dep_no = "";
        //String keyword = "";
        String number = "";
            String file="";


        //使用父部門
        if (this.ddl_department.SelectedValue!="")
        {
            dep_no = this.ddl_department.SelectedValue;
                
           
        }
        else {

         
            dep_no = this.ddl_unit.SelectedValue;
           
        }

        //keyword = this.tb_word.Text;

        number=this.tb_number.Text;
        file=this.tb_file.Text;

        this.ObjectDataSource3.SelectParameters[0].DefaultValue = dep_no;
        this.ObjectDataSource3.SelectParameters[1].DefaultValue = number;
        this.ObjectDataSource3.SelectParameters[2].DefaultValue = file;

        OperatesObject.OperatesExecute(200104, 2, String.Format("查詢公文附件 條件 部門:{0},文號{1},檔名{2}", dep_no, number,file));
            

        this.GridView1.DataBind();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del") {
            int index = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;

            int id = Convert.ToInt32(this.GridView1.DataKeys[index].Value);


            using (NXEIPEntities model = new NXEIPEntities()) {
                doc06 d06 = new doc06();
                d06.d06_no = id;
                model.doc06.Attach(d06);

                d06.d06_status = "2";

                /*
                    var d07=(from d in model.doc07 where d.d06_no==id select d);
                    var d08 = (from d in model.doc08 where d.d06_no == id select d);


                    foreach (var d in d08)
                    {
                        model.doc08.DeleteObject(d);
                    }

                    foreach (var d in d07)
                    {
                        model.doc07.DeleteObject(d);
                    }
                    model.doc06.DeleteObject(d06);
                 * 
                 * 
                 */
                OperatesObject.OperatesExecute(200104, 4, String.Format("刪除公文附件 d06_no:{0}", id));
         
                    model.SaveChanges();
            }
            this.GridView1.DataBind();
        }
    }
}