using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class lib_calendar : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptInclude(this, typeof(UserControl), "calendar_js", ResolveClientUrl("~/js/calendar.js"));
    }

    public string Get_Date() 
    {
        try
        {
            string date = new ChangeObject().ROCDTtoADDT(this.tbox_date.Text);
            Convert.ToDateTime(date);
        }
        catch
        {
            throw new Exception("日期格式錯誤!!");
        }

        return this.tbox_date.Text;
    }
}
