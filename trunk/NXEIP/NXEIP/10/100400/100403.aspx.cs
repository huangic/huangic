using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;
using NLog;

public partial class _10_100400_100403 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            using (NXEIPEntities model = new NXEIPEntities())
            {
                //div字串
                string strDiv = "";

                //大類別
                var r05Data = (from d in model.rep05 where d.r05_status == "1" orderby d.r05_no select new { d.r05_no, d.r05_name });

                int[] r05_no = r05Data.Select(o => o.r05_no).ToArray();

                foreach (var r in r05Data)
                {
                    strDiv = @"<div class='box'>
                                <div class='head'>
                                <div class='p1'></div>
                                <div class='p2'><a href='100403-0.aspx?r05_no=" + r.r05_no + "'>" + r.r05_name + "</a></div></div>";

                    //維修Q&A資料
                    int[] qat_no = (from d in model.qatype
                                    where r05_no.Contains( d.qat_r05no.Value)
                                    select d.qat_no).ToArray();

                    strDiv += "<div class='content'>";
                    var askData = (from d in model.ask
                                   where qat_no.Contains( d.qat_no)
                                   select d).OrderByDescending(o => o.ask_createtime).Take(3);

                    if (askData.Count() > 0)
                    {
                        foreach (var rr in askData)
                        {
                            strDiv += @"<li class='ps1'>
                                        <a class='a-letter-mq' href='../../20/200700/200702.aspx?qat_no=" + rr.qat_no + "'>" + rr.ask_question + @"</a></li>
                                        <li class='arrow_ms02'><a class='a-letter-ma' href='../../20/200700/200702.aspx?qat_no=" + rr.qat_no + "'>" + rr.ask_answer + @"</a></li>
                                        <div class='border-bottom-block2'></div>";
                        }

                        strDiv += @"<div class='b2'><span class='pmore'><a class='pmore' href='100403-0.aspx?r05_no=" + r.r05_no + "'></a></span></div>";
                    }
                    else
                    {
                        strDiv += "<li class='ps1'><a class='a-letter-mq' href='#'>查無資料</a></li>";
                    }

                    strDiv += "</div></div>";

                    this.div_maintain.InnerHtml += strDiv;

                }

                //維修廠商
                strDiv = @"<div class='box'>
                                <div class='head'>
                                <div class='p1'></div>
                                <div class='p2'><a href='100403-3.aspx'>維修廠商名錄</a></div></div>";

                //維修廠商資料
                strDiv += "<div class='content'>";
                var r04Data = (from d in model.rep04
                               where d.r04_status == "1"
                               orderby d.r04_createtime descending
                               select d).Take(3);

                if (r04Data.Count() > 0)
                {
                    foreach (var rr in r04Data)
                    {
                        strDiv += @"<li class='ps1'>
                                        <a class='a-letter-mq' href='100403-3.aspx'>" + rr.r04_name + @"</a></li>
                                        <div class='border-bottom-block2'></div>";
                    }

                    strDiv += @"<div class='b2'><span class='pmore'><a class='pmore' href='100403-3.aspx'></a></span></div>";
                }
                else
                {
                    strDiv += "<li class='ps1'><a class='a-letter-mq' href='#'>查無資料</a></li>";
                }

                strDiv += "</div></div>";
                this.div_maintain.InnerHtml += strDiv;
            }

        }
        
    }

    
   
}