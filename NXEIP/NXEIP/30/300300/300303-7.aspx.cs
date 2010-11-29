using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity;
using NXEIP.DAO;
using System.Data;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.HPSF;
using NPOI.SS.UserModel;

public partial class _30_300300_300303_7 : System.Web.UI.Page
{
    private NXEIPEntities model = new NXEIPEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.Navigator1.SubFunc = "簽到表";

            if (Request["e02_no"] != null)
            {
                this.hidd_no.Value = Request["e02_no"];

                int e02_no = Convert.ToInt32(this.hidd_no.Value);
                var e02data = (from d in model.e02 where d.e02_no == e02_no select d).FirstOrDefault();
                int count = (from dd in model.e04 where dd.e02_no == e02_no && dd.e04_check == "1" select dd).Count();
                this.lab_titile.Text = "以下為報名『" + e02data.e02_name + "第" + e02data.e02_flag + "期』已核可之" + count + "位成員列表";
                this.hidd_name.Value = e02data.e02_name + "第" + e02data.e02_flag + "期簽到冊";
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //列印
        if (this.Get_Checked())
        {
            string arg = this.hidd_checked.Value;
            string e02_no = this.hidd_no.Value;
            Response.Write("<script>newwindow=window.open('300303-8.aspx?e02_no=" + e02_no + "&arg=" + arg + "&Count=" + new Random().Next(1000) + "','new_eip','menubar=yes,scrollbars=yes,resizable=yes, width=700,height=400')</script>");

            //操作記錄
            OperatesObject.OperatesExecute(300303, new SessionObject().sessionUserID, 3, "印列課程簽到冊Excel e02_no:" + e02_no);
        }
        else
        {
            this.ShowMsg("請至少勾選一項欄位!");
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        
        if (this.Get_Checked())
        {
            //Excel下載
            InitializeWorkbook();
            GenerateData();

            string filename = this.hidd_name.Value + ".xls";

            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
            Response.Clear();

            Response.BinaryWrite(WriteToStream().GetBuffer());
            Response.End();

            //操作記錄
            OperatesObject.OperatesExecute(300303, new SessionObject().sessionUserID, 2, "匯出課程簽到冊Excel e02_no:" + this.hidd_no.Value);
        }
        else
        {
            this.ShowMsg("請至少勾選一項欄位!");
        }

    }

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

        string data = "X";
        //單位
        if (isShow[0].Equals("1"))
        {
            data += ","+peo_data.depname;
        }
        //職稱
        if (isShow[1].Equals("1"))
        {
            data += "," + peo_data.proname;
        }
        //姓名
        if (isShow[2].Equals("1"))
        {
            data += "," + peo_data.name;
        }
        //身份證
        if (isShow[3].Equals("1"))
        {
            data += "," + peo_data.idcard;
        }
        //電話
        if (isShow[4].Equals("1"))
        {
            data += "," + peo_data.tel;
        }
        
        return data;

    }

    private bool Get_Checked()
    {
        bool check = false;
        string str = "";
        if (this.cbox_1.Checked)
        {
            str += "1";
            check = true;
        }
        else
        {
            str += "0";
        }
        if (this.cbox_2.Checked)
        {
            str += ",1";
            check = true;
        }
        else
        {
            str += ",0";
        }
        if (this.cbox_3.Checked)
        {
            str += ",1";
            check = true;
        }
        else
        {
            str += ",0";
        }
        if (this.cbox_4.Checked)
        {
            str += ",1";
            check = true;
        }
        else
        {
            str += ",0";
        }
        if (this.cbox_5.Checked)
        {
            str += ",1";
            check = true;
        }
        else
        {
            str += ",0";
        }
        
        this.hidd_checked.Value = str;
        return check;
    }

    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(this.GetUrl("300303.aspx"));
    }
    protected void btn_cancel2_Click(object sender, EventArgs e)
    {
        Response.Redirect(this.GetUrl("300303-3.aspx"));
    }
    private string GetUrl(string tag)
    {
        string url = tag;
        url += "?sdate=" + Request["sdate"];
        url += "&edate=" + Request["edate"];
        url += "&type_1=" + Request["type_1"];
        url += "&type_2=" + Request["type_2"];
        url += "&e01_no=" + Request["e01_no"];
        url += "&e02_name=" + Request["e02_name"];
        url += "&e02_no=" + Request["e02_no"];
        url += "&model=" + Request["model"];
        url += "&pageIndex=" + Request["pageIndex"];
        return url;
    }

    private void ShowMsg(string msg)
    {
        string script = "<script>window.alert('" + msg + "');</script>";
        this.ClientScript.RegisterStartupScript(this.GetType(), "MyScript", script);
    }

    HSSFWorkbook hssfworkbook;

    MemoryStream WriteToStream()
    {
        //Write the stream data of workbook to the root directory
        MemoryStream file = new MemoryStream();
        hssfworkbook.Write(file);
        return file;
    }

    /// <summary>
    /// 建立工作表資料
    /// </summary>
    /// <param name="myTable"></param>
    void GenerateData()
    {
        Sheet sheet1 = hssfworkbook.CreateSheet("Sheet1");
        int e02_no = Convert.ToInt32(this.hidd_no.Value);
        string[] isShow = this.hidd_checked.Value.Split(',');
        string[] colname = { "單位", "姓名", "職稱", "身分證字號", "電話" };

        //表頭
        var e02Data = (from d in model.e02 where d.e02_no == e02_no select d).FirstOrDefault();
        sheet1.CreateRow(0).CreateCell(0).SetCellValue(e02Data.e02_mechani + "\n" + e02Data.e02_name + "第" + e02Data.e02_flag + "期簽到冊");
        

        //日期,地點
        ChangeObject cboj = new ChangeObject();
        string date = cboj._ROCtoROCYMD(cboj._ADtoROC(e02Data.e02_sdate.Value));
        string place = (from t in model.e01 where t.e01_no == e02Data.e01_no select t.e01_name).FirstOrDefault();
        Row row2 = sheet1.CreateRow(1);
        row2.CreateCell(0).SetCellValue("日期：" + date);
        row2.CreateCell(1).SetCellValue("上課地點：" + place);

        //欄位表頭
        int colCount = 0;
        Row row3 = sheet1.CreateRow(2);
        for (int i = 0; i < isShow.Length; i++)
        {
            if (isShow[i].Equals("1"))
            {
                row3.CreateCell(colCount).SetCellValue(colname[i]);
                colCount++;
            }
        }
        //簽到欄
        row3.CreateCell(colCount).SetCellValue("簽到");

        //資料
        //此課程核可人員之UID
        int[] e04_uid = (from dd in model.e04 where dd.e02_no == e02_no && dd.e04_check == "1" orderby dd.e04_no select dd.e04_peouid).ToArray();
        for (int i = 0; i < e04_uid.Length; i++)
        {
            Row row = sheet1.CreateRow(i + 3);
            string[] data = this.dataStr(e04_uid[i], this.hidd_checked.Value).Split(',');
            for (int j = 1; j < data.Length; j++)
            {
                row.CreateCell(j - 1).SetCellValue(data[j]);
            }
        }
    }

    /// <summary>
    /// 工作表初始化
    /// </summary>
    void InitializeWorkbook()
    {
        hssfworkbook = new HSSFWorkbook();

        ////create a entry of DocumentSummaryInformation
        DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
        dsi.Company = "";
        hssfworkbook.DocumentSummaryInformation = dsi;

        ////create a entry of SummaryInformation
        SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
        si.Subject = "";
        hssfworkbook.SummaryInformation = si;
    }
}