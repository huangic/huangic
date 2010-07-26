using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace IMMENSITY.SWFUploadAPI
{
    [Serializable]
    public class SWFUploadFileInfo
    {

        /// <summary>
        /// 编号(唯一)
        /// </summary>
        public int Id;
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName = string.Empty;
        /// <summary>
        /// 原始文件名
        /// </summary>
        public string OriginalFileName = string.Empty;
        /// <summary>
        /// 文件存放路径
        /// </summary>
        public string Path = string.Empty;
    }
}
