using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity;
using NXEIP.DAO;

public partial class _30_300300_300303_8 : System.Web.UI.Page
{
    private NXEIPEntities model = new NXEIPEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if (Request["e02_no"] != null)
            {
                this.hidd_no.Value = Request["e02_no"];
                int e02_no = Convert.ToInt32(this.hidd_no.Value);
                string arg = "0,1,1,1,0";//Request["arg"];
                string[] isShow = arg.Split(',');
                string[] colname = { "單位", "姓名", "職稱", "身分證字號", "電話" };
                int colSpan = 6;
                for (int i = 0; i < isShow.Length; i++)
                {
                    if (isShow[i].Equals("0"))
                    {
                        colSpan--;
                    }
                }

                //資料總筆數
                int rowCount = (from x in model.e04 where x.e02_no == e02_no && x.e04_check == "1" select x).Count();
                //列印一頁所需筆數
                int pageCount = 20;
                //頁數
                int page = 1;
                if (rowCount > pageCount)
                {
                    page = rowCount / pageCount;
                    if (rowCount % pageCount > 0)
                    {
                        page++;
                    }
                }

                string tableStr = "", table_coltilte = "", table_title = "", table_place = "";

                //表頭
                var e02Data = (from d in model.e02 where d.e02_no == e02_no select d).FirstOrDefault();
                table_title = @"<tr><td align='center' colspan='" + colSpan + "'>" +
                                    e02Data.e02_mechani + "<br/>" + e02Data.e02_name + "第" + e02Data.e02_flag + "期簽到冊" +
                             "</td></tr>";

                //日期,地點
                ChangeObject cboj = new ChangeObject();
                string date = cboj._ROCtoROCYMD(cboj._ADtoROC(e02Data.e02_sdate.Value));
                string place = (from t in model.e01 where t.e01_no == e02Data.e01_no select t.e01_name).FirstOrDefault();
                table_place = @"<tr><td colspan='" + colSpan + "'>"+
                                    "<span style='float:left'>日期：" + date + "</span>" +
                                    "<span style='float:right'>上課地點：" + place + "</span>"+
                                "</td></tr>";

                //欄位表頭
                table_coltilte = "<tr>";
                for (int i = 0; i < isShow.Length; i++)
                {
                    if (isShow[i].Equals("1"))
                    {
                        table_coltilte += "<td>" + colname[i] + "</td>";
                    }
                }
                table_coltilte += "<td>簽到</td>";
                table_coltilte += "</tr>";
                
                //製作表格
                for (int pageIndex = 1; pageIndex <= page; pageIndex++)
                {
                    //此頁所需之資料
                    int start = 0;
                    if (pageIndex > 1)
                    {
                        start = (pageIndex-1) * pageCount;
                    }
                    //此課程核可人員之UID
                    int[] e04_uid = (from dd in model.e04 where dd.e02_no == e02_no && dd.e04_check == "1" orderby dd.e04_no select dd.e04_peouid).Skip(start).Take(pageCount).ToArray();
                    string table_body = "";
                    for (int i = 0; i < e04_uid.Length; i++)
                    {
                        table_body += this.dataStr(e04_uid[i],arg);
                    }

                    //此頁字串
                    tableStr += "<table border='1' cellSpacing='0' cellPadding='0' width='100%'>";
                    tableStr += table_title;
                    tableStr += table_place;
                    tableStr += table_coltilte;
                    tableStr += table_body;
                    tableStr += "</table>";
                    //分頁
                    if (pageIndex < page)
                    {
                        tableStr += "<br style='PAGE-BREAK-BEFORE: always' clear='all'>";
                    }
                }

                //放至DIV
                this.div_table.InnerHtml = tableStr;
            }
        }
    }

    /// <summary>
    /// 人員資料
    /// </summary>
    /// <param name="uid"></param>
    /// <param name="arg"></param>
    /// <returns></returns>
    private string dataStr(int uid, string arg)
    {
        string[] isShow = arg.Split(',');

        var peo_data = (from p in model.people
                    where p.peo_uid == uid
                    from d in model.departments
                    where d.dep_no == p.dep_no
                    from t in model.types
                    where t.typ_no == p.peo_pfofess
                    select new { name = p.peo_name, depname = d.dep_name, proname = t.typ_cname, idcard = p.peo_idcard, tel = p.peo_tel }).FirstOrDefault();

        string data = "<tr>";
        //單位
        if (isShow[0].Equals("1"))
        {
            data += "<td>" + peo_data.depname + "</td>";
        }
        //職稱
        if (isShow[1].Equals("1"))
        {
            data += "<td>" + peo_data.proname + "</td>";
        }
        //姓名
        if (isShow[2].Equals("1"))
        {
            data += "<td>" + peo_data.name + "</td>";
        }
        //身份證
        if (isShow[3].Equals("1"))
        {
            data += "<td>" + peo_data.idcard + "</td>";
        }
        //電話
        if (isShow[4].Equals("1"))
        {
            data += "<td>" + peo_data.tel + "</td>";
        }
        //簽到欄
        data += "<td>&nbsp;</td>";
        data += "</tr>";

        return data;

    }
}