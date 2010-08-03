using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;

/// <summary>
/// JqGridJSON 的摘要描述
/// </summary>
/// 
namespace FileManager
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

    public class JqGridItem
    {
        public string id;
        public string[] cell;
    }

    public class FileItem : JqGridItem
    {


        public FileItem(doc01 file, doc02 fileDetial)
        {
            this.cell = new string[4];
            cell[0] = file.d01_file;
            cell[1] = fileDetial.d02_date.ToString();
            cell[2] = fileDetial.d02_KB.ToString();

            cell[3] = fileDetial.d02_format;

        }
    }
}