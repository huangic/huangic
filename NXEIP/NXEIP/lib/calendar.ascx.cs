using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class lib_calendar : System.Web.UI.UserControl
{
    /// <summary>
    /// 取得或設定日期
    /// </summary>
    public DateTime _ADDate
    {
        get
        {
            try
            {
                return new ChangeObject()._ROCtoAD(this.tbox_date.Text);
            }
            catch
            {
                throw new Exception("日期格式錯誤!!");
            }
        }
        
        set
        {
            this.tbox_date.Text = new ChangeObject()._ADtoROC(value);
        }
    }

    /// <summary>
    /// 不顯示日期格式:false
    /// </summary>
    public bool _Show
    {
        set
        {
            this.span_1.Visible = value;
        }
    }

    public string _GetID
    {
        get
        {
            return this.tbox_date.ClientID;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptInclude(this, typeof(UserControl), "calendar_js", ResolveClientUrl("~/js/calendar.js"));
        
        
    }
    
}
