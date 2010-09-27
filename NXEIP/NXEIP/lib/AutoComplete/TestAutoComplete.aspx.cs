using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class lib_AutoComplete_TestAutoComplete : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            //給預設值
            this.Autocomplete1._value = "aaaa";
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //取回值 部門,姓名,workid,peo_uid,dep_no
        this.div_msg.InnerHtml = this.Autocomplete1.GetValue();
    }
}