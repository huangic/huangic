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

        /// <summary>
        /// 做目錄判斷
        /// </summary>
        /// <param name="file"></param>
        /// <param name="fileDetial"></param>
        public FileItem(doc01 file, doc02 fileDetial)
        {
            this.cell = new string[6];
            
            cell[0] = file.d01_file;
            
            cell[1] = fileDetial.d02_date.ToString();
            
            cell[2] = fileDetial.d02_KB.ToString();

            
            cell[3] = fileDetial.d02_format;
            
            cell[4] = file.d01_url;
            //權限判斷
            cell[5] = bool.TrueString;
            this.id = file.d01_no.ToString();

        }


        public static FileItem GetPermissionFileItem(doc01 file, doc02 fileDetial, int peo_uid)
        {
            FileItem f = new FileItem(file, fileDetial);
            
            //權限欄位判斷
            if(file.peo_uid!=peo_uid){
                    f.cell[5]=bool.FalseString;
            }

            return f;
        }
    }
}