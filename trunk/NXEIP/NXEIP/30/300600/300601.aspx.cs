using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;
using NLog;
using System.Data;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.HPSF;
using NPOI.HSSF.Util;
using System.Web.UI.DataVisualization;

public partial class _30_300600_300601 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.calendar1._ADDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-01-01"));
            this.calendar2._ADDate = DateTime.Now;

            //叫修分類管理者
            this.ObjectDataSource2.SelectParameters["peo_uid"].DefaultValue = new SessionObject().sessionUserID;
            this.lv_cat.DataBind();
            if (this.lv_cat.Items.Count > 0)
            {
                this.hidd_r05no.Value = this.lv_cat.DataKeys[0].Value.ToString();
                this.loadData();
            }

            //圖表
            this.div_chart1.Style.Add("display", "none");

        }

        if (Request["__EVENTTARGET"] == this.UpdatePanel1.ClientID && String.IsNullOrEmpty(Request["__EVENTARGUMENT"]))
        {
            this.GridView1.DataBind();
        }
    }

    private void loadData()
    {
        this.ObjectDataSource1.SelectParameters["r05_no"].DefaultValue = this.hidd_r05no.Value;
        this.ObjectDataSource1.SelectParameters["sd"].DefaultValue = this.calendar1._ADDate.ToString("yyyy-MM-dd 00:00:00");
        this.ObjectDataSource1.SelectParameters["ed"].DefaultValue = this.calendar2._ADDate.ToString("yyyy-MM-dd 23:59:59");
        this.GridView1.DataBind();
    }

    protected void lv_cat_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        if (e.CommandName == "click_cat")
        {
            this.hidd_r05no.Value = this.lv_cat.DataKeys[e.Item.DataItemIndex].Value.ToString();
            this.loadData();
        }
        
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int r02_no = int.Parse(this.GridView1.DataKeys[int.Parse(e.CommandArgument.ToString())].Value.ToString());

        if (e.CommandName.Equals("del"))
        {
            _100403DAO dao = new _100403DAO();
            rep02 d = dao.GetRep02ByNo(r02_no);
            d.r02_status = "4";
            d.r02_createuid = int.Parse(new SessionObject().sessionUserID);
            d.r02_createtime = DateTime.Now;
            dao.UpData();
            OperatesObject.OperatesExecute(300601, 4, "刪除叫修紀錄 r02_no:" + r02_no);
            this.GridView1.DataBind();
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            UtilityDAO dao = new UtilityDAO();

            e.Row.Cells[0].Text = dao.Get_DepartmentName(Convert.ToInt32(e.Row.Cells[0].Text));

            int peo_uid = Convert.ToInt32(e.Row.Cells[1].Text);

            e.Row.Cells[1].Text = dao.Get_PeopleName(peo_uid) + "(" + dao.Get_PeopleExtension(peo_uid) + ")";

            DateTime date = Convert.ToDateTime(e.Row.Cells[2].Text);

            e.Row.Cells[2].Text = new ChangeObject()._ADtoROC(date) + " " + date.ToString("HH:mm");

        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        if (this.calendar1._ADDate > this.calendar2._ADDate)
        {
            this.ShowMsg("起始日期需小於迄日期");
        }
        else
        {
            this.loadData();
        }
    }
    private void ShowMsg(string msg)
    {
        string script = "<script>window.alert('" + msg + "');</script>";
        this.ClientScript.RegisterStartupScript(this.GetType(), "MSG", script);
    }
    /// <summary>
    /// 下載Excel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        HSSFWorkbook workbook = new HSSFWorkbook();
        MemoryStream ms = new MemoryStream();
        Sheet sheet1 = workbook.CreateSheet("sheet");
        bool download = false;

        using (NXEIPEntities model = new NXEIPEntities())
        {
            string[] status = { "1", "2", "3" };

            int r05_no = int.Parse(this.hidd_r05no.Value);

            DataTable dt = new DataTable();
            dt.Columns.Add("Title");
            dt.Columns.Add("Count");

            int row_index = 0, pic_row = 0, total = 0;

            //子分類集合
            var rep06Data = from d in model.rep06
                            where d.r05_no == r05_no && d.r06_parent == 0 && d.r06_status == "1"
                            orderby d.r06_order
                            select d;
            if (rep06Data.Count() > 0)
            {
                download = true;
            }

            //圖片物件
            HSSFPatriarch patriarch = (HSSFPatriarch)sheet1.CreateDrawingPatriarch();

            //子子分類集合
            foreach (var r in rep06Data)
            {
                //圖片位置
                pic_row = row_index;

                var rep06Sub = from d in model.rep06
                               where d.r06_parent == r.r06_no && d.r06_status == "1"
                               orderby d.r06_order
                               select d;
                //清空表格
                dt.Rows.Clear();

                foreach (var rr in rep06Sub)
                {
                    //維修件數
                    int rep02_count = (from d in model.rep02
                                       where d.r06_no == rr.r06_no && status.Contains(d.r02_status)
                                       select d).Count();
                    DataRow row = dt.NewRow();
                    row["Title"] = rr.r06_name;
                    row["Count"] = rep02_count.ToString();
                    dt.Rows.Add(row);
                }

                int rep02_sum = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    rep02_sum += int.Parse(dt.Rows[i]["Count"].ToString());
                }
                //總數
                total += rep02_sum;

                if (dt.Rows.Count > 0)
                {
                    //加入工作表
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Row sheetRow = sheet1.CreateRow(row_index);
                        if (i == 0)
                        {
                            sheetRow.CreateCell(0).SetCellValue(r.r06_name);
                        }
                        sheetRow.CreateCell(1).SetCellValue(dt.Rows[i]["Title"].ToString());
                        sheetRow.CreateCell(2).SetCellValue(dt.Rows[i]["Count"].ToString());
                        sheetRow.CreateCell(3).SetCellValue(this.Percentage(rep02_sum, int.Parse(dt.Rows[i]["Count"].ToString())));
                        if (i == dt.Rows.Count - 1)
                        {
                            sheetRow.CreateCell(4).SetCellValue(rep02_sum.ToString());
                            row_index++;
                            sheet1.CreateRow(row_index).CreateCell(3).SetCellValue("100%");
                            row_index++;
                        }
                        row_index++;
                    }

                    //產生圖片資料流
                    MemoryStream msPic = this.CreatePic(dt);

                    // 在 workbook 加入圖片Binary，注意格式
                    int pictureIdx = workbook.AddPicture(msPic.ToArray(), PictureType.PNG);

                    // 加入圖片 因為後面會呼叫 Resize()，所以 HSSFClientAnchor 填入 row1 的值即可
                    HSSFClientAnchor anchor = new HSSFClientAnchor(0, 0, 0, 0, 6, pic_row, 8, pic_row + 2);
                    HSSFPicture pict = (HSSFPicture)patriarch.CreatePicture(anchor, pictureIdx);

                    // 將圖片回復成原來的大小
                    pict.Resize();
                }
            }

            //總數加入工作表
            sheet1.CreateRow(row_index).CreateCell(4).SetCellValue(total.ToString());
            
        }

        if (download)
        {

            // 將 workbook 寫入 MemoryStream，準備匯出
            workbook.Write(ms);

            //匯出
            Response.AddHeader("Content-Disposition", string.Format("attachment; filename="
                                                    + Guid.NewGuid().ToString() + ".xls"));
            Response.BinaryWrite(ms.ToArray());
        }
        else
        {
            JsUtil.AlertJs(this, "查無子項目類別!!");
        }

        workbook = null;
        ms.Close();
        ms.Dispose();
        
    }

    private MemoryStream CreatePic(DataTable dt)
    {
        MemoryStream msPic = new MemoryStream();
        Chart1.Series[0].XValueMember = dt.Columns[0].ColumnName;
        Chart1.Series[0].YValueMembers = dt.Columns[1].ColumnName;
        Chart1.DataSource = dt;
        Chart1.DataBind();
        // 取得 Chart 圖片檔，注意格式
        Chart1.SaveImage(msPic, System.Web.UI.DataVisualization.Charting.ChartImageFormat.Png);
        return msPic;
    }

    private string Percentage(int sum, int num)
    {
        if (num == 0 || sum == 0)
        {
            return "0.0%";
        }
        else
        {
            double avg = sum / num * 100;
            return avg.ToString("#.#") + "%";
        }
    }

   
}