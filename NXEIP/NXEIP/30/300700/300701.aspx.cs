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

public partial class _30_300700_300701 : System.Web.UI.Page
{
    private static Logger logger = LogManager.GetCurrentClassLogger();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {

            using (NXEIPEntities model = new NXEIPEntities())
            {

                sys06 sys = (from d in model.sys06 where d.s06_status == "1" && d.sfu_no == 200107 orderby d.s06_order orderby d.s06_no select d).First();


                this.hidden_cat.Value = sys.s06_no.ToString();

            }



            this.Search();
            this.GridView1.DataBind();
        }
      




       
    }






    protected static string GetDepartmentName(int dep_no)
    {
        using (NXEIPEntities model = new NXEIPEntities())
        {
            var dep = (from d in model.departments where d.dep_no == dep_no select d).First();

            return dep.dep_name;

        }

    }

    protected static string GetCatName(int cat_no)
    {
        using (NXEIPEntities model = new NXEIPEntities())
        {
            var cat = (from c in model.sys06 where c.s06_no == cat_no select c).First();

            if (cat.s06_level == 1)
            {
                return cat.s06_name;
            }
            else
            {
                var p_cat = (from c in model.sys06 where c.s06_no == cat.s06_parent select c).First();
                return p_cat.s06_name;
            }



        }
    }

    protected static string GetCatChildName(int cat_no)
    {
        using (NXEIPEntities model = new NXEIPEntities())
        {
            var cat = (from c in model.sys06 where c.s06_no == cat_no select c).First();

            if (cat.s06_level == 1)
            {
                return "";
            }
            else
            {
                return cat.s06_name;
            }



        }
    }








    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {



        //Bind內部的DataSource
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ObjectDataSource ods = (ObjectDataSource)e.Row.FindControl("ObjectDataSource2");




            var v = (doc09)e.Row.DataItem;

            ods.SelectParameters[0].DefaultValue = v.d09_no.ToString();

        }

    }
    /// <summary>
    /// 依條件查詢
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {



        Search();

    }


    private void Search()
    {

        //因為這是從200107派生出來的 保留DEP NO的彈性  DEP_NO 沒有值表示是全府的
        String dep_no = "";
        //String keyword = "";
        String cat = "";
        String peo_uid = new SessionObject().sessionUserID;

        cat = String.IsNullOrEmpty(this.hidden_childcat.Value) ? this.hidden_cat.Value : this.hidden_childcat.Value;


        //keyword = this.tb_word.Text;

        string status = this.DropDownList1.SelectedValue;




        this.GridView1.DataSourceID = "ObjectDataSource3";
        this.ObjectDataSource3.SelectParameters[0].DefaultValue = peo_uid;
        this.ObjectDataSource3.SelectParameters[1].DefaultValue = dep_no;
        this.ObjectDataSource3.SelectParameters[2].DefaultValue = cat;
        this.ObjectDataSource3.SelectParameters[3].DefaultValue = status;


        //藏欄位
        if (status != "3")
        {
            this.GridView1.Columns[0].Visible = false;
        }
        else
        {
            this.GridView1.Columns[0].Visible = true;
        }



        OperatesObject.OperatesExecute(200107, 2, String.Format("查詢審核檔案區 條件 部門:{0},分類:{1},狀態{0}", dep_no, cat, status));

        this.GridView1.DataBind();
    }




    protected void lv_cat_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        if (e.CommandName == "click_cat")
        {
            int index = Convert.ToInt32(e.Item.DataItemIndex);


            this.hidden_cat.Value = this.lv_cat.DataKeys[index].Value.ToString();
            this.hidden_childcat.Value = "";



            this.lv_child.DataBind();



            //顯示子項目的DIV
            this.childDiv.Visible = this.lv_child.Items.Count > 0;



            this.lv_cat.DataBind();




            this.Search();


        }
    }


    protected void lv_child_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        if (e.CommandName == "click_childcat")
        {
            int index = Convert.ToInt32(e.Item.DataItemIndex);


            this.hidden_childcat.Value = this.lv_child.DataKeys[index].Value.ToString();

            //e.Item
            //LinkButton lb =(LinkButton)e.Item.Parent;

            //lb.CssClass = "a-letter-s1";
            this.lv_child.DataBind();

            this.Search();

        }
    }




    protected string GetPeopleName(int? peoUid)
    {
        if (peoUid.HasValue)
        {
            return new PeopleDAO().GetPeopleNameByUid(peoUid.Value);
        }
        else
        {
            return "";
        }
    }

    protected string GetROCDate(DateTime? date)
    {
        if (date.HasValue)
        {
            return new ChangeObject()._ADtoROC(date.Value).ToString();
        }
        else
        {
            return "";
        }
    }


    protected string GetStatus(string status)
    {
        if (status == "1")
        {
            return "通過";
        }
        if (status == "2")
        {
            return "未通過";
        }
        if (status == "3")
        {
            return "審核中";
        }
        if (status == "4")
        {
            return "刪除";
        }
        return "";
    }


    protected void btn_ok_Click(object sender, EventArgs e)
    {
        int peo_uid = int.Parse(new SessionObject().sessionUserID);

        using (NXEIPEntities model = new NXEIPEntities())
        {

            //核可全部選擇的項目
            for (int i = 0; i < this.GridView1.Rows.Count; i++)
            {
                if (((CheckBox)(this.GridView1.Rows[i].FindControl("cbox"))).Checked)
                {
                    int id = System.Convert.ToInt32(this.GridView1.DataKeys[i].Value.ToString());
                    doc09 doc = new doc09();

                    doc.d09_no = id;


                    model.doc09.Attach(doc);
                    doc.d09_status = "1";
                    doc.d09_checkdate = DateTime.Now;
                    doc.d09_checkuid = peo_uid;
                    OperatesObject.OperatesExecute(300701, 3, "檔案區審核通過 d09_no:{0},d09_status:通過",id);
                }


                
            }

            model.SaveChanges();

        }
        this.GridView1.DataBind();

    }



    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        //logger.Debug("msg" + this.hidden_reason.Value);
        
        int peo_uid = int.Parse(new SessionObject().sessionUserID);
        string reason=this.hidden_reason.Value;
        using (NXEIPEntities model = new NXEIPEntities())
        {

            //核可全部選擇的項目
            for (int i = 0; i < this.GridView1.Rows.Count; i++)
            {
                if (((CheckBox)(this.GridView1.Rows[i].FindControl("cbox"))).Checked)
                {
                    int id = System.Convert.ToInt32(this.GridView1.DataKeys[i].Value.ToString());
                    doc09 doc = new doc09();

                    doc.d09_no = id;


                    model.doc09.Attach(doc);
                    doc.d09_status = "2";
                    doc.d09_checkdate = DateTime.Now;
                    doc.d09_checkuid = peo_uid;
                    doc.d09_reason = reason;
                    OperatesObject.OperatesExecute(300701, 3, "檔案區審核通過 d09_no:{0},d09_status:未通過,d09_reason:{1}", id, reason);
                }



            }

            model.SaveChanges();

        }
        this.GridView1.DataBind();

    }
}