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
                //return default(DateTime);
            }
        }
        
        set
        {
            try
            {
                this.tbox_date.Text = new ChangeObject()._ADtoROC(value);
            }
            catch
            {
                this.tbox_date.Text = new ChangeObject()._ADtoROC(DateTime.Now);
            }
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

    /// <summary>
    /// 回傳民國日期
    /// </summary>
    public string _ROC
    {
        get
        {
            try
            {
                return new ChangeObject()._ADtoROC(this._ADDate);
            }
            catch
            {
                return "";
            }
        }
    }

    /// <summary>
    /// 回傳西元日期
    /// </summary>
    public string _AD
    {
        get
        {
            try
            {
                return new ChangeObject()._ROCtoAD(this._ROC).ToString("yyyy-MM-dd");
            }
            catch
            {
                return "";
            }
        }
    }

    /// <summary>
    /// 檢查日期是否合法 True:合法
    /// </summary>
    /// <returns></returns>
    public bool CheckDateTime()
    {
        try
        {
            string[] temp = this.tbox_date.Text.Split('-');
            DateTime d = new DateTime(int.Parse(temp[0]) + 1911, int.Parse(temp[1]), int.Parse(temp[2]));
            return true;
        }
        catch
        {
            return false;
        }
    }

    public void ClearValue()
    {
        this.tbox_date.Text = "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptInclude(this, typeof(UserControl), "calendar_js", ResolveClientUrl("~/js/calendar.js"));
    }
    
}
