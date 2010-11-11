using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using ICSharpCode.SharpZipLib.Zip.Compression;

namespace Compress
{
    /// <summary>
    /// BasePage 的摘要描述
    /// </summary>
    public class BasePage : Page
    {
        public BasePage()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }


        /// <summary>
        /// 設定序列化後的字串長度為多少後啟用壓縮
        /// </summary>
        private static Int32 LimitLength = 10240;

        /// <summary>
        /// 設定壓縮比率，壓縮比率越高性消耗也將增大
        /// </summary>
        private static Int32 ZipLevel = ICSharpCode.SharpZipLib.Zip.Compression.Deflater.BEST_COMPRESSION;

        /// <summary>
        /// override掉Page中原來的SavePageStateToPersistenceMedium()
        /// </summary>
        /// <param name="pViewState">ViewState物件</param>
        protected override void SavePageStateToPersistenceMedium(Object pViewState)
        {
            //實現一個用於將資訊寫入字串的 TextWriter
            StringWriter mWriter = new StringWriter();

            //序列化 Web Page檢視狀態
            LosFormatter mFormat = new LosFormatter();
            mFormat.Serialize(mWriter, pViewState);

            //將序列化後的物件轉成Base64字串
            String vStateStr = mWriter.ToString();

            //設置是否啟用了加密方式，預設情況下為不啟用
            Boolean mUseZip = false;

            //判斷序列化物件的字串長度是否超出10K
            if (vStateStr.Length > LimitLength)
            {
                //如果ViewState大於30K就進行壓縮，同時將狀態設為加密方式
                mUseZip = true;

                Byte[] pBytes = Compress(vStateStr);

                //將位元組陣列轉換為Base64字串
                vStateStr = System.Convert.ToBase64String(pBytes);
            }

            //將壓縮後的ViewState存放到隱藏欄位中
            ClientScript.RegisterHiddenField("__MSPVSTATE", vStateStr);

            //將是否啟用壓縮狀態存放到隱藏欄位中
            ClientScript.RegisterHiddenField("__MSPVSTATE_ZIP", mUseZip.ToString().ToLower());
        }

        /// <summary>
        /// 對字串進行壓縮
        /// </summary>
        /// <param name="pViewState">ViewState字串</param>
        /// <returns>返回流的位元組陣列</returns>
        public static Byte[] Compress(String pViewState)
        {
            //將字串轉換為位元組陣列
            Byte[] pBytes = System.Convert.FromBase64String(pViewState);

            //建立記憶體的Stream
            MemoryStream mMemory = new MemoryStream();

            Deflater mDeflater = new Deflater(ZipLevel);
            ICSharpCode.SharpZipLib.Zip.Compression.Streams.DeflaterOutputStream mStream = new ICSharpCode.SharpZipLib.Zip.Compression.Streams.DeflaterOutputStream(mMemory, mDeflater, 131072);

            mStream.Write(pBytes, 0, pBytes.Length);
            mStream.Close();

            return mMemory.ToArray();
        }

        /// <summary>
        /// 重寫將所有保存的視圖狀態資訊載入到頁面物件
        /// </summary>
        /// <returns>保存的ViewState</returns>
        protected override Object LoadPageStateFromPersistenceMedium()
        {
            //使用Request方法獲取序列化的ViewState字串
            String mViewState = this.Request.Form.Get("__MSPVSTATE");
            //使用Request方法獲取當前的ViewState是否啟用了壓縮
            String mViewStateZip = this.Request.Form.Get("__MSPVSTATE_ZIP");

            Byte[] pBytes;

            //如果有壓縮的話，才解壓縮，否則就直接轉成位元組
            if (mViewStateZip == "true")
            {
                pBytes = DeCompress(mViewState);
            }
            else
            {
                //將ViewState的Base64字串轉換成位元組
                pBytes = System.Convert.FromBase64String(mViewState);
            }

            //序列化 Web 表單頁的ViewState
            LosFormatter mFormat = new LosFormatter();

            //將指定的檢視狀態值轉換為有限物件序列化 (LOS) 格式化的物件
            return mFormat.Deserialize(System.Convert.ToBase64String(pBytes));
        }

        /// <summary>
        /// 解壓縮ViewState字串
        /// </summary>
        /// <param name="pViewState">ViewState字串</param>
        /// <returns>返回流的位元組陣列</returns>
        public static Byte[] DeCompress(String pViewState)
        {
            //將Base64字串轉換為位元組陣列
            Byte[] pBytes = System.Convert.FromBase64String(pViewState);

            ICSharpCode.SharpZipLib.Zip.Compression.Streams.InflaterInputStream mStream = new ICSharpCode.SharpZipLib.Zip.Compression.Streams.InflaterInputStream(new MemoryStream(pBytes));

            //創建支援記憶體存儲的流
            MemoryStream mMemory = new MemoryStream();
            Int32 mSize;

            Byte[] mWriteData = new Byte[4096];

            while (true)
            {
                mSize = mStream.Read(mWriteData, 0, mWriteData.Length);
                if (mSize > 0)
                {
                    mMemory.Write(mWriteData, 0, mSize);
                }
                else
                {
                    break;
                }
            }

            mStream.Close();
            return mMemory.ToArray();
        }
    }
}