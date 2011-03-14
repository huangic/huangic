﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMMENSITY.SWFUploadAPI;
using lib.SWFUpload;


//TODO:PostBack 的檔案清單要儲存狀態 其他目錄下的JS呼叫路徑(未測試)
    

namespace lib.SWFUpload
{
    public partial class UC_SWFUpload : System.Web.UI.UserControl
    {

        protected override void OnInit(EventArgs e)
        {

            Page.RegisterRequiresControlState(this);
            base.OnInit(e);
        }
        
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Bind();
                this.hidIdList.Value = string.Empty;
            }

            
        }

        protected override void OnPreRender(EventArgs e)
        {
            ScriptManager.RegisterClientScriptInclude(this, typeof(UserControl), "JQuery", ResolveClientUrl("~/js/jquery-1.4.2.min.js"));
            ScriptManager.RegisterClientScriptInclude(this, typeof(UserControl), "SWFUploadScriptCore", ResolveClientUrl("~/js/swfupload/swfupload.js"));
            ScriptManager.RegisterClientScriptInclude(this, typeof(UserControl), "SWFUploadScriptHandlers", ResolveClientUrl("~/js/swfupload/handlers.js"));
            ScriptManager.RegisterClientScriptInclude(this, typeof(UserControl), "SWFUploadScriptJson", ResolveClientUrl("~/js/json2.js"));

            base.OnPreRender(e);
        }
        public SWFUploadInfo SwfUploadInfo { get; set; }
        private void Bind()
        {
            StringBuilder sbHtml = new StringBuilder();
            if (SwfUploadInfo == null) { SwfUploadInfo = new SWFUploadInfo(); }
            this.hidON.Value = SwfUploadInfo.OldFileName;
            if (SwfUploadInfo.UploadMode == UpMode.LIST)
            {
                sbHtml.Append("\r\n");
                sbHtml.Append("<div class=\"uploadpic\">\r\n");
                sbHtml.Append(" <div class=\"upbatch\">\r\n");
                sbHtml.Append("     <!-- 选择图片[over] -->\r\n");
                sbHtml.Append("     <div id=\"swfupload_header\" class=\"swfupload_header\">\r\n");
                sbHtml.Append("         <span class=\"swfupload_pic_name\">檔案名稱</span> <span class=\"swfupload_pic_state\">上傳狀態</span>\r\n");
                sbHtml.Append("         <div class=\"swfupload_pic_option\">\r\n");
                sbHtml.Append("             上傳進度</div>\r\n");
                sbHtml.Append("         <span class=\"swfupload_pic_percent\">檔案操作</span>\r\n");
                sbHtml.Append("     </div>\r\n");
                sbHtml.Append("     <ul id=\"" + GetControlId("divFileProgressContainer") + "\" class=\"swfupload_main\">\r\n");
                sbHtml.Append("         <!--载入图片文件列表-->\r\n");
                sbHtml.Append("     </ul>\r\n");
                sbHtml.Append(" </div>\r\n");
                sbHtml.Append(" <div style=\"text-align: right; margin: 5px 0 -5px 0;\">\r\n");
                sbHtml.Append("     <span id=\"" + GetControlId("spanButtonPlaceholder") + "\"></span>\r\n");
                sbHtml.Append(" </div>\r\n");
                sbHtml.Append(" </div>\r\n");
                sbHtml.Append(" <div id=\"thumbnails\">\r\n");
                sbHtml.Append(" </div>\r\n");
            }
            else
            {
                sbHtml.Append("<div>\r\n");
                sbHtml.Append("    <div id=\"" + GetControlId("divFileProgressContainer") + "\"></div>\r\n");
                sbHtml.Append("    <span id=\"" + GetControlId("spanButtonPlaceholder") + "\"></span>\r\n");
                sbHtml.Append("</div>\r\n");
            }
            this.lalHtml.Text = sbHtml.ToString();
        }
        /// <summary>
        /// 获取与SWFUpload控件组合后的控件Id
        /// </summary>
        /// <param name="id">副Id</param>
        /// <returns></returns>
        protected string GetControlId(string id)
        {
            return string.Format("{0}_{1}", this.ID, id.Trim());
        }

        /// <summary>
        /// 文件相对路径集合(上传多文件时使用)
        /// </summary>
        public string[] FilePathList
        {
            get
            {
                List<SWFUploadFileInfo> list = this.SWFUploadFileInfoList;
                if (list.Count > 0)
                    return (from lt in list select lt.Path).ToArray();
                return new string[] { };
            }
        }
        /// <summary>
        /// 文件相对路径
        /// </summary>
        public string FilePath
        {
            get
            {
                List<SWFUploadFileInfo> list = this.SWFUploadFileInfoList;
                if (list.Count > 0)
                    return list[0].Path;
                return string.Empty;
            }
        }
        /// <summary>
        /// 文件名集合(上传多文件时使用)
        /// </summary>
        public string[] FileNameList
        {
            get
            {
                List<SWFUploadFileInfo> list = this.SWFUploadFileInfoList;
                if (list.Count > 0)
                    return (from lt in list select lt.FileName).ToArray();
                return new string[] { };
            }
        }
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName
        {
            get
            {
                List<SWFUploadFileInfo> list = this.SWFUploadFileInfoList;
                if (list.Count > 0)
                    return list[0].FileName;
                return this.hidON.Value;
            }
        }

        /// <summary>
        /// 获取JSON反序列化数据
        /// </summary>
        public List<SWFUploadFileInfo> SWFUploadFileInfoList
        {
            get
            {
                string json = this.hidIdList.Value;
                List<SWFUploadFileInfo> listUFI = new List<SWFUploadFileInfo>();
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<SWFUploadFileInfo>));
                if (json != string.Empty)
                    using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
                        listUFI = (List<SWFUploadFileInfo>)serializer.ReadObject(ms);//反序列化
                return listUFI;
            }
        }

        /// <summary>
        /// 清除目前上傳項目
        /// </summary>
        public void ClearFile() { 
        
            //this.SwfUploadInfo.PathArg
            SWFUploadFile uf = new SWFUploadFile();
            //設定刪除
            String rootDir = new ArgumentsObject().Get_argValue(this.SwfUploadInfo.PathArg);
            
            foreach (var f in this.SWFUploadFileInfoList)
            {

                String del_msg = uf.Delete(rootDir + f.Path, f.FileName, true);
            }



        }

    }
}