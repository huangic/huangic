using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class lib_SubMenuControl : System.Web.UI.UserControl
{
   /// <summary>
   /// 功能項目代碼
   /// </summary>
    public String SysFuncCode { get; set; }
    
    protected void Page_Load(object sender, EventArgs e)
    {
       
      

    }


    protected void Page_Init(object sender, EventArgs e)
    {
        //因為該死的USERCONTROL不能互相傳直 需要用INTERFACE
        ISubMenuControl uc = (ISubMenuControl)Page.Master.FindControl("SubHeaderMenu1");

        uc.SetCode(this.SysFuncCode);
    }
}