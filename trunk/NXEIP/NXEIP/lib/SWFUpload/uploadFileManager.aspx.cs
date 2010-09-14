using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMMENSITY.SWFUploadAPI;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using lib.SWFUpload;
using Entity;

namespace lib.SWFUpload
{
    public partial class uploadFileManager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string cmd = SWFUrlOper.GetStringParamValue("cmd");
                if (cmd == string.Empty)
                    this.uploadFile();
                else if (cmd == "del")
                    this.DelFile();
            }
        }

        public void uploadFile()
        {
            //保存的路径
            string savePath = SWFUrlOper.GetFormStringParamValue("path");
            //旧文件名,方便删除
            string oldFileName = SWFUrlOper.GetFormStringParamValue("fn");
            //是否需要小图
            bool isSmall = SWFUrlOper.GetFormStringParamValue("small").ToLower() == "true" ? true : false;
            //是否需要水印
            bool isWaterMark = SWFUrlOper.GetFormStringParamValue("wm").ToLower() == "true" ? true : false;
            //小图宽度
            int smallWidth = SWFUrlOper.GetFormIntParamValue("sw");
            //小图高度
            int smallHeight = SWFUrlOper.GetFormIntParamValue("sh");
            //图片扩展名
            string[] imgExtension = new string[] { "jpg", "gif", "png", "bmp" };
            //已上传文件的文件名
            string nameList = SWFUrlOper.GetFormStringParamValue("data");
            //可上傳空間大小(MB)
            double quota = 100;
            try
            {
                // 获取上传的文件信息
                HttpPostedFile file_upload = Request.Files["Filedata"];
                string extension = string.Empty;
                string fileName = string.Empty;
                //bool isImg = false;
                
              
                
                
                
                if (file_upload.ContentLength > 0)
                {
                    int KBsize=(int)Math.Ceiling(((decimal)file_upload.ContentLength)/1024);
                    
                    fileName = file_upload.FileName;
                    if (fileName.IndexOf(".") != -1)
                        extension = fileName.Substring(fileName.LastIndexOf(".") + 1, fileName.Length - fileName.LastIndexOf(".") - 1);

                    SWFUploadFile uf = new SWFUploadFile();

                    
                    //上傳抓資料庫
                    //取上傳目錄
                    ArgumentsObject args = new ArgumentsObject();

                    string path = args.Get_argValue("upload_dir");
                    if (!string.IsNullOrEmpty(path))
                    {
                        uf.Path = path;
                    }



                    //判斷重復檔名
                    NXEIPEntities model = new NXEIPEntities();

                    SessionObject sessionObj = new SessionObject();
                    //要改用COOKIES的值來判斷
                    int peo_uid = System.Convert.ToInt32(sessionObj.sessionUserID);

                    var files = from d in model.doc01
                                where d.peo_uid == peo_uid && d.d01_file.ToLower() == file_upload.FileName.ToLower()
                                select d;
                    if (files.Count() > 0)
                    {
                        Response.StatusCode = 500;
                        Response.Write("檔案重複上傳");
                        HttpContext.Current.ApplicationInstance.CompleteRequest();
                        return;
                    }

                    //判斷TOTAL檔案空間
                   //使用資料庫判斷 減少IO
                    var currentsize = (from d2 in model.doc02
                                         from d in model.doc01
                                         where d.d01_no == d2.d01_no && d.peo_uid == peo_uid
                                         select d2).Sum(c=>c.d02_KB);

                    double total = currentsize.GetValueOrDefault(0) + KBsize;

                    if (((total) / 1024) >= quota) {
                        Response.StatusCode = 500;
                        Response.Write("空間不足");
                        HttpContext.Current.ApplicationInstance.CompleteRequest();
                        return;
                    }

                    
                    if (isSmall)
                    {
                        uf.SmallPic = true;
                        uf.MaxWith = smallWidth == 0 ? uf.MaxWith : smallWidth;
                        uf.MaxHeight = smallHeight == 0 ? uf.MaxHeight : smallHeight;
                    }
                    uf.IsWaterMark = isWaterMark;
                    int state = 0;
                    string newFileName=uf.SaveFile(file_upload, savePath, oldFileName, ref state);
                    //0:上传成功.  1:没有选择要上传的文件.  2:上传文件类型不符.   3:上传文件过大  -1:应用程序错误.
                    int statusCode = 500;
                    switch (state)
                    {
                        case 0:
                            statusCode = 200;
                            break;
                        case 1:
                            statusCode = 200;
                            break;
                        default:
                            statusCode = 500;
                            break;
                    }
                    
                    List<SWFUploadFileInfo> listufi = new List<SWFUploadFileInfo>();
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<SWFUploadFileInfo>));
                    if (nameList != string.Empty)
                        using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(nameList)))
                            listufi = (List<SWFUploadFileInfo>)serializer.ReadObject(ms);//反序列化

                    listufi.Add(new SWFUploadFileInfo()
                    {
                        Id = listufi.Count,
                        FileName = newFileName,
                        OriginalFileName = fileName,
                        Path = savePath,
                        Size=KBsize,
                        Extension=extension
                    });
                    string postData = "";
                    //序列化
                    using (MemoryStream stream = new MemoryStream())
                    {
                        serializer.WriteObject(stream, listufi);
                        postData = Encoding.UTF8.GetString(stream.ToArray());
                    }

                    Response.StatusCode = statusCode;
                    Response.Write(postData);
                }
            }
            catch (Exception e)
            {
                //内部服务器错误
                Response.StatusCode = 500;
                Response.Write("內部服務器錯誤:" + e.Message.ToString());
            }
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <returns></returns>
        public void DelFile()
        {
            string name = SWFUrlOper.GetStringParamValue("name");
            string msg = new SWFUploadFile().Delete(SWFUrlOper.GetStringParamValue("f"), SWFUrlOper.GetStringParamValue("name"), true);
            Response.Write(msg);
            Response.End();
        }
    }
}
