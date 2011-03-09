using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity;
using NXEIP.DAO;

public partial class _20_200700_200702 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.LoadData("", null, "");
            
            

            using (NXEIPEntities model = new NXEIPEntities())
            {
                //業務類別
                _200702DAO dao = new _200702DAO();
                
                var sfu_no = dao.Get_qatype("2");
                foreach (var p in sfu_no)
                {
                    string sfu_name = (from d in model.sysfuction
                                       where d.sfu_no == p.qat_s06no
                                       select d.sfu_name).FirstOrDefault();
                    ListItem item = new ListItem(sfu_name, p.qat_no.ToString());
                    this.ddl_sfuno.Items.Add(item);
                }
                this.ddl_sfuno.Items.Insert(0, new ListItem("全部", "0"));

                //維修類別
                var r05_no = dao.Get_qatype("3");
                foreach (var p in r05_no)
                {
                    string r05_name = (from d in model.rep05
                                       where d.r05_no == p.qat_r05no
                                       select d.r05_name).FirstOrDefault();
                    ListItem item = new ListItem(r05_name, p.qat_no.ToString());
                    this.ddl_r05no.Items.Add(item);
                }
                this.ddl_r05no.Items.Insert(0, new ListItem("全部", "0"));
            }
        }

        if (Request["__EVENTTARGET"] == this.UpdatePanel1.ClientID && String.IsNullOrEmpty(Request["__EVENTARGUMENT"]))
        {
            this.GridView1.DataBind();
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        string key = this.tbox_key.Text.Trim();
        string self = "";
        int? qat_no = null;

        if (this.rbl_self.Checked)
        {
            if (this.ddl_self.SelectedValue == "0")
            {
                self = "1";
            }
            else
            {
                self = "";
                qat_no = int.Parse(this.ddl_self.SelectedValue);
            }
        }

        if (this.rbl_sfu.Checked)
        {
            if (this.ddl_sfuno.SelectedValue == "0")
            {
                self = "2";
            }
            else
            {
                self = "";
                qat_no = int.Parse(this.ddl_sfuno.SelectedValue);
            }
        }

        if (this.rbl_r05.Checked)
        {
            if (this.ddl_r05no.SelectedValue == "0")
            {
                self = "3";
            }
            else
            {
                self = "";
                qat_no = int.Parse(this.ddl_r05no.SelectedValue);
            }
        }

        this.LoadData(self, qat_no, key);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="self"></param>
    /// <param name="qat_no"></param>
    /// <param name="key"></param>
    private void LoadData(string self, int? qat_no, string key)
    {
        this.ObjectDataSource1.SelectParameters["self"].DefaultValue = self;
        if (qat_no.HasValue)
        {
            this.ObjectDataSource1.SelectParameters["qat_no"].DefaultValue = qat_no.Value.ToString();
        }
        else
        {
            this.ObjectDataSource1.SelectParameters["qat_no"].DefaultValue = string.Empty;
        }
        this.ObjectDataSource1.SelectParameters["key"].DefaultValue = key;

        this.GridView1.DataBind();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = int.Parse(e.CommandArgument.ToString());
        int ask_no = int.Parse(this.GridView1.DataKeys[rowIndex].Value.ToString());

        if (e.CommandName.Equals("del"))
        {
            _200702DAO dao = new _200702DAO();

            ask d = dao.Get_ask(ask_no);
            d.ask_status = "2";
            dao.Update();

            OperatesObject.OperatesExecute(200702, 4, "刪除問答 ask_no:" + ask_no);

            this.GridView1.DataBind();
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //可查詢類別
        int[] my_qatno = new _200702DAO().Get_MyQAtype(int.Parse(new SessionObject().sessionUserID));

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int ask_no = int.Parse(this.GridView1.DataKeys[e.Row.DataItemIndex].Value.ToString());

            ask data = new _200702DAO().Get_ask(ask_no);
            
            ChangeObject cobj = new ChangeObject();

            e.Row.Cells[1].Text = cobj._ADtoROCDT(data.ask_date.Value);
            if (data.ask_rdate.HasValue)
            {
                e.Row.Cells[2].Text = cobj._ADtoROCDT(data.ask_rdate.Value);
            }
            e.Row.Cells[3].Text = new UtilityDAO().Get_PeopleName(int.Parse(new SessionObject().sessionUserID));

            //是否為自己發問
            int peo_uid = int.Parse(new SessionObject().sessionUserID);
            if (data.ask_peouid != peo_uid)
            {
                e.Row.Cells[5].Text = "&nbsp;";
            }

            //是否可回覆
            bool yes = false;
            if (my_qatno != null)
            {
                for (int i = 0; i < my_qatno.Length; i++)
                {
                    if (my_qatno[i] == data.qat_no)
                    {
                        yes = true;
                        break;
                    }
                }
            }

            if (!yes)
            {
                e.Row.Cells[4].Text = "&nbsp;";
            }

        }
    }



    
}