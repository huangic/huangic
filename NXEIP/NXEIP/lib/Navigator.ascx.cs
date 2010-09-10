using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class lib_Navigator : System.Web.UI.UserControl
{

    /// <summary>
    /// 系統功能代碼
    /// </summary>
    public String SysFuncNo { get; set; }


    /// <summary>
    /// 附加的子功能說明(如果有設定會出現在導覽的後端)
    /// </summary>
    public String SubFunc { get; set; }



    public override void RenderControl(HtmlTextWriter writer)
    {
        
        String CacheKey="nav_"+this.SysFuncNo;

        //判斷快取
        Object cache = CacheUtil.GetItem(CacheKey);

        if (cache == null)
       {
            ///取代碼的群組
            ///
            
            using(NXEIPEntities model=new NXEIPEntities() ){

                try
                {
                    //取群組代碼
                    int sfu_no = int.Parse(SysFuncNo);

                    // StringBuilder sb = new StringBuilder();
                    List<String> navItem = new List<string>();


                    var sysfunction = (from f in model.sysfuction where f.sfu_no == sfu_no select f).First();

                    //正向寫入
                    navItem.Add(sysfunction.sfu_name);


                    int parent_no = sysfunction.sfu_parent.Value;


                    while (parent_no != 0)
                    {
                        var sysfun_child = (from f in model.sysfuction where f.sfu_no == parent_no select f).First();
                        parent_no = sysfun_child.sfu_parent.Value;


                        navItem.Add(sysfun_child.sfu_name);
                    }




                    var sys = (from s in model.sys where s.sys_no == sysfunction.sys_no select s).First();


                    navItem.Add(sys.sys_name);
                    CacheUtil.AddItem(CacheKey, navItem);
                }
                catch (Exception ex) { 
                 
                }
            }

           
        }

        //沒值就見鬼了
        cache = CacheUtil.GetItem(CacheKey);

        List<String> navs=(List<String>)cache; 


        //清空
        this.nav.Controls.Clear();

        //寫入NAV;

        HtmlGenericControl span = new HtmlGenericControl("span");

        StringBuilder sb = new StringBuilder();

        //這邊要反向寫入SB中所以用INSERT
        String sub=String.IsNullOrWhiteSpace(this.SubFunc)?"":" - "+this.SubFunc;

        for (int i = 0; i < navs.Count; i++) {
            if(i==0){
            sb.Insert(0, "<strong>"+navs[i]+sub+"</strong>");
            }else{
                sb.Insert(0, navs[i]+" / ");
            }
        }
        span.InnerHtml = sb.ToString();

        this.nav.Controls.Add(span);
    
        
        base.RenderControl(writer);
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}