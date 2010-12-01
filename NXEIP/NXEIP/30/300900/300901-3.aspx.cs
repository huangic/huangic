using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;
using NLog;
using NXEIP.DynamicForm;
using Newtonsoft.Json;

public partial class _30_300900_300901_3 : System.Web.UI.Page
{
    private static Logger logger = LogManager.GetCurrentClassLogger();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            String mode = Request.QueryString["mode"];
            String ID = Request.QueryString["ID"];
            String UID = Request.QueryString["UID"];

            this.hidden_id.Value = ID;
            this.hidden_uid.Value = UID;
           

            if (mode != null && mode.Equals("edit"))
            {
                this.Navigator1.SubFunc = "修改表單";
                //設定要變更的欄位
                int no=int.Parse(ID);
            
                
                //取欄位
                Form01DAO dao = new Form01DAO();

                var column=dao.GetColumnsByFormNO(no);

                Form f = new Form();

                f.Columns = column.ToList();

                Column c=f.GetColumn(UID);

                this.tb_name.Text = c.Name;
                this.tb_description.Text = c.Description;

                this.tb_max.Text = c.MaxLength.ToString();

                this.tb_order.Text = c.Order.ToString();

                SetValue(c.Items);

                this.ShowValue();
             
                this.DropDownList1.DataBind();

                this.DropDownList1.SelectedValue = ((int)c.ColumnType).ToString();

                this.rb_required.SelectedValue = c.Required.ToString();

            }
            else
            {
                //新增模式
                logger.Info("mode:new");
                this.Navigator1.SubFunc = "新增表單";
                //this.DepartTreeTextBox1.Add(new SessionObject().sessionUserID);
            }
        }
    }

    protected void btn_ok_Click(object sender, EventArgs e)
    {
        logger.Debug("OK");
        logger.Debug(Request["clientID"]);

        String msg = "";

        //判斷模式
        if (this.hidden_uid.Value != "")
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
        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "closeThickBox", "self.parent.update('" + msg + "');", true);


    }

    private void Adding()
    {

        try
        {

            

            using (NXEIPEntities model = new NXEIPEntities())
            {
                int id = int.Parse(this.hidden_id.Value);
                Form01DAO dao = new Form01DAO();
                var columns=dao.GetColumnsByFormNO(id).ToList();
                //column.AsEnumerable().Concat()
                //column.Add)

                Column col = new Column();

                col.UID = Guid.NewGuid().ToString();
                col.Name = this.tb_name.Text;
                col.Description = this.tb_description.Text;
                col.MinLength = 0;
                col.Required = bool.Parse(this.rb_required.SelectedValue);
                col.Order = int.Parse(this.tb_order.Text);

                //col.MaxLength= int.Parse(this.tb)

                col.ColumnType = int.Parse(this.DropDownList1.SelectedValue);

                //選項欄位
                col.Items = GetValue();





                columns.Add(col);

                form01 form = new form01 { f01_no = id };

                model.form01.Attach(form);


                form.f01_columns = JsonConvert.SerializeObject(columns);

                    
                
                //取COLUMN


               
                model.SaveChanges();
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

            using (NXEIPEntities model = new NXEIPEntities())
            {
                int id = int.Parse(this.hidden_id.Value);
                Form01DAO dao = new Form01DAO();
                var columns = dao.GetColumnsByFormNO(id).ToList();

                Form f = new Form();

                f.Columns = columns.ToList();

                Column col = f.GetColumn(this.hidden_uid.Value);

                col.UID = Guid.NewGuid().ToString();
                col.Name = this.tb_name.Text;
                col.Description = this.tb_description.Text;
                col.MinLength = 0;
                col.Required = bool.Parse(this.rb_required.SelectedValue);
                col.Order = int.Parse(this.tb_order.Text);

                //col.MaxLength= int.Parse(this.tb)

                col.ColumnType = int.Parse(this.DropDownList1.SelectedValue);

                //選項欄位
                col.Items = GetValue();





                

                form01 form = new form01 { f01_no = id };

                model.form01.Attach(form);


                form.f01_columns = JsonConvert.SerializeObject(f.Columns);



                //取COLUMN



                model.SaveChanges();
            }

                      
        }
        catch
        {
            
        }
        
    }


    private void settingForm(){
    
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {


        this.showItem.Visible = ColumnType.GetColumnType(int.Parse(this.DropDownList1.SelectedValue)).ShowItem;
    
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //設定值
        List<String> Values = GetValue();
        if (!Values.Contains(this.tb_value.Text))
        {
            string key = this.tb_value.Text;
            string value = this.tb_value.Text;


            Values.Add(key + "@" + value);
        }


        SetValue(Values);
        this.tb_value.Text = "";
        this.ShowValue();

    }

    private List<String> GetValue() {
        return this.hidden_value.Value.Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries).ToList();
    }

    private void SetValue(List<String> value) {
        if (value != null)
        {
            this.hidden_value.Value = String.Join(",", value);
        }
    }
    

    private void ShowValue(){
        this.ListBox1.Items.Clear();

        List<String> value = GetValue();

        foreach (var i in value) {
            String[] item=i.Split('@');

            this.ListBox1.Items.Add(new ListItem(item[0],item[1]));
        }

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        List<String> values = GetValue();

        String key = this.ListBox1.SelectedItem.Value;
        String Value = this.ListBox1.SelectedValue;

        values.Remove(key+"@"+Value);

        SetValue(values);

        this.ShowValue();
    }
}
