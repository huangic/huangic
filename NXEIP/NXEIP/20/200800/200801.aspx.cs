using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;

public partial class _20_200800_200801 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            //init DataSource


            this.hidden_mode.Value = "1";

            this.Search();

               


        }



        if (Request["__EVENTTARGET"] == this.UpdatePanel1.ClientID && String.IsNullOrEmpty(Request["__EVENTARGUMENT"]))
        {
            this.ListView1.DataBind();
        }

    }


    protected void Button_Click(object sender, EventArgs e)
    {
        Button button = sender as Button;
        String Command = button.CommandName;

        InitButtonCss(button);

        if (Command == "male")
        {
            hidden_mode.Value = "1";
            this.Search();
        }

        if (Command == "female")
        {
            hidden_mode.Value = "2";
            this.Search();
        }

        

        //if()
    }



    private void Search()
    {

        //init DataSource
               
        this.ObjectDataSource1.SelectParameters[0].DefaultValue = "";

        this.ObjectDataSource1.SelectParameters[1].DefaultValue = this.hidden_mode.Value;


        this.ListView1.DataBind();


       
    }


    /// <summary>
    /// 初始化與設定按鈕的CSS屬性
    /// </summary>
    /// <param name="b"></param>
    private void InitButtonCss(Button b)
    {
        this.Button1.CssClass = "a-input";
        this.Button2.CssClass = "a-input";
        

        b.CssClass = "b-input2";
    }



    protected String GetTitleName(int type_no)
    {
        return new UtilityDAO().Get_TypesCName(type_no);

    }


    protected string GetDepartmentName(int dep_no)
    {
        using (NXEIPEntities model = new NXEIPEntities())
        {
            var dep = (from d in model.departments where d.dep_no == dep_no select d).First();
            if (dep.dep_level > 1)
            {
                var parent_dep = (from d in model.departments where d.dep_no == dep.dep_parentid select d).First();

                return parent_dep.dep_name + "-" + dep.dep_name;
            }
            else
            {
                return dep.dep_name;
            }
        }

    }

}