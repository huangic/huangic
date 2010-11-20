using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IMMENSITY.SWFUploadAPI;
using System.Web.UI.WebControls;
using System.IO;


namespace NXEIP.DAO
{
    /// <summary>
    /// TreatFile 的摘要描述
    /// </summary>
    /// 
    [Serializable]
    public class TreatFile
    {
        public TreatFile()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }


        public TreatFile(FileUpload fu) {
            this.Extension = Path.GetExtension(fu.FileName);
            this.OriginalFileName = fu.FileName;
            this.FileName = this.NewFileName + Extension;
            this.Size = fu.FileBytes.Count();
        }

        private string NewFileName=DateTime.Now.ToString("yMdhhmmssfff");
        public String FileName { get; set; }

        public String OriginalFileName { get; set; }
        public String Extension { get;set; }

        public String FilePath{get;set;}

        public int Size{get;set;}

        
        public static List<TreatFile> ConvertUploadFile(List<SWFUploadFileInfo> files)
        {
            List<TreatFile> l = new List<TreatFile>();

            foreach (var f in files)
            {
                l.Add(new TreatFile() { FileName = f.OriginalFileName });
            }

            return l;
        }

    }
}