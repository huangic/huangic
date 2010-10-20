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

public partial class _20_200100_200104 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack) { 
            //建立GRID VIEW
            this.GridView1.DataBind();
        }
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
}