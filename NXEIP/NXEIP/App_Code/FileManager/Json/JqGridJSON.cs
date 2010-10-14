using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;

/// <summary>
/// JqGridJSON 的摘要描述
/// 
/// JqGrid使用的物件項目(用來轉換JSON)
/// </summary>
/// 
namespace NXEIP.FileManager.Json
{
    public class JqGridJSON
    {
        public JqGridJSON()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        public int page { get; set; }
        public int total { get; set; }
        public int records { get; set; }

        public ICollection<JqGridItem> rows { get; set; }


    }

    /// <summary>
    /// 表格欄位
    /// </summary>
    public class JqGridItem
    {
        public string id;
        public string[] cell;
    }
    /// <summary>
    /// 將檔案項目的INSTANCE直接轉成JqGrid的欄位
    /// </summary>
    public class FileItem : JqGridItem
    {


        public FileItem(doc01 file, doc02 fileDetial)
        {
            this.cell = new string[5];
            cell[0] = file.d01_file;
            cell[1] = fileDetial.d02_date.ToString();
            cell[2] = fileDetial.d02_KB.ToString();

            cell[3] = fileDetial.d02_format;
            cell[4] = file.d01_url;
            this.id = file.d01_no.ToString();

        }
    }
}