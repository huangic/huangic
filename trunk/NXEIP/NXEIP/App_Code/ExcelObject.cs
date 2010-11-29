using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.HPSF;


/// <summary>
/// ExcelObject 的摘要描述
/// </summary>
public class ExcelObject
{
	public ExcelObject()
	{
		
	}

    /// <summary>
    /// 回傳Excel資料流
    /// </summary>
    /// <param name="dt">資料表</param>
    /// <returns></returns>
    public MemoryStream ExportExcel(DataTable dt)
    {
        InitializeWorkbook();
        GenerateData(dt);
        return WriteToStream();
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
    void GenerateData(DataTable myTable)
    {
        Sheet sheet1 = hssfworkbook.CreateSheet("sheet1");

        //抬頭名稱
        Row titleRow = sheet1.CreateRow(0);
        for (int i = 0; i < myTable.Columns.Count; i++)
        {
            titleRow.CreateCell(i).SetCellValue(myTable.Columns[i].ColumnName);
        }
        
        //資料
        for (int i = 0; i < myTable.Rows.Count; i++)
        {
            Row row = sheet1.CreateRow(i + 1);
            for (int j = 0; j < myTable.Columns.Count; j++)
            {
                row.CreateCell(j).SetCellValue(myTable.Rows[i][j].ToString());
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